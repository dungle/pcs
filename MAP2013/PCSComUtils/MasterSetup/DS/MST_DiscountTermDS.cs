using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_DiscountTermDS 
	{
		public MST_DiscountTermDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_DiscountTermDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_DiscountTerm
		///    </Description>
		///    <Inputs>
		///        MST_DiscountTermVO       
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
		///       Tuesday, February 15, 2005
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
				MST_DiscountTermVO objObject = (MST_DiscountTermVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_DiscountTerm("
				+ MST_DiscountTermTable.CODE_FLD + ","
				+ MST_DiscountTermTable.DESCRIPTION_FLD + ","
				+ MST_DiscountTermTable.QUANTITY_FLD + ","
				+ MST_DiscountTermTable.RATE_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_DiscountTermTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_DiscountTermTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_DiscountTermTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_DiscountTermTable.RATE_FLD].Value = objObject.Rate;


				
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
		///       This method uses to delete data from MST_DiscountTerm
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
			strSql=	"DELETE " + MST_DiscountTermTable.TABLE_NAME + " WHERE  " + "DiscountTermID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_DiscountTerm
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_DiscountTermVO
		///    </Outputs>
		///    <Returns>
		///       MST_DiscountTermVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 15, 2005
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
				+ MST_DiscountTermTable.DISCOUNTTERMID_FLD + ","
				+ MST_DiscountTermTable.CODE_FLD + ","
				+ MST_DiscountTermTable.DESCRIPTION_FLD + ","
				+ MST_DiscountTermTable.QUANTITY_FLD + ","
				+ MST_DiscountTermTable.RATE_FLD
				+ " FROM " + MST_DiscountTermTable.TABLE_NAME
				+" WHERE " + MST_DiscountTermTable.DISCOUNTTERMID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_DiscountTermVO objObject = new MST_DiscountTermVO();

				while (odrPCS.Read())
				{ 
				objObject.DiscountTermID = int.Parse(odrPCS[MST_DiscountTermTable.DISCOUNTTERMID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_DiscountTermTable.CODE_FLD].ToString().Trim();
				objObject.Description = odrPCS[MST_DiscountTermTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.Quantity = Decimal.Parse(odrPCS[MST_DiscountTermTable.QUANTITY_FLD].ToString().Trim());
				objObject.Rate = Decimal.Parse(odrPCS[MST_DiscountTermTable.RATE_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_DiscountTerm
		///    </Description>
		///    <Inputs>
		///       MST_DiscountTermVO       
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

			MST_DiscountTermVO objObject = (MST_DiscountTermVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_DiscountTerm SET "
				+ MST_DiscountTermTable.CODE_FLD + "=   ?" + ","
				+ MST_DiscountTermTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_DiscountTermTable.QUANTITY_FLD + "=   ?" + ","
				+ MST_DiscountTermTable.RATE_FLD + "=  ?"
				+" WHERE " + MST_DiscountTermTable.DISCOUNTTERMID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_DiscountTermTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_DiscountTermTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_DiscountTermTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.RATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_DiscountTermTable.RATE_FLD].Value = objObject.Rate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_DiscountTermTable.DISCOUNTTERMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_DiscountTermTable.DISCOUNTTERMID_FLD].Value = objObject.DiscountTermID;


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
		///       This method uses to get all data from MST_DiscountTerm
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
		///       Tuesday, February 15, 2005
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
				+ MST_DiscountTermTable.DISCOUNTTERMID_FLD + ","
				+ MST_DiscountTermTable.CODE_FLD + ","
				+ MST_DiscountTermTable.DESCRIPTION_FLD + ","
				+ MST_DiscountTermTable.QUANTITY_FLD + ","
				+ MST_DiscountTermTable.RATE_FLD
					+ " FROM " + MST_DiscountTermTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_DiscountTermTable.TABLE_NAME);

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
		///       Tuesday, February 15, 2005
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
				+ MST_DiscountTermTable.DISCOUNTTERMID_FLD + ","
				+ MST_DiscountTermTable.CODE_FLD + ","
				+ MST_DiscountTermTable.DESCRIPTION_FLD + ","
				+ MST_DiscountTermTable.QUANTITY_FLD + ","
				+ MST_DiscountTermTable.RATE_FLD 
		+ "  FROM " + MST_DiscountTermTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_DiscountTermTable.TABLE_NAME);

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
