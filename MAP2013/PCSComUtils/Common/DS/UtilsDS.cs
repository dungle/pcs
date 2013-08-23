using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Text;
using log4net;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using System.Text.RegularExpressions;



namespace PCSComUtils.Common.DS
{
	/// <summary>
	/// Summary description for UtilsDS.
	/// </summary>
	
	public class UtilsDS
	{
	    private ILog _logger = LogManager.GetLogger(typeof(UtilsDS));
		private const string THIS = "PCSComUtils.Common.DS.UtilsDS";

		/// <summary>
		/// Get comma-separated list of In stock tranaction type ID
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ. 28 Dec, 2005</author>
		public string GetInStockTransTypeID()
		{
			string strInStockIDs = "0";			
									
			string strSQL = "SELECT " + MST_TranTypeTable.TRANTYPEID_FLD + " FROM  " + MST_TranTypeTable.TABLE_NAME;
			strSQL += " WHERE " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransactionHistoryType.In +  " OR " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransactionHistoryType.Both ;
						
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

			ocmdPCS = new OleDbCommand(strSQL, oconPCS);
			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dstPCS, MST_TranTypeTable.TABLE_NAME);

			if(dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count != 0)
			{
				for(int i =0; i < dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count; i++)
				{
					strInStockIDs += ", " + dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows[i][MST_TranTypeTable.TRANTYPEID_FLD].ToString();
				}
			}

			return strInStockIDs;
		}

		/// <summary>
		/// Get comma-separated list of Out stock tranaction type ID
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ. 28 Dec, 2005</author>
		public string GetOutStockTransTypeID()
		{
			string strOutStockIDs = "0";
						
			string strSQL = "SELECT " + MST_TranTypeTable.TRANTYPEID_FLD + " FROM  " + MST_TranTypeTable.TABLE_NAME;
			strSQL += " WHERE " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransactionHistoryType.Out;

			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			
			ocmdPCS = new OleDbCommand(strSQL, oconPCS);
			ocmdPCS.Connection.Open();
			
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dstPCS, MST_TranTypeTable.TABLE_NAME);

			if(dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count != 0)
			{
				for(int i =0; i < dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count; i++)
				{
					strOutStockIDs += ", " + dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows[i][MST_TranTypeTable.TRANTYPEID_FLD].ToString();
				}
			}

			return strOutStockIDs;
		}

		public DateTime GetDBDate() 
		{
			const string METHOD_NAME = THIS + ".GetDBDate()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				strSql=	" SELECT  getdate() ";
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.CommandTimeout = 10000;

				ocmdPCS.Connection.Open();
				return (DateTime)ocmdPCS.ExecuteScalar();

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

		public string GetNoByMask(string strTableName,string strFieldName,DateTime dtEntryDate, string strFormat)
		{

			const string METHOD_NAME = THIS + ".GetNoByMask()";
			const string DATE_STRING_SHORT = "D";
			const string DATE_STRING_FULL = "DD";
			const string MONTH_STRING_SHORT = "M";
			const string MONTH_STRING_FULL = "MM";
			const string YEAR_STRING_SHORT = "YY";
			const string YEAR_STRING_FULL = "YYYY";
			if (strFormat == String.Empty)
			{
				strFormat ="YYYYMMDD0000";
			}
			else
			{
				//strFormat = strFormat.ToUpper();
				//get FormatType from Database
				Sys_ParamDS objSys_ParamDS = new Sys_ParamDS();
				strFormat = objSys_ParamDS.GetNameValue(strFormat);
				if (strFormat == String.Empty)
				{
					strFormat ="YYYYMMDD0000";
				}
			}
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{

				string strFormat_Type = strFormat.Substring(0,strFormat.IndexOf("0"));
				string strFormat_Number = strFormat.Substring(strFormat.IndexOf("0"));

				//Replace the format_type with real value
				//1.Year
				if (strFormat_Type.IndexOf(YEAR_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(YEAR_STRING_FULL,dtEntryDate.Year.ToString());
				}
				else
				{
					if (strFormat_Type.IndexOf(YEAR_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(YEAR_STRING_SHORT,dtEntryDate.Year.ToString().Substring(2));
					}
				}
				//2.Month
				if (strFormat_Type.IndexOf(MONTH_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(MONTH_STRING_FULL,dtEntryDate.Month.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(MONTH_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(MONTH_STRING_SHORT,dtEntryDate.Month.ToString());
					}
				}

				//3.Day
				if (strFormat_Type.IndexOf(DATE_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(DATE_STRING_FULL,dtEntryDate.Day.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(DATE_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(DATE_STRING_SHORT,dtEntryDate.Day.ToString());
					}
				}

				strFormat = strFormat_Type + strFormat_Number;

				string strYearMonthDay = dtEntryDate.Year.ToString() + dtEntryDate.Month.ToString().PadLeft(2,'0') + dtEntryDate.Day.ToString().PadLeft(2,'0');
				string strSql = String.Empty;
				strSql =  " SELECT max(" + strFieldName + ")" ;
				strSql += " FROM " + strTableName ;
				strSql += " WHERE " + strFieldName + " like '"+ strFormat_Type + "%'" ;

				//strSql += " WHERE " + strFieldName + " like '"+ strYearMonthDay + "%'" ;
	 
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					//return strFormat.Replace(strFormat_Number,"1".PadLeft(strFormat_Number.Length,'0'));
					strFormat_Number = "1".PadLeft(strFormat_Number.Length,'0');
					strFormat = strFormat_Type + strFormat_Number;
					return strFormat;
				}
				string strMaxValue = objResult.ToString().Trim();
				string strResult = strFormat_Type;

				int intNumberLength = strFormat_Number.Length;

				if (strMaxValue == String.Empty)
				{
					strResult += "1".PadLeft(intNumberLength,'0');					
				}
				else
				{
					int intNextValue = 0;
					try 
					{
						intNextValue = int.Parse(strMaxValue.Substring(strFormat_Type.Length+1)) + 1;
					}
					catch 
					{
						intNextValue = 1;
					}
					strResult += intNextValue.ToString().PadLeft(intNumberLength,'0');
				}

				return strResult;
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

		/// <summary>
		/// GetNoByMask new 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="fieldName"></param>
		/// <param name="prefix"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Dec 7 2005</date>
        public string GetNoByMask(string tableName, string fieldName, string prefix, string format)
		{
			const string DATE_STRING_SHORT = "D";
			const string DATE_STRING_FULL = "DD";
			const string MONTH_STRING_SHORT = "M";
			const string MONTH_STRING_FULL = "MM";
			const string YEAR_STRING_SHORT = "YY";
			const string YEAR_STRING_FULL = "YYYY";
			
			format = format == String.Empty ? "YYYYMMDD####" : format.ToUpper();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				//Get Date from Server
				DateTime dtEntryDate = new DateTime();
                dtEntryDate = GetDBDate();
				string strFormat_Type = string.Empty;
				//strFormat_Type += pstrPrefix;
				strFormat_Type += format.Substring(0,format.IndexOf("#"));
				string strFormat_Number = format.Substring(format.IndexOf("#"));

				//Replace the format_type with real value
				//1.Year
				if (strFormat_Type.IndexOf(YEAR_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(YEAR_STRING_FULL,dtEntryDate.Year.ToString());
				}
				else
				{
					if (strFormat_Type.IndexOf(YEAR_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(YEAR_STRING_SHORT,dtEntryDate.Year.ToString().Substring(2));
					}
				}
				//2.Month
				if (strFormat_Type.IndexOf(MONTH_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(MONTH_STRING_FULL,dtEntryDate.Month.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(MONTH_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(MONTH_STRING_SHORT,dtEntryDate.Month.ToString());
					}
				}

				//3.Day
				if (strFormat_Type.IndexOf(DATE_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(DATE_STRING_FULL,dtEntryDate.Day.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(DATE_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(DATE_STRING_SHORT,dtEntryDate.Day.ToString());
					}
				}
				strFormat_Type = prefix + strFormat_Type;
				string strSql = String.Empty;
				strSql =  " SELECT max(" + fieldName + ")" ;
				strSql += " FROM " + tableName ;
				strSql += " WHERE " + fieldName + " like '"+ strFormat_Type + "%'" ;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					strFormat_Number = "1".PadLeft(strFormat_Number.Length,'0');
					format = strFormat_Type + strFormat_Number;
					return format;
				}
				string strMaxValue = objResult.ToString().Trim();
				string strResult = strFormat_Type;

				int intNumberLength = strFormat_Number.Length;

				if (strMaxValue == String.Empty)
				{
					strResult += "1".PadLeft(intNumberLength,'0');					
				}
				else
				{
					int intNextValue = 0;
					try 
					{
						intNextValue = int.Parse(strMaxValue.Substring(strFormat_Type.Length)) + 1;
					}
					catch 
					{
						intNextValue = 1;
					}
					strResult += intNextValue.ToString().PadLeft(intNumberLength,'0');
				}

				return strResult;
			}
			catch
			{
				// Standard value incase unknown error
				return DateTime.Now.ToString("yyyyMMdd") + "0001";
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
        ///     Generates transaction number from mask and entry date
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="prefix"></param>
        /// <param name="format"></param>
        /// <param name="entryDate"></param>
        /// <returns></returns>
        public string GetNoByMask(string tableName, string fieldName, string prefix, string format, DateTime entryDate, out int revision)
        {
            const string dateStringShort = "D";
            const string dateStringFull = "DD";
            const string monthStringShort = "M";
            const string monthStringFull = "MM";
            const string yearStringShort = "YY";
            const string yearStringFull = "YYYY";

            format = format == String.Empty ? "YYYYMMDD####" : format.ToUpper();
            revision = 1;

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                string formatType = format.Substring(0, format.IndexOf("#"));
                string formatNumber = format.Substring(format.IndexOf("#"));

                //Replace the format_type with real value
                //1.Year
                if (formatType.IndexOf(yearStringFull) >= 0)
                {
                    formatType = formatType.Replace(yearStringFull, entryDate.Year.ToString());
                }
                else
                {
                    if (formatType.IndexOf(yearStringShort) >= 0)
                    {
                        formatType = formatType.Replace(yearStringShort, entryDate.Year.ToString().Substring(2));
                    }
                }
                //2.Month
                if (formatType.IndexOf(monthStringFull) >= 0)
                {
                    formatType = formatType.Replace(monthStringFull, entryDate.Month.ToString().PadLeft(2, '0'));
                }
                else
                {
                    if (formatType.IndexOf(monthStringShort) >= 0)
                    {
                        formatType = formatType.Replace(monthStringShort, entryDate.Month.ToString());
                    }
                }

                //3.Day
                if (formatType.IndexOf(dateStringFull) >= 0)
                {
                    formatType = formatType.Replace(dateStringFull, entryDate.Day.ToString().PadLeft(2, '0'));
                }
                else
                {
                    if (formatType.IndexOf(dateStringShort) >= 0)
                    {
                        formatType = formatType.Replace(dateStringShort, entryDate.Day.ToString());
                    }
                }
                formatType = prefix + formatType;
                string sql = " SELECT max(" + fieldName + ")";
                sql += " FROM " + tableName;
                sql += " WHERE " + fieldName + " LIKE '" + formatType + "%'";
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(sql, oconPCS);
                ocmdPCS.Connection.Open();

                object objResult = ocmdPCS.ExecuteScalar();
                if (objResult == DBNull.Value)
                {
                    formatNumber = "1".PadLeft(formatNumber.Length, '0');
                    format = formatType + formatNumber;
                    return format;
                }
                string strMaxValue = objResult.ToString().Trim();
                string strResult = formatType;

                int intNumberLength = formatNumber.Length;

                if (strMaxValue == String.Empty)
                {
                    strResult += "1".PadLeft(intNumberLength, '0');
                }
                else
                {
                    int intNextValue = 0;
                    try
                    {
                        intNextValue = int.Parse(strMaxValue.Substring(formatType.Length)) + 1;
                    }
                    catch
                    {
                        intNextValue = 1;
                    }
                    revision = intNextValue;
                    strResult += intNextValue.ToString().PadLeft(intNumberLength, '0');
                }

                return strResult;
            }
            catch
            {
                // Standard value incase unknown error
                return DateTime.Now.ToString("yyyyMMdd") + "0001";
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

		public string GetNoByMask(string pstrUsername, string pstrTableName,string pstrFieldName,string pstrPrefix, string pstrFormat)
		{
			const string DATE_STRING_SHORT = "D";
			const string DATE_STRING_FULL = "DD";
			const string MONTH_STRING_SHORT = "M";
			const string MONTH_STRING_FULL = "MM";
			const string YEAR_STRING_SHORT = "YY";
			const string YEAR_STRING_FULL = "YYYY";

		    pstrFormat = pstrFormat == String.Empty ? "YYYYMMDD####" : pstrFormat.ToUpper();

		    OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				//Get Date from Server
				DateTime dtEntryDate = new DateTime();
				dtEntryDate = GetDBDate();
				string strFormat_Type = string.Empty;
				//strFormat_Type += pstrPrefix;
				strFormat_Type += pstrFormat.Substring(0,pstrFormat.IndexOf("#"));
				string strFormat_Number = pstrFormat.Substring(pstrFormat.IndexOf("#"));

				//Replace the format_type with real value
				//1.Year
				if (strFormat_Type.IndexOf(YEAR_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(YEAR_STRING_FULL,dtEntryDate.Year.ToString());
				}
				else
				{
					if (strFormat_Type.IndexOf(YEAR_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(YEAR_STRING_SHORT,dtEntryDate.Year.ToString().Substring(2));
					}
				}
				//2.Month
				if (strFormat_Type.IndexOf(MONTH_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(MONTH_STRING_FULL,dtEntryDate.Month.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(MONTH_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(MONTH_STRING_SHORT,dtEntryDate.Month.ToString());
					}
				}

				//3.Day
				if (strFormat_Type.IndexOf(DATE_STRING_FULL) >= 0)
				{
					strFormat_Type = strFormat_Type.Replace(DATE_STRING_FULL,dtEntryDate.Day.ToString().PadLeft(2,'0'));
				}
				else
				{
					if (strFormat_Type.IndexOf(DATE_STRING_SHORT) >= 0)
					{
						strFormat_Type = strFormat_Type.Replace(DATE_STRING_SHORT,dtEntryDate.Day.ToString());
					}
				}
			    strFormat_Type = pstrUsername != ""
			                         ? pstrUsername + "-" + pstrPrefix + strFormat_Type
			                         : pstrPrefix + strFormat_Type;

			    var strSql =  " SELECT max(" + pstrFieldName + ")" ;
				strSql += " FROM " + pstrTableName ;
				strSql += " WHERE " + pstrFieldName + " like '"+ strFormat_Type + "%'" ;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult == DBNull.Value)
				{
					strFormat_Number = "1".PadLeft(strFormat_Number.Length,'0');
					pstrFormat = strFormat_Type + strFormat_Number;
					return pstrFormat;
				}
				string strMaxValue = objResult.ToString().Trim();
				string strResult = strFormat_Type;

				int intNumberLength = strFormat_Number.Length;

				if (strMaxValue == String.Empty)
				{
					strResult += "1".PadLeft(intNumberLength,'0');					
				}
				else
				{
					int intNextValue;
					try 
					{
						intNextValue = int.Parse(strMaxValue.Substring(strFormat_Type.Length)) + 1;
					}
					catch 
					{
						intNextValue = 1;
					}
					strResult += intNextValue.ToString().PadLeft(intNumberLength,'0');
				}

				return strResult;
			}
			catch
			{
				// Standard value incase unknown error
				return DateTime.Now.ToString("yyyyMMdd") + "0001";
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

		public bool IsTableOrViewConfigured(string pstrTableOrViewName)
		{
			bool blnRet = true;
			return blnRet;
		}

		public DataTable GetRows(string pstrTableName, string pstrFieldName, string pstrFieldValue, Hashtable phashOtherConditions, string pstrConditionByRecord)
		{
			const string METHOD_NAME = THIS + ".GetRows()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql = " SELECT * FROM " + pstrTableName + " WHERE 1=1 ";

				if(pstrFieldName != null && pstrFieldValue != null)
				{
					if (pstrFieldName.Length > 0 && pstrFieldValue.Length > 0)
					{
						strSql += " AND (" + pstrFieldName + " like '" + pstrFieldValue + "%')";
					}
				}
				if (phashOtherConditions != null)
				{
					IDictionaryEnumerator myEnumerator = phashOtherConditions.GetEnumerator();

					while( myEnumerator.MoveNext() )
					{
						if(myEnumerator.Value == DBNull.Value)
						{
							strSql += " AND (" + pstrTableName + "." + myEnumerator.Key.ToString().Trim() + " IS NULL)";
						}
						else
						{
							strSql += " AND (" + pstrTableName + "." + myEnumerator.Key.ToString().Trim() + "='" + myEnumerator.Value + "')";
						}
					}
				}

				strSql += pstrConditionByRecord;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,pstrTableName);

				return dstPCS.Tables[0];
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

		public DataTable GetRows(string pstrTableName, string pstrExpression)
		{
			const string METHOD_NAME = THIS + ".GetRows()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT * "
					+ " FROM " + pstrTableName;
				if ((pstrExpression.Trim() != string.Empty) && (pstrExpression != null))
					strSql += " " + pstrExpression;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrTableName);

				return dstPCS.Tables[pstrTableName];
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

		public DataTable GetRowsWithWhere(string pstrTableName, string pstrFieldName, string pstrFieldValue, string pstrWhereClause)
		{
			const string METHOD_NAME = THIS + ".GetRows()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT * "
					+ " FROM " + pstrTableName;
				if ((pstrWhereClause.Trim() != string.Empty) && (pstrWhereClause != null))
					strSql += " " + pstrWhereClause;
				if (pstrFieldName != null && pstrFieldName != String.Empty)
				{
					strSql += " AND " + pstrFieldName + " LIKE '" + pstrFieldValue + "%'";
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrTableName);

				return dstPCS.Tables[pstrTableName];
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
		/// Execute PO report and return data table
		/// </summary>
		/// <param name="pstrSql">Query string to be executed</param>
		/// <returns>Result DataTable</returns>
		public DataTable ExecutePOReport(ref string pstrSql, int pintPOMasterID)
		{
			const string METHOD_NAME = THIS + ".ExecutePOReport()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				pstrSql += " WHERE " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." +
					PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + " = " + pintPOMasterID +
					" ORDER BY DATEPART(d, " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.REQUIREDDATE_FLD + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				return dstPCS.Tables[0];
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
		/// Get working day in a year
		/// </summary>
		/// <param name="pintYear"></param>
		/// <returns></returns>
		public ArrayList GetWorkingDayByYear(int pintYear)
		{
			const string METHOD_NAME = THIS + ".GetWorkingDayByYear()";
			DataSet dstPCS = new DataSet();		
			
			ArrayList arrDayOfWeek = new ArrayList();
			
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME
					+ " WHERE " + MST_WorkingDayMasterTable.YEAR_FLD + "=" + pintYear;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayMasterTable.TABLE_NAME);
				
				if(dstPCS != null)
				{
					if(dstPCS.Tables[0].Rows.Count != 0)
					{
						DataRow drow = dstPCS.Tables[0].Rows[0];

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.MON_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Monday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.TUE_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Tuesday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.WED_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Wednesday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.THU_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Thursday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.FRI_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Friday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.SAT_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Saturday);
						}

						if(!bool.Parse(drow[MST_WorkingDayMasterTable.SUN_FLD].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Sunday);
						}
					}
				}

				return arrDayOfWeek;
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

		public ArrayList GetHolidaysInYear(DateTime pdtmStartDate, ScheduleMethodEnum penuSchedule)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS = null;			
			try 
			{
				string strSql = "SELECT "
					+ MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
					+ MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.OFFDAY_FLD + ","
					+ MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.COMMENT_FLD + ","
					+ MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
					+ " FROM " + MST_WorkingDayDetailTable.TABLE_NAME
					+ " INNER JOIN " + MST_WorkingDayMasterTable.TABLE_NAME 
					+ " ON " + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + "=" + MST_WorkingDayMasterTable.TABLE_NAME + "." + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD
					+ " WHERE " + MST_WorkingDayMasterTable.TABLE_NAME + ".[" + MST_WorkingDayMasterTable.YEAR_FLD + "]=" + pdtmStartDate.Year;
				
				//if(penuSchedule == ScheduleMethodEnum.Backward)
				//{
				//	strSql += " AND " + MST_WorkingDayDetailTable.TABLE_NAME + "."  + MST_WorkingDayDetailTable.OFFDAY_FLD + " <= '" + pdtmStartDate.ToString("yyyy-MM-dd") + "'";
				//}
				//else
				//{
				//	strSql += " AND " + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.OFFDAY_FLD + " >= '" + pdtmStartDate.ToString("yyyy-MM-dd") + "'";
				//}
				
				strSql += " ORDER BY " + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.OFFDAY_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_WorkingDayDetailTable.TABLE_NAME);
								
				if(dstPCS != null)
				{
					if(dstPCS.Tables[0].Rows.Count != 0)
					{	
						//have data--> create new array list to add items
						ArrayList arrHolidays = new ArrayList();
						for(int i =0; i< dstPCS.Tables[0].Rows.Count; i++)
						{
							DateTime dtmTemp = DateTime.Parse(dstPCS.Tables[0].Rows[i][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString());
							//truncate hour, minute, second
							dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);							
							arrHolidays.Add(dtmTemp);
						}
						// return holidays in a year
						return arrHolidays;
					}
					else
					{
						// other else, return a blank list
						return new ArrayList();
					}
				}
				// return a bank list
				return new ArrayList();
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
		/// Get all holidays in a year
		/// </summary>
		/// <param name="pintYear">Year</param>
		/// <returns>List of Holiday</returns>
		/// <author>DungLA</author>
		public ArrayList GetHolidaysInYear(int pintYear)
		{
			const string METHOD_NAME = THIS + ".GetHolidaysInYear()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS = null;			
			try 
			{
				string strSql = "SELECT OffDay FROM dbo.MST_WorkingDayDetail WHERE DATEPART(year, OffDay) = " + pintYear
					+ " ORDER BY OffDay";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_WorkingDayDetailTable.TABLE_NAME);
								
				if(dstPCS != null)
				{
					if(dstPCS.Tables[0].Rows.Count != 0)
					{	
						//have data--> create new array list to add items
						ArrayList arrHolidays = new ArrayList();
						for(int i =0; i< dstPCS.Tables[0].Rows.Count; i++)
						{
							DateTime dtmTemp = DateTime.Parse(dstPCS.Tables[0].Rows[i][MST_WorkingDayDetailTable.OFFDAY_FLD].ToString());
							//truncate hour, minute, second
							dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);							
							arrHolidays.Add(dtmTemp);
						}
						// return holidays in a year
						return arrHolidays;
					}
					else
					{
						// other else, return a blank list
						return new ArrayList();
					}
				}
				// return a bank list
				return new ArrayList();
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
		/// Retrieve all data from Workday Calendar Master table
		/// </summary>
		/// <returns></returns>
		public DataTable GetWorkingDay()
		{
			const string METHOD_NAME = THIS + ".GetWorkingDay()";
			DataSet dstPCS = new DataSet();		
			
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
					+ MST_WorkingDayMasterTable.SUN_FLD + ","
					+ MST_WorkingDayMasterTable.CCNID_FLD + ","
					+ MST_WorkingDayMasterTable.YEAR_FLD + ","
					+ MST_WorkingDayMasterTable.MON_FLD + ","
					+ MST_WorkingDayMasterTable.TUE_FLD + ","
					+ MST_WorkingDayMasterTable.WED_FLD + ","
					+ MST_WorkingDayMasterTable.THU_FLD + ","
					+ MST_WorkingDayMasterTable.FRI_FLD + ","
					+ MST_WorkingDayMasterTable.SAT_FLD
					+ " FROM " + MST_WorkingDayMasterTable.TABLE_NAME;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_WorkingDayMasterTable.TABLE_NAME);
				
				return dstPCS.Tables[0];
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
		/// Retrieve all data from workday calendar detail table
		/// </summary>
		/// <returns></returns>
		public DataTable GetHolidays()
		{
			const string METHOD_NAME = THIS + ".GetHolidays()";
			DataSet dstPCS = new DataSet();			
		
			OleDbConnection oconPCS = null;			
			try 
			{
				string strSql = "SELECT MST_WorkingDayDetail.WorkingDayDetailID, MST_WorkingDayDetail.OffDay,"
					+ " MST_WorkingDayDetail.Comment, MST_WorkingDayDetail.WorkingDayMasterID, MST_WorkingDayMaster.[Year]"
					+ " FROM MST_WorkingDayDetail JOIN MST_WorkingDayMaster"
					+ " ON MST_WorkingDayDetail.WorkingDayMasterID = MST_WorkingDayMaster.WorkingDayMasterID";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_WorkingDayDetailTable.TABLE_NAME);
								
				return dstPCS.Tables[0];
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

		public DataTable GetPurPoseByTransType(int pintTransTypeID)
		{
			const string METHOD_NAME = THIS + ".GetObjectPurPose()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT *"
					+ " FROM "  + PRO_IssuePurposeTable.TABLE_NAME
					+ " WHERE " + PRO_IssuePurposeTable.TRANTYPEID_FLD + "=" + (int) pintTransTypeID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter oldaPCS = new OleDbDataAdapter();
				oldaPCS.SelectCommand = ocmdPCS;
				oldaPCS.Fill(dstPCS, "PRO_IssuePurpose");

				return dstPCS.Tables[0];					
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
		/// Update selected state for multi selection form
		/// </summary>
		/// <param name="pstrTableName">Temp Table Name</param>
		/// <param name="pstrFilter">Filter string</param>
		/// <param name="pblnSelected">Select state</param>
		/// <returns></returns>
		public DataSet UpdateSelected(string pstrTableName, string pstrFilter, bool pblnSelected)
		{
			const string METHOD_NAME = THIS + ".UpdateSelected()";
			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql =	"UPDATE " + pstrTableName + " SET SELECTED = ?"
					+ " WHERE 1=1";
				if (pstrFilter != null && pstrFilter != string.Empty)
				{
					if (pstrFilter.ToUpper().IndexOf("LIKE") > 0)
						pstrFilter = pstrFilter.Replace("*", "%");

					strSql += " AND " + pstrFilter;
				}
				strSql += "; SELECT * FROM " + pstrTableName;

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("Selected", OleDbType.Boolean)).Value = pblnSelected;
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstPCS = new DataSet();
				odadPCS.Fill(dstPCS, pstrTableName);
				return dstPCS;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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

        public DataSet UpdateSelectedRow(string pstrTableName, string pstrFilter, bool pblnSelected,int iProductId,int iWorkOrderMasterID,int iWorkOrderDetailID,int iComponentID)
        {
            const string METHOD_NAME = THIS + ".UpdateSelected()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                string strSql = "UPDATE " + pstrTableName + " SET SELECTED = ?"
                    + " WHERE 1=1 AND ProductID= " + iProductId
                    +" AND WorkOrderMasterID =" + iWorkOrderMasterID
                    +" AND WorkOrderDetailID =" + iWorkOrderDetailID
                    +" AND ComponentID=" + iComponentID;
                if (pstrFilter != null && pstrFilter != string.Empty)
                {
                    if (pstrFilter.ToUpper().IndexOf("LIKE") > 0)
                        pstrFilter = pstrFilter.Replace("*", "%");

                    strSql += " AND " + pstrFilter;
                }
                strSql += "; SELECT * FROM " + pstrTableName;

                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Parameters.Add(new OleDbParameter("Selected", OleDbType.Boolean)).Value = pblnSelected;
                ocmdPCS.Connection.Open();
                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                DataSet dstPCS = new DataSet();
                odadPCS.Fill(dstPCS, pstrTableName);
                return dstPCS;
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

		public void UpdateTempTable(DataSet pdtbData)
		{

            const string METHOD_NAME = THIS + ".UpdateTempTable()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            var odadPCS = new OleDbDataAdapter();

            try
            {
                strSql = "SELECT * "
                     + "  FROM " + pdtbData.Tables[0].TableName;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                var cmdSelect = new OleDbCommand(strSql, oconPCS);
                cmdSelect.CommandTimeout = 10000;
                odadPCS.SelectCommand = cmdSelect;
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pdtbData.EnforceConstraints = false;
                int iCount = pdtbData.Tables[0].Rows.Count;

                odadPCS.Update(pdtbData, pdtbData.Tables[0].TableName);
            }
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw ex;
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
		/// <summary>
		/// Get selected records from temp table
		/// </summary>
		/// <param name="pstrTableName">Temp table name</param>
		/// <param name="pdstResultData">Result dataset structure</param>
		/// <returns></returns>
		public DataSet GetSelectedRecords(string pstrTableName, DataSet pdstResultData)
		{
			const string METHOD_NAME = THIS + ".GetSelectedRecords()";
			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql =	"SELECT * FROM " + pstrTableName
                    + " WHERE Selected = 1;;Drop table " + pstrTableName;
              

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				string sResultTableName = pstrTableName;
				if (pdstResultData.Tables.Count > 0)
					sResultTableName = pdstResultData.Tables[0].TableName;
				odadPCS.Fill(pdstResultData, sResultTableName);
				return pdstResultData;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
