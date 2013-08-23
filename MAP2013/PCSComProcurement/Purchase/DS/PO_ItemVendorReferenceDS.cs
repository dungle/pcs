using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.DS
{
	public class PO_ItemVendorReferenceDS 
	{
		public PO_ItemVendorReferenceDS()
		{
		}

		private const string THIS = "PCSComProcurement.Purchase.DS.PO_ItemVendorReferenceDS";


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///        PO_ItemVendorReferenceVO       
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
		///       Monday, April 04, 2005
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
				PO_ItemVendorReferenceVO objObject = (PO_ItemVendorReferenceVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + PO_ItemVendorReferenceTable.TABLE_NAME + "("
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEM_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEM_FLD].Value = objObject.VendorItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD].Value = objObject.VendorItemRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD].Value = objObject.VendorItemDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD].Value = objObject.MinOrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD].Value = objObject.FixedLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD].Value = objObject.VarianceLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRICE_FLD].Value = objObject.Price;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CARRIERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CARRIERID_FLD].Value = objObject.CarrierID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.BUYINGUM_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.BUYINGUM_FLD].Value = objObject.BuyingUM;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].Value = objObject.CapacityPeriod;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QASTATUS_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QASTATUS_FLD].Value = objObject.QAStatus;


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
		///       This method uses to add data to PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///        PO_ItemVendorReferenceVO       
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
		///       April 05, 2005
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
				PO_ItemVendorReferenceVO objObject = (PO_ItemVendorReferenceVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO " + PO_ItemVendorReferenceTable.TABLE_NAME + "("
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
				strSql += "; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEM_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEM_FLD].Value = objObject.VendorItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD].Value = objObject.VendorItemRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD].Value = objObject.VendorItemDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD].Value = objObject.MinOrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD].Value = objObject.FixedLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD].Value = objObject.VarianceLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRICE_FLD].Value = objObject.Price;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CARRIERID_FLD, OleDbType.Integer));
				if (objObject.CarrierID > 0)
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.BUYINGUM_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.BUYINGUM_FLD].Value = objObject.BuyingUM;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD, OleDbType.VarWChar));
				if (objObject.CapacityPeriod != string.Empty)
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].Value = objObject.CapacityPeriod;
				else
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QASTATUS_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
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
		///       This method uses to delete data from PO_ItemVendorReference
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
			strSql = "DELETE " + PO_ItemVendorReferenceTable.TABLE_NAME + " WHERE  " + PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + "=" + pintID.ToString();
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
		///       This method uses to delete data from PO_ItemVendorReference
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
		public void DeleteItemVendor(int pintPartyID,int pintProductID)
		{
			const string METHOD_NAME = THIS + ".DeleteItemVendor()";
			string strSql = String.Empty;
			strSql = "DELETE " + PO_ItemVendorReferenceTable.TABLE_NAME 
					+ " WHERE  " + PO_ItemVendorReferenceTable.PARTYID_FLD + "=" + pintPartyID.ToString()
					+ "    AND " + PO_ItemVendorReferenceTable.PRODUCTID_FLD + "=" + pintProductID.ToString();
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
		///       This method uses to get data from PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_ItemVendorReferenceVO
		///    </Outputs>
		///    <Returns>
		///       PO_ItemVendorReferenceVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, April 04, 2005
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
					+ PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + ","
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD
					+ " FROM " + PO_ItemVendorReferenceTable.TABLE_NAME
					+ " WHERE " + PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ItemVendorReferenceVO objObject = new PO_ItemVendorReferenceVO();

				while (odrPCS.Read())
				{
					objObject.ItemVendorReferenceID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CCNID_FLD].ToString().Trim());
					objObject.VendorItem = odrPCS[PO_ItemVendorReferenceTable.VENDORITEM_FLD].ToString().Trim();
					objObject.VendorItemRevision = odrPCS[PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD].ToString().Trim();
					objObject.VendorItemDescription = odrPCS[PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD] != DBNull.Value)
						objObject.MinOrderQuantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD] != DBNull.Value)
						objObject.FixedLeadTime = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD] != DBNull.Value)
						objObject.VarianceLeadTime = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PRICE_FLD] != DBNull.Value)
						objObject.Price = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.PRICE_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.QUANTITY_FLD] != DBNull.Value)
						objObject.Quantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.QUANTITY_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CARRIERID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CURRENCYID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.PARTYID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.VENDORLOCID_FLD] != DBNull.Value)
						objObject.VendorLocID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.VENDORLOCID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.BUYINGUM_FLD] != DBNull.Value)
						objObject.BuyingUM = int.Parse(odrPCS[PO_ItemVendorReferenceTable.BUYINGUM_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.ENDDATE_FLD] != DBNull.Value)
						objObject.EndDate = DateTime.Parse(odrPCS[PO_ItemVendorReferenceTable.ENDDATE_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CAPACITY_FLD] != DBNull.Value)
						objObject.Capacity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.CAPACITY_FLD].ToString().Trim());
					objObject.CapacityPeriod = odrPCS[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].ToString().Trim();
					if (odrPCS[PO_ItemVendorReferenceTable.QASTATUS_FLD] != DBNull.Value)
						objObject.QAStatus = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.QASTATUS_FLD].ToString().Trim());

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
		///       This method uses to get data from PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///        PartyID, ProductID
		///    </Inputs>
		///    <Outputs>
		///       PO_ItemVendorReferenceVO
		///    </Outputs>
		///    <Returns>
		///       PO_ItemVendorReferenceVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       April 05, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintPartyID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + ","
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD
					+ " FROM " + PO_ItemVendorReferenceTable.TABLE_NAME
					+ " WHERE " + PO_ItemVendorReferenceTable.PARTYID_FLD + "=" + pintPartyID
					+ " AND " + PO_ItemVendorReferenceTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ItemVendorReferenceVO objObject = new PO_ItemVendorReferenceVO();

				while (odrPCS.Read())
				{
					if (odrPCS[PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD] != DBNull.Value)
						objObject.ItemVendorReferenceID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CCNID_FLD].ToString().Trim());
					objObject.VendorItem = odrPCS[PO_ItemVendorReferenceTable.VENDORITEM_FLD].ToString().Trim();
					objObject.VendorItemRevision = odrPCS[PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD].ToString().Trim();
					objObject.VendorItemDescription = odrPCS[PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD] != DBNull.Value)
						objObject.MinOrderQuantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD] != DBNull.Value)
						objObject.FixedLeadTime = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD] != DBNull.Value)
						objObject.VarianceLeadTime = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PRICE_FLD] != DBNull.Value)
						objObject.Price = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.PRICE_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.QUANTITY_FLD] != DBNull.Value)
						objObject.Quantity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.QUANTITY_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CARRIERID_FLD] != DBNull.Value)
						objObject.CarrierID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CARRIERID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CURRENCYID_FLD] != DBNull.Value)
						objObject.CurrencyID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.CURRENCYID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.PARTYID_FLD] != DBNull.Value)
						objObject.PartyID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.PARTYID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.VENDORLOCID_FLD] != DBNull.Value)
						objObject.VendorLocID = int.Parse(odrPCS[PO_ItemVendorReferenceTable.VENDORLOCID_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.BUYINGUM_FLD] != DBNull.Value)
						objObject.BuyingUM = int.Parse(odrPCS[PO_ItemVendorReferenceTable.BUYINGUM_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.ENDDATE_FLD] != DBNull.Value)
						objObject.EndDate = DateTime.Parse(odrPCS[PO_ItemVendorReferenceTable.ENDDATE_FLD].ToString().Trim());
					if (odrPCS[PO_ItemVendorReferenceTable.CAPACITY_FLD] != DBNull.Value)
						objObject.Capacity = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.CAPACITY_FLD].ToString().Trim());
					objObject.CapacityPeriod = odrPCS[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].ToString().Trim();
					if (odrPCS[PO_ItemVendorReferenceTable.QASTATUS_FLD] != DBNull.Value)
						objObject.QAStatus = Decimal.Parse(odrPCS[PO_ItemVendorReferenceTable.QASTATUS_FLD].ToString().Trim());
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
		///       This method uses to update data to PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///       PO_ItemVendorReferenceVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       05-Apr-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PO_ItemVendorReferenceVO objObject = (PO_ItemVendorReferenceVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + PO_ItemVendorReferenceTable.TABLE_NAME + " SET "
					+ PO_ItemVendorReferenceTable.CCNID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + "=   ?" + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD + "=  ?"
					+ " WHERE " + PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEM_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEM_FLD].Value = objObject.VendorItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD].Value = objObject.VendorItemRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD].Value = objObject.VendorItemDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD].Value = objObject.MinOrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD].Value = objObject.FixedLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD].Value = objObject.VarianceLeadTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRICE_FLD].Value = objObject.Price;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CARRIERID_FLD, OleDbType.Integer));
				if (objObject.CarrierID > 0)
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CARRIERID_FLD].Value = objObject.CarrierID;
				else
					ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CARRIERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CURRENCYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CURRENCYID_FLD].Value = objObject.CurrencyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.VENDORLOCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.VENDORLOCID_FLD].Value = objObject.VendorLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.BUYINGUM_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.BUYINGUM_FLD].Value = objObject.BuyingUM;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.ENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.ENDDATE_FLD].Value = objObject.EndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITY_FLD].Value = objObject.Capacity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD].Value = objObject.CapacityPeriod;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.QASTATUS_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD].Value = objObject.ItemVendorReferenceID;

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
		///       This method uses to get all data from PO_ItemVendorReference
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
		///       Monday, April 04, 2005
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
					+ PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + ","
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD
					+ " FROM " + PO_ItemVendorReferenceTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ItemVendorReferenceTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_ItemVendorReference
		///    </Description>
		///    <Inputs>
		///        VendorID, VendorLocID, CCNID 
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
		///       Monday, April 04, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(int pintVendorID, int pintVendorLocID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ","
					+ PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.QASTATUS_FLD
					+ " FROM " + PO_ItemVendorReferenceTable.TABLE_NAME
					+ " JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.PRODUCTID_FLD 
					+ "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.BUYINGUM_FLD 
					+ "=" + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " WHERE " + PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.CCNID_FLD + "=" + pintCCNID;
				if (pintVendorID > 0)
					strSql += " AND " + PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.PARTYID_FLD + "=" + pintVendorID;
				if (pintVendorLocID > 0)
					strSql += " AND " + PO_ItemVendorReferenceTable.TABLE_NAME + "." + PO_ItemVendorReferenceTable.VENDORLOCID_FLD + "=" + pintVendorLocID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ItemVendorReferenceTable.TABLE_NAME);

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
		///       Monday, April 04, 2005
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
					+ PO_ItemVendorReferenceTable.ITEMVENDORREFERENCEID_FLD + ","
					+ PO_ItemVendorReferenceTable.CCNID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEM_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMREVISION_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORITEMDESCRIPTION_FLD + ","
					+ PO_ItemVendorReferenceTable.MINORDERQUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.FIXEDLEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.VARIANCELEADTIME_FLD + ","
					+ PO_ItemVendorReferenceTable.PRICE_FLD + ","
					+ PO_ItemVendorReferenceTable.QUANTITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CARRIERID_FLD + ","
					+ PO_ItemVendorReferenceTable.CURRENCYID_FLD + ","
					+ PO_ItemVendorReferenceTable.PRODUCTID_FLD + ","
					+ PO_ItemVendorReferenceTable.PARTYID_FLD + ","
					+ PO_ItemVendorReferenceTable.VENDORLOCID_FLD + ","
					+ PO_ItemVendorReferenceTable.BUYINGUM_FLD + ","
					+ PO_ItemVendorReferenceTable.ENDDATE_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITY_FLD + ","
					+ PO_ItemVendorReferenceTable.CAPACITYPERIOD_FLD + ","
					+ PO_ItemVendorReferenceTable.QASTATUS_FLD
					+ "  FROM " + PO_ItemVendorReferenceTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, PO_ItemVendorReferenceTable.TABLE_NAME);

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
		///       This method uses to get all data from from specific table with expression
		///    </Description>
		///    <Inputs>
		///               
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
		///       April 07, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetRows(string pstrTableName, string pstrExpression)
		{
			const string METHOD_NAME = THIS + ".GetRows()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT * "
					+ " FROM " + pstrTableName;
				if ((pstrExpression.Trim() != string.Empty) && (pstrExpression != null))
					strSql += " " + pstrExpression;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrTableName);

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