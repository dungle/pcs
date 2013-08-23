using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;


namespace PCSComUtils.ErrorMsg.DS
{
	public class Sys_Error_MsgDS
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.Sys_Error_MsgDS";
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
				+ Sys_Error_MsgTable.ERROR_MSGID_FLD + ","
				+ Sys_Error_MsgTable.CODE_FLD + ","
				+ Sys_Error_MsgTable.MSGDEFAULT_FLD + ","
				+ Sys_Error_MsgTable.MSGVN_FLD + ","
				+ Sys_Error_MsgTable.MSGEN_FLD + ","
				+ Sys_Error_MsgTable.MSGJP_FLD + ","
				+ Sys_Error_MsgTable.DESCRIPTION_FLD
					+ " FROM " + Sys_Error_MsgTable.TABLE_NAME;
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_Error_MsgTable.TABLE_NAME);

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

		public string GetErrorMessage(int pintCode, KindOfLanguage pKindOfLanguge)
		{
			return String.Empty;
		}
	}

	public enum KindOfLanguage
	{
		Default,
		English,
		VietNamese,
		Japanese
	}
}
