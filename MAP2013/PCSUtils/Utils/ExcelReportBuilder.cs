using System;
using System.Collections;
using System.IO;
using System.Drawing;
using Microsoft.Office.Interop;
using Range = Microsoft.Office.Interop.Excel.Range;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;
using Chart = Microsoft.Office.Interop.Excel.Chart;
using Excel = Microsoft.Office.Interop.Excel;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Summary description for ExcelReportBuilder.
	/// </summary>
	public class ExcelReportBuilder : IDisposable
	{

		public static int PENDING_BEFORE_START_EXCEL_PROCESS = 1500;
		private object MISSING = Type.Missing;

		private Microsoft.Office.Interop.Excel.Application mExcelApp = new Microsoft.Office.Interop.Excel.Application();		

		#region	PROPERTIES
		
		private string mstrExcelTemplateFile;
		public string ExcelTemplateFile
		{
			get
			{
				return mstrExcelTemplateFile;
			}
			set
			{
				mstrExcelTemplateFile = value;
			}
		}


		private int mnCurrentSheetIndex = 1;
		/// <summary>
		/// While setting this Property, the Current-WorkSheet object that ExcelReportBuilder use to work
		/// (with other function) will change
		/// </summary>
		public int CurrentSheetIndex
		{
			get
			{
				return mnCurrentSheetIndex;
			}			
			set
			{
				mnCurrentSheetIndex = value;
				ChangeCurrentWorksheet(mnCurrentSheetIndex);
			}
		}

		
		private Microsoft.Office.Interop.Excel.Workbook mBook;
		/// <summary>
		/// READONLY
		/// Return current (working) workbook (for all function in here) of this Builder
		/// </summary>
		public Workbook CurrentWorkbook
		{
			get
			{
				return mBook;
			}			
		}



		private Microsoft.Office.Interop.Excel.Worksheet mSheet;
		/// <summary>
		/// READONLY
		/// Return current (working) worksheet (for all function in here) of this Builder
		/// </summary>
		public Worksheet CurrentWorksheet
		{
			get
			{
				return mSheet;
			}
		}


		#endregion PROPERTIES

		
		/// <summary>
		/// Initialize all things to working with provided XLS Workbook
		/// Open the workbook (with provided path)
		/// Set the working worksheet to worksheet No.1
		/// </summary>
		/// <param name="pstrWorkbookToOpen"></param>
		public ExcelReportBuilder(string pstrWorkbookToOpen)
		{
			OpenWorkbook(pstrWorkbookToOpen);
			ChangeCurrentWorksheet(1);
		}


		#region IDisposable Members
		public void Dispose()
		{
			try
			{
				if (mBook != null)
				{
					mBook.Close(false, MISSING,MISSING);
					mBook = null;
				}
			}
			catch{}

			try
			{
				if (mSheet != null)
					mSheet = null;
			}
			catch{}

			try
			{
				if (mExcelApp != null)
				{
					mExcelApp.Quit();
					mExcelApp = null;
				}
			}
			catch{}
		}
		#endregion

		
		#region REFER TO EXCEL ELEMENT FUNCTION
		

		/// <summary>
		/// ROOT FUNCTION
		/// MUST USE, MUST SHARE
		/// Wrap the get_Range() function of the Interop
		/// we can put address string like "A1:D4, F2:G5" to the first parameter to retrieve UNION of 2 ranges
		/// or "A1:D4 F2:G5" to retrieve INTERSECT of 2 ranges
		/// </summary>
		/// <param name="pObj1"></param>
		/// <param name="pObj2"></param>
		/// <returns></returns>
		public Range GetRange(object pObj1, object pObj2)
		{
			return mSheet.get_Range(pObj1, pObj2);
		}

		/// <summary>
		/// ROOT FUNCTION
		/// MUST USE, MUST SHARE
		/// </summary>
		/// <param name="pnFromRowIndex"></param>
		/// <param name="pnFromColumnIndex"></param>
		/// <param name="pnToRowIndex"></param>
		/// <param name="pnToColumnIndex"></param>
		/// <returns></returns>
		public Range GetRange(int pnFromRowIndex, int pnFromColumnIndex, 
			int pnToRowIndex, int pnToColumnIndex	)
		{
			return mSheet.get_Range(
				mSheet.Cells[pnFromRowIndex	,	pnFromColumnIndex	],
				mSheet.Cells[pnToRowIndex		,	pnToColumnIndex		]
				);
		}

		/// <summary>
		/// ROOT FUNCTION
		/// MUST USE, MUST SHARE
		/// </summary>
		/// <param name="pnRowIndex"></param>
		/// <param name="pnColumnIndex"></param>
		/// <returns></returns>
		public Range GetCell(int pnRowIndex, int pnColumnIndex)
		{
			return CRange( mSheet.Cells[pnRowIndex,pnColumnIndex] );
		}
	
		public Range GetCell(string pstrCellAddress)
		{
			return GetRange(pstrCellAddress,Type.Missing);
		}
		/// <summary>
		/// Get entire row
		/// </summary>
		/// <param name="pnRowIndex"></param>
		/// <returns></returns>
		public Range GetRow(int pnRowIndex)
		{
			return CRange(mSheet.Rows[pnRowIndex,MISSING]);
		}


		/// <summary>
		/// Get List of row, for example: "1:3" return row 1 2 and 3
		/// </summary>
		/// <param name="pstrRowAddress">string of address: "1:3" or etc</param>
		/// <returns></returns>
		public Range GetRow(string pstrRowAddress)
		{
			return CRange(mSheet.Rows[pstrRowAddress ,MISSING]);
		}

		/// <summary>
		/// Get entire COlumn
		/// </summary>
		/// <param name="pnColumnIndex"></param>
		/// <returns></returns>
		public Range GetColumn(int pnColumnIndex)
		{
			return CRange( mSheet.Columns[pnColumnIndex,MISSING]) ;
		}


		/// <summary>
		/// Get List of column, for example: "1:3" return column 1 2 and 3
		/// </summary>
		/// <param name="pstrColumnAddress">string of address: "1:3" or etc</param>
		/// <returns></returns>
		public Range GetColumn(string pstrColumnAddress)
		{
			return CRange(mSheet.Columns[pstrColumnAddress ,MISSING]);
		}



		/// <summary>
		///  get the fisrt Chart of CurrentWorksheet
		/// </summary>
		/// <returns></returns>
		public Excel.ChartObject GetChart()
		{			
			Excel.ChartObjects charts = (Excel.ChartObjects)mSheet.ChartObjects(Type.Missing);        		
			
			return (Excel.ChartObject)charts.Item(1);
		}

		/// <summary>
		/// get the Chart of provided index
		/// </summary>
		/// <param name="pChartIndex"></param>
		/// <returns></returns>
		public Excel.ChartObject GetChart(int pChartIndex)
		{			
			Excel.ChartObjects charts = (Excel.ChartObjects)mSheet.ChartObjects(Type.Missing);        		
			
			return (Excel.ChartObject)charts.Item(pChartIndex);
		}

		/// <summary>
		/// get the Chart having Name = pstrChartName
		/// </summary>
		/// <param name="pChartIndex"></param>
		/// <returns></returns>
		public Excel.ChartObject GetChart(string pstrChartName)
		{			
			Excel.ChartObjects charts = (Excel.ChartObjects)mSheet.ChartObjects(Type.Missing);
			
			return (Excel.ChartObject)charts.Item(pstrChartName);
		}


		#endregion


		
		#region CHANGE EXCEL ELEMENT PROPERTIES: COLOR, ...
				
		/// <summary>
		/// Set back color for specific rannge
		/// </summary>
		/// <param name="pRange"></param>
		/// <param name="pInteropColor"></param>
		public static void SetRangeColor(Range pRange, InteropExcelColorEnum pInteropColor)
		{
			pRange.Interior.ColorIndex = (int)pInteropColor;			
		}

		/// <summary>
		/// Set back color for specific cell
		/// CALL: public void SetRangeColor(Range pRange, InteropExcelColorEnum pInteropColor)
		/// </summary>
		/// <param name="pnRowIndex"></param>
		/// <param name="pnColumnIndex"></param>
		/// <param name="pInteropColor"></param>
		public void SetCellColor(int pnRowIndex, int pnColumnIndex, InteropExcelColorEnum pInteropColor)
		{
			SetRangeColor( GetCell(pnRowIndex,pnColumnIndex)  , pInteropColor);
		}

	
		#endregion


		
		#region WORKBOOK, WORKSHEET FUNCTION	


		/// <summary>
		/// Open Excel Workbook
		/// set CurrentWorkbook to received-workbook
		/// EXCEPTION: throw FileNotFoundException if error
		/// </summary>
		/// <param name="pstrWorkbookToOpen">Full path of Workbook to Open</param>		
		public void OpenWorkbook(string pstrWorkbookToOpen)
		{	
			if(! File.Exists(pstrWorkbookToOpen))	// file not exist
			{
				throw new FileNotFoundException(pstrWorkbookToOpen);				
			}

			/// clear all atribbute on Working Book
			System.IO.File.SetAttributes(pstrWorkbookToOpen, FileAttributes.Archive);
		
			mBook = mExcelApp.Workbooks.Open(pstrWorkbookToOpen,
				Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);

			mExcelApp.ScreenUpdating = false;
			mExcelApp.Visible = false;
			mExcelApp.DisplayAlerts = false;
		}

		/// <summary>
		/// Save the Current working workbook
		/// CLose the Current working workbook
		/// Quit the Excel Interop process
		/// 
		/// After calling this function, you should NOT use this Builder ANYMORE
		/// </summary>
		public void CloseWorkbook()
		{
			mExcelApp.ScreenUpdating = true;
			mExcelApp.Visible = false;
			mExcelApp.DisplayAlerts = false;

			mBook.Save();
			mBook.Close(Type.Missing,Type.Missing,Type.Missing);		
			mSheet = null;
			mBook = null;

			if (mExcelApp != null) 
			{
				mExcelApp.Quit();
				System.Runtime.InteropServices.Marshal.ReleaseComObject (mExcelApp);
				mExcelApp = null;
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

		}

		/// <summary>
		/// This function return object reference to the Worksheet with provide index
		/// 
		/// EXCEPTION: If the Worksheet is not exist, IndexOutOfRangeException will throw out
		/// </summary>
		/// <param name="pnWorkSheetIndexToGet">Sheet index to get</param>
		/// <returns></returns>
		public Worksheet GetWorksheet(int pnWorksheetIndexToGet)
		{			
			try
			{
				return (Worksheet)mBook.Worksheets.get_Item(pnWorksheetIndexToGet);				
			}
			catch(Exception ex)
			{
				throw new IndexOutOfRangeException(ex.Message);
			}
		}
		
		/// <summary>
		/// Change the CUrrent worksheet (current WOrking Sheet for other function)
		/// Also Change the CurrentSheetIndex
		/// 
		/// EXCEPTION: If the Worksheet is not exist, IndexOutOfRangeException will throw out
		/// </summary>
		/// <param name="pnWorksheetIndex">Sheet index to change</param>
		/// <returns></returns>
		public Worksheet ChangeCurrentWorksheet(int pnWorksheetIndex)
		{	
			/// Get the worksheet
			mSheet = GetWorksheet(pnWorksheetIndex);

			/// change the Current Worksheet Index
			mnCurrentSheetIndex = pnWorksheetIndex;

			return mSheet;
		}


		#endregion



		/// <summary>
		/// ROOT FUNCTION
		/// MUST BE SHARE, MUST USE: COMMON IN THIS CLASS
		/// Convert function
		/// Convert input object to Microsoft.Office.Interop.Excel.Range type
		/// EXCEPTION: throw InvalidCastException if error
		/// </summary>
		/// <param name="pobj">object to convert</param>
		/// <returns>object of Range type</returns>
		public static Microsoft.Office.Interop.Excel.Range CRange(object pobj)
		{
			try
			{
				return ( Microsoft.Office.Interop.Excel.Range)pobj;
			}
			catch(Exception ex)
			{
				throw new InvalidCastException(ex.Message);
			}
		}

		/// <summary>
		/// Wait PENDING_BEFORE_START_EXCEL_PROCESS miliseconds before Start the external Process to open Excel
		/// Show the excel file
		/// </summary>		
		/// <param name="pstrFileToOpenPath">Provide Excel file to open, include full path</param>
		/// <returns>true if OK, false if fail</returns>
		public static bool ShowExcelFile( string pstrFileToOpenPath)
		{
			const string EXCEL_EXE = "EXCEL.EXE";
			
			if( !File.Exists(pstrFileToOpenPath) )	//file not exist
			{
				throw new FileNotFoundException(pstrFileToOpenPath);
			}

			try			
			{
				Microsoft.Office.Interop.Excel.Application objExcelApp = new Microsoft.Office.Interop.Excel.Application();
				string strExcelInstallFolder = objExcelApp.Path;

				System.Threading.Thread.Sleep(PENDING_BEFORE_START_EXCEL_PROCESS);
				System.Diagnostics.Process.Start( strExcelInstallFolder + Path.DirectorySeparatorChar + EXCEL_EXE, "\"" + pstrFileToOpenPath +"\"");
			}				
			catch
			{
				return false;
			}
			return true;
		}
	

	}



	/// <summary>
	/// Enum for ColorIndex of EXCEL COM-Interop
	/// We use this for assign back color of Cell
	/// Example: (CRange(objSheet.Cells[1,1]) ).Interior.ColorIndex = InteropExcelColorEnum.Red;
	/// </summary>
	public enum InteropExcelColorEnum
	{
		Black = 1,
		White = 2,
		Red = 3,
		BrightGreen = 4,
		Blue = 5,
		Yellow = 6,
		Pink = 7,
		Turquoise = 8,
		DarkRed = 9,
		Green = 10,
		DarkBlue = 11,
		
		UNKNOWN_1 = 12,

		Violet = 13,
		Teal = 14,
		Gray15 = 15,
		Gray50 = 16,

		UNKNOWN_2 = 16,
		UNKNOWN_3 = 17,
		UNKNOWN_4 = 18,
		UNKNOWN_5 = 19,
		UNKNOWN_6 = 20,
		UNKNOWN_7 = 21,
		UNKNOWN_8 = 22,
		UNKNOWN_9 = 23,
		UNKNOWN_10 = 24,
		UNKNOWN_11 = 25,
		UNKNOWN_12 = 26,
		UNKNOWN_13 = 27,
		UNKNOWN_14 = 28,
		UNKNOWN_15 = 29,
		UNKNOWN_16 = 30,
		UNKNOWN_17 = 31,
		UNKNOWN_18 = 32,
		
		SkyBlue = 33,
		LightTurquoise = 34,
		LightGreen = 35,
		LightYellow = 36,
		PaleBlue = 37,
		Rose = 38,
		Lavender = 39,
		Tan = 40,
		LightBlue = 41,
		Aqua = 42,
		Lime = 43,
		Gold = 44,
		LightOrange = 45,
		Orange = 46,
		BlueGray = 47,
		Gray40 = 48,
		DarkTeal = 49,
		SeaGreen = 50,
		DarkGreen = 51,
		OliveGreen = 52,
		Brown = 53,
		Plum = 54,
		Indigo = 55,
		Gray80 = 56
	}
}
