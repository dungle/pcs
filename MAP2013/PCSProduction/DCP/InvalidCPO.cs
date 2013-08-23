using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.ErrorMsg.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduction.DCP
{
	/// <summary>
	/// Summary description for InvalidCPO.
	/// </summary>
	public class InvalidCPO : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid tgridOverCapacityWC;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.ContextMenu ctxmnuClipboard;
		private System.Windows.Forms.MenuItem mnuCopy;
		private System.Windows.Forms.MenuItem mnuSelectAll;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InvalidCPO()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private DataTable m_dtbInvalidWOLineAndCPO;
		public InvalidCPO(DataTable pdtbInvalidWOLineAndCPO) : this()
		{
			m_dtbInvalidWOLineAndCPO = pdtbInvalidWOLineAndCPO;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InvalidCPO));
			this.btnClose = new System.Windows.Forms.Button();
			this.tgridOverCapacityWC = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblMessage = new System.Windows.Forms.Label();
			this.ctxmnuClipboard = new System.Windows.Forms.ContextMenu();
			this.mnuCopy = new System.Windows.Forms.MenuItem();
			this.mnuSelectAll = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.tgridOverCapacityWC)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(566, 374);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 22);
			this.btnClose.TabIndex = 60;
			this.btnClose.Text = "&Close";
			// 
			// tgridOverCapacityWC
			// 
			this.tgridOverCapacityWC.AllowUpdate = false;
			this.tgridOverCapacityWC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tgridOverCapacityWC.CaptionHeight = 17;
			this.tgridOverCapacityWC.CollapseColor = System.Drawing.Color.Black;
			this.tgridOverCapacityWC.ContextMenu = this.ctxmnuClipboard;
			this.tgridOverCapacityWC.ExpandColor = System.Drawing.Color.Black;
			this.tgridOverCapacityWC.GroupByCaption = "Drag a column header here to group by that column";
			this.tgridOverCapacityWC.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.tgridOverCapacityWC.Location = new System.Drawing.Point(6, 32);
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
			this.tgridOverCapacityWC.Size = new System.Drawing.Size(620, 334);
			this.tgridOverCapacityWC.TabIndex = 59;
			this.tgridOverCapacityWC.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"CPO No\" Dat" +
				"aField=\"CPOID\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"WOLineID\" DataField=\"WorkOrderDetailID\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Start Date\" DataField=\"StartDate\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Due Da" +
				"te\" DataField=\"DueDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Error Reason\" DataField=\"ErrorReaso" +
				"n\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Pa" +
				"rt Number\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1Tru" +
				"eDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackCo" +
				"lor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;" +
				"}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}FilterBar{}Headin" +
				"g{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Bac" +
				"kColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Styl" +
				"e17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}" +
				"Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style26{}Style25{}Style9{}Style8" +
				"{}Style24{}Style23{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style" +
				"22{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}S" +
				"tyle36{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style3" +
				"3{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Tahoma, 11world;}Style41{Alig" +
				"nHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{}Style47{" +
				"AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{BackColor:Aqua;}Style59{AlignHor" +
				"z:Near;}Style58{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Style51{}Style" +
				"50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Style55{}St" +
				"yle56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}St" +
				"yle61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near" +
				";}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Data><" +
				"/Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" AllowRowSizing=\"None\" Cap" +
				"tionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"D" +
				"ottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGrou" +
				"p=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 616, 330</ClientRect><BorderSi" +
				"de>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle paren" +
				"t=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBar" +
				"Style parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\"" +
				" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"" +
				"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle " +
				"parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Reco" +
				"rdSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Se" +
				"lected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me" +
				"=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Width>62</Width><Height>15</Height><DCIdx>0</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" />" +
				"<Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"" +
				"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style " +
				"parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible>" +
				"<ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /" +
				"><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</" +
				"Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>" +
				"8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
				"><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15" +
				"</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle pare" +
				"nt=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHea" +
				"derStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
				"yle50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><W" +
				"idth>65</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Sty" +
				"le53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterSty" +
				"le parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Width>172</Width><Height>15</Height><DCIdx>5</DCIdx></C1" +
				"DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyl" +
				"es><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style p" +
				"arent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style pare" +
				"nt=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style paren" +
				"t=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style paren" +
				"t=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"He" +
				"ading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style pare" +
				"nt=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1<" +
				"/horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth>" +
				"<ClientArea>0, 0, 616, 330</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style" +
				"14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(6, 6);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(620, 20);
			this.lblMessage.TabIndex = 61;
			this.lblMessage.Text = "The following CPO and/or Work Order Lines are not valid. Please check and fix bef" +
				"ore running DCP again !";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ctxmnuClipboard
			// 
			this.ctxmnuClipboard.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.mnuSelectAll,
																							this.mnuCopy});
			// 
			// mnuCopy
			// 
			this.mnuCopy.Index = 1;
			this.mnuCopy.Text = "Copy";
			this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
			// 
			// mnuSelectAll
			// 
			this.mnuSelectAll.Index = 0;
			this.mnuSelectAll.Text = "Select All";
			this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
			// 
			// InvalidCPO
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 403);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tgridOverCapacityWC);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "InvalidCPO";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Invalid CPOs and Work Order Lines";
			this.Load += new System.EventHandler(this.InvalidCPO_Load);
			((System.ComponentModel.ISupportInitialize)(this.tgridOverCapacityWC)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private const string THIS = "PCSProduction.DCP.InvalidCPO";		

		private void InvalidCPO_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".InvalidCPO_Load()";

			const string STARTDATE_COL = "StartDate";
			const string DUEDATE_COL = "DueDate";
			const string QUANTITY_COL = "Quantity";
			const string ERRORCODE_COL = "ErrorCode";
			const string ERRORREASON_COL = "ErrorReason";

			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				if (m_dtbInvalidWOLineAndCPO != null)
				{
					m_dtbInvalidWOLineAndCPO.Columns.Add(ERRORREASON_COL,typeof(string));
					foreach (DataRow drow in m_dtbInvalidWOLineAndCPO.Rows)
					{
						drow[ERRORREASON_COL] = ErrorMessageBO.GetErrorMessage(Convert.ToInt32(drow[ERRORCODE_COL]));
					}
					DataTable dtbLayout = FormControlComponents.StoreGridLayout(tgridOverCapacityWC);
					tgridOverCapacityWC.DataSource = m_dtbInvalidWOLineAndCPO;
					FormControlComponents.RestoreGridLayout(tgridOverCapacityWC,dtbLayout);
					tgridOverCapacityWC.Columns[STARTDATE_COL].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
					tgridOverCapacityWC.Columns[DUEDATE_COL].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
					tgridOverCapacityWC.Columns[QUANTITY_COL].NumberFormat =Constants.DECIMAL_NUMBERFORMAT;
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
	}
}
