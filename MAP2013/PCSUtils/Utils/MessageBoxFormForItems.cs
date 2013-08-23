using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Summary description for MessageBoxFormForItems.
	/// </summary>
	public class MessageBoxFormForItems : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public string FormTitle;
		public string MessageDescription;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		public System.Windows.Forms.Label lblPrimaryVendor;
		public System.Windows.Forms.Label lblListPrice;
		public System.Windows.Forms.Label lblTitle;
		public System.Windows.Forms.Label lblVendorCurrency;
		public System.Windows.Forms.Label lblExchangeRate;
		public System.Windows.Forms.Label lblVendorLoc;
		public System.Windows.Forms.Label lblProductionLine;
		public System.Windows.Forms.Label lblVendorDeliverySchedule;
		public DataTable BugReason;

		public MessageBoxFormForItems()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		private void MessageBoxFormForItems_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			new Security().SetRightForUserOnForm(this, SystemProperty.UserName);

			DataTable dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
			dgrdData.DataSource = BugReason;

			//Restore layout
			FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);

			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MessageBoxFormForItems));
			this.lblTitle = new System.Windows.Forms.Label();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblPrimaryVendor = new System.Windows.Forms.Label();
			this.lblListPrice = new System.Windows.Forms.Label();
			this.lblVendorLoc = new System.Windows.Forms.Label();
			this.lblVendorCurrency = new System.Windows.Forms.Label();
			this.lblExchangeRate = new System.Windows.Forms.Label();
			this.lblProductionLine = new System.Windows.Forms.Label();
			this.lblVendorDeliverySchedule = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(6, 2);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(672, 23);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "The list of items which is not enough information";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgrdData
			// 
			this.dgrdData.AllowUpdate = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(6, 26);
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
			this.dgrdData.Size = new System.Drawing.Size(622, 397);
			this.dgrdData.TabIndex = 1;
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Part Number" +
				"\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Part Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Lack of infor" +
				"mation\" DataField=\"Reason\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols>" +
				"<Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{Fore" +
				"Color:HighlightText;BackColor:Highlight;}Caption{AlignHorz:Center;}Normal{}Style" +
				"25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style" +
				"19{}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{AlignHorz:Near;}Style10{" +
				"AlignHorz:Near;}Style11{}OddRow{}Style13{}Style12{}Style32{}Style33{}Style31{}Fo" +
				"oter{}Style29{AlignHorz:Near;}Style28{AlignHorz:Center;}Style27{}Style26{}Record" +
				"Selector{AlignImage:Center;}Style24{}Style23{AlignHorz:Near;}Style22{AlignHorz:C" +
				"enter;}Style21{}Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;Ali" +
				"gnVert:Center;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;" +
				"}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1," +
				" 1, 1;ForeColor:ControlText;BackColor:Control;}Style7{}Style8{}FilterBar{}Style5" +
				"{}Style4{}Style9{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Center;}" +
				"Style35{AlignHorz:Near;}Style6{}Style1{}Style30{}Style3{}Style2{}</Data></Styles" +
				"><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaption" +
				"Height=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelect" +
				"orWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"" +
				"1\"><ClientRect>0, 0, 618, 393</ClientRect><BorderSide>0</BorderSide><CaptionStyl" +
				"e parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><Eve" +
				"nRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"" +
				"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\"" +
				" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle " +
				"parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\"" +
				" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Recor" +
				"dSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style " +
				"parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Width>128</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"S" +
				"tyle23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGr" +
				"ay,Single</ColumnDivider><Width>201</Width><Height>15</Height><DCIdx>1</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><S" +
				"tyle parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>70</Width><Height>15</" +
				"Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>248</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCol" +
				"s></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Nor" +
				"mal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\"" +
				" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" " +
				"/><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><S" +
				"tyle parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><" +
				"Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" " +
				"/><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /><" +
				"/NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifie" +
				"d</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 618, 393<" +
				"/ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle" +
				" parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(558, 428);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(70, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblPrimaryVendor
			// 
			this.lblPrimaryVendor.Location = new System.Drawing.Point(56, 80);
			this.lblPrimaryVendor.Name = "lblPrimaryVendor";
			this.lblPrimaryVendor.TabIndex = 3;
			this.lblPrimaryVendor.Text = "Primary Vendor";
			this.lblPrimaryVendor.Visible = false;
			// 
			// lblListPrice
			// 
			this.lblListPrice.Location = new System.Drawing.Point(56, 100);
			this.lblListPrice.Name = "lblListPrice";
			this.lblListPrice.TabIndex = 4;
			this.lblListPrice.Text = "Purchasing Price";
			this.lblListPrice.Visible = false;
			// 
			// lblVendorLoc
			// 
			this.lblVendorLoc.Location = new System.Drawing.Point(56, 122);
			this.lblVendorLoc.Name = "lblVendorLoc";
			this.lblVendorLoc.TabIndex = 5;
			this.lblVendorLoc.Text = "Vendor\'s Location";
			this.lblVendorLoc.Visible = false;
			// 
			// lblVendorCurrency
			// 
			this.lblVendorCurrency.Location = new System.Drawing.Point(56, 144);
			this.lblVendorCurrency.Name = "lblVendorCurrency";
			this.lblVendorCurrency.TabIndex = 6;
			this.lblVendorCurrency.Text = "Vendor\'s Currency";
			this.lblVendorCurrency.Visible = false;
			// 
			// lblExchangeRate
			// 
			this.lblExchangeRate.Location = new System.Drawing.Point(222, 130);
			this.lblExchangeRate.Name = "lblExchangeRate";
			this.lblExchangeRate.Size = new System.Drawing.Size(140, 23);
			this.lblExchangeRate.TabIndex = 7;
			this.lblExchangeRate.Text = "Currency Exchange Rate";
			this.lblExchangeRate.Visible = false;
			// 
			// lblProductionLine
			// 
			this.lblProductionLine.Location = new System.Drawing.Point(224, 152);
			this.lblProductionLine.Name = "lblProductionLine";
			this.lblProductionLine.Size = new System.Drawing.Size(140, 23);
			this.lblProductionLine.TabIndex = 8;
			this.lblProductionLine.Text = "Production Line";
			this.lblProductionLine.Visible = false;
			// 
			// lblVendorDeliverySchedule
			// 
			this.lblVendorDeliverySchedule.Location = new System.Drawing.Point(222, 176);
			this.lblVendorDeliverySchedule.Name = "lblVendorDeliverySchedule";
			this.lblVendorDeliverySchedule.Size = new System.Drawing.Size(148, 23);
			this.lblVendorDeliverySchedule.TabIndex = 9;
			this.lblVendorDeliverySchedule.Text = "Vendor\'s Delivery Schedule";
			this.lblVendorDeliverySchedule.Visible = false;
			// 
			// MessageBoxFormForItems
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(634, 455);
			this.Controls.Add(this.lblVendorDeliverySchedule);
			this.Controls.Add(this.lblProductionLine);
			this.Controls.Add(this.lblExchangeRate);
			this.Controls.Add(this.lblVendorCurrency);
			this.Controls.Add(this.lblVendorLoc);
			this.Controls.Add(this.lblListPrice);
			this.Controls.Add(this.lblPrimaryVendor);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MessageBoxFormForItems";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PCS Message Box";
			this.Load += new System.EventHandler(this.MessageBoxFormForItems_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		public MessageBoxFormForItems(string pstrFormTitle, string pstrMessageDes, DataTable pdtbSource)
		{
			FormTitle = pstrFormTitle;
			if (pstrFormTitle != null && pstrFormTitle.Trim() != string.Empty)
				lblTitle.Text = pstrFormTitle;
			MessageDescription = pstrMessageDes;
			BugReason = pdtbSource;
		}

		public MessageBoxFormForItems(DataTable pdtbSource)
		{
			BugReason = pdtbSource;
		}
	}
}
