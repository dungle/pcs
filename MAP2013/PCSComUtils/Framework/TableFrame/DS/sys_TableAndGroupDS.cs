using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.TableFrame.DS
{
	public class sys_TableAndGroupDS 
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.DS.sys_TableAndGroupDS";

		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				sys_TableAndGroupVO objObject = (sys_TableAndGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + sys_TableAndGroupTable.TABLE_NAME + " ( "
				+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
				+ sys_TableAndGroupTable.TABLEID_FLD + ","
				+ sys_TableAndGroupTable.TABLEORDER_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEGROUPID_FLD].Value = objObject.TableGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEID_FLD].Value = objObject.TableID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEORDER_FLD].Value = objObject.TableOrder;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
			string strSql = string.Empty;
			strSql = "DELETE " + sys_TableAndGroupTable.TABLE_NAME +
					 " WHERE  " + "TableID" + "=" + pintID.ToString();
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

		public void DeleteTableAndGroup(int pintTableID, int pintTableGroupID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = string.Empty;
			strSql = "DELETE " + sys_TableAndGroupTable.TABLE_NAME +
				" WHERE  " + sys_TableAndGroupTable.TABLEID_FLD + "=" + pintTableID.ToString()
				+ " AND " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "=" + pintTableGroupID.ToString();
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
				+ sys_TableAndGroupTable.TABLEANDGROUPID_FLD + ","
				+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
				+ sys_TableAndGroupTable.TABLEID_FLD + ","
				+ sys_TableAndGroupTable.TABLEORDER_FLD
				+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
				+" WHERE " + sys_TableAndGroupTable.TABLEANDGROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_TableAndGroupVO objObject = new sys_TableAndGroupVO();

				while (odrPCS.Read())
				{ 
				objObject.TableAndGroupID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEANDGROUPID_FLD].ToString());
				objObject.TableGroupID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEGROUPID_FLD].ToString());
				objObject.TableID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEID_FLD].ToString());
				objObject.TableOrder = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEORDER_FLD].ToString());

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

		public int CountTableInGroup(int pintTableID)
		{
			const string METHOD_NAME = THIS + ".CountTableInGroup()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT Count(*)"
					+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
					+" WHERE " + sys_TableAndGroupTable.TABLEID_FLD + "=" + pintTableID;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					return int.Parse(objResult.ToString());
				}
				else
				{
					return 0;
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
		public bool CheckIfTableExisted(int pintTableId,int pintTableGroupID)
		{
			const string METHOD_NAME = THIS + ".GetMaxTableOrder()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT count(*) "
					+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
					+ " WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "=" + pintTableGroupID
					+ "	AND " + sys_TableAndGroupTable.TABLEID_FLD + "=" + pintTableId;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				int intTableCount = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				if (intTableCount == 0)
				{
					return false;
				}else
				{
					return true;
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

		public int GetMaxTableOrder(int pintTableGroupID)
		{
			const string METHOD_NAME = THIS + ".GetMaxTableOrder()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT isnull(Max("
					+ sys_TableAndGroupTable.TABLEORDER_FLD 
					+ "),0) + 1 "
					+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
					+" WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "=" + pintTableGroupID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
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

			sys_TableAndGroupVO objObject = (sys_TableAndGroupVO) pobjObjecVO;
			
			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql =	"UPDATE sys_TableAndGroup SET "
							+ sys_TableAndGroupTable.TABLEGROUPID_FLD + "=   ?" + ","
							+ sys_TableAndGroupTable.TABLEID_FLD + "=   ?" + ","
							+ sys_TableAndGroupTable.TABLEORDER_FLD + "=  ?"
							+" WHERE " + sys_TableAndGroupTable.TABLEANDGROUPID_FLD + "= ?";


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEGROUPID_FLD].Value = objObject.TableGroupID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEID_FLD].Value = objObject.TableID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEORDER_FLD].Value = objObject.TableOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableAndGroupTable.TABLEANDGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableAndGroupTable.TABLEANDGROUPID_FLD].Value = objObject.TableAndGroupID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
				string strSql = "SELECT " + sys_TableAndGroupTable.TABLEANDGROUPID_FLD
								+ "," + sys_TableAndGroupTable.TABLEGROUPID_FLD
								+ "," +  sys_TableAndGroupTable.TABLEID_FLD
								+ "," +  sys_TableAndGroupTable.TABLEORDER_FLD
								+ "  FROM " +sys_TableAndGroupTable.TABLE_NAME 
								+ " ORDER BY " + sys_TableAndGroupTable.TABLEGROUPID_FLD 
								+ "," + sys_TableAndGroupTable.TABLEORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_TableAndGroupTable.TABLE_NAME);

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
				+ sys_TableAndGroupTable.TABLEANDGROUPID_FLD + ","
				+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
				+ sys_TableAndGroupTable.TABLEID_FLD + ","
				+ sys_TableAndGroupTable.TABLEORDER_FLD;


				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_TableAndGroupTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		public int GetGroupIDByTableID(int pintTableID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			string strSql = "SELECT  " + sys_TableAndGroupTable.TABLEGROUPID_FLD
							+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
							+ " WHERE " + sys_TableAndGroupTable.TABLEID_FLD + "=" 
							+ pintTableID.ToString();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				int mGroupID = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return mGroupID;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME,ex);
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

		public ArrayList GetObjectVOs(int pintTableGroupID)
		{
			ArrayList arrObjects = new ArrayList(); 
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			string strSql = "SELECT  " + sys_TableAndGroupTable.TABLEANDGROUPID_FLD 
							+ "," + sys_TableAndGroupTable.TABLEGROUPID_FLD
							+ "," + sys_TableAndGroupTable.TABLEID_FLD  
							+ "," + sys_TableAndGroupTable.TABLEORDER_FLD
							+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
							+ " WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "="
							+ pintTableGroupID.ToString()
							+ "ORDER BY " + sys_TableAndGroupTable.TABLEORDER_FLD;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				while (odrPCS.Read())
				{ 
					sys_TableAndGroupVO objAndVO = new sys_TableAndGroupVO();
					objAndVO.TableAndGroupID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEANDGROUPID_FLD].ToString());
					objAndVO.TableGroupID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEGROUPID_FLD].ToString());
					objAndVO.TableID = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEID_FLD].ToString());
					objAndVO.TableOrder = int.Parse(odrPCS[sys_TableAndGroupTable.TABLEORDER_FLD].ToString());

					arrObjects.Add(objAndVO);
				}		
				return arrObjects;					
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME,ex);
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

		public int GetTablePositionInGroup(int pintGroupID,int pintTableId)
		{
			const string METHOD_NAME = THIS + ".GetTablePositionInGroup()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT isnull("
					+ sys_TableAndGroupTable.TABLEORDER_FLD + ",0) "
					+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
					+" WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "=" + pintGroupID
					+" And  " + sys_TableAndGroupTable.TABLEID_FLD + "=" + pintTableId;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return  int.Parse(ocmdPCS.ExecuteScalar().ToString());
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

		public void CopyTwoGroup(int pintFromGroup, int pintToGroup)
		{
			const string METHOD_NAME = THIS + ".CopyTwoGroup()";
			string strSql = string.Empty;
			strSql=	"INSERT INTO " + sys_TableAndGroupTable.TABLE_NAME + "("
			+ sys_TableAndGroupTable.TABLEGROUPID_FLD + ","
			+ sys_TableAndGroupTable.TABLEID_FLD + ","
			+ sys_TableAndGroupTable.TABLEORDER_FLD + ")" 
			+ " SELECT " + pintToGroup + ","
			+ sys_TableAndGroupTable.TABLEID_FLD + ","
			+ sys_TableAndGroupTable.TABLEORDER_FLD 
			+ " FROM " + sys_TableAndGroupTable.TABLE_NAME
			+ " WHERE " + sys_TableAndGroupTable.TABLEGROUPID_FLD + "=" + pintFromGroup + "" ;

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
	}
}
