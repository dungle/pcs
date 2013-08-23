using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.ScrapRecording.DS
{
	public class PRO_ComponentScrapDetailDS 
	{
		public PRO_ComponentScrapDetailDS()
		{
		}
		private const string THIS = "PCSComProduction.ScrapRecording.DS.PRO_ComponentScrapDetailDS";
		private const string WOM = " WOMaster";
		private const string WOD = " WODetail";
		private const string COMPSCRAP = " CompScrap";
		private const string PRO = " Product";
		private const string WOLINE = "WOLine";
		private const string COMPONENT = " Component";
		private const string COMPONENT_CODE = "ITM_ProductCode";
		private const string COMPONENT_DESCRIPTION = "ITM_ProductDescription";
		private const string COMPONENT_REVISION = "ITM_ProductRevision";
		private const string COMPONENT_UM = "MST_UnitOfMeasureCode";
		private const string UM = "MST_UnitOfMeasure";
		private const string SCRAP_REASON = "ScrapReason";
		private const string SCRAP_DES = " ScrapReasonDesc";
		private const string AVAILABLE_QUANTITY = "AvailableQuantity";
		private const string OPERATION = "Operation";
		
		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_ComponentScrapDetailVO objObject = (PRO_ComponentScrapDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_ComponentScrapDetail("
				+ PRO_ComponentScrapDetailTable.LOT_FLD + ","
				+ PRO_ComponentScrapDetailTable.SERIAL_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.PRODUCTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LINE_FLD + ","
				+ PRO_ComponentScrapDetailTable.WOROUTINGID_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].Value = objObject.ScrapQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD].Value = objObject.ComponentScrapMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.COMPONENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].Value = objObject.ComponentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.LINE_FLD].Value = objObject.Line;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WOROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.LINE_FLD].Value = objObject.WORoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD].Value = objObject.ScrapReasonID;


				
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
	
		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + PRO_ComponentScrapDetailTable.TABLE_NAME + " WHERE  " + "ComponentScrapDetailID" + "=" + pintID.ToString();
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
	
		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>

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
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LOT_FLD + ","
				+ PRO_ComponentScrapDetailTable.SERIAL_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.PRODUCTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LINE_FLD + ","
				+ PRO_ComponentScrapDetailTable.WOROUTINGID_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD
				+ " FROM " + PRO_ComponentScrapDetailTable.TABLE_NAME
				+" WHERE " + PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ComponentScrapDetailVO objObject = new PRO_ComponentScrapDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.ComponentScrapDetailID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD].ToString().Trim());
				objObject.Lot = odrPCS[PRO_ComponentScrapDetailTable.LOT_FLD].ToString().Trim();
				objObject.Serial = odrPCS[PRO_ComponentScrapDetailTable.SERIAL_FLD].ToString().Trim();
				objObject.ScrapQuantity = Decimal.Parse(odrPCS[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString().Trim());
				objObject.ComponentScrapMasterID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD].ToString().Trim());
				objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD].ToString().Trim());
				objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD].ToString().Trim());
				objObject.ComponentID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.Line = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.LINE_FLD].ToString().Trim());
				objObject.WORoutingID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.WOROUTINGID_FLD].ToString().Trim());
				objObject.ScrapReasonID = int.Parse(odrPCS[PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD].ToString().Trim());

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

		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			PRO_ComponentScrapDetailVO objObject = (PRO_ComponentScrapDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_ComponentScrapDetail SET "
				+ PRO_ComponentScrapDetailTable.LOT_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.SERIAL_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.LINE_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.WOROUTINGID_FLD + "=   ?" + ","
				+ PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD + "=  ?"
				+" WHERE " + PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].Value = objObject.ScrapQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD].Value = objObject.ComponentScrapMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.COMPONENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].Value = objObject.ComponentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.LINE_FLD].Value = objObject.Line;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.WOROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.WOROUTINGID_FLD].Value = objObject.WORoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD].Value = objObject.ScrapReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD].Value = objObject.ComponentScrapDetailID;


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

		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>

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
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LOT_FLD + ","
				+ PRO_ComponentScrapDetailTable.SERIAL_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.PRODUCTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LINE_FLD + ","
				+ PRO_ComponentScrapDetailTable.WOROUTINGID_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD
					+ " FROM " + PRO_ComponentScrapDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ComponentScrapDetailTable.TABLE_NAME);

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
		/// GetComponentScrapDetailByMasterID
		/// </summary>
		/// <param name="pintComponentScrapMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, July 1 2005</date>
		public DataSet GetComponentScrapDetailByMasterID(int pintComponentScrapMasterID)
		{
			const string METHOD_NAME = THIS + ".GetComponentScrapDetailByMasterID()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.LINE_FLD + ","
					+ WOM + Constants.DOT + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
					+ WOD + Constants.DOT + PRO_WorkOrderDetailTable.LINE_FLD + Constants.WHITE_SPACE + WOLINE + ","
					+ PRO + Constants.DOT + ITM_ProductTable.CODE_FLD + ","
					+ PRO + Constants.DOT + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ PRO + Constants.DOT + ITM_ProductTable.REVISION_FLD + ","
					//+ OPERATION + Constants.DOT + PRO_WORoutingTable.STEP_FLD + ","
					+ COMPONENT + Constants.DOT + ITM_ProductTable.CODE_FLD + Constants.WHITE_SPACE + COMPONENT_CODE + ","
					+ COMPONENT + Constants.DOT + ITM_ProductTable.DESCRIPTION_FLD + Constants.WHITE_SPACE + COMPONENT_DESCRIPTION + ","
					+ COMPONENT + Constants.DOT + ITM_ProductTable.REVISION_FLD + Constants.WHITE_SPACE + COMPONENT_REVISION + ","
					+ COMPONENT + Constants.DOT + ITM_ProductTable.STOCKUMID_FLD + ","
					+ UM + Constants.DOT + MST_UnitOfMeasureTable.CODE_FLD + Constants.WHITE_SPACE + COMPONENT_UM + ", "
					+ " FROMLOC." + MST_LocationTable.CODE_FLD + " FromLocation,"
					+ " FROMBIN." + MST_BINTable.CODE_FLD + " FromBin,"
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ","
//					+ " 0 AS " + AVAILABLE_QUANTITY + ", "
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.AVAILABLEQUANTITY_FLD + ","
					+ " TOLOC." + MST_LocationTable.CODE_FLD + " ToLocation,"
					+ " TOBIN." + MST_BINTable.CODE_FLD + " ToBin,"
					+ SCRAP_REASON + Constants.DOT + PRO_ScrapReasonTable.SCRAPREASONDESC_FLD + Constants.WHITE_SPACE + SCRAP_DES + ", "
					+ SCRAP_REASON + Constants.DOT + PRO_ScrapReasonTable.SCRAPREASONID_FLD + ", "
//					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.LOT_FLD + ","
//					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.SERIAL_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.COMPONENTID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.FROMBINID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.TOBINID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD + ","
					
					
					+ COMPSCRAP + Constants.DOT + ITM_ProductTable.PRODUCTID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + ","
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD
					+ " FROM " + PRO_ComponentScrapDetailTable.TABLE_NAME + COMPSCRAP
					+ " INNER JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME + WOM + " ON " 
					+ WOM + Constants.DOT + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD
					+ " INNER JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME + WOD + " ON " 
					+ WOD + Constants.DOT + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + PRO + " ON " 
					+ PRO + Constants.DOT + ITM_ProductTable.PRODUCTID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.PRODUCTID_FLD	
					+ " INNER JOIN " + ITM_ProductTable.TABLE_NAME + COMPONENT + " ON " 
					+ COMPONENT + Constants.DOT + ITM_ProductTable.PRODUCTID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.COMPONENTID_FLD
					+ " LEFT JOIN " + PRO_ScrapReasonTable.TABLE_NAME + Constants.WHITE_SPACE + SCRAP_REASON + " ON " 
					+ SCRAP_REASON + Constants.DOT + PRO_ScrapReasonTable.SCRAPREASONID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD
					+ " LEFT JOIN " + PRO_WORoutingTable.TABLE_NAME + Constants.WHITE_SPACE + OPERATION + " ON " 
					+ OPERATION + Constants.DOT + PRO_WORoutingTable.WOROUTINGID_FLD + " = " 
					+ COMPSCRAP + Constants.DOT + PRO_ComponentScrapDetailTable.WOROUTINGID_FLD
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + Constants.WHITE_SPACE + UM + " ON " 
					+ COMPONENT + Constants.DOT + ITM_ProductTable.STOCKUMID_FLD + " = " 
                    + UM + Constants.DOT + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " LEFT JOIN " + MST_LocationTable.TABLE_NAME + " FROMLOC ON FROMLOC." + MST_LocationTable.LOCATIONID_FLD
					+ " = " + COMPSCRAP + "." + PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD
					+ " LEFT JOIN " + MST_LocationTable.TABLE_NAME + " TOLOC ON TOLOC." + MST_LocationTable.LOCATIONID_FLD
					+ " = " + COMPSCRAP + "." + PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD
					+ " LEFT JOIN " + MST_BINTable.TABLE_NAME + " FROMBIN ON FROMBIN." + MST_BINTable.BINID_FLD
					+ " = " + COMPSCRAP + "." + PRO_ComponentScrapDetailTable.FROMBINID_FLD
					+ " LEFT JOIN " + MST_BINTable.TABLE_NAME + " TOBIN ON TOBIN." + MST_BINTable.BINID_FLD
					+ " = " + COMPSCRAP + "." + PRO_ComponentScrapDetailTable.TOBINID_FLD
					+ " WHERE " + PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + " = " + pintComponentScrapMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_ComponentScrapDetailTable.TABLE_NAME);

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

	
		///    <summary>
		///       This method uses to add data to PRO_ComponentScrapDetail
		///    </summary>
		///    <Inputs>
		///        PRO_ComponentScrapDetailVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Tuesday, June 07, 2005
		///    </History>
		
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
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD + ","
				+ PRO_ComponentScrapDetailTable.FROMBINID_FLD + ","
				+ PRO_ComponentScrapDetailTable.TOBINID_FLD + ","
				+ PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERMASTERID_FLD + ","
				+ PRO_ComponentScrapDetailTable.WORKORDERDETAILID_FLD + ","
				+ PRO_ComponentScrapDetailTable.COMPONENTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.PRODUCTID_FLD + ","
				+ PRO_ComponentScrapDetailTable.LINE_FLD + ","
				+ PRO_ComponentScrapDetailTable.AVAILABLEQUANTITY_FLD + ","
				+ PRO_ComponentScrapDetailTable.SCRAPREASONID_FLD 
				+ "  FROM " + PRO_ComponentScrapDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_ComponentScrapDetailTable.TABLE_NAME);

			}
			catch(OleDbException ex)
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
