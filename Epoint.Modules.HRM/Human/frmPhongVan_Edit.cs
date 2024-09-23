using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Controls;
using Epoint.Systems;
using Epoint.Systems.Commons;
using Epoint.Systems.Data;
using Epoint.Systems.Librarys;
using Epoint.Lists;

namespace Epoint.Modules.HRM
{
	public partial class frmPhongVan_Edit : Epoint.Systems.Customizes.frmEdit
	{
		public frmPhongVan_Edit()
		{
			InitializeComponent();

			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
            txtMa_ViTri.Validating += new CancelEventHandler(txtMa_ViTri_Validating);
            txtMa_Sp.Validating += new CancelEventHandler(txtMa_Sp_Validating);     
		}

		new public void Load(enuEdit enuNew_Edit, DataRow drEdit)
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
                lbtTen_Bp.Text = string.Empty;
            //txtMa_Sp
            if (txtMa_Sp.Text.Trim() != string.Empty)
            {
                lbtTen_Sp.Text = DataTool.SQLGetNameByCode("LISANPHAM", "Ma_Sp", "Ten_Sp", txtMa_Sp.Text.Trim());
                dicName.SetValue(lbtTen_Sp.Name, lbtTen_Sp.Text);
            }
            else
                lbtTen_Sp.Text = string.Empty;
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
                lbtTen_Sp.Text = string.Empty;
            }
            else
            {
                txtMa_Sp.Text = drLookup["Ma_Sp"].ToString();
                lbtTen_Sp.Text = drLookup["Ten_Sp"].ToString();
            }

            dicName.SetValue(lbtTen_Sp.Name, lbtTen_Sp.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
		private bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);			

			if (!DataTool.SQLUpdate(enuNew_Edit, "HRPHONGVAN", ref drEdit))
				return false;

			return true;
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
	}
}
