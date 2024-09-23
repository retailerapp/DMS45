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
	public partial class frmCongTrinh_Edit : frmEdit
	{
		#region Phuong thuc

		public frmCongTrinh_Edit()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            this.txtMa_CbNv.Validating +=new CancelEventHandler(txtMa_CbNv_Validating);
            this.txtMa_Sp.Validating += new CancelEventHandler(txtMa_Sp_Validating);
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
				txtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CBNV", "Ten_CbNv", txtMa_CbNv.Text.Trim());
			else
				txtTen_CbNv.Text = string.Empty;

            //txtMa_Sp
            if (txtMa_Sp.Text.Trim() != string.Empty)
                txtTen_Sp.Text = DataTool.SQLGetNameByCode("LISANPHAM", "Ma_Sp", "Ten_Sp", txtMa_Sp.Text.Trim());
            else
                txtTen_Sp.Text = string.Empty;
		}
        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = false;

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
        void txtMa_Sp_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Sp.Text.Trim();
            bool bRequire = false;

           
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
			if (!DataTool.SQLUpdate(enuNew_Edit, "HRCONGTRINH", ref drEdit))
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
