using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_PaymentTermDS 
	{
		public MST_PaymentTermDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_PaymentTermDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_PaymentTerm
		///    </Description>
		///    <Inputs>
		///        MST_PaymentTermVO       
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
		///       Tuesday, January 25, 2005
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
				MST_PaymentTermVO objObject = (MST_PaymentTermVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_PaymentTerm("
				+ MST_PaymentTermTable.CODE_FLD + ","
				+ MST_PaymentTermTable.TYPE_FLD + ","
				+ MST_PaymentTermTable.DESCRIPTION_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTPERCENT_FLD + ","
				+ MST_PaymentTermTable.NETDUEDATE_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTDATE_FLD + ","
				+ MST_PaymentTermTable.CCNID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.TYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DISCOUNTPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_PaymentTermTable.DISCOUNTPERCENT_FLD].Value = objObject.DiscountPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.NETDUEDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.NETDUEDATE_FLD].Value = objObject.NetDueDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DISCOUNTDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.DISCOUNTDATE_FLD].Value = objObject.DiscountDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.CCNID_FLD].Value = objObject.CCNID;


				
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
		///       This method uses to delete data from MST_PaymentTerm
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
			strSql=	"DELETE " + MST_PaymentTermTable.TABLE_NAME + " WHERE  " + "PaymentTermID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_PaymentTerm
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_PaymentTermVO
		///    </Outputs>
		///    <Returns>
		///       MST_PaymentTermVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
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
				+ MST_PaymentTermTable.PAYMENTTERMID_FLD + ","
				+ MST_PaymentTermTable.CODE_FLD + ","
				+ MST_PaymentTermTable.TYPE_FLD + ","
				+ MST_PaymentTermTable.DESCRIPTION_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTPERCENT_FLD + ","
				+ MST_PaymentTermTable.NETDUEDATE_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTDATE_FLD + ","
				+ MST_PaymentTermTable.CCNID_FLD
				+ " FROM " + MST_PaymentTermTable.TABLE_NAME
				+" WHERE " + MST_PaymentTermTable.PAYMENTTERMID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_PaymentTermVO objObject = new MST_PaymentTermVO();

				while (odrPCS.Read())
				{ 
				objObject.PaymentTermID = int.Parse(odrPCS[MST_PaymentTermTable.PAYMENTTERMID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_PaymentTermTable.CODE_FLD].ToString().Trim();
				objObject.Type = odrPCS[MST_PaymentTermTable.TYPE_FLD].ToString().Trim();
				objObject.Description = odrPCS[MST_PaymentTermTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.DiscountPercent = Decimal.Parse(odrPCS[MST_PaymentTermTable.DISCOUNTPERCENT_FLD].ToString().Trim());
				objObject.NetDueDate = int.Parse(odrPCS[MST_PaymentTermTable.NETDUEDATE_FLD].ToString().Trim());
				objObject.DiscountDate = int.Parse(odrPCS[MST_PaymentTermTable.DISCOUNTDATE_FLD].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS[MST_PaymentTermTable.CCNID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_PaymentTerm
		///    </Description>
		///    <Inputs>
		///       MST_PaymentTermVO       
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

			MST_PaymentTermVO objObject = (MST_PaymentTermVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_PaymentTerm SET "
				+ MST_PaymentTermTable.CODE_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.TYPE_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.DISCOUNTPERCENT_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.NETDUEDATE_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.DISCOUNTDATE_FLD + "=   ?" + ","
				+ MST_PaymentTermTable.CCNID_FLD + "=  ?"
				+" WHERE " + MST_PaymentTermTable.PAYMENTTERMID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.TYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_PaymentTermTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DISCOUNTPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_PaymentTermTable.DISCOUNTPERCENT_FLD].Value = objObject.DiscountPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.NETDUEDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.NETDUEDATE_FLD].Value = objObject.NetDueDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.DISCOUNTDATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.DISCOUNTDATE_FLD].Value = objObject.DiscountDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_PaymentTermTable.PAYMENTTERMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_PaymentTermTable.PAYMENTTERMID_FLD].Value = objObject.PaymentTermID;


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
		///       This method uses to get all data from MST_PaymentTerm
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
		///       Tuesday, January 25, 2005
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
				+ MST_PaymentTermTable.PAYMENTTERMID_FLD + ","
				+ MST_PaymentTermTable.CODE_FLD + ","
				+ MST_PaymentTermTable.TYPE_FLD + ","
				+ MST_PaymentTermTable.DESCRIPTION_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTPERCENT_FLD + ","
				+ MST_PaymentTermTable.NETDUEDATE_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTDATE_FLD + ","
				+ MST_PaymentTermTable.CCNID_FLD
					+ " FROM " + MST_PaymentTermTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_PaymentTermTable.TABLE_NAME);

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
		///       Tuesday, January 25, 2005
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
				+ MST_PaymentTermTable.PAYMENTTERMID_FLD + ","
				+ MST_PaymentTermTable.CODE_FLD + ","
				+ MST_PaymentTermTable.TYPE_FLD + ","
				+ MST_PaymentTermTable.DESCRIPTION_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTPERCENT_FLD + ","
				+ MST_PaymentTermTable.NETDUEDATE_FLD + ","
				+ MST_PaymentTermTable.DISCOUNTDATE_FLD + ","
				+ MST_PaymentTermTable.CCNID_FLD 
		+ "  FROM " + MST_PaymentTermTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_PaymentTermTable.TABLE_NAME);

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
