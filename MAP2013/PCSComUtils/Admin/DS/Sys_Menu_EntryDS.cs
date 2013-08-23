using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

using PCSComUtils.PCSExc;
using System.Collections.Generic;

namespace PCSComUtils.Admin.DS
{
    public class Sys_Menu_EntryDS
    {
        private const string THIS = "PCSComUtils.Admin.DS.DS.Sys_Menu_EntryDS";

        #region IObjectDS Members

        public void Add(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".Add()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var objObject = (Sys_Menu_EntryVO)pobjObjectVO;
                string strSql = String.Empty;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO Sys_Menu_Entry("
                         + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                         + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                         + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                         + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                         + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                         + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                         + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                         + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                         + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                         + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                         + Sys_Menu_EntryTable.DESCRIPTION_FLD + ","
                         + Sys_Menu_EntryTable.TYPE_FLD + ")"
                         + "VALUES(?,?,?,?,?,?,?,?,?,?,?)";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.SHORTCUT_FLD].Value = objObject.Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].Value = objObject.Parent_Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.BUTTON_CAPTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].Value = objObject.Button_Caption;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].Value = objObject.Text_CaptionDefault;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].Value = objObject.Text_Caption_Vi_VN;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].Value = objObject.Text_Caption_EN_US;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].Value = objObject.Text_Caption_JA_JP;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].Value =
                    objObject.Text_Caption_Language_Default;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_CHILD_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_CHILD_FLD].Value = objObject.Parent_Child;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.FORMLOAD_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.FORMLOAD_FLD].Value = objObject.FormLoad;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.DESCRIPTION_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.FORMLOAD_FLD].Value = objObject.FormLoad;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TYPE_FLD].Value = objObject.Type;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void Delete(int pintID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            string strSql = "DELETE " + Sys_Menu_EntryTable.TABLE_NAME + " WHERE  " + "Menu_EntryID" + "=" + pintID;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }


            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public object GetObjectVO(int pintID)
        {
            const string METHOD_NAME = THIS + ".GetObjectVO()";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + "=" + pintID;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                var objObject = new Sys_Menu_EntryVO();

                while (odrPCS.Read())
                {
                    objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    objObject.Button_Caption =
                        int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    objObject.Text_CaptionDefault =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_Vi_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    try
                    {
                        objObject.CollapsedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    try
                    {
                        objObject.ExpandedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    objObject.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString().Trim();
                    objObject.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString().Trim();
                    try
                    {
                        objObject.IsTransaction =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.ISTRANSACTION_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    try
                    {
                        objObject.IsUserCreated =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.ISUSERCREATED_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    objObject.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString().Trim();
                    objObject.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString().Trim();
                    objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                }
                return objObject;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void Update(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".Update()";

            var objObject = (Sys_Menu_EntryVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);
                string strSql = "UPDATE Sys_Menu_Entry SET "
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + "=  ?"
                                + " WHERE " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.SHORTCUT_FLD].Value = objObject.Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].Value = objObject.Parent_Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.BUTTON_CAPTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].Value = objObject.Button_Caption;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].Value = objObject.Text_CaptionDefault;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].Value = objObject.Text_Caption_Vi_VN;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].Value = objObject.Text_Caption_EN_US;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].Value = objObject.Text_Caption_JA_JP;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].Value =
                    objObject.Text_Caption_Language_Default;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_CHILD_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_CHILD_FLD].Value = objObject.Parent_Child;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].Value = objObject.CollapsedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].Value = objObject.ExpandedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PREFIX_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PREFIX_FLD].Value = objObject.Prefix;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSFORMAT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSFORMAT_FLD].Value = objObject.TransFormat;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISTRANSACTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISTRANSACTION_FLD].Value = objObject.IsTransaction;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISUSERCREATED_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISUSERCREATED_FLD].Value = objObject.IsUserCreated;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TABLENAME_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TABLENAME_FLD].Value = objObject.TableName;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].Value = objObject.TransNoFieldName;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TYPE_FLD].Value = objObject.Type;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.MENU_ENTRYID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].Value = objObject.Menu_EntryID;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                    {
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    }
                    else
                    {
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataSet List()
        {
            const string METHOD_NAME = THIS + ".List()";
            var dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, Sys_Menu_EntryTable.TABLE_NAME);

                return dstPCS;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void UpdateDataSet(DataSet pData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            var odadPCS = new OleDbDataAdapter();

            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + "  FROM " + Sys_Menu_EntryTable.TABLE_NAME;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pData.EnforceConstraints = false;
                odadPCS.Update(pData, Sys_Menu_EntryTable.TABLE_NAME);
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                }
                else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                }

                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        #endregion

        public int AddAndReturnID(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".AddAndReturnID()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var objObject = (Sys_Menu_EntryVO)pobjObjectVO;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                string strSql = "INSERT INTO Sys_Menu_Entry("
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.DESCRIPTION_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ")"
                                + "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                strSql += " ; SELECT @@IDENTITY AS NEWID";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].Value = objObject.Parent_Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.SHORTCUT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.SHORTCUT_FLD].Value = objObject.Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.BUTTON_CAPTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].Value = objObject.Button_Caption;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].Value = objObject.Text_CaptionDefault;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].Value = objObject.Text_Caption_Vi_VN;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].Value = objObject.Text_Caption_EN_US;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].Value = objObject.Text_Caption_JA_JP;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].Value =
                    objObject.Text_Caption_Language_Default;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_CHILD_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_CHILD_FLD].Value = objObject.Parent_Child;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.FORMLOAD_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.FORMLOAD_FLD].Value = objObject.FormLoad;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.DESCRIPTION_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.DESCRIPTION_FLD].Value = objObject.Description;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TYPE_FLD].Value = objObject.Type;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].Value = objObject.CollapsedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].Value = objObject.ExpandedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PREFIX_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PREFIX_FLD].Value = objObject.Prefix;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSFORMAT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSFORMAT_FLD].Value = objObject.TransFormat;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISTRANSACTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISTRANSACTION_FLD].Value = objObject.IsTransaction;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISUSERCREATED_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISUSERCREATED_FLD].Value = objObject.IsUserCreated;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TABLENAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TABLENAME_FLD].Value = objObject.TableName;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].Value = objObject.TransNoFieldName;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                return int.Parse(ocmdPCS.ExecuteScalar().ToString());
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                    {
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    }
                    else
                    {
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
        public DataSet GetAllProtection()
        {
            const string METHOD_NAME = THIS + ".GetAllProtection()";
            const string MENU_SEPARATOR = "'-'";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                // Create String Query
                string strSql = " SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                    // HACK: dungla 11-25-2005
                    // select more information
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                    // END: dungla 11-25-2005
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + " <> " + MENU_SEPARATOR
                                + " AND (" + Sys_Menu_EntryTable.TYPE_FLD + " = " + (int)MenuTypeEnum.VisibleBoth
                                + " OR " + Sys_Menu_EntryTable.TYPE_FLD + " = " + (int)MenuTypeEnum.InvisibleMenu + ")"
                                + " ORDER BY " + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, Sys_Menu_EntryTable.TABLE_NAME);
                strSql = " SELECT "
                         + Sys_RightTable.RIGHTID_FLD + ","
                         + Sys_RightTable.PERMISSION_FLD + ","
                         + Sys_RightTable.ROLEID_FLD + ","
                         + Sys_RightTable.MENU_ENTRYID_FLD
                         + " FROM " + Sys_RightTable.TABLE_NAME
                         + " ORDER BY " + Sys_RightTable.ROLEID_FLD;
                odadPCS.SelectCommand.CommandText = strSql;
                odadPCS.Fill(dstPCS, Sys_RightTable.TABLE_NAME);

                strSql = " SELECT "
                         + Sys_RoleTable.ROLEID_FLD + ","
                         + Sys_RoleTable.NAME_FLD + ","
                         + Sys_RoleTable.DESCRIPTION_FLD
                         + " FROM " + Sys_RoleTable.TABLE_NAME
                         + " WHERE Name <> '" + Constants.ALL_ROLE + "'"
                         + " ORDER BY " + Sys_RoleTable.ROLEID_FLD;

                odadPCS.SelectCommand.CommandText = strSql;
                odadPCS.Fill(dstPCS, Sys_RoleTable.TABLE_NAME);

                return dstPCS;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public object GetObjectVO(string pstrObjectName)
        {
            const string METHOD_NAME = THIS + ".GetObjectVO()";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.FORMLOAD_FLD + "= '" + pstrObjectName + "'";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                var objObject = new Sys_Menu_EntryVO();

                while (odrPCS.Read())
                {
                    objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    objObject.Button_Caption =
                        int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    objObject.Text_CaptionDefault =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_Vi_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                }
                return objObject;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public object GetMenuByShortCut(string pstrShortcut)
        {
            const string METHOD_NAME = THIS + ".GetMenuByShortCut()";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.SHORTCUT_FLD + "= ?";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.SHORTCUT_FLD].Value = pstrShortcut;

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                var objObject = new Sys_Menu_EntryVO();

                while (odrPCS.Read())
                {
                    objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    objObject.Button_Caption =
                        int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    objObject.Text_CaptionDefault =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_Vi_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    try
                    {
                        objObject.CollapsedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }

                    try
                    {
                        objObject.ExpandedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    objObject.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString().Trim();
                    objObject.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString().Trim();
                    try
                    {
                        objObject.IsUserCreated =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.ISUSERCREATED_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    try
                    {
                        objObject.IsTransaction =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.ISTRANSACTION_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    objObject.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString().Trim();
                    objObject.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString().Trim();
                    objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                }
                return objObject;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public ArrayList GetAllMenus(CultureInfo pculInfo)
        {
            const string METHOD_NAME = THIS + ".GetAllMenus()";
            //get language code from Culture Info
            string strLanCode = pculInfo.Name;
            strLanCode = strLanCode.Replace("-", "_");

            var arrList = new ArrayList();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " ORDER BY " + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "," +
                                Sys_Menu_EntryTable.BUTTON_CAPTION_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataReader odrPCS = ocmdPCS.ExecuteReader();

                //get all menu
                while (odrPCS.Read())
                {
                    var objObject = new Sys_Menu_EntryVO
                    {
                        Menu_EntryID =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim()),
                        Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim(),
                        Parent_Shortcut =
                            odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim(),
                        Button_Caption =
                            int.Parse(
                            odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim()),
                        Text_CaptionDefault =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim(),
                        Text_Caption_Vi_VN =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim(),
                        Text_Caption_EN_US =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim(),
                        Text_Caption_JA_JP =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim(),
                        Text_Caption_Language_Default =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString()
                            .Trim(),
                        Parent_Child =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim()),
                        FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim()
                    };
                    try
                    {
                        objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    arrList.Add(objObject);
                }
                //close the reader
                odrPCS.Close();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
            return arrList;
        }

        public ArrayList GetAllMenus(CultureInfo pculInfo, string pstrUserName)
        {
            const string METHOD_NAME = THIS + ".GetAllMenus()";
            //get language code from Culture Info
            string strLanCode = pculInfo.Name;
            strLanCode = strLanCode.Replace("-", "_");

            var arrList = new ArrayList();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + "A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + "A." + Sys_Menu_EntryTable.DESCRIPTION_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + ","
                                + " SUM(B.Permission) Permission " +
                                " FROM " + Sys_Menu_EntryTable.TABLE_NAME + " A INNER JOIN " + Sys_RightTable.TABLE_NAME
                                + " B ON A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + " = B." +
                                Sys_RightTable.MENU_ENTRYID_FLD
                                + " INNER JOIN " + Sys_RoleTable.TABLE_NAME + " C ON B. " + Sys_RightTable.ROLEID_FLD +
                                " =  C." + Sys_RoleTable.ROLEID_FLD
                                + " INNER JOIN " + Sys_UserToRoleTable.TABLE_NAME + " D ON C." +
                                Sys_RoleTable.ROLEID_FLD +
                                " = D." + Sys_UserToRoleTable.ROLEID_FLD
                                + " INNER JOIN " + Sys_UserTable.TABLE_NAME + " E ON D." +
                                Sys_UserToRoleTable.USERID_FLD +
                                " = E." + Sys_UserTable.USERID_FLD
                                + " WHERE " + Sys_UserTable.USERNAME_FLD + " = '" + pstrUserName + "'" + " and " +
                                Sys_RightTable.PERMISSION_FLD + " <> 0 AND "
                                + Sys_Menu_EntryTable.TYPE_FLD + "= 0"
                                +
                                " Group by A.Menu_EntryID, Shortcut, Parent_Shortcut, Button_Caption, Text_CaptionDefault, Text_Caption_VI_VN, Text_Caption_EN_US, Text_Caption_JA_JP, Text_Caption_Language_Default,Parent_Child,A.Description,FormLoad,Type "
                                + " ORDER BY " + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "," +
                                Sys_Menu_EntryTable.BUTTON_CAPTION_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataReader odrPCS = ocmdPCS.ExecuteReader();

                //get all menu
                while (odrPCS.Read())
                {
                    var objObject = new Sys_Menu_EntryVO
                    {
                        Menu_EntryID =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim()),
                        Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim(),
                        Parent_Shortcut =
                            odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim(),
                        Button_Caption =
                            int.Parse(
                            odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim()),
                        Text_CaptionDefault =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim(),
                        Text_Caption_Vi_VN =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim(),
                        Text_Caption_EN_US =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim(),
                        Text_Caption_JA_JP =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim(),
                        Text_Caption_Language_Default =
                            odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString()
                            .Trim(),
                        Description = odrPCS[Sys_Menu_EntryTable.DESCRIPTION_FLD].ToString().Trim(),
                        Parent_Child =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim()),
                        FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim()
                    };
                    try
                    {
                        objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                    }
                    catch
                    {
                    }
                    arrList.Add(objObject);
                }
                //close the reader
                odrPCS.Close();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
            return arrList;
        }

        public DataSet GetRestMenu(string pstrParentShortcut, int pintButtonCaption)
        {
            const string METHOD_NAME = THIS + ".GetRestMenu()";

            var dstData = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "= ?"
                                + " AND " + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + "> " + pintButtonCaption
                                + " ORDER BY " + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].Value = pstrParentShortcut;

                ocmdPCS.Connection.Open();
                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstData, Sys_Menu_EntryTable.TABLE_NAME);
                return dstData;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void UpdateDataTable(DataTable pdtbData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataTable()";
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            var odadPCS = new OleDbDataAdapter();

            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD
                                + "  FROM " + Sys_Menu_EntryTable.TABLE_NAME;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                odadPCS.Update(pdtbData);
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                    {
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    }
                    else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                    {
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    }
                }

                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public int GetAllTransactions(Sys_Menu_Entry menu)
        {
            const string METHOD_NAME = THIS + ".GetAllTransactions()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT ISNULL(COUNT(*), 0) FROM "
                                + menu.TableName
                                + " WHERE " + menu.TransNoFieldName + " LIKE '?%'";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(menu.TransNoFieldName, OleDbType.VarWChar));
                ocmdPCS.Parameters[menu.TransNoFieldName].Value = menu.Prefix;
                ocmdPCS.Connection.Open();
                object objResult = ocmdPCS.ExecuteScalar();
                try
                {
                    return int.Parse(objResult.ToString());
                }
                catch
                {
                    return 0;
                }
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void UpdateMenu(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".UpdateMenu()";

            var objObject = (Sys_Menu_EntryVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);
                string strSql = "UPDATE " + Sys_Menu_EntryTable.TABLE_NAME + " SET "
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.ISTRANSACTION_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.ISUSERCREATED_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + "=   ?" + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + "=  ?"
                                + " WHERE " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.SHORTCUT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.SHORTCUT_FLD].Value = objObject.Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].Value = objObject.Parent_Shortcut;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.BUTTON_CAPTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].Value = objObject.Button_Caption;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].Value = objObject.Text_CaptionDefault;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].Value = objObject.Text_Caption_Vi_VN;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].Value = objObject.Text_Caption_EN_US;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].Value = objObject.Text_Caption_JA_JP;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD,
                                                          OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].Value =
                    objObject.Text_Caption_Language_Default;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PARENT_CHILD_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PARENT_CHILD_FLD].Value = objObject.Parent_Child;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PREFIX_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.PREFIX_FLD].Value = objObject.Prefix;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSFORMAT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSFORMAT_FLD].Value = objObject.TransFormat;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISTRANSACTION_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISTRANSACTION_FLD].Value = objObject.IsTransaction;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.ISUSERCREATED_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.ISUSERCREATED_FLD].Value = objObject.IsUserCreated;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TABLENAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TABLENAME_FLD].Value = objObject.TableName;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].Value = objObject.TransNoFieldName;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.TYPE_FLD].Value = objObject.Type;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.MENU_ENTRYID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].Value = objObject.Menu_EntryID;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                    {
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    }
                    else
                    {
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void GetMenuByFormLoad(string pstrFormName, out string strTableName, out string strTransNoFieldName,
                                      out string strPrefix, out string strFormat)
        {
            const string METHOD_NAME = THIS + ".GetMenuByFormLoad()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            OleDbDataReader odrPCS = null;
            strTableName = string.Empty;
            strTransNoFieldName = string.Empty;
            strPrefix = string.Empty;
            strFormat = string.Empty;

            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD
                                + "  FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.FORMLOAD_FLD + "=?";
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.FORMLOAD_FLD, OleDbType.VarWChar)).Value =
                    pstrFormName;
                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();
                while (odrPCS.Read())
                {
                    strTableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString();
                    strTransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString();
                    strPrefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString();
                    strFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString();
                }
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public Sys_Menu_EntryVO GetMenuByFormLoadForCK(string pstrFormName, string pstrTableName,
                                                       string pstrTransNoFieldName, string pstrPrefix)
        {
            const string METHOD_NAME = THIS + ".GetMenuByFormLoad()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            OleDbDataReader odrPCS = null;

            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD
                                + "  FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.FORMLOAD_FLD + "=?"
                                + " AND " + Sys_Menu_EntryTable.TABLENAME_FLD + "=?"
                                + " AND " + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + "=?"
                                + " AND " + Sys_Menu_EntryTable.PREFIX_FLD + "=?";
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.FORMLOAD_FLD, OleDbType.VarWChar)).Value =
                    pstrFormName;
                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TABLENAME_FLD, OleDbType.VarWChar)).Value
                    = pstrTableName;
                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD, OleDbType.VarWChar))
                    .Value = pstrTransNoFieldName;
                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.PREFIX_FLD, OleDbType.VarWChar)).Value =
                    pstrPrefix;
                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();
                var voMenu = new Sys_Menu_EntryVO();
                if (odrPCS.HasRows)
                {
                    while (odrPCS.Read())
                    {
                        voMenu.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString();
                        voMenu.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString();
                        voMenu.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString();
                        voMenu.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString();
                    }
                }
                else
                {
                    odrPCS.Close();
                    strSql = "SELECT "
                             + Sys_Menu_EntryTable.PREFIX_FLD + ","
                             + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                             + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                             + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD
                             + "  FROM " + Sys_Menu_EntryTable.TABLE_NAME
                             + " WHERE " + Sys_Menu_EntryTable.FORMLOAD_FLD + "=?"
                             + " AND " + Sys_Menu_EntryTable.TABLENAME_FLD + "=?"
                             + " AND ISNULL(" + Sys_Menu_EntryTable.ISUSERCREATED_FLD + ",0) =0"
                             + " AND " + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + "=?";
                    ocmdPCS.CommandText = strSql;
                    ocmdPCS.Parameters.Clear();
                    ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.FORMLOAD_FLD, OleDbType.VarWChar)).
                        Value = pstrFormName;
                    ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TABLENAME_FLD, OleDbType.VarWChar)).
                        Value = pstrTableName;
                    ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD,
                                                              OleDbType.VarWChar)).Value = pstrTransNoFieldName;
                    if (oconPCS.State != ConnectionState.Open)
                        ocmdPCS.Connection.Open();
                    odrPCS = ocmdPCS.ExecuteReader();
                    while (odrPCS.Read())
                    {
                        voMenu.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString();
                        voMenu.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString();
                        voMenu.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString();
                        voMenu.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString();
                    }
                }

                return voMenu;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        #region Methods for menu icon

        public object GetObjectVOWithImageFields(int pintID)
        {
            const string METHOD_NAME = THIS + ".GetObjectVOWithImageFields()";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " WHERE " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + "=" + pintID;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                var objObject = new Sys_Menu_EntryVO();

                while (odrPCS.Read())
                {
                    objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    objObject.Button_Caption =
                        int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    objObject.Text_CaptionDefault =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_Vi_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default =
                        odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                    objObject.CollapsedImage =
                        int.Parse(odrPCS[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].ToString().Trim());
                    objObject.ExpandedImage = int.Parse(odrPCS[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].ToString().Trim());
                }
                return objObject;
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void UpdateWithImageFields(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".UpdateWithImageFields()";

            var objObject = (Sys_Menu_EntryVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);
                string strSql = "UPDATE Sys_Menu_Entry SET "
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + "= ?" + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + "= ?"
                                + " WHERE " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].Value = objObject.CollapsedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].Value = objObject.ExpandedImage;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Menu_EntryTable.MENU_ENTRYID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].Value = objObject.Menu_EntryID;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public ArrayList GetAllMenusWithImageFields(CultureInfo pculInfo)
        {
            const string METHOD_NAME = THIS + ".GetAllMenusWithImageFields()";
            //get language code from Culture Info
            string strLanCode = pculInfo.Name;
            strLanCode = strLanCode.Replace("-", "_");

            var arrList = new ArrayList();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.REPORTID_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD
                                + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                                + " ORDER BY " + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "," +
                                Sys_Menu_EntryTable.BUTTON_CAPTION_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataReader odrPCS = ocmdPCS.ExecuteReader();

                //get all menu
                while (odrPCS.Read())
                {
                    var objObject = new Sys_Menu_EntryVO();
                    try
                    {
                        objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    }
                    catch { }
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    try
                    {
                        objObject.Button_Caption = int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    }
                    catch
                    {
                        //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                    objObject.Text_CaptionDefault = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_Vi_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    try
                    {
                        objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    }
                    catch
                    { //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex); 
                    }
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    objObject.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString().Trim();
                    objObject.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString().Trim();
                    objObject.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString().Trim();
                    objObject.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString().Trim();
                    objObject.ReportID = odrPCS[Sys_Menu_EntryTable.REPORTID_FLD].ToString().Trim();

                    try
                    {
                        objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                        objObject.CollapsedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].ToString().Trim());
                        objObject.ExpandedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                        //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                    arrList.Add(objObject);
                }
                //close the reader
                odrPCS.Close();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
            return arrList;
        }

        public List<Sys_Menu_Entry> GetAllMenusWithImageFields(CultureInfo pculInfo, string pstrUserName)
        {
            const string METHOD_NAME = THIS + ".GetAllMenusWithImageFields()";
            //get language code from Culture Info
            string strLanCode = pculInfo.Name;
            strLanCode = strLanCode.Replace("-", "_");
            var arrList = new List<Sys_Menu_Entry>();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                                + "A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + ","
                                + Sys_Menu_EntryTable.SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + ","
                                + Sys_Menu_EntryTable.BUTTON_CAPTION_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD + ","
                                + Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD + ","
                                + Sys_Menu_EntryTable.PARENT_CHILD_FLD + ","
                                + "A." + Sys_Menu_EntryTable.DESCRIPTION_FLD + ","
                                + Sys_Menu_EntryTable.FORMLOAD_FLD + ","
                                + Sys_Menu_EntryTable.TYPE_FLD + ","
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD + ","
                                + Sys_Menu_EntryTable.REPORTID_FLD + ","
                                + " SUM(B.Permission) Permission " +
                                " FROM " + Sys_Menu_EntryTable.TABLE_NAME + " A INNER JOIN " + Sys_RightTable.TABLE_NAME
                                + " B ON A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD + " = B." +
                                Sys_RightTable.MENU_ENTRYID_FLD
                                + " INNER JOIN " + Sys_RoleTable.TABLE_NAME + " C ON B. " + Sys_RightTable.ROLEID_FLD +
                                " =  C." + Sys_RoleTable.ROLEID_FLD
                                + " INNER JOIN " + Sys_UserToRoleTable.TABLE_NAME + " D ON C." +
                                Sys_RoleTable.ROLEID_FLD +
                                " = D." + Sys_UserToRoleTable.ROLEID_FLD
                                + " INNER JOIN " + Sys_UserTable.TABLE_NAME + " E ON D." +
                                Sys_UserToRoleTable.USERID_FLD +
                                " = E." + Sys_UserTable.USERID_FLD
                                + " WHERE " + Sys_UserTable.USERNAME_FLD + " = '" + pstrUserName + "'" + " and " +
                                Sys_RightTable.PERMISSION_FLD + " <> 0" + " AND ("
                                + Sys_Menu_EntryTable.TYPE_FLD + "= " + (int)MenuTypeEnum.VisibleBoth + " OR "
                                + Sys_Menu_EntryTable.TYPE_FLD + "= " + (int)MenuTypeEnum.VisibleMenu + ")"                                
                                + " Group by A.Menu_EntryID, Shortcut, Parent_Shortcut, Button_Caption, Text_CaptionDefault, Text_Caption_VI_VN, Text_Caption_EN_US, Text_Caption_JA_JP, Text_Caption_Language_Default,Parent_Child,A.Description,FormLoad,Type, " +
                                Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD + ", " + Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD +
                                ", "
                                + Sys_Menu_EntryTable.PREFIX_FLD + ","
                                + Sys_Menu_EntryTable.TABLENAME_FLD + ","
                                + Sys_Menu_EntryTable.TRANSFORMAT_FLD + ","
                                + Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD + ","
                                + Sys_Menu_EntryTable.REPORTID_FLD
                                + " ORDER BY " + Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD + "," +
                                Sys_Menu_EntryTable.BUTTON_CAPTION_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataReader odrPCS = ocmdPCS.ExecuteReader();

                //get all menu
                while (odrPCS.Read())
                {
                    var objObject = new Sys_Menu_Entry();
                    try
                    {
                        objObject.Menu_EntryID = int.Parse(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD].ToString().Trim());
                    }
                    catch { }
                    objObject.Shortcut = odrPCS[Sys_Menu_EntryTable.SHORTCUT_FLD].ToString().Trim();
                    objObject.Parent_Shortcut = odrPCS[Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD].ToString().Trim();
                    try
                    {
                        objObject.Button_Caption = int.Parse(odrPCS[Sys_Menu_EntryTable.BUTTON_CAPTION_FLD].ToString().Trim());
                    }
                    catch
                    {
                        //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                    objObject.Text_CaptionDefault = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTIONDEFAULT_FLD].ToString().Trim();
                    objObject.Text_Caption_VI_VN = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_VI_VN_FLD].ToString().Trim();
                    objObject.Text_Caption_EN_US = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_EN_US_FLD].ToString().Trim();
                    objObject.Text_Caption_JA_JP = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_JA_JP_FLD].ToString().Trim();
                    objObject.Text_Caption_Language_Default = odrPCS[Sys_Menu_EntryTable.TEXT_CAPTION_LANGUAGE_DEFAULT_FLD].ToString().Trim();
                    try
                    {
                        objObject.Parent_Child = int.Parse(odrPCS[Sys_Menu_EntryTable.PARENT_CHILD_FLD].ToString().Trim());
                    }
                    catch
                    { //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex); 
                    }
                    objObject.FormLoad = odrPCS[Sys_Menu_EntryTable.FORMLOAD_FLD].ToString().Trim();
                    objObject.TransFormat = odrPCS[Sys_Menu_EntryTable.TRANSFORMAT_FLD].ToString().Trim();
                    objObject.TransNoFieldName = odrPCS[Sys_Menu_EntryTable.TRANSNOFIELDNAME_FLD].ToString().Trim();
                    objObject.TableName = odrPCS[Sys_Menu_EntryTable.TABLENAME_FLD].ToString().Trim();
                    objObject.Prefix = odrPCS[Sys_Menu_EntryTable.PREFIX_FLD].ToString().Trim();
                    objObject.ReportID = odrPCS[Sys_Menu_EntryTable.REPORTID_FLD].ToString().Trim();

                    try
                    {
                        objObject.Type = int.Parse(odrPCS[Sys_Menu_EntryTable.TYPE_FLD].ToString().Trim());
                        objObject.CollapsedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.COLLAPSEDIMAGE_FLD].ToString().Trim());
                        objObject.ExpandedImage =
                            int.Parse(odrPCS[Sys_Menu_EntryTable.EXPANDEDIMAGE_FLD].ToString().Trim());
                    }
                    catch
                    {
                        //throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                    }
                    arrList.Add(objObject);
                }
                //close the reader
                odrPCS.Close();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
            return arrList;
        }

        public ArrayList GetAllDefaultMenus()
        {
            const string METHOD_NAME = THIS + ".GetAllDefaultMenus()";
            var arrList = new ArrayList();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = string.Empty;
                strSql = "SELECT DISTINCT "
                         + "A." + Sys_Menu_EntryTable.MENU_ENTRYID_FLD
                         + " FROM " + Sys_Menu_EntryTable.TABLE_NAME + " A "
                         + " INNER JOIN " + Sys_Menu_EntryTable.TABLE_NAME + " B "
                         + " ON A." + Sys_Menu_EntryTable.SHORTCUT_FLD + " = B." +
                         Sys_Menu_EntryTable.PARENT_SHORTCUT_FLD
                         + " WHERE A." + Sys_Menu_EntryTable.TYPE_FLD + " IN (" + ((int)MenuTypeEnum.VisibleBoth) + "," +
                         ((int)MenuTypeEnum.VisibleMenu) + ")"
                         + " AND B." + Sys_Menu_EntryTable.TYPE_FLD + " IN (" + ((int)MenuTypeEnum.VisibleBoth) + "," +
                         ((int)MenuTypeEnum.VisibleMenu) + ")"
                         + " AND B." + Sys_Menu_EntryTable.PARENT_CHILD_FLD + " = " +
                         ((int)MenuParentChildEnum.SpecialLeafMenu)
                         + " UNION "
                         + " SELECT " + Sys_Menu_EntryTable.MENU_ENTRYID_FLD
                         + " FROM " + Sys_Menu_EntryTable.TABLE_NAME
                         + " WHERE " + Sys_Menu_EntryTable.PARENT_CHILD_FLD + "=" +
                         ((int)MenuParentChildEnum.SpecialLeafMenu);

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataReader odrPCS = ocmdPCS.ExecuteReader();

                //get all menu
                while (odrPCS.Read())
                {
                    arrList.Add(odrPCS[Sys_Menu_EntryTable.MENU_ENTRYID_FLD]);
                }
                //close the reader
                odrPCS.Close();
            }
            catch (OleDbException ex)
            {
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (InvalidOperationException ex)
            {
                throw new PCSDBException(ErrorCode.INVALIDEXCEPTION, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }

            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
            return arrList;
        }

        #endregion
    }
}