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
using DevExpress.Utils.OAuth;

namespace Epoint.Modules.AR
{
    public partial class frmImportDMSOrder_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        public DataTable dtOrderHeader = null;
        public DataTable dtOrderDetail = null, dtOrderDetailDiscount, dtOrderDetailPromotion;
        public string strMa_Px = string.Empty;
        public string strMa_CbNv_GH = string.Empty;
        BindingSource bdsViewHD = new BindingSource();
        public DataTable dtVoucherSelect;

        string strMa_Ct = string.Empty;
        string strKey = string.Empty;
        frmVoucher_Edit frmEditCtHD;
        enuEdit enuNew_Edit;
        string URLString = @"http://103.160.5.28:8082/api/getorderdetails";
        //string URLString = @"http://103.160.5.28:8082/api/getorderdetails/01-02-2024/29-02-2024";
        string APIKey = "/a34f1e9d13d55f5b1eaf48e84aace18d/";
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
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "Detail";
                button.HeaderText = "Detail";
                button.Text = "Xem";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dgvViewHD.Columns.Add(button);
            }

        }

        private void FillData()
        {
            this.dteNgay_BD.Text = Library.DateToStr(DateTime.Now);
            this.dteNgay_Kt.Text = Library.DateToStr(DateTime.Now);

        }

        private bool Save_OM_Detail(DataTable dtEditCt)
        {
            if (true)
            {


                

                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_ValidateDataDMSOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IsImported", true);// Import
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter pHeader = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Header",
                    TypeName = "TVP_OM_DMSOrd",
                    Value = this.dtOrderHeader,
                };
                command.Parameters.Add(pHeader);
                SqlParameter pPromotion = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Detail",
                    TypeName = "TVP_OM_DMSOrdDetail",
                    Value = this.dtOrderDetail,
                };
                command.Parameters.Add(pPromotion);

                SqlParameter pdetail = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Promotion",
                    TypeName = "TVP_OM_DMSPromotionDetail",
                    Value = this.dtOrderDetailPromotion,
                };
                command.Parameters.Add(pdetail);
                SqlParameter pDiscount = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Discount",
                    TypeName = "TVP_OM_DMSDiscountDetail",
                    Value = this.dtOrderDetailDiscount,
                };
                command.Parameters.Add(pDiscount);             
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception exception)
                {
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                    return false;
                }


            }
        }
        void LoadDataFromApi()
        {

            DataTable dtFilter = new DataTable();
            DataTable dtDoiTuongCheck = SQLExec.ExecuteReturnDt("SELECT TOP 0 * FROM LIDOITUONG WHERE 0 = 1");

            dtFilter.Columns.Add(new DataColumn("MaDonHang", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Dvcs", typeof(string)));

            try
            {
                this.dtOrderHeader = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSOrd SELECT * FROM @T");
                this.dtOrderDetail = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSOrdDetail SELECT * FROM @T");
                this.dtOrderDetailDiscount = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSDiscountDetail SELECT * FROM @T");
                this.dtOrderDetailPromotion = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSPromotionDetail SELECT * FROM @T");

              
                string Parram = APIKey + Library.StrToDate(dteNgay_BD.Text).ToString("dd-MM-yyyy") + "/" + Library.StrToDate(dteNgay_Kt.Text).ToString("dd-MM-yyyy");

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
                    drNew["Loai_Ct"] = header.LoaiDonHang;
                    drNew["Ma_Kho"] = header.MaKho;
                    drNew["Ngay_Ct"] = header.NgayDonHang;
                    drNew["Ma_Dt"] = header.MaKhachHang;
                    DataRow drDoituong = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", header.MaKhachHang);
                    if (drDoituong == null)
                    {
                        DataRow drDoituongNew = dtDoiTuongCheck.NewRow();
                        drDoituongNew["Ma_Dt"] = header.MaKhachHang;
                        drDoituongNew["Ten_Dt"] = header.TenKhachHang;
                        drDoituongNew["Ong_Ba"] = header.TenKhachHang;
                        drDoituongNew["Dia_Chi"] = header.DiaChiKhachHang;
                        drDoituongNew["So_Phone"] = header.DienThoaiKhachHang;
                        dtDoiTuongCheck.Rows.Add(drDoituongNew);
                        dtDoiTuongCheck.AcceptChanges();
                        drNew["Ten_Dt"] = header.TenKhachHang;

                    }
                    else
                    {
                        drNew["Ten_Dt"] = drDoituong["Ten_Dt"];
                    }

                    drNew["Ma_Cbnv_BH"] = header.NhanVienBanHang;
                    DataRow drNhanVien = DataTool.SQLGetDataRowByID("LINHANVIEN", "Ma_CBNv", header.NhanVienBanHang);
                    drNew["Ten_Cbnv_BH"] = drNhanVien != null ? drNhanVien["Ten_Cbnv"] : header.TenNhanVienBanHang;
                    drNew["TTien"] = header.TongTien;
                    drNew["TChiet_Khau"] = header.TongTienChietKhau;
                    drNew["Ghi_Chu"] = header.GhiChu;
                    DataRow drFilter = dtFilter.NewRow();

                    //Set Default 
                    drFilter["MaDonHang"] = header.MaDonHang;
                    drFilter["Ma_Dvcs"] = Element.sysMa_DvCs;
                    //string strTrangThai = SQLExec.ExecuteReturnValue("dbo.OM_CheckDMSOrder", drFilter, CommandType.StoredProcedure).ToString();
                    //drNew["Trang_Thai"] = strTrangThai;
                    drNew["Chon"] = true;

                    dtOrderHeader.Rows.Add(drNew);
                    foreach (var chitiet in header.ChitietDonHangrow)
                    {
                        DataRow drNewDetail = dtOrderDetail.NewRow();
                        drNewDetail["MaDonHang"] = header.MaDonHang;
                        drNewDetail["Ma_Vt"] = chitiet.MaHang;
                        DataRow drvattu = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", chitiet.MaHang);
                        drNewDetail["Ten_Vt"] = drvattu != null ? drvattu["Ten_Vt"] : "Not ok";
                        drNewDetail["Ma_Kho"] = chitiet.MaKho;
                        drNewDetail["Dvt"] = chitiet.Dvt;
                        drNewDetail["He_So"] = chitiet.HeSoQuyDoi;
                        drNewDetail["So_Luong"] = chitiet.SoLuong;
                        drNewDetail["Gia"] = chitiet.Gia;
                        drNewDetail["Tien"] = chitiet.Tien;
                        dtOrderDetail.Rows.Add(drNewDetail);
                        //MessageBox.Show($"  MaHang: {chitiet.MaHang}, Dvt: {chitiet.Dvt}, SoLuong: {chitiet.SoLuong}, Gia: {chitiet.Gia}, Tien: {chitiet.Tien}");
                    }
                    foreach (var ctPromotion in header.ChiTietKhuyenMairow)
                    {
                        DataRow drNewDetailPromotion = dtOrderDetailPromotion.NewRow();
                        drNewDetailPromotion["MaDonHang"] = header.MaDonHang;
                        drNewDetailPromotion["Ma_Vt"] = ctPromotion.MaHang;
                        drNewDetailPromotion["Ma_Kho"] = ctPromotion.MaKho;
                        drNewDetailPromotion["Dvt"] = ctPromotion.Dvt;
                        drNewDetailPromotion["He_So"] = ctPromotion.HeSoQuyDoi;
                        drNewDetailPromotion["So_Luong"] = ctPromotion.SoLuong;
                        drNewDetailPromotion["Ma_CtKM"] = ctPromotion.MaChuongTrinhKm;
                        dtOrderDetailPromotion.Rows.Add(drNewDetailPromotion);
                        //MessageBox.Show($"  MaHang: {chitiet.MaHang}, Dvt: {chitiet.Dvt}, SoLuong: {chitiet.SoLuong}, Gia: {chitiet.Gia}, Tien: {chitiet.Tien}");
                    }
                    foreach (var ctDiscount in header.ChiTietChietKhaurow)
                    {
                        DataRow drNewDetailDiscount = dtOrderDetailDiscount.NewRow();
                        drNewDetailDiscount["MaDonHang"] = header.MaDonHang;
                        drNewDetailDiscount["Ma_Vt"] = ctDiscount.MaHang;
                        drNewDetailDiscount["Ma_CtKM"] = ctDiscount.MaChuongTrinhKm;
                        drNewDetailDiscount["Tien"] = ctDiscount.TienChietKhau;
                        dtOrderDetailDiscount.Rows.Add(drNewDetailDiscount);
                        //MessageBox.Show($"  MaHang: {chitiet.MaHang}, Dvt: {chitiet.Dvt}, SoLuong: {chitiet.SoLuong}, Gia: {chitiet.Gia}, Tien: {chitiet.Tien}");
                    }

                }
                DataTable DtCheck = new DataTable();

                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_ValidateDataDMSOrder";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IsImported", false);
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
                SqlParameter pPromotion = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Detail",
                    TypeName = "TVP_OM_DMSOrdDetail",
                    Value = dtOrderDetail,
                };
                command.Parameters.Add(pPromotion);

                SqlParameter pdetail = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Promotion",
                    TypeName = "TVP_OM_DMSPromotionDetail",
                    Value = dtOrderDetailPromotion,
                };
                command.Parameters.Add(pdetail);
                SqlParameter pDiscount = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Discount",
                    TypeName = "TVP_OM_DMSDiscountDetail",
                    Value = dtOrderDetailDiscount,
                };
                command.Parameters.Add(pDiscount);
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

                DataTable dtOrderHeaderChecking = dtOrderHeader.Copy();
                dtOrderHeaderChecking.Columns.Add(new DataColumn("ErrorMessage", typeof(string)));
                dtOrderHeaderChecking.Columns.Add(new DataColumn("Ma_CT", typeof(string)));
                if (DtCheck.Rows.Count > 0)
                {
                    foreach (DataRow drChecking in dtOrderHeaderChecking.Rows)
                    {
                        DataRow[] DrError = DtCheck.Select("MaDonHang = '" + drChecking["MaDonHang"] + "'");
                        if (DrError.Length > 0)
                        {
                            drChecking["ErrorMessage"] = DrError[0]["ErrorMessage"];
                            drChecking["Ma_Ct"] = DrError[0]["Ma_Ct"];
                            drChecking["Trang_Thai"] = DrError[0]["Trang_Thai"];
                            drChecking["TChiet_Khau"] = DrError[0]["TChiet_Khau"];
                            drChecking["Ma_Kho"] = DrError[0]["Ma_Kho"];
                            drChecking["CHON"] = DrError[0]["CHON"];
                        }
                    }
                }


                // Update Status 

                DataRow[] drCheckDataList = DtCheck.Select("CHON = 0");
                foreach (DataRow drCheckData in drCheckDataList)
                {
                    string So_Ct = drCheckData["So_Ct"].ToString();
                    string Ma_Dt = drCheckData["Ma_Dt"].ToString();


                    for (int i = this.dtOrderHeader.Rows.Count - 1; i >= 0; i--)
                    {
                        if (this.dtOrderHeader.Rows[i]["So_Ct"].ToString() == So_Ct && this.dtOrderHeader.Rows[i]["Ma_Dt"].ToString() == Ma_Dt)
                            dtOrderHeader.Rows.RemoveAt(i);
                    }

                }


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
               
                //// Them KH moi
                if (dtDoiTuongCheck.Rows.Count > 0)
                {
                    if (EpointMessage.MsgYes_No("Tồn tại khách hàng chưa có trong hệ thống.Bạn có muốn thêm mới không?"))
                    {
                        frmInvoiceImportCustomer_View frmCustomer = new frmInvoiceImportCustomer_View();
                        frmCustomer.Load(dtDoiTuongCheck);
                    }
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

            if (e.ColumnIndex == dgvViewHD.Columns["Detail"].Index)
            {
                //Do something with your button.
                string MaDonHang = drCurrent["MaDonHang"].ToString();

                DataTable DtDetail = this.dtOrderDetail.Copy();
                DtDetail.Columns.Add(new DataColumn("TIEN4", typeof(decimal)));
                DataRow[] drFilterByMaDonHang = DtDetail.Select( "MaDonHang = '" + MaDonHang + "'");
                DataTable dtDetail0 = DtDetail.Clone();
                foreach (DataRow dr2 in drFilterByMaDonHang)
                {
                    string Ma_Vt = dr2["Ma_Vt"].ToString();
                    string filter = "MaDonHang = '" + MaDonHang + "' and Ma_Vt = '" + Ma_Vt + "'";

                    object sumObject;
                    sumObject = this.dtOrderDetailDiscount.Compute("Sum(Tien)", filter);
                   string tienCk = sumObject.ToString();
                    if(tienCk != string.Empty)
                    {
                        dr2["TIEN4"] = Convert.ToDecimal(tienCk);
                    }

                    dtDetail0.ImportRow(dr2);
                    dtDetail0.AcceptChanges();
                }


                frmInvoiceImportDetail_View frm = new frmInvoiceImportDetail_View();
                frm.Load(dtDetail0, MaDonHang, "MaDonHang");

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
        public string LoaiDonHang { get; set; }
        public string MaKho { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayDonHang { get; set; }
        public decimal TongTien { get; set; }
        public decimal TongTienChietKhau { get; set; }
        public List<OrderDetail> ChitietDonHangrow { get; set; }
        public List<DiscountDetail> ChiTietChietKhaurow { get; set; }
        public List<PromotionDetail> ChiTietKhuyenMairow { get; set; }
        // ... (other properties)


        //Thong in khach hang
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChiKhachHang { get; set; }
        public string DienThoaiKhachHang { get; set; }
        public string LoaiKhachHang { get; set; }

        // Nhan vien ban hang
        public string NhanVienBanHang { get; set; }
        public string TenNhanVienBanHang { get; set; }
        public string DiaChiNhanVienBanHang { get; set; }
        public string DienThoaiNhanVienBanHang { get; set; }



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
        public decimal HeSoQuyDoi { get; set; }

    }
}