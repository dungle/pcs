using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Plan.DS
{
	public class MTR_MPSCycleOptionMasterDS 
	{
		public MTR_MPSCycleOptionMasterDS()
		{
		}
		private const string THIS = "PCSComMaterials.Plan.DS.MTR_MPSCycleOptionMasterDS";
		///    <summary>
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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
				MTR_MPSCycleOptionMasterVO objObject = (MTR_MPSCycleOptionMasterVO) pobjObjectVO;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				string strSql =	"INSERT INTO MTR_MPSCycleOptionMaster("
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.GROUPBY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

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
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
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
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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
			strSql=	"DELETE " + MTR_MPSCycleOptionMasterTable.TABLE_NAME + " WHERE  " + "MPSCycleOptionMasterID" + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
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
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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
					+ MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD
					+ " FROM " + MTR_MPSCycleOptionMasterTable.TABLE_NAME
					+" WHERE " + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MTR_MPSCycleOptionMasterVO objObject = new MTR_MPSCycleOptionMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					objObject.Cycle = odrPCS[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].ToString().Trim();
					objObject.AsOfDate = DateTime.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].ToString().Trim());
					try
					{
						objObject.MPSGenDate = DateTime.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD].ToString().Trim());
					}
					catch{}
					
					objObject.PlanHorizon = int.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.CCNID_FLD].ToString().Trim());
					objObject.GroupBy = int.Parse(odrPCS[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD].ToString().Trim());
					objObject.Description = odrPCS[MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD].ToString().Trim();

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
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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

			MTR_MPSCycleOptionMasterVO objObject = (MTR_MPSCycleOptionMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MTR_MPSCycleOptionMaster SET "
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + "=   ?" + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD + "=  ?"
					+" WHERE " + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.GROUPBY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;


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
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
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
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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
					+ MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD
					+ " FROM " + MTR_MPSCycleOptionMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MTR_MPSCycleOptionMasterTable.TABLE_NAME);

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
		/// CheckMRP
		/// </summary>
		/// <param name="pintMpsCycleOptionMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, October 20 2005</date>
		public int CheckMRP(int pintMpsCycleOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".CheckMRP()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT COUNT(*)" 
					+ " FROM " + MTR_MRPCycleOptionMasterTable.TABLE_NAME 
					+ " WHERE " + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintMpsCycleOptionMasterID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				return int.Parse(objReturn.ToString());
				
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
		/// CheckDCPOption
		/// </summary>
		/// <param name="pintMpsCycleOptionMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, December 13 2005</date>
		public int CheckDCPOption (int pintMpsCycleOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".CheckDCPOption()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT COUNT(*)" 
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME 
					//+ " WHERE " + PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintMpsCycleOptionMasterID.ToString();
					+ " WHERE " + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintMpsCycleOptionMasterID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				return int.Parse(objReturn.ToString());
				
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
		///       This method uses to add data to MTR_MPSCycleOptionMaster
		///    </summary>
		///    <Inputs>
		///        MTR_MPSCycleOptionMasterVO       
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
					+ MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD 
					+ "  FROM " + MTR_MPSCycleOptionMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,MTR_MPSCycleOptionMasterTable.TABLE_NAME);

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
		/// <param name="pobjMasterVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, August 10 2005</date>
		public int AddAndReturnID(object pobjMasterVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				MTR_MPSCycleOptionMasterVO objObject = (MTR_MPSCycleOptionMasterVO) pobjMasterVO;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				string strSql =	"INSERT INTO MTR_MPSCycleOptionMaster("
					+ MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.CCNID_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.GROUPBY_FLD + ","
					+ MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?)"
					+ " SELECT @@IDENTITY" ;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
				if (objObject.MPSGenDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;
				}
				else
					ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.MPSGENDATE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.GROUPBY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();	
				if (objReturn != null)
				{
					return int.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}


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
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
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
