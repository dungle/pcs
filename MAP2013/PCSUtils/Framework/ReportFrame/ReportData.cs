using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for ReportData.
	/// </summary>
	public class ReportData : Form
	{
		private SaveFileDialog dlgSaveFile;
		private ImageList imglstButtonImage;
		private ToolBar tbarReportData;
		private ToolBarButton btnF2;
		private ToolBarButton btnF3;
		private ToolBarButton btnF4;
		private ToolBarButton btnF5;
		private ToolBarButton toolBarSeperator1;
		private ToolBarButton btnF6;
		private ToolBarButton btnF7;
		private ToolBarButton btnF8;
		private ToolBarButton toolBarSeperator2;
		private ToolBarButton btnF9;
		private ToolBarButton btnF10;
		private ToolBarButton btnF11;
		private C1TrueDBGrid gridReportData;
		private IContainer components;

		private const string THIS = "PCSUtils.Framework.ReportFrame.ReportData";
		private string strFilterString = string.Empty;
		private string strPreviousFilterString = string.Empty;
		/// <summary>
		/// Determine if user has run the RowFilter function before or not
		/// </summary>
		private bool blnRunRowFilter;
		private DataTable mtblReportData;
		/// <summary>
		/// Report data
		/// </summary>
		public DataTable Data
		{
			get { return mtblReportData; }
			set { mtblReportData = value; }
		}

		public ReportData()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportData));
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.imglstButtonImage = new System.Windows.Forms.ImageList(this.components);
			this.tbarReportData = new System.Windows.Forms.ToolBar();
			this.btnF2 = new System.Windows.Forms.ToolBarButton();
			this.btnF3 = new System.Windows.Forms.ToolBarButton();
			this.btnF4 = new System.Windows.Forms.ToolBarButton();
			this.btnF5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarSeperator1 = new System.Windows.Forms.ToolBarButton();
			this.btnF6 = new System.Windows.Forms.ToolBarButton();
			this.btnF7 = new System.Windows.Forms.ToolBarButton();
			this.btnF8 = new System.Windows.Forms.ToolBarButton();
			this.toolBarSeperator2 = new System.Windows.Forms.ToolBarButton();
			this.btnF9 = new System.Windows.Forms.ToolBarButton();
			this.btnF10 = new System.Windows.Forms.ToolBarButton();
			this.btnF11 = new System.Windows.Forms.ToolBarButton();
			this.gridReportData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.gridReportData)).BeginInit();
			this.SuspendLayout();
			// 
			// dlgSaveFile
			// 
			this.dlgSaveFile.Filter = "Excel file|*.xls";
			// 
			// imglstButtonImage
			// 
			this.imglstButtonImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglstButtonImage.ImageSize = new System.Drawing.Size(24, 24);
			this.imglstButtonImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstButtonImage.ImageStream")));
			this.imglstButtonImage.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbarReportData
			// 
			this.tbarReportData.AutoSize = false;
			this.tbarReportData.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							  this.btnF2,
																							  this.btnF3,
																							  this.btnF4,
																							  this.btnF5,
																							  this.toolBarSeperator1,
																							  this.btnF6,
																							  this.btnF7,
																							  this.btnF8,
																							  this.toolBarSeperator2,
																							  this.btnF9,
																							  this.btnF10,
																							  this.btnF11});
			this.tbarReportData.ButtonSize = new System.Drawing.Size(32, 32);
			this.tbarReportData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbarReportData.DropDownArrows = true;
			this.tbarReportData.ImageList = this.imglstButtonImage;
			this.tbarReportData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tbarReportData.Location = new System.Drawing.Point(0, 0);
			this.tbarReportData.Name = "tbarReportData";
			this.tbarReportData.ShowToolTips = true;
			this.tbarReportData.Size = new System.Drawing.Size(520, 48);
			this.tbarReportData.TabIndex = 2;
			this.tbarReportData.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbarReportData_ButtonClick);
			// 
			// btnF2
			// 
			this.btnF2.ImageIndex = 0;
			this.btnF2.ToolTipText = "F2 - Clear all filter";
			// 
			// btnF3
			// 
			this.btnF3.ImageIndex = 1;
			this.btnF3.ToolTipText = "F3: Filter with current value";
			// 
			// btnF4
			// 
			this.btnF4.ImageIndex = 2;
			this.btnF4.ToolTipText = "F4: Filter with except current value";
			// 
			// btnF5
			// 
			this.btnF5.ImageIndex = 3;
			this.btnF5.ToolTipText = "F5: Return previous filter";
			// 
			// toolBarSeperator1
			// 
			this.toolBarSeperator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnF6
			// 
			this.btnF6.ImageIndex = 4;
			this.btnF6.ToolTipText = "F6: Row filter";
			// 
			// btnF7
			// 
			this.btnF7.ImageIndex = 5;
			this.btnF7.ToolTipText = "F7: View single record";
			// 
			// btnF8
			// 
			this.btnF8.ImageIndex = 6;
			this.btnF8.ToolTipText = "F8: Sum current column";
			// 
			// toolBarSeperator2
			// 
			this.toolBarSeperator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnF9
			// 
			this.btnF9.ImageIndex = 7;
			this.btnF9.ToolTipText = "F9: Export data to Excel";
			// 
			// btnF10
			// 
			this.btnF10.ImageIndex = 8;
			this.btnF10.ToolTipText = "F10: Print data to printer";
			// 
			// btnF11
			// 
			this.btnF11.ImageIndex = 9;
			this.btnF11.ToolTipText = "F11: Show drill down report";
			// 
			// gridReportData
			// 
			this.gridReportData.AllowUpdate = false;
			this.gridReportData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridReportData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gridReportData.CaptionHeight = 17;
			this.gridReportData.CollapseColor = System.Drawing.Color.Black;
			this.gridReportData.ColumnFooters = true;
			this.gridReportData.ExpandColor = System.Drawing.Color.Black;
			this.gridReportData.GroupByCaption = "Drag a column header here to group by that column";
			this.gridReportData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.gridReportData.Location = new System.Drawing.Point(0, 48);
			this.gridReportData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.gridReportData.Name = "gridReportData";
			this.gridReportData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridReportData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridReportData.PreviewInfo.ZoomFactor = 75;
			this.gridReportData.PrintInfo.ShowOptionsDialog = false;
			this.gridReportData.RecordSelectorWidth = 17;
			this.gridReportData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.gridReportData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.gridReportData.RowHeight = 15;
			this.gridReportData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.gridReportData.Size = new System.Drawing.Size(518, 302);
			this.gridReportData.TabIndex = 3;
			this.gridReportData.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}E" +
				"ditor{}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inactive" +
				"CaptionText;BackColor:InactiveCaption;}FilterBar{}Footer{}Caption{AlignHorz:Cent" +
				"er;}Style1{}Normal{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}St" +
				"yle12{}OddRow{}RecordSelector{AlignImage:Center;}Style13{}Heading{Wrap:True;Back" +
				"Color:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}" +
				"Style8{}Style10{AlignHorz:Near;}Style11{}Style14{}Style15{}Style9{}</Data></Styl" +
				"es><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCapti" +
				"onHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSele" +
				"ctorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup" +
				"=\"1\"><ClientRect>0, 0, 516, 300</ClientRect><BorderSide>0</BorderSide><CaptionSt" +
				"yle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><E" +
				"venRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me" +
				"=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Grou" +
				"p\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyl" +
				"e parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style" +
				"4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Rec" +
				"ordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Styl" +
				"e parent=\"Normal\" me=\"Style1\" /></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedS" +
				"tyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Styl" +
				"e parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style p" +
				"arent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style pa" +
				"rent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style pa" +
				"rent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=" +
				"\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style p" +
				"arent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits" +
				">1</horzSplits><Layout>None</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><" +
				"ClientArea>0, 0, 516, 300</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style1" +
				"4\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// ReportData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 352);
			this.Controls.Add(this.tbarReportData);
			this.Controls.Add(this.gridReportData);
			this.Name = "ReportData";
			this.Text = "Report Data";
			this.Load += new System.EventHandler(this.ReportData_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridReportData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ReportData_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
			gridReportData.DataSource = mtblReportData;
		}
		private void ClearAllFilter()
		{
			try
			{
				if (mtblReportData != null)
				{
					this.mtblReportData.DefaultView.RowFilter = string.Empty;
					strFilterString = string.Empty;
					strPreviousFilterString = string.Empty;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void FilterWithCurrentValue(bool blnExceptCurrentValue)
		{
			const string METHOD_NAME = THIS + ".FilterWithCurrentValue()";
			const string JOIN_FILTER_OPERATION = " and ";
			const string FILTER_OPERATION_LIKE = " like ";
			const string FILTER_OPERATION_EXCEPT = " <> ";
			// in order to use custom filter
			// we have to bind this True DbGrid to a Table not a DataSet
			try
			{
                if (gridReportData.RowCount == 0)
				{
					// if there is no row, this method will return without doing anything
					return;
				}
				// get the column name
				string strColumnName = gridReportData.Splits[0].DisplayColumns[gridReportData.Col].DataColumn.DataField;
				// get the cel value
				string strCellValue = gridReportData[gridReportData.Row, gridReportData.Col].ToString();

				// get the filter string

				string strFilterStringColumn = string.Empty;
				if (mtblReportData.Columns[strColumnName].DataType == typeof (string)
					|| mtblReportData.Columns[strColumnName].DataType == typeof (char))
				{
					if (!blnExceptCurrentValue)
					{
						strFilterStringColumn = "[" + strColumnName + "]" + FILTER_OPERATION_LIKE + " '" + strCellValue + "%'";
					}
					else
					{
						strFilterStringColumn = "[" + strColumnName + "]" + " not " + FILTER_OPERATION_LIKE + " '" + strCellValue + "%'";
					}
				}
				else
				{
					if (!blnExceptCurrentValue)
					{
						strFilterStringColumn = "[" + strColumnName + "]= '" + strCellValue + "'";
					}
					else
					{
						strFilterStringColumn = "[" + strColumnName + "]" + FILTER_OPERATION_EXCEPT + " '" + strCellValue + "'";
					}
				}

				strPreviousFilterString = strFilterString;
				if (strFilterString != string.Empty)
				{
					strFilterString += JOIN_FILTER_OPERATION + strFilterStringColumn;
				}
				else
				{
					strFilterString = strFilterStringColumn;
				}
				this.mtblReportData.DefaultView.RowFilter = strFilterString;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ReturnPreviousFilter()
		{
			try
			{
				this.mtblReportData.DefaultView.RowFilter = strPreviousFilterString;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void RowFilter()
		{
			gridReportData.FilterBar = !gridReportData.FilterBar;
			if (blnRunRowFilter) 
				gridReportData.FilterActive = true;
			blnRunRowFilter = true;
			if (gridReportData.FilterBar)
			{
				DataTable dtData = mtblReportData.Copy();

				foreach (DataColumn dtColumn in dtData.Columns)
				{
					string strFieldName = dtColumn.ColumnName;
					if (gridReportData.Columns[strFieldName].ValueItems.Presentation == PresentationEnum.CheckBox)
						continue;
					if (!gridReportData.Splits[0].DisplayColumns[strFieldName].Visible)
						continue;
					AddValueIntoComboBoxInTrueDBGrid(gridReportData.Columns[strFieldName],dtData,strFieldName);
					gridReportData.Splits[0].DisplayColumns[strFieldName].FilterButton = true;
					gridReportData.Splits[0].DisplayColumns[strFieldName].Button = false;

				}
				gridReportData.FilterActive = true;
			}
		}
		private void SumCurrentColumn()
		{
			const string METHOD_NAME = THIS + ".SumCurrentColumn()";
			//In order to use custom filter
			//we have to bind this True DbGrid to a Table not a DataSet
			try
			{
                if (gridReportData.RowCount == 0)
				{
					return;
				}
				//get the column name
				string strColumnName = gridReportData.Splits[0].DisplayColumns[gridReportData.Col].DataColumn.DataField;

				//check the data type for this column
				//mtblReportData.Columns[strColumnName].DataType == 

                int intGridRows = this.gridReportData.RowCount;

				// now compute the number of unique values for the country and city columns
				double dblTotalValue = 0;
				for (int i = 0; i < intGridRows; i++)
				{
					try
					{
						dblTotalValue += double.Parse(this.gridReportData[i, strColumnName].ToString());
					}
					catch
					{
						dblTotalValue += 0;
					}
				}
				gridReportData.ColumnFooters = true;
				this.gridReportData.Columns[strColumnName].FooterText = dblTotalValue.ToString();
				//viewing this value
				//PCSMessageBox.Show(ErrorCode.MESSAGE_VIEWTABLE_SUMCOLUMN 
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		private void PrintDataToPrinter()
		{
			const string METHOD_NAME = THIS + ".PrintDataToPrinter()";
			try
			{ //gridReportData.PrintInfo;
				Font fntFont;
				fntFont = new Font(gridReportData.PrintInfo.PageHeaderStyle.Font.Name, gridReportData.PrintInfo.PageHeaderStyle.Font.Size, FontStyle.Italic);
				gridReportData.PrintInfo.PageHeaderStyle.Font = fntFont;
				gridReportData.PrintInfo.PageHeader = "Composers Table";

				//column headers will be on every page
				gridReportData.PrintInfo.RepeatColumnHeaders = true;

				//'display page numbers (centered)
				gridReportData.PrintInfo.PageFooter = "Page: \\p";

				//'invoke print preview
				gridReportData.PrintInfo.UseGridColors = true;
				gridReportData.PrintInfo.PrintPreview();

				// Insert code to print the file.    
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		private void AddValueIntoComboBoxInTrueDBGrid(C1DataColumn c1dcDataColumn, DataTable dtData, string strFieldName) 
		{
			c1dcDataColumn.ValueItems.Presentation = PresentationEnum.ComboBox;


			ValueItem vi = new ValueItem();

			int intFirstRow = c1dcDataColumn.ValueItems.Values.Add(vi);
			c1dcDataColumn.ValueItems.Values[intFirstRow].DisplayValue = "ALL";
			c1dcDataColumn.ValueItems.Values[intFirstRow].Value = "";
			
			ArrayList arValue = new ArrayList();

            for (int i = 0; i < gridReportData.RowCount; i++)
			{
				string strValue = String.Empty;
					
				if (gridReportData.Columns[strFieldName].CellValue(i) != DBNull.Value && gridReportData.Columns[strFieldName].CellValue(i).ToString() != String.Empty)
					strValue = gridReportData.Columns[strFieldName].CellText(i);
					
				if (strValue == String.Empty) 
					continue;

				if (arValue.IndexOf(strValue) < 0)
					arValue.Add(strValue);
			}

			arValue.Sort();
			IEnumerator myEnumerator = arValue.GetEnumerator();
			while ( myEnumerator.MoveNext() )
			{
				vi = new ValueItem(myEnumerator.Current.ToString(), myEnumerator.Current);
				c1dcDataColumn.ValueItems.Values.Add(vi);
			}
		}

		private void tbarReportData_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			const string METHOD_NAME = THIS + ".tbarReportData_ButtonClick()";
			try
			{
				if (e.Button.Equals(this.btnF2))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F2));
				}
				if (e.Button.Equals(this.btnF3))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F3));
				}
				if (e.Button.Equals(this.btnF4))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F4));
				}
				if (e.Button.Equals(this.btnF5))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F5));
				}
				if (e.Button.Equals(this.btnF6))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F6));
				}
				if (e.Button.Equals(this.btnF7))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F7));
				}
				if (e.Button.Equals(this.btnF8))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F8));
				}
				if (e.Button.Equals(this.btnF9))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F9));
				}
				if (e.Button.Equals(this.btnF10))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F10));
				}
				if (e.Button.Equals(this.btnF11))
				{
					this.ControlKeyDown(this, new KeyEventArgs(Keys.F11));
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void ControlKeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ControlKeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F2:
						// clear all filter
						ClearAllFilter();
						break;
					case Keys.F3:
						// filter with current value
						FilterWithCurrentValue(false);
						break;
					case Keys.F4:
						// filter with except current value
						FilterWithCurrentValue(true);
						break;
					case Keys.F5:
						// return previous filter
						ReturnPreviousFilter();
						break;
					case Keys.F6:
						// row filter
						RowFilter();
						break;
					case Keys.F7:
						// single Record
						//ViewSingleRecord();
						break;
					case Keys.F8:
						// sum current column
						SumCurrentColumn();
						break;
					case Keys.F9:
						// export to Excel
						// display form allow user to select file name
						SaveFileDialog saveFile = new SaveFileDialog();

						saveFile.DefaultExt = "*.xls";
						saveFile.Filter = "xls File|*.xls| All File|*.*";

						if(saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&	saveFile.FileName.Length > 0) 
						{
							try
							{	
								if(System.IO.File.Exists(saveFile.FileName))
									File.Delete(saveFile.FileName);

								//tgridViewTable.ExportToExcel(saveFile.FileName);
								gridReportData.ExportToDelimitedFile(saveFile.FileName,RowSelectorEnum.AllRows,"\t",""," ",true, "Unicode");
								PCSMessageBox.Show(ErrorCode.MESSAGE_TASK_COMPLETED,MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,new string[]{"Exporting"});
							}
							catch
							{
								PCSMessageBox.Show(ErrorCode.CAN_NOT_READ_EXCEL_FILE,MessageBoxButtons.OK,MessageBoxIcon.Error);
							}
						}
						break;
					case Keys.F10:
						// Print data to printer
						PrintDataToPrinter();
						break;
					case Keys.F11:
						// Show drill down report
						//ShowDrillDownReport();
						break;
					case Keys.Escape:
						Close();
						break;
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
