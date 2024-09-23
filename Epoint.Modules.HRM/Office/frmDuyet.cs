using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;

namespace Epoint.Modules.HRM
{
	public partial class frmDuyet : Epoint.Systems.Customizes.frmEdit
	{
        public string strTableName = string.Empty;
        public string strColumnName = string.Empty;
        public string strIdent00 = string.Empty;

		public frmDuyet()
		{
			InitializeComponent();

			this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);

			this.chkDuyet.CheckedChanged += new EventHandler(chkDuyet_CheckedChanged);

			this.btSave.Click += new EventHandler(btSave_Click);
			this.btExit.Click += new EventHandler(btExit_Click);
		}

		public void Load(DataRow drEdit)
		{
			this.drEdit = drEdit;

			Common.ScaterMemvar(this, ref drEdit);

			chkDuyet.Enabled = !chkDuyet.Checked;

			btSave.Enabled = false;

			this.ShowDialog();
		}

		private bool FormCheckValid()
		{			
			return true;
		}

		private bool Save()
		{
            //frmPmTb frmPmTb = new frmPmTb();
            //frmPmTb

			string strSQLExec = string.Empty;
			Hashtable htPara;

            //drCurrent = ((DataRowView)bdsPmTb.Current).Row;

			//Lưu phần Checked
			if (chkDuyet.Enabled)
			{
                if (strColumnName == "DUYET" && strTableName == "HRKHCV")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET", chkDuyet.Checked);
                    htPara.Add("DUYET_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE HRKHCV SET Duyet = @Duyet, Duyet_Log = @Duyet_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet"] = chkDuyet.Checked;
                        drEdit["Duyet_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET" && strTableName == "L14TDCV")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET", chkDuyet.Checked);
                    htPara.Add("DUYET_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14TDCV SET Duyet = @Duyet, Duyet_Log = @Duyet_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet"] = chkDuyet.Checked;
                        drEdit["Duyet_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_GD" && strTableName == "L14PMTB")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_GD", chkDuyet.Checked);
                    htPara.Add("DUYET_GD_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14PMTB SET Duyet_GD = @Duyet_GD, Duyet_GD_Log = @Duyet_GD_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_GD"] = chkDuyet.Checked;
                        drEdit["Duyet_GD_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_HCNS" && strTableName == "L14PMTB")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_HCNS", chkDuyet.Checked);
                    htPara.Add("DUYET_HCNS_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14PMTB SET Duyet_HCNS = @Duyet_HCNS, Duyet_HCNS_Log = @Duyet_HCNS_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_HCNS"] = chkDuyet.Checked;
                        drEdit["Duyet_HCNS_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_NHAN" && strTableName == "L14PMTB")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_NHAN", chkDuyet.Checked);
                    htPara.Add("DUYET_NHAN_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14PMTB SET Duyet_Nhan = @Duyet_Nhan, Duyet_Nhan_Log = @Duyet_Nhan_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_Nhan"] = chkDuyet.Checked;
                        drEdit["Duyet_Nhan_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_TRA" && strTableName == "L14PMTB")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_TRA", chkDuyet.Checked);
                    htPara.Add("DUYET_TRA_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14PMTB SET Duyet_Tra = @Duyet_Tra, Duyet_Tra_Log = @Duyet_Tra_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_Tra"] = chkDuyet.Checked;
                        drEdit["Duyet_Tra_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_GD" && strTableName == "L14NGHIPHEP")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_GD", chkDuyet.Checked);
                    htPara.Add("DUYET_GD_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14NGHIPHEP SET Duyet_GD = @Duyet_GD, Duyet_GD_Log = @Duyet_GD_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_GD"] = chkDuyet.Checked;
                        drEdit["Duyet_GD_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_NQL" && strTableName == "L14NGHIPHEP")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_NQL", chkDuyet.Checked);
                    htPara.Add("DUYET_NQL_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14NGHIPHEP SET Duyet_NQL = @Duyet_NQL, Duyet_NQL_Log = @Duyet_NQL_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_NQL"] = chkDuyet.Checked;
                        drEdit["Duyet_NQL_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_GD" && strTableName == "L14PMVPP")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_GD", chkDuyet.Checked);
                    htPara.Add("DUYET_GD_LOG", txtDuyet_Log.Text);
                    htPara.Add("STT", drEdit["Stt"]);

                    strSQLExec = @"UPDATE L14PMVPP SET Duyet_GD = @Duyet_GD, Duyet_GD_Log = @Duyet_GD_Log WHERE Stt = @Stt";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_GD"] = chkDuyet.Checked;
                        drEdit["Duyet_GD_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET_HCNS" && strTableName == "L14PMVPP")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET_HCNS", chkDuyet.Checked);
                    htPara.Add("DUYET_HCNS_LOG", txtDuyet_Log.Text);
                    htPara.Add("STT", drEdit["Stt"]);

                    strSQLExec = @"UPDATE L14PMVPP SET Duyet_HCNS = @Duyet_HCNS, Duyet_HCNS_Log = @Duyet_HCNS_Log WHERE Stt = @Stt";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet_HCNS"] = chkDuyet.Checked;
                        drEdit["Duyet_HCNS_Log"] = txtDuyet_Log.Text;
                    }
                }
                else if (strColumnName == "DUYET" && strTableName == "L14LTDX")
                {
                    htPara = new Hashtable();
                    htPara.Add("DUYET", chkDuyet.Checked);
                    htPara.Add("DUYET_LOG", txtDuyet_Log.Text);
                    htPara.Add("IDENT00", drEdit["Ident00"]);

                    strSQLExec = @"UPDATE L14LTDX SET Duyet = @Duyet, Duyet_Log = @Duyet_Log WHERE Ident00 = @Ident00";

                    if (SQLExec.Execute(strSQLExec, htPara, CommandType.Text))
                    {
                        drEdit["Duyet"] = chkDuyet.Checked;
                        drEdit["Duyet_Log"] = txtDuyet_Log.Text;
                    }
                }
			}
			return true;
		}

        void Insert()
        {
            if (strTableName == "L14NGHIPHEP" && (bool)drEdit["Duyet_GD"] && (bool)drEdit["Duyet_NQL"])
            {
                string strDelete = "DELETE FROM HRKHCV WHERE NghiPhep_Ref = " + drEdit["Ident00"].ToString();
                SQLExec.Execute(strDelete);
                
                DataRow drKhCv = SQLExec.ExecuteReturnDt("SELECT * FROM HRKHCV WHERE 0 = 1").NewRow();
                Common.SetDefaultDataRow(ref drKhCv);
                drKhCv["Ngay"] = drEdit["Tu_Ngay"];
                drKhCv["Ma_CbNv"] = drEdit["Ma_CbNv"];
                drKhCv["Noi_Dung"] = drEdit["Ly_Do"];
                drKhCv["Ma_CbNv_Ht"] = drEdit["Ma_CbNv_Ht"];
                drKhCv["Ngay_DkHt"] = drEdit["Den_Ngay"];
                drKhCv["KHCV_Ref"] = drEdit["Ident00"];

                DataTool.SQLUpdate(Epoint.Systems.enuEdit.New, "HRKHCV", ref drKhCv);
            }
        }

		void chkDuyet_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDuyet.Checked)
				this.txtDuyet_Log.Text = Common.GetCurrent_Log();
			else
				this.txtDuyet_Log.Text = string.Empty;

			this.btSave.Enabled = true;
		}

		void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Common.ScaterMemvar(this, ref drEdit);

			btSave.Enabled = false;
		}

		void btSave_Click(object sender, EventArgs e)
		{
			if (this.FormCheckValid())
			{
				this.Save();
                this.Insert();
				this.Close();
			}
		}

		void btExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

            //if (!Element.sysIs_Admin)
            //{
            //    if (!Common.CheckPermission("DUYET", Epoint.Systems.enuPermission_Type.Allow_Access))
            //        tabControl1.TabPages.Remove(tabPage3);
            //}
		}
	}
}
