using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.DataAccess;
namespace PCSComMaterials.Inventory.DS
{
	public class IV_BalanceLocationDS 
	{
		public IV_BalanceLocationDS()
		{
		}
		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_BalanceLocationDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_BalanceLocation
		///    </Description>
		///    <Inputs>
		///        IV_BalanceLocationVO       
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
				IV_BalanceLocationVO objObject = (IV_BalanceLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_BalanceLocation("
				+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.STOCKUMID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceLocationTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceLocationTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceLocationTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.STOCKUMID_FLD].Value = objObject.StockUMID;


				
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
		/// Add and return ID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, October 10 2006</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_BalanceLocationVO objObject = (IV_BalanceLocationVO) pobjObjectVO;
				string strSql = String.Empty;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_BalanceLocation("
					+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
					+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
					+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
					+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
					+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
					+ IV_BalanceLocationTable.STOCKUMID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceLocationTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceLocationTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.CommitQuantity != 0)
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.COMMITQUANTITY_FLD].Value = DBNull.Value;	
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID != 0)
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.STOCKUMID_FLD].Value = DBNull.Value;
				}

				
				strSql += " ; SELECT @@IDENTITY AS NEWID";

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
		///       This method uses to delete data from IV_BalanceLocation
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
			strSql=	"DELETE " + IV_BalanceLocationTable.TABLE_NAME + " WHERE  " + "BalanceLocationID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
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
		///       This method uses to get data from IV_BalanceLocation
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_BalanceLocationVO
		///    </Outputs>
		///    <Returns>
		///       IV_BalanceLocationVO
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
				+ IV_BalanceLocationTable.BALANCELOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.STOCKUMID_FLD
				+ " FROM " + IV_BalanceLocationTable.TABLE_NAME
				+" WHERE " + IV_BalanceLocationTable.BALANCELOCATIONID_FLD + "=" + pintID;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_BalanceLocationVO objObject = new IV_BalanceLocationVO();

				while (odrPCS.Read())
				{ 
				objObject.BalanceLocationID = int.Parse(odrPCS[IV_BalanceLocationTable.BALANCELOCATIONID_FLD].ToString());
				objObject.EffectDate = DateTime.Parse(odrPCS[IV_BalanceLocationTable.EFFECTDATE_FLD].ToString());
				objObject.OHQuantity = Decimal.Parse(odrPCS[IV_BalanceLocationTable.OHQUANTITY_FLD].ToString());
				objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_BalanceLocationTable.COMMITQUANTITY_FLD].ToString());
				objObject.ProductID = int.Parse(odrPCS[IV_BalanceLocationTable.PRODUCTID_FLD].ToString());
				objObject.LocationID = int.Parse(odrPCS[IV_BalanceLocationTable.LOCATIONID_FLD].ToString());
				objObject.MasterLocationID = int.Parse(odrPCS[IV_BalanceLocationTable.MASTERLOCATIONID_FLD].ToString());
				objObject.StockUMID = int.Parse(odrPCS[IV_BalanceLocationTable.STOCKUMID_FLD].ToString());

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
		///       This method uses to update data to IV_BalanceLocation
		///    </Description>
		///    <Inputs>
		///       IV_BalanceLocationVO       
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

			IV_BalanceLocationVO objObject = (IV_BalanceLocationVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_BalanceLocation SET "
				+ IV_BalanceLocationTable.EFFECTDATE_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.OHQUANTITY_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.PRODUCTID_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.LOCATIONID_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + "=   ?" + ","
				+ IV_BalanceLocationTable.STOCKUMID_FLD + "=  ?"
				+" WHERE " + IV_BalanceLocationTable.BALANCELOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceLocationTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceLocationTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.CommitQuantity != 0)
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.COMMITQUANTITY_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID != 0)
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceLocationTable.STOCKUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceLocationTable.BALANCELOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceLocationTable.BALANCELOCATIONID_FLD].Value = objObject.BalanceLocationID;


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
		///       This method uses to get all data from IV_BalanceLocation
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
				+ IV_BalanceLocationTable.BALANCELOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceLocationTable.TABLE_NAME;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BalanceLocationTable.TABLE_NAME);

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
		/// Get all balance location in this period and previous period
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, October 11 2006</date>
		public DataSet GetAllBalanceLocationInTwoPeriod(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllBalanceLocationInTwoPeriod()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_BalanceLocationTable.BALANCELOCATIONID_FLD + ","
					+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
					+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
					+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
					+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
					+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
					+ IV_BalanceLocationTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceLocationTable.TABLE_NAME
					+ " WHERE " + IV_BalanceLocationTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.ToShortDateString() + "' OR " 
					+ IV_BalanceLocationTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.AddMonths(-1).ToShortDateString() + "'"; 
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BalanceLocationTable.TABLE_NAME);

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
				+ IV_BalanceLocationTable.BALANCELOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.EFFECTDATE_FLD + ","
				+ IV_BalanceLocationTable.OHQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.COMMITQUANTITY_FLD + ","
				+ IV_BalanceLocationTable.PRODUCTID_FLD + ","
				+ IV_BalanceLocationTable.LOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.MASTERLOCATIONID_FLD + ","
				+ IV_BalanceLocationTable.STOCKUMID_FLD
				+ " FROM " + IV_BalanceLocationTable.TABLE_NAME;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_BalanceLocationTable.TABLE_NAME);

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
