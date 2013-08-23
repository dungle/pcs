using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_VisibilityGroupDS 
	{
		public Sys_VisibilityGroupDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.Sys_VisibilityGroupDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_VisibilityGroup
		///    </Description>
		///    <Inputs>
		///        Sys_VisibilityGroupVO       
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
				Sys_VisibilityGroupVO objObject = (Sys_VisibilityGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_VisibilityGroup("
				+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
				+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
				+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
				+ Sys_VisibilityGroupTable.TYPE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.CONTROLNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.PARENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.PARENTID_FLD].Value = objObject.ParentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.FORMNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXT_FLD].Value = objObject.GroupText;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTVN_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTVN_FLD].Value = objObject.GroupTextVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTJP_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTJP_FLD].Value = objObject.GroupTextJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.TYPE_FLD].Value = objObject.Type;


				
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
		///       This method uses to delete data from Sys_VisibilityGroup
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
			strSql=	"DELETE " + Sys_VisibilityGroupTable.TABLE_NAME + " WHERE  " + "VisibilityGroupID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_VisibilityGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_VisibilityGroupVO
		///    </Outputs>
		///    <Returns>
		///       Sys_VisibilityGroupVO
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
				+ Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + ","
				+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
				+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
				+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
				+ Sys_VisibilityGroupTable.TYPE_FLD
				+ " FROM " + Sys_VisibilityGroupTable.TABLE_NAME
				+" WHERE " + Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_VisibilityGroupVO objObject = new Sys_VisibilityGroupVO();

				while (odrPCS.Read())
				{ 
				objObject.VisibilityGroupID = int.Parse(odrPCS[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].ToString().Trim());
				objObject.ControlName = odrPCS[Sys_VisibilityGroupTable.CONTROLNAME_FLD].ToString().Trim();
				objObject.ParentID = int.Parse(odrPCS[Sys_VisibilityGroupTable.PARENTID_FLD].ToString().Trim());
				objObject.FormName = odrPCS[Sys_VisibilityGroupTable.FORMNAME_FLD].ToString().Trim();
				objObject.GroupText = odrPCS[Sys_VisibilityGroupTable.GROUPTEXT_FLD].ToString().Trim();
				objObject.GroupTextVN = odrPCS[Sys_VisibilityGroupTable.GROUPTEXTVN_FLD].ToString().Trim();
				objObject.GroupTextJP = odrPCS[Sys_VisibilityGroupTable.GROUPTEXTJP_FLD].ToString().Trim();
				objObject.Type = int.Parse(odrPCS[Sys_VisibilityGroupTable.TYPE_FLD].ToString().Trim());

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
		///       This method uses to update data to Sys_VisibilityGroup
		///    </Description>
		///    <Inputs>
		///       Sys_VisibilityGroupVO       
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

			Sys_VisibilityGroupVO objObject = (Sys_VisibilityGroupVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_VisibilityGroup SET "
				+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.PARENTID_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.FORMNAME_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + "=   ?" + ","
				+ Sys_VisibilityGroupTable.TYPE_FLD + "=  ?"
				+" WHERE " + Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.CONTROLNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.PARENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.PARENTID_FLD].Value = objObject.ParentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.FORMNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXT_FLD].Value = objObject.GroupText;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTVN_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTVN_FLD].Value = objObject.GroupTextVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTJP_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTJP_FLD].Value = objObject.GroupTextJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD].Value = objObject.VisibilityGroupID;


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
		///       This method uses to get all data from Sys_VisibilityGroup
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
				+ Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + ","
				+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
				+ "ISNULL(" + Sys_VisibilityGroupTable.PARENTID_FLD + ",0) PARENTID,"
				+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
				+ Sys_VisibilityGroupTable.TYPE_FLD
					+ " FROM " + Sys_VisibilityGroupTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				//odadPCS.FillSchema()
				odadPCS.Fill(dstPCS,Sys_VisibilityGroupTable.TABLE_NAME);

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
				+ Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + ","
				+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
				+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
				+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
				+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
				+ Sys_VisibilityGroupTable.TYPE_FLD 
		+ "  FROM " + Sys_VisibilityGroupTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_VisibilityGroupTable.TABLE_NAME);

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
		
		public void UpdateDataSetRoleAndItem(DataSet pData)
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
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityItemTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityItemTable.TABLE_NAME]);

				strSql=	"SELECT "
					+ Sys_VisibilityGroup_RoleTable.VISIBILITYGROUP_ROLEID_FLD + ","
					+ Sys_VisibilityGroup_RoleTable.GROUPID_FLD + ","
					+ Sys_VisibilityGroup_RoleTable.ROLEID_FLD 
					+ "  FROM " + Sys_VisibilityGroup_RoleTable.TABLE_NAME;
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME]);

				strSql=	"SELECT "
					+ Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + ","
					+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
					+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
					+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
					+ Sys_VisibilityGroupTable.TYPE_FLD 
					+ "  FROM " + Sys_VisibilityGroupTable.TABLE_NAME;

				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME]);

				pData.AcceptChanges();

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

		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			int intID;
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_VisibilityGroupVO objObject = (Sys_VisibilityGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_VisibilityGroup("
					+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
					+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
					+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
					+ Sys_VisibilityGroupTable.TYPE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?) "
					+ "SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.CONTROLNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.PARENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.PARENTID_FLD].Value = objObject.ParentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.FORMNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXT_FLD].Value = objObject.GroupText;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTVN_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTVN_FLD].Value = objObject.GroupTextVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.GROUPTEXTJP_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.GROUPTEXTJP_FLD].Value = objObject.GroupTextJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibilityGroupTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibilityGroupTable.TYPE_FLD].Value = objObject.Type;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				intID = Convert.ToInt32(ocmdPCS.ExecuteScalar().ToString());
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
			return intID;
		}

		public void UpdateAllDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ Sys_VisibilityGroupTable.VISIBILITYGROUPID_FLD + ","
					+ Sys_VisibilityGroupTable.CONTROLNAME_FLD + ","
					+ Sys_VisibilityGroupTable.PARENTID_FLD + ","
					+ Sys_VisibilityGroupTable.FORMNAME_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXT_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTVN_FLD + ","
					+ Sys_VisibilityGroupTable.GROUPTEXTJP_FLD + ","
					+ Sys_VisibilityGroupTable.TYPE_FLD 
					+ "  FROM " + Sys_VisibilityGroupTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityGroupTable.TABLE_NAME]);

				strSql=	"SELECT "
					+ Sys_VisibilityItemTable.VISIBILITYITEMID_FLD + ","
					+ Sys_VisibilityItemTable.NAME_FLD + ","
					+ Sys_VisibilityItemTable.GROUPID_FLD + ","
					+ Sys_VisibilityItemTable.TYPE_FLD 
					+ "  FROM " + Sys_VisibilityItemTable.TABLE_NAME;
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityItemTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityItemTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityItemTable.TABLE_NAME]);

				strSql=	"SELECT "
					+ Sys_VisibilityGroup_RoleTable.VISIBILITYGROUP_ROLEID_FLD + ","
					+ Sys_VisibilityGroup_RoleTable.GROUPID_FLD + ","
					+ Sys_VisibilityGroup_RoleTable.ROLEID_FLD 
					+ "  FROM " + Sys_VisibilityGroup_RoleTable.TABLE_NAME;
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = true;
				odadPCS.Update(pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME]);
				pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME]);

				pData.AcceptChanges();
			}
//			catch(OleDbException ex)
//			{
//				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
//				{
//					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
//				}
//				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
//				{
//
//					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
//				}
//				
//				else
//				{
//					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//				}
//			}			

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
		///       This method uses to get hidden control
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
		///       sonht
		///    </Authors>
		///    <History>
		///       Wednesday, April 12, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetVisibleControl(string pstrFormName,string pstrControlName,string[] pstrRoleIDs)
		{
			const string METHOD_NAME = THIS + ".GetHiddenControl()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				string strRoleIDs = string.Empty;
				for(int i = 0; i < pstrRoleIDs.Length; i++)
				{
					strRoleIDs += pstrRoleIDs[i] + ",";
				}
				strRoleIDs = strRoleIDs.Substring(0,strRoleIDs.Length - 1);
//				strSql=	"SELECT VC." + Sys_VisibleControlTable.FORMNAME_FLD + ", VC."
//					+ Sys_VisibleControlTable.CONTROLNAME_FLD + ", VC."
//					+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD
//					+ " FROM " + Sys_Role_ControlGroupTable.TABLE_NAME + " RCG"
//					+ " INNER JOIN "  + Sys_ControlGroupTable.TABLE_NAME + " CG"
//					+ " ON RCG." + Sys_Role_ControlGroupTable.CONTROLGROUPID_FLD + "=" + " CG." + Sys_ControlGroupTable.CONTROLGROUPID_FLD
//					+ " INNER JOIN "  + Sys_VisibleControlTable.TABLE_NAME + " VC"
//					+ " ON CG." + Sys_ControlGroupTable.CONTROLGROUPID_FLD + "=" + " VC." + Sys_ControlGroupTable.CONTROLGROUPID_FLD
//					+ " WHERE VC." + Sys_VisibleControlTable.FORMNAME_FLD + "='" + pstrFormName + "'"
//					+ " AND RCG." + Sys_Role_ControlGroupTable.ROLEID_FLD + " IN (" + strRoleIDs + ")";
				
				strSql = "SELECT VG.FormName,VI.Name ControlName, ISNULL(VI.Type,0) Type" 
					+ " FROM sys_VisibilityGroup_Role VGR"
					+ " INNER JOIN sys_VisibilityGroup VG ON VGR.GroupID=VG.VisibilityGroupID"
					+ " INNER JOIN sys_VisibilityItem VI ON VG.VisibilityGroupID=VI.GroupID"
					+ " WHERE VG.FormName = '" + pstrFormName + "'"
					+ " AND VGR.RoleID IN (" + strRoleIDs + ")";
				if(pstrControlName != string.Empty)
				{
					strSql += " AND VI.Name = '" + pstrControlName + "'";
				}
				// HACK: SonHT 2005-11-08
				if(pstrFormName != string.Empty)
				{
					strSql += " AND VG.FormName = '" + pstrFormName + "'";
				}

				strSql += " ORDER BY VG.FormName, VI.Name";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_VisibleControlTable.TABLE_NAME);
				return dstPCS.Tables[0];
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
