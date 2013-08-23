using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_AdditionChargesDS 
	{
		public PO_AdditionChargesDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_AdditionChargesDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_AdditionCharges
		///    </Description>
		///    <Inputs>
		///        PO_AdditionChargesVO       
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
		///       Tuesday, March 01, 2005
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
				PO_AdditionChargesVO objObject = (PO_AdditionChargesVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_AdditionCharges("
					+ PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ PO_AdditionChargesTable.REASONID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.VATAMOUNT_FLD].Value = objObject.VatAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.ADDCHARGEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.ADDCHARGEID_FLD].Value = objObject.AddChargeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.REASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.REASONID_FLD].Value = objObject.ReasonID;


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
		///       This method uses to delete data from PO_AdditionCharges
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
			strSql = "DELETE " + PO_AdditionChargesTable.TABLE_NAME + " WHERE  " + "AdditionChargesID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_AdditionCharges
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_AdditionChargesVO
		///    </Outputs>
		///    <Returns>
		///       PO_AdditionChargesVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
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
					+ PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + ","
					+ PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ PO_AdditionChargesTable.REASONID_FLD
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME
					+ " WHERE " + PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_AdditionChargesVO objObject = new PO_AdditionChargesVO();

				while (odrPCS.Read())
				{
					objObject.AdditionChargesID = int.Parse(odrPCS[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD].ToString());
					objObject.Quantity = Decimal.Parse(odrPCS[PO_AdditionChargesTable.QUANTITY_FLD].ToString());
					objObject.UnitPrice = Decimal.Parse(odrPCS[PO_AdditionChargesTable.UNITPRICE_FLD].ToString());
					objObject.Amount = Decimal.Parse(odrPCS[PO_AdditionChargesTable.AMOUNT_FLD].ToString());
					objObject.VatAmount = Decimal.Parse(odrPCS[PO_AdditionChargesTable.VATAMOUNT_FLD].ToString());
					objObject.TotalAmount = Decimal.Parse(odrPCS[PO_AdditionChargesTable.TOTALAMOUNT_FLD].ToString());
					objObject.PurchaseOrderDetailID = int.Parse(odrPCS[PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].ToString());
					objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD].ToString());
					objObject.AddChargeID = int.Parse(odrPCS[PO_AdditionChargesTable.ADDCHARGEID_FLD].ToString());
					objObject.ReasonID = int.Parse(odrPCS[PO_AdditionChargesTable.REASONID_FLD].ToString());

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
		///       This method uses to update data to PO_AdditionCharges
		///    </Description>
		///    <Inputs>
		///       PO_AdditionChargesVO       
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

			PO_AdditionChargesVO objObject = (PO_AdditionChargesVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_AdditionCharges SET "
					+ PO_AdditionChargesTable.QUANTITY_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.UNITPRICE_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.AMOUNT_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.VATAMOUNT_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.TOTALAMOUNT_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.ADDCHARGEID_FLD + "=   ?" + ","
					+ PO_AdditionChargesTable.REASONID_FLD + "=  ?"
					+ " WHERE " + PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.AMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.AMOUNT_FLD].Value = objObject.Amount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.VATAMOUNT_FLD].Value = objObject.VatAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_AdditionChargesTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD].Value = objObject.PurchaseOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.ADDCHARGEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.ADDCHARGEID_FLD].Value = objObject.AddChargeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.REASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.REASONID_FLD].Value = objObject.ReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_AdditionChargesTable.ADDITIONCHARGESID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD].Value = objObject.AdditionChargesID;


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
		///       This method uses to get all data from PO_AdditionCharges
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
		///       Tuesday, March 01, 2005
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
					+ PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + ","
					+ PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ PO_AdditionChargesTable.REASONID_FLD
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_AdditionChargesTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_AdditionCharges
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
		///       Tuesday, March 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT 0 AS 'ChargeLine', "
					+ PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + ","
					+ PO_PurchaseOrderDetailTable.LINE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ MST_AddChargeTable.TABLE_NAME + "." + MST_AddChargeTable.CODE_FLD + " AS " + MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ MST_ReasonTable.TABLE_NAME + "." + MST_ReasonTable.CODE_FLD + " AS " + MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.REASONID_FLD + ","
					+ PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ pintPOMasterID + " AS " + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME
					+ " JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " ON " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " JOIN " + MST_AddChargeTable.TABLE_NAME
					+ " ON " + MST_AddChargeTable.TABLE_NAME + "." + MST_AddChargeTable.ADDCHARGEID_FLD
					+ " = " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.ADDCHARGEID_FLD
					+ " JOIN " + MST_ReasonTable.TABLE_NAME
					+ " ON " + MST_ReasonTable.TABLE_NAME + "." + MST_ReasonTable.REASONID_FLD
					+ " = " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.REASONID_FLD
					+ " WHERE " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;
					//+ " AND " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_AdditionChargesTable.TABLE_NAME);

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
		///       Tuesday, March 01, 2005
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
					+ PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + ","
					+ PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ PO_AdditionChargesTable.REASONID_FLD
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_AdditionChargesTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to check UM of all products
		///    </Description>
		///    <Inputs>
		///        int POMaterID
		///    </Inputs>
		///    <Outputs>
		///       return true if all of product have same UM
		///       else return false
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       07-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool IsChargeByQuantity(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".IsChargeByQuantity()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT DISTINCT "
					+ PO_PurchaseOrderDetailTable.BUYINGUMID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

				// if data set return more than one row then return false
				if (dstPCS.Tables[SO_SaleOrderDetailTable.TABLE_NAME].Rows.Count > 1)
				{
					return false;
				}
				else
				{
					return true;
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get additional charge by PO Master
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetAdditionalChargeByPOMasterID(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".GetAdditionalChargeByPOMasterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ MST_AddChargeTable.TABLE_NAME + "." + MST_AddChargeTable.CODE_FLD + " AS " + MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD + ","
					//+ MST_ReasonTable.TABLE_NAME + "." + MST_ReasonTable.CODE_FLD + " AS " + MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.AMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.VATAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.TOTALAMOUNT_FLD + ","
					+ PO_AdditionChargesTable.ADDITIONCHARGESID_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.ADDCHARGEID_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.REASONID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + " as " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD  + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + " as " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME
					+ " JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " ON " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD
					+ " = " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " JOIN " + MST_AddChargeTable.TABLE_NAME
					+ " ON " + MST_AddChargeTable.TABLE_NAME + "." + MST_AddChargeTable.ADDCHARGEID_FLD
					+ " = " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.ADDCHARGEID_FLD
//					+ " JOIN " + MST_ReasonTable.TABLE_NAME
//					+ " ON " + MST_ReasonTable.TABLE_NAME + "." + MST_ReasonTable.REASONID_FLD
//					+ " = " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.REASONID_FLD
					+ " WHERE " + PO_AdditionChargesTable.TABLE_NAME + "." + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;

				// create table to hold data
				DataTable tblData = new DataTable(PO_AdditionChargesTable.TABLE_NAME);
				DataColumn dcolLine = new DataColumn("Line", typeof (int));
				dcolLine.AutoIncrement = true;
				dcolLine.AutoIncrementSeed = 1;
				dcolLine.AutoIncrementStep = 1;
				tblData.Columns.Add(dcolLine);
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.QUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.UNITPRICE_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.AMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.VATAMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.TOTALAMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.ADDITIONCHARGESID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.ADDCHARGEID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.REASONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD, typeof(int)));

				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_AdditionChargesTable.QUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_AdditionChargesTable.UNITPRICE_FLD, typeof(decimal)));

				// primary key
//				DataColumn[] dcolKey = new DataColumn[1];
//				dcolKey[0] = tblData.Columns[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD];
//				tblData.PrimaryKey = dcolKey;

				// fill data
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(tblData);
				// add table to dataset
				dstPCS.Tables.Add(tblData);

				// return
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
		///       This method uses to get data from PO detail by PO Master
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetDataByPOMasterID(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDataByPOMasterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD + ","
					+ " 0 AS " + PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ "0 AS " + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.UNITPRICE_FLD + " AS " + PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD + ","
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;
					//+ " AND " + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " > 0";

				// create table to hold data
				DataTable tblData = new DataTable(PO_AdditionChargesTable.TABLE_NAME);
				DataColumn dcolLine = new DataColumn("Line", typeof (int));
				dcolLine.AutoIncrement = true;
				dcolLine.AutoIncrementSeed = 1;
				dcolLine.AutoIncrementStep = 1;
				tblData.Columns.Add(dcolLine);

				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(MST_AddChargeTable.TABLE_NAME + MST_AddChargeTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(MST_ReasonTable.TABLE_NAME + MST_ReasonTable.CODE_FLD, typeof(string)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.QUANTITY_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.UNITPRICE_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.AMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.VATAMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.TOTALAMOUNT_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.ADDITIONCHARGESID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.ADDCHARGEID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.REASONID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.PURCHASEORDERDETAILID_FLD, typeof(int)));
				tblData.Columns.Add(new DataColumn(PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD, typeof(int)));

				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.UNITPRICE_FLD, typeof(decimal)));
				tblData.Columns.Add(new DataColumn(PO_PurchaseOrderDetailTable.TABLE_NAME + PO_AdditionChargesTable.QUANTITY_FLD, typeof(decimal)));

				tblData.Columns[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD].AutoIncrement = true;
				tblData.Columns[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD].AutoIncrementSeed = 1;
				tblData.Columns[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD].AutoIncrementStep = 1;

				// primary key
//				DataColumn[] dcolKey = new DataColumn[1];
//				dcolKey[0] = tblData.Columns[PO_AdditionChargesTable.ADDITIONCHARGESID_FLD];
//				tblData.PrimaryKey = dcolKey;

				// fill data
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				DataTable tblTemp = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(tblTemp);
				foreach (DataRow drowData in tblTemp.Rows)
				{
					DataRow drowNew = tblData.NewRow();
					foreach (DataColumn dcolData in tblTemp.Columns)
					{
						drowNew[dcolData.ColumnName] = drowData[dcolData.ColumnName];
					}
					tblData.Rows.Add(drowNew);
				}
				// add table to dataset
				dstPCS.Tables.Add(tblData);

				// return
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
		///       This method uses to get data for SOLine dropdown
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetPODetailByPOMaster(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".GetPODetailByPOMaster()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT " + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

				return dstPCS.Tables[PO_PurchaseOrderDetailTable.TABLE_NAME];
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
		///       This method uses to check a PO Master was charged or not
		///    </Description>
		///    <Inputs>
		///        Purchase Order Master ID (int)
		///    </Inputs>
		///    <Outputs>
		///       return true if charged
		///       else return false
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       18-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool AlreadyCharged(int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".AlreadyCharged()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ISNULL(COUNT(*), 0) "
					+ " FROM " + PO_AdditionChargesTable.TABLE_NAME
					+ " WHERE " + PO_AdditionChargesTable.PURCHASEORDERMASTERID_FLD + "=" + pintPOMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					if (int.Parse(objResult.ToString()) > 0)
						return true;
					else
						return false;
				}
				else
					return false;
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