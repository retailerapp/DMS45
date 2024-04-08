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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace Epoint.Modules.AR
{
    public partial class frmDiscountDetail_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        private dgvGridControl dgvViewHD = new dgvGridControl();
        public string strMa_Ct = string.Empty;
        public string strStt = string.Empty;
        BindingSource bdsViewHD = new BindingSource();
        public DataTable dtVoucherSelect;
        //dgvControl dgvViewHD = new dgvControl();

        //string strMa_Ct = string.Empty;
        //string strKey = string.Empty;

        #endregion

        #region Contructor

        public frmDiscountDetail_View()
        {
            InitializeComponent();

            btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

        }



        public void Load(string strMa_Ct, string strStt)
        {

            this.strMa_Ct = strMa_Ct;
            this.strStt = strStt;
            Build();
            FillData();
            BindingLanguage();

            ShowDialog();
        }
        public void Load()
        {

            Build();
            FillData();
            BindingLanguage();

            ShowDialog();
        }
        #endregion

        #region Build, FillData

        private void Build()
        {
            dgvViewHD.strZone = "OM_DISCOUTDETAIL";
            dgvViewHD.ReadOnly = true;


            dgvViewHD.BuildGridView(this.isLookup);
            dgvViewHD.Dock = DockStyle.Fill;





            //cl.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            //dgvViewHD.dgvGridView.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(gridView1_CustomDrawGroupRow);


            this.ListOrder.Controls.Add(dgvViewHD);
        }
        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridColumn cl = (GridColumn)dgvViewHD.dgvGridView.Columns["MA_CTKM"];
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            if (info.Column == cl)
            {
                info.GroupText = "Ship country starts with " + info.GroupValueText;
            }
        }
        private void FillData()
        {
            bdsViewHD = new BindingSource();

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strMa_Ct);
            string strTable_Ct = (string)drDmCt["Table_Ct"];

            //string strSelect = " Stt , Ma_Ct, Ngay_Ct, So_Ct, Dien_Giai, Ma_Nx, Ma_Kho, Ma_Vt, Ten_Vt, Dvt, So_Luong9, Gia_Nt9, Tien_Nt9, Ma_TTe,Tk_No, Tk_Co, Gia_Nt, Gia, Tien_Nt, Tien, CAST(0 AS BIT) AS Chon ";

            dtViewHD = Discount.GetDiscountDetail(this.strMa_Ct, this.strStt);

            bdsViewHD.DataSource = dtViewHD;
            dgvViewHD.DataSource = bdsViewHD;

            bdsViewHD.Position = 0;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsViewHD;


            // Make the group footers always visible. 
            //dgvViewHD.dgvGridView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;// GroupFooterShowMode.VisibleAlways;
            //// Create and setup the first summary item. 
            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.FieldName = "MA_CTKM";
            //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //dgvViewHD.dgvGridView.GroupSummary.Add(item);

            //dgvViewHD.dgvGridView.OptionsView.ShowGroupedColumns = true;
            //GridColumn cl = (GridColumn)dgvViewHD.dgvGridView.Columns["MA_CTKM"];
            //dgvViewHD.dgvGridView.SortInfo.ClearAndAddRange(new[] {
            //    new GridColumnSortInfo(cl, DevExpress.Data.ColumnSortOrder.Ascending)
            //});

            //dgvViewHD.dgvGridView.GroupSummary.Add(cl);
            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.FieldName = "Tien4";
            //item.ShowInGroupColumnFooter = dgvViewHD.dgvGridView.Columns["Tien4"];
            //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //item.DisplayFormat = "Sum = {0:c2}";
            //dgvViewHD.dgvGridView.GroupSummary.Add(item);


        }




        #endregion

        #region Su kien

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    DataRow drCurrent = ((DataRowView)bdsViewHD.Current).Row;
                    //    drCurrent["Chon"] = !(bool)drCurrent["Chon"];
                    break;
            }


            base.OnKeyDown(e);
        }
        void dgvViewHD_CellMouseClick(object sender, EventArgs e)
        {

            if (bdsViewHD.Position < 0)
                return;


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