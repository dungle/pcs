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
	
	public class ITM_ShipToleranceDS 
	{
		public ITM_ShipToleranceDS()
		{
		}
		private const string THIS = "PCSComProduct.Items.DS.DS.ITM_ShipToleranceDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_ShipTolerance
		///    </Description>
		///    <Inputs>
		///        ITM_ShipToleranceVO       
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
		///       Wednesday, January 19, 2005
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
				ITM_ShipToleranceVO objObject = (ITM_ShipToleranceVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO ITM_ShipTolerance("
				+ ITM_ShipToleranceTable.CODE_FLD + ","
				+ ITM_ShipToleranceTable.DESCRIPTION_FLD + ","
				+ ITM_ShipToleranceTable.OVERQTY_FLD + ","
				+ ITM_ShipToleranceTable.UNDERQTY_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.OVERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.OVERQTY_FLD].Value = objObject.OverQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.UNDERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.UNDERQTY_FLD].Value = objObject.UnderQty;


				
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
		///       This method uses to delete data from ITM_ShipTolerance
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
			strSql=	"DELETE " + ITM_ShipToleranceTable.TABLE_NAME + " WHERE  " + "ShipToleranceID" + "=" + pintID.ToString();
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
		///       This method uses to get data from ITM_ShipTolerance
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_ShipToleranceVO
		///    </Outputs>
		///    <Returns>
		///       ITM_ShipToleranceVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, January 19, 2005
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
				+ ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD + ","
				+ ITM_ShipToleranceTable.CODE_FLD + ","
				+ ITM_ShipToleranceTable.DESCRIPTION_FLD + ","
				+ ITM_ShipToleranceTable.OVERQTY_FLD + ","
				+ ITM_ShipToleranceTable.UNDERQTY_FLD
				+ " FROM " + ITM_ShipToleranceTable.TABLE_NAME
				+" WHERE " + ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ShipToleranceVO objObject = new ITM_ShipToleranceVO();

				while (odrPCS.Read())
				{ 
				objObject.ShipToleranceID = int.Parse(odrPCS[ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD].ToString().Trim());
				objObject.Code = odrPCS[ITM_ShipToleranceTable.CODE_FLD].ToString().Trim();
				objObject.Description = odrPCS[ITM_ShipToleranceTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.OverQty = Decimal.Parse(odrPCS[ITM_ShipToleranceTable.OVERQTY_FLD].ToString().Trim());
				objObject.UnderQty = Decimal.Parse(odrPCS[ITM_ShipToleranceTable.UNDERQTY_FLD].ToString().Trim());

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
		///       This method uses to update data to ITM_ShipTolerance
		///    </Description>
		///    <Inputs>
		///       ITM_ShipToleranceVO       
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

			ITM_ShipToleranceVO objObject = (ITM_ShipToleranceVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE ITM_ShipTolerance SET "
				+ ITM_ShipToleranceTable.CODE_FLD + "=   ?" + ","
				+ ITM_ShipToleranceTable.DESCRIPTION_FLD + "=   ?" + ","
				+ ITM_ShipToleranceTable.OVERQTY_FLD + "=   ?" + ","
				+ ITM_ShipToleranceTable.UNDERQTY_FLD + "=  ?"
				+" WHERE " + ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.OVERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.OVERQTY_FLD].Value = objObject.OverQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.UNDERQTY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.UNDERQTY_FLD].Value = objObject.UnderQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD].Value = objObject.ShipToleranceID;


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
		///       This method uses to get all data from ITM_ShipTolerance
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
		///       Wednesday, January 19, 2005
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
				+ ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD + ","
				+ ITM_ShipToleranceTable.CODE_FLD + ","
				+ ITM_ShipToleranceTable.DESCRIPTION_FLD + ","
				+ ITM_ShipToleranceTable.OVERQTY_FLD + ","
				+ ITM_ShipToleranceTable.UNDERQTY_FLD
					+ " FROM " + ITM_ShipToleranceTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,ITM_ShipToleranceTable.TABLE_NAME);

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
		///       Wednesday, January 19, 2005
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
				+ ITM_ShipToleranceTable.SHIPTOLERANCEID_FLD + ","
				+ ITM_ShipToleranceTable.CODE_FLD + ","
				+ ITM_ShipToleranceTable.DESCRIPTION_FLD + ","
				+ ITM_ShipToleranceTable.OVERQTY_FLD + ","
				+ ITM_ShipToleranceTable.UNDERQTY_FLD 
		+ "  FROM " + ITM_ShipToleranceTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,ITM_ShipToleranceTable.TABLE_NAME);

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
