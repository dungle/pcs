using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduct.STDCost.DS
{
	public class CST_STDItemCostDS 
	{
		public CST_STDItemCostDS()
		{
		}
		private const string THIS = "PCSComProduct.STDCost.DS.CST_STDItemCostDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_STDItemCost
		///    </Description>
		///    <Inputs>
		///        CST_STDItemCostVO       
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
		///       Thursday, February 09, 2006
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
				CST_STDItemCostVO objObject = (CST_STDItemCostVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_STDItemCost("
				+ CST_STDItemCostTable.COST_FLD + ","
				+ CST_STDItemCostTable.ROLLUPDATE_FLD + ","
				+ CST_STDItemCostTable.PRODUCTID_FLD + ","
				+ CST_STDItemCostTable.COSTELEMENTID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_STDItemCostTable.COST_FLD].Value = objObject.Cost;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.ROLLUPDATE_FLD, OleDbType.Date));
				if(objObject.RollUpDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[CST_STDItemCostTable.ROLLUPDATE_FLD].Value = objObject.RollUpDate;
				}
				else
				{
					ocmdPCS.Parameters[CST_STDItemCostTable.ROLLUPDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_STDItemCostTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_STDItemCostTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;
				
				ocmdPCS.CommandText = strSql;
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from CST_STDItemCost
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

		public void DeleteByProduct(int pintProducID)
		{
			const string METHOD_NAME = THIS + ".DeleteByProduct()";

			string strSql = "DELETE " + CST_STDItemCostTable.TABLE_NAME + " WHERE  " 
						    + CST_STDItemCostTable.PRODUCTID_FLD + "=" + pintProducID.ToString();

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
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
	
		public void Delete(int pintItemCostID)
		{			
			const string METHOD_NAME = THIS + ".Delete()";

			string strSql = "DELETE " + CST_STDItemCostTable.TABLE_NAME + " WHERE  " 
							+ CST_STDItemCostTable.STDITEMCOSTID_FLD + "=" + pintItemCostID.ToString();

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
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
		///       This method uses to get data from CST_STDItemCost
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_STDItemCostVO
		///    </Outputs>
		///    <Returns>
		///       CST_STDItemCostVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, February 09, 2006
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
				+ CST_STDItemCostTable.STDITEMCOSTID_FLD + ","
				+ CST_STDItemCostTable.COST_FLD + ","
				+ CST_STDItemCostTable.ROLLUPDATE_FLD + ","
				+ CST_STDItemCostTable.PRODUCTID_FLD + ","
				+ CST_STDItemCostTable.COSTELEMENTID_FLD
				+ " FROM " + CST_STDItemCostTable.TABLE_NAME
				+" WHERE " + CST_STDItemCostTable.STDITEMCOSTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_STDItemCostVO objObject = new CST_STDItemCostVO();

				while (odrPCS.Read())
				{ 
					objObject.STDItemCostID = int.Parse(odrPCS[CST_STDItemCostTable.STDITEMCOSTID_FLD].ToString().Trim());
					objObject.Cost = Decimal.Parse(odrPCS[CST_STDItemCostTable.COST_FLD].ToString().Trim());
					if(!odrPCS[CST_STDItemCostTable.ROLLUPDATE_FLD].Equals(DBNull.Value))
					{
						objObject.RollUpDate = DateTime.Parse(odrPCS[CST_STDItemCostTable.ROLLUPDATE_FLD].ToString().Trim());
					}
					else
					{
						objObject.RollUpDate = DateTime.MinValue;
					}

					objObject.ProductID = int.Parse(odrPCS[CST_STDItemCostTable.PRODUCTID_FLD].ToString().Trim());
					objObject.CostElementID = int.Parse(odrPCS[CST_STDItemCostTable.COSTELEMENTID_FLD].ToString().Trim());
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
		///       This method uses to update data to CST_STDItemCost
		///    </Description>
		///    <Inputs>
		///       CST_STDItemCostVO       
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

			CST_STDItemCostVO objObject = (CST_STDItemCostVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_STDItemCost SET "
				+ CST_STDItemCostTable.COST_FLD + "=   ?" + ","
				+ CST_STDItemCostTable.ROLLUPDATE_FLD + "=   ?" + ","
				+ CST_STDItemCostTable.PRODUCTID_FLD + "=   ?" + ","
				+ CST_STDItemCostTable.COSTELEMENTID_FLD + "=  ?"
				+" WHERE " + CST_STDItemCostTable.STDITEMCOSTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.COST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_STDItemCostTable.COST_FLD].Value = objObject.Cost;

				if(objObject.RollUpDate != DateTime.MinValue)
				{
					ocmdPCS.Parameters[CST_STDItemCostTable.ROLLUPDATE_FLD].Value = objObject.RollUpDate;
				}
				else
				{
					ocmdPCS.Parameters[CST_STDItemCostTable.ROLLUPDATE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_STDItemCostTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.COSTELEMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_STDItemCostTable.COSTELEMENTID_FLD].Value = objObject.CostElementID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_STDItemCostTable.STDITEMCOSTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_STDItemCostTable.STDITEMCOSTID_FLD].Value = objObject.STDItemCostID;

				ocmdPCS.CommandText = strSql;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from CST_STDItemCost
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
		///       Thursday, February 09, 2006
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
					+ CST_STDItemCostTable.STDITEMCOSTID_FLD + ","
					+ CST_STDItemCostTable.COST_FLD + ","
					+ CST_STDItemCostTable.ROLLUPDATE_FLD + ","
					+ CST_STDItemCostTable.PRODUCTID_FLD + ","
					+ CST_STDItemCostTable.COSTELEMENTID_FLD
					+ " FROM " + CST_STDItemCostTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_STDItemCostTable.TABLE_NAME);

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

		public DataTable GetItemCostDetail(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetItemCostDetail()";
			DataTable dtbResult = new DataTable(CST_STDItemCostTable.TABLE_NAME);
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  CST_STDItemCost.STDItemCostID,";
				strSql += " STD_CostElement.CostElementTypeID,";				
				strSql += " CST_STDItemCost.ProductID,";
				strSql += " CST_STDItemCost.CostElementID,";
				strSql += " STD_CostElement.Code as STD_CostElementCode,";
				strSql += " STD_CostElement.Name as STD_CostElementName,";
				strSql += " CST_STDItemCost.Cost,";
				strSql += " CST_STDItemCost.RollUpDate, ";
				strSql += " STD_CostElement.OrderNo";

				strSql += " FROM CST_STDItemCost";
				strSql += " INNER JOIN STD_CostElement ON CST_STDItemCost.CostElementID = STD_CostElement.CostElementID";

				strSql += " WHERE CST_STDItemCost.ProductID = " + pintProductID.ToString();

				strSql += " ORDER BY STD_CostElement.OrderNo ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
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
		
		public DataTable GetProductItemInfo(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetProductItemInfo()";
			DataTable dtbResult = new DataTable(ITM_ProductTable.TABLE_NAME);
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = "SELECT  ITM_Product.Code,";
				strSql += " ITM_Product.ProductID,";
				strSql += " ITM_Product.Revision,";
				strSql += " ITM_Product.Description,"; 
				strSql += " ITM_Product.MakeItem,";
				strSql += " MST_UnitOfMeasure.Code as MST_UnitOfMeasureCode,";
				strSql += " ITM_Product.StockUMID";
				strSql += " FROM ITM_Product";
				strSql += " INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID";

				strSql += " WHERE ITM_Product.ProductID = " + pintProductID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
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
		///       Thursday, February 09, 2006
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
					+ CST_STDItemCostTable.STDITEMCOSTID_FLD + ","
					+ CST_STDItemCostTable.COST_FLD + ","
					+ CST_STDItemCostTable.ROLLUPDATE_FLD + ","
					+ CST_STDItemCostTable.PRODUCTID_FLD + ","
					+ CST_STDItemCostTable.COSTELEMENTID_FLD
					+ "  FROM " + CST_STDItemCostTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;

				odadPCS.Update(pData, CST_STDItemCostTable.TABLE_NAME);
			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
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
		/// Get cost of none make item
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetNoneMakeCost(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetNoneMakeCost()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT STDItemCostID, ProductID, CostElementID, RollupDate, ISNULL(Cost, 0) Cost"
					+ " FROM CST_STDItemCost"
					+ " WHERE ProductID IN (SELECT ProductID FROM ITM_Product WHERE MakeItem <> 1 AND CCNID = " + pintCCNID + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, CST_STDItemCostTable.TABLE_NAME);

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
		/// Delete cost of Make item in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		public void DeleteMakeItemCost(int pintCCNID)
		{			
			const string METHOD_NAME = THIS + ".DeleteMakeItemCost()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "DELETE CST_STDItemCost WHERE ProductID IN"
					+ " (SELECT ProductID FROM ITM_Product WHERE MakeItem = 1 AND CCNID = " + pintCCNID + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
	
		/// <summary>
		/// List standard cost of all item in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Standard cost of all item in CCN</returns>
		public DataTable List(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT STDItemCostID, ISNULL(Cost, 0) Cost, RollupDate, CST_STDItemCost.ProductID,"
					+ " CST_STDItemCost.CostElementID, STD_CostElementType.Code AS TypeCode"
					+ " FROM CST_STDItemCost JOIN ITM_Product"
					+ " ON CST_STDItemCost.ProductID = ITM_Product.ProductID"
					+ " JOIN STD_CostElement"
					+ " ON CST_STDItemCost.CostElementID = STD_CostElement.CostElementID"
					+ " JOIN STD_CostElementType"
					+ " ON STD_CostElement.CostElementTypeID = STD_CostElementType.CostElementTypeID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbData = new DataTable(CST_STDItemCostTable.TABLE_NAME);
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

	}
}
