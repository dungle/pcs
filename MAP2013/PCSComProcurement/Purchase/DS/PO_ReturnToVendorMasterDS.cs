using System;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_ReturnToVendorMasterDS 
	{
		public PO_ReturnToVendorMasterDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.PO_ReturnToVendorMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_ReturnToVendorMaster
		///    </Description>
		///    <Inputs>
		///        PO_ReturnToVendorMasterVO       
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
		///       Wednesday, March 09, 2005
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
				PO_ReturnToVendorMasterVO objObject = (PO_ReturnToVendorMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_ReturnToVendorMaster("
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					//+ PO_ReturnToVendorMasterTable.CCNID_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].Value = objObject.PurchaseLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RTVNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RTVNO_FLD].Value = objObject.RTVNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.CCNID_FLD].Value = objObject.CCNID;

//				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD].Value = objObject.ShipFormLocID;


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
		///       This method uses to add data to PO_ReturnToVendorMaster
		///    </Description>
		///    <Inputs>
		///        PO_ReturnToVendorMasterVO       
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
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddNewReturnToVendor(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddNewReturnToVendor()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				PO_ReturnToVendorMasterVO objObject = (PO_ReturnToVendorMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO PO_ReturnToVendorMaster("
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD + ","
					+ PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.BYINVOICE_FLD + ","
					+ PO_ReturnToVendorMasterTable.BYPO_FLD + ","
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";
				//+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","


				strSql += "; SELECT @@IDENTITY as NewID";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD, OleDbType.Integer));
				if (objObject.PurchaseLocID > 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].Value = objObject.PurchaseLocID;
				}
				else
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.PurchaseOrderMasterID > 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID > 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				}
				else
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
				}

				/*
				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;
				*/

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RTVNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RTVNO_FLD].Value = objObject.RTVNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD, OleDbType.Integer));
				if (objObject.InvoiceMasterID != 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD].Value = objObject.InvoiceMasterID;
				}
				else
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.BYINVOICE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.BYINVOICE_FLD].Value = objObject.ByInvoice;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.BYPO_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.BYPO_FLD].Value = objObject.ByPO;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				}
				else
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PARTYID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD, OleDbType.Integer));
				if (objObject.ProductionLineId > 0)
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD].Value = objObject.ProductionLineId;
				}
				else
				{
					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD].Value = DBNull.Value;
				}

				//ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD, OleDbType.Integer));
//				if (objObject.ShipFormLocID > 0) 
//				{
//					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD].Value = objObject.ShipFormLocID;
//				}
//				else
//				{
//					ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD].Value = DBNull.Value;
//				}

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

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
		///       This method uses to delete data from PO_ReturnToVendorMaster
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
			strSql = "DELETE " + PO_ReturnToVendorMasterTable.TABLE_NAME + " WHERE  " + "PartyID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_ReturnToVendorMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_ReturnToVendorMasterVO
		///    </Outputs>
		///    <Returns>
		///       PO_ReturnToVendorMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, March 09, 2005
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
				string strSql = "SELECT "
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					+ PO_ReturnToVendorMasterTable.BYINVOICE_FLD + ","
					+ PO_ReturnToVendorMasterTable.BYPO_FLD + ","
					+ PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD
					+ " FROM " + PO_ReturnToVendorMasterTable.TABLE_NAME
					+ " WHERE " + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ReturnToVendorMasterVO objObject = new PO_ReturnToVendorMasterVO();

				while (odrPCS.Read())
				{
					objObject.PartyID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PARTYID_FLD].ToString().Trim());
					try
					{
						objObject.PurchaseLocID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.InvoiceMasterID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.INVOICEMASTERID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						objObject.ByInvoice = Convert.ToBoolean(odrPCS[PO_ReturnToVendorMasterTable.BYINVOICE_FLD]);
					}
					catch{}
					try
					{
						objObject.ByPO = Convert.ToBoolean(odrPCS[PO_ReturnToVendorMasterTable.BYPO_FLD]);
					}
					catch{}
					try
					{
						objObject.ProductionLineId = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD].ToString().Trim());
					}
					catch{}
					objObject.MasterLocationID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.ReturnToVendorMasterID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[PO_ReturnToVendorMasterTable.POSTDATE_FLD].ToString().Trim());
					objObject.RTVNo = odrPCS[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.CCNID_FLD].ToString().Trim());
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

		public object GetObjectVORTV(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD
					+ " FROM " + PO_ReturnToVendorMasterTable.TABLE_NAME
					+ " WHERE " + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ReturnToVendorMasterVO objObject = new PO_ReturnToVendorMasterVO();

				while (odrPCS.Read())
				{
					try
					{
						objObject.PartyID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PARTYID_FLD].ToString().Trim());
					}
					catch
					{
						objObject.PartyID = 0;
					}
					objObject.PurchaseLocID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].ToString().Trim());
					try
					{
						objObject.PurchaseOrderMasterID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].ToString().Trim());
					}
					catch
					{
						
					}
					objObject.MasterLocationID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.ReturnToVendorMasterID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].ToString().Trim());
					objObject.PostDate = DateTime.Parse(odrPCS[PO_ReturnToVendorMasterTable.POSTDATE_FLD].ToString().Trim());
					objObject.RTVNo = odrPCS[PO_ReturnToVendorMasterTable.RTVNO_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.CCNID_FLD].ToString().Trim());
					try
					{
						objObject.ProductionLineId = int.Parse(odrPCS[PO_ReturnToVendorMasterTable.PRODUCTIONLINE_FLD].ToString().Trim());
					}
					catch
					{
						
					}

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
		///       This method uses to update data to PO_ReturnToVendorMaster
		///    </Description>
		///    <Inputs>
		///       PO_ReturnToVendorMasterVO       
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

			PO_ReturnToVendorMasterVO objObject = (PO_ReturnToVendorMasterVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE PO_ReturnToVendorMaster SET "
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + "=   ?" + ","
					//+ PO_ReturnToVendorMasterTable.CCNID_FLD + "=   ?" + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD + "=  ?"
					+ " WHERE " + PO_ReturnToVendorMasterTable.PARTYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD].Value = objObject.PurchaseLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD].Value = objObject.PurchaseOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.RTVNO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.RTVNO_FLD].Value = objObject.RTVNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.CCNID_FLD].Value = objObject.CCNID;

//				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD, OleDbType.Integer));
//				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.SHIPFORMLOCID_FLD].Value = objObject.ShipFormLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorMasterTable.PARTYID_FLD].Value = objObject.PartyID;


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
		///       This method uses to get all data from PO_ReturnToVendorMaster
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
		///       Wednesday, March 09, 2005
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
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					//+ PO_ReturnToVendorMasterTable.CCNID_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD
					+ " FROM " + PO_ReturnToVendorMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ReturnToVendorMasterTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_ReturnToVendorMaster
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
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetReturnToVendorMasterInfo(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".GetReturnToVendorMasterInfo()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = " SELECT * FROM v_PO_ReturnToVendorMaster "
					+ " WHERE " + PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + "=" + pintReturnToVendorMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ReturnToVendorMasterTable.TABLE_NAME);

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
		///       Wednesday, March 09, 2005
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
					+ PO_ReturnToVendorMasterTable.PARTYID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASELOCID_FLD + ","
					+ PO_ReturnToVendorMasterTable.PURCHASEORDERMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.MASTERLOCATIONID_FLD + ","
					+ PO_ReturnToVendorMasterTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorMasterTable.POSTDATE_FLD + ","
					+ PO_ReturnToVendorMasterTable.RTVNO_FLD + ","
					//+ PO_ReturnToVendorMasterTable.CCNID_FLD + ","
					+ PO_ReturnToVendorMasterTable.CCNID_FLD
					+ "  FROM " + PO_ReturnToVendorMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_ReturnToVendorMasterTable.TABLE_NAME);

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

		public void DeleteReturnToVendor(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteReturnToVendor()";
			string strSql = String.Empty;
			strSql = "Update MST_TransactionHistory SET TranTypeID=27, InspStatus=12 Where TranTypeID = (Select TranTypeID From MST_TranType Where Code='POReturnToVendor')"
				+ " AND RefMasterID = " + pintReturnToVendorMasterID //+ " AND IssuePurposeID=" + (int)PurposeEnum.ReturnToVendor
				+ "; Delete PO_ReturnToVendorDetail Where ReturnToVendorMasterID = " + pintReturnToVendorMasterID
				+ "; DELETE " + PO_ReturnToVendorMasterTable.TABLE_NAME + " WHERE  " + "ReturnToVendorMasterID" + "=" + pintReturnToVendorMasterID;
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

		public DataTable GetLocationBin(int pintProductionLineID)
		{
			const string METHOD_NAME = THIS + ".ListBomDetailOfProduct()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "Select PL.LocationID, BinID From pro_productionline PL"
					+ " Inner Join MST_Bin B on PL.LocationID=B.LocationID and B.BinTypeID=4"
					+ " Where ProductionLineID = " + pintProductionLineID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PRO_ProductionLineTable.TABLE_NAME);

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

	}
}