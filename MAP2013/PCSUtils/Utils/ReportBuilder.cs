using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Data;
using System.Collections.Specialized;
using System.IO;
using C1.C1Report;
using C1.Win.C1Preview;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using BorderStyleEnum = C1.C1Report.BorderStyleEnum;
using Group = C1.C1Report.Group;


namespace PCSUtils.Utils
{	
	/// <summary>
	/// Public interface
	/// External Interface for building dynamic report in C# file.
	/// </summary>
	public interface IDynamicReport
	{
		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		string PCSConnectionString
		{
			get;
			set;
		}		

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		ReportBuilder PCSReportBuilder
		{
			get;
			set;
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
        C1PrintPreviewControl PCSReportViewer
		{
			get;
			set;
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		object Result
		{
			get;
			set;
		}


		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		bool UseReportViewerRenderEngine
		{
			get;
			set;
		}


		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		string ReportDefinitionFolder
		{
			get ;
			set ;
		}

	
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		string ReportLayoutFile
		{
			get ;
			set ;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod">name of the method to call (which declare in the DynamicReport C# file)</param>
		/// <param name="pobjParameters">Array of parameters provide to call the Method with method name = pstrMethod</param>
		/// <returns></returns>
		object Invoke(string pstrMethod, object[] pobjParameters);	        
	}

	/// <summary>
	/// Report Helper class.
	/// </summary>
	[Serializable]
	public class ReportBuilder
	{
		#region PUBLIC REPORT RELATE CONSTANTS

		/// <summary>
		/// MMM
		/// </summary>
		public const string FORMAT_MONTH_3CHAR = "MMM";
		/// <summary>
		/// 00 - 2 digits
		/// </summary>
		public const string FORMAT_DAY_2CHAR = "00";
		/// <summary>
		/// - Seperator between Date,Time, ... Ex: 01-Dec, 22-Jan
		/// </summary>
		public const string SEPERATOR_DATETIME = "-";

		/// <summary>
		/// :  is the sign to seperate several info of a parameter (display on the report layout)
		/// </summary>
		public const string SEPERATOR_PARAMETER_DISPLAY_INFO = ": ";

		/// <summary>
		///  CONVENTION
		/// </summary>
		public const string LABEL_PREFIX = "lbl";
		public const string FIELD_PREFIX = "fld";
		public const string REPORT_QUANTITY_RELATE_FIELD_PREFIX = "_" + FIELD_PREFIX;
		public const string REPORT_TAG_QUANTITY_RELATE = "FORMAT_QUANTITY";


		#endregion

		#region CONSTANTS

		private const string THIS = "PCSUtils.Utils.ReportBuilder";
		

		private const string GROUP_FIELD_PREFIX = "grb";

		private const int LABEL_DISTANCE = 10;
		private const string BOLD = "BOLD";
		private const string ITALIC = "ITALIC";
		private const string STRIKETHROUGH = "STRIKETHROUGH";
		private const string UNDERLINE = "UNDERLINE";
		
		private const int NOT_FOUND = -1;
		private const string FONT_SEPARATOR = "|";
		private const string COLON = ":";
		private const int HEIGHT_MULTIPLIER = 40;
		private const double MARGIN = 50;
		private const double PARAMETER_LABEL_WIDTH = 1260;
		private const int SPACE_BETWEEN_ROW = 45;
		private const int COLON_WIDTH = 90;
		
		private const string FLDTITLE = "fldTitle";

		private const string FONT_NAME_FLD = "FontName";
		private const string FONT_SIZE_FLD = "FontSize";
		private const string FONT_STRIKE_FLD = "FontStrike";
		private const string FONT_BOLD_FLD = "FontBold";
		private const string FONT_ITALIC_FLD = "FontItalic";
		private const string FONT_UNDERLINE_FLD = "FontUnderline";
		
		/// <summary>
		/// Default logo file name
		/// </summary>
		protected const string LOGO_FILE = "\\logo.jpg";

		#endregion CONSTANTS

		#region	PROPERTIES FOR BUILD REPORT "FOR RENDERING" DATATABLE

		protected DataTable mdtbSourceDataTable ;
		public DataTable SourceDataTable
		{
			get
			{
				return mdtbSourceDataTable;
			}
			set
			{
				mdtbSourceDataTable = value;
			}
		}

		
		protected DataTable mdtbRenderDataTable ;
		public DataTable RenderDataTable
		{
			get
			{
				return mdtbRenderDataTable;
			}
			set
			{
				mdtbRenderDataTable = value;
			}
		}

		
		protected string mstrReportName;
		public string ReportName
		{
			get 
			{
				return mstrReportName;
			}
			set
			{
				mstrReportName = value;
			}
		}

	

		#endregion

		#region	PROPERTIES FOR RENDERING REPORT ON PREVIEW CONTROL

		/// <summary>
		/// PCS Setting, if true, we need to ROUND all Quantity-relate field to integer unit.
		/// </summary>
		public static bool NeedToRoundQuantity
		{
			get
			{
			    var roundedQuantity = Utilities.Instance.GetParamValue("RoundedQuantity");
                if (string.IsNullOrEmpty(roundedQuantity))
                {
                    return false;
                }
			    bool needToRound;
			    bool.TryParse(roundedQuantity, out needToRound);
			    return needToRound;
			}
		}


		protected double mdblActualPageWidth;		
		/// <summary>
		/// READONLY: get the real size, actual, render - width of the page where the report lay on
		/// </summary>
		public double ActualPageWidth
		{
			get
			{
				Layout objLayout = mrptReport.Layout;
				mdblActualPageWidth =  objLayout.PageSize.Width - (float)objLayout.MarginLeft - (float)objLayout.MarginRight;
				return mdblActualPageWidth;
			}
		}


		protected bool mblnUseLayoutFile;
		/// <summary>
		/// Get or set this property.
		/// If true, Report Builder will use layout file (specify in ReportFileName)
		/// to render on ReportViewer
		/// </summary>
		public bool UseLayoutFile
		{
			get 
			{
				return mblnUseLayoutFile;
			}
			set
			{
				mblnUseLayoutFile = value;
			}
		}



		protected string mstrReportDefinitionFolder = System.Windows.Forms.Application.CommonAppDataPath;
		public string ReportDefinitionFolder
		{
			get 
			{
				return mstrReportDefinitionFolder;
			}
			set
			{
				mstrReportDefinitionFolder = value;
			}
		}

		protected string mstrReportLayoutFile = "*.xml";
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

		
		protected C1PrintPreviewControl mppvReportViewer;
		public C1PrintPreviewControl ReportViewer
		{
			get 
			{
				return mppvReportViewer;
			}
			set
			{
				mppvReportViewer = value;
			}
		}


		protected C1Report mrptReport = new C1Report();
		public C1Report Report
		{
			get 
			{
				return mrptReport;
			}
			set
			{
				mrptReport = value;
			}
		}


		#endregion

		#region Properties for Report

		protected float fPageSize = 0;

	

		protected string mLogoFile = LOGO_FILE;
		/// <summary>
		/// Sets the Logo file name
		/// </summary>
		public string LogoFile
		{
			set { mLogoFile = value; }
		}

		/// <summary>
		/// Fields list of report
		/// </summary>
		protected ArrayList mFieldList;
		/// <summary>
		/// Parametes with value of report
		/// </summary>
		private ArrayList mParameters;
		/// <summary>
		/// Parameters value from user
		/// </summary>
		private ArrayList mParamValues;
		/// <summary>
		/// Sets Parameters value list
		/// </summary>
		public ArrayList ParamValues
		{
			set { mParamValues = value; }
		}

		/// <summary>
		/// Sets Fields list of report
		/// </summary>
		public ArrayList FieldList
		{
			set { mFieldList = value; }
		}

		/// <summary>
		/// Sets Parametes of report
		/// </summary>
		public ArrayList Parameters
		{
			set { mParameters = value; }
		}

		/// <summary>
		/// Field font format: "Name|Size|FontStyle|GdiCharSet|GdiVerticalFont|GraphicsUnit"
		/// </summary>
		private string[] strFont;
		/// <summary>
		/// Report to draw
		/// </summary>
		private sys_ReportVO mvoReport;
		/// <summary>
		/// Sets Report to draw
		/// </summary>
		public sys_ReportVO ReportVO
		{
			set { mvoReport = value; }
		}

		#endregion

		#region ADAPTER FUNCTIONS TO EXTERNAL	

		/// <summary>
		/// Thachnn: 17/10/2005
		/// Standard empty Constructor
		/// 1. Clear the inner report object
		/// 2. Setup default report style		
		/// </summary>
		public ReportBuilder()
		{
			// initialize control
			mrptReport.Clear();
			mrptReport.ReportName = mstrReportName;


			#region DEFAULT REPORT STYLE
			mrptReport.Font.Name = "Arial Narrow";
			mrptReport.Font.Size = 6.0f;
			mrptReport.Font.Bold = false;
			mrptReport.Font.Italic = false;
			mrptReport.Font.Strikethrough = false;
			mrptReport.Font.Underline = false;
			#endregion DEFAULT REPORT STYLE

			// Initialyze Actual Report Width
			mdblActualPageWidth = ActualPageWidth;
		}

		/// <summary>
		/// Analyse the layout file
		/// </summary>
		/// <returns>true if succeed, false if failure</returns>
		public bool AnalyseLayoutFile()
		{
			bool blnRet = false;

			try
			{
				string[] arrstrReportInDefinitionFile = mrptReport.GetReportInfo( mstrReportDefinitionFolder + "\\" + mstrReportLayoutFile);
				mrptReport.Load(mstrReportDefinitionFolder + "\\" + mstrReportLayoutFile, arrstrReportInDefinitionFile[0]);
				blnRet = true;
			}
			catch	// if test error, we return false
			{
				blnRet = false;
			}			

			return blnRet;
		}

		/// <summary>
		/// /// Thachnn: 17/10/2005
		/// Analyse the Source DataTable, make some changes to easily render the Report.
		/// This function will make change to the RenderDataTable Property.
		/// This function should use only if it is override in the sub class of Report Builder
		/// THROW: Exception
		/// </summary>
		/// <returns>DataTable that will be using (by this ReportBuilder) for rendering on mppvReportViewer object</returns>
		public DataTable MakeDataTableForRender()
		{	
			mdtbRenderDataTable = mdtbSourceDataTable.Copy();
			return mdtbRenderDataTable;
		}
		
		/// <summary>
		/// /// Thachnn: 17/10/2005
		/// Render the Report using DataSource on mppvReportViewer object
		/// </summary>
		public void RenderReport()
		{
			// prevent re-entrant calls
			if (mrptReport.IsBusy)
			{
				return;
			}

			if(mblnUseLayoutFile)
			{
				#region RENDER WITH DEFINITION FILE			

				string[] arrstrReportInDefinitionFile = mrptReport.GetReportInfo( mstrReportDefinitionFolder + "\\" + mstrReportLayoutFile);
				mrptReport.Load(mstrReportDefinitionFolder + "\\" + mstrReportLayoutFile, arrstrReportInDefinitionFile[0]);					
			
				// set datasource object that provides data to report.
				mrptReport.DataSource.Recordset = mdtbRenderDataTable;

				#endregion
			}
			else
			{					
				#region RENDER AUTOMATIC BY CODE BASED ON CONFIGURATION
				// clear old fields first
				mrptReport.Clear();
				// set datasource object that provides data to report.
				mrptReport.DataSource.Recordset = mdtbSourceDataTable;

				// layout report
				LayoutReport();
				// draw report header first
				double dTop = DrawReportHeader(mLogoFile);
				// draw page header
				DrawPageHeader(dTop);
				// draw page footer
				DrawPageFooter();
				// draw report footer
				DrawReportFooter();
				// draw detail
				DrawDetail(mFieldList);
				// draw group by field
				DrawGroupBySection(mFieldList);
				// sorting
				DrawSortSection(mFieldList);

				#endregion
			}

			if( RefreshReport() )	/// apply all change to the report Document
			{
				ReformatNumberInC1Report(mrptReport);

				try
				{
					mppvReportViewer.Document = mrptReport.Document;
				}
				catch{}
			}
		}

		/// <summary>
		/// Thachnn: 
		/// Refresh new change to the PrintPreview control (like re-render, but more faster)
		/// </summary>
		/// <returns>true if succeed, false if failure</returns>
		public bool RefreshReport()
		{
			bool blnRet = false;

			try
			{
				mrptReport.Render();
				blnRet = true;
			}
			catch
			{}

			return blnRet;
		}
		

		#endregion ADAPTER FUNCTIONS TO EXTERNAL	

		#region DRAWING REPORT SECTION - Draw Header, Page Header, Detail, Page Footer, Footer for auto render (without layout file)

		#region Layout report
		/// <summary>
		/// Layout report based on report configuration
		/// </summary>
		public void LayoutReport()
		{
			// Get layout object of report
			Layout objLayout = mrptReport.Layout;
			// paper size
			objLayout.PaperSize = (PaperKind)mvoReport.PaperSize;
			// margin left
			objLayout.MarginLeft = mvoReport.MarginLeft;
			// margin right
			objLayout.MarginRight = mvoReport.MarginRight;
			// margin top
			objLayout.MarginTop = mvoReport.MarginTop;
			// margin bottom
			objLayout.MarginBottom = mvoReport.MarginBottom;
			// gutter in top
			if (mvoReport.MarginGutterPos)
				objLayout.MarginTop += mvoReport.MarginGutter;
			else // gutter in left
				objLayout.MarginLeft += mvoReport.MarginGutter;
			// orientation
			if (mvoReport.Orientation)
				objLayout.Orientation = OrientationEnum.Landscape;
			else
				objLayout.Orientation = OrientationEnum.Portrait;

			// get actual page size
			fPageSize = objLayout.PageSize.Width - (float)objLayout.MarginLeft - (float)objLayout.MarginRight;

			// recalculate the field width
			mFieldList = ReCalcualteFieldWidth(mFieldList);
		}
		#endregion

		#region Report Header
		/// <summary>
		/// Draws report header section
		/// </summary>
		/// <param name="pstrLogoFile">Path to logo image file</param>
		/// <returns>top of next section</returns>
		public double DrawReportHeader(string pstrLogoFile)
		{
			const string FLDLOGO = "fldLogo";
			Section secHeader = GetSectionByType(SectionTypeEnum.PageHeader);
			secHeader.Visible = true;
			secHeader.BackColor = Color.Transparent;
			secHeader.CanGrow = true;
			secHeader.CanShrink = true;

			DataRow drowFont = GetFont(mvoReport.FontReportHeader);

			secHeader.Height = float.Parse(drowFont[FONT_SIZE_FLD].ToString()) * HEIGHT_MULTIPLIER;

			// draw the logo first
			Field fldLogo = DrawReportHeaderLogo(secHeader, FLDLOGO, pstrLogoFile, 0, 0, 1110, 540, PictureAlignEnum.Zoom);
			// draw a line under the logo
			Field fldLine = DrawHorizontalLine(secHeader, "divHeader", 0, fldLogo.Top + fldLogo.Height + SPACE_BETWEEN_ROW,
				fPageSize, 1, BorderStyleEnum.Solid);
			fldLine.LineSlant = LineSlantEnum.NoSlant;
			fldLine.LineWidth = 0;
			fldLine.BorderColor = Color.Black;
			// report title
			Field fldTitle = DrawReportHeaderTitle(secHeader, fldLine.Top + SPACE_BETWEEN_ROW);
			// get the parameter top
			// draw parameter if any
			// HACKED: Thachnn: 30/12/2005: change interface of DrawReportHeaderParameter()
			ArrayList arrDrawnParameters = DrawReportHeaderParameter(secHeader, mParamValues, fldTitle.Top + fldTitle.Height + SPACE_BETWEEN_ROW);
			Field fldLastParam = (Field)arrDrawnParameters[arrDrawnParameters.Count-1];
			// HACKED: Thachnn: 30/12/2005
			return fldLastParam.Top + fldLastParam.Height + SPACE_BETWEEN_ROW;
		}
		
		/// <summary>
		/// Draws header information: Company Name, Address, Tel, Fax, etc..
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="pdTop">Top</param>
		/// <returns>The last field of header information</returns>
		public Field DrawReportHeaderInfo(Section pSection, double pdTop)
		{
			#region Draws Box
			double dLeft = fPageSize - 3715;
			Field fldBox = DrawBox(pSection, dLeft, pdTop, 3715, 1260);
			fldBox.Calculated = false;
			fldBox.BackColor = Color.Transparent;
			fldBox.BorderColor = Color.Black;
			fldBox.BorderStyle = BorderStyleEnum.Solid;
			#endregion

			// update section height
			pSection.Height = fldBox.Height;

			Rectangle rcSize = new Rectangle(Convert.ToInt32(dLeft), Convert.ToInt32(pdTop + LABEL_DISTANCE), 1215, 300);

			#region Company
			// company label
			Field fldInfo = DrawField(pSection, "lblCompany", "Company:", rcSize);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.RightMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginRight = MARGIN;
			// company value
			fldInfo = DrawField(pSection, "fldCompany", SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME),
			                    dLeft + rcSize.Width, rcSize.Y, 2500, rcSize.Height);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.LeftMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginLeft = MARGIN;
			// move to next info
			rcSize.Offset(0, rcSize.Height);

			#endregion

			#region Address
			// address label
			fldInfo = DrawField(pSection, "lblAddress", "Address:", rcSize);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.RightMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginRight = MARGIN;
			// address value
			fldInfo = DrawField(pSection, "fldAddress", SystemProperty.SytemParams.Get(SystemParam.ADDRESS),
				dLeft + rcSize.Width, rcSize.Y, 2500, rcSize.Height);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.LeftMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginLeft = MARGIN;
			// move to next info
			rcSize.Offset(0, rcSize.Height);
			#endregion

			#region Tel
			// tel label
			fldInfo = DrawField(pSection, "lblTel", "Tel:", rcSize);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.RightMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginRight = MARGIN;
			// tel value
			fldInfo = DrawField(pSection, "fldTel", SystemProperty.SytemParams.Get(SystemParam.TEL),
				dLeft + rcSize.Width, rcSize.Y, 2500, rcSize.Height);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.LeftMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginLeft = MARGIN;
			// move to next info
			rcSize.Offset(0, rcSize.Height);
			#endregion

			#region Fax
			// fax label
			fldInfo = DrawField(pSection, "lblFax", "Fax:", rcSize);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.RightMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginRight = MARGIN;
			// fax value
			fldInfo = DrawField(pSection, "fldFax", SystemProperty.SytemParams.Get(SystemParam.FAX),
				dLeft + rcSize.Width, rcSize.Y, 2500, rcSize.Height);
			fldInfo.Calculated = false;
			fldInfo.Align = FieldAlignEnum.LeftMiddle;
			fldInfo.BackColor = Color.Transparent;
			fldInfo.MarginLeft = MARGIN;
			#endregion

			return fldInfo;
		}

		/// <summary>
		/// Draws report title
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		public Field DrawReportHeaderTitle(Section pSection, double pdTop)
		{
			Field objField = new Field();

			objField = DrawField(pSection, FLDTITLE, objField);
			objField.Calculated = false;
			objField.Top = pdTop;
			objField.Left = 0;
			objField.Font.Bold = true;
			objField.Font.Size = 12;
			objField.Font.Name = "Arial Narrow";
			objField.Text = mvoReport.ReportName.ToUpper();
			DataRow drowFont = GetFont(mvoReport.FontReportHeader);
			float fFontSize = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
			objField.Font.Bold = bool.Parse(drowFont[FONT_BOLD_FLD].ToString());
			objField.Align = FieldAlignEnum.LeftMiddle;
			objField.Font.Size = fFontSize;
			objField.Font.Name = drowFont[FONT_NAME_FLD].ToString();
			objField.Width = fPageSize;
			objField.Height = fFontSize * HEIGHT_MULTIPLIER;
			objField.ForeColor = Color.Black;
			objField.CanGrow = true;
			objField.CanShrink = true;
			objField.Anchor = AnchorEnum.Top;

			// update section height
			pSection.Height += objField.Height;

			return objField;
		}

		/// <summary>
		/// Thachnn: 30/12/2005
		/// Draws parameter list (line by line). Ignore empty parameter.
		/// 
		/// 1.. Get all the parameters in which have value to a NameValueCollection
		/// 2.. Draw 'em all by using DrawParameters()
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrParamValues">Parameters list</param>
		/// <param name="pdTop">Top of first parameter</param>
		public ArrayList DrawReportHeaderParameter(Section pSection, ArrayList parrParamValues, double pdTop)
		{
			/// 1.. Get all the parameters in which have value to a NameValueCollection
			NameValueCollection arrParamAndValue = new NameValueCollection();
			double dblX_START_LOCATION = 0;
			double dblY_START_LOCATION =  pdTop;

			foreach (sys_ReportHistoryParaVO voParamValue in parrParamValues)
			{
				// do not draw empty parameter
				if (voParamValue.ParaValue.Trim() == string.Empty)
					continue;
				sys_ReportParaVO voParameter = FindParam(voParamValue.ParaName);
				
				string strParaCaption = 	voParameter.ParaCaption;

				string strParaValue = string.Empty;
                				
				// draw value field
				if (voParameter.FilterField1 != string.Empty)
				{
					strParaValue = voParamValue.FilterField1Value;
					if (voParameter.FilterField2 != string.Empty)
						strParaValue += " (" + voParamValue.FilterField2Value + ")";
				}
				else
				{
					if (voParameter.DataType == (int)TypeCode.DateTime)
					{
						try
						{
							strParaValue = Convert.ToDateTime(voParamValue.ParaValue).ToString(Constants.DATETIME_FORMAT_HOUR);
						}
						catch{}
					}
					else
						strParaValue = voParamValue.ParaValue;
				}
				
				arrParamAndValue.Add(strParaCaption,strParaValue);

				// update section height				
				pSection.Height += LABEL_DEFAULT_HEIGHT + DISTANCE_BETWEEN_CONTROL;
			}

			// 2.. Draw 'em all by using DrawParameters()			
			DataRow drowFont = GetFont(mvoReport.FontParameter);
			ArrayList arrRet = DrawParameters(pSection, dblX_START_LOCATION, dblY_START_LOCATION, arrParamAndValue, drowFont);
			
			if(arrRet.Count == 0)	// nothing was draw
			{
				// add a temp field to the array, because later Drawing Section Function will use the last field in this array to draw next section
				Field fldTemp = new Field();
				fldTemp.Left = dblX_START_LOCATION;
				fldTemp.Top = dblY_START_LOCATION;
				fldTemp.Visible = false;
				arrRet.Add(fldTemp);
			}
			return arrRet;
		}


		/// <summary>
		/// Draws parameter value to template file
		/// </summary>
		/// <param name="pSection"></param>
		/// <param name="parrParamValues"></param>
		/// <returns></returns>
		public void DrawReportHeaderParameter(Section pSection, ArrayList parrParamValues)
		{
			foreach (sys_ReportHistoryParaVO voParamValue in parrParamValues)
			{
				// do not draw empty parameter
				if (voParamValue.ParaValue.Trim() == string.Empty)
					continue;
				sys_ReportParaVO voParameter = FindParam(voParamValue.ParaName);
				try
				{
					Field fldParam = this.mrptReport.Fields[voParameter.ParaName];
					string strValue = string.Empty;
					if (voParamValue.FilterField1Value != null && voParamValue.FilterField1Value.Trim() != string.Empty)
					{
						if (voParamValue.FilterField2Value != null && voParamValue.FilterField2Value.Trim() != string.Empty)
							strValue = voParamValue.FilterField1Value + " (" + voParamValue.FilterField2Value + ")";
						else
							strValue = voParamValue.FilterField1Value;
					}
					else
						strValue = voParamValue.ParaValue;
					fldParam.Text = strValue;
				}
				catch{}
			}
		}

		/// <summary>
		/// Dungla
		/// Draws parameter list
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrParamValues">Parameters list</param>
		/// <param name="pdTop">Top of first parameter</param>
		public Field DrawReportHeaderParameterOLD(Section pSection, ArrayList parrParamValues, double pdTop)
		{
			Field fldData = new Field();
			DataRow drowFont = GetFont(mvoReport.FontParameter);
			float fParameterHeight = float.Parse(drowFont[FONT_SIZE_FLD].ToString()) * HEIGHT_MULTIPLIER;
			Rectangle rcSize = new Rectangle(0, Convert.ToInt32(pdTop), 1260, Convert.ToInt32(fParameterHeight));
			foreach (sys_ReportHistoryParaVO voParamValue in parrParamValues)
			{
				// do not draw empty parameter
				if (voParamValue.ParaValue.Trim() == string.Empty)
					continue;
				sys_ReportParaVO voParameter = FindParam(voParamValue.ParaName);
				// draw label field first
				fldData = DrawField(pSection, LABEL_PREFIX + voParamValue.ParaName, voParameter.ParaCaption, rcSize);
				fldData.Align = FieldAlignEnum.LeftMiddle;
				fldData.Calculated = false;
				fldData.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
				fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
				fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
				fldData.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
				fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
				fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
				fldData.ForeColor = Color.Black;
				fldData.BackColor = Color.Transparent;
				fldData.MarginLeft = MARGIN;
				//fldData.BorderColor = Color.Transparent;
				fldData.BorderStyle = BorderStyleEnum.Transparent;

				// move to colon field
				rcSize.Offset(rcSize.Width, 0);
				rcSize.Width = COLON_WIDTH;
				// draw the colon field
				fldData = DrawField(pSection, LABEL_PREFIX + voParamValue.ParaName + "sep", 
					":", rcSize);
				fldData.Align = FieldAlignEnum.LeftMiddle;
				fldData.Calculated = false;
				fldData.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
				fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
				fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
				fldData.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
				fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
				fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
				fldData.SetZOrder(false);
				fldData.ForeColor = Color.Black;
				fldData.BackColor = Color.Transparent;
				fldData.MarginLeft = MARGIN;
				fldData.BorderStyle = BorderStyleEnum.Transparent;
				// move to value field
				rcSize.Offset(rcSize.Width, 0);
				// sets new width
				rcSize.Width = Convert.ToInt32(fPageSize - PARAMETER_LABEL_WIDTH - COLON_WIDTH);
				// draw value field
				if (voParameter.FilterField1 != string.Empty)
				{
					if (voParameter.FilterField2 != string.Empty)
					{
						string strValue = string.Empty;
						if (voParamValue.FilterField1Value.Trim() != string.Empty)
						{
							strValue += voParamValue.FilterField1Value.Trim();
							if (voParamValue.FilterField2Value.Trim() != string.Empty)
								strValue += " - " + voParamValue.FilterField2Value.Trim();
						}
						else if (voParamValue.FilterField2Value.Trim() != string.Empty)
							strValue += voParamValue.FilterField2Value.Trim();
						// draw filter field 1 and filter field 2 value
						fldData = DrawField(pSection, FIELD_PREFIX + voParamValue.ParaName, 
							strValue, rcSize);
					}
					else
					{
						// draw filter field 1 value
						fldData = DrawField(pSection, FIELD_PREFIX + voParamValue.ParaName, 
							voParamValue.FilterField1Value, rcSize);
					}
				}
				else
				{
					fldData = DrawField(pSection, FIELD_PREFIX + voParamValue.ParaName, voParamValue.ParaValue, rcSize);
				}
				fldData.Align = FieldAlignEnum.LeftMiddle;
				fldData.Calculated = false;
				fldData.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
				fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
				fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
				fldData.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
				fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
				fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
				fldData.SetZOrder(false);
				fldData.ForeColor = Color.Black;
				fldData.BackColor = Color.Transparent;
				fldData.MarginLeft = MARGIN;
				fldData.BorderStyle = BorderStyleEnum.Transparent;
				// update section height
				pSection.Height += rcSize.Height;
				// reset width for new parameter
				rcSize.Width = 1260;
				// reset left for new parameter
				rcSize.X = 0;
				// move to next parameter
				rcSize.Offset(0, rcSize.Height);
			}
			return fldData;
		}


		/// <summary>
		/// Draws all fields which is summary on top of report
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrFields">List of Report Fields</param>
		/// <param name="pdTop">Top of field</param>
		public void DrawReportHeaderSumTop(Section pSection, ArrayList parrFields, double pdTop)
		{
			Field fldData = new Field();
			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, Convert.ToInt32(pdTop), 1000, 300);
			
			bool blnHasField = false;

			DataRow drowFont = GetFont(mvoReport.FontPageHeader);
			for (int i = 0; i < parrFields.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)parrFields[i];

				// if field width greater than 1 then display, else hide it.
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					// reset field width
					rcSize.Width = voReportField.Width;
					fldData = DrawField(pSection, LABEL_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
						"=Sum(" + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET + ")",
						rcSize);
				
					fldData.Align = FieldAlignEnum.RightMiddle;
					fldData.Anchor = AnchorEnum.Top;
					fldData.Calculated = true;
					fldData.CanGrow = true;
					fldData.CanShrink = true;
					fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldData.Font.Size = 9;
					fldData.Font.Bold = true;
					fldData.Format = voReportField.Format;
					fldData.Visible = voReportField.SumTopReport;
					
					if (voReportField.SumTopReport)
						blnHasField = true;

					// move on to next field
					rcSize.Offset(rcSize.Width, 0);
				}
			}
			// update section height
			if (blnHasField)
				pSection.Height += fldData.Height;
		}

		/// <summary>
		/// Draws report header log
		/// </summary>
		/// <param name="psectionReportHeader">Header Section</param>
		/// <param name="pstrName">Name of Field</param>
		/// <param name="pstrImageFilePath">Image path</param>
		/// <param name="pdbLeft">Left</param>
		/// <param name="pdbTop">Top</param>
		/// <param name="pdbWidth">Width</param>
		/// <param name="pdbHeight">Height</param>
		/// <param name="pPictureAlign">Alignment</param>
		/// <returns>true if succeed, false if failure</returns>
		public Field DrawReportHeaderLogo(Section psectionReportHeader, string pstrName, string pstrImageFilePath, double pdbLeft, double pdbTop, double pdbWidth, double pdbHeight, PictureAlignEnum pPictureAlign)
		{
			Field objFld = new Field();
			objFld = DrawField(psectionReportHeader, pstrName, objFld);

			objFld.Left = pdbLeft;
			objFld.Calculated = false;
			objFld.Top = pdbTop;
			objFld.Width = pdbWidth;
			objFld.Height = pdbHeight;
			objFld.PictureAlign = pPictureAlign;
			objFld.Anchor = AnchorEnum.Top;
			objFld.SetZOrder(true);
			objFld.ZOrder = 100;
			objFld.Visible = true;
			if(File.Exists(pstrImageFilePath))
			{	
				Image objImg = Image.FromFile(pstrImageFilePath);
				objFld.Picture = objImg;
			}
			else
				objFld.Picture = null;

			Field fldCompany = DrawField(psectionReportHeader, "fldCompany", SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME).ToUpper(),
				objFld.Left + objFld.Width + SPACE_BETWEEN_ROW, objFld.Top, 2500, objFld.Height);

			fldCompany.Anchor = AnchorEnum.Top;			
			fldCompany.CanGrow = false;
			fldCompany.CanShrink = false;
			fldCompany.Calculated = false;
			fldCompany.Align = FieldAlignEnum.LeftBottom;
			fldCompany.BackColor = Color.Transparent;
			fldCompany.BorderStyle = BorderStyleEnum.Transparent;
			fldCompany.MarginLeft = MARGIN;

			return objFld;
		}
		#endregion

		#region Page Header

		/// <summary>
		/// Draws page header section
		/// </summary>
		/// <returns>True if succeed | False if failure</returns>
		public bool DrawPageHeader(double pdTop)
		{
			bool blnRet = false;
			const string PAGEHEADER = "PageHeader";			
			Section secPageHeader = GetSectionByName(PAGEHEADER);
			secPageHeader.Visible = true;
			secPageHeader.CanGrow = true;
			secPageHeader.CanShrink = true;
			secPageHeader.Height = 0;

			Field fldData = new Field();

			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, Convert.ToInt32(pdTop), 1000, 350);
			DataRow drowFont = GetFont(mvoReport.FontPageHeader);
			// draw the first line
			int intIndex = 0;
			for (int i = 0; i < mFieldList.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)mFieldList[i];
				// if field width greater than 1 then display, else hide it.
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					// reset field width
					rcSize.Width = voReportField.Width;
					fldData = DrawField(secPageHeader, LABEL_PREFIX + voReportField.FieldName, voReportField.FieldCaption, rcSize);
					fldData.Align = FieldAlignEnum.CenterMiddle;
					fldData.Anchor = AnchorEnum.Top;
					fldData.Calculated = false;
					fldData.CanGrow = true;
					fldData.CanShrink = false;
					fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldData.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
					fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
					fldData.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
					fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
					fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
					fldData.BackColor = Color.Transparent;
					fldData.BorderStyle = BorderStyleEnum.Transparent;
					if (mvoReport.TableBorder == (int)TableBoderEnum.Vertical ||
						mvoReport.TableBorder == (int)TableBoderEnum.Both)
					{
						fldData.BorderStyle = BorderStyleEnum.Solid;
						fldData.BorderColor = Color.Black;
					}
					
					// move to next field
					rcSize.Offset(rcSize.Width, 0);
					intIndex ++;
				}
			}
			if (mvoReport.TableBorder == (int)TableBoderEnum.None)
			{
				Field fldLine = DrawHorizontalLine(secPageHeader, "divBottomHeader",
				                                   0, fldData.Top + fldData.Height, fPageSize, 1, BorderStyleEnum.Solid);
				fldLine.LineSlant = LineSlantEnum.NoSlant;
				fldLine.BorderColor = Color.Black;
				fldLine.LineWidth = 0;
			}
			// draw sum top page
			DrawPageHeaderSumTop(secPageHeader, mFieldList, rcSize.Height);
			return blnRet;
		}
		/// <summary>
		/// Draws the line below the page header
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		public void DrawPageHeaderBelowLine(Section pSection)
		{
		}

		/// <summary>
		/// Draws all fields which is summary on top of page
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrFields">List of Report Fields</param>
		/// <param name="pdTop">Top of field</param>
		public void DrawPageHeaderSumTop(Section pSection, ArrayList parrFields, double pdTop)
		{
			Field fldData = new Field();
			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, Convert.ToInt32(pdTop), 1000, 300);
			
			DataRow drowFont = GetFont(mvoReport.FontPageHeader);
			rcSize.Height = Convert.ToInt32(float.Parse(drowFont[FONT_SIZE_FLD].ToString()) * HEIGHT_MULTIPLIER);

			bool blnHasField = false;
			for (int i = 0; i < parrFields.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)parrFields[i];

				// if field width greater than 1 then display, else hide it.
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					rcSize.Width = voReportField.Width;
					fldData = DrawField(pSection, LABEL_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
						"=Sum(" + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET + ")",
						rcSize);
					
					fldData.Align = FieldAlignEnum.RightMiddle;
					fldData.Calculated = true;
					fldData.Anchor = AnchorEnum.Top;
					fldData.CanGrow = true;
					fldData.CanShrink = true;
					fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldData.Font.Size = 9;
					fldData.Font.Bold = true;
					fldData.Format = voReportField.Format;
					fldData.Visible = voReportField.SumTopPage;

					if (voReportField.SumTopPage)
						blnHasField = true;

					// move on to next field
					rcSize.Offset(rcSize.Width, 0);
				}
			}
			// update section height
			if (blnHasField)
				pSection.Height += fldData.Height;
		}

		#endregion

		#region Detail and Group section
		/// <summary>
		/// Draws the detail section of report
		/// </summary>
		/// <param name="parrFields">List of fields to be draw</param>
		public void DrawDetail(ArrayList parrFields)
		{
			// get section
			Section secDetail = GetSectionByType(SectionTypeEnum.Detail);
			// make new section visible
			secDetail.Visible = true;
			secDetail.BackColor = Color.Transparent;
			secDetail.CanGrow = true;
			secDetail.CanShrink = true;
			secDetail.Height = 300;
			
			// create new field
			Field fldDetail = new Field();
			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, 0, 1000, 350);
			// get default font from page setup
			DataRow drowFont = GetFont(mvoReport.FontDetail);
			// draw the first vertical line if necessary
			if (mvoReport.TableBorder == (int)TableBoderEnum.Vertical ||
				mvoReport.TableBorder == (int)TableBoderEnum.Both)
			{
				// draw one more vertical line at the BEGIN of first field
				Field fldVLine = DrawVerticalLine(secDetail, "vLineBegin", 0, 0, rcSize.Height, 1, BorderStyleEnum.Solid);
				fldVLine.SetZOrder(true);
			}
			for (int i = 0; i < parrFields.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)parrFields[i];
				if (voReportField.Visisble && !voReportField.GroupBy && voReportField.Width > 1)
				{
					// reset field width
					rcSize.Width = voReportField.Width;
					// get font from field setup
					if (voReportField.Font != string.Empty)
						drowFont = GetFont(voReportField.Font);
					
					fldDetail = DrawField(secDetail, FIELD_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
						Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET, rcSize);
					
					fldDetail.Calculated = true;
					fldDetail.WordWrap = true;
					fldDetail.CanGrow = true;
					fldDetail.CanShrink = false;
					fldDetail.Anchor = AnchorEnum.Top;
					if (voReportField.Font != null && voReportField.Font.Trim() != string.Empty)
						drowFont = GetFont(voReportField.Font);
					fldDetail.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldDetail.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
					fldDetail.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
					fldDetail.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
					fldDetail.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
					fldDetail.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
					fldDetail.Format = voReportField.Format;
					fldDetail.BorderStyle = BorderStyleEnum.Transparent;
					fldDetail.BackColor = Color.Transparent;

					// field alignment
					switch (voReportField.Align)
					{
						case PCSAligmentType.LEFT:
							fldDetail.Align = FieldAlignEnum.LeftMiddle;
							fldDetail.MarginLeft = MARGIN;
							break;
						case PCSAligmentType.CENTER:
							fldDetail.Align = FieldAlignEnum.CenterMiddle;
							break;
						case PCSAligmentType.RIGHT:
							fldDetail.Align = FieldAlignEnum.RightMiddle;
							fldDetail.MarginRight = MARGIN;
							break;
						default:
							fldDetail = AlignField(fldDetail, (TypeCode)voReportField.DataType);
							break;
					}

					if (voReportField.DataType == (int)TypeCode.Boolean)
						fldDetail.CheckBox = CheckBoxEnum.CheckBox;
					// change the display type of field based on configuration
					switch (voReportField.Type)
					{
						case (int)FieldTypeEnum.BarChart:
							// draw the max value first
							Field fldMax = DrawMaxField(secDetail, voReportField.FieldName, rcSize, false);
							// draw the bar
							DrawChartField(secDetail, voReportField.FieldName, fldDetail, fldMax,  rcSize);
							// hide the data field
							fldDetail.Visible = false;
							break;
						case (int)FieldTypeEnum.RecordCounter:
							// field will not get data from datasource
							fldDetail.Calculated = false;
							// start from 1
							fldDetail.Text = "1";
							// turn on the running sum property of current field
							fldDetail.RunningSum = RunningSumEnum.SumOverGroup;
							break;
					}
					// draw table
					switch (mvoReport.TableBorder)
					{
						case (int)TableBoderEnum.Both:
						case (int)TableBoderEnum.Vertical:
							// draw the vertical line at right of field
							Field fldVLine = DrawVerticalLine(secDetail, "vLine" + i.ToString(), fldDetail.Left + fldDetail.Width, 0, rcSize.Height, 1, BorderStyleEnum.Solid);
							fldVLine.SetZOrder(true);
							break;
					}
					// move on to next field
					rcSize.Offset(rcSize.Width, 0);
				} // if (!voReportField.GroupBy)
			} // for (int i = 0; i < parrFields.Count; i++)
			if (mvoReport.TableBorder == (int)TableBoderEnum.Horizontal || 
				mvoReport.TableBorder == (int)TableBoderEnum.Both)
			{
				// draw the horizontal line at bottom of field
				Field fldHLine = DrawHorizontalLine(secDetail, "hLine", 0, fldDetail.Top + fldDetail.Height, fPageSize, 1, BorderStyleEnum.Solid);
				fldHLine.SetZOrder(true);
			}
		}
		/// <summary>
		/// Draws all Sort fields
		/// </summary>
		/// <param name="marrFieldList">List of fields</param>
		public void DrawSortSection(ArrayList marrFieldList)
		{
			for (int i = 0; i < marrFieldList.Count; i++)
			{
				sys_ReportFieldsVO voReportField = (sys_ReportFieldsVO)marrFieldList[i];
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					if (voReportField.Sort != PCSSortType.NONE)
					{
						mrptReport.Groups.Add(GROUP_FIELD_PREFIX + "[" + voReportField.FieldName + "]", voReportField.FieldName, (SortEnum)voReportField.Sort);
						mrptReport.Groups[GROUP_FIELD_PREFIX + "[" + voReportField.FieldName + "]"].SectionHeader.Visible = false;
						mrptReport.Groups[GROUP_FIELD_PREFIX + "[" + voReportField.FieldName + "]"].SectionFooter.Visible = false;
						mrptReport.Groups[GROUP_FIELD_PREFIX + "[" + voReportField.FieldName + "]"].KeepTogether = KeepTogetherEnum.KeepNothing;
					}
				}
			}
		}
		/// <summary>
		/// Sort a group based on Sort Type
		/// </summary>
		/// <param name="pgGroupToSort">Group To Sort</param>
		/// <param name="pintSortType">Sort Type</param>
		/// <returns>Sorted Group</returns>
		public Group SortGroup(Group pgGroupToSort, int pintSortType)
		{
			pgGroupToSort.Sort = (SortEnum)pintSortType;
			return pgGroupToSort;
		}
		/// <summary>
		/// Draws a Group to report
		/// </summary>
		/// <param name="marrFieldList">Field List to be draw as group</param>
		/// <returns>New Group</returns>
		public void DrawGroupBySection(ArrayList marrFieldList)
		{
			DataRow drowFont = GetFont(mvoReport.FontGroupBy);
			float fGroupBySize = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
			const double GROUP_TAB = 350;
			const double GROUP_HEIGHT = 450;
			int intLastGroupIndex = -1;
			for (int i = 0; i < marrFieldList.Count; i++)
			{
				sys_ReportFieldsVO voField = (sys_ReportFieldsVO)marrFieldList[i];
				if (voField.GroupBy && voField.Width > 1)
					intLastGroupIndex = i;
			}
			int intIndex = 0;
			for (int i = 0; i < marrFieldList.Count; i++)
			{
				sys_ReportFieldsVO voField = (sys_ReportFieldsVO)marrFieldList[i];
				if (voField.GroupBy && voField.Width > 1)
				{
					// add new group
					Group gData = mrptReport.Groups.Add(GROUP_FIELD_PREFIX + voField.FieldName, voField.FieldName, SortEnum.NoSort);
					// sorting group
					if (voField.Sort > 0)
						gData = SortGroup(gData, voField.Sort);
					else
						gData.Sort = SortEnum.Ascending;
					// group header visible
					gData.SectionHeader.Visible = true;
					gData.SectionHeader.Repeat = true;
					gData.SectionHeader.CanGrow = true;
					gData.SectionHeader.CanShrink = true;
					gData.SectionHeader.Height = GROUP_HEIGHT;
					// add group header field
					Field fldGroup = DrawField(gData.SectionHeader, "fldHdr[" + voField.FieldName + "]", "\"" + voField.FieldName + ": \" & [" + voField.FieldName + "]", 
						intIndex * GROUP_TAB, 0, fPageSize - (intIndex * GROUP_TAB), gData.SectionHeader.Height);
					fldGroup.Calculated = true;
					fldGroup.WordWrap = true;
					fldGroup.CanGrow = true;
					fldGroup.CanShrink = false;
					fldGroup.Anchor = AnchorEnum.Top;
					fldGroup.BorderStyle = BorderStyleEnum.Solid;
					fldGroup.BorderColor = Color.DarkGray;
					fldGroup.BackColor = Color.Transparent;
					fldGroup.Align = FieldAlignEnum.LeftMiddle;
					fldGroup.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldGroup.Font.Size = fGroupBySize;
					fldGroup.Font.Strikethrough = bool.Parse(drowFont[FONT_STRIKE_FLD].ToString());
					fldGroup.Font.Bold = bool.Parse(drowFont[FONT_BOLD_FLD].ToString());
					fldGroup.Font.Italic = bool.Parse(drowFont[FONT_ITALIC_FLD].ToString());
					fldGroup.Font.Underline = bool.Parse(drowFont[FONT_UNDERLINE_FLD].ToString());
					fldGroup.MarginLeft = MARGIN;

					// if current group is the last group, draw a line to fill the blank
					if (i == intLastGroupIndex)
					{
						Field fldLine = DrawHorizontalLine(gData.SectionHeader, "divGroup" + i, 0, gData.SectionHeader.Height, fPageSize - fldGroup.Width, 1, BorderStyleEnum.Solid);
						fldLine.BorderColor = Color.DarkGray;
					}
					// group footer invisible
					gData.SectionFooter.Visible = false;
					if (voField.BottomGroup)
					{
						// display group footer
						gData.SectionFooter.Visible = true;
						gData.SectionFooter.Repeat = true;
						gData.SectionFooter.CanGrow = true;
						gData.SectionFooter.CanShrink = true;
						//gData.SectionFooter.BackColor = Color.FromArgb(150, 150, 220);
						gData.SectionFooter.Height = gData.SectionHeader.Height;
						// add group footer field
						fldGroup = DrawField(gData.SectionFooter, "fldFtr[" + voField.FieldName + "]", string.Empty, 
							intIndex * GROUP_TAB, 0, fPageSize - (intIndex * GROUP_TAB), gData.SectionFooter.Height);
						fldGroup.Calculated = false;
						fldGroup.WordWrap = true;
						fldGroup.CanGrow = true;
						fldGroup.CanShrink = false;
						fldGroup.Anchor = AnchorEnum.Top;
						fldGroup.BorderStyle = BorderStyleEnum.Solid;
						fldGroup.BorderColor = Color.DarkGray;//Color.FromArgb(0, 0, 150);
						fldGroup.BackColor = Color.Transparent;//Color.FromArgb(150, 150, 220);
						fldGroup.Align = FieldAlignEnum.LeftMiddle;
						sys_ReportFieldsVO voReportField = null;
						// use to draw more than one field
						Rectangle rcSize = new Rectangle(Convert.ToInt32(fldGroup.Left), 0, 1000, 450);
			
						for (int j = 0; j < marrFieldList.Count; j++)
						{
							voReportField = (sys_ReportFieldsVO)marrFieldList[j];

							// if field width greater than 1 then display, else hide it.
							if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
							{
								// reset field width
								rcSize.Width = voReportField.Width;
								fldGroup = DrawField(gData.SectionFooter, LABEL_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
									Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET, rcSize);
				
								fldGroup.Align = FieldAlignEnum.RightMiddle;
								fldGroup.Calculated = true;
								fldGroup.WordWrap = true;
								fldGroup.CanGrow = true;
								fldGroup.CanShrink = false;
								fldGroup.Font.Name = drowFont[FONT_NAME_FLD].ToString();
								fldGroup.Font.Size = fGroupBySize;
								fldGroup.Font.Strikethrough = bool.Parse(drowFont[FONT_STRIKE_FLD].ToString());
								fldGroup.Font.Bold = bool.Parse(drowFont[FONT_BOLD_FLD].ToString());
								fldGroup.Font.Italic = bool.Parse(drowFont[FONT_ITALIC_FLD].ToString());
								fldGroup.Font.Underline = bool.Parse(drowFont[FONT_UNDERLINE_FLD].ToString());
								fldGroup.Format = voReportField.Format;
								fldGroup.BackColor = Color.Transparent;
								bool blnVisible = false;
								if (voReportField.DataType != (int)TypeCode.Boolean &&
									voReportField.DataType != (int)TypeCode.String &&
									voReportField.DataType != (int)TypeCode.DateTime &&
									voReportField.DataType != (int)TypeCode.Char &&
									!voReportField.Type.Equals((int)FieldTypeEnum.RecordCounter))
									blnVisible = true;

								fldGroup.Visible = blnVisible;

								if (blnVisible)
									fldGroup.Text = "=Sum(" + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET + ")";
					
								// move on to next field
								rcSize.Offset(rcSize.Width, 0);
							}
						}
					}
					// increase the index
					intIndex++;
				}
			}
		}

		#endregion

		#region Page Footer
		/// <summary>
		/// Draws the Page footer section
		/// </summary>
		/// <returns>true if succeed, false if failure</returns>
		public bool DrawPageFooter()
		{
			bool blnRet = false;
			try
			{
				const string PAGEFOOTER = "PageFooter";
				Section secPageFooter = GetSectionByName(PAGEFOOTER);
				secPageFooter.Visible = true;
				secPageFooter.Height = 0;

				// draw sum bottom of page fields first
				DrawPageFooterSumBottom(secPageFooter, mFieldList);

				Field fldFooter = DrawHorizontalLine(secPageFooter, "divFooter", 0, 0, fPageSize, 1, BorderStyleEnum.Solid);
				fldFooter.BorderColor = Color.Black;
			
				DrawPageFooterPrintOn(secPageFooter, 0);
				DrawPageFooterPageNumber(secPageFooter, 0);

				// update section height
				secPageFooter.Height += 300;

				blnRet = true;
			}
			catch
			{
                blnRet = false;
			}
			
			return blnRet;
		}
		/// <summary>
		/// Draws the printed date/time
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="pdTop">Top of label</param>
		public void DrawPageFooterPrintOn(Section pSection, double pdTop)
		{
			const string FLDPRINTON = "fldPrintOn";
			string strText = "\"" + mvoReport.ReportName.ToUpper() + " Printed on: \" + Format(Now(),\"dd-MM-yyyy\") + \"   \" + Format(Now(),\"Long Time\")";
					
			Field objField = new Field();
			objField = DrawField(pSection, FLDPRINTON, objField);
			objField.Calculated = false;
			objField.Align = FieldAlignEnum.LeftBottom;
			objField.Text = strText;
			objField.Left = 0;
			objField.Top = pdTop;
			objField.Width = fPageSize / 2;
			objField.Height = 300;
			objField.CanGrow = true;
			objField.Calculated = true;
			
		}
		/// <summary>
		/// Draws the Page Number
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="pdTop">Top of label</param>
		public void DrawPageFooterPageNumber(Section pSection, double pdTop)
		{
			const string FLDPAGENUMBER = "fldPageNumber";
			const string strText = "\"Page \" & [Page] & \"/\" & [Pages]";
			Field objField = new Field();
			objField = DrawField(pSection,FLDPAGENUMBER,objField);
			objField.Calculated = false;
			objField.Align = FieldAlignEnum.RightBottom;
			objField.Text = strText;
			objField.Width = fPageSize / 2;
			objField.Left = fPageSize - objField.Width;
			objField.Top = pdTop;
			objField.Height = 300;
			objField.CanGrow = true;
			objField.Calculated = true;		

			/// free variables
			objField = null;
		}

		/// <summary>
		/// Draws all field which is summary on bottom of page
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrFields">List of Report Fields</param>
		public void DrawPageFooterSumBottom(Section pSection, ArrayList parrFields)
		{
			Field fldData = new Field();
			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, 0, 1000, 300);
			
			// get the configuration font
			DataRow drowFont = GetFont(mvoReport.FontPageFooter);
			rcSize.Height = Convert.ToInt32(float.Parse(drowFont[FONT_SIZE_FLD].ToString()) * HEIGHT_MULTIPLIER);
			bool blnHasField = false;
			for (int i = 0; i < parrFields.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)parrFields[i];

				// if field width greater than 1 then display, else hide it.
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					rcSize.Width = voReportField.Width;
					fldData = DrawField(pSection, FIELD_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
						"=Sum(" + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET + ")",
						rcSize);
					
					fldData.Align = FieldAlignEnum.RightMiddle;
					fldData.CanGrow = true;
					fldData.CanShrink = true;
					//fldData.Calculated = true;
					fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldData.Font.Size = 9;
					//fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
					fldData.Font.Bold = true; //Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
					//fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
					//fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
					fldData.Format = voReportField.Format;
					fldData.Visible = voReportField.SumBottomPage;
				
					if (voReportField.SumBottomPage)
						blnHasField = true;

					// move on to next field
					rcSize.Offset(rcSize.Width, 0);
				}
			}

			// update section height
			if (blnHasField)
				pSection.Height += fldData.Height;
		}

		#endregion

		#region Report Footer
		/// <summary>
		/// Draws the Report footer section
		/// </summary>
		public void DrawReportFooter()
		{
			const string REPORTFOOTER = "Footer";
			Section secReportFooter = GetSectionByName(REPORTFOOTER);
			secReportFooter.Visible = true;
			// draw sum bottom field first
			DrawReportFooterSumBottom(secReportFooter, mFieldList);
			// draw the signature
			DrawReportFooterSignature(secReportFooter, mvoReport.Signatures, secReportFooter.Height);

		}
		/// <summary>
		/// Draws the signatures to report footer
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="pdTop">Top of signature</param>
		/// <param name="pstrSignature">Signatures</param>
		public void DrawReportFooterSignature(Section pSection, string pstrSignature, double pdTop)
		{
			const string METHOD_NAME = THIS + ".DrawReportFooterSignature()";
			const string FLDSIGNATURE_UPPER = "fldSignatureUpper";
			const string FLDSIGNATURE_LOWER = "fldSignatureLower";
			if (pstrSignature.Trim() == string.Empty)
				return;
			// get list of signature first
			ArrayList arrSignatures = GetSignatures(pstrSignature);
			// width of each signature
			double dWidth = (double)fPageSize / (double)arrSignatures.Count;
			Rectangle rcSize = new Rectangle(0, Convert.ToInt32(pdTop), Convert.ToInt32(dWidth), 300);
			Field fldSign = new Field();
			// each signature will have two field in report footer
			// draw the uppert part
			for (int i = 0; i < arrSignatures.Count; i++)
			{
				string[] strUpper = arrSignatures[i].ToString().Split(Constants.VIEW_TABLE_ITEM_SEPARATOR);
				if (strUpper.Length < 2)
					throw new PCSBOException(ErrorCode.MESSAGE_INVALID_FORMAT, METHOD_NAME, new Exception());
				fldSign = DrawField(pSection, FLDSIGNATURE_UPPER + i.ToString(), strUpper[0], rcSize);
				fldSign.Font.Bold = true;
				fldSign.CanGrow = true;
				fldSign.CanShrink = true;
				fldSign.Align = FieldAlignEnum.CenterBottom;
				// move to next field
				rcSize.Offset(rcSize.Width, 0);
			}
			// draw the lower part of signature below the upper part
			rcSize = new Rectangle(0, Convert.ToInt32(fldSign.Top + fldSign.Height), Convert.ToInt32(dWidth), 300);
			for (int i = 0; i < arrSignatures.Count; i++)
			{
				string[] strLower = arrSignatures[i].ToString().Split(Constants.VIEW_TABLE_ITEM_SEPARATOR);
				if (strLower.Length < 2)
					throw new PCSBOException(ErrorCode.MESSAGE_INVALID_FORMAT, METHOD_NAME, new Exception());
				fldSign = DrawField(pSection, FLDSIGNATURE_LOWER + i.ToString(), strLower[1], rcSize);
				fldSign.Font.Bold = true;
				fldSign.CanGrow = true;
				fldSign.CanShrink = true;
				fldSign.Align = FieldAlignEnum.CenterTop;
				fldSign.Font.Size = 8;
				// move to next field
				rcSize.Offset(rcSize.Width, 0);
			}
			// update section height
			pSection.Height += rcSize.Height * 2;
		}

		/// <summary>
		/// Draws all fields which is summary on bottom of report
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="parrFields">List of Report Fields</param>
		public void DrawReportFooterSumBottom(Section pSection, ArrayList parrFields)
		{
			Field fldData = new Field();
			sys_ReportFieldsVO voReportField = null;
			// use to draw more than one field
			Rectangle rcSize = new Rectangle(0, 0, 1000, 300);
			
			// get font from configuration
			DataRow drowFont = GetFont(mvoReport.FontPageFooter);
			rcSize.Height = Convert.ToInt32(float.Parse(drowFont[FONT_SIZE_FLD].ToString()) * HEIGHT_MULTIPLIER);
			bool blnHasField = false;
			for (int i = 0; i < parrFields.Count; i++)
			{
				voReportField = (sys_ReportFieldsVO)parrFields[i];

				// if field width greater than 1 then display, else hide it.
				if (voReportField.Visisble && voReportField.Width > 1 && !voReportField.GroupBy)
				{
					rcSize.Width = voReportField.Width;
					fldData = DrawField(pSection, FIELD_PREFIX + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET,
						"=Sum(" + Constants.OPEN_SBRACKET + voReportField.FieldName + Constants.CLOSE_SBRACKET + ")",
						rcSize);
				
					fldData.Align = FieldAlignEnum.RightMiddle;
					fldData.Font.Name = drowFont[FONT_NAME_FLD].ToString();
					fldData.Font.Size = float.Parse(drowFont[FONT_SIZE_FLD].ToString());
					fldData.Font.Strikethrough = Convert.ToBoolean(drowFont[FONT_STRIKE_FLD].ToString());
					fldData.Font.Bold = Convert.ToBoolean(drowFont[FONT_BOLD_FLD].ToString());
					fldData.Font.Italic = Convert.ToBoolean(drowFont[FONT_ITALIC_FLD].ToString());
					fldData.Font.Underline = Convert.ToBoolean(drowFont[FONT_UNDERLINE_FLD].ToString());
					fldData.Format = voReportField.Format;
					fldData.Visible = voReportField.SumBottomReport;

					if (voReportField.SumBottomReport)
						blnHasField = true;

					// move on to next field
					rcSize.Offset(rcSize.Width, 0);
				}
			}

			// update section height
			if (blnHasField)
				pSection.Height += fldData.Height;
		}

		#endregion

		#endregion

		#region PRIVATE UTIL FUNCTIONS
		
		/// <summary>
		/// Thachnn: 14/Oct/2005
		/// Merge multi DataTables (have same schema) from a dataset into only one DataTable
		/// </summary>
		/// <param name="pdst">DataSet contain table(s) to merge</param>
		/// <returns>result DataTable if OK, empty DataTable if fail or error</returns>
		private  DataTable MergeManyDataTablesIntoOne(DataSet pdst)
		{
			DataTable dtbRet = new DataTable();
			try
			{			
				if (pdst.Tables.Count > 0)
				{
					dtbRet = pdst.Tables[0].Clone();

					foreach (DataTable table in pdst.Tables)
					{
						/// Don't change this method to Rows.AddRange please. Thachnn.
						foreach (DataRow drow in table.Rows)
						{
							dtbRet.ImportRow(drow);
						}
					}
				}
			}
			catch //(Exception ex)
			{
				// throw ex;
			}
			return dtbRet;
		}

		/// <summary>
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private  ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if( !arrRet.Contains(objGet)  )
					{
						arrRet.Add(objGet);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

		/// <summary>
		/// Gets font from PCS font string format
		/// </summary>
		/// <param name="pstrFontString">Font to get</param>
		/// <returns>DataRow</returns>
		private DataRow GetFont(string pstrFontString)
		{
			DataTable dtbFont = new DataTable();
			dtbFont.Columns.Add(new DataColumn(FONT_NAME_FLD, typeof(string)));
			dtbFont.Columns.Add(new DataColumn(FONT_SIZE_FLD, typeof(float)));
			dtbFont.Columns.Add(new DataColumn(FONT_STRIKE_FLD, typeof(bool)));
			dtbFont.Columns.Add(new DataColumn(FONT_BOLD_FLD, typeof(bool)));
			dtbFont.Columns.Add(new DataColumn(FONT_ITALIC_FLD, typeof(bool)));
			dtbFont.Columns.Add(new DataColumn(FONT_UNDERLINE_FLD, typeof(bool)));
            
			DataRow drowResult = dtbFont.NewRow();
			strFont = pstrFontString.Split(FONT_SEPARATOR.ToCharArray());
			drowResult[FONT_NAME_FLD] = strFont[0];
			drowResult[FONT_SIZE_FLD] = float.Parse(strFont[1]);
			drowResult[FONT_BOLD_FLD] = (strFont[2].ToUpper().IndexOf(BOLD) > NOT_FOUND);
			drowResult[FONT_ITALIC_FLD] = (strFont[2].ToUpper().IndexOf(ITALIC) > NOT_FOUND);
			drowResult[FONT_STRIKE_FLD] = (strFont[2].ToUpper().IndexOf(STRIKETHROUGH) > NOT_FOUND);
			drowResult[FONT_UNDERLINE_FLD] = (strFont[2].ToUpper().IndexOf(UNDERLINE) > NOT_FOUND);
			return drowResult;
		}
		

		/// <summary>
		/// Thachnn:30/12/2005
		/// Set font attribute (store in a DataRow, (refer to Mr.DungLA) ) to provided fields
		/// </summary>
		/// <param name="pfldToSet"></param>
		/// <param name="pdtr"></param>
		/// <returns>field with font applied</returns>
		private Field SetFieldFont(Field pfldToSet, DataRow pdtrFont)
		{
			try
			{
				pfldToSet.Font.Size				= float.Parse(pdtrFont[FONT_SIZE_FLD].ToString() );
			}
			catch{}
			try
			{
				pfldToSet.Font.Name			= pdtrFont[FONT_NAME_FLD].ToString();
			}
			catch{}
			try
			{
				pfldToSet.Font.Strikethrough	= Convert.ToBoolean(pdtrFont[FONT_STRIKE_FLD].ToString() );
			}
			catch{}
			try
			{
				pfldToSet.Font.Bold				= Convert.ToBoolean(pdtrFont[FONT_BOLD_FLD].ToString() );
			}
			catch{}
			try
			{
				pfldToSet.Font.Italic				= Convert.ToBoolean(pdtrFont[FONT_ITALIC_FLD].ToString() );
			}
			catch{}
			try
			{
				pfldToSet.Font.Underline		= Convert.ToBoolean(pdtrFont[FONT_UNDERLINE_FLD].ToString() );
			}
			catch{}

			return pfldToSet;
		}

		/// <summary>
		/// Find the parameter in the list by Para Name
		/// </summary>
		/// <param name="pstrParamName">Name of Param to find</param>
		/// <returns>Found Param or Argument Exception if not found</returns>
		public sys_ReportParaVO FindParam(string pstrParamName)
		{
			// find the param object in the list from param name
			foreach (sys_ReportParaVO voParam in mParameters)
			{
				if (voParam.ParaName == pstrParamName)
					return voParam;
			}
			throw new ArgumentException();
		}

		/// <summary>
		/// Gets the list of signature from the Signatures
		/// </summary>
		/// <param name="pstrSignatures">All Signatures from configuration</param>
		/// <returns>List of signature</returns>
		private ArrayList GetSignatures(string pstrSignatures)
		{
			ArrayList arrSigns = new ArrayList();
			// each line in signatures will be one signature
			string[] strSigns = pstrSignatures.Split("\r\n".ToCharArray());
			foreach (string strSign in strSigns)
			{
				if (strSign.Trim() != string.Empty)
					arrSigns.Add(strSign);
			}
			arrSigns.TrimToSize();
			return arrSigns;
		}
		/// <summary>
		/// Recalculates field width based on report configuration
		/// </summary>
		/// <param name="parrFields">Field List</param>
		/// <returns>Field list with new Width</returns>
		private ArrayList ReCalcualteFieldWidth(ArrayList parrFields)
		{
			// get total width of all fields
			float fTotalWidth = 0;
			foreach (sys_ReportFieldsVO voField in parrFields)
			{
				if (voField.Visisble && !voField.GroupBy && voField.Width > 1)
					fTotalWidth += voField.Width;
			}
			// rate between Page Size and Total Width
			float fRate = fPageSize / fTotalWidth;
			// apply the rate to field width
			foreach (sys_ReportFieldsVO voField in parrFields)
			{
				if (voField.Visisble && !voField.GroupBy && voField.Width > 1)
					voField.Width = Convert.ToInt32(voField.Width * fRate);
			}

			// return value
			return parrFields;
		}

		
		#endregion PRIVATE UTIL FUNCTIONS
		
		#region ROOT DRAWING FUNCTION - Draw field overload

		/// <summary>
		/// Draws a Field
		/// </summary>
		/// <param name="pFieldToDraw">Field To Draw</param>
		/// <returns>New Field</returns>
		public Field DrawField(Field pFieldToDraw)
		{
			Field ofield = pFieldToDraw;

			try
			{
				mrptReport.Fields.Add(ofield);				
			}
			catch //(Exception ex) //DEBUG:
			{
				//DEBUG: System.Windows.Forms.MessageBox.Show(ex.Message);
				ofield = null;
			}

			return ofield;
		}

		/// <summary>
		/// Draws field to section with name
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrName">Name of Field</param>
		/// <param name="pfFieldToDraw">Field to Draw</param>
		/// <returns>New Field</returns>
		public Field DrawField(Section pSection, string pstrName, Field pfFieldToDraw)
		{
			Field ofield = pfFieldToDraw;

			try
			{
				pSection.Fields.Add(ofield);
				if(pstrName != null && pstrName != string.Empty)
				{
					ofield.Name = pstrName;
				}
			}
			catch
			{
				//DEBUG: 
				//System.Windows.Forms.MessageBox.Show(ex.Message);

				ofield = null;
			}
			return ofield;
		}

		/// <summary>
		/// Draws field with full information
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrName">Name of Field</param>
		/// <param name="pobjValue">Field Value</param>
		/// <param name="pdStartLeft">Left</param>
		/// <param name="pdStartTop">Top</param>
		/// <param name="pdCellWidth">Width</param>
		/// <param name="pdCellHeight">Height</param>
		/// <returns>New Field</returns>
		public Field DrawField(Section pSection, string pstrName, object pobjValue, double pdStartLeft, double pdStartTop , double pdCellWidth, double pdCellHeight)
		{
			Field 	ofield = new Field();

			try
			{	
				ofield = DrawField(pSection,pstrName,ofield);
				if(pstrName != null && pstrName != string.Empty)
				{
					ofield.Name = pstrName;
				}
				ofield.Text = pobjValue.ToString();
				ofield.Left = pdStartLeft;
				ofield.Top = pdStartTop;
				ofield.Width = pdCellWidth;
				ofield.Height = pdCellHeight;

				ofield.Calculated = true;
				//ofield.Format = ;
				ofield.Font.Name = "Arial Narrow";
				ofield.Font.Size = 9;
				ofield.Font.Strikethrough = false;
				ofield.Font.Bold = false;
				ofield.Font.Italic = false;
				ofield.Font.Underline = false;
			
				if(pobjValue is String)
				{
					ofield.Align = FieldAlignEnum.RightTop;
				}
				else if(pobjValue is Boolean)
				{
					ofield.Align = FieldAlignEnum.CenterTop;
				}
				else
				{
					ofield.Align = FieldAlignEnum.LeftTop;
				}				
			}
			catch
			{
				//DEBUG: 
				//System.Windows.Forms.MessageBox.Show(ex.Message);
				ofield = null;
			}

			return ofield;
		}

		/// <summary>
		/// Draws field with full information
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrName">Name of Field</param>
		/// <param name="pobjValue">Field Value</param>
		/// <param name="prcSize">Size and Location</param>
		/// <returns>New Field</returns>
		public Field DrawField(Section pSection, string pstrName, object pobjValue, Rectangle prcSize)
		{
			Field ofield;

			string strValue = string.Empty;
			if(pobjValue != null)
			{
                strValue = pobjValue.ToString();
			}
			try
			{	
				ofield = pSection.Fields.Add(pstrName, strValue, prcSize);
				if(!string.IsNullOrEmpty(pstrName))
				{
					ofield.Name = pstrName;
				}
				ofield.Text = strValue;
				ofield.Calculated = true;
				ofield.Anchor = AnchorEnum.Top;
				ofield.Font.Name = "Arial Narrow";
				ofield.Font.Size = 9;
				ofield.Font.Strikethrough = false;
				ofield.Font.Bold = false;
				ofield.Font.Italic = false;
				ofield.Font.Underline = false;
			}
			catch // DEBUG:
			{
				ofield = null;
			}

			return ofield;
		}

		
		#endregion		
		
		#region PUBLIC UTIL FUNCTIONS

		#region Get report element, change properties (align, ...) , draw to existed elements
		

		/// <summary>
		/// Gets Field by its name
		/// throw ArgumentException if not found
		/// </summary>
		/// <param name="pstrFieldNameToGet">Name of Field</param>
		/// <returns>Field</returns>
		public Field GetFieldByName(string pstrFieldNameToGet)
		{
			try
			{
				return mrptReport.Fields[pstrFieldNameToGet];
			}
			catch
			{
				throw new ArgumentException(pstrFieldNameToGet);
			}
		}

        public void SetPictureField(string fieldName, string pictureFile)
        {
            try
            {
                mrptReport.Fields[fieldName].Picture = null;
                mrptReport.Fields[fieldName].Picture = Path.Combine(ReportDefinitionFolder, pictureFile);
                var field = mrptReport.Fields[fieldName];
                field.PictureAlign = PictureAlignEnum.Stretch;
            }
            catch{}
        }

		/// <summary>
		/// Gets Field by its name
		/// throw ArgumentException if not found
		/// </summary>
		/// <param name="pSection">Section to get field</param>
		/// <param name="pstrFieldNameToGet">Name of Field</param>
		/// <returns>Field</returns>
		public Field GetFieldByName(Section pSection, string pstrFieldNameToGet)
		{
			return pSection.Fields[pstrFieldNameToGet];
		}	

		/// <summary>
		/// Gets the report Section by name
		/// </summary>
		/// <param name="pstrSectionName">Name of the section to get</param>
		/// <returns>Section object with name</returns>
		public Section GetSectionByName(string pstrSectionName)
		{			
			return mrptReport.Sections[pstrSectionName];			
		}
		/// <summary>
		/// Gets the report Section by SectionTypeEnum
		/// </summary>
		/// <param name="psecType">Section Type get from SectionTypeEnum</param>
		/// <returns>Section object with name</returns>
		public Section GetSectionByType(SectionTypeEnum psecType)
		{			
			return mrptReport.Sections[psecType];
		}


		/// <summary>
		/// Sets alignment of a field
		/// </summary>
		/// <param name="pfldFieldToAlgin">Field to align</param>
		/// <param name="penumAlign">Align type</param>
		/// <returns>Aligned Field</returns>
		public Field AlignField(Field pfldFieldToAlgin, FieldAlignEnum penumAlign)
		{
			pfldFieldToAlgin.Align = penumAlign;
			return pfldFieldToAlgin;
		}

		/// <summary>
		/// Sets alignment of a field
		/// </summary>
		/// <param name="pfldFieldToAlgin">Field to align</param>
		/// <param name="pobjTypeCode">Code of data type of field</param>
		/// <returns>Aligned Field</returns>
		public Field AlignField(Field pfldFieldToAlgin, TypeCode pobjTypeCode)
		{
			switch (pobjTypeCode)
			{
				case TypeCode.String:
					pfldFieldToAlgin = AlignField(pfldFieldToAlgin, FieldAlignEnum.LeftMiddle);
					pfldFieldToAlgin.MarginLeft = MARGIN;
					break;
				case TypeCode.DateTime:
				case TypeCode.Boolean:
					pfldFieldToAlgin = AlignField(pfldFieldToAlgin, FieldAlignEnum.CenterMiddle);
					break;
				default:
					pfldFieldToAlgin = AlignField(pfldFieldToAlgin, FieldAlignEnum.RightMiddle);
					pfldFieldToAlgin.MarginRight = MARGIN;
					break;
			}
			return pfldFieldToAlgin;
		}

	
		/// <summary>
		/// Draws prefefined field list in report layout
		/// </summary>
		/// <param name="parrValuesToFill">Value to fill</param>
		/// <param name="pstrFieldPrefix">Field Prefix</param>
		/// <param name="pnStartID">Start ID</param>
		/// <param name="pnEndID">End ID</param>
		/// <returns>true if succeed, false if failure</returns>
		public bool DrawPredefinedList(ArrayList parrValuesToFill, string pstrFieldPrefix, int pnStartID, int pnEndID)
		{
			bool blnRet = false;
			
			if(pnStartID < 0 || pnEndID < 0 )
			{
				return false;
			}

			for(int i = pnStartID; i <= pnEndID; i++)
			{
				try
				{
					mrptReport.Fields[pstrFieldPrefix + i.ToString()].Text = parrValuesToFill[i].ToString();
				}
				catch	// draw continue, don't care about error value in the parrValuesToFill
				{
					//break;
				}
			}		

			return blnRet;
		}
		
		/// <summary>
		/// Sets field (existed in the report) to have new value
		/// <exception>not throw EXCEPTION</exception>
		/// <author>Thachnn</author>
		/// </summary>
		/// <param name="pstrFieldName">fieldname to set</param>
		/// <param name="pstrValueToSet">value to set</param>
		/// <returns>true if succeed, false if failure</returns>
		public bool DrawPredefinedField(string pstrFieldName, string pstrValueToSet)
		{
			bool blnRet = false;
			try
			{
				mrptReport.Fields[pstrFieldName].Text = pstrValueToSet;
				mrptReport.Fields[pstrFieldName].Value = pstrValueToSet;
				blnRet = true;
			}
			catch
			{
				// can't draw, may be FieldName is incorrect
			}

			return blnRet;
		}

		
		#endregion Get report element, change properties (align, ...) , draw to existed elements

		#region DRAW UTILS FUNCTIONS - Wrap Root Drawing, draw some predefine, box, line, sum field, ...

		/// <summary>
		/// Draws a Horizontal Line
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrName">Name</param>
		/// <param name="pdbLeft">Left</param>
		/// <param name="pdbTop">Top</param>
		/// <param name="pdbLong">Long</param>
		/// <param name="pdbThick">Thick size</param>
		/// <param name="pBorderStyle">Bordery Style</param>
		/// <returns>New Line</returns>
		public Field DrawHorizontalLine(Section pSection, string pstrName, double pdbLeft, double pdbTop, double pdbLong, double pdbThick, BorderStyleEnum pBorderStyle)
		{
			Field objFld = new Field();			
			objFld.Left = pdbLeft;
			objFld.Top = pdbTop;								
			objFld.Anchor = AnchorEnum.Top;
			objFld.BorderStyle = BorderStyleEnum.Solid;
			objFld.BorderColor = Color.LightGray;
			//objFld.SetZOrder(true);
			//objFld.ZOrder = 100;
			objFld.Visible = true;
			objFld.CanGrow = true;
			objFld.CanShrink = false;
			objFld.LineSlant = LineSlantEnum.NoSlant;

			objFld.Height = 0;	// make horizontal line			
			objFld.Width = pdbLong;	// make horizontal width = LONG
			objFld.BorderStyle = pBorderStyle;
			//objFld.LineWidth = pdbThick;
            
			return DrawField(pSection,pstrName,objFld);
		}

		/// <summary>
		/// Draws a Vertical Line
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrName">Name</param>
		/// <param name="pdbLeft">Left</param>
		/// <param name="pdbTop">Top</param>
		/// <param name="pdbLong">Long</param>
		/// <param name="pdbThick">Thick size</param>
		/// <param name="pBorderStyle">Bordery Style</param>
		/// <returns>New Line</returns>
		public Field DrawVerticalLine(Section pSection, string pstrName, double pdbLeft, double pdbTop, double pdbLong, double pdbThick, BorderStyleEnum pBorderStyle)
		{
			Field objFld = new Field();			
			objFld.Left = pdbLeft;
			objFld.Top = pdbTop;								
			objFld.Anchor = AnchorEnum.Top;
			objFld.BorderStyle = BorderStyleEnum.Solid;
			objFld.BorderColor = Color.LightGray;
			//objFld.SetZOrder(true);			
			//objFld.ZOrder = 100;
			objFld.Visible = true;
			objFld.CanGrow = true;
			objFld.CanShrink = false;
			objFld.LineSlant = LineSlantEnum.NoSlant;

			objFld.Height = pdbLong; // make vertical width = 0
			objFld.Width = 0;	// make vertical  line
			objFld.BorderStyle = pBorderStyle;
			//objFld.LineWidth = pdbThick;
            
			return DrawField(pSection,pstrName,objFld);
		}

		/// <summary>
		/// Draws chart field
		/// </summary>
		/// <param name="pSection">Section to draw</param>
		/// <param name="pstrFieldName">Field Name</param>
		/// <param name="pfldData">Data Field</param>
		/// /// <param name="fldMax">Max Field</param>
		/// <param name="prcSize">Size and Location in Rectangle format</param>
		/// <returns>New Field</returns>
		public Field DrawChartField(Section pSection, string pstrFieldName, Field pfldData, Field fldMax, Rectangle prcSize)
		{
			Field fldChart = DrawField(pSection, "fldChart" + pstrFieldName, string.Empty, prcSize);
			fldChart.BackColor = Color.FromArgb(58, 156, 124);
			fldChart.Calculated = true;
			fldChart.Height = pfldData.Height / 2;
			fldChart.Top = (pfldData.Height - fldChart.Height) / 2;
			fldChart.CanGrow = false;
			fldChart.CanShrink = false;
			if (pSection.OnPrint.Trim() != string.Empty)
			{
				pSection.OnPrint += "\n" + fldChart.Name + ".Width = " + fldMax.Name + ".Width * "
					+ "([" + pstrFieldName + "] / " + fldMax.Name + ")";
			}
			else
			{
				pSection.OnPrint = fldChart.Name + ".Width = " + fldMax.Name + ".Width * "
					+ "([" + pstrFieldName + "] / " + fldMax.Name + ")";
			}
			return fldChart;
		}
		
		/// <summary>
		/// Draws a field to hold the max value of a columns
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pstrFieldName">Field Name</param>
		/// <param name="prcSize">Field Size, Location</param>
		/// <param name="pblnVisible">true = visible, false = invisible</param>
		/// <returns>New Field</returns>
		public Field DrawMaxField(Section pSection, string pstrFieldName, Rectangle prcSize, bool pblnVisible)
		{
			// draw the max value first
			Field fldMax = mrptReport.Sections[SectionTypeEnum.Footer].Fields.Add("fldMax" + pstrFieldName, "=Max([" + pstrFieldName + "])", 0, 0, prcSize.Width, prcSize.Height);
			fldMax.Visible = pblnVisible;
			fldMax.Calculated = true;
			return fldMax;
		}
		/// <summary>
		/// Draws a Box
		/// </summary>
		/// <param name="pSection">Section to Draw</param>
		/// <param name="pdLeft">Left of Box</param>
		/// <param name="pdTop">Top of Box</param>
		/// <param name="pdWidth">Width of Box</param>
		/// <param name="pdHeight">Height of Box</param>
		/// <returns>New Box</returns>
		public Field DrawBox(Section pSection, double pdLeft, double pdTop, double pdWidth, double pdHeight)
		{
			Field fldBox = DrawField(pSection, "fldBox", string.Empty, pdLeft, pdTop, pdWidth, pdHeight);
			fldBox.Calculated = false;
			fldBox.BorderStyle = BorderStyleEnum.Solid;
			fldBox.BorderColor = Color.Black;
			fldBox.BackColor = Color.Transparent;
			return fldBox;
		}
	
		
		#endregion

		#region THACHNN: draw a list of parameters (provide NameValueCollection of para caption and its value, where to draw)	
		
		const float CHARACTER_WIDTH_RATIO = 85f;
		const int LABEL_WIDTH_ADJUSTMENT = 100;
		const int DISTANCE_BETWEEN_CONTROL = 50;			
		const int LABEL_DEFAULT_HEIGHT = 210;			


		/// <summary>
		/// /// Thachnn: 30/12/2005
		/// Draw list of parameter, on the specific Section, with Caption and Value in the NameValueCollection
		/// Draw starts at: X_START_LOCATION and Y_START_LOCATION
		/// Draw the Caption, then the Colon, then the Value of the Parameter.
		/// Some setting depend on the constant describe in this Region (DISTANCE_BETWEEN_CONTROL, LABEL_DEFAULT_HEIGHT, ...
		/// </summary>
		/// <param name="pSectionWhereToDraw"></param>
		/// <param name="pnX_START_LOCATION"></param>
		/// <param name="pnY_START_LOCATION"></param>
		/// <param name="parrParamAndValue"></param>
		/// <returns>Array of drawn fields</returns>
		public ArrayList DrawParameters(Section pSectionWhereToDraw, 
			double pnX_START_LOCATION  ,
			double pnY_START_LOCATION  ,
			System.Collections.Specialized.NameValueCollection parrParamAndValue,
			float pfFontSize)
		{
			/// all predefine and some value to draw is scale to font size = 9.
			/// If the font size is differ than 9, we multiple with that font size and divide to the CHARWIDTH_SCALE_TO_FONT_SIZE
			float CHARWIDTH_SCALE_TO_FONT_SIZE = 9;

			ArrayList arrRet = new ArrayList();	/// store all draw fields, return after function finish
			int nMaxParameterCaptionLength = GetMaxStringLength(parrParamAndValue.AllKeys);	/// store max length of strings, to draw the Caption of the parameter

			double dblActualCaptionHeight = LABEL_DEFAULT_HEIGHT*pfFontSize/CHARWIDTH_SCALE_TO_FONT_SIZE;
			double dblActualCaptionWidth = nMaxParameterCaptionLength*CHARACTER_WIDTH_RATIO*pfFontSize/CHARWIDTH_SCALE_TO_FONT_SIZE ;

			int nYOrder = 0;	// index to draw parameter line by line 
			foreach(string strCaption in parrParamAndValue.AllKeys)
			{
				/// 1###  draw Parameter caption field
				Field fldParaCaption = CreateParameterCaptionField(pnX_START_LOCATION , 
					pnY_START_LOCATION + nYOrder*( dblActualCaptionHeight + DISTANCE_BETWEEN_CONTROL) ,					
					(int) (dblActualCaptionWidth + LABEL_WIDTH_ADJUSTMENT), 
					dblActualCaptionHeight, 
					strCaption.Trim()					
					) ;				
				// set name for parameter fld + ParaName , for later reference
				fldParaCaption.Name = LABEL_PREFIX + strCaption.Trim();				
				//TESTED:								fldParaCaption.BorderStyle = BorderStyleEnum.Solid;
				// add control 
				fldParaCaption = DrawField(pSectionWhereToDraw, LABEL_PREFIX + strCaption , fldParaCaption);
                

				///  2### add the colon after the caption control
				Field fldColon = CreateColonField(fldParaCaption.Left + fldParaCaption.Width + DISTANCE_BETWEEN_CONTROL , fldParaCaption.Top , 50, fldParaCaption.Height);
				fldColon = DrawField(pSectionWhereToDraw, LABEL_PREFIX + strCaption , fldColon) ;


				/// 3###  add the value after the colon
				Field fldParaValue = CreateParameterValueField( fldColon.Left + fldColon.Width + DISTANCE_BETWEEN_CONTROL , fldColon.Top , fldColon.Height ,parrParamAndValue.Get(strCaption) ) ;
				fldParaValue = DrawField(	pSectionWhereToDraw, LABEL_PREFIX + strCaption , fldParaValue);
				nYOrder++;	

				fldParaCaption.Font.Size = fldColon.Font.Size = fldParaValue.Font.Size = pfFontSize;
				/// Add all drawed fields to the arrRet, to return 'em all
				arrRet.Add(fldParaCaption);
				arrRet.Add(fldColon);
				arrRet.Add(fldParaValue);
			}

			return arrRet;
		}
	

		/// <summary>
		/// Thachnn: 30/12/2005
		/// OVERLOAD DrawParameters, Use for the Draw Parameter of Auto render Report feature (render report without layout file)
		/// 
		/// Draw list of parameter, on the specific Section, with Caption and Value in the NameValueCollection
		/// Draw starts at: X_START_LOCATION and Y_START_LOCATION
		/// Draw the Caption, then the Colon, then the Value of the Parameter.
		/// Some setting depend on the constant describe in this Region (DISTANCE_BETWEEN_CONTROL, LABEL_DEFAULT_HEIGHT, ...
		/// </summary>
		/// <param name="pSectionWhereToDraw"></param>
		/// <param name="pnX_START_LOCATION"></param>
		/// <param name="pnY_START_LOCATION"></param>
		/// <param name="parrParamAndValue"></param>
		/// <param name="pdtrFontToApply"></param>
		/// <returns>Array of drawn fields, applied font</returns>		
		public ArrayList DrawParameters(Section pSectionWhereToDraw, 
			double pnX_START_LOCATION  ,
			double pnY_START_LOCATION  ,
			System.Collections.Specialized.NameValueCollection parrParamAndValue,
			DataRow pdtrFontToApply)
		{
			ArrayList arrRet = new ArrayList();	/// store all draw fields, return after function finish
			float fFontSize = 8f;	/// temporary
			try
			{
				fFontSize = float.Parse(pdtrFontToApply[FONT_SIZE_FLD].ToString() );
			}catch{}

            arrRet = DrawParameters(pSectionWhereToDraw, pnX_START_LOCATION, pnY_START_LOCATION,parrParamAndValue, fFontSize );

			/// apply font to all drawn fields
			foreach(Field fld in arrRet)
			{
				SetFieldFont(fld,pdtrFontToApply);
			}

			return arrRet;
		}
	

		/// <summary>
		/// Create a simple field, you should use only in this region.
		/// No border, transparent, font, size depend on the report layout default setting
		/// </summary>
		/// <param name="pnLeft"></param>
		/// <param name="pnTop"></param>
		/// <param name="pnWidth"></param>
		/// <param name="pnHeight"></param>
		/// <param name="pstrText"></param>
		/// <returns></returns>
		private Field CreateField(double pnLeft, double pnTop, double pnWidth, double pnHeight, string pstrText)
		{
			Field fld = new Field();			
			fld.Align = FieldAlignEnum.LeftMiddle;
			fld.Calculated = false;				
			fld.ForeColor = Color.Black;
			//fld.BackColor = Color.Transparent;
			
			///TESTED			fld.BackColor = Color.BlueViolet;

			//fld.MarginLeft = MARGIN;
			//fld.BorderColor = Color.Transparent;
			//fld.BorderStyle = BorderStyleEnum.Transparent;
			//test
			fld.BorderColor = Color.Black;
			fld.BorderStyle = BorderStyleEnum.Transparent;

			fld.Text = pstrText;
			fld.Left = pnLeft;
			fld.Top = pnTop;
			fld.Width = pnWidth;
			fld.Height = pnHeight;
			
			return fld;
		}
		
		/// <summary>
		/// Create Caption of the parameter. Depend on the CreateField in this region
		/// </summary>
		/// <param name="pnLeft"></param>
		/// <param name="pnTop"></param>
		/// <param name="pnWidth"></param>
		/// <param name="pnHeight"></param>
		/// <param name="pstrValue"></param>
		/// <returns></returns>
		private Field CreateParameterCaptionField(double pnLeft, double pnTop , double pnWidth, double pnHeight, string pstrValue)
		{
			Field fldCaption = CreateField(pnLeft,pnTop, pnWidth, pnHeight,pstrValue);	

			return fldCaption;
		}

		/// <summary>
		/// Create a field with Colon (:) mark. Depend on the CreateField in this region
		/// </summary>
		/// <param name="pnLeft"></param>
		/// <param name="pnTop"></param>
		/// <param name="pnWidth"></param>
		/// <param name="pnHeight"></param>
		/// <returns></returns>
		private Field CreateColonField(double pnLeft, double pnTop, double pnWidth, double pnHeight)
		{						
			Field fldColon = CreateField(pnLeft,pnTop, pnWidth, pnHeight, COLON);
			//TESTED					fldColon.BackColor = Color.Blue;
			return fldColon;
		}

		/// <summary>
		/// Create Value FIeld of the parameter. Depend on the CreateField in this region
		/// Auto width, depend on number of char in the value and CHARACTER_WIDTH_RATIO
		/// </summary>
		/// <param name="pnLeft"></param>
		/// <param name="pnTop"></param>
		/// <param name="pnHeight"></param>
		/// <param name="pstrValue"></param>
		/// <returns></returns>
		private Field CreateParameterValueField(double pnLeft, double pnTop , double pnHeight, string pstrValue)
		{
			Field fldValue = CreateField(pnLeft,pnTop, 10, pnHeight,pstrValue);
			//TESTED					fldValue.BackColor = Color.Red;			
			fldValue.Width = 	(int) (pstrValue.Length*CHARACTER_WIDTH_RATIO + LABEL_WIDTH_ADJUSTMENT);			
			fldValue.ZOrder = -1;

			return fldValue;
		}

		/// <summary>
		/// Get the maxString Length in the string Collection
		/// </summary>
		/// <param name="parr"></param>
		/// <returns></returns>
		private int GetMaxStringLength(string[] parr)
		{
			int nMaxParameterCaptionLength = 0;
			/// caculate the max-length of Parameter Name (in the label) )
			foreach(string str in parr)
			{				
				nMaxParameterCaptionLength = System.Math.Max(nMaxParameterCaptionLength, str.Length);
			}
			/// After finish the foreach loop, nMaxParameterCaptionLength will contain max char of Parameter Caption

			return nMaxParameterCaptionLength;            
		}


		#endregion

		#region THACHNN: draw a Box with a label below (like Make by, Check by, ...)
        
		const float BOX_CAPTION_FONT_SIZE = 5.5f;

		/// <summary>
		/// Draw a box, with Caption below, like Made by, Checked by, Approved by, 
		/// </summary>
		/// <param name="pSectionWhereToDraw"></param>
		/// <param name="pdblXStart"></param>
		/// <param name="pdblYStart"></param>
		/// <param name="pdblWidth"></param>
		/// <param name="pdblHeight"></param>
		/// <param name="pstrBoxCaption"></param>
		/// <param name="pdblCaptionHeight"></param>
		/// <returns></returns>
		public Field DrawBoxWithLabelBelow(Section pSectionWhereToDraw, 
			double pdblXStart,
			double pdblYStart,
			double pdblWidth,
			double pdblHeight,
			string pstrBoxCaption,
			double pdblCaptionHeight)
		{
			const string LINE = "LINE";
			Field fldBox = DrawBox(pSectionWhereToDraw,pdblXStart,pdblYStart,pdblWidth,pdblHeight);
			fldBox.BorderColor = Color.Gray;
			fldBox.BackColor = Color.White;
			//double dblBoxWidth = pstrBoxCaption.Length*CHARACTER_WIDTH_RATIO + 2*CAPTION_PADDING;			

			Field fldLine = DrawHorizontalLine(pSectionWhereToDraw,FIELD_PREFIX + pstrBoxCaption + LINE,
				fldBox.Left , fldBox.Top + fldBox.Height - pdblCaptionHeight - DISTANCE_BETWEEN_CONTROL/2,
				fldBox.Width,  1, BorderStyleEnum.Solid);
			fldLine.BorderColor = Color.Gray;

			Field fldBoxCaption = DrawField(pSectionWhereToDraw, FIELD_PREFIX+ pstrBoxCaption,  pstrBoxCaption,
				fldBox.Left + DISTANCE_BETWEEN_CONTROL/2, fldBox.Top + fldBox.Height - pdblCaptionHeight , 
				fldBox.Width - DISTANCE_BETWEEN_CONTROL , pdblCaptionHeight - DISTANCE_BETWEEN_CONTROL/2);
			fldBoxCaption.Font.Size = BOX_CAPTION_FONT_SIZE;
			fldBoxCaption = AlignField(fldBoxCaption,FieldAlignEnum.CenterMiddle);

			return fldBox;
		}


		/// <summary>
		/// Fastly draw Made by, Checked by, Approved by to the report
		/// </summary>
		/// <param name="pSectionWhereToDraw">Section to draw, you should draw to HEADER Section</param>
		/// <param name="pdblXStart"></param>
		/// <param name="pdblYStart"></param>
		/// <param name="pdblBoxWidth">Width of each child Box</param>
		/// <param name="pdblBoxHeight"></param>
		/// <param name="pdblBoxCaptionHeight">Height of Caption field</param>
		public void DrawBoxGroup_Madeby_Checkedby_Approvedby(Section pSectionWhereToDraw, double pdblXStart, double pdblYStart, double pdblBoxWidth, double pdblBoxHeight, double pdblBoxCaptionHeight)
		{			
			const string MADE_BY = "Made by";
			const string CHECKED_BY = "Checked by";
			const string APPROVED_BY = "Approved by";
			
			DrawBoxWithLabelBelow(pSectionWhereToDraw, pdblXStart												, pdblYStart ,  pdblBoxWidth, pdblBoxHeight , MADE_BY			, pdblBoxCaptionHeight );
			DrawBoxWithLabelBelow(pSectionWhereToDraw, pdblXStart + pdblBoxWidth						, pdblYStart ,  pdblBoxWidth, pdblBoxHeight , CHECKED_BY		, pdblBoxCaptionHeight );
			DrawBoxWithLabelBelow(pSectionWhereToDraw, pdblXStart + pdblBoxWidth + pdblBoxWidth   , pdblYStart ,  pdblBoxWidth, pdblBoxHeight , APPROVED_BY	, pdblBoxCaptionHeight );
		}


		#endregion THACHNN: draw a Box with a label below (like Make by, Check by, ...)

		#region THACHNN: clear not - existed days in month
		/// <summary>
		/// return an ArrayList contain day numbers in which do not exist in provide month.
		/// EXAMPLE: with month = 11,  year = 2005, return-ArrayList will contain int array: {31}
		/// EXAMPLE: with month = 2,  year = 2005, return-ArrayList will contain int array: {29,30,31}
		/// </summary>
		/// <param name="pnYear"></param>
		/// <param name="pnMonth"></param>
		/// <returns></returns>
		public static ArrayList GetDaysNotExistInMonth(int pnYear, int pnMonth)
		{
			ArrayList arrDaysToClear = new ArrayList();	// contain day columns is not exist
			for(int i = DateTime.DaysInMonth(pnYear, pnMonth) + 1 ; i < 32  ; i++ )
			{
				arrDaysToClear.Add(i);
			}
			return arrDaysToClear;
		}


		/// <summary>
		/// Thachnn: 05/01/2006
		/// HIDE the column of not-existed day in current month
		/// provided the array of field names to clear. For example = {fldA, fldB, fldSumA, ...}
		/// This function will set ForeColor  = Transparent, Text = "", Format = ""
		/// </summary>
		/// <param name="pnYear"></param>
		/// <param name="pnMonth"></param>
		/// <param name="parrFieldToClear"></param>
		public void HideColumnNotExistInMonth(int pnYear, int pnMonth, string[] parrFieldToClear)
		{
			// 1. DAY TO CLEAR
			ArrayList arrDaysToClear = GetDaysNotExistInMonth(pnYear,pnMonth);

			// 2. DO CLEAR
			foreach(string str in parrFieldToClear)
			{
				foreach(int i in arrDaysToClear)
				{					
					Field fldToClear = this.GetFieldByName(str+i);
					fldToClear.ForeColor = Color.Transparent;
					fldToClear.Visible = false;
					// fldToClear.Text = "";
					// fldToClear.Format = "";
				}
			}
		}

		/// <summary>
		/// Hide columns not exist in selected month. Then refine layout of report.
		/// This method will work correctly when call before binding datasource for report object
		/// </summary>
		/// <param name="pintYear">Year</param>
		/// <param name="pintMonth">Month</param>
		/// <param name="pstrDayColPrefix">Prefix of day field. Ex: lblD31 (1-Dec)</param>
		/// <param name="pstrDayOfWeekPrefix">Prefix of day of week field. Ex: lblDay (Tue)</param>
		/// <param name="pstrDivPrefix">Prefix of Div field. Ex: div30 (seperator field between 30 and 31)</param>
		/// <param name="pstrTotalField">Name of Total field. Ex: Total</param>
		/// <param name="pstrDetailPrefix">Prefix of Detail field. Ex: D (D01)</param>
		/// <param name="pstrFooterPrefix">Prefix of Footer field. Ex: Ftr01</param>
		/// <param name="pdFieldWidth">Width of one field</param>
		public void HideColumnNotExistInMonth(int pintYear, int pintMonth, string pstrDayColPrefix, string pstrDayOfWeekPrefix,
			string pstrDivPrefix, string pstrTotalField, string pstrDetailPrefix, string pstrFooterPrefix, 
			string pstrColPrefix, string pstrLineTopField, string pstrLineMiddleField,
			string pstrLineBottomField, string pstrLineDetailField, string pstrLineFooterField, double pdFieldWidth)
		{
			const string DAY_FORMAT = "00";
			int intDaysInMonth = DateTime.DaysInMonth(pintYear, pintMonth);
			string strTotal = string.Empty;
			for (int i = 1; i <= intDaysInMonth; i++)
				strTotal = "+" + pstrColPrefix + i.ToString(DAY_FORMAT);
			try
			{
				mrptReport.Fields[FIELD_PREFIX + pstrTotalField].Text = strTotal;
			}
			catch{}
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					string strDateCol = pstrDayColPrefix + i.ToString(DAY_FORMAT);
					string strDayOfWeek = pstrDayOfWeekPrefix + i.ToString(DAY_FORMAT);
					string strDiv = pstrDivPrefix + i.ToString(DAY_FORMAT);
					string strDivDetail = pstrDivPrefix + pstrDetailPrefix + i.ToString(DAY_FORMAT);
					string strDetail = FIELD_PREFIX + pstrColPrefix + i.ToString(DAY_FORMAT);
					string strFooter = FIELD_PREFIX + pstrFooterPrefix + i.ToString(DAY_FORMAT);
					// hide the label first
					try
					{
						mrptReport.Fields[strDateCol].Visible = false;
					}
					catch{}
					// hide the day of week
					try
					{
						mrptReport.Fields[strDayOfWeek].Visible = false;
					}
					catch{}
					// hide the div
					try
					{
						mrptReport.Fields[strDiv].Visible = false;
						mrptReport.Fields[strDivDetail].Visible = false;
					}
					catch{}
					// hide the data field
					try
					{
						mrptReport.Fields[strDetail].Visible = false;
					}
					catch{}
					// hide the footer
					try
					{
						mrptReport.Fields[strFooter].Visible = false;
					}
					catch{}
				}
				try
				{
					double dWidth = mrptReport.Fields[pstrLineTopField].Width;
					mrptReport.Fields[pstrLineMiddleField].Width = mrptReport.Fields[pstrLineMiddleField].Width - (31 - intDaysInMonth) * pdFieldWidth;
					mrptReport.Fields[pstrLineTopField].Width =
						mrptReport.Fields[pstrLineBottomField].Width =
						mrptReport.Fields[pstrLineDetailField].Width = dWidth - (31 - intDaysInMonth) * pdFieldWidth;
					dWidth = mrptReport.Fields[pstrLineTopField].Width;
					// move the total field
					mrptReport.Fields[LABEL_PREFIX + pstrTotalField].Left = dWidth - mrptReport.Fields[LABEL_PREFIX + pstrTotalField].Width;
					mrptReport.Fields[pstrDivPrefix + pstrDetailPrefix + pstrTotalField].Left = mrptReport.Fields[pstrDivPrefix + pstrTotalField].Left = dWidth;
					mrptReport.Fields[pstrDayOfWeekPrefix + pstrTotalField].Left = dWidth - mrptReport.Fields[LABEL_PREFIX + pstrTotalField].Width;
					mrptReport.Fields[FIELD_PREFIX + pstrTotalField].Left = dWidth - mrptReport.Fields[LABEL_PREFIX + pstrTotalField].Width;
				}
				catch{}
			}
		}

		#endregion
		
		#region THACHNN: spread columns within Width

		
		/// <summary>
		/// Thachnn: 10/01/2006		
		/// Spread the column, from the StartLeft, within the WidthToSpread
		/// This function only changes the Left and the Width of affect fields.
		/// 
		/// Getting fields to spread by: plus FieldNamesToMove and Index (from StartIndex to EndIndex)
		/// If this function meets the non-existed field, it will ignore and continue to the next field.
		/// </summary>
		/// <param name="parrFieldNamesToMove"></param>
		/// <param name="pnStartIndex"></param>
		/// <param name="pnEndIndex"></param>
		/// <param name="pdblLeftStartPositionToSpread"></param>
		/// <param name="pdblWidthToSpread"></param>
		public void SpreadColumnsWithinWidth(	StringCollection parrFieldNamesToMove, 
			int pnStartIndex , int pnEndIndex , 
			double pdblLeftStartPositionToSpread, double pdblWidthToSpread)
		{
			if(parrFieldNamesToMove.Count <= 0 )
				return;
			if(pnStartIndex > pnEndIndex)
				throw new ArgumentException(pnStartIndex + ">" + pnEndIndex);
			
			/// Init the width ratio of each columns (will be spread)
			/// if error, maybe the FieldsName Prefix is wrong, we return immediatelly
			System.Collections.Hashtable htbSamplingRatio = new System.Collections.Hashtable();
			string strSamplingFieldName = parrFieldNamesToMove[0];	// if goes here, parrFieldNamesToMove always has at least 1 member
			double dblTotalWidthBeforeSpread = 0;
			try
			{
				for(int i = pnStartIndex ; i <= pnEndIndex ; i++)
				{
					dblTotalWidthBeforeSpread += GetFieldByName( strSamplingFieldName+i.ToString("00")).Width ;
				}
				for(int i = pnStartIndex ; i <= pnEndIndex ; i++)
				{
					htbSamplingRatio.Add(i , (GetFieldByName( strSamplingFieldName+i.ToString("00")).Width) / dblTotalWidthBeforeSpread);
				}
			}
			catch{return;}


			foreach(string str in parrFieldNamesToMove)
			{
				for(int i = pnStartIndex ; i <= pnEndIndex ; i++)
				{
					try	 /// if error, maybe the fieldname pair is wrong, not existed in the rendered report  (= string + index )
					{
						Field fld = GetFieldByName( str+i.ToString("00"));
						fld.Width = pdblWidthToSpread * Convert.ToDouble( htbSamplingRatio[i] );

						if(i == pnStartIndex )
						{
							fld.Left = pdblLeftStartPositionToSpread;
						}
						else
						{
							int iMinus1 = i-1;
							Field fldPrevious =  GetFieldByName( str + iMinus1.ToString("00"));
							fld.Left = fldPrevious.Left  + fldPrevious.Width;

							string string1 = string.Empty;
						}

						string string2 = string.Empty;
					}
					catch	  /// continue to the next loop, next pair of field name
					{
						continue;
					}
				}
			}
		}

		#endregion	THACHNN: spread columns within Width

		#region THACHNN: Round all display value on C1Report to integer unit
		
		/// <summary>
		/// CAUTION: This function must be call after the Report Object is loaded and rendered !!!
		/// Ask PCS System (UtilsBO.GetRoundedQuantity() ), whether number in C1Report need to round to unit? (ex: 4.3 ---> 4,  4.6 ---> 5)
		/// by default, this function will not execute unless PCS asks it to do ( UtilsBO.GetRoundedQuantity() == true)
		/// 
		/// If Need To Round = true: it will loop throught the report object, 
		/// If field start with _fld  (By convention) or has QUANTITY string mask in the Tag property, we will:
		/// change Field.Format ---> #,##0
		/// if Format = #,##0.00 or string.Empty
		/// <author>Thachnn: 12/01/2006</author>
		/// </summary>
		/// <param name="pReport"></param>		
		public static void ReformatNumberInC1Report(C1Report pReport)
		{	

			if(NeedToRoundQuantity)
			{
				foreach(Field fld in pReport.Fields)
				{				
					string strFindPattern = @"(?<NumberFormatToFind>(\s*)(#)(.*)(\.)(0*)(\s*))";
					string strReplacePattern = "#,##0";
					
					/// Check whether this field is quantity relate: Start with _fld or contain QUANTITY string in the Tag:
					bool blnQtyRelate = fld.Name.ToLower().StartsWith(REPORT_QUANTITY_RELATE_FIELD_PREFIX);
					blnQtyRelate = blnQtyRelate ||   (  (fld.Tag != null)? (fld.Tag.ToString().IndexOf(REPORT_TAG_QUANTITY_RELATE) >= 0 )  : false  )   ;

					if(blnQtyRelate)	// if field name is Quantity relate 
					{
						if(fld.Format != null )
						{
							fld.Format = FormControlComponents.FindAndReplaceAll(fld.Format , strFindPattern, strReplacePattern , System.Text.RegularExpressions.RegexOptions.None);
						}
						else if(fld.Format == null)
						{
							fld.Format = strReplacePattern;
						}
					}

				}	// end foreach

			}	// end if
		}	// end function


		#endregion THACHNN: Round all display value on C1Report to integer unit

		#region THACHNN: Draw Day of Week, Day in Month, etc... on the report fields list

		/// <summary>
		/// This function will fill DayInMonth and DayOfWeek to predefined fields on C1Report.
		/// DayInMonth will appear like "01-Dec" format
		/// DayOfWeek will appear like "Sat" "Sun" format
		/// 
		/// Fields will be filled is specified by combine Prefix and a 2-digit DayIndex.
		/// DayInMonth will be fields like: pstrDayInMonthFieldNamePrefix + xx
		/// DayOfWeek will be fields like: pstrDayOfWeekFieldNamePrefix + xx
		/// This function will loop from pnStartIndex to pnEndIndex to get what "xx" is.
		/// <author>Thachnn: 18/01/2006</author>
		/// </summary>
		/// <param name="pnYear"></param>
		/// <param name="pnMonth"></param>
		/// <param name="pstrDayInMonthFieldNamePrefix"></param>
		/// <param name="pstrDayOfWeekFieldNamePrefix"></param>
		/// <param name="pnStartIndex"></param>
		/// <param name="pnEndIndex"></param>
		public void DrawPredefinedList_DaysOfWeek( int pnYear, int pnMonth , 
			string pstrDayInMonthFieldNamePrefix,   string pstrDayOfWeekFieldNamePrefix,
			int pnStartIndex, int pnEndIndex )
		{
			const string SUNDAY_3CHAR = "Sun";
			Color SUNDAY_FORECOLOR = Color.Red;
			Color SUNDAY_BACKCOLOR = Color.Yellow;			

			if(pnStartIndex <=0 || pnEndIndex <= 0)
				return;
			
			if(pnStartIndex > pnEndIndex)
				throw new ArgumentException(pnStartIndex + ">" + pnEndIndex);
			

			for(int i = pnStartIndex ; i <= pnEndIndex ; i++)
			{
				try
				{	
					DateTime dtm = new DateTime(pnYear, pnMonth, i );					
                    string strDayInMonth = i.ToString(FORMAT_DAY_2CHAR) + SEPERATOR_DATETIME +  dtm.ToString(FORMAT_MONTH_3CHAR);
					string strDayOfWeek = dtm.DayOfWeek.ToString().Substring(0,3);
					
					DrawPredefinedField(pstrDayInMonthFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR), strDayInMonth);
					DrawPredefinedField(pstrDayOfWeekFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR), strDayOfWeek);

					#region		Make Sunday RedText on Yellow Background
					
					if(strDayOfWeek == SUNDAY_3CHAR)
					{
						Field fldSunDay ;

						fldSunDay = GetFieldByName(pstrDayInMonthFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR) );						
						fldSunDay.BackColor = SUNDAY_BACKCOLOR;
						fldSunDay.ForeColor = SUNDAY_FORECOLOR;
						
						fldSunDay = GetFieldByName(pstrDayOfWeekFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR) );
						fldSunDay.BackColor = SUNDAY_BACKCOLOR;
						fldSunDay.ForeColor = SUNDAY_FORECOLOR;
					}					

					#endregion	Make Sunday RedText on Yellow Background
				}
				catch{}
			}

		}	/// end function


		
		public void DrawPredefinedList_MarkOffDayInMonth(ArrayList arrOffDays,  int pnYear, int pnMonth , 
			string pstrDayInMonthFieldNamePrefix,   string pstrDayOfWeekFieldNamePrefix,
			int pnStartIndex, int pnEndIndex )
		{			
			Color OFFDAY_FORECOLOR = Color.Red;
			Color OFFDAY_BACKCOLOR = Color.Yellow;			

			if(pnStartIndex <=0 || pnEndIndex <= 0)
				return;
			
			if(pnStartIndex > pnEndIndex)
				throw new ArgumentException(pnStartIndex + ">" + pnEndIndex);
			
			for(int i = pnStartIndex ; i <= pnEndIndex ; i++)
			{
				try
				{	
					if(arrOffDays.Contains(i) )
					{
						#region		Make Offday RedText on Yellow Background
					
						Field fldOffDay ;

						fldOffDay = GetFieldByName(pstrDayInMonthFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR) );						
						fldOffDay.BackColor = OFFDAY_BACKCOLOR;
						fldOffDay.ForeColor = OFFDAY_FORECOLOR;
						
						fldOffDay = GetFieldByName(pstrDayOfWeekFieldNamePrefix+i.ToString(FORMAT_DAY_2CHAR) );
						fldOffDay.BackColor = OFFDAY_BACKCOLOR;
						fldOffDay.ForeColor = OFFDAY_FORECOLOR;

						#endregion	Make Offday RedText on Yellow Background
					}			
				}
				catch{}
			}	// end for each day in FROM ---> TO index

		}	/// end function


		#endregion THACHNN: Draw Day of Week, Day in Month, etc... on the report fields list


		#region THACHNN: Mark "RED" to the report field

		/// <summary>
		/// this field is use internally and only in this Region: #region THACHNN: Mark "RED" to the report field
		/// It is: the color we will apply to the ForeColor of the field.
		/// <author>thachnn 28/02/2006</author>
		/// </summary>
		protected static Color NEGATIVE_NUMBER_COLOR = Color.Red;
		/// <summary>
		/// this field is use internally and only in this Region: #region THACHNN: Mark "RED" to the report field
		/// It is: the pointer to the Report in which the static mrptReport_MarkRedToNegative() event handler will use to process.
		/// ---- When use it with instance object.AttachDelegate_MarkRedToNegativeNumberFieldProcessing(), we must assign it to the inner Report member field (of this ReportBuilder object).
		/// ---- When use it in the static situation, it must be assign to the Report object (is the parameter pass in)
		/// <author>Thachnn 28/02/2006</author>
		/// </summary>
		protected static C1Report mrptReportPointer_HaveNegativeNumberFieldToMarkRed;

		/// <summary>
		/// This function is designed to attach to the PrintSection Event handler. It will execute each time the section is render. In that moment, we will inspect all field in rendering section and mark RED to the field which has negative number in value or text.
		/// It works only with mrptReportPointer_HaveNegativeNumberFieldToMarkRed (protected static member field).
		///  ---- this function can be assign to C1Report_PrintSection, EndSection to make field in the rendering section can be inspect
		/// After using this function, field will be mark RED with NEGATIVE_NUMBER_COLOR 
		/// or reset to NORMAL_NUMBER_COLOR (original color of the field before inspecting)
		/// <author>Thachnn 28/02/2006</author>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected static void mrptReport_MarkRedToNegative(object sender, ReportEventArgs e)
		{			
			foreach(Field fld in mrptReportPointer_HaveNegativeNumberFieldToMarkRed.Sections[e.Section].Fields)
			{
				Color NORMAL_NUMBER_COLOR = Color.Black;
				decimal decFieldEvaluate = decimal.Zero;

				if(fld.Calculated == false && fld.Text != null)
				{					
					try
					{						
						decFieldEvaluate = Convert.ToDecimal(fld.Text);					
					}
					catch
					{
						decFieldEvaluate = decimal.Zero;						
					}

					if(decFieldEvaluate < decimal.Zero)
					{
						fld.ForeColor = NEGATIVE_NUMBER_COLOR;							
					}
					else
					{
						fld.ForeColor = NORMAL_NUMBER_COLOR;							
					}

				}
				else if(fld.Calculated && fld.Text != null)
				{
					
					try
					{
						decFieldEvaluate = Convert.ToDecimal(fld.Value);
					}
					catch
					{
						decFieldEvaluate = decimal.Zero;
					}

					if(decFieldEvaluate  < decimal.Zero)
					{				
						fld.ForeColor = NEGATIVE_NUMBER_COLOR;
					}
					else
					{
						fld.ForeColor = NORMAL_NUMBER_COLOR;							
					}					
				}
				else
				{
					fld.ForeColor = NORMAL_NUMBER_COLOR;
				}
			}	// end foreach
		}

		
		/// <summary>
		/// This function will attach the mrptReport_MarkRedToNegative() fuction to PrintSection or EndSection event handler (of the inner C1Report member object of this ReportBuilder object). So, each time the section rendered, its fields will be check to mark RED if field value is negative number. 
		/// If you need to apply this processing to other Report, please use MarkRedToNegativeNumberField() function instead.
		/// --- NOTE: You should call the ReportBuilder.RenderReport(); after running this function	 to make change affect
		/// <author>Thachnn: 28/02/2006</author>
		/// </summary>
		public void MarkRedToNegativeNumberField()
		{				
            MarkRedToNegativeNumberField(this.mrptReport);
		}

		/// <summary>
		/// After using this function, field will be mark RED with NEGATIVE_NUMBER_COLOR 
		/// or reset to NORMAL_NUMBER_COLOR (original color of the field before inspecting)
		/// <author>thachnn 28/02/2006</author>
		/// </summary>
		/// <param name="prptApplyToReport"></param>
		public static void MarkRedToNegativeNumberField(C1Report prptApplyToReport)
		{
			// set the static pointer to the Report object (pass in by the parameter )
			// we need to assign the  mrptReport object to the static pointer mrptReportPointer_HaveNegativeNumberFieldToMarkRed before attaching the event handler.
			// see the comment of static field mrptReportPointer_HaveNegativeNumberFieldToMarkRed and comment of this function for more detail.
			mrptReportPointer_HaveNegativeNumberFieldToMarkRed = prptApplyToReport;			
			mrptReportPointer_HaveNegativeNumberFieldToMarkRed.PrintSection += new ReportEventHandler(mrptReport_MarkRedToNegative);
			/// mrptReportPointer_HaveNegativeNumberFieldToMarkRed.EndSection += new ReportEventHandler(this.mrptReport_EndSection);
		}


		
		#endregion THACHNN: Mark "RED" to the report field		

		
		#region THACHNN: HELPER Safety Convert Number and String for C1Report
		
		
		/// <summary>
		/// Return Decimal.Zero if input is null, DBNull or string.Empty.
		/// </summary>
		/// <param name="pobjInput"></param>
		/// <returns></returns>
		public static decimal ToDecimal(object pobjInput)
		{	
			decimal decRet = decimal.Zero;
			if(pobjInput == null || pobjInput is DBNull)
			{
				return decRet;
			}
			if(pobjInput.ToString() == string.Empty)
			{
				return decRet;
			}
			else
			{
				return Convert.ToDecimal(pobjInput);
			}			
		}

		/// <summary>
		/// Return Double.Zero if input is null, DBNull or string.Empty.
		/// </summary>
		/// <param name="pobjInput"></param>
		/// <returns></returns>
		public static double ToDouble(object pobjInput)
		{	
			double dblRet = 0d;
			if(pobjInput == null || pobjInput is DBNull)
			{
				return dblRet;
			}
			if(pobjInput.ToString() == string.Empty)
			{
				return dblRet;
			}
			else
			{
				return Convert.ToDouble(pobjInput);
			}			
		}

		/// <summary>
		/// Return Int.Zero if input is null, DBNull or string.Empty.
		/// </summary>
		/// <param name="pobjInput"></param>
		/// <returns></returns>
		public static int ToInt32(object pobjInput)
		{	
			int intRet = 0;
			if(pobjInput == null || pobjInput is DBNull)
			{
				return intRet;
			}
			if(pobjInput.ToString() == string.Empty)
			{
				return intRet;
			}
			else
			{
				return Convert.ToInt32(pobjInput);
			}			
		}

		
		#endregion THACHNN: HELPER Safety Convert Number and String for C1Report

		#endregion PUBLIC UTIL FUNCTIONS

	}	// end class ReportBuilder
	
	
}	// end namespace