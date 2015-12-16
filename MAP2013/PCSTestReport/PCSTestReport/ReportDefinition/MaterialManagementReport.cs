using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;
using PCSUtils.Utils;

namespace MaterialManagementReport
{
	[Serializable]
	public class MaterialManagementReport : MarshalByRefObject, IDynamicReport
	{
		public MaterialManagementReport()
		{
		}

		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;
		private object mResult;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mReportViewer; }
			set { mReportViewer = value; }
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		private bool mUseReportViewerRenderEngine = true;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by
		/// this IDynamicReport or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}

		private string mstrReportDefinitionFolder = string.Empty;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mstrReportDefinitionFolder; }
			set { mstrReportDefinitionFolder = value; }
		}


		private string mstrReportLayoutFile = string.Empty;		
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get 
			{
				return mstrReportLayoutFile;
			}
			set
			{
				mstrReportLayoutFile = value;
			}
		}


		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCNID, string pstrCategoryID, string pstrProductID)
		{
			DataTable dtbReportData = new DataTable();
			try
			{
				int intCCNID = 0;
				try
				{
					intCCNID = int.Parse(pstrCCNID);
				}
				catch
				{
				}
				int intCategoryID = 0;
				try
				{
					intCategoryID = int.Parse(pstrCategoryID);
				}
				catch
				{
				}
				int intProductID = 0;
				try
				{
					intProductID = int.Parse(pstrProductID);
				}
				catch
				{
				}
				DataTable dtbTop = new DataTable("MaterialReport");
				if (intProductID > 0)
					dtbTop = GetOneItem(intProductID);
				else
					dtbTop = GetTopLevelItem(intCCNID, intCategoryID);
				DataTable dtbAllChild = GetAllItems(intCCNID, intCategoryID);
				dtbReportData = dtbTop.Clone();
				foreach (DataRow drowData in dtbTop.Rows)
					BuildData(dtbReportData, dtbAllChild, drowData, 0, intCCNID, intCategoryID);
				// add number list
				dtbReportData = AddNumberedListToDataTable(dtbReportData);
			}
			catch
			{
			}
			return dtbReportData;
		}

		private void BuildData(DataTable pdtbData, DataTable pdtbAllChild, DataRow pdrowNew, int pintLevel, int pintCCNID, int pintCategoryID)
		{
			try
			{
				DataRow drowItem = pdtbData.NewRow();
				foreach (DataColumn dcolData in pdtbData.Columns)
					drowItem[dcolData.ColumnName] = pdrowNew[dcolData.ColumnName];
				drowItem["Level"] = pintLevel;
				pdtbData.Rows.Add(drowItem);
				// get child
				//DataTable dtbChild = GetChild(pintCCNID, pintCategoryID, int.Parse(drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString()));
				DataRow[] drowsChild = GetChild(pdtbAllChild, int.Parse(drowItem["ProductID"].ToString()));
				foreach (DataRow drowChild in drowsChild)
					BuildData(pdtbData, pdtbAllChild, drowChild, pintLevel + 1, pintCCNID, pintCategoryID);
			}
			catch
			{
			}
		}

		private DataRow[] GetChild(DataTable pdtbAllChilds, int pintParentID)
		{
			try
			{
				DataRow[] drowResult = pdtbAllChilds.Select("ParentID = '" + pintParentID.ToString() + "'");
				return drowResult;
			}
			catch
			{
				return null;
			}
		}

		private DataTable GetTopLevelItem(int pintCCNID, int pintCategoryID)
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbItem = new DataTable();
				string strSql = "SELECT	DISTINCT '0' AS 'Level', "
					+ "ITM_Category.Code AS CategoryCode, "
					+ "ITM_Product.Code AS 'Part No.', "
					+ "ITM_Product.Description AS 'Part Name', "
					+ "ITM_Product.Revision AS 'Model', "
					+ "MST_UnitOfMeasure.Code AS 'Stock UM', "
					+ "MST_WorkCenter.Code AS 'ProcessCode', "
					+ "MST_WorkCenter.Name AS 'ProcessName',"
					+ " CAST(NULL AS decimal(18,5)) AS 'BOM Qty',"
					+ " CAST(NULL AS decimal(18,5)) AS 'LeadTimeOffset',"
					+ " CAST(NULL AS decimal(18,5)) AS 'Shrink',"
					+ " MST_Party.Code AS 'Supplier',"
					+ " ITM_Product.ProductID, NULL AS 'ParentID',"
					+ " ITM_Product.MakeItem"
					+ " FROM ITM_Product JOIN ITM_BOM"
					+ " ON ITM_Product.ProductID = ITM_BOM.ProductID"
					+ " LEFT JOIN (SELECT PRO_ProductionLine.Code, PRO_ProductionLine.Name,"
					+ " MST_WorkCenter.WorkCenterID, MST_WorkCenter.ProductionLineID, ITM_Routing.ProductID"
					+ " FROM ITM_Routing JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine"
					+ " ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE ISNULL(MST_WorkCenter.IsMain , 0)= 1) MST_WorkCenter"
					+ " ON ITM_Product.ProductID = MST_WorkCenter.ProductID"
					+ " LEFT JOIN MST_Party"
					+ " ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " JOIN MST_UnitOfMeasure"
					+ " ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID"
					+ " WHERE ITM_Product.ProductID NOT IN (SELECT ITM_BOM.ComponentID FROM ITM_BOM)"
					+ " AND ITM_Product.CCNID = " + pintCCNID;
				if (pintCategoryID > 0)
					strSql += " AND ITM_Product.CategoryID = " + pintCategoryID;
				strSql += " ORDER BY ITM_Product.ProductID";
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);
				odad.Fill(dtbItem);
				return dtbItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}
		}

		private DataTable GetAllItems(int pintCCNID, int pintCategoryID)
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbItem = new DataTable();
				string strSql = "SELECT	DISTINCT '1' AS 'Level', "
					+ "ITM_Category.Code AS CategoryCode, "
					+ " ITM_Product.Code AS 'Part No.', "
					+ " ITM_Product.Description AS 'Part Name', "
					+ " ITM_Product.Revision AS 'Model', "
					+ " MST_UnitOfMeasure.Code AS 'Stock UM', "
					+ " MST_WorkCenter.Code AS 'ProcessCode', "
					+ " MST_WorkCenter.Name AS 'ProcessName',"
					+ " ITM_BOM.Quantity AS 'BOM Qty', ITM_BOM.LeadTimeOffset, ITM_BOM.Shrink,"
					+ " MST_Party.Code AS 'Supplier',"
					+ " ITM_Product.ProductID, ITM_BOM.ProductID AS 'ParentID',"
					+ " ITM_Product.MakeItem"
					+ " FROM ITM_Product JOIN ITM_BOM"
					+ " ON ITM_Product.ProductID = ITM_BOM.ComponentID"
					+ " LEFT JOIN (SELECT PRO_ProductionLine.Code, PRO_ProductionLine.Name,"
					+ " MST_WorkCenter.WorkCenterID, MST_WorkCenter.ProductionLineID, ITM_Routing.ProductID"
					+ " FROM ITM_Routing JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine"
					+ " ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE ISNULL(MST_WorkCenter.IsMain , 0)= 1) MST_WorkCenter"
					+ " ON ITM_Product.ProductID = MST_WorkCenter.ProductID"
					+ " LEFT JOIN MST_Party"
					+ " ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " JOIN MST_UnitOfMeasure"
					+ " ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID;
				if (pintCategoryID > 0)
					strSql += " AND ITM_Product.CategoryID = " + pintCategoryID;
				strSql += " ORDER BY ITM_Product.ProductID";
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);
				odad.Fill(dtbItem);
				return dtbItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}
		}

		private DataTable GetOneItem(int pintProductID)
		{
			OleDbConnection cn = null;
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbItem = new DataTable();
				string strSql = "SELECT	DISTINCT '0' AS 'Level', "
					+ "ITM_Category.Code AS CategoryCode, "
					+ "ITM_Product.Code AS 'Part No.', "
					+ "ITM_Product.Description AS 'Part Name', "
					+ "ITM_Product.Revision AS 'Model', "
					+ "MST_UnitOfMeasure.Code AS 'Stock UM', "
					+ "MST_WorkCenter.Code AS 'ProcessCode', "
					+ "MST_WorkCenter.Name AS 'ProcessName',"
					+ " CAST(NULL AS decimal(18,5)) AS 'BOM Qty',"
					+ " CAST(NULL AS decimal(18,5)) AS 'LeadTimeOffset',"
					+ " CAST(NULL AS decimal(18,5)) AS 'Shrink',"
					+ " MST_Party.Code AS 'Supplier',"
					+ " ITM_Product.ProductID, NULL AS 'ParentID',"
					+ " ITM_Product.MakeItem"
					+ " FROM ITM_Product JOIN ITM_BOM"
					+ " ON ITM_Product.ProductID = ITM_BOM.ProductID"
					+ " LEFT JOIN (SELECT PRO_ProductionLine.Code, PRO_ProductionLine.Name,"
					+ " MST_WorkCenter.WorkCenterID, MST_WorkCenter.ProductionLineID, ITM_Routing.ProductID"
					+ " FROM ITM_Routing JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine"
					+ " ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE ISNULL(MST_WorkCenter.IsMain , 0)= 1) MST_WorkCenter"
					+ " ON ITM_Product.ProductID = MST_WorkCenter.ProductID"
					+ " LEFT JOIN MST_Party"
					+ " ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " JOIN MST_UnitOfMeasure"
					+ " ON MST_UnitOfMeasure.UnitOfMeasureID = ITM_Product.StockUMID"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ITM_Product.ProductID = " + pintProductID;
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);
				odad.Fill(dtbItem);
				return dtbItem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}
		}

		//**************************************************************************              
		///    <summary>
		///       Add a column named "NumberedList" to dataTable		
		///    </summary>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       ThachNN
		///    </Authors>
		///    <History>
		///       21-Sep-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private DataTable AddNumberedListToDataTable(DataTable pdtb)
		{
			try
			{
				DataTable dtbRet = pdtb.Copy();

				DataColumn odcol = new DataColumn("NumberedList");
				odcol.DataType = typeof (string);
				odcol.DefaultValue = "";
				dtbRet.Columns.Add(odcol);

				int[] arriInputLevel = ExtractArrayOfLevelFromDataTable(pdtb);
				StringCollection arrNumberedList = GetNumberedListForBOMProduct(arriInputLevel, 0, ".");

				int intCount = 0;
				foreach (DataRow row in dtbRet.Rows)
				{
					string strNumber = arrNumberedList[intCount];
					string strPartNo = row["Part No."].ToString();
					row["NumberedList"] = strNumber;
					// indent the part no following the level
					int intLevel = int.Parse(row["Level"].ToString());
					for (int i = 0; i < intLevel; i++)
						strPartNo = "'- " + strPartNo;
					row["Part No."] = strPartNo;
					intCount++;
				}
				return dtbRet;
			}
			catch
			{
				return null;
			}
		}

		//**************************************************************************              
		///    <summary>
		///       Generate a list of number string (format as 1.1.2.3, ...) to append to BOM product report
		///       Use only by AddNumberedListToDataTable() method
		///    </summary>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       ThachNN
		///    </Authors>
		///    <History>
		///       21-Sep-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static StringCollection GetNumberedListForBOMProduct(int[] parriInput,
		                                                            int pintRootNumber,
		                                                            string pstrDeli)
		{
			try
			{
				#region DEFINE Variables

				StringCollection arrRet = new StringCollection();
				int intRecordCount = parriInput.Length;
				StringCollection arrParentString = new StringCollection();
				for (int intCounter = 0; intCounter < intRecordCount + 1; intCounter++)
				{
					arrParentString.Add("");
				}
				int[] arriLevelHit = new int[intRecordCount + 1];

				#endregion

				int intPrev = pintRootNumber; // in start phase, iRootNumber is iPrev
				foreach (int i in parriInput)
				{
					string strOut = "";
					/// Update level hit count == active running number of last index
					(arriLevelHit[i])++; // increase the hit count  of level i
					arriLevelHit[i + 1] = 0; // reset hit count of level i+1 to ZERO

					if (i == pintRootNumber) // if the level is restart to iRootNumber
					{
						// level 0, not exist
						// Parent string of level iRootNumber, alway = ""
						// strOut always = "1"
						arrParentString[i] = "";
						strOut = "1";
					}
					else
					{
						strOut = arrParentString[i] + pstrDeli + arriLevelHit[i];
						if (strOut.StartsWith("."))
							strOut = strOut.Remove(0, 1);
					}
					intPrev = i;
					arrParentString[i + 1] = strOut;
					arrRet.Add(strOut);
				}
				return arrRet;
			}
			catch
			{
				return null;
			}
		}

		//**************************************************************************              
		///    <summary>
		///       Return array of int, contain level columm (named "Level") from DataTable
		///       Use only by AddNumberedListToDataTable() method
		///    </summary>
		///    <Inputs>
		///       DataTable contain column "Level"
		///    </Inputs>
		///    <Outputs>
		///       Array of int, contain value from "Level" data column
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       ThachNN
		///    </Authors>
		///    <History>
		///       21-Sep-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static int[] ExtractArrayOfLevelFromDataTable(DataTable pdtb)
		{
			try
			{
				int[] arrintRet = new int[pdtb.Rows.Count];
				int intCount = 0;
				foreach (DataRow row in pdtb.Rows)
				{
					arrintRet[intCount] = int.Parse(row["Level"].ToString());
					intCount++;
				}
				return arrintRet;
			}
			catch
			{
				return null;
			}
		}

	}
}