using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Lists;

namespace Epoint.Modules.AP
{
    public partial class frmCtNM_Edit : frmVoucher_Edit
    {
        private string strTk_NoTmp = string.Empty;
        private string strTk_CoTmp = string.Empty;
        private string strModule = "02";
        private bool bMa_Thue_Changed = false;
        string strColumnNameBeforeAddRow = string.Empty;//Lưu lại cột trước khi thêm mới một hàng

        //Phan bo chi phi truc tiep tren Phieu NM
        public double dbTien_Pb;
        string strLoai_Pb = "1";

        #region Contructor

        public frmCtNM_Edit()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(frmEditCtTien_KeyDown);

            this.btImportExcel.Click += new EventHandler(btnImportExcel_Click);

            this.btHanTt.Click += new EventHandler(btHanTt_Click);

            this.btInherit.Click += new EventHandler(btInherit_Click);

            linkHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(linkHelp_LinkClicked);

            btPb_Cp.Click += new EventHandler(btPb_Cp_Click);
            btPb_ThueNK.Click += new EventHandler(btPb_ThueNK_Click);

            tabVoucher.SelectedIndexChanged += new EventHandler(tabVoucher_SelectedIndexChanged);
            tabVoucher.Enter += new EventHandler(tabVoucher_Enter);

            txtMa_Dt.Validating += new CancelEventHandler(txtMa_Dt_Validating);

            txtMa_Hd.Enter += new EventHandler(txtMa_Hd_Enter);
            txtMa_Hd.Validating += new CancelEventHandler(txtMa_Hd_Validating);

            txtMa_Ct.Enter += new EventHandler(txtMa_Ct_Enter);
            txtMa_Ct.Validating += new CancelEventHandler(txtMa_Ct_Validating);
            txtMa_Ct.TextChanged += new EventHandler(txtMa_Ct_TextChanged);

            txtSo_Ct.Validating += new CancelEventHandler(txtSo_Ct_Validating);
            dteNgay_Ct.Validating += new CancelEventHandler(dteNgay_Ct_Validating);
            txtMa_Quyen.Validating += new CancelEventHandler(txtMa_Quyen_Validating);

            txtMa_Tte.Validating += new CancelEventHandler(txtMa_Tte_Validating);
            numTy_Gia.Leave += new EventHandler(numTy_Gia_Leave);

            txtMa_Thue.Enter += new EventHandler(txtMa_Thue_Enter);
            txtMa_Thue.Validating += new CancelEventHandler(txtMa_Thue_Validating);

            txtMa_So_Thue.Validating += new CancelEventHandler(txtMa_So_Thue_Validating);

            txtTk_No3.Enter += new EventHandler(txtTk_No3_Enter);
            txtTk_No3.Validating += new CancelEventHandler(txtTk_No3_Validating);

            txtTk_Co3.Enter += new EventHandler(txtTk_Co3_Enter);
            txtTk_Co3.Validating += new CancelEventHandler(txtTk_Co3_Validating);

            txtMa_Xe.Validating += new CancelEventHandler(txtMa_Xe_Validating);
            //txtTk_Co.Enter += new EventHandler(txtTk_Co_Enter);
            //txtTk_Co.Validating += new CancelEventHandler(txtTk_Co_Validating);

            numTTien.Validated += new EventHandler(numTTien_Validated);
            numTTien_Nt.Validated += new EventHandler(numTTien_Nt_Validated);
            numTTien3.Validated += new EventHandler(numTTien3_Validated);
            numTTien_Nt3.Validated += new EventHandler(numTTien_Nt3_Validated);

            dgvEditCt1.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEditCt_CellValidating);
            dgvEditCt1.CellValidated += new DataGridViewCellEventHandler(dgvEditCt_CellValidated);
            dgvEditCt1.CellEnter += new DataGridViewCellEventHandler(dgvEditCt_CellEnter);
            dgvEditCt1.CellLeave += new DataGridViewCellEventHandler(dgvEditCt_CellLeave);
            dgvEditCt1.KeyDown += new KeyEventHandler(dgvEditCt_KeyDown);
            dgvEditCt1.CellClick += new DataGridViewCellEventHandler(dgvEditCt1_CellClick);

            dgvEditCt2.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEditCt2_CellValidating);
            dgvEditCt2.CellValidated += new DataGridViewCellEventHandler(dgvEditCt2_CellValidated);
            dgvEditCt2.CellEnter += new DataGridViewCellEventHandler(dgvEditCt_CellEnter);

        }

        public override void Load(enuEdit enuNew_Edit, DataRow drEdit, DataSet dsVoucher)
        {
            this.drEdit = drEdit;
            this.dsVoucher = dsVoucher;

            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            this.strMa_Ct = ((string)drEdit["Ma_Ct"]).Trim();
            this.drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct);
            this.Object_ID = (string)SQLExec.ExecuteReturnValue("SELECT MAX(Object_ID) FROM SYSDMCT WHERE Ma_Ct LIKE '" + this.strMa_Ct + "'");

            if (strMa_Ct == "NK")
                this.pnlVAT.Visible = false;

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
                this.strStt = Common.GetNewStt(strModule, true);
            else
                this.strStt = drEdit["Stt"].ToString();

            this.Build();
            this.FillData();
            this.Init_Ct();

            Common.ScaterMemvar(this, ref drEditPh);

            //Mac dinh
            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                string strSQL = "SELECT TOP 1 Ma_Quyen FROM LIQUYENSO WHERE Month(Ngay_Begin) <= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() +
                                "') AND Month(Ngay_End) >= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Year(Ngay_End) = Year ('"
                                + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "')";
                DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);
                foreach (DataRow drQuyen in dtQuyen_So.Rows)
                {
                    txtMa_Quyen.Text = drQuyen["Ma_Quyen"].ToString();
                }

                //Mac dinh co dua vao so sach
                chkNoPosted.Checked = (bool)drDmCt["Default_Not_Dkhoan"];
            }

            txtMa_Tte.bTextChange = false;
            numTy_Gia.bTextChange = false;
            txtMa_So_Thue.bTextChange = false;
            //txtTk_Co.Enabled = true;

            this.Ma_Tte_Valid();
            this.BindingLanguage();
            this.LoadDicName();

            this.ucNotice.Text = (string)drEditPh["Create_Log"];
            if (!isAccept)
                this.ShowDialog();
            else
                this.ActiveControl = txtMa_Ct;
        }

        #endregion

        #region Phuong thuc

        private void Build()
        {
            dgvEditCt1.bSortMode = false;
            dgvEditCt1.strZone = (string)drDmCt["Zone_EditCt1"];
            dgvEditCt1.BuildGridView();

            dgvEditCt2.bSortMode = false;
            dgvEditCt2.strZone = (string)drDmCt["Zone_EditCt2"];
            dgvEditCt2.BuildGridView();

            dgvEditCt2.Height = dgvEditCt1.Height;
            dgvEditCt2.Width = dgvEditCt1.Width;
            dgvEditCt2.Location = dgvEditCt1.Location;
            dgvEditCt2.Anchor = dgvEditCt1.Anchor;
            //dgvEditCt2.Visible = false;

            //string strMa_Ct_Access = SQLExec.ExecuteReturnValue("SELECT Ma_Ct_Access FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "' AND MEMBER_TYPE = 'U'").ToString();
            //if (!Element.sysIs_Admin)
            //    if (Common.Inlist(strMa_Ct, strMa_Ct_Access))
            //    {
            //        if (!Common.CheckPermission("ACCESS_PRICE", enuPermission_Type.Allow_Access))
            //        {
            //            if (dgvEditCt1.Columns.Contains("TIEN"))
            //                dgvEditCt1.Columns["TIEN"].Visible = false;

            //            dgvEditCt1.Columns["GIA_NT9"].Visible = false;
            //            dgvEditCt1.Columns["TIEN_NT9"].Visible = false;

            //            numTTien0.Visible = false;
            //            numTTien_Nt0.Visible = false;
            //            numTTien.Visible = false;
            //            numTTien_Nt.Visible = false;
            //            numTTien3.Visible = false;
            //            numTTien_Nt3.Visible = false;
            //            numTTien5.Visible = false;
            //            numTTien_Nt5.Visible = false;
            //            numTTien6.Visible = false;
            //            numTTien_Nt6.Visible = false;
            //        }
            //    }
        }

        private void FillData()
        {
            string strKeyFillterCt = " Stt = '" + ((string)drEdit["Stt"]).Trim() + "' ";

            string strSelectPh = " *, TTien0 + TTien3 + TTien5 AS TTien, TTien_Nt0 + TTien_Nt3 + TTien_Nt5 AS TTien_Nt ";
            string strSelectCt = enuNew_Edit == enuEdit.New ? " TOP 1 * " : "*";// enuNew_Edit == enuEdit.New lấy hàng đầu tiên

            dtEditPh = DataTool.SQLGetDataTable((string)drDmCt["Table_Ph"], strSelectPh, strKeyFillterCt, null);
            dtEditCt = DataTool.SQLGetDataTable((string)drDmCt["Table_Ct"], strSelectCt, strKeyFillterCt, null);

            //ThongLH: History
            dtEditCtOrg = dtEditCt.Copy();
            drEditPhOrg = drEdit;

            DataColumn dc = new DataColumn("Deleted", typeof(bool));
            dc.DefaultValue = false;
            dtEditCt.Columns.Add(dc);

            bdsEditCt.DataSource = dtEditCt;

            dgvEditCt1.DataSource = bdsEditCt;
            dgvEditCt1.ClearSelection();

            dgvEditCt2.DataSource = bdsEditCt;
            dgvEditCt2.ClearSelection();
        }

        private void Init_Ct()
        {
            txtMa_Tte.InputMask = (string)Systems.Librarys.Parameters.GetParaValue("MA_TTE_LIST");

            if (dtEditPh.Rows.Count == 0)
                dtEditPh.Rows.Add(dtEditPh.NewRow());

            if (dtEditCt.Rows.Count == 0)
                dtEditCt.Rows.Add(dtEditCt.NewRow());

            drEditPh = dtEditPh.Rows[0];
            drCurrent = dtEditCt.Rows[0];

            if (this.enuNew_Edit == enuEdit.New || this.enuNew_Edit == enuEdit.Copy)
            {
                if (this.enuNew_Edit == enuEdit.New)
                {
                    //Clear Content in drCurrent
                    foreach (DataColumn dcEditCt in dtEditCt.Columns)
                        drCurrent[dcEditCt] = DBNull.Value;

                    Common.SetDefaultDataRow(ref drEditPh);
                    Common.SetDefaultDataRow(ref drCurrent);

                    //Ngầm định 1 số thông tin từ chứng từ cũ
                    if (drEdit != null)
                        Common.CopyDataRow(drEdit, drCurrent, (string)drDmCt["Carry_Header"]);

                    drCurrent["Ma_DvCs"] = Element.sysMa_DvCs;
                    drCurrent["Stt"] = strStt;
                    drCurrent["Stt0"] = 1;
                    drCurrent["Ma_Ct"] = strMa_Ct;
                    drCurrent["Ngay_Ct"] = ((string)Epoint.Systems.Librarys.Parameters.GetParaValue("NGAY_CT") == "0" && drEdit["Ngay_Ct"] != DBNull.Value) ? drEdit["Ngay_Ct"] : DateTime.Now;

                    drCurrent["Tk_No"] = (string)drDmCt["Tk_No"];
                    drCurrent["Tk_Co"] = (string)drDmCt["Tk_Co"];
                    drCurrent["Ma_Tte"] = Element.sysMa_Tte;
                    drCurrent["Ty_Gia"] = 1;
                    drCurrent["Deleted"] = false;

                    //Clear Content in drEditPh
                    foreach (DataColumn dcEditPh in dtEditPh.Columns)
                        drEditPh[dcEditPh] = DBNull.Value;

                    drEditPh["Ma_DvCs"] = drCurrent["Ma_DvCs"];
                    drEditPh["Stt"] = drCurrent["Stt"];
                    drEditPh["Ma_Ct"] = drCurrent["Ma_Ct"];
                    drEditPh["Ngay_Ct"] = drCurrent["Ngay_Ct"];
                }

                //Tinh so chung tu
                if ((bool)SQLExec.ExecuteReturnValue("SELECT Is_So_Ct FROM SYSDMCT WHERE Ma_Ct = '" + strMa_Ct + "'") == true)//1: Tính tự động, 0-Tính theo thủ công từng tháng
                    Cong_So_Ct_Auto();
                else
                {
                    string strLoai_Ma_Ct = ((DateTime)drCurrent["Ngay_Ct"]).Month.ToString().Trim();
                    string strSQLExec = "EXEC Sp_Cong_So_Ct '" + strMa_Ct + "', '" + strLoai_Ma_Ct + "'";

                    DataTable dtSo_Ct = SQLExec.ExecuteReturnDt(strSQLExec);

                    if (dtSo_Ct.Rows.Count > 0)
                        drEditPh["So_Ct"] = drCurrent["So_Ct"] = (string)dtSo_Ct.Rows[0][0];
                }

                if (drEditPh.Table.Columns.Contains("Duyet"))
                {
                    drEditPh["Duyet"] = (bool)drDmCt["Default_Duyet"];
                    drEditPh["Duyet_Log"] = Common.GetCurrent_Log();
                    drEditPh["So_Ct_Lap"] = drCurrent["So_Ct"].ToString();
                }
                if (drEditPh.Table.Columns.Contains("Is_Thue_Vat"))
                    drEditPh["Is_Thue_Vat"] = (bool)drDmCt["Default_VAT"];
            }

            Voucher.Update_Header(this);
            Voucher.Update_Stt(this, strModule);
            //if (strMa_Ct == "MTL")
            //if ((string)drDmCt["Nh_Ct"] == "2")
            //{
            //    lblTk_Co.Name = "lblTk_No";
            //    lblTk_Co.Tag = "Tk_No";
            //    txtTk_Co.Name = "txtTk_No";
            //}            

            // Neu la phieu chi phi
            if (strMa_Ct == "CP")
            {
                if (dgvEditCt1.Columns.Contains("SO_LUONG9"))
                    dgvEditCt1.Columns["SO_LUONG9"].ReadOnly = true;

                if (dgvEditCt1.Columns.Contains("GIA_NT9"))
                    dgvEditCt1.Columns["GIA_NT9"].ReadOnly = true;

                if (dgvEditCt1.Columns.Contains("TIEN_NT6"))
                    dgvEditCt1.Columns["TIEN_NT6"].ReadOnly = true;

                if (dgvEditCt1.Columns.Contains("TK_NO6"))
                    dgvEditCt1.Columns["TK_NO6"].ReadOnly = true;

                if (dgvEditCt1.Columns.Contains("TK_CO6"))
                    dgvEditCt1.Columns["TK_CO6"].ReadOnly = true;

                btPb_Cp.Visible = true;
            }
            else if (strMa_Ct == "NM")
                btPb_Cp.Visible = true;
            else if (strMa_Ct == "NK")
                btPb_ThueNK.Visible = true;

            if (dgvEditCt1.Columns.Contains("TIEN_NT3") && dgvEditCt1.Columns.Contains("TIEN3"))
            {
                dgvEditCt1.Columns["TIEN_NT3"].ReadOnly = true;
                dgvEditCt1.Columns["TIEN3"].ReadOnly = true;
            }

            if (dgvEditCt2.Columns.Contains("TIEN_NT3") && dgvEditCt2.Columns.Contains("TIEN3"))
            {
                dgvEditCt2.Columns["TIEN_NT3"].ReadOnly = true;
                dgvEditCt2.Columns["TIEN3"].ReadOnly = true;
            }

            dgvEditCt1.Columns["Dvt"].ReadOnly = true;
            //BindingTTien            

            //txtInherit.Text = Voucher.GetInheritVoucher(this);

            if (isAccept)
            {
                numTTien0.DataBindings.Clear();
                numTTien_Nt0.DataBindings.Clear();

                numTTien_Nt3.DataBindings.Clear();
                numTTien3.DataBindings.Clear();

                numTTien.DataBindings.Clear();
                numTTien_Nt.DataBindings.Clear();

                numTTien5.DataBindings.Clear();
                numTTien_Nt5.DataBindings.Clear();

                numTTien6.DataBindings.Clear();
                numTTien_Nt6.DataBindings.Clear();

                numTSo_Luong.DataBindings.Clear();
            }

            numTTien0.DataBindings.Add("Value", dtEditPh, "TTien0");
            numTTien_Nt0.DataBindings.Add("Value", dtEditPh, "TTien_Nt0");

            numTTien_Nt3.DataBindings.Add("Value", dtEditPh, "TTien_Nt3");
            numTTien3.DataBindings.Add("Value", dtEditPh, "TTien3");

            numTTien.DataBindings.Add("Value", dtEditPh, "TTien");
            numTTien_Nt.DataBindings.Add("Value", dtEditPh, "TTien_Nt");

            numTTien5.DataBindings.Add("Value", dtEditPh, "TTien5");
            numTTien_Nt5.DataBindings.Add("Value", dtEditPh, "TTien_Nt5");

            numTTien6.DataBindings.Add("Value", dtEditPh, "TTien6");
            numTTien_Nt6.DataBindings.Add("Value", dtEditPh, "TTien_Nt6");

            numTSo_Luong.DataBindings.Add("Value", dtEditPh, "TSo_Luong");
        }

        private void Cong_So_Ct_Auto()
        {
            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                Hashtable htSo_Ct = new Hashtable();
                htSo_Ct.Add("MA_CT", strMa_Ct);
                htSo_Ct.Add("NGAY_CT", Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString());
                htSo_Ct.Add("MA_TTE", drCurrent["Ma_TTe"].ToString());
                htSo_Ct.Add("MA_DVCS", Element.sysMa_DvCs);
                drEditPh["So_Ct"] = drCurrent["So_Ct"] = Convert.ToString(SQLExec.ExecuteReturnValue("sp_Cong_So_Ct_Auto", htSo_Ct, CommandType.StoredProcedure));
                txtSo_Ct.Text = drCurrent["So_Ct"].ToString();
            }
        }
        private void LoadDicName()
        {
            if (txtMa_Ct.Text.Trim() != string.Empty && drDmCt != null)
            {
                dicName.SetValue("Ten_Ct", (string)drDmCt["Ten_Ct"]);
            }

            //txtMa_Dt
            if (txtMa_Dt.Text.Trim() != string.Empty)
            {
                txtTen_Dt.Text = DataTool.SQLGetNameByCode("LIDOITUONG", "Ma_Dt", "Ten_Dt", txtMa_Dt.Text.Trim());
                dicName.SetValue(txtTen_Dt.Name, txtTen_Dt.Text);
            }
            else
                txtTen_Dt.Text = string.Empty;

            //txtMa_Hd
            if (txtMa_Hd.Text.Trim() != string.Empty)
            {
                txtTen_Hd.Text = DataTool.SQLGetNameByCode("LIHOPDONG", "Ma_Hd", "Ten_Hd", txtMa_Hd.Text.Trim());
                dicName.SetValue(txtTen_Hd.Name, txtTen_Hd.Text);
            }
            else
                txtTen_Hd.Text = string.Empty;
        }

        private bool FormCheckValid()
        {
            if (!Common.CheckDataLocked(Library.StrToDate(this.dteNgay_Ct.Text)))
            {
                string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Dữ liệu đã bị khóa" : "Data have been locked";
                Common.MsgCancel(strMsg);
                return false;
            }

            if (Common.GetPartitionCurrent() != 0 && this.enuNew_Edit == enuEdit.Edit && this.drEditPh["Ngay_Ct", DataRowVersion.Original] != DBNull.Value)
            {
                if (((DateTime)this.drEditPh["Ngay_Ct"]).Year != ((DateTime)this.drEditPh["Ngay_Ct", DataRowVersion.Original]).Year)
                {
                    Common.MsgCancel("Dữ liệu đã phân vùng, không cho phép sửa chứng từ từ năm này sang năm khác");
                    return false;
                }
            }

            //Kiểm tra nghiệp vụ hợp lệ
            foreach (DataRow dr in dtEditCt.Rows)
            {
                if ((bool)dr["Deleted"])
                    continue;

                //Kiem tra Ma_Kho -> null thi khong cho luu
                if (dtEditCt.Columns.Contains("Ma_Kho") && Convert.ToString(dr["Ma_Kho"]) == "")
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Mã kho không được bỏ trống !" : "Warehouse code must not be empty";
                    Common.MsgCancel(strMsg);
                    return false;
                }

                if (dtEditCt.Columns.Contains("Tien") && Convert.ToDouble(dr["Tien"]) != 0 && ((string)dr["Tk_No"] == string.Empty || (string)dr["Tk_Co"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán không hợp lệ" : "Transaction invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }
                if (dtEditCt.Columns.Contains("Ma_Thue") && (string)dr["Ma_Thue"] != string.Empty && ((string)dr["Tk_No3"] == string.Empty || (string)dr["Tk_Co3"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán thuế không hợp lệ" : "Transaction VAT invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }
                if (dtEditCt.Columns.Contains("Tien5") && Convert.ToDouble(dr["Tien5"]) != 0 && ((string)dr["Tk_No5"] == string.Empty || (string)dr["Tk_Co5"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán thuế NK không hợp lệ" : "Transaction import tax invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }
            }

            if (!Voucher.CheckDuplicateInvoice(this))
                return false;

            return true;
        }

        public override bool Save()
        {
            //Nếu các dòng bị xóa hết thì ko cho lưu --> do là chứng từ rỗng
            if (dtEditCt.Select("Deleted = false").Length == 0)
                return false;

            Common.GatherMemvar(this, ref this.drEditPh);
            Voucher.Update_Detail(this);

            if (!FormCheckValid())
                return false;

            if (this.enuNew_Edit == enuEdit.New || this.enuNew_Edit == enuEdit.Copy)
            {
                drEditPh["Create_Log"] = Common.GetCurrent_Log();
                drEditPh["LastModify_Log"] = string.Empty;
            }
            else
            {
                drEditPh["LastModify_Log"] = Common.GetCurrent_Log();
                if ((string)drEditPh["Create_Log"] == string.Empty)
                    drEditPh["Create_Log"] = drEditPh["LastModify_Log"];
            }

            Voucher.Update_TTien(this);
            Voucher.Update_Stt(this, strModule);
            Voucher.UpdateSo_Ct(this);

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (dtEditCt.Rows.Count > 0) //Cập nhật lại dữ liệu từ chi tiết lên Header
                    Common.CopyDataRow(dtEditCt.Rows[0], drEditPh, (string)drDmCt["Update_Header"]);

                drEdit = drEditPh;
            }

            ////Sync data-------------
            /*string Is_Sync = Convert.ToString(SQLExec.ExecuteReturnValue("SELECT Parameter_Value FROM SYSPARAMETER WHERE Parameter_ID = 'SYNC_BEGIN'"));
            if (Is_Sync == "1")
            {
                SqlConnection sqlCon = SQLExecSync1.GetNewSQLConnectionSync1();
                if (sqlCon.State != ConnectionState.Open)
                {
                    SQLExec.Execute("UPDATE SYSPARAMETER SET Parameter_Value = 0 WHERE Parameter_ID = 'SYNC_BEGIN'");
                }
                else
                {
                    VoucherSync1.Update_Header(this);
                    VoucherSync1.SQLUpdateCt(this);
                }
            }
            //Update lai Ma_Dvcs
            this.drEditPh["Ma_Dvcs"] = Element.sysMa_DvCs;
            foreach (DataRow drEditCt in dtEditCt.Rows)
            {
                drEditCt["Ma_DvCs"] = Element.sysMa_DvCs;
            }

            dtEditCt.AcceptChanges();
            ////----------------------*/

            return Voucher.SQLUpdateCt(this);
        }

        private void Ma_Tte_Valid()
        {
            string strMa_Tte = txtMa_Tte.Text.Trim();

            if (Common.Inlist(this.strMa_Ct, (string)Epoint.Systems.Librarys.Parameters.GetParaValue("CT_LOCKED_EXCHANGE")))
                numTy_Gia.Enabled = false;
            else
                numTy_Gia.Enabled = true;

            if (Element.sysMa_Tte == strMa_Tte)
            {
                numTy_Gia.Value = 1;
                numTy_Gia.bReadOnly = true;

                this.pnlTTien.Visible = false;
                this.pnlTTien_Nt.Left = this.pnlTTien.Right - this.pnlTTien_Nt.Width;

                if (dgvEditCt1.Columns.Contains("TIEN"))
                    dgvEditCt1.Columns["TIEN"].Visible = false;

                //if (dgvEditCt2.Columns.Contains("TIEN3"))
                //    dgvEditCt2.Columns["TIEN3"].Visible = false;

                if (dgvEditCt1.Columns.Contains("TIEN5"))
                    dgvEditCt1.Columns["TIEN5"].Visible = false;

                if (dgvEditCt2.Columns.Contains("TIEN5"))
                    dgvEditCt2.Columns["TIEN5"].Visible = false;

                if (dgvEditCt2.Columns.Contains("TIEN6"))
                    dgvEditCt2.Columns["TIEN6"].Visible = false;
            }
            else
            {
                numTy_Gia.bReadOnly = false;

                if (dteNgay_Ct.Text != Library.DateToStr((DateTime)drEditPh["Ngay_Ct"]) || txtMa_Tte.bTextChange)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("NGAY_CT", Library.StrToDate(dteNgay_Ct.Text));
                    ht.Add("MA_TTE", strMa_Tte);

                    //numTy_Gia.Value = Convert.ToDouble(SQLExec.ExecuteReturnValue("sp_GetTyGia", ht, CommandType.StoredProcedure));
                }

                this.pnlTTien.Visible = true;
                this.pnlTTien_Nt.Left = this.pnlTTien.Left - this.pnlTTien_Nt.Width;

                if (dgvEditCt1.Columns.Contains("TIEN"))
                    dgvEditCt1.Columns["TIEN"].Visible = true;

                //if (dgvEditCt2.Columns.Contains("TIEN3"))
                //    dgvEditCt2.Columns["TIEN3"].Visible = true;

                if (dgvEditCt1.Columns.Contains("TIEN5"))
                    dgvEditCt1.Columns["TIEN5"].Visible = true;

                if (dgvEditCt2.Columns.Contains("TIEN5"))
                    dgvEditCt2.Columns["TIEN5"].Visible = true;

                if (dgvEditCt2.Columns.Contains("TIEN6"))
                    dgvEditCt2.Columns["TIEN6"].Visible = true;
            }

            if (dteNgay_Ct.Text != Library.DateToStr((DateTime)drEditPh["Ngay_Ct"]) || txtMa_Tte.bTextChange || numTy_Gia.bTextChange)
            {
                Voucher.Update_Detail(this);
                Voucher.Calc_Tien_All(this);
                Voucher.Adjust_TThue_Vat(this, true);

                if (txtMa_Tte.bTextChange)
                    txtMa_Tte.bTextChange = false;
            }

            numTTien_Nt.Scale = numTTien_Nt0.Scale = numTTien_Nt3.Scale = strMa_Tte == Element.sysMa_Tte ? 0 : 2;

            Voucher.FormatTien_Nt(dgvEditCt1, strMa_Tte);
            Voucher.FormatTien_Nt(dgvEditCt2, strMa_Tte);

            dgvEditCt1.ResizeGridView();
            dgvEditCt2.ResizeGridView();
        }

        private bool Ma_Thue_Valid()
        {
            #region NK
            if (strMa_Ct == "NK")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                if (drCurrent["Ma_Thue"] == DBNull.Value)
                    drCurrent["Ma_Thue"] = string.Empty;

                if (drCurrent["Ma_Thue"].ToString().Trim() == string.Empty)
                {
                    drCurrent["Thue_GtGt"] = 0;
                    drCurrent["Ma_So_Thue"] = string.Empty;
                    drCurrent["Ten_DtGtGt"] = string.Empty;
                    drCurrent["Tk_No3"] = string.Empty;
                    drCurrent["Tk_Co3"] = string.Empty;
                    drCurrent["Tien3"] = 0;
                    drCurrent["Tien_Nt3"] = 0;

                    return false;
                }
                string strMa_Thue = (string)drCurrent["Ma_Thue"];

                DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", drCurrent["Ma_Thue"].ToString());
                DataRow drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", drCurrent["Ma_Dt"].ToString());

                if (drDmDt != null)
                {
                    if (this.bMa_Thue_Changed)
                    {
                        drCurrent["Ma_So_Thue"] = drDmDt["Ma_So_Thue"].ToString();
                        drCurrent["Ten_DtGtGt"] = drDmDt["Ten_Dt"].ToString();
                    }
                    else
                    {
                        if (drCurrent["Ma_So_Thue"] == DBNull.Value || (string)drCurrent["Ma_So_Thue"] == string.Empty)
                            drCurrent["Ma_So_Thue"] = drDmDt["Ma_So_Thue"].ToString();

                        if (drCurrent["Ten_DtGtGt"] == DBNull.Value || (string)drCurrent["Ten_DtGtGt"] == string.Empty)
                            drCurrent["Ten_DtGtGt"] = drDmDt["Ten_Dt"].ToString();
                    }
                }

                if (drDmThue != null)
                {
                    if (this.bMa_Thue_Changed)
                    {
                        if (drDmThue["Loai_Thue"].ToString().Trim() == "2")
                        {
                            drCurrent["Tk_No3"] = drCurrent["Tk_No"].ToString();
                            drCurrent["Tk_Co3"] = drDmThue["Tk"].ToString();
                        }
                        else
                        {
                            drCurrent["Tk_No3"] = drDmThue["Tk"].ToString();
                            drCurrent["Tk_Co3"] = drCurrent["Tk_Co"].ToString();
                        }
                    }
                    else
                    {
                        if (drDmThue["Loai_Thue"].ToString().Trim() == "2")
                        {
                            if (drCurrent["Tk_No3"] == DBNull.Value || (string)drCurrent["Tk_No3"] == string.Empty)
                                drCurrent["Tk_No3"] = drCurrent["Tk_No"].ToString();

                            if (drCurrent["Tk_Co3"] == DBNull.Value || (string)drCurrent["Tk_Co3"] == string.Empty)
                                drCurrent["Tk_Co3"] = drDmThue["Tk"].ToString();
                        }
                        else
                        {
                            if (drCurrent["Tk_No3"] == DBNull.Value || (string)drCurrent["Tk_No3"] == string.Empty)
                                drCurrent["Tk_No3"] = drDmThue["Tk"].ToString();

                            if (drCurrent["Tk_Co3"] == DBNull.Value || (string)drCurrent["Tk_Co3"] == string.Empty)
                                drCurrent["Tk_Co3"] = drCurrent["Tk_Co"].ToString();
                        }
                    }
                }

                this.bMa_Thue_Changed = false;
                return true;
            }
            #endregion

            #region NM
            else
            {

                DataRow drEditCt = dtEditCt.Rows[0];

                string strMa_Thue = txtMa_Thue.Text;


                if (strMa_Thue == string.Empty)
                {
                    txtTk_No3.Text = string.Empty;
                    txtTk_Co3.Text = string.Empty;
                    txtTen_DtGtgt.Text = string.Empty;
                    txtMa_So_Thue.Text = string.Empty;

                    txtTk_No3.Enabled = false;
                    txtTk_Co3.Enabled = false;
                    txtTen_DtGtgt.Enabled = false;
                    txtMa_So_Thue.Enabled = false;
                    numTTien_Nt3.Enabled = false;
                    numTTien3.Enabled = false;

                    return false;
                }
                else
                {
                    txtTk_No3.Enabled = true;
                    txtTk_Co3.Enabled = true;
                    txtTen_DtGtgt.Enabled = true;
                    txtMa_So_Thue.Enabled = true;
                    numTTien_Nt3.Enabled = true;
                    numTTien3.Enabled = true;
                }

                DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", strMa_Thue);

                if (drDmThue != null)
                {
                    if (txtMa_Thue.bTextChange)
                    {
                        if ((string)drDmCt["Nh_Ct"] == "1")
                        {
                            txtTk_No3.Text = (string)drDmThue["Tk"];
                            txtTk_Co3.Text = (string)drEditCt["Tk_Co"];
                        }
                        else
                        {
                            txtTk_No3.Text = (string)drEditCt["Tk_No"];
                            txtTk_Co3.Text = (string)drDmThue["Tk"];
                        }
                    }
                    else
                    {
                        if ((string)drDmCt["Nh_Ct"] == "1")
                        {
                            if (txtTk_No3.Text.Trim() == string.Empty)
                                txtTk_No3.Text = (string)drDmThue["Tk"];

                            if (txtTk_Co3.Text.Trim() == string.Empty)
                                txtTk_Co3.Text = (string)drEditCt["Tk_Co"];
                        }
                        else
                        {
                            if (txtTk_No3.Text.Trim() == string.Empty)
                                txtTk_No3.Text = (string)drEditCt["Tk_No"];

                            if (txtTk_Co3.Text.Trim() == string.Empty)
                                txtTk_Co3.Text = (string)drDmThue["Tk"];
                        }
                    }
                }

                string strMa_Dt = txtMa_Dt.Text;
                DataRow drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", drCurrent["Ma_Dt"].ToString());

                if (drDmDt != null)
                {
                    if (txtMa_Thue.bTextChange)
                    {
                        txtTen_DtGtgt.Text = drDmDt["Ten_Dt"].ToString();
                        txtMa_So_Thue.Text = drDmDt["Ma_So_Thue"].ToString();
                    }
                    else
                    {
                        if (txtTen_DtGtgt.Text.Trim() == string.Empty)
                            txtTen_DtGtgt.Text = (string)drDmDt["Ten_Dt"];

                        if (txtMa_So_Thue.Text.Trim() == string.Empty)
                            txtMa_So_Thue.Text = (string)drDmDt["Ma_So_Thue"];
                    }
                }
            }
            #endregion
            return true;
        }

        private bool CellKeyEnter()
        {//Ham thuc hien phim Enter: true: thuc hien thanh cong, false: khong thuc hien duoc

            if (dgvEditCt1.CurrentCell == null)
                return false;

            DataGridViewCell dgvCell = dgvEditCt1.CurrentCell;
            string strCurrentColumn = dgvCell.OwningColumn.Name.ToUpper();

            #region Enter tai Ten_Vt
            if (Common.Inlist(strCurrentColumn, "TEN_VT"))
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                if (drCurrent["Ma_Vt"] == DBNull.Value || (string)drCurrent["Ma_Vt"] == string.Empty)
                {
                    bool bIsCurrentLastRow = dgvEditCt1.bIsCurrentLastRow;

                    if (bdsEditCt.Count > 1)
                    {
                        bdsEditCt.RemoveCurrent();
                        dtEditCt.AcceptChanges();
                    }

                    if (bIsCurrentLastRow)
                    {
                        this.dgvEditCt1.ClearSelection();
                        this.SelectNextControl(dgvEditCt1, true, true, true, true);
                    }

                    return true;
                }

                return false;
            }
            #endregion

            if (dgvEditCt1.Columns.Contains("THUE_NK"))
            {
                //Phiếu nhập khẩu
                #region Enter tai Thue_NK

                if (Common.Inlist(strCurrentColumn, "TIEN_NT5_"))
                {
                    if (dgvCell.FormattedValue.ToString() == string.Empty)
                    {
                        if (dgvEditCt1.bIsCurrentLastRow)
                        {
                            if (!Voucher.AddRow(this))
                            {
                                this.dgvEditCt1.ClearSelection();
                                this.SelectNextControl(dgvEditCt1, true, true, true, true);
                            }
                            else
                                dgvEditCt1.FocusNextFirstCell();
                        }
                        else
                            dgvEditCt1.FocusNextFirstCell();

                        return true;
                    }

                    return false;
                }
                #endregion

                #region Enter tai Tk_No5, Tk_Co5
                if (Common.Inlist(strCurrentColumn, "TK_CO5"))
                {
                    if (dgvEditCt1.bIsCurrentLastRow)
                    {
                        if (!Voucher.AddRow(this))
                            //this.SelectNextControl(dgvEditCt, true, true, true, true); Khong dung duoc lenh nay
                            return false;
                        else
                            dgvEditCt1.FocusNextFirstCell();

                        return true;
                    }

                    return false;
                }
                #endregion
            }
            else
            {
                //Phiếu nhập mua
                #region Enter tai TIEN_NT9
                if (Common.Inlist(strCurrentColumn, "TIEN_NT9"))
                {
                    if (txtMa_Tte.Text.Trim() == Element.sysMa_Tte)
                    {
                        // Cap nhat tien TIEN_NT9 truoc khi xuong dong
                        double dbTien_Nt9 = 0;
                        if (double.TryParse(dgvEditCt1.CurrentCell.FormattedValue.ToString().Trim(), out dbTien_Nt9))
                        {
                            dgvEditCt1.CancelEdit();
                            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                            drCurrent["TIEN_NT9"] = dbTien_Nt9;
                            Voucher.Calc_So_Luong(drCurrent);
                            Voucher.Update_TTien(this);
                        }
                        else if (dbTien_Nt9 == 0)
                        {
                            drCurrent["GIA_NT9"] = 0;
                            Voucher.Calc_So_Luong(drCurrent);
                            Voucher.Update_TTien(this);

                        }

                        if (dgvEditCt1.bIsCurrentLastRow)
                        {
                            if (!Voucher.AddRow(this))
                            {
                                this.dgvEditCt1.ClearSelection();
                                this.SelectNextControl(dgvEditCt1, true, true, true, true);
                            }
                            else
                            {
                                strColumnNameBeforeAddRow = strCurrentColumn;
                                dgvEditCt1.FocusNextFirstCell();
                                return true;
                            }
                        }
                        else
                        {
                            strColumnNameBeforeAddRow = strCurrentColumn;
                            dgvEditCt1.FocusNextFirstCell();
                        }
                    }
                    return false;
                }

                #endregion

                //#region Enter tai SO_LUONG9 --> Phan quyen truy cap gia, tien
                //string strMa_Ct_Access = SQLExec.ExecuteReturnValue("SELECT Ma_Ct_Access FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "' AND MEMBER_TYPE = 'U'").ToString();
                //if (!Element.sysIs_Admin)
                //    if (Common.Inlist(strMa_Ct, strMa_Ct_Access))
                //    {
                //        if (!Common.CheckPermission("ACCESS_PRICE", enuPermission_Type.Allow_Access))
                //        {
                //            if (Common.Inlist(strCurrentColumn, "SO_LUONG9"))
                //            {
                //                if (txtMa_Tte.Text.Trim() == Element.sysMa_Tte)
                //                {
                //                    // Cap nhat tien SO_LUONG9 truoc khi xuong dong
                //                    double dbSo_Luong9 = 0;
                //                    if (double.TryParse(dgvEditCt1.CurrentCell.FormattedValue.ToString().Trim(), out dbSo_Luong9))
                //                    {
                //                        dgvEditCt1.CancelEdit();
                //                        drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                //                        drCurrent["SO_LUONG9"] = dbSo_Luong9;
                //                        Voucher.Calc_So_Luong(drCurrent);
                //                        Voucher.Update_TTien(this);
                //                    }

                //                    if (dgvEditCt1.bIsCurrentLastRow)
                //                    {
                //                        if (!Voucher.AddRow(this))
                //                        {
                //                            this.dgvEditCt1.ClearSelection();
                //                            this.SelectNextControl(dgvEditCt1, true, true, true, true);
                //                        }
                //                        else
                //                        {
                //                            strColumnNameBeforeAddRow = strCurrentColumn;
                //                            dgvEditCt1.FocusNextFirstCell();
                //                            return true;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        strColumnNameBeforeAddRow = strCurrentColumn;
                //                        dgvEditCt1.FocusNextFirstCell();
                //                    }
                //                }
                //                return false;
                //            }
                //        }
                //    }

                //#endregion

                #region Enter TIEN
                if (Common.Inlist(strCurrentColumn, "TIEN"))
                {
                    // Cap nhat tien TIEN truoc khi xuong dong
                    double dbTien = 0;
                    if (double.TryParse(dgvEditCt1.CurrentCell.FormattedValue.ToString().Trim(), out dbTien))
                    {
                        dgvEditCt1.CancelEdit();
                        drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                        drCurrent["TIEN"] = dbTien;
                        Voucher.Calc_So_Luong(drCurrent);
                        Voucher.Update_TTien(this);
                    }

                    if (dgvEditCt1.bIsCurrentLastRow)
                    {
                        if (!Voucher.AddRow(this))
                            return false;
                        else
                        {
                            strColumnNameBeforeAddRow = strCurrentColumn;
                            dgvEditCt1.FocusNextFirstCell();
                        }

                        return true;
                    }

                    return false;
                }
                #endregion
            }
            //dtEditCt.AcceptChanges();
            return false;
        }

        private void Phan_Bo_Thue_Nk()
        {
            frmPhanBoThueNk frm = new frmPhanBoThueNk();
            frm.ShowDialog();

            if (frm.isAccept)
            {
                double dTTien5 = frm.numTTien5.Value;
                string strLoai_Pb = frm.txtLoai_Pb.Text;

                Voucher.Phan_Bo_Thue_Nk(this, dTTien5, strLoai_Pb);
            }
        }

        private void Phan_Bo_Cp()
        {
            this.strMa_Ct = ((string)drEdit["Ma_Ct"]).Trim();

            //Phan bo chi phi truc tiep trên Phieu NM
            if (strMa_Ct == "NM")
            {
                double dbTTien_Nt = Common.SumDCValue(dtEditCt, "Tien_Nt", "");
                double dbTSo_Luong = Common.SumDCValue(dtEditCt, "So_Luong9", "");

                frmPbCp frmPb = new frmPbCp();
                frmPb.Load(this);
                dbTien_Pb = frmPb.numTien_Pb_Nt.Value;
                strLoai_Pb = frmPb.txtLoai_Pb.Text;

                if (frmPb.isAccept)//Chap nhap phan bo
                {
                    int iRow = dgvEditCt1.RowCount;

                    DataRow drEditCt = dtEditCt.NewRow();
                    Common.CopyDataRow(dtEditCt.Rows[0], drEditCt);

                    for (int i = 0; i < iRow; i++)
                    {
                        DataRow drEditCtNew = dtEditCt.NewRow();
                        Common.CopyDataRow(drEditCt, drEditCtNew);

                        drEditCtNew["Ma_Vt"] = dgvEditCt1.Rows[i].Cells["Ma_Vt"].Value.ToString();
                        drEditCtNew["Ten_Vt"] = dgvEditCt1.Rows[i].Cells["Ten_Vt"].Value.ToString();
                        drEditCtNew["Ma_Kho"] = "";
                        drEditCtNew["Dvt"] = dgvEditCt1.Rows[i].Cells["Dvt"].Value.ToString();
                        drEditCtNew["Tk_No"] = dgvEditCt1.Rows[i].Cells["Tk_No"].Value.ToString();
                        drEditCtNew["Tk_Co"] = dgvEditCt1.Rows[i].Cells["Tk_Co"].Value.ToString();
                        drEditCtNew["So_Luong9"] = 0;
                        drEditCtNew["Gia_Nt9"] = 0;
                        drEditCtNew["Gia_Nt"] = 0;
                        drEditCtNew["Gia"] = 0;
                        if (strLoai_Pb == "1")//Phan bo theo gia tri
                        {
                            drEditCtNew["Tien_Nt9"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["Tien_Nt9"].Value) * dbTien_Pb) / dbTTien_Nt, 0);
                            drEditCtNew["Tien_Nt"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["Tien_Nt9"].Value) * dbTien_Pb) / dbTTien_Nt, 0);
                            drEditCtNew["Tien"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["Tien"].Value) * dbTien_Pb) / dbTTien_Nt, 0);
                        }
                        else //Phan bo theo so luong
                        {
                            drEditCtNew["Tien_Nt9"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["So_Luong9"].Value) * dbTien_Pb) / dbTSo_Luong, 0);
                            drEditCtNew["Tien_Nt"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["So_Luong9"].Value) * dbTien_Pb) / dbTSo_Luong, 0);
                            drEditCtNew["Tien"] = Math.Round((Convert.ToDouble(dgvEditCt1.Rows[i].Cells["So_Luong9"].Value) * dbTien_Pb) / dbTSo_Luong, 0);
                        }
                        drEditCtNew["Stt0"] = Common.MaxDCValue(dtEditCt, "Stt0") + 1;

                        dtEditCt.Rows.Add(drEditCtNew);
                    }
                    dtEditCt.AcceptChanges();
                }
            }
            else //Phan bo tren Phieu chi phi mua hang
            {
                frmPbCp_Dkl frm = new frmPbCp_Dkl();
                frm.Load(this);
            }
            Voucher.Update_TTien(this);
            this.dgvEditCt1.Focus();
        }

        private void TTien_Valid()
        {
            numTTien0.Value = numTTien_Nt0.Value * numTy_Gia.Value;

            if (numTTien3.Value == 0)
                numTTien3.Value = numTTien_Nt3.Value * numTy_Gia.Value;
            else if (numTTien_Nt3.Value == 0 && numTy_Gia.Value != 0)
                numTTien_Nt3.Value = numTTien3.Value / numTy_Gia.Value;

            this.drEditPh["TTien0"] = numTTien0.Value;
            this.drEditPh["TTien_Nt0"] = numTTien_Nt0.Value;
            this.drEditPh["TTien3"] = numTTien3.Value;
            this.drEditPh["TTien_Nt3"] = numTTien_Nt3.Value;

            this.drEditPh["TTien"] = Convert.ToDouble(this.drEditPh["TTien0"]) + Convert.ToDouble(this.drEditPh["TTien3"]);
            this.drEditPh["TTien_Nt"] = Convert.ToDouble(this.drEditPh["TTien_Nt0"]) + Convert.ToDouble(this.drEditPh["TTien_Nt3"]);

            if (numTTien_Nt3.bTextChange || numTTien3.bTextChange)
                Voucher.Adjust_TThue_Vat(this, false);
        }
        public void Update_Gia_Vt(DataRow drEditCt)
        {
            //Thong: Lay giá gần nhất từ chứng từ
            if ((bool)drDmCt["Is_Gia"])
            {
                //Chi cap nhat gia vat tu khi co so luong
                if (drEditCt["Ma_Vt"] == DBNull.Value || (string)drEditCt["Ma_Vt"] == string.Empty)
                    return;

                if (drEditCt["So_Luong9"] == DBNull.Value || Convert.ToDouble(drEditCt["So_Luong9"]) == 0)
                    return;

                if (drEditCt["Gia_Nt9"] != DBNull.Value && Convert.ToDouble(drEditCt["Gia_Nt9"]) != 0)
                    return;

                Hashtable htParameter = new Hashtable();
                htParameter.Add("MA_VT", (string)drEditCt["Ma_Vt"]);
                htParameter.Add("MA_DT", (string)drEditCt["Ma_Dt"]);
                htParameter.Add("MA_CT", strMa_Ct);
                htParameter.Add("NGAY_CT", this.dteNgay_Ct.Text);

                drEditCt["Gia_Nt9"] = SQLExec.ExecuteReturnValue("sp_GetGiaMax", htParameter, CommandType.StoredProcedure);
            }

            else if (!(bool)drDmCt["Is_Gia"])//Lấy giá từ chính sách giá
            {
                if (drEditCt["Ma_Vt"] == DBNull.Value || (string)drEditCt["Ma_Vt"] == string.Empty)
                    return;

                if (drEditCt["So_Luong9"] == DBNull.Value || Convert.ToDouble(drEditCt["So_Luong9"]) == 0)
                    return;

                if (drEditCt["Gia_Nt9"] != DBNull.Value && Convert.ToDouble(drEditCt["Gia_Nt9"]) != 0)
                    return;

                Hashtable htParameter = new Hashtable();
                htParameter.Add("MA_VT", (string)drEditCt["Ma_Vt"]);
                htParameter.Add("MA_DT", (string)drEditCt["Ma_Dt"]);
                htParameter.Add("DVT", (string)drEditCt["Dvt"]);
                htParameter.Add("NGAY_CT", this.dteNgay_Ct.Text);

                drEditCt["Gia_Nt9"] = SQLExec.ExecuteReturnValue("sp_GetGiaMua", htParameter, CommandType.StoredProcedure);
            }
        }

        private void HanTt()
        {
            frmHanTt_View frmHanTt = new frmHanTt_View();
            frmHanTt.Load(this);

            //Khóa không cho người dùng sửa lại dữ liệu sau khi đã tick thanh toán
            if (dtHanTt0 != null && dtHanTt0.Select("Thanh_Toan = true").Length > 0)
            {
                this.dgvEditCt1.ReadOnly = true;
                this.dteNgay_Ct.Enabled = false;
                this.txtMa_Tte.Enabled = false;
                this.numTy_Gia.Enabled = false;
                this.txtMa_Dt.Enabled = false;
                this.numHan_Tt.Enabled = false;
            }
            else
            {
                this.dgvEditCt1.ReadOnly = false;
                this.dteNgay_Ct.Enabled = true;
                this.txtMa_Tte.Enabled = true;
                this.numTy_Gia.Enabled = true;
                this.txtMa_Dt.Enabled = true;
                this.numHan_Tt.Enabled = true;
            }
        }

        //Tính số chứng từ khi chọn lại Mã chứng từ
        private void TinhSoCt()
        {
            //Tinh so chung tu
            string strLoai_Ma_Ct = (Convert.ToDateTime(this.dteNgay_Ct.Text)).Month.ToString().Trim();
            string strSQLExec = "EXEC Sp_Cong_So_Ct '" + this.txtMa_Ct.Text + "', '" + strLoai_Ma_Ct + "'";

            DataTable dtSo_Ct = SQLExec.ExecuteReturnDt(strSQLExec);

            if (dtSo_Ct.Rows.Count > 0)
            {
                txtSo_Ct.Text = (string)dtSo_Ct.Rows[0][0];

                Voucher.Update_Detail(this, "So_Ct");
            }
        }

        private void InheritVoucher() //Kế thừa chứng từ từ phiếu khác
        {
            Voucher.Update_Header(this);
            Voucher.Update_Detail(this);

            frmInheritVoucher_Filter frm = new frmInheritVoucher_Filter();
            frm.Load(this);

            Voucher.Update_Detail(this);
            Voucher.Calc_So_Luong_All(this);
            Voucher.Update_TTien(this);
        }

        #endregion

        #region Su kien

        #region FormEvent

        void btnImportExcel_Click(object sender, EventArgs e)
        {
            Voucher.ImportCtExcel(this);
        }

        void btHanTt_Click(object sender, EventArgs e)
        {
            this.HanTt();
        }

        void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string currentDir = Environment.CurrentDirectory;
            System.Diagnostics.Process.Start(currentDir + @"\Help\" + drDmCt["Help_File"]);
        }

        void btInherit_Click(object sender, EventArgs e)
        {
            this.InheritVoucher();

            txtMa_Tte.Text = this.strMa_Tte;
            numTy_Gia.Value = this.dbTy_Gia;

            this.Ma_Tte_Valid();
            dgvEditCt1.Select();
        }

        void btPb_Cp_Click(object sender, EventArgs e)
        {
            Phan_Bo_Cp();
        }

        void btPb_ThueNK_Click(object sender, EventArgs e)
        {
            Phan_Bo_Thue_Nk();
        }

        void txtMa_Ct_Enter(object sender, EventArgs e)
        {
            string strCreate_Log = Common.Show_Log((string)drEditPh["Create_Log"]);
            string strLastModify_Log = Common.Show_Log((string)drEditPh["LastModify_Log"]);
            string strLog = string.Empty;
            strLog += strCreate_Log != string.Empty ? "; Create: " + strCreate_Log : "";
            strLog += strLastModify_Log != string.Empty ? "; Last Modify: " + strLastModify_Log : "";

            this.ucNotice.SetText(txtMa_Ct.Text, dicName.GetValue("Ten_Ct") + strLog);
        }
        void txtMa_Ct_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Ct.Text.Trim();
            bool bRequire = true;
            string strKey = "(Table_Ct = '" + (string)drDmCt["Table_Ct"] + "')";

            frmQuickLookup frmLookup = new frmQuickLookup("SYSDMCT", "DMCT");
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "SYSDMCT", "Ma_Ct", strValue, bRequire, strKey);

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
                txtMa_Ct.Text = string.Empty;
            else
            {
                txtMa_Ct.Text = drLookup["Ma_Ct"].ToString();

                dicName.SetValue("Ten_Ct", drLookup["Ten_Ct"].ToString());
            }

            //Tính lại Số chứng từ trong truờng hợp chọn lại Ma_Ct khác
            if (this.enuNew_Edit != enuEdit.Edit && txtMa_Ct.bTextChange)
            {
                this.TinhSoCt();
            }
        }
        void txtMa_Ct_TextChanged(object sender, EventArgs e)
        {
            this.drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct);
        }

        void txtSo_Ct_Validating(object sender, CancelEventArgs e)
        {
            if (txtSo_Ct.Text == string.Empty)
                return;

            string strTablePh = (string)drDmCt["Table_Ph"];
            string strMa_Ct = txtMa_Ct.Text;
            string strSo_Ct = txtSo_Ct.Text;

            DateTime dNgay_Ct = Library.StrToDate(dteNgay_Ct.Text);

            string strSQLExec = "SELECT COUNT(Stt) FROM " + strTablePh + " WHERE Stt <> @Stt AND So_Ct = @So_Ct AND MONTH(Ngay_Ct) = MONTH(@Ngay_Ct) AND YEAR(Ngay_Ct) = @Nam AND Ma_Ct = @Ma_Ct AND Ma_DvCs = @Ma_DvCs";

            Hashtable ht = new Hashtable();
            ht.Add("MA_CT", strMa_Ct);
            ht.Add("SO_CT", strSo_Ct);
            ht.Add("NGAY_CT", dNgay_Ct);
            ht.Add("NAM", dNgay_Ct.Year);
            ht.Add("STT", drEditPh["Stt"]);
            ht.Add("MA_DVCS", Element.sysMa_DvCs);

            if (Convert.ToInt32(SQLExec.ExecuteReturnValue(strSQLExec, ht, CommandType.Text)) > 0)
            {
                if (!Common.MsgYes_No("Chứng từ số: " + txtSo_Ct.Text + " Ngày: " + dteNgay_Ct.Text + " đã tồn tại.\n Bạn có muốn tiếp tục không ?"))
                    e.Cancel = true;
            }

            //Cap nhat So hoa don            
            //txtSo_Ct0.Text = txtSo_Ct.Text;
        }
        void dteNgay_Ct_Validating(object sender, CancelEventArgs e)
        {
            //Quyen so
            if (txtMa_Quyen.Text == "")
            {
                string strSQL = "SELECT TOP 1 Ma_Quyen FROM LIQUYENSO WHERE Month(Ngay_BatDau) <= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() +
                                "') AND Month(Ngay_KetThuc) >= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Year(Ngay_KetThuc) = Year ('"
                                + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Ma_Ct = '" + strMa_Ct + "'";
                DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);
                foreach (DataRow drQuyen in dtQuyen_So.Rows)
                {
                    txtMa_Quyen.Text = drQuyen["Ma_Quyen"].ToString();
                }
            }

            //Tu dong tao so chung tu
            if ((bool)SQLExec.ExecuteReturnValue("SELECT Is_So_Ct FROM SYSDMCT WHERE Ma_Ct = '" + strMa_Ct + "'") == true)//1: Tính tự động, 0-Tính theo thủ công từng tháng
                Cong_So_Ct_Auto();

            this.Ma_Tte_Valid();
            Common.GatherMemvar(this, ref drEditPh);

            //Cap nhat Ngay hoa don            
            //dteNgay_Ct0.Text = dteNgay_Ct.Text;
        }

        void txtMa_Quyen_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Quyen.Text.Trim();
            bool bRequire = false;

            //frmQuyenSo frmLookup = new frmQuyenSo();
            DataRow drLookup = Lookup.ShowLookup("Ma_Quyen", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Quyen.Text = string.Empty;
            }
            else
            {
                txtMa_Quyen.Text = drLookup["Ma_Quyen"].ToString();
            }

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_Tte_Validating(object sender, CancelEventArgs e)
        {
            this.Ma_Tte_Valid();
        }
        void numTy_Gia_Leave(object sender, EventArgs e)
        {
            this.Ma_Tte_Valid();
        }

        void txtMa_Dt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Dt.Text.Trim();
            bool bRequire = true;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Dt", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Dt.Text = string.Empty;
                txtTen_Dt.Text = string.Empty;
            }
            else
            {
                txtMa_Dt.Text = drLookup["Ma_Dt"].ToString();
                txtTen_Dt.Text = drLookup["Ten_Dt"].ToString();

                if (txtMa_Dt.bTextChange && this.enuNew_Edit == enuEdit.New)//Khi them moi thi cap nhat Ong_Ba, Dia_Chi theo Ma_Dt
                {
                    txtOng_Ba.Text = drLookup["Ong_Ba"].ToString() == string.Empty ? drLookup["Ten_Dt"].ToString() : drLookup["Ong_Ba"].ToString();
                    txtDia_Chi.Text = drLookup["Dia_Chi"].ToString();
                }
                if (txtMa_Dt.bTextChange && this.enuNew_Edit == enuEdit.Edit)//Khi sua chung tu
                {
                    if (txtOng_Ba.Text == "")
                        txtOng_Ba.Text = drLookup["Ong_Ba"].ToString() == string.Empty ? drLookup["Ten_Dt"].ToString() : drLookup["Ong_Ba"].ToString();
                    if (txtDia_Chi.Text == "")
                        txtDia_Chi.Text = drLookup["Dia_Chi"].ToString();
                }
            }

            //dicName[txtTen_Dt.Name] = txtTen_Dt.Text;

            Voucher.Update_Detail(this, "Ma_Dt");

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        void txtMa_Hd_Enter(object sender, EventArgs e)
        {
            txtTen_Hd.Text = dicName.GetValue(txtTen_Hd.Name);
        }
        void txtMa_Hd_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Hd.Text.Trim();
            bool bRequire = false;
            string strKeyValid = "";

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Hd", strValue, bRequire, "", strKeyValid);

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Hd.Text = string.Empty;
                txtTen_Hd.Text = string.Empty;
            }
            else
            {
                txtMa_Hd.Text = drLookup["Ma_Hd"].ToString();
                txtTen_Hd.Text = drLookup["Ten_Hd"].ToString();

                if (txtMa_Hd.bTextChange)
                {
                    txtMa_Dt.Text = drLookup["Ma_Dt"].ToString();
                    DataRow drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", (string)drLookup["Ma_Dt"]);
                    if (drDmDt != null)
                    {
                        txtTen_Dt.Text = (string)drDmDt["Ten_Dt"];
                        if ((string)drDmDt["Ong_Ba"] != string.Empty)
                            txtOng_Ba.Text = (string)drDmDt["Ong_Ba"];
                        else
                            txtOng_Ba.Text = (string)drDmDt["Ten_Dt"];

                        txtDia_Chi.Text = (string)drDmDt["Dia_Chi"];
                    }
                }
            }

            dicName.SetValue(txtTen_Hd.Name, txtTen_Hd.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        //void txtMa_Km_Enter(object sender, EventArgs e)
        //{
        //    lbtTen_Km.Text = dicName.GetValue(lbtTen_Km.Name);
        //}
        //void txtMa_Km_Validating(object sender, CancelEventArgs e)
        //{
        //    string strValue = txtMa_Km.Text.Trim();
        //    object objReturn = null;

        //    bool bRequire = false;
        //    drCurrent = ((DataRowView)bdsEditCt.Current).Row;
        //    string strTk_No = (string)drCurrent["Tk_No"];
        //    string strTk_Co = (string)drCurrent["Tk_Co"];

        //    objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_No + "'");
        //    if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
        //        bRequire = true;
        //    else
        //    {
        //        objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_Co + "'");
        //        if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
        //            bRequire = true;
        //    }

        //    //
        //    DataRow drLookup = Lookup.ShowLookup("Ma_Km", strValue, bRequire, "");

        //    if (bRequire && drLookup == null)
        //        e.Cancel = true;

        //    if (drLookup == null)
        //    {
        //        txtMa_Km.Text = string.Empty;
        //        lbtTen_Km.Text = string.Empty;
        //    }
        //    else
        //    {
        //        txtMa_Km.Text = drLookup["Ma_Km"].ToString();
        //        lbtTen_Km.Text = drLookup["Ten_Km"].ToString();
        //    }

        //    dicName.SetValue(lbtTen_Km.Name, lbtTen_Km.Text);

        //    if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
        //    {
        //        ((txtTextLookup)sender).AutoFilter.Visible = false;
        //        this.SelectNextControl(this.ActiveControl, true, true, true, true);
        //    }
        //}

        void txtMa_Thue_Enter(object sender, EventArgs e)
        {
            this.ucNotice.Text = dicName.GetValue("Ten_Thue");
        }
        void txtMa_Thue_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Thue.Text;
            bool bRequire = false;

            string strMa_Thue_Old = drEditPh["Ma_Thue"] == DBNull.Value ? string.Empty : (string)drEditPh["Ma_Thue"];

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Thue", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup != null)
            {
                txtMa_Thue.Text = (string)drLookup["Ma_Thue"];
                dicName.SetValue("Ten_Thue", drLookup["Ten_Thue"].ToString());
                txtMa_HoaDon.Text = (string)drLookup["Ma_HoaDon"];
                txtKh_HoaDon.Text = (string)drLookup["Kh_HoaDon"];

                this.drEditPh["Thue_Gtgt"] = drLookup["Thue_Suat"];
            }

            this.Ma_Thue_Valid();

            Voucher.Update_Detail(this, "Ma_Thue, Thue_Gtgt");

            if (txtMa_Thue.bTextChange)
                Voucher.Adjust_TThue_Vat(this, true);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_So_Thue_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_So_Thue.Text.Trim();

            bool bRequire = false;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            if (strValue == "/" || strValue == @"\")
            {
                //
                DataRow drLookup = Lookup.ShowLookup("Ma_So_Thue", strValue, bRequire, "");

                if (bRequire && drLookup == null)
                    e.Cancel = true;

                if (drLookup == null)
                {
                    txtMa_So_Thue.Text = string.Empty;
                }
                else
                {
                    txtMa_So_Thue.Text = drLookup["Ma_So_Thue"].ToString();
                    txtTen_DtGtgt.Text = drLookup["Ten_Dt"].ToString();
                }
            }
            else if (strValue != string.Empty && txtMa_So_Thue.bTextChange)
            {
                DataTable dtLookup = SQLExec.ExecuteReturnDt("SELECT * FROM LIDOITUONG WHERE Ma_So_Thue = '" + strValue + "'");

                if (dtLookup != null)
                {
                    if (dtLookup.Rows.Count == 1)
                    {
                        txtMa_So_Thue.Text = dtLookup.Rows[0]["Ma_So_Thue"].ToString();
                        txtTen_DtGtgt.Text = dtLookup.Rows[0]["Ten_Dt"].ToString();
                    }
                    else
                    {
                        dtLookup = SQLExec.ExecuteReturnDt("SELECT * FROM LIDOITUONG WHERE Ma_So_Thue LIKE '" + strValue + "%'");

                        if (dtLookup.Rows.Count >= 1)
                        {
                            //
                            DataRow drLookup = Lookup.ShowLookup("Ma_So_Thue", strValue, bRequire, "");

                            if (bRequire && drLookup == null)
                                e.Cancel = true;

                            if (drLookup == null)
                            {
                                txtMa_So_Thue.Text = string.Empty;
                            }
                            else
                            {
                                txtMa_So_Thue.Text = drLookup["Ma_So_Thue"].ToString();
                                txtTen_DtGtgt.Text = drLookup["Ten_Dt"].ToString();
                            }
                        }
                        else
                        {
                            //if (Common.MsgYes_No("Bạn có chắc chắn thêm mới đối tượng - mã số thuế?"))
                            //{
                            //    DataRow drNew = dtLookup.NewRow();
                            //    drNew["Ma_Dt"] = drNew["Ma_So_Thue"] = strValue;
                            //    drNew["Ma_Nh_Dt"] = "MA_SO_THUE";

                            //    frmDoiTuong_Edit frmEdit = new frmDoiTuong_Edit();
                            //    frmEdit.Load(enuEdit.New, drNew);

                            //    if (frmEdit.isAccept)
                            //    {
                            //        txtMa_So_Thue.Text = (string)drNew["Ma_So_Thue"];
                            //        txtTen_DtGtgt.Text = (string)drNew["Ten_Dt"];
                            //    }
                            //}
                        }
                    }
                }
            }
            //this.SelectNextControl(this.ActiveControl, true, true, true, true);
        }

        void txtTk_No3_Enter(object sender, EventArgs e)
        {
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            //this.ucNotice.Text = dicName.GetValue("Ten_Tk_No3");
            //if ((string)drCurrent["Ma_Thue"] != string.Empty)
            //    this.ucNotice.Text = Voucher.GetDuCuoi(drCurrent, txtTk_No3.Text);
        }
        void txtTk_No3_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_No3.Text.Trim();
            bool bRequire = txtMa_Thue.Text.Trim() == string.Empty ? false : true;

            //
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_No3.Text = string.Empty;
                this.ucNotice.Text = string.Empty;
            }
            else
            {
                txtTk_No3.Text = drLookup["Tk"].ToString();
                dicName.SetValue("Ten_Tk_No3", drLookup["Ten_Tk"].ToString());
            }

            Voucher.Update_Detail(this, "Tk_No3");

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtTk_Co3_Enter(object sender, EventArgs e)
        {
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            //this.ucNotice.Text = dicName.GetValue("Ten_Tk_Co3");
            //if ((string)drCurrent["Ma_Thue"] != string.Empty)
            //    this.ucNotice.Text = Voucher.GetDuCuoi(drCurrent, txtTk_Co3.Text);
        }
        void txtTk_Co3_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Co3.Text.Trim();
            bool bRequire = txtMa_Thue.Text.Trim() == string.Empty ? false : true;

            //
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Co3.Text = string.Empty;
                this.ucNotice.Text = string.Empty;
            }
            else
            {
                txtTk_Co3.Text = drLookup["Tk"].ToString();
                dicName.SetValue("Ten_Tk_Co3", drLookup["Ten_Tk"].ToString());
            }

            Voucher.Update_Detail(this, "Tk_Co3");

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtTk_Co_Enter(object sender, EventArgs e)
        {
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            //this.ucNotice.Text = dicName.GetValue("Ten_Tk_Co3");
            //this.ucNotice.Text = Voucher.GetDuCuoi(drCurrent, txtTk_Co.Text);
        }
        //void txtTk_Co_Validating(object sender, CancelEventArgs e)
        //{
        //    string strValue = txtTk_Co.Text.Trim();
        //    bool bRequire = true;

        //    //
        //    DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

        //    if (bRequire && drLookup == null)
        //        e.Cancel = true;

        //    if (drLookup == null)
        //    {
        //        txtTk_Co.Text = string.Empty;
        //        this.ucNotice.Text = string.Empty;
        //    }
        //    else
        //    {
        //        txtTk_Co.Text = drLookup["Tk"].ToString();
        //        lbtTen_Tk_Co.Text = drLookup["Ten_Tk"].ToString();
        //    }

        //    if ((string)drDmCt["Nh_Ct"] == "1")
        //        Voucher.Update_Detail(this, "Tk_Co");
        //    else
        //        Voucher.Update_Detail(this, "Tk_No");

        //    if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
        //    {
        //        ((txtTextLookup)sender).AutoFilter.Visible = false;
        //        this.SelectNextControl(this.ActiveControl, true, true, true, true);
        //    }
        //}


        void txtMa_Xe_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Xe.Text.Trim();
            bool bRequire = false;

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
        void frmEditCtTien_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    Voucher.DeleteRow(this, dgvEditCt1);
                    break;

                case Keys.F4:

                    if (tabVoucher.SelectedTab == tpChiTiet1)
                        tabVoucher.SelectedTab = tpChiTiet2;
                    else
                        tabVoucher.SelectedTab = tpChiTiet1;
                    break;

                case Keys.Up:

                    if (this.dgvEditCt1.Focused && this.dgvEditCt1.bIsCurrentFirstRow)
                        this.SelectNextControl(dgvEditCt1, false, true, true, true);

                    else if (this.dgvEditCt2.Focused && this.dgvEditCt2.bIsCurrentFirstRow)
                        this.SelectNextControl(dgvEditCt2, false, true, true, true);

                    break;

                case Keys.F6:
                    Phan_Bo_Thue_Nk();
                    break;

                case Keys.F10:
                    this.InheritVoucher();
                    break;

                case Keys.F11:
                    this.HanTt();
                    break;
            }

            if (!this.dgvEditCt1.Focused)
                this.dgvEditCt1.ClearSelection();

            if (!this.dgvEditCt2.Focused)
                this.dgvEditCt2.ClearSelection();
        }

        void numTTien_Validated(object sender, EventArgs e)
        {
            TTien_Valid();
        }
        void numTTien_Nt_Validated(object sender, EventArgs e)
        {
            TTien_Valid();
        }
        void numTTien3_Validated(object sender, EventArgs e)
        {
            TTien_Valid();
        }
        void numTTien_Nt3_Validated(object sender, EventArgs e)
        {
            TTien_Valid();
        }

        void tabVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabVoucher.SelectedTab == tpChiTiet1)
            {
                if (bDgvEditCtFocusing)
                    this.dgvEditCt1.Focus();
            }
            else if (tabVoucher.SelectedTab == tpChiTiet2)
            {
                if (bDgvEditCtFocusing)
                    this.dgvEditCt2.Focus();
            }
        }
        void tabVoucher_Enter(object sender, EventArgs e)
        {
            this.SelectNextControl(tabVoucher, true, true, true, true);
        }
        #endregion

        #region DataGridViewEvent

        void dgvEditCt_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Xu ly Notice
            dgvVoucher dgvEditCt = (dgvVoucher)sender;

            if (dgvEditCt.CurrentCell == null)
                return;

            if (this.ActiveControl != dgvEditCt)
                return;

            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (strColumnName == "TK_NO")
                this.strTk_NoTmp = dgvCell.FormattedValue.ToString();

            else if (strColumnName == "TK_CO")
                this.strTk_CoTmp = dgvCell.FormattedValue.ToString();

            if (Common.Inlist(strColumnName, "MA_VT,MA_KHO"))
            {
                if ((string)drCurrent["Ma_Vt"] != string.Empty)
                {
                    drCurrent["Ngay_Ct"] = dteNgay_Ct.Text;
                    ucNotice.Text = Voucher.GetTonCuoi(drCurrent);
                }

                dicName.SetValue("TON_CUOI", ucNotice.Text);
            }
            else if (Common.Inlist(strColumnName, "TEN_VT,DVT"))
            {
                ucNotice.Text = dicName.GetValue("TON_CUOI");
            }
            else if (Common.Inlist(strColumnName, "TK_NO, TK_CO, TK_NO3, TK_CO3, TK_NO5, TK_CO5, TK_NO6, TK_CO6"))
            {
                //if ((string)drCurrent[strColumnName] != string.Empty)
                //    this.ucNotice.SetText(string.Empty, Voucher.GetDuCuoi(drCurrent, (string)drCurrent[strColumnName]));
            }
            else if (dgvCell.Tag != null)
                this.ucNotice.SetText(dgvCell.Value.ToString(), (string)dgvCell.Tag);
        }

        void dgvEditCt_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //Cai dat Lookup

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            //Xu ly phim Enter
            if (dgvEditCt.kLastKey == Keys.Enter)
            {
                dgvEditCt.kLastKey = Keys.None;

                if (strMa_Ct == "NK" && strColumnName == "TK_CO5")
                    e.Cancel = !dgvLookupTk(ref dgvCell, strColumnName);

                if (this.CellKeyEnter())
                    e.Cancel = true;
            }

            //Xu ly Lookup
            if (this.ActiveControl == null)
                return;

            if (this.ActiveControl == dgvEditCt || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                bool bLookup = true;

                if (Common.Inlist(strColumnName, "TK_NO,TK_CO,TK_NO3,TK_CO3,TK_NO5,TK_NO6,TK_CO6"))//,TK_CO5
                    bLookup = dgvLookupTk(ref dgvCell, strColumnName);

                else if (strColumnName == "MA_DT")
                    bLookup = dgvLookupMa_Dt(ref dgvCell);

                else if (strColumnName == "MA_BP")
                    bLookup = dgvLookupMa_Bp(ref dgvCell);

                else if (strColumnName == "MA_KM")
                    bLookup = dgvLookupMa_Km(ref dgvCell);

                else if (strColumnName == "MA_SP")
                    bLookup = dgvLookupMa_Sp(ref dgvCell);

                else if (strColumnName == "MA_JOB")
                    bLookup = dgvLookupMa_Job(ref dgvCell);

                else if (strColumnName == "MA_THUE")
                    bLookup = dgvLookupMa_Thue(ref dgvCell);

                else if (strColumnName == "MA_VT")
                    bLookup = dgvLookupMa_Vt(ref dgvCell);

                else if (strColumnName == "MA_KHO")
                    bLookup = dgvLookupMa_Kho(ref dgvCell);

                else if (strColumnName == "MA_PO")
                    bLookup = dgvLookupMa_Po(ref dgvCell);

                if (bLookup == false)
                    e.Cancel = true;
            }
            else
                dgvEditCt.CancelEdit();
        }

        void dgvEditCt_CellValidated(object sender, DataGridViewCellEventArgs e)
        {//Cai dat cac ham tinh toan    

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt && this.ActiveControl != null && this.ActiveControl.GetType().Name != "DataGridViewTextBoxEditingControl")
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            //if (drCurrent[strColumnName].GetType() == typeof(decimal) && drCurrent[strColumnName, DataRowVersion.Original] == DBNull.Value)
            //    drCurrent.AcceptChanges();

            //Cập nhật giá từ chính sách giá
            if (Common.Inlist(strColumnName, "SO_LUONG9"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    //drCurrent["GIA_NT9"] = 0;
                    //drCurrent["TIEN_NT9"] = 0;
                    Update_Gia_Vt(drCurrent);
                }
            }
            else if (Common.Inlist(strColumnName, "DVT"))
            {
                if (drCurrent.RowState == DataRowState.Added || drCurrent[strColumnName] != drCurrent[strColumnName, DataRowVersion.Original])
                {
                    drCurrent["GIA_NT9"] = 0;
                    drCurrent["TIEN_NT9"] = 0;
                    Update_Gia_Vt(drCurrent);
                }
            }
            else if (Common.Inlist(strColumnName, "GIA_NT9"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    double dbGIA_NT9 = 0;
                    if (double.TryParse(drCurrent["GIA_NT9"].ToString(), out dbGIA_NT9))
                    {
                        if (dbGIA_NT9 == 0)
                            drCurrent["TIEN_NT9"] = 0;
                    }
                    else if (dbGIA_NT9 == 0)
                        drCurrent["TIEN_NT9"] = 0;
                }
            }

            if (Common.Inlist(strColumnName, "SO_LUONG9,GIA_NT9,TIEN_NT9,TIEN"))
            {
                if (strColumnName == "TIEN_NT9" && Convert.ToDouble(drCurrent[strColumnName]) == 0)
                {
                    drCurrent["GIA_NT9"] = 0;
                    drCurrent["TIEN"] = 0;
                    drCurrent["TIEN_NT"] = 0;
                    Voucher.Update_TTien(this);
                    drCurrent.AcceptChanges();
                }

                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    Voucher.Calc_So_Luong(drCurrent);
                    Voucher.Update_TTien(this);
                    Voucher.Adjust_TThue_Vat(this, true);
                }

                //Nếu Sl <> 0, Gia <> 0 nhưng Thanh tien = 0
                if (Convert.ToDouble(drCurrent["SO_LUONG9"]) != 0 && Convert.ToDouble(drCurrent["GIA_NT9"]) != 0 && Convert.ToDouble(drCurrent["TIEN_NT9"]) == 0)
                {
                    Voucher.Calc_So_Luong(drCurrent);
                    Voucher.Update_TTien(this);
                    Voucher.Adjust_TThue_Vat(this, true);
                }
            }
            else if (Common.Inlist(strColumnName, "MA_THUE,TIEN_NT3,TIEN3"))
            {
                Voucher.Calc_Thue_Vat(drCurrent);
                Voucher.Update_TTien(this);
            }

            else if (strMa_Ct == "NK" && Common.Inlist(strColumnName, "TIEN_NT,TIEN"))
            {
                Voucher.Calc_Thue_Nk(drCurrent);
                Voucher.Calc_Thue_TTDB(drCurrent);
                Voucher.Calc_Thue_Vat(drCurrent);
                Voucher.Update_TTien(this);
            }

            else if (Common.Inlist(strColumnName, "TIEN_NT5,TIEN5,THUE_NK"))
            {
                double dbTien5 = dgvCell.Value == DBNull.Value ? 0 : Convert.ToDouble(dgvCell.Value);
                if (dbTien5 == 0)
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO5"].ReadOnly = true;
                    dgvEditCt.CurrentRow.Cells["TK_CO5"].ReadOnly = true;
                }
                else
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO5"].ReadOnly = false;
                    dgvEditCt.CurrentRow.Cells["TK_CO5"].ReadOnly = false;
                }

                Voucher.Calc_Thue_Nk(drCurrent);
                Voucher.Update_TTien(this);
            }

            else if (Common.Inlist(strColumnName, "TIEN_NT6,TIEN6,THUE_TTDB"))
            {
                double dbTien6 = dgvCell.Value == DBNull.Value ? 0 : Convert.ToDouble(dgvCell.Value);
                if (dbTien6 == 0)
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO6"].ReadOnly = true;
                    dgvEditCt.CurrentRow.Cells["TK_CO6"].ReadOnly = true;
                }
                else
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO6"].ReadOnly = false;
                    dgvEditCt.CurrentRow.Cells["TK_CO6"].ReadOnly = false;
                }

                Voucher.Calc_Thue_TTDB(drCurrent);
                Voucher.Update_TTien(this);
            }

            if (dgvEditCt.Columns.Contains("MA_THUE"))
                this.Ma_Thue_Valid();

            if (Common.Inlist(strColumnName, "SO_LUONG9,GIA_NT9,TIEN_NT9,TIEN"))
                drCurrent.AcceptChanges();

            bdsEditCt.EndEdit();//Cap nhat lai DataSource
        }


        void dgvEditCt_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt)
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = dgvEditCt.CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();
        }

        void dgvEditCt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                if (dgvEditCt1.CurrentCell.OwningColumn.DataPropertyName == "DVT")
                {
                    drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                    string strMa_Vt = (string)drCurrent["Ma_Vt"];
                    string strDvt_Old = (string)drCurrent["Dvt"];
                    string strDvt_Chuan = string.Empty;

                    DataRow drDmVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", strMa_Vt);
                    strDvt_Chuan = (string)drDmVt["Dvt"];

                    string inputMask = (string)drDmVt["Dvt"];

                    for (int i = 1; i <= 3; i++)
                        inputMask += (string)drDmVt["Dvt" + i] == string.Empty ? string.Empty : "," + (string)drDmVt["Dvt" + i];

                    if (inputMask != string.Empty)
                        inputMask += "," + inputMask;
                    if (inputMask == null || inputMask == string.Empty)
                        return;

                    string[] strArrInputMask = inputMask.Split(',');
                    for (int i = 0; i <= strArrInputMask.Length - 1; i++)
                        if (strArrInputMask[i] == strDvt_Old)
                        {
                            drCurrent["Dvt"] = strArrInputMask[i + 1];
                            break;
                        }

                    if ((string)drCurrent["Dvt"] == strDvt_Chuan)
                        drCurrent["He_So9"] = 1;
                    else
                        for (int i = 1; i <= 3; i++)
                            if ((string)drDmVt["Dvt" + i] == (string)drCurrent["Dvt"])
                                drCurrent["He_So9"] = drDmVt["He_So" + i];

                    Voucher.Calc_So_Luong(drCurrent);
                }
        }

        void dgvEditCt1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (dgvEditCt1.CurrentCell.OwningColumn.DataPropertyName == "IS_TAISAN")
            {
                frmCreate_TSCD_Edit Create_TSCD = new frmCreate_TSCD_Edit();

                Create_TSCD.txtMa_Ts.Text = dgvEditCt1.CurrentRow.Cells["Ma_Vt"].Value.ToString();
                Create_TSCD.txtTen_Ts.Text = dgvEditCt1.CurrentRow.Cells["Ten_Vt"].Value.ToString();
                Create_TSCD.dteNgay_Ps.Text = dteNgay_Ct.Text;
                Create_TSCD.txtSo_Ct.Text = txtSo_Ct.Text;
                Create_TSCD.txtMa_Tte.Text = txtMa_Tte.Text;
                Create_TSCD.numTy_Gia.Value = 1;
                Create_TSCD.txtDien_Giai.Text = txtDien_Giai.Text;
                Create_TSCD.txtMa_Bp.Text = "";
                Create_TSCD.txtMa_Km.Text = "";
                if (Common.Inlist(strColumnName, "MA_SP"))
                    Create_TSCD.txtMa_Sp.Text = dgvEditCt1.CurrentRow.Cells["Ma_Sp"].Value.ToString();
                Create_TSCD.txtDvt.Text = dgvEditCt1.CurrentRow.Cells["Dvt"].Value.ToString();
                Create_TSCD.numSo_Luong.Value = Convert.ToDouble(dgvEditCt1.CurrentRow.Cells["So_Luong9"].Value);
                Create_TSCD.numTien_NG.Value = Convert.ToDouble(dgvEditCt1.CurrentRow.Cells["Tien_Nt9"].Value);
                Create_TSCD.numTien_NG_Nt.Value = Convert.ToDouble(dgvEditCt1.CurrentRow.Cells["Tien_Nt9"].Value);
                Create_TSCD.numTien_CL.Value = Convert.ToDouble(dgvEditCt1.CurrentRow.Cells["Tien_Nt9"].Value);
                Create_TSCD.numTien_CL_Nt.Value = Convert.ToDouble(dgvEditCt1.CurrentRow.Cells["Tien_Nt9"].Value);
                Create_TSCD.strStt = drCurrent["Stt"].ToString();
                Create_TSCD.iStt0 = Convert.ToInt16(drCurrent["Stt0"]);

                Create_TSCD.Load();
            }
        }

        void dgvEditCt2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {// Hien notice khi Gotfocus

        }

        void dgvEditCt2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { //Xu ly phim Enter, Lookup danh muc

            if (this.ActiveControl != dgvEditCt2)
                return;

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            DataGridViewCell dgvCell = dgvEditCt.CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            //Xu ly phim Enter
            if (dgvEditCt2.kLastKey == Keys.Enter)
            {
                dgvEditCt2.kLastKey = Keys.None;

                if (dgvEditCt2.bIsCurrentLastRow && dgvEditCt2.bIsCurrentLastColumn)
                {
                    this.SelectNextControl(dgvEditCt2, true, true, true, true);
                }
            }

            if (Common.Inlist(strColumnName, "SO_SERI0"))
            {
                string strSo_Seri = dgvCell.FormattedValue.ToString().Trim();
                strSo_Seri = strSo_Seri.ToUpper();
                dgvEditCt.CancelEdit();
                dgvCell.Value = strSo_Seri;
            }

            if (this.ActiveControl == dgvEditCt2 || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                bool bLookup = true;

                if (Common.Inlist(strColumnName, "TK_NO5,TK_CO5,TK_NO6,TK_CO6"))
                    bLookup = dgvLookupTk(ref dgvCell, strColumnName);

                else if (strColumnName == "MA_THUE")
                    bLookup = dgvLookupMa_Thue(ref dgvCell);

                if (strColumnName == "MA_SO_THUE")
                    bLookup = dgvLookupMa_So_Thue(ref dgvCell);

                else if (Common.Inlist(strColumnName, "TK_NO3,TK_CO3"))
                    bLookup = dgvLookupTk(ref dgvCell, strColumnName);
            }
        }

        void dgvEditCt2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {// Tinh toan cac Gia tri, cong thuc
            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt && this.ActiveControl != null && this.ActiveControl.GetType().Name != "DataGridViewTextBoxEditingControl")
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (Common.Inlist(strColumnName, "TIEN_NT3,TIEN3,MA_THUE"))
            {
                Voucher.Calc_Thue_Vat(drCurrent);
                Voucher.Update_TTien(this);

            }
            else if (Common.Inlist(strColumnName, "TIEN_NT5,TIEN5,THUE_NK"))
            {
                double dbTien5 = dgvCell.Value == DBNull.Value ? 0 : Convert.ToDouble(dgvCell.Value);
                if (dbTien5 == 0)
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO5"].ReadOnly = true;
                    dgvEditCt.CurrentRow.Cells["TK_CO5"].ReadOnly = true;
                }
                else
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO5"].ReadOnly = false;
                    dgvEditCt.CurrentRow.Cells["TK_CO5"].ReadOnly = false;
                }

                Voucher.Calc_Thue_Nk(drCurrent);
                Voucher.Update_TTien(this);
            }
            else if (Common.Inlist(strColumnName, "TIEN_NT6,TIEN6,THUE_TTDB"))
            {
                double dbTien6 = dgvCell.Value == DBNull.Value ? 0 : Convert.ToDouble(dgvCell.Value);
                if (dbTien6 == 0)
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO6"].ReadOnly = true;
                    dgvEditCt.CurrentRow.Cells["TK_CO6"].ReadOnly = true;
                }
                else
                {
                    dgvEditCt.CurrentRow.Cells["TK_NO6"].ReadOnly = false;
                    dgvEditCt.CurrentRow.Cells["TK_CO6"].ReadOnly = false;
                }

                Voucher.Calc_Thue_TTDB(drCurrent);
                Voucher.Update_TTien(this);
            }

            bdsEditCt.EndEdit();//Cap nhat lai DataSource
        }

        void dgvEditCt2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvVoucher dgvEditCt = (dgvVoucher)sender;

            if (dgvEditCt.CurrentCell == null)
                return;

            if (this.ActiveControl != dgvEditCt)
                return;

            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (strColumnName == "MA_THUE")
                this.bMa_Thue_Changed = true;
        }

        #endregion

        #region DataGridViewLookup

        private bool dgvLookupMa_So_Thue(ref DataGridViewCell dgvCell)
        {
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            if (strValue == "/" || strValue == @"\")
            {
                frmDoiTuong frmLookup = new frmDoiTuong();
                DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONG", "Ma_So_Thue", strValue, bRequire, "", "");

                if (bRequire && drLookup == null)
                    return false;

                if (drLookup == null)
                {
                    dgvCell.Value = string.Empty;
                    dgvCell.Tag = string.Empty;
                }
                else
                {
                    dgvEditCt1.CancelEdit();

                    dgvCell.Value = drLookup["Ma_So_Thue"].ToString();
                    dgvCell.Tag = drLookup["Ten_Dt"].ToString();

                    drCurrent["Ma_So_Thue"] = dgvCell.Value;
                    drCurrent["Ten_DtGtGt"] = dgvCell.Tag;
                }
            }
            else if (strValue != string.Empty && (drCurrent["Ma_So_Thue", DataRowVersion.Original] == DBNull.Value || strValue != (string)drCurrent["Ma_So_Thue", DataRowVersion.Original]))
            {
                DataTable dtLookup = SQLExec.ExecuteReturnDt("SELECT * FROM LIDOITUONG WHERE Ma_So_Thue = '" + strValue + "'");

                if (dtLookup != null)
                {
                    if (dtLookup.Rows.Count == 1)
                    {
                        dgvCell.Value = dtLookup.Rows[0]["Ma_So_Thue"].ToString();
                        dgvCell.Tag = dtLookup.Rows[0]["Ten_Dt"].ToString();

                        drCurrent["Ma_So_Thue"] = dgvCell.Value;
                        drCurrent["Ten_DtGtGt"] = dgvCell.Tag;
                    }
                    else
                    {
                        dtLookup = SQLExec.ExecuteReturnDt("SELECT * FROM LIDOITUONG WHERE Ma_So_Thue LIKE '" + strValue + "%'");

                        if (dtLookup.Rows.Count >= 1)
                        {
                            frmDoiTuong frmLookup = new frmDoiTuong();
                            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONG", "Ma_So_Thue", strValue, bRequire, "");

                            if (bRequire && drLookup == null)
                                return false;

                            if (drLookup == null)
                            {
                                dgvCell.Value = string.Empty;
                                dgvCell.Tag = string.Empty;
                            }
                            else
                            {
                                dgvEditCt1.CancelEdit();

                                dgvCell.Value = drLookup["Ma_So_Thue"].ToString();
                                dgvCell.Tag = drLookup["Ten_Dt"].ToString();

                                drCurrent["Ma_So_Thue"] = dgvCell.Value;
                                drCurrent["Ten_DtGtGt"] = dgvCell.Tag;
                            }
                        }
                        else
                        {
                            if (Common.MsgYes_No("Bạn có muốn thêm Mã đối tượng và Mã số thuế này ?"))
                            {
                                DataRow drNew = dtLookup.NewRow();
                                drNew["Ma_Dt"] = drNew["Ma_So_Thue"] = strValue;
                                drNew["Ma_Nh_Dt"] = "MA_SO_THUE";

                                frmDoiTuong_Edit frmEdit = new frmDoiTuong_Edit();
                                frmEdit.Load(enuEdit.New, drNew);

                                if (frmEdit.isAccept)
                                {
                                    dgvEditCt1.CancelEdit();

                                    dgvCell.Value = drNew["Ma_So_Thue"].ToString();
                                    dgvCell.Tag = drNew["Ten_Dt"].ToString();

                                    drCurrent["Ma_So_Thue"] = dgvCell.Value;
                                    drCurrent["Ten_DtGtGt"] = dgvCell.Tag;
                                }
                            }
                        }
                    }
                }
            }

            drCurrent.AcceptChanges();

            return true;
        }
        private bool dgvLookupTk(ref DataGridViewCell dgvCell, string strColumnName)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = true;

            if (strColumnName == "TK_NO3" || strColumnName == "TK_CO3")
                if (drCurrent["Ma_Thue"].ToString() == string.Empty)
                    bRequire = false;

            if (strColumnName == "TK_NO5" || strColumnName == "TK_CO5")
            {
                if (drCurrent["TIEN5"] == DBNull.Value || Convert.ToDouble(drCurrent["TIEN5"]) == 0)
                    bRequire = false;
            }
            else
            {
                if (strColumnName == "TK_NO6" || strColumnName == "TK_CO6")
                    if (drCurrent["TIEN6"] == DBNull.Value || Convert.ToDouble(drCurrent["TIEN6"]) == 0)
                        bRequire = false;
            }

            //
            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Tk"].ToString();
                //dgvCell.Tag = drLookup["Ten_Tk"].ToString();                
            }

            return true;
        }

        private bool dgvLookupMa_Dt(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = true;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Dt", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Dt"].ToString();
                dgvCell.Tag = drLookup["Ten_Dt"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Bp(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Bp", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Bp"].ToString();
                dgvCell.Tag = drLookup["Ten_Bp"].ToString();
            }

            return true;
        }

        private bool dgvLookupMa_Km(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;
            object objReturn = null;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strTk_No = (string)drCurrent["Tk_No"];
            string strTk_Co = (string)drCurrent["Tk_Co"];

            objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_No + "'");
            if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
                bRequire = true;
            else
            {
                objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_Co + "'");
                if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
                    bRequire = true;
            }

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Km", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Km"].ToString();
                dgvCell.Tag = drLookup["Ten_Km"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Sp(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;
            object objReturn = null;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strTk_No = (string)drCurrent["Tk_No"];
            string strTk_Co = (string)drCurrent["Tk_Co"];

            objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Sp FROM LITAIKHOAN WHERE Tk = '" + strTk_No + "'");
            if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
                bRequire = true;
            else
            {
                objReturn = SQLExec.ExecuteReturnValue("SELECT Tk_Sp FROM LITAIKHOAN WHERE Tk = '" + strTk_Co + "'");
                if (objReturn != null && objReturn != DBNull.Value && (bool)objReturn)
                    bRequire = true;
            }


            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Sp", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Sp"].ToString();
                dgvCell.Tag = drLookup["Ten_Sp"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Job(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = true;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Job", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Job"].ToString();
                dgvCell.Tag = drLookup["Ten_Job"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Thue(ref DataGridViewCell dgvCell)
        {
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strMa_Thue_Old = drCurrent["Ma_Thue"] == DBNull.Value ? string.Empty : (string)drCurrent["Ma_Thue"];

            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Thue", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
                drCurrent["Thue_GtGt"] = 0;

                dgvEditCt2.CurrentRow.Cells["Tien_Nt3"].ReadOnly = true;
                dgvEditCt2.CurrentRow.Cells["Tk_No3"].ReadOnly = true;
                dgvEditCt2.CurrentRow.Cells["Tk_Co3"].ReadOnly = true;
            }
            else
            {
                dgvEditCt2.CancelEdit();
                dgvCell.Value = drLookup["Ma_Thue"].ToString();
                dgvCell.Tag = drLookup["Ten_Thue"].ToString();
                drCurrent["Thue_GtGt"] = Convert.ToInt32(drLookup["Thue_Suat"]);

                dgvEditCt2.CurrentRow.Cells["Tien_Nt3"].ReadOnly = false;
                dgvEditCt2.CurrentRow.Cells["Tien3"].ReadOnly = false;
                dgvEditCt2.CurrentRow.Cells["Tk_No3"].ReadOnly = false;
                dgvEditCt2.CurrentRow.Cells["Tk_Co3"].ReadOnly = false;
            }

            this.Ma_Thue_Valid();
            return true;
        }

        private bool dgvLookupMa_Vt(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //frmVatTu frmLookup = new frmVatTu();
            DataRow drLookup = Lookup.ShowLookup("Ma_Vt", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                string strMa_Vt_Old = string.Empty;

                if (drCurrent.HasVersion(DataRowVersion.Original))
                    strMa_Vt_Old = drCurrent["Ma_Vt", DataRowVersion.Original] == DBNull.Value ? string.Empty : (string)drCurrent["Ma_Vt", DataRowVersion.Original];
                else
                    strMa_Vt_Old = drCurrent["Ma_Vt"] == DBNull.Value ? string.Empty : (string)drCurrent["Ma_Vt"];

                string strMa_Vt = (string)drLookup["Ma_Vt"];

                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Vt"].ToString();
                dgvCell.Tag = drLookup["Ten_Vt"].ToString();

                if (strMa_Vt != strMa_Vt_Old)
                {
                    drCurrent["Ten_Vt"] = drLookup["Ten_Vt"];
                    drCurrent["Dvt"] = drLookup["Dvt"];
                    drCurrent["Dvt"] = drLookup["Dvt_MD"].ToString() == string.Empty ? drLookup["Dvt"] : drLookup["Dvt_MD"];
                    drCurrent["He_So9"] = 1;


                    string strDvt_Old = (string)drCurrent["Dvt"];
                    string strDvt_Chuan = (string)drLookup["Dvt"];



                    if ((string)drCurrent["Dvt"] == strDvt_Chuan)
                        drCurrent["He_So9"] = 1;
                    else
                        for (int i = 1; i <= 3; i++)
                            if ((string)drLookup["Dvt" + i] == (string)drCurrent["Dvt"])
                                drCurrent["He_So9"] = drLookup["He_So" + i];

                    Voucher.Calc_So_Luong(drCurrent);
                    //if (strMa_Ct == "MTL")//TL
                    if ((string)drDmCt["Nh_Ct"] == "2")
                    {
                        drCurrent["Tk_Co"] = drLookup["Tk_Vt"];

                    }
                    else//HD
                    {
                        drCurrent["Tk_No"] = drLookup["Tk_Vt"];
                    }

                }
                else
                {
                    if (drCurrent["Ten_Vt"] == DBNull.Value || (string)drCurrent["Ten_Vt"] == string.Empty)
                        drCurrent["Ten_Vt"] = drLookup["Ten_Vt"];

                    if (drCurrent["Dvt"] == DBNull.Value || (string)drCurrent["Dvt"] == string.Empty)
                        drCurrent["Dvt"] = drLookup["Dvt"];
                    //if (strMa_Ct == "TL")//TL
                    if ((string)drDmCt["Nh_Ct"] == "2")
                    {
                        if (drCurrent["Tk_Co"] == DBNull.Value || (string)drCurrent["Tk_Co"] == string.Empty)
                            drCurrent["Tk_Co"] = drLookup["Tk_Vt"];
                    }
                    else//HD
                    {
                        if (drCurrent["Tk_No"] == DBNull.Value || (string)drCurrent["Tk_No"] == string.Empty)
                            drCurrent["Tk_No"] = drLookup["Tk_Vt"];
                    }
                }
            }
            return true;
        }

        private bool dgvLookupMa_Kho(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Kho", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Kho"].ToString();
                dgvCell.Tag = drLookup["Ten_Kho"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Po(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //frmPo frmLookup = new frmPo();
            DataRow drLookup = Lookup.ShowLookup("Ma_Po", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Po"].ToString();
                dgvCell.Tag = drLookup["Ten_Po"].ToString();
            }
            return true;
        }

        #endregion

        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.dgvEditCt1.ClearSelection(); //Chi co tac dung sau khi show form

            if (this.enuNew_Edit == enuEdit.Edit)
            {
                //Chứng từ đã duyệt -> không cho sửa
                if (!Element.sysIs_Admin) //Nếu không phải là Admin
                {
                    if (Parameters.GetParaValue("NOT_EDIT_DUYET").ToString().Trim() == "1" && drEditPh["Duyet_Log"].ToString() != "" && (bool)drEditPh["Duyet"] == true)
                    {
                        this.btgAccept.btAccept.Enabled = false;
                    }

                    //Sua chung tu
                    string strCreate_User = (string)drEditPh["Create_Log"];
                    if (strCreate_User != string.Empty && strCreate_User.Substring(14) != Element.sysUser_Id)
                    {
                        string strUser_Allow = (string)SQLExec.ExecuteReturnValue("SELECT Member_ID_Allow FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'") + ",";

                        if (!strUser_Allow.Contains("*,")) //Được phép sửa tất cả
                        {
                            if (!strUser_Allow.Contains(strCreate_User.Substring(14) + ","))
                            {
                                this.btgAccept.btAccept.Enabled = false;
                                return;
                            }
                        }
                    }

                }

                if (!Common.CheckDataLocked((DateTime)drEdit["Ngay_Ct"]))
                {
                    this.dteNgay_Ct.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;
                }

                //if (!Element.sysIs_Admin)
                //{
                //	string strCreate_User = (string)drEditPh["Create_Log"];

                //	if (strCreate_User != string.Empty && strCreate_User.Substring(14) != Element.sysUser_Id)
                //	{
                //		string strUser_Allow = (string)SQLExec.ExecuteReturnValue("SELECT Member_ID_Allow FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'") + ",";

                //		if (!strUser_Allow.Contains("*,")) //Được phép sửa tất cả
                //		{
                //			if (!strUser_Allow.Contains(strCreate_User.Substring(14) + ","))
                //			{
                //				this.btgAccept.btAccept.Enabled = false;
                //				return;
                //			}
                //		}
                //	}
                //}


            }
        }
    }
}