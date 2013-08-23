using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_StockTakingDS 
	{
		public IV_StockTakingDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_StockTakingDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_StockTaking
		///    </Description>
		///    <Inputs>
		///        IV_StockTakingVO       
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
		///       Monday, November 20, 2006
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
				IV_StockTakingVO objObject = (IV_StockTakingVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_StockTaking("
					+ IV_StockTakingTable.QUANTITY_FLD + ","
					+ IV_StockTakingTable.SLIPCODE_FLD + ","
					+ IV_StockTakingTable.NOTE_FLD + ","
					+ IV_StockTakingTable.BOOKQUANTITY_FLD + ","
					+ IV_StockTakingTable.PRODUCTID_FLD + ","
					+ IV_StockTakingTable.STOCKUMID_FLD + ","
					+ IV_StockTakingTable.COUNTINGMETHODID_FLD + ","
					+ IV_StockTakingTable.STOCKTAKINGMASTERID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.SLIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingTable.SLIPCODE_FLD].Value = objObject.SlipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.NOTE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingTable.NOTE_FLD].Value = objObject.Note;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.BOOKQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingTable.BOOKQUANTITY_FLD].Value = objObject.BookQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.COUNTINGMETHODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.COUNTINGMETHODID_FLD].Value = objObject.CountingMethodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.STOCKTAKINGMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.STOCKTAKINGMASTERID_FLD].Value = objObject.StockTakingMasterID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from IV_StockTaking
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
			strSql = "DELETE " + IV_StockTakingTable.TABLE_NAME + " WHERE  " + "StockTakingID" + "=" + pintID.ToString();
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

	public void DeleteStockTaking(int pintStockTakingMasterID)
	{
		const string METHOD_NAME = THIS + ".Delete()";
		string strSql = String.Empty;
		strSql = "Delete " + IV_StockTakingTable.TABLE_NAME  + " Where StockTakingMasterID = " + pintStockTakingMasterID + ";" +
			"DELETE " + IV_StockTakingMasterTable.TABLE_NAME + " WHERE  " + "StockTakingMasterID =" + pintStockTakingMasterID;
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
		///       This method uses to get data from IV_StockTaking
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_StockTakingVO
		///    </Outputs>
		///    <Returns>
		///       IV_StockTakingVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Monday, November 20, 2006
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
					+ IV_StockTakingTable.STOCKTAKINGID_FLD + ","
					+ IV_StockTakingTable.QUANTITY_FLD + ","
					+ IV_StockTakingTable.SLIPCODE_FLD + ","
					+ IV_StockTakingTable.NOTE_FLD + ","
					+ IV_StockTakingTable.BOOKQUANTITY_FLD + ","
					+ IV_StockTakingTable.PRODUCTID_FLD + ","
					+ IV_StockTakingTable.STOCKUMID_FLD + ","
					+ IV_StockTakingTable.COUNTINGMETHODID_FLD + ","
					+ IV_StockTakingTable.STOCKTAKINGMASTERID_FLD
					+ " FROM " + IV_StockTakingTable.TABLE_NAME
					+ " WHERE " + IV_StockTakingTable.STOCKTAKINGID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_StockTakingVO objObject = new IV_StockTakingVO();

				while (odrPCS.Read())
				{
					objObject.StockTakingID = int.Parse(odrPCS[IV_StockTakingTable.STOCKTAKINGID_FLD].ToString());
					objObject.Quantity = Decimal.Parse(odrPCS[IV_StockTakingTable.QUANTITY_FLD].ToString());
					objObject.SlipCode = odrPCS[IV_StockTakingTable.SLIPCODE_FLD].ToString();
					objObject.Note = odrPCS[IV_StockTakingTable.NOTE_FLD].ToString();
					objObject.BookQuantity = Decimal.Parse(odrPCS[IV_StockTakingTable.BOOKQUANTITY_FLD].ToString());
					objObject.ProductID = int.Parse(odrPCS[IV_StockTakingTable.PRODUCTID_FLD].ToString());
					objObject.StockUMID = int.Parse(odrPCS[IV_StockTakingTable.STOCKUMID_FLD].ToString());
					objObject.CountingMethodID = int.Parse(odrPCS[IV_StockTakingTable.COUNTINGMETHODID_FLD].ToString());
					objObject.StockTakingMasterID = int.Parse(odrPCS[IV_StockTakingTable.STOCKTAKINGMASTERID_FLD].ToString());

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
		///       This method uses to update data to IV_StockTaking
		///    </Description>
		///    <Inputs>
		///       IV_StockTakingVO       
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

			IV_StockTakingVO objObject = (IV_StockTakingVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_StockTaking SET "
					+ IV_StockTakingTable.QUANTITY_FLD + "=   ?" + ","
					+ IV_StockTakingTable.SLIPCODE_FLD + "=   ?" + ","
					+ IV_StockTakingTable.NOTE_FLD + "=   ?" + ","
					+ IV_StockTakingTable.BOOKQUANTITY_FLD + "=   ?" + ","
					+ IV_StockTakingTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_StockTakingTable.STOCKUMID_FLD + "=   ?" + ","
					+ IV_StockTakingTable.COUNTINGMETHODID_FLD + "=   ?" + ","
					+ IV_StockTakingTable.STOCKTAKINGMASTERID_FLD + "=  ?"
					+ " WHERE " + IV_StockTakingTable.STOCKTAKINGID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.SLIPCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingTable.SLIPCODE_FLD].Value = objObject.SlipCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.NOTE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_StockTakingTable.NOTE_FLD].Value = objObject.Note;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.BOOKQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingTable.BOOKQUANTITY_FLD].Value = objObject.BookQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.COUNTINGMETHODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.COUNTINGMETHODID_FLD].Value = objObject.CountingMethodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.STOCKTAKINGMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.STOCKTAKINGMASTERID_FLD].Value = objObject.StockTakingMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingTable.STOCKTAKINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingTable.STOCKTAKINGID_FLD].Value = objObject.StockTakingID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all data from IV_StockTaking
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
		///       Monday, November 20, 2006
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
					+ IV_StockTakingTable.STOCKTAKINGID_FLD + ","
					+ IV_StockTakingTable.QUANTITY_FLD + ","
					+ IV_StockTakingTable.SLIPCODE_FLD + ","
					+ IV_StockTakingTable.NOTE_FLD + ","
					+ IV_StockTakingTable.BOOKQUANTITY_FLD + ","
					+ IV_StockTakingTable.PRODUCTID_FLD + ","
					+ IV_StockTakingTable.STOCKUMID_FLD + ","
					+ IV_StockTakingTable.COUNTINGMETHODID_FLD + ","
					+ IV_StockTakingTable.STOCKTAKINGMASTERID_FLD
					+ " FROM " + IV_StockTakingTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_StockTakingTable.TABLE_NAME);

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
		///       Code Generate
		///    </Authors>
		///    <History>
		///       Monday, November 20, 2006
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
					+ IV_StockTakingTable.STOCKTAKINGID_FLD + ","
					+ IV_StockTakingTable.QUANTITY_FLD + ","
					+ IV_StockTakingTable.SLIPCODE_FLD + ","
					+ IV_StockTakingTable.NOTE_FLD + ","
					+ IV_StockTakingTable.BOOKQUANTITY_FLD + ","
					+ IV_StockTakingTable.PRODUCTID_FLD + ","
					+ IV_StockTakingTable.STOCKUMID_FLD + ","
					+ IV_StockTakingTable.COUNTINGMETHODID_FLD + ","
					+ IV_StockTakingTable.STOCKTAKINGMASTERID_FLD
					+ " FROM " + IV_StockTakingTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_StockTakingTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		/// GetStockTakingByPeriodID
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, July 26 2006</date>
		public DataSet GetStockTakingByPeriodID(int pintStockTakingPeriodID)
		{
			const string METHOD_NAME = THIS + ".GetStockTakingByPeriodID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT LocationID, BinID, ProductID, SUM(Quantity) Quantity, SUM(BookQuantity) BookQuantity, StockTakingDate"
					+ " FROM IV_StockTakingMaster M JOIN IV_StockTaking D"
					+ " ON M.StockTakingMasterID = D.StockTakingMasterID"
					+ " WHERE M.StockTakingPeriodID = " + pintStockTakingPeriodID
					+ " GROUP BY LocationID, BinID, ProductID, StockTakingDate"
					+ " ORDER BY LocationID, BinID, ProductID, StockTakingDate DESC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_StockTakingTable.TABLE_NAME);

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

		public DataTable GetQuantityFromBin(int pintLocationID,int pintBinID)
		{
			const string METHOD_NAME = THIS + ".GetStockTakingByPeriodID()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT BinCacheID, OHQuantity, CommitQuantity, DemanQuantity, SupplyQuantity, Lot, InspStatus, BinID, CCNID, LocationID, MasterLocationID, ProductID"
					+ " From IV_BinCache Where LocationID = " + pintLocationID + " AND BinID = " + pintBinID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BinCacheTable.TABLE_NAME);

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


		public DataTable ListItemToUpdate(int pintPeriodID)
		{
			const string METHOD_NAME = THIS + ".ListItemToUpdate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT DISTINCT * FROM "
					+ " (SELECT B.LocationID, B.BinID, B.ProductID, StockUMID"
					+ " FROM IV_BinCache B JOIN ITM_Product P ON B.ProductID = P.ProductID"
					+ " UNION ALL"
					+ " SELECT LocationID, BinID, ProductID, StockUMID"
					+ " FROM IV_StockTakingMaster M JOIN IV_StockTaking D"
					+ " ON M.StockTakingMasterID = D.StockTakingMasterID"
					+ " WHERE M.StockTakingPeriodID = " + pintPeriodID + ") A"
					+ " ORDER BY A.LocationID, A.BinID, A.ProductID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
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


	}
}