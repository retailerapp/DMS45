using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Lists;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Modules;

namespace Epoint.Modules.CA
{
    public partial class frmCtTien_Edit : frmVoucher_Edit
    {
        private string strTk_NoTmp = string.Empty;
        private string strTk_CoTmp = string.Empty;
        private bool bMa_Thue_Changed = false;
        private string strMa_Thue_Old = string.Empty;
        private string strModule = "01";
        private bool bCheckDataLockedCtHanTt = false;

        #region Contructor

        public frmCtTien_Edit()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(frmEditCtTien_KeyDown);

            this.btHanTt.Click += new EventHandler(btHanTt_Click);            

            linkHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(linkHelp_LinkClicked);

            this.btInherit.Click += new EventHandler(btInherit_Click);

            tabVoucher.SelectedIndexChanged += new EventHandler(tabVoucher_SelectedIndexChanged);
            tabVoucher.Enter += new EventHandler(tabVoucher_Enter);
            txtMa_Nvu.Validating += new CancelEventHandler(txtMa_Nvu_Validating);
            //txtMa_Dt.Enter += new EventHandler(txtMa_Dt_Enter);
            txtMa_Dt.Validating += new CancelEventHandler(txtMa_Dt_Validating);

            txtMa_Hd.Enter += new EventHandler(txtMa_Hd_Enter);
            txtMa_Hd.Validating += new CancelEventHandler(txtMa_Hd_Validating);

            txtMa_Ct.Enter += new EventHandler(txtMa_Ct_Enter);
            txtMa_Ct.Validating += new CancelEventHandler(txtMa_Ct_Validating);
            txtMa_Ct.TextChanged += new EventHandler(txtMa_Ct_TextChanged);

            txtSo_Ct.Validating += new CancelEventHandler(txtSo_Ct_Validating);

            dteNgay_Ct.Validating += new CancelEventHandler(dteNgay_Ct_Validating);
            txtMa_Tte.Validating += new CancelEventHandler(txtMa_Tte_Validating);
            numTy_Gia.Leave += new EventHandler(numTy_Gia_Leave);

            txtDien_Giai.Validating += new CancelEventHandler(txtDien_Giai_Validating);

            dgvEditCt1.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEditCt_CellValidating);
            dgvEditCt1.CellValidated += new DataGridViewCellEventHandler(dgvEditCt_CellValidated);
            dgvEditCt1.CellEnter += new DataGridViewCellEventHandler(dgvEditCt_CellEnter);
            dgvEditCt1.CellValueChanged += new DataGridViewCellEventHandler(dgvEditCt_CellValueChanged);

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
            this.Object_ID = strMa_Ct;

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
                this.strStt = Common.GetNewStt(strModule, true);
            else
                this.strStt = drEdit["Stt"].ToString();

            this.Build();
            this.FillData();
            this.Init_Ct();

            Common.ScaterMemvar(this, ref drEditPh);

            txtMa_Tte.bTextChange = false;
            numTy_Gia.bTextChange = false;

            this.Ma_Tte_Valid();
            this.BindingLanguage();
            this.LoadDicName();
            this.ucNotice.Text = (string)drEditPh["Create_Log"];
            if (!isAccept)
                this.ShowDialog();
            else
                this.ActiveControl = txtMa_Nvu;
            
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

            dgvEditCt2.Size = dgvEditCt1.Size;
            dgvEditCt2.Location = dgvEditCt1.Location;
            dgvEditCt2.Anchor = dgvEditCt1.Anchor;            
            dgvEditCt2.TabIndex = dgvEditCt1.TabIndex;
        }

        private void FillData()
        {
            string strKeyFillterCt = " Stt = '" + ((string)drEdit["Stt"]).Trim() + "' ";

            string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt ";
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
                    drEditPh["Ma_Ct"] = drCurrent["Ma_Ct"];
                    drEditPh["Stt"] = drCurrent["Stt"];
                    drEditPh["Ngay_Ct"] = drCurrent["Ngay_Ct"];
                    drEditPh["So_Ct"] = drCurrent["So_Ct"];
                }

                //Tinh so chung tu
                string strLoai_Ma_Ct = ((DateTime)drCurrent["Ngay_Ct"]).Month.ToString().Trim();
                string strSQLExec = "EXEC Sp_Cong_So_Ct '" + strMa_Ct + "', '" + strLoai_Ma_Ct + "'";

                DataTable dtSo_Ct = SQLExec.ExecuteReturnDt(strSQLExec);

                if (dtSo_Ct.Rows.Count > 0)
                    drEditPh["So_Ct"] = drCurrent["So_Ct"] = (string)dtSo_Ct.Rows[0][0];

                if (drEditPh.Table.Columns.Contains("Duyet"))
                    drEditPh["Duyet"] = (bool)drDmCt["Default_Duyet"];
                if (drEditPh.Table.Columns.Contains("Is_Thue_Vat"))
                    drEditPh["Is_Thue_Vat"] = (bool)drDmCt["Default_VAT"];
            }

            Voucher.Update_Header(this);
            Voucher.Update_Stt(this, strModule);

            if (this.enuNew_Edit == enuEdit.Edit)
            {
                numTTien_CLTG.Value = Convert.ToDouble(SQLExec.ExecuteReturnValue("SELECT ISNULL(SUM(Tien_ClTg), 0) FROM GLTHANHTOAN WHERE Stt = '" + this.strStt + "'"));
            }

            //BindingTTien
            if (isAccept)
            {
                numTTien0.DataBindings.Clear();
                numTTien_Nt0.DataBindings.Clear();

                numTTien_Nt3.DataBindings.Clear();
                numTTien3.DataBindings.Clear();

                numTTien.DataBindings.Clear();
                numTTien_Nt.DataBindings.Clear();
            }

            numTTien0.DataBindings.Add("Value", dtEditPh, "TTien0");
            numTTien_Nt0.DataBindings.Add("Value", dtEditPh, "TTien_Nt0");

            numTTien_Nt3.DataBindings.Add("Value", dtEditPh, "TTien_Nt3");
            numTTien3.DataBindings.Add("Value", dtEditPh, "TTien3");

            numTTien.DataBindings.Add("Value", dtEditPh, "TTien");
            numTTien_Nt.DataBindings.Add("Value", dtEditPh, "TTien_Nt");

            if (dgvEditCt2.Columns.Contains("Tien"))
                dgvEditCt2.Columns["Tien"].ReadOnly = true;

            if (dgvEditCt2.Columns.Contains("Thue_GtGt"))
                dgvEditCt2.Columns["Thue_Gtgt"].ReadOnly = true;

            //Quyen so
            string strSQL = "SELECT Quyen_So FROM LIQUYENSO WHERE Month(Ngay_Begin) <= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Month(Ngay_End) >= Month('"
                         + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Year(Ngay_End) = Year ('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "')";
            DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);

            cboQuyen_So.lstItem.BuildListView("Quyen_So:80");
            cboQuyen_So.lstItem.DataSource = dtQuyen_So;
            cboQuyen_So.lstItem.Size = new Size(80, cboQuyen_So.lstItem.Items.Count * 60);
            cboQuyen_So.lstItem.GridLines = true;

        }

        private void LoadDicName()
        {

            if (txtMa_Nvu.Text.Trim() != string.Empty)
                lbtTen_Nvu.Text = DataTool.SQLGetNameByCode("LINGHIEPVU", "Ma_Nvu", "Ten_Nvu", txtMa_Nvu.Text.Trim());
            else
                lbtTen_Nvu.Text = string.Empty;
            //txtMa_Hd
            if (txtMa_Hd.Text.Trim() != string.Empty)
            {
                txtTen_Hd.Text = DataTool.SQLGetNameByCode("LIHOPDONG", "Ma_Hd", "Ten_Hd", txtMa_Hd.Text.Trim());
                dicName.SetValue(txtTen_Hd.Name, txtTen_Hd.Text);
            }
            else
                txtTen_Hd.Text = string.Empty;

            if (txtMa_Ct.Text.Trim() != string.Empty && drDmCt != null)
            {
                dicName.SetValue("Ten_Ct", (string)drDmCt["Ten_Ct"]);
            }
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

            if (chkIs_UngTruoc.Checked && numHan_Tt.Value != 0)
            {
                Common.MsgCancel("Chứng từ khai báo trùng vừa là Ứng trước vừa là Ghi nhận nợ!");
                return false;
            }

            //Kiểm tra nghiệp vụ hợp lệ
            foreach (DataRow dr in dtEditCt.Rows)
            {
                if ((bool)dr["Deleted"])
                    continue;

                if (dtEditCt.Columns.Contains("Tien") && Convert.ToDouble(dr["Tien"]) != 0 && ((string)dr["Tk_No"] == string.Empty || (string)dr["Tk_Co"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán không hợp lệ" : "Transaction error";
                    Common.MsgCancel(strMsg);
                    return false;
                }
                if (dtEditCt.Columns.Contains("Ma_Thue") && (string)dr["Ma_Thue"] != string.Empty && ((string)dr["Tk_No3"] == string.Empty || (string)dr["Tk_Co3"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán thuế không hợp lệ" : "Transaction VAT invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }
            }

            if (!Voucher.CheckDuplicateInvoice(this))
                return false;

            return true;
        }
        private void InheritVoucher() //Kế thừa chứng từ từ phiếu khác
        {
            Voucher.Update_Header(this);
            Voucher.Update_Detail(this);

            frmInheritVoucher_Filter frm = new frmInheritVoucher_Filter();
            frm.Load(this);
            Voucher.Update_Detail(this);
            Voucher.Update_TTien(this);
        }
        void btInherit_Click(object sender, EventArgs e)
        {
            this.InheritVoucher();
        }
        public override bool Save()
        {
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

            if (!(bool)drEditPh["Duyet"]) //Trường hợp chứng từ chưa duyệt
            {
                if (dtEditPh.Columns.Contains("Ngay_Ct_Lap"))
                {
                    drEditPh["Ngay_Ct_Lap"] = drEditPh["Ngay_Ct"];
                    drEditPh["So_Ct_Lap"] = drEditPh["So_Ct"];
                }
            }

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (dtEditCt.Rows.Count > 0) //Cập nhật lại dữ liệu từ chi tiết lên Header
                    Common.CopyDataRow(dtEditCt.Rows[0], drEditPh, (string)drDmCt["Update_Header"]);

                drEdit = drEditPh;
            }

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

                if (dgvEditCt2.Columns.Contains("TIEN3"))
                    dgvEditCt2.Columns["TIEN3"].ReadOnly = true;
            }
            else
            {
                numTy_Gia.bReadOnly = false;

                if (dteNgay_Ct.Text != Library.DateToStr((DateTime)drEditPh["Ngay_Ct"]) || txtMa_Tte.bTextChange)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("NGAY_CT", Library.StrToDate(dteNgay_Ct.Text));
                    ht.Add("MA_TTE", strMa_Tte);

                    numTy_Gia.Value = Convert.ToDouble(SQLExec.ExecuteReturnValue("sp_GetTyGia", ht, CommandType.StoredProcedure));
                }

                this.pnlTTien.Visible = true;
                this.pnlTTien_Nt.Left = this.pnlTTien.Left - this.pnlTTien_Nt.Width;

                if (dgvEditCt1.Columns.Contains("TIEN"))
                    dgvEditCt1.Columns["TIEN"].Visible = true;

                if (dgvEditCt2.Columns.Contains("TIEN3"))
                    dgvEditCt2.Columns["TIEN3"].ReadOnly = false;
            }

            if (dteNgay_Ct.Text != Library.DateToStr((DateTime)drEditPh["Ngay_Ct"]) || txtMa_Tte.bTextChange || numTy_Gia.bTextChange)
            {
                Common.GatherMemvar(this, ref this.drEditPh);
                Voucher.Update_Detail(this);
                Voucher.Calc_Tien_All(this);

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
                        if (drCurrent["Tk_Co3"] == DBNull.Value || (string)drCurrent["Tk_Co3"] == string.Empty)
                            drCurrent["Tk_No3"] = drDmThue["Tk"].ToString();

                        if (drCurrent["Tk_No3"] == DBNull.Value || (string)drCurrent["Tk_No3"] == string.Empty)
                            drCurrent["Tk_Co3"] = drCurrent["Tk_Co"].ToString();
                    }
                }
            }

            this.bMa_Thue_Changed = false;

            return true;
        }

        private bool CellKeyEnter()
        {//Ham thuc hien phim Enter: true: thuc hien thanh cong, false: khong thuc hien duoc

            if (dgvEditCt1.CurrentCell == null)
                return false;

            DataGridViewCell dgvCell = dgvEditCt1.CurrentCell;
            string strCurrentColumn = dgvCell.OwningColumn.Name.ToUpper();

            #region Enter tai Tk_No, Tk_Co
            if (Common.Inlist(strCurrentColumn, "TK_NO,TK_CO"))
            {
                string strOldValue = (strCurrentColumn == "TK_NO" ? this.strTk_NoTmp : this.strTk_CoTmp);

                if (dgvCell.FormattedValue.ToString() == string.Empty)
                {
                    bool bIsCurrentLastRow = dgvEditCt1.bIsCurrentLastRow;

                    if (strOldValue != string.Empty)
                    {
                        dgvCell.Value = strOldValue;
                        Voucher.DeleteRow(this, dgvEditCt1);
                    }
                    else
                    {
                        if (bdsEditCt.Count > 1) //Chỉ remove khi không phải là dòng đầu tiên
                        {
                            bdsEditCt.RemoveCurrent();
                            dtEditCt.AcceptChanges();
                        }
                    }

                    if (bIsCurrentLastRow)
                        this.SelectNextControl(dgvEditCt1, true, true, true, true);

                    return true;
                }
            }
            #endregion

            #region Enter tai Ma_Thue
            if (Common.Inlist(strCurrentColumn, "MA_THUE"))
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

            #region Enter tai Tk_Co3
            if (Common.Inlist(strCurrentColumn, "TK_CO3"))
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

            return false;
        }

        private void HanTt()
        {
            frmHanTt_View frmHanTt = new frmHanTt_View();
            frmHanTt.Load(this);
            //check save new
            if (frmHanTt.isSaveNew)
            {
                this.btgAccept.btAccept.Enabled = true;
            } 
            //Khóa không cho người dùng sửa lại dữ liệu sau khi đã tick thanh toán
            if (dtHanTt0 != null && dtHanTt0.Select("Thanh_Toan = true").Length > 0)
            {
                this.dgvEditCt1.ReadOnly = true;
                this.dteNgay_Ct.Enabled = false;
                this.txtMa_Tte.Enabled = false;
                this.numTy_Gia.Enabled = false;
                this.txtMa_Dt.Enabled = false;
                this.numHan_Tt.Enabled = false;

                foreach (DataGridViewColumn dgvc in dgvEditCt1.Columns)
                    dgvc.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            }
            else
            {
                this.dgvEditCt1.ReadOnly = false;
                this.dteNgay_Ct.Enabled = true;
                this.txtMa_Tte.Enabled = true;
                this.numTy_Gia.Enabled = true;
                this.txtMa_Dt.Enabled = true;
                this.numHan_Tt.Enabled = true;

                foreach (DataGridViewColumn dgvc in dgvEditCt1.Columns)
                    dgvc.DefaultCellStyle.ForeColor = SystemColors.WindowText;
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
        #endregion

        #region Su kien

        #region FormEvent

        void btHanTt_Click(object sender, EventArgs e)
        {
            this.HanTt();
        }        
        void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            string currentDir = Environment.CurrentDirectory;
            System.Diagnostics.Process.Start(currentDir + @"\Help\" + drDmCt["Help_File"]);
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
        }

        void dteNgay_Ct_Validating(object sender, CancelEventArgs e)
        {
            //Quyen so
            string strSQL = "SELECT Quyen_So FROM LIQUYENSO WHERE Month(Ngay_Begin) <= Month('" + dteNgay_Ct.Text + "') AND Month(Ngay_End) >= Month('"
                                    + dteNgay_Ct.Text + "') AND Year(Ngay_End) = Year ('" + dteNgay_Ct.Text + "')";
            DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);

            cboQuyen_So.lstItem.BuildListView("Quyen_So:80");
            cboQuyen_So.lstItem.DataSource = dtQuyen_So;
            cboQuyen_So.lstItem.Size = new Size(80, cboQuyen_So.lstItem.Items.Count * 60);
            cboQuyen_So.lstItem.GridLines = true;

            this.Ma_Tte_Valid();
            Common.GatherMemvar(this, ref drEditPh);
        }
        void txtMa_Tte_Validating(object sender, CancelEventArgs e)
        {
            this.Ma_Tte_Valid();
        }
        void numTy_Gia_Leave(object sender, EventArgs e)
        {
            this.Ma_Tte_Valid();
        }

        void txtMa_Nvu_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Nvu.Text.Trim();
            bool bRequire = true;
            string strFilter = "(CHARINDEX('" + strMa_Ct + "', Ma_Ct_List, 0) > 0 OR Ma_Ct_List = '*')";
            string strValid = "";

            DataRow drLookup = Lookup.ShowLookup("Ma_Nvu", strValue, bRequire, strFilter, strValid);

            if (drLookup == null) //Bắt buộc phải nhập Ma_Nvu
            {
                e.Cancel = true;
                return;
            }

            this.drDmNvu = drLookup;

            txtMa_Nvu.Text = drLookup["Ma_Nvu"].ToString();
            lbtTen_Nvu.Text = drLookup["Ten_Nvu"].ToString();

            //chkNoPosted.Checked = !(bool)drDmNvu["NoPosted"];

            //if (this.enuNew_Edit == enuEdit.New || this.enuNew_Edit == enuEdit.Copy)
            //    drEditPh["Duyet"] = (bool)drDmNvu["Default_Duyet"];

            Ma_Tte_Valid();

            Voucher.Update_DmNvu(this); //Cập nhật chi tiết hạch toán ngầm định vào chứng từ
        }
        void txtMa_Dt_Validating(object sender, CancelEventArgs e)
        {
            //if ((((sender.GetType().Name != "txtTextLookup") || (((txtTextLookup)sender).UseAutoFilter == false))))
            //{
                Common.ResetTextChange(this);

                string strValue = txtMa_Dt.Text.Trim();
                bool bRequire = true;

                //frmDoiTuong frmLookup = new frmDoiTuong();
                DataRow drLookup = Lookup.ShowLookup("Ma_Dt" , strValue, bRequire, "");

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

                    if (txtMa_Dt.bTextChange)
                    {
                        txtTen_Dt.Text = drLookup["Ten_Dt"].ToString();
                        txtOng_Ba.Text = drLookup["Ong_Ba"].ToString() == string.Empty ? drLookup["Ten_Dt"].ToString() : drLookup["Ong_Ba"].ToString();
                        txtDia_Chi.Text = drLookup["Dia_Chi"].ToString();
                    }

                    //Cap nhat xuong chi tiet
                    if (txtMa_Dt.Text != (string)drEditPh["Ma_Dt"])
                    {
                        foreach (DataRow dr in dtEditCt.Rows)
                        {
                            if ((string)dr["Ma_Dt"] == (string)drEditPh["Ma_Dt"])
                                dr["Ma_Dt"] = txtMa_Dt.Text;
                        }
                    }
                }

                //dicName[txtTen_Dt.Name] = txtTen_Dt.Text;                
                Common.GatherMemvar(this, ref drEditPh);

                if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
                {
                    ((txtTextLookup)sender).AutoFilter.Visible = false;
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
            //}
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

            //frmHopDong frmLookup = new frmHopDong();
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

        void txtDien_Giai_Validating(object sender, CancelEventArgs e)
        {
            //Cap nhat xuong chi tiet
            if (txtDien_Giai.Text != (string)drEditPh["Dien_Giai"])
            {
                foreach (DataRow dr in dtEditCt.Rows)
                {
                    if ((string)dr["Dien_Giai"] == (string)drEditPh["Dien_Giai"])
                        dr["Dien_Giai"] = txtDien_Giai.Text;
                }
            }

            Common.GatherMemvar(this, ref drEditPh);
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
                
                case Keys.F11:
                    this.HanTt();
                    break;

            }

            if (!this.dgvEditCt1.Focused)
                this.dgvEditCt1.ClearSelection();

            if (!this.dgvEditCt2.Focused)
                this.dgvEditCt2.ClearSelection();

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
        {// Hien notice khi Gotfocus

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

            else if (strColumnName == "DIEN_GIAI")
            {
                if (drCurrent["DIEN_GIAI"] == DBNull.Value || (string)drCurrent["DIEN_GIAI"] == string.Empty)
                    drCurrent["DIEN_GIAI"] = drEditPh["DIEN_GIAI"];
            }            
            else if (Common.Inlist(strColumnName, "TIEN_NT3, TIEN3, TK_NO3, TK_CO3") && !bCheckDataLockedCtHanTt)
            {
                if ((string)drCurrent["MA_THUE"] == string.Empty)
                    dgvEditCt.Columns[strColumnName].ReadOnly = true;
                else
                    dgvEditCt.Columns[strColumnName].ReadOnly = false;
            }

            else if (Common.Inlist(strColumnName, "MA_THUE"))
            {
                strMa_Thue_Old = (string)drCurrent["Ma_Thue"];
            }

            if (Common.Inlist(strColumnName, "TK_NO, TK_CO, TK_NO3, TK_CO3"))//dgvCell.Tag == null && 
            {
                if ((string)drCurrent[strColumnName] != string.Empty)
                    this.ucNotice.SetText(string.Empty, Voucher.GetDuCuoi(drCurrent, (string)drCurrent[strColumnName]));
            }
            else if (dgvCell.Tag != null)
                this.ucNotice.SetText(dgvCell.Value.ToString(), (string)dgvCell.Tag);
        }

        void dgvEditCt_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { //Xu ly phim Enter, Lookup danh muc

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            //Xu ly phim Enter
            if (dgvEditCt.kLastKey == Keys.Enter)
            {
                dgvEditCt.kLastKey = Keys.None;

                if (strColumnName == "TK_CO3")
                    e.Cancel = !dgvLookupTk(ref dgvCell, strColumnName);

                if (this.CellKeyEnter())
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (this.ActiveControl == null)
                return;

            //Xu ly Lookup
            if (this.ActiveControl == dgvEditCt || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                bool bLookup = true;

                if (Common.Inlist(strColumnName, "TK_NO,TK_CO,TK_NO3,TK_CO3"))
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

                else if (strColumnName == "MA_TC")
                    bLookup = dgvLookupMa_Tc(ref dgvCell);

                else if (strColumnName == "MA_TS")
                    bLookup = dgvLookupMa_Ts(ref dgvCell);

                if (bLookup == false)
                    e.Cancel = true;
            }
            else
                dgvEditCt.CancelEdit();
        }

        void dgvEditCt_CellValidated(object sender, DataGridViewCellEventArgs e)
        {// Tinh toan cac Gia tri, cong thuc
            if (bCheckDataLockedCtHanTt)
                return;

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt && this.ActiveControl != null && this.ActiveControl.GetType().Name != "DataGridViewTextBoxEditingControl")
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (Common.Inlist(strColumnName, "TIEN,TIEN_NT,TIEN_NT9"))
            {
                Voucher.Calc_Tien(drCurrent);
                Voucher.Update_TTien(this);
            }
            else if (Common.Inlist(strColumnName, "MA_THUE,TIEN_NT3,TIEN3"))
            {
                Voucher.Calc_Thue_Vat(drCurrent);
                Voucher.Update_TTien(this);
            }

            this.Ma_Thue_Valid();

            bdsEditCt.EndEdit();//Cap nhat lai DataSource
        }

        void dgvEditCt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

        void dgvEditCt2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvVoucher dgvEditCt = (dgvVoucher)sender;

            if (dgvEditCt.CurrentCell == null)
                return;

            if (this.ActiveControl != dgvEditCt)
                return;

            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (strColumnName == "TIEN3")
            {
                if ((string)drCurrent["MA_THUE"] == string.Empty)
                    dgvEditCt.Columns[strColumnName].ReadOnly = true;
                else
                {
                    if (txtMa_Tte.Text == Element.sysMa_Tte)
                        dgvEditCt.Columns[strColumnName].ReadOnly = true;
                    else
                        dgvEditCt.Columns[strColumnName].ReadOnly = false;
                }
            }
        }

        void dgvEditCt2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { //Xu ly phim Enter, Lookup danh muc

            if (this.ActiveControl != dgvEditCt2)
                return;

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            DataGridViewCell dgvCell = dgvEditCt.CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            //Xu ly phim Enter
            if (dgvEditCt.kLastKey == Keys.Enter)
            {
                dgvEditCt.kLastKey = Keys.None;

                if (dgvEditCt.bIsCurrentLastRow && dgvEditCt.bIsCurrentLastColumn)
                {
                    this.SelectNextControl(dgvEditCt, true, true, true, true);
                }
            }

            if (Common.Inlist(strColumnName, "SO_SERI0"))
            {
                string strSo_Seri = dgvCell.FormattedValue.ToString().Trim();
                strSo_Seri = strSo_Seri.ToUpper();
                dgvEditCt.CancelEdit();
                dgvCell.Value = strSo_Seri;
            }

            //Xu ly Lookup
            if (this.ActiveControl == null)
                return;

            if (this.ActiveControl == dgvEditCt || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                bool bLookup = true;

                if (strColumnName == "MA_SO_THUE")
                    bLookup = dgvLookupMa_So_Thue(ref dgvCell);

                if (bLookup == false)
                    e.Cancel = true;
            }
            else
                dgvEditCt.CancelEdit();
        }

        void dgvEditCt2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {// Tinh toan cac Gia tri, cong thuc
            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt && this.ActiveControl != null && this.ActiveControl.GetType().Name != "DataGridViewTextBoxEditingControl")
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (Common.Inlist(strColumnName, "TIEN3"))
            {
                Voucher.Calc_Thue_Vat(drCurrent);
                Voucher.Update_TTien(this);
            }

            bdsEditCt.EndEdit();//Cap nhat lai DataSource
        }

        #endregion

        #region DataGridViewLookup
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

            //frmTaiKhoan frmLookup = new frmTaiKhoan();
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
                if (!Element.sysIs_Admin)
                {
                    Hashtable htPara = new Hashtable();
                    htPara.Add("MEMBER_ID", Element.sysUser_Id);
                    htPara.Add("TK", drLookup["Tk"].ToString());

                    DataTable dtPermissionTk = SQLExec.ExecuteReturnDt("sp_CheckPermissionTk", htPara, CommandType.StoredProcedure);

                    if (dtPermissionTk.Select("Type = 'VIEW'").Length == 1 && !(bool)(dtPermissionTk.Select("Type = 'VIEW'")[0])["Allow"])
                    {
                        Common.MsgCancel("Người dùng [" + Element.sysUser_Id + "] không được xem tài khoản [" + drLookup["Tk"].ToString() + "]");
                        dgvCell.Value = string.Empty;
                        dgvCell.Tag = string.Empty;
                        return false;
                    }
                }
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

            //frmDoiTuong frmLookup = new frmDoiTuong();
            DataRow drLookup = Lookup.ShowLookup("Ma_Dt" , strValue, bRequire, "", "");

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


            DataRow drLookup = Lookup.ShowLookup("Ma_Bp", strValue, bRequire, "", "Nh_Cuoi = 1");

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

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strTk_No = (string)drCurrent["Tk_No"];
            string strTk_Co = (string)drCurrent["Tk_Co"];

            if ((bool)SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_No + "'"))
                bRequire = true;
            else if ((bool)SQLExec.ExecuteReturnValue("SELECT Tk_Km FROM LITAIKHOAN WHERE Tk = '" + strTk_Co + "'"))
                bRequire = true;

            //frmKhoanMuc frmLookup = new frmKhoanMuc();
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

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strTk_No = (string)drCurrent["Tk_No"];
            string strTk_Co = (string)drCurrent["Tk_Co"];

            if ((bool)SQLExec.ExecuteReturnValue("SELECT Tk_Sp FROM LITAIKHOAN WHERE Tk = '" + strTk_No + "'"))
                bRequire = true;
            else if ((bool)SQLExec.ExecuteReturnValue("SELECT Tk_Sp FROM LITAIKHOAN WHERE Tk = '" + strTk_Co + "'"))
                bRequire = true;

            //frmSanPham frmLookup = new frmSanPham();
            DataRow drLookup = Lookup.ShowLookup( "Ma_Sp", strValue, bRequire, "", "");

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

            bool bRequire = false;

            //frmTacVu frmLookup = new frmTacVu();
            DataRow drLookup = Lookup.ShowLookup ("Ma_Job", strValue, bRequire, "", "");

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
            if (bCheckDataLockedCtHanTt)
                return true;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            //strMa_Thue_Old = drCurrent["Ma_Thue"] == DBNull.Value ? string.Empty : (string)drCurrent["Ma_Thue"];

            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //frmThue frmLookup = new frmThue();
            DataRow drLookup = Lookup.ShowLookup("Ma_Thue", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return false;

            if (drLookup == null)
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
                drCurrent["Thue_GtGt"] = 0;

                if (dgvEditCt1.Columns.Contains("Tien_Nt3"))
                    dgvEditCt1.CurrentRow.Cells["Tien_Nt3"].ReadOnly = false;

                if (dgvEditCt1.Columns.Contains("Tien3"))
                    dgvEditCt1.CurrentRow.Cells["Tien3"].ReadOnly = false;

                dgvEditCt1.CurrentRow.Cells["Tk_No3"].ReadOnly = true;
                dgvEditCt1.CurrentRow.Cells["Tk_Co3"].ReadOnly = true;
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Thue"].ToString();
                dgvCell.Tag = drLookup["Ten_Thue"].ToString();
                drCurrent["Thue_GtGt"] = Convert.ToInt32(drLookup["Thue_Suat"]);

                //drCurrent["Ma_HoaDon"] = drLookup["Ma_HoaDon"].ToString();
                //drCurrent["Kh_HoaDon"] = drLookup["Kh_HoaDon"].ToString();

                if (dgvEditCt1.Columns.Contains("Tien_Nt3"))
                    dgvEditCt1.CurrentRow.Cells["Tien_Nt3"].ReadOnly = false;

                if (dgvEditCt1.Columns.Contains("Tien3"))
                    dgvEditCt1.CurrentRow.Cells["Tien3"].ReadOnly = false;

                dgvEditCt1.CurrentRow.Cells["Tk_No3"].ReadOnly = false;
                dgvEditCt1.CurrentRow.Cells["Tk_Co3"].ReadOnly = false;
            }

            if (strMa_Thue_Old != (string)drCurrent["Ma_Thue"])
                bMa_Thue_Changed = true;

            this.Ma_Thue_Valid();

            return true;
        }

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
                //frmDoiTuong frmLookup = new frmDoiTuong();
                DataRow drLookup = Lookup.ShowLookup( "Ma_So_Thue", strValue, bRequire, "", "");

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
                            //frmDoiTuong frmLookup = new frmDoiTuong();
                            DataRow drLookup = Lookup.ShowLookup( "Ma_So_Thue", strValue, bRequire, "");

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
                            //if (Common.MsgYes_No("Bạn có chắc chắn thêm mới đối tượng - mã số thuế?"))
                            //{
                            //    DataRow drNew = dtLookup.NewRow();
                            //    drNew["Ma_Dt"] = drNew["Ma_So_Thue"] = strValue;
                            //    drNew["Ma_Nh_Dt"] = "MA_SO_THUE";

                            //    frmDoiTuong_Edit frmEdit = new frmDoiTuong_Edit();
                            //    frmEdit.Load(enuEdit.New, drNew);

                            //    if (frmEdit.isAccept)
                            //    {
                            //        dgvEditCt1.CancelEdit();

                            //        dgvCell.Value = drNew["Ma_So_Thue"].ToString();
                            //        dgvCell.Tag = drNew["Ten_Dt"].ToString();

                            //        drCurrent["Ma_So_Thue"] = dgvCell.Value;
                            //        drCurrent["Ten_DtGtGt"] = dgvCell.Tag;
                            //    }
                            //}
                        }
                    }
                }
            }

            drCurrent.AcceptChanges();

            return true;
        }

        private bool dgvLookupMa_Tc(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            //frmThuChi frmLookup = new frmThuChi();
            DataRow drLookup = Lookup.ShowLookup("Ma_Tc", strValue, bRequire, "", "");

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
                dgvCell.Value = drLookup["Ma_Tc"].ToString();
                dgvCell.Tag = drLookup["Ten_Tc"].ToString();
            }
            return true;
        }

        private bool dgvLookupMa_Ts(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;

            frmQuickLookup frmLookup = new frmQuickLookup("ASTS", "CtTs");
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "ASTS", "Ma_Ts", strValue, bRequire, "", "");

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
                dgvCell.Value = drLookup["Ma_Ts"].ToString();
                dgvCell.Tag = drLookup["Ten_Ts"].ToString();
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
                if (!Common.CheckDataLocked((DateTime)drEdit["Ngay_Ct"]))
                {
                    this.dteNgay_Ct.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;
                }

                if (Voucher.CheckDataLockedCtHanTt(this))
                {
                    this.dteNgay_Ct.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;

                    lblNoticeThanhToan.Visible = true;
                    bCheckDataLockedCtHanTt = true;

                    //Khóa Tk, Ma_Dt, Tien
                    dteNgay_Ct.Enabled = false;
                    txtMa_Tte.Enabled = false;
                    numTy_Gia.Enabled = false;
                    txtMa_Dt.Enabled = false;
                    numHan_Tt.Enabled = false;
                    chkIs_UngTruoc.Enabled = false;

                    if (dgvEditCt1.Columns.Contains("Tk_No"))
                        dgvEditCt1.Columns["Tk_No"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tk_Co"))
                        dgvEditCt1.Columns["Tk_Co"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tk_No3"))
                        dgvEditCt1.Columns["Tk_No3"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tk_Co3"))
                        dgvEditCt1.Columns["Tk_Co3"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Ma_Dt"))
                        dgvEditCt1.Columns["Ma_Dt"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Ma_Dt_Co"))
                        dgvEditCt1.Columns["Ma_Dt_Co"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tien_Nt9"))
                        dgvEditCt1.Columns["Tien_Nt9"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tien_Nt"))
                        dgvEditCt1.Columns["Tien_Nt"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tien"))
                        dgvEditCt1.Columns["Tien"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Ma_Thue"))
                        dgvEditCt1.Columns["Ma_Thue"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tien_Nt3"))
                        dgvEditCt1.Columns["Tien_Nt3"].ReadOnly = true;

                    if (dgvEditCt1.Columns.Contains("Tien3"))
                        dgvEditCt1.Columns["Tien3"].ReadOnly = true;

                    foreach (DataGridViewColumn dgvc in dgvEditCt1.Columns)
                    {
                        if (dgvc.ReadOnly)
                            dgvc.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                    }
                }

                if (!Element.sysIs_Admin)
                {
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
            }
        }        
    }
   
}

