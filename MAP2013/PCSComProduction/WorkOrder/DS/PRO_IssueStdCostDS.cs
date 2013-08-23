using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.WorkOrder.DS
{
	public class PRO_IssueStdCostDS 
	{
		public PRO_IssueStdCostDS()
		{
		}
		private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_IssueStdCostDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_IssueStdCost
		///    </Description>
		///    <Inputs>
		///        PRO_IssueStdCostVO       
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
		///       Tuesday, May 31, 2005
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
				PRO_IssueStdCostVO objObject = (PRO_IssueStdCostVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_IssueStdCost("
				+ PRO_IssueStdCostTable.POSTDATE_FLD + ","
				+ PRO_IssueStdCostTable.TRANSTYPE_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + ","
				+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + ","
				+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + ","
				+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + ","
				+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + ","
				+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + ","
				+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.TRANSTYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.TRANSTYPE_FLD].Value = objObject.TransType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMATERIAL01_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMATERIAL01_FLD].Value = objObject.CostMaterial01;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD].Value = objObject.CostMaterialOverHead02;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD].Value = objObject.CostMachineSetup03;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD].Value = objObject.CostMachineSetupFixed04;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD].Value = objObject.CostMachineSetupVar05;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINERUN06_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINERUN06_FLD].Value = objObject.CostMachineRun06;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD].Value = objObject.CostMachineFixed07;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD].Value = objObject.CostMachineVariable08;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUP09_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUP09_FLD].Value = objObject.CostLaborSetup09;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD].Value = objObject.CostLaborSetupFixed10;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD].Value = objObject.CostLaborSetupVariable11;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORRUN12_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORRUN12_FLD].Value = objObject.CostLaborRun12;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORFIXED13_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORFIXED13_FLD].Value = objObject.CostLaborFixed13;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD].Value = objObject.CostLaborVariable14;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD].Value = objObject.CostOutsideProc15;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD].Value = objObject.CostAssemblyScrap16;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTSHRINK17_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTSHRINK17_FLD].Value = objObject.CostShrink17;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTFREIGHT18_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTFREIGHT18_FLD].Value = objObject.CostFreight18;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD].Value = objObject.CostUserStandard1_19;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD].Value = objObject.CostUserStandard2_20;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD].Value = objObject.CostTotalAmount21;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD].Value = objObject.IssueMaterialDetailID;


				
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
		///       This method uses to delete data from PRO_IssueStdCost
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
			strSql=	"DELETE " + PRO_IssueStdCostTable.TABLE_NAME + " WHERE  " + "IssueStdCostID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PRO_IssueStdCost
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_IssueStdCostVO
		///    </Outputs>
		///    <Returns>
		///       PRO_IssueStdCostVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + ","
				+ PRO_IssueStdCostTable.POSTDATE_FLD + ","
				+ PRO_IssueStdCostTable.TRANSTYPE_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + ","
				+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + ","
				+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + ","
				+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + ","
				+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + ","
				+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + ","
				+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD
				+ " FROM " + PRO_IssueStdCostTable.TABLE_NAME
				+" WHERE " + PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_IssueStdCostVO objObject = new PRO_IssueStdCostVO();

				while (odrPCS.Read())
				{ 
				objObject.IssueStdCostID = int.Parse(odrPCS[PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD].ToString().Trim());
				objObject.PostDate = DateTime.Parse(odrPCS[PRO_IssueStdCostTable.POSTDATE_FLD].ToString().Trim());
				objObject.TransType = odrPCS[PRO_IssueStdCostTable.TRANSTYPE_FLD].ToString().Trim();
				objObject.CostMaterial01 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMATERIAL01_FLD].ToString().Trim());
				objObject.CostMaterialOverHead02 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD].ToString().Trim());
				objObject.CostMachineSetup03 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD].ToString().Trim());
				objObject.CostMachineSetupFixed04 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD].ToString().Trim());
				objObject.CostMachineSetupVar05 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD].ToString().Trim());
				objObject.CostMachineRun06 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINERUN06_FLD].ToString().Trim());
				objObject.CostMachineFixed07 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD].ToString().Trim());
				objObject.CostMachineVariable08 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD].ToString().Trim());
				objObject.CostLaborSetup09 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUP09_FLD].ToString().Trim());
				objObject.CostLaborSetupFixed10 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD].ToString().Trim());
				objObject.CostLaborSetupVariable11 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD].ToString().Trim());
				objObject.CostLaborRun12 = odrPCS[PRO_IssueStdCostTable.COSTLABORRUN12_FLD].ToString().Trim();
				objObject.CostLaborFixed13 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORFIXED13_FLD].ToString().Trim());
				objObject.CostLaborVariable14 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD].ToString().Trim());
				objObject.CostOutsideProc15 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD].ToString().Trim());
				objObject.CostAssemblyScrap16 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD].ToString().Trim());
				objObject.CostShrink17 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTSHRINK17_FLD].ToString().Trim());
				objObject.CostFreight18 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTFREIGHT18_FLD].ToString().Trim());
				objObject.CostUserStandard1_19 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD].ToString().Trim());
				objObject.CostUserStandard2_20 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD].ToString().Trim());
				objObject.CostTotalAmount21 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD].ToString().Trim());
				objObject.IssueMaterialDetailID = int.Parse(odrPCS[PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD].ToString().Trim());

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
		/// GetObjectVOByProduct
		/// </summary>
		/// <param name="pintIssueMaterialDetailID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, September 5 2005</date>
		public object GetObjectVOByProduct(int pintIssueMaterialDetailID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOByProduct()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + ","
					+ PRO_IssueStdCostTable.POSTDATE_FLD + ","
					+ PRO_IssueStdCostTable.TRANSTYPE_FLD + ","
					+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + ","
					+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + ","
					+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + ","
					+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + ","
					+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + ","
					+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + ","
					+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + ","
					+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + ","
					+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + ","
					+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + ","
					+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + ","
					+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD
					+ " FROM " + PRO_IssueStdCostTable.TABLE_NAME
					+" WHERE " + PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD + "=" + pintIssueMaterialDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_IssueStdCostVO objObject = new PRO_IssueStdCostVO();

				while (odrPCS.Read())
				{ 
					objObject.IssueStdCostID = int.Parse(odrPCS[PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[PRO_IssueStdCostTable.POSTDATE_FLD].ToString().Trim());
					objObject.TransType = odrPCS[PRO_IssueStdCostTable.TRANSTYPE_FLD].ToString().Trim();
					objObject.CostMaterial01 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMATERIAL01_FLD].ToString().Trim());
					objObject.CostMaterialOverHead02 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD].ToString().Trim());
					objObject.CostMachineSetup03 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD].ToString().Trim());
					objObject.CostMachineSetupFixed04 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD].ToString().Trim());
					objObject.CostMachineSetupVar05 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD].ToString().Trim());
					objObject.CostMachineRun06 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINERUN06_FLD].ToString().Trim());
					objObject.CostMachineFixed07 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD].ToString().Trim());
					objObject.CostMachineVariable08 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD].ToString().Trim());
					objObject.CostLaborSetup09 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUP09_FLD].ToString().Trim());
					objObject.CostLaborSetupFixed10 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD].ToString().Trim());
					objObject.CostLaborSetupVariable11 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD].ToString().Trim());
					objObject.CostLaborRun12 = odrPCS[PRO_IssueStdCostTable.COSTLABORRUN12_FLD].ToString().Trim();
					objObject.CostLaborFixed13 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORFIXED13_FLD].ToString().Trim());
					objObject.CostLaborVariable14 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD].ToString().Trim());
					objObject.CostOutsideProc15 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD].ToString().Trim());
					objObject.CostAssemblyScrap16 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD].ToString().Trim());
					objObject.CostShrink17 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTSHRINK17_FLD].ToString().Trim());
					objObject.CostFreight18 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTFREIGHT18_FLD].ToString().Trim());
					objObject.CostUserStandard1_19 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD].ToString().Trim());
					objObject.CostUserStandard2_20 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD].ToString().Trim());
					objObject.CostTotalAmount21 = Decimal.Parse(odrPCS[PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD].ToString().Trim());
					objObject.IssueMaterialDetailID = int.Parse(odrPCS[PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_IssueStdCost
		///    </Description>
		///    <Inputs>
		///       PRO_IssueStdCostVO       
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

			PRO_IssueStdCostVO objObject = (PRO_IssueStdCostVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_IssueStdCost SET "
				+ PRO_IssueStdCostTable.POSTDATE_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.TRANSTYPE_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + "=   ?" + ","
				+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD + "=  ?"
				+" WHERE " + PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.TRANSTYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.TRANSTYPE_FLD].Value = objObject.TransType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMATERIAL01_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMATERIAL01_FLD].Value = objObject.CostMaterial01;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD].Value = objObject.CostMaterialOverHead02;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD].Value = objObject.CostMachineSetup03;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD].Value = objObject.CostMachineSetupFixed04;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD].Value = objObject.CostMachineSetupVar05;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINERUN06_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINERUN06_FLD].Value = objObject.CostMachineRun06;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD].Value = objObject.CostMachineFixed07;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD].Value = objObject.CostMachineVariable08;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUP09_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUP09_FLD].Value = objObject.CostLaborSetup09;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD].Value = objObject.CostLaborSetupFixed10;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD].Value = objObject.CostLaborSetupVariable11;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORRUN12_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORRUN12_FLD].Value = objObject.CostLaborRun12;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORFIXED13_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORFIXED13_FLD].Value = objObject.CostLaborFixed13;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD].Value = objObject.CostLaborVariable14;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD].Value = objObject.CostOutsideProc15;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD].Value = objObject.CostAssemblyScrap16;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTSHRINK17_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTSHRINK17_FLD].Value = objObject.CostShrink17;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTFREIGHT18_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTFREIGHT18_FLD].Value = objObject.CostFreight18;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD].Value = objObject.CostUserStandard1_19;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD].Value = objObject.CostUserStandard2_20;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD].Value = objObject.CostTotalAmount21;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD].Value = objObject.IssueMaterialDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD].Value = objObject.IssueStdCostID;


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
		///       This method uses to get all data from PRO_IssueStdCost
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
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + ","
				+ PRO_IssueStdCostTable.POSTDATE_FLD + ","
				+ PRO_IssueStdCostTable.TRANSTYPE_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + ","
				+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + ","
				+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + ","
				+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + ","
				+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + ","
				+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + ","
				+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD
					+ " FROM " + PRO_IssueStdCostTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueStdCostTable.TABLE_NAME);

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
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueStdCostTable.ISSUESTDCOSTID_FLD + ","
				+ PRO_IssueStdCostTable.POSTDATE_FLD + ","
				+ PRO_IssueStdCostTable.TRANSTYPE_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIAL01_FLD + ","
				+ PRO_IssueStdCostTable.COSTMATERIALOVERHEAD02_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUP03_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPFIXED04_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINESETUPVAR05_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINERUN06_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEFIXED07_FLD + ","
				+ PRO_IssueStdCostTable.COSTMACHINEVARIABLE08_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUP09_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPFIXED10_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORSETUPVARIABLE11_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORRUN12_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORFIXED13_FLD + ","
				+ PRO_IssueStdCostTable.COSTLABORVARIABLE14_FLD + ","
				+ PRO_IssueStdCostTable.COSTOUTSIDEPROC15_FLD + ","
				+ PRO_IssueStdCostTable.COSTASSEMBLYSCRAP16_FLD + ","
				+ PRO_IssueStdCostTable.COSTSHRINK17_FLD + ","
				+ PRO_IssueStdCostTable.COSTFREIGHT18_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD1_19_FLD + ","
				+ PRO_IssueStdCostTable.COSTUSERSTANDARD2_20_FLD + ","
				+ PRO_IssueStdCostTable.COSTTOTALAMOUNT21_FLD + ","
				+ PRO_IssueStdCostTable.ISSUEMATERIALDETAILID_FLD 
		+ "  FROM " + PRO_IssueStdCostTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_IssueStdCostTable.TABLE_NAME);

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
