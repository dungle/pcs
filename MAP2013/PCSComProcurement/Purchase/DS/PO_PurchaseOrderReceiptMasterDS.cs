using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_PurchaseOrderReceiptMasterDS 
	{
		public PO_PurchaseOrderReceiptMasterDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.DS.PO_PurchaseOrderReceiptMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_PurchaseOrderReceiptMaster
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseOrderReceiptMasterVO       
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
				PO_PurchaseOrderReceiptMasterVO objObject = (PO_PurchaseOrderReceiptMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME + "("
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ")"
					+ " VALUES(?, ?, ?, ?, ?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].Value = objObject.ReceiveNo;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

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
		///       This method uses to add data to PO_PurchaseOrderReceiptMaster
		///    </Description>
		///    <Inputs>
		///        PO_PurchaseOrderReceiptMasterVO       
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
		///       09-Mar-2005
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
				PO_PurchaseOrderReceiptMasterVO objObject = (PO_PurchaseOrderReceiptMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				strSql = "INSERT INTO " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME + "("
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.REFNO_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.INVOICEMASTERID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.USERNAME_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.LASTCHANGE_FLD + ", "
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ")"
					+ " VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
				strSql += " ; Select @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD].Value = objObject.ReceiptType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.REFNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.REFNO_FLD].Value = objObject.RefNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				if(objObject.InvoiceMasterID > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.INVOICEMASTERID_FLD].Value = objObject.InvoiceMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.INVOICEMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD, OleDbType.Integer));
				if (objObject.ProductionLineID > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD].Value = objObject.ProductionLineID;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD].Value = objObject.Purpose;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.USERNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.USERNAME_FLD].Value = objObject.Username;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.LASTCHANGE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.LASTCHANGE_FLD].Value = objObject.LastChange;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].Value = objObject.ReceiveNo;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		///       This method uses to delete data from PO_PurchaseOrderReceiptMaster
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
			strSql = "DELETE " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME + " WHERE  " + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 0)
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
				else
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
		///       This method uses to get data from PO_PurchaseOrderReceiptMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_PurchaseOrderReceiptMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_PurchaseOrderReceiptMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///       DungLa: 11-Mar-2005
		///    </Authors>
		///    <History>
		///       Tuesday, March 01, 2005
		///       11-Mar-2005: Check for null value from DB - DungLa
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
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.REFNO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME
					+ " WHERE " + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_PurchaseOrderReceiptMasterVO objObject = new PO_PurchaseOrderReceiptMasterVO();

				while (odrPCS.Read())
				{
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.CCNID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD] != DBNull.Value)
						objObject.PurchaseOrderReceiptID = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD] != DBNull.Value)
						objObject.PostDate = DateTime.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD].ToString());
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD] != DBNull.Value)
						objObject.ProductionLineID = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.PRODUCTIONLINEID_FLD].ToString());

					objObject.ReceiveNo = odrPCS[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].ToString().Trim();
					objObject.RefNo = odrPCS[PO_PurchaseOrderReceiptMasterTable.REFNO_FLD].ToString().Trim();
					try
					{
						objObject.ReceiptType = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD].ToString().Trim());
					}
					catch{}
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
						objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].ToString());
					
					if (odrPCS[PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD] != DBNull.Value)
						objObject.Purpose = int.Parse(odrPCS[PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD].ToString());

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
		///       This method uses to update data to PO_PurchaseOrderReceiptMaster
		///    </Description>
		///    <Inputs>
		///       PO_PurchaseOrderReceiptMasterVO       
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

			PO_PurchaseOrderReceiptMasterVO objObject = (PO_PurchaseOrderReceiptMasterVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME + " SET "
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + "=  ?"
					+ PO_PurchaseOrderReceiptMasterTable.REFNO_FLD + "=  ?"
					+ PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD + "=  ?"
					+ PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD + "=  ?"
					+ " WHERE " + PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD].Value = objObject.ReceiveNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.REFNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.REFNO_FLD].Value = objObject.RefNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD].Value = objObject.ReceiptType;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD].Value = objObject.PurchaseOrderReceiptID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_PurchaseOrderReceiptMasterTable.PURPOSE_FLD].Value = objObject.Purpose;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		///       This method uses to get all data from PO_PurchaseOrderReceiptMaster
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
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderReceiptMasterTable.TABLE_NAME);

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
					+ PO_PurchaseOrderReceiptMasterTable.CCNID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.POSTDATE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.REFNO_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.RECEIPTTYPE_FLD + ","
					+ PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERMASTERID_FLD
					+ " FROM " + PO_PurchaseOrderReceiptMasterTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_PurchaseOrderReceiptMasterTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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

		public DataTable GetPOReceiptMaster(int pintPOReceiptMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT  PORM.*, PL.Code ProductionLine, POM.PartyID"
						+ " FROM PO_PurchaseOrderReceiptMaster PORM"
						+ " LEFT JOIN PRO_ProductionLine PL ON PORM.ProductionLineID=PL.ProductionLineID"
						+ " LEFT JOIN PO_PurchaseOrderMaster POM ON PORM.PurchaseOrderMasterID=POM.PurchaseOrderMasterID"
						+ " WHERE PORM.PurchaseOrderReceiptID=" + pintPOReceiptMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderReceiptMasterTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		
		public string CheckReturn(PO_PurchaseOrderReceiptMasterVO pvoReceiptMaster, string pstrProductID, bool pblnByInvoice)
		{
			const string METHOD_NAME = THIS + ".CheckReturn()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT M.ReturnToVendorMasterID, RTVNo"
					+ " FROM PO_ReturnToVendorMaster M JOIN PO_ReturnToVendorDetail D"
					+ " ON M.ReturnToVendorMasterID = D.ReturnToVendorMasterID"
					+ " WHERE D.ProductID IN (" + pstrProductID + ")";
				if (pblnByInvoice)
					strSql += " AND M.InvoiceMasterID = " + pvoReceiptMaster.InvoiceMasterID;
				else
					strSql += " AND M.PurchaseOrderMasterID = " + pvoReceiptMaster.PurchaseOrderMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				string strResult = string.Empty;
				if (dtbData.Rows.Count > 0)
				{
					foreach (DataRow drowData in dtbData.Rows)
						strResult += drowData[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString().Trim() + ",";
					strResult = strResult.Substring(0, strResult.Length - 1);
				}
				return strResult;
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
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
				else
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