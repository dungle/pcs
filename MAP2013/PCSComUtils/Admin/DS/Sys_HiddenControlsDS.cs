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
	public class Sys_HiddenControlsDS 
	{
		public Sys_HiddenControlsDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.Sys_HiddenControlsDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_HiddenControls
		///    </Description>
		///    <Inputs>
		///        Sys_HiddenControlsVO       
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
		///       Wednesday, April 06, 2005
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
				Sys_HiddenControlsVO objObject = (Sys_HiddenControlsVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_HiddenControls("
				+ Sys_HiddenControlsTable.FORMNAME_FLD + ","
				+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
				+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.CONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.SUBCONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.SUBCONTROLNAME_FLD].Value = objObject.SubControlName;


				
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
		///       This method uses to delete data from Sys_HiddenControls
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
			strSql=	"DELETE " + Sys_HiddenControlsTable.TABLE_NAME + " WHERE  " + "HiddenControlsID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_HiddenControls
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_HiddenControlsVO
		///    </Outputs>
		///    <Returns>
		///       Sys_HiddenControlsVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, April 06, 2005
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
				+ Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD + ","
				+ Sys_HiddenControlsTable.FORMNAME_FLD + ","
				+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
				+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD
				+ " FROM " + Sys_HiddenControlsTable.TABLE_NAME
				+" WHERE " + Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_HiddenControlsVO objObject = new Sys_HiddenControlsVO();

				while (odrPCS.Read())
				{ 
				objObject.HiddenControlsID = int.Parse(odrPCS[Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD].ToString().Trim());
				objObject.FormName = odrPCS[Sys_HiddenControlsTable.FORMNAME_FLD].ToString().Trim();
				objObject.ControlName = odrPCS[Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString().Trim();
				objObject.SubControlName = odrPCS[Sys_HiddenControlsTable.SUBCONTROLNAME_FLD].ToString().Trim();

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
		///       This method uses to update data to Sys_HiddenControls
		///    </Description>
		///    <Inputs>
		///       Sys_HiddenControlsVO       
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

			Sys_HiddenControlsVO objObject = (Sys_HiddenControlsVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_HiddenControls SET "
				+ Sys_HiddenControlsTable.FORMNAME_FLD + "=   ?" + ","
				+ Sys_HiddenControlsTable.CONTROLNAME_FLD + "=   ?" + ","
				+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD + "=  ?"
				+" WHERE " + Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.CONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.SUBCONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.SUBCONTROLNAME_FLD].Value = objObject.SubControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD].Value = objObject.HiddenControlsID;


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
		///       This method uses to get all data from Sys_HiddenControls
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
		///       Wednesday, April 06, 2005
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
				+ Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD + ","
				+ Sys_HiddenControlsTable.FORMNAME_FLD + ","
				+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
				+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD
					+ " FROM " + Sys_HiddenControlsTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_HiddenControlsTable.TABLE_NAME);

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
		///       Wednesday, April 06, 2005
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
				+ Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD + ","
				+ Sys_HiddenControlsTable.FORMNAME_FLD + ","
				+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
				+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD 
		+ "  FROM " + Sys_HiddenControlsTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_HiddenControlsTable.TABLE_NAME);

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
		public bool GetHiddenControl(string pstrFormName,string pstrControlName,string pstrSubControl,string[] pstrRoleIDs)
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
					strRoleIDs = pstrRoleIDs[i] + ",";
				}
				strRoleIDs = strRoleIDs.Substring(0,strRoleIDs.Length - 1);
				strSql=	"SELECT COUNT(*)"
					+ " FROM " + Sys_HiddenControls_RoleTable.TABLE_NAME + " HR"
					+ " INNER JOIN "  + Sys_HiddenControlsTable.TABLE_NAME + " H"
					+ " ON HR." + Sys_HiddenControls_RoleTable.HIDDENCONTROLSID_FLD + "=" + " H." + Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD
					+ " WHERE H." + Sys_HiddenControlsTable.FORMNAME_FLD + "='" + pstrFormName + "'"
					+ " AND H." + Sys_HiddenControlsTable.CONTROLNAME_FLD + "='" + pstrControlName + "'"
					+ " AND HR." + Sys_HiddenControls_RoleTable.ROLEID_FLD + " IN (" + strRoleIDs + ")";
				if(pstrSubControl != string.Empty)
				{
					strSql += " AND H." + Sys_HiddenControlsTable.SUBCONTROLNAME_FLD + "='" + pstrSubControl + "'";
				}

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				int intCount = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return (intCount > 0 ? true : false);
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
		public DataTable GetVisibleControl(string pstrFormName,string pstrControlName,string pstrSubControl,string[] pstrRoleIDs)
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
				strSql=	"SELECT " + Sys_HiddenControlsTable.FORMNAME_FLD + ","
					+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
					+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD
					+ " FROM " + Sys_HiddenControls_RoleTable.TABLE_NAME + " HR"
					+ " INNER JOIN "  + Sys_HiddenControlsTable.TABLE_NAME + " H"
					+ " ON HR." + Sys_HiddenControls_RoleTable.HIDDENCONTROLSID_FLD + "=" + " H." + Sys_HiddenControlsTable.HIDDENCONTROLSID_FLD
					+ " WHERE H." + Sys_HiddenControlsTable.FORMNAME_FLD + "='" + pstrFormName + "'"
					+ " AND HR." + Sys_HiddenControls_RoleTable.ROLEID_FLD + " IN (" + strRoleIDs + ")";
				if(pstrControlName != string.Empty)
				{
					strSql += " AND H." + Sys_HiddenControlsTable.CONTROLNAME_FLD + "='" + pstrControlName + "'";
				}
				if(pstrSubControl != string.Empty)
				{
					strSql += " AND H." + Sys_HiddenControlsTable.SUBCONTROLNAME_FLD + "='" + pstrSubControl + "'";
				}
				strSql += " GROUP BY " + Sys_HiddenControlsTable.FORMNAME_FLD + ","
					+ Sys_HiddenControlsTable.CONTROLNAME_FLD + ","
					+ Sys_HiddenControlsTable.SUBCONTROLNAME_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_HiddenControlsTable.TABLE_NAME);
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
