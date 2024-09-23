using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.HRM
{
    public partial class frmChamCong_Edit : frmEdit
    {
        #region Phuong thuc

        public frmChamCong_Edit()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
            txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
            txtMa_ViTri.Validating += new CancelEventHandler(txtMa_ViTri_Validating);
        }

        public void Load(enuEdit enuNew_Edit, DataRow drEdit)
        {
            this.drEdit = drEdit;
            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            Common.ScaterMemvar(this, ref drEdit);

            BindingLanguage();
            LoadDicName();

            this.ShowDialog();
        }

        private void LoadDicName()
        {
            //txtMa_CbNv
            if (txtMa_CbNv.Text.Trim() != string.Empty)
            {
                txtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", txtMa_CbNv.Text.Trim());
                dicName.SetValue(txtTen_CbNv.Name, txtTen_CbNv.Text);
            }
            else
                lbtTen_Bp.Text = string.Empty;

            //txtMa_Bp
            if (txtMa_Bp.Text.Trim() != string.Empty)
            {
                lbtTen_Bp.Text = DataTool.SQLGetNameByCode("LIBOPHAN", "Ma_Bp", "Ten_Bp", txtMa_Bp.Text.Trim());
                dicName.SetValue(lbtTen_Bp.Name, lbtTen_Bp.Text);
            }
            else
                lbtTen_Bp.Text = string.Empty;

            //txtMa_ViTri
            if (txtMa_ViTri.Text.Trim() != string.Empty)
            {
                lbtTen_ViTri.Text = DataTool.SQLGetNameByCode("HRVITRI", "Ma_ViTri", "Ten_ViTri", txtMa_ViTri.Text.Trim());
                dicName.SetValue(lbtTen_ViTri.Name, lbtTen_ViTri.Text);
            }
            else
                lbtTen_ViTri.Text = string.Empty;
        }

        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = true;
            txtMa_CbNv.strLookupKeyFilter = "Is_Van_Tay = 0";

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CbNv.Text = string.Empty;
                txtTen_CbNv.Text = string.Empty;
            }
            else
            {
                txtMa_CbNv.Text = drLookup["Ma_CbNv"].ToString();
                txtTen_CbNv.Text = drLookup["Ten_CbNv"].ToString();
            }

            dicName.SetValue(txtTen_CbNv.Name, txtTen_CbNv.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_Bp_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Bp.Text.Trim();
            bool bRequire = false;

           //
            DataRow drLookup = Lookup.ShowLookup("Ma_Bp", strValue, bRequire, "", "Nh_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Bp.Text = string.Empty;
                lbtTen_Bp.Text = string.Empty;
            }
            else
            {
                txtMa_Bp.Text = drLookup["Ma_Bp"].ToString();
                lbtTen_Bp.Text = drLookup["Ten_Bp"].ToString();
            }

            dicName.SetValue(lbtTen_Bp.Name, lbtTen_Bp.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_ViTri_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_ViTri.Text.Trim();
            bool bRequire = false;

            frmViTri frmLookup = new frmViTri();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "HRVITRI", "Ma_ViTri", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_ViTri.Text = string.Empty;
                lbtTen_ViTri.Text = string.Empty;
            }
            else
            {
                txtMa_ViTri.Text = drLookup["Ma_ViTri"].ToString();
                lbtTen_ViTri.Text = drLookup["Ten_ViTri"].ToString();
            }

            dicName.SetValue(lbtTen_ViTri.Name, lbtTen_ViTri.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        public bool FormCheckValid()
        {
            bool bvalid = true;
            return bvalid;
        }

        public bool Save()
        {
            Common.GatherMemvar(this, ref drEdit);

            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;

            //Luu xuong CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "HRTUYENDUNGKH", ref drEdit))
                return false;

            return true;
        }
        #endregion

        #region Su kien
        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                isAccept = true;
                this.Close();
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            isAccept = false;
            this.Close();
        }
        #endregion
    }
}
