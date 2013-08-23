using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_ApprovalLevelDS 
	{
		public MST_ApprovalLevelDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_ApprovalLevelDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_ApprovalLevel
		///    </Description>
		///    <Inputs>
		///        MST_ApprovalLevelVO       
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
				MST_ApprovalLevelVO objObject = (MST_ApprovalLevelVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_ApprovalLevel("
				+ MST_ApprovalLevelTable.LEVEL_FLD + ","
				+ MST_ApprovalLevelTable.AMOUNT_FLD + ","
				+ MST_ApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_ApprovalLevelTable.CCNID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.LEVEL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.LEVEL_FLD].Value = objObject.Level;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.CCNID_FLD].Value = objObject.CCNID;


				
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
		///       This method uses to delete data from MST_ApprovalLevel
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
			strSql=	"DELETE " + MST_ApprovalLevelTable.TABLE_NAME + " WHERE  " + "ApprovalLevelID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_ApprovalLevel
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_ApprovalLevelVO
		///    </Outputs>
		///    <Returns>
		///       MST_ApprovalLevelVO
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
				+ MST_ApprovalLevelTable.APPROVALLEVELID_FLD + ","
				+ MST_ApprovalLevelTable.LEVEL_FLD + ","
				+ MST_ApprovalLevelTable.AMOUNT_FLD + ","
				+ MST_ApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_ApprovalLevelTable.CCNID_FLD
				+ " FROM " + MST_ApprovalLevelTable.TABLE_NAME
				+" WHERE " + MST_ApprovalLevelTable.APPROVALLEVELID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_ApprovalLevelVO objObject = new MST_ApprovalLevelVO();

				while (odrPCS.Read())
				{ 
				objObject.ApprovalLevelID = int.Parse(odrPCS[MST_ApprovalLevelTable.APPROVALLEVELID_FLD].ToString().Trim());
				objObject.Level = odrPCS[MST_ApprovalLevelTable.LEVEL_FLD].ToString().Trim();
				objObject.Amount = Decimal.Parse(odrPCS[MST_ApprovalLevelTable.AMOUNT_FLD].ToString().Trim());
				objObject.Description = odrPCS[MST_ApprovalLevelTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.CCNID = int.Parse(odrPCS[MST_ApprovalLevelTable.CCNID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_ApprovalLevel
		///    </Description>
		///    <Inputs>
		///       MST_ApprovalLevelVO       
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

			MST_ApprovalLevelVO objObject = (MST_ApprovalLevelVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_ApprovalLevel SET "
				+ MST_ApprovalLevelTable.LEVEL_FLD + "=   ?" + ","
				+ MST_ApprovalLevelTable.AMOUNT_FLD + "=   ?" + ","
				+ MST_ApprovalLevelTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_ApprovalLevelTable.CCNID_FLD + "=  ?"
				+" WHERE " + MST_ApprovalLevelTable.APPROVALLEVELID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.LEVEL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.LEVEL_FLD].Value = objObject.Level;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_ApprovalLevelTable.APPROVALLEVELID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_ApprovalLevelTable.APPROVALLEVELID_FLD].Value = objObject.ApprovalLevelID;


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
		///       This method uses to get all data from MST_ApprovalLevel
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
				+ MST_ApprovalLevelTable.APPROVALLEVELID_FLD + ","
				+ MST_ApprovalLevelTable.LEVEL_FLD + ","
				+ MST_ApprovalLevelTable.AMOUNT_FLD + ","
				+ MST_ApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_ApprovalLevelTable.CCNID_FLD
					+ " FROM " + MST_ApprovalLevelTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_ApprovalLevelTable.TABLE_NAME);

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
				+ MST_ApprovalLevelTable.APPROVALLEVELID_FLD + ","
				+ MST_ApprovalLevelTable.LEVEL_FLD + ","
				+ MST_ApprovalLevelTable.AMOUNT_FLD + ","
				+ MST_ApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_ApprovalLevelTable.CCNID_FLD 
		+ "  FROM " + MST_ApprovalLevelTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_ApprovalLevelTable.TABLE_NAME);

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
