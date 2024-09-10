using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Epoint
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Lists;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Epoint.Modules.AR
{
    public partial class frmGopPXK_Edit : frmVoucher_Edit
    {
        #region Declare
        private string strTk_NoTmp = string.Empty;
        private string strTk_CoTmp = string.Empty;
        private string strModule = "04";

        public string strStt_List = string.Empty;
        public DataTable dtStt = null;
        public DataTable dtDetail;
        DataTable dtImport;
        #endregion

        #region Contructor

        public frmGopPXK_Edit()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(frmEditCtTien_KeyDown);



            tabVoucher.Enter += new EventHandler(tabVoucher_Enter);

            txtMa_CBNV_GH.Validating += new CancelEventHandler(txtMa_CBNV_GH_Validating);
            txtMa_Xe.Validating += new CancelEventHandler(txtMa_Xe_Validating);

            dteNgay_Ct.Validating += new CancelEventHandler(dteNgay_Ct_Validating);

            btAddHD.Click += new EventHandler(btAddHD_Click);
            btCheckStock.Click += new EventHandler(btCheckStock_Click);
            btCheckCustomerCredit.Click += new EventHandler(btCheckCustomerCredit_Click);
        }




        public void Load(enuEdit enuNew_Edit, DataRow drEdit)
        {
            this.drEdit = drEdit;
            //this.drEditPh = drEdit;
            this.drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct != string.Empty ? this.strMa_Ct : drEdit["Ma_Ct"].ToString());
            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (this.drEdit.Table.Rows.Count == 0)
                {
                    dteNgay_Ct.Text = Library.DateToStr(DateTime.Now);
                    this.drEdit = drEdit.Table.NewRow();
                    this.drEdit["Ngay_Ct"] = dteNgay_Ct.Text;
                    this.drEdit["Active"] = true;
                    Common.SetDefaultDataRow(ref this.drEdit);
                }
                this.strStt = Common.GetNewStt(strModule, true);
                TinhSoCtPXK();
                txtMa_Ct.Text = this.drDmCt["Ma_Ct"].ToString();
                txtNh_Ct.Text = this.drDmCt["Nh_Ct"].ToString();
                //if (txtNh_Ct.Text == "1")
                //    txtMa_Ct.Text = "IN";
            }

            else
                this.strStt = drEdit["Ma_PX"].ToString();


            Common.ScaterMemvar(this, ref drEdit);


            this.Build();
            this.FillData();
            this.Init_Ct();


            if (enuNew_Edit == enuEdit.New)
            {

                //TinhSoCtPXK();

            }
            this.BindingLanguage();
            this.LoadDicName();

            if (!isAccept)
                this.ShowDialog();
            else
                this.ActiveControl = txtMa_Px;
        }

        #endregion



        private void Build()
        {
            dgvEditCt1.bSortMode = false;
            dgvEditCt1.strZone = "OM_PXKDETAIL_EDIT";
            dgvEditCt1.BuildGridView();

        }

        private void FillData()
        {

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (dtDetail != null)
                {
                    dtEditCt = dtDetail;
                    if (!dtEditCt.Columns.Contains("Deleted"))
                    {
                        DataColumn dc = new DataColumn("Deleted", typeof(bool));
                        dc.DefaultValue = false;
                        dtEditCt.Columns.Add(dc);
                    }

                    // Update du lieu tu drPh xuong dtCt theo danh sach strColumnList


                    foreach (DataRow dr in dtEditCt.Rows)
                        dr["MA_PX"] = txtMa_Px.Text;


                    bdsEditCt.DataSource = dtEditCt;

                    dgvEditCt1.DataSource = bdsEditCt;
                    dgvEditCt1.ClearSelection();

                    drEdit["TTien"] = Common.SumDCValue(dtEditCt, "TTien", "");
                    numTTien.Value = Convert.ToDouble(drEdit["TTien"]);

                }
                else
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("MA_PX", drEdit["Ma_PX"].ToString());
                    ht.Add("MA_DVCS", Element.sysMa_DvCs);
                    dtEditCt = SQLExec.ExecuteReturnDt("sp_GetPhieuXuatKhoDetail", ht, CommandType.StoredProcedure);
                    if (!dtEditCt.Columns.Contains("Deleted"))
                    {
                        DataColumn dc = new DataColumn("Deleted", typeof(bool));
                        dc.DefaultValue = false;
                        dtEditCt.Columns.Add(dc);
                    }
                    bdsEditCt.DataSource = dtEditCt;
                    dgvEditCt1.DataSource = bdsEditCt;
                }
            }
            else
            {

                Hashtable ht = new Hashtable();
                ht.Add("MA_PX", drEdit["Ma_PX"].ToString());
                ht.Add("MA_DVCS", Element.sysMa_DvCs);
                dtEditCt = SQLExec.ExecuteReturnDt("sp_GetPhieuXuatKhoDetail", ht, CommandType.StoredProcedure);

                if (!dtEditCt.Columns.Contains("Deleted"))
                {
                    DataColumn dc = new DataColumn("Deleted", typeof(bool));
                    dc.DefaultValue = false;
                    dtEditCt.Columns.Add(dc);
                }

                bdsEditCt.DataSource = dtEditCt;
                dgvEditCt1.DataSource = bdsEditCt;

                drEdit["TTien"] = Common.SumDCValue(dtEditCt, "TTien", "");
                numTTien.Value = Common.SumDCValue(dtEditCt, "TTien", "");

            }


            GetInfoPXK();


        }

        private void Init_Ct()
        {



            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (enuNew_Edit == enuEdit.New)
                    TinhSoCtPXK();

            }
            else
            {
                //if(Voucher.CheckDataLockedCtHanTtPXK(drEdit["Ma_PX"].ToString()))
                //{
                //    btAddHD.Enabled = false;
                //    btCheckStock.Enabled = false;
                //    this.btgAccept.btAccept.Enabled = false;
                //}
            }


            //BindingTTien            
            if (isAccept)
            {

                //numTTien0.DataBindings.Clear();
                ////numTTien3.DataBindings.Clear();
                //numTTien.DataBindings.Clear();
                ////numTTien4.DataBindings.Clear();
                //numTSo_Luong.DataBindings.Clear();
            }

            numTTien.Value = Convert.ToDouble(drEdit["TTIEN"]);
            //numTTien0.DataBindings.Add("Value", drEdit, "TTien");
            //numTTien.DataBindings.Add("Value", drEdit, "TTien"); 
            //numTSo_Luong.DataBindings.Add("Value", dtEditPh, "TSo_Luong");

        }

        private void LoadDicName()
        {

            //txtMa_Dt
            if (txtMa_CBNV_GH.Text.Trim() != string.Empty)
            {
                txtTen_CbNv_Gh.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CBNV", "Ten_CBNV", txtMa_CBNV_GH.Text.Trim());
                dicName.SetValue(txtTen_CbNv_Gh.Name, txtTen_CbNv_Gh.Text);
            }
            else
                txtTen_CbNv_Gh.Text = string.Empty;

            //txtMa_Hd


        }

        private bool FormCheckValid()
        {
            if (!Common.CheckDataLocked(Library.StrToDate(this.dteNgay_Ct.Text)))
            {
                string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Dữ liệu đã bị khóa" : "Data have been locked";
                Common.MsgCancel(strMsg);
                return false;
            }

            if (Element.sysWorkingYear != Library.StrToDate(dteNgay_Ct.Text).Year)
            {
                EpointMessage.MsgOk("Ngày chứng từ không nằm trong năm làm việc ! ");
                return false;
            }

            if (txtMa_CBNV_GH.Text == string.Empty)
            {
                EpointMessage.MsgOk("Mã Nhân viên giao hàng không được rỗng ! ");
                txtMa_CBNV_GH.Focus();
                return false;
            }

            //Kiểm tra nghiệp vụ hợp lệ
            foreach (DataRow dr in dtEditCt.Rows)
            {

                if (enuNew_Edit == enuEdit.New && DataTool.SQLCheckExist("OM_PXKDetail", "Stt", dr["stt"].ToString()))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Số chứng từ :" + dr["So_Ct"].ToString() + " đã tồn tại trong một phiếu xuất kho khác!" : "Voucher NBR :" + dr["So_Ct"].ToString() + " exist in another PXK!";
                    Common.MsgCancel(strMsg);
                    return false;
                }

            }
            string strCheckQtyOnhand = Parameters.GetParaValue("CHECKONHAND") == null ? "Y" : (string)Parameters.GetParaValue("CHECKONHAND");
            if (strCheckQtyOnhand == "Y" && txtMa_Ct.Text == "IN")
            {
                DataTable dtCheckTon = GetStockDetail();
                if (dtCheckTon != null && dtCheckTon.Rows.Count > 0)
                {
                    EpointMessage.MsgOk("Tồn tại mặt hàng bị âm kho khi xuất kho. Kiểm tra lại tồn kho! ");
                    return false;
                }
            }
            string strCheckCustomerCredit = Parameters.GetParaValue("ARCUSTCREDIT") == null ? "N" : (string)Parameters.GetParaValue("ARCUSTCREDIT");
            if (strCheckQtyOnhand == "Y" && txtMa_Ct.Text == "IN")
            {
                DataTable dtCheckingCredit = this.CheckCustomerCreditDetail();
                if (dtCheckingCredit != null && dtCheckingCredit.Select("IsCheck = 1").Length > 0)
                {
                    EpointMessage.MsgOk("Tồn tại khách hàng quá hạn mức công nợ! ");
                    frmCheckCustCredit_View frm = new frmCheckCustCredit_View();
                    frm.Load(dtCheckingCredit);
                    return false;
                }
            }
            return true;
        }

        public override bool Save()
        {
            Common.GatherMemvar(this, ref this.drEdit);

            this.dtImport = SQLExec.ExecuteReturnDt("DECLARE @TVP_PXKDETAIL AS TVP_PXKDETAIL SELECT * FROM @TVP_PXKDETAIL");

            foreach (DataRow drEdit in dtEditCt.Rows)
            {
                DataRow drNew = dtImport.NewRow();
                Common.CopyDataRow(drEdit, drNew);
                dtImport.Rows.Add(drNew);
            }


            if (!FormCheckValid())
                return false;

            if (enuNew_Edit == enuEdit.New)
                TinhSoCtPXK();

            //Luu xuong CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "OM_PXK", ref drEdit))
                return false;

            Save_PXKDetail(dtEditCt);
            return true;
        }

        private bool CellKeyEnter()
        {//Ham thuc hien phim Enter: true: thuc hien thanh cong, false: khong thuc hien duoc			

            if (dgvEditCt1.CurrentCell == null)
                return false;

            DataGridViewCell dgvCell = dgvEditCt1.CurrentCell;
            string strCurrentColumn = dgvCell.OwningColumn.Name.ToUpper();

            return false;
        }


        void btCheckStock_Click(object sender, EventArgs e)
        {
            frmCheckStock_View frm = new frmCheckStock_View();
            frm.Load(this.dtEditCt, Library.StrToDate(dteNgay_Ct.Text), this.txtMa_Px.Text);
        }
        void btCheckCustomerCredit_Click(object sender, EventArgs e)
        {
            frmCheckCustCredit_View frm = new frmCheckCustCredit_View();
            frm.Load(this.dtEditCt, Library.StrToDate(dteNgay_Ct.Text), this.txtMa_Px.Text);
        }
        void btAddHD_Click(object sender, EventArgs e)
        {

            frmChonHD_View frm = new frmChonHD_View();
            frm.dtViewCur = this.dtEditCt;
            frm.LoadCheckPXK(enuEdit.New, txtMa_Px.Text, txtMa_CBNV_GH.Text, txtMa_Ct.Text);
            if (frm.is_Accept)
            {
                DataTable dt = frm.dtVoucherSelect;
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = this.dtEditCt.NewRow();
                    Common.CopyDataRow(dr, drNew);
                    this.dtEditCt.Rows.Add(drNew);
                }
                //FillData();
            }
        }


        void dteNgay_Ct_Validating(object sender, CancelEventArgs e)
        {
            this.drEdit["Ngay_Ct"] = Library.StrToDate(dteNgay_Ct.Text);
            TinhSoCtPXK();

        }

        private void txtMa_CBNV_GH_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CBNV_GH.Text.Trim();
            bool bRequire = true;

            DataRow drLookup = Lookup.ShowLookup("Ma_CBNV", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CBNV_GH.Text = string.Empty;
                txtTen_CbNv_Gh.Text = string.Empty;
            }
            else
            {
                txtMa_CBNV_GH.Text = drLookup["Ma_CBNV"].ToString();
                txtTen_CbNv_Gh.Text = drLookup["Ten_CBNV"].ToString();

            }



            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        void txtMa_Xe_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Xe.Text.Trim();
            bool bRequire = true;

            DataRow drLookup = Lookup.ShowQuickLookup("MA_XE", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Xe.Text = string.Empty;
                //txtTen_CbNv_Gh.Text = string.Empty;
            }
            else
            {
                txtMa_Xe.Text = drLookup["Ma_Xe"].ToString();

            }



            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        public void Delete()
        {

            DataRow drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            if (!Common.CheckDataLocked(Library.StrToDate(dteNgay_Ct.Text)))
            {
                EpointMessage.MsgOk("Ngày dữ liệu đã bị khóa !");
                return;
            }
            if (dgvEditCt1.Focused)
                DeletePXKDetail();

        }
        private void DeletePXKDetail()
        {
            if (bdsEditCt.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            if (Voucher.CheckDataLockedCtHanTtHD((string)drCurrent["Stt"]))
            {
                if (!Common.MsgYes_No("Chứng từ đã được thanh toán . Bạn có muốn xóa thanh toán của chứng từ này !"))
                    return;
            }

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE") + " Chi tiết phiếu xuất"))
                return;



            Hashtable htPara = new Hashtable();
            htPara["MA_PX"] = drCurrent["MA_PX"].ToString();
            htPara["STT"] = drCurrent["STT"].ToString();
            htPara["MA_DVCS"] = Element.sysMa_DvCs;
            if (SQLExec.Execute("sp_Delete_PXKDetail", htPara, CommandType.StoredProcedure))
            {
                bdsEditCt.RemoveAt(bdsEditCt.Position);
                dtEditCt.AcceptChanges();
            }
            else
            {
                EpointMessage.MsgOk("Không xóa được chi tiết");
            }
        }
        private void TinhSoCtPXK()
        {
            if (this.enuNew_Edit != enuEdit.New)
                return;
            string Ma_Px = txtMa_Px.Text;
            DateTime Ngay_Ct = Library.StrToDate(dteNgay_Ct.Text);
            string strSo_Ct_New = string.Empty;


            Hashtable htParameter = new Hashtable();
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);
            htParameter.Add("MA_PX", this.drEdit["Ma_Px"]);
            htParameter.Add("NGAY_CT", this.drEdit["Ngay_Ct"]);

            strSo_Ct_New = SQLExec.ExecuteReturnValue("sp_TAOPXK", htParameter, CommandType.StoredProcedure).ToString();

            txtMa_Px.Text = strSo_Ct_New;
            this.drEdit["MA_PX"] = strSo_Ct_New;

            if (dtEditCt == null)
                return;

            foreach (DataRow dr in dtEditCt.Rows)
            {
                dr["Ma_PX"] = txtMa_Px.Text;
                dr.AcceptChanges();
            }


            //drEdit.AcceptChanges();

        }

        void frmEditCtTien_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    Delete();
                    break;



                case Keys.F10:
                    //this.InheritVoucher();
                    break;

                case Keys.F11:
                    //this.HanTt();
                    break;

            }

            if (!this.dgvEditCt1.Focused)
                this.dgvEditCt1.ClearSelection();


        }



        void tabVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void tabVoucher_Enter(object sender, EventArgs e)
        {
            this.SelectNextControl(tabVoucher, true, true, true, true);
        }

        public bool UpdateCt(DataTable dtEditCt)
        {
            #region UpdateCt: Cap nhat tung dong trong dtEditCt


            SqlConnection sqlCon = SQLExec.GetNewSQLConnection();
            SqlCommand sqlCom = sqlCon.CreateCommand();

            SqlTransaction sqlTran = sqlCom.Connection.BeginTransaction("Update_Voucher_Tran");

            sqlCom.Transaction = sqlTran;



            int iSave_Ct_Success = 0;


            sqlCom.Parameters.Clear();

            //Xoa du lieu cu trong Chung tu
            if (this.enuNew_Edit == enuEdit.Edit)
            {
                sqlCom.CommandType = CommandType.Text;
                sqlCom.CommandText = "DELETE FROM OM_PXKDetail WHERE MA_PX = @MA_PX";
                sqlCom.Parameters.AddWithValue("@MA_PX", (string)drEdit["MA_PX"]);
                try
                {
                    sqlCom.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            //Luu du lieu vao Ct
            sqlCom.CommandText = "sp_UpdatePXK";
            sqlCom.CommandType = CommandType.StoredProcedure;

            string strKey = "Object_id = Object_id('sp_UpdatePXK')";
            DataTable dtUpdateCt_Para = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);

            foreach (DataRow dr in dtEditCt.Rows)
            {
                //Khong luu nhung dong danh dau xoa
                if (dr.Table.Columns.Contains("Deleted") && (bool)dr["Deleted"])
                    continue;

                sqlCom.Parameters.Clear();

                DataRow drEditCt = dr;
                Common.SetDefaultDataRow(ref drEditCt);

                foreach (DataRow drPara in dtUpdateCt_Para.Rows)
                {
                    string strColumnName = ((string)drPara["Name"]).Replace("@", "");

                    if (!drEditCt.Table.Columns.Contains(strColumnName))
                        continue;

                    sqlCom.Parameters.AddWithValue("@" + strColumnName, drEditCt[strColumnName]);
                }

                try
                {
                    sqlCom.ExecuteNonQuery();
                    SQLExec.Execute("Update GLVoucher SET Ma_CBNV_GH = '" + txtMa_CBNV_GH.Text + "' , So_Ct_Lap = '" + txtMa_Px.Text + "'" +
                                            "WHERE Stt  = '" + dr["Stt"].ToString() + "'");

                    SQLExec.Execute("Update ARBan SET Ma_CBNV_GH = '" + txtMa_CBNV_GH.Text + "' " +
                                          "WHERE Stt  = '" + dr["Stt"].ToString() + "'");
                    iSave_Ct_Success += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra :" + ex.Message);
                    sqlCom.Transaction.Rollback();
                    return false;
                }
            }

            #endregion

            sqlTran.Commit();
            return true;
        }
        private void Save_PXKDetail(DataTable dtEditCt)
        {
            if (true)
            {
                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "Sp_Update_PXKDetail";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ma_PX", txtMa_Px.Text);
                command.Parameters.AddWithValue("@Ma_CBNV_GH", txtMa_CBNV_GH.Text);
                command.Parameters.AddWithValue("@LOAI_PX", txtMa_Ct.Text);
                command.Parameters.AddWithValue("@IS_UPDATE", enuNew_Edit == enuEdit.New? "1" : "0");
                command.Parameters.AddWithValue("@UserId", Element.sysUser_Id);
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@TVP_PXKDETAIL",
                    TypeName = "TVP_PXKDETAIL",
                    Value = this.dtImport,
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

        private DataTable GetStockDetail()
        {
            DataTable dtReturn = new DataTable();


            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "sp_GetPXKDetail";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_PX", txtMa_Px.Text);
            command.Parameters.AddWithValue("@Ngay_Ct", Library.StrToDate(dteNgay_Ct.Text));
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            command.Parameters.AddWithValue("@Is_NotAvail", true);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@TVP_PXKDETAIL",
                TypeName = "TVP_PXKDETAIL",
                Value = this.dtImport,
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
                return null;
            }

        }
        private DataTable CustomerCheckingCredit()
        {
            DataTable dtReturn = new DataTable();

           
            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "sp_PXKCheckingCustomerCredit";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_PX", this.txtMa_Px.Text);
            command.Parameters.AddWithValue("@Ngay_Ct", Library.StrToDate(dteNgay_Ct.Text));
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@TVP_PXKDETAIL",
                TypeName = "TVP_PXKDETAIL",
                Value = this.dtImport,
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
        private DataTable CheckCustomerCreditDetail()
        {
            DataTable dtReturn = new DataTable();


            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "sp_PXKCheckingCustomerCredit";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_PX", txtMa_Px.Text);
            command.Parameters.AddWithValue("@Ngay_Ct", Library.StrToDate(dteNgay_Ct.Text));
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            command.Parameters.AddWithValue("@Is_NotAvail", true); 
            command.Parameters.AddWithValue("@IsChecking", true);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@TVP_PXKDETAIL",
                TypeName = "TVP_PXKDETAIL",
                Value = this.dtImport,
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
                return null;
            }

        }
        void GetInfoPXK()
        {
            if (this.strMa_Ct != "IN")
                return;

            if (this.dtStt != null)
            {
                DataTable dtReturn = new DataTable();
                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "IN_GetPXKInfo";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@SttList",
                    TypeName = "TVP_STTLIST",
                    Value = this.dtStt
                };
                command.Parameters.Add(parameter);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dtReturn);

                    lbtStt.Text = dtReturn.Rows[0][0].ToString();
                }
                catch
                {
                    lbtStt.Text = string.Empty;
                }
                //Hashtable htPara = new Hashtable();
                //htPara.Add("STTLIST", strStt_List);
                //htPara.Add("MA_DVCS", Element.sysMa_DvCs);

                //lbtStt.Text = SQLExec.ExecuteReturnValue("sp_GetPXKInfo", htPara, CommandType.StoredProcedure).ToString();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.dgvEditCt1.ClearSelection(); //Chi co tac dung sau khi show form

            if (this.enuNew_Edit == enuEdit.Edit)
            {
                if (!Common.CheckDataLocked((DateTime)drEdit["Ngay_Ct"]))
                {
                    this.dteNgay_Ct.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;
                }

                if (!Element.sysIs_Admin)
                {
                    string strCreate_User = (string)this.drEdit["User_Crtd"];

                    if (strCreate_User != string.Empty && strCreate_User != Element.sysUser_Id)
                    {
                        string strUser_Allow = (string)SQLExec.ExecuteReturnValue("SELECT Member_ID_Allow FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'") + ",";

                        if (!strUser_Allow.Contains("*,")) //Được phép sửa tất cả
                        {
                            if (!strUser_Allow.Contains(strCreate_User + ","))
                            {
                                this.btgAccept.btAccept.Enabled = false;
                                return;
                            }
                        }
                    }
                }


                if (Voucher.CheckDataLockedCtHanTtPXK(drEdit["Ma_PX"].ToString()))
                {
                    btAddHD.Enabled = false;
                    btCheckStock.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;
                }

                this.btgAccept.btAccept.Enabled = Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_Edit);

            }
            else if (this.enuNew_Edit == enuEdit.New)
            {
                this.btgAccept.btAccept.Enabled = Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_New);

            }
        }

        public string strDiscItem { get; set; }


    }
}
