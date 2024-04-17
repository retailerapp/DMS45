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
using DevExpress.Data.Filtering;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Epoint.Modules.AR
{
    public partial class frmImportHD_View : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien
        public DataTable dtViewHD;
        public DataTable dtImport = null;
        public DataTable dtOrderHeader = new DataTable(), DtOrderDetail = new DataTable();
        public string strMa_Px = string.Empty;
        public string strMa_CbNv_GH = string.Empty;
        public bool iChecked = true;
        BindingSource bdsViewHD = new BindingSource();
        public DataTable dtVoucherSelect;
        //dgvControl dgvViewHD = new dgvControl();

        string strMa_Ct = string.Empty;
        string strKey = string.Empty;
        frmVoucher_Edit frmEditCtHD;
        enuEdit enuNew_Edit;

        #endregion

        #region Contructor

        public frmImportHD_View()
        {
            InitializeComponent();

            //btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            //btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            btFile.Click += new EventHandler(btFile_Click);
            btLoadData.Click += new EventHandler(btLoadData_Click);
            btImportData.Click += new EventHandler(btThanhtoan_Click);

            txtFile_Name.TextChanged += new EventHandler(txtSo_Ct_TextChanged);
            dgvViewHD.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvViewHD_CellMouseClick);
        }









        public void Load()
        {
            //this.frmEditCtHD = frmEditCtHD;
            //this.strMa_Ct = strMa_Ct;
            //this.strKey = strKey;

            Build();
            FillData();
            BindingLanguage();

            this.Show();
        }


        #endregion

        #region Build, FillData

        private void Build()
        {

            dgvViewHD.strZone = "OM_DMSIMPORT_YENCHAU";
            dgvViewHD.ReadOnly = true;


            dgvViewHD.BuildGridView(this.isLookup);


            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "Detail";
                button.HeaderText = "Detail";
                button.Text = "Xem";
                button.UseColumnTextForButtonValue = true; //dont forget this line
                this.dgvViewHD.Columns.Add(button);
            }
            //this.Controls.Add(dgvViewHD);
            btImportData.Enabled = false;
        }

        private void FillData()
        {

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
        private bool Save_OM_DetailPHYEN(DataTable dtEditCt)
        {
            if (true)
            {


                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "SP_OM_ImportOrderYenChau";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Header",
                    TypeName = "TVP_OM_DMSYENCHAU",
                    Value = dtEditCt,
                };
                command.Parameters.Add(parameter);
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
            return false;
        }
        void LoadDataExcel()
        {

            try
            {

                DataTable dtExcel = new DataTable();
                //DataTable dtStruckColumn = new DataTable();
                dtImport = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_Ord SELECT * FROM @T");
                string cColumnNameList = "";
                foreach (DataColumn clName in dtImport.Columns)
                {
                    cColumnNameList += clName.ColumnName.ToString() + ",";
                }

                dtExcel = Common.ReadExcel(txtFile_Name.Text);

                SetdefaultOM(ref dtImport, dtExcel);

                if (dtImport != null)
                {
                    bdsViewHD = new BindingSource();
                    bdsViewHD.DataSource = dtImport;
                    dgvViewHD.DataSource = bdsViewHD;
                    bdsViewHD.Position = 0;

                    //Uy quyen cho lop co so tim kiem           
                    bdsSearch = bdsViewHD;

                    //dgvViewHD.ReadOnly = true;
                    dgvViewHD.BuildGridView(cColumnNameList);


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được bảng dữ liệu " + txtFile_Name.Text + ex.Message);
            }


        }
        void LoadDataExcelForYenChau()
        {

            try
            {

                DataTable dtExcel = new DataTable();
                DataTable DtCheck = new DataTable();

                dtOrderHeader = SQLExec.ExecuteReturnDt("DECLARE @T AS TVP_OM_DMSYENCHAU SELECT * FROM @T");
                string cColumnNameList = "";
                //foreach (DataColumn clName in dtImport.Columns)
                //{
                //    cColumnNameList += clName.ColumnName.ToString() + ",";
                //}

                dtExcel = Common.ReadExcel(txtFile_Name.Text);

                SetdefaultOM(ref dtOrderHeader, dtExcel);



                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_ValidateDataDMSOrderYenChau";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter pHeader = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Header",
                    TypeName = "TVP_OM_DMSYENCHAU",
                    Value = dtOrderHeader,
                };
                command.Parameters.Add(pHeader);
                //SqlParameter pdetail = new SqlParameter
                //{
                //    SqlDbType = SqlDbType.Structured,
                //    ParameterName = "@Detail",
                //    TypeName = "TVP_OM_DMSOrdDetail",
                //    Value = dtOrderDetail,
                //};
                //command.Parameters.Add(pdetail);
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
                /////////// View detail
                SqlCommand command1 = SQLExec.GetNewSQLConnection().CreateCommand();
                command1.CommandText = "OM_GetDataDMSOrderDetailYenChau";
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@UserID", Element.sysUser_Id);
                command1.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter pHeader1 = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@Header",
                    TypeName = "TVP_OM_DMSYENCHAU",
                    Value = dtOrderHeader,
                };
                command1.Parameters.Add(pHeader1);             
                try
                {
                    using (SqlDataReader dr1 = command1.ExecuteReader())
                    {
                        DtOrderDetail = new DataTable();
                        DtOrderDetail.Load(dr1);
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
               //////////////View
                if (DtCheck != null)
                {
                    bdsViewHD = new BindingSource();
                    bdsViewHD.DataSource = DtCheck;
                    dgvViewHD.DataSource = bdsViewHD;
                    bdsViewHD.Position = 0;

                    //Uy quyen cho lop co so tim kiem           
                    bdsSearch = bdsViewHD;

                    //dgvViewHD.ReadOnly = true;
                    //dgvViewHD.BuildGridView(cColumnNameList);

                    if (DtCheck.Rows.Count > 0)
                    {

                        if (DtCheck.Rows[0]["IsDataChecked"].ToString() == "NOTOK")
                        {
                            iChecked = false;
                            //dtImport = dtOrderHeader;
                            btImportData.Enabled = false;
                        }
                        else
                        {
                            dtImport = dtOrderHeader;
                            btImportData.Enabled = true;
                        }
                        /*

                        // Update Status 
                        
                        DataRow[] drCheckDataList = DtCheck.Select("CHON = 0");
                        foreach (DataRow drCheckData in drCheckDataList)
                        {

                            string So_Ct = drCheckData["So_Ct"].ToString();
                            string Ma_Dt = drCheckData["Ma_Dt"].ToString();


                            for (int i = this.dtOrderHeader.Rows.Count - 1; i>= 0; i--)
                            {
                                if (this.dtOrderHeader.Rows[i]["So_Ct"].ToString() == So_Ct && this.dtOrderHeader.Rows[i]["Ma_Dt"].ToString() == Ma_Dt)
                                    dtOrderHeader.Rows.RemoveAt(i);
                            }
                            //foreach(DataRow drDataImport in this.dtOrderHeader.Rows) 
                            //{
                            //    if (drDataImport["So_Ct"].ToString() == So_Ct && drDataImport["Ma_Dt"].ToString() == Ma_Dt)
                            //        dtOrderHeader.Rows.Remove(drDataImport);
                            //}
                        }
                        */


                    }



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được bảng dữ liệu " + txtFile_Name.Text + ex.Message);
            }


        }
        void LoadDataExcelM1()
        {
            string cColumnNameList = "";

            string strConnectString =
                "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + txtFile_Name.Text;

            OdbcConnection odbcConn = new OdbcConnection(strConnectString);
            odbcConn.Open();

            OdbcCommand odbcComm = new OdbcCommand();
            odbcComm.Connection = odbcConn;

            try
            {
                OdbcDataAdapter odbcDA;
                DataTable dtTestColumn = new DataTable();
                dtImport = new DataTable();



                //Kiểm tra tồn tại cột dữ liệu
                odbcComm.CommandText = "SELECT * FROM [Sheet1$] WHERE 0 = 1";
                odbcDA = new OdbcDataAdapter(odbcComm);
                odbcDA.Fill(dtTestColumn);
                foreach (DataColumn clName in dtTestColumn.Columns)
                {
                    cColumnNameList += clName.ColumnName.ToString() + ",";

                }
                //Kiểm tra xong

                odbcComm.CommandText =
                    @"SELECT VendID,VendName,Ma_Vt,Ten_Vt,He_So,So_LuongT,So_LuongL,SumT,So_Luong,Tien,Tien2,Ten_CBNV_GH,
                            Ngay_Ct,Ngay_Ct0,Ten_Cbnv,Ma_DH,Ngay_Giao,LineItem_Id,Gia,Chieu_Khau_Line,Chieu_Khau,Chieu_Khau_Nhom,Chiet_Khau_HD,CK,
                            Ma_Dt,Ten_Dt,StatusItem,RouteID,Status,Type,Type_Order,Hang_Km,So_Nha,Duong,Quoc_Gia,Thanh_Pho,Quan,Phuong,Vi_tri_Ch,
                            Loai_Hinh_KD1,Loai_Hinh_KD2,Loai_Hinh_KD3,Trong_Luong,Division,Industry,Standard_SKU,Variant,Product_Form,Pack_Type,
                            Packsize,Sub_division,Category,Sub_Category,Brand_Name,Brandy_Name,Team,Ma_CbNv_Bh,So_Ct0
                        " +
                        " FROM [Sheet1$] " +
                        " WHERE 1 = 1";

                odbcDA = new OdbcDataAdapter(odbcComm);
                odbcDA.Fill(dtImport);
                odbcConn.Close();

                if (dtImport != null)
                {
                    bdsViewHD = new BindingSource();


                    bdsViewHD.DataSource = dtImport;
                    dgvViewHD.DataSource = bdsViewHD;

                    bdsViewHD.Position = 0;

                    //Uy quyen cho lop co so tim kiem           
                    bdsSearch = bdsViewHD;

                    //dgvViewHD.ReadOnly = true;
                    dgvViewHD.BuildGridView(cColumnNameList);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được bảng dữ liệu " + txtFile_Name.Text + ex.Message);
            }




        }
        void LoadDataExcelDiana()
        {
            string cColumnNameList = "";
            //string strConnectString =
            //    "Driver={Microsoft Excel Driver (*.xls, *.xlsx)};DBQ=" + ofd.FileName;

            //string strConnectString =
            //    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ofd.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX = 2\"";

            string strConnectString =
                "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + txtFile_Name.Text;

            OdbcConnection odbcConn = new OdbcConnection(strConnectString);
            odbcConn.Open();

            OdbcCommand odbcComm = new OdbcCommand();
            odbcComm.Connection = odbcConn;

            try
            {
                OdbcDataAdapter odbcDA;
                DataTable dtTestColumn = new DataTable();
                dtImport = new DataTable();



                //Kiểm tra tồn tại cột dữ liệu
                odbcComm.CommandText = "SELECT * FROM [Sheet1$] WHERE 0 = 1";
                odbcDA = new OdbcDataAdapter(odbcComm);
                odbcDA.Fill(dtTestColumn);
                foreach (DataColumn c in dtTestColumn.Columns)
                {
                    cColumnNameList += c.ColumnName.ToString() + ",";

                }
                //Kiểm tra xong

                odbcComm.CommandText =
                    @"SELECT *
                        " +
                        " FROM [Sheet1$] " +
                        " WHERE A7 <> ''";

                odbcDA = new OdbcDataAdapter(odbcComm);
                odbcDA.Fill(dtImport);
                odbcConn.Close();

                if (dtImport != null)
                {
                    bdsViewHD = new BindingSource();


                    bdsViewHD.DataSource = dtImport;
                    dgvViewHD.DataSource = bdsViewHD;

                    bdsViewHD.Position = 0;

                    //Uy quyen cho lop co so tim kiem           
                    bdsSearch = bdsViewHD;

                    //dgvViewHD.ReadOnly = true;
                    dgvViewHD.BuildGridView(cColumnNameList);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được bảng dữ liệu " + txtFile_Name.Text + ex.Message);
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

                    if (dtExcel.Columns.Contains("DNgay_Ct"))
                    {
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
                    }
                    if (dtExcel.Columns.Contains("Tien2"))
                        drImport["Tien2"] = drImport["Tien"];
                    if (dtExcel.Columns.Contains("Tien9"))
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
                string So_Ct = drCurrent["So_Ct"].ToString();
                frmInvoiceImportDetail_View frm = new frmInvoiceImportDetail_View();
                frm.Load(this.DtOrderDetail, So_Ct);

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
        void btFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xls files (*.xls)|*.xlsx";
            ofd.RestoreDirectory = true;

            ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFile_Name.Text = ofd.FileName;

            }

        }
        void btLoadData_Click(object sender, EventArgs e)
        {
            ////LoadDataExcelDiana();
            //if (File.Exists(@"C:\USERS\VANHU\DESKTOP\THDIANA.XLSX"))
            //    txtFile_Name.Text = @"C:\USERS\VANHU\DESKTOP\THDIANA.XLSX";

            if (!File.Exists(txtFile_Name.Text))
            {
                EpointMessage.MsgOk("File don hang ko ton tai");
                return;
            }
            if (0 == 1)
                LoadDataExcel();
            else // test for YenChau
                LoadDataExcelForYenChau();
        }
        void btThanhtoan_Click(object sender, EventArgs e)
        {
            EpointProcessBox.Show(this);


        }

        public override void EpointRelease()
        {
            bool IsSuccess = true;
            if (dtImport == null || dtImport.Rows.Count == 0)
                EpointProcessBox.AddMessage("Không có dữ liệu import");
            else
                if (false)
                Save_OM_Detail(dtImport);
            else
                IsSuccess = Save_OM_DetailPHYEN(dtImport);

            EpointProcessBox.AddMessage("Kết thúc import dữ liệu!");

            btLoadData.Enabled = false;
        }

        public DataTable GetEntriesBySearch(string expression,  DataTable table)
        {
            DataTable list = null;
            list = table;
         
            string sortOrder;
            sortOrder = "";


            DataRow[] rows = list.Select(expression, sortOrder);

            list = null; // for testing
            list = new DataTable(); // for testing

            foreach (DataRow row in rows)
            {
                list.ImportRow(row);
            }

            return list;
        }
        #endregion
    }
}