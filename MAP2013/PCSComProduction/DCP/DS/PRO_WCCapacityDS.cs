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
	public class PRO_WCCapacityDS 
	{
		private const string THIS = "PCSComProduction.DCP.DS.PRO_WCCapacityDS";

		public PRO_WCCapacityDS()
		{
		}
		
		/// <summary>
		/// Get shift pattern 
		/// </summary>
		/// <returns></returns>
		public DataTable GetShiftWithShiftPattern()
		{
			const string METHOD_NAME = THIS + ".GetShiftWithShiftPattern()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + ", " 
					+ PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTDESC_FLD + ", Count(" + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ") as ShiftPattern"
					+ " FROM  " + PRO_ShiftTable.TABLE_NAME  
					+ " LEFT JOIN " + PRO_ShiftPatternTable.TABLE_NAME 
					+ " ON " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + "=" + PRO_ShiftPatternTable.TABLE_NAME +"." + PRO_ShiftPatternTable.SHIFTID_FLD
					+ " GROUP BY " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + ", " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTDESC_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ShiftTable.TABLE_NAME);
				
				if(dstPCS != null)
				{
					return dstPCS.Tables[PRO_ShiftTable.TABLE_NAME];
				}
				return null;				
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

		public DataSet GetShiftAndShiftPattern()
		{
			const string METHOD_NAME = THIS + ".GetShiftWithShiftPattern()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + ", " 
					+ PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTDESC_FLD + ", Count(" + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ") as ShiftPattern"
					+ " FROM  " + PRO_ShiftTable.TABLE_NAME  
					+ " LEFT JOIN " + PRO_ShiftPatternTable.TABLE_NAME 
					+ " ON " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + "=" + PRO_ShiftPatternTable.TABLE_NAME +"." + PRO_ShiftPatternTable.SHIFTID_FLD
					+ " GROUP BY " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD + ", " + PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTDESC_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ShiftTable.TABLE_NAME);
				
				if(dstPCS != null)
				{
					return dstPCS;
				}
				return null;				
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
		/// SELECT sum(DateDiff(Minute, WorkTimeFrom, WorkTimeTo)) WorkingMinutes 
		/// FROM PRO_ShiftPattern sp, PRO_Shift s
		/// WHERE s.ShiftID = sp.ShiftID 
		/// AND sp.ShiftID in (pobjShiftCapacity)
		/// </summary>
		/// <param name="pstrShiftIDs"></param>
		/// <returns></returns>
		public int GetTotalWorkingTime(string pstrShiftIDs)
		{
			const string METHOD_NAME = THIS + ".GetTotalWorkingTime()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "SELECT ISNULL(Sum(DateDiff(minute, " + PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ", " + PRO_ShiftPatternTable.WORKTIMETO_FLD + ")), 0) as WorkingMinutes";
				strSql += " FROM " + PRO_ShiftPatternTable.TABLE_NAME + ", " + PRO_ShiftTable.TABLE_NAME;
				strSql += " WHERE " + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTID_FLD + "=" +  PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD;
				strSql += " AND " + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTID_FLD + " IN (" + pstrShiftIDs + ")";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();

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
		/// Get actual working time of list of selected shift.
		/// </summary>
		/// <param name="pstrShiftIDs"></param>
		/// <returns></returns>
		public int GetTotalActualWorkingTime(string pstrShiftIDs)
		{
			const string METHOD_NAME = THIS + ".GetTotalWorkingTime()";
			 
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try
			{				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "SELECT ISNULL( Sum(";
				
				strSql += " ISNULL(DateDiff(second, " + PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ", " + PRO_ShiftPatternTable.WORKTIMETO_FLD + "), 0)";
				strSql += " - ISNULL(DateDiff(second, " + PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ", " + PRO_ShiftPatternTable.EXTRASTOPTO_FLD + "), 0)";
				strSql += " - ISNULL(DateDiff(second, " + PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ", " + PRO_ShiftPatternTable.REFRESHINGTO_FLD + "), 0)";
				strSql += " - ISNULL(DateDiff(second, " + PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ", " + PRO_ShiftPatternTable.REGULARSTOPTO_FLD + "), 0)";

				strSql += "), 0)";
				strSql += " FROM " + PRO_ShiftPatternTable.TABLE_NAME + ", " + PRO_ShiftTable.TABLE_NAME;
				strSql += " WHERE " + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTID_FLD + "=" +  PRO_ShiftTable.TABLE_NAME + "." + PRO_ShiftTable.SHIFTID_FLD;
				strSql += " AND " + PRO_ShiftPatternTable.TABLE_NAME + "." + PRO_ShiftPatternTable.SHIFTID_FLD + " IN (" + pstrShiftIDs + ")";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();

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
		///    <summary>
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_WCCapacityVO objObject = (PRO_WCCapacityVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_WCCapacity("
				+ PRO_WCCapacityTable.MACHINENO_FLD + ", "
				+ PRO_WCCapacityTable.BEGINDATE_FLD + ", "
				+ PRO_WCCapacityTable.ENDDATE_FLD + ", "
				+ PRO_WCCapacityTable.FACTOR_FLD + ", "
				+ PRO_WCCapacityTable.CAPACITY_FLD + ", "
				+ PRO_WCCapacityTable.CREWSIZE_FLD + ", "
				+ PRO_WCCapacityTable.CCNID_FLD + ", "
				+ PRO_WCCapacityTable.WCTYPE_FLD + ", "
				+ PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.MACHINENO_FLD, OleDbType.Integer));
				if(objObject.MachineNo != Int32.MinValue)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = objObject.MachineNo;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.BEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.BEGINDATE_FLD].Value = objObject.BeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.FACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.FACTOR_FLD].Value = objObject.Factor;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CREWSIZE_FLD, OleDbType.Decimal));
				if(objObject.CrewSize != Decimal.MinusOne)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = objObject.CrewSize;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WCTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WCTYPE_FLD].Value = objObject.WCType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;


				
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
	
		
		public int AddAndReturnId(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnId()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_WCCapacityVO objObject = (PRO_WCCapacityVO) pobjObjectVO;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				string strSql = "INSERT INTO PRO_WCCapacity("
					+ PRO_WCCapacityTable.MACHINENO_FLD + ","
					+ PRO_WCCapacityTable.BEGINDATE_FLD + ","
					+ PRO_WCCapacityTable.ENDDATE_FLD + ","
					+ PRO_WCCapacityTable.FACTOR_FLD + ","
					+ PRO_WCCapacityTable.CAPACITY_FLD + ","
					+ PRO_WCCapacityTable.CREWSIZE_FLD + ","
					+ PRO_WCCapacityTable.CCNID_FLD + ","
					+ PRO_WCCapacityTable.WCTYPE_FLD + ","
					+ PRO_WCCapacityTable.WORKCENTERID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?)"
					+ "; SELECT @@IDENTITY as LatestID";
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.MACHINENO_FLD, OleDbType.Integer));
				if(objObject.MachineNo != Int32.MinValue)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = objObject.MachineNo;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.BEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.BEGINDATE_FLD].Value = objObject.BeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.FACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.FACTOR_FLD].Value = objObject.Factor;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CREWSIZE_FLD, OleDbType.Decimal));
				if(objObject.CrewSize != Decimal.MinusOne)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = objObject.CrewSize;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WCTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WCTYPE_FLD].Value = objObject.WCType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;
				
				ocmdPCS.Connection.Open();

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

		///    <summary>
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			
			string strSql = "DELETE " + PRO_ShiftCapacityTable.TABLE_NAME + " WHERE  " + PRO_ShiftCapacityTable.WCCAPACITYID_FLD + "=" + pintID.ToString() + ";";
			strSql += " DELETE " + PRO_WCCapacityTable.TABLE_NAME + " WHERE  " + PRO_WCCapacityTable.WCCAPACITYID_FLD + "=" + pintID;

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
	
		
		public void DeleteByWorkCenter(int pintCCNID, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
						
			string strSql = "DELETE " + PRO_WCCapacityTable.TABLE_NAME + " WHERE  " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterID;
			strSql += " AND " + PRO_WCCapacityTable.CCNID_FLD + "=" + pintCCNID;
			
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
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
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
				+ PRO_WCCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_WCCapacityTable.MACHINENO_FLD + ","
				+ PRO_WCCapacityTable.BEGINDATE_FLD + ","
				+ PRO_WCCapacityTable.ENDDATE_FLD + ","
				+ PRO_WCCapacityTable.FACTOR_FLD + ","
				+ PRO_WCCapacityTable.CAPACITY_FLD + ","
				+ PRO_WCCapacityTable.CREWSIZE_FLD + ","
				+ PRO_WCCapacityTable.CCNID_FLD + ","
				+ PRO_WCCapacityTable.WCTYPE_FLD + ","
				+ PRO_WCCapacityTable.WORKCENTERID_FLD
				+ " FROM " + PRO_WCCapacityTable.TABLE_NAME
				+ " WHERE " + PRO_WCCapacityTable.WCCAPACITYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_WCCapacityVO objObject = new PRO_WCCapacityVO();

				while (odrPCS.Read())
				{ 
					objObject.WCCapacityID = int.Parse(odrPCS[PRO_WCCapacityTable.WCCAPACITYID_FLD].ToString().Trim());					
					objObject.BeginDate = DateTime.Parse(odrPCS[PRO_WCCapacityTable.BEGINDATE_FLD].ToString().Trim());
					objObject.EndDate = DateTime.Parse(odrPCS[PRO_WCCapacityTable.ENDDATE_FLD].ToString().Trim());
					objObject.Factor = Decimal.Parse(odrPCS[PRO_WCCapacityTable.FACTOR_FLD].ToString().Trim());
					objObject.Capacity = Decimal.Parse(odrPCS[PRO_WCCapacityTable.CAPACITY_FLD].ToString().Trim());
					if(!odrPCS[PRO_WCCapacityTable.MACHINENO_FLD].Equals(DBNull.Value))
					{
						objObject.MachineNo = int.Parse(odrPCS[PRO_WCCapacityTable.MACHINENO_FLD].ToString().Trim());
					}
					else
					{
						objObject.MachineNo = Int32.MinValue;
					}

					if(!odrPCS[PRO_WCCapacityTable.CREWSIZE_FLD].Equals(DBNull.Value))
					{
						objObject.CrewSize = Decimal.Parse(odrPCS[PRO_WCCapacityTable.CREWSIZE_FLD].ToString().Trim());
					}
					else
					{
						objObject.CrewSize = Decimal.MinusOne;
					}
					objObject.CCNID = int.Parse(odrPCS[PRO_WCCapacityTable.CCNID_FLD].ToString().Trim());
					objObject.WCType = int.Parse(odrPCS[PRO_WCCapacityTable.WCTYPE_FLD].ToString().Trim());
					objObject.WorkCenterID = int.Parse(odrPCS[PRO_WCCapacityTable.WORKCENTERID_FLD].ToString().Trim());
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
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_WCCapacityVO objObject = (PRO_WCCapacityVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_WCCapacity SET "
				+ PRO_WCCapacityTable.MACHINENO_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.BEGINDATE_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.ENDDATE_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.FACTOR_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.CAPACITY_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.CREWSIZE_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.CCNID_FLD + "=   ?" + ","				
				+ PRO_WCCapacityTable.WCTYPE_FLD + "=   ?" + ","
				+ PRO_WCCapacityTable.WORKCENTERID_FLD + "=  ?"
				+" WHERE " + PRO_WCCapacityTable.WCCAPACITYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.MACHINENO_FLD, OleDbType.Integer));
				if(objObject.MachineNo != Int32.MinValue)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = objObject.MachineNo;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.MACHINENO_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.BEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.BEGINDATE_FLD].Value = objObject.BeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_WCCapacityTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.FACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.FACTOR_FLD].Value = objObject.Factor;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CREWSIZE_FLD, OleDbType.Decimal));
				if(objObject.CrewSize != Decimal.MinusOne)
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = objObject.CrewSize;
				}
				else
				{
					ocmdPCS.Parameters[PRO_WCCapacityTable.CREWSIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WCTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WCTYPE_FLD].Value = objObject.WCType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WCCapacityTable.WCCAPACITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_WCCapacityTable.WCCAPACITYID_FLD].Value = objObject.WCCapacityID;

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
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
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
				+ PRO_WCCapacityTable.WCCAPACITYID_FLD + ", "
				+ PRO_WCCapacityTable.MACHINENO_FLD + ", "
				+ PRO_WCCapacityTable.BEGINDATE_FLD + ", "
				+ PRO_WCCapacityTable.ENDDATE_FLD + ", "
				+ PRO_WCCapacityTable.FACTOR_FLD + ", "
				+ PRO_WCCapacityTable.CAPACITY_FLD + ", "
				+ PRO_WCCapacityTable.CREWSIZE_FLD + ", "
				+ PRO_WCCapacityTable.CCNID_FLD + ", "
				+ PRO_WCCapacityTable.WCTYPE_FLD + ", "
				+ PRO_WCCapacityTable.WORKCENTERID_FLD
					+ " FROM " + PRO_WCCapacityTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_WCCapacityTable.TABLE_NAME);

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
		

		public DataSet ListByWorkCenter(int pintCCNID, int pintWorkCenterId)
		{
			const string METHOD_NAME = THIS + ".ListByWorkCenter()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_WCCapacityTable.BEGINDATE_FLD + ", "
					+ PRO_WCCapacityTable.ENDDATE_FLD + ", "
					+ PRO_WCCapacityTable.WCCAPACITYID_FLD + ", "					
					+ PRO_WCCapacityTable.FACTOR_FLD + ", "		
					+ PRO_WCCapacityTable.WCTYPE_FLD + ", "
					+ PRO_WCCapacityTable.CREWSIZE_FLD + ", "
					+ PRO_WCCapacityTable.MACHINENO_FLD + ", "
					+ PRO_WCCapacityTable.CAPACITY_FLD + ", "
					+ PRO_WCCapacityTable.CCNID_FLD + ", "							
					+ PRO_WCCapacityTable.WORKCENTERID_FLD
					+ " FROM " + PRO_WCCapacityTable.TABLE_NAME
					+ " WHERE " + PRO_WCCapacityTable.WORKCENTERID_FLD + "=" + pintWorkCenterId
					+ " AND " + PRO_WCCapacityTable.CCNID_FLD + "=" + pintCCNID
					+ " ORDER BY " + PRO_WCCapacityTable.BEGINDATE_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_WCCapacityTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_WCCapacity
		///    </summary>
		///    <Inputs>
		///        PRO_WCCapacityVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, August 12, 2005
		///    </History>		
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ PRO_WCCapacityTable.WCCAPACITYID_FLD + ","
				+ PRO_WCCapacityTable.MACHINENO_FLD + ","
				+ PRO_WCCapacityTable.BEGINDATE_FLD + ","
				+ PRO_WCCapacityTable.ENDDATE_FLD + ","
				+ PRO_WCCapacityTable.FACTOR_FLD + ","
				+ PRO_WCCapacityTable.CAPACITY_FLD + ","
				+ PRO_WCCapacityTable.CREWSIZE_FLD + ","
				+ PRO_WCCapacityTable.CCNID_FLD + ","
				+ PRO_WCCapacityTable.WCTYPE_FLD + ", "
				+ PRO_WCCapacityTable.WORKCENTERID_FLD 
				+ "  FROM " + PRO_WCCapacityTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_WCCapacityTable.TABLE_NAME);

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
		/// Get Workcenter Capacity By ID
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public DataTable GetWCCapacity(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetWCCapacity()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = " SELECT WCC.CCNID, WCC.WorkCenterID,WC.Code, ISNULL(WCC.Capacity,0) Capacity, WCC.WCCapacityID, TotalTime.TotalWorkTime, WCC.BeginDate, WCC.EndDate"
								+ " FROM PRO_WCCapacity WCC"
								+ " INNER JOIN MST_WorkCenter WC ON WCC.WorkCenterID=WC.WorkCenterID AND WCC.CCNID=WC.CCNID"
								+ " INNER JOIN ("
									+ " SELECT SC.WCCapacityID, SUM(TT.TotalWorkTime) TotalWorkTime "
									+ " FROM PRO_ShiftCapacity SC "
									+ " LEFT JOIN ( "
										+ " SELECT ShiftID, CCNID, SUM( DATEDIFF(SECOND,WorkTimeFrom,WorkTimeTo)-ISNULL(DATEDIFF(SECOND,RegularStopFrom,RegularStopTo),0)-ISNULL(DATEDIFF(SECOND,RefreshingFrom,RefreshingTo),0)-ISNULL(DATEDIFF(SECOND,ExtraStopFrom,ExtraStopTo),0) ) TotalWorkTime "
										+ " FROM PRO_ShiftPattern "
										+ " GROUP BY ShiftID, CCNID "
									+ " ) TT ON SC.ShiftID=TT.ShiftID"
									+ " GROUP BY SC.WCCapacityID "
								+ " ) TotalTime ON WCC.WCCapacityID=TotalTime.WCCapacityID  AND WCC.CCNID = (SELECT CCNID FROM PRO_DCOptionMaster WHERE DCOptionMasterID = " + pintDCOptionMasterID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_WorkCenterTable.TABLE_NAME);
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
		public DataTable GetWCCapacityAndShift(int pintDCOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDCOptionMaster()";
			DataSet dstPCS = new DataSet();
						
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = " SELECT SC.ShiftCapacityID, SC.ShiftID, SP.EffectDateFrom, "
					+ "		WCC.WCCapacityID, WCC.BeginDate, WCC.EndDate,  "
					+ "		WCC.CCNID, WCC.WorkCenterID, WC.Code WorkCenterCode, SP.ShiftPatternID, "
					+ "		SP.WorkTimeFrom, SP.WorkTimeTo, SP.RegularStopFrom, SP.RegularStopTo,  "
					+ "		SP.RefreshingFrom, SP.RefreshingTo, SP.ExtraStopFrom, SP.ExtraStopTo, "
					+ "		DATEDIFF(SECOND,SP.WorkTimeFrom,SP.WorkTimeTo) - ISNULL(DATEDIFF(SECOND,SP.RegularStopFrom, SP.RegularStopTo),0)"
					+ "		- ISNULL(DATEDIFF(SECOND,SP.RefreshingFrom, SP.RefreshingTo),0) - ISNULL(DATEDIFF(SECOND,SP.ExtraStopFrom, SP.ExtraStopTo),0) NetWorkTime"

//					+ "		CASE WHEN WCC.BeginDate <= SP.EffectDateFrom THEN WCC.BeginDate "
//					+ "		     WHEN WCC.BeginDate > SP.EffectDateFrom THEN SP.EffectDateFrom "
//					+ "		END FromDate, "
//					+ "		CASE WHEN WCC.EndDate <= SP.EffectDateTo THEN WCC.EndDate "
//					+ "		     WHEN WCC.EndDate > SP.EffectDateTo THEN SP.EffectDateTo "
//					+ "		END  ToDate "
					+ "	FROM PRO_ShiftCapacity SC "
					+ "	INNER JOIN PRO_WCCapacity WCC ON WCC.WCCapacityID=SC.WCCapacityID "
					+ "	LEFT JOIN MST_WorkCenter WC ON WC.WorkCenterID=WCC.WorkCenterID "
					+ "	INNER JOIN PRO_ShiftPattern SP ON SC.ShiftID=SP.ShiftID AND WCC.CCNID=( "
					+ "		SELECT CCNID FROM PRO_DCOptionMaster WHERE DCOptionMasterID=" + pintDCOptionMasterID + ") ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_DCOptionMasterTable.TABLE_NAME);
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
