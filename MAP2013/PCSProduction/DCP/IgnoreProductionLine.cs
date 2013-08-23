using System;
using System.ComponentModel;
using System.Data;
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
	/// Summary description for IgnoreProductionLine.
	/// </summary>
	public class IgnoreProductionLine : Form
	{
		private C1TrueDBGrid dgrdData;
		private Button btnClose;
		private Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private DataTable dtbIgnoreList;

		public DataTable IgnoreList
		{
			get { return dtbIgnoreList; }
			set { dtbIgnoreList = value; }
		}

		private DataTable dtbGridLayOut;
		private DataSet dstData;
		public const string THIS = "PCSProduction.DCP.IgnoreProductionLine";
		const string IGNORED_FLD = "Ignored";

		public IgnoreProductionLine(DataTable pdtbSelectedList)
		{
			InitializeComponent();

			dtbIgnoreList = pdtbSelectedList;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IgnoreProductionLine));
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrdData
			// 
			this.dgrdData.AccessibleDescription = "";
			this.dgrdData.AccessibleName = "";
			this.dgrdData.AllowColMove = false;
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
			this.dgrdData.Size = new System.Drawing.Size(668, 257);
			this.dgrdData.TabIndex = 10;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Code\" DataF" +
				"ield=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Name\" DataField=\"Name\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"Location\" DataField=\"Location\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Balance Planning\" DataField=\"" +
				"BalancePlanning\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"Department\" DataField=\"Department\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ignored\" DataF" +
				"ield=\"Ignored\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn" +
				"></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style" +
				"58{AlignHorz:Center;ForeColor:WindowText;}Style59{AlignHorz:Near;}Style31{}Capti" +
				"on{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}Style25{}Selected{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{}Style15{}Sty" +
				"le16{AlignHorz:Center;ForeColor:WindowText;}Style17{AlignHorz:Near;}Style10{Alig" +
				"nHorz:Near;}Style11{}Style68{}Style13{}Style63{}Style62{}Style61{}Style60{}Style" +
				"67{}Style66{}Style65{AlignHorz:Center;}Style64{AlignHorz:Center;}Style37{}Style3" +
				"4{AlignHorz:Center;ForeColor:WindowText;}Style35{AlignHorz:Center;}OddRow{}Style" +
				"29{AlignHorz:Near;}Style28{AlignHorz:Center;ForeColor:WindowText;}Style27{}Style" +
				"26{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}Style22{Al" +
				"ignHorz:Center;ForeColor:WindowText;}Style21{}Style20{}Inactive{ForeColor:Inacti" +
				"veCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:Tr" +
				"ue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:C" +
				"enter;}Style24{}Style6{}Style1{}Style2{}Style9{}Style8{}FilterBar{}Style5{}Style" +
				"4{}Style69{}Style38{}Style39{}Style36{}Style12{}Group{AlignVert:Center;Border:No" +
				"ne,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style32{}Style33{}Style30{}Style3{" +
				"}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}</Data></Styles><Spli" +
				"ts><C1.Win.C1TrueDBGrid.MergeView AllowColMove=\"False\" Name=\"\" CaptionHeight=\"17" +
				"\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" FilterBar=\"True\" MarqueeStyle" +
				"=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollG" +
				"roup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 664, 253</ClientRect><Borde" +
				"rSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle pa" +
				"rent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filter" +
				"BarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Styl" +
				"e3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" m" +
				"e=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveSty" +
				"le parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><R" +
				"ecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=" +
				"\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\"" +
				" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>51</Width><Height>15</Height><DCIdx>5</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\"" +
				" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style1" +
				"8\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>140</Width><Heigh" +
				"t>15</Height><Locked>True</Locked><Button>True</Button><DCIdx>0</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style pa" +
				"rent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Width>160</Width><Height>15</Height" +
				"><Locked>True</Locked><Button>True</Button><DCIdx>1</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1" +
				"\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>4" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
				"<Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Width>90</Width><Height>15</Height><Locked>True</Locked><DCIdx>3</DCI" +
				"dx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><Na" +
				"medStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><" +
				"Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Sty" +
				"le parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Styl" +
				"e parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Styl" +
				"e parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style par" +
				"ent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Sty" +
				"le parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSp" +
				"lits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSe" +
				"lWidth><ClientArea>0, 0, 664, 253</ClientArea><PrintPageHeaderStyle parent=\"\" me" +
				"=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOK.Location = new System.Drawing.Point(2, 261);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(60, 23);
			this.btnOK.TabIndex = 15;
			this.btnOK.Text = "OK";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(606, 261);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "&Close";
			// 
			// IgnoreProductionLine
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(668, 287);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.dgrdData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "IgnoreProductionLine";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ignore Production Line";
			this.Load += new System.EventHandler(this.IgnoreProductionLine_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void IgnoreProductionLine_Load(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".IgnoreProductionLine_Load()";
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

				//load all production line
				ProductionLineBO boProductionLine = new ProductionLineBO();
				dstData = boProductionLine.List();
				if (dtbIgnoreList.Rows.Count > 0)
				{
					foreach (DataRow drowData in dtbIgnoreList.Rows)
					{
						dstData.Tables[0].Select(PRO_ProductionLineTable.PRODUCTIONLINEID_FLD
							+ "=" + drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString().Trim())[0][IGNORED_FLD] = true;
					}
				}
				dgrdData.DataSource = dstData.Tables[0];

				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);
				foreach (C1DisplayColumn c1Col in dgrdData.Splits[0].DisplayColumns)
					if (c1Col.DataColumn.DataField != IGNORED_FLD)
						c1Col.Locked = true;
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
				if (e.Column.DataColumn.DataField == IGNORED_FLD)
				{
					bool blnIgnored = Convert.ToBoolean(e.Column.DataColumn.Value);
					if (!blnIgnored)
					{
						foreach (DataRow drowData in dtbIgnoreList.Rows)
						{
							if (drowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString()
								== dgrdData[dgrdData.Row, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString())
							{
								dtbIgnoreList.Rows.Remove(drowData);
								break;
							}
						}
					}
					else
					{
						DataRow drowIgnore = dtbIgnoreList.NewRow();
						drowIgnore[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD] = dgrdData[dgrdData.Row, PRO_ProductionLineTable.PRODUCTIONLINEID_FLD].ToString();
						drowIgnore[MST_WorkCenterTable.WORKCENTERID_FLD] = dgrdData[dgrdData.Row, MST_WorkCenterTable.WORKCENTERID_FLD].ToString();
						dtbIgnoreList.Rows.Add(drowIgnore);
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
	}
}
