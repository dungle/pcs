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
	public class CST_RecoverMaterialMasterDS 
	{
		public CST_RecoverMaterialMasterDS()
		{
		}
		private const string THIS = "PCSComMaterials.ActualCost.DS.CST_RecoverMaterialMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to CST_RecoverMaterialMaster
		///    </Description>
		///    <Inputs>
		///        CST_RecoverMaterialMasterVO       
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
				CST_RecoverMaterialMasterVO objObject = (CST_RecoverMaterialMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_RecoverMaterialMaster("
				+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
				+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
				+ CST_RecoverMaterialMasterTable.CCNID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
				+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
				+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD].Value = objObject.FromLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMBINID_FLD].Value = objObject.FromBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;


				
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
		/// <summary>
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObjectVO"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Mar 2 2006</date>
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				CST_RecoverMaterialMasterVO objObject = (CST_RecoverMaterialMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO CST_RecoverMaterialMaster("
					+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
					+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
					+ CST_RecoverMaterialMasterTable.CCNID_FLD + ","
					+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
					+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
					+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
					+ CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD + ","
					+ CST_RecoverMaterialMasterTable.USERNAME_FLD + ","
					+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?) SELECT @@Identity";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD].Value = objObject.FromLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMBINID_FLD].Value = objObject.FromBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD].Value = objObject.AvailableQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objReturn = ocmdPCS.ExecuteScalar();	
				if (objReturn != null)
				{
					return int.Parse(objReturn.ToString());
				}
				return 0;
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
			return 0;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from CST_RecoverMaterialMaster
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
			strSql=	"DELETE " + CST_RecoverMaterialMasterTable.TABLE_NAME + " WHERE  " + "RecoverMaterialMasterID" + "=" + pintID.ToString();
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
		///       This method uses to get data from CST_RecoverMaterialMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       CST_RecoverMaterialMasterVO
		///    </Outputs>
		///    <Returns>
		///       CST_RecoverMaterialMasterVO
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
				+ CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
				+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
				+ CST_RecoverMaterialMasterTable.CCNID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
				+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
				+ CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD + ","
				+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD
				+ " FROM " + CST_RecoverMaterialMasterTable.TABLE_NAME
				+" WHERE " + CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				CST_RecoverMaterialMasterVO objObject = new CST_RecoverMaterialMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.RecoverMaterialMasterID = int.Parse(odrPCS[CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD].ToString().Trim());
				objObject.PostDate = DateTime.Parse(odrPCS[CST_RecoverMaterialMasterTable.POSTDATE_FLD].ToString().Trim());
				objObject.TransNo = odrPCS[CST_RecoverMaterialMasterTable.TRANSNO_FLD].ToString().Trim();
				objObject.CCNID = int.Parse(odrPCS[CST_RecoverMaterialMasterTable.CCNID_FLD].ToString().Trim());
				objObject.FromLocationID = int.Parse(odrPCS[CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD].ToString().Trim());
				objObject.FromBinID = int.Parse(odrPCS[CST_RecoverMaterialMasterTable.FROMBINID_FLD].ToString().Trim());
				objObject.Quantity = Decimal.Parse(odrPCS[CST_RecoverMaterialMasterTable.QUANTITY_FLD].ToString().Trim());
				objObject.AvailableQty = Decimal.Parse(odrPCS[CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[CST_RecoverMaterialMasterTable.PRODUCTID_FLD].ToString().Trim());

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
		///       This method uses to update data to CST_RecoverMaterialMaster
		///    </Description>
		///    <Inputs>
		///       CST_RecoverMaterialMasterVO       
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

			CST_RecoverMaterialMasterVO objObject = (CST_RecoverMaterialMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE CST_RecoverMaterialMaster SET "
				+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.CCNID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD + "=   ?" + ","
				+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD + "=  ?"
				+" WHERE " + CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD].Value = objObject.FromLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.FROMBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.FROMBINID_FLD].Value = objObject.FromBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD].Value = objObject.AvailableQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD].Value = objObject.RecoverMaterialMasterID;


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
		///       This method uses to get all data from CST_RecoverMaterialMaster
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
				+ CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
				+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
				+ CST_RecoverMaterialMasterTable.CCNID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
				+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
				+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD
					+ " FROM " + CST_RecoverMaterialMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_RecoverMaterialMasterTable.TABLE_NAME);

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
		/// ListByMasterID
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Mar 3 2006</date>
		public DataSet ListByMasterID(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".ListByMasterID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ " RM." + CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.CCNID_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
					+ " RM." + CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD + ","
					+ " P." + ITM_ProductTable.CODE_FLD + ","
					+ " P." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ " P." + ITM_ProductTable.REVISION_FLD + ","
					+ " L." + MST_LocationTable.CODE_FLD + Constants.WHITE_SPACE + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + " ,"
					+ " B." + MST_BINTable.CODE_FLD + Constants.WHITE_SPACE + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD + " ,"
					+ " RM." + CST_RecoverMaterialMasterTable.PRODUCTID_FLD
					+ " FROM " + CST_RecoverMaterialMasterTable.TABLE_NAME + " RM"
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " P" 
					+ " ON RM." + CST_RecoverMaterialMasterTable.PRODUCTID_FLD + " = P." + ITM_ProductTable.PRODUCTID_FLD
					+ " INNER JOIN " + MST_LocationTable.TABLE_NAME + " L"
					+ " ON RM." + CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + " = L." + MST_LocationTable.LOCATIONID_FLD
					+ " INNER JOIN " + MST_BINTable.TABLE_NAME + " B"
					+ " ON RM." + CST_RecoverMaterialMasterTable.FROMBINID_FLD + " = B." + MST_BINTable.BINID_FLD
					+ " WHERE " + CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + " = " + pintMasterID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,CST_RecoverMaterialMasterTable.TABLE_NAME);

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
				+ CST_RecoverMaterialMasterTable.RECOVERMATERIALMASTERID_FLD + ","
				+ CST_RecoverMaterialMasterTable.POSTDATE_FLD + ","
				+ CST_RecoverMaterialMasterTable.TRANSNO_FLD + ","
				+ CST_RecoverMaterialMasterTable.CCNID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMLOCATIONID_FLD + ","
				+ CST_RecoverMaterialMasterTable.FROMBINID_FLD + ","
				+ CST_RecoverMaterialMasterTable.QUANTITY_FLD + ","
				+ CST_RecoverMaterialMasterTable.AVAILABLEQTY_FLD + ","
				+ CST_RecoverMaterialMasterTable.PRODUCTID_FLD 
				+ "  FROM " + CST_RecoverMaterialMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,CST_RecoverMaterialMasterTable.TABLE_NAME);

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
	}
}
