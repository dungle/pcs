using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSProduction.WorkOrder
{
    partial class ManufacturingClose
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected C1.Win.C1Input.C1DateEdit dtmToDueDate;
		protected C1.Win.C1Input.C1DateEdit dtmFromDueDate;
		protected C1.Win.C1Input.C1DateEdit dtmPostDate;
		protected System.Windows.Forms.Button btnSearchMasLoc;
		protected System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.Label lblPostDate;
		protected C1.Win.C1TrueDBGrid.C1TrueDBGrid gridWOClose;
		protected System.Windows.Forms.Button btnSearch;
		public System.Windows.Forms.Button btnCloseWO;
		protected System.Windows.Forms.Button btnClose;
		protected System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblCCN;
		protected C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblToDueDate;
		private System.Windows.Forms.Label lblFromDueDate;
        protected System.Windows.Forms.CheckBox chkSelectAllManuf;

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManufacturingClose));
            this.dtmToDueDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromDueDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
            this.btnSearchMasLoc = new System.Windows.Forms.Button();
            this.txtMasLoc = new System.Windows.Forms.TextBox();
            this.lblMasLoc = new System.Windows.Forms.Label();
            this.lblPostDate = new System.Windows.Forms.Label();
            this.gridWOClose = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCloseWO = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblCCN = new System.Windows.Forms.Label();
            this.chkSelectAllManuf = new System.Windows.Forms.CheckBox();
            this.lblToDueDate = new System.Windows.Forms.Label();
            this.lblFromDueDate = new System.Windows.Forms.Label();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWOClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmToDueDate
            // 
            // 
            // dtmToDueDate.Calendar
            // 
            this.dtmToDueDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDueDate.CustomFormat = "dd-MM-yyyy";
            this.dtmToDueDate.EmptyAsNull = true;
            this.dtmToDueDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToDueDate.Location = new System.Drawing.Point(292, 48);
            this.dtmToDueDate.Name = "dtmToDueDate";
            this.dtmToDueDate.Size = new System.Drawing.Size(93, 20);
            this.dtmToDueDate.TabIndex = 10;
            this.dtmToDueDate.Tag = null;
            this.dtmToDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDueDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtmFromDueDate
            // 
            // 
            // dtmFromDueDate.Calendar
            // 
            this.dtmFromDueDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDueDate.CustomFormat = "dd-MM-yyyy";
            this.dtmFromDueDate.EmptyAsNull = true;
            this.dtmFromDueDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromDueDate.Location = new System.Drawing.Point(99, 48);
            this.dtmFromDueDate.Name = "dtmFromDueDate";
            this.dtmFromDueDate.Size = new System.Drawing.Size(95, 20);
            this.dtmFromDueDate.TabIndex = 8;
            this.dtmFromDueDate.Tag = null;
            this.dtmFromDueDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDueDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtmPostDate
            // 
            // 
            // dtmPostDate.Calendar
            // 
            this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmPostDate.CustomFormat = "dd-MM-yyyy";
            this.dtmPostDate.EmptyAsNull = true;
            this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmPostDate.Location = new System.Drawing.Point(100, 4);
            this.dtmPostDate.Name = "dtmPostDate";
            this.dtmPostDate.Size = new System.Drawing.Size(93, 20);
            this.dtmPostDate.TabIndex = 3;
            this.dtmPostDate.Tag = null;
            this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // btnSearchMasLoc
            // 
            this.btnSearchMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchMasLoc.Location = new System.Drawing.Point(194, 26);
            this.btnSearchMasLoc.Name = "btnSearchMasLoc";
            this.btnSearchMasLoc.Size = new System.Drawing.Size(24, 20);
            this.btnSearchMasLoc.TabIndex = 6;
            this.btnSearchMasLoc.Text = "...";
            this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
            // 
            // txtMasLoc
            // 
            this.txtMasLoc.Location = new System.Drawing.Point(100, 26);
            this.txtMasLoc.Name = "txtMasLoc";
            this.txtMasLoc.Size = new System.Drawing.Size(93, 20);
            this.txtMasLoc.TabIndex = 5;
            this.txtMasLoc.Text = "";
            this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.txtMasLoc.Leave += new System.EventHandler(this.txtMasLoc_Leave);
            // 
            // lblMasLoc
            // 
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMasLoc.Location = new System.Drawing.Point(5, 26);
            this.lblMasLoc.Name = "lblMasLoc";
            this.lblMasLoc.Size = new System.Drawing.Size(93, 20);
            this.lblMasLoc.TabIndex = 4;
            this.lblMasLoc.Text = "Mas. Location";
            this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPostDate
            // 
            this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPostDate.Location = new System.Drawing.Point(5, 4);
            this.lblPostDate.Name = "lblPostDate";
            this.lblPostDate.Size = new System.Drawing.Size(93, 20);
            this.lblPostDate.TabIndex = 2;
            this.lblPostDate.Text = "Post Date";
            this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridWOClose
            // 
            this.gridWOClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gridWOClose.CaptionHeight = 17;
            this.gridWOClose.CollapseColor = System.Drawing.Color.Black;
            this.gridWOClose.ExpandColor = System.Drawing.Color.Black;
            this.gridWOClose.FilterBar = true;
            this.gridWOClose.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.System;
            this.gridWOClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridWOClose.GroupByCaption = "Drag a column header here to group by that column";
            this.gridWOClose.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.gridWOClose.Location = new System.Drawing.Point(3, 72);
            this.gridWOClose.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
            this.gridWOClose.Name = "gridWOClose";
            this.gridWOClose.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.gridWOClose.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.gridWOClose.PreviewInfo.ZoomFactor = 75;
            this.gridWOClose.PrintInfo.ShowOptionsDialog = false;
            this.gridWOClose.RecordSelectorWidth = 17;
            this.gridWOClose.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.gridWOClose.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.gridWOClose.RowHeight = 15;
            this.gridWOClose.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.gridWOClose.Size = new System.Drawing.Size(626, 352);
            this.gridWOClose.TabIndex = 12;
            this.gridWOClose.Text = "c1TrueDBGrid1";
            this.gridWOClose.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.gridWOClose_AfterColEdit);
            this.gridWOClose.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Select\" Dat" +
                "aField=\"Selected\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataCol" +
                "umn><C1DataColumn Level=\"0\" Caption=\"Work Order\" DataField=\"WorkOrderNo\"><ValueI" +
                "tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"WO Line\" Dat" +
                "aField=\"Line\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
                "Caption=\"Open Quantity\" DataField=\"OpenQuantity\"><ValueItems /><GroupInfo /></C1" +
                "DataColumn><C1DataColumn Level=\"0\" Caption=\"Part Number\" DataField=\"PartNumber\">" +
                "<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Part " +
                "Name\" DataField=\"Description\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
                "olumn Level=\"0\" Caption=\"Model\" DataField=\"Revision\"><ValueItems /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"UM\" DataField=\"UM\"><ValueItems /" +
                "><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Start Date, Time\" " +
                "DataField=\"StartDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
                "vel=\"0\" Caption=\"Due Date, Time\" DataField=\"DueDate\"><ValueItems /><GroupInfo />" +
                "</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Category\" DataField=\"ITM_Categor" +
                "yCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win" +
                ".C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;" +
                "BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCa" +
                "ption;}Style78{}Style79{}Selected{ForeColor:HighlightText;BackColor:Highlight;}E" +
                "ditor{}Style72{}Style73{}Style70{AlignHorz:Center;}Style71{AlignHorz:Near;}Style" +
                "76{AlignHorz:Center;}Style77{AlignHorz:Near;}Style74{}Style75{}Style81{}Style80{" +
                "}FilterBar{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeCol" +
                "or:ControlText;BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{Al" +
                "ignHorz:Center;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12" +
                "{}Style13{}Style29{AlignHorz:Near;}Style27{}Style22{AlignHorz:Near;}Style28{Alig" +
                "nHorz:Center;}Style26{}Style9{}Style8{}Style25{}Style24{}Style5{}Style4{}Style7{" +
                "}Style6{}Style1{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRo" +
                "w{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHo" +
                "rz:Near;}Style32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Micro" +
                "soft Sans Serif, 8.25pt;}Style41{AlignHorz:Near;}Style40{AlignHorz:Center;}Style" +
                "43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Center;" +
                "}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:Center;}Recor" +
                "dSelector{AlignImage:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Center;" +
                "}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:C" +
                "enter;}Style69{}Style68{}Style63{}Style62{}Style61{}Style60{}Style67{}Style66{}S" +
                "tyle65{AlignHorz:Near;}Style64{AlignHorz:Center;}Group{BackColor:ControlDark;Bor" +
                "der:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBG" +
                "rid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHe" +
                "ight=\"17\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"" +
                "17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Clien" +
                "tRect>0, 0, 622, 348</ClientRect><BorderSide>0</BorderSide><CaptionStyle parent=" +
                "\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle" +
                " parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" " +
                "/><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Styl" +
                "e12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"H" +
                "ighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRo" +
                "wStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector" +
                "\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"N" +
                "ormal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2" +
                "\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Styl" +
                "e3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle" +
                " parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /" +
                "><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>47<" +
                "/Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
                "adingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" />" +
                "<FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
                "le31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle paren" +
                "t=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
                "/ColumnDivider><Width>119</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayC" +
                "olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style paren" +
                "t=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorSty" +
                "le parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\"" +
                " /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><Colu" +
                "mnDivider>DarkGray,Single</ColumnDivider><Width>56</Width><Height>15</Height><DC" +
                "Idx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
                "=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" " +
                "me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle par" +
                "ent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Vi" +
                "sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>110</Wi" +
                "dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
                "ngStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><Fo" +
                "oterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style5" +
                "5\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"" +
                "Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
                "lumnDivider><Width>137</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColu" +
                "mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"" +
                "Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle " +
                "parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" />" +
                "<GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnD" +
                "ivider>DarkGray,Single</ColumnDivider><Width>57</Width><Height>15</Height><DCIdx" +
                ">6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
                "tyle22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=" +
                "\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent" +
                "=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visib" +
                "le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heigh" +
                "t><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
                "e2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"St" +
                "yle3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderSty" +
                "le parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\"" +
                " /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>8" +
                "6</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
                "HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" " +
                "/><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"S" +
                "tyle67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle par" +
                "ent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Singl" +
                "e</ColumnDivider><Width>34</Width><Height>15</Height><DCIdx>7</DCIdx></C1Display" +
                "Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style pare" +
                "nt=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorSt" +
                "yle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75" +
                "\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><Col" +
                "umnDivider>DarkGray,Single</ColumnDivider><Width>120</Width><Height>15</Height><" +
                "DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
                "me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3" +
                "\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle p" +
                "arent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><" +
                "Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>120</" +
                "Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn></internalCols></C1.W" +
                "in.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><" +
                "Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Styl" +
                "e parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style" +
                " parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style par" +
                "ent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style pa" +
                "rent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style" +
                " parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSt" +
                "yles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layou" +
                "t><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientArea>0, 0, 622, 348</ClientA" +
                "rea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=" +
                "\"\" me=\"Style15\" /></Blob>";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(569, 46);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCloseWO
            // 
            this.btnCloseWO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCloseWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCloseWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCloseWO.Location = new System.Drawing.Point(4, 426);
            this.btnCloseWO.Name = "btnCloseWO";
            this.btnCloseWO.Size = new System.Drawing.Size(118, 23);
            this.btnCloseWO.TabIndex = 14;
            this.btnCloseWO.Text = "Close &Work Orders";
            this.btnCloseWO.Click += new System.EventHandler(this.btnCloseWO_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(569, 426);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(509, 426);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 15;
            this.btnHelp.Text = "&Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblCCN
            // 
            this.lblCCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(516, 4);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(32, 20);
            this.lblCCN.TabIndex = 0;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSelectAllManuf
            // 
            this.chkSelectAllManuf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAllManuf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSelectAllManuf.Location = new System.Drawing.Point(129, 426);
            this.chkSelectAllManuf.Name = "chkSelectAllManuf";
            this.chkSelectAllManuf.Size = new System.Drawing.Size(85, 20);
            this.chkSelectAllManuf.TabIndex = 13;
            this.chkSelectAllManuf.Text = "Select &All";
            this.chkSelectAllManuf.Enter += new System.EventHandler(this.chkSelectAllManuf_Enter);
            this.chkSelectAllManuf.Leave += new System.EventHandler(this.chkSelectAllManuf_Leave);
            this.chkSelectAllManuf.CheckedChanged += new System.EventHandler(this.chkSelectAllManuf_CheckedChanged);
            // 
            // lblToDueDate
            // 
            this.lblToDueDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblToDueDate.ForeColor = System.Drawing.Color.Black;
            this.lblToDueDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToDueDate.Location = new System.Drawing.Point(220, 48);
            this.lblToDueDate.Name = "lblToDueDate";
            this.lblToDueDate.Size = new System.Drawing.Size(70, 20);
            this.lblToDueDate.TabIndex = 9;
            this.lblToDueDate.Text = "To Due Date";
            this.lblToDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFromDueDate
            // 
            this.lblFromDueDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFromDueDate.ForeColor = System.Drawing.Color.Black;
            this.lblFromDueDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromDueDate.Location = new System.Drawing.Point(5, 48);
            this.lblFromDueDate.Name = "lblFromDueDate";
            this.lblFromDueDate.Size = new System.Drawing.Size(93, 20);
            this.lblFromDueDate.TabIndex = 7;
            this.lblFromDueDate.Text = "From Due Date";
            this.lblFromDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.cboCCN.Location = new System.Drawing.Point(550, 4);
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(78, 21);
            this.cboCCN.TabIndex = 1;
            this.cboCCN.Text = "CCN";
            this.cboCCN.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
                "ta>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style2{" +
                "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
                "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
                "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
                "ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}Reco" +
                "rdSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,," +
                "1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}St" +
                "yle1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
                "Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Vert" +
                "icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 118, 158</Client" +
                "Rect><VScrollBar><Width>17</Width></VScrollBar><HScrollBar><Height>17</Height></" +
                "HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Eve" +
                "nRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
                "t=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
                "RowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me" +
                "=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle pare" +
                "nt=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" " +
                "/><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Nam" +
                "edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
                "tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
                "e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
                " parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Styl" +
                "e parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><S" +
                "tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
                "Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRec" +
                "SelWidth></Blob>";
            // 
            // ManufacturingClose
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.dtmToDueDate);
            this.Controls.Add(this.dtmFromDueDate);
            this.Controls.Add(this.dtmPostDate);
            this.Controls.Add(this.btnSearchMasLoc);
            this.Controls.Add(this.txtMasLoc);
            this.Controls.Add(this.gridWOClose);
            this.Controls.Add(this.lblMasLoc);
            this.Controls.Add(this.lblPostDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnCloseWO);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.chkSelectAllManuf);
            this.Controls.Add(this.lblToDueDate);
            this.Controls.Add(this.lblFromDueDate);
            this.KeyPreview = true;
            this.Name = "ManufacturingClose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manufacturing Close";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManufacturingClose_KeyDown);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ManufacturingClose_Closing);
            this.Load += new System.EventHandler(this.ManufacturingClose_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtmToDueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFromDueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmPostDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWOClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
    }
}
