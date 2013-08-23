using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.TableFrame.DS
{
	public class sys_TableGroupDS 
	{
		public sys_TableGroupDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.DS.sys_TableGroupDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_TableGroup
		///    </Description>
		///    <Inputs>
		///        sys_TableGroupVO       
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
		///       Monday, December 27, 2004
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
				sys_TableGroupVO objObject = (sys_TableGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + sys_TableGroupTable.TABLE_NAME + " ("
				+ sys_TableGroupTable.CODE_FLD + ","
				+ sys_TableGroupTable.TABLEGROUPNAME_FLD + ","
				+ sys_TableGroupTable.GROUPORDER_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.TABLEGROUPNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.TABLEGROUPNAME_FLD].Value = objObject.TableGroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;


				
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
	

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_TableGroup
		///    </Description>
		///    <Inputs>
		///        sys_TableGroupVO       
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
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public int AddAndReturnMaxID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				sys_TableGroupVO objObject = (sys_TableGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + sys_TableGroupTable.TABLE_NAME + " ("
					+ sys_TableGroupTable.CODE_FLD + ","
					+ sys_TableGroupTable.TABLEGROUPNAME_FLD + ","
					+ sys_TableGroupTable.GROUPORDER_FLD + ")"
					+ "VALUES(?,?,?)";

				//get the newest ID value
				strSql += " ; SELECT @@IDENTITY as MAXID";
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.TABLEGROUPNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.TABLEGROUPNAME_FLD].Value = objObject.TableGroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString()) ;	

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
	

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_TableGroup
		///    </Description>
		///    <Inputs>
		///        sys_TableGroupVO       
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
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public int AddGroupAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				sys_TableGroupVO objObject = (sys_TableGroupVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO " + sys_TableGroupTable.TABLE_NAME + " ("
					+ sys_TableGroupTable.CODE_FLD + ","
					+ sys_TableGroupTable.TABLEGROUPNAME_FLD + ","
					+ sys_TableGroupTable.GROUPORDER_FLD + ")"
					+ "VALUES(?,?,?); Select @@IDENTITY as NewID ";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.TABLEGROUPNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.TABLEGROUPNAME_FLD].Value = objObject.TableGroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE || 
					ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)	
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
		///       This method uses to delete data from sys_TableGroup
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
			string strSql = string.Empty;
			strSql = "DELETE " + sys_TableGroupTable.TABLE_NAME + 
					 " WHERE  " + "TableGroupID" + "=" + pintID.ToString();
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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from sys_TableGroup
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_TableGroupVO
		///    </Outputs>
		///    <Returns>
		///       sys_TableGroupVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
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
				+ sys_TableGroupTable.TABLEGROUPID_FLD + ","
				+ sys_TableGroupTable.CODE_FLD + ","
				+ sys_TableGroupTable.TABLEGROUPNAME_FLD + ","
				+ sys_TableGroupTable.GROUPORDER_FLD
				+ " FROM " + sys_TableGroupTable.TABLE_NAME
				+" WHERE " + sys_TableGroupTable.TABLEGROUPID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_TableGroupVO objObject = new sys_TableGroupVO();

				while (odrPCS.Read())
				{ 
				objObject.TableGroupID = int.Parse(odrPCS[sys_TableGroupTable.TABLEGROUPID_FLD].ToString());
				objObject.Code = odrPCS[sys_TableGroupTable.CODE_FLD].ToString();
				objObject.TableGroupName = odrPCS[sys_TableGroupTable.TABLEGROUPNAME_FLD].ToString();
				objObject.GroupOrder = int.Parse(odrPCS[sys_TableGroupTable.GROUPORDER_FLD].ToString());

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
		///       This method uses to update data to sys_TableGroup
		///    </Description>
		///    <Inputs>
		///       sys_TableGroupVO       
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

			sys_TableGroupVO objObject = (sys_TableGroupVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE " + sys_TableGroupTable.TABLE_NAME +  " SET "
				+ sys_TableGroupTable.CODE_FLD + "=   ?" + ","
				+ sys_TableGroupTable.TABLEGROUPNAME_FLD + "=   ?" + ","
				+ sys_TableGroupTable.GROUPORDER_FLD + "=  ?"
				+" WHERE " + sys_TableGroupTable.TABLEGROUPID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.CODE_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.TABLEGROUPNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[sys_TableGroupTable.TABLEGROUPNAME_FLD].Value = objObject.TableGroupName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.GROUPORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableGroupTable.GROUPORDER_FLD].Value = objObject.GroupOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableGroupTable.TABLEGROUPID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableGroupTable.TABLEGROUPID_FLD].Value = objObject.TableGroupID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE 
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE )
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
		///       This method uses to get all data from sys_TableGroup
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
		///       December 29, 2004
		///    </History>
		///    <Notes>
		///		Modified by NgocHT
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
				string strSql = "SELECT " + sys_TableGroupTable.TABLEGROUPID_FLD 
								+ "," + sys_TableGroupTable.CODE_FLD 
								+ "," +  sys_TableGroupTable.TABLEGROUPNAME_FLD
								+ "," +  sys_TableGroupTable.GROUPORDER_FLD 
								+ "  FROM " +sys_TableGroupTable.TABLE_NAME 
								+ " ORDER BY " + sys_TableGroupTable.GROUPORDER_FLD;
//								+ "," + sys_TableGroupTable.GROUPORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_TableGroupTable.TABLE_NAME);

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
		///       Monday, December 27, 2004
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
				+ sys_TableGroupTable.TABLEGROUPID_FLD + ","
				+ sys_TableGroupTable.CODE_FLD + ","
				+ sys_TableGroupTable.TABLEGROUPNAME_FLD + ","
				+ sys_TableGroupTable.GROUPORDER_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_TableGroupTable.TABLE_NAME);

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get the order of row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      the order of group
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MaxGroupOrder() 
		{
			const string METHOD_NAME = THIS + ".GroupOrder()";
			string strSql;
			strSql = "SELECT ISNULL (Max(" + sys_TableGroupTable.GROUPORDER_FLD  + "),0) FROM   " +
					 sys_TableGroupTable.TABLE_NAME ;

			int max;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				max = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return max;

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
		///       This method uses to get the min order of row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      position minimum of row
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MinGroupOrder() 
		{
			const string METHOD_NAME = THIS + ".GroupOrder()";
			string strSql;
			strSql = "SELECT ISNULL (MIN(" + sys_TableGroupTable.GROUPORDER_FLD  + "),0) FROM   " + 
					 sys_TableGroupTable.TABLE_NAME ;

			int min;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				min = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return min;

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
		///       This method uses to get the id of row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      the maximum of groupid
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int MaxGroupID() 
		{
			const string METHOD_NAME = THIS + ".MaxGroupID()";
			string strSql;
			strSql = "SELECT isnull (Max(" + sys_TableGroupTable.TABLEGROUPID_FLD  + "),0) FROM   " + sys_TableGroupTable.TABLE_NAME ;

			int max;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				max = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				return max;

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
		///       This method uses to get rows in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      row
		///    </Outputs>
		///    <Returns>
		///       array
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs()
		{
			ArrayList arrObjects = new ArrayList(); 
			const string METHOD_NAME = THIS + ".GetObjectVOs()";
			DataSet dstPCS = new DataSet();
			string strSql = "SELECT  " 
							+ sys_TableGroupTable.TABLEGROUPID_FLD 
							+ "," + sys_TableGroupTable.CODE_FLD
							+ "," + sys_TableGroupTable.TABLEGROUPNAME_FLD
							+ "," + sys_TableGroupTable.GROUPORDER_FLD
							+ " FROM " + sys_TableGroupTable.TABLE_NAME
							+ " ORDER BY " + sys_TableGroupTable.GROUPORDER_FLD;

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
					sys_TableGroupVO objGroupVO = new sys_TableGroupVO();
					objGroupVO.TableGroupID = int.Parse(odrPCS[sys_TableGroupTable.TABLEGROUPID_FLD].ToString());
					objGroupVO.Code = odrPCS[sys_TableGroupTable.CODE_FLD].ToString();
					objGroupVO.TableGroupName = odrPCS[sys_TableGroupTable.TABLEGROUPNAME_FLD].ToString();
					objGroupVO.GroupOrder = int.Parse(odrPCS[sys_TableGroupTable.GROUPORDER_FLD].ToString());

					arrObjects.Add(objGroupVO);
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
	}
}
