using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;

namespace Epoint.Lists
{
	public partial class frmKho_Edit : Epoint.Lists.frmEdit
	{		

        #region Phuong thuc

		public frmKho_Edit()
		{
			InitializeComponent();

            txtMa_Kho_Cha.Validating += new CancelEventHandler(txtMa_Kho_Cha_Validating);
            txtTk_Kho.Validating += new CancelEventHandler(txtTk_Kho_Validating);
            txtTk_Gv.Validating += new CancelEventHandler(txtTk_Gv_Validating);
        }

        public override void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{
			this.drEdit = drEdit;
			this.enuNew_Edit = enuNew_Edit;
			this.Tag = (char)enuNew_Edit + "," + this.Tag;

            //New: khi them moi thi khong can ke thua
            if (enuNew_Edit != enuEdit.New)
                Common.ScaterMemvar(this, ref drEdit);

            //Edit: Disable Ma_Kho
            if (enuNew_Edit == enuEdit.Edit)
                txtMa_Kho.Enabled = false;

			BindingLanguage();
			LoadDicName();

			this.ShowDialog();
		}

		private void LoadDicName()
		{
 
		}

		public override bool FormCheckValid()
        {
            bool bvalid = true ;
            if (txtMa_Kho.Text.Trim() == string.Empty)
            {
				Common.MsgOk(Languages.GetLanguage("Ma_Kho") + " " +
							  Languages.GetLanguage("Not_Null"));
                return false;
            }			

			if (txtTen_Kho.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Ten_Kho") + " " +
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

			//Luu xuong CSDL
			if (!DataTool.SQLUpdate(enuNew_Edit, "LIKHO", ref drEdit))
				return false;

            ////Sync data-------------
            //string Is_Sync = Convert.ToString(SQLExec.ExecuteReturnValue("SELECT Parameter_Value FROM SYSPARAMETER WHERE Parameter_ID = 'SYNC_BEGIN'"));
            //if (Is_Sync == "1")
            //{
            //    SqlConnection sqlCon = SQLExecSync1.GetNewSQLConnectionSync1();
            //    if (sqlCon.State != ConnectionState.Open)
            //    {
            //        SQLExec.Execute("UPDATE SYSPARAMETER SET Parameter_Value = 0 WHERE Parameter_ID = 'SYNC_BEGIN'");
            //        string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Quá trình đồng bộ đang bị gián đoạn. Vui lòng chờ trong ít phút !" : "The synchronization process is interrupted. Please wait a few minutes !";
            //        Common.MsgCancel(strMsg);
            //    }
            //    else
            //    {
            //        DataToolSync1.SQLUpdate(enuNew_Edit, "LIKHO", ref drEdit);
            //    }
            //}
            ////----------------------

            ////Doi ma
            //if (this.enuNew_Edit == enuEdit.Edit)
            //    DataTool.SQLChangeID("MA_KHO", drEdit);

            ////Sync data-------------            
            //if (Is_Sync == "1")
            //{
            //    SqlConnection sqlCon = SQLExecSync1.GetNewSQLConnectionSync1();
            //    if (sqlCon.State != ConnectionState.Open)
            //    {
            //        SQLExec.Execute("UPDATE SYSPARAMETER SET Parameter_Value = 0 WHERE Parameter_ID = 'SYNC_BEGIN'");
            //        string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Quá trình đồng bộ đang bị gián đoạn. Vui lòng chờ trong ít phút !" : "The synchronization process is interrupted. Please wait a few minutes !";
            //        Common.MsgCancel(strMsg);
            //    }
            //    else
            //    {
            //        DataToolSync1.SQLChangeID("MA_KHO", drEdit);
            //    }
            //}
            ////----------------------

			return true;
		}
        #endregion

        #region Su kien

        void txtMa_Kho_Cha_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Kho_Cha.Text.Trim();
            bool bRequire = false;

            frmKho frmLookup = new frmKho();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIKHO", "Ma_Kho", strValue, bRequire, "Nh_Cuoi = 0");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Kho_Cha.Text = string.Empty;
                lbtTen_Kho_Cha.Text = string.Empty;
            }
            else
            {
                txtMa_Kho_Cha.Text = ((string)drLookup["Ma_Kho"]).Trim();
                lbtTen_Kho_Cha.Text = ((string)drLookup["Ten_Kho"]).Trim();
            }

            dicName.SetValue(lbtTen_Kho_Cha.Name, lbtTen_Kho_Cha.Text);
        }
        /*
        private void txtTk_Vt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Vt.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Vt.Text = string.Empty;
                lbtTen_Tk_Vt.Text = string.Empty;
            }
            else
            {
                txtTk_Vt.Text = ((string)drLookup["Tk"]).Trim();
                lbtTen_Tk_Vt.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTen_Tk_Vt.Name, lbtTen_Tk_Vt.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        private void txtTk_Dt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Dt.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

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

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        */
        private void txtTk_Gv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Gv.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Gv.Text = string.Empty;
                lbtTen_Tk_Gv.Text = string.Empty;
            }
            else
            {
                txtTk_Gv.Text = ((string)drLookup["Tk"]).Trim();
                lbtTen_Tk_Gv.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTen_Tk_Gv.Name, lbtTen_Tk_Gv.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        private void txtTk_Kho_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Kho.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Kho.Text = string.Empty;
                lbtTk_Kho.Text = string.Empty;
            }
            else
            {
                txtTk_Kho.Text = ((string)drLookup["Tk"]).Trim();
                lbtTk_Kho.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTk_Kho.Name, lbtTk_Kho.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void txtTk_No_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_No.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_No.Text = string.Empty;
                lbtTk_No.Text = string.Empty;
            }
            else
            {
                txtTk_No.Text = ((string)drLookup["Tk"]).Trim();
                lbtTk_No.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTk_No.Name, lbtTk_No.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        private void txtTk_Dthu_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Dthu.Text.Trim();
            bool bRequire = false;

            frmTaiKhoan frmLookup = new frmTaiKhoan();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LITAIKHOAN", "Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Dthu.Text = string.Empty;
                lbtTk_Dthu.Text = string.Empty;
            }
            else
            {
                txtTk_Dthu.Text = ((string)drLookup["Tk"]).Trim();
                lbtTk_Dthu.Text = ((string)drLookup["Ten_Tk"]).Trim();
            }

            dicName.SetValue(lbtTk_Dthu.Name, lbtTk_Dthu.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        #endregion
    }
}