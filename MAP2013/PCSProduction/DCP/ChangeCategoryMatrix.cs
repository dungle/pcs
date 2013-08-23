using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ChangeCategoryMatrix.
	/// </summary>
	public class ChangeCategoryMatrix : System.Windows.Forms.Form
	{
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtCCN;
		private System.Windows.Forms.TextBox txtWorkCenter;
		private System.Windows.Forms.Label lblWorkCenter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private const string THIS = "PCSProduction.DCP.ChangeCategoryMatrix";
		private DataSet dstData, dstRealData;
		private int intMasterID = 0;
		private System.Windows.Forms.Label lblUM;
		private bool blnHasChange  = false;

		public ChangeCategoryMatrix()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public ChangeCategoryMatrix(string pstrWorkCenter, string pstrCCN, int pintCCID)
		{
			const string METHOD_NAME = THIS + ".ChangeCategoryMatrix()";
			try
			{
				InitializeComponent();
				txtWorkCenter.Text = pstrWorkCenter;
				txtCCN.Text = pstrCCN;
				intMasterID = pintCCID;
				BindDataToMatrixGrid(pintCCID);
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

		private void BindDataToMatrixGrid(int pintChangeCategoryID)
		{
			const string METHOD_NAME = THIS + ".CreateTemplate()";
			try
			{
				DataSet dstInData = new ChangeCategoryBO().List(pintChangeCategoryID);
				if (dstInData.Tables[0].Rows.Count == 0)
				{
					btnSave.Enabled = false;
				}
				string strEmptyCaption = " ";
				dstData	 = new DataSet();
				dstData.Tables.Add(new DataTable());
				dstData.Tables[0].Columns.Add(ITM_ProductTable.PRODUCTID_FLD);
				dstData.Tables[0].Columns.Add(strEmptyCaption);
				foreach (DataRow drowData in dstInData.Tables[0].Rows)
				{
					dstData.Tables[0].Columns.Add(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString(), typeof(int));
				}

				foreach (DataRow drowData in dstInData.Tables[0].Rows)
				{
					DataRow drowNew = dstData.Tables[0].NewRow();
					drowNew[strEmptyCaption] = drowData[ITM_ProductTable.CODE_FLD] + Constants.OPEN_SBRACKET + drowData[ITM_ProductTable.REVISION_FLD].ToString() + Constants.CLOSE_SBRACKET;
					drowNew[ITM_ProductTable.PRODUCTID_FLD] = drowData[ITM_ProductTable.PRODUCTID_FLD];

					dstData.Tables[0].Rows.Add(drowNew);
				}

				dgrdData.DataSource = dstData.Tables[0];
				for(int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					if (i > 1)
					{
						dgrdData.Splits[0].DisplayColumns[i].DataColumn.Caption = dstInData.Tables[0].Rows[i - 2][ITM_ProductTable.CODE_FLD].ToString() + Constants.OPEN_SBRACKET + dstInData.Tables[0].Rows[i - 2][ITM_ProductTable.REVISION_FLD].ToString() + Constants.CLOSE_SBRACKET;
						dgrdData.Splits[0].DisplayColumns[i].Style.HorizontalAlignment = AlignHorzEnum.Far;
					}
					dgrdData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
					dgrdData.Splits[0].DisplayColumns[i].AutoSize();
					dgrdData.Columns[i].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				}
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.PRODUCTID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[strEmptyCaption].Style.BackColor = dgrdData.Splits[0].DisplayColumns[0].HeadingStyle.BackColor;
				dgrdData.Splits[0].DisplayColumns[strEmptyCaption].Locked = true;

				//Get all Real data
				dstRealData = new ChangeCategoryBO().ListMatrixByChangeCategoryMasterID(pintChangeCategoryID);
				if (dstRealData.Tables[0].Rows.Count == 0)
				{
					blnHasChange = true;
				}

				for (int i =0; i < dstRealData.Tables[0].Rows.Count; i++)
				{
					dgrdData[GetRowIndexByProductID(int.Parse(dstRealData.Tables[0].Rows[i][PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD].ToString())), dstRealData.Tables[0].Rows[i][PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD].ToString()]
						= dstRealData.Tables[0].Rows[i][PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD].ToString();
				}

				for (int i =0; i < dstInData.Tables[0].Rows.Count; i++)
				{
					for (int j = 0; j <dgrdData.RowCount; j++ )
					{
						if (dgrdData[j, i+2].ToString() == string.Empty)
						{
							dgrdData[j, i+2] = 0;
						}
					}
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
		}

		private int GetRowIndexByProductID(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetRowIndexByProductID()";
			try
			{
				for (int i =0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() == pintProductID.ToString())
					{
						return i;
					}
				}
				return 0;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ChangeCategoryMatrix));
			this.txtWorkCenter = new System.Windows.Forms.TextBox();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblCCN = new System.Windows.Forms.Label();
			this.lblWorkCenter = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtCCN = new System.Windows.Forms.TextBox();
			this.lblUM = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// txtWorkCenter
			// 
			this.txtWorkCenter.AccessibleDescription = "";
			this.txtWorkCenter.AccessibleName = "";
			this.txtWorkCenter.Location = new System.Drawing.Point(76, 6);
			this.txtWorkCenter.Name = "txtWorkCenter";
			this.txtWorkCenter.ReadOnly = true;
			this.txtWorkCenter.Size = new System.Drawing.Size(119, 20);
			this.txtWorkCenter.TabIndex = 52;
			this.txtWorkCenter.Text = "";
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowSort = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(4, 32);
			this.dgrdData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.dgrdData.Name = "dgrdData";
			this.dgrdData.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.dgrdData.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.dgrdData.PreviewInfo.ZoomFactor = 75;
			this.dgrdData.PrintInfo.ShowOptionsDialog = false;
			this.dgrdData.RecordSelectorWidth = 16;
			this.dgrdData.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.dgrdData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.dgrdData.RowHeight = 15;
			this.dgrdData.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.dgrdData.Size = new System.Drawing.Size(622, 382);
			this.dgrdData.TabIndex = 54;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.dgrdData_BeforeColEdit);
			this.dgrdData.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColEdit);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Style11{}Style12{}Style13{}Style5{}Style4{}Style7{}Style6{}EvenRow{Back" +
				"Color:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Heading{Wrap:T" +
				"rue;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:C" +
				"ontrol;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Filter" +
				"Bar{}OddRow{}Footer{}Caption{AlignHorz:Center;}Normal{Font:Microsoft Sans Serif," +
				" 8.25pt;}Style10{AlignHorz:Near;}HighlightRow{ForeColor:HighlightText;BackColor:" +
				"Highlight;}Editor{}RecordSelector{AlignImage:Center;}Style9{}Style8{}Style3{}Sty" +
				"le2{}Style14{}Style15{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;Align" +
				"Vert:Center;}Style1{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name" +
				"=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeS" +
				"tyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScr" +
				"ollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 618, 378</ClientRect><B" +
				"orderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyl" +
				"e parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Fi" +
				"lterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"" +
				"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Headin" +
				"g\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inactiv" +
				"eStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" " +
				"/><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle par" +
				"ent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1Tru" +
				"eDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style pa" +
				"rent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent" +
				"=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=" +
				"\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Nor" +
				"mal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"No" +
				"rmal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=" +
				"\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><ve" +
				"rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Defau" +
				"ltRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 618, 378</ClientArea><Pri" +
				"ntPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"S" +
				"tyle15\" /></Blob>";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(4, 419);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 55;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCCN.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(490, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 53;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblWorkCenter
			// 
			this.lblWorkCenter.AccessibleDescription = "";
			this.lblWorkCenter.AccessibleName = "";
			this.lblWorkCenter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblWorkCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWorkCenter.Location = new System.Drawing.Point(4, 6);
			this.lblWorkCenter.Name = "lblWorkCenter";
			this.lblWorkCenter.Size = new System.Drawing.Size(72, 20);
			this.lblWorkCenter.TabIndex = 56;
			this.lblWorkCenter.Text = "Work Center";
			this.lblWorkCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(567, 419);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 22);
			this.btnClose.TabIndex = 57;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtCCN
			// 
			this.txtCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCCN.Location = new System.Drawing.Point(524, 6);
			this.txtCCN.Name = "txtCCN";
			this.txtCCN.ReadOnly = true;
			this.txtCCN.Size = new System.Drawing.Size(102, 20);
			this.txtCCN.TabIndex = 58;
			this.txtCCN.Text = "";
			// 
			// lblUM
			// 
			this.lblUM.Location = new System.Drawing.Point(202, 13);
			this.lblUM.Name = "lblUM";
			this.lblUM.Size = new System.Drawing.Size(198, 15);
			this.lblUM.TabIndex = 59;
			this.lblUM.Text = "Unit of Change Category is Second (s)";
			// 
			// ChangeCategoryMatrix
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.lblUM);
			this.Controls.Add(this.txtCCN);
			this.Controls.Add(this.txtWorkCenter);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.lblWorkCenter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "ChangeCategoryMatrix";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change Category - Matrix";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ChangeCategoryMatrix_Closing);
			this.Load += new System.EventHandler(this.ChangeCategoryMatrix_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ChangeCategoryMatrix_Load(object sender, System.EventArgs e)
		{
			dgrdData.RowHeight = Constants.DEFAULT_ROW_HEIGHT;
			dgrdData.MarqueeStyle = MarqueeEnum.HighlightCell;
			dgrdData.HighLightRowStyle.BackColor = Color.FromArgb((byte)Constants.BACKGROUND_COLOUR_R, (byte)Constants.BACKGROUND_COLOUR_G, (byte)Constants.BACKGROUND_COLOUR_B);
			dgrdData.HighLightRowStyle.ForeColor = Color.FromArgb((byte)Constants.FORE_COLOUR_R, (byte)Constants.FORE_COLOUR_R, (byte)Constants.FORE_COLOUR_R);
			dgrdData.Style.VerticalAlignment =  C1.Win.C1TrueDBGrid.AlignVertEnum.Center;
			if(dgrdData.Splits.Count > 0)
			{
				// Set default alignment
				foreach(C1DisplayColumn c1Column in dgrdData.Splits[0].DisplayColumns)
				{
					c1Column.HeadingStyle.HorizontalAlignment =  C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
				}
			}
			dgrdData.Splits[0].DisplayColumns[1].AllowFocus = false;
			// Not allow user change column order
			dgrdData.AllowColMove = false;
			dgrdData.Refresh();
			btnSave.Visible = true;
			// this.MaximizeBox = false;
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = ".btnSave_Click()";
			try
			{
				if (dgrdData.EditActive) return;
				dstRealData.Tables[0].Rows.Clear();
				dstRealData.Tables[0].AcceptChanges();

				//get data from grid
				for (int i =0; i <dstData.Tables[0].Rows.Count; i++)
				{
					for (int j =0; j <dstData.Tables[0].Rows.Count; j++)
					{
						DataRow drow = dstRealData.Tables[0].NewRow();
						drow[PRO_ChangeCategoryMatrixTable.SOURCEPRODUCTID_FLD] = dstData.Tables[0].Rows[i][ITM_ProductTable.PRODUCTID_FLD];
						drow[PRO_ChangeCategoryMatrixTable.DESTPRODUCTID_FLD] = dstData.Tables[0].Rows[j][ITM_ProductTable.PRODUCTID_FLD];;
						drow[PRO_ChangeCategoryMatrixTable.CHANGETIME_FLD] = dgrdData[i, dstData.Tables[0].Rows[j][ITM_ProductTable.PRODUCTID_FLD].ToString()];
						drow[PRO_ChangeCategoryMatrixTable.CHANGECATEGORYMASTERID_FLD] = intMasterID;
						dstRealData.Tables[0].Rows.Add(drow);
					}
				}

				new ChangeCategoryBO().UpdateDataSetMatrix(dstRealData, intMasterID);
				PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
				blnHasChange = false;
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

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if (dgrdData.EditActive) return;
			this.Close();
		}

		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = ".dgrdData_BeforeColUpdate()";
			try
			{
//				try
//				{
//					if (int.Parse(e.Column.DataColumn.Value.ToString()) < 0)
//					{
//						e.Cancel = true;
//						PCSMessageBox.Show(ErrorCode.MESSAGE_CHANGECATEGORY_CHANGETIME, MessageBoxIcon.Error);
//					}
//				}
//				catch
//				{
//					e.Cancel = true;
//					PCSMessageBox.Show(ErrorCode.MESSAGE_CHANGECATEGORY_CHANGETIME, MessageBoxIcon.Error);
//				}
				if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
				{
					if (e.Column.DataColumn.Text == string.Empty)
					{
						return;
					}
					try
					{
						if (decimal.Parse(e.Column.DataColumn.Text) < 0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_CHANGECATEGORY_CHANGETIME, MessageBoxIcon.Warning);
							e.Cancel = true;
						}
					}
					catch
					{
						e.Cancel = true;
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

		private void ChangeCategoryMatrix_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".ChangeCategoryMatrix_Closing()";
			try
			{
				if (blnHasChange)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							//Save before exit
							btnSave_Click(btnSave, new EventArgs());
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
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

		private void dgrdData_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = ".dgrdData_AfterColEdit()";
			try
			{
				blnHasChange = true;
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

		private void dgrdData_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColEdit()";
			try
			{
				if (e.Column.DataColumn.DataField == dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD].ToString())
				{
					e.Cancel = true;
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
