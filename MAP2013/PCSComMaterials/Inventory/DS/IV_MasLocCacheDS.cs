using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComMaterials.Inventory.DS
{
	
	public class IV_MasLocCacheDS 
	{
		public IV_MasLocCacheDS()
		{
		}
		private const string THIS = "PCSComMaterials.Inventory.DS.IV_MasLocCacheDS";

		#region Validating Methods

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

		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
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

		public bool HasProductID(int pintProductID, int pintCCNID, int pintMasterLocationID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT Count(*) "
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

				if(pstrLot.Length != 0)
				{
					strSql += "     AND " + IV_MasLocCacheTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'";
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
		///       This method uses to check commit quantity
		///    </Description>
		///    <Inputs>
		///       CCNID, MasterLocationID, CommitQuantity, ProductID
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
		public decimal CheckCommitQuantity(int pintCCNID, int pintMasterLocationID, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantityByLocation()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0)"
					+ " - ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " - " + pdecCommitQuantity
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

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
		public decimal CheckCommitQuantityByLot(int pintCCNID, int pintMasterLocationID, string pstrLot, decimal pdecCommitQuantity, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".CheckCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0) - " + pdecCommitQuantity
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "='" + pstrLot + "'";

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

		#endregion

		#region Update methods

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///        IV_MasLocCacheVO       
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
				IV_MasLocCacheVO objObject = (IV_MasLocCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_MasLocCache("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_MasLocCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.AVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = objObject.AVGCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.SUMMITEMCOST21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.SUMMITEMCOST21_FLD].Value = objObject.SummItemCost21;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				
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
		///       This method uses to add data to IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///        IV_MasLocCacheVO       
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


		public int AddReturnedGoods(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnedGoods()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_MasLocCacheVO objObject = (IV_MasLocCacheVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_MasLocCache("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD + ")"
					+ "VALUES(?,?,?,?,?)";
				
				strSql += " ; SELECT @@IDENTITY as NewID ";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.AVGCOST_FLD, OleDbType.Decimal));
				if (objObject.AVGCost < 0) 
				{
					ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = objObject.AVGCost;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());


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
		///       This method uses to delete data from IV_MasLocCache
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
			strSql=	"DELETE " + IV_MasLocCacheTable.TABLE_NAME + " WHERE  " + "MasLocCacheID" + "=" + pintID.ToString();
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
		///       This method uses to update data to IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///       IV_MasLocCacheVO       
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
			const string METHOD_NAME = THIS + ".Update()";

			IV_MasLocCacheVO objObject = (IV_MasLocCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_MasLocCache SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "=" + IV_MasLocCacheTable.OHQUANTITY_FLD  + " +  ?" + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + "=   ?" 
					+" WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?"
					+" AND " +  IV_MasLocCacheTable.CCNID_FLD + "=   ?" 
					+" AND " +  IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=   ?" ;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;


				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.AVGCOST_FLD, OleDbType.Decimal));
				if (objObject.AVGCost >=0) 
				{
					ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = objObject.AVGCost;
				}
				else
				{
					ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

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
		///       This method uses to update data to IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///       IV_MasLocCacheVO       
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

			IV_MasLocCacheVO objObject = (IV_MasLocCacheVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_MasLocCache SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.LOT_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.CCNID_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + "=   ?" + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD + "=   ?" 
					+" WHERE " + IV_MasLocCacheTable.MASLOCCACHEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = objObject.OHQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.DEMANQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.DEMANQUANTITY_FLD].Value = objObject.DemanQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_MasLocCacheTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.INSPSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.INSPSTATUS_FLD].Value = objObject.InspStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.AVGCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.AVGCOST_FLD].Value = objObject.AVGCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.SUMMITEMCOST21_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.SUMMITEMCOST21_FLD].Value = objObject.SummItemCost21;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASLOCCACHEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASLOCCACHEID_FLD].Value = objObject.MasLocCacheID;

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
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD + ","
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD 
					+ "  FROM " + IV_MasLocCacheTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_MasLocCacheTable.TABLE_NAME);

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
		///     This method uses to update MasterLocationCache.CommitQuantity where MasLocID, ProductID
		///    </Description>
		///    <Inputs>
		///        int MasLocID, int ProductID, decimal CommitQuantity
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///      
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    01-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateCommitQuantity(int pintCCNID, int pintMasLocID, int pintProductID, decimal decCommitQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + "=   ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.COMMITQUANTITY_FLD].Value = decCommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		///        int CCNID, int MasLocID, int ProductID, decimal CommitQuantity
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///      
		///    </Returns>
		///    <Authors>
		///       Sonht
		///       DungLa: 11-Mar-2005
		///    </Authors>
		///    <History>
		///    01-Mar-2005
		///    11-Mar-2005: Pass one more parameter: CCNID
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateOnHandQuantity(int pintCCNID, int pintMasLocID, int pintProductID, decimal decOnHandQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "=   ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = decOnHandQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		///        CCNID, MasterLocationID, ProductID, decimal CommitQuantity
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
		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintProductID, decimal pdecCommitQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		///        CCNID, MasterLocationID, ProductID, Lot, decimal CommitQuantity
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
		public void UpdateCommitQuantityForSO(int pintCCNID, int pintMasterLocationID, int pintProductID, string pstrLot, decimal pdecCommitQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + "= ISNULL("
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0) + " + pdecCommitQuantity
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_MasLocCacheTable.LOT_FLD].Value = pstrLot;

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
		///    Update substract commit inventory
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
		public void UpdateSubtractCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintProductID, decimal pdecCommitQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + "= "
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + " - " + pdecCommitQuantity
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				//prepare value for parameters
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		///        int CCNID, int MasLocID, int ProductID, decimal CommitQuantity
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///      
		///    </Returns>
		///    <Authors>
		///       Sonht
		///       DungLa: 11-Mar-2005
		///    </Authors>
		///    <History>
		///    01-Mar-2005
		///    11-Mar-2005: Pass one more parameter: CCNID
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasLocID, int pintProductID, decimal decOnHandQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "= " + IV_MasLocCacheTable.OHQUANTITY_FLD + "- ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = decOnHandQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		///        int CCNID, int MasLocID, int ProductID, decimal CommitQuantity
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
		///    11-Mar-2005: Pass one more parameter: CCNID
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasLocID, int pintProductID, decimal decOnHandQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "= ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) + ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = decOnHandQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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
		public void UpdateAddOHQuantity(int pintCCNID, int pintMasterLocationID, int pintProductID, string pstrLot, decimal pdecQuantity)
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
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "= ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) + ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = pdecQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_MasLocCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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

		public void UpdateSubtractOHQuantity(int pintCCNID, int pintMasLocID, int pintProductID, string pstrLot, decimal pdecOnHandQuantity)
		{
			const string METHOD_NAME = THIS + ".UpdateOnHandQuantity()";
			#region Check before subtract OHQuantity 
			// HACK: SonHT
			if(GetQuantityOnHand(pintCCNID,pintMasLocID,pintProductID) < pdecOnHandQuantity)
			{
				throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, METHOD_NAME, null);
			}

			#endregion

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + IV_MasLocCacheTable.TABLE_NAME + " SET "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + "= " + IV_MasLocCacheTable.OHQUANTITY_FLD + "- ?"
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "= ?"
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.OHQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MasLocCacheTable.OHQUANTITY_FLD].Value = pdecOnHandQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].Value = pintMasLocID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.CCNID_FLD].Value = pintCCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.LOT_FLD, OleDbType.VarWChar, 40));
				ocmdPCS.Parameters[IV_MasLocCacheTable.LOT_FLD].Value = pstrLot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MasLocCacheTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MasLocCacheTable.PRODUCTID_FLD].Value = pintProductID;

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

		public void UpdateAllQuantityFromLocation()
		{
			const string METHOD_NAME = THIS + ".UpdateAllQuantityFromLocation()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "UPDATE IV_MasLocCache SET OHQuantity = "
					+ " (SELECT SUM(OHQuantity) FROM IV_LocationCache"
					+ " WHERE ProductID = IV_MasLocCache.ProductID"
					+ " AND MasterLocationID = IV_MasLocCache.MasterLocationID)";
				
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
		public void UpdateAllQuantityFromBin()
		{
			const string METHOD_NAME = THIS + ".UpdateAllQuantityFromBin()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "UPDATE IV_MasLocCache SET OHQuantity = "
					+ " (SELECT SUM(OHQuantity) FROM IV_BinCache"
					+ " WHERE ProductID = IV_MasLocCache.ProductID"
					+ " AND MasterLocationID = IV_MasLocCache.MasterLocationID)";
				
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
		#endregion

		#region Gets methods

		public DataTable GetOnhandQuantity(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetOnhandQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT SUM(ISNULL(OHQuantity, 0)) AS Quantity"
					+ ", ProductID FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE IV_MasLocCache.CCNID = " + pintCCNID
					+ " AND IV_MasLocCache.MasterLocationID = " + SystemProperty.MasterLocationID
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
		/// <summary>
		/// Get On-hand quantity of product in MasterLocation and by Lot
		/// </summary>
		/// <returns>On-hand Quantity</returns>
		public decimal GetQuantityOnHandByLot(int pintCCNID, int pintMasterLocationID, int pintProductID, string pstrLot)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = " SELECT ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ",0)),0)" 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID					
					+ "      AND " + IV_MasLocCacheTable.LOT_FLD + "='" + pstrLot.Replace("'", "''") + "'" 
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();

				if (objReturn == null) 
				{
					return 0;
				}
				else
				{
					return Decimal.Parse(objReturn.ToString());
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
		/// Get On-hand quantity of product in MasterLocation
		/// </summary>
		public decimal GetQuantityOnHand(int pintCCNID, int pintMasterLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = " SELECT ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0)" 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID					
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();

				if (objReturn == null) 
				{
					return 0;
				}
				else
				{
					return Decimal.Parse(objReturn.ToString());
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
		/// Get On-hand quantity of product in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pblnIsFirstPeriod">Determine whenerver get onhand for first period or not</param>
		public DataTable GetQuantityOnHand(int pintCCNID, bool pblnIsFirstPeriod)
		{
			const string METHOD_NAME = THIS + ".GetQuantityOnHand()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT SUM(ISNULL(OHQuantity, 0)) AS " + IV_MasLocCacheTable.OHQUANTITY_FLD
					+ ", ProductID FROM IV_BinCache"
					+ " JOIN MST_BIN ON IV_BinCache.BinID = MST_BIN.BinID"
					+ " WHERE IV_BinCache.CCNID = " + pintCCNID;
				if (!pblnIsFirstPeriod)
					strSql += " AND MST_BIN.BinTypeID <> " + (int)BinTypeEnum.LS;
                strSql += " GROUP BY ProductID";
				
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
		/// <summary>
		/// Get available quantity of a Product from the list of Master Location
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">List of Master Location</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Available Quantity</returns>
		/// <old-param name="pstrmasterlocationids">List of Master Location</old-param>
		public decimal GetAvailableQuantityForPlan(int pintCCNID, int pintMasterLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityForPlan()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)"
					+ " - ISNULL(" + ITM_ProductTable.SAFETYSTOCK_FLD + ", 0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.PRODUCTID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " WHERE " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

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
		/// Get available quantity of all Product in a Master Location
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">List of Master Location</param>
		/// <returns>Available Quantity</returns>
		public DataTable GetAvailableQuantityForPlan(int pintCCNID, int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityForPlan()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			DataSet dstPCS = new DataSet();
			try 
			{
				string strSql=	"SELECT IV_BinCache.LocationID, ProductID,"
					+ " ISNULL(SUM(ISNULL(OHQuantity, 0)), 0) - ISNULL(SUM(ISNULL(CommitQuantity, 0)), 0) AS 'SupplyQuantity'"
					+ " FROM IV_BinCache JOIN MST_BIN"
					+ " ON IV_BinCache.BinID = MST_BIN.BinID"
					+ " JOIN enm_BINType ON MST_BIN.BinTypeID = enm_BINType.BinTypeID"
					+ " WHERE CCNID = " + pintCCNID
					+ " AND enm_BINType.BinTypeID NOT IN (" + (int)BinTypeEnum.NG + "," + (int)BinTypeEnum.LS + ")"
					+ " AND MasterLocationID = " + pintMasterLocationID
					+ " GROUP BY IV_BinCache.LocationID, ProductID"
					+ " ORDER BY IV_BinCache.LocationID, ProductID";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_MasLocCacheTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		/// Get Commit Quantity of Product in a Master Location
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

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
		/// Get Commit Quantity of Product in a Master Location by Lot
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pstrLot">Lot</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Total Commit Quantity</returns>
		public decimal GetCommitQuantity(int pintCCNID, int pintMasterLocationID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetCommitQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)),0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "=?";

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_MasLocCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_MasLocCacheVO
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
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD + ","
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+" WHERE " + IV_MasLocCacheTable.MASLOCCACHEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_MasLocCacheVO objObject = new IV_MasLocCacheVO();

				while (odrPCS.Read())
				{
					objObject.MasLocCacheID = int.Parse(odrPCS[IV_MasLocCacheTable.MASLOCCACHEID_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.OHQUANTITY_FLD] != DBNull.Value)
						objObject.OHQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.OHQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.COMMITQUANTITY_FLD] != DBNull.Value)
						objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.DEMANQUANTITY_FLD] != DBNull.Value)
						objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD] != DBNull.Value)
						objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_MasLocCacheTable.LOT_FLD].ToString().Trim();
					if (odrPCS[IV_MasLocCacheTable.INSPSTATUS_FLD] != DBNull.Value)
						objObject.InspStatus = int.Parse(odrPCS[IV_MasLocCacheTable.INSPSTATUS_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[IV_MasLocCacheTable.CCNID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.AVGCOST_FLD] != DBNull.Value)
						objObject.AVGCost = decimal.Parse(odrPCS[IV_MasLocCacheTable.AVGCOST_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.SUMMITEMCOST21_FLD] != DBNull.Value)
						objObject.SummItemCost21 = Decimal.Parse(odrPCS[IV_MasLocCacheTable.SUMMITEMCOST21_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_MasLocCacheTable.PRODUCTID_FLD].ToString().Trim());
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
		///       This method uses to get data from IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_MasLocCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_MasLocCacheVO
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

		public object GetObjectVO(int pintMasLocID, int pintProductID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD + ","
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
					+ " AND " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_MasLocCacheVO objObject = new IV_MasLocCacheVO();

				while (odrPCS.Read())
				{
					objObject.MasLocCacheID = int.Parse(odrPCS[IV_MasLocCacheTable.MASLOCCACHEID_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.OHQUANTITY_FLD] != DBNull.Value)
						objObject.OHQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.OHQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.COMMITQUANTITY_FLD] != DBNull.Value)
						objObject.CommitQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.COMMITQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.DEMANQUANTITY_FLD] != DBNull.Value)
						objObject.DemanQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.DEMANQUANTITY_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD] != DBNull.Value)
						objObject.SupplyQuantity = Decimal.Parse(odrPCS[IV_MasLocCacheTable.SUPPLYQUANTITY_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_MasLocCacheTable.LOT_FLD].ToString().Trim();
					if (odrPCS[IV_MasLocCacheTable.INSPSTATUS_FLD] != DBNull.Value)
						objObject.InspStatus = int.Parse(odrPCS[IV_MasLocCacheTable.INSPSTATUS_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[IV_MasLocCacheTable.CCNID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[IV_MasLocCacheTable.MASTERLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.AVGCOST_FLD] != DBNull.Value)
						objObject.AVGCost = decimal.Parse(odrPCS[IV_MasLocCacheTable.AVGCOST_FLD].ToString().Trim());
					if (odrPCS[IV_MasLocCacheTable.SUMMITEMCOST21_FLD] != DBNull.Value)
						objObject.SummItemCost21 = Decimal.Parse(odrPCS[IV_MasLocCacheTable.SUMMITEMCOST21_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_MasLocCacheTable.PRODUCTID_FLD].ToString().Trim());
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
		///       This method uses to get data from IV_MasLocCache
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_MasLocCacheVO
		///    </Outputs>
		///    <Returns>
		///       IV_MasLocCacheVO
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

		public int GetMasLocCacheID(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".GetMasLocCacheID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+" WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+" AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+" AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID ;

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
					if (objResult.ToString().Trim() == String.Empty)
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

		public decimal GetOnHanQty(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = " SELECT ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0)" 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				object objReturn = ocmdPCS.ExecuteScalar();

				if (objReturn == null) 
				{
					return 0;
				}
				else
				{
					return Decimal.Parse(objReturn.ToString());
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
		///       This method uses to get AvgCost of Product in a MasterLocation by CCN
		///    </Description>
		///    <Inputs>
		///        int ProductID
		///    </Inputs>
		///    <Outputs>
		///       AvgCost
		///    </Outputs>
		///    <Returns>
		///       Decimal
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

		public decimal GetAvgCost(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".GetAvgCost()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = " SELECT ISNULL(" + IV_MasLocCacheTable.AVGCOST_FLD + ",0) " 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ "      AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ "      AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();

				if (objResult != null)
					return decimal.Parse(objResult.ToString());
				else
					return 0;
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
		///       CCNID, MasterLocationID, CommitQuantity, ProductID
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
		public decimal GetAvailableQuantity(int pintCCNID, int pintMasterLocationID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

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
		///       CCNID, MasterLocationID, CommitQuantity, ProductID
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
		public decimal GetAvailableQuantity(int pintCCNID, int pintProductID, ArrayList parrListMasterLocationID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantity()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strInWhere = " in (";
				for (int i =0; i < parrListMasterLocationID.Count; i++)
				{
					strInWhere += parrListMasterLocationID[i].ToString() + ",";
				}
				strInWhere += 0.ToString() + ")";
				string strSql=	"SELECT ISNULL(SUM(ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0)), 0) - ISNULL(SUM(ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)), 0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + strInWhere;

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
		public decimal GetAvailableQuantityByLot(int pintCCNID, int pintMasterLocationID, string pstrLot, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetAvailableQuantityByLot()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL("
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0)"
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME 
					+ " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + IV_MasLocCacheTable.LOT_FLD + "='" + pstrLot + "'";

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
		///    Get AVG Cost by ProductID and MasLocID
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       Decimal
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, May 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetAVGCostByProductIDMasLocID(int pintProductID, int pintMasLocID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ IV_MasLocCacheTable.AVGCOST_FLD
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.MASLOCCACHEID_FLD + "=" + pintMasLocID + " and " + IV_MasLocCacheTable.PRODUCTID_FLD + " = " + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
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
		///       This method uses to get all data from IV_MasLocCache
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
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD + ","
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_MasLocCacheTable.TABLE_NAME);

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

		public DataTable GetBeginQuantityOfNiguri(DateTime pdtmAsOfDate)
		{
			const string METHOD_NAME = THIS + "GetBeginQuantityOfNiguri";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			DataSet dstPCS = new DataSet();
			try 
			{
				string strSql=	"SELECT " + IV_BeginMRPTable.BEGINMRPID_FLD + ","
					+ IV_BeginMRPTable.ASOFTDATE_FLD + ","
					+ IV_BeginMRPTable.LOCATIONID_FLD + ","
					+ IV_BeginMRPTable.PRODUCTID_FLD + ","
					+ IV_BeginMRPTable.QUANTITYMAP_FLD + ","
					+ IV_BeginMRPTable.QUANTITY_FLD
					+ " FROM " + IV_BeginMRPTable.TABLE_NAME
					+ " WHERE " + IV_BeginMRPTable.ASOFTDATE_FLD + "=?"
					+ " ORDER BY LocationID, ProductID";
		
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("AsOfDate", OleDbType.Date)).Value = pdtmAsOfDate;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_BeginMRPTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
		public DataSet ListAllCache()
		{
			const string METHOD_NAME = THIS + ".ListAllCache()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql =	"SELECT "
					+ IV_MasLocCacheTable.MASLOCCACHEID_FLD + ","
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.COMMITQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.DEMANQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.SUPPLYQUANTITY_FLD + ","
					+ IV_MasLocCacheTable.LOT_FLD + ","
					+ IV_MasLocCacheTable.INSPSTATUS_FLD + ","
					+ IV_MasLocCacheTable.CCNID_FLD + ","
					+ IV_MasLocCacheTable.MASTERLOCATIONID_FLD + ","
					+ IV_MasLocCacheTable.AVGCOST_FLD + ","
					+ IV_MasLocCacheTable.SUMMITEMCOST21_FLD + ","
					+ IV_MasLocCacheTable.PRODUCTID_FLD 
					+ " FROM " + IV_MasLocCacheTable.TABLE_NAME;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dtbData = new DataSet();
				odadPCS.Fill(dtbData, IV_MasLocCacheTable.TABLE_NAME);
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

		#endregion
	}
}
