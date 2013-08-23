using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Text;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComProduct.Items.DS
{
	public class ITM_BOMDS
	{
		private const string THIS = "PCSComProduct.Items.DS.ITM_BOMDS";
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				ITM_BOMVO objObject = (ITM_BOMVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO ITM_BOM("
				+ ITM_BOMTable.PRODUCTID_FLD + ","
				+ ITM_BOMTable.LINE_FLD + ","
				+ ITM_BOMTable.COMPONENTID_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
				+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
				+ ITM_BOMTable.QUANTITY_FLD + ","
				+ ITM_BOMTable.ROUTINGID_FLD + ","
				+ ITM_BOMTable.SHRINK_FLD + ","
				+ ITM_BOMTable.ANCESTOR_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
				+ ITM_BOMTable.ALTERNATIVE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.PRODUCTID_FLD].Value = objObject.ProductID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.PRODUCTID_FLD].Value = objObject.Line;


				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.COMPONENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.COMPONENTID_FLD].Value = objObject.ComponentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].Value = objObject.EffectiveBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDATE_FLD].Value = objObject.EffectiveEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.LEADTIMEOFFSET_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.LEADTIMEOFFSET_FLD].Value = objObject.LeadTimeOffset;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.ROUTINGID_FLD].Value = objObject.RoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.SHRINK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.SHRINK_FLD].Value = objObject.Shrink;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ANCESTOR_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_BOMTable.ANCESTOR_FLD].Value = objObject.Ancestor;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEENDDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDAY_FLD].Value = objObject.EffectiveEndDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEBEGINDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].Value = objObject.EffectiveBeginDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ALTERNATIVE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.ALTERNATIVE_FLD].Value = objObject.Alternative;
				
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
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + ITM_BOMTable.TABLE_NAME + " WHERE  " + "BomID" + "=" + pintID.ToString();
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
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
				+ ITM_BOMTable.BOMID_FLD + ","
				+ ITM_BOMTable.PRODUCTID_FLD + ","
				+ ITM_BOMTable.COMPONENTID_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
				+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
				+ ITM_BOMTable.QUANTITY_FLD + ","
				+ ITM_BOMTable.ROUTINGID_FLD + ","
				+ ITM_BOMTable.SHRINK_FLD + ","
				+ ITM_BOMTable.ANCESTOR_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
				+ ITM_BOMTable.ALTERNATIVE_FLD
				+ " FROM " + ITM_BOMTable.TABLE_NAME
				+" WHERE " + ITM_BOMTable.BOMID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_BOMVO objObject = new ITM_BOMVO();

				while (odrPCS.Read())
				{ 
				objObject.BomID = int.Parse(odrPCS[ITM_BOMTable.BOMID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[ITM_BOMTable.PRODUCTID_FLD].ToString().Trim());
				objObject.ComponentID = int.Parse(odrPCS[ITM_BOMTable.COMPONENTID_FLD].ToString().Trim());
				objObject.EffectiveBeginDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
				objObject.EffectiveEndDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
				objObject.LeadTimeOffset = Decimal.Parse(odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD].ToString().Trim());
				objObject.Quantity = Decimal.Parse(odrPCS[ITM_BOMTable.QUANTITY_FLD].ToString().Trim());
				objObject.RoutingID = int.Parse(odrPCS[ITM_BOMTable.ROUTINGID_FLD].ToString().Trim());
				objObject.Shrink = Decimal.Parse(odrPCS[ITM_BOMTable.SHRINK_FLD].ToString().Trim());
				objObject.Ancestor = odrPCS[ITM_BOMTable.ANCESTOR_FLD].ToString().Trim();
				objObject.EffectiveEndDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString().Trim());
				objObject.EffectiveBeginDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString().Trim());
				objObject.Alternative = int.Parse(odrPCS[ITM_BOMTable.ALTERNATIVE_FLD].ToString().Trim());
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
		public object GetObjectVO(int pintProductID, int pintComponentID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ ITM_BOMTable.BOMID_FLD + ","
					+ ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+ " WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + ITM_BOMTable.COMPONENTID_FLD + "=" + pintComponentID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_BOMVO objObject = new ITM_BOMVO();

				while (odrPCS.Read())
				{
					if (odrPCS[ITM_BOMTable.BOMID_FLD] != DBNull.Value)
						objObject.BomID = int.Parse(odrPCS[ITM_BOMTable.BOMID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[ITM_BOMTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.COMPONENTID_FLD] != DBNull.Value)
						objObject.ComponentID = int.Parse(odrPCS[ITM_BOMTable.COMPONENTID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD] != DBNull.Value)
						objObject.EffectiveBeginDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD] != DBNull.Value)
						objObject.EffectiveEndDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD] != DBNull.Value)
						objObject.LeadTimeOffset = Decimal.Parse(odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.QUANTITY_FLD] != DBNull.Value)
						objObject.Quantity = Decimal.Parse(odrPCS[ITM_BOMTable.QUANTITY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.ROUTINGID_FLD] != DBNull.Value)
						objObject.RoutingID = int.Parse(odrPCS[ITM_BOMTable.ROUTINGID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.SHRINK_FLD] != DBNull.Value)
						objObject.Shrink = Decimal.Parse(odrPCS[ITM_BOMTable.SHRINK_FLD].ToString().Trim());
					objObject.Ancestor = odrPCS[ITM_BOMTable.ANCESTOR_FLD].ToString().Trim();
					if (odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD] != DBNull.Value)
						objObject.EffectiveEndDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD] != DBNull.Value)
						objObject.EffectiveBeginDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.ALTERNATIVE_FLD] != DBNull.Value)
						objObject.Alternative = int.Parse(odrPCS[ITM_BOMTable.ALTERNATIVE_FLD].ToString().Trim());
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
		public ArrayList GetComponentOfItem(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetComponentOfItem()";
			
			ArrayList arrObjects = new ArrayList();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ ITM_BOMTable.BOMID_FLD + ","
					+ ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+ " WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintID
					+ " AND ((" + ITM_BOMTable.ALTERNATIVE_FLD + "= (SELECT MIN("
					+ ITM_BOMTable.ALTERNATIVE_FLD + ") FROM " + ITM_BOMTable.TABLE_NAME
					+ " WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintID + "))"
					+ " OR (" + ITM_BOMTable.ALTERNATIVE_FLD + " IS NULL))";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					ITM_BOMVO objObject = new ITM_BOMVO();
					if (odrPCS[ITM_BOMTable.BOMID_FLD] != DBNull.Value)
						objObject.BomID = int.Parse(odrPCS[ITM_BOMTable.BOMID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.PRODUCTID_FLD] != DBNull.Value)
						objObject.ProductID = int.Parse(odrPCS[ITM_BOMTable.PRODUCTID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.COMPONENTID_FLD] != DBNull.Value)
						objObject.ComponentID = int.Parse(odrPCS[ITM_BOMTable.COMPONENTID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD] != DBNull.Value)
						objObject.EffectiveBeginDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD] != DBNull.Value)
						objObject.EffectiveEndDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD] != DBNull.Value)
						objObject.LeadTimeOffset = Decimal.Parse(odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.QUANTITY_FLD] != DBNull.Value)
						objObject.Quantity = Decimal.Parse(odrPCS[ITM_BOMTable.QUANTITY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.ROUTINGID_FLD] != DBNull.Value)
						objObject.RoutingID = int.Parse(odrPCS[ITM_BOMTable.ROUTINGID_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.SHRINK_FLD] != DBNull.Value)
						objObject.Shrink = Decimal.Parse(odrPCS[ITM_BOMTable.SHRINK_FLD].ToString().Trim());
					objObject.Ancestor = odrPCS[ITM_BOMTable.ANCESTOR_FLD].ToString().Trim();
					if (odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD] != DBNull.Value)
						objObject.EffectiveEndDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD] != DBNull.Value)
						objObject.EffectiveBeginDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString().Trim());
					if (odrPCS[ITM_BOMTable.ALTERNATIVE_FLD] != DBNull.Value)
						objObject.Alternative = int.Parse(odrPCS[ITM_BOMTable.ALTERNATIVE_FLD].ToString().Trim());
					arrObjects.Add(objObject);
				}
				arrObjects.TrimToSize();
				return arrObjects;
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
		public object GetObjectVO_ValidateAncestor(int pintProductID)
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
					+ ITM_BOMTable.BOMID_FLD + ","
					+ ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+" WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintProductID.ToString() 
					+ " and " + ITM_BOMTable.COMPONENTID_FLD + " = 0";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_BOMVO objObject = new ITM_BOMVO();

				while (odrPCS.Read())
				{ 
					objObject.BomID = int.Parse(odrPCS[ITM_BOMTable.BOMID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[ITM_BOMTable.PRODUCTID_FLD].ToString().Trim());
					objObject.ComponentID = int.Parse(odrPCS[ITM_BOMTable.COMPONENTID_FLD].ToString().Trim());
					objObject.EffectiveBeginDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
					objObject.EffectiveEndDate = DateTime.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
					objObject.LeadTimeOffset = Decimal.Parse(odrPCS[ITM_BOMTable.LEADTIMEOFFSET_FLD].ToString().Trim());
					objObject.Quantity = Decimal.Parse(odrPCS[ITM_BOMTable.QUANTITY_FLD].ToString().Trim());
					objObject.RoutingID = int.Parse(odrPCS[ITM_BOMTable.ROUTINGID_FLD].ToString().Trim());
					objObject.Shrink = Decimal.Parse(odrPCS[ITM_BOMTable.SHRINK_FLD].ToString().Trim());
					objObject.Ancestor = odrPCS[ITM_BOMTable.ANCESTOR_FLD].ToString().Trim();
					objObject.EffectiveEndDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString().Trim());
					objObject.EffectiveBeginDay = int.Parse(odrPCS[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString().Trim());
					objObject.Alternative = int.Parse(odrPCS[ITM_BOMTable.ALTERNATIVE_FLD].ToString().Trim());

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
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			ITM_BOMVO objObject = (ITM_BOMVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE ITM_BOM SET "
				+ ITM_BOMTable.PRODUCTID_FLD + "=   ?" + ","
				+ ITM_BOMTable.COMPONENTID_FLD + "=   ?" + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + "=   ?" + ","
				+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + "=   ?" + ","
				+ ITM_BOMTable.LEADTIMEOFFSET_FLD + "=   ?" + ","
				+ ITM_BOMTable.QUANTITY_FLD + "=   ?" + ","
				+ ITM_BOMTable.ROUTINGID_FLD + "=   ?" + ","
				+ ITM_BOMTable.SHRINK_FLD + "=   ?" + ","
				+ ITM_BOMTable.ANCESTOR_FLD + "=   ?" + ","
				+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + "=   ?" + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + "=   ?" + ","
				+ ITM_BOMTable.ALTERNATIVE_FLD + "=  ?"
				+" WHERE " + ITM_BOMTable.BOMID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.COMPONENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.COMPONENTID_FLD].Value = objObject.ComponentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].Value = objObject.EffectiveBeginDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEENDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDATE_FLD].Value = objObject.EffectiveEndDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.LEADTIMEOFFSET_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.LEADTIMEOFFSET_FLD].Value = objObject.LeadTimeOffset;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ROUTINGID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.ROUTINGID_FLD].Value = objObject.RoutingID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.SHRINK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_BOMTable.SHRINK_FLD].Value = objObject.Shrink;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ANCESTOR_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_BOMTable.ANCESTOR_FLD].Value = objObject.Ancestor;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEENDDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDAY_FLD].Value = objObject.EffectiveEndDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.EFFECTIVEBEGINDAY_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].Value = objObject.EffectiveBeginDay;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.ALTERNATIVE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.ALTERNATIVE_FLD].Value = objObject.Alternative;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_BOMTable.BOMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_BOMTable.BOMID_FLD].Value = objObject.BomID;


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
					+ ITM_BOMTable.BOMID_FLD + ","
					+ ITM_BOMTable.LINE_FLD + ","
					+ ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,ITM_BOMTable.TABLE_NAME);

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
				+ ITM_BOMTable.BOMID_FLD + ","
				+ ITM_BOMTable.LINE_FLD + ","
				+ ITM_BOMTable.PRODUCTID_FLD + ","
				+ ITM_BOMTable.COMPONENTID_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
				+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
				+ ITM_BOMTable.QUANTITY_FLD + ","
				+ ITM_BOMTable.ROUTINGID_FLD + ","
				+ ITM_BOMTable.SHRINK_FLD + ","
				+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
				+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
				+ ITM_BOMTable.ALTERNATIVE_FLD 
				+ "  FROM " + ITM_BOMTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,ITM_BOMTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
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
		public ArrayList GetAllParent()
		{
			const string METHOD_NAME = THIS + ".GetAllParent()";

			ArrayList arrObjects = new ArrayList();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT DISTINCT("
					+ ITM_BOMTable.PRODUCTID_FLD + ")"
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+ " WHERE " + ITM_BOMTable.PRODUCTID_FLD + " NOT IN (SELECT "
					+ ITM_BOMTable.COMPONENTID_FLD + " FROM " + ITM_BOMTable.TABLE_NAME + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					arrObjects.Add(int.Parse(odrPCS[ITM_BOMTable.PRODUCTID_FLD].ToString()));
				}
				arrObjects.TrimToSize();
				return arrObjects;
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
		public DataTable ListBomDetailOfProduct(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ ITM_BOMTable.BOMID_FLD + ","
					+ ITM_BOMTable.LINE_FLD + ","
					+ "A." + ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ "B." + ITM_ProductTable.CODE_FLD + ","
					+ "B." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ "C." + MST_UnitOfMeasureTable.CODE_FLD + " as StockCode,"
					+ "D." + ITM_CategoryTable.CODE_FLD + " as ITM_CategoryCode,"
					+ "E." + MST_PartyTable.CODE_FLD + " as MST_PartyCode,"
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ "A." + ITM_BOMTable.ROUTINGID_FLD + ","
					+ "(Select Step From ITM_Routing Where ITM_Routing.RoutingID = A.RoutingID) as Operation,"
					+ ITM_BOMTable.ALTERNATIVE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD 
					+ " FROM " + ITM_BOMTable.TABLE_NAME + " A inner join " + ITM_ProductTable.TABLE_NAME + " B on A.ComponentID = B.ProductID "
					+ " inner join " + MST_UnitOfMeasureTable.TABLE_NAME + " C on B." + ITM_ProductTable.STOCKUMID_FLD + " = C." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " left join "  + ITM_CategoryTable.TABLE_NAME + " D on B." + ITM_ProductTable.CATEGORYID_FLD + " = D." + ITM_CategoryTable.CATEGORYID_FLD
					+ " left join "  + MST_PartyTable.TABLE_NAME + " E on B." + ITM_ProductTable.PRIMARYVENDORID_FLD + " = E." + MST_PartyTable.PARTYID_FLD
					+ " WHERE A." + ITM_BOMTable.PRODUCTID_FLD + " = " + pintProductID.ToString()
					+ " ORDER BY Line";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,ITM_BOMTable.TABLE_NAME);

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
		public void CopyBOM(int pintSourceProductID, int pintDestinationProductID)
		{
			const string METHOD_NAME = THIS + ".CopyBOM()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO ITM_BOM ( "
					+ ITM_BOMTable.PRODUCTID_FLD + ","
					+ ITM_BOMTable.LINE_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD + ")"
					+ " SELECT " 
					+ pintDestinationProductID + ","
					+ ITM_BOMTable.LINE_FLD + ","
					+ ITM_BOMTable.COMPONENTID_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDATE_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDATE_FLD + ","
					+ ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
					+ ITM_BOMTable.QUANTITY_FLD + ","
					+ ITM_BOMTable.ROUTINGID_FLD + ","
					+ ITM_BOMTable.SHRINK_FLD + ","
					+ ITM_BOMTable.ANCESTOR_FLD + ","
					+ ITM_BOMTable.EFFECTIVEENDDAY_FLD + ","
					+ ITM_BOMTable.EFFECTIVEBEGINDAY_FLD + ","
					+ ITM_BOMTable.ALTERNATIVE_FLD 
					+ "  FROM " + ITM_BOMTable.TABLE_NAME 
					+ "  WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintSourceProductID;

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
		/// <summary>
		/// Get the required quantity of component in BOM
		/// </summary>
		/// <param name="pintProductID">Parent Product</param>
		/// <param name="pintComponentID">Component</param>
		/// <returns>Decimal</returns>
		public decimal GetRequiredQuantity (int pintProductID, int pintComponentID)
		{
			const string METHOD_NAME = THIS + ".GetRequiredQuantity()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT ISNULL("
					+ ITM_BOMTable.QUANTITY_FLD + ", 0)"
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+" WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pintProductID.ToString() 
					+ " AND " + ITM_BOMTable.COMPONENTID_FLD + "=" + pintComponentID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					return decimal.Parse(objResult.ToString());
				}
				else
					return decimal.Zero;
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
		/// Gets top level item of system
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetTopLevelItem(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetTopLevelItem()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ProductID, MakeItem, ISNULL(CostCenterRateMasterID, 0) CostCenterRateMasterID,"
					+ " Code, Description, Revision"
					// just for debug only
					//+ " FROM ITM_Product WHERE ProductID IN (1055,1017, 1054)";
					+ " FROM ITM_Product WHERE ProductID NOT IN"
					+ " (SELECT ComponentID FROM ITM_BOM)"
					+ " AND ITM_Product.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				DataTable dtbBOM = new DataTable(ITM_ProductTable.TABLE_NAME);
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbBOM);

				return dtbBOM;
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
		/// Gets Bom structure with item info
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetBOMStructure(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetBOMStructure()";
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ITM_BOM.ProductID AS ParentID, ITM_BOM.ComponentID AS ProductID,"
					+ " ISNULL(ITM_BOM.Quantity, 0) Quantity, Code, Description, Revision, "
					+ " ITM_Product.MakeItem, ISNULL(ITM_Product.CostCenterRateMasterID, 0) CostCenterRateMasterID"
					+ " FROM ITM_BOM JOIN ITM_Product ON ITM_BOM.ComponentID = ITM_Product.ProductID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID;
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
		public string GetComponentOfItem(string pstrProductID)
		{
			const string METHOD_NAME = THIS + ".GetComponentOfItem()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"SELECT "
					+ ITM_BOMTable.COMPONENTID_FLD
					+ " FROM " + ITM_BOMTable.TABLE_NAME
					+ " WHERE " + ITM_BOMTable.PRODUCTID_FLD + "=" + pstrProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				StringBuilder strResult = new StringBuilder();
				while (odrPCS.Read())
					strResult.Append(odrPCS[ITM_BOMTable.COMPONENTID_FLD].ToString()).Append(",");
				return strResult.ToString();
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

		public DataSet ListComponents()
		{
			const string METHOD_NAME = THIS + ".ListComponents()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT B.ProductID AS ParentID, P.ProductID,"
					+ " C.Code AS ITM_CategoryCode, P.Code AS PartNumber, P.Description AS PartName"
					+ " , P.Revision AS Model, U.Code AS UM, P.StockUMID"
					+ " FROM ITM_Product P JOIN ITM_BOM B ON P.ProductID = B.ComponentID"
					+ " LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " JOIN MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataSet dstData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstData);
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
		public DataSet ListComponents(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".ListComponents()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT B.ProductID AS ParentID, P.ProductID,"
					+ " C.Code AS ITM_CategoryCode, P.Code AS PartNumber, P.Description AS PartName"
					+ " , P.Revision AS Model, U.Code AS UM, P.StockUMID"
					+ " FROM ITM_Product P JOIN ITM_BOM B ON P.ProductID = B.ComponentID"
					+ " LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " JOIN MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID"
					+ " WHERE B.ProductID = " + pintProductID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataSet dstData = new DataSet();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstData);
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
        public DataSet ListBomItemOfProduct(int pintProductID)
        {
            const string METHOD_NAME = THIS + ".List()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + ITM_BOMTable.COMPONENTID_FLD + ","
                    + ITM_BOMTable.QUANTITY_FLD + ","
                    + ITM_BOMTable.SHRINK_FLD + ","
                    + ITM_BOMTable.LEADTIMEOFFSET_FLD + ","
                    + ITM_BOMTable.ROUTINGID_FLD + ","
                    + " null as " + PRO_WorkOrderBomDetailTable.WORKORDERBOMMASTERID_FLD
                    + " FROM " + ITM_BOMTable.TABLE_NAME
                    + " WHERE " + ITM_BOMTable.PRODUCTID_FLD + " = " + pintProductID;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

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
