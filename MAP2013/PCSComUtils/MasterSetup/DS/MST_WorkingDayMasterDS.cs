using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_WorkingDayMasterDS 
	{
		public MST_WorkingDayMasterDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_WorkingDayMasterDS";
	
		///    <summary>
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MST_WorkingDayMasterVO objObject = (MST_WorkingDayMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_WorkingDayMaster("
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SUN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SUN_FLD].Value = objObject.Sun;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.YEAR_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.YEAR_FLD].Value = objObject.Year;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.MON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.MON_FLD].Value = objObject.Mon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.TUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.TUE_FLD].Value = objObject.Tue;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.WED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.WED_FLD].Value = objObject.Wed;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.THU_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.THU_FLD].Value = objObject.Thu;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.FRI_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.FRI_FLD].Value = objObject.Fri;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SAT_FLD].Value = objObject.Sat;


				
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
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MST_WorkingDayMasterVO objObject = (MST_WorkingDayMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_WorkingDayMaster("
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?) SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SUN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SUN_FLD].Value = objObject.Sun;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.YEAR_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.YEAR_FLD].Value = objObject.Year;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.MON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.MON_FLD].Value = objObject.Mon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.TUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.TUE_FLD].Value = objObject.Tue;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.WED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.WED_FLD].Value = objObject.Wed;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.THU_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.THU_FLD].Value = objObject.Thu;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.FRI_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.FRI_FLD].Value = objObject.Fri;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SAT_FLD].Value = objObject.Sat;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				object objResult = ocmdPCS.ExecuteScalar();	
				if (objResult != null && objResult != DBNull.Value)
				{
					return int.Parse(objResult.ToString());
				}
				
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
	

		///    <summary>
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + MST_WorkingDayMasterTable.TABLE_NAME + " WHERE  " + "WorkingDayMasterID" + "=" + pintID.ToString();
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
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME
					+" WHERE " + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkingDayMasterVO objObject = new MST_WorkingDayMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkingDayMasterID = int.Parse(odrPCS[MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD].ToString().Trim());
					objObject.Sun = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SUN_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MST_WorkingDayMasterTable.CCNID_FLD].ToString().Trim());
					objObject.Year = int.Parse(odrPCS[MST_WorkingDayMasterTable.YEAR_FLD].ToString().Trim());
					objObject.Mon = bool.Parse(odrPCS[MST_WorkingDayMasterTable.MON_FLD].ToString().Trim());
					objObject.Tue = bool.Parse(odrPCS[MST_WorkingDayMasterTable.TUE_FLD].ToString().Trim());
					objObject.Wed = bool.Parse(odrPCS[MST_WorkingDayMasterTable.WED_FLD].ToString().Trim());
					objObject.Thu = bool.Parse(odrPCS[MST_WorkingDayMasterTable.THU_FLD].ToString().Trim());
					objObject.Fri = bool.Parse(odrPCS[MST_WorkingDayMasterTable.FRI_FLD].ToString().Trim());
					objObject.Sat = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SAT_FLD].ToString().Trim());

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
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			MST_WorkingDayMasterVO objObject = (MST_WorkingDayMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_WorkingDayMaster SET "
					+ MST_WorkingDayMasterTable.SUN_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.MON_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.WED_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.THU_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + "=   ?" + ","
					+ MST_WorkingDayMasterTable.SAT_FLD + "=  ?"
					+" WHERE " + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SUN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SUN_FLD].Value = objObject.Sun;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.YEAR_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.YEAR_FLD].Value = objObject.Year;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.MON_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.MON_FLD].Value = objObject.Mon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.TUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.TUE_FLD].Value = objObject.Tue;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.WED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.WED_FLD].Value = objObject.Wed;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.THU_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.THU_FLD].Value = objObject.Thu;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.FRI_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.FRI_FLD].Value = objObject.Fri;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.SAT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.SAT_FLD].Value = objObject.Sat;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD].Value = objObject.WorkingDayMasterID;


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
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayMasterTable.TABLE_NAME);

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
		///       This method uses to add data to MST_WorkingDayMaster
		///    </summary>
		///    <Inputs>
		///        MST_WorkingDayMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Thursday, July 21, 2005
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
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD 
					+ "  FROM " + MST_WorkingDayMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,MST_WorkingDayMasterTable.TABLE_NAME);

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
		/// Get MST_WorkingDayMasterVO object by CCN and Year
		/// </summary>
		public object GetObjectVO(int pintCCNID, int pintYear)
		{
			return null;
		}
		/// <summary>
		/// GetNoWorkingDay
		/// </summary>
		/// <param name="pintYear"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 16 2006</date>
		public object GetNoWorkingDay(int pintYear)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME
					+" WHERE " + MST_WorkingDayMasterTable.YEAR_FLD + "=" + pintYear;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkingDayMasterVO objObject = new MST_WorkingDayMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkingDayMasterID = int.Parse(odrPCS[MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD].ToString().Trim());
					objObject.Sun = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SUN_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MST_WorkingDayMasterTable.CCNID_FLD].ToString().Trim());
					objObject.Year = int.Parse(odrPCS[MST_WorkingDayMasterTable.YEAR_FLD].ToString().Trim());
					objObject.Mon = bool.Parse(odrPCS[MST_WorkingDayMasterTable.MON_FLD].ToString().Trim());
					objObject.Tue = bool.Parse(odrPCS[MST_WorkingDayMasterTable.TUE_FLD].ToString().Trim());
					objObject.Wed = bool.Parse(odrPCS[MST_WorkingDayMasterTable.WED_FLD].ToString().Trim());
					objObject.Thu = bool.Parse(odrPCS[MST_WorkingDayMasterTable.THU_FLD].ToString().Trim());
					objObject.Fri = bool.Parse(odrPCS[MST_WorkingDayMasterTable.FRI_FLD].ToString().Trim());
					objObject.Sat = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SAT_FLD].ToString().Trim());

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

		public object GetWDCalendarMaster(int pintYearID, int pintCCNID)
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
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM  " + MST_WorkingDayMasterTable.TABLE_NAME
					+ " WHERE " + MST_WorkingDayMasterTable.YEAR_FLD + "=" + pintYearID
					+ " AND " + MST_WorkingDayMasterTable.CCNID_FLD + "=" + pintCCNID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkingDayMasterVO objObject = new MST_WorkingDayMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.WorkingDayMasterID = int.Parse(odrPCS[MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD].ToString().Trim());
					objObject.Sun = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SUN_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MST_WorkingDayMasterTable.CCNID_FLD].ToString().Trim());
					objObject.Year = int.Parse(odrPCS[MST_WorkingDayMasterTable.YEAR_FLD].ToString().Trim());
					objObject.Mon = bool.Parse(odrPCS[MST_WorkingDayMasterTable.MON_FLD].ToString().Trim());
					objObject.Tue = bool.Parse(odrPCS[MST_WorkingDayMasterTable.TUE_FLD].ToString().Trim());
					objObject.Wed = bool.Parse(odrPCS[MST_WorkingDayMasterTable.WED_FLD].ToString().Trim());
					objObject.Thu = bool.Parse(odrPCS[MST_WorkingDayMasterTable.THU_FLD].ToString().Trim());
					objObject.Fri = bool.Parse(odrPCS[MST_WorkingDayMasterTable.FRI_FLD].ToString().Trim());
					objObject.Sat = bool.Parse(odrPCS[MST_WorkingDayMasterTable.SAT_FLD].ToString().Trim());

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
		/// <summary>
		/// GetCollectionOffDays
		/// </summary>
		/// <param name="pintYear"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 16 2006</date>
		public DataSet GetCollectionOffDays(int pintYear)
		{
			const string METHOD_NAME = THIS + ".GetCollectionOffDays()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME
					+ " WHERE " + MST_WorkingDayMasterTable.YEAR_FLD + " = " + pintYear;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayMasterTable.TABLE_NAME);

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
		/// Get master and detail of working day calendar
		/// </summary>
		/// <returns>DataSet</returns>
		/// <author>DungLA</author>
		public DataSet GetCalendar()
		{
			const string METHOD_NAME = THIS + ".GetCalendar()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT WDM." + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.SUN_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.CCNID_FLD
					+ ", WDM."  + MST_WorkingDayMasterTable.YEAR_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.MON_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.TUE_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.WED_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.THU_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.FRI_FLD
					+ ", WDM." + MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME + " WDM"
					+ "	ORDER BY WDM." + MST_WorkingDayMasterTable.YEAR_FLD + ";";

				strSql = strSql + " SELECT " + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ ", " + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD
					+ ", " + MST_WorkingDayDetailTable.OFFDAY_FLD
					+ " FROM " + MST_WorkingDayDetailTable.TABLE_NAME + " WDD"
					+ " ORDER BY " + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + ", " + MST_WorkingDayDetailTable.OFFDAY_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayMasterTable.TABLE_NAME);
				dstPCS.Tables[0].TableName = MST_WorkingDayMasterTable.TABLE_NAME;
				dstPCS.Tables[1].TableName = MST_WorkingDayDetailTable.TABLE_NAME;
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
	}
}
