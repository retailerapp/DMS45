using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.HRM
{
    public partial class frmViewTransaction : frmView
    {
        #region Phuong thuc

        DataTable dtChamCong;
        DataTable dtQTCT;

        BindingSource bdsChamCong = new BindingSource();
        BindingSource bdsQTCT = new BindingSource();

        public frmViewTransaction()
        {
            InitializeComponent();

            //this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            //this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            this.btLoadData.Click += new EventHandler(btLoadData_Click);
            this.txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
        }

        public override void Load()
        {
            Build();
            FillData();
            BindingLanguage();
            LoadDicName();
            this.dteNgay_Ct1.Text = DateTime.Now.ToShortDateString();
            this.dteNgay_Ct2.Text = DateTime.Now.ToShortDateString();


            //this.txtTime1.Text = string.Format(DateTime.Now.ToLongTimeString(), "hh:mm:ss");
            this.Show();
        }

        private void LoadDicName()
        {

        }
        private void Build()
        {
           
            dgvChamCong.strZone = "HRDATACHAMCONG";
            dgvChamCong.BuildGridView();

           
        }
        private void FillData()
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("MA_CBNV", txtMa_CbNv.Text);
            htPara.Add("NGAY_CT1", Library.StrToDate(dteNgay_Ct1.Text));
            htPara.Add("NGAY_CT2", Library.StrToDate(dteNgay_Ct2.Text));
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);
            //                    strSQL = @"DELETE HRCHAMCONG WHERE TranText = @TRANTEXT
            //                                INSERT HRCHAMCONG(TranText,FileSource,Ma_Dvcs) VALUES(@TRANTEXT,@FILESOURCE,@MA_DVCS)";
            dtChamCong = SQLExec.ExecuteReturnDt("sp_rptDataChamCong", htPara, CommandType.StoredProcedure);
            bdsChamCong.DataSource = dtChamCong;
            dgvChamCong.DataSource = bdsChamCong;

            ExportControl = dgvChamCong;
        }
        public bool FormCheckValid()
        {
            bool bvalid = true;

            return bvalid;
        }

        public bool Save()
        {

            return true;
        }
        #endregion

        #region Su kien
        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                this.Close();
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       

        private void btLoadData_Click(object sender, EventArgs e)
        {
            FillData();
        }

        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = false;

            frmQuickLookup frmLookup = new frmQuickLookup();
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "");

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

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        #endregion

       

    }
}
