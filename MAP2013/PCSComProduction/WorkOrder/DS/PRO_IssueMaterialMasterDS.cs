using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComProduction.WorkOrder.DS
{
	public class PRO_IssueMaterialMasterDS 
	{
		public PRO_IssueMaterialMasterDS()
		{
		}
		private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_IssueMaterialMasterDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PRO_IssueMaterialMaster
		///    </Description>
		///    <Inputs>
		///        PRO_IssueMaterialMasterVO       
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
		///       Tuesday, May 31, 2005
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
				PRO_IssueMaterialMasterVO objObject = (PRO_IssueMaterialMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_IssueMaterialMaster("
				+ PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + ","
				+ PRO_IssueMaterialMasterTable.CCNID_FLD + ","
				+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD].Value = objObject.IssueMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUENO_FLD].Value = objObject.IssueNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;


				
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
		///       This method uses to add data to PRO_IssueMaterialMaster
		///    </Description>
		///    <Inputs>
		///        PRO_IssueMaterialMasterVO       
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
		///       Tuesday, May 31, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				PRO_IssueMaterialMasterVO objObject = (PRO_IssueMaterialMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PRO_IssueMaterialMaster("
					+ PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + ","
					+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + ","
					+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + ","
					+ PRO_IssueMaterialMasterTable.CCNID_FLD + ","
					+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + ","
					+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + ","
					+ PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD + ","
					+ PRO_IssueMaterialMasterTable.TOBINID_FLD + ","
					+ PRO_IssueMaterialMasterTable.ISSUEPURPOSEID_FLD + ","
					+ PRO_IssueMaterialMasterTable.SHIFTID_FLD + ","
					+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

				strSql += "; SELECT @@IDENTITY as NewID"; 

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUENO_FLD].Value = objObject.IssueNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				if (objObject.ProductID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.PRODUCTID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				if (objObject.WorkOrderMasterID > 0) 
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD].Value = DBNull.Value;
				}
				// HACK: Trada 27-10-2005
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.ToLocationID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD].Value = objObject.ToLocationID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.TOBINID_FLD, OleDbType.Integer));
				if (objObject.ToBinID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.TOBINID_FLD].Value = objObject.ToBinID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.TOBINID_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
				if (objObject.IssuePurposeID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUEPURPOSEID_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.SHIFTID_FLD, OleDbType.Integer));
				if (objObject.ShiftID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.SHIFTID_FLD].Value = objObject.ShiftID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.SHIFTID_FLD].Value = DBNull.Value;
				}
				 // END: Trada 27-10-2005
				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				if (objObject.WorkOrderDetailID > 0)
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;
				}
				else
				{
					ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD].Value = DBNull.Value;
				}

				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.CommandTimeout = 10000;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

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
		///       This method uses to delete data from PRO_IssueMaterialMaster
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
			strSql=	"DELETE " + PRO_IssueMaterialMasterTable.TABLE_NAME + " WHERE  " + "MasterLocationID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PRO_IssueMaterialMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PRO_IssueMaterialMasterVO
		///    </Outputs>
		///    <Returns>
		///       PRO_IssueMaterialMasterVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + ","
				+ PRO_IssueMaterialMasterTable.CCNID_FLD + ","
				+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD
				+ " FROM " + PRO_IssueMaterialMasterTable.TABLE_NAME
				+" WHERE " + PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_IssueMaterialMasterVO objObject = new PRO_IssueMaterialMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.MasterLocationID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD].ToString().Trim());
				objObject.IssueMaterialMasterID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD].ToString().Trim());
				objObject.PostDate = DateTime.Parse(odrPCS[PRO_IssueMaterialMasterTable.POSTDATE_FLD].ToString().Trim());
				objObject.IssueNo = odrPCS[PRO_IssueMaterialMasterTable.ISSUENO_FLD].ToString().Trim();
				objObject.CCNID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.CCNID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.PRODUCTID_FLD].ToString().Trim());
				objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD].ToString().Trim());
				objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD].ToString().Trim());

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
		///       This method uses to update data to PRO_IssueMaterialMaster
		///    </Description>
		///    <Inputs>
		///       PRO_IssueMaterialMasterVO       
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

			PRO_IssueMaterialMasterVO objObject = (PRO_IssueMaterialMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PRO_IssueMaterialMaster SET "
				+ PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.CCNID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + "=   ?" + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD + "=  ?"
				+" WHERE " + PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD].Value = objObject.IssueMaterialMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.ISSUENO_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.ISSUENO_FLD].Value = objObject.IssueNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


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
		///       This method uses to get all data from PRO_IssueMaterialMaster
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
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + ","
				+ PRO_IssueMaterialMasterTable.CCNID_FLD + ","
				+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD
					+ " FROM " + PRO_IssueMaterialMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueMaterialMasterTable.TABLE_NAME);

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
		///       Tuesday, May 31, 2005
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
				+ PRO_IssueMaterialMasterTable.MASTERLOCATIONID_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.POSTDATE_FLD + ","
				+ PRO_IssueMaterialMasterTable.ISSUENO_FLD + ","
				+ PRO_IssueMaterialMasterTable.CCNID_FLD + ","
				+ PRO_IssueMaterialMasterTable.PRODUCTID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERMASTERID_FLD + ","
				+ PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD 
		+ "  FROM " + PRO_IssueMaterialMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PRO_IssueMaterialMasterTable.TABLE_NAME);

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

		/// <summary>
		/// Get master issue and relate info
		/// </summary>
		/// <param name="pintIssueMasterID"></param>
		/// <returns></returns>
		public DataTable GetMasterIssue(int pintIssueMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT IMM.*, ML.CODE MASLOC, L.CODE LOCATION, B.CODE BIN, IP.Description ISSUEPURPOSE, Shift.ShiftDesc"
					+ " FROM PRO_IssueMaterialMaster IMM"
					+ " LEFT JOIN MST_MasterLocation ML ON IMM.MasterLocationID=ML.MasterLocationID"
					+ " LEFT JOIN MST_Location L ON IMM.ToLocationID=L.LocationID"
					+ " LEFT JOIN MST_BIN B ON IMM.ToBINID=B.BINID"
					+ " LEFT JOIN PRO_IssuePurpose IP ON IMM.IssuePurposeID=IP.IssuePurposeID"
					+ " LEFT JOIN PRO_Shift Shift ON IMM.ShiftID=Shift.ShiftID"
					+ " WHERE IssueMaterialMasterID=" + pintIssueMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueMaterialMasterTable.TABLE_NAME);

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
		/// 
		/// </summary>
		/// <returns></returns>
		public DataSet GetLocAndBin(string pstrLocs, string pstrBins)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = "SELECT LocationID, CODE"
					+ " FROM MST_Location WHERE LocationID IN " + pstrLocs + "; "
					+ " SELECT BinID, CODE"
					+ " FROM MST_BIN  WHERE BinID IN " + pstrBins + "; ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PRO_IssueMaterialMasterTable.TABLE_NAME);

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
	}
}
