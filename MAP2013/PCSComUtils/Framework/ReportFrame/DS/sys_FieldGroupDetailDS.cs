using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class Sys_FieldGroupDetailDS 
	{
		public Sys_FieldGroupDetailDS()
		{
		}
		private const string THIS = ".Sys_FieldGroupDetailDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_FieldGroupDetail
		///    </Description>
		///    <Inputs>
		///        Sys_FieldGroupDetailVO       
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
				Sys_FieldGroupDetailVO objObject = (Sys_FieldGroupDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_FieldGroupDetail("
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD + ")"
					+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.FIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.FIELDGROUPID_FLD].Value = objObject.FieldGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.FIELDNAME_FLD].Value = objObject.FieldName;
				
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
		///       This method uses to delete data from Sys_FieldGroupDetail
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
			strSql=	"DELETE " + Sys_FieldGroupDetailTable.TABLE_NAME + " WHERE  " + "FieldGroupDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_FieldGroupDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_FieldGroupDetailVO
		///    </Outputs>
		///    <Returns>
		///       Sys_FieldGroupDetailVO
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
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD
					+ " FROM " + Sys_FieldGroupDetailTable.TABLE_NAME
					+" WHERE " + Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_FieldGroupDetailVO objObject = new Sys_FieldGroupDetailVO();

				while (odrPCS.Read())
				{ 
					objObject.FieldGroupDetailID = int.Parse(odrPCS[Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD].ToString().Trim());
					objObject.FieldGroupID = int.Parse(odrPCS[Sys_FieldGroupDetailTable.FIELDGROUPID_FLD].ToString().Trim());
					objObject.ReportID = odrPCS[Sys_FieldGroupDetailTable.REPORTID_FLD].ToString().Trim();
					objObject.FieldName = odrPCS[Sys_FieldGroupDetailTable.FIELDNAME_FLD].ToString().Trim();
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
		///       This method uses to update data to Sys_FieldGroupDetail
		///    </Description>
		///    <Inputs>
		///       Sys_FieldGroupDetailVO       
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

			Sys_FieldGroupDetailVO objObject = (Sys_FieldGroupDetailVO) pobjObjecVO;			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_FieldGroupDetail SET "
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + "=   ?" + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + "=   ?" + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD + "=  ?"
					+" WHERE " + Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.FIELDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.FIELDGROUPID_FLD].Value = objObject.FieldGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.FIELDNAME_FLD].Value = objObject.FieldName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD].Value = objObject.FieldGroupDetailID;

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
		///       This method uses to get all data from Sys_FieldGroupDetail
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
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				string strSql = "SELECT "
					+ Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD
					+ " FROM " + Sys_FieldGroupDetailTable.TABLE_NAME;
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_FieldGroupDetailTable.TABLE_NAME);

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
					+ Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD 
					+ "  FROM " + Sys_FieldGroupDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_FieldGroupDetailTable.TABLE_NAME);

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
		///       This method uses to delete data from Sys_FieldGroupDetail
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(int pintFieldGroupID, string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;			

			try
			{	
				Utils utils = new Utils();							
				string strSql = "DELETE " + Sys_FieldGroupDetailTable.TABLE_NAME 
					+ " WHERE " + Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + "=" + pintFieldGroupID.ToString()
					+ " AND " + Sys_FieldGroupDetailTable.REPORTID_FLD + "= ?";				
								
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);			

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.REPORTID_FLD].Value = pstrReportID;

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
	
		/// <summary>
		/// Get All field and group of a report
		/// </summary>
		/// <param name="pstrReportID">ReportID</param>
		/// <returns>List of sys_FieldGroupDetailVO object</returns>
		public ArrayList List(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDGROUPID_FLD + ","
					+ Sys_FieldGroupDetailTable.REPORTID_FLD + ","
					+ Sys_FieldGroupDetailTable.FIELDNAME_FLD
					+ " FROM " + Sys_FieldGroupDetailTable.TABLE_NAME
					+ " WHERE " + Sys_FieldGroupDetailTable.REPORTID_FLD+ "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_FieldGroupDetailTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_FieldGroupDetailTable.REPORTID_FLD].Value = pstrReportID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ArrayList arrResult = new ArrayList();
				while (odrPCS.Read())
				{
					Sys_FieldGroupDetailVO objObject = new Sys_FieldGroupDetailVO();
					objObject.FieldGroupDetailID = int.Parse(odrPCS[Sys_FieldGroupDetailTable.FIELDGROUPDETAILID_FLD].ToString().Trim());
					objObject.FieldGroupID = int.Parse(odrPCS[Sys_FieldGroupDetailTable.FIELDGROUPID_FLD].ToString().Trim());
					objObject.ReportID = odrPCS[Sys_FieldGroupDetailTable.REPORTID_FLD].ToString().Trim();
					objObject.FieldName = odrPCS[Sys_FieldGroupDetailTable.FIELDNAME_FLD].ToString().Trim();
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