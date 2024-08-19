using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Linq;
using Epoint.Systems;
using Epoint.Systems.Data;
using Epoint.Systems.Controls;
using Epoint.Systems.Customizes;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Elements;
using System.IO;
using System.Data.OleDb;
namespace Epoint.Modules
{
    public class Voucher
    {

        //SQLUpdateCt
        public static bool SQLUpdateCt(frmVoucher_Edit frmEditCt)
        {
            SqlConnection sqlCon = SQLExec.GetNewSQLConnection();
            SqlCommand sqlCom = sqlCon.CreateCommand();

            SqlTransaction sqlTran = sqlCom.Connection.BeginTransaction("Update_Voucher_Tran");

            sqlCom.Transaction = sqlTran;

            string strKey = string.Empty;
            string strTable_Ph = (string)frmEditCt.drDmCt["Table_Ph"];
            string sp_UpdatePh = (string)frmEditCt.drDmCt["sp_UpdatePh"];
            string strStt = (string)frmEditCt.drEditPh["Stt"];
            string strMa_Ct = (string)frmEditCt.drEditPh["Ma_Ct"];
            #region Update CT TVP

            #endregion
            #region UpdatePh
            if (frmEditCt.drEditPh != null)
            {//Có nhiều trường hợp cập nhật CT mà không cần cập nhật PH(VD: frmEditLR)

                sqlCom.CommandText = sp_UpdatePh;
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCom.Parameters.Clear();

                strKey = "Object_id = Object_id('" + sp_UpdatePh + "')";
                DataTable dtUpdatePh_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

                sqlCom.Parameters.AddWithValue("@strNew_Edit", (char)frmEditCt.enuNew_Edit);
                Common.SetDefaultDataRow(ref frmEditCt.drEditPh);

                foreach (DataRow drPara in dtUpdatePh_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!frmEditCt.drEditPh.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, frmEditCt.drEditPh[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlTran.Rollback();
                    return false;
                }
            }
            #endregion

            #region UpdateCt
            if (!UpdateCt(frmEditCt, sqlCom, frmEditCt.dtEditCt))
                return false;

            //UpdateCtLR
            if (Common.Inlist(frmEditCt.strMa_Ct, "LR,TR"))
            {
                if (!UpdateCt(frmEditCt, sqlCom, ((IEditCtLR)frmEditCt).dtEdiCtLR, "sp_UpdateIN_LR", "INLAPRAP"))
                    return false;
            }

            #endregion

            #region Update Discount and promotion
            if (!OM_SaveOM_SalesDics(frmEditCt))
                return false;
            #endregion


            #region UpdateLSX
            if (frmEditCt.dtCtVt != null)
                if (!UpdateCtSX(frmEditCt, sqlCom, frmEditCt.dtCtVt))
                    return false;
            #endregion
            //#region UpdateQueue

            //if (!Voucher.UpdateQueue(sqlCom, frmEditCt.drEditPh))
            //    return false;

            //#endregion

            #region UpdateHanTt0
            if (!UpdateHanTt(frmEditCt, sqlCom))
                return false;
            #endregion

            #region History
            if (frmEditCt.enuNew_Edit == enuEdit.Edit)
                if (!SaveHistory(frmEditCt, sqlCom))
                    return false;
            #endregion

            //Luu So_Ct
            string strLoai_Ma_Ct = ((DateTime)frmEditCt.drEditPh["Ngay_Ct"]).Month.ToString().Trim();
            string[] strParaName = new string[] { "Ma_Ct", "Loai_Ma_Ct", "Ngay_Ct", "So_Ct" };
            object[] objParaValue = new object[] { frmEditCt.strMa_Ct, strLoai_Ma_Ct, frmEditCt.drEditPh["Ngay_Ct"], frmEditCt.drEditPh["So_Ct"] };
            SQLExec.Execute("Sp_Luu_So_Ct", strParaName, objParaValue, CommandType.StoredProcedure);

            Update_dsVoucher(frmEditCt);
            sqlTran.Commit();

            return true;
        }

        public static bool UpdateCt(frmVoucher_Edit frmEditCt, SqlCommand sqlCom, DataTable dtEditCt)
        {
            string sp_UpdateCt = (string)frmEditCt.drDmCt["sp_UpdateCt"];
            string strTable_Ct = (string)frmEditCt.drDmCt["Table_Ct"];

            return UpdateCt(frmEditCt, sqlCom, dtEditCt, sp_UpdateCt, strTable_Ct);
        }

        public static bool UpdateCt(frmVoucher_Edit frmEditCt, SqlCommand sqlCom, DataTable dtEditCt, string sp_UpdateCt, string strTable_Ct)
        {
            #region UpdateCt: Cap nhat tung dong trong dtEditCt

            int iSave_Ct_Success = 0;


            sqlCom.Parameters.Clear();

            //Xoa du lieu cu trong Chung tu
            if (frmEditCt.enuNew_Edit == enuEdit.Edit)
            {
                sqlCom.CommandType = CommandType.Text;
                sqlCom.CommandText = "DELETE FROM " + strTable_Ct + " WHERE Stt = @Stt";
                sqlCom.Parameters.AddWithValue("@Stt", (string)frmEditCt.drEditPh["Stt"]);
                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            //Luu du lieu vao Ct
            sqlCom.CommandText = sp_UpdateCt;
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('" + sp_UpdateCt + "')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtEditCt.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                if (dr.Table.Columns.Contains("Deleted") && (bool)dr["Deleted"])
                    continue;

                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                    iSave_Ct_Success += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            #endregion

            #region UpdateCt: khong thuc hien duoc dong nao -> xoa han chung tu nay
            if (iSave_Ct_Success == 0)
            {
                if (Common.MsgYes_No("Chứng từ không có dữ liệu, có tiếp tục lưu hay không?"))
                {//Neu van tiep tuc luu thi xem nhu xoa chung tu nay

                    sqlCom.Transaction.Rollback();
                    SQLDeleteCt(frmEditCt.strStt, frmEditCt.strMa_Ct);
                    return true;
                }
                else
                {
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            return true;
            #endregion
        }

        #region UpdateLSX
        public static void UpdateLSX(frmVoucher_Edit frmEditCt, DataTable dtDinhMucVt)
        {
            if (dtDinhMucVt.Rows.Count < 0)
                return;

            foreach (DataRow dr in dtDinhMucVt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht["STT"] = dr["Stt"];
                ht["NGAY_CT"] = dr["Ngay_Ct"];
                ht["MA_SO"] = dr["Ma_SO"];
                ht["MA_SP"] = dr["Ma_Sp"];
                ht["MA_VT"] = dr["Ma_Vt"];
                ht["TEN_VT"] = dr["Ten_Vt"];
                ht["DVT"] = dr["Dvt"];
                ht["SO_LUONG"] = dr["So_Luong"];
                ht["SO_LUONG_DM"] = dr["So_Luong_Dm"];
                ht["SO_LUONG_VTDC"] = dr["So_Luong_VtDc"];
                //ht["GHI_CHU_LSX"] = dr["Ghi_Chu_Lsx"];
                ht["DELETED"] = dr["Deleted"];
                ht["MA_DVCS"] = Element.sysMa_DvCs;

                SQLExec.Execute("Sp_UpdateMA_LSX", ht, CommandType.StoredProcedure);
            }
        }
        public static bool UpdateCtSX(frmVoucher_Edit frmEditCt, SqlCommand sqlCom, DataTable dtCtVt)
        {
            string sp_UpdateCt = "Sp_UpdateMA_LSX";
            string strTable_Ct = "MALSX";

            #region UpdateCt: Cap nhat tung dong trong dtEditCt

            int iSave_Ct_Success = 0;


            sqlCom.Parameters.Clear();

            //Xoa du lieu cu trong Chung tu
            if (frmEditCt.enuNew_Edit == enuEdit.Edit)
            {
                sqlCom.CommandType = CommandType.Text;
                sqlCom.CommandText = "DELETE FROM " + strTable_Ct + " WHERE Stt = @Stt";
                sqlCom.Parameters.AddWithValue("@Stt", (string)frmEditCt.drEditPh["Stt"]);
                sqlCom.Parameters.AddWithValue("@Ma_Ct", (string)frmEditCt.drEditPh["Ma_Ct"]);

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            //Luu du lieu vao Ct
            sqlCom.CommandText = sp_UpdateCt;
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('" + sp_UpdateCt + "')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtCtVt.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                if (dr.Table.Columns.Contains("Deleted") && (bool)dr["Deleted"])
                {
                    //// Xoa nhung dong de duoc danh dau Deleted tren dtEditCt.Rows
                    //foreach (DataRow drDmVt in dtCtVt.Rows)
                    //{
                    //    if ((string)drDmVt["Ma_SO"] == (string)dr["Ma_SO"] && (string)drDmVt["Ma_Sp"] == (string)dr["Ma_Vt"])
                    //        drDmVt["Deleted"] = true;
                    //}
                    //dtCtVt.AcceptChanges();
                    continue;
                }

                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                    iSave_Ct_Success += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            #endregion

            #region UpdateCt: khong thuc hien duoc dong nao -> xoa han chung tu nay
            if (iSave_Ct_Success == 0)
            {
                if (Common.MsgYes_No("Chứng từ không có dữ liệu, có tiếp tục lưu hay không?"))
                {//Neu van tiep tuc luu thi xem nhu xoa chung tu nay

                    sqlCom.Transaction.Rollback();
                    SQLDeleteCt(frmEditCt.strStt, frmEditCt.strMa_Ct);
                    return true;
                }
                else
                {
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }
            return true;
            #endregion
        }
        #endregion

        public static bool UpdateHanTt(frmVoucher_Edit frmEditCt, SqlCommand sqlCom)
        {
            if (frmEditCt.dtHanTt0 == null || frmEditCt.dtHanTt0.Rows.Count <= 0)
                return true;

            string strStt = (string)frmEditCt.drEditPh["Stt"];
            sqlCom.CommandText = "sp_UpdateHanTt0";
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('sp_UpdateHanTt0')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            DataRow[] drArrHanTt0 = frmEditCt.dtHanTt0.Select();

            //Lưu xuống CSDL
            Hashtable htParameter = new Hashtable();
            htParameter.Add("STT", strStt);
            htParameter.Add("MA_CT", frmEditCt.drEditPh["Ma_Ct"]);
            htParameter.Add("NGAY_CT", frmEditCt.drEditPh["Ngay_Ct"]);
            htParameter.Add("SO_CT", frmEditCt.drEditPh["So_Ct"]);
            htParameter.Add("DIEN_GIAI", frmEditCt.drEditPh["Dien_Giai"]);
            htParameter.Add("MA_TTE", frmEditCt.drEditPh["Ma_Tte"]);
            htParameter.Add("TY_GIA", frmEditCt.drEditPh["Ty_Gia"]);
            htParameter.Add("TK", string.Empty);
            htParameter.Add("MA_DT", string.Empty);
            htParameter.Add("TIEN_TT", 0);
            htParameter.Add("TIEN_TT_NT", 0);
            htParameter.Add("TIEN_CLTG", 0);
            htParameter.Add("STT_HD", string.Empty);
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);

            foreach (DataRow dr in drArrHanTt0)
            {
                if (!(bool)dr["Modify"])
                    continue;

                htParameter["TK"] = dr["Tk"];
                htParameter["MA_DT"] = dr["Ma_Dt"];
                htParameter["TIEN_TT"] = dr["Tien_Tt1"];
                htParameter["TIEN_TT_NT"] = dr["Tien_Tt_Nt1"];
                htParameter["TIEN_CLTG"] = dr["Tien_CLTG"];
                htParameter["STT_HD"] = dr["Stt"];
                htParameter["LASTMODIFY_LOG"] = dr["LastModify_Log"];

                //SQLExec.Execute("sp_UpdateHanTt0", htParameter, CommandType.StoredProcedure);				

                sqlCom.Parameters.Clear();

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!htParameter.Contains(strColumnName.ToUpper()))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, htParameter[strColumnName.ToUpper()]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            return true;
        }

        public static void Update_DmNvu(frmVoucher_Edit frmEditCt)
        {
            string strDefaultColumnList = "Tk_No,Tk_Co,Tk_No2,Tk_Co2,Ma_Dt,Ma_Bp,Ma_Km,Ma_Vt_Sp,Ma_Hd,Ma_Job,Ma_CbNv,Ma_Thue,Ma_Kho,Ma_KhoN";
            bool bCheckExist_DefaultValue = DataTool.SQLCheckExist("sys.procedures", "Name", "sp_GetDmNvu_DefaultValue");

            if (frmEditCt.enuNew_Edit == enuEdit.Edit)
            {
                Control ctrl = frmEditCt.Controls.Find("txtMa_Nvu", true).FirstOrDefault();
                if (ctrl != null && !(ctrl.Focused && ((txtTextLookup)ctrl).bTextChange))
                    return;
            }

            #region Cập nhật trên Form
            foreach (string strDefaultColumn in strDefaultColumnList.Split(','))
            {
                if (!frmEditCt.drDmNvu.Table.Columns.Contains(strDefaultColumn)) //Kiểm tra tồn tại Cột
                    continue;

                string strDefaultValue = frmEditCt.drDmNvu[strDefaultColumn].ToString();

                TextBox txt = frmEditCt.Controls.Find("txt" + strDefaultColumn, true).FirstOrDefault() as TextBox;

                if (strDefaultValue != "" && txt != null)
                {
                    if (bCheckExist_DefaultValue)
                    {
                        //Tìm giá trị ngầm định trên phiếu trước đó
                        Hashtable htPara = new Hashtable();
                        htPara.Add("MA_NVU", frmEditCt.drDmNvu["Ma_Nvu"].ToString());
                        htPara.Add("TABLE_CT", frmEditCt.drDmCt["Table_Ct"].ToString());
                        htPara.Add("NGAY_CT", frmEditCt.drEditPh["Ngay_Ct"]);
                        htPara.Add("DEFAULTCOLUMN", strDefaultColumn);
                        htPara.Add("DEFAULTVALUELIST", strDefaultValue);
                        htPara.Add("MA_DVCS", Element.sysMa_DvCs);

                        string strValue = SQLExec.ExecuteReturnValue("sp_GetDmNvu_DefaultValue", htPara, CommandType.StoredProcedure).ToString();


                        if (strValue != "")
                            txt.Text = strValue;
                        else
                            txt.Text = strDefaultValue.Split(',')[0];
                    }
                    else
                    {
                        if (!strDefaultValue.Contains(",") && !txt.Text.StartsWith(strDefaultValue))
                            txt.Text = strDefaultValue;
                    }
                }
            }
            #endregion

            #region Cập nhật trên lưới
            foreach (DataRow dr in frmEditCt.dtEditCt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                    continue;

                if ((bool)dr["Deleted"])
                    continue;

                foreach (string strDefaultColumn in strDefaultColumnList.Split(','))
                {
                    if (!frmEditCt.drDmNvu.Table.Columns.Contains(strDefaultColumn)) //Kiểm tra tồn tại Cột
                        continue;

                    string strDefaultValue = frmEditCt.drDmNvu[strDefaultColumn].ToString();

                    if (strDefaultValue != "" && frmEditCt.dtEditCt.Columns.Contains(strDefaultColumn))
                    {
                        if (bCheckExist_DefaultValue)
                        {
                            //Tìm giá trị ngầm định trên phiếu trước đó
                            Hashtable htPara = new Hashtable();
                            htPara.Add("MA_NVU", frmEditCt.drDmNvu["Ma_Nvu"].ToString());
                            htPara.Add("TABLE_CT", frmEditCt.drDmCt["Table_Ct"].ToString());
                            htPara.Add("NGAY_CT", frmEditCt.drEditPh["Ngay_Ct"]);
                            htPara.Add("DEFAULTCOLUMN", strDefaultColumn);
                            htPara.Add("DEFAULTVALUELIST", strDefaultValue);
                            htPara.Add("MA_DVCS", Element.sysMa_DvCs);

                            string strValue = SQLExec.ExecuteReturnValue("sp_GetDmNvu_DefaultValue", htPara, CommandType.StoredProcedure).ToString();

                            if (strValue != "")
                                dr[strDefaultColumn] = strValue;
                            else
                                dr[strDefaultColumn] = strDefaultValue.Split(',')[0];
                        }
                        else
                        {
                            if (!strDefaultValue.Contains(",") && !dr[strDefaultColumn].ToString().StartsWith(strDefaultValue))
                                dr[strDefaultColumn] = strDefaultValue;
                        }
                    }
                }
            }
            #endregion
        }
        public static bool SaveHistory(frmVoucher_Edit frmEditCt, SqlCommand sqlCom)
        {
            DataTable dtEditCtOrg = frmEditCt.dtEditCtOrg;
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtHistory = frmEditCt.dtEditCt.Clone();

            #region dtHistory
            foreach (DataRow drEditCtOrg in dtEditCtOrg.Rows)
            {
                DataRow[] arrdrEditCt = dtEditCt.Select("Stt0 = " + drEditCtOrg["Stt0"].ToString());
                if (arrdrEditCt.Length == 0)
                {
                    dtHistory.ImportRow(drEditCtOrg);
                }
                else
                {
                    DataRow drEditCt = arrdrEditCt[0];
                    DataRow drHistory = dtHistory.NewRow();
                    Common.SetDefaultDataRow(ref drHistory);
                    drHistory["Stt"] = drEditCtOrg["Stt"];
                    drHistory["Stt0"] = drEditCtOrg["Stt0"];
                    drHistory["Ma_Ct"] = drEditCtOrg["Ma_Ct"];
                    drHistory["Ngay_Ct"] = drEditCtOrg["Ngay_Ct"];
                    drHistory["So_Ct"] = drEditCtOrg["So_Ct"];
                    drHistory["Dien_Giai"] = drEditCtOrg["Dien_Giai"];

                    bool bIs_Edit = false;
                    foreach (DataColumn dc in dtEditCtOrg.Columns)
                    {
                        switch (dc.DataType.ToString())
                        {
                            case "System.Boolean":
                            case "System.Byte":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Decimal":
                            case "System.Double":
                                if (Convert.ToDouble(drEditCtOrg[dc.ColumnName]) != Convert.ToDouble(drEditCt[dc.ColumnName]))
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    bIs_Edit = true;
                                }
                                break;
                            case "System.String":
                                if (Convert.ToString(drEditCtOrg[dc.ColumnName]) != Convert.ToString(drEditCt[dc.ColumnName]))
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    bIs_Edit = true;
                                }
                                break;
                            case "System.DateTime":
                                if (drEditCtOrg[dc.ColumnName].ToString() != drEditCt[dc.ColumnName].ToString())
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    bIs_Edit = true;
                                }
                                break;
                        }
                    }

                    if (bIs_Edit == true)
                    {
                        dtHistory.Rows.Add(drHistory);
                    }
                }
            }

            dtHistory.AcceptChanges();
            if (dtHistory.Rows.Count == 0)
                return true;

            DataColumn dc2 = new DataColumn("Member_ID", typeof(string));
            dc2.DefaultValue = Element.sysUser_Id;
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Update_Type", typeof(string));
            dc2.DefaultValue = "E";
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Ngay_Update", typeof(DateTime));
            dc2.DefaultValue = DateTime.Now;
            dtHistory.Columns.Add(dc2);


            #endregion

            #region Luu du lieu vao History
            sqlCom.CommandText = "Sp_UpdateHistoryVoucher";
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('Sp_UpdateHistoryVoucher')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtHistory.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            return true;
            #endregion
        }
        public static bool SaveHistoryDelete(string Stt, string Ma_Ct, SqlCommand sqlCom)
        {
            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", Ma_Ct);

            string strKeyFillterCt = " Stt = '" + Stt + "' ";

            string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt ";
            string strSelectCt = "*";// enuNew_Edit == enuEdit.New lấy hàng đầu tiên

            DataTable dtEditPh = DataTool.SQLGetDataTable((string)drDmCt["Table_Ph"], strSelectPh, strKeyFillterCt, null);
            DataTable dtEditCt = DataTool.SQLGetDataTable((string)drDmCt["Table_Ct"], strSelectCt, strKeyFillterCt, null);

            //ThongLH: History
            DataTable dtEditCtOrg = dtEditCt.Copy();

            DataTable dtHistory = dtEditCt.Clone();

            #region dtHistory
            foreach (DataRow drEditCtOrg in dtEditCtOrg.Rows)
            {
                DataRow[] arrdrEditCt = dtEditCt.Select("Stt0 = " + drEditCtOrg["Stt0"].ToString());
                if (arrdrEditCt.Length == 0)
                {
                    dtHistory.ImportRow(drEditCtOrg);
                }
                else
                {
                    DataRow drEditCt = arrdrEditCt[0];
                    DataRow drHistory = dtHistory.NewRow();
                    Common.SetDefaultDataRow(ref drHistory);
                    drHistory["Stt"] = drEditCtOrg["Stt"];
                    drHistory["Stt0"] = drEditCtOrg["Stt0"];
                    drHistory["Ma_Ct"] = drEditCtOrg["Ma_Ct"];
                    drHistory["Ngay_Ct"] = drEditCtOrg["Ngay_Ct"];
                    drHistory["So_Ct"] = drEditCtOrg["So_Ct"];
                    drHistory["Dien_Giai"] = drEditCtOrg["Dien_Giai"];

                    bool bIs_Edit = true;
                    foreach (DataColumn dc in dtEditCtOrg.Columns)
                    {
                        switch (dc.DataType.ToString())
                        {
                            case "System.Boolean":
                            case "System.Byte":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Decimal":
                            case "System.Double":
                                if (Convert.ToDouble(drEditCtOrg[dc.ColumnName]) != Convert.ToDouble(drEditCt[dc.ColumnName]))
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    //bIs_Edit = true;
                                }
                                break;
                            case "System.String":
                                if (Convert.ToString(drEditCtOrg[dc.ColumnName]) != Convert.ToString(drEditCt[dc.ColumnName]))
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    //bIs_Edit = true;
                                }
                                break;
                            case "System.DateTime":
                                if (drEditCtOrg[dc.ColumnName].ToString() != drEditCt[dc.ColumnName].ToString())
                                {
                                    drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                    //bIs_Edit = true;
                                }
                                break;
                        }
                    }

                    if (bIs_Edit == true)
                    {
                        dtHistory.Rows.Add(drHistory);
                    }
                }
            }

            dtHistory.AcceptChanges();
            if (dtHistory.Rows.Count == 0)
                return true;

            DataColumn dc2 = new DataColumn("Member_ID", typeof(string));
            dc2.DefaultValue = Element.sysUser_Id;
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Update_Type", typeof(string));
            dc2.DefaultValue = "D";
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Ngay_Update", typeof(DateTime));
            dc2.DefaultValue = DateTime.Now;
            dtHistory.Columns.Add(dc2);


            #endregion

            #region Luu du lieu vao History
            sqlCom.CommandText = "Sp_UpdateHistoryVoucher";
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('Sp_UpdateHistoryVoucher')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtHistory.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            return true;
            #endregion
        }
        public static bool SaveHistoryDelete_All(string Stt, string Ma_Ct, SqlCommand sqlCom)
        {
            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", Ma_Ct);

            string strKeyFillterCt = " Stt = '" + Stt + "' ";

            string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt ";
            string strSelectCt = "*";// enuNew_Edit == enuEdit.New lấy hàng đầu tiên

            DataTable dtEditPh = DataTool.SQLGetDataTable((string)drDmCt["Table_Ph"], strSelectPh, strKeyFillterCt, null);
            DataTable dtEditCt = DataTool.SQLGetDataTable((string)drDmCt["Table_Ct"], strSelectCt, strKeyFillterCt, null);

            //ThongLH: History
            DataTable dtEditCtOrg = dtEditCt.Copy();

            DataTable dtHistory = dtEditCt.Clone();

            //Common.CopyDataColumn(dtEditCt, dtHistory,string.Empty);

            #region dtHistory
            foreach (DataRow drEditCtOrg in dtEditCtOrg.Rows)
            {
                DataRow[] arrdrEditCt = dtEditCt.Select("Stt0 = " + drEditCtOrg["Stt0"].ToString());
                if (arrdrEditCt.Length == 0)
                {
                    dtHistory.ImportRow(drEditCtOrg);
                }
                else
                {
                    DataRow drEditCt = arrdrEditCt[0];
                    DataRow drHistory = dtHistory.NewRow();
                    Common.SetDefaultDataRow(ref drHistory);
                    drHistory["Stt"] = drEditCtOrg["Stt"];
                    drHistory["Stt0"] = drEditCtOrg["Stt0"];
                    drHistory["Ma_Ct"] = drEditCtOrg["Ma_Ct"];
                    drHistory["Ngay_Ct"] = drEditCtOrg["Ngay_Ct"];
                    drHistory["So_Ct"] = drEditCtOrg["So_Ct"];
                    drHistory["Dien_Giai"] = drEditCtOrg["Dien_Giai"];

                    bool bIs_Edit = true;
                    foreach (DataColumn dc in dtEditCtOrg.Columns)
                    {
                        switch (dc.DataType.ToString())
                        {
                            case "System.Boolean":
                            case "System.Byte":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Decimal":
                            case "System.Double":
                                drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                break;
                            case "System.String":
                                drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                //bIs_Edit = true;                               
                                break;
                            case "System.DateTime":
                                drHistory[dc.ColumnName] = drEditCtOrg[dc.ColumnName];
                                break;
                        }
                    }

                    if (bIs_Edit == true)
                    {
                        dtHistory.Rows.Add(drHistory);
                    }
                }
            }

            dtHistory.AcceptChanges();
            if (dtHistory.Rows.Count == 0)
                return true;

            DataColumn dc2 = new DataColumn("Member_ID", typeof(string));
            dc2.DefaultValue = Element.sysUser_Id;
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Update_Type", typeof(string));
            dc2.DefaultValue = "D";
            dtHistory.Columns.Add(dc2);

            dc2 = new DataColumn("Ngay_Update", typeof(DateTime));
            dc2.DefaultValue = DateTime.Now;
            dtHistory.Columns.Add(dc2);


            #endregion

            #region Luu du lieu vao History
            sqlCom.CommandText = "Sp_UpdateHistoryVoucher";
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('Sp_UpdateHistoryVoucher')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtHistory.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            return true;
            #endregion
        }
        public static bool SQLDeleteCt(string strStt, string strMa_Ct)
        {
            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strMa_Ct);

            if (drDmCt == null)
                return false;

            //Kiem tra Permission
            if (!Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_Delete))
            {
                Common.MsgCancel(Languages.GetLanguage("No_Permission") + ' ' + Languages.GetLanguage("Delete"));
                return false;
            }

            if (!Element.sysIs_Admin)
            {
                string strCreate_User = (string)SQLExec.ExecuteReturnValue("SELECT ISNULL(MAX(Create_Log), '') FROM GLVOUCHER (NOLOCK) WHERE Stt = '" + strStt + "'");

                if (strCreate_User != string.Empty && strCreate_User.Substring(14) != Element.sysUser_Id)
                {
                    string strUser_Allow = (string)SQLExec.ExecuteReturnValue("SELECT Member_ID_Allow FROM SYSMEMBER   (NOLOCK) WHERE Member_ID = '" + Element.sysUser_Id + "'") + ",";

                    if (!strUser_Allow.Contains("*,")) //Được phép sửa tất cả
                    {
                        if (!strUser_Allow.Contains(strCreate_User.Substring(14) + ","))
                        {
                            Common.MsgCancel("Không xóa được chứng từ do " + strCreate_User + " lập, liên hệ với Admin!");
                            return false;
                        }
                    }
                }
            }

            DataRow drEditPh = DataTool.SQLGetDataRowByID((string)drDmCt["Table_Ph"], "Stt", strStt);

            SqlConnection sqlCon = SQLExec.GetNewSQLConnection();
            SqlCommand sqlCom = sqlCon.CreateCommand();

            SqlTransaction sqlTran = sqlCom.Connection.BeginTransaction("Deleting_Voucher_Tran");

            sqlCom.Transaction = sqlTran;

            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];

            //#region DeleteQueue

            //if (!Voucher.DeleteQueue(sqlCom, drEditPh))
            //{
            //    sqlTran.Rollback();
            //    return false;
            //}

            //#endregion
            #region Save His


            try
            {
                SaveHistoryDelete_All(strStt, strMa_Ct, sqlCom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                sqlTran.Rollback();
                return false;
            }

            #endregion

            #region Delete Chi tiết khuyến mãi         
            if (Common.Inlist(strMa_Ct, "IN"))
            {
                sqlCom.Parameters.Clear();
                sqlCom.CommandText = "EXEC OM_DeleteOM_SalesDics @Stt,@Ma_Dvcs";
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Parameters.AddWithValue("@Stt", strStt);
                sqlCom.Parameters.AddWithValue("@Ma_Dvcs", Element.sysMa_DvCs);
                try
                {
                    sqlCom.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlTran.Rollback();
                    return false;
                }
            }



            #endregion
            #region Delete HanTt
            //Common.SQLPushQueue(sqlCom, "SP_CT_DELETE_HANTT '" + strStt + "'", strStt);
            //Không viết trong Delete_All do: khi F3 chứng từ, chương trình chay Delele_All -> sai

            sqlCom.Parameters.Clear();
            sqlCom.CommandText = "EXEC SP_CT_DELETE_HANTT @Stt";
            sqlCom.CommandType = CommandType.Text;
            sqlCom.Parameters.AddWithValue("@Stt", strStt);
            try
            {
                sqlCom.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                sqlTran.Rollback();
                return false;
            }




            #endregion
            #region Delete HanTt đối với PTT
            if (Common.Inlist(strMa_Ct, "PTT"))
            {

                sqlCom.Parameters.Clear();
                sqlCom.CommandText = "EXEC sp_Ct_Delete_PhieuTT  @Stt";
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Parameters.AddWithValue("@Stt", strStt);
                try
                {
                    sqlCom.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlTran.Rollback();
                    return false;
                }

            }


            #endregion
            #region Delete Ct

            sqlCom.Parameters.Clear();
            sqlCom.CommandText = "DELETE FROM " + strTable_Ct + " WHERE Stt = @Stt";
            sqlCom.CommandType = CommandType.Text;
            sqlCom.Parameters.AddWithValue("@Stt", strStt);

            try
            {
                sqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                sqlTran.Rollback();
                return false;
            }

            //DeleteCtLR
            if (Common.Inlist(strMa_Ct, "LR,TR"))
            {
                sqlCom.Parameters.Clear();
                sqlCom.CommandText = "DELETE FROM INLAPRAP WHERE Stt = @Stt";
                sqlCom.CommandType = CommandType.Text;
                sqlCom.Parameters.AddWithValue("@Stt", strStt);

                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlTran.Rollback();
                    return false;
                }
            }
            #endregion

            #region Delete Ph

            sqlCom.CommandText = "DELETE FROM " + strTable_Ph + " WHERE Stt = @Stt";

            try
            {
                sqlCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                sqlTran.Rollback();
                return false;
            }

            #endregion



            sqlTran.Commit();
            return true;
        }

        //Update_Header, Update_Detail
        public static void Update_Header(frmVoucher_Edit frmEditCt)
        {//Tao cau truc column cho dtEditPh va Copy row du lieu dau tien tu dtEditCt -> drEditPh

            Common.CopyDataColumn(frmEditCt.dtEditCt, frmEditCt.drEditPh.Table, (string)frmEditCt.drDmCt["Update_Header"]);

            if (frmEditCt.enuNew_Edit == enuEdit.Edit || frmEditCt.enuNew_Edit == enuEdit.Copy)
                Common.CopyDataRow(frmEditCt.dtEditCt.Rows[0], frmEditCt.drEditPh, (string)frmEditCt.drDmCt["Update_Header"]);
            else
            {
                Common.CopyDataRow(frmEditCt.dtEditCt.Rows[0], frmEditCt.drEditPh, (string)frmEditCt.drDmCt["Update_Header"]);
                Common.CopyDataRow(frmEditCt.drEdit, frmEditCt.drEditPh, (string)frmEditCt.drDmCt["Carry_Header"]);

                if (frmEditCt.dtEditPh.Columns.Contains("Ma_Dt") && frmEditCt.dtEditCt.Columns.Contains("Ma_Dt"))
                    frmEditCt.dtEditCt.Rows[0]["Ma_Dt"] = frmEditCt.dtEditPh.Rows[0]["Ma_Dt"];

                if (frmEditCt.dtEditPh.Columns.Contains("Dien_Giai") && frmEditCt.dtEditCt.Columns.Contains("Dien_Giai"))
                    frmEditCt.dtEditCt.Rows[0]["Dien_Giai"] = frmEditCt.dtEditPh.Rows[0]["Dien_Giai"];
            }
        }

        public static void Update_Detail(frmVoucher_Edit frmEditCt)
        {// Update du lieu tu drPh xuong dtCt

            string strColumnList = ((string)frmEditCt.drDmCt["Update_Detail"]);

            Update_Detail(frmEditCt, strColumnList);
        }

        public static void Update_Detail(frmVoucher_Edit frmEditCt, string strColumnList)
        {// Update du lieu tu drPh xuong dtCt theo danh sach strColumnList

            strColumnList = strColumnList.Replace(" ", "");
            Common.GatherMemvar(frmEditCt, ref frmEditCt.drEditPh);

            foreach (DataRow dr in frmEditCt.dtEditCt.Rows)
                Common.CopyDataRow(frmEditCt.drEditPh, dr, strColumnList);
        }

        public static bool AddRow(frmVoucher_Edit frmEditCt)
        {
            DataRow drCurrent = ((DataRowView)frmEditCt.bdsEditCt.Current).Row;
            DataTable dtEditCt = (DataTable)frmEditCt.bdsEditCt.DataSource;

            double dbTien = drCurrent["Tien"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien"]);
            double dbTien9 = drCurrent["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_Nt9"]);
            double dbTien3 = dtEditCt.Columns.Contains("Tien3") ? (drCurrent["Tien3"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien3"])) : 0;
            double dbSo_Luong9 = dtEditCt.Columns.Contains("So_Luong9") ? (drCurrent["So_Luong9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["So_Luong9"])) : 0;

            bool bNewRow;

            if (dbTien + dbTien3 + dbTien9 + dbSo_Luong9 == 0)
                bNewRow = false;
            else
                bNewRow = true;

            if (bNewRow)
            {
                DataRow drNew = dtEditCt.NewRow();

                Common.SetDefaultDataRow(ref drNew);
                Common.CopyDataRow(drCurrent, drNew, ((string)frmEditCt.drDmCt["Carry_Detail"]));

                drNew["Stt0"] = Common.MaxDCValue(dtEditCt, "Stt0") + 1;
                drNew["Deleted"] = false;

                if (drNew.Table.Columns.Contains("Auto_Cost"))
                {
                    if ((string)frmEditCt.drDmCt["Nh_Ct"] == "2")
                        drNew["Auto_Cost"] = true;
                    else
                        drNew["Auto_Cost"] = false;
                }

                if (frmEditCt.dtEditPh.Columns.Contains("Ma_Dt") && frmEditCt.dtEditCt.Columns.Contains("Ma_Dt"))
                    drNew["Ma_Dt"] = frmEditCt.dtEditPh.Rows[0]["Ma_Dt"];

                if (frmEditCt.dtEditPh.Columns.Contains("Dien_Giai") && frmEditCt.dtEditCt.Columns.Contains("Dien_Giai"))
                    drNew["Dien_Giai"] = frmEditCt.dtEditPh.Rows[0]["Dien_Giai"];

                dtEditCt.Rows.Add(drNew);

                dtEditCt.AcceptChanges();
            }

            return bNewRow;
        }

        public static void DeleteRow(frmVoucher_Edit frmEditCt, dgvVoucher dgvEditCt)
        {
            if (dgvEditCt.Focused == false)
                return;

            frmEditCt.drCurrent = ((DataRowView)frmEditCt.bdsEditCt.Current).Row;
            frmEditCt.drCurrent["Deleted"] = !((bool)frmEditCt.drCurrent["Deleted"]);

            if ((bool)frmEditCt.drCurrent["Deleted"] == true)
            {
                Font font = new Font(dgvEditCt.Font.FontFamily, dgvEditCt.Font.Size, FontStyle.Strikeout);
                dgvEditCt.CurrentRow.DefaultCellStyle.Font = font;
            }
            else
            {
                dgvEditCt.CurrentRow.DefaultCellStyle.Font = dgvEditCt.Font;
            }

            Update_TTien(frmEditCt);
        }

        //So_Luong
        public static void Calc_So_Luong(DataRow drEditCt)
        {

            if (drEditCt["He_So9"] == DBNull.Value || Convert.ToDouble(drEditCt["He_So9"]) == 0)
                drEditCt["He_So9"] = 1;

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", (string)drEditCt["Ma_Ct"]);
            double dbTienAlowRound = Convert.ToDouble(Parameters.GetParaValue("TRON_THANH_TIEN_BAN"));

            double dHe_So9 = Convert.ToDouble(drEditCt["He_So9"]);
            double dbSo_Luong9 = (drEditCt["So_Luong9"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["So_Luong9"]);
            double dbGia_Nt9 = (drEditCt["Gia_Nt9"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Gia_Nt9"]);
            double dbTien_Nt9 = (drEditCt["Tien_Nt9"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt9"]);
            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);

            //if (dbGia_Nt9 == 0 && dbSo_Luong9 != 0 && dbTien_Nt9 != 0)
            //    dbGia_Nt9 = dbTien_Nt9 / dbSo_Luong9;

            //Kiểm tra tròn tiền Tien_Nt9 = So_Luong9 * Gia_Nt9
            double dbChenh_Lech = (dbTien_Nt9 - dbSo_Luong9 * dbGia_Nt9) * dbTy_Gia;
            dbChenh_Lech = Math.Round(dbChenh_Lech, MidpointRounding.AwayFromZero);

            //------------- Chưa tính trường hợp số tiền quá nhỏ--------

            if (Math.Abs(dbChenh_Lech) > dbTienAlowRound)
            {
                if (dbTien_Nt9 == 0)
                    dbTien_Nt9 = Math.Round(dbSo_Luong9 * dbGia_Nt9, 2, MidpointRounding.AwayFromZero);
                else if (dbGia_Nt9 == 0 && dbSo_Luong9 != 0)
                    dbGia_Nt9 = dbTien_Nt9 / dbSo_Luong9;
                //Khi người dùng sửa lại So_Luong, Gia => Chương trình tính lại Tiền
                else if (Convert.ToDouble(drEditCt["So_Luong9"]) != Convert.ToDouble(drEditCt["So_Luong9", DataRowVersion.Original]) || Convert.ToDouble(drEditCt["Gia_Nt9"]) != Convert.ToDouble(drEditCt["Gia_Nt9", DataRowVersion.Original]))
                {
                    dbTien_Nt9 = Math.Round(dbSo_Luong9 * dbGia_Nt9, 2, MidpointRounding.AwayFromZero);
                }
                //Khi người dùng sửa lại Tiền => Chương trình tính lại Giá
                else if (dbSo_Luong9 != 0 && Convert.ToDouble(drEditCt["Tien_Nt9"]) != Convert.ToDouble(drEditCt["Tien_Nt9", DataRowVersion.Original]))
                {
                    dbGia_Nt9 = dbTien_Nt9 / dbSo_Luong9;
                }
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt9 = Math.Round(dbTien_Nt9, MidpointRounding.AwayFromZero);

            //Cap nhat So_Luong, Gia_Nt, Gia
            double dbSo_Luong = Math.Round(dbSo_Luong9 * dHe_So9, 2, MidpointRounding.AwayFromZero);
            double dbGia_Nt = Math.Round(dbGia_Nt9 / dHe_So9, 4, MidpointRounding.AwayFromZero);

            double dbGia = Math.Round(dbGia_Nt * dbTy_Gia, 2, MidpointRounding.AwayFromZero);

            drEditCt["Tien_Nt9"] = dbTien_Nt9;
            drEditCt["Gia_Nt9"] = dbGia_Nt9;
            drEditCt["So_Luong"] = dbSo_Luong;

            if ((bool)drDmCt["Is_Hd"])// Hóa đơn bán hàng, hàng bán trả lại
            {
                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    drEditCt["Gia_Nt2"] = dbGia;
                else
                    drEditCt["Gia_Nt2"] = dbGia_Nt;

                drEditCt["Gia2"] = dbGia;

                Calc_Tien(drEditCt);
            }
            else
            {
                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    drEditCt["Gia_Nt"] = dbGia;
                else
                    drEditCt["Gia_Nt"] = dbGia_Nt;

                drEditCt["Gia"] = dbGia;

                Calc_Tien(drEditCt);
            }
        }

        public static void Calc_So_Luong_All(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            for (int i = 0; i <= dtEditCt.Rows.Count - 1; i++)
            {
                DataRow drEditCt = dtEditCt.Rows[i];
                if (dtEditCt.Columns.Contains("DELETED") && (bool)drEditCt["DELETED"] == true)
                    continue;

                Voucher.Calc_So_Luong(drEditCt);
            }
        }

        //Tien
        public static void Calc_Tien(DataRow drEditCt)
        {
            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", (string)drEditCt["Ma_Ct"]);

            DataRow drDmThue = null;

            if (drEditCt.Table.Columns.Contains("Ma_Thue") && drEditCt.Table.Columns.Contains("Tien3"))
                drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);

            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);
            double dbTien_Nt9 = drEditCt["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien_Nt9"]);
            double dbTien = (bool)drDmCt["Is_Hd"] ? Convert.ToDouble(drEditCt["Tien2"]) : Convert.ToDouble(drEditCt["Tien"]);
            double dbTien_Nt = dbTien_Nt9;

            //Cho phep dieu chinh trong gioi han dbTron_Tien
            if ((string)drEditCt["Ma_Tte"] != Element.sysMa_Tte)
            {
                double dbTron_Tien = Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia"));

                if (dbTien_Nt != 0)
                    if (Math.Abs(Math.Round(dbTien_Nt * dbTy_Gia - dbTien, 0, MidpointRounding.AwayFromZero)) > dbTron_Tien || dbTien == 0)
                    {
                        if ((bool)drDmCt["Is_Hd"])
                        {
                            drEditCt["Tien2"] = Math.Round(dbTien_Nt * dbTy_Gia, MidpointRounding.AwayFromZero);
                            drEditCt["Tien_Nt2"] = dbTien_Nt;

                            if (drEditCt.Table.Columns.Contains("Tien4"))
                            {

                                drEditCt["Chiet_Khau"] = dbTien_Nt9 == 0 ? 0 : (Convert.ToDouble(drEditCt["Tien4"]) * 100) / dbTien_Nt9;

                                drEditCt["Tien4"] = 0;
                                drEditCt["Tien_Nt4"] = 0;



                            }
                        }
                        else
                        {
                            drEditCt["Tien"] = Math.Round(dbTien_Nt * dbTy_Gia, MidpointRounding.AwayFromZero);
                            drEditCt["Tien_Nt"] = dbTien_Nt;
                        }
                        if (drDmThue != null && drDmThue["Gia_Thue"].ToString() == "1" && (string)drDmCt["Ma_Nvu"] == "V")
                            drEditCt["Tien_Nt3"] = drEditCt["Tien3"] = 0;
                    }
            }
            else //VND
            {
                if ((bool)drDmCt["Is_Hd"])
                {
                    drEditCt["Tien_Nt2"] = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);
                    drEditCt["Tien2"] = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);

                    if (drEditCt.Table.Columns.Contains("Tien4"))
                    {

                        drEditCt["Chiet_Khau"] = dbTien_Nt9 == 0 ? 0 : (Convert.ToDouble(drEditCt["Tien4"]) * 100) / dbTien_Nt9;
                        drEditCt["Tien4"] = 0;
                        drEditCt["Tien_Nt4"] = 0;
                    }
                }
                else
                {
                    drEditCt["Tien_Nt"] = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);
                    drEditCt["Tien"] = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);
                }

                if (drDmThue != null && drDmThue["Gia_Thue"].ToString() == "1" && (string)drDmCt["Ma_Nvu"] == "V")
                    drEditCt["Tien_Nt3"] = drEditCt["Tien3"] = 0;
            }

            drEditCt.AcceptChanges();

            if ((bool)drDmCt["Is_Hd"])
            {
                Voucher.Calc_Chiet_Khau(drEditCt);
                return;
            }

            if (drEditCt.Table.Columns.Contains("Ma_Thue"))
                Voucher.Calc_Thue_Vat(drEditCt);
        }

        public static void Calc_Tien_All(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            for (int i = 0; i <= dtEditCt.Rows.Count - 1; i++)
            {
                DataRow drEditCt = dtEditCt.Rows[i];

                if (dtEditCt.Columns.Contains("DELETED") && (bool)drEditCt["DELETED"] == true)
                    continue;

                Voucher.Calc_Tien(drEditCt);
            }

            Voucher.Adjust_TTien(frmEditCt);

            if ((bool)frmEditCt.drDmCt["Is_Hd"])
                Voucher.Adjust_Chiet_Khau(frmEditCt);

            Voucher.Adjust_TThue_Vat(frmEditCt);

            Voucher.Update_TTien(frmEditCt);
        }

        public static void Adjust_TTien(frmVoucher_Edit frmEditCt)
        {//Tinh lai tien theo tong tien va ty gia, dieu chinh vao dong lon nhat

            string strKeyFilter = "Deleted <> true";
            Voucher.Update_TTien(frmEditCt);

            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataRow drEditPh = frmEditCt.drEditPh;

            double dbTy_Gia = Convert.ToDouble(drEditPh["Ty_Gia"]);
            double dbTTien = Convert.ToDouble(drEditPh["TTien0"]);
            double dbTTien_Nt = Convert.ToDouble(drEditPh["TTien_Nt0"]);

            if (Convert.ToBoolean(frmEditCt.drDmCt["Adjust_TTien_By_Exchange"]) == true)
            {
                double dbChenh_Lech = dbTTien - Math.Round(dbTTien_Nt * dbTy_Gia, MidpointRounding.AwayFromZero);

                if ((bool)frmEditCt.drDmCt["Is_Hd"])
                {
                    if (Math.Abs(dbChenh_Lech) >= Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                    {
                        int iMaxRow = Common.MaxDCPosition(dtEditCt, "Tien2", strKeyFilter);

                        DataRow drMax = dtEditCt.Rows[iMaxRow];
                        drMax["Tien2"] = Convert.ToDouble(drMax["Tien2"]) + Math.Round(dbChenh_Lech, MidpointRounding.AwayFromZero);
                    }
                }
                else
                {
                    if (Math.Abs(dbChenh_Lech) >= Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                    {
                        int iMaxRow = Common.MaxDCPosition(dtEditCt, "Tien", strKeyFilter);

                        DataRow drMax = dtEditCt.Rows[iMaxRow];
                        drMax["Tien"] = Convert.ToDouble(drMax["Tien"]) + Math.Round(dbChenh_Lech, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        public static void Calc_Tien_Von(DataRow drEditCt) //Tính giá vốn trên trường số lượng (không tính trên Quy đổi)
        {
            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", (string)drEditCt["Ma_Ct"]);

            double dbSo_Luong = (drEditCt["So_Luong"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["So_Luong"]);
            double dbGia_Nt = (drEditCt["Gia_Nt"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Gia_Nt"]);
            double dbGia = (drEditCt["Gia"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Gia"]);
            double dbTien_Nt = (drEditCt["Tien_Nt"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt"]);
            double dbTien = (drEditCt["Tien"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien"]);
            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);

            //if (dbGia_Nt == 0 && dbSo_Luong != 0 && dbTien_Nt != 0)
            //    dbGia_Nt = dbTien_Nt / dbSo_Luong;

            //Kiểm tra tròn tiền Tien_Nt = So_Luong * Gia_Nt
            double dbChenh_Lech = (dbTien_Nt - dbSo_Luong * dbGia_Nt) * dbTy_Gia;
            dbChenh_Lech = Math.Round(dbChenh_Lech, MidpointRounding.AwayFromZero);

            if (Math.Abs(dbChenh_Lech) > Convert.ToDouble(Parameters.GetParaValue("TRON_THANH_TIEN")))
            {
                if (dbTien_Nt == 0)
                    dbTien_Nt = Math.Round(dbSo_Luong * dbGia_Nt, 2, MidpointRounding.AwayFromZero);
                else if (dbGia_Nt == 0 && dbSo_Luong != 0)
                    dbGia_Nt = dbTien_Nt / dbSo_Luong;

                //Khi người dùng sửa lại So_Luong, Gia => Chương trình tính lại Tiền
                else if (Convert.ToDouble(drEditCt["So_Luong"]) != Convert.ToDouble(drEditCt["So_Luong", DataRowVersion.Original]) || Convert.ToDouble(drEditCt["Gia_Nt"]) != Convert.ToDouble(drEditCt["Gia_Nt", DataRowVersion.Original]))
                {
                    dbTien_Nt = Math.Round(dbSo_Luong * dbGia_Nt, 2, MidpointRounding.AwayFromZero);
                }
                //Khi người dùng sửa lại Tiền => Chương trình tính lại Giá
                else if (dbSo_Luong != 0 && Convert.ToDouble(drEditCt["Tien_Nt"]) != Convert.ToDouble(drEditCt["Tien_Nt", DataRowVersion.Original]))
                {
                    dbGia_Nt = dbTien_Nt / dbSo_Luong;
                }
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);

            //Cap nhat So_Luong, Gia_Nt, Gia
            dbGia = Math.Round(dbGia_Nt * dbTy_Gia, 2, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbGia_Nt = dbGia;

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien = Math.Round(dbTien_Nt, MidpointRounding.AwayFromZero);
            else
                dbTien = Math.Round(dbTien_Nt * dbTy_Gia, 0, MidpointRounding.AwayFromZero);

            drEditCt["Tien_Nt"] = dbTien_Nt;
            drEditCt["Tien"] = dbTien;
            drEditCt["Gia_Nt"] = dbGia_Nt;
            drEditCt["Gia"] = dbGia;

            drEditCt.AcceptChanges();
        }

        public static void Calc_Tien_Von_All(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            for (int i = 0; i <= dtEditCt.Rows.Count - 1; i++)
            {
                DataRow drEditCt = dtEditCt.Rows[i];
                if (dtEditCt.Columns.Contains("DELETED") && (bool)drEditCt["DELETED"] == true)
                    continue;

                if (dtEditCt.Columns.Contains("AUTO_COST") && (bool)drEditCt["AUTO_COST"] == true)
                    continue;

                Calc_Tien_Von(drEditCt);
            }
        }

        //Thue_VAT
        public static void Calc_Thue_Vat(DataRow drEditCt)
        {
            if (drEditCt["Ma_Thue"].ToString() == string.Empty)
            {
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;

                drEditCt.AcceptChanges();
                return;
            }

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", (string)drEditCt["Ma_Ct"]);
            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);

            if (drDmThue == null || drDmCt == null)
                return;

            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);
            double dbThue_Gtgt = Convert.ToDouble(drEditCt["Thue_Gtgt"]);
            string strMa_Nvu = (string)drDmCt["Ma_Nvu"];

            //Gia co phu phi
            if ((bool)drDmThue["Gia_Pp"])
            {
                double dbTien_Nt3_ = drEditCt["Tien_Nt3"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien_Nt3"]) : 0;
                double dbTien3_ = drEditCt["Tien3"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien3"]) : 0;

                double dbChenh_Lech_Ty_Gia = dbTien3_ - dbTien_Nt3_ * dbTy_Gia;
                if (Math.Abs(dbChenh_Lech_Ty_Gia) > Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                    drEditCt["Tien3"] = Math.Round(dbTien_Nt3_ * dbTy_Gia, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    drEditCt["Tien3"] = drEditCt["Tien_Nt3"];

                //Người dùng gõ tay vào trường Tien3 -> Tính lại Tien_Nt3
                //double dbChenh_Lech_Ty_Gia = dbTien3_ - dbTien_Nt3_ * dbTy_Gia;
                //if (Math.Abs(dbChenh_Lech_Ty_Gia) > Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                //    drEditCt["Tien_Nt3"] = Math.Round(dbTien3_ / dbTy_Gia, 2, MidpointRounding.AwayFromZero);

                //if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte && Element.sysMa_Tte == "VND")
                //    drEditCt["Tien_Nt3"] = drEditCt["Tien3"] = Math.Round(dbTien3_, 0, MidpointRounding.AwayFromZero);

                return;
            }

            double dbTien_Nt = (bool)drDmCt["Is_Hd"] ? Convert.ToDouble(drEditCt["Tien_Nt2"]) : Convert.ToDouble(drEditCt["Tien_Nt"]);
            double dbTien = (bool)drDmCt["Is_Hd"] ? Convert.ToDouble(drEditCt["Tien2"]) : Convert.ToDouble(drEditCt["Tien"]);

            // Cong thue nhap khau vao
            double dbTien_Nt5 = 0, dbTien5 = 0;
            if (drEditCt.Table.Columns.Contains("Tien_Nt5"))
            {
                dbTien_Nt5 = drEditCt["Tien_Nt5"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien_Nt5"]) : 0;
                dbTien5 = drEditCt["Tien5"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien5"]) : 0;

                dbTien_Nt += dbTien_Nt5;
                dbTien += dbTien5;
            }

            // Cong thue tieu thu dac biet vao
            double dbTien_Nt6 = 0, dbTien6 = 0;
            if (drEditCt.Table.Columns.Contains("Tien6"))
            {
                dbTien_Nt6 = drEditCt["Tien_Nt6"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien_Nt6"]) : 0;
                dbTien6 = drEditCt["Tien6"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien6"]) : 0;

                dbTien_Nt += dbTien_Nt6;
                dbTien += dbTien6;
            }

            //double dbTien_Nt3 = drEditCt["Tien_Nt3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
            //double dbTien3 = drEditCt["Tien3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien3"]);

            double dbTien_Nt3 = drEditCt["Tien_Nt3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
            drEditCt["Tien_Nt3"] = dbTien_Nt3 = Math.Round(dbTien_Nt3, 2, MidpointRounding.AwayFromZero);

            double dbTien3 = drEditCt["Tien3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien3"]);
            drEditCt["Tien3"] = dbTien3 = Math.Round(dbTien3, 0, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                drEditCt["Tien3"] = drEditCt["Tien_Nt3"] = dbTien3 = dbTien_Nt3 = Math.Round(dbTien_Nt3, 2, MidpointRounding.AwayFromZero);

            double dbTien_Nt3_Calc = 0;
            double dbTien3_Calc = 0;

            if (drDmThue == null)
                return;

            //Gia da bao gom VAT
            if (drDmThue["Gia_Thue"].ToString() == "1")
            {
                dbTien_Nt += dbTien_Nt3;
                dbTien += dbTien3;

                double dbThue_Suat = Convert.ToDouble(dbThue_Gtgt) / 100;
                dbTien_Nt3_Calc = Math.Round(Math.Ceiling((dbTien_Nt * dbThue_Suat / (1 + dbThue_Suat)) * 100) / 100, 2, MidpointRounding.AwayFromZero);
                dbTien3_Calc = Math.Round(Math.Ceiling((dbTien * dbThue_Suat / (1 + dbThue_Suat)) * 100) / 100, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt3_Calc = Math.Round(dbTien_Nt3_Calc, MidpointRounding.AwayFromZero);

                dbTien_Nt = dbTien_Nt - dbTien_Nt3_Calc;
                dbTien = dbTien - dbTien3_Calc;
            }
            else
            {
                dbTien_Nt3_Calc = Math.Round(Math.Ceiling((dbTien_Nt * dbThue_Gtgt) * 100) / 100 / 100, 2, MidpointRounding.AwayFromZero);
                dbTien3_Calc = Math.Round(Math.Ceiling((dbTien * dbThue_Gtgt / 100) * 100) / 100, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt3_Calc = Math.Round(dbTien_Nt3_Calc, MidpointRounding.AwayFromZero);
            }

            //Cho phep dieu chinh trong gioi han dbTron_Vat
            double dbTron_Vat = Convert.ToDouble(Parameters.GetParaValue("Tron_VAT"));

            if (Math.Abs(dbTien3_Calc - dbTien3) > dbTron_Vat ||
                 Math.Abs(dbTien_Nt3_Calc - dbTien_Nt3) * dbTy_Gia > dbTron_Vat || dbTien3 == 0)
            {
                drEditCt["Tien3"] = dbTien3_Calc;
                drEditCt["Tien_Nt3"] = dbTien_Nt3_Calc;
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                drEditCt["Tien3"] = drEditCt["Tien_Nt3"];

            if (strMa_Nvu == "K")// Nếu là phiếu kế toán
                drEditCt["Tien_Nt9"] = dbTien_Nt;

            if ((bool)drDmCt["Is_Hd"])// Hóa đơn bán hàng
            {
                drEditCt["Tien_Nt2"] = dbTien_Nt;
                drEditCt["Tien2"] = dbTien;
            }
            else
            {
                if (dbTien_Nt5 != 0 || dbTien5 != 0)
                {
                    dbTien_Nt -= dbTien_Nt5;
                    dbTien -= dbTien5;
                }
                if (dbTien_Nt6 != 0 || dbTien6 != 0)
                {
                    dbTien_Nt -= dbTien_Nt6;
                    dbTien -= dbTien6;
                }
                drEditCt["Tien_Nt"] = dbTien_Nt;
                drEditCt["Tien"] = dbTien;
            }

            drEditCt.AcceptChanges();
        }

        public static void Calc_Thue_Vat_All(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            for (int i = 0; i <= frmEditCt.dtEditCt.Rows.Count - 1; i++)
            {
                DataRow drEditCt = frmEditCt.dtEditCt.Rows[i];

                if (dtEditCt.Columns.Contains("DELETED") && (bool)drEditCt["DELETED"] == true)
                    continue;

                Voucher.Calc_Thue_Vat(drEditCt);
            }

            Voucher.Update_TTien(frmEditCt);

            Voucher.Adjust_TThue_Vat(frmEditCt);
        }

        public static void Adjust_TThue_Vat(frmVoucher_Edit frmEditCt)
        {
            Adjust_TThue_Vat(frmEditCt, false);
        }

        public static void Adjust_TThue_Vat(frmVoucher_Edit frmEditCt, bool bTinhLaiThue)
        {//Tinh lai tien thue theo tong tien va thue suat, dieu chinh vao dong lon nhat

            if (!Convert.ToBoolean(frmEditCt.drDmCt["Adjust_TTien_By_VatRate"]) == true)
                return;

            if (!frmEditCt.dtEditCt.Columns.Contains("Tien3"))
                return;

            string strMa_Thue = (string)frmEditCt.dtEditCt.Rows[0]["Ma_Thue"];
            string strMa_Tte = (string)frmEditCt.dtEditCt.Rows[0]["Ma_Tte"];

            int iRound_Nt = strMa_Tte == Element.sysMa_Tte ? 0 : 2;

            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", strMa_Thue);
            if (drDmThue == null)
            {
                foreach (DataRow drEditCt in frmEditCt.dtEditCt.Rows)
                {
                    drEditCt["Thue_GtGt"] = 0;
                    drEditCt["Tien_Nt3"] = 0;
                    drEditCt["Tien3"] = 0;
                }

                Update_TTien(frmEditCt);
                return;
            }

            string strKeyFilter = "Deleted <> true";
            double dbTy_Gia = Convert.ToDouble(frmEditCt.drEditPh["Ty_Gia"]);
            double dbThue_GtGt = Convert.ToDouble(drDmThue["Thue_Suat"]);

            double dbTTien3 = Convert.ToDouble(frmEditCt.drEditPh["TTien3"]);
            double dbTTien_Nt3 = Convert.ToDouble(frmEditCt.drEditPh["TTien_Nt3"]);

            double dbTTien = Common.SumDCValue(frmEditCt.dtEditCt, (bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien2" : "Tien", strKeyFilter);
            double dbTTien_Nt = Common.SumDCValue(frmEditCt.dtEditCt, (bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien_Nt2" : "Tien_Nt", strKeyFilter);

            if (strMa_Tte == Element.sysMa_Tte)
            {
                dbTTien = dbTTien_Nt;
                dbTTien3 = dbTTien_Nt3;
            }

            if (!(bool)drDmThue["Gia_Pp"])// Giá không có phụ phí
            {
                if (!bTinhLaiThue)
                {
                    double dbCheck_Chenh_Lech3 = dbTTien3 - (dbTTien * dbThue_GtGt / 100);
                    double dbCheck_Chenh_Lech_Nt3 = dbTTien_Nt3 - (dbTTien_Nt * dbThue_GtGt / 100);

                    dbCheck_Chenh_Lech3 = Math.Round(dbCheck_Chenh_Lech3, MidpointRounding.AwayFromZero);
                    dbCheck_Chenh_Lech_Nt3 = Math.Round(dbCheck_Chenh_Lech_Nt3, iRound_Nt, MidpointRounding.AwayFromZero);

                    if (Math.Abs(dbCheck_Chenh_Lech3) > Convert.ToDouble(Parameters.GetParaValue("Tron_Vat")) ||
                         Math.Abs(dbCheck_Chenh_Lech_Nt3) * dbTy_Gia > Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                    {//Reset tro ve so ban dau

                        Voucher.Update_TTien(frmEditCt);
                        return;
                    }
                }
                else
                {
                    dbTTien_Nt3 = Math.Round(Math.Ceiling(((dbTTien_Nt * dbThue_GtGt) / 100) * 100) / 100, iRound_Nt, MidpointRounding.AwayFromZero);
                    dbTTien3 = Math.Round(Math.Ceiling(((dbTTien * dbThue_GtGt) / 100) * 100) / 100, MidpointRounding.AwayFromZero);
                }
            }

            if ((bool)drDmThue["Gia_Pp"] && strMa_Tte != Element.sysMa_Tte)
            {
                if (dbTTien3 - Math.Round(dbTTien_Nt3 * dbTy_Gia, MidpointRounding.AwayFromZero) > Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                {
                    Voucher.Update_TTien(frmEditCt);
                    return;
                }
            }

            if (dbTTien == 0)
                dbThue_GtGt = 0;
            else
                dbThue_GtGt = dbTTien3 / dbTTien;

            if (drDmThue["Gia_Thue"].ToString() == "1")
            {//Giá có thuế
                double dbTien_Nt = 0, dbTien = 0;
                double dbTien_Nt3 = 0, dbTien3 = 0;

                foreach (DataRow dr in frmEditCt.dtEditCt.Select(strKeyFilter))
                {
                    dbTien_Nt = Convert.ToDouble(dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien_Nt2" : "Tien_Nt"]);
                    dbTien = Convert.ToDouble(dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien2" : "Tien"]);

                    dbTien_Nt3 = Convert.ToDouble(dr["Tien_Nt3"]);
                    dbTien3 = Convert.ToDouble(dr["Tien3"]);

                    dbTien_Nt += dbTien_Nt3;
                    dbTien += dbTien3;

                    dr["Tien_Nt3"] = dbTien_Nt3 = Math.Round(dbTien_Nt * dbThue_GtGt / (1 + dbThue_GtGt), 2, MidpointRounding.AwayFromZero);
                    dr["Tien3"] = dbTien3 = Math.Round(dbTien * dbThue_GtGt / (1 + dbThue_GtGt), MidpointRounding.AwayFromZero);

                    dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien_Nt2" : "Tien_Nt"] = dbTien_Nt - dbTien_Nt3;
                    dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien2" : "Tien"] = dbTien - dbTien3;
                }
            }
            else
            {//Giá không thuế

                foreach (DataRow dr in frmEditCt.dtEditCt.Select(strKeyFilter))
                {
                    dr["Tien_Nt3"] = Math.Round(Convert.ToDouble(dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien_Nt2" : "Tien_Nt"]) * dbThue_GtGt, iRound_Nt, MidpointRounding.AwayFromZero);
                    dr["Tien3"] = Math.Round(Convert.ToDouble(dr[(bool)frmEditCt.drDmCt["Is_Hd"] ? "Tien2" : "Tien"]) * dbThue_GtGt, MidpointRounding.AwayFromZero);
                }

                double dbTTien3_ = Math.Round(Common.SumDCValue(frmEditCt.dtEditCt, "Tien3", strKeyFilter), MidpointRounding.AwayFromZero);
                double dbTTien_Nt3_ = Math.Round(Common.SumDCValue(frmEditCt.dtEditCt, "Tien_Nt3", strKeyFilter), iRound_Nt, MidpointRounding.AwayFromZero);

                int iMaxRow_ = Common.MaxDCPosition(frmEditCt.dtEditCt, "Tien3", strKeyFilter);
                DataRow drMax_ = frmEditCt.dtEditCt.Rows[iMaxRow_];

                if (dbTTien3 != dbTTien3_ && dbTTien3 != 0)
                    drMax_["Tien3"] = Convert.ToDouble(drMax_["Tien3"]) + (dbTTien3 - dbTTien3_);

                if (dbTTien_Nt3 != dbTTien_Nt3_ && dbTTien_Nt3 != 0)
                    drMax_["Tien_Nt3"] = Convert.ToDouble(drMax_["Tien_Nt3"]) + (dbTTien_Nt3 - dbTTien_Nt3_);
            }

            Voucher.Update_TTien(frmEditCt);
        }

        //Chiet_Khau
        public static void Calc_Chiet_Khau(DataRow drEditCt)
        {
            string strMa_Tte = (string)drEditCt["Ma_Tte"];

            double dbTien_Nt2 = (drEditCt["Tien_Nt2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt2"]);

            double dbTien2_Old = 0;
            double dbTien2 = (drEditCt["Tien2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien2"]);
            if (drEditCt.RowState == DataRowState.Modified)
                dbTien2_Old = (drEditCt["Tien2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien2", DataRowVersion.Original]);
            else
                dbTien2_Old = dbTien2;

            //Nếu gõ lại thành tiền phải tính lại từ đầu
            if (dbTien2_Old != dbTien2)
            {
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;

                drEditCt["Tien_Nt4"] = 0;
                drEditCt["Tien4"] = 0;
            }

            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);
            //Gia da bao gom VAT
            if (drDmThue != null && drDmThue["Gia_Thue"].ToString() == "1")
            {
                double dbTien_Nt3 = (drEditCt["Tien_Nt3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
                double dbTien3 = (drEditCt["Tien3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien3"]);

                dbTien_Nt2 += dbTien_Nt3;
                dbTien2 += dbTien3;

                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;
            }

            double dbTien_Nt4 = (drEditCt["Tien_Nt4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt4"]);
            double dbTien4 = (drEditCt["Tien4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien4"]);
            double dbChiet_Khau = Convert.ToDouble(drEditCt["Chiet_Khau"]);

            dbTien_Nt2 += dbTien_Nt4;
            dbTien2 += dbTien4;

            double dbTien_Nt4_Calc = dbTien_Nt4;
            double dbTien4_Calc = dbTien4;

            dbTien_Nt4_Calc = Math.Round(dbTien_Nt2 * dbChiet_Khau / 100, 2, MidpointRounding.AwayFromZero);
            dbTien4_Calc = Math.Round(dbTien2 * dbChiet_Khau / 100, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt4_Calc = dbTien4_Calc;

            dbTien_Nt2 = dbTien_Nt2 - dbTien_Nt4_Calc;
            dbTien2 = dbTien2 - dbTien4_Calc;

            drEditCt["Tien_Nt4"] = dbTien_Nt4_Calc;
            drEditCt["Tien4"] = dbTien4_Calc;
            drEditCt["Tien_Nt2"] = dbTien_Nt2;
            drEditCt["Tien2"] = dbTien2;

            Calc_Thue_Vat(drEditCt);
        }
        public static void Calc_Chiet_Khau_AP(DataRow drEditCt)
        {
            string strMa_Tte = (string)drEditCt["Ma_Tte"];

            double dbTien_Nt = (drEditCt["Tien_Nt"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt"]);

            double dbTien_Old = 0;
            double dbTien = (drEditCt["Tien"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien"]);
            if (drEditCt.RowState == DataRowState.Modified)
                dbTien_Old = (drEditCt["Tien"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien", DataRowVersion.Original]);
            else
                dbTien_Old = dbTien;

            //Nếu gõ lại thành tiền phải tính lại từ đầu
            if (dbTien_Old != dbTien)
            {
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;

                drEditCt["Tien_Nt4"] = 0;
                drEditCt["Tien4"] = 0;
            }

            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);
            //Gia da bao gom VAT
            if (drDmThue != null && drDmThue["Gia_Thue"].ToString() == "1")
            {
                double dbTien_Nt3 = (drEditCt["Tien_Nt3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
                double dbTien3 = (drEditCt["Tien3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien3"]);

                dbTien_Nt += dbTien_Nt3;
                dbTien += dbTien3;

                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;
            }

            double dbTien_Nt4 = (drEditCt["Tien_Nt4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt4"]);
            double dbTien4 = (drEditCt["Tien4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien4"]);
            double dbChiet_Khau = Convert.ToDouble(drEditCt["Chiet_Khau"]);

            dbTien_Nt += dbTien_Nt4;
            dbTien += dbTien4;

            double dbTien_Nt4_Calc = dbTien_Nt4;
            double dbTien4_Calc = dbTien4;

            dbTien_Nt4_Calc = Math.Round(dbTien_Nt * dbChiet_Khau / 100, 2, MidpointRounding.AwayFromZero);
            dbTien4_Calc = Math.Round(dbTien * dbChiet_Khau / 100, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt4_Calc = dbTien4_Calc;

            dbTien_Nt = dbTien_Nt - dbTien_Nt4_Calc;
            dbTien = dbTien - dbTien4_Calc;

            drEditCt["Tien_Nt4"] = dbTien_Nt4_Calc;
            drEditCt["Tien4"] = dbTien4_Calc;
            drEditCt["Tien_Nt"] = dbTien_Nt;
            drEditCt["Tien"] = dbTien;

            Calc_Thue_Vat(drEditCt);
        }
        public static void Calc_Chiet_Khau_All(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if (dtEditCt.Columns.Contains("Deleted") && (bool)drEditCt["Deleted"] == true)
                    continue;

                Calc_Chiet_Khau(drEditCt);
            }

            Update_TTien(frmEditCt);
        }
        public static void Calc_Chiet_Khau_All_AP(frmVoucher_Edit frmEditCt)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;

            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if (dtEditCt.Columns.Contains("Deleted") && (bool)drEditCt["Deleted"] == true)
                    continue;

                Calc_Chiet_Khau_AP(drEditCt);
            }

            Update_TTien(frmEditCt);
        }
        public static void Adjust_Chiet_Khau(frmVoucher_Edit frmEditCt)
        {
            string strKeyFilter = "Deleted <> true";

            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataRow drEditPh = frmEditCt.drEditPh;

            double dbTy_Gia = Convert.ToDouble(drEditPh["Ty_Gia"]);
            double dbTron_Chiet_Khau = Convert.ToDouble(Parameters.GetParaValue("Tron_Chiet_Khau"));

            double dbTTien_Nt4 = Common.SumDCValue(dtEditCt, "Tien_Nt4", strKeyFilter);
            double dbTTien4 = Common.SumDCValue(dtEditCt, "Tien4", strKeyFilter);

            //drEditPh["TTien_Nt4"] = SUM(dtEditCt["Tien_Nt4"])
            double dbChenh_Lech = Convert.ToDouble(drEditPh["TTien_Nt4"]) - dbTTien_Nt4;
            if (Math.Abs(dbChenh_Lech) * dbTy_Gia <= dbTron_Chiet_Khau)
            {
                int iMax = Common.MaxDCPosition(dtEditCt, "Tien_Nt4", strKeyFilter);
                DataRow drEditCt = dtEditCt.Rows[iMax];

                double dbTien_Nt4 = (drEditCt["Tien_Nt4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt4"]);
                double dbTien_Nt2 = (drEditCt["Tien_Nt2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt2"]);

                drEditCt["Tien_Nt4"] = dbTien_Nt4 + dbChenh_Lech;
                drEditCt["Tien_Nt2"] = dbTien_Nt2 - dbChenh_Lech;
            }

            //drEditPh["TTien4"] = SUM(dtEditCt["Tien4"])
            dbChenh_Lech = Convert.ToDouble(drEditPh["TTien4"]) - dbTTien4;
            if (Math.Abs(dbChenh_Lech) <= dbTron_Chiet_Khau)
            {
                int iMax = Common.MaxDCPosition(dtEditCt, "Tien4", strKeyFilter);
                DataRow drEditCt = dtEditCt.Rows[iMax];

                double dbTien4 = (drEditCt["Tien4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien4"]);
                double dbTien2 = (drEditCt["Tien2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien2"]);

                drEditCt["Tien4"] = dbTien4 + dbChenh_Lech;
                drEditCt["Tien2"] = dbTien2 - dbChenh_Lech;
            }
        }

        //Thue_Nk
        public static void Calc_Thue_Nk(DataRow drEditCt)
        {
            double dbThue_Nk = (drEditCt["Thue_Nk"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Thue_Nk"]);
            double dbThue_Nk_Org = (drEditCt["Thue_Nk", DataRowVersion.Original] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Thue_Nk", DataRowVersion.Original]);

            if (dbThue_Nk == 0 && dbThue_Nk_Org != 0) //Xóa bỏ thuế suất NK
            {
                drEditCt["Thue_Nk"] = 0;
                drEditCt["Tien5"] = 0;
                drEditCt["Tien_Nt5"] = 0;

                drEditCt["Tk_No5"] = string.Empty;
                drEditCt["Tk_Co5"] = string.Empty;

                drEditCt.AcceptChanges();

                Calc_Thue_Vat(drEditCt);
                return;
            }

            double dbTien = (drEditCt["Tien"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien"]);
            double dbTien_Nt5 = (drEditCt["Tien_Nt5"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt5"]);
            double dbTien5 = (drEditCt["Tien5"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien5"]);
            double dbTien5_Cal = 0;
            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);

            if (dbThue_Nk != 0) //Tính Thuế NK theo thuế suất
            {
                dbTien5_Cal = Math.Round(dbTien * dbThue_Nk / 100);
            }
            else //Người dùng gõ tay thuế NK
            {
                dbTien5_Cal = dbTien_Nt5;
            }

            //Điều chỉnh tiền
            if (Math.Abs(Math.Round(dbTien5_Cal - dbTien_Nt5 * dbTy_Gia, 0, MidpointRounding.AwayFromZero)) > Convert.ToDouble(Parameters.GetParaValue("TRON_VAT")))
            {
                dbTien_Nt5 = Math.Round(dbTien5_Cal / dbTy_Gia, 2, MidpointRounding.AwayFromZero);
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt5 = dbTien5_Cal;

            drEditCt["Tien5"] = dbTien5_Cal;
            drEditCt["Tien_Nt5"] = dbTien_Nt5;

            if (dbTien5_Cal == 0)
            {
                drEditCt["Tk_No5"] = string.Empty;
                drEditCt["Tk_Co5"] = string.Empty;
            }
            else
            {
                if (drEditCt["Tk_No5"] == DBNull.Value || (string)drEditCt["Tk_No5"] == string.Empty)
                    drEditCt["Tk_No5"] = drEditCt["Tk_No"];

                if (drEditCt["Tk_Co5"] == DBNull.Value || (string)drEditCt["Tk_Co5"] == string.Empty)
                    drEditCt["Tk_Co5"] = Parameters.GetParaValue("TK_THUE_NK");//Lay trong Syspara                        
            }

            drEditCt.AcceptChanges();

            Calc_Thue_Vat(drEditCt);
        }

        public static void Phan_Bo_Thue_Nk(frmVoucher_Edit frmEditCt, double dbTTien5, string strLoai_Pb)
        {
            string strKeyFilter = "Deleted <> true";

            double dbTSo_Luong = Common.SumDCValue(frmEditCt.dtEditCt, "So_Luong", strKeyFilter);
            double dbTTien = Common.SumDCValue(frmEditCt.dtEditCt, "Tien", strKeyFilter);

            DataRow drEditCt = frmEditCt.dtEditCt.Rows[0];
            double dbTy_Gia = Convert.ToDouble(frmEditCt.drEditPh["Ty_Gia"]);

            DataTable dtEditCt = frmEditCt.dtEditCt;
            for (int i = 0; i <= dtEditCt.Rows.Count - 1; i++)
            {
                double dbTien5 = 0;
                drEditCt = dtEditCt.Rows[i];

                if (dtEditCt.Columns.Contains("Deleted") && (bool)drEditCt["Deleted"] == true)
                    continue;

                if (strLoai_Pb == "1") //Theo gia tri
                {
                    if (dbTTien != 0)
                    {
                        double dbTien = (drEditCt["Tien"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien"]);
                        dbTien5 = Math.Round((dbTien * dbTTien5) / dbTTien, MidpointRounding.AwayFromZero);
                    }
                }
                else // Theo So luong
                {
                    if (dbTSo_Luong != 0)
                    {
                        double dbSo_Luong = (drEditCt["So_Luong"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["So_Luong"]);
                        dbTien5 = Math.Round((dbSo_Luong * dbTTien5) / dbTSo_Luong, MidpointRounding.AwayFromZero);
                    }
                }

                double dbTien_Nt5 = Math.Round(dbTien5 / dbTy_Gia, 2, MidpointRounding.AwayFromZero);

                if ((string)frmEditCt.drEditPh["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt5 = dbTien5;

                drEditCt["Thue_Nk"] = 0;
                drEditCt["Tien_Nt5"] = dbTien_Nt5;
                drEditCt["Tien5"] = dbTien5;

                if (dbTien5 == 0)
                {
                    drEditCt["Tk_No5"] = string.Empty;
                    drEditCt["Tk_Co5"] = string.Empty;
                }
                else
                {
                    if (drEditCt["Tk_No5"] == DBNull.Value || (string)drEditCt["Tk_No5"] == string.Empty)
                        drEditCt["Tk_No5"] = drEditCt["Tk_No"];

                    if (drEditCt["Tk_Co5"] == DBNull.Value || (string)drEditCt["Tk_Co5"] == string.Empty)
                        drEditCt["Tk_Co5"] = Parameters.GetParaValue("TK_THUE_NK");//Lay trong Syspara
                }

                drEditCt.AcceptChanges();
            }

            //Kiểm tra chênh lệch
            double dbTTien5_ = Common.SumDCValue(dtEditCt, "Tien5", strKeyFilter);

            if (dbTTien5_ != dbTTien5)
            {
                int iMax = Common.MaxDCPosition(dtEditCt, "Tien5", strKeyFilter);
                drEditCt = dtEditCt.Rows[iMax];

                double dbTien5 = Convert.ToDouble(drEditCt["Tien5"]);
                dbTien5 = dbTien5 + (dbTTien5 - dbTTien5_);

                drEditCt["Tien5"] = dbTien5;

                if ((string)frmEditCt.drEditPh["Ma_Tte"] == Element.sysMa_Tte)
                    drEditCt["Tien_Nt5"] = dbTien5;
                else
                    drEditCt["Tien_Nt5"] = Math.Round(dbTien5 / dbTy_Gia, 2, MidpointRounding.AwayFromZero);

                drEditCt.AcceptChanges();
            }

            Calc_Thue_Vat_All(frmEditCt);
        }

        //Thue_TTDB
        public static void Calc_Thue_TTDB(DataRow drEditCt)
        {
            double dbThue_TtDb = (drEditCt["Thue_Ttdb"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Thue_Ttdb"]);
            double dbThue_TtDb_Org = (drEditCt["Thue_Ttdb", DataRowVersion.Original] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Thue_Ttdb", DataRowVersion.Original]);

            if (dbThue_TtDb == 0 && dbThue_TtDb_Org != 0) //Xóa bỏ thuế suất TTDB
            {
                drEditCt["Thue_Ttdb"] = 0;
                drEditCt["Tien6"] = 0;
                drEditCt["Tien_Nt6"] = 0;

                drEditCt["Tk_No6"] = string.Empty;
                drEditCt["Tk_Co6"] = string.Empty;

                drEditCt.AcceptChanges();

                Calc_Thue_Vat(drEditCt);
                return;
            }
            double dbTien5 = (drEditCt["Tien5"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien5"]);
            double dbTien = (drEditCt["Tien"] == DBNull.Value) ? 0 : (Convert.ToDouble(drEditCt["Tien"]) + dbTien5);
            dbTien += dbTien5;

            double dbTien_Nt6 = (drEditCt["Tien_Nt6"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt6"]);
            double dbTien6 = (drEditCt["Tien6"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien6"]);
            double dbTien6_Cal = 0;
            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);

            if (dbThue_TtDb != 0) //Tính Thuế TTDB theo thuế suất
            {
                dbTien6_Cal = Math.Round(dbTien * dbThue_TtDb / 100);
            }
            else //Người dùng gõ tay thuế TTDB
            {
                dbTien6_Cal = dbTien6;
            }

            //Điều chỉnh tiền
            if (Math.Abs(Math.Round(dbTien6_Cal - dbTien_Nt6 * dbTy_Gia, 0, MidpointRounding.AwayFromZero)) > Convert.ToDouble(Parameters.GetParaValue("TRON_VAT")))
            {
                dbTien_Nt6 = Math.Round(dbTien6_Cal / dbTy_Gia, 2, MidpointRounding.AwayFromZero);
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt6 = dbTien6_Cal;

            drEditCt["Tien6"] = dbTien6_Cal;
            drEditCt["Tien_Nt6"] = dbTien_Nt6;

            if (dbTien6_Cal == 0)
            {
                drEditCt["Tk_No6"] = string.Empty;
                drEditCt["Tk_Co6"] = string.Empty;
            }
            else
            {
                if (drEditCt["Tk_No6"] == DBNull.Value || (string)drEditCt["Tk_No6"] == string.Empty)
                    drEditCt["Tk_No6"] = drEditCt["Tk_No"];

                if (drEditCt["Tk_Co6"] == DBNull.Value || (string)drEditCt["Tk_Co6"] == string.Empty)
                    drEditCt["Tk_Co6"] = Parameters.GetParaValue("TK_THUE_TTDB");//Lay trong Syspara
            }

            drEditCt.AcceptChanges();

            Calc_Thue_Vat(drEditCt);
        }

        //Tong_Tien
        public static void Update_TTien(frmVoucher_Edit frmEditCt)
        {
            string strKeyFilter = "Deleted <> true";
            DataRow drEditPh = frmEditCt.drEditPh;
            DataTable dtEditCt = frmEditCt.dtEditCt;

            if ((bool)frmEditCt.drDmCt["Is_Hd"])
            {
                drEditPh["TTien_Nt0"] = Common.SumDCValue(dtEditCt, "Tien_Nt2", strKeyFilter);
                drEditPh["TTien0"] = Common.SumDCValue(dtEditCt, "Tien2", strKeyFilter);
            }
            else
            {
                drEditPh["TTien_Nt0"] = Common.SumDCValue(dtEditCt, "Tien_Nt", strKeyFilter);
                drEditPh["TTien0"] = Common.SumDCValue(dtEditCt, "Tien", strKeyFilter);
            }

            drEditPh["TTien_Nt"] = drEditPh["TTien_Nt0"];
            drEditPh["TTien"] = drEditPh["TTien0"];

            if (drEditPh.Table.Columns.Contains("TTien_Nt3") && dtEditCt.Columns.Contains("Tien_Nt3"))
            {
                drEditPh["TTien_Nt3"] = Common.SumDCValue(dtEditCt, "Tien_Nt3", strKeyFilter);
                drEditPh["TTien3"] = Common.SumDCValue(dtEditCt, "Tien3", strKeyFilter);

                drEditPh["TTien_Nt"] = Convert.ToDouble(drEditPh["TTien_Nt"]) + Convert.ToDouble(drEditPh["TTien_Nt3"]);
                drEditPh["TTien"] = Convert.ToDouble(drEditPh["TTien"]) + Convert.ToDouble(drEditPh["TTien3"]);
            }

            if (drEditPh.Table.Columns.Contains("TTien_Nt4") && dtEditCt.Columns.Contains("Tien_Nt4"))
            {
                drEditPh["TTien_Nt4"] = Common.SumDCValue(dtEditCt, "Tien_Nt4", strKeyFilter);
                drEditPh["TTien4"] = Common.SumDCValue(dtEditCt, "Tien4", strKeyFilter);
            }

            if (drEditPh.Table.Columns.Contains("TTien4_HD") && dtEditCt.Columns.Contains("Tien_CKInvoice"))
            {
                drEditPh["TTien4_HD"] = Common.SumDCValue(dtEditCt, "Tien_CKInvoice", strKeyFilter);
            }
            if (drEditPh.Table.Columns.Contains("TTien4_Line") && dtEditCt.Columns.Contains("Tien_CK") && dtEditCt.Columns.Contains("Tien_M4"))
            {
                drEditPh["TTien4_Line"] = Common.SumDCValue(dtEditCt, "Tien_CK", strKeyFilter) + Common.SumDCValue(dtEditCt, "Tien_M4", strKeyFilter);
            }


            if (drEditPh.Table.Columns.Contains("TTien_Nt5") && dtEditCt.Columns.Contains("Tien_Nt5"))
            {
                double dbTTien_Nt5 = Common.SumDCValue(dtEditCt, "Tien_Nt5", strKeyFilter);
                double dbTTien5 = Common.SumDCValue(dtEditCt, "Tien5", strKeyFilter);

                drEditPh["TTien_Nt5"] = dbTTien_Nt5;
                drEditPh["TTien5"] = dbTTien5;

                drEditPh["TTien_Nt"] = Convert.ToDouble(drEditPh["TTien_Nt"]) + dbTTien_Nt5;
                drEditPh["TTien"] = Convert.ToDouble(drEditPh["TTien"]) + dbTTien5;
            }

            if (drEditPh.Table.Columns.Contains("TTien_Nt6") && dtEditCt.Columns.Contains("Tien_Nt6"))
            {
                double dbTTien_Nt6 = Common.SumDCValue(dtEditCt, "Tien_Nt6", strKeyFilter);
                double dbTTien6 = Common.SumDCValue(dtEditCt, "Tien6", strKeyFilter);

                drEditPh["TTien_Nt6"] = dbTTien_Nt6;
                drEditPh["TTien6"] = dbTTien6;

                drEditPh["TTien_Nt"] = Convert.ToDouble(drEditPh["TTien_Nt"]) + dbTTien_Nt6;
                drEditPh["TTien"] = Convert.ToDouble(drEditPh["TTien"]) + dbTTien6;
            }
            if (drEditPh.Table.Columns.Contains("TTien_Nt9") && dtEditCt.Columns.Contains("Tien_Nt9"))
            {
                drEditPh["TTien_Nt9"] = Common.SumDCValue(dtEditCt, "Tien_Nt9", strKeyFilter);
            }


            if (drEditPh.Table.Columns.Contains("TSo_Luong") && dtEditCt.Columns.Contains("So_Luong"))
            {
                drEditPh["TSo_Luong"] = Common.SumDCValue(dtEditCt, "So_Luong", strKeyFilter);
                drEditPh["TSo_Luong"] = Common.SumDCValue(dtEditCt, "So_Luong", strKeyFilter);
            }

            if (drEditPh.Table.Columns.Contains("TSo_Luong9") && dtEditCt.Columns.Contains("So_Luong9"))
            {
                drEditPh["TSo_Luong9"] = Common.SumDCValue(dtEditCt, "So_Luong9", strKeyFilter);
            }

            if (drEditPh.Table.Columns.Contains("TTien_CK_M4") && dtEditCt.Columns.Contains("Tien_CK_M4"))
            {
                drEditPh["TTien_CK_M4"] = Common.SumDCValue(dtEditCt, "Tien_CK_M4", strKeyFilter);
            }
            frmEditCt.drEditPh.EndEdit();
        }

        //Stt, Gia_Vt, dsVoucher
        public static void Update_Stt(frmVoucher_Edit frmEditCt, string strModule)
        {//Kiem tra frmEditCt.strStt co bi trung khong, roi update cho cac table lien quan

            string strTable_Ph = (string)frmEditCt.drDmCt["Table_Ph"];

            if (frmEditCt.enuNew_Edit == enuEdit.New || frmEditCt.enuNew_Edit == enuEdit.Copy)
            {
                //MessageBox.Show("E1");
                while (DataTool.SQLCheckExist(strTable_Ph, "Stt", frmEditCt.strStt))
                {
                    frmEditCt.strStt = Common.GetNewStt(strModule, true);
                }

                if (Common.Inlist(frmEditCt.strMa_Ct, "IN,INT") && frmEditCt.enuNew_Edit == enuEdit.New)
                {
                    string[] Key = new string[4] { "Ma_Dvcs", "Ma_Ct", "So_Ct", "Ngay_Ct" };
                    string[] Value = new string[4] { Element.sysMa_DvCs, frmEditCt.strMa_Ct, frmEditCt.strSo_Ct, Library.DateToStr((DateTime)frmEditCt.drEditPh["Ngay_Ct"]) };
                    //MessageBox.Show("E2");
                    //while (DataTool.SQLCheckExist(strTable_Ph, Key, Value))
                    //{
                    //    frmEditCt.strSo_Ct = TinhSoCt(frmEditCt, (DateTime)frmEditCt.drEditPh["Ngay_Ct"]);
                    //    //Value = new string[4] { Element.sysMa_DvCs, frmEditCt.strMa_Ct, frmEditCt.strSo_Ct, Library.DateToStr((DateTime)frmEditCt.drEditPh["Ngay_Ct"]) };

                    //}
                    //MessageBox.Show("E3");
                    frmEditCt.drEditPh["So_Ct"] = frmEditCt.strSo_Ct;

                    foreach (DataRow drCt in frmEditCt.dtEditCt.Rows)
                        drCt["So_Ct"] = frmEditCt.strSo_Ct;
                }

                frmEditCt.drEditPh["Stt"] = frmEditCt.strStt;

                foreach (DataRow drCt in frmEditCt.dtEditCt.Rows)
                {
                    drCt["Stt"] = frmEditCt.strStt;
                    //drCt["So_Ct"] = frmEditCt.strSo_Ct;
                }
                //MessageBox.Show("E4");
                frmEditCt.drEditPh.Table.AcceptChanges();
                frmEditCt.dtEditCt.AcceptChanges();
            }

            //Cap nhat Stt Thanh toan chung tu
        }
        private static string TinhSoCt(frmVoucher_Edit frmEditCt, DateTime dteNgay_ct)
        {
            if (frmEditCt.enuNew_Edit != enuEdit.New)
                return frmEditCt.strSo_Ct;
            string strMa_Ct = frmEditCt.strMa_Ct;
            string strSo_Ct_New = string.Empty;


            Hashtable htParameter = new Hashtable();
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);
            htParameter.Add("MA_CT", strMa_Ct);
            htParameter.Add("NGAY_CT", Library.DateToStr(dteNgay_ct));

            strSo_Ct_New = SQLExec.ExecuteReturnValue("sp_Cong_So_Ct_New", htParameter, CommandType.StoredProcedure).ToString();

            return strSo_Ct_New;

        }
        public static void Update_dsVoucher(frmVoucher_Edit frmEditCt)
        {
            string strTable_Ph = (string)frmEditCt.drDmCt["Table_Ph"];
            string strTable_Ct = (string)frmEditCt.drDmCt["Table_Ct"];

            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataRow drEditPh = frmEditCt.drEditPh;

            if (frmEditCt.dsVoucher == null)
                return;

            if (!frmEditCt.dsVoucher.Tables.Contains(strTable_Ph) || !frmEditCt.dsVoucher.Tables.Contains(strTable_Ct))
                return;

            DataTable dtViewPh = frmEditCt.dsVoucher.Tables[strTable_Ph];
            DataTable dtViewCt = frmEditCt.dsVoucher.Tables[strTable_Ct];

            if (frmEditCt.enuNew_Edit == enuEdit.New || frmEditCt.enuNew_Edit == enuEdit.Copy)
            {
                //Ph
                DataRow drViewPh_New = dtViewPh.NewRow();

                Common.CopyDataRow(drEditPh, drViewPh_New);

                dtViewPh.Rows.Add(drViewPh_New);
                dtViewPh.AcceptChanges();

                //Ct
                foreach (DataRow drCt in dtEditCt.Rows)
                {
                    if (dtEditCt.Columns.Contains("Deleted") && (bool)drCt["Deleted"] == true)
                        continue;

                    DataRow drViewCt_New = dtViewCt.NewRow();

                    Common.CopyDataRow(drCt, drViewCt_New);

                    dtViewCt.Rows.Add(drViewCt_New);
                }

                dtViewCt.AcceptChanges();
            }
            else
            {
                //Ph: drEdit
                Common.CopyDataRow(drEditPh, frmEditCt.drEdit);
                dtViewPh.AcceptChanges();

                //Ct: Remove những hàng cũ trong dtViewCt
                DataRow[] drArr = dtViewCt.Select("Stt = '" + frmEditCt.strStt + "'");

                foreach (DataRow dr in drArr)
                    dr.Delete();

                foreach (DataRow drCt in dtEditCt.Rows)
                {
                    if (dtEditCt.Columns.Contains("Deleted") && (bool)drCt["Deleted"] == true)
                        continue;

                    DataRow drViewCt_New = dtViewCt.NewRow();

                    Common.CopyDataRow(drCt, drViewCt_New);

                    dtViewCt.Rows.Add(drViewCt_New);
                }

                dtViewCt.AcceptChanges();
            }
        }
        public static bool OM_SaveOM_SalesDics(frmVoucher_Edit frmEditCt)
        {
            if (frmEditCt.dtEditCtDisc != null && frmEditCt.dtEditCtDisc.Rows.Count > 0)
            {
                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_SaveOM_SalesDics";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Stt", frmEditCt.strStt);
                command.Parameters.AddWithValue("@Ma_Ct", frmEditCt.strMa_Ct);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@EditDisc",
                    TypeName = "TVP_DiscAmt",
                    Value = frmEditCt.dtEditCtDisc
                };
                command.Parameters.Add(parameter);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                    return false;
                }

            }
            return true;
        }
        public static void FormatTien_Nt(dgvControl dgv, string strMa_Tte)
        {
            string strTien_Nt = string.Empty;
            for (int i = 0; i <= 9; i++)
            {
                strTien_Nt = i == 0 ? "Tien_Nt" : "Tien_Nt" + i;

                if (dgv.Columns.Contains(strTien_Nt))
                    dgv.Columns[strTien_Nt].DefaultCellStyle.Format = strMa_Tte == Element.sysMa_Tte ? "N0" : "N2";
            }


            if (dgv.Columns.Contains("Gia_Nt9"))
                dgv.Columns["Gia_Nt9"].DefaultCellStyle.Format = strMa_Tte == Element.sysMa_Tte ? "N0" : "N2";

            if (dgv.Columns.Contains("Gia_Nt"))
                dgv.Columns["Gia_Nt"].DefaultCellStyle.Format = strMa_Tte == Element.sysMa_Tte ? "N2" : "N2";

            if (dgv.Columns.Contains("So_Luong9"))
                dgv.Columns["So_Luong9"].DefaultCellStyle.Format = "N0";

            if (dgv.Columns.Contains("So_Luong"))
                dgv.Columns["So_Luong"].DefaultCellStyle.Format = "N2";
        }

        public static string GetTonCuoi(DataRow drEditCt)
        {
            double dbTonCuoi = 0;
            return GetTonCuoi(drEditCt, ref dbTonCuoi);
        }
        public static string GetTonCuoi(DataRow drEditCt, ref double dbTonCuoi)
        {
            Hashtable ht = new Hashtable();

            ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
            if (drEditCt.Table.Columns.Contains("MA_KHO"))
            {
                ht["MA_KHO"] = drEditCt["Ma_Kho"];
            }
            ht["MA_VT"] = drEditCt["Ma_Vt"];
            ht["MA_DVCS"] = drEditCt["Ma_DvCs"];
            ht["STT"] = drEditCt["Stt"];

            DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetTonCuoi", ht, CommandType.StoredProcedure);
            DataRow dr = dt.Rows[0];

            dbTonCuoi = Convert.ToDouble(dr["Ton_Cuoi"]);

            return (string)dr["Ten_Vt"] + ": " + Convert.ToDouble(dr["Ton_Cuoi"]).ToString("N2") + " " + (string)dr["Dvt"];
        }
        public static string GetTonCuoi0(DataRow drEditCt, ref double dbTonCuoi)
        {
            Hashtable ht = new Hashtable();

            ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
            if (drEditCt.Table.Columns.Contains("MA_KHO"))
            {
                ht["MA_KHO"] = drEditCt["Ma_Kho"];
            }
            ht["MA_VT"] = drEditCt["Ma_Vt"];
            ht["MA_DVCS"] = drEditCt["Ma_DvCs"];
            ht["STT"] = drEditCt["Stt"];

            DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetTonCuoi0", ht, CommandType.StoredProcedure);
            DataRow dr = dt.Rows[0];

            dbTonCuoi = Convert.ToDouble(dr["Ton_Cuoi"]);

            return (string)dr["Ten_Vt"] + ": " + Convert.ToDouble(dr["Ton_Cuoi"]).ToString("N2") + " " + (string)dr["Dvt"];
        }
        public static string GetGiaBanLast(DataRow drEditCt)
        {
            double dbGia_Last = 0;
            return GetGiaBanLast(drEditCt, ref dbGia_Last);

        }
        public static string GetGiaBanLast(DataRow drEditCt, ref double dbGiaLast)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht["MA_VT"] = drEditCt["Ma_Vt"];
                ht["MA_DT"] = drEditCt["MA_DT"];
                ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
                ht["DVT"] = drEditCt["Dvt"];
                ht["STT"] = drEditCt["Stt"];
                ht["MA_DVCS"] = drEditCt["Ma_DvCs"];


                DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetGiaBanLast", ht, CommandType.StoredProcedure);
                DataRow dr = dt.Rows[0];

                dbGiaLast = Convert.ToDouble(dr["Gia_Last"]);

                return "-- Giá bán sau cùng :" + dbGiaLast.ToString();
            }
            catch
            { return string.Empty; }
        }
        public static string GetGiaVonBQTT(DataRow drEditCt, ref double dbGiaLast)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht["MA_VT"] = drEditCt["Ma_Vt"];
                ht["MA_DT"] = drEditCt["MA_DT"];
                ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
                ht["DVT"] = drEditCt["Dvt"];
                ht["STT"] = drEditCt["Stt"];
                ht["MA_DVCS"] = drEditCt["Ma_DvCs"];


                DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetGiaBQTT", ht, CommandType.StoredProcedure);
                DataRow dr = dt.Rows[0];

                dbGiaLast = Convert.ToDouble(dr["Gia_BQTT"]);

                return "-- Giá bình quân thời điểm :" + dbGiaLast.ToString();
            }
            catch
            { return string.Empty; }
        }
        public static string GetDuCuoi(DataRow drEditCt, string strTk)
        {
            Hashtable ht = new Hashtable();

            ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
            ht["MA_DVCS"] = drEditCt["Ma_DvCs"];
            ht["STT"] = drEditCt["Stt"];
            ht["TK"] = strTk;
            ht["MA_DT"] = drEditCt.Table.Columns.Contains("Ma_Dt") ? drEditCt["Ma_Dt"] : string.Empty;
            ht["MA_SP"] = drEditCt.Table.Columns.Contains("Ma_Sp") ? drEditCt["Ma_Sp"] : string.Empty;

            DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetDuCuoi", ht, CommandType.StoredProcedure);
            DataRow dr = dt.Rows[0];

            return (string)dr["Ten_Tk"];
        }

        public static double GetDuCuoiDt(DataRow drEditCt, string strTk, string strMa_Dt)
        {
            Hashtable ht = new Hashtable();

            ht["NGAY_CT"] = drEditCt["Ngay_Ct"];
            ht["MA_DVCS"] = drEditCt["Ma_DvCs"];
            ht["STT"] = drEditCt["Stt"];
            ht["TK"] = strTk;
            ht["MA_DT"] = strMa_Dt;

            DataTable dt = SQLExec.ExecuteReturnDt("Sp_GetDuCuoi", ht, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return Convert.ToDouble(dr["Du_No"]);
            }

            return 0;
        }
        public static double GetDuCuoiCtHd(frmVoucher_Edit frm, string sttHD, string sttPt)
        {
            Hashtable ht = new Hashtable();


            ht["MA_DVCS"] = Element.sysMa_DvCs;
            ht["STT_HD"] = sttHD;
            ht["STT_PT"] = sttPt;
            DataTable dt = SQLExec.ExecuteReturnDt("sp_AR_SDHanTt", ht, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return Convert.ToDouble(dr["Tien_No1"]);
            }

            return 0;
        }
        public static bool CheckDataLockedCtHanTt(frmVoucher_Edit frm)
        {
            bool bReturn = (bool)DataTool.SQLCheckExist("GLTHANHTOAN", "Stt", frm.strStt);

            return bReturn;
        }
        public static bool CheckDataLockedCtHanTtHD(string strStt)
        {
            bool bReturn = (bool)DataTool.SQLCheckExist("GLTHANHTOAN", "Stt_HD", strStt);
            return bReturn;
        }

        public static bool CheckDataLockedCtHanTtPXK(string strMa_Px)
        {
            bool bReturn = false;
            Hashtable ht = new Hashtable();
            ht.Add("MA_PX", strMa_Px);
            ht.Add("MA_DVCS", Element.sysMa_DvCs);
            bReturn = (bool)SQLExec.ExecuteReturnValue("Sp_CheckThanhToanPXK", ht, CommandType.StoredProcedure);
            return bReturn;
        }
        public static bool CheckDataLockedPXK(string strStt)
        {
            DataTable dt = SQLExec.ExecuteReturnDt("SELECT STT FROM GLVOUCHER (NOLOCK) WHERE Stt = '" + strStt + "' AND So_Ct_Lap <> ''");
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static void UpdateSo_Ct(frmVoucher_Edit frmEditCt)
        {
            if (frmEditCt.enuNew_Edit == enuEdit.New || frmEditCt.enuNew_Edit == enuEdit.Copy)
            {
                string strTablePh = (string)frmEditCt.drDmCt["Table_Ph"];
                string strMa_Ct = (string)frmEditCt.drEditPh["Ma_Ct"];
                string strSo_Ct = (string)frmEditCt.drEditPh["So_Ct"];
                DateTime dNgay_Ct = (DateTime)frmEditCt.drEditPh["Ngay_Ct"];
                string strLoai_Ma_Ct = dNgay_Ct.Month.ToString().Trim();

                string strSQLExec = "SELECT COUNT(Stt) FROM " + strTablePh + " WHERE Stt <> @Stt AND So_Ct = @So_Ct AND MONTH(Ngay_Ct) = MONTH(@Ngay_Ct) AND YEAR(Ngay_Ct) = @Nam AND Ma_Ct = @Ma_Ct AND Ma_DvCs = @Ma_DvCs";

                Hashtable ht = new Hashtable();
                ht.Add("MA_CT", strMa_Ct);
                ht.Add("SO_CT", strSo_Ct);
                ht.Add("NGAY_CT", dNgay_Ct);
                ht.Add("NAM", dNgay_Ct.Year);
                ht.Add("STT", frmEditCt.drEditPh["Stt"]);
                ht.Add("MA_DVCS", frmEditCt.drEditPh["Ma_DvCs"]);

                if (Convert.ToInt32(SQLExec.ExecuteReturnValue(strSQLExec, ht, CommandType.Text)) > 0)
                {
                    if (Common.MsgYes_No("Chứng từ số: " + strSo_Ct + " Ngày: " + dNgay_Ct.ToShortDateString() + " đã tồn tại.\n Bạn có muốn tự tăng số kô?"))
                    {
                        strSQLExec = "EXEC Sp_Cong_So_Ct '" + strMa_Ct + "', '" + strLoai_Ma_Ct + "'";
                        DataTable dtSo_Ct = SQLExec.ExecuteReturnDt(strSQLExec);

                        if (dtSo_Ct.Rows.Count > 0)
                        {
                            frmEditCt.drEditPh["So_Ct"] = (string)dtSo_Ct.Rows[0][0];
                            foreach (DataRow dr in frmEditCt.dtEditCt.Rows)
                            {
                                dr["So_Ct"] = (string)dtSo_Ct.Rows[0][0];
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckDuplicateInvoice(frmVoucher_Edit frm)
        {
            bool bReturn = true;

            if (!frm.dtEditCt.Columns.Contains("So_Ct0") || !frm.dtEditCt.Columns.Contains("So_Seri0"))
                return true;

            foreach (DataRow dr in frm.dtEditCt.Rows)
            {
                if ((string)dr["So_Ct0"] == string.Empty || (string)dr["So_Seri0"] == string.Empty)
                    continue;

                string strQuery = "SELECT COUNT(So_Ct0) FROM vw_THUEVAT WHERE So_Ct0 = '" + (string)dr["So_Ct0"] + "' AND So_Seri0 = '" + (string)dr["So_Seri0"] + "' AND Stt <> '" + (string)dr["Stt"] + "'";

                int obj = (int)SQLExec.ExecuteReturnValue(strQuery);
                if (obj >= 1)
                {
                    string strMsg = "Số hóa đơn  = {" + (string)dr["So_Ct0"] + "} và số Seri = {" + (string)dr["So_Seri0"] + "} đã tồn tại, bạn có muốn lưu không?";

                    if ((string)frm.drDmCt["Ma_Nvu"] == "K" && Common.MsgYes_No(strMsg, "Y"))
                        continue;
                    else if ((string)frm.drDmCt["Ma_Nvu"] == "V" && Common.MsgYes_No(strMsg, "Y"))
                        return true;
                    else
                        return false;
                    //if (Common.MsgYes_No(strMsg, "Y"))
                    //    continue;
                    //else
                    //    return false;
                }
            }

            return bReturn;
        }
        /*
		//Update Queue
		public static bool UpdateQueue(SqlCommand sqlCom, DataRow drHeader)
		{
			if (drHeader == null)
				return false;

			string strStt = (string)drHeader["Stt"];
			string strMa_Ct = (string)drHeader["Ma_Ct"];
			DateTime dtNgay_Ct = (DateTime)drHeader["Ngay_Ct"];

			if (strStt == string.Empty || strMa_Ct == string.Empty)
				return false;

			string @strSp_Exec = "SP_CT_UPDATE80'" + strStt + "', '" + strMa_Ct + "'";

			//Cập nhật vào bảng hàng đợi, loại trừ dữ liệu trùng
			string strSQLExec =
				" DELETE FROM SYSQUEUE WHERE Sp_Exec = @strSp_Exec AND Stt = @strStt " +
				" INSERT INTO SYSQUEUE(Sp_Exec, Stt, Ngay_Ct) VALUES (@strSp_Exec, @strStt, @dtNgay_Ct) ";

			sqlCom.CommandType = CommandType.Text;
			sqlCom.CommandText = strSQLExec;

			sqlCom.Parameters.Clear();
			sqlCom.Parameters.AddWithValue("@strSp_Exec", strSp_Exec);
			sqlCom.Parameters.AddWithValue("@strStt", strStt);
			sqlCom.Parameters.AddWithValue("@dtNgay_Ct", dtNgay_Ct);

			try
			{
				sqlCom.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
				return false;
			}

			return true;
		}

		public static bool DeleteQueue(SqlCommand sqlCom, DataRow drHeader)
		{
			if (drHeader == null)
				return false;

			string strStt = (string)drHeader["Stt"];
			string strMa_Ct = (string)drHeader["Ma_Ct"];
			DateTime dtNgay_Ct = (DateTime)drHeader["Ngay_Ct"];

			string @strSp_Exec = "SP_CT_DELETE80'" + strStt + "', '" + strMa_Ct + "'";

			//Cập nhật vào bảng hàng đợi, loại trừ dữ liệu trùng
			string strSQLExec =
				" DELETE FROM SYSQUEUE WHERE Stt = @strStt " +
				" INSERT INTO SYSQUEUE(Sp_Exec, Stt, Ngay_Ct) VALUES (@strSp_Exec, @strStt, @dtNgay_Ct) ";

			sqlCom.CommandType = CommandType.Text;
			sqlCom.CommandText = strSQLExec;

			sqlCom.Parameters.Clear();
			sqlCom.Parameters.AddWithValue("@strSp_Exec", strSp_Exec);
			sqlCom.Parameters.AddWithValue("@strStt", strStt);
			sqlCom.Parameters.AddWithValue("@dtNgay_Ct", dtNgay_Ct);

			try
			{
				sqlCom.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
				return false;
			}
			return true;
		}
        */
        public static string GetInheritVoucher(frmVoucher_Edit frmEditCt)
        {
            //Hiển thị chứng từ gốc kế thừa
            string strSQLExec = @"
				DECLARE @_InheritList VARCHAR(1000)
				SET @_InheritList = ''
				SELECT @_InheritList = @_InheritList + Ma_Ct + ':' + So_Ct + ','
					FROM GLVOUCHER   (NOLOCK)
					WHERE Stt IN (SELECT Stt_Org FROM " + (string)frmEditCt.drDmCt["Table_Ct"] + @" WHERE Stt = '" + frmEditCt.strStt + @"')

				SELECT @_InheritList";

            return SQLExec.ExecuteReturnValue(strSQLExec).ToString();
        }

        public static void ImportCtExcel(frmVoucher_Edit frmEditCt)
        {
            if (!Common.MsgYes_No("Bạn muốn import dữ liệu hay kết xuất file mẫu dữ liệu Import?"))
            {
                string strPath = Common.GetBufferValue("ImportExcelPath");

                if (strPath == string.Empty)
                    strPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                strPath += "EpointVoucherTemplate.xls";

                if (!System.IO.File.Exists(strPath))
                    System.IO.File.WriteAllBytes(strPath, Modules.Properties.Resources.EpointVoucherTemplate);

                System.Diagnostics.Process.Start(strPath);

                return;
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
                ofd.RestoreDirectory = true;

                if (Common.GetBufferValue("ImportExcelPath") != string.Empty)
                    ofd.InitialDirectory = Common.GetBufferValue("ImportExcelPath");
                else
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Common.SetBufferValue("ImportExcelPath", System.IO.Path.GetDirectoryName(ofd.FileName));

                    frmEditCt.dtEditCt.Clear();

                    //string strConnectString =
                    //    "Driver={Microsoft Excel Driver (*.xls, *.xlsx)};DBQ=" + ofd.FileName;

                    //string strConnectString =
                    //    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ofd.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX = 2\"";

                    string strConnectString =
                        "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + ofd.FileName;

                    OdbcConnection odbcConn = new OdbcConnection(strConnectString);
                    odbcConn.Open();

                    OdbcCommand odbcComm = new OdbcCommand();
                    odbcComm.Connection = odbcConn;

                    try
                    {
                        OdbcDataAdapter odbcDA;
                        DataTable dtTestColumn = new DataTable();
                        DataTable dtImport = new DataTable();

                        string strMa_Vt_List = "";

                        //Kiểm tra tồn tại cột dữ liệu
                        odbcComm.CommandText = "SELECT * FROM [Sheet1$] WHERE 0 = 1";
                        odbcDA = new OdbcDataAdapter(odbcComm);
                        odbcDA.Fill(dtTestColumn);
                        //Kiểm tra xong

                        odbcComm.CommandText =
                             "SELECT * " +
                                  " FROM [Sheet1$] " +
                                  " WHERE Ma_Vt <> ''";

                        odbcDA = new OdbcDataAdapter(odbcComm);
                        odbcDA.Fill(dtImport);
                        odbcConn.Close();

                        if (dtImport != null)
                        {
                            int iStt0 = 0;

                            foreach (DataRow drImport in dtImport.Rows)
                            {
                                iStt0++;

                                DataRow drNew = frmEditCt.dtEditCt.NewRow();

                                DataTool.SetDefaultDataRow(ref drNew);

                                drNew["Stt"] = frmEditCt.strStt;
                                drNew["Stt0"] = iStt0;
                                drNew["Ma_Vt"] = drImport["Ma_Vt"];
                                drNew["So_Luong9"] = drImport["So_Luong"];
                                drNew["Gia_Nt9"] = drImport["Gia"];
                                drNew["Tien_Nt9"] = drImport["Tien"];

                                drNew["He_So9"] = 1;
                                drNew["So_Luong"] = drImport["So_Luong"];
                                drNew["Gia"] = drImport["Gia"];
                                drNew["Tien"] = drImport["TIen"];

                                if (drImport.Table.Columns.Contains("Ma_Kho"))
                                    drNew["Ma_Kho"] = drImport["Ma_Kho"];

                                if (drImport.Table.Columns.Contains("Ten_Vt"))
                                    drNew["Ten_Vt"] = drImport["Ten_Vt"];

                                if (drNew.Table.Columns.Contains("Auto_Cost"))
                                {
                                    if ((string)frmEditCt.drDmCt["Nh_Ct"] == "2")
                                        drNew["Auto_Cost"] = true;
                                    else
                                        drNew["Auto_Cost"] = false;
                                }

                                DataRow drDmVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", drImport["Ma_Vt"].ToString());

                                if (drDmVt != null)
                                {
                                    drNew["Ten_Vt"] = drImport["Ten_Vt"];
                                    drNew["Dvt"] = drDmVt["Dvt"];

                                    //Xác định Tk_No, Tk_Co
                                    if ((bool)frmEditCt.drDmCt["Is_Hd"])
                                    {
                                        if ((string)frmEditCt.drDmCt["Nh_Ct"] == "1") //HBTL
                                        {
                                            drNew["Tk_No2"] = drDmVt["Tk_HBTL"];
                                            drNew["Tk_No"] = drDmVt["Tk_Vt"];
                                            drNew["Tk_Co"] = drDmVt["Tk_Gv"];
                                        }
                                        else
                                        {
                                            drNew["Tk_Co2"] = drDmVt["Tk_Dt"];
                                            drNew["Tk_No"] = drDmVt["Tk_Gv"];
                                            drNew["Tk_Co"] = drDmVt["Tk_Vt"];
                                            drNew["Tk_No2"] = (string)frmEditCt.drDmCt["Tk_No"];
                                        }

                                    }
                                    else
                                    {
                                        if ((string)frmEditCt.drDmCt["Nh_Ct"] == "1")
                                            drNew["Tk_No"] = drDmVt["Tk_Vt"];
                                        else
                                            drNew["Tk_Co"] = drDmVt["Tk_Vt"];
                                    }
                                }

                                frmEditCt.dtEditCt.Rows.Add(drNew);
                                drNew.AcceptChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không mở được bảng dữ liệu " + ofd.FileName + ex.Message);
                    }

                    Voucher.Update_Detail(frmEditCt);
                    Voucher.Calc_So_Luong_All(frmEditCt);
                    Voucher.Update_TTien(frmEditCt);
                }
            }
        }

        public static string Cong_So_Ct(frmVoucher_Edit frmEdit)
        {
            //DateTime dteNgay_Ct1 = Library.StrToDate("1/1/" + ((DateTime)frmEdit.drEditPh["Ngay_Ct"]).Year.ToString());
            //DateTime dteNgay_Ct2 = dteNgay_Ct1.AddYears(1).AddDays(-1);

            DateTime dteNgay_Ct1 = ((DateTime)frmEdit.drEditPh["Ngay_Ct"]);
            Hashtable htPara = new Hashtable();
            htPara.Add("TABLENAME", "GLVOUCHER");
            htPara.Add("COLUMNNAME", "So_Ct");
            htPara.Add("CURRENTID", frmEdit.drEdit["So_Ct"].ToString());
            htPara.Add("KEY", "Ma_DvCs = '" + Element.sysMa_DvCs + "' AND Ma_Ct = '" + frmEdit.strMa_Ct + "' AND MONTH(Ngay_Ct) = " + dteNgay_Ct1.Month + " AND YEAR(Ngay_Ct) = " + dteNgay_Ct1.Year + "");
            htPara.Add("PREFIXLEN", Convert.ToInt32(frmEdit.drDmCt["PrefixLen"]));
            htPara.Add("SUFFIXLEN", Convert.ToInt32(frmEdit.drDmCt["SuffixLen"]));

            return (string)SQLExec.ExecuteReturnValue("sp_GetNewID", htPara, CommandType.StoredProcedure);
        }
        public static string Cong_So_Ct0(frmVoucher_Edit frmEdit)
        {
            //DateTime dteNgay_Ct1 = Library.StrToDate("1/1/" + ((DateTime)frmEdit.drEditPh["Ngay_Ct"]).Year.ToString());
            //DateTime dteNgay_Ct2 = dteNgay_Ct1.AddYears(1).AddDays(-1);
            DateTime dteNgay_Ct1 = ((DateTime)frmEdit.drEditPh["Ngay_Ct"]);
            Hashtable htPara = new Hashtable();
            htPara.Add("TABLENAME", "ARBAN");
            htPara.Add("COLUMNNAME", "So_Ct0");
            htPara.Add("CURRENTID", frmEdit.drEdit["So_Ct0"].ToString());
            htPara.Add("KEY", "Ma_DvCs = '" + Element.sysMa_DvCs + "' AND So_Seri0 ='" + frmEdit.drEdit["So_Seri0"].ToString() + "'");
            //htPara.Add("PREFIXLEN", Convert.ToInt32(frmEdit.drDmCt["PrefixLen"]));
            //htPara.Add("SUFFIXLEN", Convert.ToInt32(frmEdit.drDmCt["SuffixLen"]));
            htPara.Add("PREFIXLEN", "");
            htPara.Add("SUFFIXLEN", "");

            return (string)SQLExec.ExecuteReturnValue("sp_GetNewID", htPara, CommandType.StoredProcedure);
        }
        public static string Cong_So_Ct_Years(frmVoucher_Edit frmEdit)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("TABLENAME", frmEdit.drDmCt["Table_Ph"].ToString());
            htPara.Add("COLUMNNAME", "So_Ct");
            htPara.Add("MA_CT", frmEdit.strMa_Ct);
            htPara.Add("NGAY_CT", frmEdit.drEditPh["Ngay_Ct"]);
            htPara.Add("CURRENTID", frmEdit.drEdit["So_Ct"].ToString() == string.Empty ? "000000" : frmEdit.drEdit["So_Ct"].ToString());
            htPara.Add("KEY", "Ma_DvCs = '" + Element.sysMa_DvCs + "' AND Ma_Ct = '" + frmEdit.strMa_Ct + "' AND YEAR(Ngay_Ct) = " + ((DateTime)frmEdit.drEditPh["Ngay_Ct"]).Year + "");
            htPara.Add("PREFIXLEN", 0);//Convert.ToInt32(frmEdit.drDmCt["PrefixLen"])
            htPara.Add("SUFFIXLEN", 6);//Convert.ToInt32(frmEdit.drDmCt["SuffixLen"])

            return (string)SQLExec.ExecuteReturnValue("sp_GetNewID_Years", htPara, CommandType.StoredProcedure);
        }
        public static void UpdateSo_Ct_Years(frmVoucher_Edit frmEditCt)
        {
            if (frmEditCt.enuNew_Edit == enuEdit.New || frmEditCt.enuNew_Edit == enuEdit.Copy)
            {
                string strTablePh = (string)frmEditCt.drDmCt["Table_Ph"];
                string strMa_Ct = (string)frmEditCt.drEditPh["Ma_Ct"];
                string strSo_Ct = (string)frmEditCt.drEditPh["So_Ct"];
                int iYear = ((DateTime)frmEditCt.drEditPh["Ngay_Ct"]).Year;

                string strSQLExec = "SELECT COUNT(Stt) FROM " + strTablePh + " WITH(NOLOCK) WHERE Stt <> @Stt AND So_Ct = @So_Ct AND YEAR(Ngay_Ct) = @Year AND Ma_Ct = @Ma_Ct AND Ma_DvCs = @Ma_DvCs";

                Hashtable ht = new Hashtable();
                ht.Add("MA_CT", strMa_Ct);
                ht.Add("SO_CT", strSo_Ct);
                ht.Add("YEAR", iYear);
                ht.Add("STT", frmEditCt.drEditPh["Stt"]);
                ht.Add("MA_DVCS", frmEditCt.drEditPh["Ma_DvCs"]);

                if (Convert.ToInt32(SQLExec.ExecuteReturnValue(strSQLExec, ht, CommandType.Text)) > 0)
                {
                    frmEditCt.drEditPh["So_Ct"] = Voucher.Cong_So_Ct_Years(frmEditCt);

                    foreach (DataRow dr in frmEditCt.dtEditCt.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted)
                            continue;

                        dr["So_Ct"] = frmEditCt.drEditPh["So_Ct"];
                    }
                }
                else
                {
                    foreach (DataRow dr in frmEditCt.dtEditCt.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted)
                            continue;

                        dr["So_Ct"] = frmEditCt.drEditPh["So_Ct"];
                    }
                }
            }
        }
        public static void CopyNewRow(frmVoucher_Edit frmEditCt)
        {
            dgvControl view = (frmEditCt.ActiveControl.GetType() == typeof(dgvVoucher)) ? ((dgvControl)frmEditCt.ActiveControl) : null;
            int num = (view != null) ? view.CurrentCell.ColumnIndex : 0;
            DataRow drSource = ((DataRowView)frmEditCt.bdsEditCt.Current).Row;
            DataTable dataSource = (DataTable)frmEditCt.bdsEditCt.DataSource;
            DataRow dr = dataSource.NewRow();
            Common.SetDefaultDataRow(ref dr);
            Common.CopyDataRow(drSource, dr);
            dr["Stt0"] = Common.MaxDCValue(dataSource, "Stt0") + 1.0;
            dr["Deleted"] = false;
            if (dr.Table.Columns.Contains("So_Luong"))
            {
                dr["So_Luong"] = 0;
            }
            if (dr.Table.Columns.Contains("So_Luong9"))
            {
                dr["So_Luong9"] = 0;
            }
            if (dr.Table.Columns.Contains("Gia"))
            {
                dr["Gia"] = 0;
            }
            if (dr.Table.Columns.Contains("Gia_Nt"))
            {
                dr["Gia_Nt"] = 0;
            }
            if (dr.Table.Columns.Contains("Gia_Nt9"))
            {
                dr["Gia_Nt9"] = 0;
            }
            if (dr.Table.Columns.Contains("Tien"))
            {
                dr["Tien"] = 0;
            }
            if (dr.Table.Columns.Contains("Tien_Nt"))
            {
                dr["Tien_Nt"] = 0;
            }
            if (dr.Table.Columns.Contains("Tien_Nt9"))
            {
                dr["Tien_Nt9"] = 0;
            }
            dataSource.Rows.Add(dr);
            dr.AcceptChanges();
            frmEditCt.bdsEditCt.MoveLast();
            if (view != null)
            {
                view.CurrentRow.Cells[num].Selected = true;
            }
        }



        #region Hạn thanh toán
        public static void HanTt(frmVoucher_Edit frmEditCt)
        {
            new frmHanTt().Load(frmEditCt);
            HanTt_LockCt(frmEditCt);
        }

        public static void HanTt_LockCt(frmVoucher_Edit frmEditCt)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            DataGridView viewCt1 = null;
            DataGridView viewCt2 = null;

            object[] objArray = frmEditCt.Controls.Find("dgvEditCt1", true);
            object[] objArray2 = frmEditCt.Controls.Find("dgvEditCt2", true);
            if (objArray.Length > 0)
            {
                viewCt1 = (DataGridView)objArray[0];
            }
            if (objArray2.Length > 0)
            {
                viewCt2 = (DataGridView)objArray2[0];
            }
            foreach (DataGridViewRow row in (IEnumerable)viewCt1.Rows)
            {
                DataTable table;
                DataTable table2;
                DataRow row2 = ((DataRowView)row.DataBoundItem).Row;
                flag = flag2 = false;
                string strTk_No = ((bool)frmEditCt.drDmCt["Is_HD"]) ? row2["Tk_No2"].ToString() : row2["Tk_No"].ToString();
                string strTK_Co = ((bool)frmEditCt.drDmCt["Is_HD"]) ? row2["Tk_Co2"].ToString() : row2["Tk_Co"].ToString();
                string strMa_Dt = row2["Ma_Dt"].ToString();
                string strMa_Dt_Co = (row2.Table.Columns.Contains("Ma_Dt_Co") && (row2["Ma_Dt_Co"].ToString() != "")) ? row2["Ma_Dt_Co"].ToString() : strMa_Dt;
                if ((frmEditCt.dtHanTt0 != null) && (frmEditCt.dtHanTt0.Select("Tk = '" + strTk_No + "' AND Ma_Dt = '" + strMa_Dt + "'").Length > 0))
                {
                    table = frmEditCt.dtHanTt0;
                }
                else
                {
                    table = SQLExec.ExecuteReturnDt("SELECT Tk, Ma_Dt, ISNULL(SUM(Tien_Tt), 0) AS Tien_Tt1, ISNULL(SUM(Tien_Tt_Nt), 0) AS Tien_Tt_Nt1 FROM R80CtHanTt WHERE (Stt_PT = '" + frmEditCt.strStt + "' OR Stt_HD = '" + frmEditCt.strStt + "') AND (Tk = '" + strTk_No + "') AND (Ma_Dt = '" + strMa_Dt + "') GROUP BY Tk, Ma_Dt");
                }
                if (table.Select("(Tien_Tt1 + Tien_Tt_Nt1 <> 0) AND (Tk = '" + strTk_No + "') AND (Ma_Dt = '" + strMa_Dt + "')").Length > 0)
                {
                    flag = true;
                }
                if ((frmEditCt.dtHanTt0 != null) && (frmEditCt.dtHanTt0.Select("Tk = '" + strTK_Co + "' AND Ma_Dt = '" + strMa_Dt_Co + "'").Length > 0))
                {
                    table2 = frmEditCt.dtHanTt0;
                }
                else
                {
                    table2 = SQLExec.ExecuteReturnDt("SELECT Tk, Ma_Dt, ISNULL(SUM(Tien_Tt), 0) AS Tien_Tt1, ISNULL(SUM(Tien_Tt_Nt), 0) AS Tien_Tt_Nt1 FROM R80CtHanTt WHERE (Stt_PT = '" + frmEditCt.strStt + "' OR Stt_HD = '" + frmEditCt.strStt + "') AND (Tk = '" + strTK_Co + "') AND (Ma_Dt = '" + strMa_Dt_Co + "') GROUP BY Tk, Ma_Dt");
                }
                if (table2.Select("(Tien_Tt1 + Tien_Tt_Nt1 <> 0) AND (Tk = '" + strTK_Co + "') AND (Ma_Dt = '" + strMa_Dt_Co + "')").Length > 0)
                {
                    flag2 = true;
                }
                if (flag || flag2)
                {
                    flag3 = true;
                }
                if ((bool)frmEditCt.drDmCt["Is_HD"])
                {
                    if (row.DataGridView.Columns.Contains("Tk_No2"))
                    {
                        row.Cells["Tk_No2"].ReadOnly = flag;
                        row.Cells["Tk_No2"].Style.ForeColor = flag ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                }
                else if (row.DataGridView.Columns.Contains("Tk_No"))
                {
                    row.Cells["Tk_No"].ReadOnly = flag;
                    row.Cells["Tk_No"].Style.ForeColor = flag ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if ((bool)frmEditCt.drDmCt["Is_HD"])
                {
                    if (row.DataGridView.Columns.Contains("Tk_Co2"))
                    {
                        row.Cells["Tk_Co2"].ReadOnly = flag2;
                        row.Cells["Tk_Co2"].Style.ForeColor = flag2 ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                }
                else if (row.DataGridView.Columns.Contains("Tk_Co"))
                {
                    row.Cells["Tk_Co"].ReadOnly = flag2;
                    row.Cells["Tk_Co"].Style.ForeColor = flag2 ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if (row.DataGridView.Columns.Contains("Ma_Dt_Co"))
                {
                    row.Cells["Ma_Dt_Co"].ReadOnly = flag2;
                    row.Cells["Ma_Dt_Co"].Style.ForeColor = flag2 ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if (row.DataGridView.Columns.Contains("Ma_Dt"))
                {
                    row.Cells["Ma_Dt"].ReadOnly = flag || flag2;
                    row.Cells["Ma_Dt"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if ((bool)frmEditCt.drDmCt["Is_HD"])
                {
                    if (row.DataGridView.Columns.Contains("Tien2"))
                    {
                        row.Cells["Tien2"].ReadOnly = flag || flag2;
                        row.Cells["Tien2"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                    if (row.DataGridView.Columns.Contains("Tien_Nt2"))
                    {
                        row.Cells["Tien_Nt2"].ReadOnly = flag || flag2;
                        row.Cells["Tien_Nt2"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                }
                else
                {
                    if (row.DataGridView.Columns.Contains("Tien"))
                    {
                        row.Cells["Tien"].ReadOnly = flag || flag2;
                        row.Cells["Tien"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                    if (row.DataGridView.Columns.Contains("Tien_Nt"))
                    {
                        row.Cells["Tien_Nt"].ReadOnly = flag || flag2;
                        row.Cells["Tien_Nt"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                    }
                }
                if (row.DataGridView.Columns.Contains("So_Luong9"))
                {
                    row.Cells["So_Luong9"].ReadOnly = flag || flag2;
                    row.Cells["So_Luong9"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if (row.DataGridView.Columns.Contains("Gia_Nt9"))
                {
                    row.Cells["Gia_Nt9"].ReadOnly = flag || flag2;
                    row.Cells["Gia_Nt9"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                }
                if (row.DataGridView.Columns.Contains("Tien_Nt9"))
                {
                    row.Cells["Tien_Nt9"].ReadOnly = flag || flag2;
                    row.Cells["Tien_Nt9"].Style.ForeColor = (flag || flag2) ? SystemColors.GrayText : SystemColors.ControlText;
                }
            }
            if (frmEditCt.Controls.ContainsKey("dteNgay_Ct"))
            {
                frmEditCt.Controls["dteNgay_Ct"].Enabled = !flag3;
            }
            if (frmEditCt.Controls.ContainsKey("txtMa_Tte"))
            {
                frmEditCt.Controls["txtMa_Tte"].Enabled = !flag3;
            }
            if (frmEditCt.Controls.ContainsKey("numTy_Gia"))
            {
                frmEditCt.Controls["numTy_Gia"].Enabled = !flag3;
            }
            if (frmEditCt.Controls.ContainsKey("txtMa_Dt"))
            {
                frmEditCt.Controls["txtMa_Dt"].Enabled = !flag3;
            }
            if (frmEditCt.Controls.ContainsKey("btHanTt"))
            {
                frmEditCt.Controls["btHanTt"].ForeColor = flag3 ? Color.Red : Color.Blue;
            }
        }

        //TVP
        //public static DataTable GetTVPValue(string strTableName, string strTableTypeName, DataTable dtTableSource)
        //{
        //    string strColumnLst = (string)SQLExec.ExecuteReturnValue("\r\n\t\t\t\t\tDECLARE @_ColList VARCHAR(1000)\r\n\t\t\t\t\tSELECT @_ColList =  CASE WHEN @_ColList IS NULL THEN '' ELSE @_ColList + ',' END + Name \r\n\t\t\t\t\t\t\tFROM sys.columns \r\n\t\t\t\t\t\t\tWHERE object_id IN (SELECT Type_Table_object_id FROM sys.table_types where name = '" + strTableTypeName + "') \r\n\t\t\t\t\t\t\tORDER BY column_id\r\n\t\t\t\t\tSELECT @_ColList");
        //    DataTable dtTVPStructure = DataTool.SQLGetDataTable(strTableName, strColumnLst, "0=1", "");
        //    if (dtTableSource != null)
        //    {
        //        foreach (DataRow row in dtTableSource.Rows)
        //        {
        //            if ((row.RowState != DataRowState.Deleted) && (!row.Table.Columns.Contains("Deleted") || !((bool)row["Deleted"])))
        //            {
        //                DataRow dr = dtTVPStructure.NewRow();
        //                DataTool.SetDefaultDataRow(ref dr);
        //                Common.CopyDataRow(row, dr);
        //                dtTVPStructure.Rows.Add(dr);
        //            }
        //        }
        //    }
        //    return dtTVPStructure;
        //}
        public static DataTable GetTVPValue(string strTableName, string strTableTypeName, DataTable dtTableSource)
        {
            //Tạo cấu trúc bảng 
            string strSQLExec = @"
					DECLARE @_ColList VARCHAR(4000)
					SELECT @_ColList =  CASE WHEN @_ColList IS NULL THEN '' ELSE @_ColList + ',' END + Name 
							FROM sys.columns 
							WHERE object_id IN (SELECT Type_Table_object_id FROM sys.table_types where name = '" + strTableTypeName + @"') 
							ORDER BY column_id
					SELECT @_ColList";

            string strColList = (string)SQLExec.ExecuteReturnValue(strSQLExec);
            DataTable dtTVPStructure;

            if (strTableName == strTableTypeName) //Trường hợp Tên bảng === trùng với tên của TVP => Lấy cấu trúc trùng cấu trúc TVP
                dtTVPStructure = SQLExec.ExecuteReturnDt("DECLARE @Table AS " + strTableTypeName + " SELECT * FROM @Table");
            else //Lấy cấu trúc từ Table
                dtTVPStructure = DataTool.SQLGetDataTable(strTableName, strColList, "0=1", ""); //Lấy cấu trúc bảng từ Bàng nguồn theo cấu trúc TableType

            //Copy dữ liệu vào bảng tham số
            if (dtTableSource != null)
            {
                foreach (DataRow drSource in dtTableSource.Rows)
                {
                    if (drSource.Table.Columns.Contains("Deleted") && (bool)drSource["Deleted"])
                        continue;

                    DataRow drNew = dtTVPStructure.NewRow();
                    DataTool.SetDefaultDataRow(ref drNew);

                    Common.CopyDataRow(drSource, drNew);
                    dtTVPStructure.Rows.Add(drNew);
                }
            }

            return dtTVPStructure;
        }

        public static DataTable GetTVPValue(string strTableTypeName, DataTable dtTableSource)
        {
            //Tạo cấu trúc bảng 
            DataTable dtTVPStructure;

            dtTVPStructure = SQLExec.ExecuteReturnDt("DECLARE @Table AS " + strTableTypeName + " SELECT * FROM @Table");

            //Copy dữ liệu vào bảng tham số
            if (dtTableSource != null)
            {
                foreach (DataRow drSource in dtTableSource.Rows)
                {
                    if (drSource.Table.Columns.Contains("Deleted") && (bool)drSource["Deleted"])
                        continue;

                    DataRow drNew = dtTVPStructure.NewRow();
                    DataTool.SetDefaultDataRow(ref drNew);

                    Common.CopyDataRow(drSource, drNew);
                    dtTVPStructure.Rows.Add(drNew);
                }
            }

            return dtTVPStructure;
        }




        public static bool Check_Stt_PXK(string sSTT)
        {
            Hashtable htPara = new Hashtable();
            htPara["STT"] = sSTT;
            htPara["MA_DVCS"] = Element.sysMa_DvCs;

            return Convert.ToBoolean(SQLExec.ExecuteReturnValue("sp_Check_PXKDetail", htPara, CommandType.StoredProcedure));

        }

        #endregion

    }
}
