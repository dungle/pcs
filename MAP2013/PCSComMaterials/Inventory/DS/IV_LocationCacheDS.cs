using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComMaterials.Inventory.DS
{
	
	public class IV_LocationCacheDS 
	{
		public IV_LocationCacheDS()
		{
		}
		private const string THIS = "PCSComMaterials.Inventory.DS.IV_LocationCacheDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_LocationCache
		///    </Description>
		///    <Inputs>
		///        IV_LocationCacheVO       
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


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_LocationCacheVO objObject = (IV_LocationCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_LocationCache("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

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
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_LocationCache
		///    </Description>
		///    <Inputs>
		///        IV_LocationCacheVO       
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
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_LocationCacheVO objObject = (IV_LocationCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_LocationCache("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD + ")"
					+ " VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to check if this product is existed or not
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

		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID, int pintLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
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
		
		//**************************************************************************

		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID, int pintLocationID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
				
				if(pstrLot.Length != 0)
				{
					strSql += "      AND " + IV_LocationCacheTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'";
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from IV_LocationCache
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
			strSql=	"DELETE " + IV_LocationCacheTable.TABLE_NAME + " WHERE  " + "LocationCacheID" + "=" + pintID.ToString();
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from IV_LocationCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_LocationCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_LocationCacheVO
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

		public int GetLocationCacheID(int pintProductID
			,int pintCCNID
			,int pintMasterLocationID
			,int pintLocationID)
		{
			const string METHOD_NAME = THIS + ".GetLocationCacheID()";

			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+" WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+" AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+" AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+" AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				
				if (objResult == null)
				{
					return -1;
				}
				else
				{
					if (objResult.ToString() == String.Empty)
					{
						return -1;
					}
					else
					{
						return int.Parse(objResult.ToString());
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from IV_LocationCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_LocationCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_LocationCacheVO
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
				strSql=	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD + ","
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+" WHERE " + IV_LocationCacheTable.LOCATIONCACHEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_LocationCacheVO objObject = new IV_LocationCacheVO();

				while (odrPCS.Read())
				{ 
					objObject.LocationCacheID = int.Parse(odrPCS[IV_LocationCacheTable.LOCATIONCACHEID_FLD].ToString().Trim());
					objObject.OHQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.OHQUANTITY_FLD].ToString().Trim());
					objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_LocationCacheTable.LOT_FLD].ToString().Trim();
					if(odrPCS[IV_LocationCacheTable.INSPSTATUS_FLD] != DBNull.Value)
						objObject.InspStatus = int.Parse(odrPCS[IV_LocationCacheTable.INSPSTATUS_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[IV_LocationCacheTable.CCNID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_LocationCacheTable.PRODUCTID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[IV_LocationCacheTable.LOCATIONID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_LocationCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from IV_LocationCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_LocationCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_LocationCacheVO
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

		public decimal GetOnhandQty(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".GetOnhandQty()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			IV_LocationCacheVO objObject = (IV_LocationCacheVO) pobjObjecVO;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_LocationCacheTable.OHQUANTITY_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+ " WHERE " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from IV_LocationCache by MasterLocation, Location and Product
		///    </Description>
		///    <Inputs>
		///        MasLocID, LocationID, ProductID
		///    </Inputs>
		///    <Outputs>
		///       IV_LocationCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_LocationCacheVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       24-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintMasLocID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD + ","
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+ " WHERE " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_LocationCacheVO objObject = new IV_LocationCacheVO();

				while (odrPCS.Read())
				{
					if (odrPCS[IV_LocationCacheTable.LOCATIONCACHEID_FLD] != DBNull.Value)
						objObject.LocationCacheID = int.Parse(odrPCS[IV_LocationCacheTable.LOCATIONCACHEID_FLD].ToString().Trim());
					if (odrPCS[IV_LocationCacheTable.OHQUANTITY_FLD] != DBNull.Value)
						objObject.OHQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.OHQUANTITY_FLD].ToString().Trim());
					else
						objObject.OHQuantity = 0;
					if (odrPCS[IV_LocationCacheTable.COMMITQUANTITY_FLD] != DBNull.Value)
						objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					else
						objObject.CommitQuantity = 0;
					if (odrPCS[IV_LocationCacheTable.DEMANQUANTITY_FLD] != DBNull.Value)
						objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					else
						objObject.DemanQuantity = 0;
					if (odrPCS[IV_LocationCacheTable.SUPPLYQUANTITY_FLD] != DBNull.Value)
						objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_LocationCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					else
						objObject.SupplyQuantity = 0;
					objObject.Lot = odrPCS[IV_LocationCacheTable.LOT_FLD].ToString().Trim();
					if (odrPCS[IV_LocationCacheTable.INSPSTATUS_FLD] != DBNull.Value)
						objObject.InspStatus = int.Parse(odrPCS[IV_LocationCacheTable.INSPSTATUS_FLD].ToString().Trim());
					if (odrPCS[IV_LocationCacheTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[IV_LocationCacheTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[IV_LocationCacheTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[IV_LocationCacheTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[IV_LocationCacheTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[IV_LocationCacheTable.LOCATIONID_FLD].ToString().Trim());
					if (odrPCS[IV_LocationCacheTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[IV_LocationCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());
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
		/// <summary>
		/// GetAvailableQtyAndInsStatusByProduct
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasLocID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintProductID"></param>
		/// <returns>DataSet</returns>
		public DataSet GetAvailableQtyAndInsStatusByProduct(int pintCCNID, int pintMasLocID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQtyAndInsStatusByProduct()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)" + " AS AvailQuantity, " 
					+ IV_LocationCacheTable.INSPSTATUS_FLD
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " GROUP BY " + IV_LocationCacheTable.INSPSTATUS_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_LocationCacheTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to IV_LocationCache
		///    </Description>
		///    <Inputs>
		///       IV_LocationCacheVO       
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

			IV_LocationCacheVO objObject = (IV_LocationCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_LocationCache SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.LOT_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.CCNID_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + "=   ?" + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=   ?" 
					+" WHERE " + IV_LocationCacheTable.LOCATIONCACHEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONCACHEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONCACHEID_FLD].Value = objObject.LocationCacheID;


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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to IV_LocationCache
		///    </Description>
		///    <Inputs>
		///       IV_LocationCacheVO       
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
		
	
		public void UpdateReturnedGoods(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateReturnedGoods()";

			IV_LocationCacheVO objObject = (IV_LocationCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_LocationCache SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + "= " + IV_LocationCacheTable.OHQUANTITY_FLD + " +  ?" 
					+ " WHERE " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";


				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from IV_LocationCache
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
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD + ","
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_LocationCacheTable.TABLE_NAME);

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
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD + ","
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD 
					+ "  FROM " + IV_LocationCacheTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_LocationCacheTable.TABLE_NAME);

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to update a available quantity of product
		///    </Description>
		///    <Inputs>
		///        product ID, location id
		///    </Inputs>
		///    <Outputs>
		///       available quantity
		///    </Outputs>
		///    <Returns>
		///       decimal
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       23-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetQuantityOnHand(int pintLocation, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT SUM(ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0))"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocation
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update a available quantity of product
		///    </Description>
		///    <Inputs>
		///        product ID, location id
		///    </Inputs>
		///    <Outputs>
		///       available quantity
		///    </Outputs>
		///    <Returns>
		///       decimal
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       23-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetQuantityOnHand(int pintCCNID, int pintMasterLocationID, int pintLocation, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT SUM(ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0))"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocation
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
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

		//**************************************************************************              
		///    <Description>
		///     This method uses to update CommitQuantity where LocID, ProductID
		///    </Description>
		///    <Inputs>
		///        int LocID, int ProductID, decimal CommitQuantity
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///      
		///    </Returns>
		///    <Authors>
		///       Sonht
		///    </Authors>
		///    <History>
		///    01-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID,int pintProductID,decimal pdecCommit)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + "=   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.COMMITQUANTITY_FLD].Value = pdecCommit;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

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

		//**************************************************************************              
		///    <Description>
		///     This method uses to update MasterLocationCache.POnHandQuantity where MasLocID, ProductID
		///    </Description>
		///    <Inputs>
		///        CCNID, int MasLocID, LocationID, int ProductID, decimal CommitQuantity
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///      
		///    </Returns>
		///    <Authors>
		///       Sonht
		///       DungLa - 11-Mar-2005
		///    </Authors>
		///    <History>
		///    01-Mar-2005
		///    11-Mar-2005: pass more paramters: CCNID and MasterLocationID
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateOnHandQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID,int pintProductID,decimal pdecQuantity)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD +  "=   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

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

		//**************************************************************************              
		///    <Description>
		///     This method uses to update CommitQuantity for Sale order
		///    </Description>
		///    <Inputs>
		///        CCNID, MasterLocationID, LocationID, ProductID, decimal CommitQuantity
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
		///    21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, decimal pdecCommitQuantity)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		//**************************************************************************              
		///    <Description>
		///     This method uses to update CommitQuantity for Sale order
		///    </Description>
		///    <Inputs>
		///        CCNID, MasterLocationID, LocationID, ProductID, Lot, decimal CommitQuantity
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
		///    21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, string pstrLot, decimal pdecCommitQuantity)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOT_FLD].Value = pstrLot;

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to check commit quantity
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, LocationID, CommitQuantity, ProductID
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       return decimal.MinusOne if select query return a null value
		///       else return query value
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal CheckCommitQuantityByLocation(int pintCCNID, int pintMasterLocationID, int pintLocationID, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantityByLocation()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " - " + pdecCommitQuantity
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
		public decimal GetCommitQtyByPostDate(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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
		/// GetOHQtyByPostDate
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, December 20 2005</date>
		public decimal GetOHQtyByPostDate(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOHQtyByPostDate()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, December 19 2005</date>
		public decimal GetCommitQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
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
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					+ " AND TT.Code = 'SOCommitment' AND TH.PostDate > ?"
					//+ pdtmPostDate
					//+ " AND getdate() >= ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " ),0) + "  
					
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Code = 'SOCancelCommitment' AND TH.PostDate > ?" //+ pdtmPostDate 
					//+ " AND getdate() >= ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
                    + " ),0) + "  

					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Code = 'ShippingManagement' AND TH.PostDate > ?" 
					//+ pdtmPostDate 
					//+ " AND getdate() >= ?" 
					//+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ "),0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_1, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_1].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_2, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_2].Value = pdtmPostDate;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_3, OleDbType.Date));
				ocmdPCS.Parameters[PARAMETTER_3].Value = pdtmPostDate;
				
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_4, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_4].Value = pdtmPostDate;
//				
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_5, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_5].Value = pdtmPostDate;
//
//				ocmdPCS.Parameters.Add(new OleDbParameter(PARAMETTER_6, OleDbType.DBDate));
//				ocmdPCS.Parameters[PARAMETTER_6].Value = pdtmPostDate;

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
		/// <param name="pdtmPostDate"></param>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasterLocationID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, December 20 2005</date>
		public decimal GetOHQtyByPostDate(DateTime pdtmPostDate, int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
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
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					//+ " AND TT.Code IN ('POPurchaseOrderReceipts','WOReversal','PROWorkOrderCompletion','SOReturnGoodsReceive') 
					+ " AND TT.Type = " + (int)TransactionHistoryType.In
					+ " AND TH.PostDate > ?"
					//+ pdtmPostDate
					//+ " AND TH.PostDate <= ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " ),0)"
					
					+ " - " 
					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0)" 
					+ " FROM MST_TransactionHistory TH " 
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString()
					//+ " AND TT.Code IN ('POPurchaseOrderReceipts','WOReversal','PROWorkOrderCompletion','SOReturnGoodsReceive') 
					+ " AND TT.Type = " + (int)TransactionHistoryType.Both
					+ " AND TH.PostDate > ?"
					//+ pdtmPostDate
					//+ " AND TH.PostDate <= ?" //+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ " ),0) + "  

					+ " ISNULL((SELECT ISNULL(SUM(ISNULL(Quantity,0)),0) "
					+ " FROM MST_TransactionHistory TH "
					+ " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID "
					+ " WHERE ProductID = " + pintProductID.ToString() 
					+ " AND TT.Type = " + (int)TransactionHistoryType.Out
					//+ " AND TT.Code IN ('PROIssueMaterial','ShippingManagement','POReturnToVendor')" 
					+ " AND TH.PostDate > ?" 
					//+ pdtmPostDate 
					//+ " AND TH.PostDate <= ?" 
					//+ (new UtilsDS()).GetDBDate() 
					+ " AND TH.MasterLocationID = " + pintMasterLocationID.ToString()
					+ " AND TH.CCNID = " + pintCCNID.ToString()
					+ " AND TH.LocationID = " + pintLocationID.ToString()
					+ "),0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;
				
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

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to check commit quantity by Lot
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, LocationID, Lot, CommitQuantity, ProductID
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       return decimal.MinusOne if select query return a null value
		///       else return query value
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal CheckCommitQuantityByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, string pstrLot, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0) - " + pdecCommitQuantity
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "='" + pstrLot + "'";

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to check commit quantity
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, LocationID, CommitQuantity, ProductID
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       return decimal.MinusOne if select query return a null value
		///       else return query value
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetAvailableQuantityByLocation(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityByLocation()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get the QAStatus
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, LocationID, CommitQuantity, ProductID
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       return decimal.MinusOne if select query return a null value
		///       else return query value
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetQAStatusByLocation(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityByLocation()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT isnull("
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ",0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to check commit quantity by Lot
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, LocationID, Lot, CommitQuantity, ProductID
		///    </Inputs>
		///    <Outputs>
		///       decimal
		///    </Outputs>
		///    <Returns>
		///       return decimal.MinusOne if select query return a null value
		///       else return query value
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetAvailableQuantityByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableCommitQuantityByLot()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "='" + pstrLot + "'";

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

		//**************************************************************************              
		///    <Description>
		///    Update substract commit quantity
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, Apr 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateSubtractCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, decimal pdecCommitQuantity)	
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + "= "
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + " - " + pdecCommitQuantity
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

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

		//**************************************************************************              
		///    <Description>
		///     This method uses to update MasterLocationCache.POnHandQuantity where MasLocID, ProductID
		///    </Description>
		///    <Inputs>
		///        CCNID, int MasLocID, LocationID, int ProductID, decimal Quantity
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///    </Returns>
		///    <Authors>
		///    TuanDm
		///    </Authors>
		///    <History>
		///    Tuesday - May 10 - 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintProductID, decimal pdecQuantity)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + " = " + IV_LocationCacheTable.OHQUANTITY_FLD + "-   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

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

		//**************************************************************************              
		/// <summary>
		/// Add more quantity to On-hand quantity of Item in Location
		/// </summary>
		//**************************************************************************
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintProductID, decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateAddOHQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + " = ISNULL(" + IV_LocationCacheTable.OHQUANTITY_FLD + ", 0) +   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

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
		/// Get On-hand quantity of product in Location and by Lot
		/// </summary>
		/// <returns>On-hand Quantity</returns>
		public decimal GetQuantityOnHandByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_LocationCacheTable.OHQUANTITY_FLD
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
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
		/// Remove Lot controlled Item from Location when On-hand Quantity of Item is empty
		/// </summary>
		public void RemoveItemByLot(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID, string pstrLot)
		{
		}
		/// <summary>
		/// Remove Item from Location when On-hand Quantity of Item is empty
		/// </summary>
		public void RemoveItem(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
		}
		/// <summary>
		/// Update subtract on-hand quantity by Lot
		/// </summary>
		/// <param name="pdecQuantity">Quantity to subtract</param>
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintProductID, string pstrLot, decimal pdecQuantity)
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
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + " = " + IV_LocationCacheTable.OHQUANTITY_FLD + "-   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

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
		/// Add more quantity to On-hand quantity of Lot controlled Item in Location
		/// </summary>
		/// <param name="pdecQuantity">Quantity to add</param>
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintLocID, int pintProductID, string pstrLot, decimal pdecQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateAddOHQuantity()";

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_LocationCacheTable.TABLE_NAME + " SET "
					+ IV_LocationCacheTable.OHQUANTITY_FLD + " = ISNULL(" + IV_LocationCacheTable.OHQUANTITY_FLD + ", 0)+   ?"
					+ " WHERE " + IV_LocationCacheTable.LOCATIONID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_LocationCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOCATIONID_FLD].Value = pintLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_LocationCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_LocationCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_LocationCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.CommandText = strSql;
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
		/// Get Commit Quantity of Product in a Location
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID;

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
		/// Get Commit Quantity of Product in a Location by Lot
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pstrLot">Lot</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME 
					+ " WHERE " + IV_LocationCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_LocationCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_LocationCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintLocationID
					+ " AND " + IV_LocationCacheTable.LOT_FLD + "=?";

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

		public void UpdateAllQuantityFromBin()
		{
			const string METHOD_NAME = THIS + ".UpdateAllQuantityFromBin()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "DELETE IV_LocationCache\n"
					+ " DELETE IV_MasLocCache\n"
					+ " INSERT INTO IV_LocationCache (CCNID, MasterLocationID, LocationID, ProductID, OHQuantity, CommitQuantity)\n"
					+ " SELECT CCNID, MasterLocationID, LocationID, ProductID, SUM(ISNULL(OHQuantity,0)), SUM(ISNULL(CommitQuantity,0))\n"
					+ " FROM IV_BinCache GROUP BY CCNID, MasterLocationID, LocationID, ProductID\n"
					+ " INSERT INTO IV_MasLocCache (CCNID, MasterLocationID, ProductID, OHQuantity, CommitQuantity)\n"
					+ " SELECT CCNID, MasterLocationID, ProductID, SUM(ISNULL(OHQuantity,0)), SUM(ISNULL(CommitQuantity,0))\n"
					+ " FROM IV_BinCache GROUP BY CCNID, MasterLocationID, ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
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
		public DataSet ListAllCache()
		{
			const string METHOD_NAME = THIS + ".ListAllCache()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ IV_LocationCacheTable.LOCATIONCACHEID_FLD + ","
					+ IV_LocationCacheTable.OHQUANTITY_FLD + ","
					+ IV_LocationCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_LocationCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_LocationCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_LocationCacheTable.LOT_FLD + ","
					+ IV_LocationCacheTable.INSPSTATUS_FLD + ","
					+ IV_LocationCacheTable.CCNID_FLD + ","
					+ IV_LocationCacheTable.PRODUCTID_FLD + ","
					+ IV_LocationCacheTable.LOCATIONID_FLD + ","
					+ IV_LocationCacheTable.MASTERLOCATIONID_FLD 
					+ " FROM " + IV_LocationCacheTable.TABLE_NAME;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dtbData = new DataSet();
				odadPCS.Fill(dtbData, IV_LocationCacheTable.TABLE_NAME);
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

	}
}
