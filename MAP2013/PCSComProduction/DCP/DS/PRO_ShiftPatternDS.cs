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
	public class PRO_ShiftPatternDS 
	{
		public PRO_ShiftPatternDS()
		{
		}
		private const string THIS = "PCSComProduction.DCP.DS.PRO_ShiftPatternDS";
		private DateTime dtmSpecialDay = new DateTime(1000/01/01);
		///    <summary>
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ShiftPatternVO objObject = (PRO_ShiftPatternVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ShiftPattern("
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.SHIFTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.SHIFTID_FLD].Value = objObject.ShiftID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].Value = objObject.EffectDateFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATETO_FLD].Value = objObject.EffectDateTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].Value = objObject.WorkTimeFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMETO_FLD].Value = objObject.WorkTimeTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].Value = objObject.RegularStopFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPTO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].Value = objObject.RegularStopTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].Value = objObject.RefreshingFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGTO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGTO_FLD].Value = objObject.RefreshingTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].Value = objObject.ExtraStopFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPTO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].Value = objObject.ExtraStopTo;


				
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
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, August 23 2005</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ShiftPatternVO objObject = (PRO_ShiftPatternVO) pobjObjectVO;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				string strSql=	"INSERT INTO PRO_ShiftPattern("
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)"
					+ " SELECT @@IDENTITY";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.SHIFTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.SHIFTID_FLD].Value = objObject.ShiftID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].Value = objObject.EffectDateFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATETO_FLD].Value = objObject.EffectDateTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].Value = objObject.WorkTimeFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMETO_FLD].Value = objObject.WorkTimeTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPFROM_FLD, OleDbType.Date));
				if (objObject.RegularStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].Value = objObject.RegularStopFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPTO_FLD, OleDbType.Date));
				if (objObject.RegularStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].Value = objObject.RegularStopTo;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGFROM_FLD, OleDbType.Date));
				if (objObject.RefreshingFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].Value = objObject.RefreshingFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGTO_FLD, OleDbType.Date));
				if (objObject.RefreshingTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGTO_FLD].Value = objObject.RefreshingTo;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGTO_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPFROM_FLD, OleDbType.Date));
				if (objObject.ExtraStopFrom.ToShortDateString()!= dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].Value = objObject.ExtraStopFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPTO_FLD, OleDbType.Date));
				if (objObject.ExtraStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].Value = objObject.ExtraStopTo;
                }
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].Value = DBNull.Value;

				
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
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_ShiftPatternTable.TABLE_NAME + " WHERE  " + "ShiftPatternID" + "=" + pintID.ToString();
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
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
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
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD
					+ " FROM " + PRO_ShiftPatternTable.TABLE_NAME
					+" WHERE " + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ShiftPatternVO objObject = new PRO_ShiftPatternVO();

				while (odrPCS.Read())
				{ 
					objObject.ShiftPatternID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTPATTERNID_FLD].ToString().Trim());
					objObject.ShiftID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_ShiftPatternTable.CCNID_FLD].ToString().Trim());
					objObject.Comment = odrPCS[PRO_ShiftPatternTable.COMMENT_FLD].ToString().Trim();
					try
					{
						objObject.EffectDateFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.EffectDateTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATETO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.WorkTimeFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.WorkTimeTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMETO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RegularStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RegularStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RefreshingFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RefreshingTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGTO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ExtraStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ExtraStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].ToString().Trim());
					}
					catch{}

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

		public object GetObjectVOByShiftID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOByShiftID()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD
					+ " FROM " + PRO_ShiftPatternTable.TABLE_NAME
					+" WHERE " + PRO_ShiftPatternTable.SHIFTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ShiftPatternVO objObject = new PRO_ShiftPatternVO();

				while (odrPCS.Read())
				{ 
					objObject.ShiftPatternID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTPATTERNID_FLD].ToString().Trim());
					objObject.ShiftID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_ShiftPatternTable.CCNID_FLD].ToString().Trim());
					objObject.Comment = odrPCS[PRO_ShiftPatternTable.COMMENT_FLD].ToString().Trim();
					try
					{
						objObject.EffectDateFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.EffectDateTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATETO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.WorkTimeFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.WorkTimeTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMETO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RegularStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RegularStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RefreshingFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.RefreshingTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGTO_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ExtraStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ExtraStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].ToString().Trim());
					}
					catch{}

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
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_ShiftPatternVO objObject = (PRO_ShiftPatternVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ShiftPattern SET "
					+ PRO_ShiftPatternTable.SHIFTID_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + "=   ?" + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD + "=  ?"
					+" WHERE " + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.SHIFTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.SHIFTID_FLD].Value = objObject.ShiftID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.COMMENT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].Value = objObject.EffectDateFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EFFECTDATETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.EFFECTDATETO_FLD].Value = objObject.EffectDateTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMEFROM_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].Value = objObject.WorkTimeFrom;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.WORKTIMETO_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.WORKTIMETO_FLD].Value = objObject.WorkTimeTo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPFROM_FLD, OleDbType.Date));
				if (objObject.RegularStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].Value = objObject.RegularStopFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REGULARSTOPTO_FLD, OleDbType.Date));
				if (objObject.RegularStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].Value = objObject.RegularStopTo;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGFROM_FLD, OleDbType.Date));
				if (objObject.RefreshingFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].Value = objObject.RefreshingFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.REFRESHINGTO_FLD, OleDbType.Date));
				if (objObject.RefreshingTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGTO_FLD].Value = objObject.RefreshingTo;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.REFRESHINGTO_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPFROM_FLD, OleDbType.Date));
				if (objObject.ExtraStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].Value = objObject.ExtraStopFrom;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.EXTRASTOPTO_FLD, OleDbType.Date));
				if (objObject.ExtraStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString())
				{
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].Value = objObject.ExtraStopTo;
				}
				else
					ocmdPCS.Parameters[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ShiftPatternTable.SHIFTPATTERNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ShiftPatternTable.SHIFTPATTERNID_FLD].Value = objObject.ShiftPatternID;


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
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
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
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD
					+ " FROM " + PRO_ShiftPatternTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ShiftPatternTable.TABLE_NAME);

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
		/// GetWorkingTime
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, September 18 2006</date>
		public DataSet GetWorkingTime()
		{
			const string METHOD_NAME = THIS + ".GetWorkingTime()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD
					+ " FROM " + PRO_ShiftPatternTable.TABLE_NAME
					+ " WHERE " + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + " IN (1,2,3)"
					+ " ORDER BY " + PRO_ShiftPatternTable.SHIFTPATTERNID_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ShiftPatternTable.TABLE_NAME);

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
		///       This method uses to add data to PRO_ShiftPattern
		///    </summary>
		///    <Inputs>
		///        PRO_ShiftPatternVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, August 02, 2005
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
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD 
					+ "  FROM " + PRO_ShiftPatternTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,PRO_ShiftPatternTable.TABLE_NAME);

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
		/// GetShiftPartternByShiftCode
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintShiftID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		public object GetShiftPartternByShiftCode(int pintShiftID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT "
					+ PRO_ShiftPatternTable.SHIFTPATTERNID_FLD + ","
					+ PRO_ShiftPatternTable.SHIFTID_FLD + ","
					+ PRO_ShiftPatternTable.CCNID_FLD + ","
					+ PRO_ShiftPatternTable.COMMENT_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATEFROM_FLD + ","
					+ PRO_ShiftPatternTable.EFFECTDATETO_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMEFROM_FLD + ","
					+ PRO_ShiftPatternTable.WORKTIMETO_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.REGULARSTOPTO_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGFROM_FLD + ","
					+ PRO_ShiftPatternTable.REFRESHINGTO_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPFROM_FLD + ","
					+ PRO_ShiftPatternTable.EXTRASTOPTO_FLD
					+ " FROM " + PRO_ShiftPatternTable.TABLE_NAME
					+ " WHERE " + PRO_ShiftPatternTable.CCNID_FLD + " = " + pintCCNID.ToString()
					+ " AND " + PRO_ShiftPatternTable.SHIFTID_FLD + " = " + pintShiftID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ShiftPatternVO objObject = new PRO_ShiftPatternVO();

				while (odrPCS.Read())
				{ 
					objObject.ShiftPatternID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTPATTERNID_FLD].ToString().Trim());
					objObject.ShiftID = int.Parse(odrPCS[PRO_ShiftPatternTable.SHIFTID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[PRO_ShiftPatternTable.CCNID_FLD].ToString().Trim());
					objObject.Comment = odrPCS[PRO_ShiftPatternTable.COMMENT_FLD].ToString().Trim();
					objObject.EffectDateFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATEFROM_FLD].ToString().Trim());
					objObject.EffectDateTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EFFECTDATETO_FLD].ToString().Trim());
					objObject.WorkTimeFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMEFROM_FLD].ToString().Trim());
					objObject.WorkTimeTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.WORKTIMETO_FLD].ToString().Trim());
					if (odrPCS[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].ToString().Trim() != string.Empty)
					{
						objObject.RegularStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPFROM_FLD].ToString().Trim());
					}
					else
						objObject.RegularStopFrom = dtmSpecialDay;
					if (odrPCS[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].ToString().Trim() != string.Empty)
					{
						objObject.RegularStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REGULARSTOPTO_FLD].ToString().Trim());
					}
					else
						objObject.RegularStopTo = dtmSpecialDay;
					if (odrPCS[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].ToString().Trim() != string.Empty)
					{
						objObject.RefreshingFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGFROM_FLD].ToString().Trim());
					}
					else
						objObject.RefreshingFrom = dtmSpecialDay;
					if (odrPCS[PRO_ShiftPatternTable.REFRESHINGTO_FLD].ToString().Trim() != string.Empty)
					{
						objObject.RefreshingTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.REFRESHINGTO_FLD].ToString().Trim());
					}
					else
						objObject.RefreshingTo = dtmSpecialDay;
					if (odrPCS[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].ToString().Trim() != string.Empty)
					{
						objObject.ExtraStopFrom = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPFROM_FLD].ToString().Trim());
					}
					else
						objObject.ExtraStopFrom = dtmSpecialDay;
					if (odrPCS[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].ToString().Trim() != string.Empty)
					{
						objObject.ExtraStopTo = DateTime.Parse(odrPCS[PRO_ShiftPatternTable.EXTRASTOPTO_FLD].ToString().Trim());
					}
					else
						objObject.ExtraStopTo = dtmSpecialDay;

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
	}
}
