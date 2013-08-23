using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.TableFrame.DS
{
	public class sys_TableFieldDS 
	{
		public sys_TableFieldDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.DS.sys_TableFieldDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_TableField
		///    </Description>
		///    <Inputs>
		///        sys_TableFieldVO       
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
		///       Monday, December 27, 2004
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
				sys_TableFieldVO objObject = (sys_TableFieldVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO sys_TableField("
				+ sys_TableFieldTable.TABLEID_FLD + ","
				+ sys_TableFieldTable.FIELDNAME_FLD + ","
				+ sys_TableFieldTable.CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.CAPTION_FLD + ","
				+ sys_TableFieldTable.INVISIBLE_FLD + ","
				+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
				+ sys_TableFieldTable.ALIGN_FLD + ","
				+ sys_TableFieldTable.WIDTH_FLD + ","
				+ sys_TableFieldTable.SORTTYPE_FLD + ","
				+ sys_TableFieldTable.FORMATS_FLD + ","
				+ sys_TableFieldTable.READONLY_FLD + ","
				+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
				+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
				+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
				+ sys_TableFieldTable.ITEMS_FLD + ","
				+ sys_TableFieldTable.FROMTABLE_FLD + ","
				+ sys_TableFieldTable.FROMFIELD_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD3_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD1_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
                + sys_TableFieldTable.FORMATFIELD1_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD1_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD2_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD2_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD2_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD3_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD3_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD3_FLD + ","
				+ sys_TableFieldTable.FIELDORDER_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?"
				+ ",?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.TABLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.TABLEID_FLD].Value = objObject.TableID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELDNAME_FLD].Value = objObject.FieldName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONJP_FLD].Value = objObject.CaptionJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONVN_FLD].Value = objObject.CaptionVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONEN_FLD].Value = objObject.CaptionEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTION_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTION_FLD].Value = objObject.Caption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.INVISIBLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.INVISIBLE_FLD].Value = objObject.Invisible;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CHARACTERCASE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.CHARACTERCASE_FLD].Value = objObject.CharacterCase;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.ALIGN_FLD].Value = objObject.Align;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.SORTTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.SORTTYPE_FLD].Value = objObject.SortType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATS_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATS_FLD].Value = objObject.Formats;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.READONLY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.READONLY_FLD].Value = objObject.ReadOnly;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.NOTALLOWNULL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.NOTALLOWNULL_FLD].Value = objObject.NotAllowNull;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.IDENTITYCOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.IDENTITYCOLUMN_FLD].Value = objObject.IdentityColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.UNIQUECOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.UNIQUECOLUMN_FLD].Value = objObject.UniqueColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ITEMS_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.ITEMS_FLD].Value = objObject.Items;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FROMTABLE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FROMTABLE_FLD].Value = objObject.FromTable;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FROMFIELD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FROMFIELD_FLD].Value = objObject.FromField;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD1_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD1_FLD].Value = objObject.FilterField1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD2_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD2_FLD].Value = objObject.FilterField2;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD3_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD3_FLD].Value = objObject.FilterField3;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD1_FLD, OleDbType.Integer));
				if (objObject.Align1 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD1_FLD].Value = objObject.Align1;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD1_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].Value = objObject.CaptionEN1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONJP_FLD].Value = objObject.CaptionJP1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONVN_FLD].Value = objObject.CaptionVN1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD1_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD1_FLD].Value = objObject.Formats1;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD1_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD1_FLD].Value = objObject.Width1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD2_FLD, OleDbType.Integer));
				if (objObject.Align2 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD2_FLD].Value = objObject.Align2;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD2_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONEN_FLD].Value = objObject.CaptionEN2;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONJP_FLD].Value = objObject.CaptionJP2;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONVN_FLD].Value = objObject.CaptionVN2;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD2_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD2_FLD].Value = objObject.Formats2;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD2_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD2_FLD].Value = objObject.Width2;


				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD3_FLD, OleDbType.Integer));
				if (objObject.Align3 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD3_FLD].Value = objObject.Align3;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD3_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONEN_FLD].Value = objObject.CaptionEN3;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONJP_FLD].Value = objObject.CaptionJP3;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONVN_FLD].Value = objObject.CaptionVN3;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD3_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD3_FLD].Value = objObject.Formats3;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD3_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD3_FLD].Value = objObject.Width3;



				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELDORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELDORDER_FLD].Value = objObject.FieldOrder;


				
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
		///       This method uses to delete data from sys_TableField
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
			strSql=	"DELETE " + sys_TableFieldTable.TABLE_NAME + " WHERE  " + "TableFieldID" + "=" + pintID.ToString();
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
		///       This method uses to delete data from sys_TableField
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

		public void DeleteTable(int pintTableID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + sys_TableFieldTable.TABLE_NAME + " WHERE  " + sys_TableFieldTable.TABLEID_FLD + "=" + pintTableID.ToString();
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
		///       This method uses to get data from sys_TableField
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_TableFieldVO
		///    </Outputs>
		///    <Returns>
		///       sys_TableFieldVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
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
				strSql=	"SELECT "
				+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
				+ sys_TableFieldTable.TABLEID_FLD + ","
				+ sys_TableFieldTable.FIELDNAME_FLD + ","
				+ sys_TableFieldTable.CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.CAPTION_FLD + ","
				+ sys_TableFieldTable.INVISIBLE_FLD + ","
				+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
				+ sys_TableFieldTable.ALIGN_FLD + ","
				+ sys_TableFieldTable.WIDTH_FLD + ","
				+ sys_TableFieldTable.SORTTYPE_FLD + ","
				+ sys_TableFieldTable.FORMATS_FLD + ","
				+ sys_TableFieldTable.READONLY_FLD + ","
				+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
				+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
				+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
				+ sys_TableFieldTable.ITEMS_FLD + ","
				+ sys_TableFieldTable.FROMTABLE_FLD + ","
				+ sys_TableFieldTable.FROMFIELD_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD2_FLD + ","

				+ sys_TableFieldTable.FILTERFIELD3_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD1_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD1_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD1_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD2_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD2_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD2_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD3_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD3_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD3_FLD + ","


				+ sys_TableFieldTable.FIELDORDER_FLD
				+ " FROM " + sys_TableFieldTable.TABLE_NAME
				+" WHERE " + sys_TableFieldTable.TABLEFIELDID_FLD + "=" + pintID;

				DataAccess.Utils utils = new  Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_TableFieldVO objObject = new sys_TableFieldVO();

				while (odrPCS.Read())
				{ 
					objObject.TableFieldID = int.Parse(odrPCS[sys_TableFieldTable.TABLEFIELDID_FLD].ToString());
					objObject.TableID = int.Parse(odrPCS[sys_TableFieldTable.TABLEID_FLD].ToString());
					objObject.FieldName = odrPCS[sys_TableFieldTable.FIELDNAME_FLD].ToString();
					objObject.CaptionJP = odrPCS[sys_TableFieldTable.CAPTIONJP_FLD].ToString();
					objObject.CaptionVN = odrPCS[sys_TableFieldTable.CAPTIONVN_FLD].ToString();
					objObject.CaptionEN = odrPCS[sys_TableFieldTable.CAPTIONEN_FLD].ToString();
					objObject.Caption = odrPCS[sys_TableFieldTable.CAPTION_FLD].ToString();
					objObject.Invisible = bool.Parse(odrPCS[sys_TableFieldTable.INVISIBLE_FLD].ToString());
					objObject.CharacterCase = int.Parse(odrPCS[sys_TableFieldTable.CHARACTERCASE_FLD].ToString());
					objObject.Align = int.Parse(odrPCS[sys_TableFieldTable.ALIGN_FLD].ToString());
					objObject.Width = int.Parse(odrPCS[sys_TableFieldTable.WIDTH_FLD].ToString());
					objObject.SortType = int.Parse(odrPCS[sys_TableFieldTable.SORTTYPE_FLD].ToString());
					objObject.Formats = odrPCS[sys_TableFieldTable.FORMATS_FLD].ToString();
					objObject.ReadOnly = bool.Parse(odrPCS[sys_TableFieldTable.READONLY_FLD].ToString());
					objObject.NotAllowNull = bool.Parse(odrPCS[sys_TableFieldTable.NOTALLOWNULL_FLD].ToString());
					objObject.IdentityColumn = bool.Parse(odrPCS[sys_TableFieldTable.IDENTITYCOLUMN_FLD].ToString());
					objObject.UniqueColumn = bool.Parse(odrPCS[sys_TableFieldTable.UNIQUECOLUMN_FLD].ToString());
					objObject.Items = odrPCS[sys_TableFieldTable.ITEMS_FLD].ToString();
					objObject.FromTable = odrPCS[sys_TableFieldTable.FROMTABLE_FLD].ToString();
					objObject.FromField = odrPCS[sys_TableFieldTable.FROMFIELD_FLD].ToString();
					objObject.FilterField1 = odrPCS[sys_TableFieldTable.FILTERFIELD1_FLD].ToString();
					objObject.FilterField2 = odrPCS[sys_TableFieldTable.FILTERFIELD2_FLD].ToString();

					objObject.FilterField3 = odrPCS[sys_TableFieldTable.FILTERFIELD3_FLD].ToString();
					if (odrPCS[sys_TableFieldTable.ALIGNFIELD1_FLD] != DBNull.Value)
					{
						objObject.Align1 = int.Parse(odrPCS[sys_TableFieldTable.ALIGNFIELD1_FLD].ToString());
					}
					else
						objObject.Align1 = -1;
					objObject.CaptionEN1 = odrPCS[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].ToString();
					objObject.CaptionVN1 = odrPCS[sys_TableFieldTable.FIELD1CAPTIONVN_FLD].ToString();
					objObject.CaptionJP1 = odrPCS[sys_TableFieldTable.FIELD1CAPTIONJP_FLD].ToString();
					objObject.Formats1 = odrPCS[sys_TableFieldTable.FORMATFIELD1_FLD].ToString();
					if (odrPCS[sys_TableFieldTable.WIDTHFIELD1_FLD] != DBNull.Value)
					{
						objObject.Width1 = int.Parse(odrPCS[sys_TableFieldTable.WIDTHFIELD1_FLD].ToString());
					}
					else
						objObject.Width1 = 0;
					if (odrPCS[sys_TableFieldTable.ALIGNFIELD2_FLD].ToString() != string.Empty)
					{
						objObject.Align2 = int.Parse(odrPCS[sys_TableFieldTable.ALIGNFIELD2_FLD].ToString());
					}
					else
						objObject.Align2 = -1;
					objObject.CaptionEN2 = odrPCS[sys_TableFieldTable.FIELD2CAPTIONEN_FLD].ToString();
					objObject.CaptionVN2 = odrPCS[sys_TableFieldTable.FIELD2CAPTIONVN_FLD].ToString();
					objObject.CaptionJP2 = odrPCS[sys_TableFieldTable.FIELD2CAPTIONJP_FLD].ToString();
					
					objObject.Formats2 = odrPCS[sys_TableFieldTable.FORMATFIELD2_FLD].ToString();
					if (odrPCS[sys_TableFieldTable.WIDTHFIELD2_FLD] != DBNull.Value)
					{
						objObject.Width2 = int.Parse(odrPCS[sys_TableFieldTable.WIDTHFIELD2_FLD].ToString());
					}
					else
						objObject.Width2 = 0;
					if (odrPCS[sys_TableFieldTable.ALIGNFIELD3_FLD] != DBNull.Value)
					{
						objObject.Align3 = int.Parse(odrPCS[sys_TableFieldTable.ALIGNFIELD3_FLD].ToString());
					}
					else
						objObject.Align3 = -1;
					objObject.CaptionEN3 = odrPCS[sys_TableFieldTable.FIELD3CAPTIONEN_FLD].ToString();
					objObject.CaptionVN3 = odrPCS[sys_TableFieldTable.FIELD3CAPTIONVN_FLD].ToString();
					objObject.CaptionJP3 = odrPCS[sys_TableFieldTable.FIELD3CAPTIONJP_FLD].ToString();
					objObject.Formats3 = odrPCS[sys_TableFieldTable.FORMATFIELD3_FLD].ToString();
					if (odrPCS[sys_TableFieldTable.WIDTHFIELD3_FLD] != DBNull.Value)
					{
						objObject.Width3 = int.Parse(odrPCS[sys_TableFieldTable.WIDTHFIELD3_FLD].ToString());
					}
					else
						objObject.Width3 = 0;
					objObject.FieldOrder = int.Parse(odrPCS[sys_TableFieldTable.FIELDORDER_FLD].ToString());
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
		///       This method uses to get data from sys_TableField
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_TableFieldVO
		///    </Outputs>
		///    <Returns>
		///       sys_TableFieldVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public int GetTotalColumnWidth(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetTotalColumnWidth()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT SUM("
					+ sys_TableFieldTable.WIDTH_FLD + " + isnull(WidthField1,0) + isnull(WidthField2,0) + isnull(WidthField3,0)) "
					+ " FROM " + sys_TableFieldTable.TABLE_NAME
					+" WHERE " + sys_TableFieldTable.TABLEID_FLD + "=" + pintID;

				DataAccess.Utils utils = new  Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					return 0;
				}
				else
				{
					string strResult = objResult.ToString();
					if (strResult == String.Empty)
					{
						return 0;
					}
					else
					{
						return int.Parse(strResult);
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
		///       This method uses to update data to sys_TableField
		///    </Description>
		///    <Inputs>
		///       sys_TableFieldVO       
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

			sys_TableFieldVO objObject = (sys_TableFieldVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE sys_TableField SET "
				+ sys_TableFieldTable.TABLEID_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELDNAME_FLD + "=   ?" + ","
				+ sys_TableFieldTable.CAPTIONJP_FLD + "=   ?" + ","
				+ sys_TableFieldTable.CAPTIONVN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.CAPTIONEN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.CAPTION_FLD + "=   ?" + ","
				+ sys_TableFieldTable.INVISIBLE_FLD + "=   ?" + ","
				+ sys_TableFieldTable.CHARACTERCASE_FLD + "=   ?" + ","
				+ sys_TableFieldTable.ALIGN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.WIDTH_FLD + "=   ?" + ","
				+ sys_TableFieldTable.SORTTYPE_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FORMATS_FLD + "=   ?" + ","
				+ sys_TableFieldTable.READONLY_FLD + "=   ?" + ","
				+ sys_TableFieldTable.NOTALLOWNULL_FLD + "=   ?" + ","
				+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.UNIQUECOLUMN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.ITEMS_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FROMTABLE_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FROMFIELD_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FILTERFIELD1_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FILTERFIELD2_FLD + "=   ?" + ","

				+ sys_TableFieldTable.FILTERFIELD3_FLD + "=   ?" + ","
				+ sys_TableFieldTable.ALIGNFIELD1_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FORMATFIELD1_FLD + "=   ?" + ","
				+ sys_TableFieldTable.WIDTHFIELD1_FLD + "=   ?" + ","
				+ sys_TableFieldTable.ALIGNFIELD2_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FORMATFIELD2_FLD + "=   ?" + ","
				+ sys_TableFieldTable.WIDTHFIELD2_FLD + "=   ?" + ","
				+ sys_TableFieldTable.ALIGNFIELD3_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + "=   ?" + ","
				+ sys_TableFieldTable.FORMATFIELD3_FLD + "=   ?" + ","
				+ sys_TableFieldTable.WIDTHFIELD3_FLD + "=   ?" + ","

				+ sys_TableFieldTable.FIELDORDER_FLD + "=  ?"

				+" WHERE " + sys_TableFieldTable.TABLEFIELDID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.TABLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.TABLEID_FLD].Value = objObject.TableID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELDNAME_FLD].Value = objObject.FieldName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONJP_FLD].Value = objObject.CaptionJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONVN_FLD].Value = objObject.CaptionVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTIONEN_FLD].Value = objObject.CaptionEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CAPTION_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.CAPTION_FLD].Value = objObject.Caption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.INVISIBLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.INVISIBLE_FLD].Value = objObject.Invisible;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.CHARACTERCASE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.CHARACTERCASE_FLD].Value = objObject.CharacterCase;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGN_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.ALIGN_FLD].Value = objObject.Align;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.SORTTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.SORTTYPE_FLD].Value = objObject.SortType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATS_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATS_FLD].Value = objObject.Formats;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.READONLY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.READONLY_FLD].Value = objObject.ReadOnly;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.NOTALLOWNULL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.NOTALLOWNULL_FLD].Value = objObject.NotAllowNull;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.IDENTITYCOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.IDENTITYCOLUMN_FLD].Value = objObject.IdentityColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.UNIQUECOLUMN_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_TableFieldTable.UNIQUECOLUMN_FLD].Value = objObject.UniqueColumn;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ITEMS_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.ITEMS_FLD].Value = objObject.Items;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FROMTABLE_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FROMTABLE_FLD].Value = objObject.FromTable;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FROMFIELD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FROMFIELD_FLD].Value = objObject.FromField;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD1_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD1_FLD].Value = objObject.FilterField1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD2_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD2_FLD].Value = objObject.FilterField2;


				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FILTERFIELD3_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FILTERFIELD3_FLD].Value = objObject.FilterField3;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD1_FLD, OleDbType.Integer));
				if (objObject.Align1 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD1_FLD].Value = objObject.Align1;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD1_FLD].Value = DBNull.Value;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].Value = objObject.CaptionEN1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONJP_FLD].Value = objObject.CaptionJP1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD1CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD1CAPTIONVN_FLD].Value = objObject.CaptionVN1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD1_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD1_FLD].Value = objObject.Formats1;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD1_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD1_FLD].Value = objObject.Width1;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD2_FLD, OleDbType.Integer));
				if (objObject.Align2 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD2_FLD].Value = objObject.Align2;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD2_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONEN_FLD].Value = objObject.CaptionEN2;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONJP_FLD].Value = objObject.CaptionJP2;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD2CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD2CAPTIONVN_FLD].Value = objObject.CaptionVN2;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD2_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD2_FLD].Value = objObject.Formats2;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD2_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD2_FLD].Value = objObject.Width2;


				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.ALIGNFIELD3_FLD, OleDbType.Integer));
				if (objObject.Align3 != -1)
				{
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD3_FLD].Value = objObject.Align3;
				}
				else
					ocmdPCS.Parameters[sys_TableFieldTable.ALIGNFIELD3_FLD].Value = DBNull.Value;
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONEN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONEN_FLD].Value = objObject.CaptionEN3;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONJP_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONJP_FLD].Value = objObject.CaptionJP3;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELD3CAPTIONVN_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELD3CAPTIONVN_FLD].Value = objObject.CaptionVN3;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FORMATFIELD3_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_TableFieldTable.FORMATFIELD3_FLD].Value = objObject.Formats3;	

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.WIDTHFIELD3_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.WIDTHFIELD3_FLD].Value = objObject.Width3;




				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.FIELDORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.FIELDORDER_FLD].Value = objObject.FieldOrder;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_TableFieldTable.TABLEFIELDID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_TableFieldTable.TABLEFIELDID_FLD].Value = objObject.TableFieldID;


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
		///       This method uses to get all data from sys_TableField
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
		///       Monday, December 27, 2004
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
				+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
				+ sys_TableFieldTable.TABLEID_FLD + ","
				+ sys_TableFieldTable.FIELDNAME_FLD + ","
				+ sys_TableFieldTable.CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.CAPTION_FLD + ","
				+ sys_TableFieldTable.INVISIBLE_FLD + ","
				+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
				+ sys_TableFieldTable.ALIGN_FLD + ","
				+ sys_TableFieldTable.WIDTH_FLD + ","
				+ sys_TableFieldTable.SORTTYPE_FLD + ","
				+ sys_TableFieldTable.FORMATS_FLD + ","
				+ sys_TableFieldTable.READONLY_FLD + ","
				+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
				+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
				+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
				+ sys_TableFieldTable.ITEMS_FLD + ","
				+ sys_TableFieldTable.FROMTABLE_FLD + ","
				+ sys_TableFieldTable.FROMFIELD_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD3_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD1_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD1_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD1_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD2_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD2_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD2_FLD + ","
				+ sys_TableFieldTable.ALIGNFIELD3_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.FORMATFIELD3_FLD + ","
				+ sys_TableFieldTable.WIDTHFIELD3_FLD + ","
				+ sys_TableFieldTable.FIELDORDER_FLD
					+ " FROM " + sys_TableFieldTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_TableFieldTable.TABLE_NAME);

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
		///       This method uses to get all data from sys_TableField
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
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List(int pTableID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					#region Deleted

					//					+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
					//					+ sys_TableFieldTable.TABLEID_FLD + ","
					//					+ sys_TableFieldTable.FIELDNAME_FLD + ","
					//					+ sys_TableFieldTable.CAPTIONJP_FLD + ","
					//					+ sys_TableFieldTable.CAPTIONVN_FLD + ","
					//					+ sys_TableFieldTable.CAPTIONEN_FLD + ","
					//					+ sys_TableFieldTable.CAPTION_FLD + ","
					//					+ sys_TableFieldTable.INVISIBLE_FLD + ","
					//					+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
					//					+ sys_TableFieldTable.ALIGN_FLD + ","
					//					+ sys_TableFieldTable.WIDTH_FLD + ","
					//					+ sys_TableFieldTable.SORTTYPE_FLD + ","
					//					+ sys_TableFieldTable.FORMATS_FLD + ","
					//					+ sys_TableFieldTable.READONLY_FLD + ","
					//					+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
					//					+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
					//					+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
					//					+ sys_TableFieldTable.ITEMS_FLD + ","
					//					+ sys_TableFieldTable.FROMTABLE_FLD + ","
					//					+ sys_TableFieldTable.FROMFIELD_FLD + ","
					//					+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
					//					+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
					//					+ sys_TableFieldTable.FIELDORDER_FLD

					#endregion

					+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
					+ sys_TableFieldTable.TABLEID_FLD + ","
					+ sys_TableFieldTable.FIELDNAME_FLD + ","
					+ sys_TableFieldTable.CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.CAPTION_FLD + ","
					+ sys_TableFieldTable.INVISIBLE_FLD + ","
					+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
					+ sys_TableFieldTable.ALIGN_FLD + ","
					+ sys_TableFieldTable.WIDTH_FLD + ","
					+ sys_TableFieldTable.SORTTYPE_FLD + ","
					+ sys_TableFieldTable.FORMATS_FLD + ","
					+ sys_TableFieldTable.READONLY_FLD + ","
					+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
					+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
					+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
					+ sys_TableFieldTable.ITEMS_FLD + ","
					+ sys_TableFieldTable.FROMTABLE_FLD + ","
					+ sys_TableFieldTable.FROMFIELD_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD3_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD1_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD1_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD1_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD2_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD2_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD2_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD3_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD3_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD3_FLD + ","
					+ sys_TableFieldTable.FIELDORDER_FLD
					+ " FROM " + sys_TableFieldTable.TABLE_NAME
					+ " WHERE TableID=" + pTableID
					+ " ORDER BY " + sys_TableFieldTable.FIELDORDER_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_TableFieldTable.TABLE_NAME);

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
		///       This method uses to get fieldname where TableName
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
		///       Monday, December 27, 2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.TABLEFIELDID_FLD + ","
					//+ sys_TableFieldTable.TABLEID_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELDNAME_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.CAPTION_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.INVISIBLE_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.CHARACTERCASE_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.ALIGN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.WIDTH_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.SORTTYPE_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FORMATS_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.READONLY_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.NOTALLOWNULL_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.ITEMS_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FROMTABLE_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FROMFIELD_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FILTERFIELD1_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FILTERFIELD2_FLD + ","

					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FILTERFIELD3_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.ALIGNFIELD1_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FORMATFIELD1_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.WIDTHFIELD1_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.ALIGNFIELD2_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FORMATFIELD2_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.WIDTHFIELD2_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.ALIGNFIELD3_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FORMATFIELD3_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.WIDTHFIELD3_FLD + ","
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.FIELDORDER_FLD
					+ " FROM " + sys_TableFieldTable.TABLE_NAME
					+ " INNER JOIN " + sys_TableTable.TABLE_NAME + " ON " 
					+ sys_TableFieldTable.TABLE_NAME + "." + sys_TableFieldTable.TABLEID_FLD  
					+ " = " + sys_TableTable.TABLE_NAME + "." + sys_TableTable.TABLEID_FLD
					+ " WHERE TableOrView = '" + pstrTableName + "'"
					+ " ORDER BY " + sys_TableFieldTable.FIELDORDER_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_TableFieldTable.TABLE_NAME);

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

		/// <summary>
		/// UpdateDataSetByTableID
		/// </summary>
		/// <param name="pdstData"></param>
		/// <param name="pintTableID"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Dec 1 2005</date>
		public void UpdateDataSetByTableID(DataSet pdstData, int pintTableID)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetByTableID()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "

					#region Deleted

//					+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
//					+ sys_TableFieldTable.TABLEID_FLD + ","
//					+ sys_TableFieldTable.FIELDNAME_FLD + ","
//					+ sys_TableFieldTable.CAPTIONJP_FLD + ","
//					+ sys_TableFieldTable.CAPTIONVN_FLD + ","
//					+ sys_TableFieldTable.CAPTIONEN_FLD + ","
//					+ sys_TableFieldTable.CAPTION_FLD + ","
//					+ sys_TableFieldTable.INVISIBLE_FLD + ","
//					+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
//					+ sys_TableFieldTable.ALIGN_FLD + ","
//					+ sys_TableFieldTable.WIDTH_FLD + ","
//					+ sys_TableFieldTable.SORTTYPE_FLD + ","
//					+ sys_TableFieldTable.FORMATS_FLD + ","
//					+ sys_TableFieldTable.READONLY_FLD + ","
//					+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
//					+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
//					+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
//					+ sys_TableFieldTable.ITEMS_FLD + ","
//					+ sys_TableFieldTable.FROMTABLE_FLD + ","
//					+ sys_TableFieldTable.FROMFIELD_FLD + ","
//					+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
//					+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
//					+ sys_TableFieldTable.FIELDORDER_FLD

					#endregion

					+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
					+ sys_TableFieldTable.TABLEID_FLD + ","
					+ sys_TableFieldTable.FIELDNAME_FLD + ","
					+ sys_TableFieldTable.CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.CAPTION_FLD + ","
					+ sys_TableFieldTable.INVISIBLE_FLD + ","
					+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
					+ sys_TableFieldTable.ALIGN_FLD + ","
					+ sys_TableFieldTable.WIDTH_FLD + ","
					+ sys_TableFieldTable.SORTTYPE_FLD + ","
					+ sys_TableFieldTable.FORMATS_FLD + ","
					+ sys_TableFieldTable.READONLY_FLD + ","
					+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
					+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
					+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
					+ sys_TableFieldTable.ITEMS_FLD + ","
					+ sys_TableFieldTable.FROMTABLE_FLD + ","
					+ sys_TableFieldTable.FROMFIELD_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
					+ sys_TableFieldTable.FILTERFIELD3_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD1_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD1CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD1_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD1_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD2_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD2CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD2_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD2_FLD + ","
					+ sys_TableFieldTable.ALIGNFIELD3_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONEN_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONVN_FLD + ","
					+ sys_TableFieldTable.FIELD3CAPTIONJP_FLD + ","
					+ sys_TableFieldTable.FORMATFIELD3_FLD + ","
					+ sys_TableFieldTable.WIDTHFIELD3_FLD + ","
					+ sys_TableFieldTable.FIELDORDER_FLD
					+ " FROM " + sys_TableFieldTable.TABLE_NAME
					+ " WHERE " + sys_TableFieldTable.TABLEID_FLD + " = " + pintTableID.ToString();


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,sys_TableFieldTable.TABLE_NAME);

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
		///       Monday, December 27, 2004
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
				+ sys_TableFieldTable.TABLEFIELDID_FLD + ","
				+ sys_TableFieldTable.TABLEID_FLD + ","
				+ sys_TableFieldTable.FIELDNAME_FLD + ","
				+ sys_TableFieldTable.CAPTIONJP_FLD + ","
				+ sys_TableFieldTable.CAPTIONVN_FLD + ","
				+ sys_TableFieldTable.CAPTIONEN_FLD + ","
				+ sys_TableFieldTable.CAPTION_FLD + ","
				+ sys_TableFieldTable.INVISIBLE_FLD + ","
				+ sys_TableFieldTable.CHARACTERCASE_FLD + ","
				+ sys_TableFieldTable.ALIGN_FLD + ","
				+ sys_TableFieldTable.WIDTH_FLD + ","
				+ sys_TableFieldTable.SORTTYPE_FLD + ","
				+ sys_TableFieldTable.FORMATS_FLD + ","
				+ sys_TableFieldTable.READONLY_FLD + ","
				+ sys_TableFieldTable.NOTALLOWNULL_FLD + ","
				+ sys_TableFieldTable.IDENTITYCOLUMN_FLD + ","
				+ sys_TableFieldTable.UNIQUECOLUMN_FLD + ","
				+ sys_TableFieldTable.ITEMS_FLD + ","
				+ sys_TableFieldTable.FROMTABLE_FLD + ","
				+ sys_TableFieldTable.FROMFIELD_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD1_FLD + ","
				+ sys_TableFieldTable.FILTERFIELD2_FLD + ","
				+ sys_TableFieldTable.FIELDORDER_FLD;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_TableFieldTable.TABLE_NAME);

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
		///       List fieldname via TableName to fill in combo box
		///    </Description>
		///    <Inputs>
		///        pintTableID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListFieldName(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".ListFieldName()";
			DataSet dstPCS = new DataSet();

			string strSql = "SELECT " + SchemaColumnTable.COLUMN_NAME_FLD
				+ "," +  SchemaColumnTable.ORDINAL_POSITION_FLD
				+ " FROM " + SchemaColumnTable.TABLE_NAME
				+ " WHERE " + SchemaColumnTable.TABLE_NAME_FLD + " = '" + pstrTableName + "'"
				+ " ORDER BY " + SchemaColumnTable.ORDINAL_POSITION_FLD;

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SchemaColumnTable.TABLE_NAME);
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
		///       List fieldname via TableName to fill in combo box
		///    </Description>
		///    <Inputs>
		///        pintTableID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetColumnProperty(string pstrTableName,string pstrColumnName)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " 
					+ "COLUMNPROPERTY( OBJECT_ID('" + pstrTableName + "'),'" + pstrColumnName + "','IsIdentity') Isdentity"
					+ "," + "COLUMNPROPERTY( OBJECT_ID('" + pstrTableName + "'),'" + pstrColumnName + "','IsIndexable') IsIndexable"
					+ "," + "COLUMNPROPERTY( OBJECT_ID('" + pstrTableName + "'),'" + pstrColumnName + "','AllowsNull') AllowsNull";


				DataAccess.Utils utils = new  Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ArrayList arrProperty = new ArrayList();

				while (odrPCS.Read())
				{ 
					// Isdentity
					arrProperty.Add(int.Parse(odrPCS[0].ToString()));
					// IsIndexable
					arrProperty.Add(int.Parse(odrPCS[1].ToString()));
					// AllowsNull
					arrProperty.Add(int.Parse(odrPCS[2].ToString()));
				}
				return arrProperty;
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
		/// GetInformationSchema
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, April 14 2006</date>
		public DataSet GetInformationSchema(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".GetInformationSchema()";
			DataSet dstPCS = new DataSet();

			string strSql = "select COLUMN_NAME, DATA_TYPE "
				+ " FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME"
				+ " = '" + pstrTableName + "'";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,SchemaColumnTable.TABLE_NAME);
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
		///       List fieldname via TableName to fill in combo box
		///    </Description>
		///    <Inputs>
		///        pintTableID
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       15-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
//		public DataSet ListFieldName(int pintTableID)
//		{
//			const string METHOD_NAME = THIS + ".ListFieldName()";
//			DataSet dstPCS = new DataSet();
//
//			string strSql = "SELECT " + SchemaColumnTable.COLUMN_NAME_FLD
//				+ "," +  SchemaColumnTable.ORDINAL_POSITION_FLD
//				+ " FROM " + SchemaColumnTable.TABLE_NAME
//				+ " WHERE " + SchemaColumnTable.TABLE_NAME_FLD + " = " + pintTableID
//				+ " ORDER BY " + SchemaColumnTable.ORDINAL_POSITION_FLD;
//
//			OleDbConnection oconPCS =null;
//			OleDbCommand ocmdPCS = null;
//			try 
//			{
//				DataAccess.Utils utils = new DataAccess.Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//				ocmdPCS.Connection.Open();
//
//				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
//				odadPCS.Fill(dstPCS,sys_TableFieldTable.TABLE_NAME);
//				return dstPCS;
//
//			}
//			catch(OleDbException ex)
//			{
//				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
//			}			
//
//			catch (Exception ex) 
//			{
//				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
//			}
//
//			finally 
//			{
//				if (oconPCS!=null) 
//				{
//					if (oconPCS.State != ConnectionState.Closed) 
//					{
//						oconPCS.Close();
//					}
//				}
//			}
//		}		
	
	}
}
