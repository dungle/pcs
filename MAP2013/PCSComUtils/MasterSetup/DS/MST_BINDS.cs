using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_BINDS 
	{
		public MST_BINDS()
		{
		}

		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_BINDS";
        public DataRow GetDefaultLocBin(int pintLocationID)
        {
            const string METHOD_NAME = THIS + ".GetDefaultLocBin()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                    + MST_BINTable.BINID_FLD + ", "
                    + "L." + MST_LocationTable.CODE_FLD + " AS " + MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD + ", "
                    + "B." + MST_BINTable.CODE_FLD + " AS " + MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD
                    + " FROM " + MST_BINTable.TABLE_NAME + " B JOIN " + MST_LocationTable.TABLE_NAME + " L"
                    + " ON B." + MST_BINTable.LOCATIONID_FLD + " = L." + MST_LocationTable.LOCATIONID_FLD
                    + " WHERE B." + MST_BINTable.LOCATIONID_FLD + "=" + pintLocationID.ToString()
                    + " AND " + MST_BINTable.BINTYPEID_FLD + "=" + (int)BinTypeEnum.IN;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                DataTable dtbResult = new DataTable();
                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dtbResult);
                if (dtbResult.Rows.Count > 0)
                    return dtbResult.Rows[0];
                else
                    return null;
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
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				MST_BINVO objObject = (MST_BINVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO MST_BIN("
					+ MST_BINTable.CODE_FLD + ","
					+ MST_BINTable.NAME_FLD + ","
					+ MST_BINTable.LENGTH_FLD + ","
					+ MST_BINTable.WIDTH_FLD + ","
					+ MST_BINTable.HEIGHT_FLD + ","
					+ MST_BINTable.LOCATIONID_FLD + ","
					+ MST_BINTable.LENGTHUNITID_FLD + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + ","
					+ MST_BINTable.WIDTHUNITID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_BINTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_BINTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LENGTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.LENGTH_FLD].Value = objObject.Length;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.WIDTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.HEIGHT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.HEIGHT_FLD].Value = objObject.Height;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LENGTHUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.LENGTHUNITID_FLD].Value = objObject.LengthUnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.HEIGHTUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.HEIGHTUNITID_FLD].Value = objObject.HeightUnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.WIDTHUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.WIDTHUNITID_FLD].Value = objObject.WidthUnitID;


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
		///       This method uses to delete data from MST_BIN
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
			strSql = "DELETE " + MST_BINTable.TABLE_NAME + " WHERE  " + "BinID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_BIN
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_BINVO
		///    </Outputs>
		///    <Returns>
		///       MST_BINVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///       DungLa: Check for null value in database
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///       18-Mar-2005: DungLa - Check for null value in database
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
					+ MST_BINTable.BINID_FLD + ","
					+ MST_BINTable.CODE_FLD + ","
					+ MST_BINTable.NAME_FLD + ","
					+ MST_BINTable.LENGTH_FLD + ","
					+ MST_BINTable.WIDTH_FLD + ","
					+ MST_BINTable.HEIGHT_FLD + ","
					+ MST_BINTable.LOCATIONID_FLD + ","
					+ MST_BINTable.LENGTHUNITID_FLD + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + ","
					+ MST_BINTable.WIDTHUNITID_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME
					+ " WHERE " + MST_BINTable.BINID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_BINVO objObject = new MST_BINVO();

				while (odrPCS.Read())
				{
					if (odrPCS[MST_BINTable.BINID_FLD] != DBNull.Value)
						objObject.BinID = int.Parse(odrPCS[MST_BINTable.BINID_FLD].ToString().Trim());
					objObject.Code = odrPCS[MST_BINTable.CODE_FLD].ToString().Trim();
					objObject.Name = odrPCS[MST_BINTable.NAME_FLD].ToString().Trim();
					if (odrPCS[MST_BINTable.LENGTH_FLD] != DBNull.Value)
						objObject.Length = Decimal.Parse(odrPCS[MST_BINTable.LENGTH_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.WIDTH_FLD] != DBNull.Value)
						objObject.Width = Decimal.Parse(odrPCS[MST_BINTable.WIDTH_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.HEIGHT_FLD] != DBNull.Value)
						objObject.Height = Decimal.Parse(odrPCS[MST_BINTable.HEIGHT_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[MST_BINTable.LOCATIONID_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.LENGTHUNITID_FLD] != DBNull.Value)
						objObject.LengthUnitID = int.Parse(odrPCS[MST_BINTable.LENGTHUNITID_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.HEIGHTUNITID_FLD] != DBNull.Value)
						objObject.HeightUnitID = int.Parse(odrPCS[MST_BINTable.HEIGHTUNITID_FLD].ToString().Trim());
					if (odrPCS[MST_BINTable.WIDTHUNITID_FLD] != DBNull.Value)
						objObject.WidthUnitID = int.Parse(odrPCS[MST_BINTable.WIDTHUNITID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_BIN
		///    </Description>
		///    <Inputs>
		///       MST_BINVO       
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

			MST_BINVO objObject = (MST_BINVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE MST_BIN SET "
					+ MST_BINTable.CODE_FLD + "=   ?" + ","
					+ MST_BINTable.NAME_FLD + "=   ?" + ","
					+ MST_BINTable.LENGTH_FLD + "=   ?" + ","
					+ MST_BINTable.WIDTH_FLD + "=   ?" + ","
					+ MST_BINTable.HEIGHT_FLD + "=   ?" + ","
					+ MST_BINTable.LOCATIONID_FLD + "=   ?" + ","
					+ MST_BINTable.LENGTHUNITID_FLD + "=   ?" + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + "=   ?" + ","
					+ MST_BINTable.WIDTHUNITID_FLD + "=  ?"
					+ " WHERE " + MST_BINTable.BINID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_BINTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_BINTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LENGTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.LENGTH_FLD].Value = objObject.Length;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.WIDTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.HEIGHT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[MST_BINTable.HEIGHT_FLD].Value = objObject.Height;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.LENGTHUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.LENGTHUNITID_FLD].Value = objObject.LengthUnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.HEIGHTUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.HEIGHTUNITID_FLD].Value = objObject.HeightUnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.WIDTHUNITID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.WIDTHUNITID_FLD].Value = objObject.WidthUnitID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_BINTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_BINTable.BINID_FLD].Value = objObject.BinID;


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
		///       This method uses to get all data from MST_BIN
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


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_BINTable.BINID_FLD + ","
					+ MST_BINTable.CODE_FLD + ","
					+ MST_BINTable.NAME_FLD + ","
					+ MST_BINTable.LENGTH_FLD + ","
					+ MST_BINTable.WIDTH_FLD + ","
					+ MST_BINTable.HEIGHT_FLD + ","
					+ MST_BINTable.LOCATIONID_FLD + ","
					+ MST_BINTable.LENGTHUNITID_FLD + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + ","
					+ MST_BINTable.WIDTHUNITID_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_BINTable.TABLE_NAME);

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
		///       Tuesday, January 25, 2005
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
					+ MST_BINTable.BINID_FLD + ","
					+ MST_BINTable.CODE_FLD + ","
					+ MST_BINTable.NAME_FLD + ","
					+ MST_BINTable.LENGTH_FLD + ","
					+ MST_BINTable.WIDTH_FLD + ","
					+ MST_BINTable.HEIGHT_FLD + ","
					+ MST_BINTable.LOCATIONID_FLD + ","
					+ MST_BINTable.LENGTHUNITID_FLD + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + ","
					+ MST_BINTable.WIDTHUNITID_FLD
					+ "  FROM " + MST_BINTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, MST_BINTable.TABLE_NAME);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get Bin by LocationID
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       Feb - 17 - 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListByLocationID(int pintLocationID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_BINTable.BINID_FLD + ","
					+ MST_BINTable.CODE_FLD + ","
					+ MST_BINTable.NAME_FLD + ","
					+ MST_BINTable.LENGTH_FLD + ","
					+ MST_BINTable.WIDTH_FLD + ","
					+ MST_BINTable.HEIGHT_FLD + ","
					+ MST_BINTable.LOCATIONID_FLD + ","
					+ MST_BINTable.LENGTHUNITID_FLD + ","
					+ MST_BINTable.HEIGHTUNITID_FLD + ","
					+ MST_BINTable.WIDTHUNITID_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME
					+ " WHERE " + MST_BINTable.LOCATIONID_FLD + "=" + pintLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_LocationTable.TABLE_NAME);

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
		///       This method uses to get data for Bin dropdown
		///    </Description>
		///    <Inputs>
		///        int
		///    </Inputs>
		///    <Outputs>
		///       DataTable
		///    </Outputs>
		///    <Returns>
		///       DataTable
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       17-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable GetDataForTrueDBDropDown(int pintLocationID)
		{
			const string METHOD_NAME = THIS + ".GetDataForMasLocDropDown()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ MST_BINTable.BINID_FLD + ","
					+ MST_BINTable.CODE_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME;
				if (pintLocationID != 0)
					strSql += " WHERE " + MST_BINTable.LOCATIONID_FLD + "=" + pintLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_BINTable.TABLE_NAME);

				return dstPCS.Tables[MST_BINTable.TABLE_NAME];
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
		///       This method uses to get bin code from id
		///    </Description>
		///    <Inputs>
		///        id
		///    </Inputs>
		///    <Outputs>
		///       string Code
		///    </Outputs>
		///    <Returns>
		///       string Code
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       21-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetCodeFromID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetCodeFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_BINTable.CODE_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME
					+" WHERE " + MST_BINTable.BINID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return objResult.ToString().Trim();
				else
					return string.Empty;
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
		/// Get LocationID by BinID
		/// </summary>
		/// <param name="pintBINID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, October 9 2006</date>
		public int GetLocationIDByBinID(int pintBINID)
		{
			const string METHOD_NAME = THIS + ".GetLocationIDByBinID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_BINTable.LOCATIONID_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME
					+" WHERE " + MST_BINTable.BINID_FLD + "=" + pintBINID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return int.Parse(objResult.ToString());
				else
					return 0;
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

		public int GetBinIDByProductionLine(int pintLocID)
		{
			const string METHOD_NAME = THIS + ".GetCodeFromID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ MST_BINTable.BINID_FLD
					+ " FROM " + MST_BINTable.TABLE_NAME
					+" WHERE " + MST_BINTable.LOCATIONID_FLD + "=" + pintLocID.ToString()
					+ " AND " + MST_BINTable.BINTYPEID_FLD + "=" + (int)BinTypeEnum.IN;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
					return int.Parse(objResult.ToString());
				else
					return 0;
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