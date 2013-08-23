using System;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.DS
{
	
	public class SO_SaleOrderDetailDS 
	{
		public SO_SaleOrderDetailDS()
		{
		}

		private const string THIS = "PCSComSale.Order.DS.SO_SaleOrderDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_SaleOrderDetail
		///    </Description>
		///    <Inputs>
		///        SO_SaleOrderDetailVO       
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
		///       Tuesday, February 01, 2005
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
				SO_SaleOrderDetailVO objObject = (SO_SaleOrderDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_SaleOrderDetail("
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Value = objObject.SaleOrderLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.AUTOCOMMIT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.AUTOCOMMIT_FLD].Value = objObject.AutoCommit;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].Value = objObject.ExportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].Value = objObject.SpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].Value = objObject.DiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.NETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.NETAMOUNT_FLD].Value = objObject.NetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].Value = objObject.ItemCustomerCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD].Value = objObject.ItemCustomerRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.CANCELREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.CANCELREASONID_FLD].Value = objObject.CancelReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.VATPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.VATPERCENT_FLD].Value = objObject.VATPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].Value = objObject.ExportTaxPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].Value = objObject.SpecialTaxPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SHIPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SHIPQUANTITY_FLD].Value = objObject.ShipQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.BACKORDERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.BACKORDERQTY_FLD].Value = objObject.BackOrderQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.STOCKQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.STOCKQUANTITY_FLD].Value = objObject.StockQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD].Value = objObject.ConvertedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.REASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.REASONID_FLD].Value = objObject.ReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;


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
		///       This method uses to delete data from SO_SaleOrderDetail
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
			strSql = "DELETE " + SO_SaleOrderDetailTable.TABLE_NAME + " WHERE  " + "SaleOrderDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from SO_SaleOrderDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_SaleOrderDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_SaleOrderDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
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
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_SaleOrderDetailVO objObject = new SO_SaleOrderDetailVO();

				while (odrPCS.Read())
				{
					if (odrPCS[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
					{
						objObject.SaleOrderDetailID = int.Parse(odrPCS[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SALEORDERLINE_FLD] != DBNull.Value)
					{
						objObject.SaleOrderLine = int.Parse(odrPCS[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.AUTOCOMMIT_FLD] != DBNull.Value)
					{
						objObject.AutoCommit = bool.Parse(odrPCS[SO_SaleOrderDetailTable.AUTOCOMMIT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] != DBNull.Value)
					{
						objObject.OrderQuantity = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.UNITPRICE_FLD] != DBNull.Value)
					{
						objObject.UnitPrice = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.UNITPRICE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.VATAMOUNT_FLD] != DBNull.Value)
					{
						objObject.VATAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.VATAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD] != DBNull.Value)
					{
						objObject.ExportTaxAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD] != DBNull.Value)
					{
						objObject.SpecialTaxAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] != DBNull.Value)
					{
						objObject.TotalAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD] != DBNull.Value)
					{
						objObject.DiscountAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.NETAMOUNT_FLD] != DBNull.Value)
					{
						objObject.NetAmount = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.NETAMOUNT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] != DBNull.Value)
					{
						objObject.ItemCustomerCode = odrPCS[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].ToString().Trim();
					}
					if (odrPCS[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] != DBNull.Value)
					{
						objObject.ItemCustomerRevision = odrPCS[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD].ToString().Trim();
					}
					if (odrPCS[SO_SaleOrderDetailTable.CANCELREASONID_FLD] != DBNull.Value)
					{
						objObject.CancelReasonID = int.Parse(odrPCS[SO_SaleOrderDetailTable.CANCELREASONID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.VATPERCENT_FLD] != DBNull.Value)
					{
						objObject.VATPercent = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.VATPERCENT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD] != DBNull.Value)
					{
						objObject.ExportTaxPercent = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD] != DBNull.Value)
					{
						objObject.SpecialTaxPercent = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.UMRATE_FLD] != DBNull.Value)
					{
						objObject.UMRate = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.UMRATE_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SHIPQUANTITY_FLD] != DBNull.Value)
					{
						objObject.ShipQuantity = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.SHIPQUANTITY_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.BACKORDERQTY_FLD] != DBNull.Value)
					{
						objObject.BackOrderQty = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.BACKORDERQTY_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.STOCKQUANTITY_FLD] != DBNull.Value)
					{
						objObject.StockQuantity = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.STOCKQUANTITY_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.PRODUCTID_FLD] != DBNull.Value)
					{
						objObject.ProductID = int.Parse(odrPCS[SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD] != DBNull.Value)
					{
						objObject.ConvertedQuantity = Decimal.Parse(odrPCS[SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.REASONID_FLD] != DBNull.Value)
					{
						objObject.ReasonID = int.Parse(odrPCS[SO_SaleOrderDetailTable.REASONID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] != DBNull.Value)
					{
						objObject.SaleOrderMasterID = int.Parse(odrPCS[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.STOCKUMID_FLD] != DBNull.Value)
					{
						objObject.StockUMID = int.Parse(odrPCS[SO_SaleOrderDetailTable.STOCKUMID_FLD].ToString().Trim());
					}
					if (odrPCS[SO_SaleOrderDetailTable.SELLINGUMID_FLD] != DBNull.Value)
					{
						objObject.SellingUMID = int.Parse(odrPCS[SO_SaleOrderDetailTable.SELLINGUMID_FLD].ToString().Trim());
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
		///       This method uses to update data to SO_SaleOrderDetail
		///    </Description>
		///    <Inputs>
		///       SO_SaleOrderDetailVO       
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

			SO_SaleOrderDetailVO objObject = (SO_SaleOrderDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_SaleOrderDetail SET "
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + "=   ?" + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD + "=  ?"
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERLINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].Value = objObject.SaleOrderLine;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.AUTOCOMMIT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.AUTOCOMMIT_FLD].Value = objObject.AutoCommit;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.VATAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.VATAMOUNT_FLD].Value = objObject.VATAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD].Value = objObject.ExportTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD].Value = objObject.SpecialTaxAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.TOTALAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD].Value = objObject.TotalAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD].Value = objObject.DiscountAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.NETAMOUNT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.NETAMOUNT_FLD].Value = objObject.NetAmount;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].Value = objObject.ItemCustomerCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD].Value = objObject.ItemCustomerRevision;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.CANCELREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.CANCELREASONID_FLD].Value = objObject.CancelReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.VATPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.VATPERCENT_FLD].Value = objObject.VATPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD].Value = objObject.ExportTaxPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD].Value = objObject.SpecialTaxPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SHIPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SHIPQUANTITY_FLD].Value = objObject.ShipQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.BACKORDERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.BACKORDERQTY_FLD].Value = objObject.BackOrderQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.STOCKQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.STOCKQUANTITY_FLD].Value = objObject.StockQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD].Value = objObject.ConvertedQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.REASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.REASONID_FLD].Value = objObject.ReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;


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
		///       This method uses to get all data from SO_SaleOrderDetail with total commit detail
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetSaleOrderTotalCommit(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = " SELECT * FROM v_GetSaleOrderTotalInvCommit " +
					" WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_SaleOrderDetail with total commit detail
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetAvgCommitCost(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = " SELECT * FROM v_GetAvgCommitCost " +
					" WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_SaleOrderDetail
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
		///       Tuesday, February 01, 2005
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
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		/// <summary>
		/// GetSODetailDeliverySchedule
		/// </summary>
		/// <param name="pintSOMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, April 28 2006</date>
		public DataSet GetSODetailDeliverySchedule(int pintSOMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = " select D.SaleOrderDetailID, ISNUll(sum(DL.deliveryquantity), 0) TotalDelivery "
						+ " from SO_DeliverySchedule DL " 
						+ " inner join SO_SaleOrderDetail D on D.SaleOrderDetailID = DL.SaleOrderDetailID "
						+ " where D.SaleOrderDetailID in "
						+ " (Select SaleOrderDetailID from SO_DeliverySchedule)  "
						+ " and D.SaleOrderMasterID = " + pintSOMasterID.ToString()
						+ " group by D.SaleOrderDetailID ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_SaleOrderDetail
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
                    + " CA." + ITM_CategoryTable.CODE_FLD + " " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " MST_UnitOfMeasureCode,"
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_CancelReasonTable.TABLE_NAME + "." + SO_CancelReasonTable.CODE_FLD + " SO_CancelReasonCode,"
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ "(SELECT Count (*) FROM " + SO_DeliveryScheduleTable.TABLE_NAME + " DS " 
					+ " WHERE DS." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD 
					+ ") AS " + SO_DeliveryScheduleTable.DELIVERYNO_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME

					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + " = "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					
					+ " LEFT JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + " = "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD

					+ " LEFT JOIN " + SO_CancelReasonTable.TABLE_NAME
					+ " ON " + SO_CancelReasonTable.TABLE_NAME + "." + SO_CancelReasonTable.CANCELREASONID_FLD + " = "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.CANCELREASONID_FLD
					
					+ " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME 
					+ " CA ON CA" + "." + ITM_CategoryTable.CATEGORYID_FLD + " = "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD	

					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID.ToString()
					+ " ORDER BY " + SO_SaleOrderDetailTable.SALEORDERLINE_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_SaleOrderDetail by SOCode
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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(string pstrSaleOrderCode)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME + " SODetail INNER JOIN " + SO_SaleOrderMasterTable.TABLE_NAME + " SOMaster "
					+ " ON " + "SODetail." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + " = SOMaster." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " WHERE SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " = '" + pstrSaleOrderCode.ToString() + "'";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       Tuesday, February 01, 2005
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
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ "  FROM " + SO_SaleOrderDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_SaleOrderDetailTable.TABLE_NAME);
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

		public DataTable Search(int pintCCNID, DateTime pdtmFromDeliveryDate, DateTime pdtmToDeliveryDate,
			string pstrOrderNo, string pstrItem, string pstrRevision, string pstrDescription, int pintLocationID, int pintBinID)
		{
			const string METHOD_NAME = THIS + ".Search()";
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm";

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DS.DeliveryScheduleID, SOD.SaleOrderDetailID,SOM.SaleOrderMasterID,SOD.ProductID,SOD.SellingUMID,SOM.CCNID, \n"
					+ " SOM.ShipFromLocID,P.MasterLocationID, P.LocationID ITM_ProductLocationID, ISNULL(P.BinID,0) ITM_ProductBinID, PL.Code ITM_ProductLocationIDCode,PB.Code ITM_ProductBinIDCode, \n"
					+ " SOD.StockUMID, SOD.UMRate, \n"
					+ " SOM.CODE, SOD.SaleOrderLine,DS.Line,  DS.ScheduleDate, P.Code Item, P.Description, P.Revision, UM.Code UMCode, 0 RELEASEVALUE \n"
					+ " , ML.Code MasterLocation, 0 Bin, " + pintLocationID + " LocationID,"
					+ " (SELECT Code FROM MST_Location WHERE LocationID = " + pintLocationID + ") MST_LocationCode,"
					+ pintBinID + " BinID, (SELECT Code FROM MST_Bin WHERE BinID = " + pintBinID + ") MST_BinCode,"
					+ " ISNULL(DS.DeliveryQuantity,0) DeliveryQuantity, Cache.AVAILABLEQUANTITY,"
					+ " CIR.CustomerItemCode CusCode, CIR.CustomerItemModel CusName, SOM.Priority,"
					+ " '' RELEASE, SO_Gate.Code as SO_GateCode \n"
					+ " FROM SO_DeliverySchedule DS \n"
					+ " INNER JOIN SO_SaleOrderDetail SOD on SOD.Saleorderdetailid = DS.SaleOrderDetailID \n"
					+ " INNER JOIN SO_SaleOrderMaster SOM on SOM.Saleordermasterid = SOD.SaleOrderMasterID AND SOM.CCNID = " + pintCCNID.ToString() + " \n"
					+ " JOIN (SELECT ISNULL(SUM(OHQuantity - ISNULL(CommitQuantity,0)),0) AVAILABLEQUANTITY, ProductID"
					+ " FROM IV_BinCache WHERE BinID = " + pintBinID
					+ " GROUP BY ProductID) Cache ON SOD.ProductID = Cache.ProductID"
					+ " LEFT JOIN SO_CustomerItemRefDetail CIR ON CIR.ProductID = SOD.ProductID "
					+ " AND (SELECT PartyID FROM dbo.SO_CustomerItemRefMaster"
					+ " WHERE CustomerItemRefMasterID = CIR.CustomerItemRefMasterID) = SOM.PartyID "
					+ " INNER JOIN ITM_Product P ON SOD.ProductID = P.ProductID  \n"
					+ " INNER JOIN MST_UnitOfMeasure AS UM ON UM.UNITOFMEASUREID = SOD.SELLINGUMID  \n"
					+ " Left JOIN MST_Location AS PL ON P.LocationID = PL.LocationID  \n"
					+ " Left JOIN MST_Bin AS PB ON P.BinID = PB.BinID  \n"
					+ " LEFT JOIN SO_Gate ON SO_Gate.GateID = DS.GateID"
					+ " INNER JOIN MST_MasterLocation ML ON SOM.ShipFromLocID = ML.MasterLocationID"
					+ " AND DS.DeliveryScheduleID NOT IN (SELECT DeliveryScheduleID FROM SO_CommitInventoryDetail)";
					
				// TRANSFROM
				if (pdtmFromDeliveryDate > DateTime.MinValue)
				{
					strSql += " AND DATEDIFF(minute, DS.ScheduleDate, '" + pdtmFromDeliveryDate.ToString(SQL_DATETIME_FORMAT) + "') <= 0  \n";
				}
				// TRANSTO
				if (pdtmToDeliveryDate > DateTime.MinValue)
				{
					strSql += " AND DATEDIFF(minute, DS.ScheduleDate,'" + pdtmToDeliveryDate.ToString(SQL_DATETIME_FORMAT) + "') >= 0  \n";
				}
				// ORDERNO
				if (pstrOrderNo.Length > 0)
				{
					pstrOrderNo = pstrOrderNo.Replace("'", "''");
					strSql += " AND (SOM.Code = '" + pstrOrderNo + "') \n";
				}
				// Code
				if (pstrItem.Length > 0)
				{
					pstrItem = pstrItem.Replace("'", "''");
					strSql += " AND (P.Code = '" + pstrItem + "') \n";
				}
				// Revision
				if (pstrRevision.Length > 0)
				{
					pstrRevision = pstrRevision.Replace("'", "''");
					strSql += " AND (P.Revision = '" + pstrRevision + "') \n";
				}
				// Description
				if (pstrDescription.Length > 0)
				{
					pstrDescription = pstrDescription.Replace("'", "''");
					strSql += " AND (P.Description = '" + pstrDescription + "') \n";
				}
				//strSql += " ORDER BY SOM.CODE, SOD.SALEORDERLINE, DS.LINE \n";
				strSql += " ORDER BY DS.ScheduleDate, Item, P.Revision \n";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
		///       This method uses to Search SO Release 
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Monday, February 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetUMRate(int pintStockUMID, int pintSellingUMID)
		{
			const string METHOD_NAME = THIS + ".GetUMRate()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_UMRateTable.SCALE_FLD
					+ " FROM " + MST_UMRateTable.TABLE_NAME
					+ " WHERE " + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintStockUMID
					+ " AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintSellingUMID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				decimal decScale = 0;

				while (odrPCS.Read())
				{
					decScale = Decimal.Parse(odrPCS[MST_UMRateTable.SCALE_FLD].ToString().Trim());
				}
				return decScale;
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
		///       This method uses to get TotalDelivery from PO_PurchaseOrderDetail
		///    </Description>
		///    <Inputs>
		///       PODetailID
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
		///       29-Mar-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetOrderQuantity(int pintSODetail)
		{
			const string METHOD_NAME = THIS + ".GetOrderQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "SELECT ISNULL("
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ", 0)"
					+ " FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].Value = pintSODetail;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn != null)
				{
					return decimal.Parse(objReturn.ToString());
				}
				else
				{
					return 0;
				}
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

		/// <summary>
		/// Check the shipment of Item on the SOLine
		/// </summary>
		/// <param name="pintSODetail"></param>
		/// <returns></returns>
		/// <author>
		/// Do Manh Tuan
		/// 07-07-2005
		/// </author>
		public bool IsRemainQuantityNotShip(int pintSODetail)
		{
			const string METHOD_NAME = THIS + ".GetOrderQuantity()";
			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "SELECT (Select Sum(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ") FROM " +
					SO_CommitInventoryDetailTable.TABLE_NAME + " CMDetail inner join " + SO_DeliveryScheduleTable.TABLE_NAME + " SODel on CMDetail." 
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + " = SODel." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ " inner join " + SO_SaleOrderDetailTable.TABLE_NAME  + " SODetail on SODetail." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " = SODel." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " Where SODetail." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = " + pintSODetail
					+ " ) - (Select sum(" + SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ") FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " Where SaleOrderDetailID = " + pintSODetail + ")";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn != null) && (objReturn != DBNull.Value))
				{
					if (decimal.Parse(objReturn.ToString()) != 0)
					{
						return false;
					}
				}
				else
				{
					return false;
				}
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
			return true;
		}

		#region // HACK: DuongNA 2005-10-21
		//**************************************************************************              
		///    <Description>
		///       This method uses to Search SO Release 
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Nguyen An Duong
		///    </Authors>
		///    <History>
		///       Friday, October 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetUMRateForProduct(int pintProductID, int pintSellingUMID)
		{
			const string METHOD_NAME = THIS + ".GetUMRateForProduct()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ MST_UMRateTable.SCALE_FLD
					+ " FROM " + MST_UMRateTable.TABLE_NAME
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + ITM_ProductTable.STOCKUMID_FLD
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintSellingUMID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				decimal decScale = 0;

				while (odrPCS.Read())
				{
					decScale = Decimal.Parse(odrPCS[MST_UMRateTable.SCALE_FLD].ToString().Trim());
				}
				return decScale;
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

		#endregion // END: DuongNA 2005-10-21

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
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForImport(DataSet pData, int intMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ "  FROM " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + intMasterID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_SaleOrderDetailTable.TABLE_NAME);
				pData.Tables[SO_SaleOrderDetailTable.TABLE_NAME].Clear();
				odadPCS.Fill(pData.Tables[SO_SaleOrderDetailTable.TABLE_NAME]);
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
		
		public decimal GetUnitPriceDefualt(int pProductID, int pCustomerID)
		{
			const string METHOD_NAME = THIS + ".GetOrderQuantity()";
			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "SELECT CASE ("
						+ "	SELECT Count(*) FROM SO_CustomerItemRefDetail CIR"
						+ "	Inner Join SO_CustomerItemRefMaster CIM ON CIR.CustomerItemRefMasterID = CIM.CustomerItemRefMasterID"
						+ "	AND ProductID = " + pProductID + " AND PartyID = " + pCustomerID + ") WHEN 0 THEN "
						+ "		(Select ISNULL(ListPrice,0) From Itm_Product Where ProductID = " + pProductID + ")"
						+ " ELSE"
						+ "		(SELECT UnitPrice FROM SO_CustomerItemRefDetail CIR"
						+ "		Inner Join SO_CustomerItemRefMaster CIM ON CIR.CustomerItemRefMasterID = CIM.CustomerItemRefMasterID"
						+ "		AND ProductID = " + pProductID + " AND PartyID = " + pCustomerID + ")"
						+ " END ";

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn != null) && (objReturn != DBNull.Value))
				{
					return Convert.ToDecimal(objReturn);
				}
				else
				{
					return 0;
				}
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

		///    <Description>
		///       This method uses to get list item detail with UnitPrice change
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
		///       CanhNv
		///    </Authors>
		///    <History>
		///       18/04/2007
		///    </History>
		///    <Notes>
		///    </Notes>
		public DataSet GetListDetail(int printSaleOrderMasterId,int printPartyID)
		{
			const string METHOD_NAME = THIS + ".GetListDetail()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				//Todo strSql
				strSql = "SELECT CIRD.UnitPrice UnitPriceCustomerIRD, "
					+ "SOD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.AUTOCOMMIT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.VATAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.EXPORTTAXAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SPECIALTAXAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.TOTALAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.DISCOUNTAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.NETAMOUNT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.CANCELREASONID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.VATPERCENT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.EXPORTTAXPERCENT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SPECIALTAXPERCENT_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.UMRATE_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SHIPQUANTITY_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.BACKORDERQTY_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.STOCKQUANTITY_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.CONVERTEDQUANTITY_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.REASONID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
					+ "SOD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD
						+ " FROM SO_SaleOrderDetail SOD"
						+ " INNER JOIN SO_CustomerItemRefDetail CIRD ON SOD.Productid = CIRD.ProductID" 
						+ " WHERE CIRD.CustomerItemRefMasterID = " +  printPartyID.ToString()
						+ " AND CIRD.Unitprice <> 0" 
						+ " AND SOD.SaleOrdermasterid = " + printSaleOrderMasterId.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

				odadPCS.Fill(dstPCS, SO_SaleOrderDetailTable.TABLE_NAME);

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
	}
}