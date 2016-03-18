using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_MiscellaneousIssueDetailDS 
	{
		public IV_MiscellaneousIssueDetailDS()
		{
		}

		private const string THIS = "PCSComMaterials.Inventory.BO.IV_MiscellaneousIssueDetailDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to IV_MiscellaneousIssueDetail
		///    </Description>
		///    <Inputs>
		///        IV_MiscellaneousIssueDetailVO       
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
		///       Monday, December 19, 2005
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
				IV_MiscellaneousIssueDetailVO objObject = (IV_MiscellaneousIssueDetailVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO IV_MiscellaneousIssueDetail("
					+ IV_MiscellaneousIssueDetailTable.LOT_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.LOT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD].Value = objObject.MiscellaneousIssueMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
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
		///       This method uses to delete data from IV_MiscellaneousIssueDetail
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
			strSql = "DELETE " + IV_MiscellaneousIssueDetailTable.TABLE_NAME + " WHERE  " + "MiscellaneousIssueDetailID" + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					}
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

		public DataSet GetMiscellaneousDetailByMaster(int pintMaster)
		{
			const string METHOD_NAME = THIS + ".GetMiscellaneousDetailByMaster()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT  null as Line, MiscellaneousIssueDetailID, C.Code AS ITM_CategoryCode, "
					+ " P.Code PartNumber, P.Description PartName, P.Revision Model,"
					+ " V.Code AS MST_PartyCode, U.Code AS UM, P.LotControl, MST_Department.Code AS Department, MST_Reason.Code Reason,"
                    + " D.Quantity, D.AvailableQty, P.ProductID, D.Lot, MiscellaneousIssueMasterID, D.StockUMID, D.DepartmentID, D.ReasonID"
					+ " FROM IV_MiscellaneousIssueDetail D JOIN ITM_Product P ON D.ProductID = P.ProductID"
					+ " LEFT JOIN MST_UnitOfMeasure U ON D.StockUMID = U.UnitOfMeasureID"
					+ " LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " LEFT JOIN MST_Party V ON P.PrimaryVendorID = V.PartyID"
					+ " LEFT JOIN MST_Department ON D.DepartmentID = MST_Department.DepartmentID"
					+ " LEFT JOIN MST_Reason ON D.ReasonID = MST_Reason.ReasonID"
                    + " WHERE MiscellaneousIssueMasterID=" + pintMaster;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_MiscellaneousIssueDetailTable.TABLE_NAME);

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
		///       This method uses to get data from IV_MiscellaneousIssueDetail
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       IV_MiscellaneousIssueDetailVO
		///    </Outputs>
		///    <Returns>
		///       IV_MiscellaneousIssueDetailVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 19, 2005
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
				strSql = "SELECT "
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.LOT_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD
					+ " FROM " + IV_MiscellaneousIssueDetailTable.TABLE_NAME
					+ " WHERE " + IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_MiscellaneousIssueDetailVO objObject = new IV_MiscellaneousIssueDetailVO();

				while (odrPCS.Read())
				{
					objObject.MiscellaneousIssueDetailID = int.Parse(odrPCS[IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD].ToString().Trim());
					objObject.Lot = odrPCS[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString().Trim();
					objObject.Quantity = Decimal.Parse(odrPCS[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].ToString().Trim());
					objObject.MiscellaneousIssueMasterID = int.Parse(odrPCS[IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD].ToString().Trim());

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
		///       This method uses to update data to IV_MiscellaneousIssueDetail
		///    </Description>
		///    <Inputs>
		///       IV_MiscellaneousIssueDetailVO       
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

			IV_MiscellaneousIssueDetailVO objObject = (IV_MiscellaneousIssueDetailVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE IV_MiscellaneousIssueDetail SET "
					+ IV_MiscellaneousIssueDetailTable.LOT_FLD + "=   ?" + ","
					+ IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + "=   ?" + ","
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + "=   ?" + ","
					+ IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD + "=   ?" + ","
					+ IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD + "=  ?"
					+ " WHERE " + IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.LOT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.LOT_FLD].Value = objObject.Lot;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.QUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].Value = objObject.Quantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD].Value = objObject.MiscellaneousIssueMasterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD].Value = objObject.MiscellaneousIssueDetailID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
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
		///       This method uses to get all data from IV_MiscellaneousIssueDetail
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
		///       Monday, December 19, 2005
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
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.LOT_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD
					+ " FROM " + IV_MiscellaneousIssueDetailTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_MiscellaneousIssueDetailTable.TABLE_NAME);

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
		///       This method uses to get all data from IV_MiscellaneousIssueDetail
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
		///       Monday, December 19, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable List(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".List()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT IV_MiscellaneousIssueDetail.Quantity, IV_MiscellaneousIssueDetail.ProductID,"
					+ " IssuePurposeID, IV_MiscellaneousIssueMaster.SourceBinID, SourceBin.BinTypeID AS SourceBinType,"
					+ " IV_MiscellaneousIssueMaster.DesBinID, DesBin.BinTypeID AS DesBinType"
					+ " FROM IV_MiscellaneousIssueDetail JOIN IV_MiscellaneousIssueMaster"
					+ " ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID"
					+ " JOIN MST_BIN AS SourceBin"
					+ " ON IV_MiscellaneousIssueMaster.SourceBinID = SourceBin.BinID"
					+ " JOIN MST_BIN AS DesBin"
					+ " ON IV_MiscellaneousIssueMaster.DesBinID = DesBin.BinID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate <= ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
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
		///       Monday, December 19, 2005
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
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.LOT_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD + ","
					+ IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD
					+ "  FROM " + IV_MiscellaneousIssueDetailTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, IV_MiscellaneousIssueDetailTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					}
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

		/// <summary>
		/// Gets destroy quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		public DataTable GetDestroyQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL(Quantity, 0)), 0) AS " + IV_MiscellaneousIssueDetailTable.QUANTITY_FLD + ", ProductID"
					+ " FROM IV_MiscellaneousIssueDetail JOIN IV_MiscellaneousIssueMaster"
					+ " ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID"
					+ " = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID"
					+ " JOIN PRO_IssuePurpose"
					+ " ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " WHERE IV_MiscellaneousIssueMaster.CCNID = " + pintCCNID
					+ " AND PRO_IssuePurpose.Code = " + (int) PurposeEnum.Scrap
					+ " AND IV_MiscellaneousIssueMaster.PostDate >= ?"
					+ " AND IV_MiscellaneousIssueMaster.PostDate <= ?"
					+ " GROUP BY ProductID ORDER BY ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;

				cmdPCS.Connection.Open();
				OleDbDataAdapter odadData = new OleDbDataAdapter(cmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		public DataSet ListAllDetailByMasterID(int printmiscellaneousIssueMasterId)
		{
			const string METHOD_NAME = THIS + ".()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT * FROM " + IV_MiscellaneousIssueDetailTable.TABLE_NAME +
						 " WHERE " + IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + "="+ printmiscellaneousIssueMasterId.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

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

		public void DeleteByMasterID(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + IV_MiscellaneousIssueDetailTable.TABLE_NAME + " WHERE  " + IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					}
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
	}
}