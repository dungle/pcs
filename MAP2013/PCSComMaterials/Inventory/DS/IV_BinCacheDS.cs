using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComMaterials.Inventory.DS
{
	public class IV_BinCacheDS 
	{
		private const string THIS = "PCSComMaterials.Inventory.DS.IV_BinCacheDS";
		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID)
		{
			const string METHOD_NAME = THIS + ".HasProductID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ "      AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ "      AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn == null) 
				{
					return false;
				}
				else
				{
					if (int.Parse(objReturn.ToString()) > 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
				}
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

		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".HasProductID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ "      AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ "      AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;
				
				//if(pstrLot.Length != 0)// modified by Tuan TQ, 04 Jan, 2006.

				if(pstrLot != null && pstrLot.Length != 0)
				{
					strSql += " AND " + IV_BinCacheTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'";
				}

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if (objReturn == null) 
				{
					return false;
				}
				else
				{
					if (int.Parse(objReturn.ToString()) > 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
				}
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
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_BinCache("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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
	
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + IV_BinCacheTable.TABLE_NAME + "("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?)";
				strSql += " ; SELECT @@IDENTITY AS NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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

		public void AddReturnedGoods(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnedGoods()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_BinCache("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD + ")"
					+ " VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + IV_BinCacheTable.TABLE_NAME + " WHERE  " + "BinCacheID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
				strSql=	"SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD  
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINCACHEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_BinCacheVO objObject = new IV_BinCacheVO();

				while (odrPCS.Read())
				{ 
					objObject.BinCacheID = int.Parse(odrPCS[IV_BinCacheTable.BINCACHEID_FLD].ToString().Trim());
					objObject.OHQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.OHQUANTITY_FLD].ToString().Trim());
					objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_BinCacheTable.LOT_FLD].ToString().Trim();
					objObject.InspStatus = int.Parse(odrPCS[IV_BinCacheTable.INSPSTATUS_FLD].ToString().Trim());
					objObject.BinID = int.Parse(odrPCS[IV_BinCacheTable.BINID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[IV_BinCacheTable.CCNID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[IV_BinCacheTable.LOCATIONID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_BinCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_BinCacheTable.PRODUCTID_FLD].ToString().Trim());

				}		
				return objObject;					
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

		public object GetObjectVO(int pintMasLocID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_BinCacheVO objObject = new IV_BinCacheVO();

				while (odrPCS.Read())
				{
					if (odrPCS[IV_BinCacheTable.BINCACHEID_FLD] != DBNull.Value)
						objObject.BinCacheID = int.Parse(odrPCS[IV_BinCacheTable.BINCACHEID_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.OHQUANTITY_FLD] != DBNull.Value)
						objObject.OHQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.OHQUANTITY_FLD].ToString().Trim());
					else
						objObject.OHQuantity = 0;
					if (odrPCS[IV_BinCacheTable.COMMITQUANTITY_FLD] != DBNull.Value)
						objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					else
						objObject.CommitQuantity = 0;
					if (odrPCS[IV_BinCacheTable.DEMANQUANTITY_FLD] != DBNull.Value)
						objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					else
						objObject.DemanQuantity = 0;
					if (odrPCS[IV_BinCacheTable.SUPPLYQUANTITY_FLD] != DBNull.Value)
						objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_BinCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					else
						objObject.SupplyQuantity = 0;
					objObject.Lot = odrPCS[IV_BinCacheTable.LOT_FLD].ToString().Trim();
					if (odrPCS[IV_BinCacheTable.INSPSTATUS_FLD] != DBNull.Value)
						objObject.InspStatus = int.Parse(odrPCS[IV_BinCacheTable.INSPSTATUS_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.BINID_FLD] != DBNull.Value)
						objObject.BinID = int.Parse(odrPCS[IV_BinCacheTable.BINID_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[IV_BinCacheTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[IV_BinCacheTable.LOCATIONID_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[IV_BinCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[IV_BinCacheTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[IV_BinCacheTable.PRODUCTID_FLD].ToString().Trim());
				}
				return objObject;
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

		public decimal GetOnhandQty(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".GetOnhandQty()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjecVO;

			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_BinCacheTable.OHQUANTITY_FLD 
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND "   + IV_BinCacheTable.CCNID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.LOCATIONID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.PRODUCTID_FLD + "=   ?" ;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;


				ocmdPCS.Connection.Open();
				//odrPCS = ocmdPCS.ExecuteReader();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == null)
				{
					return 0;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
				}
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
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_BinCache SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "=   ?" + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + "=   ?" + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + "=   ?" + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + "=   ?" + ","
					+ IV_BinCacheTable.LOT_FLD + "=   ?" + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + "=   ?" + ","
					+ IV_BinCacheTable.BINID_FLD + "=   ?" + ","
					+ IV_BinCacheTable.CCNID_FLD + "=   ?" + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + "=   ?" + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ IV_BinCacheTable.PRODUCTID_FLD + "=   ?" 
					+ " WHERE " + IV_BinCacheTable.BINCACHEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINCACHEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINCACHEID_FLD].Value = objObject.BinCacheID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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

		public void UpdateReturnedGoods(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateReturnedGoods()";

			IV_BinCacheVO objObject = (IV_BinCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_BinCache SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "= " + IV_BinCacheTable.OHQUANTITY_FLD + " +   ?" 
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND "   + IV_BinCacheTable.CCNID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.LOCATIONID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=   ?" 
					+ " AND "   + IV_BinCacheTable.PRODUCTID_FLD + "=   ?" ;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
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

		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_BinCacheTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BinCacheTable.TABLE_NAME);

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

		/// <summary>
		/// GetAvailableQtyAndInsStatusByProduct
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasLocID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns>DataSet</returns>
		public DataSet GetAvailableQtyAndInsStatusByProduct(int pintCCNID, int pintMasLocID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQtyAndInsStatusByProduct()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)" + " AS AvailQuantity, " 
					+ IV_BinCacheTable.INSPSTATUS_FLD
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " GROUP BY " + IV_LocationCacheTable.INSPSTATUS_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_BinCacheTable.TABLE_NAME);

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
		
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD 
					+ "  FROM " + IV_BinCacheTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_BinCacheTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			

			catch(InvalidOperationException ex)
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
		
		public void UpdateDataSetForTaking(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetForTaking()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD 
					+ "  FROM " + IV_BinCacheTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_BinCacheTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
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
		public decimal GetQuantityOnHand(int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				decimal decQuantity = 0;
				try
				{
					decQuantity = decimal.Parse(ocmdPCS.ExecuteScalar().ToString());
				}
				catch{}
				return decQuantity;
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

		public decimal GetQuantityOnHand(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				decimal decQuantity = 0;
				try
				{
					decQuantity = decimal.Parse(ocmdPCS.ExecuteScalar().ToString());
				}
				catch{}
				return decQuantity;
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
		/// <summary>
		/// GetQuantityOnHand
		/// </summary>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, August 14 2006</date>
		public decimal GetQuantityOnHand(int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				decimal decQuantity = 0;
				try
				{
					decQuantity = decimal.Parse(ocmdPCS.ExecuteScalar().ToString());
				}
				catch{}
				return decQuantity;
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

		public void UpdateCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID,int pintProductID,decimal pdecCommit)
		{
			const string METHOD_NAME = THIS + ".UpdateCommitQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + "=   ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.COMMITQUANTITY_FLD].Value = pdecCommit;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			

			catch(InvalidOperationException ex)
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

		public void UpdateOnHandQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID, int pintProductID,decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateOnHandQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "=   ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			
			catch(InvalidOperationException ex)
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

		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecCommitQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateCommitQuantityForSO()";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}

			catch(InvalidOperationException ex)
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
		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, string pstrLot, decimal pdecCommitQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateCommitQuantityForSO()";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOT_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}

			catch(InvalidOperationException ex)
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
		public decimal CheckCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - ISNULL(SUM(ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " - " + pdecCommitQuantity
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.MinusOne;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		/// <summary>
		/// GetCommitQtyByPostDate
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
		public decimal GetCommitQtyByPostDate(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.Zero;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		/// <summary>
		/// GetCommitQtyByPostDate
		/// </summary>
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
		public decimal GetCommitQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID,int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQtyByPostDate()";
			const string PARAMETTER_1 = THIS + "param1";
			const string PARAMETTER_2 = THIS + "param2";
			const string PARAMETTER_3 = THIS + "param3";
			const string PARAMETTER_4 = THIS + "param4";
			const string PARAMETTER_5 = THIS + "param5";
			const string PARAMETTER_6 = THIS + "param6";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					+ " AND TT.Code = 'SOCommitment' AND TH.PostDate > ?"
					//+ pdtmPostDate
					//+ " AND TH.PostDate < ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ " ),0) + "  
					
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Code = 'SOCancelCommitment' AND TH.PostDate > ?" //+ pdtmPostDate 
					//+ " AND TH.PostDate < ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ " ),0) + "  

					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Code = 'ShippingManagement' AND TH.PostDate > ?" 
					//+ pdtmPostDate 
					//+ " AND TH.PostDate < ?" 
					//+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ "),0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + " = " + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + " = " + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + " = " + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + " = " + pintBinID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_1, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_1].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_2, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_2].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_3, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_3].Value = pdtmPostDate;
				
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_4, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_4].Value = (new UtilsDS()).GetDBDate();
//				
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_5, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_5].Value = pdtmPostDate;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_6, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_6].Value = (new UtilsDS()).GetDBDate();
				
				//ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.Zero;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		/// <summary>
		/// GetOHQtyByPostDate
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		public decimal GetOHQtyByPostDate(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOHQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.Zero;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		public DataTable GetOHQtyByPostDate(int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOHQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME
					+ " WHERE 1=1";
				if (pintMasterLocationID > 0)
						strSql += " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID;
				if (pintLocationID > 0)
					strSql += " AND " + IV_BinCacheTable.LOCATIONID_FLD + " = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND " + IV_BinCacheTable.BINID_FLD + " = " + pintBinID;
				if (pintProductID > 0)
					strSql += " AND " + IV_BinCacheTable.PRODUCTID_FLD + " = " + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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
		/// <summary>
		/// GetOHQtyByPostDate
		/// </summary>
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
		public decimal GetOHQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID,int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOHQtyByPostDate()";
			const string PARAMETTER_1 = THIS + "param1";
			const string PARAMETTER_2 = THIS + "param2";
			const string PARAMETTER_3 = THIS + "param3";
			const string PARAMETTER_4 = THIS + "param4";
			const string PARAMETTER_5 = THIS + "param5";
			const string PARAMETTER_6 = THIS + "param6";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					+ " AND TT.Type = " + (int) TransactionHistoryType.In
					+ " AND TH.PostDate > ?"
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ " ),0)"  
					
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					+ " AND TT.Type = " + (int) TransactionHistoryType.Both
					+ " AND TH.PostDate > ?"
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ " ),0) + "  

					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Type = " + (int) TransactionHistoryType.Out
					+ " AND TH.PostDate > ?" 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " AND TH.BinID = " + pintBinID.ToString()
					+ "),0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + " = " + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + " = " + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + " = " + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + " = " + pintBinID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_1, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_1].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_2, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_2].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_3, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_3].Value = pdtmPostDate;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.Zero;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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

		public DataTable GetOHQtyByPostDate(DateTime pdtmPostDate, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOHQtyByPostDate()";
			const string PARAMETTER_1 = THIS + "param1";
			const string PARAMETTER_2 = THIS + "param2";
			const string PARAMETTER_3 = THIS + "param3";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				string strSql=	"SELECT LocationID, BinID, ProductID, ISNULL(SUM(ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Type = " + (int) TransactionHistoryType.In
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?";
				if (pintMasterLocationID > 0)
					strSql += " AND TH.MasterLocationID = " + pintMasterLocationID;
				if (pintLocationID > 0)
					strSql += " AND TH.LocationID = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND TH.BinID = " + pintBinID;
				if (pintProductID > 0)
					strSql += " AND ProductID = " + pintProductID;
				strSql += " ),0)"  
					
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Type = " + (int) TransactionHistoryType.Both
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?";
				if (pintMasterLocationID > 0)
					strSql += " AND TH.MasterLocationID = " + pintMasterLocationID;
				if (pintLocationID > 0)
					strSql += " AND TH.LocationID = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND TH.BinID = " + pintBinID;
				if (pintProductID > 0)
					strSql += " AND ProductID = " + pintProductID;
				strSql += " ),0)"  

					+ " + ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Type = " + (int) TransactionHistoryType.Out
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?";
				if (pintMasterLocationID > 0)
					strSql += " AND TH.MasterLocationID = " + pintMasterLocationID;
				if (pintLocationID > 0)
					strSql += " AND TH.LocationID = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND TH.BinID = " + pintBinID;
				if (pintProductID > 0)
					strSql += " AND ProductID = " + pintProductID;
				strSql += " ),0) " + IV_BinCacheTable.OHQUANTITY_FLD
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE 1=1";
				if (pintMasterLocationID > 0)
					strSql += " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + " = " + pintMasterLocationID;
				if (pintLocationID > 0)
					strSql += " AND " + IV_BinCacheTable.LOCATIONID_FLD + " = " + pintLocationID;
				if (pintBinID > 0)
					strSql += " AND " + IV_BinCacheTable.BINID_FLD + " = " + pintBinID;
				if (pintProductID > 0)
					strSql += " AND " + IV_BinCacheTable.PRODUCTID_FLD + " = " + pintBinID;
				strSql += " GROUP BY LocationID, BinID, ProductID";
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_1, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_1].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_2, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_2].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_3, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_3].Value = pdtmPostDate;
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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
		public decimal CheckCommitQuantityByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, string pstrLot, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0) - " + pdecCommitQuantity
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.LOT_FLD + "='" + pstrLot + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.MinusOne;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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

		public decimal GetAvailableQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.MinusOne;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		
		public DataTable GetAvailableQuantity(int pintBinID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(" + IV_BinCacheTable.OHQUANTITY_FLD + " - ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ",0),0)"
					+ Constants.AVAILABLE_QTY_COL + ", " + IV_BinCacheTable.PRODUCTID_FLD
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " GROUP BY " + IV_BinCacheTable.PRODUCTID_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
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
		public int GetQAStatus(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQAStatus()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT isnull("
					+ IV_BinCacheTable.INSPSTATUS_FLD + ",0) " 
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return 0;
				}
				else
				{
					return int.Parse(objReturn.ToString());
				}
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

		public decimal GetAvailableQuantityByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityByLot()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_BinCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.LOT_FLD + "='" + pstrLot + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.MinusOne;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		public void UpdateSubtractCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, decimal pdecCommitQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateSubtractCommitQuantity()";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + "= "
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + " - " + pdecCommitQuantity
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}

			catch(InvalidOperationException ex)
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
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID, int pintProductID,decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateOnHandQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "=" + IV_BinCacheTable.OHQUANTITY_FLD + "- ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			
			catch(InvalidOperationException ex)
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
		/// <summary>
		/// Add more quantity to On-hand quantity of Item in Bin
		/// </summary>
		/// <param name="pdecQuantity">Quantity to add</param>
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID, int pintProductID,decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateOnHandQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "= ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0) + ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			
			catch(InvalidOperationException ex)
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

		/// <summary>
		/// Get On-hand quantity of product in Bin and by Lot
		/// </summary>
		/// <returns>On-hand Quantity</returns>
		public decimal GetQuantityOnHandByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHandByLot()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "-" + IV_BinCacheTable.COMMITQUANTITY_FLD
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.LOT_FLD + "='" + pstrLot + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				if ((objReturn == null) || (objReturn == DBNull.Value))
				{
					return decimal.MinusOne;
				}
				else
				{
					return decimal.Parse(objReturn.ToString());
				}
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
		/// <summary>
		/// Remove Item from Bin when On-hand Quantity of Item is empty
		/// </summary>
		public void RemoveItem(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
		}
		/// <summary>
		/// Remove Lot controlled Item from Bin when On-hand Quantity of Item is empty
		/// </summary>
		public void RemoveItemByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID, string pstrLot)
		{
		}
		/// <summary>
		/// Update subtract on-hand quantity by Lot
		/// </summary>
		/// <param name="pdecQuantity">Quantity to subtract</param>
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID, int pintProductID, string pstrLot, decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateSubtractOHQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{

				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "=" + IV_BinCacheTable.OHQUANTITY_FLD + "- ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			
			catch(InvalidOperationException ex)
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
		/// <summary>
		/// Add more quantity to On-hand quantity of Lot controlled Item in Bin
		/// </summary>
		/// <param name="pdecQuantity">Quantity to add</param>
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintBinID, int pintProductID, string pstrLot, decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateOnHandQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_BinCacheTable.TABLE_NAME + " SET "
					+ IV_BinCacheTable.OHQUANTITY_FLD + "= ISNULL(" + IV_BinCacheTable.OHQUANTITY_FLD + ", 0) + ?"
					+ " WHERE " + IV_BinCacheTable.BINID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_BinCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_BinCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.BINID_FLD].Value = pintBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.LOCATIONID_FLD].Value = pintLocID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_BinCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_BinCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_BinCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			
			catch(InvalidOperationException ex)
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
		/// <summary>
		/// Get Commit Quantity of Product in a Bin
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pintBinID">Bin</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				try
				{
					return decimal.Parse(objReturn.ToString());
				}
				catch
				{
					return decimal.Zero;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		/// <summary>
		/// Get Commit Quantity of Product in a Bin by Lot
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pintBinID">Bin</param>
		/// <param name="pstrLot">Lot</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_BinCacheTable.TABLE_NAME 
					+ " WHERE " + IV_BinCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_BinCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_BinCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_BinCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintBinID
					+ " AND " + IV_BinCacheTable.LOT_FLD + "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar)).Value = pstrLot;
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();
				try
				{
					return decimal.Parse(objReturn.ToString());
				}
				catch
				{
					return decimal.Zero;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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

		/// <summary>
		/// Determine that Product is existed in Bin yet
		/// </summary>
		/// <param name="pintBinID">Bin to check</param>
		/// <param name="pintProductID">Product to check</param>
		/// <returns>true if exist, false if not exist</returns>
		public bool IsHasProductInBin(int pintBinID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".IsHasProductInBin()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) FROM IV_BinCache"
					+ " WHERE BinID = " + pintBinID
					+ " AND ProductID = " + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				int intCount = 0;
				try
				{
					intCount = Convert.ToInt32(ocmdPCS.ExecuteScalar());
				}
				catch{}
				return intCount > 0;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		public DataTable GetDSCacheQuantity(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetDSCacheQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT SUM(ISNULL(OHQuantity, 0)) AS Quantity"
					+ ", ProductID FROM IV_BinCache"
					+ " JOIN MST_BIN ON IV_BinCache.BinID = MST_Bin.BinID"
					+ " WHERE IV_BinCache.CCNID = " + pintCCNID
					+ " AND IV_BinCache.MasterLocationID = " + SystemProperty.MasterLocationID
					+ " AND MST_Bin.BinTypeID = " + (int)BinTypeEnum.LS
					+ " GROUP BY ProductID";
				
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
		public DataTable ListAllCache()
		{
			const string METHOD_NAME = THIS + ".ListAllCache()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT B.BinCacheID, B.CCNID, B.MasterLocationID, B.LocationID, B.BinID, B.ProductID, StockUMID,"
					+ " OHQuantity, CommitQuantity"
					+ " FROM IV_BinCache B JOIN ITM_Product P ON B.ProductID = P.ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(IV_BinCacheTable.TABLE_NAME);
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

		public DataSet ListAllBinCache()
		{
			const string METHOD_NAME = THIS + ".ListAllBinCache()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ IV_BinCacheTable.BINCACHEID_FLD + ","
					+ IV_BinCacheTable.OHQUANTITY_FLD + ","
					+ IV_BinCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_BinCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_BinCacheTable.LOT_FLD + ","
					+ IV_BinCacheTable.INSPSTATUS_FLD + ","
					+ IV_BinCacheTable.BINID_FLD + ","
					+ IV_BinCacheTable.CCNID_FLD + ","
					+ IV_BinCacheTable.LOCATIONID_FLD + ","
					+ IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_BinCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_BinCacheTable.TABLE_NAME;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstData = new DataSet();
				odadPCS.Fill(dstData, IV_BinCacheTable.TABLE_NAME);
				return dstData;
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

        public DataTable ListAllBinCache(List<string> binIdList)
        {
            const string METHOD_NAME = THIS + ".ListAllBinCache()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                var strSql = "SELECT "
                    + IV_BinCacheTable.BINCACHEID_FLD + ","
                    + IV_BinCacheTable.OHQUANTITY_FLD + ","
                    + IV_BinCacheTable.COMMITQUANTITY_FLD + ","
                    + IV_BinCacheTable.DEMANQUANTITY_FLD + ","
                    + IV_BinCacheTable.SUPPLYQUANTITY_FLD + ","
                    + IV_BinCacheTable.LOT_FLD + ","
                    + IV_BinCacheTable.INSPSTATUS_FLD + ","
                    + IV_BinCacheTable.BINID_FLD + ","
                    + IV_BinCacheTable.CCNID_FLD + ","
                    + IV_BinCacheTable.LOCATIONID_FLD + ","
                    + IV_BinCacheTable.MASTERLOCATIONID_FLD + ","
                    + IV_BinCacheTable.PRODUCTID_FLD
                    + " FROM " + IV_BinCacheTable.TABLE_NAME
                    + " WHERE " + IV_BinCacheTable.BINID_FLD + " IN (" + string.Join(",", binIdList.ToArray()) + ")";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                var table = new DataTable();
                odadPCS.Fill(table);
                return table;
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

		public DataTable GetAvailableQtyByPostDate(DateTime pdtmPostDate)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("GetAvailableQtyByPostDate", oconPCS);
				ocmdPCS.CommandType = CommandType.StoredProcedure;
				ocmdPCS.Parameters.Add(new OleDbParameter("P1", OleDbType.Date)).Value = pdtmPostDate;

				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(IV_BinCacheTable.TABLE_NAME);
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
		public DataTable GetAvailableQtyByPostDate(DateTime pdtmPostDate, string pstrProductIDs)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = " SELECT A.MasterLocationID, A.LocationID, A.BinID, A.ProductID,"
					+ " ISNULL(A.OHQuantity,0) - ISNULL(A.CommitQuantity,0) AS AVAILABLEQUANTITY"
					+ " FROM "
					+ " ((SELECT IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID,"
					+ " SUM(IV_BinCache.OHQuantity)"
					+ " - ISNULL((SELECT SUM(Quantity) AS OHQuantity"
					+ " FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Type IN (1,2)"
					+ " AND TH.MasterLocationID = IV_BinCache.MasterLocationID"
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?"
					+ " ),0)"
					+ " + ISNULL((SELECT SUM(Quantity) AS OHQuantity"
					+ " FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Type = 0"
					+ " AND TH.MasterLocationID = IV_BinCache.MasterLocationID"
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?"
					+ " ),0) AS OHQuantity, 0 AS CommitQuantity"
					+ " FROM IV_BinCache"
					+ " GROUP BY IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID)"
					+ " "
					+ " UNION ALL"
					+ " "
					+ " (SELECT IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID,"
					+ " 0 AS OHQuantity, SUM(ISNULL(IV_BinCache.CommitQuantity,0))"
					+ " - ISNULL((SELECT SUM(Quantity) AS OHQuantity"
					+ " FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Code = 'SOCommitment'"
					+ " AND TH.MasterLocationID = IV_BinCache.MasterLocationID"
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?"
					+ " ),0)"
					+ " + ISNULL((SELECT SUM(Quantity) AS OHQuantity"
					+ " FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Code = 'SOCancelCommitment'"
					+ " AND TH.MasterLocationID = IV_BinCache.MasterLocationID"
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?"
					+ " ),0)"
					+ " + ISNULL((SELECT SUM(Quantity) AS OHQuantity"
					+ " FROM MST_TransactionHistory TH JOIN MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE TT.Code = 'ShippingManagement'"
					+ " AND TH.MasterLocationID = IV_BinCache.MasterLocationID"
					+ " AND TH.LocationID = IV_BinCache.LocationID"
					+ " AND TH.BinID = IV_BinCache.BinID"
					+ " AND TH.ProductID = IV_BinCache.ProductID"
					+ " AND TH.PostDate > ?"
					+ " ),0) AS CommitQuantity"
					+ " FROM IV_BinCache"
					+ " GROUP BY IV_BinCache.MasterLocationID, IV_BinCache.LocationID, IV_BinCache.BinID, IV_BinCache.ProductID) ) A"
					+ " WHERE ISNULL(A.OHQuantity,0) - ISNULL(A.CommitQuantity,0) <> 0"
					+ " AND A.ProductID IN (" + pstrProductIDs + ")";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("P1", OleDbType.Date)).Value = pdtmPostDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("P2", OleDbType.Date)).Value = pdtmPostDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("P3", OleDbType.Date)).Value = pdtmPostDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("P4", OleDbType.Date)).Value = pdtmPostDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("P5", OleDbType.Date)).Value = pdtmPostDate;

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(IV_BinCacheTable.TABLE_NAME);
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

        public DataTable GetCacheData(int binId)
        {
            const string METHOD_NAME = THIS + ".GetCacheData()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var strSql = "SELECT [BinCacheID],[OHQuantity],[CommitQuantity],[DemanQuantity],[SupplyQuantity]"
                         + " ,[Lot],[InspStatus],[BinID],[CCNID],[LocationID]"
                         + " ,[MasterLocationID],[ProductID]"
                         + " FROM " + IV_BinCacheTable.TABLE_NAME
                         + " WHERE " + IV_BinCacheTable.BINID_FLD + "=" + binId;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var dtbData = new DataTable();
                var odadPCS = new OleDbDataAdapter(ocmdPCS);
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
