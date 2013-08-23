using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;

using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
namespace PCSComProduct.Items.DS
{
	public class ITM_HierarchyDS 
	{
		public ITM_HierarchyDS()
		{
		}
		private const string THIS = "PCSComProduct.Items.DS.ITM_HierarchyDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_Hierarchy
		///    </Description>
		///    <Inputs>
		///        ITM_HierarchyVO       
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
		///       Tuesday, March 15, 2005
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
				ITM_HierarchyVO objObject = (ITM_HierarchyVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO ITM_Hierarchy("
				+ ITM_HierarchyTable.SOURCE_FLD + ","
				+ ITM_HierarchyTable.DESTINATION_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_HierarchyTable.SOURCE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_HierarchyTable.SOURCE_FLD].Value = objObject.Source;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_HierarchyTable.DESTINATION_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_HierarchyTable.DESTINATION_FLD].Value = objObject.Des;


				
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
	

	

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from ITM_Hierarchy
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
			strSql=	"DELETE " + ITM_HierarchyTable.TABLE_NAME + " WHERE  " + "HierarchyID" + "=" + pintID.ToString();
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
		///       This method uses to get data from ITM_Hierarchy
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_HierarchyVO
		///    </Outputs>
		///    <Returns>
		///       ITM_HierarchyVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, March 15, 2005
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
				+ ITM_HierarchyTable.HIERARCHYID_FLD + ","
				+ ITM_HierarchyTable.SOURCE_FLD + ","
				+ ITM_HierarchyTable.DESTINATION_FLD
				+ " FROM " + ITM_HierarchyTable.TABLE_NAME
				+" WHERE " + ITM_HierarchyTable.HIERARCHYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_HierarchyVO objObject = new ITM_HierarchyVO();

				while (odrPCS.Read())
				{ 
				objObject.HierarchyID = int.Parse(odrPCS[ITM_HierarchyTable.HIERARCHYID_FLD].ToString().Trim());
				objObject.Source = int.Parse(odrPCS[ITM_HierarchyTable.SOURCE_FLD].ToString().Trim());
				objObject.Des = int.Parse(odrPCS[ITM_HierarchyTable.DESTINATION_FLD].ToString().Trim());

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
		///       This method uses to update data to ITM_Hierarchy
		///    </Description>
		///    <Inputs>
		///       ITM_HierarchyVO       
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

			ITM_HierarchyVO objObject = (ITM_HierarchyVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE ITM_Hierarchy SET "
				+ ITM_HierarchyTable.SOURCE_FLD + "=   ?" + ","
				+ ITM_HierarchyTable.DESTINATION_FLD + "=  ?"
				+" WHERE " + ITM_HierarchyTable.HIERARCHYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_HierarchyTable.SOURCE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_HierarchyTable.SOURCE_FLD].Value = objObject.Source;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_HierarchyTable.DESTINATION_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_HierarchyTable.DESTINATION_FLD].Value = objObject.Des;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_HierarchyTable.HIERARCHYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_HierarchyTable.HIERARCHYID_FLD].Value = objObject.HierarchyID;


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


		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from ITM_Hierarchy
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
		///       Tuesday, March 15, 2005
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
				+ ITM_HierarchyTable.HIERARCHYID_FLD + ","
				+ ITM_HierarchyTable.SOURCE_FLD + ","
				+ ITM_HierarchyTable.DESTINATION_FLD
					+ " FROM " + ITM_HierarchyTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,ITM_HierarchyTable.TABLE_NAME);

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
		///       This method uses to get all data from ITM_Hierarchy
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
		///       Tuesday, March 15, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable ListForProduct(int pintID)
		{
			const string METHOD_NAME = THIS + ".ListForProduct()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ ITM_HierarchyTable.HIERARCHYID_FLD + ","
					+ ITM_HierarchyTable.SOURCE_FLD + ","
					+ ITM_HierarchyTable.DESTINATION_FLD
					+ " FROM " + ITM_HierarchyTable.TABLE_NAME
					+ " WHERE " + ITM_HierarchyTable.SOURCE_FLD + " = " + pintID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,ITM_HierarchyTable.TABLE_NAME);

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
		///       Tuesday, March 15, 2005
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
				+ ITM_HierarchyTable.HIERARCHYID_FLD + ","
				+ ITM_HierarchyTable.SOURCE_FLD + ","
				+ ITM_HierarchyTable.DESTINATION_FLD 
		+ "  FROM " + ITM_HierarchyTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,ITM_HierarchyTable.TABLE_NAME);

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
		///       This method uses to get all data from ITM_Hierarchy
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
		///       Tuesday, March 15, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable ListParentOfProduct(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".List()";
			string STACK_TABLE_NAME = "#stack" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
			string PARENT_TABLE_NAME = "#parent" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	" Declare @currentItem int set @currentItem = " 
					+ pintProductID.ToString() + " DECLARE @level int  CREATE TABLE "
					+ STACK_TABLE_NAME + "(itemVisit int, level int) CREATE TABLE "
					+ PARENT_TABLE_NAME+ "(itemResult int) INSERT INTO " + STACK_TABLE_NAME + " VALUES (@currentItem, 1) SELECT @level = 1 "
					+ " WHILE @level > 0 BEGIN IF EXISTS (SELECT * FROM " + STACK_TABLE_NAME + " WHERE level = @level) BEGIN SELECT @currentItem = itemVisit FROM "
					+ STACK_TABLE_NAME + " WHERE level = @level Insert " + PARENT_TABLE_NAME + "(itemResult) values(@currentItem) DELETE FROM "
					+ STACK_TABLE_NAME + " WHERE level = @level AND itemVisit = @currentItem INSERT "+ STACK_TABLE_NAME + " SELECT Source, @level + 1 FROM ITM_Hierarchy "
					+ " WHERE Destination = @currentItem IF @@ROWCOUNT > 0  set @level = @level + 1 END ELSE set @level = @level - 1 END "
					+ " Select * from " + PARENT_TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_HierarchyTable.TABLE_NAME);
				ocmdPCS.Dispose();
				oconPCS.Close();

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

		public void CopyHierarchy(int pintSourceProductID, int pintDestinationProductID)
		{
			const string METHOD_NAME = THIS + ".CopyHierarchy()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO ITM_Hierarchy ( "
					+ ITM_HierarchyTable.SOURCE_FLD + ","
					+ ITM_HierarchyTable.DESTINATION_FLD  + " )" 
					+ " SELECT " 
					+ pintDestinationProductID + ","
					+ ITM_HierarchyTable.DESTINATION_FLD  
					+ " FROM " + ITM_HierarchyTable.TABLE_NAME 
					+ " WHERE " + ITM_HierarchyTable.SOURCE_FLD + "=" + pintSourceProductID;

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
	
	}
}
