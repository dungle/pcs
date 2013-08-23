using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
namespace PCSComMaterials.Inventory.DS
{
	public class IV_BalanceMasterLocationDS 
	{
		public IV_BalanceMasterLocationDS()
		{
		}
		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_BalanceMasterLocationDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_BalanceMasterLocation
		///    </Description>
		///    <Inputs>
		///        IV_BalanceMasterLocationVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       Code generate
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
				IV_BalanceMasterLocationVO objObject = (IV_BalanceMasterLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_BalanceMasterLocation("
				+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.STOCKUMID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.STOCKUMID_FLD].Value = objObject.StockUMID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from IV_BalanceMasterLocation
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
		///       Code generate
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
			strSql=	"DELETE " + IV_BalanceMasterLocationTable.TABLE_NAME + " WHERE  " + "BalanceMasterLocationID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_BalanceMasterLocation
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_BalanceMasterLocationVO
		///    </Outputs>
		///    <Returns>
		///       IV_BalanceMasterLocationVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
				+ IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.STOCKUMID_FLD
				+ " FROM " + IV_BalanceMasterLocationTable.TABLE_NAME
				+" WHERE " + IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_BalanceMasterLocationVO objObject = new IV_BalanceMasterLocationVO();

				while (odrPCS.Read())
				{ 
				objObject.BalanceMasterLocationID = int.Parse(odrPCS[IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD].ToString());
				objObject.EffectDate = DateTime.Parse(odrPCS[IV_BalanceMasterLocationTable.EFFECTDATE_FLD].ToString());
				objObject.OHQuantity = Decimal.Parse(odrPCS[IV_BalanceMasterLocationTable.OHQUANTITY_FLD].ToString());
				objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD].ToString());
				objObject.ProductID = int.Parse(odrPCS[IV_BalanceMasterLocationTable.PRODUCTID_FLD].ToString());
				objObject.MasterLocationID = int.Parse(odrPCS[IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD].ToString());
				objObject.StockUMID = int.Parse(odrPCS[IV_BalanceMasterLocationTable.STOCKUMID_FLD].ToString());

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
		///       This method uses to update data to IV_BalanceMasterLocation
		///    </Description>
		///    <Inputs>
		///       IV_BalanceMasterLocationVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Code Generate 
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

			IV_BalanceMasterLocationVO objObject = (IV_BalanceMasterLocationVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_BalanceMasterLocation SET "
				+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + "=   ?" + ","
				+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + "=   ?" + ","
				+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + "=   ?" + ","
				+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + "=   ?" + ","
				+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + "=   ?" + ","
				+ IV_BalanceMasterLocationTable.STOCKUMID_FLD + "=  ?"
				+" WHERE " + IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD].Value = objObject.BalanceMasterLocationID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		/// Get all record in two period, this period and previous period
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 17 2006</date>
		public DataSet GetAllBalanceMasterLocationInTwoPeriod(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllBalanceMasterLocationInTwoPeriod()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + ","
					+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + ","
					+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + ","
					+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + ","
					+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + ","
					+ IV_BalanceMasterLocationTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceMasterLocationTable.TABLE_NAME
					+ " WHERE " + IV_BalanceMasterLocationTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.ToShortDateString() + "' OR " 
					+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.AddMonths(-1).ToShortDateString() + "'"; 
				PCSComUtils.DataAccess.Utils utils = new PCSComUtils.DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BalanceMasterLocationTable.TABLE_NAME);

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
		///       This method uses to get all data from IV_BalanceMasterLocation
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
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
				+ IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceMasterLocationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BalanceMasterLocationTable.TABLE_NAME);

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
		///       Code Generate
		///    </Authors>
		///    <History>
		///       Thursday, October 05, 2006
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
				+ IV_BalanceMasterLocationTable.BALANCEMASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceMasterLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceMasterLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceMasterLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceMasterLocationTable.STOCKUMID_FLD
				+ " FROM " + IV_BalanceMasterLocationTable.TABLE_NAME;	
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_BalanceMasterLocationTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
