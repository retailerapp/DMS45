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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Epoint.Lists;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Epoint.Modules.AR
{
    public partial class frmInvoiceImportCustomer_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        private dgvControl dgvViewHD = new dgvControl();
        public string strSo_Ct = string.Empty;
        public string strStt = string.Empty;
        public string strMa_Px = string.Empty;
        BindingSource bdsDoiTuong = new BindingSource();
        DataTable dtListCustomer;
        public DataTable dtStockAvail;
        DateTime Ngay_Ct;
        #endregion

        #region Contructor

        public frmInvoiceImportCustomer_View()
        {
            InitializeComponent();

            btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            dgvViewHD.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvViewHD_CellMouseClick);
        }



        public void Load(DataTable dtLiDoiTuong)
        {

            //this.strMa_Ct = strMa_Ct;
            this.strSo_Ct = strSo_Ct;
            this.dtListCustomer = dtLiDoiTuong;
            this.Ngay_Ct = Ngay_Ct;
            DataColumn newCol = new DataColumn("IsNew", typeof(Boolean));
            newCol.DefaultValue = true;
            this.dtListCustomer.Columns.Add(newCol);
            Build();
            FillData();
            BindingLanguage();

            ShowDialog();
        }
        #endregion

        #region Build, FillData


        private void Build()
        {
            dgvViewHD.strZone = "DoiTuong";
            dgvViewHD.ReadOnly = true;
            dgvViewHD.BuildGridView(this.isLookup);
            DevExpress.XtraGrid.Columns.GridColumn columns = new DevExpress.XtraGrid.Columns.GridColumn();
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "AddCustomer";
                button.HeaderText = "Add Customer";
                button.Text = "Thêm Mới";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dgvViewHD.Columns.Add(button);
            }

            dgvViewHD.Dock = DockStyle.Fill;
            this.ListOrder.Controls.Add(dgvViewHD);
        }

        private void FillData()
        {
            bdsDoiTuong = new BindingSource();

            //DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strSo_Ct);
            //string strTable_Ct = (string)drDmCt["Table_Ct"];

            //dtViewHD = GetDiscountDetail();

            dtViewHD = this.dtListCustomer;
            bdsDoiTuong.DataSource = dtViewHD;
            dgvViewHD.DataSource = bdsDoiTuong;

            bdsDoiTuong.Position = 0;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsDoiTuong;


        }



        #endregion

        #region Su kien

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    DataRow drCurrent = ((DataRowView)bdsDoiTuong.Current).Row;
                    //    drCurrent["Chon"] = !(bool)drCurrent["Chon"];
                    break;
            }


            base.OnKeyDown(e);
        }
        void dgvViewHD_CellMouseClick(object sender, EventArgs e)
        {

            if (bdsDoiTuong.Position < 0)
                return;


        }
        void dgvViewHD_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            string strColumnName = dgvViewHD.Columns[e.ColumnIndex].Name;
            DataRow drCurrent = ((DataRowView)bdsDoiTuong.Current).Row;


            if (strColumnName == "CHON")
            {

                drCurrent["CHON"] = !Convert.ToBoolean(drCurrent["CHON"]);
                drCurrent.AcceptChanges();
            }

            if (e.ColumnIndex == dgvViewHD.Columns["AddCustomer"].Index)
            {
                //Copy hang hien tai
                if (bdsDoiTuong.Position >= 0)
                    Common.CopyDataRow(((DataRowView)bdsDoiTuong.Current).Row, ref drCurrent);
                else
                {
                    drCurrent = this.dtListCustomer.NewRow();
                }
                enuEdit enu = Convert.ToBoolean(drCurrent["IsNew"]) ? enuEdit.New : enuEdit.Edit;
                frmDoiTuong_Edit frmEdit = new frmDoiTuong_Edit();
                frmEdit.Load(enu, drCurrent);
                if (frmEdit.isAccept)
                {

                    

                    drCurrent["IsNew"] = false;
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsDoiTuong.Current).Row);
                    //dtDoiTuong.AcceptChanges();
                    drCurrent.AcceptChanges();
                }
                else
                    //dtDoiTuong.RejectChanges();
                    drCurrent.RejectChanges();

            }
        }
        void txtSo_Ct_TextChanged(object sender, EventArgs e)
        {

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