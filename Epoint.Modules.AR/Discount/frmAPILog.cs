using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using Epoint.Systems.Customizes;
using Epoint.Systems;
using Epoint.Systems.Elements;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Columns;

namespace Epoint.Modules.AR
{
    public partial class frmAPILog : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        private dgvGridControl dgvViewHD = new dgvGridControl();
        public string strMa_Ct = string.Empty;
        public string strStt = string.Empty;
        public string strMa_Px = string.Empty;
 
        DateTime Ngay_Ct;
        #endregion

        #region Contructor

        public frmAPILog()
        {
            InitializeComponent();

            btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

        }



        public void Load(string strLog)
        {
            this.txtLog.Text = strLog;

            ShowDialog();
        }

        #endregion

        #region Build, FillData


       

        #endregion

        #region Su kien

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    
                    break;
            }


            base.OnKeyDown(e);
        }
      
        void btAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}