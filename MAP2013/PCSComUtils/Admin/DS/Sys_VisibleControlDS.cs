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
	public class Sys_VisibleControlDS 
	{
		public Sys_VisibleControlDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.Sys_VisibleControlDS";

	
		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_VisibleControlVO objObject = (Sys_VisibleControlVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_VisibleControl("
				+ Sys_VisibleControlTable.FORMNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLGROUPID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.CONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.SUBCONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.SUBCONTROLNAME_FLD].Value = objObject.SubControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.CONTROLGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibleControlTable.CONTROLGROUPID_FLD].Value = objObject.ControlGroupID;


				
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
	

	

		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + Sys_VisibleControlTable.TABLE_NAME + " WHERE  " + "VisibleControlID" + "=" + pintID.ToString();
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
	

	

		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>

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
				+ Sys_VisibleControlTable.VISIBLECONTROLID_FLD + ","
				+ Sys_VisibleControlTable.FORMNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLGROUPID_FLD
				+ " FROM " + Sys_VisibleControlTable.TABLE_NAME
				+" WHERE " + Sys_VisibleControlTable.VISIBLECONTROLID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_VisibleControlVO objObject = new Sys_VisibleControlVO();

				while (odrPCS.Read())
				{ 
				objObject.VisibleControlID = int.Parse(odrPCS[Sys_VisibleControlTable.VISIBLECONTROLID_FLD].ToString().Trim());
				objObject.FormName = odrPCS[Sys_VisibleControlTable.FORMNAME_FLD].ToString().Trim();
				objObject.ControlName = odrPCS[Sys_VisibleControlTable.CONTROLNAME_FLD].ToString().Trim();
				objObject.SubControlName = odrPCS[Sys_VisibleControlTable.SUBCONTROLNAME_FLD].ToString().Trim();
				objObject.ControlGroupID = int.Parse(odrPCS[Sys_VisibleControlTable.CONTROLGROUPID_FLD].ToString().Trim());

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


		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			Sys_VisibleControlVO objObject = (Sys_VisibleControlVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_VisibleControl SET "
				+ Sys_VisibleControlTable.FORMNAME_FLD + "=   ?" + ","
				+ Sys_VisibleControlTable.CONTROLNAME_FLD + "=   ?" + ","
				+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD + "=   ?" + ","
				+ Sys_VisibleControlTable.CONTROLGROUPID_FLD + "=  ?"
				+" WHERE " + Sys_VisibleControlTable.VISIBLECONTROLID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.CONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.CONTROLNAME_FLD].Value = objObject.ControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.SUBCONTROLNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_VisibleControlTable.SUBCONTROLNAME_FLD].Value = objObject.SubControlName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.CONTROLGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibleControlTable.CONTROLGROUPID_FLD].Value = objObject.ControlGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_VisibleControlTable.VISIBLECONTROLID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_VisibleControlTable.VISIBLECONTROLID_FLD].Value = objObject.VisibleControlID;


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


		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>

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
				+ Sys_VisibleControlTable.VISIBLECONTROLID_FLD + ","
				+ Sys_VisibleControlTable.FORMNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLGROUPID_FLD
					+ " FROM " + Sys_VisibleControlTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_VisibleControlTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to Sys_VisibleControl
		///    </summary>
		///    <Inputs>
		///        Sys_VisibleControlVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Monday, July 11, 2005
		///    </History>
		
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ Sys_VisibleControlTable.VISIBLECONTROLID_FLD + ","
				+ Sys_VisibleControlTable.FORMNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD + ","
				+ Sys_VisibleControlTable.CONTROLGROUPID_FLD 
		+ "  FROM " + Sys_VisibleControlTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,Sys_VisibleControlTable.TABLE_NAME);

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
				strSql=	"SELECT VC." + Sys_VisibleControlTable.FORMNAME_FLD + ", VC."
					+ Sys_VisibleControlTable.CONTROLNAME_FLD + ", VC."
					+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD
					+ " FROM " + Sys_Role_ControlGroupTable.TABLE_NAME + " RCG"
					+ " INNER JOIN "  + Sys_ControlGroupTable.TABLE_NAME + " CG"
					+ " ON RCG." + Sys_Role_ControlGroupTable.CONTROLGROUPID_FLD + "=" + " CG." + Sys_ControlGroupTable.CONTROLGROUPID_FLD
					+ " INNER JOIN "  + Sys_VisibleControlTable.TABLE_NAME + " VC"
					+ " ON CG." + Sys_ControlGroupTable.CONTROLGROUPID_FLD + "=" + " VC." + Sys_ControlGroupTable.CONTROLGROUPID_FLD
					+ " WHERE VC." + Sys_VisibleControlTable.FORMNAME_FLD + "='" + pstrFormName + "'"
					+ " AND RCG." + Sys_Role_ControlGroupTable.ROLEID_FLD + " IN (" + strRoleIDs + ")";
				if(pstrControlName != string.Empty)
				{
					strSql += " AND VC." + Sys_VisibleControlTable.CONTROLNAME_FLD + "='" + pstrControlName + "'";
				}
				if(pstrSubControl != string.Empty)
				{
					strSql += " AND VC." + Sys_VisibleControlTable.SUBCONTROLNAME_FLD + "='" + pstrSubControl + "'";
				}
				strSql += " GROUP BY " + Sys_VisibleControlTable.FORMNAME_FLD + ","
					+ Sys_VisibleControlTable.CONTROLNAME_FLD + ","
					+ Sys_VisibleControlTable.SUBCONTROLNAME_FLD;

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
