using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.Admin.BO;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
	/// <summary>
	/// Summary description for RightByRecord.
	/// </summary>
	public class RightByRecord : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblRole;
		private System.Windows.Forms.ComboBox cboRoles;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.ComboBox cboType;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		const string THIS = "PCSUtils.Admin.RightByRecord";
		private const string SELECT = "Select";
		private const string FORMAT_TEXT = "FormatText Event";
		
		string strPKColumnName, strFKColumnName, strPKTable_Name, strSecurityTableName;
		DataSet dstData = new DataSet();
		DataSet dstRole = new DataSet();
		DataSet dstTypes = new DataSet();
		DataSet dstSecurityTable = new DataSet();
		private bool blnHasError = false;
		private System.Windows.Forms.CheckBox chkSelectAll;
		const int INVISIBLE_COLUMN_WIDTH = 1;
		public RightByRecord()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RightByRecord));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblRole = new System.Windows.Forms.Label();
			this.cboRoles = new System.Windows.Forms.ComboBox();
			this.lblType = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(533, 328);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(472, 328);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 6;
			this.btnHelp.Text = "&Help";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(411, 328);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblRole
			// 
			this.lblRole.ForeColor = System.Drawing.Color.Maroon;
			this.lblRole.Location = new System.Drawing.Point(8, 8);
			this.lblRole.Name = "lblRole";
			this.lblRole.Size = new System.Drawing.Size(40, 20);
			this.lblRole.TabIndex = 0;
			this.lblRole.Text = "Role";
			this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboRoles
			// 
			this.cboRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRoles.Location = new System.Drawing.Point(48, 8);
			this.cboRoles.Name = "cboRoles";
			this.cboRoles.Size = new System.Drawing.Size(224, 21);
			this.cboRoles.TabIndex = 1;
			this.cboRoles.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// lblType
			// 
			this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblType.ForeColor = System.Drawing.Color.Maroon;
			this.lblType.Location = new System.Drawing.Point(336, 8);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(40, 20);
			this.lblType.TabIndex = 2;
			this.lblType.Text = "Type";
			this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboType
			// 
			this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboType.Location = new System.Drawing.Point(376, 8);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(216, 21);
			this.cboType.TabIndex = 3;
			this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
			// 
			// dgrdData
			// 
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(8, 32);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 17;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(584, 288);
			this.dgrdData.TabIndex = 4;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
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
				"=\"1\"><ClientRect>0, 0, 580, 284</ClientRect><BorderSide>0</BorderSide><CaptionSt" +
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
				"ClientArea>0, 0, 580, 284</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style1" +
				"4\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.Location = new System.Drawing.Point(8, 328);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.TabIndex = 8;
			this.chkSelectAll.Text = "Select &All";
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// RightByRecord
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(600, 357);
			this.Controls.Add(this.chkSelectAll);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblType);
			this.Controls.Add(this.cboType);
			this.Controls.Add(this.lblRole);
			this.Controls.Add(this.cboRoles);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Name = "RightByRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Record Permission";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.RightByRecord_Closing);
			this.Load += new System.EventHandler(this.RightByRecord_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// RightByRecord_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <Author>Trada</Author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void RightByRecord_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".RightByRecord_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				//Load Combo box for Role
				RightByRecordBO boRightByRecord = new RightByRecordBO();
				dstRole = boRightByRecord.ListRole();
				cboRoles.DataSource = dstRole.Tables[Sys_RoleTable.TABLE_NAME];
				cboRoles.DisplayMember = Sys_RoleTable.NAME_FLD;
				cboRoles.ValueMember = Sys_RoleTable.ROLEID_FLD;
				//Load Combo box for Type
				dstTypes = boRightByRecord.ListType();
				cboType.DataSource = dstTypes.Tables[sys_RecordSecurityParamTable.TABLE_NAME];
				cboType.DisplayMember = sys_RecordSecurityParamTable.MENUNAME_FLD;
				cboType.ValueMember = sys_RecordSecurityParamTable.SOURCETABLENAME_FLD;
				cboType_SelectedIndexChanged(null,null);
				//Filter Row
				dgrdData.FilterActive = true;
				dgrdData.FilterBar = true;
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// GetDataByTableName
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private DataSet GetDataByTableName(string pstrTableName)
		{
			const string METHOD_NAME = THIS + ".GetDataByTableName()";
			try
			{
				DataSet dstFieldList = new DataSet();
				ViewTableBO boViewTable = new ViewTableBO();
				int intTableID = boViewTable.GetTableID(pstrTableName);
				dstFieldList = boViewTable.getFieldList(intTableID);
				string strSQLString = boViewTable.BuildSQLSelect(dstFieldList, pstrTableName, true);
				DataSet dstDataReturn = new DataSet();
				dstDataReturn.Tables.Add((boViewTable.getDataList(strSQLString, pstrTableName)).Tables[0].Copy());
				dstDataReturn.Tables.Add(dstFieldList.Tables[0].Copy());
				
				return dstDataReturn;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			this.Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// cboType_SelectedIndexChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 16 2005</date>
		private void cboType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboType_SelectedIndexChanged()";
			try
			{
				if (dstData.GetChanges() != null)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnSave_Click(btnSave, new EventArgs());
							if (blnHasError)
							{
								return;
							}
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							break;
					}
				}
				if (cboType.ValueMember != string.Empty)
				{
					ViewTableBO objViewTableBO = new ViewTableBO();
					DataRow[] drowTypes = dstTypes.Tables[0].Select(sys_RecordSecurityParamTable.MENUNAME_FLD + " = '" 
						+ cboType.Text + "'");
					//Get table name of security table name
					if (drowTypes.Length != 0)
					{
						strSecurityTableName = (drowTypes[0][sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD]).ToString();
					}
					else
						strSecurityTableName = string.Empty;
					DataSet dstTempDataSet = new DataSet();
					//Get data from ComboBox Type
					dstTempDataSet = GetDataByTableName(cboType.SelectedValue.ToString());
					dstData = new DataSet();
					dstData.Tables.Add(strSecurityTableName);
					dstData.Tables[0].Columns.Add(SELECT, typeof(bool));
					for (int i = 0; i < dstTempDataSet.Tables[0].Columns.Count; i++)
					{
						dstData.Tables[0].Columns.Add(dstTempDataSet.Tables[0].Columns[i].ColumnName, dstTempDataSet.Tables[0].Columns[i].DataType);
					}
					foreach(DataRow drow in dstTempDataSet.Tables[0].Rows)
					{
						DataRow drowData = dstData.Tables[0].NewRow(); 
						foreach (DataColumn dcol in dstTempDataSet.Tables[0].Columns)
						{
							drowData[dcol.ColumnName] = drow[dcol.ColumnName];
						}
						dstData.Tables[0].Rows.Add(drowData);
					}
					foreach(DataRow drow in dstData.Tables[0].Rows)
					{
						drow[SELECT] = true;
					}
					//Get data by cboRole
					if((cboRoles.SelectedIndex != -1) && (cboType.SelectedIndex != -1))
					{
						
						RightByRecordBO boRightByRecord = new RightByRecordBO();
						dstSecurityTable = boRightByRecord.GetRightByRecord(int.Parse(cboRoles.SelectedValue.ToString()), strSecurityTableName, cboType.SelectedValue.ToString());
					}
					CommonBO boCommon = new CommonBO();
					//Get primary key column of security table name
					strPKColumnName = boCommon.GetPKColumnName(strSecurityTableName);
					if (strPKColumnName == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.ERROR_DB, MessageBoxIcon.Error);
					}
					//Get foreign key column of security table name
					strFKColumnName = boCommon.GetFKColumnName(cboType.SelectedValue.ToString(), strSecurityTableName);
					if (strFKColumnName == string.Empty)
					{
						PCSMessageBox.Show(ErrorCode.ERROR_DB, MessageBoxIcon.Error);
					}
					foreach (DataRow drowSecurityTable in dstSecurityTable.Tables[0].Rows)
					{
						foreach (DataRow drowData in dstData.Tables[0].Rows)
						{
							if (drowData[strFKColumnName].ToString() == drowSecurityTable[strFKColumnName].ToString())
							{
								drowData[SELECT] = false;
							}
						}
					}

					//Bind Data to Grid
					dgrdData.DataSource = dstData.Tables[0];
					for (int i = 0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
					{
						dgrdData.Splits[0].DisplayColumns[i].Locked = true;
						dgrdData.Splits[0].DisplayColumns[i].Style.BackColor = this.BackColor;
					}
					//dgrdData.Splits[0].DisplayColumns[SELECT].DataColumn.ValueItems.Presentation = PresentationEnum.CheckBox;
					dgrdData.Splits[0].DisplayColumns[SELECT].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
					//dgrdData.Splits[0].DisplayColumns[SELECT].DataColumn.ValueItems.Translate = true;
					dgrdData.Splits[0].DisplayColumns[SELECT].Style.HorizontalAlignment =  AlignHorzEnum.Center;
					dgrdData.Splits[0].DisplayColumns[SELECT].Locked = false;
					dgrdData.Splits[0].DisplayColumns[SELECT].Style.BackColor = Color.White;
					//format the grid
					foreach (DataColumn dcolData in dstData.Tables[0].Columns)
					{
						// if data type is decimal then display only 2 digits 
						if (dcolData.DataType == typeof(decimal))
						{
							dgrdData.Columns[dcolData.ColumnName].NumberFormat = Constants.CELL_NUMBER_FORMAT;
						}
						else if (dcolData.DataType == typeof(bool))
						{
							dgrdData.Columns[dcolData.ColumnName].ValueItems.Presentation = PresentationEnum.CheckBox;
							dgrdData.Columns[dcolData.ColumnName].DefaultValue = false.ToString();
							for (int a = 0; a < dstData.Tables[0].Rows.Count; a++)
								if (dstData.Tables[0].Rows[a][dcolData.ColumnName] == DBNull.Value)
								{
									dstData.Tables[0].Rows[a][dcolData.ColumnName] = false;
								}
					
						}
						else if (dcolData.DataType == typeof(DateTime))
						{
							dgrdData.Columns[dcolData.ColumnName].Tag = Constants.DATETIME_TYPE;
							dgrdData.Columns[dcolData.ColumnName].NumberFormat = Constants.DATETIME_FORMAT;
						}
					}
					
					//Set Caption/Width
					//Get the field length for this table from table
					int intFormWidth = 0;
					DataSet dstFieldList = new DataSet();
					string strTableName = cboType.SelectedValue.ToString();
					int intTableID = objViewTableBO.GetTableID(strTableName);
					dstFieldList = objViewTableBO.getFieldList(intTableID);
					DataRow drFieldLength = objViewTableBO.GetFieldLength(dstFieldList, strTableName);

					//loop in the Field List, for each field set its corresponding column in the grid to its properties
					foreach (DataRow drRow in dstFieldList.Tables[0].Rows)
					{
						//get the Field Name
						string strFieldName = ((string) drRow[sys_TableFieldTable.FIELDNAME_FLD]).Trim();

						//Set the True DBGrid , column caption 
						//Select language here 
						dgrdData.Columns[strFieldName].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString();

						//set the data width for this column
						dgrdData.Columns[strFieldName].DataWidth = int.Parse(drFieldLength[strFieldName].ToString());

						//Sort direction
						int intSortType;
						try
						{
							intSortType = int.Parse(drRow[sys_TableFieldTable.SORTTYPE_FLD].ToString());
						}
						catch 
						{
							intSortType = 0;
						}

						string strSortField = dstData.Tables[0].DefaultView.Sort;
						switch (intSortType)
						{
							case PCSSortType.NONE:
						
								dgrdData.Columns[strFieldName].SortDirection = SortDirEnum.None;
								break;
							case PCSSortType.ASCENDING:
								if (strSortField != String.Empty) 
								{
									strSortField += "," + strFieldName;
								}
								else
								{
									strSortField += strFieldName;
								}
								dgrdData.Columns[strFieldName].SortDirection = SortDirEnum.Ascending;
								break;
							case PCSSortType.DESCENDING:
								dgrdData.Columns[strFieldName].SortDirection = SortDirEnum.Descending;
								if (strSortField != String.Empty) 
								{
									strSortField += "," + strFieldName + " DESC";
								}
								else
								{
									strSortField += strFieldName + " DESC";
								}
								break;
						}
						dstData.Tables[0].DefaultView.Sort = strSortField;

						//set Column Width
						dgrdData.Splits[0].DisplayColumns[strFieldName].Width = int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());

						intFormWidth += int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());

						if (int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString()) <= INVISIBLE_COLUMN_WIDTH)
						{
							dgrdData.Splits[0].DisplayColumns[strFieldName].MinWidth = 0;		
							dgrdData.Splits[0].DisplayColumns[strFieldName].Visible = false;
							dgrdData.Splits[0].DisplayColumns[strFieldName].Locked = true;
							dgrdData.Splits[0].DisplayColumns[strFieldName].AllowSizing = false;
							
						}
						//Set the alignment for a column
						int intAlign;
						try
						{
							intAlign = int.Parse(drRow[sys_TableFieldTable.ALIGN_FLD].ToString());
						}
						catch 
						{
							intAlign = 0;
						}
						switch (intAlign)
						{
							case PCSAligmentType.LEFT:
								dgrdData.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Near;
								break;
							case PCSAligmentType.CENTER:
								dgrdData.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Center;
								break;
							case PCSAligmentType.RIGHT:
								dgrdData.Splits[0].DisplayColumns[strFieldName].Style.HorizontalAlignment = AlignHorzEnum.Far;
								break;
						}
						//change the Character Case
						int nCase;
						try 
						{
							nCase = int.Parse(drRow[sys_TableFieldTable.CHARACTERCASE_FLD].ToString());
						}
						catch
						{
							nCase = 0;
						}
						if (nCase != 0)
						{
							dgrdData.Columns[strFieldName].NumberFormat = FORMAT_TEXT;
							dgrdData.Columns[strFieldName].Tag = nCase;
						}
						
						//For a column that gets its value from another table. 
						//We will define this column to be a button
						//When a user clicks at this button it will display another form (another instance of this form)
						if (drRow[sys_TableFieldTable.FROMTABLE_FLD] != null && drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() != String.Empty)
						{
							//Invisible the original column 
							dgrdData.Splits[0].DisplayColumns[strFieldName].Visible = false;

							//change this filter column to button enable
							string strFilterFieldName1 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim();

							//Create a string array to store temporay value for this column, so that later we can 
							//access this value to understand what we have to do with this column
							string[] strArrayValue1 = new string[6];
							strArrayValue1[0] = drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim(); //From Table Name
							strArrayValue1[1] = drRow[sys_TableFieldTable.FROMFIELD_FLD].ToString().Trim(); //Return value column
							strArrayValue1[2] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //Display value column
							if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
							{
								strArrayValue1[3] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //Display value column
							}
							else
							{
								strArrayValue1[3] = String.Empty; //Display value column
							}
							strArrayValue1[4] = strFieldName; //The column name of this column
							strArrayValue1[5] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //the original filter column name

							dgrdData.Columns[strFilterFieldName1].Caption = drRow[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].ToString().Trim();

							//dgrdData.Splits[0].DisplayColumns[strFilterFieldName1].Button = true;
							//Get the TableID, TableName, Return Field
							dgrdData.Splits[0].DisplayColumns[strFilterFieldName1].DataColumn.Tag = strArrayValue1;

							//if there is another filter column , we also have to set the same as the previous column is
							if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
							{
								//change this filter column to button enable
								string strFilterFieldName2 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim();

								dgrdData.Columns[strFilterFieldName2].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString().Trim();
								dgrdData.Splits[0].DisplayColumns[strFilterFieldName2].Button = true;

								string[] strArrayValue2 = new string[6];
								strArrayValue2[0] = drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim(); //From Table Name
								strArrayValue2[1] = drRow[sys_TableFieldTable.FROMFIELD_FLD].ToString().Trim(); //Return value column
								strArrayValue2[2] = drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim(); //Display value column
								strArrayValue2[3] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //Display value column
								strArrayValue2[4] = strFieldName; //The column name of this column
								strArrayValue2[5] = drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim(); //The original filter column name of this column

								//Get the TableID, TableName, Return Field
								dgrdData.Splits[0].DisplayColumns[strFilterFieldName2].DataColumn.Tag = strArrayValue2;
							}
						}



						//Change the caption of the Field that get data from another table
						if (drRow[sys_TableFieldTable.FROMTABLE_FLD] != null && drRow[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() != String.Empty)
						{
							//Invisible the original column 
							dgrdData.Splits[0].DisplayColumns[strFieldName].AllowSizing =
								dgrdData.Splits[0].DisplayColumns[strFieldName].Visible = false;
							//change this filter column to button enable
							string strFilterFieldName11 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim();
							
							#region //if Display external field 1 column
							dgrdData.Columns[strFilterFieldName11].Caption = drRow[sys_TableFieldTable.FIELD1CAPTIONEN_FLD].ToString().Trim();
							dgrdData.Columns[strFilterFieldName11].NumberFormat = drRow[sys_TableFieldTable.FORMATFIELD1_FLD].ToString().Trim();
							if ((drRow[sys_TableFieldTable.WIDTHFIELD1_FLD] != null)&&(drRow[sys_TableFieldTable.WIDTHFIELD1_FLD] != DBNull.Value))
							{
								dgrdData.Splits[0].DisplayColumns[strFilterFieldName11].Width = int.Parse( drRow[sys_TableFieldTable.WIDTHFIELD1_FLD].ToString());
							}
							#endregion
							
							//if there is another filter column , we also have to set the same as the previous column is
							if (drRow[sys_TableFieldTable.FILTERFIELD2_FLD] != null && drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
							{

								//change this filter column to button enable
								string strFilterFieldName22 = strFieldName + Constants.VIEW_TABLE_FILTER_SEPARATOR + drRow[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim();

								dgrdData.Columns[strFilterFieldName22].Caption = drRow[sys_TableFieldTable.FIELD2CAPTIONEN_FLD].ToString().Trim();
								if ((drRow[sys_TableFieldTable.WIDTH_FLD] != null)&&(drRow[sys_TableFieldTable.WIDTH_FLD] != DBNull.Value))
								{
									dgrdData.Splits[0].DisplayColumns[strFilterFieldName22].Width = int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());
								}
								dgrdData.Columns[strFilterFieldName22].NumberFormat = drRow[sys_TableFieldTable.FORMATFIELD2_FLD].ToString().Trim();
								if(dgrdData.Splits[0].DisplayColumns[strFilterFieldName22].Width <= 1)
								{
									dgrdData.Splits[0].DisplayColumns[strFilterFieldName22].AllowSizing =
										dgrdData.Splits[0].DisplayColumns[strFilterFieldName22].Visible = false;
								}
							}
						}

					}
				}
				dstData.AcceptChanges();
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		public bool CheckHasID(int pintItemID, int pintRoleID)
		{
			DataRow[] arrdowData = dstSecurityTable.Tables[0].Select(strFKColumnName + " = '" + pintItemID.ToString() + "'"
				+ " AND " + sys_RolePartyTable.ROLEID_FLD + " = '" + pintRoleID.ToString() + "'");
			if (arrdowData.Length > 0)
			{
				return true;
			}
			else
				return false;
			
		}
		/// <summary>
		/// ValidateData
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
		private bool ValidateData()
		{
			const string METHOD_NAME = THIS + ".ValidateData()";
			try
			{
				//Check mandatory
				if (FormControlComponents.CheckMandatory(cboRoles))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboRoles.Focus();
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboType))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
					cboType.Focus();
					return false;
				}
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			return true;
		}
		/// <summary>
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				blnHasError = true;
				if (ValidateData())
				{
					RightByRecordBO boRightByRecord = new RightByRecordBO();
					int intRoleID = int.Parse(cboRoles.SelectedValue.ToString());
					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						DataRow drowNewSecurityTableRow = dstSecurityTable.Tables[0].NewRow(); 
						if (drowData[SELECT].ToString() == false.ToString())
						{
							if (!CheckHasID(int.Parse(drowData[strFKColumnName].ToString()), intRoleID))
							{
								//add new row to dstSecurityTable
								drowNewSecurityTableRow[sys_RolePartyTable.ROLEID_FLD] = intRoleID;
								drowNewSecurityTableRow[strFKColumnName] = drowData[strFKColumnName];
								dstSecurityTable.Tables[0].Rows.Add(drowNewSecurityTableRow);
							}
						}
						else
						{
							foreach (DataRow drowSecurityTable in dstSecurityTable.Tables[0].Rows)
							{
								if (drowSecurityTable.RowState != DataRowState.Deleted)
								{
									if (drowData[strFKColumnName].ToString() == drowSecurityTable[strFKColumnName].ToString())
									{
										drowSecurityTable.Delete();
									}
								}
							}
						}
					}
					
					boRightByRecord.UpdateSecurityTable(dstSecurityTable, strSecurityTableName, cboType.SelectedValue.ToString());
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					blnHasError = false;
					dstData.AcceptChanges();
					cboType_SelectedIndexChanged(null, null);
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// RightByRecord_Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 18 2005</date>
		private void RightByRecord_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".RightByRecord_Closing()";
			try
			{
				if (dstData.GetChanges() != null)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnSave_Click(btnSave, new EventArgs());
							if (blnHasError)
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// chkSelectAll_CheckedChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Monday, December 12 2005</date>
		private void chkSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".chkSelectAll_CheckedChanged()";
			try
			{
                if (dgrdData.RowCount != 0)
				{
                    for (int i = 0; i < dgrdData.RowCount; i++)
					{
						if (chkSelectAll.Checked)
						{
							dgrdData[i, SELECT] = true;
						}
						else
							dgrdData[i, SELECT] = false;
					}
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
	}
}