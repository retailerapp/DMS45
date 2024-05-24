using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;

using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Lists;
using Epoint.Modules;



namespace Epoint.Modules.AR
{
    public class Discount
    {

        public static void CopyDataTable(DataTable dtSource, DataTable dtDest, string strFilter)
        {
            DataRow[] drArr;
            if (strFilter == string.Empty || strFilter == null)
                drArr = dtSource.Select();
            else
                drArr = dtSource.Select(strFilter);

            foreach (DataRow dr in drArr)
            {
                DataRow drDest = dtDest.NewRow();
                Common.CopyDataRow(dr, drDest);
                dtDest.Rows.Add(drDest);
            }

        }
        public static DataTable GetDiscoutProg(DateTime FromDate, string strMa_Dt, string strMa_Vt_Disc_List)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("NGAY_CT", Library.DateToStr(FromDate));
            htPara.Add("MA_DT", strMa_Dt);
            htPara.Add("ITEMID_LIST", strMa_Vt_Disc_List);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);
            return SQLExec.ExecuteReturnDt("OM_GetDiscCountProg", htPara, CommandType.StoredProcedure);

        }
        public static DataTable GetDiscoutProg(DateTime FromDate, string strMa_Dt)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("NGAY_CT", Library.DateToStr(FromDate));
            htPara.Add("MA_DT", strMa_Dt);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);
            return SQLExec.ExecuteReturnDt("sp_GetDiscCountProg", htPara, CommandType.StoredProcedure);

        }
        public static DataTable GetAutoPromotion(DateTime FromDate, string strMa_Dt, DataTable dtARBan)
        {
            DataTable dtOMSaleItem = SQLExec.ExecuteReturnDt("DECLARE @OMSaleItem AS TVP_OMSaleItem SELECT * FROM @OMSaleItem");
            CopyDataTable(dtARBan, dtOMSaleItem, "Hang_Km = 0");

            DataTable dtBreak = new DataTable();
            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "OM_GetAutoPromotion";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NGAY_CT", FromDate);
            command.Parameters.AddWithValue("@MA_DT", strMa_Dt);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@SaleItem",
                TypeName = "TVP_OMSaleItem",
                Value = dtOMSaleItem
            };
            command.Parameters.Add(parameter);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dtBreak);
                return dtBreak;
            }
            catch (Exception exception)
            {
                command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
            }

            return dtBreak;

        }
        public static DataSet GetALLDiscoutProg(string FromDate, string ToDate)
        {

            Hashtable htDisc = new Hashtable();
            htDisc["NGAY_CT1"] = FromDate;
            htDisc["NGAY_CT2"] = ToDate;
            htDisc.Add("MA_DVCS", Element.sysMa_DvCs);
            return SQLExec.ExecuteReturnDs("sp_GetDiscCountProgAll", htDisc, CommandType.StoredProcedure);

        }
        public static DataSet GetDiscoutProgManual(string FromDate, string ToDate)
        {

            Hashtable htDisc = new Hashtable();
            htDisc["NGAY_CT1"] = FromDate;
            htDisc["NGAY_CT2"] = ToDate;
            htDisc.Add("MA_DVCS", Element.sysMa_DvCs);
            return SQLExec.ExecuteReturnDs("sp_GetDiscCountProgManual", htDisc, CommandType.StoredProcedure);

        }
        public static Boolean CheckDtOnProgID(string Ma_CTKM, string Ma_Dt)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("MA_CTKM", Ma_CTKM);
            htPara.Add("MA_DT", Ma_Dt);
            DataTable dt = SQLExec.ExecuteReturnDt("SP_OM_CheckCustInGroup", htPara, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
                if ((bool)(dt.Rows[0]["Value"]))
                {
                    return true;
                }
            return false;

        }
        public static DataTable GetSaleGroupItem(string strMa_CtKm)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("MA_CTKM", strMa_CtKm);

            return SQLExec.ExecuteReturnDt("OM_GetSaleGroupItem", htPara, CommandType.StoredProcedure);

        }

        public static DataTable GetDiscBreak(string strMa_CtKm, string strMa_Vt_Disc, double dbAmt)
        {
            Hashtable htParaBreaby = new Hashtable();
            htParaBreaby["MA_CTKM"] = strMa_CtKm;
            htParaBreaby["MA_VT"] = strMa_Vt_Disc;
            htParaBreaby["QTYAMT"] = dbAmt; // Q: Qty, A : Amount
            return SQLExec.ExecuteReturnDt("OM_GetDiscBreak", htParaBreaby, CommandType.StoredProcedure);

        }

        //public static DataTable GetDiscBreakForBudget(string strMa_CtKm, string strMa_Vt_Disc, double dbAmt)
        //{
        //    Hashtable htParaBreaby = new Hashtable();
        //    htParaBreaby["MA_CTKM"] = strMa_CtKm;
        //    htParaBreaby["MA_VT"] = strMa_Vt_Disc;
        //    htParaBreaby["QTYAMT"] = dbAmt; // Q: Qty, A : Amount
        //    return SQLExec.ExecuteReturnDt("OM_GetDiscBreak", htParaBreaby, CommandType.StoredProcedure);

        //}

        public static DataTable GetDiscBreakInvoice(string strMa_CtKm, double dbAmt)
        {
            Hashtable htParaBreaby = new Hashtable();
            htParaBreaby["MA_CTKM"] = strMa_CtKm;
            htParaBreaby["QTYAMT"] = dbAmt; // Q: Qty, A : Amount
            htParaBreaby["MA_DVCS"] = Element.sysMa_DvCs;
            return SQLExec.ExecuteReturnDt("OM_GetDiscBreakInvoice", htParaBreaby, CommandType.StoredProcedure);

        }
        public static DataTable GetDiscBreakInvoice(string strMa_CtKm, DataTable dtItemSale, double dbAmt)
        {
            DataTable dtBreak = new DataTable();
            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "OM_GetDiscBreakInvoice";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_CTKM", strMa_CtKm);
            command.Parameters.AddWithValue("@QTYAMT", dbAmt);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@SaleItem",
                TypeName = "TVP_SaleItem",
                Value = dtItemSale
            };
            command.Parameters.Add(parameter);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dtBreak);
                return dtBreak;
            }
            catch (Exception exception)
            {
                command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
            }

            return dtBreak;

        }
        public static DataTable GetDiscBreakBundle(string strMa_CtKm, DataTable dtItemSale)
        {
            DataTable dtBreak = new DataTable();
            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "OM_Disc_GetBreakBundle";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_CTKM", strMa_CtKm);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@SaleItem",
                TypeName = "TVP_SaleItem",
                Value = dtItemSale
            };
            command.Parameters.Add(parameter);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dtBreak);
                return dtBreak;
            }
            catch (Exception exception)
            {
                command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
            }

            return dtBreak;

        }
        public static DataTable GetDiscountDetail(string strMa_Ct, string strStt)
        {
            Hashtable htParaBreaby = new Hashtable();
            htParaBreaby["MA_CT"] = strMa_Ct;
            htParaBreaby["STT"] = strStt; // Q: Qty, A : Amount
            return SQLExec.ExecuteReturnDt("OM_GetDiscountDetail", htParaBreaby, CommandType.StoredProcedure);

        }
        public static void Calc_Chiet_Khau_ForLine(frmVoucher_Edit frmEditCt, double dbAmtPercent, string strMa_Vt, string strMa_CtKm, string strStt_Km, bool isEditKm)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;


            double dbTien = 0, dbTien4_Old = 0, dbTien4 = 0, dbTien_Line4 = 0, dbTien_Ck = 0;

            foreach (DataRow drEditCt in dtEditCt.Select("Ma_Vt = '" + strMa_Vt + "' AND Hang_Km = 0"))
            {
                dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);
                dbTien4_Old = Convert.ToDouble(drEditCt["Tien4"]);
                dbTien_Line4 = Math.Round(dbTien * dbAmtPercent / 100);
                dbTien_Ck = dbTien_Line4 + Convert.ToDouble(drEditCt["Tien_Ck"]);
                dbTien4 = dbTien_Ck + Convert.ToDouble(drEditCt["Tien_M4"]) + Convert.ToDouble(drEditCt["Tien_Ck_M4"]);

                dbTien = dbTien - dbTien4;
                //dbTien_Ck += dbTien_Line4; // tiền chiết khấu cộng dồn.
                drEditCt["Tien_Ck"] = dbTien_Ck;

                drEditCt["Chiet_Khau"] = Math.Round((dbTien_Line4 / dbTien) * 100, 7);
                drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                drEditCt["Tien3"] = 0;
                drEditCt["Tien_Nt3"] = 0;

                drEditCt["Ma_So"] = strStt_Km;

                if (drEditCt["Ma_CtKm"].ToString() == string.Empty)
                    drEditCt["Ma_CtKm"] = strMa_CtKm;
                else
                    drEditCt["Ma_CtKm1"] = strMa_CtKm;
                drEditCt["Is_EditDisc"] = isEditKm;

                //Add vào bảng disc

                DataRow drDisc = dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drEditCt["Stt0"];
                drDisc["Ma_CTKM"] = strMa_CtKm;
                drDisc["Stt_Km"] = strStt_Km;
                drDisc["Tien4_Org"] = dbTien_Line4;
                drDisc["Tien4"] = dbTien_Line4;
                drDisc["Ma_Ns"] = string.Empty;
                dtEditCtDisc.Rows.Add(drDisc);
                drEditCt.AcceptChanges();
            }
            dtEditCt.AcceptChanges();
            dtEditCtDisc.AcceptChanges();
        }
        public static void Calc_Chiet_Khau_ForLine(frmVoucher_Edit frmEditCt, double dbAmtPercent, string strMa_Vt, string strMa_CtKm, string strStt_Km, bool isEditKm,string strMa_Ns, ref double dbAmtAlloc)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;


            double dbTien = 0, dbTien4_Old = 0, dbTien4 = 0, dbTien_Line4 = 0, dbTien_Line4_Org = 0, dbTien_Ck = 0;

            foreach (DataRow drEditCt in dtEditCt.Select("Ma_Vt = '" + strMa_Vt + "' AND Hang_Km = 0"))
            {
                dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);
                dbTien4_Old = Convert.ToDouble(drEditCt["Tien4"]);

                dbTien_Line4_Org = Math.Round(dbTien * dbAmtPercent / 100);

                if (dbAmtAlloc >= 0)
                    dbTien_Line4 = dbAmtAlloc <= dbTien_Line4_Org ? dbAmtAlloc : dbTien_Line4_Org;
                else
                    dbTien_Line4 = dbTien_Line4_Org;

                dbTien_Ck = dbTien_Line4 + Convert.ToDouble(drEditCt["Tien_Ck"]);
                dbTien4 = dbTien_Ck + Convert.ToDouble(drEditCt["Tien_M4"]) + Convert.ToDouble(drEditCt["Tien_Ck_M4"]);

                dbTien = dbTien - dbTien4;
                //dbTien_Ck += dbTien_Line4; // tiền chiết khấu cộng dồn.
                drEditCt["Tien_Ck"] = dbTien_Ck;

                drEditCt["Chiet_Khau"] = Math.Round((dbTien_Line4 / dbTien) * 100, 7);
                drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                drEditCt["Tien3"] = 0;
                drEditCt["Tien_Nt3"] = 0;

                drEditCt["Ma_So"] = strStt_Km;

                if (drEditCt["Ma_CtKm"].ToString() == string.Empty)
                    drEditCt["Ma_CtKm"] = strMa_CtKm;
                else
                    drEditCt["Ma_CtKm1"] = strMa_CtKm;
                drEditCt["Is_EditDisc"] = isEditKm;

                //Add vào bảng disc



                if (dbAmtAlloc > 0)//Cập nhật lại ngân sách sau khi add km
                    dbAmtAlloc = dbAmtAlloc - dbTien_Line4;

                DataRow drDisc = dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drEditCt["Stt0"];
                drDisc["Ma_CTKM"] = strMa_CtKm;
                drDisc["Stt_Km"] = strStt_Km;
                drDisc["Tien4_Org"] = dbTien_Line4_Org;
                drDisc["Tien4"] = dbTien_Line4;
                drDisc["Ma_Ns"] = strMa_Ns;
                dtEditCtDisc.Rows.Add(drDisc);
                drEditCt.AcceptChanges();
            }
            dtEditCt.AcceptChanges();
            dtEditCtDisc.AcceptChanges();
        }
        public static void Calc_Chiet_Khau_ForGroup(frmVoucher_Edit frmEditCt, double dbAmtPercent, string Ma_Vt_List, string strMa_CtKm, string strStt_Km, bool isEditKm, string strMa_Ns, ref double dbAmtAlloc)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;
            double dbTien = 0, dbTien4 = 0, dbTien4_N = 0, dbTien4_Org_N =0, dbTien_Ck = 0, dbTien_Line4 = 0;
            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if (Common.Inlist(drEditCt["Ma_Vt"].ToString(), Ma_Vt_List)) // Những dòng được chiết khấu
                {
                    dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);
                    dbTien4 = Convert.ToDouble(drEditCt["Tien4"]);
                    dbTien4_Org_N = Math.Round(dbTien * dbAmtPercent / 100);//Tiên áp dụng
                    if (dbAmtAlloc >= 0)
                        dbTien4_N = dbAmtAlloc <= dbTien4_Org_N ? dbAmtAlloc : dbTien4_Org_N; // nếu ns > ck thì lấy ck
                    else
                        dbTien4_N = dbTien4_Org_N;

                    dbTien_Ck = Convert.ToDouble(drEditCt["Tien_Ck"]);         
                    dbTien4 += dbTien4_N;
                    dbTien = dbTien - dbTien4;
                    drEditCt["Chiet_Khau"] = Math.Round((dbTien4 / dbTien) * 100, 7);

                    dbTien_Ck += dbTien4; // tiền chiết khấu cộng dồn.
                    drEditCt["Tien_Ck"] = dbTien_Ck;

                    drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                    drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                    drEditCt["Tien3"] = 0;
                    drEditCt["Tien_Nt3"] = 0;

                    drEditCt["Ma_So"] = strStt_Km;

                    if (drEditCt["Ma_CTKM_Group"].ToString() == string.Empty)
                        drEditCt["Ma_CTKM_Group"] = strMa_CtKm;
                    else
                        drEditCt["Ma_CTKM_Group1"] = strMa_CtKm;
                    drEditCt["Is_EditDisc"] = isEditKm;


                    DataRow drDisc = dtEditCtDisc.NewRow();
                    drDisc["Stt0"] = drEditCt["Stt0"];
                    drDisc["Ma_CTKM"] = strMa_CtKm;
                    drDisc["Stt_Km"] = strStt_Km;
                    drDisc["Tien4_Org"] = dbTien4_Org_N;
                    drDisc["Tien4"] = dbTien4_N;
                    drDisc["Ma_Ns"] = strMa_Ns;
                    dtEditCtDisc.Rows.Add(drDisc);
                }
                drEditCt.AcceptChanges();

            }
            dtEditCt.AcceptChanges();
        }

        public static void Calc_Chiet_Khau_ForInvoice(frmVoucher_Edit frmEditCt, double dbAmtPercent, string strMa_CtKm, string strStt_Km, bool isEditKm,string strMa_Ns, ref double dbAmtAlloc)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;
            double dbTien = 0, dbTien4 = 0, dbTien4_N = 0, dbTien4_Org_N = 0, dbTien_CkInvoice = 0, dbTien_CkInvoice_OLD = 0; ;
            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if ((bool)drEditCt["Hang_Km"]) // Đối với hàng KM nhập tay
                    continue;
                dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);
                dbTien4 = Convert.ToDouble(drEditCt["Tien4"]);
                dbTien4_Org_N = Math.Round(dbTien * dbAmtPercent / 100);
                if (dbAmtAlloc >= 0)
                    dbTien4_N = dbAmtAlloc <= dbTien4_Org_N ? dbAmtAlloc : dbTien4_Org_N; // nếu ns > ck thì lấy ck


                dbTien4 = dbTien4_N + Convert.ToDouble(drEditCt["Tien_M4"]) + Convert.ToDouble(drEditCt["Tien_Ck_M4"]);
                dbTien_CkInvoice_OLD = Convert.ToDouble(drEditCt["Tien_CkInvoice"]);

                dbTien = dbTien - dbTien4;
                dbTien_CkInvoice = dbTien_CkInvoice_OLD + dbTien4_N; // tiền chiết khấu invoice cộng dồn.
                drEditCt["Tien_CkInvoice"] = dbTien_CkInvoice;

                drEditCt["Chiet_Khau"] = Math.Round((dbTien4 / dbTien) * 100, 7);
                drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                drEditCt["Tien3"] = 0;
                drEditCt["Tien_Nt3"] = 0;

                if (drEditCt["Ma_CTKM_Group"].ToString() == string.Empty)
                    drEditCt["Ma_CTKM_Group"] = strMa_CtKm;
                else
                    drEditCt["Ma_CTKM_Group1"] = strMa_CtKm;

                drEditCt["Is_EditDisc"] = isEditKm;

                DataRow drDisc = dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drEditCt["Stt0"];
                drDisc["Ma_CTKM"] = strMa_CtKm;
                drDisc["Stt_Km"] = strStt_Km;
                drDisc["Tien4_Org"] = dbTien4_N;
                drDisc["Tien4"] = dbTien4_N;
                drDisc["Ma_Ns"] = strMa_Ns;
                dtEditCtDisc.Rows.Add(drDisc);

                drEditCt.AcceptChanges();
            }
            dtEditCt.AcceptChanges();
        }
        public static void Calc_Chiet_Khau_ForInvoice(frmVoucher_Edit frmEditCt, double dbAmtPercent)
        {
            DataTable dtEditCt = frmEditCt.dtEditCt;
            DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;
            double dbTien = 0, dbTien4 = 0, dbTien4_N = 0, dbTien_CkInvoice = 0;
            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if ((bool)drEditCt["Hang_Km"]) // Đối với hàng KM nhập tay
                    continue;
                dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);
                dbTien4 = Convert.ToDouble(drEditCt["Tien4"]);
                dbTien_CkInvoice = Math.Round(dbTien * dbAmtPercent / 100);
                dbTien4 = dbTien_CkInvoice + Convert.ToDouble(drEditCt["Tien_M4"]);
                //dbTien_CkInvoice = Convert.ToDouble(drEditCt["Tien_CkInvoice"]);

                dbTien = dbTien - dbTien4;
                //dbTien_CkInvoice += dbTien4; // tiền chiết khấu cộng dồn.
                drEditCt["Tien_CkInvoice"] = dbTien_CkInvoice;

                drEditCt["Chiet_Khau"] = Math.Round((dbTien4 / dbTien) * 100, 7);
                drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                drEditCt["Tien3"] = 0;
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Ma_So"] = "CKHOADON";             
                drEditCt.AcceptChanges();
            }
            dtEditCt.AcceptChanges();
        }

        public static void Calc_Chiet_Khau_ForInvoice(frmVoucher_Edit frmEditCt, double dbTotalAmt, bool isManual)
        {

            string Ma_Ct_CKHD = "CKHOADON_M";
            double dbTien = 0, dbTien4 = 0, dbTien4_Old = 0, dbTien_CkInvoice = 0, dbTien_CkInvoice_Old = 0, dbTien_Ck_M4 = 0;
            double dbTTien = Convert.ToDouble(Common.SumDCValue(frmEditCt.dtEditCt, "Tien_Nt9", ""));
            double dbAmtPercent = Math.Round((dbTotalAmt / dbTTien) * 100, 7);
            DataTable dtEditCt = frmEditCt.dtEditCt;
            frmEditCt.dtEditCtDisc = Common.FilterDatatable(frmEditCt.dtEditCtDisc, "MA_CTKM <> '" + Ma_Ct_CKHD + "'");


            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if ((bool)drEditCt["Hang_Km"]) // Đối với hàng KM nhập tay
                    continue;
                dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);

                if (dbTien == 0) // Đối với hàng KM nhập tay
                    continue;

                dbTien4_Old = Convert.ToDouble(drEditCt["Tien4"]);
                dbTien4 = Convert.ToDouble(drEditCt["Tien4"]);
                dbTien_Ck_M4 = Math.Round(dbTien * dbAmtPercent / 100); /// Tien nhap tay duoi detail
                dbTien_CkInvoice_Old = Convert.ToDouble(drEditCt["Tien_CkInvoice"]);
                dbTien_CkInvoice = Convert.ToDouble(drEditCt["Tien_CkInvoice"]) - dbTien_CkInvoice_Old + dbTien_Ck_M4;
                dbTien4 = Convert.ToDouble(drEditCt["Tien_Ck"]) + dbTien_CkInvoice + Convert.ToDouble(drEditCt["Tien_M4"]);
                dbTien = dbTien - dbTien4;
                //dbTien_CkInvoice += dbTien4; // tiền chiết khấu cộng dồn.

                drEditCt["Tien_Ck_M4"] = dbTien_Ck_M4; // Tien nhap tay Invoice
                drEditCt["Tien_CkInvoice"] = dbTien_CkInvoice;
                //drEditCt["Ma_CtCk1"] = CKHOADON
                drEditCt["Chiet_Khau"] = Convert.ToDouble( Math.Round((dbTien4 / dbTien) * 100, 7));
                drEditCt["Tien2"] = drEditCt["Tien_Nt2"] = dbTien;
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = dbTien4;

                drEditCt["Tien3"] = 0;
                drEditCt["Tien_Nt3"] = 0;
                //drEditCt["Ma_So"] = Ma_Ct_CKHD;
                if (drEditCt["MAU_SO"].ToString() == string.Empty)
                    drEditCt["MAU_SO"] = Ma_Ct_CKHD;
                //else
                //    drEditCt["Ma_CTKM_Group1"] = "CKHD";

                //drEditCt["Is_EditDisc"] = isEditKm;               

                DataRow drDisc = frmEditCt.dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drEditCt["Stt0"];

                drDisc["Ma_CTKM"] = Ma_Ct_CKHD;
                drDisc["Stt_Km"] = "CKHO001";
                drDisc["Tien4_Org"] = dbTien_Ck_M4;
                drDisc["Tien4"] = dbTien_Ck_M4;
                drDisc["Ma_Ns"] = Ma_Ct_CKHD;
                frmEditCt.dtEditCtDisc.Rows.Add(drDisc);

                drEditCt.AcceptChanges();
            }

            double dbAmt_InvoiceM = Common.SumDCValue(dtEditCt, "Tien_Ck_M4", "");
            double dbChenh_Lech = dbTotalAmt - dbAmt_InvoiceM;
            if (dbChenh_Lech != 0)
            {
                foreach (DataRow drEditCtCheck in dtEditCt.Select("Tien_Ck_M4 > 0"))
                {
                    drEditCtCheck["Tien_Ck_M4"] = Convert.ToDouble(drEditCtCheck["Tien_Ck_M4"]) + dbChenh_Lech;
                    drEditCtCheck["Tien_CkInvoice"] = Convert.ToDouble(drEditCtCheck["Tien_CkInvoice"]) + dbChenh_Lech;
                    drEditCtCheck["Tien4"] = drEditCtCheck["Tien_Nt4"] = Convert.ToDouble(drEditCtCheck["Tien4"]) + dbChenh_Lech;
                    drEditCtCheck["Tien2"] = drEditCtCheck["Tien_Nt2"] = Convert.ToDouble(drEditCtCheck["Tien_Nt9"]) - Convert.ToDouble(drEditCtCheck["Tien4"]);
                    Voucher.Calc_Tien(drEditCtCheck);

                    foreach (DataRow drEditDisc in frmEditCt.dtEditCtDisc.Select("Ma_CTKM = '" + Ma_Ct_CKHD + "' AND Stt_Km = 'CKHO001' AND Stt0 = '" + drEditCtCheck["Stt0"] + "'"))
                    {
                        drEditDisc["Tien4"] = drEditDisc["Tien4_Org"] = Convert.ToDouble(drEditDisc["Tien4"]) + dbChenh_Lech;
                        break;
                    }
                    //frmEditCt.dtEditCtDisc.Select("Ma_CTKM = 'Ma_Ct_CKHD' AND Stt_Km = 'CKHO001' AND Stt0 = '" + drEditCtCheck["Stt0"] + "'")[0]["Tien4"] = drEditCtCheck["Tien_Ck_M4"];
                    break;
                }
            }



            dtEditCt.AcceptChanges();
        }

        public static void Calc_Chiet_Khau_ForManual(frmVoucher_Edit frmEditCt, double dbAmt)
        {

            double dbTTien = Convert.ToDouble(Common.SumDCValue(frmEditCt.dtEditCt, "Tien_Nt9", ""));//+ Convert.ToDouble(drEditCt["Tien4"]);
            double dbAmtDisc = Math.Round((dbAmt / dbTTien) * 100, 7); //% trên tổng đơn hàng

            if (frmEditCt.dtEditCt.Columns.Contains("Tien_Ck_M4"))
                Calc_Chiet_Khau_ForInvoice(frmEditCt, dbAmt, true);
            else
                Calc_Chiet_Khau_ForInvoice(frmEditCt, dbAmtDisc);

            Voucher.Update_TTien(frmEditCt);
        }
        public static void ClearPromotionAuto(ref DataTable dtEditCt, ref DataTable dtEditCtDisc)
        {
            dtEditCtDisc = Common.FilterDatatable(dtEditCtDisc, "Ma_CTKM = 'CKHOADON'");

            DataTable dtTemp = dtEditCt.Clone();
            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                if ((bool)drEditCt["Deleted"] || drEditCt["Ma_Vt"].ToString() == string.Empty)
                    continue;

                if ((bool)drEditCt["Hang_Km"] && Convert.ToDouble(drEditCt["Tien_Nt9"]) == 0)
                {
                    
                    if ((string)drEditCt["Ma_CtKm_M"] == string.Empty && (string)drEditCt["Ma_So"] != string.Empty) //Là km tự động
                        continue;
                }

                drEditCt["Ma_CtKm"] = drEditCt["Ma_CTKM_Group"] = drEditCt["Ma_CTKM_Group1"] = drEditCt["Ma_CtKm1"] = drEditCt["Ma_So"] = "";

                if (drEditCt["Ma_So"].ToString() != "CKHOADON")
                {
                    drEditCt["Ma_So"] = string.Empty;
                    drEditCt["Tien_CkInvoice"] = 0;
                }


                drEditCt["Tien_Ck"] = drEditCt["Tien_CkInvoice"] = 0;
                drEditCt["Tien_CkInvoice"] = drEditCt["Tien_Ck_M4"];
                drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = Convert.ToDouble(drEditCt["Tien_M4"]) + Convert.ToDouble(drEditCt["Tien_Ck_M4"]);
                if (Convert.ToDouble(drEditCt["Tien_Nt9"]) != 0)
                    drEditCt["Chiet_Khau"] = Math.Round((Convert.ToDouble(drEditCt["Tien4"]) * 100) / Convert.ToDouble(drEditCt["Tien_Nt9"]), 8);
                else
                    drEditCt["Tien4"] = drEditCt["Tien_Nt4"] = drEditCt["Tien_M4"] = 0;

                Voucher.Calc_Thue_Vat(drEditCt);

                dtTemp.ImportRow(drEditCt);
            }
            dtEditCt = dtTemp;


        }
        

        public static void CalDiscountFreeItem(frmVoucher_Edit frmEditCt, string strMa_CtKm, string strSttKM, bool is_EditDisc, double dbDiscTime, DataRow drEditCtCur)
        {

            if (dbDiscTime == 0)
                return;

            Hashtable htFreeItem = new Hashtable();
            htFreeItem["MA_CTKM"] = strMa_CtKm;
            htFreeItem["STT"] = strSttKM;
            htFreeItem["DISCTIME"] = dbDiscTime;
            htFreeItem["NGAY_CT"] = Convert.ToDateTime(frmEditCt.drEditPh["Ngay_Ct"]);
            htFreeItem["MA_DVCS"] = Element.sysMa_DvCs;
            DataTable dtDiscFreeItem = SQLExec.ExecuteReturnDt("OM_GetDiscFreeItem", htFreeItem, CommandType.StoredProcedure);

            foreach (DataRow drFreeItem in dtDiscFreeItem.Rows)
            {

                DataRow drFreeItemNewAdd = frmEditCt.dtDiscFreeItem.NewRow();
                Common.SetDefaultDataRow(ref drFreeItemNewAdd);
                Common.CopyDataRow(drEditCtCur, drFreeItemNewAdd);
                if (frmEditCt.dtDiscFreeItem.Rows.Count > 0)
                    drFreeItemNewAdd["Stt0"] = Math.Max(Common.MaxDCValue(frmEditCt.dtEditCt, "Stt0"), Common.MaxDCValue(frmEditCt.dtDiscFreeItem, "Stt0")) + 1.0;
                else
                    drFreeItemNewAdd["Stt0"] = Common.MaxDCValue(frmEditCt.dtEditCt, "Stt0") + 1.0;
                drFreeItemNewAdd["Deleted"] = false;
                drFreeItemNewAdd["Ma_Vt"] = drFreeItem["Ma_Vt"].ToString();
                drFreeItemNewAdd["Ten_Vt"] = drFreeItem["Ten_Vt"].ToString();
                drFreeItemNewAdd["So_Luong9"] = drFreeItem["So_Luong_Km"];
                drFreeItemNewAdd["So_Luong"] = drFreeItem["So_Luong_Km"];
                drFreeItemNewAdd["Gia_Dft"] = drFreeItem["So_Luong_Org"];
                drFreeItemNewAdd["Dvt"] = drFreeItem["Dvt"].ToString();
                drFreeItemNewAdd["Ma_Kho"] = drFreeItem["Ma_Kho"].ToString() != string.Empty ? drFreeItem["Ma_Kho"] : drFreeItemNewAdd["Ma_Kho"];
                if (dtDiscFreeItem.Columns.Contains("Ma_Lo"))
                {
                    drFreeItemNewAdd["Ma_Lo"] = drFreeItem["Ma_Lo"];
                    drFreeItemNewAdd["Han_Sd"] = drFreeItem["Han_Sd"];
                }
                drFreeItemNewAdd["Ma_So"] = strSttKM;
                drFreeItemNewAdd["Gia_Nt9"] = 0;
                drFreeItemNewAdd["Tien_Nt9"] = 0;
                drFreeItemNewAdd["Gia_Nt2"] = 0;
                drFreeItemNewAdd["Gia2"] = 0;
                drFreeItemNewAdd["Tien_Nt2"] = 0;
                drFreeItemNewAdd["Tien2"] = 0;
                drFreeItemNewAdd["Tien_Nt3"] = 0;
                drFreeItemNewAdd["Tien3"] = 0;
                drFreeItemNewAdd["Tien_Nt4"] = 0;
                drFreeItemNewAdd["Tien4"] = 0;
                drFreeItemNewAdd["Tien_M4"] = drFreeItemNewAdd["Tien_Ck_M4"] = drFreeItemNewAdd["Tien_Ck"] = drFreeItemNewAdd["Tien_CkInvoice"] = 0;
                drFreeItemNewAdd["Hang_Km"] = true;
                drFreeItemNewAdd["Auto_Cost"] = true;
                drFreeItemNewAdd["Ma_CtKM"] = strMa_CtKm;
                drFreeItemNewAdd["Ma_CtKM_Group"] = drFreeItemNewAdd["Ma_CtKM_Group1"] = drFreeItemNewAdd["Ma_CtKM_M"] = "";
                drFreeItemNewAdd["Is_EditDisc"] = is_EditDisc;
                drFreeItemNewAdd["He_So9"] = 1;


                DataRow drDmVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", dtDiscFreeItem.Rows[0]["Ma_Vt"].ToString());
                string strDvt_Chuan = (string)drDmVt["Dvt"];

                if (dtDiscFreeItem.Rows[0]["Dvt"].ToString() == strDvt_Chuan)
                    drFreeItemNewAdd["He_So9"] = 1;
                else
                    for (int i = 1; i <= 3; i++)
                        if ((string)drDmVt["Dvt" + i] == (string)drFreeItemNewAdd["Dvt"])
                            drFreeItemNewAdd["He_So9"] = drDmVt["He_So" + i];

                if (dtDiscFreeItem.Columns.Contains("He_So9"))
                {
                    drFreeItemNewAdd["He_So9"] = drFreeItem["He_So9"];
                }
                frmEditCt.dtDiscFreeItem.Rows.Add(drFreeItemNewAdd);

                //DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;
                DataRow drDisc = frmEditCt.dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drFreeItemNewAdd["Stt0"];
                drDisc["Ma_CTKM"] = strMa_CtKm;
                drDisc["Stt_Km"] = strSttKM;
                drDisc["Tien4_Org"] = drFreeItem["So_Luong_Org"];
                drDisc["Tien4"] = drFreeItem["So_Luong_Km"];
                drDisc["Ma_Ns"] = strMa_CtKm;
                frmEditCt.dtEditCtDisc.Rows.Add(drDisc);


            }

        }
        public static void CalDiscountFreeItem(frmVoucher_Edit frmEditCt, string strMa_CtKm, string strSttKM, bool is_EditDisc, double dbDiscTime, DataRow drEditCtCur,string strMa_Ns, ref double dbQtyAlloc)
        {
            double dbSo_Luong_Km = 0;
            if (dbDiscTime == 0)
                return;

            Hashtable htFreeItem = new Hashtable();
            htFreeItem["MA_CTKM"] = strMa_CtKm;
            htFreeItem["STT"] = strSttKM;
            htFreeItem["DISCTIME"] = dbDiscTime;
            htFreeItem["QTYALLOC"] = dbQtyAlloc;
            htFreeItem["NGAY_CT"] = Convert.ToDateTime(frmEditCt.drEditPh["Ngay_Ct"]);
            htFreeItem["MA_DVCS"] = Element.sysMa_DvCs;
            DataTable dtDiscFreeItem = SQLExec.ExecuteReturnDt("OM_GetDiscFreeItem", htFreeItem, CommandType.StoredProcedure);

            foreach (DataRow drFreeItem in dtDiscFreeItem.Rows)
            {
                dbSo_Luong_Km = Convert.ToDouble(drFreeItem["So_Luong_Km"]);
                DataRow drFreeItemNewAdd = frmEditCt.dtDiscFreeItem.NewRow();
                Common.SetDefaultDataRow(ref drFreeItemNewAdd);
                Common.CopyDataRow(drEditCtCur, drFreeItemNewAdd);



                if (frmEditCt.dtDiscFreeItem.Rows.Count > 0)
                    drFreeItemNewAdd["Stt0"] = Math.Max(Common.MaxDCValue(frmEditCt.dtEditCt, "Stt0"), Common.MaxDCValue(frmEditCt.dtDiscFreeItem, "Stt0")) + 1.0;
                else
                    drFreeItemNewAdd["Stt0"] = Common.MaxDCValue(frmEditCt.dtEditCt, "Stt0") + 1.0;
                drFreeItemNewAdd["Deleted"] = false;
                drFreeItemNewAdd["Ma_Vt"] = drFreeItem["Ma_Vt"].ToString();
                drFreeItemNewAdd["Ten_Vt"] = drFreeItem["Ten_Vt"].ToString();
                drFreeItemNewAdd["So_Luong9"] = dbSo_Luong_Km;
                drFreeItemNewAdd["So_Luong"] = dbSo_Luong_Km;
                drFreeItemNewAdd["Gia_Dft"] = drFreeItem["So_Luong_Org"];
                drFreeItemNewAdd["Dvt"] = drFreeItem["Dvt"].ToString();
                drFreeItemNewAdd["Ma_Kho"] = drFreeItem["Ma_Kho"].ToString() != string.Empty ? drFreeItem["Ma_Kho"] : drFreeItemNewAdd["Ma_Kho"];
                if (dtDiscFreeItem.Columns.Contains("Ma_Lo"))
                {
                    drFreeItemNewAdd["Ma_Lo"] = drFreeItem["Ma_Lo"];
                    drFreeItemNewAdd["Han_Sd"] = drFreeItem["Han_Sd"];
                }
                drFreeItemNewAdd["Ma_So"] = strSttKM;
                drFreeItemNewAdd["Gia_Nt9"] = 0;
                drFreeItemNewAdd["Tien_Nt9"] = 0;
                drFreeItemNewAdd["Gia_Nt2"] = 0;
                drFreeItemNewAdd["Gia2"] = 0;
                drFreeItemNewAdd["Tien_Nt2"] = 0;
                drFreeItemNewAdd["Tien2"] = 0;
                drFreeItemNewAdd["Tien_Nt3"] = 0;
                drFreeItemNewAdd["Tien3"] = 0;
                drFreeItemNewAdd["Tien_Nt4"] = 0;
                drFreeItemNewAdd["Tien4"] = 0;
                drFreeItemNewAdd["Tien_M4"] = drFreeItemNewAdd["Tien_Ck_M4"] = drFreeItemNewAdd["Tien_Ck"] = drFreeItemNewAdd["Tien_CkInvoice"] = 0;
                drFreeItemNewAdd["Hang_Km"] = true;
                drFreeItemNewAdd["Auto_Cost"] = true;
                drFreeItemNewAdd["Ma_CtKM"] = strMa_CtKm;
                drFreeItemNewAdd["Ma_CtKM_Group"] = drFreeItemNewAdd["Ma_CtKM_Group1"] = drFreeItemNewAdd["Ma_CtKM_M"] = "";
                drFreeItemNewAdd["Is_EditDisc"] = is_EditDisc;
                drFreeItemNewAdd["He_So9"] = 1;


                DataRow drDmVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", dtDiscFreeItem.Rows[0]["Ma_Vt"].ToString());
                string strDvt_Chuan = (string)drDmVt["Dvt"];

                if (dtDiscFreeItem.Rows[0]["Dvt"].ToString() == strDvt_Chuan)
                    drFreeItemNewAdd["He_So9"] = 1;
                else
                    for (int i = 1; i <= 3; i++)
                        if ((string)drDmVt["Dvt" + i] == (string)drFreeItemNewAdd["Dvt"])
                            drFreeItemNewAdd["He_So9"] = drDmVt["He_So" + i];

                if (dtDiscFreeItem.Columns.Contains("He_So9"))
                {
                    drFreeItemNewAdd["He_So9"] = drFreeItem["He_So9"];
                }
                frmEditCt.dtDiscFreeItem.Rows.Add(drFreeItemNewAdd);

                //DataTable dtEditCtDisc = frmEditCt.dtEditCtDisc;
                DataRow drDisc = frmEditCt.dtEditCtDisc.NewRow();
                drDisc["Stt0"] = drFreeItemNewAdd["Stt0"];
                drDisc["Ma_CTKM"] = strMa_CtKm;
                drDisc["Stt_Km"] = strSttKM;
                drDisc["Tien4_Org"] = drFreeItem["So_Luong_Org"];
                drDisc["Tien4"] = dbSo_Luong_Km;
                drDisc["Ma_Ns"] = strMa_Ns;
                frmEditCt.dtEditCtDisc.Rows.Add(drDisc);

                if (dbQtyAlloc > 0)//Cập nhật lại ngân sách sau khi add km
                    dbQtyAlloc = dbQtyAlloc - dbSo_Luong_Km;

            }

        }

        #region OM_SaveOM_SalesDics

        /*public static bool OM_SaveOM_SalesDics(frmVoucher_Edit frmEditCt)
        {
            if (frmEditCt.dtEditCtDisc.Rows.Count > 0)
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
        }*/
        #endregion
        #region L - Loại khuyến mãi dòng
        /*
                private void CalcDiscount(frmVoucher_Edit frmEditCt, DateTime dteNgay_Ct, string strMa_Dt, string strStt) //ref DataTable dtEditCt
                {

                    //Lấy các chương trình khuyến mãi / chiết khấu đang chạy
                    DataTable dtDiscCount = Discount.GetDiscoutProg(dteNgay_Ct, strMa_Dt);

                    DataTable dtEditCt = frmEditCt.dtEditCt;
                    DataTable dtDiscFreeItemAddNew;

                    Discount.ClearPromotionAuto(ref dtEditCt);

                    if (dtDiscCount.Rows.Count == 0)
                        return;

                    frmEditCt.dtDiscFreeItem = dtEditCt.Clone();

                    frmEditCt.dtEditCtDisc = new DataTable("OM_SalesDics");
                    DataColumn dcStt0 = new DataColumn("Stt0", typeof(string));
                    dcStt0.DefaultValue = "";
                    frmEditCt.dtEditCtDisc.Columns.Add(dcStt0);
                    DataColumn dcMa_Vt = new DataColumn("Ma_CTKM", typeof(string));
                    dcMa_Vt.DefaultValue = "";
                    frmEditCt.dtEditCtDisc.Columns.Add(dcMa_Vt);

                    DataColumn dcTien4 = new DataColumn("Tien4", typeof(double));
                    dcTien4.DefaultValue = 0;
                    frmEditCt.dtEditCtDisc.Columns.Add(dcTien4);


                    string strMa_Vt = string.Empty;

                    //double dbDiscAmt = 0; // Tiền KM
                    //double dbDiscPer = 0; // % khuyến mãi

                    double dbSo_luong = 0;
                    double dbNgan_Sach = 0;
                    double dbSaleAmt = 0;
                    double dbTien = 0;
                    double dbTien4 = 0;
                    double dbTTien4 = 0;

                    foreach (DataRow drDisc in dtDiscCount.Rows)
                    {
                        string strMa_CtKm = drDisc["Ma_CTKM"].ToString();
                        string strLoai_CtKm = drDisc["Loai_Km"].ToString();
                        string strLoai_Ap_KM = drDisc["Loai_Ap_KM"].ToString();
                        string strHinh_Thuc_KM = drDisc["Hinh_Thuc_KM"].ToString();
                        string strBreakBy = drDisc["BreakBy"].ToString(); // Kiểm tra theo
                        bool isEditKm = Convert.ToBoolean(drDisc["AllowEditDisc"]);
                        dtDiscFreeItemAddNew = dtEditCt.Clone();


                        if (!Discount.CheckDtOnProgID(strMa_CtKm, strMa_Dt))
                            continue;

                        dbNgan_Sach = strHinh_Thuc_KM == "IN" ? Convert.ToDouble(drDisc["TSo_Luong"]) : Convert.ToDouble(drDisc["TTien"]);
                        if (dbNgan_Sach != 0)
                        {
                            Hashtable htNs = new Hashtable();
                            htNs["MA_CTKM"] = strMa_CtKm;
                            htNs["STT"] = strStt;
                            DataTable dtNgan_Sach = SQLExec.ExecuteReturnDt("OM_Getbudget", htNs, CommandType.StoredProcedure);
                            dbNgan_Sach = Convert.ToDouble(dtNgan_Sach.Rows[0]["Budget"]);
                            dbSaleAmt = Convert.ToDouble(dtNgan_Sach.Rows[0]["SalesAmt"]);

                        }

                        #region L - Loại khuyến mãi dòng
                        if (strLoai_CtKm == "L") // Loại khuyến mãi dòng
                        {

                            double dbQty = 0, dbAmt = 0;

                            string strMa_Vt_Disc = string.Empty;
                            string strMa_Vt_Disc_List = string.Empty;

                            DataTable dtItemSale = dtEditCt.DefaultView.ToTable(true, "Ma_Vt");
                            //DataTable dtItemSale = SQLExec.ExecuteReturnDt("select * from OM_DiscItem WHERE Ma_CTKM = '" + strMa_CtKm + "'");
                            foreach (DataRow dritem in dtItemSale.Rows)
                            {
                                strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                                strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
                                dbQty = Common.SumDCValue(dtEditCt, "So_Luong", "MA_VT = '" + strMa_Vt_Disc + "' AND Hang_Km  <> true");
                                dbAmt = Common.SumDCValue(dtEditCt, "Tien_Nt9", "MA_VT = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");

                                Hashtable htParaBreaby = new Hashtable();
                                htParaBreaby["MA_CTKM"] = strMa_CtKm;
                                htParaBreaby["MA_VT"] = strMa_Vt_Disc;
                                htParaBreaby["QTYAMT"] = strBreakBy == "Q" ? dbQty : dbAmt; // Q: Qty, A : Amount
                                DataTable dtBreakBy = SQLExec.ExecuteReturnDt("OM_GetDiscBreak", htParaBreaby, CommandType.StoredProcedure);
                                if (dtBreakBy.Rows.Count > 0)
                                {

                                    double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                                    string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                                    double dbBreakQty = Convert.ToDouble(dtBreakBy.Rows[0]["BreakQty"]);
                                    int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);

                                    if (strHinh_Thuc_KM == "PP") // áp dụng cho Chiết khấu dòng %
                                    {
                                        Discount.Calc_Chiet_Khau_ForLine(frmEditCt, dbAmtDisc, strMa_Vt_Disc, strMa_CtKm, isEditKm);
                                    }
                                    else if (strHinh_Thuc_KM == "II") // Chiết khấu tiền 
                                    {
                                        dbAmtDisc *= iDiscTime;
                                        double dbPer = Math.Round((dbAmtDisc / dbTien) * 100, 7);
                                        Discount.Calc_Chiet_Khau_ForLine(frmEditCt, dbPer, strMa_Vt_Disc, strMa_CtKm, isEditKm);
                                    }
                                    else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                                    {
                                        Discount.CalDiscountFreeItem(frmEditCt, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Rows[0]);
                                    }
                                }
                            }
                        }
                        #endregion
                        #region G - Khuyến mãi theo nhóm mặt hàng
                        else if (strLoai_CtKm == "G") // Loại khuyến mãi nhóm
                        {
                            double dbQty = 0, dbAmt = 0;
                            string strMa_Vt_Disc = string.Empty;
                            string strMa_Vt_Disc_List = string.Empty;

                            DataTable dtDiscItemSale = SQLExec.ExecuteReturnDt("select * from OM_DiscItem WHERE Ma_CTKM = '" + strMa_CtKm + "'");
                            foreach (DataRow dritem in dtDiscItemSale.Rows)
                            {
                                strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                                strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
                                dbQty += Common.SumDCValue(dtEditCt, "So_Luong", "Ma_Vt = '" + strMa_Vt_Disc + "' AND Hang_Km <> true");
                                dbAmt += Common.SumDCValue(dtEditCt, "Tien_Nt9", "Ma_Vt = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");

                            }

                            Hashtable htParaBreaby = new Hashtable();
                            htParaBreaby["MA_CTKM"] = strMa_CtKm;
                            htParaBreaby["QTYAMT"] = strBreakBy == "Q" ? dbQty : dbAmt; // Q: Qty, A : Amount
                            DataTable dtBreakBy = SQLExec.ExecuteReturnDt("OM_GetDiscBreak", htParaBreaby, CommandType.StoredProcedure);

                            if (dtBreakBy.Rows.Count > 0)
                            {
                                double dbBreakAmt = Convert.ToDouble(dtBreakBy.Rows[0]["BreakAmt"]);
                                string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                                double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                                int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi
                                if (strHinh_Thuc_KM == "PP")
                                {
                                    Discount.Calc_Chiet_Khau_ForGroup(frmEditCt, dbAmtDisc, strMa_Vt_Disc_List, strMa_CtKm, isEditKm);

                                }
                                else if (strHinh_Thuc_KM == "II")
                                {
                                    dbAmtDisc *= iDiscTime;
                                    double dbPer = Math.Round((dbAmtDisc / dbAmt) * 100, 7);
                                    Discount.Calc_Chiet_Khau_ForGroup(frmEditCt, dbPer, strMa_Vt_Disc_List, strMa_CtKm, isEditKm);
                                }
                                else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                                {
                                    //int aTemp = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi
                                    Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Rows[0]);
                                }
                                dtEditCt.AcceptChanges();

                            }

                            Calc_Thue_Vat_All();

                        }
                        #endregion

                        #region I - Chiết khấu hóa đơn

                        if (strLoai_CtKm == "I") // Loại Khuyến mãi/ CK Invoice
                        {

                            double dbTTien = 0;
                            double dbTien_CkInvoice = 0;

                            #region Kiểm tra theo số tiền
                            if (strBreakBy == "A") // Kiểm tra theo số tiền
                            {

                                dbTTien = Convert.ToDouble(Common.SumDCValue(dtEditCt, "Tien_Nt9", ""));//+ Convert.ToDouble(drEditCt["Tien4"]);
                                Hashtable htParaBreaby = new Hashtable();
                                htParaBreaby["MA_CTKM"] = strMa_CtKm;
                                htParaBreaby["QTYAMT"] = dbTTien;
                                DataTable dtBreakBy = SQLExec.ExecuteReturnDt("OM_GetDiscBreakInvoice", htParaBreaby, CommandType.StoredProcedure);

                                if (dtBreakBy.Rows.Count > 0)
                                {
                                    string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                                    double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                                    int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi

                                    if (strHinh_Thuc_KM == "II")
                                    {
                                        dbAmtDisc = Math.Round((dbAmtDisc / dbTTien) * 100, 7); //% trên tổng đơn hàng
                                    }
                                    Discount.Calc_Chiet_Khau_ForInvoice(frmEditCt, dbAmtDisc, strMa_CtKm, isEditKm);
                                }

                            }
                            #endregion
                        }

                        #endregion

                    }


                    foreach (DataRow drit in frmEditCt.dtDiscFreeItem.Rows)
                    {
                        Voucher.Calc_So_Luong(drit);
                        dtEditCt.ImportRow(drit);
                    }
                    frmEditCtdtDiscFreeItem.Rows.Clear();

                    dtEditCt.AcceptChanges();
                    bdsEditCt.DataSource = dtEditCt;
                    dgvEditCt1.DataSource = bdsEditCt;

                    Discount.OM_SaveOM_SalesDics(this);
                    EpointMessage.MsgOk("Đã tính xong KM");
                }

                */
        /*       if (strLoai_CtKm == "L") // Loại khuyến mãi dòng
                {

                    double dbQty = 0, dbAmt = 0;

                    string strMa_Vt_Disc = string.Empty;
                    string strMa_Vt_Disc_List = string.Empty;

                    DataTable dtItemSale = dtEditCt.DefaultView.ToTable(true, "Ma_Vt");
                    foreach (DataRow dritem in dtEditCt.Rows)
                    {
                        strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                        strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
                        dbQty += Common.SumDCValue(dtEditCt, "So_Luong", "Ma_Vt = '" + strMa_Vt_Disc + "' AND Hang_Km = 0");
                        dbAmt += Common.SumDCValue(dtEditCt, "Tien_Nt9", "Ma_Vt = '" + strMa_Vt_Disc + "'  AND Hang_Km = 0");
                    }

                    #region Kiểm tra theo số lượng
                    if (strBreakBy == "Q") // kiểm tra theo số lượng
                    {

                       
                        foreach (DataRow drEditCt in dtEditCt.Rows)
                        {
                            if ((bool)drEditCt["Hang_Km"]) // Đối với hàng KM nhập tay
                                continue;

                            dbSo_luong = Convert.ToDouble(drEditCt["So_Luong"]);
                            strDiscItem = drEditCt["Ma_Vt"].ToString();
                            dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]);

                            Hashtable htParaBreaby = new Hashtable();
                            htParaBreaby["MA_CTKM"] = strMa_CtKm;
                            htParaBreaby["QTYAMT"] = dbSo_luong;
                            htParaBreaby["MA_VT"] = strDiscItem;
                            DataTable dtBreakBy = SQLExec.ExecuteReturnDt("OM_GetDiscBreak", htParaBreaby, CommandType.StoredProcedure);

                            if (dtBreakBy.Rows.Count > 0)
                            {

                                if (true)
                                {
                                    double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                                    string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                                    double dbBreakQty = Convert.ToDouble(dtBreakBy.Rows[0]["BreakQty"]);
                                    //bool bHe_So = Convert.ToBoolean(dtBreakBy.Rows[0]["He_So"]);

                                    if (strHinh_Thuc_KM == "PP") // áp dụng cho Chiết khấu dòng %
                                    {
                                        Discount.Calc_Chiet_Khau_ForLine(this, dbAmtDisc, drEditCt["Stt0"].ToString(), strMa_CtKm, isEditKm);
                                    }
                                    else if (strHinh_Thuc_KM == "II") // Chiết khấu tiền 
                                    {
                                        dbAmtDisc *= Convert.ToDouble(dtBreakBy.Rows[0]["DiscTime"]);
                                        double dbPer = Math.Round((dbAmtDisc / dbTien) * 100, 7);
                                        Discount.Calc_Chiet_Khau_ForLine(this, dbPer, drEditCt["Stt0"].ToString(), strMa_CtKm, isEditKm);
                                    }
                                    else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                                    {
                                        int iSLKM = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);
                                        CalDiscountFreeItem(strMa_CtKm, strSttKM, isEditKm, iSLKM, drEditCt);
                                    }


                                    drEditCt.AcceptChanges();
                                    dtEditCt.AcceptChanges();

                                }

                            }

                            Calc_Thue_Vat(drEditCt);
                        }


                        foreach (DataRow drit in dtDiscFreeItemAddNew.Rows)
                        {
                            Voucher.Calc_So_Luong(drit);
                            dtEditCt.ImportRow(drit);
                            //dtDiscFreeItemAddNew.Rows.Remove(drit);
                        }

                        dtDiscFreeItemAddNew.Rows.Clear();
                    }
*/
        #endregion



    }
}

