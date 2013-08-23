using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportFieldsDS
	{
		public sys_ReportFieldsDS()
		{
		}

		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportFieldsDS";

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_ReportFields
		///    </Description>
		///    <Inputs>
		///        sys_ReportFieldsVO       
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
				sys_ReportFieldsVO objObject = (sys_ReportFieldsVO) pobjObjectVO;
				string strSql = String.Empty;

				strSql = "INSERT INTO " + sys_ReportFieldsTable.TABLE_NAME + "("
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD + ")"
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDORDER_FLD].Value = objObject.FieldOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDNAME_FLD].Value = objObject.FieldName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTION_FLD].Value = objObject.FieldCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].Value = objObject.FieldCaptionEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].Value = objObject.FieldCaptionVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].Value = objObject.FieldCaptionJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FONT_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FONT_FLD].Value = objObject.Font;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.VISISBLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.VISISBLE_FLD].Value = objObject.Visisble;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.PRINTPREVIEW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.PRINTPREVIEW_FLD].Value = objObject.PrintPreview;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FORMAT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FORMAT_FLD].Value = objObject.Format;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SORT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SORT_FLD].Value = objObject.Sort;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.GROUPBY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.BOTTOMGROUP_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.BOTTOMGROUP_FLD].Value = objObject.BottomGroup;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPPAGE_FLD].Value = objObject.SumTopPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].Value = objObject.SumBottomPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPREPORT_FLD].Value = objObject.SumTopReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].Value = objObject.SumBottomReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.ALIGN_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.ALIGN_FLD].Value = objObject.Align;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.DATATYPE_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.DATATYPE_FLD].Value = objObject.DataType;


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
		///       This method uses to delete data from sys_ReportFields
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
			/// UNDONE: Thachnn says: I think this function is useless
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportFieldsTable.TABLE_NAME + " WHERE  " + sys_ReportFieldsTable.REPORTID_FLD + "=" + pintID.ToString().Trim();
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
		///       This method uses to delete data from sys_ReportFields
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(string pstrID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + sys_ReportFieldsTable.TABLE_NAME + " WHERE  " 
				+ sys_ReportFieldsTable.REPORTID_FLD + "= ? "; // + pstrID + "'";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrID;
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
		///       This method uses to get data from sys_ReportFields
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportFieldsVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportFieldsVO
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
			/// UNDONE: Thachnn says: I think this function is useless/ Report ID is be STRING
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportFieldsVO objObject = new sys_ReportFieldsVO();

				while (odrPCS.Read())
				{
					objObject.ReportID = odrPCS[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
					objObject.FieldOrder = int.Parse(odrPCS[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
					objObject.FieldName = odrPCS[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
					objObject.FieldCaption = odrPCS[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
					objObject.FieldCaptionEN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
					objObject.FieldCaptionVN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
					objObject.FieldCaptionJP = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
					objObject.Font = odrPCS[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
					objObject.Visisble = Boolean.Parse(odrPCS[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
					objObject.Type = int.Parse(odrPCS[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
					objObject.PrintPreview = Boolean.Parse(odrPCS[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
					objObject.Width = int.Parse(odrPCS[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
					objObject.Format = odrPCS[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
					objObject.Sort = int.Parse(odrPCS[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
					objObject.GroupBy = Boolean.Parse(odrPCS[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
					objObject.BottomGroup = Boolean.Parse(odrPCS[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
					objObject.SumTopPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
					objObject.SumBottomPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
					objObject.SumTopReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
					objObject.SumBottomReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
					objObject.Align = int.Parse(odrPCS[sys_ReportFieldsTable.ALIGN_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.DATATYPE_FLD] != DBNull.Value)
						objObject.DataType = int.Parse(odrPCS[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());
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
		///       This method uses to get data from sys_ReportFields
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///       List of sys_ReportFieldsVO object
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
		public ArrayList GetObjectVOs(string pstrReportID)
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
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? " // + pstrReportID + "'"
					+ " ORDER BY " + sys_ReportFieldsTable.FIELDORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();

					voReportFields.ReportID = odrPCS[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
					voReportFields.FieldOrder = int.Parse(odrPCS[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
					voReportFields.FieldName = odrPCS[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
					voReportFields.FieldCaption = odrPCS[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
					voReportFields.FieldCaptionEN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
					voReportFields.FieldCaptionVN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
					voReportFields.FieldCaptionJP = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
					voReportFields.Font = odrPCS[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();

					if (odrPCS[sys_ReportFieldsTable.ALIGN_FLD] != DBNull.Value)
					voReportFields.Align = int.Parse(odrPCS[sys_ReportFieldsTable.ALIGN_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.VISISBLE_FLD] != DBNull.Value)
						voReportFields.Visisble = Boolean.Parse(odrPCS[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.TYPE_FLD] != DBNull.Value)
						voReportFields.Type = int.Parse(odrPCS[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.PRINTPREVIEW_FLD] != DBNull.Value)
						voReportFields.PrintPreview = Boolean.Parse(odrPCS[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.WIDTH_FLD] != DBNull.Value)
						voReportFields.Width = int.Parse(odrPCS[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
					voReportFields.Format = odrPCS[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
					if (odrPCS[sys_ReportFieldsTable.SORT_FLD] != DBNull.Value)
						voReportFields.Sort = int.Parse(odrPCS[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.GROUPBY_FLD] != DBNull.Value)
						voReportFields.GroupBy = Boolean.Parse(odrPCS[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.BOTTOMGROUP_FLD] != DBNull.Value)
						voReportFields.BottomGroup = Boolean.Parse(odrPCS[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.SUMTOPPAGE_FLD] != DBNull.Value)
						voReportFields.SumTopPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD] != DBNull.Value)
						voReportFields.SumBottomPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.SUMTOPREPORT_FLD] != DBNull.Value)
						voReportFields.SumTopReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD] != DBNull.Value)
						voReportFields.SumBottomReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
					if (odrPCS[sys_ReportFieldsTable.DATATYPE_FLD] != DBNull.Value)
						voReportFields.DataType = int.Parse(odrPCS[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());

					arrObjectVOs.Add(voReportFields);
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
		///       This method uses to update data to sys_ReportFields
		///    </Description>
		///    <Inputs>
		///       sys_ReportFieldsVO       
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

			sys_ReportFieldsVO objObject = (sys_ReportFieldsVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + sys_ReportFieldsTable.TABLE_NAME + " SET "
					+ sys_ReportFieldsTable.FIELDORDER_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FONT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.TYPE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SORT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + "=  ?,"
					+ sys_ReportFieldsTable.ALIGN_FLD + "=  ?,"
					+ sys_ReportFieldsTable.DATATYPE_FLD + "=  ?"
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? ";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDORDER_FLD].Value = objObject.FieldOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDNAME_FLD].Value = objObject.FieldName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTION_FLD].Value = objObject.FieldCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].Value = objObject.FieldCaptionEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].Value = objObject.FieldCaptionVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].Value = objObject.FieldCaptionJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FONT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FONT_FLD].Value = objObject.Font;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.VISISBLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.VISISBLE_FLD].Value = objObject.Visisble;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.PRINTPREVIEW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.PRINTPREVIEW_FLD].Value = objObject.PrintPreview;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FORMAT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FORMAT_FLD].Value = objObject.Format;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SORT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SORT_FLD].Value = objObject.Sort;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.GROUPBY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.BOTTOMGROUP_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.BOTTOMGROUP_FLD].Value = objObject.BottomGroup;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPPAGE_FLD].Value = objObject.SumTopPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].Value = objObject.SumBottomPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPREPORT_FLD].Value = objObject.SumTopReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].Value = objObject.SumBottomReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.ALIGN_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.ALIGN_FLD].Value = objObject.Align;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.DATATYPE_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.DATATYPE_FLD].Value = objObject.DataType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = objObject.ReportID;


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
		///       This method uses to update data to sys_ReportFields
		///    </Description>
		///    <Inputs>
		///       sys_ReportFieldsVO       
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
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateByName(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportFieldsVO objObject = (sys_ReportFieldsVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE " + sys_ReportFieldsTable.TABLE_NAME + " SET "
					+ sys_ReportFieldsTable.FIELDORDER_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FONT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.TYPE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SORT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + "=   ?" + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + "=  ?" + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + "=  ?" + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD + "=  ?"
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? "
					+ " AND " + sys_ReportFieldsTable.FIELDNAME_FLD + "= ? ";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDORDER_FLD].Value = objObject.FieldOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTION_FLD].Value = objObject.FieldCaption;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONEN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].Value = objObject.FieldCaptionEN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].Value = objObject.FieldCaptionVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDCAPTIONJP_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].Value = objObject.FieldCaptionJP;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FONT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FONT_FLD].Value = objObject.Font;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.VISISBLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.VISISBLE_FLD].Value = objObject.Visisble;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.TYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.TYPE_FLD].Value = objObject.Type;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.PRINTPREVIEW_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.PRINTPREVIEW_FLD].Value = objObject.PrintPreview;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.WIDTH_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FORMAT_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FORMAT_FLD].Value = objObject.Format;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SORT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SORT_FLD].Value = objObject.Sort;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.GROUPBY_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.GROUPBY_FLD].Value = objObject.GroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.BOTTOMGROUP_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.BOTTOMGROUP_FLD].Value = objObject.BottomGroup;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPPAGE_FLD].Value = objObject.SumTopPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].Value = objObject.SumBottomPage;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMTOPREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMTOPREPORT_FLD].Value = objObject.SumTopReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].Value = objObject.SumBottomReport;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.ALIGN_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.ALIGN_FLD].Value = objObject.Align;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.DATATYPE_FLD, OleDbType.TinyInt));
				ocmdPCS.Parameters[sys_ReportFieldsTable.DATATYPE_FLD].Value = objObject.DataType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.FIELDNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.FIELDNAME_FLD].Value = objObject.FieldName;

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
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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
		///       This method uses to get all data from sys_ReportFields
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
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportFieldsTable.TABLE_NAME);

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
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, sys_ReportFieldsTable.TABLE_NAME);

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
		///       This method uses to get max field order of report
		///    </Description>
		///    <Inputs>
		///       ReportID
		///    </Inputs>
		///    <Outputs>
		///      Max order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///     17-Jan-2005
		///     12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxFieldOrder(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetMaxFieldOrder()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
                /// HACKED: Thachnn: fix bug FieldOrder
				string strSql = "SELECT ISNULL (Max("
					+ sys_ReportFieldsTable.FIELDORDER_FLD
					+ "),0) FROM "
					+ sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + " = ? "; //+ pstrReportID +"'" ;
				/// ENDHACKED: Thachnn: fix bug FieldOrder

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		///       This method uses to get all field marked as Group By
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///      List of field name
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       18-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetGroupByFields(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetGroupByFields()";

			ArrayList arrObjectVOs = new ArrayList();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? " // + pstrReportID + "'"
					+ " AND " + sys_ReportFieldsTable.GROUPBY_FLD + "=" + Convert.ToInt32(true)	/// UNDONE: Thachnn says: I don't understand here!
					+ " ORDER BY " + sys_ReportFieldsTable.FIELDORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();

					voReportFields.ReportID = odrPCS[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
					voReportFields.FieldOrder = int.Parse(odrPCS[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
					voReportFields.FieldName = odrPCS[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
					voReportFields.FieldCaption = odrPCS[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
					voReportFields.FieldCaptionEN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
					voReportFields.FieldCaptionVN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
					voReportFields.FieldCaptionJP = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
					voReportFields.Font = odrPCS[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
					voReportFields.Visisble = Boolean.Parse(odrPCS[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
					voReportFields.Type = int.Parse(odrPCS[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
					voReportFields.PrintPreview = Boolean.Parse(odrPCS[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
					voReportFields.Width = int.Parse(odrPCS[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
					voReportFields.Format = odrPCS[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
					voReportFields.Sort = int.Parse(odrPCS[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
					voReportFields.GroupBy = Boolean.Parse(odrPCS[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
					voReportFields.BottomGroup = Boolean.Parse(odrPCS[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
					voReportFields.SumTopPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
					voReportFields.SumBottomPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
					voReportFields.SumTopReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
					voReportFields.SumBottomReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
					voReportFields.Align = int.Parse(odrPCS[sys_ReportFieldsTable.ALIGN_FLD].ToString().Trim());
					voReportFields.DataType = int.Parse(odrPCS[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());

					arrObjectVOs.Add(voReportFields);
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
		///       This method uses to get all field marked as Sort
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///      List of field name
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA
		///       
		///    </Authors>
		///    <History>
		///       18-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public ArrayList GetSortFields(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".GetSortFields()";

			ArrayList arrObjectVOs = new ArrayList();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ sys_ReportFieldsTable.REPORTID_FLD + ","
					+ sys_ReportFieldsTable.FIELDORDER_FLD + ","
					+ sys_ReportFieldsTable.FIELDNAME_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTION_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONEN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONVN_FLD + ","
					+ sys_ReportFieldsTable.FIELDCAPTIONJP_FLD + ","
					+ sys_ReportFieldsTable.FONT_FLD + ","
					+ sys_ReportFieldsTable.VISISBLE_FLD + ","
					+ sys_ReportFieldsTable.TYPE_FLD + ","
					+ sys_ReportFieldsTable.PRINTPREVIEW_FLD + ","
					+ sys_ReportFieldsTable.WIDTH_FLD + ","
					+ sys_ReportFieldsTable.FORMAT_FLD + ","
					+ sys_ReportFieldsTable.SORT_FLD + ","
					+ sys_ReportFieldsTable.GROUPBY_FLD + ","
					+ sys_ReportFieldsTable.BOTTOMGROUP_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD + ","
					+ sys_ReportFieldsTable.SUMTOPREPORT_FLD + ","
					+ sys_ReportFieldsTable.DATATYPE_FLD + ","
					+ sys_ReportFieldsTable.ALIGN_FLD + ","
					+ sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? " //+ pstrReportID + "'"
					+ " AND " + sys_ReportFieldsTable.SORT_FLD + "> 0"
					+ " ORDER BY " + sys_ReportFieldsTable.FIELDORDER_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{
					sys_ReportFieldsVO voReportFields = new sys_ReportFieldsVO();

					voReportFields.ReportID = odrPCS[sys_ReportFieldsTable.REPORTID_FLD].ToString().Trim();
					voReportFields.FieldOrder = int.Parse(odrPCS[sys_ReportFieldsTable.FIELDORDER_FLD].ToString().Trim());
					voReportFields.FieldName = odrPCS[sys_ReportFieldsTable.FIELDNAME_FLD].ToString().Trim();
					voReportFields.FieldCaption = odrPCS[sys_ReportFieldsTable.FIELDCAPTION_FLD].ToString().Trim();
					voReportFields.FieldCaptionEN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONEN_FLD].ToString().Trim();
					voReportFields.FieldCaptionVN = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONVN_FLD].ToString().Trim();
					voReportFields.FieldCaptionJP = odrPCS[sys_ReportFieldsTable.FIELDCAPTIONJP_FLD].ToString().Trim();
					voReportFields.Font = odrPCS[sys_ReportFieldsTable.FONT_FLD].ToString().Trim();
					voReportFields.Visisble = Boolean.Parse(odrPCS[sys_ReportFieldsTable.VISISBLE_FLD].ToString().Trim());
					voReportFields.Type = int.Parse(odrPCS[sys_ReportFieldsTable.TYPE_FLD].ToString().Trim());
					voReportFields.PrintPreview = Boolean.Parse(odrPCS[sys_ReportFieldsTable.PRINTPREVIEW_FLD].ToString().Trim());
					voReportFields.Width = int.Parse(odrPCS[sys_ReportFieldsTable.WIDTH_FLD].ToString().Trim());
					voReportFields.Format = odrPCS[sys_ReportFieldsTable.FORMAT_FLD].ToString().Trim();
					voReportFields.Sort = int.Parse(odrPCS[sys_ReportFieldsTable.SORT_FLD].ToString().Trim());
					voReportFields.GroupBy = Boolean.Parse(odrPCS[sys_ReportFieldsTable.GROUPBY_FLD].ToString().Trim());
					voReportFields.BottomGroup = Boolean.Parse(odrPCS[sys_ReportFieldsTable.BOTTOMGROUP_FLD].ToString().Trim());
					voReportFields.SumTopPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPPAGE_FLD].ToString().Trim());
					voReportFields.SumBottomPage = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMPAGE_FLD].ToString().Trim());
					voReportFields.SumTopReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMTOPREPORT_FLD].ToString().Trim());
					voReportFields.SumBottomReport = Boolean.Parse(odrPCS[sys_ReportFieldsTable.SUMBOTTOMREPORT_FLD].ToString().Trim());
					voReportFields.DataType = int.Parse(odrPCS[sys_ReportFieldsTable.DATATYPE_FLD].ToString().Trim());
					voReportFields.Align = int.Parse(odrPCS[sys_ReportFieldsTable.ALIGN_FLD].ToString().Trim());

					arrObjectVOs.Add(voReportFields);
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
		///       This method uses to get sum of width of all fields of a report
		///       in order to setting report width
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///      sum of width
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       18-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int SumFieldsWidth(string pstrReportID)
		{
			const string METHOD_NAME = THIS + ".SumFieldsWidth()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT SUM(ISNULL("
					+ sys_ReportFieldsTable.WIDTH_FLD + ",0)) + 10"
					+ " FROM " + sys_ReportFieldsTable.TABLE_NAME
					+ " WHERE " + sys_ReportFieldsTable.REPORTID_FLD + "= ? " // + pstrReportID + "'"
					+ " AND " + sys_ReportFieldsTable.WIDTH_FLD + " > 1";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportFieldsTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportFieldsTable.REPORTID_FLD].Value = pstrReportID;
				ocmdPCS.Connection.Open();
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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