using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Data;
using Epoint.Systems.Librarys;
using Epoint.Systems;
using Epoint.Systems.Commons;
using System.Collections;

namespace Epoint.Lists
{
    public partial class frmDoiTuong_Edit : Epoint.Lists.frmEdit
    {
        DataRow drCurrent;
        private string strCode = string.Empty;
        const string TABLENAME = "LIDOITUONG";

        #region Phuong thuc

        public frmDoiTuong_Edit()
        {
            InitializeComponent();

            txtMa_Nh_Dt.Validating += new CancelEventHandler(txtMa_Nh_Dt_Validating);
            txtMa_Kv.Validating += new CancelEventHandler(txtMa_Kv_Validating);
            txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
            txtMa_CBNV_BH.Validating += new CancelEventHandler(txtMa_CbNv_BH_Validating);
            txtMa_CbNV_GH.Validating += new CancelEventHandler(txtMa_CbNv_GH_Validating);
            txtMa_Dt_Gia_Mua.Validating += new CancelEventHandler(txtMa_Dt_Gia_Validating);
            txtMa_Tuyen.Validating += new CancelEventHandler(txtMa_Tuyen_Validating);


            txtTk_Dt.Validating += new CancelEventHandler(txtTk_Dt_Validating);
            txtTk_Dthu.Validating += new CancelEventHandler(txtTk_Dthu_Validating);
            txtTk_Hbtl.Validating += new CancelEventHandler(txtTk_Hbtl_Validating);
        }

        public override void Load(enuEdit enuNew_Edit, DataRow drCurrent)
        {
            this.drCurrent = drCurrent;
            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            if (enuNew_Edit == enuEdit.Edit)
                this.drEdit = DataTool.SQLGetDataRowByID(TABLENAME, "Ma_Dt", drCurrent["Ma_Dt"].ToString());
            else
            {
                this.drEdit = DataTool.SQLGetDataTable(TABLENAME, null, "0 = 1", "Ma_Dt").NewRow();
                DataRow temp = DataTool.SQLGetDataRowByID(TABLENAME, "Ma_Dt", drCurrent["Ma_Dt"].ToString());
                if (temp != null)
                    Common.CopyDataRow(temp, drEdit);

                Common.CopyDataRow(drCurrent, drEdit);
                drEdit["Ma_Dt"] = GetNewID(drEdit["Ma_Dt"].ToString());
            }

            if (enuNew_Edit == enuEdit.New && drEdit.Table.Columns.Contains("Tien_No_Max"))
                drEdit["Tien_No_Max"] = 0;

            Common.ScaterMemvar(this, ref drEdit);

            this.strCode = txtMa_Dt.Text;

            BindingLanguage();
            LoadDicName();

            this.ShowDialog();
        }

        private void LoadDicName()
        {
            if (txtMa_Nh_Dt.Text.Trim() != string.Empty)
            {
                lbtTen_Nh_Dt.Text = DataTool.SQLGetNameByCode("LIDOITUONGNH", "Ma_Nh_Dt", "Ten_Nh_Dt", txtMa_Nh_Dt.Text.Trim());
            }
            else
                lbtTen_Nh_Dt.Text = string.Empty;

            if (txtMa_Kv.Text.Trim() != string.Empty)
            {
                lbtTen_Kv.Text = DataTool.SQLGetNameByCode("LIKHUVUC", "Ma_Kv", "Ten_Kv", txtMa_Kv.Text.Trim());
            }
            else
                lbtTen_CbNv.Text = string.Empty;

            if (txtMa_CbNv.Text.Trim() != string.Empty)
            {
                lbtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", txtMa_CbNv.Text.Trim());
            }
            else
                lbtTen_CbNv.Text = string.Empty;

            if (txtMa_Dt_Gia_Mua.Text.Trim() != string.Empty)
            {
                lbtTen_Dt_Gia_Mua.Text = DataTool.SQLGetNameByCode("LIDOITUONG", "Ma_Dt", "Ten_Dt", txtMa_Dt_Gia_Mua.Text.Trim());
            }
            else
                lbtTen_Dt_Gia_Mua.Text = string.Empty;


            if (txtMa_CBNV_BH.Text.Trim() != string.Empty)
            {
                lbtTen_Nv_Bh.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", txtMa_CBNV_BH.Text.Trim());
            }
            else
                lbtTen_Nv_Bh.Text = string.Empty;


            if (txtMa_CbNV_GH.Text.Trim() != string.Empty)
            {
                lbtTen_Nv_Gh.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", txtMa_CbNV_GH.Text.Trim());
            }
            else
                lbtTen_Nv_Gh.Text = string.Empty;

            if (txtMa_Tuyen.Text.Trim() != string.Empty)
            {
                lbtTen_Tuyen.Text = DataTool.SQLGetNameByCode("LITUYEN", "Ma_Tuyen", "Ten_Tuyen", txtMa_Tuyen.Text.Trim());
            }
            else
                lbtTen_Tuyen.Text = string.Empty;

            //Tk_Gv
            if (txtTk_Dthu.Text.Trim() != string.Empty)
            {
                lbtTen_Tk_Dthu.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Dthu.Text.Trim());
                dicName.Add(lbtTen_Tk_Dthu.Name, lbtTen_Tk_Dthu.Text);
            }
            else
                lbtTen_Tk_Dthu.Text = string.Empty;

            //Tk_Dt
            if (txtTk_Dt.Text.Trim() != string.Empty)
            {
                lbtTen_Tk_Dt.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Dt.Text.Trim());
                dicName.Add(lbtTen_Tk_Dt.Name, lbtTen_Tk_Dt.Text);
            }
            else
                lbtTen_Tk_Dt.Text = string.Empty;

            //Tk_HbTl
            if (txtTk_Hbtl.Text.Trim() != string.Empty)
            {
                lbtTen_Tk_Hbtl.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Hbtl.Text.Trim());
                dicName.Add(lbtTen_Tk_Hbtl.Name, lbtTen_Tk_Hbtl.Text);
            }
            else
                lbtTen_Tk_Hbtl.Text = string.Empty;

        }

        public override bool FormCheckValid()
        {
            bool bvalid = true;
            if (txtMa_Dt.Text.Trim() == string.Empty)
            {
                Common.MsgCancel(Languages.GetLanguage("Ma_Dt") + " " +
                                 Languages.GetLanguage("Not_Null"));
                return false;
            }

            if (txtTen_Dt.Text.Trim() == string.Empty)
            {
                Common.MsgCancel(Languages.GetLanguage("Ten_Dt") + " " +
                                 Languages.GetLanguage("Not_Null"));
                return false;
            }

            if (txtMa_Nh_Dt.Text.Trim() == string.Empty)
            {
                Common.MsgCancel(Languages.GetLanguage("Ma_Nh_Dt") + " " +
                                 Languages.GetLanguage("Not_Null"));
                return false;
            }

            return bvalid;
        }

        public override bool Save()
        {
            Common.GatherMemvar(this, ref drEdit);

            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;

            //Kiem tra Valid CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "LIDOITUONG", ref drEdit))
                return false;

            Common.CopyDataRow(drEdit, drCurrent);

            //Doi ma
            if (this.enuNew_Edit == enuEdit.Edit && this.strCode != txtMa_Dt.Text)
                DataTool.SQLChangeID("MA_DT", drCurrent);

            return true;
        }

        private string GetNewID(string Ma_Dt_Old)
        {
            string strMa_Dt_New = string.Empty;

            if (Ma_Dt_Old == "")
                Ma_Dt_Old = "KH000001";

            Hashtable htParameter = new Hashtable();
            htParameter.Add("TABLENAME", TABLENAME);
            htParameter.Add("COLUMNNAME", "MA_DT");
            htParameter.Add("CURRENTID", Ma_Dt_Old);
            htParameter.Add("KEY", "");
            htParameter.Add("NGAY_CT", "");
            htParameter.Add("SUFFIXLEN", "");
            strMa_Dt_New = SQLExec.ExecuteReturnValue("sp_GetNewId", htParameter, CommandType.StoredProcedure).ToString();

            return strMa_Dt_New;
        }

        #endregion

        #region Su kien

        void txtMa_Nh_Dt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Nh_Dt.Text.Trim();
            bool bRequire = true;

            frmDoiTuongNh frmLookup = new frmDoiTuongNh();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONGNH", "Ma_Nh_Dt", strValue, bRequire, "", "Nh_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Nh_Dt.Text = string.Empty;
                lbtTen_Nh_Dt.Text = string.Empty;
            }
            else
            {
                txtMa_Nh_Dt.Text = ((string)drLookup["Ma_Nh_Dt"]).Trim();
                lbtTen_Nh_Dt.Text = ((string)drLookup["Ten_Nh_Dt"]).Trim();
            }

            dicName.SetValue(lbtTen_Nh_Dt.Name, lbtTen_Nh_Dt.Text);
        }

        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = false;

            //frmNhanVien frmLookup = new frmNhanVien();
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CbNv.Text = string.Empty;
                lbtTen_CbNv.Text = string.Empty;
            }
            else
            {
                txtMa_CbNv.Text = ((string)drLookup["Ma_CbNv"]).Trim();
                lbtTen_CbNv.Text = ((string)drLookup["Ten_CbNv"]).Trim();
            }

            dicName.SetValue(lbtTen_CbNv.Name, lbtTen_CbNv.Text);
        }
        void txtMa_CbNv_BH_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CBNV_BH.Text.Trim();
            bool bRequire = true;

            //frmNhanVien frmLookup = new frmNhanVien();
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CBNV_BH.Text = string.Empty;
                lbtTen_Nv_Bh.Text = string.Empty;
            }
            else
            {
                txtMa_CBNV_BH.Text = ((string)drLookup["Ma_CbNv"]).Trim();
                lbtTen_Nv_Bh.Text = ((string)drLookup["Ten_CbNv"]).Trim();
            }

            dicName.SetValue(lbtTen_Nv_Bh.Name, lbtTen_Nv_Bh.Text);
        }
        void txtMa_CbNv_GH_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNV_GH.Text.Trim();
            bool bRequire = true;

            //frmNhanVien frmLookup = new frmNhanVien();
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CbNV_GH.Text = string.Empty;
                lbtTen_Nv_Gh.Text = string.Empty;
            }
            else
            {
                txtMa_CbNV_GH.Text = ((string)drLookup["Ma_CbNv"]).Trim();
                lbtTen_Nv_Gh.Text = ((string)drLookup["Ten_CbNv"]).Trim();
            }

            dicName.SetValue(lbtTen_Nv_Gh.Name, lbtTen_Nv_Gh.Text);
        }
        void txtMa_Kv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Kv.Text.Trim();
            bool bRequire = false;

            //frmKhuVuc frmLookup = new frmKhuVuc();
            DataRow drLookup = Lookup.ShowLookup("Ma_Kv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Kv.Text = string.Empty;
                lbtTen_Kv.Text = string.Empty;
            }
            else
            {
                txtMa_Kv.Text = ((string)drLookup["Ma_Kv"]).Trim();
                lbtTen_Kv.Text = ((string)drLookup["Ten_Kv"]).Trim();
            }

            dicName.SetValue(lbtTen_Kv.Name, lbtTen_Kv.Text);
        }
        void txtMa_Tuyen_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Tuyen.Text.Trim();
            bool bRequire = false;

            //frmKhuVuc frmLookup = new frmKhuVuc();
            DataRow drLookup = Lookup.ShowLookup("Ma_Tuyen", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Tuyen.Text = string.Empty;
                lbtTen_Tuyen.Text = string.Empty;
            }
            else
            {
                txtMa_Tuyen.Text = ((string)drLookup["Ma_Tuyen"]).Trim();
                lbtTen_Tuyen.Text = ((string)drLookup["Ten_Tuyen"]).Trim();
            }

            dicName.SetValue(lbtTen_Tuyen.Name, lbtTen_Tuyen.Text);
        }
        void txtMa_Dt_Gia_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Dt_Gia_Mua.Text.Trim();
            bool bRequire = false;

            //frmDoiTuong frmLookup = new frmDoiTuong();
            DataRow drLookup = Lookup.ShowLookup("Ma_Dt", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Dt_Gia_Mua.Text = string.Empty;
                lbtTen_Dt_Gia_Mua.Text = string.Empty;
            }
            else
            {
                txtMa_Dt_Gia_Mua.Text = ((string)drLookup["Ma_Dt"]).Trim();
                lbtTen_Dt_Gia_Mua.Text = ((string)drLookup["Ten_Dt"]).Trim();
            }

            dicName.SetValue(lbtTen_Dt_Gia_Mua.Name, lbtTen_Dt_Gia_Mua.Text);
        }

        private void txtTk_Dt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Dt.Text.Trim();
            bool bRequire = false;

            //frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Dt.Text = string.Empty;
                lbtTen_Tk_Dt.Text = string.Empty;
            }
            else
            {
                txtTk_Dt.Text = ((string)drLookup["Tk"]).Trim();
                lbtTen_Tk_Dt.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTen_Tk_Dt.Name, lbtTen_Tk_Dt.Text);
        }
        private void txtTk_Dthu_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Dthu.Text.Trim();
            bool bRequire = false;

            //frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Dthu.Text = string.Empty;
                lbtTen_Tk_Dthu.Text = string.Empty;
            }
            else
            {
                txtTk_Dthu.Text = ((string)drLookup["Tk"]).Trim();
                lbtTen_Tk_Dthu.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTen_Tk_Dthu.Name, lbtTen_Tk_Dthu.Text);
        }
        private void txtTk_Hbtl_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Hbtl.Text.Trim();
            bool bRequire = false;

            //frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Hbtl.Text = string.Empty;
                lbtTen_Tk_Hbtl.Text = string.Empty;
            }
            else
            {
                txtTk_Hbtl.Text = ((string)drLookup["Tk"]).Trim();
                lbtTen_Tk_Hbtl.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTen_Tk_Hbtl.Name, lbtTen_Tk_Hbtl.Text);
        }
        #endregion
    }
}