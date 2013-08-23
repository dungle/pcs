using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for OutOfCapacityWC.
	/// </summary>
	public class OutOfCapacityWC : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridOverCapacityWC;
		private System.Windows.Forms.Label lblMessage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu ctxmnuClipboard;
		private System.Windows.Forms.MenuItem mnuCopy;
		private System.Windows.Forms.MenuItem mnuSelectAll;
		private System.Windows.Forms.Button btnShiftPattern;
		private System.Windows.Forms.Button btnWCCapacity;

		private const string THIS = "PCSProduction.DCP.OutOfCapacityWC";		

		public OutOfCapacityWC()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OutOfCapacityWC));
			this.tgridOverCapacityWC = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ctxmnuClipboard = new System.Windows.Forms.ContextMenu();
			this.mnuSelectAll = new System.Windows.Forms.MenuItem();
			this.mnuCopy = new System.Windows.Forms.MenuItem();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnShiftPattern = new System.Windows.Forms.Button();
			this.btnWCCapacity = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.tgridOverCapacityWC)).BeginInit();
			this.SuspendLayout();
			// 
			// tgridOverCapacityWC
			// 
			this.tgridOverCapacityWC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tgridOverCapacityWC.CaptionHeight = 17;
			this.tgridOverCapacityWC.CollapseColor = System.Drawing.Color.Black;
			this.tgridOverCapacityWC.ContextMenu = this.ctxmnuClipboard;
			this.tgridOverCapacityWC.ExpandColor = System.Drawing.Color.Black;
			this.tgridOverCapacityWC.GroupByCaption = "Drag a column header here to group by that column";
			this.tgridOverCapacityWC.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridOverCapacityWC.Location = new System.Drawing.Point(4, 32);
			this.tgridOverCapacityWC.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.tgridOverCapacityWC.Name = "tgridOverCapacityWC";
			this.tgridOverCapacityWC.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.tgridOverCapacityWC.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.tgridOverCapacityWC.PreviewInfo.ZoomFactor = 75;
			this.tgridOverCapacityWC.PrintInfo.ShowOptionsDialog = false;
			this.tgridOverCapacityWC.RecordSelectorWidth = 16;
			this.tgridOverCapacityWC.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.tgridOverCapacityWC.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.tgridOverCapacityWC.RowHeight = 15;
			this.tgridOverCapacityWC.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.tgridOverCapacityWC.Size = new System.Drawing.Size(524, 302);
			this.tgridOverCapacityWC.TabIndex = 0;
			this.tgridOverCapacityWC.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"WorkCenterI" +
				"D\" DataField=\"WorkCenterID\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Work Center Code\" DataField=\"Code\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Work Center Name\" DataField" +
				"=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Over Days\" DataField=\"OverDays\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Over Percent\" DataField=\"OverPercent\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGri" +
				"d.Design.ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Style31{}Captio" +
				"n{AlignHorz:Center;}Style27{}Normal{Font:Tahoma, 11world;}Selected{ForeColor:Hig" +
				"hlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{}Style15{}Styl" +
				"e16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Sty" +
				"le12{}Style13{}Style38{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;" +
				"}Style32{}Style33{}OddRow{}Footer{}Style29{AlignHorz:Near;}Style28{AlignHorz:Nea" +
				"r;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{}Style25{}S" +
				"tyle24{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{}Inacti" +
				"ve{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aq" +
				"ua;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:Cont" +
				"rolText;AlignVert:Center;}Style9{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near" +
				";}Style43{}FilterBar{}Style42{}Style45{}Style4{}Style44{}Style8{}Style39{}Style3" +
				"6{}Style5{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;" +
				"}Style7{}Style6{}Style1{}Style30{}Style3{}Style2{}</Data></Styles><Splits><C1.Wi" +
				"n.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Col" +
				"umnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" De" +
				"fRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>" +
				"0, 0, 520, 298</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"Style" +
				"2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle paren" +
				"t=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><Foo" +
				"terStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /" +
				"><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highlig" +
				"htRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle" +
				" parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"" +
				"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\"" +
				" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
				"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Sty" +
				"le parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><E" +
				"ditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
				"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visib" +
				"le><ColumnDivider>DarkGray,Single</ColumnDivider><Width>97</Width><Height>15</He" +
				"ight><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style3" +
				"2\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
				">203</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style3" +
				"5\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me" +
				"=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn" +
				"></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style par" +
				"ent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Headin" +
				"g\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" " +
				"me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me" +
				"=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me" +
				"=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Re" +
				"cordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" " +
				"me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><" +
				"Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0" +
				", 0, 520, 298</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintP" +
				"ageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// ctxmnuClipboard
			// 
			this.ctxmnuClipboard.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.mnuSelectAll,
																							this.mnuCopy});
			// 
			// mnuSelectAll
			// 
			this.mnuSelectAll.Index = 0;
			this.mnuSelectAll.Text = "Select All";
			this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
			// 
			// mnuCopy
			// 
			this.mnuCopy.Index = 1;
			this.mnuCopy.Text = "Copy";
			this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(468, 342);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 22);
			this.btnClose.TabIndex = 58;
			this.btnClose.Text = "&Close";
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(4, 6);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(524, 20);
			this.lblMessage.TabIndex = 62;
			this.lblMessage.Text = "The following Work Centers are out of capacity. Please check and adjust before ru" +
				"nning DCP";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnShiftPattern
			// 
			this.btnShiftPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnShiftPattern.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnShiftPattern.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnShiftPattern.Location = new System.Drawing.Point(4, 342);
			this.btnShiftPattern.Name = "btnShiftPattern";
			this.btnShiftPattern.Size = new System.Drawing.Size(88, 22);
			this.btnShiftPattern.TabIndex = 64;
			this.btnShiftPattern.Text = "&Shift Pattern";
			this.btnShiftPattern.Click += new System.EventHandler(this.btnShiftPattern_Click);
			// 
			// btnWCCapacity
			// 
			this.btnWCCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWCCapacity.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnWCCapacity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnWCCapacity.Location = new System.Drawing.Point(94, 342);
			this.btnWCCapacity.Name = "btnWCCapacity";
			this.btnWCCapacity.Size = new System.Drawing.Size(126, 22);
			this.btnWCCapacity.TabIndex = 65;
			this.btnWCCapacity.Text = "W&ork Center Capacity";
			this.btnWCCapacity.Click += new System.EventHandler(this.btnWCCapacity_Click);
			// 
			// OutOfCapacityWC
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(534, 369);
			this.Controls.Add(this.btnWCCapacity);
			this.Controls.Add(this.btnShiftPattern);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tgridOverCapacityWC);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "OutOfCapacityWC";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Capacity Status After Planning";
			this.Load += new System.EventHandler(this.OutOfCapacityWC_Load);
			((System.ComponentModel.ISupportInitialize)(this.tgridOverCapacityWC)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private DataTable m_dtbOverCapacityWC;
		private int m_intCycleOptionID;

		public DataTable OverCapacityWC
		{
			set
			{
				m_dtbOverCapacityWC = value;
			}
		}

		public int CycleOptionID
		{
			set
			{
				m_intCycleOptionID = value;
			}
		}

		private void OutOfCapacityWC_Load(object sender, System.EventArgs e)
		{
			const string OVERDAYS_COL = "OverDays";
			const string OVERPERCENT_COL = "OverPercent";

			const string METHOD_NAME = THIS + ".OutOfCapacityWC_Load()";

			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				if (m_dtbOverCapacityWC != null)
				{
					DataTable dtbLayout = FormControlComponents.StoreGridLayout(tgridOverCapacityWC);
					tgridOverCapacityWC.DataSource = m_dtbOverCapacityWC;
					FormControlComponents.RestoreGridLayout(tgridOverCapacityWC,dtbLayout);
					tgridOverCapacityWC.Columns[OVERDAYS_COL].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
					tgridOverCapacityWC.Columns[OVERPERCENT_COL].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
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

		private void mnuCopy_Click(object sender, System.EventArgs e)
		{
			string  strTemp = string.Empty;   //string to be copied to the clipboard

			if (tgridOverCapacityWC.SelectedRows.Count > 0 ) 
			{
				foreach (int row in tgridOverCapacityWC.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn col in tgridOverCapacityWC.SelectedCols)
						strTemp = strTemp + col.CellText(row) + "\t"; 
					strTemp = strTemp + "\r\n";
				}
			} //
			System.Windows.Forms.Clipboard.SetDataObject(strTemp, false);		
		}

		private void mnuSelectAll_Click(object sender, System.EventArgs e)
		{
			for (int i=0; i < tgridOverCapacityWC.RowCount; i++)
			{
				tgridOverCapacityWC.SelectedRows.Add(i);
			}		
		}

		private void btnWorkDayCalendar_Click(object sender, System.EventArgs e)
		{
		}

		private void btnShiftPattern_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnShiftPattern_Click()";

			try
			{
				ShiftPattern frmShiftPattern = new ShiftPattern();
				frmShiftPattern.ShowDialog(this);
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

		private void btnWCCapacity_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnWCCapacity_Click()";

			try
			{
				int intWorkCenterID = Convert.ToInt32(tgridOverCapacityWC.Columns[MST_WorkCenterTable.WORKCENTERID_FLD].Value);
				WorkCenterCapacity frmWCCapacity = new WorkCenterCapacity(intWorkCenterID,m_intCycleOptionID);
				frmWCCapacity.ShowDialog(this);
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
