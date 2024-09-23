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
    public partial class frmTuyenDungKH_Edit : frmEdit
    {
        #region Phuong thuc

        public frmTuyenDungKH_Edit()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
            txtMa_ViTri.Validating += new CancelEventHandler(txtMa_ViTri_Validating);
            txtMa_Sp.Validating += new CancelEventHandler(txtMa_Sp_Validating);
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
            //txtMa_Bp
            if (txtMa_Bp.Text.Trim() != string.Empty)
            {
                txtTen_Bp.Text = DataTool.SQLGetNameByCode("LIBOPHAN", "Ma_Bp", "Ten_Bp", txtMa_Bp.Text.Trim());
                dicName.SetValue(txtTen_Bp.Name, txtTen_Bp.Text);
            }
            else
                txtTen_Bp.Text = string.Empty;

            //txtMa_ViTri
            if (txtMa_ViTri.Text.Trim() != string.Empty)
            {
                txtTen_ViTri.Text = DataTool.SQLGetNameByCode("HRVITRI", "Ma_ViTri", "Ten_ViTri", txtMa_ViTri.Text.Trim());
                dicName.SetValue(txtTen_ViTri.Name, txtTen_ViTri.Text);
            }
            else
                txtTen_ViTri.Text = string.Empty;
            
            //txtMa_Sp
            if (txtMa_Sp.Text.Trim() != string.Empty)
            {
                txtTen_Sp.Text = DataTool.SQLGetNameByCode("LISANPHAM", "Ma_Sp", "Ten_Sp", txtMa_Sp.Text.Trim());
                dicName.SetValue(txtTen_Sp.Name, txtTen_Sp.Text);
            }
            else
                txtTen_Sp.Text = string.Empty;
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
                txtTen_Bp.Text = string.Empty;
            }
            else
            {
                txtMa_Bp.Text = drLookup["Ma_Bp"].ToString();
                txtTen_Bp.Text = drLookup["Ten_Bp"].ToString();
            }

            dicName.SetValue(txtTen_Bp.Name, txtTen_Bp.Text);

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
                txtTen_ViTri.Text = string.Empty;
            }
            else
            {
                txtMa_ViTri.Text = drLookup["Ma_ViTri"].ToString();
                txtTen_ViTri.Text = drLookup["Ten_ViTri"].ToString();
            }

            dicName.SetValue(txtTen_ViTri.Name, txtTen_ViTri.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        void txtMa_Sp_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Sp.Text.Trim();
            bool bRequire = false;

           //
            DataRow drLookup = Lookup.ShowLookup("Ma_Sp", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Sp.Text = string.Empty;
                txtTen_Sp.Text = string.Empty;
            }
            else
            {
                txtMa_Sp.Text = drLookup["Ma_Sp"].ToString();
                txtTen_Sp.Text = drLookup["Ten_Sp"].ToString();
            }

            dicName.SetValue(txtTen_Sp.Name, txtTen_Sp.Text);

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
