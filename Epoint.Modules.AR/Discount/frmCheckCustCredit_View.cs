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
    public partial class frmCheckCustCredit_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        private dgvGridControl dgvViewHD = new dgvGridControl();
        public string strMa_Ct = string.Empty;
        public string strStt = string.Empty;
        public string strMa_Px = string.Empty;
        BindingSource bdsViewHD = new BindingSource();
        DataTable dtListInvoice;
        public DataTable dtStockAvail;
        DateTime Ngay_Ct;
        #endregion

        #region Contructor

        public frmCheckCustCredit_View()
        {
            InitializeComponent();

            btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

        }



        public void Load(DataTable dtListInvoice, DateTime Ngay_Ct, string strMa_Px)
        {

            //this.strMa_Ct = strMa_Ct;
            this.strMa_Px = strMa_Px;
            this.dtListInvoice = dtListInvoice;
            this.Ngay_Ct = Ngay_Ct;
            Build();
            FillData();
            BindingLanguage();

            ShowDialog();
        }

        #endregion

        #region Build, FillData

        private void Build()
        {
            dgvViewHD.strZone = "OM_PXKCREDIT";
            dgvViewHD.ReadOnly = true;
            dgvViewHD.BuildGridView(this.isLookup);
            dgvViewHD.Dock = DockStyle.Fill;
            this.ListOrder.Controls.Add(dgvViewHD);
        }

        private void FillData()
        {
            bdsViewHD = new BindingSource();

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strMa_Ct);
            //string strTable_Ct = (string)drDmCt["Table_Ct"];

            dtViewHD = CustomerCheckingCredit();

            bdsViewHD.DataSource = dtViewHD;
            dgvViewHD.DataSource = bdsViewHD;

            bdsViewHD.Position = 0;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsViewHD;


        }

        private DataTable CustomerCheckingCredit()
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
            command.CommandText = "sp_GetPXKCustomerCredit";
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