using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduct.Items.DS
{
	
	public class ITM_RoutingDS 
	{
		public ITM_RoutingDS()
		{
		}

		private const string THIS = "PCSComProduct.Items.DS.DS.ITM_RoutingDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_Routing
		///    </Description>
		///    <Inputs>
		///        ITM_RoutingVO       
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
		///       Wednesday, January 19, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				ITM_RoutingVO objObject = (ITM_RoutingVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO ITM_Routing("
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.STEP_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.STEP_FLD].Value = objObject.Step;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINESETUPTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINESETUPTIME_FLD].Value = objObject.MachineSetupTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINERUNTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINERUNTIME_FLD].Value = objObject.MachineRunTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINES_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINES_FLD].Value = objObject.Machines;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORRUNTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORRUNTIME_FLD].Value = objObject.LaborRunTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORSETUPTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORSETUPTIME_FLD].Value = objObject.LaborSetupTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.CREWSIZE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.CREWSIZE_FLD].Value = objObject.CrewSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.SETUPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.SETUPQUANTITY_FLD].Value = objObject.SetupQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.STUDYTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.STUDYTIME_FLD].Value = objObject.StudyTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MOVETIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MOVETIME_FLD].Value = objObject.MoveTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORCOSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORCOSTCENTERID_FLD].Value = objObject.LaborCostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINECOSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].Value = objObject.MachineCostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.FUNCTIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.FUNCTIONID_FLD].Value = objObject.FunctionID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.EFFECTENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_RoutingTable.EFFECTENDDATE_FLD].Value = objObject.EffectEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSVARLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSVARLT_FLD].Value = objObject.OSVarLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSFIXLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSFIXLT_FLD].Value = objObject.OSFixLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.EFFECTBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_RoutingTable.EFFECTBEGINDATE_FLD].Value = objObject.EffectBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSOVERLAPPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].Value = objObject.OSOverlapPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSOVERLAPQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSOVERLAPQTY_FLD].Value = objObject.OSOverlapQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSSCHEDULESEQ_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSSCHEDULESEQ_FLD].Value = objObject.OSScheduleSeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSCOST_FLD].Value = objObject.OSCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OVERLAPPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OVERLAPPERCENT_FLD].Value = objObject.OverlapPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OVERLAPQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OVERLAPQTY_FLD].Value = objObject.OverlapQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.SCHEDULESEQ_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.SCHEDULESEQ_FLD].Value = objObject.ScheduleSeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.VARLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.VARLT_FLD].Value = objObject.VarLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.FIXLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.FIXLT_FLD].Value = objObject.FixLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.ROUTINGSTATUSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Value = objObject.RoutingStatusID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.RUNQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.RUNQUANTITY_FLD].Value = objObject.RunQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.PARTYID_FLD].Value = objObject.PartyID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PACER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_RoutingTable.PACER_FLD].Value = objObject.Pacer;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to delete data from ITM_Routing
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
			strSql = "DELETE " + ITM_RoutingTable.TABLE_NAME + " WHERE  " + "RoutingID" + "=" + pintID.ToString();
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
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}


			finally
			{
				if (oconPCS != null)
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
		///       This method uses to get data from ITM_Routing
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_RoutingVO
		///    </Outputs>
		///    <Returns>
		///       ITM_RoutingVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, January 19, 2005
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
				strSql = "SELECT "
					+ ITM_RoutingTable.ROUTINGID_FLD + ","
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD
					+ " FROM " + ITM_RoutingTable.TABLE_NAME
					+ " WHERE " + ITM_RoutingTable.ROUTINGID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_RoutingVO objObject = new ITM_RoutingVO();

				while (odrPCS.Read())
				{
					objObject.RoutingID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGID_FLD].ToString().Trim());
					objObject.Step = int.Parse(odrPCS[ITM_RoutingTable.STEP_FLD].ToString().Trim());
					objObject.Type = int.Parse(odrPCS[ITM_RoutingTable.TYPE_FLD].ToString().Trim());
					objObject.MachineSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINESETUPTIME_FLD].ToString().Trim());
					objObject.MachineRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINERUNTIME_FLD].ToString().Trim());
					objObject.Machines = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINES_FLD].ToString().Trim());
					objObject.LaborRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORRUNTIME_FLD].ToString().Trim());
					objObject.LaborSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORSETUPTIME_FLD].ToString().Trim());
					objObject.CrewSize = Decimal.Parse(odrPCS[ITM_RoutingTable.CREWSIZE_FLD].ToString().Trim());
					objObject.SetupQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.SETUPQUANTITY_FLD].ToString().Trim());
					objObject.StudyTime = Decimal.Parse(odrPCS[ITM_RoutingTable.STUDYTIME_FLD].ToString().Trim());
					objObject.MoveTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MOVETIME_FLD].ToString().Trim());
					objObject.LaborCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.LABORCOSTCENTERID_FLD].ToString().Trim());
					objObject.MachineCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[ITM_RoutingTable.PRODUCTID_FLD].ToString().Trim());
					objObject.FunctionID = int.Parse(odrPCS[ITM_RoutingTable.FUNCTIONID_FLD].ToString().Trim());
					objObject.WorkCenterID = int.Parse(odrPCS[ITM_RoutingTable.WORKCENTERID_FLD].ToString().Trim());
					objObject.EffectEndDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTENDDATE_FLD].ToString().Trim());
					objObject.OSVarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSVARLT_FLD].ToString().Trim());
					objObject.OSFixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSFIXLT_FLD].ToString().Trim());
					objObject.EffectBeginDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTBEGINDATE_FLD].ToString().Trim());
					objObject.OSOverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].ToString().Trim());
					objObject.OSOverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPQTY_FLD].ToString().Trim());
					objObject.OSScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.OSSCHEDULESEQ_FLD].ToString().Trim());
					objObject.OSCost = Decimal.Parse(odrPCS[ITM_RoutingTable.OSCOST_FLD].ToString().Trim());
					objObject.OverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString().Trim());
					objObject.OverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPQTY_FLD].ToString().Trim());
					objObject.ScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString().Trim());
					objObject.VarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.VARLT_FLD].ToString().Trim());
					objObject.FixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.FIXLT_FLD].ToString().Trim());
					objObject.RoutingStatusID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGSTATUSID_FLD].ToString().Trim());
					objObject.RunQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.RUNQUANTITY_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[ITM_RoutingTable.PARTYID_FLD].ToString().Trim());
					if(odrPCS[ITM_RoutingTable.PACER_FLD] != DBNull.Value)
					objObject.Pacer = odrPCS[ITM_RoutingTable.PACER_FLD].ToString().Trim();
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to get data from ITM_Routing by ID and RollUpDate
		///    </Description>
		///    <Inputs>
		///        int productID, DateTime
		///    </Inputs>
		///    <Outputs>
		///       ITM_RoutingVO
		///    </Outputs>
		///    <Returns>
		///       ITM_RoutingVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, January 19, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintProductID, DateTime pdtmRollUpDate)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_RoutingTable.ROUTINGID_FLD + ","
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD		
					+ " FROM " + ITM_RoutingTable.TABLE_NAME
					+ " WHERE " + ITM_RoutingTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND '" + pdtmRollUpDate + "' BETWEEN '" + ITM_RoutingTable.EFFECTBEGINDATE_FLD + "' AND '"
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_RoutingVO objObject = new ITM_RoutingVO();

				while (odrPCS.Read())
				{
					objObject.RoutingID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGID_FLD].ToString().Trim());
					objObject.Step = int.Parse(odrPCS[ITM_RoutingTable.STEP_FLD].ToString().Trim());
					objObject.Type = int.Parse(odrPCS[ITM_RoutingTable.TYPE_FLD].ToString().Trim());
					objObject.MachineSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINESETUPTIME_FLD].ToString().Trim());
					objObject.MachineRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINERUNTIME_FLD].ToString().Trim());
					objObject.Machines = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINES_FLD].ToString().Trim());
					objObject.LaborRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORRUNTIME_FLD].ToString().Trim());
					objObject.LaborSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORSETUPTIME_FLD].ToString().Trim());
					objObject.CrewSize = Decimal.Parse(odrPCS[ITM_RoutingTable.CREWSIZE_FLD].ToString().Trim());
					objObject.SetupQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.SETUPQUANTITY_FLD].ToString().Trim());
					objObject.StudyTime = Decimal.Parse(odrPCS[ITM_RoutingTable.STUDYTIME_FLD].ToString().Trim());
					objObject.MoveTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MOVETIME_FLD].ToString().Trim());
					objObject.LaborCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.LABORCOSTCENTERID_FLD].ToString().Trim());
					objObject.MachineCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[ITM_RoutingTable.PRODUCTID_FLD].ToString().Trim());
					objObject.FunctionID = int.Parse(odrPCS[ITM_RoutingTable.FUNCTIONID_FLD].ToString().Trim());
					objObject.WorkCenterID = int.Parse(odrPCS[ITM_RoutingTable.WORKCENTERID_FLD].ToString().Trim());
					objObject.EffectEndDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTENDDATE_FLD].ToString().Trim());
					objObject.OSVarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSVARLT_FLD].ToString().Trim());
					objObject.OSFixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSFIXLT_FLD].ToString().Trim());
					objObject.EffectBeginDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTBEGINDATE_FLD].ToString().Trim());
					objObject.OSOverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].ToString().Trim());
					objObject.OSOverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPQTY_FLD].ToString().Trim());
					objObject.OSScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.OSSCHEDULESEQ_FLD].ToString().Trim());
					objObject.OSCost = Decimal.Parse(odrPCS[ITM_RoutingTable.OSCOST_FLD].ToString().Trim());
					objObject.OverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString().Trim());
					objObject.OverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPQTY_FLD].ToString().Trim());
					objObject.ScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString().Trim());
					objObject.VarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.VARLT_FLD].ToString().Trim());
					objObject.FixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.FIXLT_FLD].ToString().Trim());
					objObject.RoutingStatusID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGSTATUSID_FLD].ToString().Trim());
					objObject.RunQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.RUNQUANTITY_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[ITM_RoutingTable.PARTYID_FLD].ToString().Trim());
					if(odrPCS[ITM_RoutingTable.PACER_FLD] != DBNull.Value)
					objObject.Pacer = odrPCS[ITM_RoutingTable.PACER_FLD].ToString().Trim();



				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to get data from ITM_Routing by ProductID
		///    </Description>
		///    <Inputs>
		///        int productID
		///    </Inputs>
		///    <Outputs>
		///       ArrayList
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       01-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOs()";

			ArrayList arrResult = new ArrayList();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_RoutingTable.ROUTINGID_FLD + ","
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD			

					+ " FROM " + ITM_RoutingTable.TABLE_NAME
					+ " WHERE " + ITM_RoutingTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					ITM_RoutingVO objObject = new ITM_RoutingVO();
					if (odrPCS[ITM_RoutingTable.ROUTINGID_FLD] != DBNull.Value)
						objObject.RoutingID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.STEP_FLD] != DBNull.Value)
						objObject.Step = int.Parse(odrPCS[ITM_RoutingTable.STEP_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.TYPE_FLD] != DBNull.Value)
						objObject.Type = int.Parse(odrPCS[ITM_RoutingTable.TYPE_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.MACHINESETUPTIME_FLD] != DBNull.Value)
						objObject.MachineSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINESETUPTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.MACHINERUNTIME_FLD] != DBNull.Value)
						objObject.MachineRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINERUNTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.MACHINES_FLD] != DBNull.Value)
						objObject.Machines = Decimal.Parse(odrPCS[ITM_RoutingTable.MACHINES_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.LABORRUNTIME_FLD] != DBNull.Value)
						objObject.LaborRunTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORRUNTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.LABORSETUPTIME_FLD] != DBNull.Value)
						objObject.LaborSetupTime = Decimal.Parse(odrPCS[ITM_RoutingTable.LABORSETUPTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.CREWSIZE_FLD] != DBNull.Value)
						objObject.CrewSize = Decimal.Parse(odrPCS[ITM_RoutingTable.CREWSIZE_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.SETUPQUANTITY_FLD] != DBNull.Value)
						objObject.SetupQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.SETUPQUANTITY_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.STUDYTIME_FLD] != DBNull.Value)
						objObject.StudyTime = Decimal.Parse(odrPCS[ITM_RoutingTable.STUDYTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.MOVETIME_FLD] != DBNull.Value)
						objObject.MoveTime = Decimal.Parse(odrPCS[ITM_RoutingTable.MOVETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.LABORCOSTCENTERID_FLD] != DBNull.Value)
						objObject.LaborCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.LABORCOSTCENTERID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.MACHINECOSTCENTERID_FLD] != DBNull.Value)
						objObject.MachineCostCenterID = int.Parse(odrPCS[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[ITM_RoutingTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.FUNCTIONID_FLD] != DBNull.Value)
						objObject.FunctionID = int.Parse(odrPCS[ITM_RoutingTable.FUNCTIONID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.WORKCENTERID_FLD] != DBNull.Value)
						objObject.WorkCenterID = int.Parse(odrPCS[ITM_RoutingTable.WORKCENTERID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.EFFECTENDDATE_FLD] != DBNull.Value)
						objObject.EffectEndDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTENDDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSVARLT_FLD] != DBNull.Value)
						objObject.OSVarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSVARLT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSFIXLT_FLD] != DBNull.Value)
						objObject.OSFixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.OSFIXLT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.EFFECTBEGINDATE_FLD] != DBNull.Value)
						objObject.EffectBeginDate = DateTime.Parse(odrPCS[ITM_RoutingTable.EFFECTBEGINDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSOVERLAPPERCENT_FLD] != DBNull.Value)
						objObject.OSOverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSOVERLAPQTY_FLD] != DBNull.Value)
						objObject.OSOverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OSOVERLAPQTY_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSSCHEDULESEQ_FLD] != DBNull.Value)
						objObject.OSScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.OSSCHEDULESEQ_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OSCOST_FLD] != DBNull.Value)
						objObject.OSCost = Decimal.Parse(odrPCS[ITM_RoutingTable.OSCOST_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OVERLAPPERCENT_FLD] != DBNull.Value)
						objObject.OverlapPercent = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPPERCENT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.OVERLAPQTY_FLD] != DBNull.Value)
						objObject.OverlapQty = Decimal.Parse(odrPCS[ITM_RoutingTable.OVERLAPQTY_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.SCHEDULESEQ_FLD] != DBNull.Value)
						objObject.ScheduleSeq = Decimal.Parse(odrPCS[ITM_RoutingTable.SCHEDULESEQ_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.VARLT_FLD] != DBNull.Value)
						objObject.VarLT = Decimal.Parse(odrPCS[ITM_RoutingTable.VARLT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.FIXLT_FLD] != DBNull.Value)
						objObject.FixLT = Decimal.Parse(odrPCS[ITM_RoutingTable.FIXLT_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.ROUTINGSTATUSID_FLD] != DBNull.Value)
						objObject.RoutingStatusID = int.Parse(odrPCS[ITM_RoutingTable.ROUTINGSTATUSID_FLD].ToString().Trim());
					if (odrPCS[ITM_RoutingTable.RUNQUANTITY_FLD] != DBNull.Value)
						objObject.RunQuantity = Decimal.Parse(odrPCS[ITM_RoutingTable.RUNQUANTITY_FLD].ToString().Trim());
					if(odrPCS[ITM_RoutingTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[ITM_RoutingTable.PARTYID_FLD].ToString().Trim());
					if(odrPCS[ITM_RoutingTable.PACER_FLD] != DBNull.Value)
						objObject.Pacer = odrPCS[ITM_RoutingTable.PACER_FLD].ToString().Trim();


					arrResult.Add(objObject);
				}
				arrResult.TrimToSize();
				return arrResult;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to update data to ITM_Routing
		///    </Description>
		///    <Inputs>
		///       ITM_RoutingVO       
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

			ITM_RoutingVO objObject = (ITM_RoutingVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE ITM_Routing SET "
					+ ITM_RoutingTable.STEP_FLD + "=   ?" + ","
					+ ITM_RoutingTable.TYPE_FLD + "=   ?" + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.MACHINES_FLD + "=   ?" + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + "=   ?" + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + "=   ?" + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.MOVETIME_FLD + "=   ?" + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSVARLT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OSCOST_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + "=   ?" + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + "=   ?" + ","
					+ ITM_RoutingTable.VARLT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.FIXLT_FLD + "=   ?" + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + "=   ?" + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + "=   ?" + ","
					+ ITM_RoutingTable.PARTYID_FLD + "=  ?" + ","
					+ ITM_RoutingTable.PACER_FLD + "=  ?"
					+ " WHERE " + ITM_RoutingTable.ROUTINGID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.STEP_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.STEP_FLD].Value = objObject.Step;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINESETUPTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINESETUPTIME_FLD].Value = objObject.MachineSetupTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINERUNTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINERUNTIME_FLD].Value = objObject.MachineRunTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINES_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINES_FLD].Value = objObject.Machines;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORRUNTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORRUNTIME_FLD].Value = objObject.LaborRunTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORSETUPTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORSETUPTIME_FLD].Value = objObject.LaborSetupTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.CREWSIZE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.CREWSIZE_FLD].Value = objObject.CrewSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.SETUPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.SETUPQUANTITY_FLD].Value = objObject.SetupQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.STUDYTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.STUDYTIME_FLD].Value = objObject.StudyTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MOVETIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.MOVETIME_FLD].Value = objObject.MoveTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.LABORCOSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.LABORCOSTCENTERID_FLD].Value = objObject.LaborCostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.MACHINECOSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.MACHINECOSTCENTERID_FLD].Value = objObject.MachineCostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.FUNCTIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.FUNCTIONID_FLD].Value = objObject.FunctionID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.WORKCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.WORKCENTERID_FLD].Value = objObject.WorkCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.EFFECTENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_RoutingTable.EFFECTENDDATE_FLD].Value = objObject.EffectEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSVARLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSVARLT_FLD].Value = objObject.OSVarLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSFIXLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSFIXLT_FLD].Value = objObject.OSFixLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.EFFECTBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_RoutingTable.EFFECTBEGINDATE_FLD].Value = objObject.EffectBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSOVERLAPPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSOVERLAPPERCENT_FLD].Value = objObject.OSOverlapPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSOVERLAPQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSOVERLAPQTY_FLD].Value = objObject.OSOverlapQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSSCHEDULESEQ_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSSCHEDULESEQ_FLD].Value = objObject.OSScheduleSeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OSCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OSCOST_FLD].Value = objObject.OSCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OVERLAPPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OVERLAPPERCENT_FLD].Value = objObject.OverlapPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.OVERLAPQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.OVERLAPQTY_FLD].Value = objObject.OverlapQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.SCHEDULESEQ_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.SCHEDULESEQ_FLD].Value = objObject.ScheduleSeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.VARLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.VARLT_FLD].Value = objObject.VarLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.FIXLT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.FIXLT_FLD].Value = objObject.FixLT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.ROUTINGSTATUSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.ROUTINGSTATUSID_FLD].Value = objObject.RoutingStatusID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.RUNQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_RoutingTable.RUNQUANTITY_FLD].Value = objObject.RunQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.PACER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_RoutingTable.PACER_FLD].Value = objObject.Pacer;


				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_RoutingTable.ROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_RoutingTable.ROUTINGID_FLD].Value = objObject.RoutingID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to get all data from ITM_Routing
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
		///       Wednesday, January 19, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ ITM_RoutingTable.ROUTINGID_FLD + ","
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD
					+ " FROM " + ITM_RoutingTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_RoutingTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       Wednesday, January 19, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ ITM_RoutingTable.ROUTINGID_FLD + ","
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD
					+ "  FROM " + ITM_RoutingTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, ITM_RoutingTable.TABLE_NAME);

			}
			catch (OleDbException ex)
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
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
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
		///       This method uses to 
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, January 24, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListRoutingByProduct(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".ListRoutingByProduct()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ "R." + ITM_RoutingTable.ROUTINGID_FLD + ","
					+ "R." + ITM_RoutingTable.STEP_FLD + ","
					+ "R." + ITM_RoutingTable.TYPE_FLD + ","
					+ "R." + ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ "R." + ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ "R." + ITM_RoutingTable.MACHINES_FLD + ","
					+ "R." + ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ "R." + ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ "R." + ITM_RoutingTable.CREWSIZE_FLD + ","
					+ "R." + ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ "R." + ITM_RoutingTable.STUDYTIME_FLD + ","
					+ "R." + ITM_RoutingTable.MOVETIME_FLD + ","
					+ "R." + ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ "R." + ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ "R." + ITM_RoutingTable.PRODUCTID_FLD + ","
					+ "R." + ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ "R." + ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ "R." + ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ "R." + ITM_RoutingTable.OSVARLT_FLD + ","
					+ "R." + ITM_RoutingTable.OSFIXLT_FLD + ","
					+ "R." + ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ "R." + ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ "R." + ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ "R." + ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ "R." + ITM_RoutingTable.OSCOST_FLD + ","
					+ "R." + ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ "R." + ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ "R." + ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ "R." + ITM_RoutingTable.VARLT_FLD + ","
					+ "R." + ITM_RoutingTable.FIXLT_FLD + ","
					+ "R." + ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ "R." + ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ "R." + ITM_RoutingTable.PARTYID_FLD + ","
					+ "R." + ITM_RoutingTable.PACER_FLD + ","
					+ "WC." + MST_WorkCenterTable.ISMAIN_FLD + ","
					+ " PL." + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + ","
					+ " PL." + PRO_ProductionLineTable.CODE_FLD + " ProductionLine"
					+ " FROM " + ITM_RoutingTable.TABLE_NAME
					+ " R LEFT JOIN MST_WorkCenter WC ON WC.WorkCenterID = R.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine PL ON PL.ProductionLineID = WC.ProductionLineID"
                    + " WHERE " + ITM_RoutingTable.PRODUCTID_FLD + "=" + pintProductID.ToString()
					+ " ORDER BY " + ITM_RoutingTable.STEP_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_RoutingTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		public void UpdateRoutingDescription(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateRoutingDescription()";

			ITM_ProductVO objObject = (ITM_ProductVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "UPDATE " + ITM_ProductTable.TABLE_NAME + " SET "
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + "= ?,"
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + "= ?,"
					+ ITM_ProductTable.CCNID_FLD + "= ?,"
					// added: dungla 15-02-2006
					+ ITM_ProductTable.PRODUCTIONLINEID_FLD + "= ?,"
					+ ITM_ProductTable.PRODUCTGROUPID_FLD + "= ?,"
					+ ITM_ProductTable.COSTCENTERRATEMASTERID_FLD + "= ?"
					// end: dungla 15-02-2006
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGDESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].Value = objObject.RoutingDescription;
				
				if (objObject.RoutingIncrement > 0)
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer)).Value = objObject.RoutingIncrement;
				else
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer)).Value = DBNull.Value;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = objObject.CCNID;
				}

				// added: dungla 15-02-2005
				if (objObject.ProductionLineID > 0)
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTIONLINEID_FLD, OleDbType.Integer)).Value = objObject.ProductionLineID;
				else
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTIONLINEID_FLD, OleDbType.Integer)).Value = DBNull.Value;
				
				if (objObject.ProductGroupID > 0)
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTGROUPID_FLD, OleDbType.Integer)).Value = objObject.ProductGroupID;
				else
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTGROUPID_FLD, OleDbType.Integer)).Value = DBNull.Value;
				
				if (objObject.CostCenterRateMasterID > 0)
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERRATEMASTERID_FLD, OleDbType.Integer)).Value = objObject.CostCenterRateMasterID;
				else
					ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERRATEMASTERID_FLD, OleDbType.Integer)).Value = DBNull.Value;
				// end added: dungla 15-02-2005
				
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQL_ARITHMETRIC_OVERFLOW)
				{
					throw new PCSDBException(ErrorCode.ERROR_NUMBER_OVERFLOW, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}

		}

		public void CopyRouting(int pintSourceProductID, int pintDestinationProductID)
		{
			const string METHOD_NAME = THIS + ".CopyRouting()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO ITM_Routing ( "
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.PRODUCTID_FLD + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD  + " ) "
					+ " SELECT " 
					+ ITM_RoutingTable.STEP_FLD + ","
					+ ITM_RoutingTable.TYPE_FLD + ","
					+ ITM_RoutingTable.MACHINESETUPTIME_FLD + ","
					+ ITM_RoutingTable.MACHINERUNTIME_FLD + ","
					+ ITM_RoutingTable.MACHINES_FLD + ","
					+ ITM_RoutingTable.LABORRUNTIME_FLD + ","
					+ ITM_RoutingTable.LABORSETUPTIME_FLD + ","
					+ ITM_RoutingTable.CREWSIZE_FLD + ","
					+ ITM_RoutingTable.SETUPQUANTITY_FLD + ","
					+ ITM_RoutingTable.STUDYTIME_FLD + ","
					+ ITM_RoutingTable.MOVETIME_FLD + ","
					+ ITM_RoutingTable.LABORCOSTCENTERID_FLD + ","
					+ ITM_RoutingTable.MACHINECOSTCENTERID_FLD + ","
					+ pintDestinationProductID + ","
					+ ITM_RoutingTable.FUNCTIONID_FLD + ","
					+ ITM_RoutingTable.WORKCENTERID_FLD + ","
					+ ITM_RoutingTable.EFFECTENDDATE_FLD + ","
					+ ITM_RoutingTable.OSVARLT_FLD + ","
					+ ITM_RoutingTable.OSFIXLT_FLD + ","
					+ ITM_RoutingTable.EFFECTBEGINDATE_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OSOVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.OSSCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.OSCOST_FLD + ","
					+ ITM_RoutingTable.OVERLAPPERCENT_FLD + ","
					+ ITM_RoutingTable.OVERLAPQTY_FLD + ","
					+ ITM_RoutingTable.SCHEDULESEQ_FLD + ","
					+ ITM_RoutingTable.VARLT_FLD + ","
					+ ITM_RoutingTable.FIXLT_FLD + ","
					+ ITM_RoutingTable.ROUTINGSTATUSID_FLD + ","
					+ ITM_RoutingTable.RUNQUANTITY_FLD + ","
					+ ITM_RoutingTable.PARTYID_FLD + ","
					+ ITM_RoutingTable.PACER_FLD  
					+ " FROM " + ITM_RoutingTable.TABLE_NAME 
					+ " WHERE " + ITM_RoutingTable.PRODUCTID_FLD + "=" + pintSourceProductID ;
				strSql += "; UPDATE " + ITM_ProductTable.TABLE_NAME + " SET " 
					+ ITM_ProductTable.PRODUCTIONLINEID_FLD + " = (Select ProductionLineID From Itm_Product where ProductID = " + pintSourceProductID + "), " 
					+ ITM_ProductTable.PRODUCTGROUPID_FLD + " = (Select ProductGroupID From Itm_Product where ProductID = " + pintSourceProductID + "), " 
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + " = (Select RoutingDescription From Itm_Product where ProductID = " + pintSourceProductID + "), "
					+ ITM_ProductTable.COSTCENTERRATEMASTERID_FLD + " = (Select CostCenterRateMasterID From Itm_Product where ProductID = " + pintSourceProductID + ") "
					+ " WHERE ProductID = " + pintDestinationProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Get Production Line Code from ID
		/// </summary>
		/// <param name="pintProductionLineID">Production Line ID</param>
		/// <returns>Production Line Code</returns>
		public string GetProductionLineCode(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".GetProductionLineInfo()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + PRO_ProductionLineTable.CODE_FLD + " FROM " + PRO_ProductionLineTable.TABLE_NAME
					+ " WHERE " + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD + "=" + pintProductionLineID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString();
				else
					return string.Empty;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Get Product Group Code from ID
		/// </summary>
		/// <param name="pintProductGroupID">Product Group ID</param>
		/// <returns>Product Group Code</returns>
		public string GetProductGroupCode(int pintProductGroupID)
		{
			const string METHOD_NAME = THIS + ".GetProductGroupCode()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + CST_ProductGroupTable.CODE_FLD + " FROM " + CST_ProductGroupTable.TABLE_NAME
					+ " WHERE " + CST_ProductGroupTable.PRODUCTGROUPID_FLD + "=" + pintProductGroupID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString();
				else
					return string.Empty;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Get Cost Center Rate Master Code from ID
		/// </summary>
		/// <param name="pintCostCenterRateMasterID">Cost Center Rate Master ID</param>
		/// <returns>Cost Center Rate Master Code</returns>
		public string GetCostCenterMasterCode(int pintCostCenterRateMasterID)
		{
			const string METHOD_NAME = THIS + ".GetProductGroupCode()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + STD_CostCenterRateMasterTable.CODE_FLD + " FROM " + STD_CostCenterRateMasterTable.TABLE_NAME
					+ " WHERE " + STD_CostCenterRateMasterTable.COSTCENTERRATEMASTERID_FLD + "=" + pintCostCenterRateMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString();
				else
					return string.Empty;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public bool IsWorkCenterInProductionLine(int pintProductionLineId, int pintWorkCenterId)
		{
			const string METHOD_NAME = THIS + ".IsWorkCenterInProductionLine()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT COUNT(*) FROM MST_WorkCenter WHERE WorkCenterID = " + pintWorkCenterId
					+ " AND ProductionLineID = " + pintProductionLineId;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				try
				{
					int intResult = Convert.ToInt32(ocmdPCS.ExecuteScalar());
					return intResult > 0;
				}
				catch
				{
					return false;
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		public bool HasWorkCenterNotInProductionLine(int pintProductionLineId, string pstrWorkCenterIDs)
		{
			const string METHOD_NAME = THIS + ".HasWorkCenterNotInProductionLine()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT COUNT(*) FROM MST_WorkCenter WHERE WorkCenterID IN (" + pstrWorkCenterIDs + ")"
					+ " AND ProductionLineID <> " + pintProductionLineId;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				try
				{
					int intResult = Convert.ToInt32(ocmdPCS.ExecuteScalar());
					return intResult > 0;
				}
				catch
				{
					return false;
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
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