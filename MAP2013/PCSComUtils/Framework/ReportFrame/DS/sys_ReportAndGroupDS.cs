using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportAndGroupDS 
	{
		public sys_ReportAndGroupDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportAndGroupDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///        sys_ReportAndGroupVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       Modified: DungLa - 30/12/2004
		///    </History>
		///    <Notes>
		///    Change data type from database using Integer istead of VarChar
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				sys_ReportAndGroupVO objObject = (sys_ReportAndGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				
				strSql=	"INSERT INTO " + sys_ReportAndGroupTable.TABLE_NAME + "("
					+ sys_ReportAndGroupTable.GROUPID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTORDER_FLD + ")"
					+ " VALUES (?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTORDER_FLD].Value = objObject.ReportOrder;

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
		///       This method uses to delete data from sys_ReportAndGroup
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + sys_ReportAndGroupTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportAndGroupTable.GROUPID_FLD + " = ? ";// + pstrID + "'";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrID;
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
		///       This method uses to delete data from sys_ReportAndGroup
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public void DeleteByReportID(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + sys_ReportAndGroupTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportAndGroupTable.REPORTID_FLD + "= ? ";// + pstrReportID + "'";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTID_FLD].Value = pstrReportID;
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
		///       This method uses to get data from sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportAndGroupVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportAndGroupVO
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
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
				+ sys_ReportAndGroupTable.GROUPID_FLD + ","
				+ sys_ReportAndGroupTable.REPORTID_FLD + ","
				+ sys_ReportAndGroupTable.REPORTORDER_FLD
				+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
				+ " WHERE " + sys_ReportAndGroupTable.REPORTANDGROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportAndGroupVO objObject = new sys_ReportAndGroupVO();

				while (odrPCS.Read())
				{
					objObject.ReportAndGroupID = odrPCS[sys_ReportAndGroupTable.REPORTANDGROUPID_FLD].ToString().Trim();
					objObject.GroupID = odrPCS[sys_ReportAndGroupTable.GROUPID_FLD].ToString().Trim();
					objObject.ReportID = odrPCS[sys_ReportAndGroupTable.REPORTID_FLD].ToString().Trim();
					objObject.ReportOrder = int.Parse(odrPCS[sys_ReportAndGroupTable.REPORTORDER_FLD].ToString().Trim());
				}		
				return objObject;					
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get data from sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportAndGroupVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportAndGroupVO
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(string pstrReportID, string pstrGroupID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ sys_ReportAndGroupTable.REPORTANDGROUPID_FLD + ","
					+ sys_ReportAndGroupTable.GROUPID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTORDER_FLD
					+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
					+ " WHERE " + sys_ReportAndGroupTable.REPORTID_FLD + "= ? "
					+ " AND " + sys_ReportAndGroupTable.GROUPID_FLD + "= ? ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportAndGroupVO objObject = new sys_ReportAndGroupVO();

				while (odrPCS.Read())
				{
					objObject.ReportAndGroupID = odrPCS[sys_ReportAndGroupTable.REPORTANDGROUPID_FLD].ToString().Trim();
					objObject.GroupID = odrPCS[sys_ReportAndGroupTable.GROUPID_FLD].ToString().Trim();
					objObject.ReportID = odrPCS[sys_ReportAndGroupTable.REPORTID_FLD].ToString().Trim();
					objObject.ReportOrder = int.Parse(odrPCS[sys_ReportAndGroupTable.REPORTORDER_FLD].ToString().Trim());
				}		
				return objObject;					
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to update data to sys_ReportAndGroup
		///    </Description>
		///    <Inputs>
		///       sys_ReportAndGroupVO       
		///    </Inputs>
		///    <Outputs>
		///      N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportAndGroupVO objObject = (sys_ReportAndGroupVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"UPDATE " + sys_ReportAndGroupTable.TABLE_NAME + " SET "
					+ sys_ReportAndGroupTable.GROUPID_FLD + "= ?" + ","
					+ sys_ReportAndGroupTable.REPORTID_FLD + "= ?" + ","
					+ sys_ReportAndGroupTable.REPORTORDER_FLD + "= ?"
					+ " WHERE " + sys_ReportAndGroupTable.REPORTANDGROUPID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = objObject.GroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTORDER_FLD].Value = objObject.ReportOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTANDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTANDGROUPID_FLD].Value = objObject.ReportAndGroupID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get all data from sys_ReportAndGroup
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
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
					+ sys_ReportAndGroupTable.REPORTANDGROUPID_FLD + ","
					+ sys_ReportAndGroupTable.GROUPID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTORDER_FLD
					+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
					+ " ORDER BY " + sys_ReportAndGroupTable.GROUPID_FLD
					+ "," + sys_ReportAndGroupTable.REPORTORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportAndGroupTable.TABLE_NAME);

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
		///       This method uses to delete data from sys_ReportGroup
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
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
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
					+ sys_ReportAndGroupTable.REPORTANDGROUPID_FLD + ","
					+ sys_ReportAndGroupTable.GROUPID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTID_FLD + ","
					+ sys_ReportAndGroupTable.REPORTORDER_FLD
					+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
					+ " ORDER BY " + sys_ReportAndGroupTable.GROUPID_FLD
					+ "," + sys_ReportAndGroupTable.REPORTORDER_FLD;


				Utils utils = new Utils();

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);

				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportAndGroupTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
					}
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get GroupID throught ReportID
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///       GroupID
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30 - Dec 0 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetGroupIDByReportID(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetGroupIDByReportID()";
			string strSql = "SELECT  " + sys_ReportAndGroupTable.GROUPID_FLD
				+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
				+ " WHERE " + sys_ReportAndGroupTable.REPORTID_FLD + "= ? ";// + pstrReportID + "'";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();

				return ocmdPCS.ExecuteScalar().ToString().Trim();
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get rows inside the group
		///    </Description>
		///    <Inputs>
		///        GroupID       
		///    </Inputs>
		///    <Outputs>
		///       row
		///    </Outputs>
		///    <Returns>
		///       array
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30 - Dec - 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs(string pstrGroupID)
		{
			ArrayList arrObjects = new ArrayList();
			const string METHOD_NAME = THIS + ".GetObjectVOs()";
			string strSql = "SELECT  "
				+ sys_ReportAndGroupTable.REPORTANDGROUPID_FLD
				+ "," + sys_ReportAndGroupTable.GROUPID_FLD
				+ "," + sys_ReportAndGroupTable.REPORTID_FLD
				+ "," + sys_ReportAndGroupTable.REPORTORDER_FLD
				+ " FROM " + sys_ReportAndGroupTable.TABLE_NAME
				+ " WHERE " + sys_ReportAndGroupTable.GROUPID_FLD + "= ? "  
				+ " ORDER BY " + sys_ReportAndGroupTable.REPORTORDER_FLD;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();
				while (odrPCS.Read())
				{ 
					sys_ReportAndGroupVO objAndVO = new sys_ReportAndGroupVO();
					objAndVO.ReportAndGroupID = odrPCS[sys_ReportAndGroupTable.REPORTANDGROUPID_FLD].ToString().Trim();
					objAndVO.GroupID = odrPCS[sys_ReportAndGroupTable.GROUPID_FLD].ToString().Trim();
					objAndVO.ReportID = odrPCS[sys_ReportAndGroupTable.REPORTID_FLD].ToString().Trim();
					objAndVO.ReportOrder = int.Parse(odrPCS[sys_ReportAndGroupTable.REPORTORDER_FLD].ToString().Trim());

					arrObjects.Add(objAndVO);
				}
				arrObjects.TrimToSize();
				return arrObjects;					
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get the max value of last row in sys_table 
		///    </Description>
		///    <Inputs>
		///        GroupID      
		///    </Inputs>
		///    <Outputs>
		///      Max Table Order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxReportOrder(string pstrGroupID)
		{
			const string METHOD_NAME = THIS + ".GetMaxReportOrder()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ISNULL(MAX(" 
					+ sys_ReportAndGroupTable.REPORTORDER_FLD + "),0)" 
					+ " FROM  " + sys_ReportAndGroupTable.TABLE_NAME 
					+ " WHERE " + sys_ReportAndGroupTable.GROUPID_FLD + " = ? ";// + pstrGroupID + "'";

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.Connection.Open();

				// return the max order
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		}
	}
}
