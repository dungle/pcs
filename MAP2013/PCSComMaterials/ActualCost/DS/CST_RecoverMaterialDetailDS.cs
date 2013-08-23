using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.ActualCost.DS
{
	public class CST_RecoverMaterialDetailDS 
	{
		public CST_RecoverMaterialDetailDS()
		{
		}
		private const string THIS = "PCSComMaterials.ActualCost.DS.CST_RecoverMaterialDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_RecoverMaterialDetail
		///    </Description>
		///    <Inputs>
		///        CST_RecoverMaterialDetailVO       
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
		///       Friday, February 24, 2006
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
				CST_RecoverMaterialDetailVO objObject = (CST_RecoverMaterialDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_RecoverMaterialDetail("
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
				+ CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PARTYID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD].Value = objObject.RecoverMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].Value = objObject.RecoverQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD].Value = objObject.UnitOfMeasureID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.TOBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = objObject.ToBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].Value = objObject.ToLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.PARTYID_FLD].Value = objObject.PartyID;


				
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
		///       This method uses to delete data from CST_RecoverMaterialDetail
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
			strSql=	"DELETE " + CST_RecoverMaterialDetailTable.TABLE_NAME + " WHERE  " + "RecoverMaterialDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from CST_RecoverMaterialDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_RecoverMaterialDetailVO
		///    </Outputs>
		///    <Returns>
		///       CST_RecoverMaterialDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Friday, February 24, 2006
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
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
				+ CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PARTYID_FLD
				+ " FROM " + CST_RecoverMaterialDetailTable.TABLE_NAME
				+" WHERE " + CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_RecoverMaterialDetailVO objObject = new CST_RecoverMaterialDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.RecoverMaterialDetailID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD].ToString().Trim());
				objObject.RecoverMaterialMasterID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.RecoverQuantity = Decimal.Parse(odrPCS[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].ToString().Trim());
				objObject.UnitOfMeasureID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD].ToString().Trim());
				objObject.ToBinID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString().Trim());
				objObject.ToLocationID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString().Trim());
				objObject.PartyID = int.Parse(odrPCS[CST_RecoverMaterialDetailTable.PARTYID_FLD].ToString().Trim());

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
		///       This method uses to update data to CST_RecoverMaterialDetail
		///    </Description>
		///    <Inputs>
		///       CST_RecoverMaterialDetailVO       
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

			CST_RecoverMaterialDetailVO objObject = (CST_RecoverMaterialDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_RecoverMaterialDetail SET "
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.TOBINID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialDetailTable.PARTYID_FLD + "=  ?"
				+" WHERE " + CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD].Value = objObject.RecoverMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].Value = objObject.RecoverQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD].Value = objObject.UnitOfMeasureID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.TOBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.TOBINID_FLD].Value = objObject.ToBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].Value = objObject.ToLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD].Value = objObject.RecoverMaterialDetailID;


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
		///       This method uses to get all data from CST_RecoverMaterialDetail
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
		///       Friday, February 24, 2006
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
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
				+ CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PARTYID_FLD
					+ " FROM " + CST_RecoverMaterialDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_RecoverMaterialDetailTable.TABLE_NAME);

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
		///       Friday, February 24, 2006
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
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
				+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
				+ CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
				+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
				+ CST_RecoverMaterialDetailTable.PARTYID_FLD 
				+ "  FROM " + CST_RecoverMaterialDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_RecoverMaterialDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from CST_RecoverMaterialDetail
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
		///       Friday, February 24, 2006
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListByMasterID(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByMasterID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT null as Line,"
					+ CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + ","
					+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
					+ CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ","
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " as MST_UnitOfMeasureCode,"
					+ CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
					+ CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
					+ CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " as MST_LocationCode,"
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " as MST_BINCode,"
					+ MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " as MST_PartyCode,"
					+ CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
					+ CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.PARTYID_FLD
					+ " FROM " + CST_RecoverMaterialDetailTable.TABLE_NAME
					+ " LEFT JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " LEFT JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " ON " + CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + "=" + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN " + MST_LocationTable.TABLE_NAME + " ON " + CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + "=" + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " LEFT JOIN " + MST_BINTable.TABLE_NAME + " ON " + CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.TOBINID_FLD + "=" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					+ " LEFT JOIN " + MST_PartyTable.TABLE_NAME + " ON " + CST_RecoverMaterialDetailTable.TABLE_NAME + "." + CST_RecoverMaterialDetailTable.PARTYID_FLD + "=" + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD
					+ " WHERE "+ CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + "=" + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_RecoverMaterialDetailTable.TABLE_NAME);

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

		public DataSet ListBomDetailOfProduct(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT null as Line,"
					+ "0 as " + CST_RecoverMaterialDetailTable.RECOVERMATERIALDETAILID_FLD + ","
					+ "0 as " + CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD + ","
					+ "a." + ITM_BOMTable.COMPONENTID_FLD + " as " + CST_RecoverMaterialDetailTable.PRODUCTID_FLD + ","
					+ "b." + ITM_ProductTable.CODE_FLD + ","
					+ "b." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ "b." + ITM_ProductTable.REVISION_FLD + ","
					+ "c." + MST_UnitOfMeasureTable.CODE_FLD + " as MST_UnitOfMeasureCode,"
					+ ITM_BOMTable.QUANTITY_FLD + ", 0.0 as " + CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD + ","
					+ "b." + ITM_ProductTable.STOCKUMID_FLD + " as " + CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD + ","
					+ "0 as " + CST_RecoverMaterialDetailTable.TOBINID_FLD + ","
					+ "'' as " + " MST_LocationCode,"
					+ "'' as " + " MST_BINCode,"
					+ "'' as " + " MST_PartyCode,"
					+ "0 as " + CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD + ","
					+ "0 as " + CST_RecoverMaterialDetailTable.PARTYID_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME + " A inner join " + ITM_ProductTable.TABLE_NAME + " B on A.ComponentID = B.ProductID "
					+ " inner join " + MST_UnitOfMeasureTable.TABLE_NAME + " C on B." + ITM_ProductTable.STOCKUMID_FLD + " = C." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " WHERE A." + ITM_BOMTable.PRODUCTID_FLD + " = " + pintProductID.ToString()
					+ " ORDER BY Line";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, CST_RecoverMaterialDetailTable.TABLE_NAME);

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
		/// List all recoverable material in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable List(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT SUM(ISNULL(RecoverQuantity,0)) AS RecoverQuantity, SUM(ISNULL(Quantity,0)) AS Quantity,"
					+ " CST_RecoverMaterialMaster.ProductID, CST_RecoverMaterialDetail.ProductID AS ComponentID"
					+ " FROM CST_RecoverMaterialMaster JOIN CST_RecoverMaterialDetail"
					+ " ON CST_RecoverMaterialMaster.RECOVERMATERIALMASTERID = CST_RecoverMaterialDetail.RECOVERMATERIALMASTERID"
					+ " WHERE " + CST_RecoverMaterialMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + CST_RecoverMaterialMasterTable.POSTDATE_FLD + " >= ?"
					+ " AND " + CST_RecoverMaterialMasterTable.POSTDATE_FLD + " <= ?"
					+ " GROUP BY CST_RecoverMaterialMaster.ProductID, CST_RecoverMaterialDetail.ProductID"
					+ " ORDER BY CST_RecoverMaterialMaster.ProductID, CST_RecoverMaterialDetail.ProductID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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
		/// Determine whenever freight detail has more than one destination (to location and to bin)
		/// </summary>
		/// <param name="pintMasterID">Freigth Master ID</param>
		/// <returns>true if has more than one. else false</returns>
		public bool HasMultiDestination(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".HasMultiDestination()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT DISTINCT ToLocationID, ToBinID FROM CST_RecoverMaterialDetail"
					+ " WHERE RecoverMaterialMasterID = " + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return (dtbData.Rows.Count > 1);
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

		public DataTable GetDataForSlip(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDataForSlip()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT PostDate, TransNo,"
					+ " FL.Code + ' (' + FL.Name + ')'  AS FromLocation,"
					+ " FB.Code + ' (' + FB.Name + ')'  AS FromBin,"
					+ " TL.Code + ' (' + TL.Name + ')'  AS ToLocation,"
					+ " TB.Code + ' (' + TB.Name + ')'  AS ToBin, RecoverQuantity,"
					+ " ITM_Product.Code AS PartsNumber, ITM_Product.Description AS PartsName, ITM_Category.Code AS Category,"
					+ " ITM_Product.Revision AS Model, MST_UnitOfMeasure.Code AS Unit, MST_Party.Code AS ToCustomer"
					+ " FROM CST_RecoverMaterialMaster JOIN CST_RecoverMaterialDetail"
					+ " ON CST_RecoverMaterialMaster.RecoverMaterialMasterID = CST_RecoverMaterialDetail.RecoverMaterialMasterID"
					+ " JOIN MST_Location AS FL ON CST_RecoverMaterialMaster.FromLocationID = FL.LocationID"
					+ " JOIN MST_Bin AS FB ON CST_RecoverMaterialMaster.FromBinID = FB.BinID"
					+ " LEFT JOIN MST_Location AS TL ON CST_RecoverMaterialDetail.ToLocationID = TL.LocationID"
					+ " LEFT JOIN MST_Bin AS TB ON CST_RecoverMaterialDetail.ToBinID = TB.BinID"
					+ " LEFT JOIN MST_Party ON CST_RecoverMaterialDetail.PartyID = MST_Party.PartyID"
					+ " JOIN ITM_Product ON CST_RecoverMaterialDetail.ProductID = ITM_Product.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " WHERE CST_RecoverMaterialMaster.RecoverMaterialMasterID = " + pintMasterID
					+ " ORDER BY ITM_Category.Code ASC, ITM_Product.Revision ASC, ITM_Product.Code ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable(CST_RecoverMaterialMasterTable.TABLE_NAME);
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
