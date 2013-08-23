using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using PCSComSale.Order.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSSale.Order
{
	/// <summary>
	/// Summary description for SOAvoidDupleSaleOrdersDelivery.
	/// </summary>
	public class SOAvoidDupleSaleOrdersDelivery : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox cboYear;
		private System.Windows.Forms.ComboBox cboMonth;
		private System.Windows.Forms.Label lblYear;
		private System.Windows.Forms.Label lblMonth;
		private System.Windows.Forms.TextBox txtCustomer;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.Button btnCustomer;
		private System.Windows.Forms.TextBox txtCustomerName;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnSave;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private const string THIS = "PCSSale.Order.SOAvoidDupleSaleOrdersDelivery";
		private System.Windows.Forms.Button btnClose;		
		private DataTable dtbGridLayOut;
		private DataTable dtbData;
		private const string SELECT = "Selected";
		public SOAvoidDupleSaleOrdersDelivery()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SOAvoidDupleSaleOrdersDelivery));
			this.cboYear = new System.Windows.Forms.ComboBox();
			this.cboMonth = new System.Windows.Forms.ComboBox();
			this.lblYear = new System.Windows.Forms.Label();
			this.lblMonth = new System.Windows.Forms.Label();
			this.txtCustomer = new System.Windows.Forms.TextBox();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.btnCustomer = new System.Windows.Forms.Button();
			this.txtCustomerName = new System.Windows.Forms.TextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			this.SuspendLayout();
			// 
			// cboYear
			// 
			this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYear.Location = new System.Drawing.Point(88, 8);
			this.cboYear.Name = "cboYear";
			this.cboYear.Size = new System.Drawing.Size(128, 21);
			this.cboYear.TabIndex = 0;
			// 
			// cboMonth
			// 
			this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboMonth.Location = new System.Drawing.Point(88, 32);
			this.cboMonth.Name = "cboMonth";
			this.cboMonth.Size = new System.Drawing.Size(128, 21);
			this.cboMonth.TabIndex = 1;
			// 
			// lblYear
			// 
			this.lblYear.ForeColor = System.Drawing.Color.Maroon;
			this.lblYear.Location = new System.Drawing.Point(16, 8);
			this.lblYear.Name = "lblYear";
			this.lblYear.Size = new System.Drawing.Size(64, 21);
			this.lblYear.TabIndex = 2;
			this.lblYear.Text = "Year";
			this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMonth
			// 
			this.lblMonth.ForeColor = System.Drawing.Color.Maroon;
			this.lblMonth.Location = new System.Drawing.Point(16, 32);
			this.lblMonth.Name = "lblMonth";
			this.lblMonth.Size = new System.Drawing.Size(64, 21);
			this.lblMonth.TabIndex = 3;
			this.lblMonth.Text = "Month";
			this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCustomer
			// 
			this.txtCustomer.Location = new System.Drawing.Point(88, 56);
			this.txtCustomer.Name = "txtCustomer";
			this.txtCustomer.TabIndex = 4;
			this.txtCustomer.Text = "";
			this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
			this.txtCustomer.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomer_Validating);
			// 
			// lblCustomer
			// 
			this.lblCustomer.ForeColor = System.Drawing.Color.Maroon;
			this.lblCustomer.Location = new System.Drawing.Point(16, 56);
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.Size = new System.Drawing.Size(64, 21);
			this.lblCustomer.TabIndex = 5;
			this.lblCustomer.Text = "Customer";
			this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCustomer
			// 
			this.btnCustomer.Location = new System.Drawing.Point(192, 56);
			this.btnCustomer.Name = "btnCustomer";
			this.btnCustomer.Size = new System.Drawing.Size(24, 20);
			this.btnCustomer.TabIndex = 6;
			this.btnCustomer.Text = "...";
			this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
			// 
			// txtCustomerName
			// 
			this.txtCustomerName.Location = new System.Drawing.Point(218, 56);
			this.txtCustomerName.Name = "txtCustomerName";
			this.txtCustomerName.ReadOnly = true;
			this.txtCustomerName.Size = new System.Drawing.Size(302, 20);
			this.txtCustomerName.TabIndex = 7;
			this.txtCustomerName.Text = "";
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(528, 55);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(96, 21);
			this.btnSearch.TabIndex = 8;
			this.btnSearch.Text = "S&earch";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
			this.dgrdData.Location = new System.Drawing.Point(8, 88);
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
			this.dgrdData.Size = new System.Drawing.Size(616, 280);
			this.dgrdData.TabIndex = 9;
			this.dgrdData.Text = "c1TrueDBGrid1";
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Line\" DataF" +
				"ield=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"SO No.\" DataField=\"SO_SaleOrderMasterCode\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"SO Line\" DataField=\"SaleOrderLine\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Deliv" +
				"ery Line\" DataField=\"SO_DeliveryScheduleLine\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"Code\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Name\" Dat" +
				"aField=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Delivery Date, Time\" DataField=\"ScheduleD" +
				"ate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
				"Quantity\" DataField=\"DeliveryQuantity\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"Auto Cancel\" DataField=\"Selected\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Desig" +
				"n.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight" +
				";}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Selected{For" +
				"eColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{}Style70{Alig" +
				"nHorz:Center;}Style71{AlignHorz:Near;}Style74{}Style75{}FilterBar{}Heading{Wrap:" +
				"True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert" +
				":Center;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Center;}Style17{A" +
				"lignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style" +
				"29{AlignHorz:Near;}Style28{AlignHorz:Center;}Style26{}Style25{}Style9{}Style8{}S" +
				"tyle24{}Style23{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{" +
				"AlignHorz:Center;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}St" +
				"yle36{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Near;}Style32{}Style" +
				"33{}Style30{}Style49{}Style48{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40{" +
				"AlignHorz:Center;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}St" +
				"yle46{AlignHorz:Center;}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{" +
				"AlignHorz:Center;}RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}St" +
				"yle52{AlignHorz:Center;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style" +
				"57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{}Style" +
				"60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Center;}Group{Al" +
				"ignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><S" +
				"plits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHei" +
				"ght=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorW" +
				"idth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\">" +
				"<ClientRect>0, 0, 612, 276</ClientRect><BorderSide>0</BorderSide><CaptionStyle p" +
				"arent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRo" +
				"wStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Sty" +
				"le13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me" +
				"=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle par" +
				"ent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" />" +
				"<OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSe" +
				"lector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style par" +
				"ent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>40</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style" +
				"23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>135</Width><Height>15</Height><DCIdx>1</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style" +
				" parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Width>63</Width><Height>15</Heig" +
				"ht><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\"" +
				" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>7" +
				"1</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Singl" +
				"e</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>4</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style par" +
				"ent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style5" +
				"1\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><Co" +
				"lumnDivider>DarkGray,Single</ColumnDivider><Width>177</Width><Height>15</Height>" +
				"<DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>86</" +
				"Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><" +
				"FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
				"e61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent" +
				"=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</" +
				"ColumnDivider><Width>112</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent" +
				"=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>88</Width><Height>15</Height><DCI" +
				"dx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>69</Widt" +
				"h><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn></internalCols></C1.Win.C" +
				"1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Styl" +
				"e parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pa" +
				"rent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style par" +
				"ent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=" +
				"\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent" +
				"=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style par" +
				"ent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles" +
				"><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><D" +
				"efaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 612, 276</ClientArea>" +
				"<PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" m" +
				"e=\"Style15\" /></Blob>";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.Location = new System.Drawing.Point(8, 376);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 10;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(549, 376);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// SOAvoidDupleSaleOrdersDelivery
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(632, 406);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtCustomerName);
			this.Controls.Add(this.btnCustomer);
			this.Controls.Add(this.lblCustomer);
			this.Controls.Add(this.txtCustomer);
			this.Controls.Add(this.lblMonth);
			this.Controls.Add(this.lblYear);
			this.Controls.Add(this.cboMonth);
			this.Controls.Add(this.cboYear);
			this.Name = "SOAvoidDupleSaleOrdersDelivery";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Avoid Duple SaleOrders Delivery";
			this.Load += new System.EventHandler(this.SOAvoidDupleSaleOrdersDelivery_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// SOAvoidDupleSaleOrdersDelivery_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void SOAvoidDupleSaleOrdersDelivery_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOAvoidDupleSaleOrdersDelivery_Load()";
			try
			{
				//Set form security
				Security objSecurity = new Security();		
				
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					return;
				}
				//Init Combo box
				cboMonth.Items.Clear();
				cboMonth.Items.Add(string.Empty);
				for (int i = 1; i < 13; i++)
				{
					cboMonth.Items.Add(i.ToString());
				}
				cboMonth.SelectedIndex = -1;

				cboYear.Items.Clear();
				cboYear.Items.Add(string.Empty);
				for (int i = 2000; i < 2051; i++)
				{
					cboYear.Items.Add(i.ToString());
				}
				cboYear.SelectedIndex = -1;
				//get grid layout
				dtbGridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
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
		/// OpenSearchForm for selecting Customer
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// <param name="pblnAlwaysShowDialog"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private bool SelectCustomer(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				const string VIEW_VENDOR = "V_VendorCustomer";
				const string CUSTOMER_COLUMN = "Customer";
				//Add condition for customer
				htbCriteria.Add(CUSTOMER_COLUMN, 0);
				//Call OpenSearchForm for selecting Customer
				drwResult = FormControlComponents.OpenSearchForm(VIEW_VENDOR, MST_PartyTable.CODE_FLD, txtCustomer.Text, htbCriteria, pblnAlwaysShowDialog);
				
				// If has Master location matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					txtCustomer.Text = drwResult[MST_PartyTable.CODE_FLD].ToString();
					txtCustomer.Tag = int.Parse(drwResult[MST_PartyTable.PARTYID_FLD].ToString());
					txtCustomerName.Text = drwResult[MST_PartyTable.NAME_FLD].ToString();
					//Reset modify status
					txtCustomer.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{	
					txtCustomer.Tag = null;
					txtCustomer.Focus();
					return false;
				}

				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// btnCustomer_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void btnCustomer_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnCustomer_Click()";
			try
			{
				SelectCustomer(METHOD_NAME, true);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
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
		/// txtCustomer_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void txtCustomer_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_Validating()";
			try
			{
				if (txtCustomer.Text == string.Empty)
				{
					txtCustomerName.Text = string.Empty;
					txtCustomer.Tag = null;
					return;
				}

				if (!txtCustomer.Modified) return;
				e.Cancel = !SelectCustomer(METHOD_NAME, false);
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
		/// Config the grid data
		/// </summary>
		/// <param name="pblnLock"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void ConfigGrid(bool pblnLock)
		{
			const string METHOD_NAME = THIS + ".ConfigGrid()";
			try
			{
				
				dgrdData.Enabled = true;
				for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].Locked = true;
				}
				//dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Visible = true;
				dgrdData.Splits[0].DisplayColumns[SELECT].Locked = !pblnLock;
				dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].DataColumn.NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
				dgrdData.Splits[0].DisplayColumns[PRO_WorkOrderDetailTable.LINE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;

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
		/// ValidateDataToSearch
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private bool ValidateDataToSearch()
		{
			const string METHOD_NAME = THIS + ".ValidateDataToSearch()";
			try
			{
				if (FormControlComponents.CheckMandatory(cboYear))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboYear.Focus();				
					return false;
				}
				if (FormControlComponents.CheckMandatory(cboMonth))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					cboMonth.Focus();				
					return false;
				}
				if (FormControlComponents.CheckMandatory(txtCustomer))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
					txtCustomer.Focus();				
					return false;
				}
				return true;
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
		/// btnSearch_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSearch_Click()";
			try
			{
				if (ValidateDataToSearch())
				{
					DateTime dtmDateToSearch = new DateTime(int.Parse(cboYear.SelectedItem.ToString()), int.Parse(cboMonth.SelectedItem.ToString()), 1);
					AvoidDupleSODeliveryBO boAvoidDupleSODelivery = new AvoidDupleSODeliveryBO();
					dtbData = boAvoidDupleSODelivery.SearchDupleSO(dtmDateToSearch, int.Parse(txtCustomer.Tag.ToString()));
					dtbData.Columns.Add(SELECT, typeof(bool));
					dgrdData.DataSource = dtbData;
					for (int i = 0; i < dgrdData.RowCount; i++)
					{
						dgrdData[i, SELECT] = false;
						dgrdData[i, PRO_WorkOrderDetailTable.LINE_FLD] = i + 1;
					}
					//Restore layout
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayOut);			
					ConfigGrid(true);
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


			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
		/// <summary>
		/// txtCustomer_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <Date>Friday, Feb 17 2006</Date>
		private void txtCustomer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCustomer_KeyDown()";
			try
			{
				if(e.KeyCode == Keys.F4)
					if(btnCustomer.Enabled)
						btnCustomer_Click(sender,e);
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
		/// btnSave_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, Feb 17 2006</date>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				//Validating data
				if (dgrdData.RowCount == 0)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_PO_APPROVE_NO_DATA_IN_GRID, MessageBoxIcon.Warning);
					dgrdData.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				int intChecked = 0;
				for (int j = 0; j < dgrdData.RowCount; j++)
				{
					if (dgrdData[j, SELECT].ToString() != false.ToString())
					{
						intChecked++;
					}
				}
				if (intChecked == 0) 
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_AVOID_SO_SELECT_ATLEAST_ONE, MessageBoxIcon.Warning);
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SELECT]);
					dgrdData.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				if (intChecked == dgrdData.RowCount)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_AVOID_SO_CANNOT_SELECT_ALL, MessageBoxIcon.Warning);
					//MessageBox.Show("You cannot select all!");
					dgrdData.Row = 0;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SELECT]);
					dgrdData.Focus();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				string strDeliveryScheduleID = " (";
				for (int i = 0; i < dgrdData.RowCount; i++)
				{
					if (dgrdData[i, SELECT].ToString() == true.ToString())
					{
						strDeliveryScheduleID = strDeliveryScheduleID + dgrdData[i, SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString() + ",";
					}
				}
				strDeliveryScheduleID = strDeliveryScheduleID + "0)";
				AvoidDupleSODeliveryBO boAvoidDupleSODelivery = new AvoidDupleSODeliveryBO();
				boAvoidDupleSODelivery.DeleteDupleSODelivery(strDeliveryScheduleID);
				//Re-load data to grid
				btnSearch_Click(null, null);
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

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}
	}
}
