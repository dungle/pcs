using System;
using System.Data;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComSale.Order.DS
{
	
	public class SO_ReturnedGoodsDetailDS 
	{
		public SO_ReturnedGoodsDetailDS()
		{
		}

		private const string THIS = "PCSComSale.Order.DS.SO_ReturnedGoodsDetailDS";


		//**************************************************************************              
		///    <Description>
		///       Get the total received quantity
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
		///       
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetTotalReceivedQuantity(int pintSaleOrderMasterID, int pintProductID, string pstrLot, string pstrSerial)
		{
			const string METHOD_NAME = THIS + ".GetTotalReceivedQuantity()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			const string VIEW_NAME = "v_TotalReturnedGoods";
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD
					+ " FROM " + VIEW_NAME
					+ " WHERE " + SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID
					+ "    AND " + SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + "=" + pintProductID;

				if (pstrLot != String.Empty)
				{
					strSql += " AND " + SO_ReturnedGoodsDetailTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'";
				}

				if (pstrSerial != String.Empty)
				{
					strSql += " AND " + SO_ReturnedGoodsDetailTable.SERIAL_FLD + "='" + pstrSerial.Replace("'", "''") + "'";
				}

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == null || objResult == DBNull.Value)
				{
					return 0;
				}
				else
				{
					if (objResult.ToString() == String.Empty)
					{
						return 0;
					}
					else
					{
						return decimal.Parse(objResult.ToString());
					}
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
		///       This method uses to get max Line number in the Detail 
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
		///       
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxReturnedGoodsDetailLine(int pintReturnedGoodsMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMaxReturnedGoodsDetailLine()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT max ("
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ")"
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=" + pintReturnedGoodsMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				string strResult = ocmdPCS.ExecuteScalar().ToString();
				int intResult;
				try
				{
					intResult = int.Parse(strResult);
				}
				catch
				{
					intResult = 0;
				}
				return intResult;
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
		///       This method uses to add data to SO_ReturnedGoodsDetail
		///    </Description>
		///    <Inputs>
		///        SO_ReturnedGoodsDetailVO       
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
		///       Tuesday, February 22, 2005
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
				SO_ReturnedGoodsDetailVO objObject = (SO_ReturnedGoodsDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_ReturnedGoodsDetail("
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].Value = objObject.ReceiveQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.UNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.UNITID_FLD].Value = objObject.UnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD].Value = objObject.ReturnedGoodsMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LOT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.SERIAL_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.SERIAL_FLD].Value = objObject.Serial;


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
		///       This method uses to delete data from SO_ReturnedGoodsDetail
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
			strSql = "DELETE " + SO_ReturnedGoodsDetailTable.TABLE_NAME + " WHERE  " + "ReturnedGoodsDetailID" + "=" + pintID.ToString();
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
		///       This method uses to delete data from SO_ReturnedGoodsDetail
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
		public void DeleteAllReturnedGoodsDetail(int pintReturnedGoodsMasterID)
		{
			const string METHOD_NAME = THIS + ".DeleteAllReturnedGoodsDetail()";
			string strSql = String.Empty;
			strSql = "DELETE " + SO_ReturnedGoodsDetailTable.TABLE_NAME + " WHERE  " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=" + pintReturnedGoodsMasterID.ToString();
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
		///       This method uses to get data from SO_ReturnedGoodsDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_ReturnedGoodsDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_ReturnedGoodsDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 22, 2005
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
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " WHERE " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_ReturnedGoodsDetailVO objObject = new SO_ReturnedGoodsDetailVO();

				while (odrPCS.Read())
				{
					objObject.ReturnedGoodsDetailID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD].ToString().Trim());
					objObject.ReceiveQuantity = Decimal.Parse(odrPCS[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
					objObject.UnitID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim());
					objObject.UnitPrice = Decimal.Parse(odrPCS[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].ToString().Trim());
					objObject.ReturnedGoodsMasterID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD].ToString().Trim());
					objObject.BinID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.QAStatus = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ToString().Trim());
					objObject.Lot = odrPCS[SO_ReturnedGoodsDetailTable.LOT_FLD].ToString().Trim();
					objObject.Line = int.Parse(odrPCS[SO_ReturnedGoodsDetailTable.LINE_FLD].ToString().Trim());
					objObject.Serial = odrPCS[SO_ReturnedGoodsDetailTable.SERIAL_FLD].ToString().Trim();


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
		///       This method uses to update data to SO_ReturnedGoodsDetail
		///    </Description>
		///    <Inputs>
		///       SO_ReturnedGoodsDetailVO       
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

			SO_ReturnedGoodsDetailVO objObject = (SO_ReturnedGoodsDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_ReturnedGoodsDetail SET "
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + "=   ?" + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD + "=  ?"
					+ " WHERE " + SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].Value = objObject.ReceiveQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.UNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.UNITID_FLD].Value = objObject.UnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.UNITPRICE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.UNITPRICE_FLD].Value = objObject.UnitPrice;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD].Value = objObject.ReturnedGoodsMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LOT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.SERIAL_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD].Value = objObject.ReturnedGoodsDetailID;


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
		///       This method uses to get all data from SO_ReturnedGoodsDetail
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
		///       Monday, February 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListReturnedGoodsDetail(int pintReturnedGoodsID)
		{
			const string METHOD_NAME = THIS + ".ListReturnedGoodsDetail()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				//+ "'' BalanceQty,"
				strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ "COM." + SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ "(SELECT " + MST_UnitOfMeasureTable.CODE_FLD + " FROM " + MST_UnitOfMeasureTable.TABLE_NAME + " WHERE " + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + "=" + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.UNITID_FLD + ") as MST_UnitOfMeasureCode,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					// edited by dungla, fix bug for NgaHT
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " as MST_MasterLocationCode ,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " as MST_LocationCode ,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " as MST_BinCode ,"
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.BIN_FLD + " as MST_LocationBin,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.QUANTITYOFSELLING_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " Inner join " + ITM_ProductTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " left join " + MST_MasterLocationTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + "=" + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " left join " + MST_LocationTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + "=" + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " left join " + MST_BINTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BINID_FLD + "=" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					+ " left join " + SO_ConfirmShipMasterTable.TABLE_NAME + " COM ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD + " = COM." + SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD
					+ " WHERE " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=" + pintReturnedGoodsID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ReturnedGoodsDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from SO_ReturnedGoodsDetail
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
		///       Tuesday, February 22, 2005
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
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_ReturnedGoodsDetailTable.TABLE_NAME);

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
		///       Tuesday, February 22, 2005
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
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ "  FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_ReturnedGoodsDetailTable.TABLE_NAME);

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
		///       Tuesday, February 22, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetReturnedGoodsDetail(DataSet pData, int pintReturnedGoodsMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetReturnedGoodsDetail()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			try
			{
				strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.SALEORDERDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.QUANTITYOFSELLING_FLD + ","
					+ SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ "  FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_ReturnedGoodsDetailTable.TABLE_NAME);

				//Reload data from database 
				strSql = "SELECT "
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LINE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD + ","
					+ "COM." + SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.UNITID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BALANCEQTY_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.UNITPRICE_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + ","
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " as MasterLocationCode ,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + ","
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " as LocationCode ,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BINID_FLD + ","
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " as BinCode ,"
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.QASTATUS_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOT_FLD + ","
					+ SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.SERIAL_FLD
					+ " FROM " + SO_ReturnedGoodsDetailTable.TABLE_NAME
					+ " Inner join " + ITM_ProductTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " left join " + MST_MasterLocationTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD + "=" + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " left join " + MST_LocationTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.LOCATIONID_FLD + "=" + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " left join " + MST_BINTable.TABLE_NAME + " ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.BINID_FLD + "=" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					+ " left join " + SO_ConfirmShipMasterTable.TABLE_NAME + " COM ON " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.CONFIRMSHIPMASTERID_FLD + "= COM." + SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD
					+ " WHERE " + SO_ReturnedGoodsDetailTable.TABLE_NAME + "." + SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD + "=" + pintReturnedGoodsMasterID;
				pData.Clear();
				odadPCS.SelectCommand.CommandText = strSql;
				odadPCS.SelectCommand.CommandType = CommandType.Text;
				odadPCS.Fill(pData, SO_ReturnedGoodsDetailTable.TABLE_NAME);
				pData.AcceptChanges();
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
				else if (ex.Errors[1].NativeError == ErrorCode.SQLDBNULL_VIALATION_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DBNULL_VIALATION, METHOD_NAME, ex);
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

	}
}