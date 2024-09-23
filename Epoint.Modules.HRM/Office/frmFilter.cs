using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Data;
using Epoint.Systems.Controls;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Systems.Librarys;
using Epoint.Lists;

namespace Epoint.Modules
{
	public partial class frmFilter : Epoint.Systems.Customizes.frmEdit
	{
		public frmFilter()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

			txtTk.Validating += new CancelEventHandler(txtTk_Validating);
			txtTk_Du.Validating += new CancelEventHandler(txtTk_Du_Validating);
			txtMa_Thue.Validating += new CancelEventHandler(txtMa_Thue_Validating);
			txtMa_Hd.Validating += new CancelEventHandler(txtMa_Hd_Validating);
			txtMa_Dt.Validating += new CancelEventHandler(txtMa_Dt_Validating);
			txtMa_Km.Validating += new CancelEventHandler(txtMa_Km_Validating);
			txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
			txtMa_Sp.Validating += new CancelEventHandler(txtMa_Sp_Validating);
			txtMa_Kho.Validating += new CancelEventHandler(txtMa_Kho_Validating);
			txtMa_Vt.Validating += new CancelEventHandler(txtMa_Vt_Validating);
			txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
			txtMa_Job.Validating += new CancelEventHandler(txtMa_Job_Validating);
			txtMa_Tc.Validating += new CancelEventHandler(txtMa_Tc_Validating);
			txtMa_Kv.Validating += new CancelEventHandler(txtMa_Kv_Validating);
			txtMa_So_PO.Validating += new CancelEventHandler(txtMa_So_PO_Validating);
		}

		public new void Load(DataRow drEdit)
		{
			this.drEdit = drEdit;
			this.Tag = "FILTER";
			this.txtMa_Tte.InputMask = "," + (string)Parameters.GetParaValue("MA_TTE_LIST");
			this.ActiveControl = dteNgay_Ct1;

			Common.ScaterMemvar(this, ref drEdit);

			BindingLanguage();
			LoadDicName();

			this.ShowDialog();
		}

		private void LoadDicName()
		{
			//txtTk
			if (txtTk.Text.Trim() != string.Empty)
			{
				lbtTen_Tk.Text = DataTool.SQLGetNameByCode("L81DmTk", "Tk", "Ten_Tk", txtTk.Text.Trim());
				dicName.SetValue(lbtTen_Tk.Name, lbtTen_Tk.Text);
			}
			else
				lbtTen_Tk.Text = string.Empty;

			//txtTk_Du
			if (txtTk_Du.Text.Trim() != string.Empty)
			{
				lbtTen_Tk_Du.Text = DataTool.SQLGetNameByCode("L81DmTk", "Tk", "Ten_Tk", txtTk_Du.Text.Trim());
				dicName.SetValue(lbtTen_Tk_Du.Name, lbtTen_Tk_Du.Text);
			}
			else
				lbtTen_Tk_Du.Text = string.Empty;

			//txtMa_Hd
			if (txtMa_Hd.Text.Trim() != string.Empty)
			{
				lbtTen_Hd.Text = DataTool.SQLGetNameByCode("L81DmHd", "Ma_Hd", "Ten_Hd", txtMa_Hd.Text.Trim());
				dicName.SetValue(lbtTen_Hd.Name, lbtTen_Hd.Text);
			}
			else
				lbtTen_Hd.Text = string.Empty;

			//txtMa_Dt
			if (txtMa_Dt.Text.Trim() != string.Empty)
			{
				lbtTen_Dt.Text = DataTool.SQLGetNameByCode("L81DmDt", "Ma_Dt", "Ten_Dt", txtMa_Dt.Text.Trim());
				dicName.SetValue(lbtTen_Dt.Name, lbtTen_Dt.Text);
			}
			else
				lbtTen_Dt.Text = string.Empty;

			//txtMa_Km
			if (txtMa_Km.Text.Trim() != string.Empty)
			{
				lbtTen_Km.Text = DataTool.SQLGetNameByCode("L81DmKm", "Ma_Km", "Ten_Km", txtMa_Km.Text.Trim());
				dicName.SetValue(lbtTen_Km.Name, lbtTen_Km.Text);
			}
			else
				lbtTen_Km.Text = string.Empty;

			//txtMa_Bp
			if (txtMa_Bp.Text.Trim() != string.Empty)
			{
				lbtTen_Bp.Text = DataTool.SQLGetNameByCode("L81DMBP", "Ma_Bp", "Ten_Bp", txtMa_Bp.Text.Trim());
				dicName.SetValue(lbtTen_Bp.Name, lbtTen_Bp.Text);
			}
			else
				lbtTen_Bp.Text = string.Empty;

			//txtMa_Sp
			if (txtMa_Sp.Text.Trim() != string.Empty)
			{
				lbtTen_Sp.Text = DataTool.SQLGetNameByCode("L81DmSp", "Ma_Sp", "Ten_Sp", txtMa_Sp.Text.Trim());
				dicName.SetValue(lbtTen_Sp.Name, lbtTen_Sp.Text);
			}
			else
				lbtTen_Sp.Text = string.Empty;

			//txtMa_Thue
			if (txtMa_Thue.Text.Trim() != string.Empty)
			{
				lbtTen_Thue.Text = DataTool.SQLGetNameByCode("L81DmThue", "Ma_Thue", "Ten_Thue", txtMa_Thue.Text.Trim());
				dicName.SetValue(lbtTen_Thue.Name, lbtTen_Thue.Text);
			}
			else
				lbtTen_Thue.Text = string.Empty;

			//txtMa_Kho
			if (txtMa_Kho.Text.Trim() != string.Empty)
			{
				lbtTen_Kho.Text = DataTool.SQLGetNameByCode("L81DmKho", "Ma_Kho", "Ten_Kho", txtMa_Kho.Text.Trim());
				dicName.SetValue(lbtTen_Kho.Name, lbtTen_Kho.Text);
			}
			else
				lbtTen_Kho.Text = string.Empty;

			//txtMa_Vt
			if (txtMa_Vt.Text.Trim() != string.Empty)
			{
				lbtTen_Vt.Text = DataTool.SQLGetNameByCode("L81DmVt", "Ma_Vt", "Ten_Vt", txtMa_Vt.Text.Trim());
				dicName.SetValue(lbtTen_Vt.Name, lbtTen_Vt.Text);
			}
			else
				lbtTen_Vt.Text = string.Empty;

			//txtMa_CbNv
			if (txtMa_CbNv.Text.Trim() != string.Empty)
			{
				lbtTen_CbNv.Text = DataTool.SQLGetNameByCode("L81DMCBNV", "Ma_CbNv", "Ten_CbNv", txtMa_Thue.Text.Trim());
				dicName.SetValue(lbtTen_CbNv.Name, lbtTen_CbNv.Text);
			}
			else
				lbtTen_CbNv.Text = string.Empty;

			//txtMa_Job
			if (txtMa_Job.Text.Trim() != string.Empty)
			{
				lbtTen_Job.Text = DataTool.SQLGetNameByCode("L81DmJob", "Ma_Job", "Ten_Job", txtMa_Job.Text.Trim());
				dicName.SetValue(lbtTen_Job.Name, lbtTen_Job.Text);
			}
			else
				lbtTen_Job.Text = string.Empty;

			//txtMa_Tc
			if (txtMa_Tc.Text.Trim() != string.Empty)
			{
				lbtTen_Tc.Text = DataTool.SQLGetNameByCode("L81DmTc", "Ma_Tc", "Ten_Tc", txtMa_Tc.Text.Trim());
				dicName.SetValue(lbtTen_Tc.Name, lbtTen_Tc.Text);
			}
			else
				lbtTen_Tc.Text = string.Empty;

			//txtMa_Kv
			if (txtMa_Kv.Text.Trim() != string.Empty)
			{
				lbtTen_Kv.Text = DataTool.SQLGetNameByCode("L81DmKv", "Ma_Kv", "Ten_Kv", txtMa_Kv.Text.Trim());
				dicName.SetValue(lbtTen_Kv.Name, lbtTen_Kv.Text);
			}
			else
				lbtTen_Kv.Text = string.Empty;

		}

		private void btAccept_Click(object sender, EventArgs e)
		{
			Common.GatherMemvar(this, ref drEdit);

			isAccept = true;
			this.Close();

		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			isAccept = false;
			this.Close();
		}

		void txtTk_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup( "Tk", strValue, bRequire, "");

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

			dicName.SetValue(lbtTen_Tk.Name, lbtTen_Tk.Text);
		}
		void txtTk_Du_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtTk_Du.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup( "Tk", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtTk_Du.Text = string.Empty;
				lbtTen_Tk_Du.Text = string.Empty;
			}
			else
			{
				txtTk_Du.Text = drLookup["Tk"].ToString();
				lbtTen_Tk_Du.Text = drLookup["Ten_Tk"].ToString();
			}

			dicName.SetValue(lbtTen_Tk_Du.Name, lbtTen_Tk_Du.Text);
		}
		void txtMa_Hd_Validating(object sender, CancelEventArgs e)
		{	
			string strValue = txtMa_Hd.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Hd", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Hd.Text = string.Empty;
				lbtTen_Hd.Text = string.Empty;
			}
			else
			{
				txtMa_Hd.Text = drLookup["Ma_Hd"].ToString();
				lbtTen_Hd.Text = drLookup["Ten_Hd"].ToString();
			}

			dicName.SetValue(lbtTen_Hd.Name, lbtTen_Hd.Text);
		}
		void txtMa_Dt_Validating(object sender, CancelEventArgs e)
		{			
			string strValue = txtMa_Dt.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Dt", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Dt.Text = string.Empty;
				lbtTen_Dt.Text = string.Empty;
			}
			else
			{
				txtMa_Dt.Text = drLookup["Ma_Dt"].ToString();
				lbtTen_Dt.Text = drLookup["Ten_Dt"].ToString();
			}

			dicName[lbtTen_Dt.Name] = lbtTen_Dt.Text;
		}
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
				txtMa_Km.Text = string.Empty;
				lbtTen_Km.Text = string.Empty;
			}
			else
			{
				txtMa_Km.Text = drLookup["Ma_Km"].ToString();
				lbtTen_Km.Text = drLookup["Ten_Km"].ToString();
			}

			dicName.SetValue(lbtTen_Km.Name, lbtTen_Km.Text);
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
		}
		void txtMa_Sp_Validating(object sender, CancelEventArgs e)
		{	
			string strValue = txtMa_Sp.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Sp", strValue, bRequire, "");

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
		}

		void txtMa_Thue_Validating(object sender, CancelEventArgs e)
		{			
			string strValue = txtMa_Thue.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Thue", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Thue.Text = string.Empty;
				lbtTen_Thue.Text = string.Empty;
			}
			else
			{
				txtMa_Thue.Text = drLookup["Ma_Thue"].ToString();
				lbtTen_Thue.Text = drLookup["Ten_Thue"].ToString();
			}

			dicName.SetValue(lbtTen_Thue.Name, lbtTen_Thue.Text);
		}
		void txtMa_Kho_Validating(object sender, CancelEventArgs e)
		{			
			string strValue = txtMa_Kho.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Kho", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Kho.Text = string.Empty;
				lbtTen_Kho.Text = string.Empty;
			}
			else
			{
				txtMa_Kho.Text = drLookup["Ma_Kho"].ToString();
				lbtTen_Kho.Text = drLookup["Ten_Kho"].ToString();
			}

			dicName.SetValue(lbtTen_Kho.Name, lbtTen_Kho.Text);
		}
		void txtMa_Vt_Validating(object sender, CancelEventArgs e)
		{			
			string strValue = txtMa_Vt.Text.Trim();
			bool bRequire = false;

			//frmVatTu frmLookup = new frmVatTu();
			DataRow drLookup = Lookup.ShowLookup("Ma_Vt", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Vt.Text = string.Empty;
				lbtTen_Vt.Text = string.Empty;
			}
			else
			{
				txtMa_Vt.Text = drLookup["Ma_Vt"].ToString();
				lbtTen_Vt.Text = drLookup["Ten_Vt"].ToString();
			}

			dicName.SetValue(lbtTen_Vt.Name, lbtTen_Vt.Text);
		}
		void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtMa_CbNv.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_CbNv.Text = string.Empty;
				lbtTen_CbNv.Text = string.Empty;
			}
			else
			{
				txtMa_CbNv.Text = drLookup["Ma_CbNv"].ToString();
				lbtTen_CbNv.Text = drLookup["Ten_CbNv"].ToString();
			}

			dicName.SetValue(lbtTen_CbNv.Name, lbtTen_CbNv.Text);
		}
		void txtMa_Job_Validating(object sender, CancelEventArgs e)
		{
			string strValue = txtMa_Job.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Job", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Job.Text = string.Empty;
				lbtTen_Job.Text = string.Empty;
			}
			else
			{
				txtMa_Job.Text = drLookup["Ma_Job"].ToString();
				lbtTen_Job.Text = drLookup["Ten_Job"].ToString();
			}

			dicName.SetValue(lbtTen_Job.Name, lbtTen_Job.Text);
		}
		void txtMa_Tc_Validating(object sender, CancelEventArgs e)
		{
			
		}
		void txtMa_Kv_Validating(object sender, CancelEventArgs e)
		{	
			string strValue = txtMa_Kv.Text.Trim();
			bool bRequire = false;

			//
			DataRow drLookup = Lookup.ShowLookup("Ma_Kv", strValue, bRequire, "");

			if (bRequire && drLookup == null)
				e.Cancel = true;

			if (drLookup == null)
			{
				txtMa_Kv.Text = string.Empty;
				lbtTen_Kv.Text = string.Empty;
			}
			else
			{
				txtMa_Kv.Text = drLookup["Ma_Kv"].ToString();
				lbtTen_Kv.Text = drLookup["Ten_Kv"].ToString();
			}

			dicName.SetValue(lbtTen_Kv.Name, lbtTen_Kv.Text);
		}

		void txtMa_So_PO_Validating(object sender, CancelEventArgs e)
		{			
			

		}

	}
}
