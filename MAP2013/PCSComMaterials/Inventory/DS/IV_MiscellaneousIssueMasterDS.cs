using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Inventory.DS
{
	public class IV_MiscellaneousIssueMasterDS 
	{
		private const string THIS = "PCSComMaterials.Inventory.BO.IV_MiscellaneousIssueMasterDS";
        
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				IV_MiscellaneousIssueMasterVO objObject = (IV_MiscellaneousIssueMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_MiscellaneousIssueMaster("
				+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD].Value = objObject.LastChange;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].Value = objObject.DesBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].Value = objObject.SourceBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].Value = objObject.DesLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD].Value = objObject.SourceLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].Value = objObject.DesMasLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD].Value = objObject.SourceMasLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
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
	
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddAndReturnID()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			int intMasterID = 0;
			try
			{
				IV_MiscellaneousIssueMasterVO objObject = (IV_MiscellaneousIssueMasterVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO IV_MiscellaneousIssueMaster("
					+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?) SELECT @@Identity";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD].Value = objObject.LastChange;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESBINID_FLD, OleDbType.Integer));
				if (objObject.DesBinID > 0)
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].Value = objObject.DesBinID;
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].Value = null;
				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD, OleDbType.Integer));
				if (objObject.SourceBinID > 0)
                    ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].Value = objObject.SourceBinID;
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].Value = null;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.DesLocationID > 0)
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].Value = objObject.DesLocationID;
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD].Value = objObject.SourceLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.DesMasLocationID > 0)
				{
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].Value = objObject.DesMasLocationID;
				}
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD].Value = objObject.SourceMasLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.PARTYID_FLD, OleDbType.Integer));
				if (objObject.PartyID > 0)
                    ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.PARTYID_FLD].Value = objObject.PartyID;
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.PARTYID_FLD].Value = null;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
				if (objObject.IssuePurposeID > 0)
				{
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;
				}
				else
					ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD].Value = null;

						ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				oconPCS.Close();
				if ((objResult != DBNull.Value) && (objResult != null))
				{
					intMasterID = int.Parse(objResult.ToString());
				}
			}
			catch(OleDbException ex)
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
			return intMasterID;
		}
	

		public DataRow GetMiscellaneousMasterInfor(int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".GetLocToLocMasterInfor()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
					+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD + ","
					+ " ISNULL(" + IV_MiscellaneousIssueMasterTable.DESTROYAPPROVED_FLD + ",0) AS " + IV_MiscellaneousIssueMasterTable.DESTROYAPPROVED_FLD + ","
					+ "(SELECT " + MST_MasterLocationTable.CODE_FLD + " FROM " + MST_MasterLocationTable.TABLE_NAME + " WHERE " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
					+ "(SELECT " + MST_MasterLocationTable.CODE_FLD + " FROM " + MST_MasterLocationTable.TABLE_NAME + " WHERE " + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
					+ "(SELECT " + MST_LocationTable.CODE_FLD + " FROM " + MST_LocationTable.TABLE_NAME + " WHERE " + MST_LocationTable.LOCATIONID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
					+ "(SELECT " + MST_LocationTable.CODE_FLD + " FROM " + MST_LocationTable.TABLE_NAME + " WHERE " + MST_LocationTable.LOCATIONID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
					+ "(SELECT " + MST_BINTable.CODE_FLD + " FROM " + MST_BINTable.TABLE_NAME + " WHERE " + MST_BINTable.BINID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
					+ "(SELECT " + PRO_IssuePurposeTable.DESCRIPTION_FLD + " FROM " + PRO_IssuePurposeTable.TABLE_NAME + " WHERE " + PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD + ") AS " + PRO_IssuePurposeTable.TABLE_NAME + PRO_IssuePurposeTable.DESCRIPTION_FLD + ","
					+ "(SELECT " + MST_PartyTable.CODE_FLD + " FROM " + MST_PartyTable.TABLE_NAME + " WHERE " + MST_PartyTable.PARTYID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ") AS " + MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD + ","
					+ "(SELECT " + MST_PartyTable.NAME_FLD + " FROM " + MST_PartyTable.TABLE_NAME + " WHERE " + MST_PartyTable.PARTYID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ") AS " + MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD + ","
					+ "(SELECT " + MST_BINTable.CODE_FLD + " FROM " + MST_BINTable.TABLE_NAME + " WHERE " + MST_BINTable.BINID_FLD + " = " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + "." + IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ") AS " + IV_MiscellaneousIssueMasterTable.DESBINID_FLD 
					+ " FROM " + IV_MiscellaneousIssueMasterTable.TABLE_NAME
					+ " WHERE " + IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + " = " + pintMasterID;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, IV_MiscellaneousIssueMasterTable.TABLE_NAME);
			
				if (dstPCS.Tables[0].Rows.Count > 0)
				{
					return dstPCS.Tables[0].Rows[0];
				}
				else 
					return null;
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
        
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + IV_MiscellaneousIssueMasterTable.TABLE_NAME + " WHERE  " + "MiscellaneousIssueMasterID" + "=" + pintID.ToString();
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
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
				+ IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD
				+ " FROM " + IV_MiscellaneousIssueMasterTable.TABLE_NAME
				+" WHERE " + IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				IV_MiscellaneousIssueMasterVO objObject = new IV_MiscellaneousIssueMasterVO();

				while (odrPCS.Read())
				{ 
				objObject.MiscellaneousIssueMasterID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD].ToString().Trim());
				objObject.PostDate = DateTime.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.POSTDATE_FLD].ToString().Trim());
				objObject.Comment = odrPCS[IV_MiscellaneousIssueMasterTable.COMMENT_FLD].ToString().Trim();
				objObject.TransNo = odrPCS[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].ToString().Trim();
				objObject.UserName = odrPCS[IV_MiscellaneousIssueMasterTable.USERNAME_FLD].ToString().Trim();
                if (odrPCS[IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD] != DBNull.Value)
                {
                    objObject.LastChange = DateTime.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD].ToString().Trim());
                }
				objObject.CCNID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.CCNID_FLD].ToString().Trim());
				try
				{
					objObject.DesBinID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.DesBinID = 0;
				}
				try
				{
					objObject.SourceBinID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.SourceBinID = 0;
				}
				try
				{
					objObject.DesLocationID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.DesLocationID = 0;
				}
				try
				{
					objObject.SourceLocationID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.SourceLocationID = 0 ;
				}
				try
				{
					objObject.DesMasLocationID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.DesMasLocationID = 0;
				}
				try
				{
					objObject.SourceMasLocationID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.SourceMasLocationID = 0;
				}
				try
				{
					objObject.PartyID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.PARTYID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.PartyID = 0;
				}

				try
				{
					objObject.IssuePurposeID = int.Parse(odrPCS[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD].ToString().Trim());
				}
				catch 
				{
					objObject.IssuePurposeID = 0;
				}

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
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			IV_MiscellaneousIssueMasterVO objObject = (IV_MiscellaneousIssueMasterVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE IV_MiscellaneousIssueMaster SET "
				+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + "=   ?" + ","
				+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD + "=  ?"
				+" WHERE " + IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.POSTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.POSTDATE_FLD].Value = objObject.PostDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.COMMENT_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.COMMENT_FLD].Value = objObject.Comment;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.TRANSNO_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].Value = objObject.TransNo;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.USERNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD].Value = objObject.LastChange;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].Value = objObject.DesBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].Value = objObject.SourceBinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].Value = objObject.DesLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD].Value = objObject.SourceLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].Value = objObject.DesMasLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD].Value = objObject.SourceMasLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD].Value = objObject.MiscellaneousIssueMasterID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
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
				+ IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD
					+ " FROM " + IV_MiscellaneousIssueMasterTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,IV_MiscellaneousIssueMasterTable.TABLE_NAME);

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
				+ IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.POSTDATE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.COMMENT_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.TRANSNO_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.USERNAME_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.LASTCHANGE_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.CCNID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.PARTYID_FLD + ","
				+ IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD 
		+ "  FROM " + IV_MiscellaneousIssueMasterTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,IV_MiscellaneousIssueMasterTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
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
