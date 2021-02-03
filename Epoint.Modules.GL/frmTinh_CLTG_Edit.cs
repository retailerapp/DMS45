﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;


namespace Epoint.Modules.GL
{
	public partial class frmTinh_CLTG_Edit : Epoint.Systems.Customizes.frmEdit
    {
        public frmTinh_CLTG_Edit()
        {
            InitializeComponent();

            txtTk.Validating += new CancelEventHandler(txtTk_Validating);
			txtTk_Lai.Validating += new CancelEventHandler(txtTk_LaiValidating);
			txtTk_Lo.Validating += new CancelEventHandler(txtTk_Lo_LaiValidating);

			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
        }

		public void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{
			this.enuNew_Edit = enuNew_Edit;
			this.drEdit = drEdit;
			this.Tag = (char)enuNew_Edit + "," + this.Tag;			

			Common.ScaterMemvar(this, ref drEdit);
			LoadDicName();

			this.ShowDialog();
		}

		private void LoadDicName()
		{
			//txtTk
			if (txtTk.Text.Trim() != string.Empty)
				lbtTen_Tk.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk.Text.Trim());
			else
				lbtTen_Tk.Text = string.Empty;

			//txtTk_Lai
			if (txtTk_Lai.Text.Trim() != string.Empty)
				lbtTen_Tk_Lai.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Lai.Text.Trim());
			else
				lbtTen_Tk_Lai.Text = string.Empty;

			//txtTk_Lo
			if (txtTk_Lo.Text.Trim() != string.Empty)
				lbtTen_Tk_Lo.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Lo.Text.Trim());
			else
				lbtTen_Tk_Lo.Text = string.Empty;
		}

		void txtTk_LaiValidating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk_Lai.Text.Trim();
			bool bRequire = true;

			Lists.frmTaiKhoan frmLookup = new Epoint.Lists.frmTaiKhoan();
			DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, null, "Tk_Cuoi = 1");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtTk_Lai.Text = string.Empty;
				lbtTen_Tk_Lai.Text = string.Empty;
			}
			else
			{
				txtTk_Lai.Text = drLookup["Tk"].ToString();
				lbtTen_Tk_Lai.Text = drLookup["Ten_Tk"].ToString();
			}

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
		}

		void txtTk_Lo_LaiValidating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk_Lo.Text.Trim();
			bool bRequire = true;

			Lists.frmTaiKhoan frmLookup = new Epoint.Lists.frmTaiKhoan();
			DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, null, "Tk_Cuoi = 1");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtTk_Lo.Text = string.Empty;
				lbtTen_Tk_Lo.Text = string.Empty;
			}
			else
			{
				txtTk_Lo.Text = drLookup["Tk"].ToString();
				lbtTen_Tk_Lo.Text = drLookup["Ten_Tk"].ToString();
			}

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
		}

        void txtTk_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk.Text.Trim();
            bool bRequire = true;

			Lists.frmTaiKhoan frmLookup = new Lists.frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk.Text = string.Empty;
                lbtTen_Tk.Text = string.Empty;
            }
            else
            {
                txtTk.Text = drLookup["Tk"].ToString();
                lbtTen_Tk.Text = drLookup["Ten_Tk"].ToString();
            }

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

		public bool FormCheckValid()
		{
			if (numSTt.Text.Trim() == string.Empty)
			{
				Common.MsgCancel(Languages.GetLanguage("Stt") + " " + Languages.GetLanguage("Cannot_Empty"));
				return false;
			}

			if (txtTk.Text.Trim() == string.Empty)
			{
				Common.MsgCancel(Languages.GetLanguage("Tk") + " " + Languages.GetLanguage("Cannot_Empty"));
				return false;
			}

			if (txtTk_Lai.Text.Trim() == string.Empty)
			{
				Common.MsgCancel(Languages.GetLanguage("Tk_Lai") + " " + Languages.GetLanguage("Cannot_Empty"));
				return false;
			}

			if (txtTk_Lo.Text.Trim() == string.Empty)
			{
				Common.MsgCancel(Languages.GetLanguage("Tk_Lo") + " " + Languages.GetLanguage("Cannot_Empty"));
				return false;
			}
			return true;
		}

		public bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);

			//Kiem tra Valid tren Form
			if (!FormCheckValid())
				return false;			

			if (!DataTool.SQLUpdate(this.enuNew_Edit, "GLCLTG", ref drEdit))
				return false;

			return true;
		}

		private void btAccept_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				isAccept = true;
				this.Close();
			}
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			isAccept = false;
			this.Close();
		}
		
    }
}
