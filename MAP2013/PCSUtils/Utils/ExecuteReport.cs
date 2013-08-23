using System;
using System.Collections;
using System.Data;
using System.Drawing.Printing;
using System.Reflection;
using C1.C1Report;
using C1.Win.C1Preview;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Diagnostics;

namespace PCSUtils.Utils
{
    public class ExecuteReport : MarshalByRefObject, IDynamicReport
    {
        private string mConnectionString;
        private ReportBuilder mReportBuilder;
        private C1PrintPreviewControl mReportViewer;
        private object mResult;
        private string mLayoutFile;
        private readonly string mstrReportDefFolder = System.Windows.Forms.Application.StartupPath + "\\" +
                                                     PCSComUtils.Common.Constants.REPORT_DEFINITION_STORE_LOCATION;


        /// <summary>
        /// ConnectionString, provide for the Dynamic Report
        /// ALlow Dynamic Report to access the DataBase of PCS
        /// </summary>
        public string PCSConnectionString
        {
            get { return mConnectionString; }
            set { mConnectionString = value; }
        }

        /// <summary>
        /// Report Builder Utility Object
        /// Dynamic Report can use this object to render, modify, layout the report
        /// </summary>
        public ReportBuilder PCSReportBuilder
        {
            get { return mReportBuilder; }
            set { mReportBuilder = value; }
        }

        /// <summary>
        /// ReportViewer Object, provide for the DynamicReport, 
        /// allow Dynamic Report to manipulate with the REportViewer, 
        /// modify the report after rendered if needed
        /// </summary>
        public C1PrintPreviewControl PCSReportViewer
        {
            get { return mReportViewer; }
            set { mReportViewer = value; }
        }
        private string mReportFolder = string.Empty;
        public string ReportDefinitionFolder
        {
            get { return mReportFolder; }
            set { mReportFolder = value; }
        }

        /// <summary>
        /// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
        /// </summary>
        public object Result
        {
            get { return mResult; }
            set { mResult = value; }
        }

        private bool mUseReportViewerRenderEngine = true;

        /// <summary>
        /// Notify PCS whether the rendering report process is run by
        /// this IDynamicReport or the ReportViewer Engine (in the ReportViewer form)
        /// </summary>
        public bool UseReportViewerRenderEngine
        {
            get { return mUseReportViewerRenderEngine; }
            set { mUseReportViewerRenderEngine = value; }
        }

        /// <summary>
        /// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
        /// </summary>				

        private string mstrReportLayoutFile = string.Empty;
        /// <summary>
        /// Inform External Process about the Layout file
        /// in which PCS instruct to use
        /// (PCS will assign this property while ReportViewer Form execute,
        /// ReportVIewer form will use the layout file in the report config entry to put in this property)
        /// </summary>		
        public string ReportLayoutFile
        {
            get
            {
                return mstrReportLayoutFile;
            }
            set
            {
                mstrReportLayoutFile = value;
            }
        }

        public object Invoke(string pstrMethod, object[] pobjParameters)
        {
            return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsData"></param>
        /// <param name="pLayOutFile"></param>
        /// <param name="strFileExcel"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iStartCol"></param>
        /// <param name="strColExel1"></param>
        /// <param name="strColExcel2"></param>
        /// <param name="strChartName"></param>
        /// <param name="iCount"></param>
        public void showReportImageExcelRevenue(DataSet dsData, string pLayOutFile, string strFileExcel, int iStartRow, int iStartCol, string strColExel1, string strColExcel2, string strColExcel3, string strColExcel4, string strChartName, int iYear1, int iYear2, int iYear3)
        {

            #region Report layoutint
            C1Report rptReport = new C1Report();
            mLayoutFile = pLayOutFile;

            string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mstrReportDefFolder + "\\" + mLayoutFile);
            rptReport.Load(mstrReportDefFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);

            rptReport.Layout.PaperSize = PaperKind.A4;
            Field fldChart = rptReport.Fields["fldChart"];
            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + FormControlComponents.NowToUTCString() + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                objXLS.GetCell(iStartRow - 1, iStartCol + 1).Value2 = iYear1;
                objXLS.GetCell(iStartRow - 1, iStartCol + 2).Value2 = iYear2;
                objXLS.GetCell(iStartRow - 1, iStartCol + 3).Value2 = iYear3;
                for (int i = 0; i < 12; i++)
                {   //strColExel1
                    try
                    {
                        //Cot Thang
                        objXLS.GetCell(iStartRow + i, iStartCol).Value2 = i + 1;
                    }
                    catch
                    {

                    }

                    //strColExcel3
                }
                for (int j = 0; j < dsData.Tables[0].Rows.Count; j++)
                {
                    // Cot Price1
                    if (Convert.ToInt32(dsData.Tables[0].Rows[j]["Years"].ToString()) == iYear1)
                    {
                        try
                        {
                            objXLS.GetCell(iStartRow - 1 + Convert.ToInt32(dsData.Tables[0].Rows[j]["Months"].ToString()), iStartCol + 1).Value2 = Convert.ToDecimal(dsData.Tables[0].Rows[j]["DOANH_THU"].ToString());
                            //objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = i;
                        }
                        catch
                        { }
                    }

                    // Cot Price2
                    if (Convert.ToInt32(dsData.Tables[0].Rows[j]["Years"].ToString()) == iYear2)
                    {
                        try
                        {
                            objXLS.GetCell(iStartRow - 1 + Convert.ToInt32(dsData.Tables[0].Rows[j]["Months"].ToString()), iStartCol + 2).Value2 = Convert.ToDecimal(dsData.Tables[0].Rows[j]["DOANH_THU"].ToString());
                            //objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = i;
                        }
                        catch
                        { }
                    }


                    // Cot Price3
                    if (Convert.ToInt32(dsData.Tables[0].Rows[j]["Years"].ToString()) == iYear3)
                    {
                        try
                        {
                            objXLS.GetCell(iStartRow - 1 + Convert.ToInt32(dsData.Tables[0].Rows[j]["Months"].ToString()), iStartCol + 3).Value2 = Convert.ToDecimal(dsData.Tables[0].Rows[j]["DOANH_THU"].ToString());
                            //objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = i;
                        }
                        catch
                        { }
                    }

                }
                // hoan thanh

                ChartObject chart = objXLS.GetChart(strChartName);
                chart.Chart.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap, XlPictureAppearance.xlScreen);
                Image image = (Image)Clipboard.GetDataObject().GetData(typeof(Bitmap));
                fldChart.Visible = true;
                fldChart.Text = "";
                fldChart.Picture = image;

            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

            // set datasource object that provides data to report.
            //rptReport.DataSource.Recordset = dsData.Tables[0];
            // render report
            // rptReport.Render();

            //C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
            // ppvViewer.ReportViewer.NavigationBar.Visible = false;
            // ppvViewer.ReportViewer.PreviewPane.ZoomMode = ZoomModeEnum.ActualSize;
            //ppvViewer.ReportViewer.Document = rptReport.Document;
            //  ppvViewer.ReportFile = mLayoutFile;
            // ppvViewer.Show();
        }

        public void ExportExcel(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {

                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {   //strColExel1
                    objXLS.GetCell(iStartRow + i, iStartCol).Value2 = Convert.ToDecimal(i.ToString());
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["Product_Name"].ToString();
                    objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["Code"].ToString();

                    objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = Convert.ToDecimal(dsData.Tables[0].Rows[i]["Qty"].ToString());
                    objXLS.GetCell(iStartRow + i, iStartCol + 8).Value2 = Convert.ToDecimal(dsData.Tables[0].Rows[i]["Price"].ToString());
                    try
                    {
                        objXLS.GetCell(iStartRow + i, iStartCol + 9).Value2 = (Convert.ToDecimal(dsData.Tables[0].Rows[i]["Price"].ToString())) * (Convert.ToDecimal(dsData.Tables[0].Rows[i]["Qty"].ToString()));
                    }
                    catch
                    { }
                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelSaleOrder(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, DateTime dDate)
        {
            try
            {
                #region Report layout

                string EXCEL_FILE = strFileExcel;

                string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
                //FormControlComponents.NowToUTCString() 
                string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

                /// Copy layout excel report file to ExcelReport folder with a UTC datetime name

                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
                ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

                try
                {
                    objXLS.GetCell(4, 1).Value2 = "Tháng " + dDate.ToString("MM") + " năm " + dDate.ToString("yyyy");
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1
                        //objXLS.GetCell(iStartRow + i, iStartCol).Value2 = Convert.ToDecimal(i.ToString());
                        objXLS.GetCell(iStartRow + i, iStartCol).Value2 = dsData.Tables[0].Rows[i]["TransDate"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["tongyc"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_Xe_thue_ngoai"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_XHVC"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_KHVC"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["SLKO_DAPUNG"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 7).Value2 = dsData.Tables[0].Rows[i]["SLKO_DAPUNGTHIEU_HANG"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 8).Value2 = dsData.Tables[0].Rows[i]["SLKO_DAPUNGKHONG_CO_XE"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 9).Value2 = dsData.Tables[0].Rows[i]["DATHANG"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 10).Value2 = dsData.Tables[0].Rows[i]["Code"];


                    }


                #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                    objXLS.CloseWorkbook();
                    objXLS.Dispose();
                    objXLS = null;

                    #endregion
                }
                Process.Start(strDestinationFilePath);
            }
            catch
            {
                MessageBox.Show("File dang duoc mo");
            }
        }

        public void ExportExcelSaleOrderByDay(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, DateTime dDate, int iCCN)
        {
            try
            {
                #region Report layout

                string EXCEL_FILE = strFileExcel;
                string strTextTitle = "";
                if (iCCN == 2) strTextTitle = "Phòng : Bán hàng Xuân Hòa";
                if (iCCN == 6) strTextTitle = "Phòng : Bán hàng Cầu Diễn";

                string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
                //FormControlComponents.NowToUTCString() 
                string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

                /// Copy layout excel report file to ExcelReport folder with a UTC datetime name

                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
                ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

                try
                {
                    //objXLS.GetCell(4, 1).Value2 = "Tháng " + dDate.ToString("MM") + " năm " + dDate.ToString("yyyy");
                    objXLS.GetCell(3, 1).Value2 = strTextTitle;
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1
                        //objXLS.GetCell(iStartRow + i, iStartCol).Value2 = Convert.ToDecimal(i.ToString());
                        objXLS.GetCell(iStartRow + i, iStartCol).Value2 = dsData.Tables[0].Rows[i]["TransDate"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TENKH"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["TenSP"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["OrderQuantity"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["DELIQty"];
                        try
                        {
                            double dOrderQuantity = 0;
                            double dDELIQty = 0;
                            try
                            {
                                dOrderQuantity = Convert.ToDouble(dsData.Tables[0].Rows[i]["OrderQuantity"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                dDELIQty = Convert.ToDouble(dsData.Tables[0].Rows[i]["DELIQty"].ToString());
                            }
                            catch
                            { }

                            objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dOrderQuantity - dDELIQty;
                        }
                        catch
                        {
                        }
                        objXLS.GetCell(iStartRow + i, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["RequiredDate"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 7).Value2 = dsData.Tables[0].Rows[i]["DueDate"];

                        objXLS.GetCell(iStartRow + i, iStartCol + 8).Value2 = dsData.Tables[0].Rows[i]["Carrier"];
                        objXLS.GetCell(iStartRow + i, iStartCol + 9
                            ).Value2 = dsData.Tables[0].Rows[i]["Comment"];

                        try
                        {
                            double dThanhtien = 0;
                            double dOrderQuantity = 0;
                            double dPrice = 0;
                            try
                            {
                                dOrderQuantity = Convert.ToDouble(dsData.Tables[0].Rows[i]["OrderQuantity"].ToString());
                            }
                            catch
                            {
                            }
                            try
                            {
                                dPrice = Convert.ToDouble(dsData.Tables[0].Rows[i]["Unitprice"].ToString());
                            }
                            catch
                            {
                            }
                            dThanhtien = dOrderQuantity * dPrice;
                            objXLS.GetCell(iStartRow + i, iStartCol + 10).Value2 = dThanhtien;

                        }
                        catch
                        { }


                    }


                #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                    objXLS.CloseWorkbook();
                    objXLS.Dispose();
                    objXLS = null;

                    #endregion
                }
                Process.Start(strDestinationFilePath);
            }
            catch
            {
                MessageBox.Show("File dang duoc mo");
            }
        }
        public void ExportExcelMonths(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, DateTime dDate, int iWeeks)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                //Tháng 11  năm 2009
                string strDate = "Tháng" + dDate.ToString("MM") + " năm " + dDate.ToString("yyyy");
                DateTime d = new DateTime();
                //d.
                objXLS.GetCell(4, 12).Value2 = strDate;
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {   //strColExel1
                    int iWeek = 0;
                    try
                    {
                        iWeek = Convert.ToInt32(dsData.Tables[0].Rows[i]["TransDate"].ToString()) - iWeeks + 1;
                    }
                    catch
                    { }
                    if (iWeek == 0) iWeek = 1;
                    if (iWeek == 5) iWeek = 1;
                    objXLS.GetCell(iStartRow + i, iStartCol).Value2 = iWeek;
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["SLYC_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["SLYC_CD"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 7).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_XECTY_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 8).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_XECTY_CD"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 9).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_Xe_thue_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 10).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_Xe_thue_CD"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 11).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_KHVC_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 12).Value2 = dsData.Tables[0].Rows[i]["SLDAPUNG_KHVC_CD"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 16).Value2 = dsData.Tables[0].Rows[i]["THIEU_HANG_xh"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 17).Value2 = dsData.Tables[0].Rows[i]["THIEU_HANG_cd"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 19).Value2 = dsData.Tables[0].Rows[i]["KHONG_CO_XE_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 20).Value2 = dsData.Tables[0].Rows[i]["THIEU_HANG_cd"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 19).Value2 = dsData.Tables[0].Rows[i]["HANGMOI_XH"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 20).Value2 = dsData.Tables[0].Rows[i]["HANGMOI_CD"];

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelYear(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    double T1 = 0;
                    double UT1 = 0;

                    double T2 = 0;
                    double UT2 = 0;

                    double T3 = 0;
                    double UT3 = 0;

                    double T4 = 0;
                    double UT4 = 0;

                    double T5 = 0;
                    double UT5 = 0;

                    double T6 = 0;
                    double UT6 = 0;

                    double T7 = 0;
                    double UT7 = 0;

                    double T8 = 0;
                    double UT8 = 0;

                    double T9 = 0;
                    double UT9 = 0;

                    double T10 = 0;
                    double UT10 = 0;

                    double T11 = 0;
                    double UT11 = 0;

                    double T12 = 0;
                    double UT12 = 0;



                    for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[q]["MA_KH"].ToString() == arrList[k].ToString())
                        {
                            try
                            {
                                T1 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T1"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT1 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT1"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T2 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T2"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT2 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT2"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T3 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T3"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT3 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT3"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T4 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T4"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT4 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT4"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T5 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T5"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT5 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT5"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T6 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T6"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT6 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT6"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T7 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT7 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T8 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT8 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T9 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T9"].ToString());
                            }
                            catch { }
                            try
                            {
                                UT9 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT9"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T10 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT10 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T11 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T11"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT11 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT1"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T12 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T12"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT12 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT12"].ToString());
                            }
                            catch
                            { }
                        }
                    }

                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[i]["MA_KH"].ToString() == arrList[k].ToString())
                        {
                            iRow++;
                            int iCurRow = iRow + k;
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["TENKH"].ToString().ToUpper().Trim();
                                }
                                catch
                                { }
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = T1;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = UT1;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = T2;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 5, iStartRow + iCurRow, iStartCol + 5).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = UT2;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 6, iStartRow + iCurRow, iStartCol + 6).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = T3;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 7, iStartRow + iCurRow, iStartCol + 7).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = UT3;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 8, iStartRow + iCurRow, iStartCol + 8).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = T4;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 9, iStartRow + iCurRow, iStartCol + 9).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = UT4;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 10, iStartRow + iCurRow, iStartCol + 10).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = T5;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 11, iStartRow + iCurRow, iStartCol + 11).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = UT5;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 12, iStartRow + iCurRow, iStartCol + 12).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = T6;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 13, iStartRow + iCurRow, iStartCol + 13).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 14).Value2 = UT6;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 14, iStartRow + iCurRow, iStartCol + 14).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 17).Value2 = T7;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 17, iStartRow + iCurRow, iStartCol + 17).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 18).Value2 = UT7;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 18, iStartRow + iCurRow, iStartCol + 18).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 19).Value2 = T8;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 19, iStartRow + iCurRow, iStartCol + 19).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 20).Value2 = UT8;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 20, iStartRow + iCurRow, iStartCol + 20).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 21).Value2 = T9;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 21, iStartRow + iCurRow, iStartCol + 21).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 22).Value2 = UT9;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 22, iStartRow + iCurRow, iStartCol + 22).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 23).Value2 = T10;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 23, iStartRow + iCurRow, iStartCol + 23).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 24).Value2 = UT10;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 24, iStartRow + iCurRow, iStartCol + 24).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 25).Value2 = T11;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 25, iStartRow + iCurRow, iStartCol + 25).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 26).Value2 = UT11;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 26, iStartRow + iCurRow, iStartCol + 26).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 27).Value2 = T12;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 27, iStartRow + iCurRow, iStartCol + 27).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 28).Value2 = UT12;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 28, iStartRow + iCurRow, iStartCol + 28).Font.Bold = true;
                                for (int j = iStartCol; j <= iStartCol + 30; j++)
                                {
                                    objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                }

                                //End
                            }
                            if (k < arrList.ToArray().Length - 1)
                            {
                                iStatus++;
                                iCurRow = iCurRow + 1;
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["TENKH"].ToString().Trim();
                                }
                                catch
                                { }
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TENSP"];

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DONGIA_USD"];


                                //Begin 6 thang dau nam
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T1"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["UT1"];
                                }
                                catch
                                { }
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T2"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT2"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T3"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT3"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T4"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT4"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T5"].ToString());

                                }
                                catch
                                { }
                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT5"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T6"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 14).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT6"].ToString());
                                }
                                catch
                                { }

                                //End
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 15).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["SL_6TDAUNAM"].ToString());

                                }
                                catch
                                { }
                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 16).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["USD_6TDAUNAM"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 17).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T7"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 18).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT7"].ToString());
                                }
                                catch
                                { }
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 19).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T8"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 20).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT8"].ToString());
                                }
                                catch
                                { }
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 21).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T9"].ToString());

                                }
                                catch
                                { }
                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 22).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT9"].ToString());
                                }
                                catch
                                { }

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 23).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T10"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 24).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT10"].ToString());
                                }
                                catch
                                { }
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 25).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T11"].ToString());

                                }
                                catch
                                { }
                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 26).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT11"].ToString());
                                }
                                catch
                                { }
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 27).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T12"].ToString());

                                }
                                catch
                                { }

                                try
                                {

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 28).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT12"].ToString());
                                }
                                catch
                                { }
                            }
                        }

                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDebitExport(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, string strMonth)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                objXLS.GetCell(6, iStartCol + 1).Value2 = strMonth;
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {   //strColExel1

                    objXLS.GetCell(iStartRow + i, iStartCol).Value2 = dsData.Tables[0].Rows[i]["PartyName"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["Dudau"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["PSNO"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["PSCO"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["Ducuoiky"];


                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoiCHieuCongNo(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, string strMonth)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                double dSumPSNO = 0;
                double dSumPSCO = 0;
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        dSumPSNO += Convert.ToDouble(dsData.Tables[0].Rows[i]["PSNO"].ToString());
                    }
                    catch
                    { }
                    try
                    {
                        dSumPSCO += Convert.ToDouble(dsData.Tables[0].Rows[i]["PSCO"].ToString());
                    }
                    catch
                    { }
                }
                objXLS.GetCell(iStartRow - 6, 7).Value2 = dsData.Tables[0].Rows[0]["Dudau"];
                objXLS.GetCell(iStartRow - 5, 7).Value2 = dSumPSNO;
                objXLS.GetCell(iStartRow - 4, 7).Value2 = dSumPSCO;
                objXLS.GetCell(iStartRow - 3, 7).Value2 = dsData.Tables[0].Rows[0]["Ducuoiky"];
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {   //strColExel1
                    try
                    {
                        objXLS.GetCell(iStartRow + i, iStartCol).Value2 = Convert.ToDateTime(dsData.Tables[0].Rows[i]["NGAY_THC"].ToString()).ToString("dd/MM/yyyy");
                    }
                    catch
                    { }
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["CHUNG_TU"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["GHI_CHU"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["TKDU"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["PSNO"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["PSCO"];

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoanhThuTheoDaiLy(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, DateTime fdate, DateTime tDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                double doanhthutong = 0;
                objXLS.GetCell(4, 3).Value2 = fdate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetCell(5, 3).Value2 = tDate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetRange(4, 3, 4, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;
                objXLS.GetRange(5, 3, 5, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;


                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    doanhthutong += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU"].ToString());
                }
                objXLS.GetCell(8, 3).Value2 = "Tổng doanh thu:";
                objXLS.GetRange(8, 3, 8, 3).Font.Bold = true;
                objXLS.GetCell(8, 4).Value2 = doanhthutong;
                objXLS.GetRange(8, 4, 8, 4).Font.Bold = true;
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    double dSumDoanhThu = 0;
                    for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            dSumDoanhThu += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU"].ToString());
                        }
                    }
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[i]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            iRow++;
                            int iCurRow = iRow + k;
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dSumDoanhThu;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                for (int j = iStartCol; j <= iStartCol + 3; j++)
                                {
                                    objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                }

                                //End
                            }
                            iStatus++;
                            iCurRow = iCurRow + 1;
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iRow;
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN"].ToString().Trim();
                            }
                            catch
                            { }
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["Address"];

                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU"];


                        }

                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }


        public void ExportExcelPhaiThuKhachHang(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, DateTime dDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                double doanhthutong = 0;

                if (arrList != null)
                {
                    if (arrList.ToArray().Length > 0)
                    {
                        DateTime fDate = Convert.ToDateTime(dsData.Tables[0].Rows[0]["fDate"].ToString().Trim());
                        objXLS.GetCell(5, 1).Value2 = "Từ ngày :" + dsData.Tables[0].Rows[0]["fDate"].ToString().Trim() + " Đến ngày :" + dsData.Tables[0].Rows[0]["tDate"].ToString().Trim();
                        objXLS.GetCell(8, 4).Value2 = "Phát sinh Tháng " + dDate.ToString("MM");
                        objXLS.GetCell(9, 3).Value2 = " "+fDate.ToString("MM/dd");
                        double totalDUDAUKY = 0;
                        double totalPSNO = 0;
                        double totalPSCO = 0;
                        double totalDUNOCUOI = 0;
                        foreach (DataRow dr in dsData.Tables[0].Rows)
                        {
                            totalDUDAUKY += Convert.ToDouble(dr["DUDAUKY"].ToString());
                            totalPSNO += Convert.ToDouble(dr["PS_NO"].ToString());
                            totalPSCO += Convert.ToDouble(dr["PS_CO"].ToString());
                        }
                        totalDUNOCUOI = totalDUDAUKY + totalPSNO - totalPSCO;
                        objXLS.GetCell(11, 3).Value2 = totalDUDAUKY.ToString();
                        objXLS.GetCell(11, 4).Value2 = totalPSNO.ToString();
                        objXLS.GetCell(11, 5).Value2 = totalPSCO.ToString();
                        objXLS.GetCell(11, 6).Value2 = totalDUNOCUOI.ToString();


                        for (int k = 0; k < arrList.ToArray().Length; k++)
                        {
                            int iStatus = 0;
                            double dSumDUDAUKY = 0;
                            double dSumPSNO = 0;
                            double dSumPSCO = 0;
                            for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    try
                                    {
                                        dSumDUDAUKY += Convert.ToDouble(dsData.Tables[0].Rows[q]["DUDAUKY"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumPSNO += Convert.ToDouble(dsData.Tables[0].Rows[q]["PS_NO"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumPSCO += Convert.ToDouble(dsData.Tables[0].Rows[q]["PS_CO"].ToString());
                                    }
                                    catch
                                    { }
                                }
                            }
                            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[i]["MA_VUNG"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    iRow++;
                                    int iCurRow = iRow + k;
                                    if (iStatus == 0)
                                    {
                                        //Group theo khach hang
                                        try
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                            objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;
                                        }
                                        catch
                                        { }
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dSumDUDAUKY;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 2, iStartRow + iCurRow, iStartCol + 2).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dSumPSNO;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dSumPSCO;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dSumPSNO + dSumDUDAUKY - dSumPSCO;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 5, iStartRow + iCurRow, iStartCol + 5).Font.Bold = true;

                                        for (int j = iStartCol; j <= iStartCol + 7; j++)
                                        {
                                            objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                        }

                                        //End
                                    }
                                    iStatus++;
                                    iCurRow = iCurRow + 1;

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["MA"];
                                    try
                                    {
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["Ten"].ToString().Trim();
                                    }
                                    catch
                                    { }

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DUDAUKY"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["PS_NO"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["PS_CO"];
                                    double dSumDuCuoiky = 0;
                                    try
                                    {
                                        dSumDuCuoiky += Convert.ToDouble(dsData.Tables[0].Rows[i]["DUDAUKY"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumDuCuoiky += Convert.ToDouble(dsData.Tables[0].Rows[i]["PS_NO"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumDuCuoiky -= Convert.ToDouble(dsData.Tables[0].Rows[i]["PS_CO"].ToString());
                                    }
                                    catch
                                    { }
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dSumDuCuoiky;
                                }

                            }
                        }
                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoanhThuTheoNam(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, int iYear)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            int iRowEnd = 0;
            try
            {
                double dSumT1 = 0;
                double dSumT2 = 0;
                double dSumT3 = 0;

                double dSumT4 = 0;
                double dSumT5 = 0;
                double dSumT6 = 0;

                double dSumT7 = 0;
                double dSumT8 = 0;
                double dSumT9 = 0;

                double dSumT10 = 0;
                double dSumT11 = 0;
                double dSumT12 = 0;

                if (arrList != null)
                {
                    if (arrList.ToArray().Length > 0)
                    {
                        objXLS.GetCell(5, 1).Value2 = "Năm :" + iYear.ToString();
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            try
                            {
                                dSumT1 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T1"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT2 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T2"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT3 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T3"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT4 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T4"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT5 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T5"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT6 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T6"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT7 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT8 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT9 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T9"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT10 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT11 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T11"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumT12 += Convert.ToDouble(dsData.Tables[0].Rows[i]["T12"].ToString());
                            }
                            catch
                            { }
                        }
                        for (int k = 0; k < arrList.ToArray().Length; k++)
                        {
                            int iStatus = 0;
                            double dT1 = 0;
                            double dT2 = 0;
                            double dT3 = 0;

                            double dT4 = 0;
                            double dT5 = 0;
                            double dT6 = 0;

                            double dT7 = 0;
                            double dT8 = 0;
                            double dT9 = 0;

                            double dT10 = 0;
                            double dT11 = 0;
                            double dT12 = 0;



                            for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    try
                                    {
                                        dT1 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T1"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT2 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T2"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT3 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T3"].ToString());
                                    }
                                    catch
                                    { }


                                    try
                                    {
                                        dT4 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T4"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT5 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T5"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT6 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T6"].ToString());
                                    }
                                    catch
                                    { }

                                    try
                                    {
                                        dT7 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T7"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT8 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T8"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT9 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T9"].ToString());
                                    }
                                    catch
                                    { }

                                    try
                                    {
                                        dT10 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T10"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT11 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T11"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dT12 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T12"].ToString());
                                    }
                                    catch
                                    { }

                                }
                            }
                            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[i]["MA_VUNG"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    iRow++;
                                    int iCurRow = iRow + k;
                                    iRowEnd = iRow + k;
                                    if (iStatus == 0)
                                    {
                                        //Group theo khach hang
                                        try
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["MA_VUNG"].ToString().ToUpper().Trim();
                                            objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol).Font.Bold = true;
                                        }
                                        catch
                                        { }
                                        try
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                        }
                                        catch
                                        { }
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dT1;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 2, iStartRow + iCurRow, iStartCol + 2).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dT2;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dT3;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;

                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dT4;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 5, iStartRow + iCurRow, iStartCol + 5).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dT5;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 6, iStartRow + iCurRow, iStartCol + 6).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = dT6;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 7, iStartRow + iCurRow, iStartCol + 7).Font.Bold = true;

                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = dT7;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 8, iStartRow + iCurRow, iStartCol + 8).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = dT8;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 9, iStartRow + iCurRow, iStartCol + 9).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = dT9;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 10, iStartRow + iCurRow, iStartCol + 10).Font.Bold = true;

                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = dT10;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 11, iStartRow + iCurRow, iStartCol + 11).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = dT11;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 12, iStartRow + iCurRow, iStartCol + 12).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = dT12;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 13, iStartRow + iCurRow, iStartCol + 13).Font.Bold = true;


                                        for (int j = iStartCol; j <= iStartCol + 14; j++)
                                        {
                                            objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);

                                        }

                                        //End
                                    }
                                    iStatus++;
                                    iCurRow = iCurRow + 1;

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["MA"];
                                    try
                                    {
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["Ten"].ToString().Trim();
                                    }
                                    catch
                                    { }

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["T1"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["T2"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["T3"];

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["T4"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["T5"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = dsData.Tables[0].Rows[i]["T6"];

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = dsData.Tables[0].Rows[i]["T7"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = dsData.Tables[0].Rows[i]["T8"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = dsData.Tables[0].Rows[i]["T9"];

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = dsData.Tables[0].Rows[i]["T10"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = dsData.Tables[0].Rows[i]["T11"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = dsData.Tables[0].Rows[i]["T12"];


                                }

                            }
                        }

                        //Tinh tong
                        iRowEnd += 2;
                        //objXLS.GetRow.(iRowEnd).MergeCells = true;

                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol).Value2 = "Tổng Cộng := ";
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol, iStartRow + iRowEnd, iStartCol).Font.Bold = true;

                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 2).Value2 = dSumT1;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 2, iStartRow + iRowEnd, iStartCol + 2).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 3).Value2 = dSumT2;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 3, iStartRow + iRowEnd, iStartCol + 3).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 4).Value2 = dSumT3;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 4, iStartRow + iRowEnd, iStartCol + 4).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 5).Value2 = dSumT4;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 5, iStartRow + iRowEnd, iStartCol + 5).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 6).Value2 = dSumT5;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 6, iStartRow + iRowEnd, iStartCol + 6).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 7).Value2 = dSumT6;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 7, iStartRow + iRowEnd, iStartCol + 7).Font.Bold = true;

                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 8).Value2 = dSumT7;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 8, iStartRow + iRowEnd, iStartCol + 8).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 9).Value2 = dSumT8;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 9, iStartRow + iRowEnd, iStartCol + 9).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 10).Value2 = dSumT9;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 10, iStartRow + iRowEnd, iStartCol + 10).Font.Bold = true;

                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 11).Value2 = dSumT10;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 11, iStartRow + iRowEnd, iStartCol + 11).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 12).Value2 = dSumT11;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 12, iStartRow + iRowEnd, iStartCol + 12).Font.Bold = true;
                        objXLS.GetCell(iStartRow + iRowEnd, iStartCol + 13).Value2 = dSumT12;
                        objXLS.GetRange(iStartRow + iRowEnd, iStartCol + 13, iStartRow + iRowEnd, iStartCol + 13).Font.Bold = true;
                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoanhThuVaCongDon(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, DateTime fDate, DateTime tDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                double doanhthutong = 0;

                if (arrList != null)
                {
                    if (arrList.ToArray().Length > 0)
                    {



                        objXLS.GetCell(7, iStartCol + 2).Value2 = "DS Tháng " + tDate.ToString("MM");
                        objXLS.GetCell(7, iStartCol + 5).Value2 = " Tổng DT " + tDate.ToString("MM") + " tháng ";

                        objXLS.GetCell(8, iStartCol + 2).Value2 = fDate.ToString("yyyy");
                        objXLS.GetCell(8, iStartCol + 3).Value2 = tDate.ToString("yyyy");
                        objXLS.GetCell(5, 1).Value2 = "BÁO CÁO DOANH THU BÁN HÀNG THEO KHÁCH HÀNG THÁNG " + fDate.ToString("MM") + " VÀ " + fDate.ToString("MM") + " THÁNG ";
                        //"BÁO CÁO DOANH THU BÁN HÀNG THEO KHÁCH HÀNG THÁNG " 11 VÀ 11 THÁNG
                        objXLS.GetCell(8, iStartCol + 5).Value2 = fDate.ToString("yyyy");
                        objXLS.GetCell(8, iStartCol + 6).Value2 = tDate.ToString("yyyy");
                        double dSumAllDOANHTHU_THANGPre = 0;
                        double dSumAllDOANHTHU_THANGNext = 0;
                        double dSumAllDOANHTHU_LUYKE_NAMPre = 0;
                        double dSumAllDOANHTHU_LUYKE_NAMNext = 0;
                        //dSumAllDOANHTHU_THANGNext = Convert.ToDouble(dsData.Tables[0].Compute("SUM(DOANHTHU_THANGNext)", string.Empty));
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {
                            try
                            {
                                dSumAllDOANHTHU_THANGPre += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_THANGPre"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumAllDOANHTHU_THANGNext += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_THANGNext"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumAllDOANHTHU_LUYKE_NAMPre += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMPre"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumAllDOANHTHU_LUYKE_NAMNext += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMNext"].ToString());
                            }
                            catch
                            { }
                        }
                        objXLS.GetCell(9, iStartCol + 2).Value2 = dSumAllDOANHTHU_THANGPre;
                        objXLS.GetRange(9, iStartCol + 2, 9, iStartCol + 2).Font.Bold = true;
                        objXLS.GetCell(9, iStartCol + 3).Value2 = dSumAllDOANHTHU_THANGNext;
                        objXLS.GetRange(9, iStartCol + 3, 9, iStartCol + 3).Font.Bold = true;

                        objXLS.GetCell(9, iStartCol + 4).Value2 = dSumAllDOANHTHU_THANGNext / dSumAllDOANHTHU_THANGPre;
                        objXLS.GetRange(9, iStartCol + 4, 9, iStartCol + 4).Font.Bold = true;
                        objXLS.GetCell(9, iStartCol + 5).Value2 = dSumAllDOANHTHU_LUYKE_NAMPre;
                        objXLS.GetRange(9, iStartCol + 5, 9, iStartCol + 5).Font.Bold = true;
                        objXLS.GetCell(9, iStartCol + 6).Value2 = dSumAllDOANHTHU_LUYKE_NAMNext;
                        objXLS.GetRange(9, iStartCol + 6, 9, iStartCol + 6).Font.Bold = true;
                        objXLS.GetCell(9, iStartCol + 7).Value2 = dSumAllDOANHTHU_LUYKE_NAMNext / dSumAllDOANHTHU_LUYKE_NAMPre;
                        objXLS.GetRange(9, iStartCol + 7, 9, iStartCol + 7).Font.Bold = true;
                        for (int k = 0; k < arrList.ToArray().Length; k++)
                        {
                            int iStatus = 0;
                            double dSumDOANHTHU_THANGPre = 0;
                            double dSumDOANHTHU_LUYKE_NAMPre = 0;
                            double dSumDOANHTHU_THANGNext = 0;
                            double dSumDOANHTHU_LUYKE_NAMNext = 0;

                            for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    try
                                    {
                                        dSumDOANHTHU_THANGPre += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU_THANGPre"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumDOANHTHU_LUYKE_NAMPre += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU_LUYKE_NAMPre"].ToString());
                                    }
                                    catch
                                    { }

                                    try
                                    {
                                        dSumDOANHTHU_THANGNext += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU_THANGNext"].ToString());
                                    }
                                    catch
                                    { }
                                    try
                                    {
                                        dSumDOANHTHU_LUYKE_NAMNext += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU_LUYKE_NAMNext"].ToString());
                                    }
                                    catch
                                    { }

                                }
                            }
                            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                            {   //strColExel1

                                if (dsData.Tables[0].Rows[i]["MA_VUNG"].ToString().Trim() == arrList[k].ToString().Trim())
                                {
                                    iRow++;
                                    int iCurRow = iRow + k;
                                    if (iStatus == 0)
                                    {
                                        //Group theo khach hang
                                        //objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iCurRow;
                                        try
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                            objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;

                                        }
                                        catch
                                        { }
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dSumDOANHTHU_THANGPre;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 2, iStartRow + iCurRow, iStartCol + 2).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dSumDOANHTHU_THANGNext;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;

                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dSumDOANHTHU_THANGNext / dSumDOANHTHU_THANGPre;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dSumDOANHTHU_LUYKE_NAMPre;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 5, iStartRow + iCurRow, iStartCol + 5).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dSumDOANHTHU_LUYKE_NAMNext;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 6, iStartRow + iCurRow, iStartCol + 6).Font.Bold = true;
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = dSumDOANHTHU_LUYKE_NAMNext / dSumDOANHTHU_LUYKE_NAMPre;
                                        objXLS.GetRange(iStartRow + iCurRow, iStartCol + 7, iStartRow + iCurRow, iStartCol + 7).Font.Bold = true;
                                        for (int j = iStartCol; j <= iStartCol + 7; j++)
                                        {
                                            objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                        }

                                        //End
                                    }
                                    iStatus++;
                                    iCurRow = iCurRow + 1;

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iRow;
                                    try
                                    {
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN"].ToString().Trim();
                                    }
                                    catch
                                    { }

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU_THANGPre"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU_THANGNext"];
                                    double dPhanTram = 0;
                                    try
                                    {
                                        dPhanTram = Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_THANGNext"].ToString()) / Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_THANGPre"].ToString());
                                    }
                                    catch
                                    { }
                                    if (dPhanTram != 0)
                                    {
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dPhanTram;
                                    }

                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMPre"];
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMNext"];
                                    dPhanTram = 0;
                                    try
                                    {
                                        dPhanTram = Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMNext"].ToString()) / Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU_LUYKE_NAMPre"].ToString());
                                    }
                                    catch
                                    { }
                                    if (dPhanTram != 0)
                                    {
                                        objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = dPhanTram;
                                    }

                                }

                            }
                        }
                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelGiaCong(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, string strYear)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                objXLS.GetCell(4, 3).Value2 = " TỔNG HỢP DOANH THU NĂM " + strYear;
                for (int k = 0; k < arrList.ToArray().Length - 1; k++)
                {
                    int iStatus = 0;
                    double T1 = 0;
                    double UT1 = 0;

                    double T2 = 0;
                    double UT2 = 0;

                    double T3 = 0;
                    double UT3 = 0;

                    double T4 = 0;
                    double UT4 = 0;

                    double T5 = 0;
                    double UT5 = 0;

                    double T6 = 0;
                    double UT6 = 0;

                    double T7 = 0;
                    double UT7 = 0;

                    double T8 = 0;
                    double UT8 = 0;

                    double T9 = 0;
                    double UT9 = 0;

                    double T10 = 0;
                    double UT10 = 0;

                    double T11 = 0;
                    double UT11 = 0;

                    double T12 = 0;
                    double UT12 = 0;



                    for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[q]["MA_KH"].ToString() == arrList[k].ToString())
                        {
                            try
                            {
                                T1 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T1"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT1 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT1"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T2 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T2"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT2 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT2"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T3 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T3"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT3 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT3"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T4 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T4"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT4 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT4"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T5 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T5"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT5 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT5"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T6 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T6"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT6 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT6"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T7 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT7 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T8 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT8 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T9 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T9"].ToString());
                            }
                            catch { }
                            try
                            {
                                UT9 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT9"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T10 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT10 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                T11 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T11"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT11 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT1"].ToString());
                            }
                            catch
                            { }
                            try
                            {

                                T12 += Convert.ToDouble(dsData.Tables[0].Rows[q]["T12"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                UT12 += Convert.ToDouble(dsData.Tables[0].Rows[q]["UT12"].ToString());
                            }
                            catch
                            { }
                        }
                    }
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[i]["MA_KH"].ToString() == arrList[k].ToString())
                        {
                            iRow++;
                            int iCurRow = iRow + k;
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TENKH"].ToString().ToUpper().Trim();
                                }
                                catch
                                { }
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = T1;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = UT1;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = T2;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 5, iStartRow + iCurRow, iStartCol + 5).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = UT2;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 6, iStartRow + iCurRow, iStartCol + 6).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = T3;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 7, iStartRow + iCurRow, iStartCol + 7).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = UT3;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 8, iStartRow + iCurRow, iStartCol + 8).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = T4;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 9, iStartRow + iCurRow, iStartCol + 9).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = UT4;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 10, iStartRow + iCurRow, iStartCol + 10).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = T5;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 11, iStartRow + iCurRow, iStartCol + 11).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = UT5;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 12, iStartRow + iCurRow, iStartCol + 12).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = T6;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 13, iStartRow + iCurRow, iStartCol + 13).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 14).Value2 = UT6;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 14, iStartRow + iCurRow, iStartCol + 14).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 17).Value2 = T7;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 17, iStartRow + iCurRow, iStartCol + 17).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 18).Value2 = UT7;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 18, iStartRow + iCurRow, iStartCol + 18).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 19).Value2 = T8;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 19, iStartRow + iCurRow, iStartCol + 19).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 20).Value2 = UT8;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 20, iStartRow + iCurRow, iStartCol + 20).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 21).Value2 = T9;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 21, iStartRow + iCurRow, iStartCol + 21).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 22).Value2 = UT9;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 22, iStartRow + iCurRow, iStartCol + 22).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 23).Value2 = T10;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 23, iStartRow + iCurRow, iStartCol + 23).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 24).Value2 = UT10;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 24, iStartRow + iCurRow, iStartCol + 24).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 25).Value2 = T11;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 25, iStartRow + iCurRow, iStartCol + 25).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 26).Value2 = UT11;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 26, iStartRow + iCurRow, iStartCol + 26).Font.Bold = true;

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 27).Value2 = T12;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 27, iStartRow + iCurRow, iStartCol + 27).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 28).Value2 = UT12;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 28, iStartRow + iCurRow, iStartCol + 28).Font.Bold = true;
                                for (int j = iStartCol; j <= iStartCol + 30; j++)
                                {
                                    objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                }

                                //End
                            }
                            iStatus++;
                            iCurRow = iCurRow + 1;
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iRow;
                            }
                            catch
                            { }
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TENSP"];

                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DONGIA_USD"];


                            //Begin 6 thang dau nam
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T1"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["UT1"];
                            }
                            catch
                            { }
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T2"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT2"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T3"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 8).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT3"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 9).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T4"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 10).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT4"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 11).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T5"].ToString());

                            }
                            catch
                            { }
                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 12).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT5"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 13).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T6"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 14).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT6"].ToString());
                            }
                            catch
                            { }

                            //End
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 15).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["SL_6TDAUNAM"].ToString());

                            }
                            catch
                            { }
                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 16).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["USD_6TDAUNAM"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 17).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T7"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 18).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT7"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 19).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T8"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 20).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT8"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 21).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T9"].ToString());

                            }
                            catch
                            { }
                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 22).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT9"].ToString());
                            }
                            catch
                            { }

                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 23).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T10"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 24).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT10"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 25).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T11"].ToString());

                            }
                            catch
                            { }
                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 26).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT11"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 27).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["T12"].ToString());

                            }
                            catch
                            { }

                            try
                            {

                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 28).Value2 = Convert.ToDouble(dsData.Tables[0].Rows[i]["UT12"].ToString());
                            }
                            catch
                            { }
                        }

                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoanhThuTheoCNAll(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, DateTime fdate, DateTime tDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                double doanhthutong = 0;
                objXLS.GetCell(4, 3).Value2 = fdate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetCell(5, 3).Value2 = tDate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetRange(4, 3, 4, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;
                objXLS.GetRange(5, 3, 5, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;


                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    doanhthutong += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU"].ToString());
                }
                objXLS.GetCell(8, 3).Value2 = "Tổng doanh thu:";
                objXLS.GetRange(8, 3, 8, 3).Font.Bold = true;
                objXLS.GetCell(8, 4).Value2 = doanhthutong;
                objXLS.GetRange(8, 4, 8, 4).Font.Bold = true;
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    double dSumDoanhThu = 0;
                    for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            try
                            {
                                dSumDoanhThu += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU"].ToString());
                            }
                            catch
                            { }
                        }
                    }
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[i]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            iRow++;
                            int iCurRow = iRow + k;
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dSumDoanhThu;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                for (int j = iStartCol; j <= iStartCol + 3; j++)
                                {
                                    objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                }

                                //End
                            }
                            iStatus++;
                            iCurRow = iCurRow + 1;
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iRow;
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN"].ToString().Trim();
                            }
                            catch
                            { }
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["Address"];

                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU"];


                        }

                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelDoanhThuVaCongNoAll(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, DateTime fdate, DateTime tDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            try
            {
                double doanhthutong = 0;
                double dCongNo = 0;
                objXLS.GetCell(4, 3).Value2 = fdate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetCell(5, 3).Value2 = tDate.ToString("dd/MM/yyyy") + " ";

                objXLS.GetRange(4, 3, 4, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;
                objXLS.GetRange(5, 3, 5, 3).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;


                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        doanhthutong += Convert.ToDouble(dsData.Tables[0].Rows[i]["DOANHTHU"].ToString());
                    }
                    catch
                    { }
                    try
                    {
                        dCongNo += Convert.ToDouble(dsData.Tables[0].Rows[i]["CONGNO"].ToString());
                    }
                    catch
                    { }
                }
                objXLS.GetCell(8, 3).Value2 = "Tổng doanh thu:";
                objXLS.GetRange(8, 3, 8, 3).Font.Bold = true;
                objXLS.GetCell(8, 4).Value2 = doanhthutong;
                objXLS.GetRange(8, 4, 8, 4).Font.Bold = true;
                objXLS.GetCell(8, 5).Value2 = dCongNo;
                objXLS.GetRange(8, 5, 8, 5).Font.Bold = true;
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    double dSumDoanhThu = 0;
                    double dSumCongNo = 0;
                    for (int q = 0; q < dsData.Tables[0].Rows.Count; q++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[q]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            try
                            {
                                dSumDoanhThu += Convert.ToDouble(dsData.Tables[0].Rows[q]["DOANHTHU"].ToString());
                            }
                            catch
                            { }
                            try
                            {
                                dSumCongNo += Convert.ToDouble(dsData.Tables[0].Rows[q]["CONGNO"].ToString());
                            }
                            catch
                            { }
                        }
                    }
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   //strColExel1

                        if (dsData.Tables[0].Rows[i]["MA_Vung"].ToString().Trim() == arrList[k].ToString().Trim())
                        {
                            iRow++;
                            int iCurRow = iRow + k;
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN_VUNG"].ToString().ToUpper().Trim();
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 1, iStartRow + iCurRow, iStartCol + 1).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dSumDoanhThu;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 3, iStartRow + iCurRow, iStartCol + 3).Font.Bold = true;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dSumCongNo;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol + 4, iStartRow + iCurRow, iStartCol + 4).Font.Bold = true;
                                for (int j = iStartCol; j <= iStartCol + 4; j++)
                                {
                                    objXLS.SetCellColor(iStartRow + iCurRow, j, InteropExcelColorEnum.LightGreen);
                                }

                                //End
                            }
                            iStatus++;
                            iCurRow = iCurRow + 1;
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iRow;
                            try
                            {
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["TEN"].ToString().Trim();
                            }
                            catch
                            { }
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["Address"];

                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["DOANHTHU"];
                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["CONGNO"];

                        }

                    }

                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelXuatNhapTon(DataSet dsData, string strFileExcel, int iStartRow, int iStartCol,DateTime dFromDate,DateTime dToDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                double dSumPSNO = 0;
                double dSumPSCO = 0;
                double dSumTonCuoi = 0;
                objXLS.GetCell(3, 5).Value2 = dFromDate.ToString("dd/MM/yyyy");
                objXLS.GetCell(4, 5).Value2 = dToDate.ToString("dd/MM/yyyy");
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {   //strColExel1
                    dSumTonCuoi = 0;
                    try
                    {
                        objXLS.GetCell(iStartRow + i, iStartCol).Value2 = i+1;
                    }
                    catch
                    { }
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["MA"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["TEN"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["DV"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["tondau"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["InQty"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["OutQty"];
                    if (dsData.Tables[0].Rows[i]["tondau"] != DBNull.Value)
                    {
                        dSumTonCuoi += Convert.ToDouble(dsData.Tables[0].Rows[i]["tondau"].ToString());
                    }
                    if (dsData.Tables[0].Rows[i]["InQty"] != DBNull.Value)
                    {
                        dSumTonCuoi += Convert.ToDouble(dsData.Tables[0].Rows[i]["InQty"].ToString());
                    }
                    if (dsData.Tables[0].Rows[i]["OutQty"] != DBNull.Value)
                    {
                        dSumTonCuoi -= Convert.ToDouble(dsData.Tables[0].Rows[i]["OutQty"].ToString());
                    }
                    objXLS.GetCell(iStartRow + i, iStartCol + 7).Value2 = dSumTonCuoi;
                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportBaoCaoXuatNhapTon(DataSet dsData,DataSet dsetCD, string strFileExcel, int iStartRow, int iStartCol, DateTime dFromDate, DateTime dToDate)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                double dSumInput = 0;
                double dSumOutput = 0;

                double dSumInputCD = 0;
                double dSumOutputCD = 0;
                int i = 0;
                objXLS.GetCell(5,1).Value2 = " Từ ngày: "+ dFromDate.ToString("dd") + " - " + dToDate.ToString("dd/MM/yyyy");// 1- 31/12/2009
                foreach(DataRow dr in dsData.Tables[0].Rows)
                {
                    dSumInput = 0;
                    dSumOutput = 0;
                    dSumInputCD = 0;
                    dSumOutputCD = 0;
                    DataRow[] result = dsetCD.Tables[0].Select(" MA='" + dr["MA"] + "'");
                    //Tinh tong nhap XH
                    if (dr["I1"] != DBNull.Value)
                    {
                        dSumInput += Convert.ToDouble(dr["I1"].ToString());
                    }
                    if (dr["I2"] != DBNull.Value)
                    {
                        dSumInput += Convert.ToDouble(dr["I2"].ToString());
                    }
                    if (dr["I3"] != DBNull.Value)
                    {
                        dSumInput += Convert.ToDouble(dr["I3"].ToString());
                    }
                    if (dr["I4"] != DBNull.Value)
                    {
                        dSumInput += Convert.ToDouble(dr["I4"].ToString());
                    }

                    //Tinh tong nhap CD
                    if (result.Length >0)
                    {
                        if (result[0]["I1"] != DBNull.Value)
                        {
                            dSumInputCD += Convert.ToDouble(result[0]["I1"].ToString());
                        }
                        if (dr["I2"] != DBNull.Value)
                        {
                            dSumInputCD += Convert.ToDouble(result[0]["I2"].ToString());
                        }
                        if (dr["I3"] != DBNull.Value)
                        {
                            dSumInputCD += Convert.ToDouble(result[0]["I3"].ToString());
                        }
                        if (dr["I4"] != DBNull.Value)
                        {
                            dSumInputCD += Convert.ToDouble(result[0]["I4"].ToString());
                        }
                    }
                    //Tinh tong xuat XH
                    if (dr["X1"] != DBNull.Value)
                    {
                        dSumOutput += Convert.ToDouble(dr["X1"].ToString());
                    }
                    if (dr["X2"] != DBNull.Value)
                    {
                        dSumOutput += Convert.ToDouble(dr["X2"].ToString());
                    }
                    if (dr["X3"] != DBNull.Value)
                    {
                        dSumOutput += Convert.ToDouble(dr["X3"].ToString());
                    }
                    if (dr["X4"] != DBNull.Value)
                    {
                        dSumOutput += Convert.ToDouble(dr["X4"].ToString());
                    }
                    //Tinh tong xuat CD
                    if (result.Length > 0)
                    {
                        if (result[0]["X1"] != DBNull.Value)
                        {
                            dSumOutputCD += Convert.ToDouble(result[0]["X1"].ToString());
                        }
                        if (result[0]["X2"] != DBNull.Value)
                        {
                            dSumOutputCD += Convert.ToDouble(result[0]["X2"].ToString());
                        }
                        if (result[0]["X3"] != DBNull.Value)
                        {
                            dSumOutputCD += Convert.ToDouble(result[0]["X3"].ToString());
                        }
                        if (result[0]["X4"] != DBNull.Value)
                        {
                            dSumOutputCD += Convert.ToDouble(result[0]["X4"].ToString());
                        }
                    }

                    objXLS.GetCell(iStartRow + i, iStartCol).Value2 = dr["MA"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 1).Value2 = dr["TEN"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 2).Value2 = dr["GIA_BAN"];
                    objXLS.GetCell(iStartRow + i, iStartCol + 3).Value2 = dr["GIA_KH"];

                    objXLS.GetCell(iStartRow + i, iStartCol + 4).Value2 = dr["Tondau"];
                    if (result.Length >0)
                    {
                        objXLS.GetCell(iStartRow + i, iStartCol + 5).Value2 = result[0]["Tondau"];
                    }
                    objXLS.GetCell(iStartRow + i, iStartCol + 8).Value2 = dSumInput;
                    objXLS.GetCell(iStartRow + i, iStartCol + 9).Value2 = dSumInputCD;

                    objXLS.GetCell(iStartRow + i, iStartCol + 11).Value2 = dSumOutput;
                    objXLS.GetCell(iStartRow + i, iStartCol + 12).Value2 = dSumOutputCD;
                    i++;
                }


            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelBaoGiaDichVu(DataSet dsData,DataSet dsetMaster, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList,double dVAT)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            int iRowSum = 0;
            int iIndex = 0;
            double dSumAllAmount = 0;
            try
            {
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    iIndex=0;
                    double dSumAllAmountByGroup = 0;
                    dSumAllAmount = 0;
                    objXLS.GetCell(11, 1).Value2 ="Tên công ty : " +dsetMaster.Tables[0].Rows[0]["CtyName"].ToString();
                    objXLS.GetCell(12, 1).Value2 ="Đại diện : " + dsetMaster.Tables[0].Rows[0]["LH"].ToString();
                    objXLS.GetCell(13, 1).Value2 ="Địa chỉ : "+ dsetMaster.Tables[0].Rows[0]["DC"].ToString();
                    objXLS.GetCell(14, 1).Value2 ="Điện thoại : "+ dsetMaster.Tables[0].Rows[0]["DT"].ToString();
                    objXLS.GetCell(15, 1).Value2 ="MST : "+ dsetMaster.Tables[0].Rows[0]["MST"].ToString();

                    objXLS.GetCell(11, 4).Value2 = "Kính gửi : " + dsetMaster.Tables[0].Rows[0]["KH"].ToString();
                    objXLS.GetCell(12, 4).Value2 = "Loại xe : " + dsetMaster.Tables[0].Rows[0]["LX"].ToString();
                    objXLS.GetCell(13, 4).Value2 = "Biển số : " + dsetMaster.Tables[0].Rows[0]["BS"].ToString();
                    objXLS.GetCell(14, 4).Value2 = "Màu xe : " + dsetMaster.Tables[0].Rows[0]["MX"].ToString();
                    objXLS.GetCell(15, 4).Value2 = "Số khung : " + dsetMaster.Tables[0].Rows[0]["SK"].ToString();
                    #region sum all Amount by Group Categoryid
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        if (dsData.Tables[0].Rows[i]["CategoryID"].ToString() == arrList[k].ToString())
                        {
                            if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                            {
                                try
                                {
                                    dSumAllAmountByGroup += Convert.ToDouble(dsData.Tables[0].Rows[i]["Amount"].ToString());
                                }
                                catch
                                { }
                            }
                        }
                        #region Sum All Amount
                        if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                        {
                            try
                            {
                                dSumAllAmount += Convert.ToDouble(dsData.Tables[0].Rows[i]["Amount"].ToString());
                            }
                            catch
                            { }
                        }
                        #endregion
                    }
                    #endregion
                    #region View Item
                    if (k >= 1) iRow = iRow + 1;
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {   
                        if (dsData.Tables[0].Rows[i]["CategoryID"].ToString() == arrList[k].ToString())
                        {
                            iRow++;
                            iIndex++;
                            int iCurRow =0;
                            iCurRow = iRow + k;
                            
                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["CategoryName"].ToString().ToUpper().Trim();
                                }
                                catch
                                { }
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol).Font.Bold = true;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol+6).MergeCells=true;
                                //End
                            }
                            if (k < arrList.ToArray().Length )
                            {
                                iStatus++;
                                iCurRow = iCurRow + 1;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iIndex;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol +1).Value2 = dsData.Tables[0].Rows[i]["ProductCode"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DV"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["SL"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["Price"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["Amount"];
                                iRowSum = iCurRow;
                            }
                        }

                    }
                    #endregion
                    
                    objXLS.GetCell(iStartRow + iRowSum +1, iStartCol + 1).Value2 = "Tổng cộng";
                    objXLS.GetRange(iStartRow + iRowSum +1, iStartCol, iStartRow + iRowSum+1 , iStartCol + 6).Font.Bold = true;
                    objXLS.GetCell(iStartRow + iRowSum +1, iStartCol + 6).Value2 = dSumAllAmountByGroup;
                    //iRow = iRow + 1;
                }
                #region 
                
                objXLS.GetCell(iStartRow + iRowSum + 2, iStartCol + 5).Value2 = "Tổng các " + " phần ";
                objXLS.GetRange(iStartRow + iRowSum + 2, iStartCol, iStartRow + iRowSum + 2, iStartCol + 6).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 2, iStartCol + 6).Value2 = dSumAllAmount;

                objXLS.GetCell(iStartRow + iRowSum + 3, iStartCol + 5).Value2 = "Thuế VAT(%)";
                objXLS.GetRange(iStartRow + iRowSum + 3, iStartCol, iStartRow + iRowSum + 3, iStartCol + 6).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 3, iStartCol + 6).Value2 = dVAT;

                objXLS.GetCell(iStartRow + iRowSum + 4, iStartCol + 5).Value2 = "Tổng cộng ";
                objXLS.GetRange(iStartRow + iRowSum + 4, iStartCol, iStartRow + iRowSum + 4, iStartCol + 6).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 4, iStartCol + 6).Value2 = dSumAllAmount+ dVAT;
                string strChu = "";
                try
                {
                    decimal dAmount =Convert.ToDecimal((dSumAllAmount + dVAT).ToString());
                    strChu = PCSUtils.Utils.ConvertNumberToWord.ChuyenSoThanhChu(dAmount);
                }
                catch
                { }
                objXLS.GetCell(iStartRow + iRowSum + 5, iStartCol + 6).Value2 ="Bằng chữ : " + strChu;
                objXLS.GetRange(iStartRow + iRowSum + 5, iStartCol + 6, iStartRow + iRowSum + 5, iStartCol + 6).Font.Bold = true;
                objXLS.GetRange(iStartRow + iRowSum + 5, iStartCol + 1, iStartRow + iRowSum + 5, iStartCol + 6).MergeCells = true;

                #endregion
            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelBaoGiaDichVuKLe(DataSet dsData, DataSet dsetMaster, string strFileExcel, int iStartRow, int iStartCol, ArrayList arrList, double dVAT)
        {

            #region Report layout

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;
            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            int iRowSum = 0;
            int iIndex = 0;
            double dSumAllAmount = 0;
            double dSumAllVAT = 0;
            int idemVAT = 0;
            try
            {
                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                {
                    if (dsData.Tables[0].Rows[i]["VAT"] != DBNull.Value)
                    {
                        dSumAllVAT += Convert.ToDouble(dsData.Tables[0].Rows[i]["VAT"]);
                        idemVAT++;
                    }
                }
                dSumAllVAT = dSumAllVAT / idemVAT;
                string strNgay = "Ngày: ";
                try
                {
                    strNgay += Convert.ToDateTime(dsetMaster.Tables[0].Rows[0]["NgayGiaoDich"].ToString()).ToString("dd/MM/yyyy");
                }
                catch
                { }
                objXLS.GetCell(9, 5).Value2 = strNgay; 
                objXLS.GetCell(10, 5).Value2 = "Số thẻ:  " + dsetMaster.Tables[0].Rows[0]["Maso"].ToString();
                for (int k = 0; k < arrList.ToArray().Length; k++)
                {
                    int iStatus = 0;
                    iIndex = 0;
                    double dSumAllAmountByGroup = 0;
                    dSumAllAmount = 0;
                    objXLS.GetCell(13, 1).Value2 = "Tên khách hàng (Công ty): " + dsetMaster.Tables[0].Rows[0]["CtyName"].ToString();
                    objXLS.GetCell(14, 1).Value2 = "MST:  " + dsetMaster.Tables[0].Rows[0]["MST"].ToString();
                    objXLS.GetCell(15, 1).Value2 = "Địa chỉ: " + dsetMaster.Tables[0].Rows[0]["DC"].ToString();
                    objXLS.GetCell(16, 1).Value2 = "Điện thoại: " + dsetMaster.Tables[0].Rows[0]["DT"].ToString();
                    objXLS.GetCell(16, 3).Value2 = "Fax: " + dsetMaster.Tables[0].Rows[0]["Fax"].ToString();
                    objXLS.GetCell(17, 1).Value2 = "Lãi xe: " + dsetMaster.Tables[0].Rows[0]["NguoiLXe"].ToString();
                    objXLS.GetCell(17, 3).Value2 = "Điện thoại: " + dsetMaster.Tables[0].Rows[0]["DTLaiXe"].ToString();

                    //objXLS.GetCell(13, 4).Value2 = "Số đăng ký: " + dsetMaster.Tables[0].Rows[0]["DTLaiXe"].ToString();
                    objXLS.GetCell(14, 4).Value2 = "Model: " + dsetMaster.Tables[0].Rows[0]["Model"].ToString();
                    objXLS.GetCell(15, 4).Value2 = "Ngày vào: " + dsetMaster.Tables[0].Rows[0]["NgayVao"].ToString();
                    objXLS.GetCell(16, 4).Value2 = "Ngày ra: " + dsetMaster.Tables[0].Rows[0]["NgayRa"].ToString();
                    objXLS.GetCell(17, 4).Value2 = "Ngày hẹn giao xe: " + dsetMaster.Tables[0].Rows[0]["NgayHen"].ToString();

                    objXLS.GetCell(13, 5).Value2 = "Loại xe: " + dsetMaster.Tables[0].Rows[0]["LX"].ToString();
                    objXLS.GetCell(14, 5).Value2 = "Đời xe: " + dsetMaster.Tables[0].Rows[0]["DoiXe"].ToString();
                    objXLS.GetCell(15, 5).Value2 = "Giờ vào: " + dsetMaster.Tables[0].Rows[0]["GioVao"].ToString();
                    objXLS.GetCell(16, 5).Value2 = "Giờ ra: " + dsetMaster.Tables[0].Rows[0]["GioRa"].ToString();
                    objXLS.GetCell(17, 5).Value2 = "Giờ hẹn: " + dsetMaster.Tables[0].Rows[0]["GioHen"].ToString();



                    #region sum all Amount by Group Categoryid
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        if (dsData.Tables[0].Rows[i]["CategoryID"].ToString() == arrList[k].ToString())
                        {
                            if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                            {
                                try
                                {
                                    dSumAllAmountByGroup += Convert.ToDouble(dsData.Tables[0].Rows[i]["Amount"].ToString());
                                }
                                catch
                                { }
                            }
                        }
                        #region Sum All Amount
                        if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                        {
                            try
                            {
                                if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                                {
                                    dSumAllAmount += Convert.ToDouble(dsData.Tables[0].Rows[i]["Amount"].ToString());
                                }
                            }
                            catch
                            { }
                            if (dsData.Tables[0].Rows[i]["CongThayThe"] != DBNull.Value)
                            {
                                dSumAllAmount +=  Convert.ToDouble(dsData.Tables[0].Rows[i]["CongThayThe"]);
                            }
                        }
                        #endregion
                    }
                    #endregion
                    #region View Item
                    if (k >= 1) iRow = iRow + 1;
                    for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                    {
                        if (dsData.Tables[0].Rows[i]["CategoryID"].ToString() == arrList[k].ToString())
                        {
                            iRow++;
                            iIndex++;
                            int iCurRow = 0;
                            iCurRow = iRow + k;

                            if (iStatus == 0)
                            {
                                //Group theo khach hang
                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = dsData.Tables[0].Rows[i]["CategoryName"].ToString().ToUpper().Trim();
                                }
                                catch
                                { }
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol).Font.Bold = true;
                                objXLS.GetRange(iStartRow + iCurRow, iStartCol, iStartRow + iCurRow, iStartCol + 6).MergeCells = true;
                                //End
                            }
                            if (k < arrList.ToArray().Length)
                            {
                                iStatus++;
                                iCurRow = iCurRow + 1;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iIndex;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["ProductCode"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["DV"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = dsData.Tables[0].Rows[i]["SL"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4).Value2 = dsData.Tables[0].Rows[i]["Price"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 5).Value2 = dsData.Tables[0].Rows[i]["Amount"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 6).Value2 = dsData.Tables[0].Rows[i]["CongThayThe"];
                                double dSumAmount = 0;
                                if (dsData.Tables[0].Rows[i]["Amount"] != DBNull.Value)
                                {
                                    dSumAmount = Convert.ToDouble(dsData.Tables[0].Rows[i]["Amount"]);
                                }
                                if (dsData.Tables[0].Rows[i]["CongThayThe"] != DBNull.Value)
                                {
                                    dSumAmount += Convert.ToDouble(dsData.Tables[0].Rows[i]["CongThayThe"]);
                                }
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 7).Value2 = dSumAmount;
                                //CongThayThe
                                iRowSum = iCurRow;
                            }
                        }

                    }
                    #endregion

                    objXLS.GetCell(iStartRow + iRowSum + 1, iStartCol + 1).Value2 = "Tổng cộng";
                    objXLS.GetRange(iStartRow + iRowSum + 1, iStartCol, iStartRow + iRowSum + 1, iStartCol + 6).Font.Bold = true;
                    objXLS.GetCell(iStartRow + iRowSum + 1, iStartCol + 5).Value2 = dSumAllAmountByGroup;
                    //iRow = iRow + 1;
                }
                #region

                objXLS.GetCell(iStartRow + iRowSum + 2, iStartCol + 6).Value2 = "Tổng các " + " phần ";
                objXLS.GetRange(iStartRow + iRowSum + 2, iStartCol, iStartRow + iRowSum + 2, iStartCol + 7).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 2, iStartCol + 7).Value2 = dSumAllAmount;
                
                objXLS.GetRange(iStartRow + iRowSum + 2, iStartCol + 5, iStartRow + iRowSum + 2, iStartCol + 5).VerticalAlignment =true;

                objXLS.GetCell(iStartRow + iRowSum + 3, iStartCol + 6).Value2 = "Thuế VAT(%)";
                objXLS.GetRange(iStartRow + iRowSum + 3, iStartCol, iStartRow + iRowSum + 3, iStartCol + 7).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 3, iStartCol + 7).Value2 = dSumAllAmount/dSumAllVAT;
                objXLS.GetRange(iStartRow + iRowSum + 3, iStartCol + 5, iStartRow + iRowSum + 3, iStartCol + 5).VerticalAlignment = true;

                objXLS.GetCell(iStartRow + iRowSum + 4, iStartCol + 6).Value2 = "Tổng cộng ";
                objXLS.GetRange(iStartRow + iRowSum + 4, iStartCol, iStartRow + iRowSum + 4, iStartCol + 7).Font.Bold = true;
                objXLS.GetCell(iStartRow + iRowSum + 4, iStartCol + 7).Value2 = dSumAllAmount + dSumAllAmount / dSumAllVAT;
                objXLS.GetRange(iStartRow + iRowSum + 4, iStartCol + 5, iStartRow + iRowSum + 4, iStartCol + 5).VerticalAlignment = true;
                string strChu = "";
                try
                {
                    decimal dAmount = Convert.ToDecimal((dSumAllAmount + dSumAllAmount / dSumAllVAT).ToString());
                    strChu = PCSUtils.Utils.ConvertNumberToWord.ChuyenSoThanhChu(dAmount);
                }
                catch
                { }
                objXLS.GetCell(iStartRow + iRowSum + 5, iStartCol + 7).Value2 = "Bằng chữ : " + strChu;
                objXLS.GetRange(iStartRow + iRowSum + 5, iStartCol + 7, iStartRow + iRowSum + 5, iStartCol + 7).Font.Bold = true;
                objXLS.GetRange(iStartRow + iRowSum + 5, iStartCol + 1, iStartRow + iRowSum + 5, iStartCol + 7).MergeCells = true;

                objXLS.GetCell(iStartRow + iRowSum + 7, iStartCol + 1).Value2 = "Ký xá nhận của cố vẫn dịch vụ " ;
                objXLS.GetCell(iStartRow + iRowSum + 7, iStartCol + 2).Value2 = "Ký xá nhận của khách hàng ";
                objXLS.GetCell(iStartRow + iRowSum + 7, iStartCol + 5).Value2 = "Ký xá nhận của kế toán ";
                objXLS.GetRange(iStartRow + iRowSum + 7, iStartCol + 1, iStartRow + iRowSum + 7, iStartCol + 7).Font.Bold = true;
               
                objXLS.GetRange(iStartRow + iRowSum + 7, iStartCol + 1, iStartRow + iRowSum + 7, iStartCol + 1).MergeCells = true;
                objXLS.GetRange(iStartRow + iRowSum + 7, iStartCol + 2, iStartRow + iRowSum + 7, iStartCol + 4).MergeCells = true;
                objXLS.GetRange(iStartRow + iRowSum + 7, iStartCol + 5, iStartRow + iRowSum + 7, iStartCol + 7).MergeCells = true;
                #endregion
            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelKeHoachSanXuat(DataSet dsData, string strFileExcel, ArrayList arrList,ArrayList arrListDate, int iStartRow, int iStartCol)
        {

           

            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            //FormControlComponents.NowToUTCString() 
            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + DateTime.Today.ToString("dd/MM/yyyy").Replace("/", "") + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            /// 
            try
            {
                File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            }
            catch
            {
                MessageBox.Show("File đang mở xin vui lòng đóng lại,rồi in lại báo cáo");
                return;
            }
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);
            int iRow = 0;
            int iIndex = 0;
            int iCol = 0;
            try
            {
                double doanhthutong = 0;
                double dCongNo = 0;
                objXLS.GetCell(iStartRow, iStartCol).Value2 = "No";
                objXLS.GetCell(iStartRow, iStartCol + 1).Value2 = "NUMBER";
                objXLS.GetCell(iStartRow, iStartCol + 2).Value2 = "MANE";
                objXLS.GetCell(iStartRow, iStartCol + 3).Value2 = "DATE";

                if (arrListDate.ToArray().Length > 0)
                {
                    int idem = 1;
                    foreach (string strDate in arrListDate)
                    {
                        objXLS.GetCell(iStartRow, iStartCol + 3 + idem).Value2 = strDate;
                        idem++;
                    }
                }
                iStartRow = iStartRow + 1;
                //iRow = iStartRow;
                #region View infor product
                if (arrList.ToArray().Length > 0)
                {
                    foreach (string iProductID in arrList)
                    {
                        int iRowGroup = 0;
                        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                        {  

                            if (dsData.Tables[0].Rows[i]["ProductID"].ToString().Trim() == iProductID.ToString().Trim())
                            {
                                //iRow++;
                                iIndex++;
                                iRowGroup++;
                                int iCurRow = iRow;
                                iCurRow = iCurRow;
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol).Value2 = iIndex;

                                try
                                {
                                    objXLS.GetCell(iStartRow + iCurRow, iStartCol + 1).Value2 = dsData.Tables[0].Rows[i]["ProductCode"].ToString().Trim();
                                }
                                catch
                                { }
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 2).Value2 = dsData.Tables[0].Rows[i]["ProductName"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = "SYSTEM PLAN";
                                //objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["DemandQuantity"];
                                iCurRow++;
                                iRow++;
                                //objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["Quantity"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = "ACTUAL PLAN";
                                iCurRow++;
                                iRow++;
                                //objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["NetAvailableQuantity"];
                                objXLS.GetCell(iStartRow + iCurRow, iStartCol + 3).Value2 = "STOCK";
                                iCurRow++;
                                iRow++;
                            }
                        }
                    }
                }
               
                #endregion
                iRow = 0;
                #region View so luong theo ngay
                if (arrListDate.ToArray().Length > 0)
                {
                    iCol = 0;
                    foreach (string strDate in arrListDate)
                    {
                        iRow = 0;
                        if (arrList.ToArray().Length > 0)
                        {

                            foreach (string iProductID in arrList)
                            {
                                for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
                                {   //strColExel1

                                    if (dsData.Tables[0].Rows[i]["ProductID"].ToString().Trim() == iProductID.ToString().Trim())
                                    {
                                        //iRow++;
                                        int iCurRow = iRow;
                                        if (Convert.ToDateTime(dsData.Tables[0].Rows[i]["DueDate"]).ToString("dd/MM/yyyy") == strDate)
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["DemandQuantity"];
                                            iCurRow++;
                                            iRow++;
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["Quantity"];

                                            iCurRow++;
                                            iRow++;
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = dsData.Tables[0].Rows[i]["NetAvailableQuantity"];
                                            iCurRow++;
                                            iRow++;
                                        }
                                        else
                                        {
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 =null;
                                            iCurRow++;
                                            iRow++;
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = null;

                                            iCurRow++;
                                            iRow++;
                                            objXLS.GetCell(iStartRow + iCurRow, iStartCol + 4 + iCol).Value2 = null;
                                            iCurRow++;
                                            iRow++;
                                        }
                                    }
                                }
                            }
                            //Next day
                            iCol++;
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

        }

        public void ExportExcelTheoDoiGiaVT(DataSet dsData, string pLayOutFile, string strFileExcel, int iStartRow, int iStartCol, string strChartName, ArrayList arrList,string strYear)
        {

            #region Report layoutint
            C1Report rptReport = new C1Report();
            mLayoutFile = pLayOutFile;

            string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mstrReportDefFolder + "\\" + mLayoutFile);
            rptReport.Load(mstrReportDefFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);

            rptReport.Layout.PaperSize = PaperKind.A4;
            //Field fldChart = rptReport.Fields["fldChart"];
            string EXCEL_FILE = strFileExcel;

            string strTemplateFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

            string strDestinationFilePath = mstrReportDefFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + FormControlComponents.NowToUTCString() + ".XLS";

            /// Copy layout excel report file to ExcelReport folder with a UTC datetime name
            File.Copy(strTemplateFilePath, strDestinationFilePath, true);
            ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

            try
            {
                string strCol = "A";
                objXLS.GetCell(2, iStartCol).Value2 = "Năm :" + strYear;
                int iColStart = 65;
                #region View Month
                for (int i = 0; i < 12; i++)
                {
                    //Hien thi thang
                    objXLS.GetCell(iStartRow+i, iStartCol).Value2 =i+1;
                }
                #endregion
                #region View Product
                if (arrList.ToArray().Length > 0)
                {
                    int iCol=1;
                    string strLast = "";
                    strLast = strCol.Substring(strCol.Length - 1, 0);
                    foreach (string strMATB in arrList)
                    {
                        if (strLast == "Z")
                        {
                            iColStart = 65;
                           // char str1 =Convert.ToChar(strCol.Substring(0, strCol.Length - 1));
                           // strCol = str1.ToString() + Convert.ToChar(iColStart);

                        }
                        //iColStart++;
                        foreach (DataRow dr in dsData.Tables[0].Rows)
                        {
                            if (dr["MATB"] != DBNull.Value)
                            {
                                if (strMATB == dr["MATB"].ToString())
                                {
                                    objXLS.GetCell(iStartRow-1, iStartCol + iCol).Value2 = dr["TENTB"].ToString();
                                }
                            }
                        }
                       
                       
                        iCol++;
                        iColStart = iColStart + 1;
                        if (strCol.Length == 2)
                        {
                            strCol = strCol.Substring(0, 1) + Convert.ToChar(iColStart);
                        }
                        else
                        {
                            strCol = Convert.ToChar(iColStart).ToString();
                        }
                    }
                }
                #endregion

                #region View Product
                if (arrList.ToArray().Length > 0)
                {
                    int iCol = 1;
                    int iRow = iStartRow;
                    foreach (string strMATB in arrList)
                    {
                        foreach (DataRow dr in dsData.Tables[0].Rows)
                        {
                            if (dr["MATB"] != DBNull.Value)
                            {
                                if (strMATB == dr["MATB"].ToString())
                                {
                                    if (dr["Price"] != DBNull.Value)
                                    {
                                        objXLS.GetCell(iRow - 1 + Convert.ToInt32(dr["Months"].ToString()), iStartCol + iCol).Value2 = Convert.ToDecimal(dr["Price"].ToString());
                                    }
                                }
                            }
                        }
                        iCol++;
                    }
                }
                #endregion
                
                 ChartObject chart = objXLS.GetChart(strChartName);
                //Begin
                  Chart xlChart = chart.Chart;
                  string strColStarts = "A" + iStartRow;
                  int iRowEnd = iStartRow + 11;
                  string strColEnd = strCol + iRowEnd;
                  Range chartRange = objXLS.GetRange(strColStarts, strColEnd);
                  xlChart.SetSourceData(chartRange, Type.Missing);
                    
                // xlChart.ChartType = XlChartType.xl3DColumn;
                //End

            #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                #region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

                objXLS.CloseWorkbook();
                objXLS.Dispose();
                objXLS = null;
                Process.Start(strDestinationFilePath);
                #endregion
            }

           
        }
    }
}
