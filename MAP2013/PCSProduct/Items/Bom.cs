using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProduct.Items
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Bom : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblLable1;
		private System.Windows.Forms.Button btnFindItem;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.Label lblLable2;
		private System.Windows.Forms.Label lblLable3;
		private System.Windows.Forms.Label lblLable4;
		private System.Windows.Forms.Label lblLable5;
		private System.Windows.Forms.TextBox txtItemCode;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.CheckBox chkMakeItem;
		private System.Windows.Forms.TextBox txtBOMDes;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private const string THIS = "PCSProduct.Items.Bom";

		private string StockUMCode = "StockCode";
		private string CaptionLine = "Line", CaptionOperation = "Operation";
		private C1.Win.C1Input.C1DateEdit dtmEffectiveDate;

		private DataSet dstBOMDetail = new DataSet();
		public ITM_ProductVO voProduct = new ITM_ProductVO();

		private int intRowCountDelete = 0;
		private EnumAction enumAction = EnumAction.Default;
		private bool blnHasError = false;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid dgrdData;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.TextBox txtPartName;
		private C1.Win.C1Input.C1NumericEdit txtIncrement;
		private DataTable dtbGridLayout;
		private const string DECIMAL_NUMBERFORMAT_SMALL = "##############,0.00000";

		public Bom()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Bom));
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.dgrdData = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblLable1 = new System.Windows.Forms.Label();
			this.btnFindItem = new System.Windows.Forms.Button();
			this.lblRevision = new System.Windows.Forms.Label();
			this.lblLable2 = new System.Windows.Forms.Label();
			this.lblLable3 = new System.Windows.Forms.Label();
			this.lblLable4 = new System.Windows.Forms.Label();
			this.lblLable5 = new System.Windows.Forms.Label();
			this.chkMakeItem = new System.Windows.Forms.CheckBox();
			this.txtItemCode = new System.Windows.Forms.TextBox();
			this.txtBOMDes = new System.Windows.Forms.TextBox();
			this.dtmEffectiveDate = new C1.Win.C1Input.C1DateEdit();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.txtPartName = new System.Windows.Forms.TextBox();
			this.txtIncrement = new C1.Win.C1Input.C1NumericEdit();
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmEffectiveDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIncrement)).BeginInit();
			this.SuspendLayout();
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnEdit.Location = new System.Drawing.Point(126, 426);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(60, 23);
			this.btnEdit.TabIndex = 17;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOK.Location = new System.Drawing.Point(714, 426);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(60, 23);
			this.btnOK.TabIndex = 20;
			this.btnOK.Text = "&Close";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(653, 426);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 19;
			this.btnHelp.Text = "&Help";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnDelete.Location = new System.Drawing.Point(187, 426);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 18;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSave.Location = new System.Drawing.Point(65, 426);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(60, 23);
			this.btnSave.TabIndex = 16;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAdd.Location = new System.Drawing.Point(4, 426);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 15;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// dgrdData
			// 
			this.dgrdData.AllowAddNew = true;
			this.dgrdData.AllowDelete = true;
			this.dgrdData.AllowSort = false;
			this.dgrdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrdData.CaptionHeight = 17;
			this.dgrdData.CollapseColor = System.Drawing.Color.Black;
			this.dgrdData.ExpandColor = System.Drawing.Color.Black;
			this.dgrdData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
			this.dgrdData.GroupByCaption = "Drag a column header here to group by that column";
			this.dgrdData.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.dgrdData.Location = new System.Drawing.Point(3, 76);
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
			this.dgrdData.Size = new System.Drawing.Size(771, 346);
			this.dgrdData.TabIndex = 14;
			this.dgrdData.Text = "3";
			this.dgrdData.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_ButtonClick);
			this.dgrdData.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.dgrdData_AfterColUpdate);
			this.dgrdData.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.dgrdData_BeforeColUpdate);
			this.dgrdData.Leave += new System.EventHandler(this.dgrdData_Leave);
			this.dgrdData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgrdData_KeyDown);
			this.dgrdData.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Line\" DataF" +
				"ield=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Component\" DataField=\"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Component Name\" DataField=\"" +
				"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Quantity\" DataField=\"Quantity\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"Operation\" DataField=\"Operation\" NumberFormat=" +
				"\"Edit Mask\"><ValueItems Translate=\"True\" /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Stock Unit\" DataField=\"StockCode\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Eff Begin Date\" DataField=\"" +
				"EffectiveBeginDate\"><ValueItems Translate=\"True\" /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Eff End Date\" DataField=\"EffectiveEndDate\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Lead Time " +
				"Offset(s)\" DataField=\"LeadTimeOffset\"><ValueItems /><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"Eff Start Day\" DataField=\"EffectiveBeginDay\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Eff End" +
				" Day\" DataField=\"EffectiveEndDay\"><ValueItems /><GroupInfo /></C1DataColumn><C1D" +
				"ataColumn Level=\"0\" Caption=\"Shrink %\" DataField=\"Shrink\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Alternative\" DataField=\"Alt" +
				"ernative\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Category\" DataField=\"ITM_CategoryCode\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Vendor\" DataField=\"MST_PartyCode\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid." +
				"Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:High" +
				"light;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78" +
				"{}Style79{AlignHorz:Far;}Style85{AlignHorz:Far;}Editor{}Style72{}Style73{AlignHo" +
				"rz:Far;}Style70{AlignHorz:Near;}Style71{AlignHorz:Near;}Style76{AlignHorz:Near;}" +
				"Style77{AlignHorz:Near;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{}St" +
				"yle80{}Style83{AlignHorz:Near;}Style82{AlignHorz:Near;}Style89{AlignHorz:Near;}S" +
				"tyle88{AlignHorz:Near;}Style108{}Style109{}Style104{}Style105{}Style106{AlignHor" +
				"z:Near;}Style107{AlignHorz:Near;}Style100{AlignHorz:Near;}Style101{AlignHorz:Nea" +
				"r;}Style102{}Style103{}Style94{AlignHorz:Near;}Style95{AlignHorz:Near;}Style96{}" +
				"Style97{}Style90{}Style91{AlignHorz:Near;}Style92{}Style93{}Style98{}Style99{}He" +
				"ading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText" +
				";BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}" +
				"Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Selec" +
				"ted{ForeColor:HighlightText;BackColor:Highlight;}Style22{AlignHorz:Near;ForeColo" +
				"r:Maroon;}Style27{}Style28{AlignHorz:Near;ForeColor:Black;}Style24{ForeColor:Lig" +
				"htCoral;}Style9{}Style8{}Style26{}Style29{AlignHorz:Near;}Style5{}Style4{}Style7" +
				"{}Style6{}Style25{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}Odd" +
				"Row{}Style38{}Style39{}Style36{}FilterBar{}Style37{}Style34{AlignHorz:Near;ForeC" +
				"olor:Maroon;}Style35{AlignHorz:Near;}Style32{}Style33{}Style49{}Style48{}Style30" +
				"{}Style31{}Normal{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;ForeColor:Maro" +
				"on;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHor" +
				"z:Near;}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:Near;}" +
				"RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Ne" +
				"ar;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHor" +
				"z:Center;}Style69{}Style68{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;" +
				"AlignVert:Center;}Style1{}Style63{}Style62{}Style61{AlignHorz:Center;}Style60{}S" +
				"tyle67{AlignHorz:Center;}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near" +
				";}Style111{}Style110{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView Nam" +
				"e=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Marquee" +
				"Style=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalSc" +
				"rollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 767, 342</ClientRect><" +
				"BorderSide>0</BorderSide><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorSty" +
				"le parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><F" +
				"ilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=" +
				"\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Headi" +
				"ng\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inacti" +
				"veStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\"" +
				" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle pa" +
				"rent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"St" +
				"yle1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDiv" +
				"ider>DarkGray,Single</ColumnDivider><Width>33</Width><Height>15</Height><DCIdx>0" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>136</Width><" +
				"Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>167</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style" +
				"1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>52</Width><Height>15</Height><DCIdx>2</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style5" +
				"2\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>58</Width><Heig" +
				"ht>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Width>67</Width><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"St" +
				"yle1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>78</Width><Height>15</Height><DC" +
				"Idx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>53</Wi" +
				"dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style4" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>65</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"S" +
				"tyle1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Width>105</Width><Height>15</Height><DCIdx" +
				">9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle94\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>60</Width>" +
				"<Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" " +
				"/><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Width>134</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Sty" +
				"le1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Width>137</Width><Height>15</Height><DCIdx>8" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>73</Width><H" +
				"eight>15</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>74</Width><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"Style" +
				"1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>57</Width><Height>15</Height><DCIdx>12</" +
				"DCIdx></C1DisplayColumn></internalCols></C1.Win.C1TrueDBGrid.MergeView></Splits>" +
				"<NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" " +
				"/><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><" +
				"Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><S" +
				"tyle parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><S" +
				"tyle parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style " +
				"parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><" +
				"Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><hor" +
				"zSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRe" +
				"cSelWidth><ClientArea>0, 0, 767, 342</ClientArea><PrintPageHeaderStyle parent=\"\"" +
				" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// cboCCN
			// 
			this.cboCCN.AddItemSeparator = ';';
			this.cboCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboCCN.Caption = "";
			this.cboCCN.CaptionHeight = 17;
			this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.cboCCN.ColumnCaptionHeight = 17;
			this.cboCCN.ColumnFooterHeight = 17;
			this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
			this.cboCCN.ContentHeight = 15;
			this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
			this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.cboCCN.EditorHeight = 15;
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(690, 4);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(84, 21);
			this.cboCCN.TabIndex = 1;
			this.cboCCN.TextChanged += new System.EventHandler(this.cboCCN_TextChanged);
			this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
				"ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
				"cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
				"d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
				"Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
				"olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
				" 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
				"ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// lblLable1
			// 
			this.lblLable1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLable1.ForeColor = System.Drawing.Color.Maroon;
			this.lblLable1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable1.Location = new System.Drawing.Point(660, 6);
			this.lblLable1.Name = "lblLable1";
			this.lblLable1.Size = new System.Drawing.Size(30, 19);
			this.lblLable1.TabIndex = 0;
			this.lblLable1.Text = "CCN";
			this.lblLable1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnFindItem
			// 
			this.btnFindItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFindItem.Location = new System.Drawing.Point(225, 4);
			this.btnFindItem.Name = "btnFindItem";
			this.btnFindItem.Size = new System.Drawing.Size(24, 20);
			this.btnFindItem.TabIndex = 4;
			this.btnFindItem.Text = "...";
			this.btnFindItem.Click += new System.EventHandler(this.btnFindItem_Click);
			// 
			// lblRevision
			// 
			this.lblRevision.ForeColor = System.Drawing.Color.Maroon;
			this.lblRevision.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRevision.Location = new System.Drawing.Point(252, 6);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(37, 18);
			this.lblRevision.TabIndex = 5;
			this.lblRevision.Text = "Model";
			// 
			// lblLable2
			// 
			this.lblLable2.ForeColor = System.Drawing.Color.Maroon;
			this.lblLable2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable2.Location = new System.Drawing.Point(4, 28);
			this.lblLable2.Name = "lblLable2";
			this.lblLable2.Size = new System.Drawing.Size(86, 20);
			this.lblLable2.TabIndex = 7;
			this.lblLable2.Text = "Part Name";
			// 
			// lblLable3
			// 
			this.lblLable3.ForeColor = System.Drawing.Color.Maroon;
			this.lblLable3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable3.Location = new System.Drawing.Point(4, 6);
			this.lblLable3.Name = "lblLable3";
			this.lblLable3.Size = new System.Drawing.Size(86, 20);
			this.lblLable3.TabIndex = 2;
			this.lblLable3.Text = "Part No.";
			// 
			// lblLable4
			// 
			this.lblLable4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable4.Location = new System.Drawing.Point(4, 50);
			this.lblLable4.Name = "lblLable4";
			this.lblLable4.Size = new System.Drawing.Size(90, 22);
			this.lblLable4.TabIndex = 11;
			this.lblLable4.Text = "BOM Description";
			// 
			// lblLable5
			// 
			this.lblLable5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLable5.Location = new System.Drawing.Point(371, 30);
			this.lblLable5.Name = "lblLable5";
			this.lblLable5.Size = new System.Drawing.Size(58, 16);
			this.lblLable5.TabIndex = 9;
			this.lblLable5.Text = "Increment";
			// 
			// chkMakeItem
			// 
			this.chkMakeItem.Enabled = false;
			this.chkMakeItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkMakeItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkMakeItem.Location = new System.Drawing.Point(466, 52);
			this.chkMakeItem.Name = "chkMakeItem";
			this.chkMakeItem.Size = new System.Drawing.Size(84, 21);
			this.chkMakeItem.TabIndex = 13;
			this.chkMakeItem.Text = "Make Item";
			// 
			// txtItemCode
			// 
			this.txtItemCode.Location = new System.Drawing.Point(90, 4);
			this.txtItemCode.MaxLength = 24;
			this.txtItemCode.Name = "txtItemCode";
			this.txtItemCode.Size = new System.Drawing.Size(134, 20);
			this.txtItemCode.TabIndex = 3;
			this.txtItemCode.Text = "";
			this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
			this.txtItemCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemCode_Validating);
			// 
			// txtBOMDes
			// 
			this.txtBOMDes.Location = new System.Drawing.Point(90, 51);
			this.txtBOMDes.MaxLength = 200;
			this.txtBOMDes.Name = "txtBOMDes";
			this.txtBOMDes.Size = new System.Drawing.Size(374, 20);
			this.txtBOMDes.TabIndex = 12;
			this.txtBOMDes.Text = "";
			// 
			// dtmEffectiveDate
			// 
			// 
			// dtmEffectiveDate.Calendar
			// 
			this.dtmEffectiveDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dtmEffectiveDate.CustomFormat = "dd-MM-yyyy h:mm tt";
			this.dtmEffectiveDate.EmptyAsNull = true;
			this.dtmEffectiveDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.dtmEffectiveDate.Location = new System.Drawing.Point(24, 142);
			this.dtmEffectiveDate.Name = "dtmEffectiveDate";
			this.dtmEffectiveDate.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																										   new C1.Win.C1Input.ValueInterval(new System.DateTime(2006, 1, 1, 0, 0, 0, 0), new System.DateTime(2007, 1, 1, 0, 0, 0, 0), true, true)});
			this.dtmEffectiveDate.Size = new System.Drawing.Size(142, 20);
			this.dtmEffectiveDate.TabIndex = 26;
			this.dtmEffectiveDate.Tag = null;
			this.dtmEffectiveDate.Visible = false;
			this.dtmEffectiveDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.dtmEffectiveDate.ValueChanged += new System.EventHandler(this.dtmEffectiveDate_ValueChanged);
			this.dtmEffectiveDate.Enter += new System.EventHandler(this.dtmEffectiveDate_Enter);
			this.dtmEffectiveDate.DropDownClosed += new DropDownClosedEventHandler(this.dtmEffectiveDate_DropDownClosed);
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(293, 4);
			this.txtModel.Name = "txtModel";
			this.txtModel.ReadOnly = true;
			this.txtModel.Size = new System.Drawing.Size(72, 20);
			this.txtModel.TabIndex = 31;
			this.txtModel.TabStop = false;
			this.txtModel.Text = "";
			// 
			// txtPartName
			// 
			this.txtPartName.Location = new System.Drawing.Point(90, 27);
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.ReadOnly = true;
			this.txtPartName.Size = new System.Drawing.Size(275, 20);
			this.txtPartName.TabIndex = 32;
			this.txtPartName.TabStop = false;
			this.txtPartName.Text = "";
			// 
			// txtIncrement
			// 
			this.txtIncrement.AcceptsEscape = false;
			this.txtIncrement.CustomFormat = "##,0";
			this.txtIncrement.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.txtIncrement.Location = new System.Drawing.Point(424, 27);
			this.txtIncrement.MaxLength = 8;
			this.txtIncrement.Name = "txtIncrement";
			this.txtIncrement.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)(((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
				| C1.Win.C1Input.NumericInputKeyFlags.X)));
			this.txtIncrement.PostValidation.Intervals.AddRange(new C1.Win.C1Input.ValueInterval[] {
																									   new C1.Win.C1Input.ValueInterval(new System.Decimal(new int[] {
																																										 1,
																																										 0,
																																										 0,
																																										 0}), new System.Decimal(new int[] {
																																																			   255,
																																																			   0,
																																																			   0,
																																																			   0}), true, true)});
			this.txtIncrement.Size = new System.Drawing.Size(40, 20);
			this.txtIncrement.TabIndex = 10;
			this.txtIncrement.Tag = null;
			this.txtIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtIncrement.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.txtIncrement.Enter += new System.EventHandler(this.OnEnterControl);
			this.txtIncrement.Leave += new System.EventHandler(this.OnLeaveControl);
			// 
			// Bom
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(778, 453);
			this.Controls.Add(this.dgrdData);
			this.Controls.Add(this.txtPartName);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtBOMDes);
			this.Controls.Add(this.txtItemCode);
			this.Controls.Add(this.txtIncrement);
			this.Controls.Add(this.dtmEffectiveDate);
			this.Controls.Add(this.chkMakeItem);
			this.Controls.Add(this.btnFindItem);
			this.Controls.Add(this.lblRevision);
			this.Controls.Add(this.lblLable2);
			this.Controls.Add(this.lblLable3);
			this.Controls.Add(this.lblLable4);
			this.Controls.Add(this.lblLable5);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblLable1);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnDelete);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "Bom";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bill Of Material";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Bom_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Bom_Closing);
			this.Load += new System.EventHandler(this.Bom_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgrdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtmEffectiveDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIncrement)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//**************************************************************************              
		///    <Description>
		///		Reset all control and get caption of grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ResetForm()
		{
			const string METHOD_NAME = THIS + ".ResetForm()";
			try
			{
				txtBOMDes.Text = string.Empty;
				txtItemCode.Text = string.Empty;
				txtModel.Text = string.Empty;
				txtPartName.Text = string.Empty;
				txtIncrement.Value = null;
				enumAction = EnumAction.Default;

				CaptionLine = dgrdData.Splits[0].DisplayColumns[CaptionLine].DataColumn.Caption;
				CaptionOperation = dgrdData.Splits[0].DisplayColumns[CaptionOperation].DataColumn.Caption;

				txtItemCode.Focus();
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
		
		//**************************************************************************              
		///    <Description>
		///       Load all data in to Combo CNN 
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, February 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void LoadComboCCN()
		{
			const string METHOD_NAME = THIS + ".LoadComboCCN()";
			try
			{
				UtilsBO boUtils = new UtilsBO();
				DataSet dstCCN = boUtils.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN,dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
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


		//**************************************************************************              
		///    <Description>
		///     Get data of the bom from datarow after select the item
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void GetDataOfBomFromDataRow(DataRow drowData)
		{
			const string METHOD_NAME = THIS + ".GetDataOfBomFromDataRow()";
			try
			{
				dstBOMDetail = new DataSet();
				voProduct.ProductID = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
				txtItemCode.Text = drowData[ITM_ProductTable.CODE_FLD].ToString();
				txtModel.Text = drowData[ITM_ProductTable.REVISION_FLD].ToString();
				txtPartName.Text = drowData[ITM_ProductTable.DESCRIPTION_FLD].ToString();
				chkMakeItem.Checked = bool.Parse(drowData[ITM_ProductTable.MAKEITEM_FLD].ToString());
				txtBOMDes.Text = drowData[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString();
				txtIncrement.Value = drowData[ITM_ProductTable.BOMINCREMENT_FLD];

				if (dstBOMDetail.Tables.Count == 0)
				{
					CreateTableForGrid(2);
					dstBOMDetail.Tables.Add(new BomBO().ListBOMDetailsOfProduct(voProduct.ProductID).Copy());
					dstBOMDetail.Tables.Add(new BomBO().ListHierarchyOfProduct(voProduct.ProductID).Copy());
				}
				if (txtIncrement.Value.ToString().Trim() == "0")
				{
					txtIncrement.Value = 1;
				}
				intRowCountDelete =0;
				dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				EnableDisableButton(enumAction);
				if (enumAction != EnumAction.Add)
				{
					btnEdit.Enabled = true;
					dgrdData.AllowDelete = false;
					btnDelete.Enabled = true;
				}
				txtItemCode.Modified = false;
				ConfigGridLayout();
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

		
		//**************************************************************************              
		///    <Description>
		///		 Find item from product list following CCN
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnFindItem_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnFindSaleOrder_Click()";
			try 
			{
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
					if (enumAction == EnumAction.Add)
					{
						htbConditon.Add(v_ITMBOM_Product.HASBOM_FLD, 0);
					}
					else
					{
						htbConditon.Add(v_ITMBOM_Product.HASBOM_FLD, 1);
					}
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.CODE_FLD, txtItemCode.Text, htbConditon, true);
				if (drwResult != null)
				{
					GetDataOfBomFromDataRow(drwResult.Row);
					voProduct.Code = txtItemCode.Text.Trim();
				}
				else
				{
					txtItemCode.Focus();
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


		//**************************************************************************              
		///    <Description>
		///		Get product's infor and bind them on the grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void GetProductInfors(DataRow drowData)
		{
			const string METHOD_NAME = THIS + ".GetProductInfors()";
			try
			{
				txtItemCode.Text = drowData[ITM_ProductTable.CODE_FLD].ToString().Trim();
				txtModel.Text = drowData[ITM_ProductTable.REVISION_FLD].ToString().Trim();
				txtPartName.Text = drowData[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
				chkMakeItem.Checked = bool.Parse(drowData[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim());
				txtBOMDes.Text = drowData[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
				txtIncrement.Value = drowData[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim();
			
				voProduct.ProductID = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
				voProduct.Code = drowData[ITM_ProductTable.CODE_FLD].ToString().Trim();
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


		//**************************************************************************              
		///    <Description>
		///		Create template table for datagrid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void CreateTableForGrid(int intType)
		{
			const string METHOD_NAME = THIS + ".CreateTableForGrid()-";
			try
			{
				dstBOMDetail = new DataSet();
				if (intType == 1)
				{
					dstBOMDetail.Tables.Add(ITM_BOMTable.TABLE_NAME);
					//Bom table
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(CaptionLine);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.BOMID_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.PRODUCTID_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.COMPONENTID_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_ProductTable.CODE_FLD);

					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_ProductTable.DESCRIPTION_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_ProductTable.REVISION_FLD);
			
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(StockUMCode);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_ProductTable.STOCKUMID_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.QUANTITY_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.ROUTINGID_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(CaptionOperation);

					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.LEADTIMEOFFSET_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.ALTERNATIVE_FLD);

					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.EFFECTIVEBEGINDATE_FLD, typeof(DateTime));
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.EFFECTIVEENDDATE_FLD, typeof(DateTime));
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.EFFECTIVEBEGINDAY_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.EFFECTIVEENDDAY_FLD);
					dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Columns.Add(ITM_BOMTable.SHRINK_FLD);

					dstBOMDetail.Tables.Add(ITM_HierarchyTable.TABLE_NAME);

					//Hierarchy table
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.HIERARCHYID_FLD);
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.DESTINATION_FLD);
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.SOURCE_FLD);
				}
			
				if (intType == 0)
				{
					dstBOMDetail.Tables.Add(ITM_HierarchyTable.TABLE_NAME);

					//Hierarchy table
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.HIERARCHYID_FLD);
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.DESTINATION_FLD);
					dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Columns.Add(ITM_HierarchyTable.SOURCE_FLD);
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


		//**************************************************************************              
		///    <Description>
		///		Bind data into dataset form grid and controls on form
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool GetDataFormGrid()
		{
			const string METHOD_NAME = THIS + ".GetDataFormGrid()";
			try
			{
				int intDeleteBefore =0;
				for (int j =0; j <dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows.Count; j++)
				{
					if (dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j].RowState == DataRowState.Deleted)
					{
						dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows[j].Delete();
						intDeleteBefore++;
					}
					else if ((dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j].RowState == DataRowState.Modified) || (dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j].RowState == DataRowState.Added))
					{
						dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j][ITM_BOMTable.PRODUCTID_FLD] = voProduct.ProductID;
						dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j][ITM_BOMTable.COMPONENTID_FLD]= dgrdData[j-intDeleteBefore, ITM_BOMTable.COMPONENTID_FLD].ToString().Trim();
						if (dgrdData[j - intDeleteBefore, ITM_BOMTable.ROUTINGID_FLD].ToString() != string.Empty)
						{
							try
							{
								dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j][ITM_BOMTable.ROUTINGID_FLD] = dgrdData.Columns[ ITM_BOMTable.ROUTINGID_FLD].CellValue(j-intDeleteBefore);
							}
							catch
							{
								dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows[j][ITM_BOMTable.ROUTINGID_FLD] = DBNull.Value;
							}
						}
					}
					if (j - intDeleteBefore > dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows.Count - intDeleteBefore -1)
					{
						DataRow drowHier = dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].NewRow();

						drowHier[ITM_HierarchyTable.SOURCE_FLD] = voProduct.ProductID;
						drowHier[ITM_HierarchyTable.DESTINATION_FLD] = dgrdData[j-intDeleteBefore, ITM_BOMTable.COMPONENTID_FLD].ToString().Trim();
						dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows.Add(drowHier);
					}
					else
					{
						if (dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows[j].RowState != DataRowState.Deleted)
						{
							dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows[j][ITM_HierarchyTable.SOURCE_FLD] = voProduct.ProductID;
							dstBOMDetail.Tables[ITM_HierarchyTable.TABLE_NAME].Rows[j][ITM_HierarchyTable.DESTINATION_FLD] = dgrdData[j - intDeleteBefore, ITM_BOMTable.COMPONENTID_FLD].ToString().Trim();
						}
					}
				}

				//check business
				BomBO boBOM = new BomBO();
				ArrayList arrResutl = boBOM.CheckBussinessForBOM(dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME], voProduct.ProductID);
				if (arrResutl.Count > 0)
				{
					//violate business rules
					PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_WRONGBUSINESS, MessageBoxIcon.Error);
					return false;
				}
				//boBOM.Dispose();
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


		//**************************************************************************              
		///    <Description>
		///		Config for TrueDBGrid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Monday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void ConfigGridLayout()
		{
			const string METHOD_NAME = THIS + ".ConfigGridLayout()";
			try
			{
				//invisible columns
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.BOMID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.PRODUCTID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.STOCKUMID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.COMPONENTID_FLD].Visible = false;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.ROUTINGID_FLD].Visible = false;

				//lock columns
				dgrdData.Splits[0].DisplayColumns[CaptionLine].Locked = true;
				dgrdData.Splits[0].DisplayColumns[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD].Locked = true;
				dgrdData.Splits[0].DisplayColumns[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].Locked = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.REVISION_FLD].Locked  = true;
				dgrdData.Splits[0].DisplayColumns[StockUMCode].Locked = true;
	
				//set new properties for columns
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD].Button = true;
				dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.DESCRIPTION_FLD].Button = true;

				//set datetime control
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].DataColumn.Editor = dtmEffectiveDate;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEENDDATE_FLD].DataColumn.Editor = dtmEffectiveDate;

				//set width for column
				dgrdData.Splits[0].DisplayColumns[CaptionLine].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.QUANTITY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.ALTERNATIVE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.LEADTIMEOFFSET_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEENDDATE_FLD].Style.HorizontalAlignment = AlignHorzEnum.Center;
		
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;
				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.EFFECTIVEENDDAY_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;

				dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.SHRINK_FLD].Style.HorizontalAlignment = AlignHorzEnum.Far;

				dgrdData.Columns[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[ITM_BOMTable.EFFECTIVEENDDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
				dgrdData.Columns[ITM_BOMTable.ALTERNATIVE_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				dgrdData.Columns[ITM_BOMTable.EFFECTIVEENDDAY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				dgrdData.Columns[ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				dgrdData.Columns[ITM_BOMTable.LEADTIMEOFFSET_FLD].NumberFormat = Constants.INTERGER_NUMBERFORMAT;
				dgrdData.Columns[ITM_BOMTable.QUANTITY_FLD].NumberFormat = DECIMAL_NUMBERFORMAT_SMALL;
				dgrdData.Columns[ITM_BOMTable.SHRINK_FLD].NumberFormat = DECIMAL_NUMBERFORMAT_SMALL;
				
				dgrdData.Splits[0].DisplayColumns[CaptionOperation].Button = true;
				for (int i =0; i < dgrdData.Splits[0].DisplayColumns.Count; i++)
				{
					dgrdData.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
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


		private void DisableGrid(bool blnSate)
		{
			for (int i =0; i <dgrdData.Splits[0].DisplayColumns.Count; i++)
			{
				dgrdData.Splits[0].DisplayColumns[i].Locked = blnSate;
			}
		}

		//**************************************************************************              
		///    <Description>
		///		 Form load
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Bom_Load(object sender, System.EventArgs e)
		{
			
			string METHOD_NAME = THIS + ".Bom_Load()";
			try
			{
				Security objSecurity = new Security();
				this.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					return;
				}

				LoadComboCCN();
				ResetForm();
				txtItemCode.Focus();

				dtbGridLayout = FormControlComponents.StoreGridLayout(dgrdData);
				dtmEffectiveDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
				txtIncrement.Tag = OleDbType.TinyInt;
				
				//Reset form and load all ccn
				if (voProduct.ProductID == 0)
				{
					EnableDisableButton(enumAction);

					CreateTableForGrid(1);
					dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
					ConfigGridLayout();
				}
				else
				{
					voProduct = (ITM_ProductVO) new BomBO().GetObjectVOForBOM(voProduct.ProductID);
					txtItemCode.Text = voProduct.Code;
					txtModel.Text = voProduct.Revision;
					txtPartName.Text = voProduct.Description;
					chkMakeItem.Checked = bool.Parse(voProduct.MakeItem.ToString());
					txtBOMDes.Text = voProduct.BOMDescription ;
					cboCCN.SelectedValue = voProduct.CCNID;
					if (voProduct.BomIncrement != 0)
					{
						txtIncrement.Value = voProduct.BomIncrement.ToString();
					}
					CreateTableForGrid(2);
					dstBOMDetail.Tables.Add(new BomBO().ListBOMDetailsOfProduct(voProduct.ProductID).Copy());
					dstBOMDetail.Tables.Add(new BomBO().ListHierarchyOfProduct(voProduct.ProductID).Copy());
					EnableDisableButton(enumAction);
					intRowCountDelete =0;
					dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
					
					if (dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME].Rows.Count == 0)
					{
						enumAction = EnumAction.Add;
						txtIncrement.Value = 1;
						DisableGrid(false);
						btnSave.Enabled = true;
						txtBOMDes.Text = string.Empty;
						txtIncrement.Enabled = true;
						txtBOMDes.Enabled = true;
						dgrdData.AllowDelete = true;
					}
					else
					{
						DisableGrid(true);
						btnDelete.Enabled = true;
						btnEdit.Enabled = true;
						btnSave.Enabled = false;
						txtBOMDes.Enabled = false;
						txtIncrement.Enabled = false;
					}
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
					ConfigGridLayout();
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


		//**************************************************************************              
		///    <Description>
		///		Disable and enable button base on form action and security
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void EnableDisableButton(EnumAction enumAction)
		{
			if (enumAction == EnumAction.Default)
			{
				txtBOMDes.Enabled = false;
				txtIncrement.Enabled = false;
				DisableGrid(true);
				btnAdd.Enabled = true;
				btnSave.Enabled = false;
				btnFindItem.Enabled = true;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				dgrdData.AllowDelete = false;
			}
			if (enumAction == EnumAction.Add)
			{
				DisableGrid(false);
				btnSave.Enabled		= true;
				btnAdd.Enabled = false;
				txtBOMDes.Enabled	= true;
				txtIncrement.Enabled = true;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				dgrdData.AllowDelete = true;
				btnFindItem.Enabled = true;
			}
			if (enumAction == EnumAction.Edit)
			{
				DisableGrid(false);
				btnSave.Enabled		= true;
				btnAdd.Enabled = false;
				btnDelete.Enabled = false;
				txtBOMDes.Enabled	= true;
				txtIncrement.Enabled	= true;
				dgrdData.AllowDelete = true;
				btnFindItem.Enabled = false;
			}
		}


		//**************************************************************************              
		///    <Description>
		///		Bind information of compenent into grid row
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void BindDataFromSearchFormToGrid(int pintProductID)
		{
			try
			{
				if (pintProductID != 0)
				{
					ITM_ProductVO voComponent = new ITM_ProductVO();
					MST_UnitOfMeasureVO voUM = new MST_UnitOfMeasureVO();

					voComponent = (ITM_ProductVO) new ProductItemInfoBO().GetProductInfo(pintProductID);
					voUM = (MST_UnitOfMeasureVO) new UtilsBO().GetUMInfor(voComponent.StockUMID);
		
					dgrdData.EditActive = true;
					dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = voComponent.Code;
					dgrdData.Columns[ITM_ProductTable.CODE_FLD].Tag = voComponent.Code;
					dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = voComponent.Description;
					dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = voComponent.Revision;
					dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = voComponent.Revision;
					dgrdData[dgrdData.Row, ITM_BOMTable.COMPONENTID_FLD] = pintProductID;
					dgrdData[dgrdData.Row, StockUMCode] = voUM.Code;

					//get CategoryCode
					ITM_CategoryVO voCategory = (ITM_CategoryVO) new ITM_CategoryBO().GetObjectVO(voComponent.CategoryID);
					dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = voCategory.Code;

					//get PartyCode
					MST_PartyVO voParty = (MST_PartyVO) new PartyBO().GetObjectVO(voComponent.PrimaryVendorID, string.Empty);
					dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD] = voParty.Code;

                    int intIncrement = dgrdData.Row;
					if (dgrdData[dgrdData.Row, CaptionLine].ToString() == string.Empty)
					{
						if (txtIncrement.Value.ToString() != string.Empty)
						{
							intIncrement = int.Parse(txtIncrement.Value.ToString());
						}
						if ((dgrdData.Row == 0) && (intIncrement >0))
						{
							dgrdData[dgrdData.Row, CaptionLine] = intIncrement.ToString();
						}
						else if ((dgrdData.Row == 0) && (intIncrement ==0))
						{
							dgrdData[dgrdData.Row, CaptionLine] = 1;
						}
						else
						{
							if (intIncrement != 0)
							{
								dgrdData.Columns[CaptionLine].Value = int.Parse(dgrdData[dgrdData.Row -1, CaptionLine].ToString()) + intIncrement;
							}
							else
							{
								dgrdData.Columns[CaptionLine].Value = int.Parse(dgrdData[dgrdData.Row -1, CaptionLine].ToString()) + 1;
							}
						}
					}
				}
				else
				{
					dgrdData[dgrdData.Row, ITM_ProductTable.CODE_FLD] = DBNull.Value;
					dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = DBNull.Value;
					dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = DBNull.Value;
					dgrdData[dgrdData.Row, ITM_BOMTable.COMPONENTID_FLD] = DBNull.Value;
					dgrdData[dgrdData.Row, StockUMCode] = DBNull.Value;
					dgrdData[dgrdData.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] = DBNull.Value;
					dgrdData[dgrdData.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD] = DBNull.Value;
				}
				
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		//**************************************************************************              
		///    <Description>
		///		OpenSearchForm to select a product
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void dgrdData_ButtonClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_ButtonClick()";
			string strColName = dgrdData.Splits[0].DisplayColumns[dgrdData.Col].DataColumn.DataField;
			Hashtable htbCondition = new Hashtable();
			if (!btnSave.Enabled) return;
			DataRowView drwResult = null;
			try
			{
				if ((strColName == ITM_ProductTable.CODE_FLD) && (!dgrdData.Splits[0].DisplayColumns[strColName].Locked))
				{
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.CODE_FLD, dgrdData[dgrdData.Row, strColName].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.CODE_FLD, dgrdData.Columns[strColName].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						BindDataFromSearchFormToGrid(int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString()));
					}
				}
				if ((strColName == ITM_ProductTable.DESCRIPTION_FLD) && (!dgrdData.Splits[0].DisplayColumns[strColName].Locked))
				{
					if (cboCCN.SelectedValue != null)
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCondition.Add(ITM_ProductTable.CCNID_FLD, 0);
					}
					if (dgrdData.AddNewMode == AddNewModeEnum.AddNewCurrent)
					{
						drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData[dgrdData.Row, strColName].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.DESCRIPTION_FLD, dgrdData.Columns[strColName].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						BindDataFromSearchFormToGrid(int.Parse(drwResult[ITM_ProductTable.PRODUCTID_FLD].ToString()));
					}
				}
				if (strColName == CaptionOperation)
				{
					htbCondition = new Hashtable();
					htbCondition.Add(ITM_ProductTable.PRODUCTID_FLD, voProduct.ProductID);
					if (dgrdData.AddNewMode != AddNewModeEnum.NoAddNew)
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_RoutingTable.TABLE_NAME, ITM_RoutingTable.STEP_FLD, dgrdData[dgrdData.Row, strColName].ToString(), htbCondition, true);
					}
					else
					{
						drwResult = FormControlComponents.OpenSearchForm(ITM_RoutingTable.TABLE_NAME, ITM_RoutingTable.STEP_FLD, dgrdData.Columns[strColName].Text.Trim(), htbCondition, true);
					}
					if (drwResult != null)
					{
						dgrdData.Columns[ITM_BOMTable.ROUTINGID_FLD].Value = drwResult[ITM_RoutingTable.ROUTINGID_FLD];
						dgrdData.Columns[CaptionOperation].Value = drwResult[ITM_RoutingTable.STEP_FLD].ToString();
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


		//**************************************************************************              
		///    <Description>
		///		Validate data, and business
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///      true : if no error 
		///      false: if else
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private bool ValidateData()
		{
			//requires
			if (FormControlComponents.CheckMandatory(txtItemCode))
			{
				PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
				txtItemCode.Focus();
				txtItemCode.Select();
				return false;
			}

			if (txtIncrement.Value.ToString().Trim() != string.Empty)
			{
				if (!FormControlComponents.IsNumeric(txtIncrement.Value.ToString()))
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC);
					txtIncrement.Focus();
					txtIncrement.Select();
					return false;
				}
				else
				{
					voProduct.BomIncrement = int.Parse(txtIncrement.Value.ToString().Trim());
					if ((voProduct.BomIncrement > 255) || (voProduct.BomIncrement <0))
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC);
					}
				}
			}

			//validate on grid
			dgrdData.UpdateData();
            for (int i = 0; i < dgrdData.RowCount; i++)
			{
				//check all component
				if (dgrdData[i, ITM_ProductTable.CODE_FLD].ToString() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
					return false;
				}
				if (dgrdData[i, ITM_BOMTable.QUANTITY_FLD].ToString().Trim() == string.Empty)
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.QUANTITY_FLD]);
					dgrdData.Focus();
					return false;			
				}
				//check shrink
				try
				{
					if (dgrdData[i, ITM_BOMTable.SHRINK_FLD].ToString().Trim() != string.Empty)
					{
						float intShrink = float.Parse(dgrdData[i, ITM_BOMTable.SHRINK_FLD].ToString().Trim());
						if ((intShrink < 0) && (intShrink > 100))
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_SHRINK);
							dgrdData.Row = i;
							dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.SHRINK_FLD]);
							dgrdData.Focus();
							return false;
						}
					}
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_SHRINK);
					dgrdData.Row = i;
					dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.SHRINK_FLD]);
					dgrdData.Focus();
					return false;
				}
			}

            if (dgrdData.RowCount == 0)
			{
				throw new PCSException(ErrorCode.MESSAGE_BOM_EXISTATLISTROW, string.Empty, null);
			}
			return true;
		}
			

		//**************************************************************************              
		///    <Description>
		///		Validate data and update into database
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			blnHasError = true;
			try
			{
				if (!dgrdData.EditActive && ValidateData())
				{
					if (!GetDataFormGrid()) return;
					voProduct.BOMDescription = txtBOMDes.Text.Trim();
					BomBO boBOM = new BomBO();
					boBOM.UpdateAll(dstBOMDetail, voProduct);
					CreateTableForGrid(2);
					dstBOMDetail.Tables.Add(new BomBO().ListBOMDetailsOfProduct(voProduct.ProductID).Copy());
					dstBOMDetail.Tables.Add(new BomBO().ListHierarchyOfProduct(voProduct.ProductID).Copy());
					intRowCountDelete =0;
					dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
					ConfigGridLayout();
					FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
					blnHasError = false;
					btnSave.Enabled = false;
					DisableGrid(true);
					btnEdit.Enabled = true;
					btnDelete.Enabled = true;
					btnAdd.Enabled = true;
					dgrdData.AllowDelete = false;
					enumAction = EnumAction.Default;
					txtBOMDes.Enabled = false;
					txtIncrement.Enabled = false;
				}
			}
			catch (PCSException ex)
			{
				if (ex.mCode == ErrorCode.DUPLICATE_KEY)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_DUPLICATECOMPONENT, MessageBoxIcon.Error);
				}
				else
				{
					PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				}
				if (ex.CauseException != null)
				{
					try
					{
						Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
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


		//**************************************************************************              
		///    <Description>
		///		 Delete BOM of Product
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Mar 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if (dgrdData.EditActive) return;
				DataSet dstDelete = new DataSet();
				dstDelete.Tables.Add(new BomBO().ListBOMDetailsOfProduct(voProduct.ProductID).Copy());
				dstDelete.Tables.Add(new BomBO().ListHierarchyOfProduct(voProduct.ProductID).Copy());
				if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					foreach (DataRow drow in dstDelete.Tables[ITM_BOMTable.TABLE_NAME].Rows)
					{
						drow.Delete();
					}
					foreach (DataRow drow in dstDelete.Tables[ITM_HierarchyTable.TABLE_NAME].Rows)
					{
						drow.Delete();
					}
					BomBO boBOM = new BomBO();
					voProduct.BOMDescription = string.Empty;
					voProduct.BomIncrement = 0;
					boBOM.UpdateAll(dstDelete, voProduct);
					dgrdData.DataSource = dstDelete.Tables[0];
					ResetForm();
					enumAction = EnumAction.Default;
					EnableDisableButton(enumAction);
					btnAdd.Focus();
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


		//**************************************************************************              
		///    <Description>
		///    Add event, reset form and change form's state
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				ResetForm();
				CreateTableForGrid(1);
				dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
				FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
				ConfigGridLayout();
				enumAction = EnumAction.Add;
				EnableDisableButton(enumAction);
				txtIncrement.Value = 1.ToString();
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


		//**************************************************************************              
		///    <Description>
		///    Open search form by F4 button
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void txtItemCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
			{
				btnFindItem_Click(sender, new EventArgs());
			}
		}


		//**************************************************************************              
		///    <Description>
		///    Edit event, change form's action and accept for user to edit on the gird
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				enumAction = EnumAction.Edit;
				EnableDisableButton(enumAction);
				txtIncrement.Focus();
				btnEdit.Enabled = false;
				btnAdd.Enabled = false;
				ConfigGridLayout();
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


		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		//**************************************************************************              
		///    <Description>
		///		 Process before close form
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void Bom_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".SOPackListManagement_Closing()";
			try
			{
				if ((enumAction == EnumAction.Add)||(enumAction == EnumAction.Edit))
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

		
		//**************************************************************************              
		///    <Description>
		///			Check date time in every row on the grid follow by business
		///				throw Execption if has error
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, Mar 10, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void CheckDateOntheGrid(int intRowIndex,C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			DateTime dtmBegin = new DateTime(), dtmEnd = new DateTime();
			if (e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEENDDATE_FLD)
			{
				if (dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim() != string.Empty)
				{
					dtmBegin = DateTime.Parse(dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
				}
				if (e.Column.DataColumn.Value.ToString() != string.Empty)
				{
					dtmEnd = DateTime.Parse(e.Column.DataColumn.Value.ToString());
				}
				if ((dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim() != string.Empty) && (e.Column.DataColumn.Value.ToString() != string.Empty))
					if (dtmBegin > dtmEnd)
					{
						throw new PCSException(ErrorCode.MESSAGE_COSTCENTERRATE_CHECKDATEONEROW, string.Empty, null);	
					}
                if (dgrdData.RowCount > 0)
				{
                    if (intRowIndex <= dgrdData.RowCount)
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							DateTime dtmEndPrevious = DateTime.Parse(e.Column.DataColumn.Value.ToString());
							if (dgrdData[intRowIndex +1, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString() != string.Empty)
							{
								dtmBegin = DateTime.Parse(dgrdData[intRowIndex +1, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString());
								if (dtmBegin < dtmEndPrevious)
								{
									throw new PCSException(ErrorCode.MESSAGE_COSTCENTERRATE_CHECKDATEEVERYROW, string.Empty, null);	
								}
							}
						}
					}
				}
			}
			if (e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEBEGINDATE_FLD)
			{
				if (e.Column.DataColumn.Value.ToString() != string.Empty)
				{
					dtmBegin = DateTime.Parse(e.Column.DataColumn.Value.ToString());
				}
				if (dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim() != string.Empty)
				{
					dtmEnd = DateTime.Parse(dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim());
				}
				if ((e.Column.DataColumn.Value.ToString() != string.Empty) && (dgrdData[intRowIndex, ITM_BOMTable.EFFECTIVEENDDATE_FLD].ToString().Trim() != string.Empty))
					if (dtmBegin > dtmEnd)
					{
						throw new PCSException(ErrorCode.MESSAGE_COSTCENTERRATE_CHECKDATEONEROW, string.Empty, null);	
					}
                if (dgrdData.RowCount > 0)
				{
					if (intRowIndex > 0)
					{
						if (dgrdData[intRowIndex-1, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim() != string.Empty)
						{
							DateTime dtmEndPrevious = DateTime.Parse(dgrdData[intRowIndex-1, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString().Trim());
							if (dgrdData[intRowIndex-1, ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].ToString() != string.Empty)
							{
								dtmBegin = DateTime.Parse(e.Column.DataColumn.Value.ToString());
								if (dtmBegin < dtmEndPrevious)
								{
									throw new PCSException(ErrorCode.MESSAGE_COSTCENTERRATE_CHECKDATEEVERYROW, string.Empty, null);	
								}
							}
						}
					}
				}
			}
		}


		//**************************************************************************              
		///    <Description>
		///		BeforeColUpdate event,
		///			check data on the grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void dgrdData_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_BeforeColUpdate()";
			try
			{
				Hashtable htbCriteria = new Hashtable();
				DataRowView drwResult = null;
				if (e.Column.DataColumn.Text.Trim()	 == string.Empty) return;
				if (e.Column.DataColumn.DataField == ITM_ProductTable.CODE_FLD || e.Column.DataColumn.DataField == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (cboCCN.SelectedIndex >= 0)
					{
						htbCriteria.Add(MST_CCNTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					}
					else
					{
						htbCriteria.Add(MST_CCNTable.CCNID_FLD, 0);
					}
					drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, e.Column.DataColumn.DataField, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						e.Column.DataColumn.Tag = drwResult.Row;
					}
					else
					{
						e.Cancel = true;
					}
				}
				if (e.Column.DataColumn.DataField == CaptionOperation)
				{
					htbCriteria = new Hashtable();
					htbCriteria.Add(ITM_ProductTable.PRODUCTID_FLD, voProduct.ProductID);
					drwResult = FormControlComponents.OpenSearchForm(ITM_RoutingTable.TABLE_NAME, ITM_RoutingTable.STEP_FLD, dgrdData.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
					if (drwResult != null)
					{
						dgrdData.Columns[ITM_RoutingTable.ROUTINGID_FLD].Value = int.Parse(drwResult[ITM_RoutingTable.ROUTINGID_FLD].ToString());
						dgrdData.Columns[CaptionOperation].Value = drwResult[ITM_RoutingTable.STEP_FLD];
						e.Column.DataColumn.Tag = drwResult.Row;
					}
					else
					{
						e.Cancel = true;
					}
				}
				//check quantity
				if (dgrdData.Splits[0].DisplayColumns[dgrdData.Col].DataColumn.DataField == ITM_BOMTable.QUANTITY_FLD)
				{
					try
					{
						float fQuantity = float.Parse(dgrdData.Splits[0].DisplayColumns[ITM_BOMTable.QUANTITY_FLD].DataColumn.Value.ToString().Trim());
						if (fQuantity <= 0.0)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_QUANTITY, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}
					}
					catch
					{
						//cancel update and throw PCSException
						e.Cancel = true;
						PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_QUANTITY, MessageBoxIcon.Error);
						return;
					}
				}
				//check shrink
				if (e.Column.DataColumn.DataField == ITM_BOMTable.SHRINK_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							decimal intShrink = decimal.Parse(e.Column.DataColumn.Value.ToString());
							if ((intShrink < 0) || (intShrink > 99))
							{
								PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_SHRINK, MessageBoxIcon.Error);
								e.Cancel = true;
								return;
							}
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_SHRINK, MessageBoxIcon.Error);
						e.Cancel = true;
						return;
					}
				}

				//check lead time offset
				if (e.Column.DataColumn.DataField == ITM_BOMTable.LEADTIMEOFFSET_FLD)
				{
					try
					{
						if (e.Column.DataColumn.Value.ToString() != string.Empty)
						{
							decimal intLeadTime = decimal.Parse(e.Column.DataColumn.Value.ToString());
							if (intLeadTime <= 0)
							{
								PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Error);
								e.Cancel = true;
								return;
							}
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Error);
						e.Cancel = true;
						return;
					}
				}
				CheckDateOntheGrid(dgrdData.Row, e);
				//check allternative
				if (e.Column.DataColumn.DataField == ITM_BOMTable.ALTERNATIVE_FLD || 
					e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEBEGINDAY_FLD ||
					e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEENDDAY_FLD)
				{
					try
					{
						int intValue = int.Parse(e.Column.DataColumn.Value.ToString());
						if (intValue < 0)
						{
							PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}
					}
					catch
					{
						PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_QTY, MessageBoxIcon.Error);
						e.Cancel = true;
						return;
					}
				}

				//check begin eff day
				if (e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEBEGINDAY_FLD)
				{
					if ((e.Column.DataColumn.Value.ToString() != string.Empty) && (dgrdData[dgrdData.Row, ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString() != string.Empty))
					{
						int intBegin = int.Parse(e.Column.DataColumn.Value.ToString());
						int intEnd = int.Parse(dgrdData[dgrdData.Row, ITM_BOMTable.EFFECTIVEENDDAY_FLD].ToString());
						if (intBegin > intEnd)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_BEGIN_END_DAY,  MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}
					}
				}

				//check end eff day
				if (e.Column.DataColumn.DataField == ITM_BOMTable.EFFECTIVEENDDAY_FLD)
				{
					if ((e.Column.DataColumn.Value.ToString() != string.Empty) && (dgrdData[dgrdData.Row, ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString() != string.Empty))
					{
						int intEnd = int.Parse(e.Column.DataColumn.Value.ToString());
						int intBegin = int.Parse(dgrdData[dgrdData.Row, ITM_BOMTable.EFFECTIVEBEGINDAY_FLD].ToString());
						if (intBegin > intEnd)
						{
							PCSMessageBox.Show(ErrorCode.MESSAGE_BOM_BEGIN_END_DAY,  MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}
					}
				}
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				e.Cancel = true;
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				return;
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				e.Cancel = true;
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
				return;
			}
		}


		private void cboCCN_TextChanged(object sender, System.EventArgs e)
		{
			ResetForm();
			EnableDisableButton(enumAction);
		}


		private void Bom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F12)
			{
				if (btnSave.Enabled)
				{
                    dgrdData.Row = dgrdData.RowCount;
					dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.CODE_FLD]);
					dgrdData.Focus();
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///		F12 event on grid
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, Mar 21, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void dgrdData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F4:
					if (btnSave.Enabled)
					{
						dgrdData_ButtonClick(sender, null);
					}
					break;
				
			}
		}

		#region Change backcolor when focus or lost focus
		private void OnEnterControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void OnLeaveControl(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ". OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
//				CheckScopeOfControl(sender);
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		#endregion

		//**************************************************************************              
		///    <Description>
		///    Update again for Component code
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Wednesday, Apr 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void dgrdData_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			const string METHOD_NAME = THIS + ".dgrdData_AfterColUpdate()";
			try
			{
				string ColName = e.Column.DataColumn.DataField;
				if (ColName == ITM_ProductTable.CODE_FLD || ColName == ITM_ProductTable.DESCRIPTION_FLD)
				{
					if (e.Column.DataColumn.Value.ToString() != string.Empty)
					{
						BindDataFromSearchFormToGrid(int.Parse(((DataRow) e.Column.DataColumn.Tag)[ITM_ProductTable.PRODUCTID_FLD].ToString()));
					}
					else
					{
						BindDataFromSearchFormToGrid(0);
					}
				}
				if (ColName == CaptionOperation)
				{
					if (e.Column.DataColumn.Value.ToString() != string.Empty)
					{
						dgrdData.Columns[ITM_BOMTable.ROUTINGID_FLD].Value = ((DataRow) e.Column.DataColumn.Tag)[ITM_RoutingTable.ROUTINGID_FLD];
						dgrdData.Columns[CaptionOperation].Value = ((DataRow) e.Column.DataColumn.Tag)[ITM_RoutingTable.STEP_FLD].ToString();
					}
					else
					{
						dgrdData.Columns[ITM_BOMTable.ROUTINGID_FLD].Value = DBNull.Value;
						dgrdData.Columns[CaptionOperation].Value = DBNull.Value;
					}
				}
				int intIncrement = 0;
				if (dgrdData[dgrdData.Row, CaptionLine].ToString() == string.Empty)
				{
					if (txtIncrement.Value.ToString() != string.Empty)
					{
						intIncrement = int.Parse(txtIncrement.Value.ToString());
					}
					if ((dgrdData.Row == 0) && (intIncrement >0))
					{
						dgrdData[dgrdData.Row, CaptionLine] = intIncrement;
					}
					else if ((dgrdData.Row == 0) && (intIncrement ==0))
					{
						dgrdData[dgrdData.Row, CaptionLine] = 1;
					}
					else
					{
						if (intIncrement != 0)
						{
							dgrdData[dgrdData.Row, CaptionLine] = int.Parse(dgrdData[dgrdData.Row -1, CaptionLine].ToString()) + intIncrement;
						}
						else
						{
							dgrdData[dgrdData.Row, CaptionLine] = int.Parse(dgrdData[dgrdData.Row -1, CaptionLine].ToString()) + 1;
						}
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

		//**************************************************************************              
		///    <Description>
		///    Set null value for datetime cell
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, Apr 29, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private void dtmEffectiveDate_ValueChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmEffectiveDate_ValueChanged()";
			try
			{
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

		private void dgrdData_Leave(object sender, System.EventArgs e)
		{
			btnSave.Focus();
		}

		private void dtmEffectiveDate_DropDownClosed(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmTransDate_DropDownClosed()";
			try
			{
				if (dtmEffectiveDate.Text == string.Empty)
				{
					dgrdData[dgrdData.Row, dgrdData.Col] = DBNull.Value;
				}
				else
				{
					DateTime dtmValue = new DateTime(DateTime.Parse(dtmEffectiveDate.Value.ToString()).Year, DateTime.Parse(dtmEffectiveDate.Value.ToString()).Month, DateTime.Parse(dtmEffectiveDate.Value.ToString()).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
					dtmEffectiveDate.Value = dtmValue;
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

		private void dtmEffectiveDate_Enter(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".dtmEffectiveDate_Enter()", FIRSTDATE = "01-01-0001";
			const int LENGTH = 10;
			try
			{
				if ((dtmEffectiveDate.Text == string.Empty) || (dtmEffectiveDate.Text.Substring(0, LENGTH)== FIRSTDATE))
				{
					dtmEffectiveDate.Value = DateTime.Now;
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

		private void txtItemCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtItemCode_Validating()";
			try
			{
				if (!txtItemCode.Modified) return;
				if (txtItemCode.Text.Trim() == string.Empty)
				{
					if (voProduct.ProductID !=0 )
					{
						txtBOMDes.Text = string.Empty;
						txtIncrement.Value = 1;
						txtModel.Text = string.Empty;
						txtPartName.Text = string.Empty;
						voProduct.ProductID = 0;
						CreateTableForGrid(1);
						dgrdData.DataSource = dstBOMDetail.Tables[ITM_BOMTable.TABLE_NAME];
						FormControlComponents.RestoreGridLayout(dgrdData, dtbGridLayout);
						ConfigGridLayout();
					}
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbConditon = new Hashtable();
				if (cboCCN.SelectedValue != null)
				{
					htbConditon.Add(ITM_ProductTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
					htbConditon.Add(ITM_ProductTable.MAKEITEM_FLD, 1);
					if (enumAction == EnumAction.Add)
					{
						htbConditon.Add(v_ITMBOM_Product.HASBOM_FLD, 0);
					}
					else
					{
						htbConditon.Add(v_ITMBOM_Product.HASBOM_FLD, 1);
					}
				}
				else
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					e.Cancel = true;
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(v_ITMBOM_Product.VIEW_NAME, ITM_ProductTable.CODE_FLD, txtItemCode.Text, htbConditon, false);
				if (drwResult != null)
				{
					GetDataOfBomFromDataRow(drwResult.Row);
					voProduct.Code = txtItemCode.Text.Trim();
				}
				else
					e.Cancel = true;
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
