using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for SelectItems.
	/// </summary>
	public class SelectItems : Form
	{
		private C1TrueDBGrid dgrdData;
		private Button btnHelp;
		private Button btnClose;
		private Button btnOK;
		const string THIS = "PCSMaterials.ActualCost.SelectItems";
		private const string PART_NUMBER = "PartNumber";
		private const string PART_NAME = "PartName";
		private const string MODEL = "Model";
		private const string UM = "UM";
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		int intProductID = 0;
		string strCode = string.Empty;
		string strDescription = string.Empty;
		DataSet dstData = new DataSet();
		public DataRowView drvReturnDataRowView;
		DataTable dtbGridLayout = new DataTable();

		public SelectItems(int pintProductID, string pstrCode, string pstrDescription)
		{
			InitializeComponent();

			intProductID = pintProductID;
			strCode = pstrCode;
			strDescription = pstrDescription;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SelectItems));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowColMove = false;
			this.dgrdData.AllowSort = false;
			this.dgrdData.AllowUpdate = false;
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.Dock = System.Windows.Forms.DockStyle.Top;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FilterBar = true;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(0, 0);
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
			this.dgrdData.Size = new System.Drawing.Size(644, 267);
			this.dgrdData.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.dgrdData.TabIndex = 26;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.DoubleClick += new System.EventHandler(this.dgrdData_DoubleClick);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part No.\" D" +
				"ataField=\"PartNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Part Name\" DataField=\"PartName\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Model\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Stock UM\" DataFi" +
				"eld=\"UM\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"Category\" DataField=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>S" +
				"tyle58{AlignHorz:Center;ForeColor:Maroon;}Style59{AlignHorz:Near;}Style52{AlignH" +
				"orz:Center;ForeColor:Maroon;}Style53{AlignHorz:Near;}Style54{}Caption{AlignHorz:" +
				"Center;}Style56{}Normal{Font:Microsoft Sans Serif, 8.25pt;}Selected{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{}Style15{}Sty" +
				"le16{AlignHorz:Center;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style69{}" +
				"Style12{}Style13{}EvenRow{BackColor:Aqua;}Style63{}Style62{}Style61{}Style60{}St" +
				"yle67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Center;}OddRow{}Style8" +
				"{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}RecordSelector{Align" +
				"Image:Center;}Footer{}Style21{}Style55{}Style57{}Inactive{ForeColor:InactiveCapt" +
				"ionText;BackColor:InactiveCaption;}Style72{}Style73{}Style70{AlignHorz:Near;}Sty" +
				"le71{AlignHorz:Near;}Style74{}Style75{}Style20{}Heading{Wrap:True;AlignVert:Cent" +
				"er;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}FilterBar{" +
				"}Style4{}Style9{}Style68{}Style11{}Style5{}Group{BackColor:ControlDark;Border:No" +
				"ne,,0, 0, 0, 0;AlignVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data>" +
				"</Styles><Splits><C1.Win.C1TrueDBGrid.MergeView AllowColMove=\"False\" Name=\"\" Cap" +
				"tionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True" +
				"\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" V" +
				"erticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 640, 263</Cli" +
				"entRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><" +
				"EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Sty" +
				"le8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Fo" +
				"oter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle pare" +
				"nt=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" " +
				"/><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me" +
				"=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Selecte" +
				"dStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><int" +
				"ernalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style p" +
				"arent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Width>71</Width><Height>15</Height" +
				"><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>133" +
				"</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Width>233</Width><Height>15</Height><DCIdx>1</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style pare" +
				"nt=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><Col" +
				"umnDivider>DarkGray,Single</ColumnDivider><Width>122</Width><Height>15</Height><" +
				"DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><" +
				"Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>59</W" +
				"idth><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols></C1.Wi" +
				"n.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><S" +
				"tyle parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style" +
				" parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style " +
				"parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style pare" +
				"nt=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style par" +
				"ent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style " +
				"parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSty" +
				"les><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout" +
				"><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 640, 263</ClientAr" +
				"ea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"" +
				"\" me=\"Style15\" /></Blob>";
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(520, 272);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 32;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(580, 272);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 33;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOK.Location = new System.Drawing.Point(2, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(60, 23);
			this.btnOK.TabIndex = 31;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// SelectItems
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(644, 297);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.dgrdData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "SelectItems";
			this.Text = "Select Item";
			this.Load += new System.EventHandler(this.SelectItems_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void SelectItems_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SelectItems_Load()";
			try
			{
				BomBO boBOM = new BomBO();
				DataSet dstAllItem = boBOM.ListComponents();
				DataTable dtbComponents = boBOM.ListComponents(intProductID).Tables[0];
				dstData.Tables.Add(BuildSchema());
				DataRow drowItem = boBOM.GetItemInfo(intProductID);
				dstData.Tables[0].ImportRow(drowItem);
				foreach (DataRow drowData in dtbComponents.Rows)
					BuildData(dstData.Tables[0], dstAllItem.Tables[0], drowData);
				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
				BindData();
				FilterData();
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

		private DataTable BuildSchema()
		{
			DataTable dtbData = new DataTable();
			dtbData.Columns.Add(new DataColumn(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD, typeof(string)));
			dtbData.Columns.Add(new DataColumn(ITM_ProductTable.PRODUCTID_FLD, typeof(int)));
			dtbData.Columns.Add(new DataColumn(PART_NUMBER, typeof(string)));
			dtbData.Columns.Add(new DataColumn(PART_NAME, typeof(string)));
			dtbData.Columns.Add(new DataColumn(MODEL, typeof(string)));
			dtbData.Columns.Add(new DataColumn(UM, typeof(string)));
			dtbData.Columns.Add(new DataColumn(ITM_ProductTable.STOCKUMID_FLD, typeof(string)));

			return dtbData;
		}
		private void BuildData(DataTable pdtbData, DataTable pdtbAllChild, DataRow pdrowNew)
		{
			DataRow drowItem = pdtbData.NewRow();
			foreach (DataColumn dcolData in pdtbData.Columns)
				drowItem[dcolData.ColumnName] = pdrowNew[dcolData.ColumnName];
			pdtbData.Rows.Add(drowItem);
			// get child
			DataRow[] drowsChild = GetChild(pdtbAllChild, int.Parse(drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString()));
			foreach (DataRow drowChild in drowsChild)
				BuildData(pdtbData, pdtbAllChild, drowChild);
		}

		private DataRow[] GetChild(DataTable pdtbAllChilds, int pintParentID)
		{
			return pdtbAllChilds.Select("ParentID = " + pintParentID);
		}
		private void FilterData()
		{
			string strFilter = string.Empty;
			if (strCode != string.Empty)
				strFilter = PART_NUMBER + " LIKE '" + strCode + "%'";
			else if (strDescription != string.Empty)
				strFilter = PART_NAME + " LIKE '" + strDescription + "%'";
			try
			{
				dstData.Tables[0].DefaultView.RowFilter = strFilter;
			}
			catch
			{
				dstData.Tables[0].DefaultView.RowFilter = string.Empty;
			}
		}
		private void ClearAllFilter()
		{
			this.dstData.Tables[0].DefaultView.RowFilter = String.Empty;
		}

		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".SelectItems_Load()";
			try
			{
				switch (e.KeyCode)
				{
					case Keys.F2:
						ClearAllFilter();
						break;
					case Keys.Enter:
						btnOK_Click(null, null);
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

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnOK_Click()";
			try
			{
				ReturnValue();
				this.DialogResult = DialogResult.OK;
				this.Close();
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
		private void ReturnValue()
		{
			DataTable dtTmp = (DataTable)dgrdData.DataSource;
			if (dtTmp.DefaultView.Count > 0)
			{
				CurrencyManager cm;
				cm = (CurrencyManager) dgrdData.BindingContext[dgrdData.DataSource];
				drvReturnDataRowView = (DataRowView)cm.Current;
			}
			else
				drvReturnDataRowView = null;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void BindData()
		{
			dgrdData.DataSource = dstData.Tables[0];
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
		}

		private void dgrdData_DoubleClick(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_DoubleClick()";
			try
			{
				if (dgrdData.Row >= 0)
				{
					ReturnValue();
					this.DialogResult = DialogResult.OK;
					this.Close();
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
