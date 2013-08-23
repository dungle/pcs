using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_WorkCenterCapacityDS 
	{
		public MST_WorkCenterCapacityDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_WorkCenterCapacityDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_WorkCenterCapacity
		///    </Description>
		///    <Inputs>
		///        MST_WorkCenterCapacityVO       
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
				MST_WorkCenterCapacityVO objObject = (MST_WorkCenterCapacityVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_WorkCenterCapacity("
				+ MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.CREWSIZE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORSHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEQTY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINESHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD].Value = objObject.EffectiveBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD].Value = objObject.LaborHoursPerday;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD].Value = objObject.EffectiveEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.CREWSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.CREWSIZE_FLD].Value = objObject.CrewSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABORSHIFT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABORSHIFT_FLD].Value = objObject.LaborShift;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD].Value = objObject.LaborEfficiencyFactor;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD].Value = objObject.MachineHoursPerday;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEQTY_FLD].Value = objObject.MachineQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINESHIFT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINESHIFT_FLD].Value = objObject.MachineShift;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD].Value = objObject.MachineEfficiencyFactor;


				
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
		///       This method uses to delete data from MST_WorkCenterCapacity
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
			strSql=	"DELETE " + MST_WorkCenterCapacityTable.TABLE_NAME + " WHERE  " + "WorkCenterCapacityID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_WorkCenterCapacity
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_WorkCenterCapacityVO
		///    </Outputs>
		///    <Returns>
		///       MST_WorkCenterCapacityVO
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
				+ MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.CREWSIZE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORSHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEQTY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINESHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD
				+ " FROM " + MST_WorkCenterCapacityTable.TABLE_NAME
				+" WHERE " + MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_WorkCenterCapacityVO objObject = new MST_WorkCenterCapacityVO();

				while (odrPCS.Read())
				{ 
				objObject.WorkCenterCapacityID = int.Parse(odrPCS[MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD].ToString().Trim());
				objObject.EffectiveBeginDate = DateTime.Parse(odrPCS[MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
				objObject.LaborHoursPerday = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD].ToString().Trim());
				objObject.EffectiveEndDate = DateTime.Parse(odrPCS[MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
				objObject.CrewSize = int.Parse(odrPCS[MST_WorkCenterCapacityTable.CREWSIZE_FLD].ToString().Trim());
				objObject.LaborShift = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.LABORSHIFT_FLD].ToString().Trim());
				objObject.LaborEfficiencyFactor = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD].ToString().Trim());
				objObject.MachineHoursPerday = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD].ToString().Trim());
				objObject.MachineQty = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.MACHINEQTY_FLD].ToString().Trim());
				objObject.MachineShift = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.MACHINESHIFT_FLD].ToString().Trim());
				objObject.MachineEfficiencyFactor = Decimal.Parse(odrPCS[MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_WorkCenterCapacity
		///    </Description>
		///    <Inputs>
		///       MST_WorkCenterCapacityVO       
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

			MST_WorkCenterCapacityVO objObject = (MST_WorkCenterCapacityVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_WorkCenterCapacity SET "
				+ MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.CREWSIZE_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.LABORSHIFT_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.MACHINEQTY_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.MACHINESHIFT_FLD + "=   ?" + ","
				+ MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD + "=  ?"
				+" WHERE " + MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD].Value = objObject.EffectiveBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD].Value = objObject.LaborHoursPerday;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD].Value = objObject.EffectiveEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.CREWSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.CREWSIZE_FLD].Value = objObject.CrewSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABORSHIFT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABORSHIFT_FLD].Value = objObject.LaborShift;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD].Value = objObject.LaborEfficiencyFactor;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD].Value = objObject.MachineHoursPerday;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEQTY_FLD].Value = objObject.MachineQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINESHIFT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINESHIFT_FLD].Value = objObject.MachineShift;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD].Value = objObject.MachineEfficiencyFactor;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD].Value = objObject.WorkCenterCapacityID;


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
		///       This method uses to get all data from MST_WorkCenterCapacity
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
				+ MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.CREWSIZE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORSHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEQTY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINESHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD
					+ " FROM " + MST_WorkCenterCapacityTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkCenterCapacityTable.TABLE_NAME);

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
				+ MST_WorkCenterCapacityTable.WORKCENTERCAPACITYID_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEBEGINDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.EFFECTIVEENDDATE_FLD + ","
				+ MST_WorkCenterCapacityTable.CREWSIZE_FLD + ","
				+ MST_WorkCenterCapacityTable.LABORSHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.LABOREFFICIENCYFACTOR_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEHOURSPERDAY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEQTY_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINESHIFT_FLD + ","
				+ MST_WorkCenterCapacityTable.MACHINEEFFICIENCYFACTOR_FLD 
		+ "  FROM " + MST_WorkCenterCapacityTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_WorkCenterCapacityTable.TABLE_NAME);

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
