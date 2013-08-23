using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class Sys_FieldGroupDS 
	{
		public Sys_FieldGroupDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.Sys_FieldGroupDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_FieldGroup
		///    </Description>
		///    <Inputs>
		///        Sys_FieldGroupVO       
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
		///       Saturday, September 17, 2005
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
				Sys_FieldGroupVO objObject = (Sys_FieldGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_FieldGroup("
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPCODE_FLD].Value = objObject.GroupCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPLEVEL_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPLEVEL_FLD].Value = objObject.GroupLevel;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEVN_FLD].Value = objObject.GroupNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEEN_FLD].Value = objObject.GroupNameEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEJP_FLD].Value = objObject.GroupNameJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Value = objObject.ParentFieldGroupID;


				
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
		///       This method uses to delete data from Sys_FieldGroup
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
			strSql=	"DELETE " + Sys_FieldGroupTable.TABLE_NAME + " WHERE  " + "FieldGroupID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_FieldGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_FieldGroupVO
		///    </Outputs>
		///    <Returns>
		///       Sys_FieldGroupVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Saturday, September 17, 2005
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
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME
					+" WHERE " + Sys_FieldGroupTable.FIELDGROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_FieldGroupVO objObject = new Sys_FieldGroupVO();

				while (odrPCS.Read())
				{ 
					objObject.FieldGroupID = int.Parse(odrPCS[Sys_FieldGroupTable.FIELDGROUPID_FLD].ToString().Trim());
					objObject.GroupCode = odrPCS[Sys_FieldGroupTable.GROUPCODE_FLD].ToString().Trim();
					objObject.GroupOrder = int.Parse(odrPCS[Sys_FieldGroupTable.GROUPORDER_FLD].ToString().Trim());
					objObject.GroupLevel = int.Parse(odrPCS[Sys_FieldGroupTable.GROUPLEVEL_FLD].ToString().Trim());
					objObject.GroupNameVN = odrPCS[Sys_FieldGroupTable.GROUPNAMEVN_FLD].ToString().Trim();
					objObject.GroupNameEN = odrPCS[Sys_FieldGroupTable.GROUPNAMEEN_FLD].ToString().Trim();
					objObject.GroupNameJP = odrPCS[Sys_FieldGroupTable.GROUPNAMEJP_FLD].ToString().Trim();
					objObject.ReportID = odrPCS[Sys_FieldGroupTable.REPORTID_FLD].ToString().Trim();
					objObject.ParentFieldGroupID = int.Parse(odrPCS[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].ToString().Trim());

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
		///       This method uses to update data to Sys_FieldGroup
		///    </Description>
		///    <Inputs>
		///       Sys_FieldGroupVO       
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

			Sys_FieldGroupVO objObject = (Sys_FieldGroupVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_FieldGroup SET "
					+ Sys_FieldGroupTable.GROUPCODE_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + "=   ?" + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD + "=  ?"
					+" WHERE " + Sys_FieldGroupTable.FIELDGROUPID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPCODE_FLD].Value = objObject.GroupCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPLEVEL_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPLEVEL_FLD].Value = objObject.GroupLevel;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEVN_FLD].Value = objObject.GroupNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEEN_FLD].Value = objObject.GroupNameEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEJP_FLD].Value = objObject.GroupNameJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Value = objObject.ParentFieldGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.FIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.FIELDGROUPID_FLD].Value = objObject.FieldGroupID;


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
		///       This method uses to get all data from Sys_FieldGroup
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
		///       Saturday, September 17, 2005
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
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_FieldGroupTable.TABLE_NAME);

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
		///       Saturday, September 17, 2005
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
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD 
					+ "  FROM " + Sys_FieldGroupTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_FieldGroupTable.TABLE_NAME);

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
		///       This method uses to get all data from Sys_FieldGroup
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
		///       Saturday, September 17, 2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable List(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupTable.REPORTID_FLD + "= ?"
					+ " ORDER BY " + Sys_FieldGroupTable.GROUPORDER_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = pstrReportID;
				
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_FieldGroupTable.TABLE_NAME);

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


		/// <summary>
		/// Get all lower group of a Group
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pintFieldGroupID">Field Group ID</param>
		/// <returns>DataTable</returns>
		public DataTable GetLowerGroup(int pintFieldGroupID)
		{
			const string METHOD_NAME = THIS + ".GetLowerGroup()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql=	"SELECT "
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD + "= ?"
					+ " ORDER BY " + Sys_FieldGroupTable.GROUPORDER_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Value = pintFieldGroupID;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_FieldGroupTable.TABLE_NAME);

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

		/// <summary>
		/// Get all field of a group
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pintFieldGroupID">Field Group ID</param>
		/// <param name="pstrReportID">Report ID</param>
		/// <returns>DataTable</returns>
		public DataTable GetFields(int pintFieldGroupID, string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetFields()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql=	"SELECT "
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? "
					+ " AND " + sys_ReportFieldsTable.FIELDNAME_FLD + " IN ("
					+ " SELECT " + Sys_FieldGroupDetailTable.FIELDNAME_FLD
					+ " FROM " + Sys_FieldGroupDetailTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + "=" + pintFieldGroupID + ") "
					+ " ORDER BY " + sys_ReportFieldsTable.FIELDORDER_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportFieldsTable.TABLE_NAME);

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

		/// <summary>
		/// Get all field which not belong to any field group
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pstrReportId">Report ID</param>
		/// <returns>DataTable</returns>
		public DataTable GetAvailableFields(string pstrReportId)
		{
			const string METHOD_NAME = THIS + ".GetAvailableFields()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{	
				string strSql=	"SELECT "
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? "
					+ " AND " + sys_ReportFieldsTable.FIELDNAME_FLD + " NOT IN ("
					+ " SELECT " + Sys_FieldGroupDetailTable.FIELDNAME_FLD
					+ " FROM " + Sys_FieldGroupDetailTable.TABLE_NAME + ") "
					+ " ORDER BY " + sys_ReportFieldsTable.FIELDORDER_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportId;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportFieldsTable.TABLE_NAME);

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

		/// <summary>
		/// Add new object and return id
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pobjFieldGroup">Object to add</param>
		/// <returns>New ID</returns>
		public int AddAndReturnID(object pobjFieldGroup)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_FieldGroupVO objObject = (Sys_FieldGroupVO) pobjFieldGroup;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_FieldGroup("
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";
				strSql += " ; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPCODE_FLD].Value = objObject.GroupCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPLEVEL_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPLEVEL_FLD].Value = objObject.GroupLevel;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEVN_FLD].Value = objObject.GroupNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEEN_FLD].Value = objObject.GroupNameEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.GROUPNAMEJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.GROUPNAMEJP_FLD].Value = objObject.GroupNameJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD, OleDbType.Integer));
				if (objObject.ParentFieldGroupID > 0)
					ocmdPCS.Parameters[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Value = objObject.ParentFieldGroupID;
				else
					ocmdPCS.Parameters[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].Value = DBNull.Value;
			
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null && objResult != DBNull.Value)
					return int.Parse(objResult.ToString());
				else
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

		/// <summary>
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pstrReportID"></param>
		/// <param name="pintParentFieldGroupID"></param>
		/// <returns></returns>
		public int GetFieldGroupOrder(string pstrReportID, int pintParentFieldGroupID)
		{
			const string METHOD_NAME = THIS + ".GetFieldGroupOrder()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT MAX(ISNULL("
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ", 0))"
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupTable.REPORTID_FLD + "= ? ";
				if (pintParentFieldGroupID > 0)
					strSql += " AND " + Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD + "=" + pintParentFieldGroupID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				/// 12/Oct/2005 Thachnn: fix bug injection
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = pstrReportID;
				
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null && objResult != DBNull.Value)
					return int.Parse(objResult.ToString());
				else
					return 0;
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
				if (oconPCS != null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Get all field group of a report
		/// </summary>
		/// <param name="pstrReportID">Report ID</param>
		/// <returns>List of field group</returns>
		public ArrayList GetAllGroups(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetAllGroups()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_FieldGroupTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupTable.GROUPCODE_FLD + ","
					+ Sys_FieldGroupTable.GROUPORDER_FLD + ","
					+ Sys_FieldGroupTable.GROUPLEVEL_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEVN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEEN_FLD + ","
					+ Sys_FieldGroupTable.GROUPNAMEJP_FLD + ","
					+ Sys_FieldGroupTable.REPORTID_FLD + ","
					+ Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD
					+ " FROM " + Sys_FieldGroupTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupTable.REPORTID_FLD + "=?"
					+ " ORDER BY " + Sys_FieldGroupTable.GROUPORDER_FLD + "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupTable.REPORTID_FLD].Value = pstrReportID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ArrayList arrResult = new ArrayList();
				while (odrPCS.Read())
				{ 
					Sys_FieldGroupVO objObject = new Sys_FieldGroupVO();
					objObject.FieldGroupID = int.Parse(odrPCS[Sys_FieldGroupTable.FIELDGROUPID_FLD].ToString().Trim());
					objObject.GroupCode = odrPCS[Sys_FieldGroupTable.GROUPCODE_FLD].ToString().Trim();
					objObject.GroupOrder = int.Parse(odrPCS[Sys_FieldGroupTable.GROUPORDER_FLD].ToString().Trim());
					objObject.GroupLevel = int.Parse(odrPCS[Sys_FieldGroupTable.GROUPLEVEL_FLD].ToString().Trim());
					objObject.GroupNameVN = odrPCS[Sys_FieldGroupTable.GROUPNAMEVN_FLD].ToString().Trim();
					objObject.GroupNameEN = odrPCS[Sys_FieldGroupTable.GROUPNAMEEN_FLD].ToString().Trim();
					objObject.GroupNameJP = odrPCS[Sys_FieldGroupTable.GROUPNAMEJP_FLD].ToString().Trim();
					objObject.ReportID = odrPCS[Sys_FieldGroupTable.REPORTID_FLD].ToString().Trim();
					if (odrPCS[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD] != DBNull.Value)
						objObject.ParentFieldGroupID = int.Parse(odrPCS[Sys_FieldGroupTable.PARENTFIELDGROUPID_FLD].ToString().Trim());

					arrResult.Add(objObject);
				}
				arrResult.TrimToSize();
				return arrResult;
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
