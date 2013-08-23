using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class sys_ReportDS 
	{
		public sys_ReportDS()
		{
		}
		private const string THIS = "PCSComUtils.Framework.ReportFrame.DS.DS.sys_ReportDS";
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_Report
		///    </Description>
		///    <Inputs>
		///        sys_ReportVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///       Thachnn
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       22 Sep 2005
		///       12/Oct/2005 Thachnn: fix bug injection
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
				sys_ReportVO voObject = (sys_ReportVO) pobjObjectVO;
				string strSql = String.Empty;
				
				strSql=	"INSERT INTO " + sys_ReportTable.TABLE_NAME + "("
					+ sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","
					/// HACKED: Thachnn: change the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD + ") "
					+ " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?    ,?,?)";
					/// HACKED: Thachnn

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = voObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTNAME_FLD].Value = voObject.ReportName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.DESCRIPTION_FLD].Value = voObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.ISOCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.ISOCODE_FLD].Value = voObject.ISOCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTFILE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTFILE_FLD].Value = voObject.ReportFile;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTTYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTTYPE_FLD].Value = voObject.ReportType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.COMMAND_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.COMMAND_FLD].Value = voObject.Command;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINTOP_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINTOP_FLD].Value = voObject.MarginTop;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINLEFT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINLEFT_FLD].Value = voObject.MarginLeft;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINRIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINRIGHT_FLD].Value = voObject.MarginRight;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINBOTTOM_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINBOTTOM_FLD].Value = voObject.MarginBottom;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINGUTTER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINGUTTER_FLD].Value = voObject.MarginGutter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINGUTTERPOS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.MARGINGUTTERPOS_FLD].Value = voObject.MarginGutterPos;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.ORIENTATION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.ORIENTATION_FLD].Value = voObject.Orientation;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.PAPERSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.PAPERSIZE_FLD].Value = voObject.PaperSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.TABLEBORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.TABLEBORDER_FLD].Value = voObject.TableBorder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.SIGNATURES_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.SIGNATURES_FLD].Value = voObject.Signatures;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTREPORTHEADER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTREPORTHEADER_FLD].Value = voObject.FontReportHeader;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPARAMETER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPARAMETER_FLD].Value = voObject.FontParameter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPAGEHEADER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPAGEHEADER_FLD].Value = voObject.FontPageHeader;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTGROUPBY_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTGROUPBY_FLD].Value = voObject.FontGroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTDETAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTDETAIL_FLD].Value = voObject.FontDetail;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPAGEFOOTER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPAGEFOOTER_FLD].Value = voObject.FontPageFooter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTREPORTFOOTER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTREPORTFOOTER_FLD].Value = voObject.FontReportFooter;

				/// HACKED: Thachnn: modify the sys_report table
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.USETEMPLATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.USETEMPLATE_FLD].Value = voObject.UseTemplate;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.TEMPLATEFILE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.TEMPLATEFILE_FLD].Value = voObject.TemplateFile;
				/// ENDHACKED: Thachnn

				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else
					{
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
					}
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
		///       This method uses to delete data from sys_Report
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
			strSql=	"DELETE " + sys_ReportTable.TABLE_NAME + " WHERE  " 
				+ sys_ReportTable.REPORTID_FLD + "= ? "; // + pstrID + "'";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = pstrID;
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
		///       This method uses to get data from sys_Report
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(string pstrID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","

					/// HACKED: Thachnn: modify the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD 
					/// ENDHACKED:

					+ " FROM " + sys_ReportTable.TABLE_NAME
					+" WHERE " + sys_ReportTable.REPORTID_FLD + " = ? ";// + pstrID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = pstrID;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				sys_ReportVO voObject = new sys_ReportVO();

				while (odrPCS.Read())
				{ 
					voObject.ReportID = odrPCS[sys_ReportTable.REPORTID_FLD].ToString().Trim();
					voObject.ReportName = odrPCS[sys_ReportTable.REPORTNAME_FLD].ToString().Trim();
					voObject.Description = odrPCS[sys_ReportTable.DESCRIPTION_FLD].ToString().Trim();
					voObject.ISOCode = odrPCS[sys_ReportTable.ISOCODE_FLD].ToString().Trim();
					voObject.ReportFile = odrPCS[sys_ReportTable.REPORTFILE_FLD].ToString().Trim();
					voObject.ReportType = odrPCS[sys_ReportTable.REPORTTYPE_FLD].ToString().Trim();
					voObject.Command = odrPCS[sys_ReportTable.COMMAND_FLD].ToString().Trim();
					voObject.MarginTop = int.Parse(odrPCS[sys_ReportTable.MARGINTOP_FLD].ToString().Trim());
					voObject.MarginLeft = int.Parse(odrPCS[sys_ReportTable.MARGINLEFT_FLD].ToString().Trim());
					voObject.MarginRight = int.Parse(odrPCS[sys_ReportTable.MARGINRIGHT_FLD].ToString().Trim());
					voObject.MarginBottom = int.Parse(odrPCS[sys_ReportTable.MARGINBOTTOM_FLD].ToString().Trim());
					voObject.MarginGutter = int.Parse(odrPCS[sys_ReportTable.MARGINGUTTER_FLD].ToString().Trim());
					voObject.MarginGutterPos = Boolean.Parse(odrPCS[sys_ReportTable.MARGINGUTTERPOS_FLD].ToString().Trim());
					voObject.Orientation = Boolean.Parse(odrPCS[sys_ReportTable.ORIENTATION_FLD].ToString().Trim());
					voObject.PaperSize = int.Parse(odrPCS[sys_ReportTable.PAPERSIZE_FLD].ToString().Trim());
					voObject.TableBorder = int.Parse(odrPCS[sys_ReportTable.TABLEBORDER_FLD].ToString().Trim());
					voObject.Signatures = odrPCS[sys_ReportTable.SIGNATURES_FLD].ToString().Trim();
					voObject.FontReportHeader = odrPCS[sys_ReportTable.FONTREPORTHEADER_FLD].ToString().Trim();
					voObject.FontParameter = odrPCS[sys_ReportTable.FONTPARAMETER_FLD].ToString().Trim();
					voObject.FontPageHeader = odrPCS[sys_ReportTable.FONTPAGEHEADER_FLD].ToString().Trim();
					voObject.FontGroupBy = odrPCS[sys_ReportTable.FONTGROUPBY_FLD].ToString().Trim();
					voObject.FontDetail = odrPCS[sys_ReportTable.FONTDETAIL_FLD].ToString().Trim();
					voObject.FontPageFooter = odrPCS[sys_ReportTable.FONTPAGEFOOTER_FLD].ToString().Trim();
					voObject.FontReportFooter = odrPCS[sys_ReportTable.FONTREPORTFOOTER_FLD].ToString().Trim();

					/// HACKED: Thachnn: modify the sys_report table
					if (odrPCS[sys_ReportTable.USETEMPLATE_FLD] != DBNull.Value)
						voObject.UseTemplate = Boolean.Parse(odrPCS[sys_ReportTable.USETEMPLATE_FLD].ToString().Trim());
					else
						voObject.UseTemplate = false;
					voObject.TemplateFile = odrPCS[sys_ReportTable.TEMPLATEFILE_FLD].ToString().Trim();
					/// ENDHACKED
				}		
				return voObject;					
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

//		public object GetReportByReportID(string pstrReportID)
//		{
//			const string METHOD_NAME = THIS + ".GetObjectVO()";
//			
//			OleDbDataReader odrPCS = null;
//			OleDbConnection oconPCS = null;
//			OleDbCommand ocmdPCS = null;
//			try 
//			{
//				string strSql = String.Empty;
//				strSql=	"SELECT "
//					+ sys_ReportTable.REPORTID_FLD + ","
//					+ sys_ReportTable.REPORTNAME_FLD + ","
//					+ sys_ReportTable.DESCRIPTION_FLD + ","
//					+ sys_ReportTable.ISOCODE_FLD + ","
//					+ sys_ReportTable.REPORTFILE_FLD + ","
//					+ sys_ReportTable.REPORTTYPE_FLD + ","
//					+ sys_ReportTable.COMMAND_FLD + ","
//					+ sys_ReportTable.MARGINTOP_FLD + ","
//					+ sys_ReportTable.MARGINLEFT_FLD + ","
//					+ sys_ReportTable.MARGINRIGHT_FLD + ","
//					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
//					+ sys_ReportTable.MARGINGUTTER_FLD + ","
//					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
//					+ sys_ReportTable.ORIENTATION_FLD + ","
//					+ sys_ReportTable.PAPERSIZE_FLD + ","
//					+ sys_ReportTable.TABLEBORDER_FLD + ","
//					+ sys_ReportTable.SIGNATURES_FLD + ","
//					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
//					+ sys_ReportTable.FONTPARAMETER_FLD + ","
//					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
//					+ sys_ReportTable.FONTGROUPBY_FLD + ","
//					+ sys_ReportTable.FONTDETAIL_FLD + ","
//					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
//					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","
//
//					/// HACKED: Thachnn: modify the sys_report table
//					+ sys_ReportTable.USETEMPLATE_FLD + ","
//					+ sys_ReportTable.TEMPLATEFILE_FLD 
//					/// ENDHACKED:
//
//					+ " FROM " + sys_ReportTable.TABLE_NAME
//					+" WHERE " + sys_ReportTable.REPORTID_FLD + " = ? ";// + pstrID + "'";
//
//				Utils utils = new Utils();
//				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
//
//				ocmdPCS = new OleDbCommand(strSql, oconPCS);
//				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
//				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = pstrReportID;
//				ocmdPCS.Connection.Open();
//
//				odrPCS = ocmdPCS.ExecuteReader();
//
//				sys_ReportVO voObject = new sys_ReportVO();
//
//				while (odrPCS.Read())
//				{ 
//					voObject.ReportID = odrPCS[sys_ReportTable.REPORTID_FLD].ToString().Trim();
//					voObject.ReportName = odrPCS[sys_ReportTable.REPORTNAME_FLD].ToString().Trim();
//					voObject.Description = odrPCS[sys_ReportTable.DESCRIPTION_FLD].ToString().Trim();
//					voObject.ISOCode = odrPCS[sys_ReportTable.ISOCODE_FLD].ToString().Trim();
//					voObject.ReportFile = odrPCS[sys_ReportTable.REPORTFILE_FLD].ToString().Trim();
//					voObject.ReportType = odrPCS[sys_ReportTable.REPORTTYPE_FLD].ToString().Trim();
//					voObject.Command = odrPCS[sys_ReportTable.COMMAND_FLD].ToString().Trim();
//					voObject.MarginTop = int.Parse(odrPCS[sys_ReportTable.MARGINTOP_FLD].ToString().Trim());
//					voObject.MarginLeft = int.Parse(odrPCS[sys_ReportTable.MARGINLEFT_FLD].ToString().Trim());
//					voObject.MarginRight = int.Parse(odrPCS[sys_ReportTable.MARGINRIGHT_FLD].ToString().Trim());
//					voObject.MarginBottom = int.Parse(odrPCS[sys_ReportTable.MARGINBOTTOM_FLD].ToString().Trim());
//					voObject.MarginGutter = int.Parse(odrPCS[sys_ReportTable.MARGINGUTTER_FLD].ToString().Trim());
//					voObject.MarginGutterPos = Boolean.Parse(odrPCS[sys_ReportTable.MARGINGUTTERPOS_FLD].ToString().Trim());
//					voObject.Orientation = Boolean.Parse(odrPCS[sys_ReportTable.ORIENTATION_FLD].ToString().Trim());
//					voObject.PaperSize = int.Parse(odrPCS[sys_ReportTable.PAPERSIZE_FLD].ToString().Trim());
//					voObject.TableBorder = int.Parse(odrPCS[sys_ReportTable.TABLEBORDER_FLD].ToString().Trim());
//					voObject.Signatures = odrPCS[sys_ReportTable.SIGNATURES_FLD].ToString().Trim();
//					voObject.FontReportHeader = odrPCS[sys_ReportTable.FONTREPORTHEADER_FLD].ToString().Trim();
//					voObject.FontParameter = odrPCS[sys_ReportTable.FONTPARAMETER_FLD].ToString().Trim();
//					voObject.FontPageHeader = odrPCS[sys_ReportTable.FONTPAGEHEADER_FLD].ToString().Trim();
//					voObject.FontGroupBy = odrPCS[sys_ReportTable.FONTGROUPBY_FLD].ToString().Trim();
//					voObject.FontDetail = odrPCS[sys_ReportTable.FONTDETAIL_FLD].ToString().Trim();
//					voObject.FontPageFooter = odrPCS[sys_ReportTable.FONTPAGEFOOTER_FLD].ToString().Trim();
//					voObject.FontReportFooter = odrPCS[sys_ReportTable.FONTREPORTFOOTER_FLD].ToString().Trim();
//
//					/// HACKED: Thachnn: modify the sys_report table
//					if (odrPCS[sys_ReportTable.USETEMPLATE_FLD] != DBNull.Value)
//						voObject.UseTemplate = Boolean.Parse(odrPCS[sys_ReportTable.USETEMPLATE_FLD].ToString().Trim());
//					else
//						voObject.UseTemplate = false;
//					voObject.TemplateFile = odrPCS[sys_ReportTable.TEMPLATEFILE_FLD].ToString().Trim();
//					/// ENDHACKED
//				}		
//				return voObject;					
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to sys_Report
		///    </Description>
		///    <Inputs>
		///       sys_ReportVO       
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
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_ReportVO voObject = (sys_ReportVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"UPDATE " + sys_ReportTable.TABLE_NAME + " SET "
					+ sys_ReportTable.REPORTNAME_FLD + "=   ?" + ","
					+ sys_ReportTable.DESCRIPTION_FLD + "=   ?" + ","
					+ sys_ReportTable.ISOCODE_FLD + "=   ?" + ","
					+ sys_ReportTable.REPORTFILE_FLD + "=   ?" + ","
					+ sys_ReportTable.REPORTTYPE_FLD + "=   ?" + ","
					+ sys_ReportTable.COMMAND_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINTOP_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINLEFT_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + "=   ?" + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + "=   ?" + ","
					+ sys_ReportTable.ORIENTATION_FLD + "=   ?" + ","
					+ sys_ReportTable.PAPERSIZE_FLD + "=   ?" + ","
					+ sys_ReportTable.TABLEBORDER_FLD + "=   ?" + ","
					+ sys_ReportTable.SIGNATURES_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTDETAIL_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + "=   ?" + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + "=  ?" + ","
					
					/// HACKED: Thachnn: modify the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + "=   ?" + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD + "=   ?"
					/// ENDHACKED

					+ " WHERE " + sys_ReportTable.REPORTID_FLD + " = ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTNAME_FLD].Value = voObject.ReportName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.DESCRIPTION_FLD].Value = voObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.ISOCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.ISOCODE_FLD].Value = voObject.ISOCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTFILE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTFILE_FLD].Value = voObject.ReportFile;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTTYPE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTTYPE_FLD].Value = voObject.ReportType;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.COMMAND_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.COMMAND_FLD].Value = voObject.Command;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINTOP_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINTOP_FLD].Value = voObject.MarginTop;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINLEFT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINLEFT_FLD].Value = voObject.MarginLeft;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINRIGHT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINRIGHT_FLD].Value = voObject.MarginRight;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINBOTTOM_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINBOTTOM_FLD].Value = voObject.MarginBottom;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINGUTTER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.MARGINGUTTER_FLD].Value = voObject.MarginGutter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.MARGINGUTTERPOS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.MARGINGUTTERPOS_FLD].Value = voObject.MarginGutterPos;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.ORIENTATION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.ORIENTATION_FLD].Value = voObject.Orientation;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.PAPERSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.PAPERSIZE_FLD].Value = voObject.PaperSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.TABLEBORDER_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_ReportTable.TABLEBORDER_FLD].Value = voObject.TableBorder;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.SIGNATURES_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.SIGNATURES_FLD].Value = voObject.Signatures;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTREPORTHEADER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTREPORTHEADER_FLD].Value = voObject.FontReportHeader;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPARAMETER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPARAMETER_FLD].Value = voObject.FontParameter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPAGEHEADER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPAGEHEADER_FLD].Value = voObject.FontPageHeader;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTGROUPBY_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTGROUPBY_FLD].Value = voObject.FontGroupBy;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTDETAIL_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTDETAIL_FLD].Value = voObject.FontDetail;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTPAGEFOOTER_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTPAGEFOOTER_FLD].Value = voObject.FontPageFooter;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.FONTREPORTFOOTER_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.FONTREPORTFOOTER_FLD].Value = voObject.FontReportFooter;

				/// HACKED: Thachnn: modify the sys_report table
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.USETEMPLATE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[sys_ReportTable.USETEMPLATE_FLD].Value = voObject.UseTemplate;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.TEMPLATEFILE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.TEMPLATEFILE_FLD].Value = voObject.TemplateFile;
				/// ENDHACKED
				
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = voObject.ReportID;

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
		///       This method uses to get data from sys_Report
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_ReportVO
		///    </Outputs>
		///    <Returns>
		///       sys_ReportVO
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

		public Object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_Report
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
		///       12/Oct/2005 Thachnn: fix bug injection
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
					+ sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + "," 
					
					/// HACKED: Thachnn: modify the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD
					/// ENDHACKED
					
					+ " FROM " + sys_ReportTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_ReportTable.TABLE_NAME);

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
		///       This method uses to delete data from sys_Report
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
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}

		//**************************************************************************
		///    <Description>
		///       This method uses to get all data from sys_Report of a Group
		///    </Description>
		///    <Inputs>
		///       Group ID
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
		///       Created: 28 - 12 - 2004
		///       Modified: 29 - 12 - 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///		Change query string in order to get ReportID, ReportName and ReportOrder from
		///		two tables sys_Report and sys_ReportAndGroup
		///    </Notes>
		//**************************************************************************

		public DataSet List(string pstrGroupID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = "SELECT "
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD
					
					+ " FROM " + sys_ReportTable.TABLE_NAME + "," + sys_ReportAndGroupTable.TABLE_NAME
					
					+ " WHERE " +  sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + " = " 
					+ sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTID_FLD 
					+ " AND " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.GROUPID_FLD + " = ? " // + pstrGroupID + "'"
					+ " ORDER BY " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD + " ASC";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_ReportTable.TABLE_NAME);

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
		///       This method uses to get all data from sys_Report of a Group
		///    </Description>
		///    <Inputs>
		///       Group ID
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
		///       Created: 28 - 12 - 2004
		///       Modified: 29 - 12 - 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///		Change query string in order to get ReportID, ReportName and ReportOrder from
		///		two tables sys_Report and sys_ReportAndGroup
		///    </Notes>
		//**************************************************************************
		public DataSet ListByUser(string pstrGroupID, int pintUserID)
		{
			const string METHOD_NAME = THIS + ".ListByUser()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql = "SELECT "
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD
					+ " FROM " + sys_ReportTable.TABLE_NAME 
					+ " JOIN " + sys_ReportAndGroupTable.TABLE_NAME 
					+ " ON " + sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + "=" + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTID_FLD
					+ " JOIN " + Sys_Report_RoleTable.TABLE_NAME
					+ " ON " + sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + "=" + Sys_Report_RoleTable.TABLE_NAME + "." + Sys_Report_RoleTable.REPORTID_FLD
					+ " JOIN " + Sys_UserToRoleTable.TABLE_NAME
					+ " ON " + Sys_Report_RoleTable.TABLE_NAME + "." + Sys_Report_RoleTable.ROLEID_FLD + "=" + Sys_UserToRoleTable.TABLE_NAME + "." + Sys_UserToRoleTable.ROLEID_FLD
					+ " WHERE " +  Sys_UserToRoleTable.TABLE_NAME + "." + Sys_UserToRoleTable.USERID_FLD + "=" + pintUserID
					+ " AND " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.GROUPID_FLD + "= ? " // + pstrGroupID + "'"
					+ " ORDER BY " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD + " ASC";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_ReportTable.TABLE_NAME);

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
		///       Monday, December 27, 2004
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","
					
					/// HACKED: Thachnn: modify the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD
					/// ENDHACKED
					/// 
					+ " FROM " + sys_ReportTable.TABLE_NAME;


				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_ReportTable.TABLE_NAME);

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
		///       This method uses to get the max value of ReportID in sys_Report
		///    </Description>
		///    <Inputs>
		///        N/A     
		///    </Inputs>
		///    <Outputs>
		///      Max ReportID
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetMaxReportID() 
		{
			const string METHOD_NAME = THIS + ".GetMaxReportID()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ISNULL(MAX(" 
					+ sys_ReportTable.REPORTID_FLD + "),0)" 
					+ " FROM  " + sys_ReportTable.TABLE_NAME ;

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				// return the max id
				return int.Parse(ocmdPCS.ExecuteScalar().ToString().Trim());

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get the ReportName of ReportID in sys_Report
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///        ReportName
		///    </Outputs>
		///    <Returns>
		///       string
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       20-Jan-2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public string GetReportName(string pstrReportID) 
		{
			const string METHOD_NAME = THIS + ".GetReportName()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT " + sys_ReportTable.REPORTNAME_FLD
					+ " FROM " + sys_ReportTable.TABLE_NAME
					+ " WHERE " + sys_ReportTable.REPORTID_FLD + "= ? ";// + pstrReportID + "'";

				DataAccess.Utils utils = new DataAccess.Utils();

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportTable.REPORTID_FLD].Value = pstrReportID;
				

				ocmdPCS.Connection.Open();
				// return the report name
				return ocmdPCS.ExecuteScalar().ToString().Trim();
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch(FormatException ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
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
		///       This method uses to get all data from sys_Report
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///       List of sys_ReportVO
		///    </Outputs>
		///    <Returns>
		///       ArrayList
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

		public ArrayList GetAllReports()
		{
			const string METHOD_NAME = THIS + ".GetAllReports()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			ArrayList arrReports = new ArrayList();
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","

					/// HACKED: Thachnn: modify the sys_report table
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD
					/// ENDHACKED

					+ " FROM " + sys_ReportTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{ 
					sys_ReportVO voObject = new sys_ReportVO();

					voObject.ReportID = odrPCS[sys_ReportTable.REPORTID_FLD].ToString().Trim();
					voObject.ReportName = odrPCS[sys_ReportTable.REPORTNAME_FLD].ToString().Trim();
					voObject.Description = odrPCS[sys_ReportTable.DESCRIPTION_FLD].ToString().Trim();
					voObject.ISOCode = odrPCS[sys_ReportTable.ISOCODE_FLD].ToString().Trim();
					voObject.ReportFile = odrPCS[sys_ReportTable.REPORTFILE_FLD].ToString().Trim();
					voObject.ReportType = odrPCS[sys_ReportTable.REPORTTYPE_FLD].ToString().Trim();
					voObject.Command = odrPCS[sys_ReportTable.COMMAND_FLD].ToString().Trim();
					voObject.MarginTop = int.Parse(odrPCS[sys_ReportTable.MARGINTOP_FLD].ToString().Trim());
					voObject.MarginLeft = int.Parse(odrPCS[sys_ReportTable.MARGINLEFT_FLD].ToString().Trim());
					voObject.MarginRight = int.Parse(odrPCS[sys_ReportTable.MARGINRIGHT_FLD].ToString().Trim());
					voObject.MarginBottom = int.Parse(odrPCS[sys_ReportTable.MARGINBOTTOM_FLD].ToString().Trim());
					voObject.MarginGutter = int.Parse(odrPCS[sys_ReportTable.MARGINGUTTER_FLD].ToString().Trim());
					voObject.MarginGutterPos = Boolean.Parse(odrPCS[sys_ReportTable.MARGINGUTTERPOS_FLD].ToString().Trim());
					voObject.Orientation = Boolean.Parse(odrPCS[sys_ReportTable.ORIENTATION_FLD].ToString().Trim());
					voObject.PaperSize = int.Parse(odrPCS[sys_ReportTable.PAPERSIZE_FLD].ToString().Trim());
					voObject.TableBorder = int.Parse(odrPCS[sys_ReportTable.TABLEBORDER_FLD].ToString().Trim());
					voObject.Signatures = odrPCS[sys_ReportTable.SIGNATURES_FLD].ToString().Trim();
					voObject.FontReportHeader = odrPCS[sys_ReportTable.FONTREPORTHEADER_FLD].ToString().Trim();
					voObject.FontParameter = odrPCS[sys_ReportTable.FONTPARAMETER_FLD].ToString().Trim();
					voObject.FontPageHeader = odrPCS[sys_ReportTable.FONTPAGEHEADER_FLD].ToString().Trim();
					voObject.FontGroupBy = odrPCS[sys_ReportTable.FONTGROUPBY_FLD].ToString().Trim();
					voObject.FontDetail = odrPCS[sys_ReportTable.FONTDETAIL_FLD].ToString().Trim();
					voObject.FontPageFooter = odrPCS[sys_ReportTable.FONTPAGEFOOTER_FLD].ToString().Trim();
					voObject.FontReportFooter = odrPCS[sys_ReportTable.FONTREPORTFOOTER_FLD].ToString().Trim();

					/// HACKED: Thachnn: modify the sys_report table
					if (odrPCS[sys_ReportTable.USETEMPLATE_FLD] != DBNull.Value)
						voObject.UseTemplate = Boolean.Parse(odrPCS[sys_ReportTable.USETEMPLATE_FLD].ToString().Trim());
					else
						voObject.UseTemplate = false;
					voObject.TemplateFile = odrPCS[sys_ReportTable.TEMPLATEFILE_FLD].ToString().Trim();
					/// ENDHACKED


					arrReports.Add(voObject);
				}
				arrReports.TrimToSize();
				return arrReports;
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
		/// Get all report of a group
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAllReports(string pstrGroupID)
		{
			const string METHOD_NAME = THIS + ".GetAllReports()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			ArrayList arrReports = new ArrayList();
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD
					+ " FROM " + sys_ReportTable.TABLE_NAME
					+ " JOIN " + sys_ReportAndGroupTable.TABLE_NAME
					+ " ON " + sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD
					+ " = " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTID_FLD
					+ " WHERE " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.GROUPID_FLD + "= ?"
					+ " ORDER BY " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD + " ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{ 
					sys_ReportVO voObject = new sys_ReportVO();

					voObject.ReportID = odrPCS[sys_ReportTable.REPORTID_FLD].ToString().Trim();
					voObject.ReportName = odrPCS[sys_ReportTable.REPORTNAME_FLD].ToString().Trim();
					voObject.Description = odrPCS[sys_ReportTable.DESCRIPTION_FLD].ToString().Trim();
					voObject.ISOCode = odrPCS[sys_ReportTable.ISOCODE_FLD].ToString().Trim();
					voObject.ReportFile = odrPCS[sys_ReportTable.REPORTFILE_FLD].ToString().Trim();
					voObject.ReportType = odrPCS[sys_ReportTable.REPORTTYPE_FLD].ToString().Trim();
					voObject.Command = odrPCS[sys_ReportTable.COMMAND_FLD].ToString().Trim();
					voObject.MarginTop = int.Parse(odrPCS[sys_ReportTable.MARGINTOP_FLD].ToString().Trim());
					voObject.MarginLeft = int.Parse(odrPCS[sys_ReportTable.MARGINLEFT_FLD].ToString().Trim());
					voObject.MarginRight = int.Parse(odrPCS[sys_ReportTable.MARGINRIGHT_FLD].ToString().Trim());
					voObject.MarginBottom = int.Parse(odrPCS[sys_ReportTable.MARGINBOTTOM_FLD].ToString().Trim());
					voObject.MarginGutter = int.Parse(odrPCS[sys_ReportTable.MARGINGUTTER_FLD].ToString().Trim());
					voObject.MarginGutterPos = Boolean.Parse(odrPCS[sys_ReportTable.MARGINGUTTERPOS_FLD].ToString().Trim());
					voObject.Orientation = Boolean.Parse(odrPCS[sys_ReportTable.ORIENTATION_FLD].ToString().Trim());
					voObject.PaperSize = int.Parse(odrPCS[sys_ReportTable.PAPERSIZE_FLD].ToString().Trim());
					voObject.TableBorder = int.Parse(odrPCS[sys_ReportTable.TABLEBORDER_FLD].ToString().Trim());
					voObject.Signatures = odrPCS[sys_ReportTable.SIGNATURES_FLD].ToString().Trim();
					voObject.FontReportHeader = odrPCS[sys_ReportTable.FONTREPORTHEADER_FLD].ToString().Trim();
					voObject.FontParameter = odrPCS[sys_ReportTable.FONTPARAMETER_FLD].ToString().Trim();
					voObject.FontPageHeader = odrPCS[sys_ReportTable.FONTPAGEHEADER_FLD].ToString().Trim();
					voObject.FontGroupBy = odrPCS[sys_ReportTable.FONTGROUPBY_FLD].ToString().Trim();
					voObject.FontDetail = odrPCS[sys_ReportTable.FONTDETAIL_FLD].ToString().Trim();
					voObject.FontPageFooter = odrPCS[sys_ReportTable.FONTPAGEFOOTER_FLD].ToString().Trim();
					voObject.FontReportFooter = odrPCS[sys_ReportTable.FONTREPORTFOOTER_FLD].ToString().Trim();

					/// HACKED: Thachnn: modify the sys_report table
					if (odrPCS[sys_ReportTable.USETEMPLATE_FLD] != DBNull.Value)
						voObject.UseTemplate = Boolean.Parse(odrPCS[sys_ReportTable.USETEMPLATE_FLD].ToString().Trim());
					else
						voObject.UseTemplate = false;
					voObject.TemplateFile = odrPCS[sys_ReportTable.TEMPLATEFILE_FLD].ToString().Trim();
					/// ENDHACKED


					arrReports.Add(voObject);
				}
				arrReports.TrimToSize();
				return arrReports;
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
		/// Get all report of a group
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAllReports(string pstrGroupID, int pintUserID)
		{
			const string METHOD_NAME = THIS + ".GetAllReports()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			ArrayList arrReports = new ArrayList();
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + ","
					+ sys_ReportTable.REPORTNAME_FLD + ","
					+ sys_ReportTable.DESCRIPTION_FLD + ","
					+ sys_ReportTable.ISOCODE_FLD + ","
					+ sys_ReportTable.REPORTFILE_FLD + ","
					+ sys_ReportTable.REPORTTYPE_FLD + ","
					+ sys_ReportTable.COMMAND_FLD + ","
					+ sys_ReportTable.MARGINTOP_FLD + ","
					+ sys_ReportTable.MARGINLEFT_FLD + ","
					+ sys_ReportTable.MARGINRIGHT_FLD + ","
					+ sys_ReportTable.MARGINBOTTOM_FLD + ","
					+ sys_ReportTable.MARGINGUTTER_FLD + ","
					+ sys_ReportTable.MARGINGUTTERPOS_FLD + ","
					+ sys_ReportTable.ORIENTATION_FLD + ","
					+ sys_ReportTable.PAPERSIZE_FLD + ","
					+ sys_ReportTable.TABLEBORDER_FLD + ","
					+ sys_ReportTable.SIGNATURES_FLD + ","
					+ sys_ReportTable.FONTREPORTHEADER_FLD + ","
					+ sys_ReportTable.FONTPARAMETER_FLD + ","
					+ sys_ReportTable.FONTPAGEHEADER_FLD + ","
					+ sys_ReportTable.FONTGROUPBY_FLD + ","
					+ sys_ReportTable.FONTDETAIL_FLD + ","
					+ sys_ReportTable.FONTPAGEFOOTER_FLD + ","
					+ sys_ReportTable.FONTREPORTFOOTER_FLD + ","
					+ sys_ReportTable.USETEMPLATE_FLD + ","
					+ sys_ReportTable.TEMPLATEFILE_FLD
					+ " FROM " + sys_ReportTable.TABLE_NAME 
					+ " JOIN " + sys_ReportAndGroupTable.TABLE_NAME 
					+ " ON " + sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + "=" + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTID_FLD
					+ " JOIN " + Sys_Report_RoleTable.TABLE_NAME
					+ " ON " + sys_ReportTable.TABLE_NAME + "." + sys_ReportTable.REPORTID_FLD + "=" + Sys_Report_RoleTable.TABLE_NAME + "." + Sys_Report_RoleTable.REPORTID_FLD
					+ " JOIN " + Sys_UserToRoleTable.TABLE_NAME
					+ " ON " + Sys_Report_RoleTable.TABLE_NAME + "." + Sys_Report_RoleTable.ROLEID_FLD + "=" + Sys_UserToRoleTable.TABLE_NAME + "." + Sys_UserToRoleTable.ROLEID_FLD
					+ " WHERE " +  Sys_UserToRoleTable.TABLE_NAME + "." + Sys_UserToRoleTable.USERID_FLD + "=" + pintUserID
					+ " AND " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.GROUPID_FLD + "= ? "
					+ " ORDER BY " + sys_ReportAndGroupTable.TABLE_NAME + "." + sys_ReportAndGroupTable.REPORTORDER_FLD + " ASC";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(sys_ReportAndGroupTable.GROUPID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[sys_ReportAndGroupTable.GROUPID_FLD].Value = pstrGroupID;
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				odrPCS = ocmdPCS.ExecuteReader();

				while (odrPCS.Read())
				{ 
					sys_ReportVO voObject = new sys_ReportVO();

					voObject.ReportID = odrPCS[sys_ReportTable.REPORTID_FLD].ToString().Trim();
					voObject.ReportName = odrPCS[sys_ReportTable.REPORTNAME_FLD].ToString().Trim();
					voObject.Description = odrPCS[sys_ReportTable.DESCRIPTION_FLD].ToString().Trim();
					voObject.ISOCode = odrPCS[sys_ReportTable.ISOCODE_FLD].ToString().Trim();
					voObject.ReportFile = odrPCS[sys_ReportTable.REPORTFILE_FLD].ToString().Trim();
					voObject.ReportType = odrPCS[sys_ReportTable.REPORTTYPE_FLD].ToString().Trim();
					voObject.Command = odrPCS[sys_ReportTable.COMMAND_FLD].ToString().Trim();
					voObject.MarginTop = int.Parse(odrPCS[sys_ReportTable.MARGINTOP_FLD].ToString().Trim());
					voObject.MarginLeft = int.Parse(odrPCS[sys_ReportTable.MARGINLEFT_FLD].ToString().Trim());
					voObject.MarginRight = int.Parse(odrPCS[sys_ReportTable.MARGINRIGHT_FLD].ToString().Trim());
					voObject.MarginBottom = int.Parse(odrPCS[sys_ReportTable.MARGINBOTTOM_FLD].ToString().Trim());
					voObject.MarginGutter = int.Parse(odrPCS[sys_ReportTable.MARGINGUTTER_FLD].ToString().Trim());
					voObject.MarginGutterPos = Boolean.Parse(odrPCS[sys_ReportTable.MARGINGUTTERPOS_FLD].ToString().Trim());
					voObject.Orientation = Boolean.Parse(odrPCS[sys_ReportTable.ORIENTATION_FLD].ToString().Trim());
					voObject.PaperSize = int.Parse(odrPCS[sys_ReportTable.PAPERSIZE_FLD].ToString().Trim());
					voObject.TableBorder = int.Parse(odrPCS[sys_ReportTable.TABLEBORDER_FLD].ToString().Trim());
					voObject.Signatures = odrPCS[sys_ReportTable.SIGNATURES_FLD].ToString().Trim();
					voObject.FontReportHeader = odrPCS[sys_ReportTable.FONTREPORTHEADER_FLD].ToString().Trim();
					voObject.FontParameter = odrPCS[sys_ReportTable.FONTPARAMETER_FLD].ToString().Trim();
					voObject.FontPageHeader = odrPCS[sys_ReportTable.FONTPAGEHEADER_FLD].ToString().Trim();
					voObject.FontGroupBy = odrPCS[sys_ReportTable.FONTGROUPBY_FLD].ToString().Trim();
					voObject.FontDetail = odrPCS[sys_ReportTable.FONTDETAIL_FLD].ToString().Trim();
					voObject.FontPageFooter = odrPCS[sys_ReportTable.FONTPAGEFOOTER_FLD].ToString().Trim();
					voObject.FontReportFooter = odrPCS[sys_ReportTable.FONTREPORTFOOTER_FLD].ToString().Trim();

					/// HACKED: Thachnn: modify the sys_report table
					if (odrPCS[sys_ReportTable.USETEMPLATE_FLD] != DBNull.Value)
						voObject.UseTemplate = Boolean.Parse(odrPCS[sys_ReportTable.USETEMPLATE_FLD].ToString().Trim());
					else
						voObject.UseTemplate = false;
					voObject.TemplateFile = odrPCS[sys_ReportTable.TEMPLATEFILE_FLD].ToString().Trim();
					/// ENDHACKED


					arrReports.Add(voObject);
				}
				arrReports.TrimToSize();
				return arrReports;
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
		///       This method uses to execute report command and return data set
		///    </Description>
		///    <Inputs>
		///        Report Command
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
		///       10-Jan-2005		
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ExecuteReportCommand(string pstrReportCommand)
		{
			const string METHOD_NAME = THIS + ".ExecuteReportCommand()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrReportCommand, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();
				
				// Turn off constraint checking before the dataset is filled.
				// This allows the adapters to fill the dataset without concern
				// for dependencies between the tables.
				dstPCS.EnforceConstraints = false;

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportTable.TABLE_NAME);

				// Turn constraint checking back on.
				dstPCS.EnforceConstraints = true;

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
		///       This method uses to execute report command and return data set
		///    </Description>
		///    <Inputs>
		///        Report Command
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
		///       10-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ExecuteReportCommand(string pstrReportCommand, string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".ExecuteReportCommand()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrReportCommand, oconPCS);
				ocmdPCS.Connection.Open();
				
				// Turn off constraint checking before the dataset is filled.
				// This allows the adapters to fill the dataset without concern
				// for dependencies between the tables.
				dstPCS.EnforceConstraints = false;

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrTableName);

				// Turn constraint checking back on.
				dstPCS.EnforceConstraints = true;

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
		///       This method uses to execute report command and return data set
		///    </Description>
		///    <Inputs>
		///        Report Command, List of Parameter
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
		///       10-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ExecuteReportCommand(string pstrReportCommand, ArrayList parrParameter)
		{
			/// UNDONE: Thachnn says: I feel this function is not used.
			const string METHOD_NAME = THIS + ".ExecuteReportCommand()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrReportCommand, oconPCS);
				ocmdPCS.Connection.Open();
				
				// Turn off constraint checking before the dataset is filled.
				// This allows the adapters to fill the dataset without concern
				// for dependencies between the tables.
				dstPCS.EnforceConstraints = false;

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, sys_ReportTable.TABLE_NAME);

				// Turn constraint checking back on.
				dstPCS.EnforceConstraints = true;

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
	}
}