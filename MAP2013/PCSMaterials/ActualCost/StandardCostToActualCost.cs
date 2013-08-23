using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PCSMaterials.ActualCost
{
	/// <summary>
	/// Summary description for StandardCostToActualCost.
	/// </summary>
	public class StandardCostToActualCost : System.Windows.Forms.Form
	{
		private C1.Win.C1Input.C1DateEdit c1DateEdit1;
		private System.Windows.Forms.Label label1;
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtWorkOrder;
		private System.Windows.Forms.Label lblWorkOrder;
		private System.Windows.Forms.Button btnRollUp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StandardCostToActualCost()
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
			this.c1DateEdit1 = new C1.Win.C1Input.C1DateEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtWorkOrder = new System.Windows.Forms.TextBox();
			this.lblWorkOrder = new System.Windows.Forms.Label();
			this.btnRollUp = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// c1DateEdit1
			// 
			this.c1DateEdit1.AccessibleDescription = "";
			this.c1DateEdit1.AccessibleName = "";
			// 
			// c1DateEdit1.Calendar
			// 
			this.c1DateEdit1.Calendar.AccessibleDescription = "";
			this.c1DateEdit1.Calendar.AccessibleName = "";
			this.c1DateEdit1.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.c1DateEdit1.CustomFormat = "dd-MM-yyyy";
			this.c1DateEdit1.EmptyAsNull = true;
			this.c1DateEdit1.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.c1DateEdit1.Location = new System.Drawing.Point(74, 30);
			this.c1DateEdit1.Name = "c1DateEdit1";
			this.c1DateEdit1.Size = new System.Drawing.Size(87, 20);
			this.c1DateEdit1.TabIndex = 53;
			this.c1DateEdit1.Tag = null;
			this.c1DateEdit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.c1DateEdit1.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// label1
			// 
			this.label1.AccessibleDescription = "";
			this.label1.AccessibleName = "";
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(5, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 20);
			this.label1.TabIndex = 54;
			this.label1.Text = "Rollup Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboCCN
			// 
			this.cboCCN.AccessibleDescription = "";
			this.cboCCN.AccessibleName = "";
			this.cboCCN.AddItemSeparator = ';';
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
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(74, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(87, 21);
			this.cboCCN.TabIndex = 46;
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
			// lblCCN
			// 
			this.lblCCN.AccessibleDescription = "";
			this.lblCCN.AccessibleName = "";
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCCN.Location = new System.Drawing.Point(5, 8);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(71, 20);
			this.lblCCN.TabIndex = 51;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnHelp
			// 
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(224, 112);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 49;
			this.btnHelp.Text = "&Help";
			// 
			// btnClose
			// 
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnClose.Location = new System.Drawing.Point(286, 112);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 50;
			this.btnClose.Text = "&Close";
			// 
			// txtWorkOrder
			// 
			this.txtWorkOrder.AccessibleDescription = "";
			this.txtWorkOrder.AccessibleName = "";
			this.txtWorkOrder.Location = new System.Drawing.Point(74, 53);
			this.txtWorkOrder.Name = "txtWorkOrder";
			this.txtWorkOrder.Size = new System.Drawing.Size(87, 20);
			this.txtWorkOrder.TabIndex = 47;
			this.txtWorkOrder.Text = "";
			// 
			// lblWorkOrder
			// 
			this.lblWorkOrder.AccessibleDescription = "";
			this.lblWorkOrder.AccessibleName = "";
			this.lblWorkOrder.ForeColor = System.Drawing.Color.Maroon;
			this.lblWorkOrder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblWorkOrder.Location = new System.Drawing.Point(5, 53);
			this.lblWorkOrder.Name = "lblWorkOrder";
			this.lblWorkOrder.Size = new System.Drawing.Size(71, 20);
			this.lblWorkOrder.TabIndex = 52;
			this.lblWorkOrder.Text = "From Year";
			this.lblWorkOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnRollUp
			// 
			this.btnRollUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRollUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRollUp.Location = new System.Drawing.Point(8, 114);
			this.btnRollUp.Name = "btnRollUp";
			this.btnRollUp.Size = new System.Drawing.Size(73, 23);
			this.btnRollUp.TabIndex = 48;
			this.btnRollUp.Text = "Rollup";
			// 
			// StandardCostToActualCost
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(350, 143);
			this.Controls.Add(this.c1DateEdit1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtWorkOrder);
			this.Controls.Add(this.lblWorkOrder);
			this.Controls.Add(this.btnRollUp);
			this.Name = "StandardCostToActualCost";
			this.Text = "Standard Cost Rollup From Actual Cost";
			((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
