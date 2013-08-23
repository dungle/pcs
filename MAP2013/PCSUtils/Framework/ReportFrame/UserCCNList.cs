using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Framework.ReportFrame
{
	/// <summary>
	/// Summary description for UserCCNList.
	/// </summary>
	public class UserCCNList : Form
	{
		private CheckedListBox chlList;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private Button btnOK;
		private Button btnClose;

		private const string THIS = "PCSUtils.Framework.ReportFrame.UserCCNList";

		private DataTable tblData;
		private ArrayList marrReturnsList;
		public ArrayList ReturnList
		{
			get { return this.marrReturnsList; }
		}

        private string strDisplayMember;
        private C1.C1Report.C1Report c1Report1;
        private C1.C1Preview.C1PrintDocument c1PrintDocument1;
        private C1.Win.C1Preview.C1PrintPreviewDialog c1PrintPreviewDialog1;
		private string strValueMember;

		public UserCCNList(string strTableName)
		{
			const string METHOD_NAME = THIS + ".ctor()";
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			marrReturnsList = new ArrayList();
			try
			{
				UtilsBO boUtils = new UtilsBO();
				switch (strTableName)
				{
					case MST_CCNTable.TABLE_NAME:
						strDisplayMember = MST_CCNTable.CODE_FLD;
						strValueMember = MST_CCNTable.CCNID_FLD;
						// list all CCN to list
						tblData = boUtils.ListCCNForCheckListBox();
						break;
					case Sys_UserTable.TABLE_NAME:
						strDisplayMember = Sys_UserTable.USERNAME_FLD;
						strValueMember = Sys_UserTable.USERNAME_FLD;
						// list all User to list
						tblData = boUtils.ListUser();
						break;
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserCCNList));
            this.c1PrintPreviewDialog1 = new C1.Win.C1Preview.C1PrintPreviewDialog();
            this.chlList = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.c1Report1 = new C1.C1Report.C1Report();
            this.c1PrintDocument1 = new C1.C1Preview.C1PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane)).BeginInit();
            this.c1PrintPreviewDialog1.PrintPreviewControl.SuspendLayout();
            this.c1PrintPreviewDialog1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1PrintPreviewDialog1
            // 
            resources.ApplyResources(this.c1PrintPreviewDialog1, "c1PrintPreviewDialog1");
            this.c1PrintPreviewDialog1.Name = "C1PrintPreviewDialog";
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl
            // 
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.OutlineView
            // 
            resources.ApplyResources(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView, "c1PrintPreviewDialog1.PrintPreviewControl.OutlineView");
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.LineColor = System.Drawing.Color.Empty;
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewOutlineView.Name = "OutlineView";
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane.IntegrateExternalTools = true;
            resources.ApplyResources(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane, "c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane");
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel
            // 
            resources.ApplyResources(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel, "c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel");
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(200, 240);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            // 
            // c1PrintPreviewDialog1.PrintPreviewControl.ThumbnailView
            // 
            resources.ApplyResources(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView, "c1PrintPreviewDialog1.PrintPreviewControl.ThumbnailView");
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewThumbnailView.UseImageAsThumbnail = false;
            resources.ApplyResources(this.c1PrintPreviewDialog1.PrintPreviewControl, "c1PrintPreviewDialog1.PrintPreviewControl");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ImageTransparentColo" +
                    "r")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Name = "btnFileOpen";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.Tag = "C1PreviewActionEnum.FileOpen";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Open.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ImageTransparen" +
                    "tColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Name = "btnPageSetup";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.Tag = "C1PreviewActionEnum.PageSetup";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.PageSetup.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ImageTransparentCol" +
                    "or")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Name = "btnPrint";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.Tag = "C1PreviewActionEnum.Print";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Print.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ImageTransparentCo" +
                    "lor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Name = "btnReflow";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.Tag = "C1PreviewActionEnum.Reflow";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Reflow.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ImageTransparentColo" +
                    "r")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Name = "btnFileSave";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.Tag = "C1PreviewActionEnum.FileSave";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.File.Save.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ImageTransp" +
                    "arentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Name = "btnGoFirst";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.Tag = "C1PreviewActionEnum.GoFirst";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoFirst.ToolTipText" +
                    "");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ImageTranspa" +
                    "rentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Name = "btnGoLast";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.Tag = "C1PreviewActionEnum.GoLast";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoLast.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ImageTranspa" +
                    "rentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Name = "btnGoNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.Tag = "C1PreviewActionEnum.GoNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoNext.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ImageTranspa" +
                    "rentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Name = "btnGoPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.Tag = "C1PreviewActionEnum.GoPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.GoPrev.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ImageTr" +
                    "ansparentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Name = "btnHistoryNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.Tag = "C1PreviewActionEnum.HistoryNext";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryNext.ToolTip" +
                    "Text");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ImageTr" +
                    "ansparentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Name = "btnHistoryPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.Tag = "C1PreviewActionEnum.HistoryPrev";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.HistoryPrev.ToolTip" +
                    "Text");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Name = "lblOfPages";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Tag = "C1PreviewActionEnum.GoPageCount";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Text = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblOfPages.Text");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Name = "lblPage";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Tag = "C1PreviewActionEnum.GoPageLabel";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Text = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.LblPage.Text");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Name = "txtPageNo";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Tag = "C1PreviewActionEnum.GoPageNumber";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Text = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.PageNo.Text");
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Navigation.ToolTipPageNo = null;
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Checked = true;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ImageTranspare" +
                    "ntColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Name = "btnPageContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.Tag = "C1PreviewActionEnum.PageContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Continuous.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ImageTransparentCo" +
                    "lor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Name = "btnPageFacing";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.Tag = "C1PreviewActionEnum.PageFacing";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Facing.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ImageTra" +
                    "nsparentColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Name = "btnPageFacingContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.Tag = "C1PreviewActionEnum.PageFacingContinuous";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.FacingContinuous.ToolTipT" +
                    "ext");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ImageTransparentCo" +
                    "lor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Name = "btnPageSingle";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.Tag = "C1PreviewActionEnum.PageSingle";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Page.Single.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.ImageTransparentColo" +
                    "r")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Name = "btnFind";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.Tag = "C1PreviewActionEnum.Find";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Find.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Checked = true;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ImageTransparentColo" +
                    "r")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Name = "btnHandTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.Tag = "C1PreviewActionEnum.HandTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.Hand.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.ImageTranspare" +
                    "ntColor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Name = "btnSelectTextTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.Tag = "C1PreviewActionEnum.SelectTextTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Text.SelectText.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Name = "dropZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.DropZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ToolTipZoomFactor = null;
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Name = "txtZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Tag = "C1PreviewActionEnum.ZoomFactor";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Text = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomFactor.Text");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ImageTransparentCo" +
                    "lor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Name = "btnZoomIn";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.Tag = "C1PreviewActionEnum.ZoomIn";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomIn.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ImageTransparentC" +
                    "olor")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Name = "btnZoomOut";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.Tag = "C1PreviewActionEnum.ZoomOut";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOut.ToolTipText");
            // 
            // 
            // 
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomInTool,
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomOutTool});
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image = ((System.Drawing.Image)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Image")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ImageTransparentColor = ((System.Drawing.Color)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ImageTransparent" +
                    "Color")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Name = "btnZoomTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Size = ((System.Drawing.Size)(resources.GetObject("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Size")));
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.Tag = "C1PreviewActionEnum.ZoomInTool";
            this.c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ToolTipText = resources.GetString("c1PrintPreviewDialog1.PrintPreviewControl.ToolBars.Zoom.ZoomTool.ToolTipText");
            this.c1PrintPreviewDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
            // 
            // chlList
            // 
            resources.ApplyResources(this.chlList, "chlList");
            this.chlList.Name = "chlList";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "c1Report1";
            // 
            // c1PrintDocument1
            // 
            this.c1PrintDocument1.DocumentInfo.Creator = "C1Reports Engine version 2.6.20101.54005";
            // 
            // UserCCNList
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chlList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "UserCCNList";
            this.Load += new System.EventHandler(this.UserCCNList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl.PreviewPane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrintPreviewDialog1.PrintPreviewControl)).EndInit();
            this.c1PrintPreviewDialog1.PrintPreviewControl.ResumeLayout(false);
            this.c1PrintPreviewDialog1.PrintPreviewControl.PerformLayout();
            this.c1PrintPreviewDialog1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void UserCCNList_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".UserCCNList_Load()";
			try
			{
				#region Security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					this.Close();
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW);
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}
				#endregion
				chlList.DataSource = tblData;
				chlList.DisplayMember = strDisplayMember;
				chlList.ValueMember = strValueMember;
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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

		private void btnOK_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnOK_Click()";
			try
			{
				// get all checked item and add to arraylist
				foreach (int intIndex in chlList.CheckedIndices)
				{
					marrReturnsList.Add(tblData.Rows[intIndex][strValueMember]);
				}
				marrReturnsList.TrimToSize();
				// close the form
				this.Close();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				// log message.
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
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				// log message.
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

		private void btnClose_Click(object sender, EventArgs e)
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
	}
}
