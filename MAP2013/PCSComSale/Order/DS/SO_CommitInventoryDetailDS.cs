using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;


namespace PCSComSale.Order.DS
{
	
	public class SO_CommitInventoryDetailDS
	{
		public SO_CommitInventoryDetailDS()
		{
		}

		private const string THIS = "PCSComSale.Order.DS.SO_CommitInventoryDetailDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///        SO_CommitInventoryDetailVO       
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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				SO_CommitInventoryDetailVO objObject = (SO_CommitInventoryDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_CommitInventoryDetail("
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)"; // added by dungla on TuanDM machine 09-May-2005

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if(objObject.MasterLocationID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD].Value = objObject.InspectionStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, OleDbType.Integer));
				if(objObject.CommitInventoryMasterID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].Value = objObject.CommitInventoryMasterID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				if(objObject.DeliveryScheduleID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				if(objObject.ProductID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.BINID_FLD, OleDbType.Integer));
				if(objObject.BinID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.BINID_FLD].Value = objObject.BinID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.BINID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				if(objObject.LocationID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PACKED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PACKED_FLD].Value = objObject.Packed;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
				if(objObject.SellingUMID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				if(objObject.StockUMID > 0)
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				else
					ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STOCKUMID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STDCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STDCOST_FLD].Value = objObject.STDCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SHIPPED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SHIPPED_FLD].Value = objObject.Shipped;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SHIPDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SHIPDATE_FLD].Value = objObject.ShipDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD].Value = objObject.CostOfGoodsSold;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CommitInventoryDetail and return new ID
		///    </Description>
		///    <Inputs>
		///        SO_CommitInventoryDetailVO 
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       01-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
/*		public int AddReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddReturnID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				SO_CommitInventoryDetailVO objObject = (SO_CommitInventoryDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO SO_CommitInventoryDetail("
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].Value = objObject.CommitInventoryDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD].Value = objObject.InspectionStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].Value = objObject.CommitInventoryMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PACKED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PACKED_FLD].Value = objObject.Packed;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STDCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STDCOST_FLD].Value = objObject.STDCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD].Value = objObject.CostOfGoodsSold;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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
		}*/


		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from SO_CommitInventoryDetail
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
		///       Tuandm : Repair 04-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + SO_CommitInventoryDetailTable.TABLE_NAME + " WHERE  " + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + "=" + pintID.ToString();
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
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to SO_CommitInventoryDetail and return new ID
		///    </Description>
		///    <Inputs>
		///        SO_CommitInventoryDetailVO 
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       01-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		/*		public int AddReturnID(object pobjObjectVO)
				{
					const string METHOD_NAME = THIS + ".AddReturnID()";

					OleDbConnection oconPCS = null;
					OleDbCommand ocmdPCS = null;
					try
					{
						SO_CommitInventoryDetailVO objObject = (SO_CommitInventoryDetailVO) pobjObjectVO;
						string strSql = String.Empty;
						Utils utils = new Utils();
						oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
						ocmdPCS = new OleDbCommand("", oconPCS);

						strSql = "INSERT INTO SO_CommitInventoryDetail("
							+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
							+ SO_CommitInventoryDetailTable.LINE_FLD + ","
							+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
							+ SO_CommitInventoryDetailTable.LOT_FLD + ","
							+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
							+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
							+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
							+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
							+ SO_CommitInventoryDetailTable.BINID_FLD + ","
							+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
							+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
							+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
							+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
							+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
							+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
							+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
							+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ")"
							+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].Value = objObject.CommitInventoryDetailID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LINE_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LINE_FLD].Value = objObject.Line;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD].Value = objObject.InspectionStatus;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOT_FLD, OleDbType.VarWChar));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOT_FLD].Value = objObject.Lot;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].Value = objObject.CommitInventoryMasterID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.BINID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.BINID_FLD].Value = objObject.BinID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOCATIONID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SERIAL_FLD, OleDbType.VarWChar));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SERIAL_FLD].Value = objObject.Serial;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PACKED_FLD, OleDbType.Boolean));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PACKED_FLD].Value = objObject.Packed;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.UMRATE_FLD, OleDbType.Decimal));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.UMRATE_FLD].Value = objObject.UMRate;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STOCKUMID_FLD, OleDbType.Integer));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STDCOST_FLD, OleDbType.Decimal));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STDCOST_FLD].Value = objObject.STDCost;

						ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, OleDbType.Decimal));
						ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD].Value = objObject.CostOfGoodsSold;


						ocmdPCS.CommandText = strSql;
						ocmdPCS.Connection.Open();
						ocmdPCS.ExecuteNonQuery();

					}
					catch (OleDbException ex)
					{
						if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						{
							throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
						}
						else
						{
							throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
						}
					}

					catch (InvalidOperationException ex)
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
				}*/


		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///        CommitMasterID       
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       16-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void DeleteByMaster(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + SO_CommitInventoryDetailTable.TABLE_NAME + " WHERE  " + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + pintID.ToString();
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
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CommitInventoryDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CommitInventoryDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				SO_CommitInventoryDetailVO objObject = new SO_CommitInventoryDetailVO();

				while (odrPCS.Read())
				{
					objObject.MasterLocationID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.CommitInventoryDetailID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].ToString().Trim());
					objObject.Line = int.Parse(odrPCS[SO_CommitInventoryDetailTable.LINE_FLD].ToString().Trim());
					objObject.InspectionStatus = int.Parse(odrPCS[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD].ToString().Trim());
					objObject.Lot = odrPCS[SO_CommitInventoryDetailTable.LOT_FLD].ToString().Trim();
					objObject.CommitQuantity = Decimal.Parse(odrPCS[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].ToString().Trim());
					objObject.CommitInventoryMasterID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].ToString().Trim());
					objObject.DeliveryScheduleID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.PRODUCTID_FLD].ToString().Trim());
					objObject.BinID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.BINID_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.CCNID_FLD].ToString().Trim());
					objObject.LocationID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.LOCATIONID_FLD].ToString().Trim());
					objObject.Serial = odrPCS[SO_CommitInventoryDetailTable.SERIAL_FLD].ToString().Trim();
					objObject.Packed = bool.Parse(odrPCS[SO_CommitInventoryDetailTable.PACKED_FLD].ToString().Trim());
					objObject.Shipped = bool.Parse(odrPCS[SO_CommitInventoryDetailTable.SHIPPED_FLD].ToString().Trim());
					objObject.UMRate = Decimal.Parse(odrPCS[SO_CommitInventoryDetailTable.UMRATE_FLD].ToString().Trim());
					objObject.SellingUMID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[SO_CommitInventoryDetailTable.STOCKUMID_FLD].ToString().Trim());
					objObject.STDCost = Decimal.Parse(odrPCS[SO_CommitInventoryDetailTable.STDCOST_FLD].ToString().Trim());
					objObject.CostOfGoodsSold = Decimal.Parse(odrPCS[SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD].ToString().Trim());
					objObject.ShipDate = DateTime.Parse(odrPCS[SO_CommitInventoryDetailTable.SHIPDATE_FLD].ToString().Trim());

				}
				return objObject;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CommitInventoryDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CommitInventoryDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetCostOfGoodsSold(int pintProductID, string strLot)
		{
			const string METHOD_NAME = THIS + ".GetCostOfGoodsSold()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.PRODUCTID_FLD + "= ? "
					+ " AND " + SO_CommitInventoryDetailTable.LOT_FLD + "= ? " ;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = pintProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOT_FLD].Value = strLot;

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if ((objResult == DBNull.Value) || (objResult == null))
				{
					return -1 ;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
				}
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from SO_CommitInventoryDetail by CommitmentMaster
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       SO_CommitInventoryDetailVO
		///    </Outputs>
		///    <Returns>
		///       SO_CommitInventoryDetailVO
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       23-Feb-2005
		///       21-Apr-2005: update entire method
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetDetailByMaster(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetDetailByMaster()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.LINE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ", "
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.STOCKUMID_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.BINID_FLD + ", "
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " JOIN " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD 
					+ " = " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
					+ " JOIN " + ITM_ProductTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD 
					+ " = " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.PRODUCTID_FLD
					+ " JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD 
					+ "=" + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD
					+ " WHERE " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + pintMasterID;

				// create a data table to hold data
				DataTable dtbData = new DataTable(SO_CommitInventoryDetailTable.TABLE_NAME);
				dtbData.Columns.Add(new DataColumn(MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LINE_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.PRODUCTID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.STOCKUMID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(IV_MasLocCacheTable.OHQUANTITY_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.UMRATE_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LOCATIONID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.BINID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LOT_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SERIAL_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.CCNID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.PACKED_FLD, typeof(bool)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.STDCOST_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SHIPPED_FLD, typeof(bool)));

				// primary key
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].Unique = true;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrement = true;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrementSeed = 1;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrementStep = 1;

				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD];
				dtbData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				dstPCS.Tables.Add(dtbData);
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to SO_CommitInventoryDetail
		///    </Description>
		///    <Inputs>
		///       SO_CommitInventoryDetailVO       
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

			SO_CommitInventoryDetailVO objObject = (SO_CommitInventoryDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE SO_CommitInventoryDetail SET "
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + "=   ?" + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + "=  ?"
					+ " WHERE " + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].Value = objObject.CommitInventoryDetailID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LINE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LINE_FLD].Value = objObject.Line;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD].Value = objObject.InspectionStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].Value = objObject.CommitQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].Value = objObject.CommitInventoryMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD].Value = objObject.DeliveryScheduleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SERIAL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SERIAL_FLD].Value = objObject.Serial;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.PACKED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.PACKED_FLD].Value = objObject.Packed;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.UMRATE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.UMRATE_FLD].Value = objObject.UMRate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.STDCOST_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.STDCOST_FLD].Value = objObject.STDCost;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SHIPPED_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SHIPPED_FLD].Value = objObject.Shipped;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.SHIPDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.SHIPDATE_FLD].Value = objObject.ShipDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD].Value = objObject.CostOfGoodsSold;

				ocmdPCS.Parameters.Add(new OleDbParameter(SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail
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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		public DataSet List(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + pintMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       List data by SOMasterID for Cancel commitment
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, March 3, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCancelable(int pintSaleOrderMasterID)
		{
			const string METHOD_NAME = THIS + ".List()";
			const string DELIVERY = "Delivery";
			const string UNITOFMEASURE = "Measure";
			const string MASTERLOCATION = "MasterLocation";
			const string LOCATION = "Location";
			const string BIN = "Bin";
			const string PRODUCTCODE = "ProductCode";
			const string PRODUCTDES = "ProductDes";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ "C.Code,"
					+ "H." + SO_DeliveryScheduleTable.LINE_FLD + " as " + DELIVERY + ","
					+ "H." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD  + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ "(Select " + ITM_ProductTable.CODE_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as "  + PRODUCTCODE + "," 
					+ "(Select " + ITM_ProductTable.DESCRIPTION_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as "  + PRODUCTDES + "," 
					+ "(Select " + ITM_ProductTable.REVISION_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as "  + ITM_ProductTable.REVISION_FLD + "," 
					+ "(Select " + ITM_ProductTable.REVISION_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as "  + ITM_ProductTable.REVISION_FLD + "," 
					+ "(Select " + ITM_ProductTable.STOCKUMID_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as "  + ITM_ProductTable.STOCKUMID_FLD + "," 
					
					//+ "SODel." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + " as " + SO_CommitInventoryDetailTable.LINE_FLD  + ","
					+ "(Select " + MST_UnitOfMeasureTable.CODE_FLD + " From " + MST_UnitOfMeasureTable.TABLE_NAME + " Where " + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + " = A." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ") as "  + UNITOFMEASURE + "," 
					+ "(Select " + MST_MasterLocationTable.CODE_FLD + " From " + MST_MasterLocationTable.TABLE_NAME + " Where " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = A." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ") as "  + MASTERLOCATION + "," 
					+ "(Select " + MST_LocationTable.CODE_FLD + " From " + MST_LocationTable.TABLE_NAME + " Where " + MST_LocationTable.LOCATIONID_FLD + " = A." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + ") as "  + LOCATION + "," 
					+ "(Select " + MST_BINTable.CODE_FLD + " From " + MST_BINTable.TABLE_NAME + " Where " + MST_BINTable.BINID_FLD + " = A." + SO_CommitInventoryDetailTable.BINID_FLD + ") as "  + BIN + "," 
					
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD+ ","
					+ "A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + "," 
					+ "A." + SO_CommitInventoryDetailTable.BINID_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME + " A "
					+ " Inner join " + SO_CommitInventoryMasterTable.TABLE_NAME + " B on A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + " = B." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD
					+ " Inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " C on B." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + " = C." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " Inner join " + SO_DeliveryScheduleTable.TABLE_NAME + " H on A." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + " = H." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ " Inner join " + SO_SaleOrderDetailTable.TABLE_NAME + " SODel on SODel." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = H." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " WHERE B." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = " + pintSaleOrderMasterID.ToString() + " and (A.Shipped = 0 or A.Shipped is null) ";
				//+ " and (A.Packed = 0 or A.Packed is null)"
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			
			try
			{
				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + "," 
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD
					+ "  FROM " + SO_CommitInventoryDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_CommitInventoryDetailTable.TABLE_NAME);
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 0)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (InvalidOperationException ex)
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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForPackList(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD
					+ "  FROM " + SO_CommitInventoryDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_CommitInventoryDetailTable.TABLE_NAME);
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
		///       Wednesday, February 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForPackListConfirm(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD
					+ "  FROM " + SO_CommitInventoryDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_CommitInventoryDetailTable.TABLE_NAME);
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
		///       Create condition string to search item to packlist by all condition
		///    </Description>
		///    <Inputs>
		///		 pstrParams, array of string contains list parameters
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, February 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static string CreateConditionForSearchPackList(int pintMasLocID, DateTime pdtmScheduleDate, string pstrBeginSO, string pstrEndSO, bool pblnHasDate)
		{
			if (pintMasLocID == 0)
			{
				return " 1 = 0";
			}
			pstrBeginSO = pstrBeginSO.Replace("'", "''");
			pstrEndSO   = pstrEndSO.Replace("'", "''");
			
			string strCondition = string.Empty;
			strCondition = "(PACKED " + " = 0 or PACKED is null) and ";
			strCondition += " CMDetail." + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " + pintMasLocID + " and ";
			if (pblnHasDate)
			{
				strCondition += "DelSchel." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " = ? and ";
			}
			if ((pstrBeginSO != string.Empty) && (pstrEndSO != string.Empty))
			{
				strCondition += "SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " >= '" + pstrBeginSO + "' and ";
				strCondition += "SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " <= '" + pstrEndSO + "'";
			}
			else if ((pstrBeginSO != string.Empty) && (pstrEndSO == string.Empty))
			{
				strCondition += "SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " = '" + pstrBeginSO + "'";
			}
			else if ((pstrBeginSO == string.Empty) && (pstrEndSO != string.Empty))
			{
				strCondition += "SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " <= '" + pstrEndSO + "'";
			}
			else if ((pstrBeginSO == string.Empty) && (pstrEndSO == string.Empty))
			{
				strCondition += " 1 = 1";
			}
			
			return strCondition;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail don't pack of query string
		///    </Description>
		///    <Inputs>
		///       pstrQueryString : contains infomation for get data
		///			
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListItemsNotPackList(int pintMasLocID, DateTime pdtmSchedule, string pstrBeginSO, string pstrEndSO, int pintPartyID, bool pblnHasDate)
		{
			string strCondition = CreateConditionForSearchPackList(pintMasLocID, pdtmSchedule, pstrBeginSO, pstrEndSO, pblnHasDate);
			const string METHOD_NAME = THIS + ".ListItemsNotPackList()";
			const string BIN_NAME = "BinName", LOCATION_NAME = "LocationName", PRODUCT_NAME = "ProductName";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "Select CMDetail." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD 
					+ ", CMDetail." + SO_CommitInventoryDetailTable.LOCATIONID_FLD 
					+ ", CMDetail." + SO_CommitInventoryDetailTable.BINID_FLD 
					+ ", CMDetail." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD 
					+ ", DelSchel." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ", null as " + SO_PackListMasterTable.PACKLISTMASTERID_FLD 
					+ ", null as " + SO_PackListDetailTable.PACKLISTDETAILID_FLD 
					+ ", SOMaster." + SO_SaleOrderMasterTable.CODE_FLD + " as " + SO_SaleOrderMasterTable.TABLE_NAME + SO_SaleOrderMasterTable.CODE_FLD + ","
                    + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
					+ "CMDetail." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + "," 
					+ "(Select " + ITM_ProductTable.CODE_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD 
					+ " = CMDetail." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.CODE_FLD + ","
					+ "(Select " + ITM_ProductTable.DESCRIPTION_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD 
					+ " = CMDetail." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as " + PRODUCT_NAME + ","
					+ "(Select " + ITM_ProductTable.REVISION_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD 
					+ " = CMDetail." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.REVISION_FLD + ","
					+ "(Select " + MST_UnitOfMeasureTable.CODE_FLD + " From " + MST_UnitOfMeasureTable.TABLE_NAME + " Where " + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD 
					+ " = SODel." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ") as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD
					+ ", CMDetail." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD 
					+ ",(Select " + MST_LocationTable.NAME_FLD + " From " + MST_LocationTable.TABLE_NAME 
					+ " Where " + MST_LocationTable.LOCATIONID_FLD + " = CMDetail." 
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ") as " + LOCATION_NAME
					+ ",(Select " + MST_BINTable.NAME_FLD + " From " + MST_BINTable.TABLE_NAME + " Where " + MST_BINTable.BINID_FLD + " = CMDetail." 
					+ SO_CommitInventoryDetailTable.BINID_FLD + ") as " + BIN_NAME + ","
					+ "(Select " + MST_PartyTable.NAME_FLD + " From " + MST_PartyTable.TABLE_NAME + " Where " + MST_PartyTable.PARTYID_FLD 
					+ " = SOMaster." + SO_SaleOrderMasterTable.PARTYID_FLD + ") as " + MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD
					+ ",Null as Lot, null as Serial "
					+ " From " + SO_CommitInventoryDetailTable.TABLE_NAME + " CMDetail inner join " + SO_DeliveryScheduleTable.TABLE_NAME + " DelSchel on CMDetail."
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + " = DelSchel." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ " Inner join " + SO_CommitInventoryMasterTable.TABLE_NAME + " CMMaster on CMDetail." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD 
					+ " = CMMaster." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD
					+ " Inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " SOMaster on CMMaster." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD 
					+ " = SOMaster." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " Inner join " + SO_SaleOrderDetailTable.TABLE_NAME + " SODel on SODel." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = DelSchel." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " WHERE " + strCondition + " and ( CMDetail." + SO_CommitInventoryDetailTable.SHIPPED_FLD + " = 0 or CMDetail." + SO_CommitInventoryDetailTable.SHIPPED_FLD +  " is Null )" 
					+ " and (SOMaster." + SO_SaleOrderMasterTable.PARTYID_FLD + "=" + pintPartyID + " or " + pintPartyID + "=" +  0.ToString() + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				if (pblnHasDate)
				{
					ocmdPCS.Parameters.Add(SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.DBDate);
					ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = pdtmSchedule;
				}
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_PackListDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail don't pack by PackListMaster
		///    </Description>
		///    <Inputs>
		///       pstrParams : contains PackListMasterID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCommitIventoryByPackListMaster(int pintPackListMasterID)
		{
			const string METHOD_NAME = THIS + ".ListCommitIventoryByPackListMaster()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ "CMDetail." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ "CMDetail." + SO_CommitInventoryDetailTable.PACKED_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME + " CMDetail "
					+ " inner join " + SO_PackListDetailTable.TABLE_NAME + " PLDetail on PLDetail." + SO_PackListDetailTable.COMMITINVENTORYDETAILID_FLD + " = CMDetail." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD
					+ " inner join " + SO_PackListMasterTable.TABLE_NAME + " PLMaster on PLMaster." + SO_PackListDetailTable.PACKLISTMASTERID_FLD + " = PLDetail." + SO_PackListMasterTable.PACKLISTMASTERID_FLD
					+ " WHERE PLDetail." + SO_PackListMasterTable.PACKLISTMASTERID_FLD + " = " + pintPackListMasterID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail don't pack of query string
		///    </Description>
		///    <Inputs>
		///       pstrQueryString : contains infomation for get data
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCommitIventory(int pintMasLocID, DateTime pdtmSchedule, string pstrBeginSO, string pstrEndSO, int pintPartyID, bool pblnHasDate)
		{
			string strCondition = CreateConditionForSearchPackList(pintMasLocID, pdtmSchedule, pstrBeginSO, pstrEndSO, pblnHasDate);
			const string METHOD_NAME = THIS + ".ListCommitIventory()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "Select CMDetail." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ", CMDetail." + SO_CommitInventoryDetailTable.PACKED_FLD
					+ " From " + SO_CommitInventoryDetailTable.TABLE_NAME + " CMDetail inner join " + SO_DeliveryScheduleTable.TABLE_NAME + " DelSchel on CMDetail."
					+ SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " = DelSchel." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ " Inner join " + SO_CommitInventoryMasterTable.TABLE_NAME + " CMMaster on CMDetail."+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD 
					+ " = CMMaster." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD
					+ " Inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " SOMaster on CMMaster." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD 
					+ " = SOMaster." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " WHERE " + strCondition + " and ( CMDetail." + SO_CommitInventoryDetailTable.SHIPPED_FLD + " = 0 or CMDetail." + SO_CommitInventoryDetailTable.SHIPPED_FLD +  " is Null )" 
					+ " and (SOMaster." + SO_SaleOrderMasterTable.PARTYID_FLD + "=" + pintPartyID + " or " + pintPartyID + "=" +  0.ToString() + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				if (pblnHasDate)
				{
					ocmdPCS.Parameters.Add(SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD, OleDbType.DBDate);
					ocmdPCS.Parameters[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Value = pdtmSchedule;
				}
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail
		///        of SaleOrderMaster 
		///    </Description>
		///    <Inputs>
		///       pintSOID : SaleOrderMasterID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCommitItemsBySOMasterID(int pintSOID)
		{
			const string METHOD_NAME = THIS + ".ListCommitItemsBySOMasterID()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT DISTINCT "
					+ "A." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME + " A inner join "
					+ SO_CommitInventoryMasterTable.TABLE_NAME + " B on A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + " = B." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD
					+ " inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " C on B." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + " = C." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD
					+ " inner join " + SO_SaleOrderDetailTable.TABLE_NAME + " D on C." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = D." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " H on H.SaleOrderMasterID = B.SaleOrderMasterID"
					+ " WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = " + pintSOID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get SaleOrderDetailVO object by master id
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///      SaleOrderMasterVO object
		///    </Outputs>
		///    <Returns>
		///      object
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    23-Feb-2005
		///    21-Apr-2005: Update entire method
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetSOBySOMasterID(int pintSOMasterID)
		{
			const string METHOD_NAME = THIS + ".GetSOBySOMasterID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			const string VIEW_TOTAL_COMMIT = "v_TotalCommitInventory";
			const string REMAIN_QTY_COL = "RemainQty";
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + " AS " + SO_CommitInventoryDetailTable.LINE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " AS " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + ", "
					+ MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " AS " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD
					+ " - (SELECT IsNull(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0 )"   
					+ "   FROM " + VIEW_TOTAL_COMMIT 
					+ "   WHERE " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD 
					+ "     AND " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD 
					+ "     AND " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ ") AS " + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD 
					+ " - (SELECT IsNull(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0 )"   
					+ "   FROM " + VIEW_TOTAL_COMMIT 
					+ "   WHERE " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD 
					+ "     AND " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD 
					+ "     AND " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD 
					+ ") AS " + REMAIN_QTY_COL + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UMRATE_FLD + ", "
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " AS " + MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD + ", "
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.CODE_FLD + " AS " + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + ", "
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.CODE_FLD + " AS " + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD + ", "
					+ MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD + ", "
					+ MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD + ", "
					+ MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME + " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
					+ " JOIN " + SO_DeliveryScheduleTable.TABLE_NAME 
					+ " ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD 
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " JOIN " + ITM_ProductTable.TABLE_NAME 
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD 
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
					+ " ON " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD 
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD
					+ " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME
					+ " ON " + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MASTERLOCATIONID_FLD
					+ " LEFT JOIN " + MST_LocationTable.TABLE_NAME
					+ " ON " + MST_LocationTable.TABLE_NAME + "." + MST_LocationTable.LOCATIONID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LOCATIONID_FLD
					+ " LEFT JOIN " + MST_BINTable.TABLE_NAME
					+ " ON " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.BINID_FLD
					+ " WHERE " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSOMasterID
					+ "   AND "	+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD 
					+ " - (SELECT IsNull(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0 )"   
					+ "   FROM " + VIEW_TOTAL_COMMIT 
					+ "   WHERE " + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD 
					+ "     AND " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD 
					+ "     AND " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD 
					+ ") >0 " 
					+ " ORDER BY " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD;
	

				// create a data table to hold data
				DataTable dtbData = new DataTable(SO_CommitInventoryDetailTable.TABLE_NAME);
				dtbData.Columns.Add(new DataColumn(MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LINE_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.DESCRIPTION_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(ITM_ProductTable.REVISION_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.PRODUCTID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SELLINGUMID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.STOCKUMID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD, typeof(decimal)));

				dtbData.Columns.Add(new DataColumn(REMAIN_QTY_COL, typeof(decimal)));

				dtbData.Columns.Add(new DataColumn(IV_MasLocCacheTable.OHQUANTITY_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.UMRATE_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LOCATIONID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.BINID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.LOT_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SERIAL_FLD, typeof(string)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.CCNID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD, typeof(int)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.PACKED_FLD, typeof(bool)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.STDCOST_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD, typeof(decimal)));
				dtbData.Columns.Add(new DataColumn(SO_CommitInventoryDetailTable.SHIPPED_FLD, typeof(bool)));

				// primary key
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].Unique = true;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrement = true;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrementSeed = 1;
				dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].AutoIncrementStep = 1;

				DataColumn[] objColumns = new DataColumn[1];
				objColumns[0] = dtbData.Columns[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD];
				dtbData.PrimaryKey = objColumns;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				dstPCS.Tables.Add(dtbData);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get max line
		///    </Description>
		///    <Inputs>
		///            
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       Friday, March 11, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxLineByMaster(int pintCommitInventoryMasterID)
		{
			const string METHOD_NAME = THIS + ".GetMaxLineByMaster()";
			const string MAXLINE = "MAXLINE";
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT MAX(" + SO_CommitInventoryDetailTable.LINE_FLD + ") AS " + MAXLINE
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + "=" + pintCommitInventoryMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					if(odrPCS[MAXLINE] != DBNull.Value)
						return int.Parse(odrPCS[MAXLINE].ToString());
				}
                return 0;
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
		//**************************************************************************              
		///    <Description>
		///     This method uses to get commited quantity by sale order detail
		///    </Description>
		///    <Inputs>
		///        int SaleOrderDetailID
		///    </Inputs>
		///    <Outputs>
		///      decimal
		///    </Outputs>
		///    <Returns>
		///      decimal
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public decimal GetCommitedQuantity(int pintSaleOrderDetailID)
		{
			const string METHOD_NAME = THIS + ".GetMaxLineByMaster()";
			const string COMMITED_QUANTITY = "CommitedQuantity";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ISNULL(SUM(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "), 0) AS " + COMMITED_QUANTITY
					+ ", " + SO_DeliveryScheduleTable.DELIVERYNO_FLD
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " GROUP BY " + SO_DeliveryScheduleTable.DELIVERYNO_FLD
					+ " WHERE " + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" + pintSaleOrderDetailID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();

				if ((objResult == DBNull.Value) || (objResult == null))
				{
					return 0;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
				}
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
		//**************************************************************************              
		///    <Description>
		///     This method uses to get line for release
		///    </Description>
		///    <Inputs>
		///        DateTime TransDate, int CCNID, int ProductID
		///    </Inputs>
		///    <Outputs>
		///      void
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///    21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetLineForRelease(DateTime pdtmTransDate, int pintCCNID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetMaxLineByMaster()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + ", "
					+ SO_SaleOrderMasterTable.CODE_FLD + ", "
					+ SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", "
					+ SO_DeliveryScheduleTable.DELIVERYNO_FLD + ", "
					+ IV_MasLocCacheTable.OHQUANTITY_FLD + ", "
					+ MST_UnitOfMeasureTable.CODE_FLD + ", "
					+ SO_SaleOrderDetailTable.ORDERQUANTITY_FLD + ", "
					+ SO_SaleOrderMasterTable.PRIORITY_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME + ", " + SO_SaleOrderDetailTable.TABLE_NAME
					+ ", " + SO_DeliveryScheduleTable.TABLE_NAME + ", " + IV_MasLocCacheTable.TABLE_NAME + ", " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.PRODUCTID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD
					+ " AND " + IV_MasLocCacheTable.TABLE_NAME + "." + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MASTERLOCATIONID_FLD
					+ " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + "=" + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD
					+ " AND " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " AND " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.SELLINGUMID_FLD + "=" + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + "='" + pdtmTransDate.ToShortDateString()
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable(SO_SaleOrderDetailTable.TABLE_NAME);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail which don't ship by SaleOrder code
		///    </Description>
		///    <Inputs>
		///       strSaleOrderCode : Code of SaleOrderMaster
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCommitIventoryBySaleOrderNotShip(string strSaleOrderCode)
		{
			string strCondition = "(" + SO_CommitInventoryDetailTable.SHIPPED_FLD + " = 0  or " + SO_CommitInventoryDetailTable.SHIPPED_FLD + " is null) and "
					+ "C." + SO_SaleOrderMasterTable.CODE_FLD + " = '" + strSaleOrderCode + "'";
			const string METHOD_NAME = THIS + ".ListCommitIventoryBySaleOrderNotShip()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT DISTINCT "
					+ "A." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ "(Select " + ITM_ProductTable.COSTMETHOD_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + ITM_ProductTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.COSTMETHOD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME + " A inner join "
					+ SO_CommitInventoryMasterTable.TABLE_NAME + " B on A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + " = B." + SO_CommitInventoryMasterTable.COMMITINVENTORYMASTERID_FLD
					+ " inner join " + SO_SaleOrderMasterTable.TABLE_NAME + " C on B." + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD + " = C." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " WHERE " + strCondition + " and A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + " not in (Select " + SO_PackListDetailTable.COMMITINVENTORYDETAILID_FLD 
					+ " From " + SO_PackListDetailTable.TABLE_NAME + " PLD inner join " + SO_SaleOrderDetailTable.TABLE_NAME + " SOD on PLD." + SO_PackListDetailTable.SALEORDERDETAILID_FLD + " = SOD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " Where PLD." + SO_PackListDetailTable.COMMITINVENTORYDETAILID_FLD + " = A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD+ ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail don't ship by PackListMaster
		///    </Description>
		///    <Inputs>
		///       pintPackListMasterID : PackListMasterID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, February 02, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListCommitIventoryByPackListMasterNotShip(int pintPackListMasterID)
		{
			string strCondition = SO_CommitInventoryDetailTable.PACKED_FLD + " = 1  and " +
				"C." + SO_PackListMasterTable.PACKLISTMASTERID_FLD + " = " + pintPackListMasterID;
			const string METHOD_NAME = THIS + ".ListCommitIventoryByPackListMasterNotShip()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT DISTINCT "
					+ "A." + SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ "A." + SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ "(Select " + ITM_ProductTable.COSTMETHOD_FLD + " From " + ITM_ProductTable.TABLE_NAME + " Where " + ITM_ProductTable.PRODUCTID_FLD + " = A." + ITM_ProductTable.PRODUCTID_FLD + ") as " 
					+ ITM_ProductTable.COSTMETHOD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME + " A inner join "
					+ SO_PackListDetailTable.TABLE_NAME + " B on A." + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + " = B."
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD
					+ " inner join " + SO_PackListMasterTable.TABLE_NAME + " C on B." + SO_PackListDetailTable.PACKLISTMASTERID_FLD + " = C." + SO_PackListMasterTable.PACKLISTMASTERID_FLD
					+ " WHERE " + strCondition;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		/// <summary>
		/// Get remain quantity of delivery
		/// </summary>
		/// <param name="pintSoMasterID">Sale Order Maste</param>
		/// <param name="pintDeliveryScheduleID">Delivery Schedule</param>
		/// <param name="pintProductID">Product</param>
		/// <returns>Remain Quantity</returns>
		public decimal GetRemainQuantity(int pintSoMasterID, int pintDeliveryScheduleID, int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetRemainQuantity()";
			const string REMAIN_QTY = "RemainQty";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				#region // HACK: DEL DuongNA 2005-10-26
//				string strSql = "SELECT ISNULL(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", 0) - "
//					+ " (SELECT IsNull(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0 )"
//					+ " FROM v_TotalCommitInventory "
//					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID
//					+ " AND " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSoMasterID
//                    + " AND " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliveryScheduleID + ")"
//					+ " AS " + REMAIN_QTY
//					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME
//					+ " WHERE " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliveryScheduleID;
				#endregion // END: DEL DuongNA 2005-10-26

				#region // HACK: DuongNA 2005-10-26
				string strSql = "SELECT ISNULL(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", 0) - "
					+ " ISNULL((SELECT " + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD
					+ " FROM v_TotalCommitInventory "
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSoMasterID
				    + " AND " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliveryScheduleID + "),0)"
					+ " AS " + REMAIN_QTY
					+ " FROM " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " WHERE " + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "=" + pintDeliveryScheduleID;
				#endregion // END: DuongNA 2005-10-26

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();

				if ((objResult == DBNull.Value) || (objResult == null))
				{
					return 0;
				}
				else
				{
					return decimal.Parse(objResult.ToString());
				}
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

		/// <summary>
		/// Get remain quantity of delivery
		/// </summary>
		/// <returns>Remain Quantity</returns>
		public DataTable GetRemainQuantity(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetRemainQuantity()";
			const string REMAIN_QTY = "RemainQty";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT (ISNULL(DeliveryQuantity,0) - ISNULL(CommitQuantity,0)) AS " + REMAIN_QTY + ","
					+ " SaleOrderMasterID, SO_DeliverySchedule.DeliveryScheduleID, ProductID"
					+ " FROM SO_DeliverySchedule JOIN v_TotalCommitInventory"
					+ " ON SO_DeliverySchedule.DeliveryScheduleID = v_TotalCommitInventory.DeliveryScheduleID"
					+ " WHERE SaleOrderMasterID = " + pintMasterID;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbRemain = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbRemain);
				return dtbRemain;
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from SO_CommitInventoryDetail
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       Friday, October 28, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListNotCommittedByDeliveryScheduleID(int pintDeliveryScheduleID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + "=?"
					+ " AND IsNull(" + SO_CommitInventoryDetailTable.SHIPPED_FLD + ",0) = 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				ocmdPCS.Parameters.AddWithValue(SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD,pintDeliveryScheduleID);

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, SO_CommitInventoryDetailTable.TABLE_NAME);

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

		public DataSet ListNotCommitted()
		{
			const string METHOD_NAME = THIS + ".ListNotCommitted()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE IsNull(" + SO_CommitInventoryDetailTable.SHIPPED_FLD + ",0) = 0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstData = new DataSet();
				odadPCS.Fill(dstData, SO_CommitInventoryDetailTable.TABLE_NAME);

				return dstData;
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
		///       DuongNA
		///    </Authors>
		///    <History>
		///       28-10-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSetForShippingManagement(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();
			
			try
			{
				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ "  FROM " + SO_CommitInventoryDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand cmdSelect = new OleDbCommand(strSql, oconPCS);
				cmdSelect.CommandTimeout = 10000;
				odadPCS.SelectCommand = cmdSelect;
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, SO_CommitInventoryDetailTable.TABLE_NAME);
			}
			catch (OleDbException ex)
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
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
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


		public DataTable GetSchema()
		{
			const string METHOD_NAME = THIS + ".GetSchema()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + ","
					+ SO_CommitInventoryDetailTable.LINE_FLD + ","
					+ SO_CommitInventoryDetailTable.INSPECTIONSTATUS_FLD + ","
					+ SO_CommitInventoryDetailTable.LOT_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ","
					+ SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD + ","
					+ SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + ","
					+ SO_CommitInventoryDetailTable.PRODUCTID_FLD + ","
					+ SO_CommitInventoryDetailTable.BINID_FLD + ","
					+ SO_CommitInventoryDetailTable.LOCATIONID_FLD + ","
					+ SO_CommitInventoryDetailTable.SERIAL_FLD + ","
					+ SO_CommitInventoryDetailTable.PACKED_FLD + ","
					+ SO_CommitInventoryDetailTable.UMRATE_FLD + ","
					+ SO_CommitInventoryDetailTable.SELLINGUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STOCKUMID_FLD + ","
					+ SO_CommitInventoryDetailTable.STDCOST_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPPED_FLD + ","
					+ SO_CommitInventoryDetailTable.SHIPDATE_FLD + ","
					+ SO_CommitInventoryDetailTable.CCNID_FLD + ","
					+ SO_CommitInventoryDetailTable.COSTOFGOODSSOLD_FLD
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD + "=0";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable(SO_CommitInventoryDetailTable.TABLE_NAME);

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);

				return dtbData;
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