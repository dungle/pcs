using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.DCP.DS
{
	public class PRO_DCOptionMasterDS 
	{
		public PRO_DCOptionMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_DCOptionMasterDS";
		///    <summary>
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCOptionMasterVO objObject = (PRO_DCOptionMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_DCOptionMaster("
					+ PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULETYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].Value = objObject.ScheduleType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].Value = objObject.IgnoreMoveTime;

				//ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				//				if(objObject.MPSCycleOptionMasterID != 0)
				//				{
				//					//ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
				//				}
				//				else
				//				{
				//					//ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
				//				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULECODE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].Value = objObject.ScheduleCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.LASTUPDATE_FLD, OleDbType.Date));
				if(objObject.LastUpdate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = objObject.LastUpdate;
				}
				
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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
		///    </History>


		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_DCOptionMasterVO objObject = (PRO_DCOptionMasterVO) pobjObjectVO;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "INSERT INTO " + PRO_DCOptionMasterTable.TABLE_NAME
					+ "("
					+ PRO_DCOptionMasterTable.CCNID_FLD + ", "
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ", "
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ", "
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ", "
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ", "
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ", "
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ", "
					+ PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD + ", "
					+ PRO_DCOptionMasterTable.SAFETYSTOCK_FLD + ", "
					+ PRO_DCOptionMasterTable.ONHAND_FLD + ", "
					+ PRO_DCOptionMasterTable.ASOFDATE_FLD + ", "
					+ PRO_DCOptionMasterTable.PLANHORIZON_FLD + ", "
					+ PRO_DCOptionMasterTable.GROUPBY_FLD + ", "
					+ PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD + ", "
					+ PRO_DCOptionMasterTable.VERSION_FLD + ", "
					+ PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ", "
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				strSql += "; SELECT @@IDENTITY as LatestID";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULETYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].Value = objObject.ScheduleType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].Value = objObject.IgnoreMoveTime;

				//				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				//				if(objObject.MPSCycleOptionMasterID != 0)
				//				{
				//					ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
				//				}
				//				else
				//				{
				//					ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
				//				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULECODE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].Value = objObject.ScheduleCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD].Value = objObject.IncludeCheckPoint;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SAFETYSTOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.ONHAND_FLD].Value = objObject.OnHand;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.GROUPBY_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.GROUPBY_FLD].Value = objObject.GroupBy;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD, OleDbType.Date));
				if(objObject.PlanningPeriod == DateTime.MinValue)
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD].Value = objObject.PlanningPeriod;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.VERSION_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.VERSION_FLD].Value = objObject.Version;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].Value = objObject.UseCacheAsBegin;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.LASTUPDATE_FLD, OleDbType.Date));
				if(objObject.LastUpdate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = objObject.LastUpdate;
				}
				
				

				ocmdPCS.Connection.Open();

				//Add and return latest Id
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
	
		/// <summary>
		/// Get detail information of DCOption Master
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		public DataRow GetDCOptionMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetDCOptionMaster()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.LASTUPDATE_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SAFETYSTOCK_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.ONHAND_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.PLANHORIZON_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.GROUPBY_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.VERSION_FLD + ","					
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.ASOFDATE_FLD 
					//+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					//					+ MTR_MPSCycleOptionMasterTable.TABLE_NAME + "." + MTR_MPSCycleOptionMasterTable.CYCLE_FLD + " as " + MTR_MPSCycleOptionMasterTable.TABLE_NAME + MTR_MPSCycleOptionMasterTable.CYCLE_FLD + ","
					//					+ MTR_MPSCycleOptionMasterTable.TABLE_NAME + "." + MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD + ","
					//					+ MTR_MPSCycleOptionMasterTable.TABLE_NAME + "." + MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD + " AS MaxDays"
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME					
					//					+ " INNER JOIN " + MTR_MPSCycleOptionMasterTable.TABLE_NAME 
					//					+ " ON " + MTR_MPSCycleOptionMasterTable.TABLE_NAME + "." + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + "=" +   PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
					+ " WHERE " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);

				if(dstPCS.Tables[0].Rows.Count !=0)
				{
					return dstPCS.Tables[0].Rows[0];
				}
				else
				{
					return null;
				}
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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = "DELETE " + PRO_DCOptionMasterTable.TABLE_NAME + " WHERE  " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + pintID.ToString();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
		///    </History>

		public void DeleteRelatedInforOfDCOption(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteRelatedInforOfDCOption()";
			string strSql1 = "DELETE FROM " + PRO_DCPResultDetailTable.TABLE_NAME;
			strSql1 += " WHERE " + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN ";
			strSql1 += " ( ";
			strSql1 += " SELECT "  + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD;
			strSql1 += " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql1 += " WHERE " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "= " + pintDCOptionMasterID.ToString();
			strSql1 += ")";

			string strSql2 = "DELETE FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql2 += " WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " IN ";
			strSql2 += " ( ";
			strSql2 += " SELECT "  + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD;
			strSql2 += " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql2 += " WHERE " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "= " + pintDCOptionMasterID.ToString();
			strSql2 += ")";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql1, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

				ocmdPCS.CommandText = strSql2;				
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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
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
				string strSql = "SELECT "
					+ PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.PLANHORIZON_FLD + ","
					+ PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME
					+ " WHERE " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCOptionMasterVO objObject = new PRO_DCOptionMasterVO();

				while (odrPCS.Read())
				{ 
					objObject.DCOptionMasterID = int.Parse(odrPCS[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_DCOptionMasterTable.CCNID_FLD].ToString().Trim());
					objObject.Cycle = odrPCS[PRO_DCOptionMasterTable.CYCLE_FLD].ToString().Trim();
					objObject.Description = odrPCS[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.ScheduleType = int.Parse(odrPCS[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].ToString().Trim());
					objObject.IgnoreMoveTime = bool.Parse(odrPCS[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].ToString().Trim());					
					objObject.ScheduleCode = int.Parse(odrPCS[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].ToString().Trim());
					
					if(!odrPCS[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.LastUpdate = DateTime.Parse(odrPCS[PRO_DCOptionMasterTable.LASTUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LastUpdate = DateTime.MinValue;
					}
					objObject.PlanHorizon = Convert.ToInt32(odrPCS[PRO_DCOptionMasterTable.PLANHORIZON_FLD]);
					try
					{
						objObject.UseCacheAsBegin = Convert.ToBoolean(odrPCS[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD]);
					}
					catch
					{
						objObject.UseCacheAsBegin = false;
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
		
		///    <summary>
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>

		public object GetObjectVO(string pstrCycle)
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
					+ PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					+ PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ","
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME
					+ " WHERE " + PRO_DCOptionMasterTable.CYCLE_FLD + "='" + pstrCycle.Replace("'", "''") + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_DCOptionMasterVO objObject = new PRO_DCOptionMasterVO();

				if(odrPCS.Read())
				{ 
					objObject.DCOptionMasterID = int.Parse(odrPCS[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_DCOptionMasterTable.CCNID_FLD].ToString().Trim());					
					objObject.Cycle = odrPCS[PRO_DCOptionMasterTable.CYCLE_FLD].ToString().Trim();
					objObject.Description = odrPCS[PRO_DCOptionMasterTable.DESCRIPTION_FLD].ToString().Trim();
					
					objObject.ScheduleType = int.Parse(odrPCS[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].ToString().Trim());
					objObject.IgnoreMoveTime = bool.Parse(odrPCS[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].ToString().Trim());					
					objObject.ScheduleCode = int.Parse(odrPCS[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].ToString().Trim());
					
					//check null for allow null fields
					//					if(!odrPCS[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
					//					{
					//						objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					//					}
					//					else
					//					{
					//						objObject.MPSCycleOptionMasterID = 0;
					//					}
					if(!odrPCS[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.LastUpdate = DateTime.Parse(odrPCS[PRO_DCOptionMasterTable.LASTUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LastUpdate = DateTime.MinValue;
					}
					if(!odrPCS[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].Equals(DBNull.Value))
					{
						objObject.UseCacheAsBegin = Convert.ToBoolean(odrPCS[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].ToString().Trim());
					}
					else
					{
						objObject.LastUpdate = DateTime.MinValue;
					}
				}
				else
				{
					objObject = null;
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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_DCOptionMasterVO objObject = (PRO_DCOptionMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + PRO_DCOptionMasterTable.TABLE_NAME + " SET "
					+ PRO_DCOptionMasterTable.CCNID_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.SAFETYSTOCK_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.ONHAND_FLD + "=   ?" + ","
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD + "=  ?" + ","
					+ PRO_DCOptionMasterTable.PLANHORIZON_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.GROUPBY_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.VERSION_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD + "=   ?" + ","
					+ PRO_DCOptionMasterTable.ASOFDATE_FLD + "=  ?"
					+" WHERE " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "= ?";
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULETYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULETYPE_FLD].Value = objObject.ScheduleType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD].Value = objObject.IgnoreMoveTime;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD].Value = objObject.IncludeCheckPoint;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SAFETYSTOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.ONHAND_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.ONHAND_FLD].Value = objObject.OnHand;

				//				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
				//				if(objObject.MPSCycleOptionMasterID != 0)
				//				{
				//					ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
				//				}
				//				else
				//				{
				//					ocmdPCS.Parameters[PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
				//				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.SCHEDULECODE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.SCHEDULECODE_FLD].Value = objObject.ScheduleCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.LASTUPDATE_FLD, OleDbType.Date));
				if(objObject.LastUpdate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.LASTUPDATE_FLD].Value = objObject.LastUpdate;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.GROUPBY_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.VERSION_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.VERSION_FLD].Value = objObject.Version;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD].Value = objObject.UseCacheAsBegin;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD, OleDbType.Date));
				if(objObject.PlanningPeriod == DateTime.MinValue)
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD].Value = objObject.PlanningPeriod;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD].Value = objObject.DCOptionMasterID;


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

		/// <summary>
		/// CheckUniqueVersion
		/// </summary>
		/// <param name="pdtmPlanningPeriod"></param>
		/// <param name="pintVersion"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Mar 17 2006</date>
		public int CheckUniqueVersion(DateTime pdtmPlanningPeriod, int pintVersion)
		{
			const string METHOD_NAME = THIS + ".CheckUniqueVersion()";
			const string COUNT = "Count";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				
				string strSql = "SELECT COUNT(*) Count "
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME 
					+ " WHERE " + PRO_DCOptionMasterTable.VERSION_FLD + " = " + pintVersion.ToString();
				if (pdtmPlanningPeriod != DateTime.MinValue)
				{
					strSql += " AND " + PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD + " = '" + pdtmPlanningPeriod.ToShortDateString() + "'";
				}
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);
				if (dstPCS.Tables[0].Rows.Count != 0)
				{
					return int.Parse(dstPCS.Tables[0].Rows[0][COUNT].ToString());
				}
				else
					return 0;
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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
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
					+ PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_DCOptionMaster
		///    </summary>
		///    <Inputs>
		///        PRO_DCOptionMasterVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Wednesday, August 31, 2005
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
					+ PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					//+ PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.LASTUPDATE_FLD 
					+ "  FROM " + PRO_DCOptionMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_DCOptionMasterTable.TABLE_NAME);

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
		/// Get DCPOption master and detail
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataSet GetDCOption(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDCOptionMaster()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CYCLE_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CCNID_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DESCRIPTION_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.ASOFDATE_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.GROUPBY_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.LASTUPDATE_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ", 0) " + PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.PLANHORIZON_FLD + ", 0) " + PRO_DCOptionMasterTable.PLANHORIZON_FLD + "," // If user don't input maxdays then set it 100 year
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SCHEDULECODE_FLD + ","
					+ PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SCHEDULETYPE_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ",1) " + PRO_DCOptionMasterTable.IGNOREMOVETIME_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD + ",1)" + PRO_DCOptionMasterTable.INCLUDECHECKPOINT_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.ONHAND_FLD + ",1)" + PRO_DCOptionMasterTable.ONHAND_FLD + ","
					+ " ISNULL(" + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.SAFETYSTOCK_FLD + ",1)" + PRO_DCOptionMasterTable.SAFETYSTOCK_FLD
					+ " FROM " + PRO_DCOptionMasterTable.TABLE_NAME
					//+ " INNER JOIN " + MTR_MPSCycleOptionMasterTable.TABLE_NAME
					//+ " ON " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + MTR_MPSCycleOptionMasterTable.TABLE_NAME + "." + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
					+ " WHERE " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + " = " + pintDCOptionMasterID;

				strSql = strSql + "; SELECT "
					+ PRO_DCOptionDetailTable.DCOPTIONDETAILID_FLD + ","
					+ PRO_DCOptionDetailTable.MASTERLOCATIONID_FLD + ","
					+ PRO_DCOptionDetailTable.WORKORDER_FLD
					+ " FROM " + PRO_DCOptionDetailTable.TABLE_NAME 
					+ " WHERE " + PRO_DCOptionDetailTable.DCOPTIONMASTERID_FLD + "=" + pintDCOptionMasterID;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);

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
		/// Update LastUpdate field in table PRO_DCOptionMaster
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		public void SetDCOptionLastUpdate(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".SetDCOptionLastUpdate()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql += "UPDATE PRO_DCOptionMaster SET LastUpdate = GetDate() WHERE DCOptionMasterID = " + pintDCOptionMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
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

		/// <summary>
		/// Get master and detail
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataSet GetCalendar(int pintDCOptionMasterID)
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
					+ " FROM MST_WorkingDayMaster WDM"
					//	+ " WHERE WDM.Year >= "
					//	+ "		(SELECT YEAR(AsOfDate) StartYear FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") "
					//	+ "	AND WDM.Year <= "
					//	+ "		(SELECT YEAR(AsOfDate + ISNULL(MaxDays,0)) EndYear FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") "
					+ "	ORDER BY WDM.Year;";

				strSql = strSql + " SELECT " + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ ", " + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD
					+ ", " + MST_WorkingDayDetailTable.OFFDAY_FLD
					+ " FROM MST_WorkingDayDetail WDD"
					//	+ " WHERE WorkingDayMasterID IN ("
					//	+ "		SELECT WDM.WorkingDayMasterID"
					//	+ "		FROM MST_WorkingDayMaster WDM"
					//	+ "		WHERE WDM.Year >= "
					//	+ "			(SELECT YEAR(AsOfDate) StartYear FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") "
					//	+ "		AND WDM.Year <= "
					//	+ "			(SELECT YEAR(AsOfDate + ISNULL(MaxDays,0)) EndYear FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") 	"
					//	+ "	)"
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataSet GetCPOWOAndOperation(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql =	" SELECT 0 MasterID, WOCPO.WorkOrderDetailID, WOCPO.CPOID, WOCPO.ProductID,  \n"
					+ "     WOCPO.Quantity, WOCPO.StartDate, WOCPO.DueDate, ParentCPOID,WOCPO.LeadtimeOffset, 0 GetOut, S.Priority, WOCPO.IsSafetyStock \n"
					+ "  FROM (  \n"
					+ "     SELECT 0 IsSafetyStock, WOD.WorkOrderDetailID, null CPOID, WOD.ProductID, (WOD.OrderQuantity - \n" 
					+ "			(Select ISNULL(SUM(ISNULL(CompletedQuantity,0)),0) From PRO_WorkOrderCompletion Where WorkOrderDetailID = WOD.WorkOrderDetailID)) Quantity, WOD.StartDate, WOD.DueDate, NULL ParentCPOID, 0 LeadtimeOffset  \n"
					+ "     FROM PRO_WorkOrderDetail WOD  \n"
					+ "     INNER JOIN  PRO_WorkOrderMaster WOM ON WOD.WorkOrderMasterID=WOM.WorkOrderMasterID  \n"
					+ "     AND WOM.MasterLocationID IN (SELECT MasterLocationID FROM PRO_DCOptionDetail WHERE WorkOrder=1 AND DCOptionMasterID=" + pintDCOptionMasterID + ")  \n"
					+ "		AND Status = " + WOLineStatus.Released.GetHashCode() + "  \n"
					+ "		AND WOD.MfgCloseDate IS NULL"
					+ "		AND WOD.FinCloseDate IS NULL"
					+ "     UNION  \n"
					+ "     SELECT CPO.IsSafetyStock ,null WorkOrderDetailID, CPO.CPOID, CPO.ProductID, CPO.Quantity, CPO.StartDate, CPO.DueDate, CPO.ParentCPOID,LeadtimeOffset  \n"
					+ "     FROM MTR_CPO CPO "
					+ "		LEFT JOIN MTR_CPO PARENT_CPO ON CPO.ParentCPOID = PARENT_CPO.CPOID "
					+ "		LEFT JOIN ITM_BOM BOM ON CPO.ProductID = BOM.ComponentID AND PARENT_CPO.ProductID = BOM.ProductID "
					+ "		WHERE CPO.Converted=0 AND CPO.MPSCycleOptionMasterID IS NOT NULL   \n" 
					+ "		AND CPO.MPSCycleOptionMasterID=(SELECT MPSCycleOptionMasterID FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")  \n"
					+ "  ) WOCPO  \n"
					+ "  INNER JOIN ITM_Product P ON WOCPO.ProductID=P.ProductID AND P.MakeItem=1  \n"
					+ "  LEFT JOIN ITM_Source S ON P.SourceID=S.SourceID  \n"
					+ "  WHERE WOCPO.DueDate >=   \n"
					+ "     (SELECT AsOfDate FROM PRO_DCOptionMaster INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")  \n"
					+ "  AND WOCPO.DueDate <=  \n"
					+ "     (SELECT AsOfDate + ISNULL(PlanHorizon,0) FROM PRO_DCOptionMaster  INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")  \n"
					+ "  AND WOCPO.Quantity > 0"
					+ "	 ORDER BY CPOID,DueDate;  \n";
				strSql = strSql  
					+ "  SELECT 0 MasterID, 0 DetailID,  R.RoutingID, R.WORoutingID, R.Step, R.Type,    \n"
					+ "         R.Machines, R.LaborRunTime, R.LaborSetupTime, R.CrewSize, R.SetupQuantity, R.MachineSetupTime, R.MachineRunTime,     \n"
					+ "         R.StudyTime, R.MoveTime, R.LaborCostCenterID, R.MachineCostCenterID,   \n"
					+ "         R.ProductID, R.FunctionID, R.WorkCenterID, R.EffectBeginDate, R.EffectEndDate,    \n"
					+ "         R.OSVarLT, R.OSFixLT, R.OSOverlapPercent, R.OSOverlapQty, R.OSScheduleSeq,    \n"
					+ "         R.OSCost, R.OverlapPercent, R.OverlapQty, R.ScheduleSeq, R.VarLT, R.FixLT,    \n"
					+ "         R.RoutingStatusID, R.RunQuantity, R.PartyID, R.Pacer,   \n"
					+ "         R.WorkOrderMasterID, R.WorkOrderDetailID,   \n"
					+ "         WC.Code WorkCenterCode,  \n"
					+ "         WC.Description,  \n"
					+ "			CASE WHEN (SELECT IncludeCheckPoint FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") = 1 THEN   CP.SamplePattern \n"
					+ "			ELSE 0 \n"
					+ "			END SamplePattern, \n"
					+ "			CASE WHEN (SELECT IncludeCheckPoint FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") = 1 THEN   CP.SampleRate \n"
					+ "			ELSE 0 \n"
					+ "			END SampleRate, \n"
					+ "			CASE WHEN (SELECT IncludeCheckPoint FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") = 1 THEN   CP.DelayTime \n"
					+ "			ELSE 0 \n"
					+ "			END DelayTime, \n"
					+ "         CP.CheckPointID,   "
					+ "         CASE WHEN ISNULL(CP.SampleRate,0) > 0 THEN ISNULL(CP.DelayTime,0)/CP.SampleRate "
					+ "         ELSE 0 END CheckPointPerItem, "
					+ "         '2000-01-01' DueDateTime, '2000-01-01' StartDateTime,   \n"
					+ "         CASE WHEN Type=0 THEN   \n"
					+ "             CASE WHEN (SELECT IgnoreMoveTime FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") = 1 THEN    \n"
					+ "                 CASE    \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.B + "' THEN (ISNULL(R.LaborRunTime,0)+ISNULL(R.LaborSetupTime,0)) + (ISNULL(R.MachineRunTime,0)+ISNULL(R.MachineSetupTime,0)) + ISNULL(R.StudyTime,0)   \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.L + "' THEN (ISNULL(R.LaborRunTime,0)+ISNULL(R.LaborSetupTime,0)) + ISNULL(R.StudyTime,0)   \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.M + "' THEN (ISNULL(R.MachineRunTime,0)+ISNULL(R.MachineSetupTime,0)) + ISNULL(R.StudyTime,0)   \n"
					+ "                 END    \n"
					+ "             ELSE    \n"
					+ "                 CASE    \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.B + "' THEN (ISNULL(R.LaborRunTime,0)+ISNULL(R.LaborSetupTime,0)) + (ISNULL(R.MachineRunTime,0)+ISNULL(R.MachineSetupTime,0)) + ISNULL(R.StudyTime,0) + ISNULL(R.MoveTime,0)   \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.L + "' THEN (ISNULL(R.LaborRunTime,0)+ISNULL(R.LaborSetupTime,0)) + ISNULL(R.StudyTime,0) + ISNULL(R.MoveTime,0)   \n"
					+ "                     WHEN R.Pacer='" + PacerEnum.M + "' THEN (ISNULL(R.MachineRunTime,0)+ISNULL(R.MachineSetupTime,0)) + ISNULL(R.StudyTime,0) + ISNULL(R.MoveTime,0)   \n"
					+ "                 END    \n"
					+ "             END   \n"
					+ "         ELSE ISNULL(R.VarLT,0) + ISNULL(R.FixLT,0)   \n"
					+ "         END LeadTime   \n"
					+ "  FROM (   \n"
					+ "         SELECT DISTINCT NULL RoutingID, WORoutingID, Step, Type,   \n"
					+ "             MachineSetupTime, MachineRunTime, Machines, LaborRunTime,   \n"
					+ "             LaborSetupTime, CrewSize, SetupQuantity, StudyTime, MoveTime,   \n"
					+ "             LaborCostCenterID, MachineCostCenterID, ProductID, FunctionID,   \n" 
					+ "             WorkCenterID, EffectBeginDate, EffectEndDate, OSVarLT, OSFixLT,   \n"
					+ "             OSOverlapPercent, OSOverlapQty, OSScheduleSeq, OSCost, OverlapPercent, OverlapQty,   \n"
					+ "             ScheduleSeq, VarLT, FixLT, RoutingStatusID, RunQuantity, PartyID, Pacer,   \n"
					+ "             WorkOrderMasterID, WorkOrderDetailID   \n"
					+ "         FROM PRO_WORouting WOR  \n"
					+ "         WHERE WorkOrderDetailID IN (   \n"
					+ "             SELECT WorkOrderDetailID FROM PRO_WorkOrderDetail   \n"
					+ "             WHERE DueDate >= (SELECT AsOfDate FROM PRO_DCOptionMaster INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")   \n"
					+ "             AND DueDate < (SELECT AsOfDate + ISNULL(PlanHorizon,0) FROM PRO_DCOptionMaster INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + "))   \n"
					+ "         UNION    \n"
					+ "         SELECT  RoutingID, NULL WORoutingID, Step, Type,   \n"
					+ "             MachineSetupTime, MachineRunTime, Machines, LaborRunTime,   \n"
					+ "             LaborSetupTime, CrewSize, SetupQuantity, StudyTime, MoveTime,   \n"
					+ "             LaborCostCenterID, MachineCostCenterID, ProductID, FunctionID,   \n"
					+ "             WorkCenterID, EffectBeginDate, EffectEndDate, OSVarLT, OSFixLT,   \n"
					+ "             OSOverlapPercent, OSOverlapQty, OSScheduleSeq, OSCost, OverlapPercent, OverlapQty,   \n"
					+ "             ScheduleSeq, VarLT, FixLT, RoutingStatusID, RunQuantity, PartyID, Pacer,   \n"
					+ "             NULL WorkOrderMasterID, NULL WorkOrderDetailID   \n"
					+ "         FROM ITM_Routing Routing   \n"
					+ "         WHERE Routing.ProductID IN (    \n"
					+ "             SELECT DISTINCT ProductID FROM MTR_CPO    \n"
					+ "             WHERE DueDate >= (SELECT AsOfDate FROM PRO_DCOptionMaster INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")   \n"
					+ "             AND DueDate < (SELECT AsOfDate + ISNULL(PlanHorizon,0) FROM PRO_DCOptionMaster INNER JOIN MTR_MPSCycleOptionMaster ON PRO_DCOptionMaster.MPSCycleOptionMasterID = MTR_MPSCycleOptionMaster.MPSCycleOptionMasterID WHERE DCOptionMasterID=" + pintDCOptionMasterID + "))    \n"
					+ "     ) R   \n"
					+ "  LEFT JOIN PRO_CheckPoint CP ON R.ProductID=CP.ProductID AND R.WorkCenterID=CP.WorkCenterID "
					+ "  INNER JOIN MST_WorkCenter WC ON R.WorkCenterID=WC.WorkCenterID AND WC.IsMain = 1  \n"
					+ "  INNER JOIN ITM_Product P ON R.ProductID=P.ProductID AND P.MakeItem=1   \n"
					+ "  ORDER BY R.WorkOrderDetailID, R.ProductID, R.STEP   \n";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);
				dstPCS.Tables[0].TableName = ITM_ProductTable.TABLE_NAME;
				dstPCS.Tables[1].TableName = ITM_RoutingTable.TABLE_NAME;
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


		public DataTable GetChangeCategory(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetChangeCategory()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " + PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + "," 
					+ "ISNULL(" + PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ",0) CHANGETIME, " 
					+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ", " 
					+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ", " 
					+ "Master." + PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD
					+ " FROM PRO_ChangeCategoryMatrix Matrix"
					+ " INNER JOIN PRO_ChangeCategoryMaster Master ON Matrix.ChangeCategoryMasterID=Master.ChangeCategoryMasterID"
					+ " INNER JOIN MST_WorkCenter WC ON Master.WorkCenterID = WC.WorkCenterID"
					+ " WHERE Master.CCNID = (SELECT CCNID FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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

		public DataTable GetChangeCategory(int pintDCOptionMasterID, string pstrProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetChangeCategory()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " + PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMATRIXID_FLD + "," 
					+ "ISNULL(" + PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD + ",0) CHANGETIME, " 
					+ PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD + ", " 
					+ PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD + ", " 
					+ "Master." + PRO_ChangeCategoryMasterTable.WORKCENTERID_FLD
					+ " FROM PRO_ChangeCategoryMatrix Matrix"
					+ " INNER JOIN PRO_ChangeCategoryMaster Master ON Matrix.ChangeCategoryMasterID=Master.ChangeCategoryMasterID"
					+ " INNER JOIN MST_WorkCenter WC ON Master.WorkCenterID = WC.WorkCenterID"
					+ " WHERE Master.CCNID = (SELECT CCNID FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")"
					+ " AND WC.ProductionLineID IN (" + pstrProductionLineID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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

		public int GetLackOfYearInCalendar(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMinYearInCalendar()";
			DataSet dstPCS = new DataSet();
			
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
	
			try 
			{
				string strSql = "SELECT CASE WHEN " + ScheduleMethodEnum.Forward.GetHashCode() + "=( "
					+ " SELECT ScheduleCode  "
					+ " FROM PRO_DCOptionMaster  "
					+ " WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") THEN MAX(Year)+1 "
					+ " ELSE MIN(Year)-1 END"
					+ " FROM MST_WorkingDayMaster" 
					+ " WHERE CCNID=( "
					+ " SELECT CCNID  "
					+ " FROM PRO_DCOptionMaster  "
					+ " WHERE DCOptionMasterID=" + pintDCOptionMasterID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				odrPCS.Read();
				return int.Parse(odrPCS[0].ToString());
								
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


		public string[] GetFromYearToYear(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMaxYearInCalendar()";
			DataSet dstPCS = new DataSet();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT YEAR(AsOfDate),YEAR(AsOfDate + MaxDays) "
					+ "FROM PRO_DCOptionMaster "
					+ "WHERE DCOptionMasterID=" + pintDCOptionMasterID ;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				string[] strYears = new string[odrPCS.FieldCount];
				while (odrPCS.Read())
				{
					for(int i = 0; i < odrPCS.FieldCount; i++)
					{
						strYears[i] = odrPCS[i].ToString();
					}
				}		
				return strYears;
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
		public string[] GetWorkCenterNotConfig(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMaxYearInCalendar()";
			DataSet dstPCS = new DataSet();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT Code "
					+ " FROM MST_WorkCenter "
					+ " WHERE CCNID=("
					+ " SELECT CCNID "
					+ " FROM PRO_DCOptionMaster "
					+ " WHERE DCOptionMasterID=" + pintDCOptionMasterID 
					+ ") "
					+ " AND WorkCenterID NOT IN ("
					+ " SELECT WorkCenterID "
					+ " FROM PRO_WCCapacity "
					+ " WHERE CCNID=("
					+ " SELECT CCNID "
					+ " FROM PRO_DCOptionMaster "
					+ " WHERE DCOptionMasterID=" + pintDCOptionMasterID 
					+ ") "
					+ " AND ISNULL(Capacity,0) > 0)";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				string[] strYears = new string[1];
				while (odrPCS.Read())
				{
					strYears[0] = odrPCS[0].ToString();
				}		
				return strYears;
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
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetInvalidWOLineAndCPO(int pintDCOptionMasterID)
		{			
			const string METHOD_NAME = THIS + ".GetChangeCategory()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT workorderdetailid, 0 cpoid, startdate, duedate, itm_product.code, itm_product.description, itm_product.Revision,pro_workorderdetail.orderquantity quantity, " + ErrorCode.MESSAGE_MULTIDAY_WOLINE.ToString() + " errorcode FROM "
					+ " ((pro_workorderdetail INNER JOIN pro_workordermaster on pro_workorderdetail.workordermasterid = pro_workordermaster.workordermasterid)"
					+ " LEFT JOIN "
					+ " (SELECT pro_worouting.woroutingid, pro_worouting.productid FROM pro_worouting INNER JOIN mst_workcenter on pro_worouting.workcenterid = mst_workcenter.workcenterid WHERE mst_workcenter.ismain = 1) routing "
					+ " on pro_workorderdetail.productid = routing.productid)"
					+ " INNER JOIN itm_product on  pro_workorderdetail.productid = itm_product.productid"
					+ " WHERE DATEDIFF(dd,startdate,duedate) > 0"
					+ " and duedate > (SELECT asofdate FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and duedate < (SELECT DATEADD(dd,planhorizon,asofdate) FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and pro_workordermaster.MasterLocationID in (SELECT MasterLocationID FROM pro_dcoptiondetail WHERE pro_dcoptiondetail.dcoptionmasterid = " + pintDCOptionMasterID.ToString() + " and workorder = 1)"
					+ " and pro_workorderdetail.status = " + WOLineStatus.Released.GetHashCode().ToString()
					+ " UNION"
					+ " SELECT workorderdetailid, 0 cpoid, startdate, duedate, itm_product.code, itm_product.description, itm_product.Revision,pro_workorderdetail.orderquantity quantity, " + ErrorCode.MESSAGE_CPO_HAS_MULTI_MAIN_WC.ToString() + " errorcode FROM "
					+ " ((pro_workorderdetail INNER JOIN pro_workordermaster on pro_workorderdetail.workordermasterid = pro_workordermaster.workordermasterid)"
					+ " LEFT JOIN "
					+ " (SELECT pro_worouting.woroutingid routingid, pro_worouting.productid FROM pro_worouting INNER JOIN mst_workcenter on pro_worouting.workcenterid = mst_workcenter.workcenterid WHERE mst_workcenter.ismain = 1) routing "
					+ " on pro_workorderdetail.productid = routing.productid)"
					+ " INNER JOIN itm_product on  pro_workorderdetail.productid = itm_product.productid"
					+ " WHERE routing.routingid is null"
					+ " and duedate > (SELECT asofdate FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and duedate < (SELECT DATEADD(dd,planhorizon,asofdate) FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and pro_workordermaster.MasterLocationID in (SELECT MasterLocationID FROM pro_dcoptiondetail WHERE pro_dcoptiondetail.dcoptionmasterid = " + pintDCOptionMasterID.ToString() + " and workorder = 1)"
					+ " and pro_workorderdetail.status = " + WOLineStatus.Released.GetHashCode().ToString()
					+ " UNION"
					+ " SELECT null workorderdetailid, mtr_cpo.cpoid,startdate,duedate, itm_product.code, itm_product.description, itm_product.Revision,quantity, " + ErrorCode.MESSAGE_CPO_HAS_MULTI_MAIN_WC.ToString() + " errorcode FROM "
					+ " (mtr_cpo LEFT JOIN "
					+ " (SELECT itm_routing.routingid, itm_routing.productid FROM itm_routing INNER JOIN mst_workcenter on itm_routing.workcenterid = mst_workcenter.workcenterid WHERE mst_workcenter.ismain = 1) routing "
					+ " on mtr_cpo.productid = routing.productid)"
					+ " INNER JOIN itm_product on  mtr_cpo.productid = itm_product.productid"
					+ " WHERE routing.routingid is null"
					+ " and MRPCycleOptionMasterID is null"
					+ " and duedate > (SELECT asofdate FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and duedate < (SELECT DATEADD(dd,planhorizon,asofdate) FROM pro_dcoptionmaster INNER JOIN mtr_mpscycleoptionmaster on pro_dcoptionmaster.mpscycleoptionmasterid = mtr_mpscycleoptionmaster.mpscycleoptionmasterid WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")"
					+ " and mtr_cpo.MasterLocationID in (SELECT MasterLocationID FROM pro_dcoptiondetail WHERE pro_dcoptiondetail.dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")";


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetNotConfiguredWC(int pintDCOptionMasterID)
		{			
			const string METHOD_NAME = THIS + ".GetNotConfiguredWC()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT 	workcenterid,code,name,description " +
					" FROM 	mst_workcenter  " +
					" WHERE 	(workcenterid in " +
					" 	(SELECT distinct mst_workcenter.workcenterid " +
					" 	FROM 	mtr_cpo inner join itm_routing on mtr_cpo.productid = itm_routing.productid inner join mst_workcenter on itm_routing.workcenterid = mst_workcenter.workcenterid and mst_workcenter.ismain = 1  " +
					" 	WHERE	mtr_cpo.mpscycleoptionmasterid in " +
					" 		(SELECT mpscycleoptionmasterid " +
					" 			FROM pro_dcoptionmaster " +
					" 			WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ")))  " +
					" and  	(workcenterid not in " +
					" 	(SELECT distinct workcenterid " +
					" 	FROM 	pro_wccapacity " +
					" 	WHERE 	(capacity is not null) " +
					"))" +
					" and  	ccnid = " +
					" 	(SELECT ccnid FROM pro_dcoptionmaster WHERE dcoptionmasterid = " + pintDCOptionMasterID.ToString() + ") ";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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

		public DataTable SelectPlanningStartDate(int pintCycleOptionID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT " +
						PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD + ", " +
						PRO_PlanningOffsetTable.OFFSET_FLD + ", " +
						PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD + ", " +
						MST_WorkCenterTable.WORKCENTERID_FLD + " " +
						"FROM " +
						PRO_PlanningOffsetTable.TABLE_NAME  + " " +
						"INNER JOIN " + MST_WorkCenterTable.TABLE_NAME  + " " +
						"ON " + PRO_PlanningOffsetTable.TABLE_NAME + "." + PRO_PlanningOffsetTable.PRODUCTIONLINEID_FLD + "=" +
						MST_WorkCenterTable.TABLE_NAME + "." + MST_WorkCenterTable.PRODUCTIONLINEID_FLD + " AND " + MST_WorkCenterTable.ISMAIN_FLD + "=1 " +
						"WHERE " +
						PRO_PlanningOffsetTable.DCOPTIONMASTERID_FLD + "=" + pintCycleOptionID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);

				return dstPCS.Tables[PRO_DCOptionMasterTable.TABLE_NAME];
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

		public void UpdatePlanningStartDate(DataTable pdtbPlanningOffset)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT " +
						PRO_PlanningOffsetTable.PLANNINGOFFSETID_FLD + ", " +
						PRO_PlanningOffsetTable.OFFSET_FLD + ", " +
						PRO_PlanningOffsetTable.PLANNINGSTARTDATE_FLD + " " +
						"FROM " +
						PRO_PlanningOffsetTable.TABLE_NAME;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);                
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				//pdstData.EnforceConstraints = false;
				odadPCS.Update(pdtbPlanningOffset);

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
		/// 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetRelatedWorkCenter(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetRelatedWorkCenter()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" 	SELECT  " +
					"		DISTINCT  " + 
					"		MST_WorkCenter.WorkCenterID,  " +
					"		MST_WorkCenter.ProductionLineID,  " +
					"		MST_WorkCenter.Code,  " +
					"		MST_WorkCenter.Code WorkCenterCode,  " +
					"		MST_Department.Code DepartmentCode,  " +
					"		PRO_ProductionLine.BalancePlanning, MST_WorkCenter.SetMinProduce, " +
					"		PRO_ProductionLine.RoundUpDaysException,  " +
					"		PRO_PlanningOffset.PlanningOffsetID, " + 
					"		IsNull(PRO_PlanningOffset.Offset,0) AS Offset, " + 
					"		PRO_PlanningOffset.PlanningStartDate " + 
					"	FROM  " +
					"		MST_WorkCenter " + 
					"		INNER JOIN PRO_ProductionLine on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " +
					"		INNER JOIN MST_Department on PRO_productionline.DepartmentID = MST_Department.DepartmentID " +
					"		LEFT JOIN PRO_PlanningOffset ON PRO_PlanningOffset.ProductionLineID = pro_productionline.ProductionLineID AND DCOptionMasterID = " + pintDCOptionMasterID +
					"	WHERE  " +
					"		IsMain = 1 AND RunDCP = 1 ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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

		public DataTable GetRelatedWorkCenter(int pintDCOptionMasterID, string pstrProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetRelatedWorkCenter()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" 	SELECT  " +
					"		DISTINCT  " + 
					"		MST_WorkCenter.WorkCenterID,  " +
					"		MST_WorkCenter.ProductionLineID,  " +
					"		MST_WorkCenter.Code,  " +
					"		MST_WorkCenter.Code WorkCenterCode,  " +
					"		MST_Department.Code DepartmentCode,  " +
					"		PRO_ProductionLine.BalancePlanning, MST_WorkCenter.SetMinProduce, " +
					"		PRO_ProductionLine.RoundUpDaysException,  " +
					"		PRO_PlanningOffset.PlanningOffsetID, " + 
					"		IsNull(PRO_PlanningOffset.Offset,0) AS Offset, " + 
					"		PRO_PlanningOffset.PlanningStartDate " + 
					"	FROM  " +
					"		MST_WorkCenter " + 
					"		INNER JOIN PRO_ProductionLine on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " +
					"		INNER JOIN MST_Department on PRO_productionline.DepartmentID = MST_Department.DepartmentID " +
					"		LEFT JOIN PRO_PlanningOffset ON PRO_PlanningOffset.ProductionLineID = pro_productionline.ProductionLineID AND DCOptionMasterID = " + pintDCOptionMasterID +
					"	WHERE  " +
					"		IsMain = 1 AND RunDCP = 1 " +
					"	AND PRO_ProductionLine.ProductionLineID IN (" + pstrProductionLineID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ChangeCategoryMatrixTable.TABLE_NAME);

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
		/// Get safety stock for all product which produce in workcenter
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		public DataTable GetProductInfoTable(int pintProductionLineID) 
		{
			const string METHOD_NAME = THIS + ".GetSafetyStockTable()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT ProductID, LTVariableTime, " +
					" Case pro_wccapacity.WCType when 0 then pro_wccapacity.Factor " +
					" When 1 then pro_wccapacity.CrewSize " +
					" End MachineNo " +
					" FROM itm_product " +
					" Inner Join mst_workcenter on mst_workcenter.ProductionLineID = " + pintProductionLineID + " and IsMain=1 " +
					" Inner Join pro_wccapacity on pro_wccapacity.WorkCenterID = mst_workcenter.WorkCenterID " +
					" WHERE itm_product.ProductionLineID = " + pintProductionLineID +
					" ORDER BY Itm_product.ProductID";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		
		#region Bottle feed algorithm

		public DataTable GetCPOInCycle(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetCPOInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					"SELECT " +
					"	MTR_CPO.CPOID, " +
					"	MTR_CPO.Quantity, " +
					"	MTR_CPO.DueDate, " +
					"	MTR_CPO.ProductID, " +
					"	MST_WorkCenter.WorkCenterID, " +
					"	MST_WorkCenter.Code WorkCenterCode, " +
					"	PRO_CheckPoint.SamplePattern, " +
					"	PRO_CheckPoint.SampleRate, " +
					"	PRO_CheckPoint.DelayTime, " +
					"	ITM_Product.LTSalesATP, " +
					"	ITM_Routing.RoutingID, " +
					"	CASE " +
					"		WHEN (SELECT IgnoreMoveTime FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ") = 1 THEN " +
					"			CASE " +
					"				WHEN ITM_Routing.Pacer = 'M' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime " +
					"				WHEN ITM_Routing.Pacer = 'L' THEN " +
					"					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					"				WHEN ITM_Routing.Pacer = 'B' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					"			END " +
					"		ELSE " +
					"			CASE " +
					"				WHEN ITM_Routing.Pacer = 'M' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.MoveTime " +
					"				WHEN ITM_Routing.Pacer = 'L' THEN " +
					"					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					"				WHEN ITM_Routing.Pacer = 'B' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					"			END " +
					"	END LeadTime, " +
					"	0 Level ," +
					"	0 Handled ," +
					"	NULL CapacityBottleID "+
					"FROM  " +
					"	MTR_CPO INNER JOIN ITM_Product ON MTR_CPO.ProductID = ITM_Product.ProductID " +
					"		INNER JOIN ITM_Routing ON ITM_Product.ProductID = ITM_Routing.ProductID " +
					"		INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1 " +
					"		LEFT JOIN PRO_CheckPoint ON PRO_CheckPoint.ProductID = MTR_CPO.ProductID AND PRO_CheckPoint.WorkCenterID = MST_WorkCenter.WorkCenterID " +
					"WHERE " +
					"	MTR_CPO.IsMPS = 1 AND MTR_CPO.IsSafetyStock = 0 AND MTR_CPO.ParentCPOID IS NULL " +
					"	AND MPSCycleOptionMasterID = (SELECT MPSCycleOptionMasterID FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ") " +
					" 	AND MTR_CPO.DueDate >= " +
					" 		(SELECT AsOfDate FROM MTR_MPSCycleOptionMaster WHERE MPSCycleOptionMasterID = (SELECT MPSCycleOptionMasterID FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + "))" +
					" 	AND MTR_CPO.DueDate <= DateAdd(dd," +
					" 		(SELECT PlanHorizon FROM MTR_MPSCycleOptionMaster WHERE MPSCycleOptionMasterID = (SELECT MPSCycleOptionMasterID FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + "))," +
					" 		(SELECT AsOfDate FROM MTR_MPSCycleOptionMaster WHERE MPSCycleOptionMasterID = (SELECT MPSCycleOptionMasterID FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ")))";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				/*dstPCS.Tables[0].Columns[MTR_CPOTable.CPOID_FLD].AutoIncrement = true;
				dstPCS.Tables[0].Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementSeed = 0;
				dstPCS.Tables[0].Columns[MTR_CPOTable.CPOID_FLD].AutoIncrementStep = 1;*/

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

		
		public DataTable GetWCConfigInCycle(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetCPOInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT " +
					" 	PRO_WCCapacity.WorkCenterID," +
					"	MST_WorkCenter.Code WorkCenterCode," +
					" 	PRO_WCCapacity.Factor," +
					" 	IsNull(PRO_WCCapacity.CrewSize,0) CrewSize," +
					" 	IsNull(PRO_WCCapacity.MachineNo,0) MachineNo," +
					" 	PRO_WCCapacity.WCType," +
					" 	PRO_WCCapacity.Capacity," +
					"	PRO_WCCapacity.BeginDate," +
					"	PRO_WCCapacity.EndDate," +
					"	PRO_ShiftPattern.ShiftID," +
					"	PRO_ShiftPattern.WorkTimeFrom," +
					"	PRO_ShiftPattern.WorkTimeTo," +
					"	PRO_ShiftPattern.RegularStopFrom," +
					"	PRO_ShiftPattern.RegularStopTo," +
					"	PRO_ShiftPattern.RefreshingFrom," +
					"	PRO_ShiftPattern.RefreshingTo," +
					"	PRO_ShiftPattern.ExtraStopFrom," +
					"	PRO_ShiftPattern.ExtraStopTo," +

					" Case WCType When 1 Then " +
					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.MachineNo,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" when 0 then " +
 					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.CrewSize,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" end ShiftCapacity " +

					" FROM " +
					"	PRO_WCCapacity	INNER JOIN PRO_ShiftCapacity ON  PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID" +
					"			INNER JOIN PRO_ShiftPattern ON PRO_ShiftCapacity.ShiftID = PRO_ShiftPattern.ShiftID" +
					"			INNER JOIN MST_WorkCenter ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1" +
					" WHERE	PRO_WCCapacity.BeginDate <= (SELECT DateAdd(dd,PlanHorizon,AsOfDate) FROM PRO_DCOptionMaster WHERE PRO_DCOptionMaster.DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ")" +
					"	AND" +
					"	PRO_WCCapacity.EndDate >= (SELECT DATEADD(dd,-20,AsOfDate) FROM PRO_DCOptionMaster WHERE PRO_DCOptionMaster.DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetWCConfigInCycle(int pintDCOptionMasterID, string pstrProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetWCConfigInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT " +
					" 	PRO_WCCapacity.WorkCenterID," +
					"	MST_WorkCenter.Code WorkCenterCode," +
					" 	PRO_WCCapacity.Factor," +
					" 	IsNull(PRO_WCCapacity.CrewSize,0) CrewSize," +
					" 	IsNull(PRO_WCCapacity.MachineNo,0) MachineNo," +
					" 	PRO_WCCapacity.WCType," +
					" 	PRO_WCCapacity.Capacity," +
					"	PRO_WCCapacity.BeginDate," +
					"	PRO_WCCapacity.EndDate," +
					"	PRO_ShiftPattern.ShiftID," +
					"	PRO_ShiftPattern.WorkTimeFrom," +
					"	PRO_ShiftPattern.WorkTimeTo," +
					"	PRO_ShiftPattern.RegularStopFrom," +
					"	PRO_ShiftPattern.RegularStopTo," +
					"	PRO_ShiftPattern.RefreshingFrom," +
					"	PRO_ShiftPattern.RefreshingTo," +
					"	PRO_ShiftPattern.ExtraStopFrom," +
					"	PRO_ShiftPattern.ExtraStopTo," +

					" Case WCType When 1 Then " +
					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.MachineNo,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" when 0 then " +
					"  (ISNULL(PRO_WCCapacity.Factor,0)/100) * IsNull(PRO_WCCapacity.CrewSize,0) * ( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) )  " +
					" end ShiftCapacity " +

					" FROM " +
					"	PRO_WCCapacity	INNER JOIN PRO_ShiftCapacity ON  PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID" +
					"			INNER JOIN PRO_ShiftPattern ON PRO_ShiftCapacity.ShiftID = PRO_ShiftPattern.ShiftID" +
					"			INNER JOIN MST_WorkCenter ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1" +
					" WHERE	PRO_WCCapacity.BeginDate <= (SELECT DateAdd(dd,PlanHorizon,AsOfDate) FROM PRO_DCOptionMaster WHERE PRO_DCOptionMaster.DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ")" +
					"	AND" +
					"	PRO_WCCapacity.EndDate >= (SELECT DATEADD(dd,-20,AsOfDate) FROM PRO_DCOptionMaster WHERE PRO_DCOptionMaster.DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ")" + 
					" AND MST_WorkCenter.ProductionLineID IN (" + pstrProductionLineID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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


		public DataTable GetWCOutside()
		{
			const string METHOD_NAME = THIS + ".GetCPOInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					  " select workcenterid from mst_workcenter where productionlineid in  "
					  + " (select productionlineid from pro_productionline where departmentid=" 
					  + "(select departmentid from mst_department where code = 'MAKER')) ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetProductionGroup()
		{		
			const string METHOD_NAME = THIS + ".GetProductionGroup()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " +
								"	PRO_ProductionGroup.ProductionGroupID, " +
								"	PRO_ProductionGroup.GroupProductionMax, " +
								"	PRO_ProductionGroup.ProductionLineID, WC.WorkCenterID, " +
								"   PRO_ProductionGroup.CapacityOfGroup, " + 
								"	ISNULL(PRO_ProductionGroup.Priority,100) Priority, " +
								"	PRO_PGProduct.ProductID " +
								"FROM " + 
								"	PRO_ProductionGroup " + 
								"   INNER JOIN (select productionlineid,workcenterid from mst_workcenter where ismain=1) WC ON PRO_ProductionGroup.ProductionLineID=WC.ProductionLineID" +
								"	INNER JOIN PRO_PGProduct ON PRO_ProductionGroup.ProductionGroupID = PRO_PGProduct.ProductionGroupID ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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


		public DataTable GetGroupCapacity()
		{		
			const string METHOD_NAME = THIS + ".GetGroupCapacity()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " +
					"	PRO_ProductionGroup.ProductionGroupID, " +
					"	PRO_ProductionGroup.GroupProductionMax, " +
					"	PRO_ProductionGroup.ProductionLineID, WC.WorkCenterID, " +
					"   ISNULL(PRO_ProductionGroup.CapacityOfGroup, 0) CapacityOfGroup, " + 
					"	ISNULL(PRO_ProductionGroup.Priority,100) Priority " +
					"FROM " + 
					"	PRO_ProductionGroup " + 
					"   INNER JOIN (SELECT Productionlineid,workcenterid from mst_workcenter where ismain=1) WC ON PRO_ProductionGroup.ProductionLineID=WC.ProductionLineID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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


		public DataTable GetProductInfo(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetProductInfo()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT  " + 
					" 	DISTINCT ITM_Product.ProductID, ITM_Product.Revision, NULL Level, IsNull(ITM_Product.SafetyStock,0) SafetyStock,IsNull(ITM_Product.MinProduce,0) MinProduce,IsNull(ITM_Product.MaxProduce,0) MaxProduce,IsNull(ITM_Product.ScrapPercent,0) ScrapPercent, " + 
					" 	IsNull(ITM_Product.OrderQuantityMultiple,1) OrderQuantityMultiple, " +
					" 	IsNull(ITM_Product.MaxRoundUpToMin,0) MaxRoundUpToMin, " +
					" 	IsNull(ITM_Product.MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, " +
					"	ITM_Product.CategoryID, " +
					" 	0 PGPriority, " +
					" 	CASE " +
					" 		WHEN EXISTS (SELECT ITM_BOM.BOMID FROM ITM_BOM WHERE ITM_BOM.ComponentID = ITM_Product.ProductID) THEN " +
					" 			1 " +
					" 		ELSE " +
					" 			0 " +
					" 	END HasParent, " +
					"   ISNULL(ITM_Product.LTVariableTime,0) LeadTime, " +
					"	ISNULL(ITM_Routing.FixLT,0) FixLT, " +
					"	MST_WorkCenter.WorkCenterID, " +
					"	MST_WorkCenter.ProductionLineID, " +
					"	MST_WorkCenter.Code WorkCenterCode, " +
					"	ITM_Routing.RoutingID, " +
					"	IsNull(PRO_Checkpoint.SamplePattern,0) SamplePattern, " +
					"	IsNull(PRO_Checkpoint.SampleRate,0) SampleRate, " +
					"	IsNull(PRO_Checkpoint.DelayTime,0) DelayTime " +
					" FROM  " + 
					" 	ITM_Product " + 
					"	INNER JOIN ITM_Routing ON ITM_Product.ProductID = ITM_Routing.ProductID " +
					"	INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1 " +
					"	LEFT JOIN PRO_Checkpoint ON MST_WorkCenter.WorkCenterID = MST_WorkCenter.WorkCenterID AND PRO_Checkpoint.ProductID = ITM_Routing.ProductID " +
					" ORDER BY HasParent ASC ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		
		public DataSet GetProductSetupPair()
		{
			const string METHOD_NAME = THIS + ".GetProductPair()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" Select ProductID, SetupPair, (select WorkCenterID from mst_workcenter where ismain=1 and productionlineid = itm_product.productionlineid) WorkCenterID From itm_product Where len(setuppair) > 0 order by SetupPair; "
					+ " Select Distinct SetupPair, (select WorkCenterID from mst_workcenter where ismain=1 and productionlineid = itm_product.productionlineid) WorkCenterID From itm_product Where len(setuppair) > 0 order by SetupPair; ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		
		
		public DataTable GetProductPair()
		{
			const string METHOD_NAME = THIS + ".GetProductPair()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					"SELECT " +
					"	P1.ProductID ProductID1, " +
					"	P2.ProductID ProductID2 " + 
					"FROM " + 
					"	ITM_Product P1 INNER JOIN ITM_Product P2 ON P1.Revision = P2.Revision AND P1.ProductID <> P2.ProductID AND P1.ProductionlineID = P2.ProductionlineID AND P1.CategoryID = P2.CategoryID ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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


		public DataTable GetBOMInfo(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetBOMInfo()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT DISTINCT " +
					"	ITM_BOM.ProductID, " +
					"	ITM_BOM.ComponentID, " + 
					"	ITM_BOM.Quantity, " +
					"	IsNull(ITM_BOM.LeadTimeOffset,0) LeadTimeOffset, " +
					"	IsNull(ITM_BOM.Shrink,0) Shrink, " +
					"	MST_WorkCenter.WorkCenterID, " +
					"	MST_WorkCenter.Code WorkCenterCode, " +
					" 	PRO_CheckPoint.SamplePattern, " +
					" 	PRO_CheckPoint.SampleRate, " +
					" 	PRO_CheckPoint.DelayTime, " +
					" 	ITM_Routing.RoutingID, " +
					"	W2.WorkCenterID ParentWCID, " +
					"	W2.Code ParentWCCode, " +
					"	ITM_Product.Revision, " +
					" 	IsNull(ITM_Product.OrderQuantityMultiple,1) OrderQuantityMultiple, " +
					"	IsNull(ITM_Product.MinProduce,0) MinProduce," +
					"	IsNull(ITM_Product.MaxProduce,0) MaxProduce," +
					"	IsNull(ITM_Product.ScrapPercent,0) ScrapPercent," +
					" 	IsNull(ITM_Product.MaxRoundUpToMin,0) MaxRoundUpToMin, " +
					" 	IsNull(ITM_Product.MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, " +
					"	CASE " +
					"		WHEN (SELECT IgnoreMoveTime FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID.ToString() + ") = 1 THEN " +
					"			CASE " +
					"				WHEN ITM_Routing.Pacer = 'M' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime " +
					"				WHEN ITM_Routing.Pacer = 'L' THEN " +
					"					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					"				WHEN ITM_Routing.Pacer = 'B' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					"			END " +
					"		ELSE " +
					"			CASE " +
					"				WHEN ITM_Routing.Pacer = 'M' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.MoveTime " +
					"				WHEN ITM_Routing.Pacer = 'L' THEN " +
					"					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					"				WHEN ITM_Routing.Pacer = 'B' THEN " +
					"					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					"			END " +
					"	END LeadTime, " +
					"	ITM_Routing.FixLT " +
					" FROM ITM_BOM " +
					"	INNER JOIN ITM_Product ON ITM_Product.ProductID = ITM_BOM.ComponentID " +
					"	INNER JOIN ITM_Routing ON ITM_BOM.ComponentID = ITM_Routing.ProductID " +
					"	INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WOrkCenter.IsMain = 1 " +
					"	INNER JOIN ITM_Routing R2 ON ITM_BOM.ProductID = R2.ProductID " +
					"	INNER JOIN MST_WorkCenter W2 ON R2.WorkCenterID = W2.WorkCenterID AND W2.IsMain = 1 " +
					"	LEFT JOIN PRO_CheckPoint ON ITM_Routing.ProductID = PRO_CheckPoint.ProductID " +
					" WHERE ITM_Product.MakeItem = 1";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		
		
		public DataSet GetResultDataSet(bool pblnData)
		{
			const string METHOD_NAME = THIS + ".GetResultDataSet()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				//Master table
				string strSql = "SELECT ";
				if (!pblnData)
				{
					strSql += " TOP 0 ";
				}
				strSql +=
					PRO_DCPResultMasterTable.ROUTINGID_FLD + "," +
					PRO_DCPResultMasterTable.CPOID_FLD + "," +
					PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "," +
					PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "," +
					PRO_DCPResultMasterTable.DUEDATETIME_FLD + "," +
					PRO_DCPResultMasterTable.PRODUCTID_FLD + "," +
					PRO_DCPResultMasterTable.QUANTITY_FLD + "," +
					PRO_DCPResultMasterTable.STARTDATETIME_FLD + "," +
					PRO_DCPResultMasterTable.WORKCENTERID_FLD + "," +
					PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + "," +
					PRO_DCPResultMasterTable.WOROUTINGID_FLD +
					" FROM PRO_DCPResultMaster";
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.FillSchema(dstPCS,SchemaType.Mapped,PRO_DCPResultMasterTable.TABLE_NAME);
				odadPCS.Fill(dstPCS,PRO_DCPResultMasterTable.TABLE_NAME);

				//Detail table
				strSql = "SELECT ";
				if (!pblnData)
				{
					strSql += " TOP 0 ";
				}
				strSql +=
					PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + "," +
					PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "," +
					PRO_DCPResultDetailTable.ENDTIME_FLD + "," +
					PRO_DCPResultDetailTable.PERCENTAGE_FLD + "," +
					PRO_DCPResultDetailTable.QUANTITY_FLD + "," +
					PRO_DCPResultDetailTable.SHIFTID_FLD + "," +
					PRO_DCPResultDetailTable.STARTTIME_FLD + "," +
					PRO_DCPResultDetailTable.TOTALSECOND_FLD + "," +
					PRO_DCPResultDetailTable.TYPE_FLD + "," +
					PRO_DCPResultDetailTable.WOCONVERTED_FLD + "," +
					PRO_DCPResultDetailTable.WOGENERATEDID_FLD + "," +
					PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT + "," +
					PRO_DCPResultDetailTable.WORKINGDATE_FLD + 
					" FROM PRO_DCPResultDetail";				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.FillSchema(dstPCS,SchemaType.Mapped,PRO_DCPResultDetailTable.TABLE_NAME);
				odadPCS.Fill(dstPCS,PRO_DCPResultDetailTable.TABLE_NAME);

				//Dummy column
				dstPCS.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Columns.Add("CapacityBottleID",typeof(int));
				dstPCS.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Columns.Add("ProductID",typeof(int));
				dstPCS.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Columns.Add("WorkCenterCode",typeof(string));

				//Relations
				DataRelation orelDCPResult = new DataRelation(string.Empty,dstPCS.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Columns[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD],dstPCS.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Columns[PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD]);
				dstPCS.Relations.Add(orelDCPResult);

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

		public void UpdateResultDataSet(DataSet pdstDCPResult)
		{
			const string METHOD_NAME = THIS + ".UpdateResultDataSet()";
			string strSqlMaster;
			string strSqlDetail;
			OleDbConnection oconPCS =null;
			
			OleDbDataAdapter odadMaster = new OleDbDataAdapter();
			OleDbCommandBuilder odcbDetail ;
			OleDbDataAdapter odadDetail = new OleDbDataAdapter();

			odadMaster.RowUpdated += new OleDbRowUpdatedEventHandler(OnDCPResultRowUpdated);
			odadDetail.RowUpdated += new OleDbRowUpdatedEventHandler(OnDCPResultRowUpdated);

			try
			{                
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);                                
				pdstDCPResult.EnforceConstraints = false;

				strSqlMaster =	
					"SELECT " +
					PRO_DCPResultMasterTable.ROUTINGID_FLD + "," +
					PRO_DCPResultMasterTable.CPOID_FLD + "," +
					PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "," +
					PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "," +
					PRO_DCPResultMasterTable.DUEDATETIME_FLD + "," +
					PRO_DCPResultMasterTable.PRODUCTID_FLD + "," +
					PRO_DCPResultMasterTable.QUANTITY_FLD + "," +
					PRO_DCPResultMasterTable.STARTDATETIME_FLD + "," +
					PRO_DCPResultMasterTable.WORKCENTERID_FLD + "," +
					PRO_DCPResultMasterTable.WORKORDERDETAILID_FLD + "," +
					PRO_DCPResultMasterTable.WOROUTINGID_FLD +
					" FROM PRO_DCPResultMaster";	
				odadMaster.SelectCommand = new OleDbCommand(strSqlMaster, oconPCS);
                odadMaster.SelectCommand.CommandTimeout = 10000;
				string strSqlMasterInsert = 
					"INSERT INTO PRO_DCPResultMaster(RoutingID, CPOID, DCOptionMasterID, DueDateTime, ProductID, Quantity, StartDateTime, WorkCenterID, WorkOrderDetailID, WORoutingID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ; SELECT SCOPE_IDENTITY() AS DCPResultMasterID";
				odadMaster.InsertCommand = new OleDbCommand(strSqlMasterInsert,oconPCS);
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("RoutingID",OleDbType.Integer,4,"RoutingID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("CPOID",OleDbType.Integer,4,"CPOID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("DCOptionMasterID",OleDbType.Integer,4,"DCOptionMasterID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("DueDateTime",OleDbType.Date,8,"DueDateTime"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("ProductID",OleDbType.Integer,4,"ProductID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("Quantity",OleDbType.Decimal,13,"Quantity"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("StartDateTime",OleDbType.Date,8,"StartDateTime"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("WorkCenterID",OleDbType.Integer,4,"WorkCenterID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("WorkOrderDetailID",OleDbType.Integer,4,"WorkOrderDetailID"));				
				odadMaster.InsertCommand.Parameters.Add(new OleDbParameter("WORoutingID",OleDbType.Integer,4,"WORoutingID"));				
                
				odadMaster.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
				DataRow[] arrAddedMaster = pdstDCPResult.Tables[PRO_DCPResultMasterTable.TABLE_NAME].Select(string.Empty,string.Empty,DataViewRowState.Added);
				int intResult = odadMaster.Update(arrAddedMaster);

				strSqlDetail =	
					"SELECT " +
					PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + "," +
					PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "," +
					PRO_DCPResultDetailTable.ENDTIME_FLD + "," +
					PRO_DCPResultDetailTable.PERCENTAGE_FLD + "," +
					PRO_DCPResultDetailTable.QUANTITY_FLD + "," +
					PRO_DCPResultDetailTable.SHIFTID_FLD + "," +
					PRO_DCPResultDetailTable.STARTTIME_FLD + "," +
					PRO_DCPResultDetailTable.TOTALSECOND_FLD + "," +
					PRO_DCPResultDetailTable.TYPE_FLD + "," +
					PRO_DCPResultDetailTable.WOCONVERTED_FLD + "," +
					PRO_DCPResultDetailTable.WOGENERATEDID_FLD + "," +
					PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT + "," +
					PRO_DCPResultDetailTable.WORKINGDATE_FLD +
					" FROM PRO_DCPResultDetail";
                odadDetail.SelectCommand = new OleDbCommand(strSqlDetail, oconPCS);
                odadDetail.SelectCommand.CommandTimeout = 10000;
				odcbDetail = new OleDbCommandBuilder(odadDetail);
				odadDetail.Update(pdstDCPResult.Tables[PRO_DCPResultDetailTable.TABLE_NAME].Select(string.Empty,string.Empty,DataViewRowState.Added));

				pdstDCPResult.EnforceConstraints = true;
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

		private void OnDCPResultRowUpdated(object sender, OleDbRowUpdatedEventArgs e)
		{
			if (e.Status == UpdateStatus.ErrorsOccurred)
			{
				e.ToString();
			}
			else
			{
				e.ToString();
			}
			if ((e.StatementType == StatementType.Insert) || (e.StatementType == StatementType.Delete))
			{
				e.Status = UpdateStatus.SkipCurrentRow;
			}
		}

		public void DeleteDCPResult(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteDCPResult()";
			string strSql = string.Empty;
				/*"DELETE " + PRO_DCPResultDetailTable.TABLE_NAME + " WHERE " 
				+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN "
				+ " (SELECT "
				+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD
				+ " FROM "
				+ PRO_DCPResultMasterTable.TABLE_NAME
				+ " WHERE "
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD
				+ "=" + pintDCOptionMasterID + ");";*/
			
			//cascade delete

			strSql += " DELETE " + PRO_DCPResultMasterTable.TABLE_NAME
				+ " WHERE "
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD
				+ "=" + pintDCOptionMasterID;
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 10000;

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[0].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
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

		public void DeleteDCPResult(int pintDCOptionMasterID, string pstrProductionLineID)
		{
			const string METHOD_NAME = THIS + ".DeleteDCPResult()";
			string strSql = string.Empty;

			strSql += " DELETE " + PRO_DCPResultMasterTable.TABLE_NAME
				+ " WHERE " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "=" + pintDCOptionMasterID
				+ " AND WorkCenterID IN (SELECT WorkCenterID FROM MST_WorkCenter"
				+ " WHERE IsMain = 1 AND ProductionLineID IN (" + pstrProductionLineID + "))";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 10000;

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[0].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
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

		public void DeleteDCPResult(int pintDCOptionMasterID, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".DeleteDCPResult()";
			string strSql = string.Empty;
			/*"DELETE " + PRO_DCPResultDetailTable.TABLE_NAME + " WHERE " 
				+ PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN "
				+ " (SELECT "
				+ PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD
				+ " FROM "
				+ PRO_DCPResultMasterTable.TABLE_NAME
				+ " WHERE "
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD
				+ "=" + pintDCOptionMasterID + ");";*/
			
			//cascade delete

			strSql += " DELETE " + PRO_DCPResultMasterTable.TABLE_NAME
				+ " WHERE "
				+ PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD
				+ "=" + pintDCOptionMasterID
				+ " AND "
				+ MST_WorkCenterTable.WORKCENTERID_FLD
				+ "=" + pintWorkCenterID;
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

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
				if (ex.Errors[0].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
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
		/// Get all delivery schedule where ScheduleDate belong [AsofDate->AsofDate+PlanHorizon]
		/// of DCOptionMasterID
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetDeliveryScheduleData(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleData()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT 	SO_DeliverySchedule.DeliveryQuantity, " +
					"  DateAdd(ss,-IsNull(ITM_Product.LTSafetyStock,0),SO_DeliverySchedule.ScheduleDate) ScheduleDate, " +
					"  ITM_Product.SafetyStock, " +
					"  ITM_Product.Revision, " +
					"  IsNull(ITM_Product.OrderQuantityMultiple,1) OrderQuantityMultiple, " +
					"  IsNull(ITM_Product.MinProduce,0) MinProduce, " +
					"  IsNull(ITM_Product.MaxProduce,0) MaxProduce, " +
					"  IsNull(ITM_Product.ScrapPercent,0) ScrapPercent, " +
					"  IsNull(ITM_Product.MaxRoundUpToMin,0) MaxRoundUpToMin, " +
					"  IsNull(ITM_Product.MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, " +
					"  SO_SaleOrderDetail.ProductID, " +
					"  MST_WorkCenter.WorkCenterID, " +
					"  MST_WorkCenter.Code WorkCenterCode, " +
					"  PRO_CheckPoint.SamplePattern, " +
					"  PRO_CheckPoint.SampleRate, " +
					"  PRO_CheckPoint.DelayTime, " +
					"  ITM_Routing.RoutingID, ITM_Product.LTVariableTime LeadTime," +
					"  ITM_Routing.FixLT, " +
					"  0 CapacityBottleID " +
					" FROM 	SO_DeliverySchedule  " +
					"   INNER JOIN SO_SaleOrderDetail ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " +
					"   INNER JOIN ITM_Routing ON SO_SaleOrderDetail.ProductID = ITM_Routing.ProductID " +
					"   INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID " +
					"   INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1 " +
					"   LEFT JOIN PRO_CheckPoint ON PRO_CheckPoint.ProductID = SO_SaleOrderDetail.ProductID AND PRO_CheckPoint.WorkCenterID = MST_WorkCenter.WorkCenterID " +
					" WHERE  ScheduleDate > (SELECT DateAdd(dd,0,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
					"   AND ScheduleDate <= (SELECT DateAdd(dd,PlanHorizon+1,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ")  " +
					" ORDER BY SO_DeliverySchedule.ScheduleDate ASC ";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		/// SELECT	DISTINCT SO_DeliverySchedule.ScheduleDate 
		/// FROM 	SO_DeliverySchedule [inside DCP cycle] and ORDER BY SO_DeliverySchedule.ScheduleDate ASC
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetDeliveryScheduleTime(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleTime()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT	DISTINCT SO_DeliverySchedule.ScheduleDate " +
					" FROM 	SO_DeliverySchedule  " +
					" WHERE 	ScheduleDate > (SELECT AsOfDate FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
					" 	AND ScheduleDate < (SELECT DateAdd(dd,PlanHorizon,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ")  " +
					" ORDER BY SO_DeliverySchedule.ScheduleDate ASC ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetTopLevelWorkCenter()
		{
			const string METHOD_NAME = THIS + ".GetTopLevelWorkCenter()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = " SELECT DISTINCT MST_WorkCenter.WorkCenterID"
					+ " FROM ITM_Routing INNER JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " AND MST_WorkCenter.IsMain = 1 "
					+ " AND MST_WorkCenter.RunDCP = 1"
					+ " WHERE ProductID IN (SELECT ProductID FROM ITM_BOM) AND ProductID NOT IN (SELECT ComponentID FROM ITM_BOM) ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetTopLevelWorkCenter(string pstrProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetTopLevelWorkCenter()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = " SELECT DISTINCT MST_WorkCenter.WorkCenterID"
					+ " FROM MST_WorkCenter"
					+ " WHERE IsMain = 1 "
					+ " AND RunDCP = 1"
					+ " AND ProductionLineID IN (" + pstrProductionLineID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		/// Get sum(bc.ohquantity-isnull(bc.commitquantity,0)) AvailQuantity FROM iv_bincache
		/// where bintypeid=1 [is OK] or bintypeid=4 [is Buffer]
		/// </summary>
		/// <returns></returns>
		public DataTable GetAvailQuantity()
		{
			const string METHOD_NAME = THIS + ".GetAvailQuantity()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT bc.productid,sum(bc.ohquantity-isnull(bc.commitquantity,0)) AvailQuantity FROM iv_bincache bc" +
					" INNER JOIN mst_bin b on bc.binid=b.binid where bintypeid=1 or bintypeid=4" +
					" GROUP BY bc.productid";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		/// 
		/// </summary>
		/// <returns></returns>
		public DataTable DCPAvailQuantity(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".DCPAvailQuantity()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT productid,Quantity AvailQuantity FROM dcp_beginquantity WHERE DCOptionMasterID = " + pintDCOptionMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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


		public void CloseWorkOrderInCycle(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".CloseWorkOrderInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = 
				" UPDATE " +
				PRO_WorkOrderDetailTable.TABLE_NAME + " " +
				" SET " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.MfgClose + "," + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + "=GetDate() " +
				" WHERE 	DueDate >= (SELECT DateAdd(dd,1,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
				" 	AND DueDate < (SELECT DateAdd(dd,PlanHorizon+1,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " + 
				"	AND " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.Released ;
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
	
		public void CloseWorkOrderInCycle(int pintDCOptionMasterID, int p_intWorkCenterID, DateTime p_dtmPlanningStartDate)
		{
			const string METHOD_NAME = THIS + ".CloseWorkOrderInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = 
				" UPDATE " +
				PRO_WorkOrderDetailTable.TABLE_NAME + " " +
				" SET " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.MfgClose + "," + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + "=GetDate() " +
				" WHERE 	DueDate > (SELECT DateAdd(dd,0,'" + p_dtmPlanningStartDate + "') FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
				" 	AND DueDate < (SELECT DateAdd(dd,PlanHorizon+1,'" + p_dtmPlanningStartDate + "') FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " + 
				"	AND " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.Released ;
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

	
		public void CloseWorkOrderInCycleForDCPTool(int pintDCOptionMasterID, int p_intWorkCenterID, DateTime p_dtmPlanningStartDate)
		{
			const string METHOD_NAME = THIS + ".CloseWorkOrderInCycle()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = 
				" UPDATE " +
				PRO_WorkOrderDetailTable.TABLE_NAME + " " +
				" SET " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.MfgClose + "," + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + "=GetDate() " +
				" WHERE 	DueDate > (SELECT DateAdd(dd,0,'" + p_dtmPlanningStartDate + "') FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
				" 	AND DueDate < (SELECT DateAdd(dd,PlanHorizon+1,'" + p_dtmPlanningStartDate + "') FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " + 
				"	AND " + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.Released  +
				"   AND ProductID IN (Select ProductID From ITM_Routing WHERE WorkCenterID = " + p_intWorkCenterID + ")";
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

		#endregion

		#region Manual Production Planning
		/// <summary>
		/// Get struct data of PRO_DCPResultDetail table if pblnNoData == true else Get existed DCP data
		/// and get struct data PRO_DCPResultMaster only
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <param name="pintDCOptionMasterID"></param>
		/// <param name="pblnNoData"></param>
		/// <returns></returns>
		public DataSet GetDCPData(int pintProductionLineID, int pintDCOptionMasterID, bool pblnNoData)
		{
			const string METHOD_NAME = THIS + ".GetDCPData()";
			DataSet dstPCS = new DataSet();
					
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				//select detail data
				string strSql = " SELECT ";
				if (pblnNoData)
				{
					strSql += "	TOP 0 ";
				}

				strSql +=
					" 	ITM_Product.ProductID, " +
					" 	ITM_Product.Code, " +
					" 	ITM_Product.Description, " +
					" 	ITM_Product.Revision, " +
					" 	MST_UnitOfMeasure.Code AS StockUMCode, " +
					" 	PRO_DCPResultMaster.DCPResultMasterID, " +
					" 	PRO_DCPResultDetail.DCPResultDetailID, " +
					" 	PRO_DCPResultDetail.StartTime, " +
					" 	PRO_DCPResultDetail.EndTime, " +
					" 	PRO_DCPResultDetail.TotalSecond, " +
					" 	PRO_DCPResultDetail.Quantity, " +
					" 	PRO_DCPResultDetail.Percentage, " +
					" 	PRO_DCPResultDetail.WorkingDate, " +
					" 	PRO_DCPResultDetail.ShiftID, " +
					" 	PRO_DCPResultDetail.SafetyStockAmount, " +
					" 	PRO_DCPResultDetail.Type, " +
					" 	PRO_DCPResultDetail.IsManual, " +
					" 	PRO_DCPResultDetail.Converted, " +
					
					" 	PRO_DCPResultDetail.WOGeneratedID, " +
					" 	PRO_DCPResultDetail.POGeneratedID, " +

					"   PRO_DCPResultMaster.RoutingID, " +
					"	PRO_Shift.ShiftID, " +
					"	PRO_Shift.ShiftDesc, " +

					" 	PRO_DCPResultDetail.Quantity OriginalQuantity, " +
					" 	PRO_DCPResultDetail.StartTime OriginalStartTime " +

					" FROM  " +
					" 	PRO_DCPResultDetail " +
					" 	INNER JOIN PRO_DCPResultMaster " +
					" 		ON PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID " +
					" 	INNER JOIN ITM_Product " +
					" 		ON PRO_DCPResultMaster.ProductID = ITM_Product.ProductID " +
					" 	INNER JOIN MST_UnitOfMeasure " +
					" 		ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID " +
					" 	INNER JOIN MST_WorkCenter " +
					" 		ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID " +
//					"   INNER JOIN ITM_Routing " +
//					" 	    ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID " +
//					"        AND ITM_Routing.ProductID = ITM_Product.ProductID " +
					"	LEFT OUTER JOIN PRO_Shift " +
					"		ON PRO_DCPResultDetail.ShiftID = PRO_Shift.ShiftID " +
					" WHERE " +
					"	PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID +
					"	AND MST_WorkCenter.ProductionlineID = " + pintProductionLineID +
					"	AND PRO_DCPResultDetail.Type = 0 ";// +
					//"	AND PRO_DCPResultDetail.StartTime <> PRO_DCPResultDetail.EndTime ";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCSDetail = new OleDbDataAdapter(ocmdPCS);
				odadPCSDetail.Fill(dstPCS,PRO_DCPResultDetailTable.TABLE_NAME);

				//select master data
				strSql = " SELECT ";
				if (pblnNoData)
				{
					strSql += "	TOP 0 ";
				}

				strSql +=
					" PRO_DCPResultMaster.DCPResultMasterID, " +
					" PRO_DCPResultMaster.StartDateTime, " +
					" PRO_DCPResultMaster.DueDateTime, " +
					" PRO_DCPResultMaster.Quantity, " +
					" PRO_DCPResultMaster.DCOptionMasterID, " +
					" PRO_DCPResultMaster.ProductID, " +
					" PRO_DCPResultMaster.WorkCenterID " +
					" FROM  " +
					" 	PRO_DCPResultMaster " +
					"	INNER JOIN MST_WorkCenter " +
					"		ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID " +
					" WHERE " +
					"	PRO_DCPResultMaster.DCOptionMasterID = " + pintDCOptionMasterID +
					"	AND MST_WorkCenter.ProductionlineID = " + pintProductionLineID;

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				OleDbDataAdapter odadPCSMaster = new OleDbDataAdapter(ocmdPCS);
				odadPCSMaster.Fill(dstPCS,PRO_DCPResultMasterTable.TABLE_NAME);

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

		public int GetMainWorkCenterID(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetMaxYearInCalendar()";
			DataSet dstPCS = new DataSet();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "Select WorkCenterID From mst_workcenter WHERE IsMain=1 AND ProductionLineID = " 
								+ pintProductionLineID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				int intMainWorkCenterID = 0;
				while (odrPCS.Read())
				{
					intMainWorkCenterID = Convert.ToInt32(odrPCS[0]);
				}		
				return intMainWorkCenterID;
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

//		public int SaveDCPResult(int pintDCOptionMasterID, int pintWorkCenterID, int pintProductionLineID,DataTable pdtbDCPResultDetail)
//		{
//			(new PRO_DCPResultDetailDS()).UpdateDataSetManual(pdtbDCPResultDetail.DataSet);
//			return 0;
//		}

		public DataTable GetDeliveryScheduleData(int pintDCOptionMasterID, string pstrProductIDs)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleData()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = 
					" SELECT 	SO_DeliverySchedule.DeliveryQuantity, " +
					" 	DateAdd(ss,-IsNull(ITM_Product.LTSalesATP,0),SO_DeliverySchedule.ScheduleDate) ScheduleDate, " +
					" 	ITM_Product.SafetyStock, " +
					" 	ITM_Product.Revision, " +
					" 	IsNull(ITM_Product.OrderQuantityMultiple,1) OrderQuantityMultiple, " +
					" 	IsNull(ITM_Product.MinProduce,0) MinProduce, " +
					" 	IsNull(ITM_Product.MaxProduce,0) MaxProduce, " +
					" 	IsNull(ITM_Product.ScrapPercent,0) ScrapPercent, " +
					" 	IsNull(ITM_Product.MaxRoundUpToMin,0) MaxRoundUpToMin, " +
					" 	IsNull(ITM_Product.MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, " +
					" 	SO_SaleOrderDetail.ProductID, " +
					" 	MST_WorkCenter.WorkCenterID, " +
					" 	MST_WorkCenter.Code WorkCenterCode, " +
					" 	PRO_CheckPoint.SamplePattern, " +
					" 	PRO_CheckPoint.SampleRate, " +
					" 	PRO_CheckPoint.DelayTime, " +
					"	ITM_Routing.RoutingID, " +
					" 	CASE " +
					" 		WHEN (SELECT IgnoreMoveTime FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") = 1 THEN " +
					" 			CASE " +
					" 				WHEN ITM_Routing.Pacer = 'M' THEN " +
					" 					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime " +
					" 				WHEN ITM_Routing.Pacer = 'L' THEN " +
					" 					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					" 				WHEN ITM_Routing.Pacer = 'B' THEN " +
					" 					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " +
					" 			END " +
					" 		ELSE  " +
					" 			CASE " +
					" 				WHEN ITM_Routing.Pacer = 'M' THEN " +
					" 					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.MoveTime " +
					" 				WHEN ITM_Routing.Pacer = 'L' THEN " +
					" 					ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					" 				WHEN ITM_Routing.Pacer = 'B' THEN " +
					" 					ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime + ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime + ITM_Routing.MoveTime " +
					" 			END " +
					" 	END LeadTime, " +
					"	ITM_Routing.FixLT, " +
					" 	0 CapacityBottleID " +
					" FROM 	SO_DeliverySchedule  " +
					" 	INNER JOIN SO_SaleOrderDetail ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " +
					" 	INNER JOIN ITM_Routing ON SO_SaleOrderDetail.ProductID = ITM_Routing.ProductID " +
					" 	INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID " +
					" 	INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1 " +
					" 	LEFT JOIN PRO_CheckPoint ON PRO_CheckPoint.ProductID = SO_SaleOrderDetail.ProductID AND PRO_CheckPoint.WorkCenterID = MST_WorkCenter.WorkCenterID " +
					" WHERE 	ScheduleDate > (SELECT DateAdd(dd,0,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ") " +
					" 	AND ScheduleDate <= (SELECT DateAdd(dd,PlanHorizon+1,AsOfDate) FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ")  " +
					"   AND SO_SaleOrderDetail.ProductID in " + pstrProductIDs +
					" ORDER BY SO_DeliverySchedule.ScheduleDate ASC ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetRequiredProducts(int pintDCOptionMasterID,int pintWorkCenterID) //,string pstrProductIDs)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleData()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
			string strSql = "SELECT PRO_DCPResultMaster.ProductID,PRO_DCPResultMaster.RoutingID,WorkingDate,ShiftID,PRO_DCPResultMaster.WorkCenterID,PRO_DCPResultDetail.StartTime,PRO_DCPResultDetail.StartTime EndTime,SUM(PRO_DCPResultDetail.Quantity) Quantity, " +
									" SafetyStock, " +
									" Revision, " +
									" OrderQuantityMultiple, " +
									" MinProduce, " +
									" MaxProduce, " +
									" ScrapPercent, " +
									" MaxRoundUpToMin, " +
									" MaxRoundUpToMultiple,LTVariableTime, 0 TotalSecond " 
							+ " FROM PRO_DCPResultMaster "
							+ " INNER JOIN PRO_DCPResultDetail ON PRO_DCPResultMaster.DCPResultMasterID=PRO_DCPResultDetail.DCPResultMasterID "
							+ " INNER JOIN (" +
									" Select ITM_Product.SafetyStock, " +
									" ITM_Product.Revision, " +
									" IsNull(ITM_Product.OrderQuantityMultiple,1) OrderQuantityMultiple, " +
									" IsNull(ITM_Product.MinProduce,0) MinProduce, " +
									" IsNull(ITM_Product.MaxProduce,0) MaxProduce, " +
									" IsNull(ITM_Product.ScrapPercent,0) ScrapPercent, " +
									" IsNull(ITM_Product.MaxRoundUpToMin,0) MaxRoundUpToMin, " +
									" IsNull(ITM_Product.MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, " +
									" IsNull(ITM_Product.LTVariableTime,0) LTVariableTime, "
							+ "     ITM_Product.ProductID,mst_workcenter.WorkCenterID from itm_routing "
							+ "     INNER JOIN ITM_Product ON itm_routing.ProductID = ITM_Product.ProductID"
							+ "     inner join mst_workcenter on itm_routing.WorkCenterID=mst_workcenter.WorkCenterID and IsMain=1 " //and itm_routing.WorkCenterID = " + pintWorkCenterID
							+ " ) WC ON PRO_DCPResultMaster.ProductID=WC.ProductID"
							+ " WHERE /*PRO_DCPResultMaster.ProductID IN ( "
							+ "     SELECT Distinct ProductID FROM ITM_BOM WHERE ComponentID IN (Select ComponentID from Itm_bom where ProductID IN " /*+ pstrProductIDs*/ + ")"
							+ " ) AND*/ DCOptionMasterID =  " + pintDCOptionMasterID
							+ " GROUP BY PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.RoutingID, WorkingDate, ShiftID, PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultDetail.StartTime, " +
									" SafetyStock, " +
									" Revision, " +
									" OrderQuantityMultiple, " +
									" MinProduce, " +
									" MaxProduce, " +
									" ScrapPercent, " +
									" MaxRoundUpToMin, " +
									" MaxRoundUpToMultiple, LTVariableTime " 
							+ " ORDER BY PRO_DCPResultMaster.ProductID,WorkingDate, ShiftID, PRO_DCPResultMaster.WorkCenterID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public DataTable GetProductLists(int pintDCOptionMasterID,string pstrWorkCenterIDs)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleData()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ITM_Product.ProductID,wc.RoutingID,WC.WorkCenterID," +
						" ISNULL(SafetyStock,0) SafetyStock, ISNULL(LTSafetyStock,0) LTSafetyStock, " +
						" ITM_Product.Code,ITM_Product.Description,Revision,workcentercode, ISNULL(ITM_Product.AVEG,0) AVEG, " +
						" ISNULL(OrderQuantityMultiple,0) OrderQuantityMultiple, " +
						" Case WC.SetMinProduce When 1 Then ISNULL(MinProduce,0) Else 0 End MinProduce, " +
						" ISNULL(MaxProduce,0) MaxProduce, " +
						" ISNULL(ScrapPercent,0) ScrapPercent, " +
						" ISNULL(MaxRoundUpToMin,0) MaxRoundUpToMin, " +
						" ISNULL(MaxRoundUpToMultiple,0) MaxRoundUpToMultiple, ISNULL(LTVariableTime,0) LTVariableTime, ISNULL(LTFixedTime,0) LTFixedTime, " +
						" 0 TotalDelivery, 0 AverageDelivery, 0 DeliveryTimes "
					+ " FROM ITM_Product "
					+ " inner join( "
					+ "   select productid,routingid,productionlineid,mst_workcenter.workcenterid,mst_workcenter.code workcentercode,ISNULL(mst_workcenter.SetMinProduce,1) SetMinProduce from mst_workcenter "
					+ "   inner join itm_routing on mst_workcenter.Workcenterid=itm_routing.workcenterid "
					+ "   and ismain=1 "
					+ " ) wc on ITM_Product.ProductID=wc.ProductID and ITM_Product.productionlineid=wc.productionlineid "
					+ " WHERE WorkCenterID IN " + pstrWorkCenterIDs;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		/// Delete all related components in DCP result
		/// </summary>
		/// <param name="pingDCOptionMasterID"></param>
		/// <param name="pstrProductIDs"></param>
		public void DeleteRelatedComponentInDCPResult(int pingDCOptionMasterID, string pstrProductIDs)
		{
			string strSQL = " /*-- Delete all components in DCP result*/"
				+ " Delete PRO_DCPResultDetail WHERE DCPResultMasterID IN( "
				+ "    Select DCPResultMasterID FROM PRO_DCPResultMaster WHERE ProductID IN ( "
				+ "        Select ComponentID From Itm_bom where ProductID IN (0,1,2,3) "
				+ "    )  AND DCOptionMasterID = 1 "
				+ " ) "
				+ " Delete PRO_DCPResultMaster WHERE ProductID IN ( "
				+ "     Select ComponentID from Itm_bom where ProductID IN  " + pstrProductIDs
				+ " )  AND DCOptionMasterID = 1" ;
		}

		
		public DataTable GetWorkCenterListRelated(int pintDCOptionMasterID) //,string pstrProductIDs)
		{
			const string METHOD_NAME = THIS + ".GetDeliveryScheduleData()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				//-- List all wc produce component
				string strSql = " SELECT WorkCenterID, BeginDate, EndDate, Capacity, -1 WorkCenterLevel, 0 WorkCenterAncessors "
					+ " FROM PRO_WCCapacity";// Where workcenterid = 7";
					//+ " WHERE WorkCenterID IN ( "
					//+ "     SELECT PRO_WCCapacity.WorkCenterID FROM ITM_Routing "
					//+ "     INNER Join MST_WorkCenter on ITM_Routing.WorkCenterID=MST_WorkCenter.WorkCenterID and IsMain=1 "
					//+ "     WHERE ProductID IN (Select ComponentID FROM Itm_bom where ProductID IN "+pstrProductIDs+" ) "
					//+ " )";
					//+ " ) AND DCOptionMasterID=" + pintDCOptionMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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
		
		#endregion Manual Production Planning

		public DataTable GetBeginData(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetBeginData()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT * FROM " + DCP_BeginQuantityTable.TABLE_NAME
					+ " WHERE " + DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD + "=" + pintDCOptionMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable(DCP_BeginQuantityTable.TABLE_NAME);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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
		public void UpdateBeginData(DataTable pdtbBeginData)
		{
			const string METHOD_NAME = THIS + ".UpdateBeginData()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT " +
					DCP_BeginQuantityTable.DCPBEGINQUANTITYID_FLD + ", " +
					DCP_BeginQuantityTable.DCOPTIONMASTERID_FLD + ", " +
					DCP_BeginQuantityTable.QUANTITY_FLD + ", " +
					DCP_BeginQuantityTable.PRODUCTID_FLD + " " +
					"FROM " +
					DCP_BeginQuantityTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				//pdstData.EnforceConstraints = false;
				odadPCS.Update(pdtbBeginData);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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


		public void DeleteDCPInChildWC(int pintDCOptionMasterID, string pstrChildWorkCenterIDs)
		{
			const string METHOD_NAME = THIS + ".DeleteRelatedInforOfDCOption()";
			string strSql1 = "DELETE FROM " + PRO_DCPResultDetailTable.TABLE_NAME;
			strSql1 += " WHERE " + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + " IN ";
			strSql1 += " ( ";
			strSql1 += " SELECT "  + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD;
			strSql1 += " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql1 += " WHERE " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + "= " + pintDCOptionMasterID 
				+ " AND " + PRO_DCPResultMasterTable.WORKCENTERID_FLD + " IN " + pstrChildWorkCenterIDs;
			strSql1 += ")";

			string strSql2 = "DELETE FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql2 += " WHERE " + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + " IN ";
			strSql2 += " ( ";
			strSql2 += " SELECT "  + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD;
			strSql2 += " FROM " + PRO_DCPResultMasterTable.TABLE_NAME;
			strSql2 += " WHERE " + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD + " = " + pintDCOptionMasterID
				+ " AND " + PRO_DCPResultMasterTable.WORKCENTERID_FLD + " IN " + pstrChildWorkCenterIDs;
			strSql2 += ")";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql1, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

				ocmdPCS.CommandText = strSql2;				
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

		public DataSet GetProductSequence(int pintProductionLineId)
		{
			const string METHOD_NAME = THIS + ".GetProductSequence()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ProductProductionOrderID, " + pintProductionLineId + " AS ProductionLineID,"
					+ " O.ProductID, Seq, P.Code, P.Description, P.Revision"
					+ " FROM PRO_ProductProductionOrder O JOIN ITM_Product P"
					+ " ON O.ProductID = P.ProductID"
					+ " WHERE O.ProductionLineID = " + pintProductionLineId
					+ " ORDER BY Seq";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataSet dstData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstData, "PRO_ProductProductionOrder");

				return dstData;
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

		public DataSet GetProductSequenceByDefault(int pintProductionLineId)
		{
			const string METHOD_NAME = THIS + ".GetProductSequenceByDefault()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " + pintProductionLineId + " AS ProductionLineID,"
					+ " P.ProductID, 0 AS Seq, P.Code, P.Description, P.Revision"
					+ " FROM ITM_Product P"
					+ " WHERE ISNULL(P.ProductionLineID,0) = " + pintProductionLineId
					+ " ORDER BY P.Revision, P.Code, P.Description";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataSet dtbData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData, "PRO_ProductProductionOrder");

				return dtbData;
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

		public void UpdateProductSequence(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateProductSequence()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT ProductProductionOrderID, ProductID, ProductionLineID, Seq" +
					" FROM PRO_ProductProductionOrder";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData, "PRO_ProductProductionOrder");

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
		public DataTable GetShifts(string pstrProductionLineID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT MST_WorkCenter.ProductionLineID, PRO_Shift.ShiftID,"
					+ " WorkTimeFrom, WorkTimeTo,"
					+ " PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate"
					+ " FROM PRO_Shift JOIN PRO_ShiftCapacity"
					+ " ON PRO_Shift.ShiftID = PRO_ShiftCapacity.ShiftID"
					+ " JOIN PRO_WCCapacity"
					+ " ON PRO_ShiftCapacity.WCCapacityID = PRO_WCCapacity.WCCapacityID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ShiftPattern ON PRO_Shift.ShiftID = PRO_ShiftPattern.ShiftID"
					+ " WHERE MST_WorkCenter.IsMain = 1";
				if (pstrProductionLineID != null && pstrProductionLineID != string.Empty)
					strSql += " AND MST_WorkCenter.ProductionLineID IN (" + pstrProductionLineID + ")";
				DataTable dtbShift = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(strSql, oconPCS);
				odadPCS.Fill(dtbShift);
				return dtbShift;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
	}
}
