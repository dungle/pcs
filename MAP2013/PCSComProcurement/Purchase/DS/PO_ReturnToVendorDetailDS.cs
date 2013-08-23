using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComProcurement.Purchase.DS
{
	
	public class PO_ReturnToVendorDetailDS 
	{
		public PO_ReturnToVendorDetailDS()
		{
		}
		private const string THIS = "PCSComProcurement.Purchase.DS.PO_ReturnToVendorDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to PO_ReturnToVendorDetail
		///    </Description>
		///    <Inputs>
		///        PO_ReturnToVendorDetailVO       
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
		///       Wednesday, March 09, 2005
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
				PO_ReturnToVendorDetailVO objObject = (PO_ReturnToVendorDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO PO_ReturnToVendorDetail("
				+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
				+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
				+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
				+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
				+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
				+ PO_ReturnToVendorDetailTable.UMRATE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.MRB_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.MRB_FLD].Value = objObject.MRB;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.UMRATE_FLD].Value = objObject.UMRate;


				
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
		///       This method uses to delete data from PO_ReturnToVendorDetail
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
			strSql=	"DELETE " + PO_ReturnToVendorDetailTable.TABLE_NAME + " WHERE  " + "ReturnToVendorDetailID" + "=" + pintID.ToString();
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
		///       This method uses to get data from PO_ReturnToVendorDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_ReturnToVendorDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_ReturnToVendorDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, March 09, 2005
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
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
				+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
				+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
				+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
				+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
				+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
				+ PO_ReturnToVendorDetailTable.UMRATE_FLD
				+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME
				+" WHERE " + PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PO_ReturnToVendorDetailVO objObject = new PO_ReturnToVendorDetailVO();

				while (odrPCS.Read())
				{ 
				objObject.ReturnToVendorDetailID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString().Trim());
				objObject.Line = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.LINE_FLD].ToString().Trim());
				objObject.Quantity = Decimal.Parse(odrPCS[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString().Trim());
				objObject.Lot = odrPCS[PO_ReturnToVendorDetailTable.LOT_FLD].ToString().Trim();
				objObject.Serial = odrPCS[PO_ReturnToVendorDetailTable.SERIAL_FLD].ToString().Trim();
				objObject.StockUMID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString().Trim());
				objObject.BuyingUMID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString().Trim());
				objObject.MRB = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.MRB_FLD].ToString().Trim());
				objObject.LocationID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim());
				objObject.BinID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.BINID_FLD].ToString().Trim());
				objObject.ReturnToVendorMasterID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD].ToString().Trim());
				objObject.ProductID = int.Parse(odrPCS[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString().Trim());
				objObject.UMRate = Decimal.Parse(odrPCS[PO_ReturnToVendorDetailTable.UMRATE_FLD].ToString().Trim());

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
		///       This method uses to get data from PO_ReturnToVendorDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       PO_ReturnToVendorDetailVO
		///    </Outputs>
		///    <Returns>
		///       PO_ReturnToVendorDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public int GetMaxReturnToVendorDetailLine(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMaxReturnToVendorDetailLine()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT isnull(Max("
					+ PO_ReturnToVendorDetailTable.LINE_FLD + "),0) "
					+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME
					+" WHERE " + PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + "=" + pintReturnToVendorMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == null)
				{
					return 0;
				}
				else
				{
					if (objResult.ToString() != String.Empty)
					{
						return int.Parse(objResult.ToString());
					}
					else
					{
						return 0;
					}
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
		///       This method uses to update data to PO_ReturnToVendorDetail
		///    </Description>
		///    <Inputs>
		///       PO_ReturnToVendorDetailVO       
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

			PO_ReturnToVendorDetailVO objObject = (PO_ReturnToVendorDetailVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE PO_ReturnToVendorDetail SET "
				+ PO_ReturnToVendorDetailTable.LINE_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.LOT_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.SERIAL_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.MRB_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.BINID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + "=   ?" + ","
				+ PO_ReturnToVendorDetailTable.UMRATE_FLD + "=  ?"
				+" WHERE " + PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.MRB_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.MRB_FLD].Value = objObject.MRB;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD].Value = objObject.ReturnToVendorMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].Value = objObject.ReturnToVendorDetailID;


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
		///       This method uses to get all data from PO_ReturnToVendorDetail
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
		///       Wednesday, March 09, 2005
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
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
				+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
				+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
				+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
				+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
				+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
				+ PO_ReturnToVendorDetailTable.UMRATE_FLD
					+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_ReturnToVendorDetailTable.TABLE_NAME);

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

		public DataSet List(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT Line, ITM_Product.Code AS ITM_ProductCode, ITM_Product.Description, Revision,"
					+ " MST_UnitOfMeasure.Code AS MST_UnitOfMeasureCode, MST_Location.Code AS MST_LocationCode, MST_Bin.Code AS MST_BinCode,"
					+ " Quantity, UnitPrice, Amount, VATPercent, VATAmount, TotalAmount,"
					+ " RD.ReturnToVendorDetailID, ReturnToVendorMasterID, RD.ProductID,"
					+ " RD.StockUMID, RD.BuyingUMID, RefDetailID, RD.LocationID, RD.BinID, RD.UMRate"
					+ " FROM PO_ReturnToVendorDetail RD JOIN ITM_Product ON RD.ProductID = ITM_Product.ProductID"
					+ " JOIN MST_UnitOfMeasure ON RD.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " LEFT JOIN MST_Location ON RD.LocationID = MST_Location.LocationID"
					+ " LEFT JOIN MST_Bin ON RD.BinID = MST_Bin.BinID"
					+ " WHERE ReturnToVendorMasterID = " + pintMasterID
					+ " ORDER BY Line";
					Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_ReturnToVendorDetailTable.TABLE_NAME);

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

		public DataSet GetDetailByInvoiceMasterToReturn(int pintInvoiceMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByInvoiceMasterToReturn()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	" select 0 Line, P.Code ITM_ProductCode, P.Description, "
					+ " P.Revision, UM.Code MST_UnitOfMeasureCode, DL.Code MST_LocationCode, DB.Code MST_BinCode, "
					+ " 0.0 Quantity, IVD.UnitPrice, 0.0 Amount, "
					+ " IVD.VAT VatPercent, 0.0 VATAmount, 0.0 TotalAmount, "
					+ " 0 AS ReturnToVendorDetailID, 0 ReturnToVendorMasterID, IVD.ProductID, "
					+ " P.StockUMID, IVD.InvoiceUMID BuyingUMID, InvoiceDetailID RefDetaiLID,"
					+ " P.LocationID, P.BINID, (SELECT ISNULL(UMRate,1) FROM PO_PurchaseOrderDetail"
					+ " WHERE PurchaseOrderDetailID = IVD.PurchaseOrderDetailID) UMRate"
					+ " from PO_InvoiceDetail IVD  "
					+ " inner join ITM_Product P on P.ProductID = IVD.ProductID "
					+ " inner join MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = IVD.InvoiceUMID "
					+ " left join MST_Location DL on DL.LocationID = P.LocationID "
					+ " left join MST_BIN DB on DB.BINID = P.BINID "
					+ " WHERE IVD.InvoiceMasterID = " + pintInvoiceMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_ReturnToVendorDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from PO_ReturnToVendorDetail
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
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListReturnToVendorDetail(int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".ListReturnToVendorDetail()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.LINE_FLD + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " as ProductCode" + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD  + ","
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD  + ","
					
					+ "StockUnit." + MST_UnitOfMeasureTable.CODE_FLD + " as StockUnitCode " + ","
					+ "BuyingUnit." + MST_UnitOfMeasureTable.CODE_FLD + " as BuyingUnitCode " + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.UMRATE_FLD + ","
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " as LocationCode " + ","
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " as BinCode " + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.MRB_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.LOT_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
					+ "PO_ReturnToVendorDetail.UnitPrice, PO_ReturnToVendorDetail.Amount, PO_ReturnToVendorDetail.VATPercent, PO_ReturnToVendorDetail.VATAmount, "
					+ "PO_ReturnToVendorDetail.TotalAmount, "
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.BINID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.PRODUCTID_FLD 
					+ " FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME
					+ " Inner join " + ITM_ProductTable.TABLE_NAME + " ON " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD
					+ " left join " + MST_LocationTable.TABLE_NAME + " ON " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.LOCATIONID_FLD + "=" + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " left join " + MST_BINTable.TABLE_NAME + " ON " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.BINID_FLD + "=" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					+ " left join " + MST_UnitOfMeasureTable.TABLE_NAME + " as StockUnit ON " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.STOCKUMID_FLD + "=" + "StockUnit." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " left join " + MST_UnitOfMeasureTable.TABLE_NAME + " as BuyingUnit ON " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + "=" + "BuyingUnit." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " WHERE " + PO_ReturnToVendorDetailTable.TABLE_NAME + "." + PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + "=" + pintReturnToVendorMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,PO_ReturnToVendorDetailTable.TABLE_NAME);

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
		///       Wednesday, March 09, 2005
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
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
				+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
				+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOT_FLD + ","
				+ PO_ReturnToVendorDetailTable.SERIAL_FLD + ","
				+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
				+ PO_ReturnToVendorDetailTable.MRB_FLD + ","
				+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
				+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
				+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
				+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
				+ PO_ReturnToVendorDetailTable.UMRATE_FLD 
		+ "  FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_ReturnToVendorDetailTable.TABLE_NAME);

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
		/// UpdateDataSetForInvoice
		/// </summary>
		/// <param name="pData"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 11 2006</date>
		public void UpdateDataSetForInvoice(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetForInvoice()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			try
			{
				strSql=	"SELECT "
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
					+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
					+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
					+ PO_ReturnToVendorDetailTable.AMOUNT_FLD + ","
					+ PO_ReturnToVendorDetailTable.UNITPRICE_FLD + ","
					+ PO_ReturnToVendorDetailTable.VATAMOUNT_FLD + ","
					+ PO_ReturnToVendorDetailTable.VATPERCENT_FLD + ","
					+ PO_ReturnToVendorDetailTable.UMRATE_FLD + ","
					+ PO_ReturnToVendorDetailTable.REFDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD 
					+ "  FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_ReturnToVendorDetailTable.TABLE_NAME);
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
		///       Wednesday, March 09, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateReturnToVendorDataSet(DataSet pData, int pintReturnToVendorMasterID)
		{
			const string METHOD_NAME = THIS + ".UpdateReturnToVendorDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LINE_FLD + ","
					+ PO_ReturnToVendorDetailTable.QUANTITY_FLD + ","
					+ PO_ReturnToVendorDetailTable.STOCKUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BUYINGUMID_FLD + ","
					+ PO_ReturnToVendorDetailTable.LOCATIONID_FLD + ","
					+ PO_ReturnToVendorDetailTable.BINID_FLD + ","
					+ PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD + ","
					+ PO_ReturnToVendorDetailTable.PRODUCTID_FLD + ","
					+ PO_ReturnToVendorDetailTable.UMRATE_FLD + ","
					+ PO_ReturnToVendorDetailTable.UNITPRICE_FLD + ","
					+ PO_ReturnToVendorDetailTable.AMOUNT_FLD + ","
					+ PO_ReturnToVendorDetailTable.VATAMOUNT_FLD + ","
					+ PO_ReturnToVendorDetailTable.VATPERCENT_FLD + ","
					+ PO_ReturnToVendorDetailTable.REFDETAILID_FLD + ","
					+ PO_ReturnToVendorDetailTable.TOTALAMOUNT_FLD 
					+ "  FROM " + PO_ReturnToVendorDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,PO_ReturnToVendorDetailTable.TABLE_NAME);
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
		/// 
		/// </summary>
		/// <param name="pintInvoiceMasterID"></param>
		/// <returns></returns>
		public DataTable GetListOfReceivedProductsFromInvoice(int pintInvoiceMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT	a.PurchaseOrderDetailID, a.BuyingUMID, a.ProductID, a.Code, a.Revision, a.StockUMID, a.TotalReceive, a.Lot, a.Serial, a.PurchaseOrderMasterID, "
				         + "		a.TotalRemain, P.LocationID, L.Code AS LocationCode, L.Bin, B.BinID, B.Code AS BinCode, a.LotControl, a.Description,"
				         + "		a.UnitPrice, a.Amount, a.VATPercent, a.VATAmount, a.TotalAmount"
				         + "FROM    (SELECT    INVD.PurchaseOrderDetailID, POD.BuyingUMID, INVD.ProductID, "
				         + "				P.Code, P.Revision, P.Description, P.StockUMID, P.LotControl, "
				         + "				INVD.InvoiceQuantity TotalReceive, '' Lot, '' Serial,  "
				         + "				POD.PurchaseOrderMasterID, INVD.UnitPrice,  "
				         + "				(INVD.InvoiceQuantity * INVD.UnitPrice) Amount,	INVD.VAT VATPercent,  "
				         + "				(INVD.InvoiceQuantity * INVD.UnitPrice)* isnull(INVD.VAT,0) * 0.01 VATAmount, "
				         + "				(INVD.InvoiceQuantity * INVD.UnitPrice) +    "
				         + "				(INVD.InvoiceQuantity * INVD.UnitPrice) * isnull(INVD.VAT,0) * 0.01 TotalAmount,TotalRemain=INVD.InvoiceQuantity "
				         + "			FROM dbo.PO_InvoiceDetail INVD INNER JOIN "
				         + "			dbo.ITM_Product P ON INVD.ProductID = P.ProductID "
				         + "			INNER JOIN PO_PurchaseOrderDetail POD ON INVD.PurchaseOrderDetailID = POD.PurchaseOrderDetailID) a  "
				         + " LEFT OUTER JOIN ITM_Product P ON P.ProductID = a.ProductID LEFT OUTER JOIN "
				         + "				MST_Location L ON L.LocationID = P.LocationID LEFT OUTER JOIN "
				         + "				MST_BIN B ON B.BinID = P.BinID";
				
				//strSql = "SELECT * FROM v_PO_InvoiceDetailReceipt WHERE " + PO_InvoiceDetailTable.INVOICEMASTERID_FLD + " = " + pintInvoiceMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, PO_PurchaseOrderDetailTable.TABLE_NAME);

				return dstPCS.Tables[0];
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
