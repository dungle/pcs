using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using PCSComUtils.Common;
using PCSUtils.Utils;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;


namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for NotConfiguredWC.
	/// </summary>
	public class NotConfiguredWC : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridNotConfiguredWC;

		private const string THIS = "PCSProduction.DCP.NotConfiguredWC";
		private System.Windows.Forms.ContextMenu ctxmnuClipboard;
		private System.Windows.Forms.MenuItem mnuSelectAll;
		private System.Windows.Forms.MenuItem mnuCopy;		
		private DataTable m_dtbNotConfiguredWC;

		public NotConfiguredWC(DataTable pdtbNotConfiguredWC) : this ()
		{
			m_dtbNotConfiguredWC = pdtbNotConfiguredWC;
		}

		public NotConfiguredWC()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NotConfiguredWC));
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.tgridNotConfiguredWC = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ctxmnuClipboard = new System.Windows.Forms.ContextMenu();
			this.mnuSelectAll = new System.Windows.Forms.MenuItem();
			this.mnuCopy = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.tgridNotConfiguredWC)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(6, 6);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(524, 20);
			this.lblMessage.TabIndex = 65;
			this.lblMessage.Text = "The following Work Centers have not been configured. Please check before running " +
				"DCP";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(470, 324);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 22);
			this.btnClose.TabIndex = 64;
			this.btnClose.Text = "&Close";
			// 
			// tgridNotConfiguredWC
			// 
			this.tgridNotConfiguredWC.AllowUpdate = false;
			this.tgridNotConfiguredWC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tgridNotConfiguredWC.CaptionHeight = 17;
			this.tgridNotConfiguredWC.CollapseColor = System.Drawing.Color.Black;
			this.tgridNotConfiguredWC.ContextMenu = this.ctxmnuClipboard;
			this.tgridNotConfiguredWC.ExpandColor = System.Drawing.Color.Black;
			this.tgridNotConfiguredWC.GroupByCaption = "Drag a column header here to group by that column";
			this.tgridNotConfiguredWC.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridNotConfiguredWC.Location = new System.Drawing.Point(6, 32);
			this.tgridNotConfiguredWC.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.tgridNotConfiguredWC.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
			this.tgridNotConfiguredWC.Name = "tgridNotConfiguredWC";
			this.tgridNotConfiguredWC.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.tgridNotConfiguredWC.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.tgridNotConfiguredWC.PreviewInfo.ZoomFactor = 75;
			this.tgridNotConfiguredWC.PrintInfo.ShowOptionsDialog = false;
			this.tgridNotConfiguredWC.RecordSelectorWidth = 16;
			this.tgridNotConfiguredWC.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.tgridNotConfiguredWC.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.tgridNotConfiguredWC.RowHeight = 15;
			this.tgridNotConfiguredWC.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.tgridNotConfiguredWC.Size = new System.Drawing.Size(524, 284);
			this.tgridNotConfiguredWC.TabIndex = 63;
			this.tgridNotConfiguredWC.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"WorkCenterI" +
				"D\" DataField=\"WorkCenterID\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"WorkCenter Code\" DataField=\"Code\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"WorkCenter Name\" DataField=\"" +
				"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Description\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"WorkCenterID\" DataField=\"WorkCenterID\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueD" +
				"BGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColo" +
				"r:Highlight;}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}Style25{}Sty" +
				"le24{}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style1" +
				"7{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style45{}Sty" +
				"le12{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style26{}RecordSe" +
				"lector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near" +
				";}Style21{}Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVer" +
				"t:Center;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Even" +
				"Row{BackColor:Aqua;}Style6{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1," +
				" 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style3{}Style4{}Style7{}Style8" +
				"{}Style1{}Style5{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{}Filte" +
				"rBar{}Style42{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style44{}St" +
				"yle9{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Near;}Style35{AlignH" +
				"orz:Near;}Style32{}Style33{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C" +
				"1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\"" +
				" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16" +
				"\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientR" +
				"ect>0, 0, 520, 280</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=\"S" +
				"tyle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle p" +
				"arent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" />" +
				"<FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style1" +
				"2\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Hig" +
				"hlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowS" +
				"tyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" " +
				"me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Nor" +
				"mal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" />" +
				"<Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Width>104</Width><Height>15</Height><DCIdx>4</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\"" +
				" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style2" +
				"4\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>123</Width><Heigh" +
				"t>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivide" +
				"r><Width>251</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me" +
				"=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Width>174</Width><Height>15</Height><DCIdx>3</DCIdx" +
				"></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><Name" +
				"dStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><St" +
				"yle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style" +
				" parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style " +
				"parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style " +
				"parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style paren" +
				"t=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style" +
				" parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSpli" +
				"ts>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelW" +
				"idth><ClientArea>0, 0, 520, 280</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"" +
				"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
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
			// NotConfiguredWC
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(536, 351);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tgridNotConfiguredWC);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "NotConfiguredWC";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Not Configured Work Centers";
			this.Load += new System.EventHandler(this.NotConfiguredWC_Load);
			((System.ComponentModel.ISupportInitialize)(this.tgridNotConfiguredWC)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void NotConfiguredWC_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".NotConfiguredWC_Load()";

			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				if (m_dtbNotConfiguredWC != null)
				{
					DataTable dtbLayout = FormControlComponents.StoreGridLayout(tgridNotConfiguredWC);
					tgridNotConfiguredWC.DataSource = m_dtbNotConfiguredWC;
					FormControlComponents.RestoreGridLayout(tgridNotConfiguredWC,dtbLayout);
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

		private void mnuSelectAll_Click(object sender, System.EventArgs e)
		{
			for (int i=0; i < tgridNotConfiguredWC.RowCount; i++)
			{
				tgridNotConfiguredWC.SelectedRows.Add(i);
			}				
		}

		private void mnuCopy_Click(object sender, System.EventArgs e)
		{
			string  strTemp = string.Empty;   //string to be copied to the clipboard

			if (tgridNotConfiguredWC.SelectedRows.Count > 0 ) 
			{
				foreach (int row in tgridNotConfiguredWC.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn col in tgridNotConfiguredWC.SelectedCols)
						strTemp = strTemp + col.CellText(row) + "\t"; 
					strTemp = strTemp + "\r\n";
				}
			} //
			System.Windows.Forms.Clipboard.SetDataObject(strTemp, false);				
		}
	}
}
