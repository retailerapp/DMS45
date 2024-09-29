using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Elements;
using System.Collections;

namespace Epoint.Modules.AR
{
    public partial class frmDiscountConfig_Edit : Epoint.Lists.frmEdit
    {

        #region Khai báo
        private string Ma_CtKM_Old = string.Empty;
        public string Loai_Km = string.Empty;
        #endregion

        #region Phuong thuc

        public frmDiscountConfig_Edit()
        {
            InitializeComponent();

            //this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            //this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            this.cbxHinh_Thuc_Km.SelectedValueChanged += new EventHandler(cbxHinh_Thuc_Km_SelectedValueChanged);
            this.cbxLoai_KM.SelectedValueChanged += new EventHandler(cbxLoai_KM_SelectedValueChanged);

            txtMa_Ngan_Sach.Validating += new CancelEventHandler(txtMa_Ngan_Sach_Validating);

        }
        public override void Load(enuEdit enuNew_Edit, DataRow drEdit)
        {
            if (Element.Is_Running)
            {
                this.drEdit = drEdit;
                this.enuNew_Edit = enuNew_Edit;
                this.Tag = (char)enuNew_Edit + "," + this.Tag;
                this.Object_ID = "KHUYENMAI";

                if (this.enuNew_Edit == enuEdit.Copy)
                {
                    this.Ma_CtKM_Old = drEdit["Ma_CtKm"].ToString();
                    this.enuNew_Edit = enuEdit.New;
                }

                this.BindingCombobox();
                Common.ScaterMemvar(this, ref drEdit);

                BindingLanguage();
                LoadDicName();

                if (this.enuNew_Edit == enuEdit.Edit)
                {
                    txtMa_CTKM.Enabled = false;
                }


                this.ShowDialog();
            }
        }

        private void LoadDicName()
        {

        }

        public override bool FormCheckValid()
        {
            bool bvalid = true;
            if (txtMa_CTKM.Text.Trim() == string.Empty)
            {
                Common.MsgOk(Languages.GetLanguage("Ma_CTKM") + " " +
                        Languages.GetLanguage("Not_Null"));
                return false;
            }

            if (txtTen_CTKM.Text.Trim() == string.Empty)
            {
                Common.MsgOk(Languages.GetLanguage("Ten_CTKM") + " " +
                        Languages.GetLanguage("Not_Null"));
                return false;
            }
            if (!Element.sysIs_Admin)
            {
                if (!Common.CheckDataLocked(Library.StrToDate(dteNgay_BD.Text)) || !Common.CheckDataLocked(Library.StrToDate(dteNgay_Kt.Text)))
                {

                    this.lbtInformation.Text = "Dữ liệu đã bị khóa, Liên hệ Admin hệ thống.";
                    return false;
                }
            }

            return bvalid;
        }

        public override bool Save()
        {
            if (cbxHinh_Thuc_Km.SelectedValue.ToString() == "IN")
                numTTien.Value = 0;
            else
                numTSo_Luong.Value = 0;

            Common.GatherMemvar(this, ref drEdit);

            drEdit["Ten_Loai_Km"] = cbxLoai_KM.Text;
            drEdit["Ten_BreakBy"] = cbxBreakBy.Text;
            drEdit["Ten_Hinh_Thuc_KM"] = cbxHinh_Thuc_Km.Text;
            drEdit["Ten_Loai_Ap_Km"] = cbxLoai_Ap_Km.Text;


            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;



            //Luu xuong CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "OM_Discount", ref drEdit))
                return false;

            if (this.enuNew_Edit == enuEdit.New && this.Ma_CtKM_Old != string.Empty)
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("MA_CTKM_OLD", Ma_CtKM_Old);
                htPara.Add("MA_CTKM", txtMa_CTKM.Text);
                if (!SQLExec.Execute("SP_OM_CopyDiscountProg", htPara, CommandType.StoredProcedure))
                    return false;
            }

            return true;
        }
        private void BindingCombobox()
        {
            cbxLoai_KM.DataSource = SQLExec.ExecuteReturnDt(" sp_OM_GetCombovalue @Key = 'DiscountType'", CommandType.Text);
            cbxLoai_KM.ValueMember = "ID";
            cbxLoai_KM.DisplayMember = "Value";

            cbxLoai_Ap_Km.DataSource = SQLExec.ExecuteReturnDt(" sp_OM_GetCombovalue @Key = 'DiscountAply'", CommandType.Text);
            cbxLoai_Ap_Km.ValueMember = "ID";
            cbxLoai_Ap_Km.DisplayMember = "Value";


            cbxBreakBy.DataSource = SQLExec.ExecuteReturnDt(" sp_OM_GetCombovalue @Key = 'DiscountBreakBy'", CommandType.Text);
            cbxBreakBy.ValueMember = "ID";
            cbxBreakBy.DisplayMember = "Value";


            cbxHinh_Thuc_Km.DataSource = SQLExec.ExecuteReturnDt(" sp_OM_GetCombovalue @Key = 'DiscountFor'", CommandType.Text);
            cbxHinh_Thuc_Km.ValueMember = "ID";
            cbxHinh_Thuc_Km.DisplayMember = "Value";

        }
        #endregion

        #region Su kien 
        void cbxLoai_KM_SelectedValueChanged(object sender, EventArgs e)
        {
            string strType = cbxLoai_KM.SelectedValue.ToString();
            if (strType == "M") //Loại KM Tay
            {
                //cbxHinh_Thuc_Km.Enabled = false;
                cbxLoai_Ap_Km.Enabled = false;
                cbxBreakBy.Enabled = false;

                //numTSo_Luong.Enabled = false;
                //numTTien.Enabled = false;
            }
            else
            {
                cbxLoai_Ap_Km.Enabled = true;
                cbxBreakBy.Enabled = true;
            }
        }
        void cbxHinh_Thuc_Km_SelectedValueChanged(object sender, EventArgs e)
        {
            string strHinh_Thuc_Km = cbxHinh_Thuc_Km.SelectedValue.ToString();
            if (strHinh_Thuc_Km == "IN")
            {
                numTSo_Luong.Enabled = true;
                numTTien.Enabled = false;

                txtFilterNs.Text = "QtyAlloc";
            }
            else
            {
                numTSo_Luong.Enabled = false;
                numTTien.Enabled = true;

                txtFilterNs.Text = "AmtAlloc";
            }

        }
        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                this.isAccept = true;
                this.Close();
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            this.isAccept = false;
            this.Close();
        }
        void txtMa_Ngan_Sach_Validating(object sender, CancelEventArgs e)
        {

            string strValue = txtMa_Ngan_Sach.Text.Trim();

            bool bRequire = false;

            DataRow drLookup = Lookup.ShowQuickLookup("Ma_Ns", strValue, bRequire, txtFilterNs.Text + ">0", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Ngan_Sach.Text = string.Empty;
                numTSo_Luong.Value = 0;
                numTTien.Value = 0;
            }
            else
            {
                txtMa_Ngan_Sach.Text = drLookup["Ma_Ns"].ToString();
                numTSo_Luong.Value = Convert.ToDouble(drLookup["QtyAlloc"]);
                numTTien.Value = Convert.ToDouble(drLookup["AmtAlloc"]);
            }

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!Element.sysIs_Admin)
            {

                if (!Common.CheckPermission(this.Object_ID, enuPermission_Type.Allow_Edit))
                {
                    this.btgAccept.btAccept.Enabled = false;
                    return;
                }
                if (this.enuNew_Edit == enuEdit.Edit)
                    if (!Common.CheckDataLocked(Library.StrToDate(dteNgay_BD.Text)) || !Common.CheckDataLocked(Library.StrToDate(dteNgay_Kt.Text)))
                    {
                        this.btgAccept.btAccept.Enabled = false;
                        this.lbtInformation.Text = "Dữ liệu đã bị khóa, Liên hệ Admin hệ thống.";
                        return;
                    }
            }
            
        }
        

    }
    #endregion
}
