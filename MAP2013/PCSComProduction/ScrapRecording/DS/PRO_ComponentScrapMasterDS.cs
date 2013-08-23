using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.ScrapRecording.DS
{
	public class PRO_ComponentScrapMasterDS 
	{
		public PRO_ComponentScrapMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.ScrapRecording.DS.PRO_ComponentScrapMasterDS";
		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ComponentScrapMasterVO objObject = (PRO_ComponentScrapMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ComponentScrapMaster("
				+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + ","
				+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + ","
				+ PRO_ComponentScrapMasterTable.CCNID_FLD + ","
				+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.SCRAPNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].Value = objObject.ScrapNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


				
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
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_ComponentScrapMasterTable.TABLE_NAME + " WHERE  " + "ComponentScrapMasterID" + "=" + pintID.ToString();
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
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
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
				+ PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + ","
				+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + ","
				+ PRO_ComponentScrapMasterTable.CCNID_FLD + ","
				+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD
				+ " FROM " + PRO_ComponentScrapMasterTable.TABLE_NAME
				+" WHERE " + PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ComponentScrapMasterVO objObject = new PRO_ComponentScrapMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.ComponentScrapMasterID = int.Parse(odrPCS[PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD].ToString().Trim());
				objObject.ScrapNo = odrPCS[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].ToString().Trim();
				objObject.PostDate = DateTime.Parse(odrPCS[PRO_ComponentScrapMasterTable.POSTDATE_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[PRO_ComponentScrapMasterTable.CCNID_FLD].ToString().Trim());
				objObject.MasterLocationID = int.Parse(odrPCS[PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());

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
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_ComponentScrapMasterVO objObject = (PRO_ComponentScrapMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ComponentScrapMaster SET "
				+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + "=   ?" + ","
				+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + "=   ?" + ","
				+ PRO_ComponentScrapMasterTable.CCNID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD + "=  ?"
				+" WHERE " + PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.SCRAPNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].Value = objObject.ScrapNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD].Value = objObject.ComponentScrapMasterID;


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
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
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
				+ PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + ","
				+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + ","
				+ PRO_ComponentScrapMasterTable.CCNID_FLD + ","
				+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD
					+ " FROM " + PRO_ComponentScrapMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ComponentScrapMasterTable.TABLE_NAME);

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
		/// <summary>
		/// GetProductionLineCodeByScrapMasterID
		/// </summary>
		/// <param name="pintScrapMasterID"></param>
		/// <returns></returns>
		public DataTable GetProductionLineCodeByScrapMasterID(int pintScrapMasterID)
		{
			const string METHOD_NAME = THIS + ".GetProductionLineCodeByScrapMasterID()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ " M." + PRO_ComponentScrapMasterTable.PRODUCTIONLINEID_FLD + ","
					+ " P." + PRO_ProductionLineTable.CODE_FLD
					+ " FROM " + PRO_ComponentScrapMasterTable.TABLE_NAME + " M "
					+ " LEFT JOIN " + PRO_ProductionLineTable.TABLE_NAME + " P ON P." 
					+ PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + " = M." + PRO_ComponentScrapMasterTable.PRODUCTIONLINEID_FLD
					+ " WHERE M." + PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + " = " + pintScrapMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ComponentScrapMasterTable.TABLE_NAME);

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

		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapMaster
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>
		
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
				+ PRO_ComponentScrapMasterTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + ","
				+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + ","
				+ PRO_ComponentScrapMasterTable.CCNID_FLD + ","
				+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD 
		+ "  FROM " + PRO_ComponentScrapMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_ComponentScrapMasterTable.TABLE_NAME);

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
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, June 30 2005</date>
		
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ComponentScrapMasterVO objObject = (PRO_ComponentScrapMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + PRO_ComponentScrapMasterTable.TABLE_NAME + "("
					+ PRO_ComponentScrapMasterTable.CCNID_FLD + ","
					+ PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_ComponentScrapMasterTable.PRODUCTIONLINEID_FLD + ","
					+ PRO_ComponentScrapMasterTable.POSTDATE_FLD + ","
					+ PRO_ComponentScrapMasterTable.SCRAPNO_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapMasterTable.SCRAPNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapMasterTable.SCRAPNO_FLD].Value = objObject.ScrapNo;

				strSql += " ; Select @@IDENTITY as NEWID";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();	
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

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

		
	}
}
