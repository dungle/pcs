using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComMaterials.Inventory.DS
{
	
	public class IV_CostHistoryDS 
	{
		public IV_CostHistoryDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.DS.IV_CostHistoryDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_CostHistory
		///    </Description>
		///    <Inputs>
		///        IV_CostHistoryVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///       DungLa - 10 Mar 2005
		///    </Authors>
		///    <History>
		///       Wednesday, February 23, 2005
		///       10-Mar-2005: DungLa
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
				IV_CostHistoryVO objObject = (IV_CostHistoryVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(string.Empty, oconPCS);

				strSql = "INSERT INTO IV_CostHistory("
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.COSTHISTORYSEQ_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.COSTHISTORYSEQ_FLD].Value = objObject.CostHistorySeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.ICDHITEMCOST21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = objObject.ICDHItemCost21;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEDATE_FLD].Value = objObject.ReceiveDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREF_FLD].Value = objObject.ReceiveRef;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREFLINE_FLD, OleDbType.Integer));
				if (objObject.ReceiveRefLine != 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREFLINE_FLD].Value = objObject.ReceiveRefLine;
				}
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREFLINE_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.QASTATUS_FLD, OleDbType.Integer));
				if (objObject.QAStatus != 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = objObject.QAStatus;
				}
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID != 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = objObject.PartyID;
				}
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID != 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				}
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;


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
		///       This method uses to add data to IV_CostHistory
		///    </Description>
		///    <Inputs>
		///        IV_CostHistoryVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       11-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_CostHistoryVO objObject = (IV_CostHistoryVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(string.Empty, oconPCS);

				strSql = "INSERT INTO IV_CostHistory("
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";
				strSql += " ; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.COSTHISTORYSEQ_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.COSTHISTORYSEQ_FLD].Value = objObject.CostHistorySeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.ICDHITEMCOST21_FLD, OleDbType.Decimal));
				if (objObject.ICDHItemCost21 >= 0) 
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = objObject.ICDHItemCost21;
				}
				else
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEDATE_FLD].Value = objObject.ReceiveDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREF_FLD].Value = objObject.ReceiveRef;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREFLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREFLINE_FLD].Value = objObject.ReceiveRefLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = objObject.PartyID;
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.PartyLocationID > 0)
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;
				else
					ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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
		///       This method uses to add data to IV_CostHistory
		///    </Description>
		///    <Inputs>
		///        IV_CostHistoryVO       
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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void AddReturnedGoods(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnedGoods()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				IV_CostHistoryVO objObject = (IV_CostHistoryVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				//get the latest sequence number
				strSql = "SELECT isnull(Max("
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + "),0)"
					+ " FROM " + IV_CostHistoryTable.TABLE_NAME;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				int intCostHistorySeq = int.Parse(ocmdPCS.ExecuteScalar().ToString()) + 1;

				strSql = "INSERT INTO IV_CostHistory("
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.COSTHISTORYSEQ_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.COSTHISTORYSEQ_FLD].Value = intCostHistorySeq; // objObject.CostHistorySeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.ICDHITEMCOST21_FLD, OleDbType.Decimal));
				if (objObject.ICDHItemCost21 >= 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = objObject.ICDHItemCost21;
				}
				else
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEDATE_FLD].Value = objObject.ReceiveDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREF_FLD].Value = objObject.ReceiveRef;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREFLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREFLINE_FLD].Value = objObject.ReceiveRefLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.QASTATUS_FLD, OleDbType.Integer));
				if (objObject.QAStatus > 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = objObject.QAStatus;
				}
				else
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID > 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}
				else
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.STOCKUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				if (objObject.TranTypeID > 0)
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;
				}
				else
				{
					ocmdPCS.Parameters[IV_CostHistoryTable.TRANTYPEID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.CommandText = strSql;
				//ocmdPCS.Connection.Open();
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
		///       This method uses to delete data from IV_CostHistory
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
			strSql = "DELETE " + IV_CostHistoryTable.TABLE_NAME + " WHERE  " + "CostHistoryID" + "=" + pintID.ToString();
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
		///       This method uses to get data from IV_CostHistory
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_CostHistoryVO
		///    </Outputs>
		///    <Returns>
		///       IV_CostHistoryVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 23, 2005
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
					+ IV_CostHistoryTable.COSTHISTORYID_FLD + ","
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD
					+ " FROM " + IV_CostHistoryTable.TABLE_NAME
					+ " WHERE " + IV_CostHistoryTable.COSTHISTORYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_CostHistoryVO objObject = new IV_CostHistoryVO();

				while (odrPCS.Read())
				{
					objObject.CostHistoryID = int.Parse(odrPCS[IV_CostHistoryTable.COSTHISTORYID_FLD].ToString().Trim());
					objObject.CostHistorySeq = int.Parse(odrPCS[IV_CostHistoryTable.COSTHISTORYSEQ_FLD].ToString().Trim());
					objObject.ICDHItemCost21 = Decimal.Parse(odrPCS[IV_CostHistoryTable.ICDHITEMCOST21_FLD].ToString().Trim());
					objObject.ReceiveDate = DateTime.Parse(odrPCS[IV_CostHistoryTable.RECEIVEDATE_FLD].ToString().Trim());
					objObject.ReceiveRef = int.Parse(odrPCS[IV_CostHistoryTable.RECEIVEREF_FLD].ToString().Trim());
					objObject.ReceiveRefLine = int.Parse(odrPCS[IV_CostHistoryTable.RECEIVEREFLINE_FLD].ToString().Trim());
					objObject.QAStatus = int.Parse(odrPCS[IV_CostHistoryTable.QASTATUS_FLD].ToString().Trim());
					objObject.PartyID = int.Parse(odrPCS[IV_CostHistoryTable.PARTYID_FLD].ToString().Trim());
					objObject.PartyLocationID = int.Parse(odrPCS[IV_CostHistoryTable.PARTYLOCATIONID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_CostHistoryTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_CostHistoryTable.PRODUCTID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[IV_CostHistoryTable.CCNID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[IV_CostHistoryTable.STOCKUMID_FLD].ToString().Trim());
					objObject.TranTypeID = int.Parse(odrPCS[IV_CostHistoryTable.TRANTYPEID_FLD].ToString().Trim());

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
		///       This method uses to update data to IV_CostHistory
		///    </Description>
		///    <Inputs>
		///       IV_CostHistoryVO       
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

			IV_CostHistoryVO objObject = (IV_CostHistoryVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_CostHistory SET "
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.PARTYID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.CCNID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + "=   ?" + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD + "=  ?"
					+ " WHERE " + IV_CostHistoryTable.COSTHISTORYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.COSTHISTORYSEQ_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.COSTHISTORYSEQ_FLD].Value = objObject.CostHistorySeq;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.ICDHITEMCOST21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_CostHistoryTable.ICDHITEMCOST21_FLD].Value = objObject.ICDHItemCost21;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEDATE_FLD].Value = objObject.ReceiveDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREF_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREF_FLD].Value = objObject.ReceiveRef;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.RECEIVEREFLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.RECEIVEREFLINE_FLD].Value = objObject.ReceiveRefLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PARTYLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PARTYLOCATIONID_FLD].Value = objObject.PartyLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.TRANTYPEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.TRANTYPEID_FLD].Value = objObject.TranTypeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_CostHistoryTable.COSTHISTORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_CostHistoryTable.COSTHISTORYID_FLD].Value = objObject.CostHistoryID;


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
		///       This method uses to get all data from IV_CostHistory
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
		///       Wednesday, February 23, 2005
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
					+ IV_CostHistoryTable.COSTHISTORYID_FLD + ","
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD
					+ " FROM " + IV_CostHistoryTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_CostHistoryTable.TABLE_NAME);

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
		///       Wednesday, February 23, 2005
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
					+ IV_CostHistoryTable.COSTHISTORYID_FLD + ","
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + ","
					+ IV_CostHistoryTable.ICDHITEMCOST21_FLD + ","
					+ IV_CostHistoryTable.RECEIVEDATE_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREF_FLD + ","
					+ IV_CostHistoryTable.RECEIVEREFLINE_FLD + ","
					+ IV_CostHistoryTable.QASTATUS_FLD + ","
					+ IV_CostHistoryTable.PARTYID_FLD + ","
					+ IV_CostHistoryTable.PARTYLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.MASTERLOCATIONID_FLD + ","
					+ IV_CostHistoryTable.PRODUCTID_FLD + ","
					+ IV_CostHistoryTable.CCNID_FLD + ","
					+ IV_CostHistoryTable.STOCKUMID_FLD + ","
					+ IV_CostHistoryTable.TRANTYPEID_FLD
					+ "  FROM " + IV_CostHistoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_CostHistoryTable.TABLE_NAME);

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
		///       This method uses to get max sequence of CostHistory
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///       int
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       10-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxSequence()
		{
			const string METHOD_NAME = THIS + ".GetMaxSequence()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ISNULL(MAX("
					+ IV_CostHistoryTable.COSTHISTORYSEQ_FLD + "), 0)"
					+ " FROM " + IV_CostHistoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return (int.Parse(objResult.ToString()));
				else
					return 0;
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