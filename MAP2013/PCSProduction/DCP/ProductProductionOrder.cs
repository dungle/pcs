using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduction.DCP.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for ProductProductionOrder.
	/// </summary>
	public class ProductProductionOrder : Form
	{
		private C1TrueDBGrid dgrdData;
		private TextBox txtProductionLine;
		private Label lblProductionLine;
		private Button btnProductionLine;
		private Button btnSave;
		private Button btnClose;
		private Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private DataTable dtbGridLayOut;
		private DataSet dstData = new DataSet();
		const string THIS = "PCSProduction.DCP.ProductProductionOrder";
		const string SEQUENCE_FLD = "Seq";
		const string PRODUCT_IN_PRODUCTIONLINE_VIEWNAME = "v_ProductInProductionLine";
		DCOptionsBO boOption = new DCOptionsBO();

		public ProductProductionOrder()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductProductionOrder));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.txtProductionLine = new System.Windows.Forms.TextBox();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.btnProductionLine = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
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
			this.dgrdData.Location = new System.Drawing.Point(4, 30);
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
			this.dgrdData.Size = new System.Drawing.Size(474, 230);
			this.dgrdData.TabIndex = 11;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterDelete += new System.EventHandler(this.dgrdData_AfterDelete);
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part No\" Da" +
				"taField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Sequence\" DataField=\"Seq\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"R" +
				"evision\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.W" +
				"in.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style58{AlignHorz:Center;}Style59{A" +
				"lignHorz:Near;}Caption{AlignHorz:Center;}Style27{}Normal{Font:Tahoma, 11world;}S" +
				"tyle25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}S" +
				"tyle19{}Style14{}Style15{}Style16{AlignHorz:Center;ForeColor:Maroon;}Style17{Ali" +
				"gnHorz:Near;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style63{}Style62{" +
				"}Style61{}Style60{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Sty" +
				"le26{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}Style22{" +
				"AlignHorz:Center;ForeColor:Maroon;}Style21{}Style20{}Group{BackColor:ControlDark" +
				";Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveCaptionTex" +
				"t;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert" +
				":Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style" +
				"24{}Style5{}Style41{AlignHorz:Far;}Style40{AlignHorz:Center;ForeColor:Maroon;}St" +
				"yle43{}FilterBar{}Style42{}Style44{}Style45{}Style9{}Style8{}Style12{}Style4{}St" +
				"yle7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGr" +
				"id.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHei" +
				"ght=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth" +
				"=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 470, 2" +
				"26</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style" +
				"10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" " +
				"me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle par" +
				"ent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingSty" +
				"le parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"S" +
				"tyle7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"Odd" +
				"Row\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><" +
				"SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\"" +
				" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><" +
				"Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Vi" +
				"sible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>122</Width><Height>15" +
				"</Height><Button>True</Button><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sin" +
				"gle</ColumnDivider><Width>151</Width><Height>15</Height><Button>True</Button><DC" +
				"Idx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
				"ight><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style4" +
				"4\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
				">75</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internalCols><" +
				"/C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal" +
				"\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" />" +
				"<Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><" +
				"Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Styl" +
				"e parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Sty" +
				"le parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><" +
				"Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></Na" +
				"medStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</" +
				"Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 470, 226</Cl" +
				"ientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle pa" +
				"rent=\"\" me=\"Style15\" /></Blob>";
			// 
			// txtProductionLine
			// 
			this.txtProductionLine.Location = new System.Drawing.Point(90, 6);
			this.txtProductionLine.MaxLength = 24;
			this.txtProductionLine.Name = "txtProductionLine";
			this.txtProductionLine.Size = new System.Drawing.Size(126, 20);
			this.txtProductionLine.TabIndex = 13;
			this.txtProductionLine.Text = "";
			this.txtProductionLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductionLine_KeyDown);
			this.txtProductionLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtProductionLine_Validating);
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.ForeColor = System.Drawing.Color.Maroon;
			this.lblProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProductionLine.Location = new System.Drawing.Point(4, 6);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(86, 21);
			this.lblProductionLine.TabIndex = 12;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnProductionLine
			// 
			this.btnProductionLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnProductionLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnProductionLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProductionLine.Location = new System.Drawing.Point(218, 6);
			this.btnProductionLine.Name = "btnProductionLine";
			this.btnProductionLine.Size = new System.Drawing.Size(24, 20);
			this.btnProductionLine.TabIndex = 14;
			this.btnProductionLine.Text = "...";
			this.btnProductionLine.Click += new System.EventHandler(this.btnProductionLine_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(4, 264);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 16;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(418, 264);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 18;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(356, 264);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 17;
			this.btnHelp.Text = "&Help";
			// 
			// ProductProductionOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(486, 292);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtProductionLine);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.btnProductionLine);
			this.Controls.Add(this.dgrdData);
			this.Name = "ProductProductionOrder";
			this.Text = "Order Of Product In Production Line";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductProductionOrder_Closing);
			this.Load += new System.EventHandler(this.ProductProductionOrder_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private bool SelectProductionLine(bool pblnAlwaysShowDialog)
		{
			Hashtable htbCriteria = new Hashtable();				

			//Call OpenSearchForm for selecting Production Line
			DataRowView drwResult = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD, txtProductionLine.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
			
			//If has Production Line matched searching condition, fill values to form's controls
			if(drwResult != null)
			{
				txtProductionLine.Text = drwResult[PRO_ProductionLineTable.CODE_FLD].ToString();
				txtProductionLine.Tag = drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];

				//Reset Modified status
				txtProductionLine.Modified = false;

				FillDataToGrid(Convert.ToInt32(drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD]));
			}
			else if(!pblnAlwaysShowDialog)
			{
				txtProductionLine.Focus();
				return false;
			}

			return true;
		}

		private void FillDataToGrid(int pintProductionLineID)
		{
			dstData = boOption.GetProductSequence(pintProductionLineID);
			dgrdData.DataSource = dstData.Tables[0];
			
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;
			dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked = true;
			
			dgrdData.Splits[0].DisplayColumns[SEQUENCE_FLD].Locked = true;
			dgrdData.Columns[SEQUENCE_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;

			if (pintProductionLineID > 0)
			{
				dgrdData.AllowAddNew = true;
				dstData.Tables[0].Columns[ITM_ProductTable.PRODUCTIONLINEID_FLD].DefaultValue = pintProductionLineID;
			}
		}

		private void FillItemData(DataRow pdrowData)
		{			
			dgrdData.EditActive = true;
			dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = pdrowData[ITM_ProductTable.CODE_FLD];
			dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
			dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = pdrowData[ITM_ProductTable.REVISION_FLD];
			dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = pdrowData[ITM_ProductTable.PRODUCTID_FLD];
			dgrdData[dgrdData.Row, SEQUENCE_FLD] = dgrdData.Row + 1;
			btnSave.Enabled = true;
		}

		private bool SaveData()
		{
			DCOptionsBO boDCOption = new DCOptionsBO();
			boDCOption.UpdateProductSequence(dstData);
			return true;
		}
		private void btnProductionLine_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnProductionLine_Click()";
			
			try
			{
				SelectProductionLine(true);
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

		private void ProductProductionOrder_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".ManualProductionPlanning_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					return;
				}
				

				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);

				FillDataToGrid(0);
				txtProductionLine.Focus();
				btnSave.Enabled = false;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void txtProductionLine_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_Validating()";
			
			try
			{
				SelectProductionLine(false);
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

		private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtProductionLine_KeyDown()";
			
			try
			{
				if (e.KeyCode == Keys.F4)
					SelectProductionLine(true);
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

		private void dgrdData_ButtonClick(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCondition= new Hashtable();
				if (txtProductionLine.Text.Trim() != string.Empty)
					htbCondition.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
				else
				{
					String[] strParam = new string[2];
					strParam[0] = lblProductionLine.Text;
					strParam[1] = dgrdData.Columns[dgrdData.Col].Caption;
					PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
					txtProductionLine.Focus();
					return;
				}
				if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, dgrdData.Columns[dgrdData.Col].DataField, dgrdData[dgrdData.Row, dgrdData.Col].ToString(), htbCondition, true);
				else
					drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, dgrdData.Columns[dgrdData.Col].DataField, dgrdData.Columns[dgrdData.Col].Text.Trim(), htbCondition, true);
				if (drwResult != null)
					FillItemData(drwResult.Row);
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				if (txtProductionLine.Text.Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					txtProductionLine.Focus();
					return;
				}
				if (dstData == null || dstData.Tables.Count == 0 || dstData.Tables[0].Rows.Count == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PORECEIPT_INPUT_DETAIL, MessageBoxIcon.Warning);
					dgrdData.Focus();
					return;
				}
				ArrayList arrListItem = new ArrayList();
				// check duplicate item in grid
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					string strItem = dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString();
					if (!arrListItem.Contains(strItem))
						arrListItem.Add(strItem);
					else
					{
						PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Warning);
						dgrdData.Row = i;
						dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
						dgrdData.Focus();
						return;
					}
				}
				if (dstData.HasChanges())
				{
					if (SaveData())
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
						// re-load data to retrieve new detail id if any
						FillDataToGrid(Convert.ToInt32(txtProductionLine.Tag));
						btnSave.Enabled = false;
					}
					else
						PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxIcon.Warning);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void dgrdData_KeyDown(object sender, KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_KeyDown()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F4:
						if (btnSave.Enabled)
							dgrdData_ButtonClick(sender, null);
						break;
					case Keys.Delete:
						if ((e.KeyCode == Keys.Delete)&&(dgrdData.SelectedRows.Count > 0))
						{
							if (btnSave.Enabled)
							{
								dgrdData.AllowDelete = true;
								FormControlComponents.DeleteMultiRowsOnTrueDBGrid(dgrdData);
								for (int i = 0; i < dgrdData.RowCount; i++)
									if (dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty)
										dgrdData[i, SEQUENCE_FLD] = i+1;
							}
						}
						break;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD ||
					e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (e.Column.DataColumn.Tag == null || e.Column.DataColumn.Value.ToString() == string.Empty)
					{
						dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = null;
						dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = string.Empty;
						dgrdData[dgrdData.Row, SEQUENCE_FLD] = string.Empty;
						dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTIONLINEID_FLD] = string.Empty;
					}
					else
					{
						FillItemData((DataRow)e.Column.DataColumn.Tag);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD ||
					e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					# region open Product search form 
					if (dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
					{
						if (txtProductionLine.Text.Trim() != string.Empty)
							htbCriteria.Add(PRO_WorkOrderMasterTable.PRODUCTIONLINEID_FLD, int.Parse(txtProductionLine.Tag.ToString()));
						else
						{
							String[] strParam = new string[2];
							strParam[0] = lblProductionLine.Text;
							strParam[1] = e.Column.DataColumn.Caption;
							PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
							txtProductionLine.Focus();
							return;
						}
						drwResult = FormControlComponents.OpenSearchForm(PRODUCT_IN_PRODUCTIONLINE_VIEWNAME, e.Column.DataColumn.DataField, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
						if (drwResult != null)
							e.Column.DataColumn.Tag = drwResult.Row;
						else
							e.Cancel = true;
					}
					#endregion
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void dgrdData_AfterDelete(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterDelete()";
			try
			{
				for (int i = 0; i < dgrdData.RowCount; i++)
					if (dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString() != string.Empty)
						dgrdData[i, SEQUENCE_FLD] = i+1;
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ProductProductionOrder_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".WorkOrder_Closing()";
			try
			{
				if (btnSave.Enabled)
				{
					DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					switch (confirmDialog)
					{
						case DialogResult.Yes:
							try
							{
								if (SaveData())
								{
									PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
									e.Cancel = false;
								}
								else
									e.Cancel = true;
							}
							catch
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
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
				}
			}
		}

	}
}
