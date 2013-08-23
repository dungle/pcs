using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_UMRateDS 
	{
		public MST_UMRateDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_UMRateDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_UMRate
		///    </Description>
		///    <Inputs>
		///        MST_UMRateVO       
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
				MST_UMRateVO objObject = (MST_UMRateVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_UMRate("
				+ MST_UMRateTable.SCALE_FLD + ","
				+ MST_UMRateTable.DESCRIPTION_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREOUTID_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREINID_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.SCALE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_UMRateTable.SCALE_FLD].Value = objObject.Scale;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_UMRateTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.UNITOFMEASUREOUTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_UMRateTable.UNITOFMEASUREOUTID_FLD].Value = objObject.UnitOfMeasureOutID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.UNITOFMEASUREINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_UMRateTable.UNITOFMEASUREINID_FLD].Value = objObject.UnitOfMeasureInID;


				
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
		///       This method uses to delete data from MST_UMRate
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
			strSql=	"DELETE " + MST_UMRateTable.TABLE_NAME + " WHERE  " + "UMRateID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_UMRate
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_UMRateVO
		///    </Outputs>
		///    <Returns>
		///       MST_UMRateVO
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
				+ MST_UMRateTable.UMRATEID_FLD + ","
				+ MST_UMRateTable.SCALE_FLD + ","
				+ MST_UMRateTable.DESCRIPTION_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREOUTID_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREINID_FLD
				+ " FROM " + MST_UMRateTable.TABLE_NAME
				+" WHERE " + MST_UMRateTable.UMRATEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_UMRateVO objObject = new MST_UMRateVO();

				while (odrPCS.Read())
				{ 
				objObject.UMRateID = int.Parse(odrPCS[MST_UMRateTable.UMRATEID_FLD].ToString().Trim());
				objObject.Scale = Decimal.Parse(odrPCS[MST_UMRateTable.SCALE_FLD].ToString().Trim());
				objObject.Description = odrPCS[MST_UMRateTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.UnitOfMeasureOutID = int.Parse(odrPCS[MST_UMRateTable.UNITOFMEASUREOUTID_FLD].ToString().Trim());
				objObject.UnitOfMeasureInID = int.Parse(odrPCS[MST_UMRateTable.UNITOFMEASUREINID_FLD].ToString().Trim());

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
		///       Compare two unit and determine if they are scalled together
		///    </Description>
		///    <Inputs>
		///        UnitID1, UnitID2       
		///    </Inputs>
		///    <Outputs>
		///       true or false
		///    </Outputs>
		///    <Returns>
		///       MST_UMRateVO
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

		public bool isTwoUnitOfMeasureScalled(int pintUnitID1, int pintUnitID2)
		{
			const string METHOD_NAME = THIS + ".isTwoUnitOfMeasureScalled()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT count(*)"
					+ " FROM " + MST_UMRateTable.TABLE_NAME
					+" WHERE (" + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintUnitID1
					+"      AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintUnitID2 + ")"
					+"      OR (" + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintUnitID2 
					+"      AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintUnitID1 + ")" ;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				int intTmp = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				if (intTmp > 0)
				{
					return true;
				}
				else
				{
					return false;
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


		//**************************************************************************              
		///    <Description>
		///       Compare two unit and determine if they are scalled together
		///    </Description>
		///    <Inputs>
		///        UnitID1, UnitID2       
		///    </Inputs>
		///    <Outputs>
		///       true or false
		///    </Outputs>
		///    <Returns>
		///       MST_UMRateVO
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

		public decimal GetTwoUnitOfMeasureRate(int pintUnitID1, int pintUnitID2)
		{
			const string METHOD_NAME = THIS + ".GetTwoUnitOfMeasureRate()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT " + MST_UMRateTable.SCALE_FLD 
					+ " FROM " + MST_UMRateTable.TABLE_NAME
					+" WHERE (" + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintUnitID1
					+"      AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintUnitID2 + ")"
					+"      OR (" + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintUnitID2 
					+"      AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintUnitID1 + ")" ;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == null)
				{
					return -1;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
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


		//**************************************************************************              
		///    <Description>
		///       get UMRate from BuyingUM and StockUM
		///    </Description>
		///    <Inputs>
		///        pintBuyingUMID, pintStockUMID       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       decimal
		///    </Returns>
		///    <Authors>
		///       SonHT
		///    </Authors>
		///    <History>
		///       29-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public decimal GetUMRate(int pintInUMID, int pintOutUMID)
		{
			const string METHOD_NAME = THIS + ".GetUMRate()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
//				strSql=	"SELECT ISNULL(" + MST_UMRateTable.SCALE_FLD + ", 0)"
//					+ " FROM " + MST_UMRateTable.TABLE_NAME
//					+ " WHERE " + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintInUMID
//					+ " AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintOutUMID;

				if(pintInUMID == pintOutUMID) return 1;

				strSql = "Declare @var decimal(20,10)"
					+ " set @var = (select isnull(scale,0) from mst_umrate where " + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintInUMID
								+ " AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintOutUMID + ")"
					+ " if @var IS NULL "
					+ " begin"
					+ " set @var = (select Case when scale is null or scale=0 then 0  else 1/scale end from mst_umrate where " + MST_UMRateTable.UNITOFMEASUREINID_FLD + "=" + pintOutUMID
								+ " AND " + MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=" + pintInUMID + ")"
					+ " end"
					+ " Select @var";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if(objResult == DBNull.Value)
				{
					return 0;
				}
				return Convert.ToDecimal(objResult);
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
		///       This method uses to update data to MST_UMRate
		///    </Description>
		///    <Inputs>
		///       MST_UMRateVO       
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

			MST_UMRateVO objObject = (MST_UMRateVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_UMRate SET "
				+ MST_UMRateTable.SCALE_FLD + "=   ?" + ","
				+ MST_UMRateTable.DESCRIPTION_FLD + "=   ?" + ","
				+ MST_UMRateTable.UNITOFMEASUREOUTID_FLD + "=   ?" + ","
				+ MST_UMRateTable.UNITOFMEASUREINID_FLD + "=  ?"
				+" WHERE " + MST_UMRateTable.UMRATEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.SCALE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_UMRateTable.SCALE_FLD].Value = objObject.Scale;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_UMRateTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.UNITOFMEASUREOUTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_UMRateTable.UNITOFMEASUREOUTID_FLD].Value = objObject.UnitOfMeasureOutID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.UNITOFMEASUREINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_UMRateTable.UNITOFMEASUREINID_FLD].Value = objObject.UnitOfMeasureInID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_UMRateTable.UMRATEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_UMRateTable.UMRATEID_FLD].Value = objObject.UMRateID;


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
		///       This method uses to get all data from MST_UMRate
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
				+ MST_UMRateTable.UMRATEID_FLD + ","
				+ MST_UMRateTable.SCALE_FLD + ","
				+ MST_UMRateTable.DESCRIPTION_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREOUTID_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREINID_FLD
					+ " FROM " + MST_UMRateTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_UMRateTable.TABLE_NAME);

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
				+ MST_UMRateTable.UMRATEID_FLD + ","
				+ MST_UMRateTable.SCALE_FLD + ","
				+ MST_UMRateTable.DESCRIPTION_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREOUTID_FLD + ","
				+ MST_UMRateTable.UNITOFMEASUREINID_FLD 
		+ "  FROM " + MST_UMRateTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_UMRateTable.TABLE_NAME);

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
