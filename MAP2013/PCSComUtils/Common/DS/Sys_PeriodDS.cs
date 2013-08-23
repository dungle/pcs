using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;

namespace PCSComUtils.Common.DS
{
	public class Sys_PeriodDS 
	{	
		public Sys_PeriodDS()
		{
		}
		private const string THIS = "PCSComUtils.Common.DS.Sys_PeriodDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_Period
		///    </Description>
		///    <Inputs>
		///        Sys_PeriodVO       
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
		///       Thursday, May 05, 2005
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
				Sys_PeriodVO objObject = (Sys_PeriodVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_Period("
				+ Sys_PeriodTable.FROMDATE_FLD + ","
				+ Sys_PeriodTable.TODATE_FLD + ","
				+ Sys_PeriodTable.ACTIVATE_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.ACTIVATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PeriodTable.ACTIVATE_FLD].Value = objObject.Activate;


				
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
	
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_PeriodVO objObject = (Sys_PeriodVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_Period("
					+ Sys_PeriodTable.FROMDATE_FLD + ","
					+ Sys_PeriodTable.TODATE_FLD + ","
					+ Sys_PeriodTable.ACTIVATE_FLD + ")"
					+ "VALUES(?,?,?)"
					+ "; SELECT @@IDENTITY as LatestID";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.ACTIVATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PeriodTable.ACTIVATE_FLD].Value = objObject.Activate;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return Convert.ToInt32(ocmdPCS.ExecuteScalar());

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from Sys_Period
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
			strSql=	"DELETE " + Sys_PeriodTable.TABLE_NAME + " WHERE  " + "PeriodID" + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_Period
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_PeriodVO
		///    </Outputs>
		///    <Returns>
		///       Sys_PeriodVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, May 05, 2005
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
				+ Sys_PeriodTable.PERIODID_FLD + ","
				+ Sys_PeriodTable.FROMDATE_FLD + ","
				+ Sys_PeriodTable.TODATE_FLD + ","
				+ Sys_PeriodTable.ACTIVATE_FLD
				+ " FROM " + Sys_PeriodTable.TABLE_NAME
				+" WHERE " + Sys_PeriodTable.PERIODID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_PeriodVO objObject = new Sys_PeriodVO();

				while (odrPCS.Read())
				{ 
				objObject.PeriodID = int.Parse(odrPCS[Sys_PeriodTable.PERIODID_FLD].ToString().Trim());
				objObject.FromDate = DateTime.Parse(odrPCS[Sys_PeriodTable.FROMDATE_FLD].ToString().Trim());
				objObject.ToDate = DateTime.Parse(odrPCS[Sys_PeriodTable.TODATE_FLD].ToString().Trim());
				objObject.Activate = bool.Parse(odrPCS[Sys_PeriodTable.ACTIVATE_FLD].ToString().Trim());

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
		///       This method uses to get data from Sys_Period
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_PeriodVO
		///    </Outputs>
		///    <Returns>
		///       Sys_PeriodVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, May 05, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO()
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
					+ Sys_PeriodTable.PERIODID_FLD + ","
					+ Sys_PeriodTable.FROMDATE_FLD + ","
					+ Sys_PeriodTable.TODATE_FLD + ","
					+ Sys_PeriodTable.ACTIVATE_FLD
					+ " FROM " + Sys_PeriodTable.TABLE_NAME
					+" WHERE " + Sys_PeriodTable.ACTIVATE_FLD + "= 1";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_PeriodVO objObject = new Sys_PeriodVO();

				while (odrPCS.Read())
				{ 
					objObject.PeriodID = int.Parse(odrPCS[Sys_PeriodTable.PERIODID_FLD].ToString().Trim());
					objObject.FromDate = DateTime.Parse(odrPCS[Sys_PeriodTable.FROMDATE_FLD].ToString().Trim());
					objObject.ToDate = DateTime.Parse(odrPCS[Sys_PeriodTable.TODATE_FLD].ToString().Trim());
					objObject.Activate = bool.Parse(odrPCS[Sys_PeriodTable.ACTIVATE_FLD].ToString().Trim());

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
		///       This method uses to change status of activate field in Sys_Period
		///    </Description>
		///    <Inputs>
		///       Sys_PeriodVO       
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
		
	
		public void ChangeStatusOfActivate(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".ChangeStatusOfActivate()";

			Sys_PeriodVO objObject = (Sys_PeriodVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_Period SET "
				+ Sys_PeriodTable.FROMDATE_FLD + "=   ?" + ","
				+ Sys_PeriodTable.TODATE_FLD + "=   ?" + ","
				+ Sys_PeriodTable.ACTIVATE_FLD + "=  ?"
				+" WHERE " + Sys_PeriodTable.PERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.ACTIVATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PeriodTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.PERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PeriodTable.PERIODID_FLD].Value = objObject.PeriodID;


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
		///       This method uses to update data to Sys_Period
		///    </Description>
		///    <Inputs>
		///       Sys_PeriodVO       
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

			Sys_PeriodVO objObject = (Sys_PeriodVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_Period SET "
					+ Sys_PeriodTable.FROMDATE_FLD + "=   ?" + ","
					+ Sys_PeriodTable.TODATE_FLD + "=   ?" + ","
					+ Sys_PeriodTable.ACTIVATE_FLD + "=  ?"
					+" WHERE " + Sys_PeriodTable.PERIODID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.FROMDATE_FLD].Value = objObject.FromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.TODATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_PeriodTable.TODATE_FLD].Value = objObject.ToDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.ACTIVATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PeriodTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.PERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PeriodTable.PERIODID_FLD].Value = objObject.PeriodID;


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
		///       This method uses to get all data from Sys_Period
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
		///       Thursday, May 05, 2005
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
				+ Sys_PeriodTable.PERIODID_FLD + ","
				+ Sys_PeriodTable.FROMDATE_FLD + ","
				+ Sys_PeriodTable.TODATE_FLD + ","
				+ Sys_PeriodTable.ACTIVATE_FLD
					+ " FROM " + Sys_PeriodTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_PeriodTable.TABLE_NAME);

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
		///       Thursday, May 05, 2005
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
				+ Sys_PeriodTable.PERIODID_FLD + ","
				+ Sys_PeriodTable.FROMDATE_FLD + ","
				+ Sys_PeriodTable.TODATE_FLD + ","
				+ Sys_PeriodTable.ACTIVATE_FLD 
		+ "  FROM " + Sys_PeriodTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_PeriodTable.TABLE_NAME);

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
		public void ActiveNextPeriod(object pobjPeriod)
		{
			const string METHOD_NAME = THIS + ".ActiveNextPeriod()";
			Sys_PeriodVO voPeriod = (Sys_PeriodVO)pobjPeriod;
			DateTime dtmFromDate = voPeriod.FromDate.AddMonths(1);
			dtmFromDate = new DateTime(dtmFromDate.Year, dtmFromDate.Month, dtmFromDate.Day);
			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "UPDATE Sys_Period SET Activate = 1"
					+ " WHERE FromDate = ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date)).Value = dtmFromDate;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		public void CloseAllPeriod(object pobjPeriod)
		{
			const string METHOD_NAME = THIS + ".ActiveNextPeriod()";
			Sys_PeriodVO voPeriod = (Sys_PeriodVO)pobjPeriod;
			DateTime dtmFromDate = voPeriod.FromDate.Date;
			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "UPDATE Sys_Period SET Activate = 0"
					+ " WHERE FromDate <> ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PeriodTable.FROMDATE_FLD, OleDbType.Date)).Value = dtmFromDate;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		public bool IsPeriodOverlap(object pobjPeriod)
		{
			const string METHOD_NAME = THIS + ".IsPeriodOverlap()";
						
			Sys_PeriodVO voPeriod = (Sys_PeriodVO)pobjPeriod;
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql =  "SELECT Count(*) "					
					+ " FROM " + Sys_PeriodTable.TABLE_NAME
					+ " WHERE (" + Sys_PeriodTable.PERIODID_FLD + " <> " + voPeriod.PeriodID + ") " 
					+ " AND ((" + Sys_PeriodTable.FROMDATE_FLD + " BETWEEN '" + voPeriod.FromDate.ToString("yyyy-MM-dd") + "' AND '" + voPeriod.ToDate.ToString("yyyy-MM-dd") + "')"
					+ " OR (" + Sys_PeriodTable.TODATE_FLD + " BETWEEN '" + voPeriod.FromDate.ToString("yyyy-MM-dd") + "' AND '" + voPeriod.ToDate.ToString("yyyy-MM-dd") + "')"
					+ " OR ('" + voPeriod.FromDate.ToString("yyyy-MM-dd") + "' BETWEEN "  + Sys_PeriodTable.FROMDATE_FLD + " AND " + Sys_PeriodTable.TODATE_FLD + ")"
					+ " OR ('" + voPeriod.ToDate.ToString("yyyy-MM-dd") + "' BETWEEN "  + Sys_PeriodTable.FROMDATE_FLD + " AND " + Sys_PeriodTable.TODATE_FLD + "))";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn == null) 
				{
					return false;
				}
				else
				{
					if (int.Parse(objReturn.ToString()) > 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
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
	}
}
