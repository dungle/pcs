using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

// HACK: Thachnn 27/10/2005
#region USING FOR C1 REPORT
using System.Drawing.Printing;
using System.Data.OleDb;
using System.Collections.Specialized;
using System.IO;
using C1.Win.C1Preview;
//using C1.C1Report;
//using BorderStyleEnum = C1.C1Report.BorderStyleEnum;
//using Group = C1.C1Report.Group;
#endregion
// ENDHACK: Thachnn 27/10/2005


namespace PCSMaterials.Inventory
{
	/// <summary>
	/// Summary description for InventoryStatus.
	/// </summary>
	public class IVInventoryStatus : System.Windows.Forms.Form
	{        
		private C1.Win.C1List.C1Combo cboCCN;
		private System.Windows.Forms.Label lblCCN;
		private System.Windows.Forms.Label lblMasLoc;
		private System.Windows.Forms.TextBox txtMasLoc;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Button btnMasLoc;
		private System.Windows.Forms.Button btnLocation;
		private System.Windows.Forms.Button btnCategory;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnPrint;


	


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#region My variables
		const string THIS = "PCSMaterials.Inventory.IVInventoryStatus";
		UtilsBO boUtil = new UtilsBO();
		#endregion
		public IVInventoryStatus()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
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
			this.cboCCN = new C1.Win.C1List.C1Combo();
			this.lblCCN = new System.Windows.Forms.Label();
			this.txtMasLoc = new System.Windows.Forms.TextBox();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.lblMasLoc = new System.Windows.Forms.Label();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.btnMasLoc = new System.Windows.Forms.Button();
			this.btnLocation = new System.Windows.Forms.Button();
			this.btnCategory = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
			this.SuspendLayout();
			// 
			// cboCCN
			// 
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
			this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
			this.cboCCN.GapHeight = 2;
			this.cboCCN.ItemHeight = 15;
			this.cboCCN.Location = new System.Drawing.Point(308, 6);
			this.cboCCN.MatchEntryTimeout = ((long)(2000));
			this.cboCCN.MaxDropDownItems = ((short)(5));
			this.cboCCN.MaxLength = 32767;
			this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.cboCCN.Name = "cboCCN";
			this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.cboCCN.Size = new System.Drawing.Size(86, 21);
			this.cboCCN.TabIndex = 1;
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
			this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
			this.lblCCN.Location = new System.Drawing.Point(276, 6);
			this.lblCCN.Name = "lblCCN";
			this.lblCCN.Size = new System.Drawing.Size(32, 20);
			this.lblCCN.TabIndex = 0;
			this.lblCCN.Text = "CCN";
			this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMasLoc
			// 
			this.txtMasLoc.Location = new System.Drawing.Point(98, 6);
			this.txtMasLoc.Name = "txtMasLoc";
			this.txtMasLoc.Size = new System.Drawing.Size(112, 20);
			this.txtMasLoc.TabIndex = 3;
			this.txtMasLoc.Text = "";
			this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
			this.txtMasLoc.Validating += new System.ComponentModel.CancelEventHandler(this.txtMasLoc_Validating);
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(98, 28);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(112, 20);
			this.txtLocation.TabIndex = 6;
			this.txtLocation.Text = "";
			this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
			this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(98, 50);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(112, 20);
			this.txtCategory.TabIndex = 9;
			this.txtCategory.Text = "";
			this.txtCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyDown);
			this.txtCategory.Validating += new System.ComponentModel.CancelEventHandler(this.txtCategory_Validating);
			// 
			// lblMasLoc
			// 
			this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
			this.lblMasLoc.Location = new System.Drawing.Point(7, 6);
			this.lblMasLoc.Name = "lblMasLoc";
			this.lblMasLoc.Size = new System.Drawing.Size(96, 20);
			this.lblMasLoc.TabIndex = 2;
			this.lblMasLoc.Text = "Master Loccation";
			this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLocation
			// 
			this.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLocation.Location = new System.Drawing.Point(7, 28);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(96, 20);
			this.lblLocation.TabIndex = 5;
			this.lblLocation.Text = "Location";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCategory
			// 
			this.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCategory.Location = new System.Drawing.Point(7, 50);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(96, 20);
			this.lblCategory.TabIndex = 8;
			this.lblCategory.Text = "Category";
			this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnMasLoc
			// 
			this.btnMasLoc.Location = new System.Drawing.Point(212, 6);
			this.btnMasLoc.Name = "btnMasLoc";
			this.btnMasLoc.Size = new System.Drawing.Size(22, 20);
			this.btnMasLoc.TabIndex = 4;
			this.btnMasLoc.Text = "...";
			this.btnMasLoc.Click += new System.EventHandler(this.btnMasLoc_Click);
			// 
			// btnLocation
			// 
			this.btnLocation.Location = new System.Drawing.Point(212, 28);
			this.btnLocation.Name = "btnLocation";
			this.btnLocation.Size = new System.Drawing.Size(22, 20);
			this.btnLocation.TabIndex = 7;
			this.btnLocation.Text = "...";
			this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
			// 
			// btnCategory
			// 
			this.btnCategory.Location = new System.Drawing.Point(212, 50);
			this.btnCategory.Name = "btnCategory";
			this.btnCategory.Size = new System.Drawing.Size(22, 20);
			this.btnCategory.TabIndex = 10;
			this.btnCategory.Text = "...";
			this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(334, 82);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(60, 23);
			this.btnClose.TabIndex = 13;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnHelp.Location = new System.Drawing.Point(274, 82);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(60, 23);
			this.btnHelp.TabIndex = 12;
			this.btnHelp.Text = "&Help";
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnPrint.Location = new System.Drawing.Point(8, 82);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(60, 23);
			this.btnPrint.TabIndex = 11;
			this.btnPrint.Text = "&Execute";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// IVInventoryStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(402, 115);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCategory);
			this.Controls.Add(this.btnLocation);
			this.Controls.Add(this.btnMasLoc);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.txtMasLoc);
			this.Controls.Add(this.lblCCN);
			this.Controls.Add(this.cboCCN);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.lblLocation);
			this.Controls.Add(this.lblMasLoc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "IVInventoryStatus";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Inventory Status Report";
			this.Load += new System.EventHandler(this.InventoryStatus_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion




		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		/// <summary>
		/// InventoryStatus_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void InventoryStatus_Load(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".InventoryStatus_Load()";
			try
			{
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW ,MessageBoxIcon.Warning);
					return;
				}
				// Load combo box
				DataSet dstCCN = boUtil.ListCCN();
				cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
				cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
				cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
				FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],MST_CCNTable.CODE_FLD,MST_CCNTable.CCNID_FLD,MST_CCNTable.TABLE_NAME);
				if (SystemProperty.CCNID != 0)
				{
					cboCCN.SelectedValue = SystemProperty.CCNID;
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// btnMasLoc_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void btnMasLoc_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					if ((txtMasLoc.Tag != null) && (int.Parse(txtMasLoc.Tag.ToString())) != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtLocation.Text = string.Empty;
						txtLocation.Tag = null;
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// btnLocation_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void btnLocation_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnLocation_Click()";
			try
			{
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected Master Location
				if (txtMasLoc.Text.Trim() != string.Empty)
				{
					htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, int.Parse(txtMasLoc.Tag.ToString()));	
				}
				else //User has not selected Master Location
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtLocation.Text = string.Empty;
					txtMasLoc.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, true);
				if (drwResult != null)
				{
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// btnCategory_Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void btnCategory_Click(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnCategory_Click()";
			try
			{
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, true);
				if (drwResult != null)
				{
					txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}

		private void txtMasLoc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnMasLoc_Click()";
			try
			{
				if (!txtMasLoc.Modified) return;
				if (txtMasLoc.Text.Trim() == string.Empty)
				{
					txtMasLoc.Tag = null;
					txtLocation.Text = string.Empty;
					txtLocation.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected CCN
				if (cboCCN.SelectedIndex != -1)
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);	
				}
				else //User has not selected CCN
				{
					htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					if ((txtMasLoc.Tag != null) && (int.Parse(txtMasLoc.Tag.ToString())) != int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
					{
						txtLocation.Text = string.Empty;
						txtLocation.Tag = null;
					}
					txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
					txtMasLoc.Tag = drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
				}
				else
					e.Cancel = true;
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtMasLoc_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtMasLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnMasLoc_Click(sender, e);
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtLocation_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtLocation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocation_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnLocation_Click(sender, e);
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtCategory_KeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtCategory_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4)
				{
					btnCategory_Click(sender, e);
				}
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex) 
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR,MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION,MessageBoxIcon.Error);
				}
			}

		}
		/// <summary>
		/// txtLocation_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtLocation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocation_Validating()";
			try
			{
				if (!txtLocation.Modified) return;
				if (txtLocation.Text.Trim() == string.Empty)
				{
					txtLocation.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				Hashtable htbCriteria = new Hashtable();
				//User has selected Master Location
				if (txtMasLoc.Text.Trim() != string.Empty)
				{
					htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, int.Parse(txtMasLoc.Tag.ToString()));	
				}
				else //User has not selected Master Location
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtLocation.Text = string.Empty;
					txtMasLoc.Focus();
					return;
				}
				drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD, txtLocation.Text.Trim(), htbCriteria, false);
				if (drwResult != null)
				{
					txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
					txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
				}
				e.Cancel = true;
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}
		/// <summary>
		/// txtCategory_Validating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author>Trada</author>
		/// <date>Friday, October 28 2005</date>
		private void txtCategory_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtCategory_Validating()";
			try
			{
				if (!txtCategory.Modified) return;
				if (txtCategory.Text.Trim() == string.Empty)
				{
					txtCategory.Tag = null;
					return;
				}
				DataRowView drwResult = null;
				drwResult = FormControlComponents.OpenSearchForm(ITM_CategoryTable.TABLE_NAME, ITM_CategoryTable.CODE_FLD, txtCategory.Text.Trim(), null, false);
				if (drwResult != null)
				{
					txtCategory.Text = drwResult[ITM_CategoryTable.CODE_FLD].ToString();
					txtCategory.Tag = drwResult[ITM_CategoryTable.CATEGORYID_FLD];
				}
				e.Cancel = true;
			}
			catch (PCSException ex)
			{
				// Displays the error message if throwed from PCSException.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// Displays the error message if throwed from system.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					// Log error message into log file.
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					// Show message if logger has an error.
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}

		

		
		/// <summary>
		/// Thachnn: 28/10/2005
		/// Preview the report for this form
		/// Using the "InventoryStatusReport.xml" layout
		/// </summary>
		/// <history>Thachnn: 29/12/2005: Add parameter display to the report. Change USECASE.</history>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{	
			#region Constants
			const string METHOD_NAME = THIS + ".btnPrint_Click()";	
			string mstrReportDefFolder = Application.StartupPath + "\\" +Constants.REPORT_DEFINITION_STORE_LOCATION;
			const string REPORT_LAYOUT_FILE = "InventoryStatusReport.xml";
			const string REPORT_NAME = "Inventory Status";

			const string HEADER = "Header";

			const string REPORTFLD_TITLE = "fldTitle";

			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_DAY				= "fldDay";
			const string REPORTFLD_MONTH			= "fldMonth";
			const string REPORTFLD_YEAR				= "fldYear";			


			const string REPORTFLD_CATEGORY			= "fldCategory";
			const string REPORTFLD_PARTNUMBER		= "fldPartNumber";
			const string REPORTFLD_PARTNAME			= "fldPartName";
			const string REPORTFLD_MODEL			= "fldModel";
			const string REPORTFLD_STOCKUM			= "fldStockUM";
			const string REPORTFLD_LOCATION			= "fldLocation";
			const string REPORTFLD_OHQTY			= "fldOHQty";
			const string REPORTFLD_COMMITQTY		= "fldCommitQty";
			const string REPORTFLD_AVAILABLEQTY		= "fldAvailableQty";
			const string REPORTFLD_TYPE				= "fldType";
			const string REPORTFLD_SOURCE			= "fldSource";
			const string REPORTFLD_SAFETYSTOCK		= "fldSafetyStock";
			const string REPORTFLD_LOT				= "fldLot"; 
			const string REPORTFLD_WARNING			= "fldWarning";	
			

			#region QUERY COLUMMS
			const string CATEGORY_COL = "[Category]";
			const string PARTNUMBER_COL = "[Part No.]";
			const string PARTNAME_COL = "[Part Name]";
			const string MODEL_COL = "[Model]";
			const string STOCKUM_COL = "[Stock UM]";
			const string LOCATION_COL = "[Location]";				
			const string OHQTY_COL = "[OH Qty]";
			const string COMMITQTY_COL = "[Commit Qty]";
			const string AVAILABLEQTY_COL = "[Available Qty]";
			const string TYPE_COL = "[Type]";
			const string SOURCE_COL = "[Source]";
			const string SAFETYSTOCK_COL = "[Safety Stock]";
			const string LOT_COL = "[Lot]";
			const string WARNING_COL = "[Warning]";                


			#endregion


			#endregion				

			Cursor = Cursors.WaitCursor;
				
			#region /// Build dtbResult DataTable
			DataTable dtbResult;
			try 
			{
				// Check Mandatory
				if(cboCCN.SelectedValue == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
					cboCCN.Focus();
					Cursor = Cursors.Default;
					return;
				}
				if(txtMasLoc.Tag == null)
				{
					PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
					txtMasLoc.Focus();
					Cursor = Cursors.Default;
					return;
				}

				PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
				int nCCNID = int.Parse(cboCCN.SelectedValue.ToString());
				int nMasLocationID = int.Parse(txtMasLoc.Tag.ToString());

				// if input null, then we send the int.MinValue to the BO function
				int nLocationID = int.MinValue;
				int nCategoryID = int.MinValue;
				if(txtLocation.Tag != null)
				{
					nLocationID = int.Parse(txtLocation.Tag.ToString());
				}
				if(txtCategory.Tag != null)
				{
					nCategoryID = int.Parse(txtCategory.Tag.ToString());
				}
				dtbResult = objBO.GetInventoryStatusFromCCNMasLocLocationCategory(nCCNID,nMasLocationID,nLocationID,nCategoryID);
			}
			catch
			{
				dtbResult = new DataTable();
			}		
			#endregion

			try
			{
				ReportBuilder objRB;	
				objRB = new ReportBuilder();
				try
				{
					objRB.ReportName = REPORT_NAME;						
					objRB.SourceDataTable = dtbResult;					
				}
				catch (Exception ex)
				{
					/// we can't preview while we don't have any data
					return;
				}

				#region BUILD INVENTORY STATUS REPORT FIELD					
				try
				{
					objRB.ReportDefinitionFolder = mstrReportDefFolder;
					objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;					
					if(objRB.AnalyseLayoutFile() == false)
					{
						PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
						return;
					}
					//objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
					objRB.UseLayoutFile = true;	// always use layout file
				}
				catch
				{
					objRB.UseLayoutFile = false;
					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error);
				}
				#endregion				

				objRB.MakeDataTableForRender();
				//grid.DataSource = objRB.RenderDataTable;

				#region RENDER TO PRINT PREVIEW				
				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
				printPreview.FormTitle = REPORT_NAME;
				objRB.ReportViewer = printPreview.ReportViewer;				
				objRB.RenderReport();	
			


				#region COMPANY INFO // header information get from system params
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_COMPANY,SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_ADDRESS,SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_TEL,SystemProperty.SytemParams.Get(SystemParam.TEL));					
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_FAX,SystemProperty.SytemParams.Get(SystemParam.FAX));					
				}
				catch{}
				#endregion

				#region DRAW Parameters
				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				arrParamAndValue.Add(lblMasLoc.Text, txtMasLoc.Text);
				if(txtLocation.Text.Trim() != string.Empty)
				{
					arrParamAndValue.Add(lblLocation.Text, txtLocation.Text);
				}
				if(txtCategory.Text.Trim() != string.Empty)
				{
					arrParamAndValue.Add(lblCategory.Text,txtCategory.Text );
				}
			
				/// anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
				objRB.GetSectionByName(HEADER).CanGrow = true;
				objRB.DrawParameters( objRB.GetSectionByName(HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

				#endregion

				/// there are some hardcode numbers here
				/// but these numbers are use ONLY ONE and ONLY USED HERE, so we don't need to define constant for it.
				objRB.DrawBoxGroup_Madeby_Checkedby_Approvedby(objRB.GetSectionByName("Header"), 15945 -1400-1400-1400, 600, 1400, 1300, 200);


				#region DAY--MONTH--YEAR INFO
				DateTime dtm;
				try
				{
					dtm = (new UtilsBO()).GetDBDate();
				}
				catch
				{
					dtm = DateTime.Now;
				}

				try
				{
					objRB.DrawPredefinedField(REPORTFLD_DAY,dtm.Day.ToString("00"));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_MONTH,dtm.Month.ToString("00"));
				}
				catch{}
				try
				{
					objRB.DrawPredefinedField(REPORTFLD_YEAR,dtm.Year.ToString("0000"));
				}
				catch{}				
				#endregion
			
				try	// mapping report field with table column
				{
					objRB.DrawPredefinedField(REPORTFLD_CATEGORY,CATEGORY_COL);
					objRB.DrawPredefinedField(REPORTFLD_PARTNUMBER,PARTNUMBER_COL);
					objRB.DrawPredefinedField(REPORTFLD_PARTNAME,PARTNAME_COL);
					objRB.DrawPredefinedField(REPORTFLD_MODEL,MODEL_COL);
					objRB.DrawPredefinedField(REPORTFLD_STOCKUM,STOCKUM_COL);
					objRB.DrawPredefinedField(REPORTFLD_LOCATION,LOCATION_COL);				
					objRB.DrawPredefinedField(REPORTFLD_OHQTY,OHQTY_COL);
					objRB.DrawPredefinedField(REPORTFLD_COMMITQTY,COMMITQTY_COL);
					objRB.DrawPredefinedField(REPORTFLD_AVAILABLEQTY,AVAILABLEQTY_COL);
					objRB.DrawPredefinedField(REPORTFLD_TYPE,TYPE_COL);
					objRB.DrawPredefinedField(REPORTFLD_SOURCE,SOURCE_COL);
					objRB.DrawPredefinedField(REPORTFLD_SAFETYSTOCK,SAFETYSTOCK_COL);
					objRB.DrawPredefinedField(REPORTFLD_LOT,LOT_COL);
					objRB.DrawPredefinedField(REPORTFLD_WARNING,WARNING_COL);                
				}
				catch{}		
				

				objRB.RefreshReport();
				printPreview.Show();						
				this.Cursor = Cursors.Default;

				#endregion
			}
			catch(Exception ex)
			{
				//DEBUG: PCSMessageBox.Show(ErrorCode.MESSAGE_RENVIEW_REPORT,MessageBoxIcon.Error);
				//
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
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
			finally
			{
				Cursor = Cursors.Default;
			}			
		}

	}
}
