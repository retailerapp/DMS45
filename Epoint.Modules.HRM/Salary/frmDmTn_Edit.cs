using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Customizes;
using Epoint.Systems.Data;
using Epoint.Lists;

namespace Epoint.Modules.Salary
{
	public partial class frmDmTn_Edit : Epoint.Systems.Customizes.frmEdit
	{
		public frmDmTn_Edit()
		{
			InitializeComponent();

			txtTk_No.Validating += new CancelEventHandler(txtTk_No_Validating);
			txtTk_Co.Validating += new CancelEventHandler(txtTk_Co_Validating);
			txtMa_Km.Validating += new CancelEventHandler(txtMa_Km_Validating);

			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
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
			if (txtTk_No.Text != string.Empty)
				lbtTen_Tk_No.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_No.Text);
			else
				lbtTen_Tk_No.Text = string.Empty;

			if (txtTk_Co.Text != string.Empty)
				lbtTen_Tk_Co.Text = DataTool.SQLGetNameByCode("LITAIKHOAN", "Tk", "Ten_Tk", txtTk_Co.Text);
			else
				lbtTen_Tk_Co.Text = string.Empty;

			if (txtMa_Km.Text != string.Empty)
				lbtTen_Km.Text = DataTool.SQLGetNameByCode("LIKHOANMUC", "Ma_Km", "Ten_Km", txtMa_Km.Text);
			else
				lbtTen_Km.Text = string.Empty;
		}

		private bool CheckFormValid()
		{
			if (this.txtMa_Tn.Text == string.Empty)
			{
				Common.MsgCancel(Languages.GetLanguage("Ma_Tn") + " " + Languages.GetLanguage("Not_Empty"));
				return false;
			}

			return true;
		}

		private bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);

			if (!this.CheckFormValid())
				return false;

			if (!DataTool.SQLUpdate(enuNew_Edit, "HRPARALIST", ref drEdit))
				return false;

			//Doi ma
			if (this.enuNew_Edit == enuEdit.Edit)
				DataTool.SQLChangeID("MA_TN", drEdit);

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

		#region Tk_No
		
		void txtTk_No_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk_No.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				lbtTen_Tk_No.Text = string.Empty;
				lbtTen_Tk_No.Text = string.Empty;
			}
			else
			{
				txtTk_No.Text = ((string)drLookup["Tk"]).Trim();
				lbtTen_Tk_No.Text = ((string)drLookup["Ten_Tk"]).Trim();
			}

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
		}

		#endregion

		#region Tk_Co

		void txtTk_Co_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk_Co.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				lbtTen_Tk_Co.Text = string.Empty;
				lbtTen_Tk_Co.Text = string.Empty;
			}
			else
			{
				txtTk_Co.Text = ((string)drLookup["Tk"]).Trim();
				lbtTen_Tk_Co.Text = ((string)drLookup["Ten_Tk"]).Trim();
			}

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
		}
		#endregion

		#region Ma_Km

		void txtMa_Km_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtMa_Km.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Km", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				lbtTen_Km.Text = string.Empty;
				lbtTen_Km.Text = string.Empty;
			}
			else
			{
				txtMa_Km.Text = ((string)drLookup["Ma_Km"]).Trim();
				lbtTen_Km.Text = ((string)drLookup["Ten_Km"]).Trim();
			}

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
		}
		#endregion

	}
}
