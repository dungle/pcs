using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduct.Items.DS
{
	
	public class ITM_CategoryDS 
	{
		public ITM_CategoryDS()
		{
		}

		private const string THIS = "PCSComProduct.Items.DS.DS.ITM_CategoryDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_Category
		///    </Description>
		///    <Inputs>
		///        ITM_CategoryVO       
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

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				ITM_CategoryVO objObject = (ITM_CategoryVO) pobjObjectVO;
				string strSqlRoot = string.Empty, strSqlNode = string.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSqlRoot = "INSERT INTO ITM_Category("
					+ ITM_CategoryTable.PICTURE_FLD + ", "
					+ ITM_CategoryTable.CODE_FLD + ", "
					+ ITM_CategoryTable.NAME_FLD + ", "
					+ ITM_CategoryTable.CATALOGNAME_FLD + ", "
					+ ITM_CategoryTable.DESCRIPTION_FLD + ")"
					+ "VALUES(?,?,?,?,?)";

				strSqlNode = "INSERT INTO ITM_Category("
					+ ITM_CategoryTable.PICTURE_FLD + ","
					+ ITM_CategoryTable.CODE_FLD + ", "
					+ ITM_CategoryTable.NAME_FLD + ", "
					+ ITM_CategoryTable.CATALOGNAME_FLD + ", "
					+ ITM_CategoryTable.DESCRIPTION_FLD + ","
					+ ITM_CategoryTable.PARENTCATEGORYID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.PICTURE_FLD, OleDbType.Binary));
				if (objObject.Picture != null)
				{
					// convert bitmap to byte array in order to store to db
					Bitmap image = objObject.Picture;
					MemoryStream stream = new MemoryStream();
					image.Save(stream, ImageFormat.Bmp);
					byte[] bytContent = stream.ToArray();
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Value = bytContent;
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Size = bytContent.Length;
				}
				else
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CATALOGNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.CATALOGNAME_FLD].Value = objObject.CatalogNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.PARENTCATEGORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_CategoryTable.PARENTCATEGORYID_FLD].Value = objObject.ParentCategoryId;

				if (objObject.ParentCategoryId == 0)
				{
					ocmdPCS.Parameters.RemoveAt(ITM_CategoryTable.PARENTCATEGORYID_FLD);
					ocmdPCS.CommandText = strSqlRoot;
				}
				else
					ocmdPCS.CommandText = strSqlNode;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to delete data from ITM_Category
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
			strSql = "DELETE " + ITM_CategoryTable.TABLE_NAME + " WHERE  " + "CategoryID" + "=" + pintID.ToString();
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
		///       This method uses to check if a specific categoryid is a leaf node
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
		public bool IsLeafNode(int pintCategoryID)
		{
			const string METHOD_NAME = THIS + ".IsLeafNode()";
			string strSql = String.Empty;
			strSql = " SELECT count(*) ";
			strSql += " FROM " + ITM_CategoryTable.TABLE_NAME;
			strSql += " WHERE " + ITM_CategoryTable.PARENTCATEGORYID_FLD + "=" + pintCategoryID;
			strSql += "      AND " + ITM_CategoryTable.CATEGORYID_FLD + "<>" + ITM_CategoryTable.PARENTCATEGORYID_FLD;


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				int intResult = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				ocmdPCS = null;
				if (intResult > 0)
				{
					return false;
				}
				else
				{
					return true;
				}
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
		///       This method uses to get data from ITM_Category
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_CategoryVO
		///    </Outputs>
		///    <Returns>
		///       ITM_CategoryVO
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
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_CategoryTable.CATEGORYID_FLD + ","
					+ ITM_CategoryTable.CODE_FLD + ","
					+ ITM_CategoryTable.NAME_FLD + ","
					+ ITM_CategoryTable.CATALOGNAME_FLD + ","
					+ ITM_CategoryTable.DESCRIPTION_FLD + ","
					+ ITM_CategoryTable.PICTURE_FLD + ","
					+ ITM_CategoryTable.PARENTCATEGORYID_FLD
					+ " FROM " + ITM_CategoryTable.TABLE_NAME
					+ " WHERE " + ITM_CategoryTable.CATEGORYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_CategoryVO objObject = new ITM_CategoryVO();

				while (odrPCS.Read())
				{
					objObject.CategoryID = int.Parse(odrPCS[ITM_CategoryTable.CATEGORYID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_CategoryTable.CODE_FLD].ToString().Trim();
					objObject.CatalogNameVN = odrPCS[ITM_CategoryTable.CATALOGNAME_FLD].ToString().Trim();
					objObject.Name = odrPCS[ITM_CategoryTable.NAME_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_CategoryTable.DESCRIPTION_FLD].ToString().Trim();
					try
					{
						objObject.ParentCategoryId = int.Parse(odrPCS[ITM_CategoryTable.PARENTCATEGORYID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						// convert byte array to bitmap
						byte[] content = (byte[])odrPCS[ITM_CategoryTable.PICTURE_FLD];
						MemoryStream stream = new MemoryStream(content);
						objObject.Picture = new Bitmap(stream);;
					}
					catch{}
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
		///       This method uses to get data from ITM_Category
		///    </Description>
		///    <Inputs>
		///        pstrProductCode   
		///    </Inputs>
		///    <Outputs>
		///       ITM_CategoryVO
		///    </Outputs>
		///    <Returns>
		///       ITM_CategoryVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, January 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(string pstrProductCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_CategoryTable.CATEGORYID_FLD + ","
					+ ITM_CategoryTable.CODE_FLD + ","
					+ ITM_CategoryTable.NAME_FLD + ","
					+ ITM_CategoryTable.CATALOGNAME_FLD + ","
					+ ITM_CategoryTable.DESCRIPTION_FLD + ","
					+ ITM_CategoryTable.PICTURE_FLD + ","
					+ ITM_CategoryTable.PARENTCATEGORYID_FLD
					+ " FROM " + ITM_CategoryTable.TABLE_NAME
					+ " WHERE " + ITM_CategoryTable.CODE_FLD + "=?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.CODE_FLD].Value = pstrProductCode;

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_CategoryVO objObject = new ITM_CategoryVO();

				while (odrPCS.Read())
				{
					objObject.CategoryID = int.Parse(odrPCS[ITM_CategoryTable.CATEGORYID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_CategoryTable.CODE_FLD].ToString().Trim();
					objObject.CatalogNameVN = odrPCS[ITM_CategoryTable.CATALOGNAME_FLD].ToString().Trim();
					objObject.Name = odrPCS[ITM_CategoryTable.NAME_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_CategoryTable.DESCRIPTION_FLD].ToString().Trim();
					try
					{
						objObject.ParentCategoryId = int.Parse(odrPCS[ITM_CategoryTable.PARENTCATEGORYID_FLD].ToString().Trim());
					}
					catch{}
					try
					{
						// convert byte array to bitmap
						byte[] content = (byte[])odrPCS[ITM_CategoryTable.PICTURE_FLD];
						MemoryStream stream = new MemoryStream(content);
						objObject.Picture = new Bitmap(stream);;
					}
					catch{}
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
		///       This method uses to update data to ITM_Category
		///    </Description>
		///    <Inputs>
		///       ITM_CategoryVO       
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

			ITM_CategoryVO objObject = (ITM_CategoryVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSqlRoot = string.Empty, strSqlNode = string.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSqlRoot, oconPCS);
				strSqlRoot = "UPDATE ITM_Category SET "
					+ ITM_CategoryTable.CODE_FLD + "=   ?" + ","
					+ ITM_CategoryTable.NAME_FLD + "=   ?" + ","
					+ ITM_CategoryTable.CATALOGNAME_FLD + "=   ?" + ","
					+ ITM_CategoryTable.DESCRIPTION_FLD + "=   ?" + ","
					+ ITM_CategoryTable.PICTURE_FLD + "=   ?"
					+ " WHERE " + ITM_CategoryTable.CATEGORYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CATALOGNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.CATALOGNAME_FLD].Value = objObject.CatalogNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_CategoryTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.PICTURE_FLD, OleDbType.Binary));
				if (objObject.Picture != null)
				{
					// convert bitmap to byte array in order to store to db
					Bitmap image = objObject.Picture;
					MemoryStream stream = new MemoryStream();
					image.Save(stream, ImageFormat.Bmp);
					byte[] bytContent = stream.ToArray();
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Value = bytContent;
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Size = bytContent.Length;
				}
				else
					ocmdPCS.Parameters[ITM_CategoryTable.PICTURE_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_CategoryTable.CATEGORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_CategoryTable.CATEGORYID_FLD].Value = objObject.CategoryID;

				ocmdPCS.CommandText = strSqlRoot;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
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
		///       This method uses to get all data from ITM_Category
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

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_CategoryTable.CATEGORYID_FLD + ", "
					+ ITM_CategoryTable.CODE_FLD + ", "
					+ ITM_CategoryTable.NAME_FLD + ", "
					+ ITM_CategoryTable.CATALOGNAME_FLD + ", "
					+ ITM_CategoryTable.DESCRIPTION_FLD + ", "
					+ ITM_CategoryTable.PICTURE_FLD + ", "
					+ ITM_CategoryTable.PARENTCATEGORYID_FLD
					+ " FROM " + ITM_CategoryTable.TABLE_NAME
					+ " ORDER BY " + ITM_CategoryTable.CODE_FLD + ", " + ITM_CategoryTable.NAME_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_CategoryTable.TABLE_NAME);

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
		///       This method uses to get all data from ITM_Category
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
		public DataSet ListForProduct()
		{
			const string METHOD_NAME = THIS + ".ListForProduct()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ ITM_CategoryTable.CODE_FLD + ","
					+ ITM_CategoryTable.NAME_FLD + ","
					+ ITM_CategoryTable.CATALOGNAME_FLD + ","
					+ ITM_CategoryTable.DESCRIPTION_FLD + ","
					+ ITM_CategoryTable.CATEGORYID_FLD
					+ " FROM " + ITM_CategoryTable.TABLE_NAME
					+ " WHERE " + ITM_CategoryTable.CATEGORYID_FLD
					+ "       NOT IN (SELECT " + ITM_CategoryTable.PARENTCATEGORYID_FLD
					+ "               FROM " + ITM_CategoryTable.TABLE_NAME
					+ "               WHERE " + ITM_CategoryTable.PARENTCATEGORYID_FLD + " IS NOT NULL )"
					+ " ORDER BY " + ITM_CategoryTable.CODE_FLD + "," + ITM_CategoryTable.NAME_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_CategoryTable.TABLE_NAME);

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
		///       Wednesday, January 19, 2005
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
					+ ITM_CategoryTable.CATEGORYID_FLD + ","
					+ ITM_CategoryTable.CODE_FLD + ","
					+ ITM_CategoryTable.NAME_FLD + ","
					+ ITM_CategoryTable.CATALOGNAME_FLD + ","
					+ ITM_CategoryTable.DESCRIPTION_FLD + ","
					+ ITM_CategoryTable.PICTURE_FLD + ","
					+ ITM_CategoryTable.PARENTCATEGORYID_FLD
					+ "  FROM " + ITM_CategoryTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, ITM_CategoryTable.TABLE_NAME);

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
		///       Check the category to know it used or no.
		///    </Description>
		///    <Inputs>
		///        pintCategoryID
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       true : if used
		///       false: if doesn't use
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, January 20, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool CheckCategoryUsed(int pintCategoryID)
		{
			const string METHOD_NAME = THIS + ".CheckCategoryUsed()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			int number = 0;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT COUNT(*) as NUMBER"
					+ " FROM  " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CATEGORYID_FLD + " = " + pintCategoryID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				number = int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());
				if (number != 0)
				{
					return true;
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
			return false;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to check before add new child for category 
		///    </Description>
		///    <Inputs>
		///        pintCategoryID    
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, January 24, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool CheckAddNewCategory(int pintCategoryID)
		{
			const string METHOD_NAME = THIS + ".CheckAddNewCategory()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			int number = 0;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT COUNT(*) as NUMBER"
					+ " FROM  " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CATEGORYID_FLD + " = " + pintCategoryID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				number = int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());
				if (number != 0)
				{
					return true;
				}
				return false;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				if (number != 0)
				{
					throw new PCSDBException(ErrorCode.MESSAGE_CATEGORY_NOADDCHILD, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
				}
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