using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_RightDS 
	{
		public Sys_RightDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.DS.Sys_RightDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_RightVO objObject = (Sys_RightVO) pobjObjectVO;
				string strSql = String.Empty;
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_Right("
					+ Sys_RightTable.PERMISSION_FLD + ","
					+ Sys_RightTable.ROLEID_FLD + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.PERMISSION_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.PERMISSION_FLD].Value = objObject.Permission;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.ROLEID_FLD].Value = objObject.RoleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.MENU_ENTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.MENU_ENTRYID_FLD].Value = objObject.Menu_EntryID;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from Sys_Right
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
		///       Code generate
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
			strSql=	"DELETE " + Sys_RightTable.TABLE_NAME + " WHERE  " + "RightID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

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
		///       This method uses to get data from Sys_Right
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_RightVO
		///    </Outputs>
		///    <Returns>
		///       Sys_RightVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, January 17, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_RightTable.RIGHTID_FLD + ","
					+ Sys_RightTable.PERMISSION_FLD + ","
					+ Sys_RightTable.ROLEID_FLD + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD
					+ " FROM " + Sys_RightTable.TABLE_NAME
					+" WHERE " + Sys_RightTable.RIGHTID_FLD + "=" + pintID;

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_RightVO objObject = new Sys_RightVO();

				while (odrPCS.Read())
				{ 
					objObject.RightID = int.Parse(odrPCS[Sys_RightTable.RIGHTID_FLD].ToString());
					objObject.Permission = int.Parse(odrPCS[Sys_RightTable.PERMISSION_FLD].ToString());
					objObject.RoleID = int.Parse(odrPCS[Sys_RightTable.ROLEID_FLD].ToString());
					objObject.Menu_EntryID = int.Parse(odrPCS[Sys_RightTable.MENU_ENTRYID_FLD].ToString());

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
		///       This method uses to update data to Sys_Right
		///    </Description>
		///    <Inputs>
		///       Sys_RightVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Code Generate 
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

			Sys_RightVO objObject = (Sys_RightVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_Right SET "
					+ Sys_RightTable.PERMISSION_FLD + "=   ?" + ","
					+ Sys_RightTable.ROLEID_FLD + "=   ?" + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD + "=  ?"
					+" WHERE " + Sys_RightTable.RIGHTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.PERMISSION_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.PERMISSION_FLD].Value = objObject.Permission;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.ROLEID_FLD].Value = objObject.RoleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.MENU_ENTRYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.MENU_ENTRYID_FLD].Value = objObject.Menu_EntryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RightTable.RIGHTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RightTable.RIGHTID_FLD].Value = objObject.RightID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all data from Sys_Right
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
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, January 17, 2005
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
					+ Sys_RightTable.RIGHTID_FLD + ","
					+ Sys_RightTable.PERMISSION_FLD + ","
					+ Sys_RightTable.ROLEID_FLD + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD
					+ " FROM " + Sys_RightTable.TABLE_NAME;
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RightTable.TABLE_NAME);

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
		///       Code Generate
		///    </Authors>
		///    <History>
		///       Monday, January 17, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				string strSql=	" SELECT "
					+ Sys_RightTable.RIGHTID_FLD + ","
					+ Sys_RightTable.PERMISSION_FLD + ","
					+ Sys_RightTable.ROLEID_FLD + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD +
					" FROM " + Sys_RightTable.TABLE_NAME;
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,Sys_RightTable.TABLE_NAME);
				pdstData.AcceptChanges();
				pdstData.Tables[Sys_RightTable.TABLE_NAME].Clear();
				odadPCS.Fill(pdstData.Tables[Sys_RightTable.TABLE_NAME]);
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all right for a Role and ProtectObj
		///    </Description>
		///    <Inputs>
		///       int pRoleID        
		///       int pProtectObjID
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanMD
		///    </Authors>
		///    <History>
		///       Thursday, 13 - January , 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pRoleID, int pProtectObjID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_RightTable.RIGHTID_FLD + ","
					+ Sys_RightTable.PERMISSION_FLD + ","
					+ Sys_RightTable.ROLEID_FLD + ","
					+ Sys_RightTable.MENU_ENTRYID_FLD
					+ " FROM " + Sys_RightTable.TABLE_NAME
					+ " WHERE "+ Sys_RightTable.ROLEID_FLD + " = " + pRoleID + " and " + Sys_RightTable.MENU_ENTRYID_FLD + " = " + pProtectObjID ;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_RightVO objObject = new Sys_RightVO();

				while (odrPCS.Read())
				{ 
					objObject.RightID = int.Parse(odrPCS[Sys_RightTable.RIGHTID_FLD].ToString().Trim());
					objObject.Permission = int.Parse(odrPCS[Sys_RightTable.PERMISSION_FLD].ToString());
					objObject.RoleID = int.Parse(odrPCS[Sys_RightTable.ROLEID_FLD].ToString().Trim());
					objObject.Menu_EntryID = int.Parse(odrPCS[Sys_RightTable.MENU_ENTRYID_FLD].ToString().Trim());
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
		///       This method uses to get all rights assigned to a role
		///    </Description>
		///    <Inputs>
		///    pintRoleID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       number of row deleted
		///    </Returns>
		///    <Authors>
		///       DuongNA
		///    </Authors>
		///    <History>
		///       9/10/2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int DeleteRightsOfRole(string pstrRoleIDs)
		{
			const string METHOD_NAME = THIS + ".DeleteRightsOfRole()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			int nRes = 0;
			try 
			{
				string strSql = string.Empty;
				strSql=	"DELETE "
					+ Sys_RightTable.TABLE_NAME
					+ " WHERE " + Sys_RightTable.ROLEID_FLD + " IN (" + pstrRoleIDs + ")";

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
	
				nRes = ocmdPCS.ExecuteNonQuery();

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
			return nRes;
		}
		/// <summary>
		/// Remove right of Role to Menu
		/// </summary>
		/// <param name="pintRoleID">Role ID</param>
		/// <param name="pintMenuID">Menu ID</param>
		public void Delete(int pintRoleID, int pintMenuID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + Sys_RightTable.TABLE_NAME
				+ " WHERE  " + Sys_RightTable.ROLEID_FLD + "=" + pintRoleID
				+ " AND " + Sys_RightTable.MENU_ENTRYID_FLD + "=" + pintMenuID;
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrAddedRoleName"></param>
		public void InsertDefaultMenu(string pstrAddedRoleName)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			
			strSql=	" INSERT INTO Sys_Right(RoleID,Menu_EntryID,Permission)"
				+ " SELECT RoleID,Menu_EntryID,1"
				+ " FROM (SELECT RoleID FROM Sys_Role WHERE Name IN (" + pstrAddedRoleName + ") ) Role,"
				+ " (SELECT DISTINCT "
				+ "A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD
				+ " FROM " + Sys_Menu_EntryTable.TABLE_NAME + " A " 
				+ " INNER JOIN " + Sys_Menu_EntryTable.TABLE_NAME + " B " 
				+ " ON A." + Sys_Menu_EntryTable.SHORTCUT_FLD + " = B." + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD
				+ " WHERE A." + Sys_Menu_EntryTable.TYPE_FLD + " IN (" + ((int)MenuTypeEnum.VisibleBoth).ToString() + "," + ((int)MenuTypeEnum.VisibleMenu).ToString() + ")"
				+ " AND B." + Sys_Menu_EntryTable.TYPE_FLD + " IN (" + ((int)MenuTypeEnum.VisibleBoth).ToString() + "," + ((int)MenuTypeEnum.VisibleMenu).ToString() + ")"
				+ " AND B." + Sys_Menu_EntryTable.PARENT_CHILD_FLD + " = " + ((int)MenuParentChildEnum.SpecialLeafMenu).ToString()
				+ " UNION "
				+ " SELECT " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD
				+ " FROM " + Sys_Menu_EntryTable.TABLE_NAME
				+ " WHERE " + Sys_Menu_EntryTable.PARENT_CHILD_FLD + "=" + ((int)MenuParentChildEnum.SpecialLeafMenu).ToString() + ") Menu";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				
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
