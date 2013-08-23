using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportParaDS 
	{
		public sys_ReportParaDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportParaDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        sys_ReportParaVO       
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

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				sys_ReportParaVO objObject = (sys_ReportParaVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportParaTable.TABLE_NAME + "("
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARAORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.PARAORDER_FLD].Value = objObject.ParaOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARANAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARANAME_FLD].Value = objObject.ParaName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARACAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARACAPTION_FLD].Value = objObject.ParaCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DATATYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.DATATYPE_FLD].Value = objObject.DataType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.OPTIONAL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.OPTIONAL_FLD].Value = objObject.Optional;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.TAGVALUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.TAGVALUE_FLD].Value = objObject.TagValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SAMEROW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.SAMEROW_FLD].Value = objObject.SameRow;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DEFAULTVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.DEFAULTVALUE_FLD].Value = objObject.DefaultValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.ITEMS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.ITEMS_FLD].Value = objObject.Items;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMTABLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMTABLE_FLD].Value = objObject.FromTable;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMFIELD_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMFIELD_FLD].Value = objObject.FromField;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1_FLD].Value = objObject.FilterField1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].Value = objObject.FilterField1Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2_FLD].Value = objObject.FilterField2;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].Value = objObject.FilterField2Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SQLCLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.SQLCLAUSE_FLD].Value = objObject.SQLCLause;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.MULTISELECTION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.MULTISELECTION_FLD].Value = objObject.MultiSelection;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WHERECLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.WHERECLAUSE_FLD].Value = objObject.WhereClause;


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
		///       This method uses to delete data from sys_ReportPara
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
			strSql = "DELETE " + sys_ReportParaTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportParaTable.REPORTID_FLD + "=" + pintID.ToString().Trim();
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
		///       This method uses to delete data from sys_ReportPara
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportParaTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportParaTable.REPORTID_FLD + "= ? "; // + pstrID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrID ;
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
		///       This method uses to delete data from sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ReportID, ParaName    
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       08-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID, string pstrParaName)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportParaTable.TABLE_NAME 
				+ " WHERE  " + sys_ReportParaTable.REPORTID_FLD + "= ? " //'" + pstrID + "'"
				+ " AND " + sys_ReportParaTable.PARANAME_FLD + "= ? "; // '" + pstrParaName + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrID ;
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.PARANAME_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.PARANAME_FLD ].Value = pstrParaName ;
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
		///       This method uses to get data from sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportParaVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportParaVO
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
				strSql = "SELECT "
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportParaVO objObject = new sys_ReportParaVO();

				while (odrPCS.Read())
				{
					objObject.ReportID = odrPCS[sys_ReportParaTable.REPORTID_FLD].ToString().Trim();
					objObject.ParaOrder = int.Parse(odrPCS[sys_ReportParaTable.PARAORDER_FLD].ToString().Trim());
					objObject.ParaName = odrPCS[sys_ReportParaTable.PARANAME_FLD].ToString().Trim();
					objObject.ParaCaption = odrPCS[sys_ReportParaTable.PARACAPTION_FLD].ToString().Trim();
					objObject.DataType = int.Parse(odrPCS[sys_ReportParaTable.DATATYPE_FLD].ToString().Trim());
					objObject.Width = int.Parse(odrPCS[sys_ReportParaTable.WIDTH_FLD].ToString().Trim());
					objObject.Optional = Boolean.Parse(odrPCS[sys_ReportParaTable.OPTIONAL_FLD].ToString().Trim());
					objObject.TagValue = Boolean.Parse(odrPCS[sys_ReportParaTable.TAGVALUE_FLD].ToString().Trim());
					objObject.SameRow = Boolean.Parse(odrPCS[sys_ReportParaTable.SAMEROW_FLD].ToString().Trim());
					objObject.DefaultValue = odrPCS[sys_ReportParaTable.DEFAULTVALUE_FLD].ToString().Trim();
					objObject.Items = odrPCS[sys_ReportParaTable.ITEMS_FLD].ToString().Trim();
					objObject.FromTable = odrPCS[sys_ReportParaTable.FROMTABLE_FLD].ToString().Trim();
					objObject.FromField = odrPCS[sys_ReportParaTable.FROMFIELD_FLD].ToString().Trim();
					objObject.FilterField1 = odrPCS[sys_ReportParaTable.FILTERFIELD1_FLD].ToString().Trim();
					objObject.FilterField1Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].ToString().Trim());
					objObject.FilterField2 = odrPCS[sys_ReportParaTable.FILTERFIELD2_FLD].ToString().Trim();
					objObject.FilterField2Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].ToString().Trim());
					objObject.SQLCLause = odrPCS[sys_ReportParaTable.SQLCLAUSE_FLD].ToString().Trim();
					objObject.WhereClause = odrPCS[sys_ReportParaTable.WHERECLAUSE_FLD].ToString().Trim();

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
		///       This method uses to get data from sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ReportID, ParaName
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportParaVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportParaVO
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(string pstrID, string pstrParaName)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ? " // + pstrID + "'"
					+ " AND " + sys_ReportParaTable.PARANAME_FLD + "= ? "; //  + pstrParaName + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrID ;
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.PARANAME_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.PARANAME_FLD ].Value = pstrParaName ;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportParaVO objObject = new sys_ReportParaVO();

				while (odrPCS.Read())
				{
					objObject.ReportID = odrPCS[sys_ReportParaTable.REPORTID_FLD].ToString().Trim();
					objObject.ParaOrder = int.Parse(odrPCS[sys_ReportParaTable.PARAORDER_FLD].ToString().Trim());
					objObject.ParaName = odrPCS[sys_ReportParaTable.PARANAME_FLD].ToString().Trim();
					objObject.ParaCaption = odrPCS[sys_ReportParaTable.PARACAPTION_FLD].ToString().Trim();
					objObject.DataType = int.Parse(odrPCS[sys_ReportParaTable.DATATYPE_FLD].ToString().Trim());
					objObject.Width = int.Parse(odrPCS[sys_ReportParaTable.WIDTH_FLD].ToString().Trim());
					objObject.Optional = Boolean.Parse(odrPCS[sys_ReportParaTable.OPTIONAL_FLD].ToString().Trim());
					objObject.TagValue = Boolean.Parse(odrPCS[sys_ReportParaTable.TAGVALUE_FLD].ToString().Trim());
					objObject.SameRow = Boolean.Parse(odrPCS[sys_ReportParaTable.SAMEROW_FLD].ToString().Trim());
					objObject.DefaultValue = odrPCS[sys_ReportParaTable.DEFAULTVALUE_FLD].ToString().Trim();
					objObject.Items = odrPCS[sys_ReportParaTable.ITEMS_FLD].ToString().Trim();
					objObject.FromTable = odrPCS[sys_ReportParaTable.FROMTABLE_FLD].ToString().Trim();
					objObject.FromField = odrPCS[sys_ReportParaTable.FROMFIELD_FLD].ToString().Trim();
					objObject.FilterField1 = odrPCS[sys_ReportParaTable.FILTERFIELD1_FLD].ToString().Trim();
					objObject.FilterField1Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].ToString().Trim());
					objObject.FilterField2 = odrPCS[sys_ReportParaTable.FILTERFIELD2_FLD].ToString().Trim();
					objObject.FilterField2Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].ToString().Trim());
					objObject.SQLCLause = odrPCS[sys_ReportParaTable.SQLCLAUSE_FLD].ToString().Trim();
					objObject.WhereClause = odrPCS[sys_ReportParaTable.WHERECLAUSE_FLD].ToString().Trim();
					if (odrPCS[sys_ReportParaTable.MULTISELECTION_FLD] != DBNull.Value)
						objObject.MultiSelection = Convert.ToBoolean(odrPCS[sys_ReportParaTable.MULTISELECTION_FLD]);

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
		///       This method uses to get data from sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ReportID       
		///    </Inputs>
		///    <Outputs>
		///       List of sys_ReportParaVO object
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       03-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetObjectVOs(string pstrID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOs()";

			ArrayList arrObjectVOs = new ArrayList();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ? " // + pstrID + "'"
					+ " ORDER BY " + sys_ReportParaTable.PARAORDER_FLD + " ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrID ;
				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportParaVO voReportPara = new sys_ReportParaVO();
					voReportPara.ReportID = odrPCS[sys_ReportParaTable.REPORTID_FLD].ToString().Trim();
					voReportPara.ParaOrder = int.Parse(odrPCS[sys_ReportParaTable.PARAORDER_FLD].ToString().Trim());
					voReportPara.ParaName = odrPCS[sys_ReportParaTable.PARANAME_FLD].ToString().Trim();
					voReportPara.ParaCaption = odrPCS[sys_ReportParaTable.PARACAPTION_FLD].ToString().Trim();
					voReportPara.DataType = int.Parse(odrPCS[sys_ReportParaTable.DATATYPE_FLD].ToString().Trim());
					voReportPara.Width = int.Parse(odrPCS[sys_ReportParaTable.WIDTH_FLD].ToString().Trim());
					voReportPara.Optional = Boolean.Parse(odrPCS[sys_ReportParaTable.OPTIONAL_FLD].ToString().Trim());
					voReportPara.TagValue = Boolean.Parse(odrPCS[sys_ReportParaTable.TAGVALUE_FLD].ToString().Trim());
					voReportPara.SameRow = Boolean.Parse(odrPCS[sys_ReportParaTable.SAMEROW_FLD].ToString().Trim());
					voReportPara.DefaultValue = odrPCS[sys_ReportParaTable.DEFAULTVALUE_FLD].ToString().Trim();
					voReportPara.Items = odrPCS[sys_ReportParaTable.ITEMS_FLD].ToString().Trim();
					voReportPara.FromTable = odrPCS[sys_ReportParaTable.FROMTABLE_FLD].ToString().Trim();
					voReportPara.FromField = odrPCS[sys_ReportParaTable.FROMFIELD_FLD].ToString().Trim();
					voReportPara.FilterField1 = odrPCS[sys_ReportParaTable.FILTERFIELD1_FLD].ToString().Trim();
					voReportPara.FilterField1Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].ToString().Trim());
					voReportPara.FilterField2 = odrPCS[sys_ReportParaTable.FILTERFIELD2_FLD].ToString().Trim();
					voReportPara.FilterField2Width = int.Parse(odrPCS[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].ToString().Trim());
					voReportPara.SQLCLause = odrPCS[sys_ReportParaTable.SQLCLAUSE_FLD].ToString().Trim();
					voReportPara.WhereClause = odrPCS[sys_ReportParaTable.WHERECLAUSE_FLD].ToString().Trim();
					if (odrPCS[sys_ReportParaTable.MULTISELECTION_FLD] != DBNull.Value)
						voReportPara.MultiSelection = Convert.ToBoolean(odrPCS[sys_ReportParaTable.MULTISELECTION_FLD]);

					arrObjectVOs.Add(voReportPara);
				}
				arrObjectVOs.TrimToSize();
				return arrObjectVOs;
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
		///       This method uses to update data to sys_ReportPara
		///    </Description>
		///    <Inputs>
		///       sys_ReportParaVO       
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
		///       12/Oct/2005 Thachnn: fix bug injection
		///       24/Oct/2005 Thachnn: fix bug SQL string generate
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportParaVO objObject = (sys_ReportParaVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + sys_ReportParaTable.TABLE_NAME + " SET "
					+ sys_ReportParaTable.PARAORDER_FLD + "=   ?" + ","
					+ sys_ReportParaTable.PARANAME_FLD + "=   ?" + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + "=   ?" + ","
					+ sys_ReportParaTable.DATATYPE_FLD + "=   ?" + ","
					+ sys_ReportParaTable.WIDTH_FLD + "=   ?" + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + "=   ?" + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + "=   ?" + ","
					+ sys_ReportParaTable.SAMEROW_FLD + "=   ?" + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + "=   ?" + ","
					+ sys_ReportParaTable.ITEMS_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + "=   ?" + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + "=   ?" + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + "=   ?" + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + "=   ?" + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD + "=  ?"
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ?"
					+ " AND " + sys_ReportParaTable.PARANAME_FLD + "= ?"
					;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARAORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.PARAORDER_FLD].Value = objObject.ParaOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARANAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARANAME_FLD].Value = objObject.ParaName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARACAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARACAPTION_FLD].Value = objObject.ParaCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DATATYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.DATATYPE_FLD].Value = objObject.DataType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.OPTIONAL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.OPTIONAL_FLD].Value = objObject.Optional;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.TAGVALUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.TAGVALUE_FLD].Value = objObject.TagValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SAMEROW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.SAMEROW_FLD].Value = objObject.SameRow;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DEFAULTVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.DEFAULTVALUE_FLD].Value = objObject.DefaultValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.ITEMS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.ITEMS_FLD].Value = objObject.Items;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMTABLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMTABLE_FLD].Value = objObject.FromTable;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMFIELD_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMFIELD_FLD].Value = objObject.FromField;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1_FLD].Value = objObject.FilterField1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].Value = objObject.FilterField1Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2_FLD].Value = objObject.FilterField2;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].Value = objObject.FilterField2Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SQLCLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.SQLCLAUSE_FLD].Value = objObject.SQLCLause;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.MULTISELECTION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.MULTISELECTION_FLD].Value = objObject.MultiSelection;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WHERECLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.WHERECLAUSE_FLD].Value = objObject.WhereClause;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARANAME_FLD + "_1", OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARANAME_FLD + "_1"].Value = objObject.ParaName;


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
		///       This method uses to update data to sys_ReportPara
		///    </Description>
		///    <Inputs>
		///       sys_ReportParaVO, Old Para Name   
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       09-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO, string pstrParaName)
		{
			const string METHOD_NAME = THIS + ".Update()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				sys_ReportParaVO objObject = (sys_ReportParaVO) pobjObjecVO;
				string strSql = String.Empty;
				strSql = "UPDATE " + sys_ReportParaTable.TABLE_NAME + " SET "
					+ sys_ReportParaTable.PARANAME_FLD + "= ?" + ","
					+ sys_ReportParaTable.PARAORDER_FLD + "= ?" + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + "= ?" + ","
					+ sys_ReportParaTable.DATATYPE_FLD + "= ?" + ","
					+ sys_ReportParaTable.WIDTH_FLD + "= ?" + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + "= ?" + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + "= ?" + ","
					+ sys_ReportParaTable.SAMEROW_FLD + "= ?" + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + "= ?" + ","
					+ sys_ReportParaTable.ITEMS_FLD + "= ?" + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + "= ?" + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + "= ?" + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + "= ?" + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + "= ?" + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + "= ?" + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + "= ?" + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + "= ?" + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + "= ?" + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD + "= ?"
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + "= ? "
					+ " AND " + sys_ReportParaTable.PARANAME_FLD + "= ? ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARANAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARANAME_FLD].Value = objObject.ParaName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARAORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.PARAORDER_FLD].Value = objObject.ParaOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.PARACAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.PARACAPTION_FLD].Value = objObject.ParaCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DATATYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.DATATYPE_FLD].Value = objObject.DataType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.OPTIONAL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.OPTIONAL_FLD].Value = objObject.Optional;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.TAGVALUE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.TAGVALUE_FLD].Value = objObject.TagValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SAMEROW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.SAMEROW_FLD].Value = objObject.SameRow;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.DEFAULTVALUE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.DEFAULTVALUE_FLD].Value = objObject.DefaultValue;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.ITEMS_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.ITEMS_FLD].Value = objObject.Items;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMTABLE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMTABLE_FLD].Value = objObject.FromTable;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FROMFIELD_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FROMFIELD_FLD].Value = objObject.FromField;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1_FLD].Value = objObject.FilterField1;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD1WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD1WIDTH_FLD].Value = objObject.FilterField1Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2_FLD].Value = objObject.FilterField2;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.FILTERFIELD2WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportParaTable.FILTERFIELD2WIDTH_FLD].Value = objObject.FilterField2Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.SQLCLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.SQLCLAUSE_FLD].Value = objObject.SQLCLause;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.MULTISELECTION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportParaTable.MULTISELECTION_FLD].Value = objObject.MultiSelection;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.WHERECLAUSE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.WHERECLAUSE_FLD].Value = objObject.WhereClause;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportParaTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportParaTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTNAME_FLD].Value = pstrParaName;

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
		///       This method uses to get all data from sys_ReportPara
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


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportParaTable.TABLE_NAME);

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
		///       Monday, December 27, 2004
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
					+ sys_ReportParaTable.REPORTID_FLD + ","
					+ sys_ReportParaTable.PARAORDER_FLD + ","
					+ sys_ReportParaTable.PARANAME_FLD + ","
					+ sys_ReportParaTable.PARACAPTION_FLD + ","
					+ sys_ReportParaTable.DATATYPE_FLD + ","
					+ sys_ReportParaTable.WIDTH_FLD + ","
					+ sys_ReportParaTable.OPTIONAL_FLD + ","
					+ sys_ReportParaTable.TAGVALUE_FLD + ","
					+ sys_ReportParaTable.SAMEROW_FLD + ","
					+ sys_ReportParaTable.DEFAULTVALUE_FLD + ","
					+ sys_ReportParaTable.ITEMS_FLD + ","
					+ sys_ReportParaTable.FROMTABLE_FLD + ","
					+ sys_ReportParaTable.FROMFIELD_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD1WIDTH_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2_FLD + ","
					+ sys_ReportParaTable.FILTERFIELD2WIDTH_FLD + ","
					+ sys_ReportParaTable.SQLCLAUSE_FLD + ","
					+ sys_ReportParaTable.MULTISELECTION_FLD + ","
					+ sys_ReportParaTable.WHERECLAUSE_FLD
					+ " FROM " + sys_ReportParaTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportParaTable.TABLE_NAME);

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
		///       This method uses to get next order for moving up/down parameter
		///    </Description>
		///    <Inputs>
		///       ReportID, ParaName, MoveType (Up or Down)
		///    </Inputs>
		///    <Outputs>
		///       ParaOrder
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetNextOrder(string pstrReportID, int pnCurrentOrder, MoveDirection penumDirection)
		{
			const string METHOD_NAME = THIS + ".GetNextOrder()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " + sys_ReportParaTable.PARAORDER_FLD
					+ " FROM  " + sys_ReportParaTable.TABLE_NAME 
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + " =  ? " ; // + pstrReportID + "'";
				// if move up, then select max
				if (penumDirection == MoveDirection.Up)
				{
					strSql += " AND " + sys_ReportParaTable.PARAORDER_FLD + " < " + pnCurrentOrder.ToString().Trim()
						+ " ORDER BY " + sys_ReportParaTable.PARAORDER_FLD + " DESC";
				}
				else // select min
				{
					strSql += " AND " + sys_ReportParaTable.PARAORDER_FLD + " > " + pnCurrentOrder.ToString().Trim()
						+ " ORDER BY " + sys_ReportParaTable.PARAORDER_FLD + " ASC";
				}
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrReportID ;
				ocmdPCS.Connection.Open();

				// return the max order
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get max order of parameter
		///    </Description>
		///    <Inputs>
		///       ReportID, ParaName
		///    </Inputs>
		///    <Outputs>
		///       Max ParaOrder
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       05-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxOrder(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetMaxOrder()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ISNULL(MAX(" 
					+ sys_ReportParaTable.PARAORDER_FLD + "),0)" 
					+ " FROM  " + sys_ReportParaTable.TABLE_NAME 
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + " = ?" ; //'" + pstrReportID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrReportID ;
				ocmdPCS.Connection.Open();

				// return the max order
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get DataType of parameter
		///    </Description>
		///    <Inputs>
		///       ReportID, ParaName
		///    </Inputs>
		///    <Outputs>
		///       DataType
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       06-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetDataType(string pstrReportID, string pstrParaName)
		{
			const string METHOD_NAME = THIS + ".GetDataType()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " 
					+ sys_ReportParaTable.DATATYPE_FLD
					+ " FROM  " + sys_ReportParaTable.TABLE_NAME 
					+ " WHERE " + sys_ReportParaTable.REPORTID_FLD + " = ?" // '" + pstrReportID + "'"
					+ " AND " + sys_ReportParaTable.PARANAME_FLD + " = ?" ; //'" + pstrParaName + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.REPORTID_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.REPORTID_FLD ].Value = pstrReportID ;
				ocmdPCS.Parameters.Add(new OleDbParameter( sys_ReportParaTable.PARANAME_FLD , OleDbType.VarWChar));
				ocmdPCS.Parameters[ sys_ReportParaTable.PARANAME_FLD ].Value = pstrParaName ;
				ocmdPCS.Connection.Open();

				// return the data type
				return ocmdPCS.ExecuteScalar().ToString().Trim();
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get data from specified table
		///    </Description>
		///    <Inputs>
		///       Field, TableName, FilterField[]
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       07-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet GetDataFromTable(string pstrField, string pstrTableName, string[] pstrFilterFields)
		{
			/// TODO: Bro DungLA please review for injection here
			const string METHOD_NAME = THIS + ".GetDataFromTable()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			if ((pstrTableName != null) && (pstrTableName != string.Empty))
			{
				try 
				{
					// if field to selected is empty then select all
					string strSql = "SELECT ";
					strSql += (pstrField == string.Empty) ? "*" : pstrField;
					if (pstrFilterFields.Length > 0)
					{
						for (int i = 0; i < pstrFilterFields.Length; i++)
						{
							strSql += ", " + pstrFilterFields[i];
						}
					}
					strSql += " FROM " + pstrTableName;
					
					Utils utils = new Utils();
					oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
					ocmdPCS = new OleDbCommand(strSql, oconPCS);

					OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
					odadPCS.Fill(dstPCS, pstrTableName);

					return dstPCS;
				}
				catch(OleDbException ex)
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
				catch(FormatException ex)
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
			else
			{
				return dstPCS;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to execute SqlClause from Parameter and return result as DataSet
		///    </Description>
		///    <Inputs>
		///       SqlClause, WhereClause
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       07-Jan-2005
		///       Modified: 09-Jan-2005
		///    </History>
		///    <Notes>
		///    Change return type from string to DataSet
		///    </Notes>
		//**************************************************************************
		public DataSet ExecuteSqlClause(string pstrSqlClause, string pstrWhereClause)
		{
			// TODO: Bro DungLA, please review for injection
			const string METHOD_NAME = THIS + ".ExecuteSqlClause()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			DataSet dstResult = new DataSet();
			try 
			{
				string strSql = pstrSqlClause + " WHERE " + pstrWhereClause;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadResult = new OleDbDataAdapter(ocmdPCS);
				odadResult.Fill(dstResult);

				return dstResult;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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