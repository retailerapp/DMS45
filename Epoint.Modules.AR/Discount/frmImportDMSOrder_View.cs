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
using Epoint.Systems;
using Epoint.Systems.Elements;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Odbc;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using DevExpress.Xpo.DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Epoint.Modules.AR
{
    public partial class frmImportDMSOrder_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        public DataTable dtOrderHeader = null;
        public DataTable dtOrderDetail = null;
        public string strMa_Px = string.Empty;
        public string strMa_CbNv_GH = string.Empty;
        BindingSource bdsViewHD = new BindingSource();
        public DataTable dtVoucherSelect;

        string strMa_Ct = string.Empty;
        string strKey = string.Empty;
        frmVoucher_Edit frmEditCtHD;
        enuEdit enuNew_Edit;
        string URLString = @"http://103.160.5.28:8082/api/getorderdetails/";
        //string URLString = @"http://103.160.5.28:8082/api/getorderdetails/01-02-2024/29-02-2024";
        #endregion

        #region Contructor

        public frmImportDMSOrder_View()
        {
            InitializeComponent();

            //btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            //btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            btLoadData.Click += new EventHandler(btLoadData_Click);
            btThanhtoan.Click += new EventHandler(btThanhtoan_Click);
            dgvViewHD.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvViewHD_CellMouseClick);
        }







        public void Load()
        {


            Build();
            FillData();
            BindingLanguage();

            this.Show();
        }


        #endregion

        #region Build, FillData

        private void Build()
        {
            dgvViewHD.strZone = "OM_DMSIMPORT";
            dgvViewHD.ReadOnly = true;


            dgvViewHD.BuildGridView(this.isLookup);
            //dgvViewHD.Dock = DockStyle.Fill;

        }

        private void FillData()
        {
            this.dteNgay_BD.Text = Library.DateToStr(DateTime.Now);
            this.dteNgay_Kt.Text = Library.DateToStr(DateTime.Now);

        }



        private void Save_PXKDetail(DataTable dtEditCt)
        {
            if (true)
            {


                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "SP_M1_ImportOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                //command.Parameters.AddWithValue("@IS_UPDATE", "1");
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@TP_Ord",
                    TypeName = "TVP_OM_SalesOrd",
                    Value = dtEditCt,
                };
                command.Parameters.Add(parameter);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                }
            }
        }
        private void Save_PXKDetailDA(DataTable dtEditCt)
        {
            if (true)
            {


                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "SP_DA_ImportOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                //command.Parameters.AddWithValue("@IS_UPDATE", "1");
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@TP_OrdDA",
                    TypeName = "TVP_OM_SalesDiana",
                    Value = dtEditCt,
                };
                command.Parameters.Add(parameter);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                }
            }
        }
        private void Save_OM_Detail(DataTable dtEditCt)
        {
            if (true)
            {


                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "SP_OM_ImportOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@TP_Ord",
                    TypeName = "TVP_OM_Ord",
                    Value = dtEditCt,
                };
                command.Parameters.Add(parameter);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                }
            }
        }
        void LoadDataFromApi()
        {

            DataTable dtFilter = new DataTable();

            dtFilter.Columns.Add(new DataColumn("MaDonHang", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Dvcs", typeof(string)));

            try
            {
                dtOrderHeader = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSOrd SELECT * FROM @T");
                dtOrderDetail = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSOrdDetail SELECT * FROM @T");

                string Parram = Library.StrToDate(dteNgay_BD.Text).ToString("dd-MM-yyyy") + "/" + Library.StrToDate(dteNgay_Kt.Text).ToString("dd-MM-yyyy");

                WebRequest request = WebRequest.Create(URLString + Parram);
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream iDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(iDataStream);
                string jsonString = reader.ReadToEnd();

                //XmlDocument Voucher = new XmlDocument();
                //Voucher.LoadXml(json);
                dynamic JsonHeader = JsonConvert.DeserializeObject(jsonString);
                OrderData orderData = JsonConvert.DeserializeObject<OrderData>(jsonString);

                foreach (var header in orderData.Headers)
                {
                    //MessageBox.Show($"MaDonHang: {header.MaDonHang}, SoDonHang: {header.SoDonHang}, NgayDonHang: {header.NgayDonHang}");
                    DataRow drNew = dtOrderHeader.NewRow();
                    drNew["MaDonHang"] = header.MaDonHang;
                    drNew["So_Ct"] = header.SoDonHang;
                    drNew["Ngay_Ct"] = header.NgayDonHang;
                    drNew["Ma_Dt"] = header.MaKhachHang;
                    DataRow drDoituong = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", header.MaKhachHang);
                    drNew["Ten_Dt"] = drDoituong != null ? drDoituong["Ten_Dt"] : string.Empty;
                    drNew["Ma_Cbnv_BH"] = header.NhanVienBanHang;
                    DataRow drNhanVien = DataTool.SQLGetDataRowByID("LINHANVIEN", "Ma_CBNv", header.NhanVienBanHang);
                    drNew["Ten_Cbnv_BH"] = drNhanVien != null ? drNhanVien["Ten_Cbnv"] : string.Empty;
                    drNew["TTien"] = header.TongTien;
                    drNew["TChiet_Khau"] = header.TongTienChietKhau;
                    DataRow drFilter = dtFilter.NewRow();

                    //Set Default 
                    drFilter["MaDonHang"] = header.MaDonHang;
                    drFilter["Ma_Dvcs"] = Element.sysMa_DvCs;
                    string strTrangThai = SQLExec.ExecuteReturnValue("dbo.OM_CheckDMSOrder", drFilter, CommandType.StoredProcedure).ToString();
                    drNew["Trang_Thai"] = strTrangThai;
                    drNew["Chon"] = true;

                    dtOrderHeader.Rows.Add(drNew);
                    foreach (var chitiet in header.ChitietDonHangrow)
                    {
                        DataRow drNewDetail = dtOrderDetail.NewRow();
                        drNewDetail["MaDonHang"] = header.MaDonHang;
                        drNewDetail["Ma_Vt"] = chitiet.MaHang;
                        drNewDetail["Ma_Kho"] = chitiet.MaKho;
                        drNewDetail["Dvt"] = chitiet.Dvt;
                        drNewDetail["So_Luong"] = chitiet.SoLuong;
                        drNewDetail["Gia"] = chitiet.Gia;
                        drNewDetail["Tien"] = chitiet.Tien;
                        dtOrderDetail.Rows.Add(drNewDetail);
                        //MessageBox.Show($"  MaHang: {chitiet.MaHang}, Dvt: {chitiet.Dvt}, SoLuong: {chitiet.SoLuong}, Gia: {chitiet.Gia}, Tien: {chitiet.Tien}");
                    }



                }
                DataTable DtCheck = null;

                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_ValidateDataDMSOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter pHeader = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Header",
                    TypeName = "TVP_OM_DMSOrd",
                    Value = dtOrderHeader,
                };
                command.Parameters.Add(pHeader);
                SqlParameter pdetail = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Detail",
                    TypeName = "TVP_OM_DMSOrdDetail",
                    Value = dtOrderDetail,
                };
                command.Parameters.Add(pdetail);
                try
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        DtCheck.Load(dr);
                    }
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);               
                    return;
                }

                DataTable dtOrderHeaderChecking = dtOrderHeader;
                dtOrderHeaderChecking.Columns.Add(new DataColumn("ErrorMessage", typeof(string)));
                if (DtCheck.Rows.Count > 0)
                {
                    foreach (DataRow drh in dtOrderHeaderChecking.Rows)
                    {
                        DataRow[] DrError = DtCheck.Select("MaDonHang = '" + drh["MaDonHang"] + "'");
                        if (DrError.Length > 0)
                            drh["ErrorMessage"] = DrError[0]["ErrorMessage"];

                    }
                }
                    /*
                    DataTable dtExcel = new DataTable();
                    //DataTable dtStruckColumn = new DataTable();
                    dtImport = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_Ord SELECT * FROM @T");
                    string cColumnNameList = "";
                    foreach (DataColumn clName in dtImport.Columns)
                    {
                        cColumnNameList += clName.ColumnName.ToString() + ",";
                    }


                    SetdefaultOM(ref dtImport, dtImport);*/

                    if (dtOrderHeaderChecking != null)
                {
                    bdsViewHD = new BindingSource();
                    bdsViewHD.DataSource = dtOrderHeaderChecking;
                    dgvViewHD.DataSource = bdsViewHD;
                    bdsViewHD.Position = 0;

                    //Uy quyen cho lop co so tim kiem           
                    bdsSearch = bdsViewHD;

                    //dgvViewHD.ReadOnly = true;
                    //dgvViewHD.BuildGridView(cColumnNameList);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được bảng dữ liệu " + ex.Message);
            }


        }

        void SetdefaultOM(ref DataTable dtImport, DataTable dtExcel)
        {
            foreach (DataRow dr in dtExcel.Rows)
            {
                if (dr["So_Ct"].ToString() != string.Empty)
                {
                    DataRow drImport = dtImport.NewRow();
                    Common.CopyDataRow(dr, drImport);


                    string str = dr["DNgay_Ct"].ToString();
                    string[] format = { "yyyyMMdd" };
                    DateTime date;
                    if (DateTime.TryParseExact(str,
                                               format,
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out date))
                    {
                        drImport["Ngay_Ct"] = date;
                    }

                    drImport["Tien2"] = drImport["Tien"];
                    drImport["Tien9"] = Convert.ToDouble(drImport["Tien2"]) + Convert.ToDouble(drImport["Tien_Ck"]);

                    dtImport.Rows.Add(drImport);
                }

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

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        foreach (DataRow dr in dtViewHD.Rows)
                            dr["Chon"] = true;
                        break;
                    case Keys.U:
                        foreach (DataRow dr in dtViewHD.Rows)
                            dr["Chon"] = false;
                        break;
                }
            }

            base.OnKeyDown(e);
        }
        void dgvViewHD_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            string strColumnName = dgvViewHD.Columns[e.ColumnIndex].Name;
            DataRow drCurrent = ((DataRowView)bdsViewHD.Current).Row;


            if (strColumnName == "CHON")
            {

                drCurrent["CHON"] = !Convert.ToBoolean(drCurrent["CHON"]);
                drCurrent.AcceptChanges();
            }
        }

        void txtSo_Ct_TextChanged(object sender, EventArgs e)
        {

        }
        void btAccept_Click(object sender, EventArgs e)
        {

        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btLoadData_Click(object sender, EventArgs e)
        {
            //LoadDataExcelDiana();

            LoadDataFromApi();
        }
        void btThanhtoan_Click(object sender, EventArgs e)
        {
            EpointProcessBox.Show(this);


        }

        public override void EpointRelease()
        {
            if (dtOrderHeader == null || dtOrderHeader.Rows.Count == 0)
                EpointProcessBox.AddMessage("Không có dữ liệu import");
            else
                Save_OM_Detail(dtOrderHeader);
            EpointProcessBox.AddMessage("End");

        }
        #endregion
    }


    public class OrderData
    {
        public List<OrderHeader> Headers { get; set; }
    }

    public class OrderHeader
    {
        public string MaDonHang { get; set; }
        public string SoDonHang { get; set; }
        public string NhanVienBanHang { get; set; }
        public string MaKhachHang { get; set; }
        public DateTime NgayDonHang { get; set; }
        public decimal TongTien { get; set; }
        public decimal TongTienChietKhau { get; set; }
        public List<OrderDetail> ChitietDonHangrow { get; set; }
        public List<DiscountDetail> ChiTietChietKhaurow { get; set; }
        public List<PromotionDetail> ChiTietKhuyenMairow { get; set; }
        // ... (other properties)
    }

    public class OrderDetail
    {
        public string MaHang { get; set; }
        public string MaKho { get; set; }
        public string Dvt { get; set; }
        public decimal SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal Tien { get; set; }
        public decimal HeSoQuyDoi { get; set; }
        // ... (other properties)
    }
    public class DiscountDetail
    {
        public string MaHang { get; set; }
        public string MaChuongTrinhKm { get; set; }
        public decimal TienChietKhau { get; set; }

    }
    public class PromotionDetail
    {
        public string MaHang { get; set; }
        public string MaKho { get; set; }
        public string Dvt { get; set; }
        public string MaChuongTrinhKm { get; set; }
        public decimal SoLuong { get; set; }

    }
}