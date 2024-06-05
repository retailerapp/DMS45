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

namespace Epoint.Modules.AR
{
    public partial class frmInvoiceImportDetail_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        private dgvGridControl dgvViewHD = new dgvGridControl();
        public string strSo_Ct = string.Empty;
        public string strStt = string.Empty;
        public string strMa_Px = string.Empty;
        BindingSource bdsViewHD = new BindingSource();
        DataTable dtListInvoice, dtListPromotion;
        public DataTable dtStockAvail;
        DateTime Ngay_Ct;
        #endregion

        #region Contructor

        public frmInvoiceImportDetail_View()
        {
            InitializeComponent();

            btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

        }



        public void Load(DataTable dtListInvoice, string strSo_Ct)
        {

            //this.strMa_Ct = strMa_Ct;
            this.strSo_Ct = strSo_Ct;
            this.dtListInvoice = dtListInvoice;
            this.Ngay_Ct = Ngay_Ct;
            BuildForYenChau();
            FillData();
            BindingLanguage();

            ShowDialog();
        }
        public void Load(DataTable dtListInvoice, string strSo_Ct, string columnName)
        {

            //this.strMa_Ct = strMa_Ct;
            this.strSo_Ct = strSo_Ct;
            this.dtListInvoice = dtListInvoice;
            this.Ngay_Ct = Ngay_Ct;
            Build();
            FillData(columnName);
            BindingLanguage();

            ShowDialog();
        }
        public void Load(DataTable dtListInvoice, DataTable dtPromotiondetail, string strSo_Ct, string columnName)
        {

            //this.strMa_Ct = strMa_Ct;
            this.strSo_Ct = strSo_Ct;
            this.dtListInvoice = dtListInvoice;
            this.dtListPromotion = dtPromotiondetail;
            this.Ngay_Ct = Ngay_Ct;
            Build();
            FillData(columnName);
            BindingLanguage();

            ShowDialog();
        }
        #endregion

        #region Build, FillData

        private void BuildForYenChau()
        {
            dgvViewHD.strZone = "OM_INVOICRDETAIL";
            dgvViewHD.ReadOnly = true;
            dgvViewHD.BuildGridView(this.isLookup);
            dgvViewHD.Dock = DockStyle.Fill;
            this.ListOrder.Controls.Add(dgvViewHD);
        }
        private void Build()
        {
            dgvViewHD.strZone = "OM_INVOICRDETAIL_01";
            dgvViewHD.ReadOnly = true;
            dgvViewHD.BuildGridView(this.isLookup);
            dgvViewHD.BuildGridView(this.isLookup);
            dgvViewHD.Dock = DockStyle.Fill;
            this.ListOrder.Controls.Add(dgvViewHD);
        }

        private void FillData()
        {
            bdsViewHD = new BindingSource();

            //DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strSo_Ct);
            //string strTable_Ct = (string)drDmCt["Table_Ct"];

            //dtViewHD = GetDiscountDetail();

            dtViewHD = this.dtListInvoice.Clone();
            try
            {
                DataRow[] dr = this.dtListInvoice.Select("So_Ct = '" + this.strSo_Ct + "'");
                foreach (DataRow dr2 in dr)
                {

                    dtViewHD.ImportRow(dr2);
                    dtViewHD.AcceptChanges();
                }



            }
            catch (Exception exception)
            {
                EpointMessage.MsgOk(exception.Message);
            }
            bdsViewHD.DataSource = dtViewHD;
            dgvViewHD.DataSource = bdsViewHD;

            bdsViewHD.Position = 0;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsViewHD;


        }
        private void FillData(string ColumnName)
        {
            bdsViewHD = new BindingSource();

            //DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strSo_Ct);
            //string strTable_Ct = (string)drDmCt["Table_Ct"];

            //dtViewHD = GetDiscountDetail();

            dtViewHD = this.dtListInvoice.Clone();
            try
            {
                DataRow[] dr = this.dtListInvoice.Select(ColumnName + " = '" + this.strSo_Ct + "'");
                foreach (DataRow dr2 in dr)
                {

                    dtViewHD.ImportRow(dr2);
                    dtViewHD.AcceptChanges();
                }

                if (this.dtListPromotion != null)
                {
                    DataRow[] drProm = this.dtListPromotion.Select(ColumnName + " = '" + this.strSo_Ct + "'");
                    foreach (DataRow drpro2 in drProm)
                    {
                        DataRow drPromo = dtViewHD.NewRow();
                        Common.SetDefaultDataRow(ref drPromo);
                        drPromo["MaDonHang"] = drpro2["MaDonHang"];
                        drPromo["Ma_Vt"] = drpro2["Ma_Vt"]; 
                        drPromo["So_Luong"] = drpro2["So_Luong"];
                        drPromo["Dvt"] = drpro2["Dvt"];
                        DataRow drvattu = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", drpro2["Ma_Vt"].ToString());
                        drPromo["Ten_Vt"] = drvattu != null ? drvattu["Ten_Vt"] : "Not ok";                        
                        //drPromo["Ma_Ctkm"] = drpro2["Ma_Ctkm"];
                        dtViewHD.Rows.Add(drPromo);
                        dtViewHD.AcceptChanges();
                    }
                }

            }
            catch (Exception exception)
            {
                EpointMessage.MsgOk(exception.Message);
            }
            bdsViewHD.DataSource = dtViewHD;
            dgvViewHD.DataSource = bdsViewHD;

            bdsViewHD.Position = 0;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsViewHD;


        }

        private DataTable GetDiscountDetail()
        {
            DataTable dtReturn = new DataTable();

            DataTable dtImport = SQLExec.ExecuteReturnDt("DECLARE @TVP_PXKDETAIL AS TVP_PXKDETAIL SELECT * FROM @TVP_PXKDETAIL");

            foreach (DataRow drEdit in this.dtListInvoice.Rows)
            {
                DataRow drNew = dtImport.NewRow();
                Common.CopyDataRow(drEdit, drNew);
                dtImport.Rows.Add(drNew);
            }

            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "sp_GetPXKDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_PX", this.strMa_Px);
            command.Parameters.AddWithValue("@Ngay_Ct", this.Ngay_Ct);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@TVP_PXKDETAIL",
                TypeName = "TVP_PXKDETAIL",
                Value = dtImport,
            };
            command.Parameters.Add(parameter);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dtReturn);
                return dtReturn;

            }
            catch (Exception exception)
            {
                return dtReturn;
            }
            /*
            /////////// View detail
            SqlCommand command1 = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "OM_GetDataDMSOrderDetailYenChau";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter pHeader1 = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@Header",
                TypeName = "TVP_OM_DMSYENCHAU",
                Value = dtOrderHeader,
            };
            command.Parameters.Add(pHeader);
            try
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    DtOrderDetail.Load(dr);
                }
            }
            catch (Exception exception)
            {
                command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                return dtReturn;             }*/

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