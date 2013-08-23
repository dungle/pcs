using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_StockTakingDifferentDS 
	{
		public IV_StockTakingDifferentDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_StockTakingDifferentDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_StockTakingDifferent
		///    </Description>
		///    <Inputs>
		///        IV_StockTakingDifferentVO       
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
		///       Tuesday, December 26, 2006
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
				IV_StockTakingDifferentVO objObject = (IV_StockTakingDifferentVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_StockTakingDifferent("
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + ","
					+ IV_StockTakingDifferentTable.BINID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD].Value = objObject.ActualQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD].Value = objObject.DifferentQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.BINID_FLD].Value = objObject.BinID;


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
		///       This method uses to delete data from IV_StockTakingDifferent
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
			strSql = "DELETE " + IV_StockTakingDifferentTable.TABLE_NAME + " WHERE  " + "StockTakingDifferentID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_StockTakingDifferent
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_StockTakingDifferentVO
		///    </Outputs>
		///    <Returns>
		///       IV_StockTakingDifferentVO
		///    </Returns>
		///    <Authors>
		///       Code Generate 
		///    </Authors>
		///    <History>
		///       Tuesday, December 26, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + ","
					+ IV_StockTakingDifferentTable.BINID_FLD
					+ " FROM " + IV_StockTakingDifferentTable.TABLE_NAME
					+ " WHERE " + IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_StockTakingDifferentVO objObject = new IV_StockTakingDifferentVO();

				while (odrPCS.Read())
				{
					objObject.StockTakingDifferentID = int.Parse(odrPCS[IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD].ToString());
					objObject.StockTakingDate = DateTime.Parse(odrPCS[IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD].ToString());
					objObject.OHQuantity = Decimal.Parse(odrPCS[IV_StockTakingDifferentTable.OHQUANTITY_FLD].ToString());
					objObject.ActualQuantity = Decimal.Parse(odrPCS[IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD].ToString());
					objObject.DifferentQuantity = Decimal.Parse(odrPCS[IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD].ToString());
					objObject.StockTakingPeriodID = int.Parse(odrPCS[IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD].ToString());
					objObject.ProductID = int.Parse(odrPCS[IV_StockTakingDifferentTable.PRODUCTID_FLD].ToString());
					objObject.LocationID = int.Parse(odrPCS[IV_StockTakingDifferentTable.LOCATIONID_FLD].ToString());
					objObject.BinID = int.Parse(odrPCS[IV_StockTakingDifferentTable.BINID_FLD].ToString());

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
		///       This method uses to update data to IV_StockTakingDifferent
		///    </Description>
		///    <Inputs>
		///       IV_StockTakingDifferentVO       
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

			IV_StockTakingDifferentVO objObject = (IV_StockTakingDifferentVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_StockTakingDifferent SET "
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + "=   ?" + ","
					+ IV_StockTakingDifferentTable.BINID_FLD + "=  ?"
					+ " WHERE " + IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD].Value = objObject.StockTakingDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD].Value = objObject.ActualQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD].Value = objObject.DifferentQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD].Value = objObject.StockTakingPeriodID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD, OleDbType.BigInt));
				ocmdPCS.Parameters[IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD].Value = objObject.StockTakingDifferentID;


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
		///       This method uses to get all data from IV_StockTakingDifferent
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
		///       Tuesday, December 26, 2006
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
					+ IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + ","
					+ IV_StockTakingDifferentTable.BINID_FLD
					+ " FROM " + IV_StockTakingDifferentTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_StockTakingDifferentTable.TABLE_NAME);

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

		public DataTable List(int pintPeriodID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.HISTORYQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + ","
					+ IV_StockTakingDifferentTable.BINID_FLD
					+ " FROM " + IV_StockTakingDifferentTable.TABLE_NAME
					+ " WHERE " + IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + "=" + pintPeriodID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(IV_StockTakingDifferentTable.TABLE_NAME);
				odadPCS.Fill(dtbData);

				return dtbData;
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
		///       Tuesday, December 26, 2006
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
					+ IV_StockTakingDifferentTable.STOCKTAKINGDIFFERENTID_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGDATE_FLD + ","
					+ IV_StockTakingDifferentTable.OHQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.ACTUALQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.DIFFERENTQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.HISTORYQUANTITY_FLD + ","
					+ IV_StockTakingDifferentTable.STOCKTAKINGPERIODID_FLD + ","
					+ IV_StockTakingDifferentTable.PRODUCTID_FLD + ","
					+ IV_StockTakingDifferentTable.LOCATIONID_FLD + ","
					+ IV_StockTakingDifferentTable.BINID_FLD
					+ " FROM " + IV_StockTakingDifferentTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_StockTakingDifferentTable.TABLE_NAME);

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
	}
}