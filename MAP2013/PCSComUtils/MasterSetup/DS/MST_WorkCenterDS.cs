using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_WorkCenterDS 
	{
		public MST_WorkCenterDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_WorkCenterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_WorkCenter
		///    </Description>
		///    <Inputs>
		///        MST_WorkCenterVO       
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
				MST_WorkCenterVO objObject = (MST_WorkCenterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_WorkCenter("
				+ MST_WorkCenterTable.CODE_FLD + ","
				+ MST_WorkCenterTable.DESCRIPTION_FLD + ","
				+ MST_WorkCenterTable.NAME_FLD + ","
				+ MST_WorkCenterTable.CCNID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterTable.CCNID_FLD].Value = objObject.CCNID;


				
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
		///       This method uses to delete data from MST_WorkCenter
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
			strSql=	"DELETE " + MST_WorkCenterTable.TABLE_NAME + " WHERE  " + "WorkCenterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_WorkCenter
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_WorkCenterVO
		///    </Outputs>
		///    <Returns>
		///       MST_WorkCenterVO
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
				+ MST_WorkCenterTable.WORKCENTERID_FLD + ","
				+ MST_WorkCenterTable.CODE_FLD + ","
				+ MST_WorkCenterTable.DESCRIPTION_FLD + ","
				+ MST_WorkCenterTable.NAME_FLD + ","
				+ MST_WorkCenterTable.ISMAIN_FLD + ","
				+ MST_WorkCenterTable.CCNID_FLD
				+ " FROM " + MST_WorkCenterTable.TABLE_NAME
				+" WHERE " + MST_WorkCenterTable.WORKCENTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkCenterVO objObject = new MST_WorkCenterVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkCenterID = int.Parse(odrPCS[MST_WorkCenterTable.WORKCENTERID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_WorkCenterTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[MST_WorkCenterTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_WorkCenterTable.NAME_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[MST_WorkCenterTable.CCNID_FLD].ToString().Trim());
					if(!odrPCS[MST_WorkCenterTable.ISMAIN_FLD].Equals(DBNull.Value))
					{
						objObject.IsMain = bool.Parse(odrPCS[MST_WorkCenterTable.ISMAIN_FLD].ToString().Trim());
					}
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
		///       This method uses to update data to MST_WorkCenter
		///    </Description>
		///    <Inputs>
		///       MST_WorkCenterVO       
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

			MST_WorkCenterVO objObject = (MST_WorkCenterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_WorkCenter SET "
				+ MST_WorkCenterTable.CODE_FLD + "=?, "
				+ MST_WorkCenterTable.DESCRIPTION_FLD + "= ?, "
				+ MST_WorkCenterTable.NAME_FLD + "= ?, "
				+ MST_WorkCenterTable.ISMAIN_FLD + "= ?, "
				+ MST_WorkCenterTable.CCNID_FLD + "= ? "
				+" WHERE " + MST_WorkCenterTable.WORKCENTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_WorkCenterTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.ISMAIN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkCenterTable.ISMAIN_FLD].Value = objObject.IsMain;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;


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
		///       This method uses to get all data from MST_WorkCenter
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
				+ MST_WorkCenterTable.WORKCENTERID_FLD + ","
				+ MST_WorkCenterTable.CODE_FLD + ","
				+ MST_WorkCenterTable.DESCRIPTION_FLD + ","
				+ MST_WorkCenterTable.NAME_FLD + ","
				+ MST_WorkCenterTable.CCNID_FLD
					+ " FROM " + MST_WorkCenterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkCenterTable.TABLE_NAME);

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
		///       This method uses to get all data from MST_WorkCenter
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

		public DataSet ListForCombo()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ MST_WorkCenterTable.WORKCENTERID_FLD + ","
					+ MST_WorkCenterTable.CODE_FLD + ","
					+ MST_WorkCenterTable.NAME_FLD + ","
					+ MST_WorkCenterTable.DESCRIPTION_FLD
					+ " FROM " + MST_WorkCenterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkCenterTable.TABLE_NAME);

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
				+ MST_WorkCenterTable.WORKCENTERID_FLD + ","
				+ MST_WorkCenterTable.CODE_FLD + ","
				+ MST_WorkCenterTable.DESCRIPTION_FLD + ","
				+ MST_WorkCenterTable.NAME_FLD + ","
				+ MST_WorkCenterTable.ISMAIN_FLD + ","
				+ MST_WorkCenterTable.CCNID_FLD
				+ "  FROM " + MST_WorkCenterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_WorkCenterTable.TABLE_NAME);

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
		/// Update Data table 
		/// </summary>
		/// <param name="pData"></param>
		/// <author> Tuan TQ</author>
		public void UpdateMainWorkCenter(DataTable pdtbData)
		{
			const string METHOD_NAME = THIS + ".UpdateMainWorkCenter()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ MST_WorkCenterTable.WORKCENTERID_FLD + ","					
					+ MST_WorkCenterTable.ISMAIN_FLD
					+ "  FROM " + MST_WorkCenterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);				
				odadPCS.Update(pdtbData);
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
