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
    public partial class frmConfigLog : frmView
    {
        #region Phuong thuc

        DataTable dtChamCong;
        DataTable dtQTCT;

        BindingSource bdsChamCong = new BindingSource();
        BindingSource bdsQTCT = new BindingSource();

        public frmConfigLog()
        {
            InitializeComponent();

            //this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            //this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            //this.btLoadData.Click += new EventHandler(btLoadData_Click);
        }

        public override void Load()
        {
            Build();
            FillData();
            BindingLanguage();
            LoadDicName();
            //this.dteNgay_Ct1.Text = DateTime.Now.ToShortDateString();
            //this.dteNgay_Ct2.Text = DateTime.Now.ToShortDateString();


            //this.txtTime1.Text = string.Format(DateTime.Now.ToLongTimeString(), "hh:mm:ss");
            this.Show();
        }

        private void LoadDicName()
        {

        }
        private void Build()
        {
           
          
        }
        private void FillData()
        {
           
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


        #endregion

       

    }
}
