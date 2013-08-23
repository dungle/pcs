using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_BalanceBinDS 
	{
		public IV_BalanceBinDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.DS.IV_BalanceBinDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_BalanceBin
		///    </Description>
		///    <Inputs>
		///        IV_BalanceBinVO       
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

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_BalanceBinVO objObject = (IV_BalanceBinVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_BalanceBin("
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceBinTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceBinTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceBinTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.STOCKUMID_FLD].Value = objObject.StockUMID;


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
		/// <summary>
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_BalanceBinVO objObject = (IV_BalanceBinVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_BalanceBin("
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceBinTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceBinTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.CommitQuantity != 0)
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.COMMITQUANTITY_FLD].Value = DBNull.Value;	
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID != 0)
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.STOCKUMID_FLD].Value = DBNull.Value;
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
		///       This method uses to delete data from IV_BalanceBin
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
			strSql = "DELETE " + IV_BalanceBinTable.TABLE_NAME + " WHERE  " + "BalanceBinID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_BalanceBin
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_BalanceBinVO
		///    </Outputs>
		///    <Returns>
		///       IV_BalanceBinVO
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
				strSql = "SELECT "
					+ IV_BalanceBinTable.BALANCEBINID_FLD + ","
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceBinTable.TABLE_NAME
					+ " WHERE " + IV_BalanceBinTable.BALANCEBINID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_BalanceBinVO objObject = new IV_BalanceBinVO();

				while (odrPCS.Read())
				{
					objObject.BalanceBinID = int.Parse(odrPCS[IV_BalanceBinTable.BALANCEBINID_FLD].ToString());
					objObject.EffectDate = DateTime.Parse(odrPCS[IV_BalanceBinTable.EFFECTDATE_FLD].ToString());
					objObject.OHQuantity = Decimal.Parse(odrPCS[IV_BalanceBinTable.OHQUANTITY_FLD].ToString());
					objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_BalanceBinTable.COMMITQUANTITY_FLD].ToString());
					objObject.ProductID = int.Parse(odrPCS[IV_BalanceBinTable.PRODUCTID_FLD].ToString());
					objObject.LocationID = int.Parse(odrPCS[IV_BalanceBinTable.LOCATIONID_FLD].ToString());
					objObject.BinID = int.Parse(odrPCS[IV_BalanceBinTable.BINID_FLD].ToString());
					objObject.StockUMID = int.Parse(odrPCS[IV_BalanceBinTable.STOCKUMID_FLD].ToString());

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
		///       This method uses to update data to IV_BalanceBin
		///    </Description>
		///    <Inputs>
		///       IV_BalanceBinVO       
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

			IV_BalanceBinVO objObject = (IV_BalanceBinVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_BalanceBin SET "
					+ IV_BalanceBinTable.EFFECTDATE_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.BINID_FLD + "=   ?" + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD + "=  ?"
					+ " WHERE " + IV_BalanceBinTable.BALANCEBINID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.EFFECTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_BalanceBinTable.EFFECTDATE_FLD].Value = objObject.EffectDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BalanceBinTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.CommitQuantity != 0)
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.COMMITQUANTITY_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID != 0)
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}
				else
				{
					ocmdPCS.Parameters[IV_BalanceBinTable.STOCKUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BalanceBinTable.BALANCEBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BalanceBinTable.BALANCEBINID_FLD].Value = objObject.BalanceBinID;


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
		///       This method uses to get all data from IV_BalanceBin
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


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ IV_BalanceBinTable.BALANCEBINID_FLD + ","
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceBinTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_BalanceBinTable.TABLE_NAME);

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
		///       Thursday, October 05, 2006
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
					+ IV_BalanceBinTable.BALANCEBINID_FLD + ","
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceBinTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_BalanceBinTable.TABLE_NAME);

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
		/// Get all OnHand Balance Bin in this period and previous period
		/// </summary>
		/// <param name="pdtmEffectDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, October 11 2006</date>
		public DataSet GetAllOnHandBalanceBinInTwoPeriod(DateTime pdtmEffectDate)
		{
			const string METHOD_NAME = THIS + ".GetAllOnHandBalanceBinInTwoPeriod()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ IV_BalanceBinTable.BALANCEBINID_FLD + ","
					+ IV_BalanceBinTable.EFFECTDATE_FLD + ","
					+ IV_BalanceBinTable.OHQUANTITY_FLD + ","
					+ IV_BalanceBinTable.COMMITQUANTITY_FLD + ","
					+ IV_BalanceBinTable.PRODUCTID_FLD + ","
					+ IV_BalanceBinTable.LOCATIONID_FLD + ","
					+ IV_BalanceBinTable.BINID_FLD + ","
					+ IV_BalanceBinTable.STOCKUMID_FLD
					+ " FROM " + IV_BalanceBinTable.TABLE_NAME
					+ " WHERE " + IV_BalanceBinTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.ToShortDateString() + "'"
					+ " OR " + IV_BalanceBinTable.EFFECTDATE_FLD + " = '" + pdtmEffectDate.AddMonths(-1).ToShortDateString() + "'";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_BalanceBinTable.TABLE_NAME);

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

		public DataTable GetBalanceQuantity(DateTime pdtmDate)
		{
			const string METHOD_NAME = THIS + ".GetBalanceQuantity()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmLastMonth = pdtmDate.AddMonths(-1);
				string strSql = "SELECT ProductID, SUM(ISNULL(OHQuantity,0)) AS OHQuantity"
					+ " FROM IV_BalanceBin IB JOIN MST_BIN MB ON IB.BinID = MB.BinID"
					+ " AND MB.BinTypeID <> " + (int)BinTypeEnum.LS
					+ " WHERE DATEPART(year, EffectDate) = " + dtmLastMonth.Year
					+ " AND DATEPART(month, EffectDate) = " + dtmLastMonth.Month
					+ " GROUP BY ProductID ORDER BY ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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

		public DataTable GetAvailableQuantityForPlan(DateTime pdtmDate)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityForPlan()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmLastMonth = pdtmDate.AddMonths(-1);
				string strSql = "SELECT IB.LocationID, ProductID, SUM(ISNULL(OHQuantity,0)) AS SupplyQuantity"
					+ " FROM IV_BalanceBin IB JOIN MST_BIN MB ON IB.BinID = MB.BinID"
					+ " AND MB.BinTypeID NOT IN (" + (int)BinTypeEnum.NG + "," + (int)BinTypeEnum.LS + ")"
					+ " WHERE DATEPART(year, EffectDate) = " + dtmLastMonth.Year
					+ " AND DATEPART(month, EffectDate) = " + dtmLastMonth.Month
					+ " GROUP BY IB.LocationID, ProductID ORDER BY IB.LocationID, ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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
		
		/// <summary>
		/// Get balance in OK and Buffer bin
		/// </summary>
		/// <param name="pdtmDate">Effect Date</param>
		/// <returns></returns>
		public DataTable GetBeginBalance(DateTime pdtmDate)
		{
			const string METHOD_NAME = THIS + ".GetBeginBalance()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DateTime dtmLastMonth = pdtmDate.AddMonths(-1);
				string strSql = "SELECT ProductID, SUM(ISNULL(OHQuantity,0)) AS Quantity"
					+ " FROM IV_BalanceBin IB JOIN MST_BIN MB ON IB.BinID = MB.BinID"
					+ " AND MB.BinTypeID NOT IN (" + (int)BinTypeEnum.LS + "," + (int)BinTypeEnum.NG + ")"
					+ " WHERE DATEPART(year, EffectDate) = " + dtmLastMonth.Year
					+ " AND DATEPART(month, EffectDate) = " + dtmLastMonth.Month
					+ " GROUP BY ProductID ORDER BY ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable(IV_BalanceBinTable.TABLE_NAME);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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

	}
}