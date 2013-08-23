using System;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Data.OleDb;

using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;

namespace InOutStockReport
{
	/// <summary>	
	/// In-Out stock report
	/// </summary>
	[Serializable]
	public class InOutStockReport : MarshalByRefObject, PCSUtils.Utils.IDynamicReport
	{
		#region IDynamicReport Members
		
		private bool mUseReportViewerRenderEngine = true;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport 
		/// or the ReportViewer Engine (in the ReportViewer form) 
		/// </summary> 
		public bool UseReportViewerRenderEngine 
		{ 
			get{ return mUseReportViewerRenderEngine;  } 
			set{ mUseReportViewerRenderEngine = value; } 
		}
		
		private ReportBuilder mReportBuilder;
		public PCSUtils.Utils.ReportBuilder PCSReportBuilder
		{
			get{ return mReportBuilder;  }
			set{ mReportBuilder = value; }
		}

		string mstrPCSConnectionString = string.Empty;
		public string PCSConnectionString
		{
			get{ return mstrPCSConnectionString;  }
			set{ mstrPCSConnectionString = value; }
		}

		private C1.Win.C1Preview.C1PrintPreviewControl mReportViewer;	
		public C1PrintPreviewControl PCSReportViewer
		{
			get{ return mReportViewer;  }
			set{ mReportViewer = value; }
		}

		private object mResult;
		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get{ return mResult;  }
			set{ mResult = value; }
		}
		
		private string mReportLayoutFile;
		public string ReportLayoutFile
		{
			get{ return mReportLayoutFile;}
			set{ mReportLayoutFile = value;}
		}

		private string mReportDefinitionFolder;
		public string ReportDefinitionFolder
		{
			get{ return ReportDefinitionFolder;}
			set{ mReportDefinitionFolder = value;}
		}

		#endregion
		
		private enum TransHisType
		{
			/// <summary>
			/// Out
			/// </summary>
			Out = 0,
			/// <summary>
			/// In
			/// </summary>
			In = 1,
			/// <summary>
			/// Both
			/// </summary>
			Both = 2
		}

		public InOutStockReport()
		{}

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
		
		/// <summary>
		/// Get comma-separated list of In stock tranaction type ID
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ. 28 Dec, 2005</author>
		private string GetInStockTransTypeID()
		{
			OleDbConnection oconPCS = null;
			
			try
			{
				string strInStockIDs = "0";			
									
				string strSQL = "SELECT " + MST_TranTypeTable.TRANTYPEID_FLD + " FROM  " + MST_TranTypeTable.TABLE_NAME;
				strSQL += " WHERE " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransHisType.In;
				strSQL += " OR " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransHisType.Both ;
						
				DataSet dstPCS = new DataSet();
				OleDbCommand ocmdPCS = null;
			
				oconPCS = new OleDbConnection(mstrPCSConnectionString);

				ocmdPCS = new OleDbCommand(strSQL, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_TranTypeTable.TABLE_NAME);
				
				if(dstPCS != null)
				{
					if(dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count != 0)
					{
						for(int i =0; i < dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count; i++)
						{
							strInStockIDs += ", " + dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows[i][MST_TranTypeTable.TRANTYPEID_FLD].ToString();
						}
					}
				}
				return strInStockIDs;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		
		/// <summary>
		/// Get comma-separated list of Out stock tranaction type ID
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ. 28 Dec, 2005</author>
		private string GetOutStockTransTypeID()
		{
			OleDbConnection oconPCS = null;
			try
			{
				string strOutStockIDs = "0";
						
				string strSQL = "SELECT " + MST_TranTypeTable.TRANTYPEID_FLD + " FROM  " + MST_TranTypeTable.TABLE_NAME;
				strSQL += " WHERE " + MST_TranTypeTable.TYPE_FLD + "=" + (int)TransHisType.Out;

				DataSet dstPCS = new DataSet();

			
				OleDbCommand ocmdPCS = null;				
				oconPCS = new OleDbConnection(mstrPCSConnectionString);
			
				ocmdPCS = new OleDbCommand(strSQL, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MST_TranTypeTable.TABLE_NAME);
				
				if(dstPCS != null)
				{
					if(dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count != 0)
					{
						for(int i =0; i < dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows.Count; i++)
						{
							strOutStockIDs += ", " + dstPCS.Tables[MST_TranTypeTable.TABLE_NAME].Rows[i][MST_TranTypeTable.TRANTYPEID_FLD].ToString();
						}
					}
				}
				return strOutStockIDs;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}

		
		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasterLocID, string pstrLocationID, string pstrBinID, string pstrFromDate, string pstrToDate, string pstrCategoryID, string pstrProductSourceID)
		{	
			OleDbConnection oconPCS = null;
			try
			{
				const string TABLE_NAME = "InOutStockReport";

				const int    SQL_DATE_LENGTH = 10;
				const string END_TIME_OF_DAY = " 23:59:59";
				const string START_TIME_OF_DAY = " 00:00:00";

				const string NO_FLD = "[No.]";
				const string CATEGORY_FLD = "[Category]";
				const string PART_NO_FLD = "[Part No.]";
				const string PART_NAME_FLD = "[Part Name]";
				const string MODEL_FLD = "[Model]";
				const string SOURCE_FLD = "[Source]";
				const string STOCK_UM_FLD = "[Stock UM]";
				const string BEGIN_STOCK_FLD = "[Begin Stock]";
				const string END_STOCK_FLD = "[End Stock]";
				const string IN_FLD = "[In]";
				const string OUT_FLD = "[Out]";			
				
				oconPCS = new OleDbConnection(mstrPCSConnectionString);
				
				OleDbCommand ocmdPCS = null;
				string strSql = string.Empty;				
				
				string strInStockTransID = GetInStockTransTypeID();
				string strOutStockTransID = GetOutStockTransTypeID();

				//Processing data
				pstrFromDate = pstrFromDate.Substring(0, SQL_DATE_LENGTH) + START_TIME_OF_DAY;
				pstrToDate = pstrToDate.Substring(0, SQL_DATE_LENGTH) + END_TIME_OF_DAY;
				
				DataTable dtbResult = new DataTable(TABLE_NAME);
			
				if(pstrBinID == null || pstrBinID == string.Empty)
				{
					//Select by location
					strSql =  " SELECT  DISTINCT 0 as " + NO_FLD + ",";
					strSql += " ITM_Category.Code as " + CATEGORY_FLD + ",";
					strSql += " ITM_Product.Code as " + PART_NO_FLD + ",";
					strSql += " ITM_Product.Description as " + PART_NAME_FLD + ",";
					strSql += " ITM_Product.Revision as " + MODEL_FLD + ",";
					strSql += " MST_UnitOfMeasure.Code as " + STOCK_UM_FLD + ",";
					strSql += " ITM_Source.Code as " + SOURCE_FLD + ",";				

					strSql += " (IV_LocationCache.OHQuantity";
					strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += "     FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID";
					strSql += " 		AND LocationID = IV_LocationCache.LocationID";
					strSql += " 		AND PostDate >= '" + pstrFromDate + "'";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += "   )";
					strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " WHERE ProductID = IV_LocationCache.ProductID";
					strSql += " 	AND LocationID = IV_LocationCache.LocationID";
					strSql += " 	AND PostDate >= '" + pstrFromDate + "'";
					strSql += " 	AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " )";
					strSql += " ) as " + BEGIN_STOCK_FLD + ",";

					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID";
					strSql += " 		AND TransQuantity > 0";
					strSql += " 		AND LocationID = IV_LocationCache.LocationID";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " )";
					strSql += " as " + IN_FLD + ",";
	
					strSql += " (";
					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID ";				
					strSql += " 		AND LocationID = IV_LocationCache.LocationID ";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "') ";
					strSql += " 		AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " )";
					strSql += " - ";
					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID";
					strSql += " 		AND TransQuantity < 0";
					strSql += " 		AND LocationID = IV_LocationCache.LocationID ";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "') ";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " )";
					strSql += " )";
					strSql += " as " + OUT_FLD + ",";

					strSql += " (";
					strSql += " IV_LocationCache.OHQuantity";
					strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID";	  
					strSql += " 		AND LocationID = IV_LocationCache.LocationID";
					strSql += " 		AND PostDate > '" + pstrToDate + "'";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " 	)";
					strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_LocationCache.ProductID";
					strSql += " 		AND LocationID = IV_LocationCache.LocationID";
					strSql += " 		AND PostDate > '" + pstrToDate + "'";
					strSql += " 		AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " 	)";
					strSql += " ) as " + END_STOCK_FLD;

					strSql += " FROM  	ITM_Product";
					strSql += " INNER JOIN IV_LocationCache ON IV_LocationCache.ProductID = ITM_Product.ProductID";
					strSql += " INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID";				

					strSql += " LEFT JOIN ITM_Source ON ITM_Source.SourceID = ITM_Product.SourceID";
					strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";

					strSql += " WHERE ITM_Product.CCNID = " + pstrCCNID;				
					strSql += " AND IV_LocationCache.LocationID = " + pstrLocationID;
                
					if(pstrCategoryID != null && pstrCategoryID != string.Empty)
					{
						strSql += " AND ITM_Category.CategoryID = " + pstrCategoryID;
					}

					if(pstrProductSourceID != null && pstrProductSourceID != string.Empty)
					{
						strSql += " AND ITM_Source.SourceID = " + pstrProductSourceID;
					}

					strSql += " GROUP BY";
					strSql += " 	ITM_Category.Code,";
					strSql += " 	MST_UnitOfMeasure.Code,";				
					strSql += " 	ITM_Product.Code,";
					strSql += " 	ITM_Product.Revision,";
					strSql += " 	ITM_Product.Description,";
					strSql += " 	ITM_Source.Code,";
					strSql += " 	IV_LocationCache.ProductID,";
					strSql += " 	IV_LocationCache.LocationID,";
					strSql += " 	IV_LocationCache.OHQuantity";

					strSql += " ORDER BY ITM_Category.Code, ITM_Product.Code";
				}
				else
				{
					//Select by Bin
					strSql =  " SELECT  DISTINCT 0 as " + NO_FLD + ",";
					strSql += " ITM_Category.Code as " + CATEGORY_FLD + ",";
					strSql += " ITM_Product.Code as " + PART_NO_FLD + ",";
					strSql += " ITM_Product.Description as " + PART_NAME_FLD + ",";
					strSql += " ITM_Product.Revision as " + MODEL_FLD + ",";
					strSql += " MST_UnitOfMeasure.Code as " + STOCK_UM_FLD + ",";
					strSql += " ITM_Source.Code as " + SOURCE_FLD + ",";				

					strSql += " (IV_BINCache.OHQuantity";
					strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += "     FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID";
					strSql += " 		AND BinID = IV_BINCache.BinID";
					strSql += " 		AND PostDate >= '" + pstrFromDate + "'";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += "   )";
					strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " WHERE ProductID = IV_BINCache.ProductID";
					strSql += " 	AND BinID = IV_BINCache.BinID";
					strSql += " 	AND PostDate >= '" + pstrFromDate + "'";
					strSql += " 	AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " )";
					strSql += " ) as " + BEGIN_STOCK_FLD + ",";

					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID";
					strSql += " 		AND TransQuantity > 0";
					strSql += " 		AND BinID = IV_BINCache.BinID";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "')";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " )";
					strSql += " as " + IN_FLD + ",";
	
					strSql += " (";
					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID ";				
					strSql += " 		AND BinID = IV_BINCache.BinID ";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "') ";
					strSql += " 		AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " )";
					strSql += " - ";
					strSql += " (SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID";
					strSql += " 		AND TransQuantity < 0";
					strSql += " 		AND BinID = IV_BINCache.BinID ";
					strSql += " 		AND (PostDate BETWEEN '" + pstrFromDate + "' AND '" + pstrToDate + "') ";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " )";
					strSql += " )";
					strSql += " as " + OUT_FLD + ",";

					strSql += " (";
					strSql += " IV_BINCache.OHQuantity";
					strSql += " - ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID";	  
					strSql += " 		AND BinID = IV_BINCache.BinID";
					strSql += " 		AND PostDate > '" + pstrToDate + "'";
					strSql += " 		AND (MST_TranType.Type = " + (int)TransHisType.In + " OR MST_TranType.Type = " + (int)TransHisType.Both + ")";
					strSql += " 	)";
					strSql += " + ( SELECT ISNULL(SUM(TransQuantity), 0)";
					strSql += " 	FROM v_TransactionHistory";
					strSql += "		     INNER JOIN MST_TranType ON v_TransactionHistory.TranTypeID = MST_TranType.TranTypeID";
					strSql += " 	WHERE ProductID = IV_BINCache.ProductID";
					strSql += " 		AND BinID = IV_BINCache.BinID";
					strSql += " 		AND PostDate > '" + pstrToDate + "'";
					strSql += " 		AND MST_TranType.Type = " + (int)TransHisType.Out;
					strSql += " 	)";
					strSql += " ) as " + END_STOCK_FLD;

					strSql += " FROM  	ITM_Product";
					strSql += " INNER JOIN IV_BINCache ON IV_BINCache.ProductID = ITM_Product.ProductID";
					strSql += " INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID";
				
					strSql += " LEFT JOIN ITM_Source ON ITM_Source.SourceID = ITM_Product.SourceID";
					strSql += " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID";

					strSql += " WHERE ITM_Product.CCNID = " + pstrCCNID;
					strSql += " AND IV_BINCache.BinID = " + pstrBinID;
				                
					if(pstrCategoryID != null && pstrCategoryID != string.Empty)
					{
						strSql += " AND ITM_Category.CategoryID = " + pstrCategoryID;
					}

					if(pstrProductSourceID != null && pstrProductSourceID != string.Empty)
					{
						strSql += " AND ITM_Source.SourceID = " + pstrProductSourceID;
					}

					strSql += " GROUP BY";
					strSql += " 	ITM_Category.Code,";
					strSql += " 	MST_UnitOfMeasure.Code,";
					strSql += " 	ITM_Product.Code,";
					strSql += " 	ITM_Product.Revision,";
					strSql += " 	ITM_Product.Description,";
					strSql += " 	ITM_Source.Code,";
					strSql += " 	IV_BINCache.ProductID,";
					strSql += " 	IV_BINCache.BinID,";
					strSql += " 	IV_BINCache.OHQuantity";				

					strSql += " ORDER BY ITM_Category.Code, ITM_Product.Code";				
				}
			
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				Console.WriteLine("Get In-Out Stock Data OK! 12 Jan 2006");

				if(dtbResult != null)
				{
					//Assign value for Index column
					//for(int i = 0; i< dstPCS.Tables[TABLE_NAME].Rows.Count; i++)
					//{
					//	dstPCS.Tables[TABLE_NAME].Rows[i][0] = i + 1;
					//}
					Console.WriteLine("Get In-Out Stock Data OK, begin return result(Count > 0)!");
					return dtbResult;
				}
				else
				{
					Console.WriteLine("Get In-Out Stock Data OK, begin return result (Count =0)!");
					return new DataTable(TABLE_NAME);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}
	}
}