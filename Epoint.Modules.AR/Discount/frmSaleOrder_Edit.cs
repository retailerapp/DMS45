using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
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
using System.Security.Policy;

namespace Epoint.Modules.AR
{
    public partial class frmSaleOrder_Edit : frmVoucher_Edit
    {
        #region Declare
        private string strTk_NoTmp = string.Empty;
        private string strTk_CoTmp = string.Empty;
        private string strDMS_DISCMANUAL_COL = string.Empty;
        private string strModule = "04";
        private bool bDuyet = false;
        private bool bIs_So_Ct_Ctd = false;
        private bool bMa_Vt_Changed = false;
        private bool bCalcInvoiceDiscount = false;
        private int iDiscountRound = 0;
        private bool bDMS_DISCMANUAL = false;
        private bool bMa_Thue_Changed = false;
        private string strStt_Hd = string.Empty;
        private string strMa_Kho_Default = string.Empty;
        private string strMa_Thue_Default = string.Empty;
        private string strTk_No2 = string.Empty;
        private double dbTien_No1;
        private DataTable dtImportZ;
        private DataTable dtDiscCount;
        private DataTable dtDiscFreeItemAddNew = new DataTable();

        //Epoint.Systems.Data.LogWriter Log;
        #endregion

        #region Contructor

        public frmSaleOrder_Edit()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(frmEditCtTien_KeyDown);

            btInherit.Click += new EventHandler(btInherit_Click);
            linkHelp.LinkClicked += new LinkLabelLinkClickedEventHandler(linkHelp_LinkClicked);

            this.btImportExcel.Click += new EventHandler(btnImportExcel_Click);

            tabVoucher.SelectedIndexChanged += new EventHandler(tabVoucher_SelectedIndexChanged);
            tabVoucher.Enter += new EventHandler(tabVoucher_Enter);

            txtMa_Dt.Validating += new CancelEventHandler(txtMa_Dt_Validating);

            txtMa_Ct.Enter += new EventHandler(txtMa_Ct_Enter);
            txtMa_Ct.Validating += new CancelEventHandler(txtMa_Ct_Validating);
            txtMa_Ct.TextChanged += new EventHandler(txtMa_Ct_TextChanged);

            txtSo_Ct.Validating += new CancelEventHandler(txtSo_Ct_Validating);
            txtCt_Di_Kem.Validating += new CancelEventHandler(txtCt_Di_Kem_Validating);


            dteNgay_Ct.Validating += new CancelEventHandler(dteNgay_Ct_Validating);
            //dtNgay_Cap_B.ValueChanged +=  new EventHandler(dteNgay_Ct_1_ValueChanged);
            txtMa_Tte.Validating += new CancelEventHandler(txtMa_Tte_Validating);
            numTy_Gia.Leave += new EventHandler(numTy_Gia_Leave);

            txtMa_Thue.Enter += new EventHandler(txtMa_Thue_Enter);
            txtMa_Thue.Validating += new CancelEventHandler(txtMa_Thue_Validating);

            txtMa_So_Thue.Validating += new CancelEventHandler(txtMa_So_Thue_Validating);

            txtTk_No3.Enter += new EventHandler(txtTk_No3_Enter);
            txtTk_No3.Validating += new CancelEventHandler(txtTk_No3_Validating);

            txtTk_Co3.Enter += new EventHandler(txtTk_Co3_Enter);
            txtTk_Co3.Validating += new CancelEventHandler(txtTk_Co3_Validating);

            txtMa_CbNV.Validating += new CancelEventHandler(txtMa_CbNV_BH_Validating);
            txtMa_CbNV_GH.Validating += new CancelEventHandler(txtMa_CbNV_GH_Validating);

            txtMa_Hd.Enter += new EventHandler(txtMa_Hd_Enter);
            txtMa_Hd.Validating += new CancelEventHandler(txtMa_Hd_Validating);

            numCK_M.Validating += new CancelEventHandler(numChiet_Khau_Validating);

            numTTien.Validated += new EventHandler(numTTien_Validated);
            numTTien_Nt.Validated += new EventHandler(numTTien_Nt_Validated);
            numTTien3.Validated += new EventHandler(numTTien3_Validated);
            numTTien_Nt3.Validated += new EventHandler(numTTien_Nt3_Validated);
            //numTTien4.Validated += new EventHandler(numTTien4_Validated);
            numTTien_CK_M4.Validated += new EventHandler(numTTien_CK_M4_Validated);
            //numTTien_Nt4.Validated += new EventHandler(numTTien_Nt4_Validated);

            dgvEditCt1.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEditCt_CellValidating);
            dgvEditCt1.CellValidated += new DataGridViewCellEventHandler(dgvEditCt_CellValidated);
            dgvEditCt1.CellEnter += new DataGridViewCellEventHandler(dgvEditCt_CellEnter);
            dgvEditCt1.CellValueChanged += new DataGridViewCellEventHandler(dgvEditCt1_CellValueChanged);
            dgvEditCt1.KeyDown += new KeyEventHandler(dgvEditCt_KeyDown);
            dgvEditCt1.KeyUp += new KeyEventHandler(dgvEdit_Ct1_KeyUp);

            dgvEditCt2.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEditCt2_CellValidating);
            dgvEditCt2.CellValidated += new DataGridViewCellEventHandler(dgvEditCt2_CellValidated);
            dgvEditCt2.CellEnter += new DataGridViewCellEventHandler(dgvEditCt_CellEnter);
        }



        public override void Load(enuEdit enuNew_Edit, DataRow drEdit, DataSet dsVoucher)
        {
            //Log = LogWriter.Instance;
            this.drEdit = drEdit;
            this.dsVoucher = dsVoucher;

            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            this.strMa_Ct = ((string)drEdit["Ma_Ct"]).Trim();
            this.drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct);
            this.Object_ID = strMa_Ct;
            this.strMa_Kho_Default = DataTool.SQLGetNameByCode("SYSMEMBER", "Member_ID", "Ma_Kho_Ban", Element.sysUser_Id);
            this.strTk_No2 = Parameters.GetParaValue("DMS_TK_CO2_INT") == null ? string.Empty : (string)Parameters.GetParaValue("DMS_TK_CO2_INT");
            this.BindingCombobox();

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                this.strStt = Common.GetNewStt(strModule, true);
                //New Stt
                //Hashtable htPara = new Hashtable();
                //htPara.Add("MODULE", this.strModule);
                //htPara.Add("STTOUT", this.strStt);
                //htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                //SQLExec.ExecuteReturnDt("Sp_GetNewSttIn", htPara, CommandType.StoredProcedure);
            }
            else
            {
                this.strStt = drEdit["Stt"].ToString();
            }

            if (Parameters.GetParaValue("DMS_DISCMANUAL") != null && Parameters.GetParaValue("DMS_DISCMANUAL").ToString() != "")
            {
                this.strDMS_DISCMANUAL_COL = Parameters.GetParaValue("DMS_DISCMANUAL_COL").ToString();
                this.bDMS_DISCMANUAL = Parameters.GetParaValue("DMS_DISCMANUAL").ToString() == "Y" ? true : false;
            }

            this.Build();
            this.FillData();

            //Log.WriteToLog("this.Init_Ct();", false, null);
            this.Init_Ct();

            Common.ScaterMemvar(this, ref drEditPh);

            this.bDuyet = (bool)(drEditPh["Duyet"]);
            //Log.WriteToLog(" this.TinhSoCt1();", false, null);
            //this.TinhSoCt1();


            if (enuNew_Edit == enuEdit.Edit)
                if (drEditPh["So_Ct_Lap"].ToString() != "")
                {
                    txtMa_CbNV_GH.Enabled = false;
                    txtSo_Ct.Enabled = false;
                }

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                this.lblNgay_Crtd.Text = "Ngày Tạo:" + DateTime.Now.ToLongDateString();
                this.BindingTaxDefault(strMa_Thue_Default);
                cbxLoai_Tra_Hang.SelectedIndex = 0;
            }
            else
            {
                this.lblNgay_Crtd.Text = "Ngày Tạo:" + Convert.ToDateTime(drEditPh["Ngay_Crtd"]);
            }

            //======================DMS=================
            if (drDmCt.Table.Columns.Contains("Is_AutoPromotion"))
            {
                this.chkIs_CalDiscCount.Visible = (bool)drDmCt["Is_AutoPromotion"];
            }

            bool bDeliveryDate = Parameters.GetParaValue("ISDELIVERYDATE") == null || (string)Parameters.GetParaValue("ISDELIVERYDATE") == "0" ? false : true;
            int iDeliveryDay = Parameters.GetParaValue("DELIVERYDAY") == null || (string)Parameters.GetParaValue("DELIVERYDAY") == "" ? 0 : Convert.ToInt32(Parameters.GetParaValue("DELIVERYDAY"));

            if (enuNew_Edit == enuEdit.New)
            {
                dteNgay_Ct_Lap.Text = Library.DateToStr(Library.StrToDate(dteNgay_Ct.Text).AddDays(iDeliveryDay));
            }


            if (!bDeliveryDate)
            {
                dteNgay_Ct_Lap.Enabled = false;
            }

            if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Han_Tt"))
            {
                numHan_Tt.Value = Convert.ToDouble(this.drDmDt["Han_Tt"]);
            }

            this.txtMa_Tte.bTextChange = false;
            this.numTy_Gia.bTextChange = false;
            this.txtMa_So_Thue.bTextChange = false;

            this.Ma_Tte_Valid();
            this.BindingLanguage();
            this.LoadDicName();
            this.ResetTextChange(this);
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

            dgvEditCt2.Size = dgvEditCt1.Size;
            dgvEditCt2.Location = dgvEditCt1.Location;
            dgvEditCt2.Anchor = dgvEditCt1.Anchor;
            //dgvEditCt2.Visible = false;
            dgvEditCt2.TabIndex = dgvEditCt1.TabIndex;

            if (dgvEditCt2.Columns.Contains("So_Luong"))
                dgvEditCt2.Columns["So_Luong"].ReadOnly = true;
            // Check invisiable Discount manual column
            //if (Parameters.GetParaValue("DMS_DISCMANUAL") != null && Parameters.GetParaValue("DMS_DISCMANUAL").ToString() != "Y")
            //{
            //    //string strDisc_Col = Parameters.GetParaValue("DMS_DISCMANUAL_COL").ToString();


            //    foreach(string col in this.strDMS_DISCMANUAL_COL.Split(','))
            //    {
            //        if (dgvEditCt1.Columns.Contains(col))
            //            dgvEditCt1.Columns[col].Visible = !dgvEditCt1.Columns[col].Visible;
            //    }

            //    //if (dgvEditCt1.Columns.Contains("CHIET_KHAU"))
            //    //    dgvEditCt1.Columns["CHIET_KHAU"].Visible = !dgvEditCt1.Columns["CHIET_KHAU"].Visible;

            //    //if (dgvEditCt1.Columns.Contains("TIEN_M4"))
            //    //    dgvEditCt1.Columns["TIEN_M4"].Visible = !dgvEditCt1.Columns["TIEN_M4"].Visible;

            //    //if (dgvEditCt1.Columns.Contains("MA_CTKM_M"))
            //    //    dgvEditCt1.Columns["MA_CTKM_M"].Visible = !dgvEditCt1.Columns["MA_CTKM_M"].Visible;
            //}
            this.Ma_CTKM_Manual_Valid(!bDMS_DISCMANUAL);
            if (Parameters.GetParaValue("DMS_ROUNDDISCOUNT") != null && Parameters.GetParaValue("DMS_ROUNDDISCOUNT").ToString() != string.Empty)
            {
                this.iDiscountRound = Convert.ToInt32(Parameters.GetParaValue("DMS_ROUNDDISCOUNT"));
            }
            if (Parameters.GetParaValue("DMS_ISAFTERDISCOUNT") != null && Parameters.GetParaValue("DMS_ISAFTERDISCOUNT").ToString() == "1")
            {
                this.bCalcInvoiceDiscount = true;
            }

        }

        private void FillData()
        {
            string strKeyFilterCt = " Stt = '" + ((string)this.drEdit["Stt"]).Trim() + "' ";

            string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt ,TTien0 + TTien3 + TTien4 AS TTien_Nt9,CAST(0 AS MONEY) AS TTien_CK_M4 ";
            string strSelectCt = enuNew_Edit == enuEdit.New ? " TOP 1 * " : "*";// enuNew_Edit == enuEdit.New lấy hàng đầu tiên

            this.dtEditPh = DataTool.SQLGetDataTable((string)this.drDmCt["Table_Ph"], strSelectPh, strKeyFilterCt, null);
            this.dtEditCt = DataTool.SQLGetDataTable((string)this.drDmCt["Table_Ct"], strSelectCt, strKeyFilterCt, null);
            this.dtEditDisc = DataTool.SQLGetDataTable("OM_SalesDics", "*", strKeyFilterCt, null);


            if (dtEditCt.Columns.Contains("Tien_CK_M4") && dtEditPh.Rows.Count > 0)
                dtEditPh.Rows[0]["TTien_CK_M4"] = Common.SumDCValue(dtEditCt, "Tien_CK_M4", strKeyFilterCt);

            // Check order exists Manual discount
            if (dtEditCt.Columns.Contains("Ma_CTKM_M") && dtEditCt.Select("Ma_CTKM_M <> ''").Length > 0)
            {
                this.Ma_CTKM_Manual_Valid(true);// Always show columns manual discount.
            }

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
            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (enuNew_Edit == enuEdit.New)
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

                    string tempNgay_Ct = ((string)Epoint.Systems.Librarys.Parameters.GetParaValue("NGAY_CT"));

                    drCurrent["Ngay_Ct"] = (tempNgay_Ct == "0" && drEdit["Ngay_Ct"] != DBNull.Value) ? drEdit["Ngay_Ct"] : DateTime.Now;
                    //drCurrent["Ngay_Ct"] = Library.DateToStr(DateTime.Now);

                    if (tempNgay_Ct == "3")// Tạo số ctu theo ngày
                    {
                    }

                    // Lay mac dinh theo Ma_Ct
                    drCurrent["Tk_No2"] = (string)drDmCt["Tk_No"];
                    drCurrent["Tk_Co2"] = (string)drDmCt["Tk_Co"];


                    // Lay theo đối tượng
                    if ((string)drDmCt["Nh_Ct"] == "1")// Don tra
                    {
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Hbtl"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Hbtl"] != String.Empty ? (string)drDmDt["Tk_Hbtl"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_Co2"];

                    }
                    else// don ban
                    {
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dthu"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dthu"] != String.Empty ? (string)drDmDt["Tk_Dthu"] : drCurrent["Tk_Co2"];

                    }
                    drCurrent["Ma_Tte"] = Element.sysMa_Tte;
                    drCurrent["Ty_Gia"] = 1;

                    if (dtEditCt.Columns.Contains("Auto_Cost"))//&& (string)drDmCt["Nh_Ct"] == "2")
                        drCurrent["Auto_Cost"] = true;

                    //if (dtEditCt.Columns.Contains("COLOR"))
                    //    drCurrent["COLOR"] = "RED";

                    drCurrent["Deleted"] = false;

                    //Clear Content in drEditPh
                    foreach (DataColumn dcEditPh in dtEditPh.Columns)
                        drEditPh[dcEditPh] = DBNull.Value;

                    drEditPh["Ma_DvCs"] = drCurrent["Ma_DvCs"];
                    drEditPh["Stt"] = drCurrent["Stt"];
                    drEditPh["Ma_Ct"] = drCurrent["Ma_Ct"];
                    drEditPh["Ngay_Ct"] = drCurrent["Ngay_Ct"];
                    //drEditPh["Ngay_Ct"] = Library.DateToStr(DateTime.Now);

                    //TinhSoCt1();
                }

                if (drEditPh.Table.Columns.Contains("Duyet"))
                    drEditPh["Duyet"] = (bool)drDmCt["Default_Duyet"];
                if (drEditPh.Table.Columns.Contains("Is_Thue_Vat"))
                    drEditPh["Is_Thue_Vat"] = (bool)drDmCt["Default_VAT"];
                if (drEditPh.Table.Columns.Contains("Is_CalDiscCount"))
                    drEditPh["Is_CalDiscCount"] = (bool)drDmCt["Is_AutoPromotion"];

                if (this.drDmCt.Table.Columns.Contains("Ma_Thue_Dft"))
                    this.strMa_Thue_Default = drDmCt["Ma_Thue_Dft"].ToString();

                //Tinh so chung tu
                //string strLoai_Ma_Ct = ((DateTime)drCurrent["Ngay_Ct"]).Month.ToString().Trim();
                //string strSQLExec = "EXEC Sp_Cong_So_Ct '" + strMa_Ct + "', '" + strLoai_Ma_Ct + "'";

                //DataTable dtSo_Ct = SQLExec.ExecuteReturnDt(strSQLExec);

                //if (dtSo_Ct.Rows.Count > 0)
                //    drEditPh["So_Ct"] = drCurrent["So_Ct"] = (string)dtSo_Ct.Rows[0][0];


                //DateTime strNgayCt = ((DateTime)drCurrent["Ngay_Ct"]);
                //string strSo_Ct = TinhSoCt(drCurrent["Ma_Ct"].ToString(), strNgayCt);


                //drEditPh["So_Ct"] = drCurrent["So_Ct"] = strSo_Ct;

            }

            Voucher.Update_Header(this);

            Voucher.Update_Stt(this, strModule);

            if (this.drEditPh["Ma_Dt"].ToString() != string.Empty)
            {
                this.drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", this.drEditPh["Ma_Dt"].ToString());
                if (this.drDmDt.Table.Columns.Contains("Han_Tt"))
                {
                    numHan_Tt.Value = Convert.ToDouble(this.drDmDt["Han_Tt"]);
                }
            }
            if (dgvEditCt1.Columns.Contains("DVT"))
                dgvEditCt1.Columns["DVT"].ReadOnly = true;

            if ((string)drDmCt["Nh_Ct"] == "2")
            {
                lblCt_Di_Kem.Visible = false;
                txtCt_Di_Kem.Visible = false;

                cbxLoai_Tra_Hang.Visible = false;
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

                numTTien4.DataBindings.Clear();
                numTTien_Nt4.DataBindings.Clear();

                numTSo_Luong.DataBindings.Clear();
                numTSo_Luong9.DataBindings.Clear();
            }

            numTTien_Nt9.DataBindings.Add("Value", dtEditPh, "TTien_Nt9");

            numTTien0.DataBindings.Add("Value", dtEditPh, "TTien0");
            numTTien_Nt0.DataBindings.Add("Value", dtEditPh, "TTien_Nt0");

            numTTien_Nt3.DataBindings.Add("Value", dtEditPh, "TTien_Nt3");
            numTTien3.DataBindings.Add("Value", dtEditPh, "TTien3");

            numTTien.DataBindings.Add("Value", dtEditPh, "TTien");
            numTTien_Nt.DataBindings.Add("Value", dtEditPh, "TTien_Nt");

            numTTien4.DataBindings.Add("Value", dtEditPh, "TTien4");
            numTTien_Nt4.DataBindings.Add("Value", dtEditPh, "TTien_Nt4");

            if (dtEditPh.Columns.Contains("TTien_CK_M4"))
                numTTien_CK_M4.DataBindings.Add("Value", dtEditPh, "TTien_CK_M4");

            //numTTien4.DataBindings.Add("Value", dtEditPh, "TTien4");

            numTSo_Luong.DataBindings.Add("Value", dtEditPh, "TSo_Luong");
            if (dtEditPh.Columns.Contains("TSo_Luong9"))
                numTSo_Luong9.DataBindings.Add("Value", dtEditPh, "TSo_Luong9");
            //Quyen so
            //string strSQL = "SELECT Quyen_So FROM LIQUYENSO WHERE Month(Ngay_Begin) <= Month('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Month(Ngay_End) >= Month('"
            //             + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "') AND Year(Ngay_End) = Year ('" + Convert.ToDateTime(drCurrent["Ngay_Ct"]).ToShortDateString() + "')";
            //DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);


            this.dtEditCtDisc = SQLExec.ExecuteReturnDt("DECLARE @EditDisc AS TVP_DiscAmt SELECT * FROM @EditDisc");

            Discount.CopyDataTable(this.dtEditDisc, this.dtEditCtDisc, string.Empty);

            /*
                this.dtEditCtDisc = new DataTable("OM_SalesDics");
                DataColumn dcStt0 = new DataColumn("Stt0", typeof(string));
                dcStt0.DefaultValue = "";
                dtEditCtDisc.Columns.Add(dcStt0);
                DataColumn dcMa_CTKM = new DataColumn("Ma_CTKM", typeof(string));
                dcMa_CTKM.DefaultValue = "";
                dtEditCtDisc.Columns.Add(dcMa_CTKM);
                DataColumn dcStt_Km = new DataColumn("Stt_Km", typeof(string));
                dcStt_Km.DefaultValue = "";
                dtEditCtDisc.Columns.Add(dcStt_Km);

                DataColumn dcTien4_Org = new DataColumn("Tien4_Org", typeof(string));
                dcTien4_Org.DefaultValue = 0;
                dtEditCtDisc.Columns.Add(dcTien4_Org);

                DataColumn dcTien4 = new DataColumn("Tien4", typeof(double));
                dcTien4.DefaultValue = 0;
                dtEditCtDisc.Columns.Add(dcTien4);
             */

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
                numSoDu131.Value = Voucher.GetDuCuoiDt(drEditPh, "1311", txtMa_Dt.Text);
            }
            else
                txtTen_Dt.Text = string.Empty;

            //txtMa_Hd


            //txtMa_NVbh
            if (txtMa_CbNV.Text.Trim() != string.Empty)
            {
                txtTen_NVBH.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNV", "Ten_CbNV", txtMa_CbNV.Text.Trim());
                dicName.SetValue(txtTen_NVBH.Name, txtTen_NVBH.Text);
            }

            else
                txtTen_NVBH.Text = string.Empty;


            //txtMa_NVGh
            if (txtMa_CbNV_GH.Text.Trim() != string.Empty)
            {
                txtTen_NVGH.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNV", "Ten_CbNV", txtMa_CbNV_GH.Text.Trim());
                dicName.SetValue(txtTen_NVGH.Name, txtTen_NVGH.Text);
            }
            else
                txtTen_NVGH.Text = string.Empty;


            //txtMa_HD
            if (txtMa_Hd.Text.Trim() != string.Empty)
            {
                txtTen_Hd.Text = DataTool.SQLGetNameByCode("LIHOPDONG", "Ma_HD", "Ten_HD", txtMa_Hd.Text.Trim());
                dicName.SetValue(txtTen_Hd.Name, txtTen_Hd.Text);
            }
            else
                txtTen_Hd.Text = string.Empty;
        }

        private bool FormCheckValid()
        {
            double dbTienAlow = Convert.ToDouble(Parameters.GetParaValue("TRON_THANH_TIEN_BAN"));

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


            if (txtMa_Dt.Text == string.Empty)
            {
                EpointMessage.MsgOk("Mã khách hàng không được rỗng ");
                txtMa_Dt.Focus();
                return false;
            }


            if (txtMa_CbNV.Text == string.Empty)
            {
                EpointMessage.MsgOk("Mã nhân viên bán hàng không được rỗng ");
                txtMa_CbNV.Focus();
                return false;
            }

            if ((string)drDmCt["Nh_Ct"] == "1" &&  this.cbxLoai_Tra_Hang.SelectedValue.ToString() == string.Empty)
            {
                EpointMessage.MsgOk("Chọn lý do trả hàng !");
                cbxLoai_Tra_Hang.Focus();
                return false;
            }
            if (drDmCt["Nh_Ct"].ToString() == "1" && txtMa_CbNV_GH.Text == string.Empty) // Kiểm tra mã NVGH của đơn trả hàng
            {
                EpointMessage.MsgOk("Mã nhân viên giao hàng không được rỗng ");
                txtMa_CbNV_GH.Focus();
                return false;
            }


            //Kiểm tra nghiệp vụ hợp lệ
            foreach (DataRow dr in dtEditCt.Rows)
            {
                if ((bool)dr["Deleted"])
                    continue;

                //if (dtEditCt.Columns.Contains("Tien") && Convert.ToDouble(dr["So_Luong"]) != 0 && ((string)dr["Tk_No"] == string.Empty || (string)dr["Tk_Co"] == string.Empty))
                //{
                //    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán giá vốn không hợp lệ" : "Transaction cost of good sold invalid";
                //    Common.MsgCancel(strMsg);
                //    return false;
                //}



                if (dtEditCt.Columns.Contains("Tien2") && Convert.ToDouble(dr["Tien2"]) != 0 && ((string)dr["Tk_No2"] == string.Empty || (string)dr["Tk_Co2"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán doanh thu không hợp lệ" : "Transaction turnover invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }
                if (dtEditCt.Columns.Contains("Ma_Thue") && (string)dr["Ma_Thue"] != string.Empty && ((string)dr["Tk_No3"] == string.Empty || (string)dr["Tk_Co3"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Hạch toán thuế không hợp lệ" : "Transaction VAT invalid";
                    Common.MsgCancel(strMsg);
                    return false;
                }

                if (Convert.ToDouble(dr["So_Luong9"]) < 0)
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Tồn tại 1 dòng có lượng âm !" : "Qty not avail!";
                    Common.MsgCancel(strMsg);
                    return false;
                }

                if (Convert.ToDouble(dr["So_Luong9"]) != 0 && ((string)dr["Ma_Vt"] == string.Empty))
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Có dòng mã vật tư bằng rỗng!" : "Ma_VT is not empty!";
                    Common.MsgCancel(strMsg);
                    return false;
                }

                if (dtEditCt.Columns.Contains("Tien_M4") && Convert.ToDouble(dr["Tien_M4"]) == 0 && !Convert.ToBoolean(dr["Hang_Km"]))
                {
                    dr["MA_CTKM_M"] = string.Empty;
                }


                double dHe_So9 = Convert.ToDouble(dr["He_So9"]);
                double dbSo_Luong9 = (dr["So_Luong9"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["So_Luong9"]);
                double dbGia_Nt9 = (dr["Gia_Nt9"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["Gia_Nt9"]);
                double dbTien_Nt9 = (dr["Tien_Nt9"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["Tien_Nt9"]);
                double dbTy_Gia = Convert.ToDouble(dr["Ty_Gia"]);

                double dbChenh_Lech = Math.Round((dbTien_Nt9 - dbSo_Luong9 * dbGia_Nt9) * dbTy_Gia, MidpointRounding.AwayFromZero);

                if (Math.Abs(dbChenh_Lech) > dbTienAlow)
                {
                    string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Có dòng mã vật tư tiền hàng chênh lệch: " + (string)dr["Ma_Vt"] : "Exists row has value do not balance : " + (string)dr["Ma_Vt"];
                    Common.MsgCancel(strMsg);
                    return false;
                }
            }
            if (!Voucher.CheckDuplicateInvoice(this))
                return false; return true;
        }

        public override bool Save()
        {

            //if (this.bDuyet && (string)drDmCt["Nh_Ct"] == "2")
            //{
            //    EpointMessage.MsgOk("Đơn hàng không thể lưu thay đổi !");
            //    return false;
            //}

            if (!btgAccept.btAccept.Enabled)
            {
                base.ShowSuccessMessage("Không lưu được thay đổi !");
                this.Close();
                return false;

            }

            //CheckSo_Ct1();
            Common.GatherMemvar(this, ref this.drEditPh);

            //this.drEditPh["Ngay_Ct"] = dteNgay_Ct_1.Value;

            Voucher.Update_Detail(this);

            if (!FormCheckValid())
                return false;

            if (this.enuNew_Edit == enuEdit.New || this.enuNew_Edit == enuEdit.Copy)
            {
                drEditPh["Create_Log"] = Common.GetCurrent_Log();
                drEditPh["LastModify_Log"] = string.Empty;
                this.TinhSoCtDms();
            }
            else
            {
                drEditPh["LastModify_Log"] = Common.GetCurrent_Log();
                if ((string)drEditPh["Create_Log"] == string.Empty)
                    drEditPh["Create_Log"] = drEditPh["LastModify_Log"];

                Hashtable htPara = new Hashtable();
                htPara["STT"] = drEditPh["STT"].ToString();
                htPara["MA_DVCS"] = Element.sysMa_DvCs;

                //SQLExec.ExecuteReturnValue("sp_Check_PXKDetail",htPara,CommandType.StoredProcedure)

                if (Common.Inlist(strMa_Ct, "IN,INT") && !Convert.ToBoolean(SQLExec.ExecuteReturnValue("sp_Check_PXKDetail", htPara, CommandType.StoredProcedure)))
                {
                    drEditPh["So_Ct_Lap"] = string.Empty;
                    drEditPh["Duyet"] = false;
                }

            }

            // this.TestDiscount();
            // tính khuyến mãi tự động
            if ((bool)drDmCt["Is_AutoPromotion"] && chkIs_CalDiscCount.Checked)
                this.CalcDiscount();



            Voucher.Calc_So_Luong_All(this);
            Calc_Thue_Vat_All();
            Voucher.Update_TTien(this);
            Voucher.Update_Stt(this, strModule);

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                if (dtEditCt.Rows.Count > 0) //Cập nhật lại dữ liệu từ chi tiết lên Header
                    Common.CopyDataRow(dtEditCt.Rows[0], drEditPh, (string)drDmCt["Update_Header"]);

                drEdit = drEditPh;
            }


            if ((string)drDmCt["Nh_Ct"] == "1" && Parameters.GetParaValue("DMS_CHECKSDHD") != null && Parameters.GetParaValue("DMS_CHECKSDHD").ToString() == "Y")
                if (this.drEditPh["Stt_Org"].ToString() != string.Empty) // Check hóa đơn bán hàng còn dư nợ hay không
                {
                    dbTien_No1 = Voucher.GetDuCuoiCtHd(this, this.drEditPh["Stt_Org"].ToString(), string.Empty);
                    if (dbTien_No1 >= Convert.ToDouble(drEditPh["TTien0"]))
                    {
                        this.drEditPh["Is_ThanhToan"] = true;
                    }
                    else
                    {
                        EpointMessage.MsgOk("Chứng từ mua hàng đã hết số dư, không thể áp cấn trừ công nợ!");
                        this.drEditPh["Is_ThanhToan"] = false;
                        if (this.strTk_No2 != string.Empty)
                            foreach (DataRow dr in dtEditCt.Rows)
                            {
                                dr["Tk_Co2"] = this.strTk_No2;
                            }
                    }
                }

            //Discount.OM_SaveOM_SalesDics(this);
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

                if (dgvEditCt1.Columns.Contains("TIEN2"))
                    dgvEditCt1.Columns["TIEN2"].Visible = false;

                if (dgvEditCt2.Columns.Contains("TIEN"))
                    dgvEditCt2.Columns["TIEN"].Visible = false;

                if (dgvEditCt2.Columns.Contains("GIA"))
                    dgvEditCt2.Columns["GIA"].Visible = false;
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


                if (dgvEditCt1.Columns.Contains("TIEN2"))
                    dgvEditCt1.Columns["TIEN2"].Visible = true;

                if (dgvEditCt2.Columns.Contains("TIEN"))
                    dgvEditCt2.Columns["TIEN"].Visible = true;

                if (dgvEditCt2.Columns.Contains("GIA"))
                    dgvEditCt2.Columns["GIA"].Visible = true;
            }

            if (dteNgay_Ct.Text != Library.DateToStr((DateTime)drEditPh["Ngay_Ct"]) || txtMa_Tte.bTextChange || numTy_Gia.bTextChange)
            {
                Voucher.Update_Detail(this);
                Voucher.Calc_Tien_All(this);
                Voucher.Adjust_TThue_Vat(this, true);

                if (txtMa_Tte.bTextChange)
                    txtMa_Tte.bTextChange = false;
            }

            numTTien_Nt.Scale = numTTien_Nt0.Scale = numTTien_Nt3.Scale = numTTien_Nt4.Scale = strMa_Tte == Element.sysMa_Tte ? 0 : 2;

            //Voucher.FormatTien_Nt(dgvEditCt1, strMa_Tte);
            Voucher.FormatTien_Nt(dgvEditCt2, strMa_Tte);

            dgvEditCt1.ResizeGridView();
            dgvEditCt2.ResizeGridView();
        }

        private void Ma_Thue_Valid()
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

                return;
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
                    if ((string)drDmCt["Nh_Ct"] == "1") // Phieu tra lai
                    {
                        txtTk_No3.Text = (string)drDmThue["Tk"];
                        txtTk_Co3.Text = (string)drEditCt["Tk_No2"];
                    }
                    else // Hoa don ban hang
                    {
                        txtTk_No3.Text = (string)drEditCt["Tk_No2"];
                        txtTk_Co3.Text = (string)drDmThue["Tk"];
                    }
                }
                else
                {
                    if ((string)drDmCt["Nh_Ct"] == "1") // Phieu tra lai
                    {
                        if (txtTk_No3.Text.Trim() == string.Empty)
                            txtTk_No3.Text = (string)drDmThue["Tk"];

                        if (txtTk_Co3.Text.Trim() == string.Empty)
                            txtTk_Co3.Text = (string)drEditCt["Tk_No2"];
                    }
                    else // Hoa don ban hang
                    {
                        if (txtTk_No3.Text.Trim() == string.Empty)
                            txtTk_Co3.Text = (string)drEditCt["Tk_No2"];

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

            return;
        }
        private void Ma_CTKM_Manual_Valid(bool bVisibleColumn)
        {
            foreach (DataGridViewColumn dgvc in dgvEditCt1.Columns)
            {
                if (Common.Inlist(dgvc.Name, strDMS_DISCMANUAL_COL))//"TIEN_M4"  "MA_CTKM_M"  "CHIET_KHAU"
                {
                    dgvc.Visible = bVisibleColumn;
                }
            }
        }
        private void f()
        {
            if (this.enuNew_Edit != enuEdit.New)
                return;

            if (bIs_So_Ct_Ctd)
                return;

            string Ma_Ct = txtMa_Ct.Text;
            DateTime Ngay_Ct = Library.StrToDate(dteNgay_Ct.Text);
            string strSo_Ct_New = string.Empty;


            Hashtable htParameter = new Hashtable();
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);
            htParameter.Add("MA_CT", this.drEditPh["Ma_Ct"]);
            htParameter.Add("NGAY_CT", Library.DateToStr((DateTime)this.drEditPh["Ngay_Ct"]));         
            strSo_Ct_New = SQLExec.ExecuteReturnValue("sp_Cong_So_Ct_New", htParameter, CommandType.StoredProcedure).ToString();

            txtSo_Ct.Text = strSo_Ct_New;
            this.drEditPh["So_Ct"] = strSo_Ct_New;
            this.drEditPh.AcceptChanges();
            this.strSo_Ct = strSo_Ct_New;
            Common.CopyDataRow(drEditPh, dtEditCt.Rows[0], "So_Ct");

            if (Parameters.GetParaValue("SO_CT_DMS") == null || Parameters.GetParaValue("SO_CT_DMS").ToString() == "C")
                bIs_So_Ct_Ctd = true;

        }
        private void TinhSoCtDms()
        {
            if (this.enuNew_Edit != enuEdit.New)
                return;

            if (bIs_So_Ct_Ctd)
                return;

            string Ma_Ct = txtMa_Ct.Text;
            DateTime Ngay_Ct = Library.StrToDate(dteNgay_Ct.Text);
            string strSo_Ct_New = string.Empty;


            Hashtable htParameter = new Hashtable();
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);
            htParameter.Add("MA_CT", this.drEditPh["Ma_Ct"]);
            htParameter.Add("NGAY_CT", Library.DateToStr((DateTime)this.drEditPh["Ngay_Ct"]));
            htParameter.Add("MA_DT", this.txtMa_Dt.Text);
            htParameter.Add("LOAI_TRA_HANG", this.cbxLoai_Tra_Hang.Text);
            strSo_Ct_New = SQLExec.ExecuteReturnValue("sp_Cong_So_Ct_Dms", htParameter, CommandType.StoredProcedure).ToString();

            txtSo_Ct.Text = strSo_Ct_New;
            this.drEditPh["So_Ct"] = strSo_Ct_New;
            this.drEditPh.AcceptChanges();
            this.strSo_Ct = strSo_Ct_New;
            Common.CopyDataRow(drEditPh, dtEditCt.Rows[0], "So_Ct");

            if (Parameters.GetParaValue("SO_CT_DMS") == null || Parameters.GetParaValue("SO_CT_DMS").ToString() == "C")
                bIs_So_Ct_Ctd = true;

        }
        private bool CellKeyEnter()
        {//Ham thuc hien phim Enter: true: thuc hien thanh cong, false: khong thuc hien duoc			

            if (dgvEditCt1.CurrentCell == null)
                return false;

            DataGridViewCell dgvCell = dgvEditCt1.CurrentCell;
            string strCurrentColumn = dgvCell.OwningColumn.Name.ToUpper();

            #region Enter tai Tk_Co2
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
                        this.SelectNextControl(dgvEditCt1, true, true, true, true);

                    return true;
                }

                return false;
            }
            #endregion

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

                    if (dgvEditCt1.bIsCurrentLastRow)
                    {
                        if (!Voucher.AddRow(this))
                            this.SelectNextControl(dgvEditCt1, true, true, true, true);
                        else
                        {
                            dgvEditCt1.FocusNextFirstCell();
                            return true;
                        }
                    }
                    else
                        dgvEditCt1.FocusNextFirstCell();

                }
                return false;
            }

            #endregion

            #region Enter TIEN2
            if (Common.Inlist(strCurrentColumn, "TIEN2"))
            {
                if (dgvEditCt1.bIsCurrentLastRow)
                {
                    // Cap nhat Tien2 truoc khi xuống dòng
                    double dbTien2 = 0;
                    if (double.TryParse(dgvEditCt1.CurrentCell.FormattedValue.ToString().Trim(), out dbTien2))
                    {
                        dgvEditCt1.CancelEdit();
                        drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                        drCurrent["TIEN2"] = dbTien2;
                        Voucher.Calc_So_Luong(drCurrent);
                        Voucher.Update_TTien(this);
                    }

                    if (!Voucher.AddRow(this))
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

        private void Auto_Cost_Change(DataGridViewRow dgvRow, bool bAuto_Cost)
        {
            if (bAuto_Cost)
            {
                dgvRow.Cells["TIEN_NT"].Value = 0;
                dgvRow.Cells["TIEN"].Value = 0;
                dgvRow.Cells["GIA_NT"].Value = 0;
                dgvRow.Cells["GIA"].Value = 0;

                dgvRow.Cells["TIEN_NT"].ReadOnly = true;
                dgvRow.Cells["TIEN"].ReadOnly = true;
                dgvRow.Cells["GIA_NT"].ReadOnly = true;
                dgvRow.Cells["GIA"].ReadOnly = true;
            }
            else
            {
                dgvRow.Cells["TIEN_NT"].ReadOnly = false;
                dgvRow.Cells["TIEN"].ReadOnly = false;
                dgvRow.Cells["GIA_NT"].ReadOnly = false;
                dgvRow.Cells["GIA"].ReadOnly = false;
            }
        }

        //Thue_VAT
        public void Calc_Thue_Vat_Tk(DataRow drEditCt)
        {
            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", drEditCt["Ma_Thue"].ToString());

            if (drDmThue != null)
            {
                if ((string)drDmCt["Nh_Ct"] == "1") // Phieu tra lai
                {
                    drEditCt["Tk_No3"] = (string)drDmThue["Tk"];
                    drEditCt["Tk_Co3"] = (string)drEditCt["Tk_No2"];
                    drEditCt["Thue_GTGT"] = drDmThue["Thue_Suat"];
                }
                else // Hoa don ban hang
                {
                    drEditCt["Tk_No3"] = (string)drEditCt["Tk_No2"];
                    drEditCt["Tk_Co3"] = (string)drDmThue["Tk"];
                    drEditCt["Thue_GTGT"] = drDmThue["Thue_Suat"];
                }

            }
            else
            {
                drEditCt["Tk_No3"] = string.Empty;
                drEditCt["Tk_Co3"] = string.Empty;
            }
        }
        public void Calc_Thue_Vat(DataRow drEditCt)
        {
            if (drEditCt["Ma_Thue"].ToString() == string.Empty)
            {
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;

                drEditCt.AcceptChanges();
                return;
            }

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", (string)drEditCt["Ma_Ct"]);
            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);

            if (drDmThue == null || drDmCt == null)
                return;

            double dbTy_Gia = Convert.ToDouble(drEditCt["Ty_Gia"]);
            double dbThue_Gtgt = Convert.ToDouble(drEditCt["Thue_Gtgt"]);
            string strMa_Nvu = (string)drDmCt["Ma_Nvu"];

            //Gia co phu phi
            if ((bool)drDmThue["Gia_Pp"])
            {
                double dbTien_Nt3_ = drEditCt["Tien_Nt3"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien_Nt3"]) : 0;
                double dbTien3_ = drEditCt["Tien3"] != DBNull.Value ? Convert.ToDouble(drEditCt["Tien3"]) : 0;

                double dbChenh_Lech_Ty_Gia = dbTien3_ - dbTien_Nt3_ * dbTy_Gia;
                if (Math.Abs(dbChenh_Lech_Ty_Gia) > Convert.ToDouble(Parameters.GetParaValue("Tron_Ty_Gia")))
                    drEditCt["Tien3"] = Math.Round(dbTien_Nt3_ * dbTy_Gia, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    drEditCt["Tien3"] = drEditCt["Tien_Nt3"];

                return;
            }

            double dbTien_Nt = Convert.ToDouble(drEditCt["Tien_Nt2"]);
            double dbTien = Convert.ToDouble(drEditCt["Tien2"]);

            dbTien_Nt = Convert.ToDouble(drEditCt["Tien_Nt9"]) - Convert.ToDouble(drEditCt["Tien_Nt4"]);
            dbTien = Convert.ToDouble(drEditCt["Tien_Nt9"]) - Convert.ToDouble(drEditCt["Tien4"]);

            //double dbTien_Nt3 = drEditCt["Tien_Nt3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
            //double dbTien3 = drEditCt["Tien3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien3"]);

            double dbTien_Nt3 = drEditCt["Tien_Nt3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
            drEditCt["Tien_Nt3"] = dbTien_Nt3 = Math.Round(dbTien_Nt3, 2, MidpointRounding.AwayFromZero);

            double dbTien3 = drEditCt["Tien3"] == DBNull.Value ? 0 : Convert.ToDouble(drEditCt["Tien3"]);
            drEditCt["Tien3"] = dbTien3 = Math.Round(dbTien3, 0, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                drEditCt["Tien3"] = drEditCt["Tien_Nt3"] = dbTien3 = dbTien_Nt3 = Math.Round(dbTien3, 2, MidpointRounding.AwayFromZero);

            double dbTien_Nt3_Calc = 0;
            double dbTien3_Calc = 0;

            if (drDmThue == null)
                return;

            //Gia da bao gom VAT
            if (drDmThue["Gia_Thue"].ToString() == "1")
            {
                //dbTien_Nt += dbTien_Nt3;
                //dbTien += dbTien3;

                double dbThue_Suat = Convert.ToDouble(dbThue_Gtgt) / 100;
                dbTien_Nt3_Calc = Math.Round(Math.Ceiling((dbTien_Nt * dbThue_Suat / (1 + dbThue_Suat)) * 100) / 100, 2, MidpointRounding.AwayFromZero);
                dbTien3_Calc = Math.Round(Math.Ceiling((dbTien * dbThue_Suat / (1 + dbThue_Suat)) * 100) / 100, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt3_Calc = Math.Round(dbTien3_Calc, MidpointRounding.AwayFromZero);

                dbTien_Nt = dbTien_Nt - dbTien_Nt3_Calc;
                dbTien = dbTien - dbTien3_Calc;

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt = dbTien;
            }
            else
            {
                dbTien_Nt3_Calc = Math.Round(Math.Ceiling((dbTien_Nt * dbThue_Gtgt) * 100) / 100 / 100, 2, MidpointRounding.AwayFromZero);
                dbTien3_Calc = Math.Round(Math.Ceiling((dbTien * dbThue_Gtgt / 100) * 100) / 100, MidpointRounding.AwayFromZero);

                if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                    dbTien_Nt3_Calc = Math.Round(dbTien_Nt3_Calc, MidpointRounding.AwayFromZero);
            }

            //Cho phep dieu chinh trong gioi han dbTron_Vat
            double dbTron_Vat = Convert.ToDouble(Parameters.GetParaValue("Tron_VAT"));

            if (Math.Abs(dbTien3_Calc - dbTien3) > dbTron_Vat ||
                Math.Abs(dbTien_Nt3_Calc - dbTien_Nt3) * dbTy_Gia > dbTron_Vat || dbTien3 == 0)
            {
                drEditCt["Tien3"] = dbTien3_Calc;
                drEditCt["Tien_Nt3"] = dbTien_Nt3_Calc;
            }

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                drEditCt["Tien3"] = drEditCt["Tien_Nt3"];

            if (strMa_Nvu == "K")// Nếu là phiếu kế toán
                drEditCt["Tien_Nt9"] = dbTien_Nt;

            if ((bool)drDmCt["Is_Hd"])// Hóa đơn bán hàng
            {
                drEditCt["Tien_Nt2"] = dbTien_Nt;
                drEditCt["Tien2"] = dbTien;
            }
            else
            {
                drEditCt["Tien_Nt"] = dbTien_Nt;
                drEditCt["Tien"] = dbTien;
            }

            drEditCt.AcceptChanges();
        }

        public void Calc_Thue_Vat_All()
        {
            for (int i = 0; i <= dtEditCt.Rows.Count - 1; i++)
            {
                DataRow drEditCt = dtEditCt.Rows[i];

                if (dtEditCt.Columns.Contains("DELETED") && (bool)drEditCt["DELETED"] == true)
                    continue;

                Calc_Thue_Vat(drEditCt);
            }


        }

        private void TTien_Valid()
        {

            numTTien0.Value = numTTien_Nt0.Value * numTy_Gia.Value;

            if (numTTien3.Value == 0)
                numTTien3.Value = numTTien_Nt3.Value * numTy_Gia.Value;
            else if (numTTien_Nt3.Value == 0 && numTy_Gia.Value != 0)
                numTTien_Nt3.Value = numTTien3.Value / numTy_Gia.Value;

            if (numTTien4.Value == 0)
                numTTien4.Value = numTTien_Nt4.Value * numTy_Gia.Value;
            else if (numTTien_Nt4.Value == 0 && numTy_Gia.Value != 0)
                numTTien_Nt4.Value = numTTien4.Value / numTy_Gia.Value;

            this.drEditPh["TTien0"] = numTTien0.Value;
            this.drEditPh["TTien_Nt0"] = numTTien_Nt0.Value;
            this.drEditPh["TTien3"] = numTTien3.Value;
            this.drEditPh["TTien_Nt3"] = numTTien_Nt3.Value;


            this.drEditPh["TTien"] = Convert.ToDouble(this.drEditPh["TTien0"]) + Convert.ToDouble(this.drEditPh["TTien3"]);
            this.drEditPh["TTien_Nt"] = Convert.ToDouble(this.drEditPh["TTien_Nt0"]) + Convert.ToDouble(this.drEditPh["TTien_Nt3"]);

            //if (numTTien_Nt3.bTextChange || numTTien3.bTextChange)
            //    Voucher.Adjust_TThue_Vat(this, false);


            this.drEditPh["TTien4"] = numTTien4.Value;
            this.drEditPh["TTien_Nt4"] = numTTien_Nt4.Value;

            //if (chkIs_Sua_ChietKhau.Checked)
            //{
            //    double dbTTien_Nt9 = Common.SumDCValue(dtEditCt, "Tien_Nt9", "");
            //    if (dbTTien_Nt9 != 0)
            //        numChiet_Khau.Value = Math.Round(numTTien_Nt4.Value * 100 / dbTTien_Nt9, 4, MidpointRounding.AwayFromZero);

            //    Voucher.Update_Detail(this, "Chiet_Khau");
            //    Voucher.Calc_Chiet_Khau_All(this);
            //}
            //else
            //{
            //    Voucher.Adjust_Chiet_Khau(this);
            //}


            if (Common.SumDCValue(this.dtEditCt, "Tien3", "Deleted <> true") != numTTien3.Value || Common.SumDCValue(this.dtEditCt, "Tien_Nt3", "Deleted <> true") != numTTien_Nt3.Value)
            {
                this.drEditPh["TTien3"] = numTTien3.Value;
                this.drEditPh["TTien_Nt3"] = numTTien_Nt3.Value;

                Voucher.Adjust_TThue_Vat(this, false);
            }
        }

        public void Update_Gia_Vt(DataRow drEditCt)
        {
            double dbGiaMax = 0;
            //Thong: Lay giá gần nhất từ chứng từ, chỉ lấy ra giá khi có số lượng   
            if ((bool)drDmCt["Is_Gia"])
            {
                //Chi cap nhật gia vat tu khi co so luong
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
                htParameter.Add("STT", this.drEditPh["Stt"]);
                htParameter.Add("MA_DVCS", Element.sysMa_DvCs);
                dbGiaMax = Convert.ToDouble(SQLExec.ExecuteReturnValue("Sp_GetGiaMax_AR", htParameter, CommandType.StoredProcedure));
                drEditCt["Gia_Nt9"] = Math.Round(dbGiaMax * Convert.ToDouble(drEditCt["He_So9"]), 0);
                if (drEditCt.Table.Columns.Contains("Gia_Dft"))
                    drEditCt["Gia_Dft"] = drEditCt["Gia_Nt9"];
            }
            else if (!(bool)drDmCt["Is_Gia"])//Lay gia trong chinh sach gia
            {
                //Chi cap nhật gia vat tu khi co so luong
                if (drEditCt["Ma_Vt"] == DBNull.Value || (string)drEditCt["Ma_Vt"] == string.Empty)
                    return;

                if (drEditCt["So_Luong9"] == DBNull.Value || Convert.ToDouble(drEditCt["So_Luong9"]) == 0)
                    return;

                if (drEditCt["Gia_Nt9"] != DBNull.Value && Convert.ToDouble(drEditCt["Gia_Nt9"]) != 0)
                    return;

                Hashtable htParameter = new Hashtable();
                htParameter.Add("MA_VT", (string)drEditCt["Ma_Vt"]);
                htParameter.Add("MA_DT", (string)drEditCt["Ma_Dt"]);
                htParameter.Add("DVT", (string)drEditCt["DVT"]);
                htParameter.Add("NGAY_CT", this.dteNgay_Ct.Text);
                DataTable dtGia = SQLExec.ExecuteReturnDt("sp_GetGiaBan", htParameter, CommandType.StoredProcedure);

                //drEditCt["Gia_Nt9"] = SQLExec.ExecuteReturnValue("sp_GetGiaBan", htParameter, CommandType.StoredProcedure);
                if (dtGia != null && dtGia.Rows.Count > 0)
                {
                    drEditCt["Gia_Nt9"] = Convert.ToDouble(dtGia.Rows[0][0]);

                    if (Convert.ToBoolean(drEditCt["Hang_Km"]))
                        drEditCt["Gia_Nt9"] = 0;
                    if (drEditCt.Table.Columns.Contains("Gia_Dft"))
                        drEditCt["Gia_Dft"] = Convert.ToDouble(dtGia.Rows[0][1]);
                }
            }
        }
        public void Calc_Chiet_Khau(DataRow drEditCt)
        {
            string strMa_Tte = (string)drEditCt["Ma_Tte"];

            double dbTien_Nt2 = (drEditCt["Tien_Nt2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt2"]);

            double dbTien2_Old = 0;
            double dbTien2 = (drEditCt["Tien2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien2"]);
            if (drEditCt.RowState == DataRowState.Modified)
                dbTien2_Old = (drEditCt["Tien2"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien2", DataRowVersion.Original]);
            else
                dbTien2_Old = dbTien2;

            //Nếu gõ lại thành tiền phải tính lại từ đầu
            if (dbTien2_Old != dbTien2)
            {
                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;

                drEditCt["Tien_Nt4"] = 0;
                drEditCt["Tien4"] = 0;
            }

            DataRow drDmThue = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", (string)drEditCt["Ma_Thue"]);
            //Gia da bao gom VAT
            if (drDmThue != null && drDmThue["Gia_Thue"].ToString() == "1")
            {
                double dbTien_Nt3 = (drEditCt["Tien_Nt3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt3"]);
                double dbTien3 = (drEditCt["Tien3"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien3"]);

                dbTien_Nt2 += dbTien_Nt3;
                dbTien2 += dbTien3;

                drEditCt["Tien_Nt3"] = 0;
                drEditCt["Tien3"] = 0;
            }

            double dbTien_Nt4 = (drEditCt["Tien_Nt4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien_Nt4"]);
            double dbTien4 = (drEditCt["Tien4"] == DBNull.Value) ? 0 : Convert.ToDouble(drEditCt["Tien4"]);
            double dbChiet_Khau = Convert.ToDouble(drEditCt["Chiet_Khau"]);

            dbTien_Nt2 += dbTien_Nt4;
            dbTien2 += dbTien4;

            double dbTien_Nt4_Calc = dbTien_Nt4;
            double dbTien4_Calc = dbTien4;

            //dbTien_Nt4_Calc = Math.Round(dbTien_Nt2 * dbChiet_Khau / 100, 2, MidpointRounding.AwayFromZero);
            //dbTien4_Calc = Math.Round(dbTien2 * dbChiet_Khau / 100, MidpointRounding.AwayFromZero);

            if ((string)drEditCt["Ma_Tte"] == Element.sysMa_Tte)
                dbTien_Nt4_Calc = dbTien4_Calc;

            dbTien_Nt2 = dbTien_Nt2 - dbTien_Nt4_Calc;
            dbTien2 = dbTien2 - dbTien4_Calc;

            drEditCt["Tien_Nt4"] = dbTien_Nt4_Calc;
            drEditCt["Tien4"] = dbTien4_Calc;
            drEditCt["Tien_Nt2"] = dbTien_Nt2;
            drEditCt["Tien2"] = dbTien2;

            Calc_Thue_Vat(drEditCt);
        }

        private void HanTt()
        {
            frmHanTtCust_View frmHanTt = new frmHanTtCust_View();
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
        private void TinhSoCt0()
        {
            string strSQLExec = "SELECT DISTINCT So_Ct0 FROM ARBAN WHERE Ma_DvCs = '" + Element.sysMa_DvCs + "' AND So_Seri0 ='" + txtSo_Seri0.Text + "' ";//"AND YEAR(Ngay_Ct) = "+ Element.sysWorkingYear;
            DataTable dt = SQLExec.ExecuteReturnDt(strSQLExec);
            if (dt.Rows.Count > 0)
            {
                txtSo_Ct0.Text = Voucher.Cong_So_Ct0(this);

            }
            else
                txtSo_Ct0.Text = "00001";


            Voucher.Update_Detail(this, "So_Ct0");
        }
        private void CheckSo_Ct1()
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
                //if (!Common.MsgYes_No("Chứng từ số: " + txtSo_Ct.Text + " Ngày: " + dteNgay_Ct.Text + " đã tồn tại.\n Bạn có muốn tiếp tục không ?"))
                //    e.Cancel = true;
                //TinhSoCt1();
            }
        }
        private bool CheckSo_Ct0()
        {

            if (txtSo_Ct0.Text == string.Empty && txtSo_Seri0.Text == string.Empty)
                return true;
            if ((string)drDmCt["Nh_Ct"] == "1")
                return true;
            string strTableCt = (string)drDmCt["Table_Ct"];
            string strSo_Seri0 = txtSo_Seri0.Text;
            string strSo_Ct0 = txtSo_Ct0.Text;

            string strSQLExec = "SELECT Stt,So_Ct,Ngay_Ct FROM " + strTableCt + " WHERE Stt <> @Stt AND So_Ct0 = @So_Ct0 AND So_Seri0 = @So_Seri0";

            Hashtable ht = new Hashtable();
            ht.Add("SO_SERI0", strSo_Seri0);
            ht.Add("SO_CT0", strSo_Ct0);
            ht.Add("STT", drEditPh["Stt"]);
            DataTable dtCheck = SQLExec.ExecuteReturnDt(strSQLExec, ht, CommandType.Text);

            if (dtCheck.Rows.Count > 0)
            {
                Common.MsgYes_No("Số hóa đơn : " + txtSo_Ct0.Text + " Số Seri: " + txtSo_Seri0.Text + " đã tồn tại. Số chứng từ: " + dtCheck.Rows[0]["So_Ct"].ToString() + ", Ngày ct : " + dtCheck.Rows[0]["Ngay_Ct"].ToString() + "");
                return false;
            }
            return true;
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


        #region  Phần khuyến mãi và chiết khấu tự động private void 
        private void TestDiscount() //ref DataTable dtEditCt
        {
            try
            {
                double dbQty = 0, dbAmt = 0;
                //var result = dtEditCt.AsEnumerable().Where(x => (Boolean)x["Hang_Km"] == false).Select(x => new { Ma_Vt =  x["Ma_Vt"], Qty = x["So_Luong"], Amt = x["Tien_Nt9"] }).ToArray();
                //foreach (DataRow dritem in dtItemSale.Select("Hang_Km = 0"))
                //{
                //    strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                //    dbQty = result.Sum(val => (double)val.Qty);
                //    dbAmt = result.Sum(val => (double)val.Amt);
                //}

                DataTable dtItem = dtEditCt.DefaultView.ToTable(true, "Ma_Vt", "So_Luong", "Tien_Nt9", "Hang_Km");

                var query = from row in dtEditCt.AsEnumerable()
                            group row by row.Field<string>("Ma_Vt") into grp
                            select new
                            {
                                Ma_Vt = grp.Key,
                                Qty = grp.Sum(r => r.Field<decimal>("So_Luong")),
                                Amt = grp.Sum(r => r.Field<decimal>("Tien_Nt9"))
                            };
                //int TotalPrice = dtEditCt.AsEnumerable().Where(row => row.Field<int>("ProductId") > 3).Sum(row => row.Field<int>("Price"));


                foreach (DataRow dr in dtItem.Rows)
                {
                    string strMa_Vt_Disc = dr["Ma_Vt"].ToString();
                    var sum = dtItem.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("SO_LUONG"));
                    //var sum = dtEditCt.AsEnumerable()
                    //         .Where(r => r.Field<string>("Ma_Vt") == strMa_Vt_Disc && r.Field<bool>("Hang_Km") == false)
                    //         .Sum(r => r.Field<double>("So_Luong"));
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void CalcDiscount() //ref DataTable dtEditCt
        {
            // var result0 = dtEditCt.AsEnumerable().Where(x => (Boolean)x["Hang_Km"] == false).Select(x => new { Ma_Vt = x["Ma_Vt"], Qty = x["So_Luong"], Amt = x["Tien_Nt9"] }).ToArray();

            Discount.ClearPromotionAuto(ref dtEditCt, ref dtEditCtDisc);

            string strMa_Vt_Disc = string.Empty;
            string strMa_Vt_Disc_List = string.Empty;
            DataTable dtItemSaleinInvoice = dtEditCt.DefaultView.ToTable(true, "Ma_Vt", "Hang_Km");
            foreach (DataRow dritem in dtItemSaleinInvoice.Select("Hang_Km = 0"))
            {
                strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
            }
            //Lấy các chương trình khuyến mãi / chiết khấu đang chạy
            dtDiscCount = Discount.GetAutoPromotion(Library.StrToDate(dteNgay_Ct.Text), txtMa_Dt.Text, dtEditCt);
            //dtDiscCount = Discount.GetDiscoutProg(Library.StrToDate(dteNgay_Ct.Text), txtMa_Dt.Text, strMa_Vt_Disc_List);

            if (dtDiscCount.Rows.Count == 0)
                return;

            this.dtDiscFreeItem = dtEditCt.Clone();
            string strMa_Vt = string.Empty;
            double dbNgan_Sach, dbSaleAmt, dbQtyAlloc, dbAmtAlloc, dbQtyAllocated, dbAmtAllocated;
            foreach (DataRow drDisc in dtDiscCount.Rows)
            {

                dbNgan_Sach = -1;
                dbSaleAmt = -1;
                dbQtyAlloc = -1;
                dbAmtAlloc = -1;

                string strMa_CtKm = drDisc["Ma_CTKM"].ToString();
                string strMa_Ns = drDisc["Ma_Ngan_Sach"].ToString();
                string strLoai_CtKm = drDisc["Loai_Km"].ToString();
                string strLoai_Ap_KM = drDisc["Loai_Ap_KM"].ToString();
                string strHinh_Thuc_KM = drDisc["Hinh_Thuc_KM"].ToString();
                string strBreakBy = drDisc["BreakBy"].ToString(); // Kiểm tra theo
                bool isEditKm = Convert.ToBoolean(drDisc["AllowEditDisc"]);
                bool isAutoFreeItem = Convert.ToBoolean(drDisc["AllowEditDisc"]);
                dtDiscFreeItemAddNew = dtEditCt.Clone();


                if (!Discount.CheckDtOnProgID(strMa_CtKm, txtMa_Dt.Text))
                    continue;

                dbNgan_Sach = strHinh_Thuc_KM == "IN" ? Convert.ToDouble(drDisc["TSo_Luong"]) : Convert.ToDouble(drDisc["TTien"]);

                if (strMa_Ns != string.Empty)
                {
                    Hashtable htNs = new Hashtable();
                    htNs["MA_NS"] = strMa_Ns;
                    htNs["MA_CBNV"] = txtMa_CbNV.Text;
                    htNs["MA_DVCS"] = Element.sysMa_DvCs;
                    htNs["STT"] = this.strStt;
                    DataTable dtNgan_Sach = SQLExec.ExecuteReturnDt("sp_OM_GetBudgetAlloc", htNs, CommandType.StoredProcedure);
                    if (dtNgan_Sach.Rows.Count > 0)
                    {
                        dbQtyAlloc = Convert.ToDouble(dtNgan_Sach.Rows[0]["QtyAlloc_Avail"]);
                        dbAmtAlloc = Convert.ToDouble(dtNgan_Sach.Rows[0]["AmtAlloc_Avail"]);
                    }
                }



                #region L - Loại khuyến mãi dòng
                if (strLoai_CtKm == "L") // Loại khuyến mãi dòng
                {

                    double dbQtyLine = 0, dbAmtLine = 0, dbAmtDiscLine = 0, dbFactorQtyAmt = 0;
                    string strSttKM_Line = string.Empty;
                    int iDiscTimeLine = 1;
                    strMa_Vt_Disc = string.Empty;
                    strMa_Vt_Disc_List = string.Empty;

                    foreach (DataRow dritem in dtItemSaleinInvoice.Select("Hang_Km = 0"))
                    {
                        strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                        //strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
                        dbQtyLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("SO_LUONG"));
                        dbAmtLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("TIEN_NT9"));

                        //dbQtyLine = Common.SumDCValue(dtEditCt, "So_Luong", "MA_VT = '" + strMa_Vt_Disc + "' AND Hang_Km  <> true");
                        //dbAmtLine = Common.SumDCValue(dtEditCt, "Tien_Nt9", "MA_VT = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");

                        DataTable dtBreakBy = Discount.GetDiscBreak(strMa_CtKm, strMa_Vt_Disc, strBreakBy == "Q" ? dbQtyLine : dbAmtLine);

                        if (dtBreakBy.Rows.Count > 0)
                        {
                            dbAmtDiscLine = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                            strSttKM_Line = dtBreakBy.Rows[0]["Stt"].ToString();
                            iDiscTimeLine = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);
                            dbFactorQtyAmt = dtBreakBy.Columns.Contains("FactorQtyAmt") ? Convert.ToDouble(dtBreakBy.Rows[0]["FactorQtyAmt"]) : -1;
                            if (strHinh_Thuc_KM == "PP") // áp dụng cho Chiết khấu dòng %
                            {

                                Discount.Calc_Chiet_Khau_ForLine(this, dbAmtDiscLine, strMa_Vt_Disc, strMa_CtKm, strSttKM_Line, isEditKm, strMa_Ns, ref dbAmtAlloc);
                                //Discount.Calc_Chiet_Khau_ForLine(this, dbAmtDisc, strMa_Vt_Disc, strMa_CtKm, strSttKM, isEditKm);
                            }
                            else if (strHinh_Thuc_KM == "II") // Chiết khấu tiền 
                            {
                                dbAmtDiscLine *= iDiscTimeLine;
                                double dbPerLine = Math.Round((dbAmtDiscLine / dbAmtLine) * 100, 7);

                                Discount.Calc_Chiet_Khau_ForLine(this, dbPerLine, strMa_Vt_Disc, strMa_CtKm, strSttKM_Line, isEditKm, strMa_Ns, ref dbAmtAlloc);
                                //Discount.Calc_Chiet_Khau_ForLine(this, dbPer, strMa_Vt_Disc, strMa_CtKm, strSttKM, isEditKm);
                            }
                            else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                            {
                                //Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Select("Ma_Vt = '" + strMa_Vt_Disc + "'")[0]);
                                foreach (DataRow drbr in dtBreakBy.Rows)
                                {
                                    dbAmtDiscLine = Convert.ToDouble(drbr["Amt"]);
                                    strSttKM_Line = drbr["Stt"].ToString();
                                    iDiscTimeLine = Convert.ToInt32(drbr["DiscTime"]);

                                    Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM_Line, isEditKm, iDiscTimeLine, dtEditCt.Select("Ma_Vt = '" + strMa_Vt_Disc + "'")[0], strMa_Ns, ref dbQtyAlloc);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region G - Khuyến mãi theo nhóm mặt hàng
                else if (strLoai_CtKm == "G" && strLoai_Ap_KM == "IT") // Loại khuyến mãi nhóm
                {

                    DataTable dtSaleItemForGroup = new DataTable("OM_SalesItem");
                    dtSaleItemForGroup.Columns.Add(new DataColumn("Ma_Vt", typeof(string)));
                    dtSaleItemForGroup.Columns.Add(new DataColumn("Qty", typeof(double)));
                    dtSaleItemForGroup.Columns.Add(new DataColumn("Amt", typeof(double)));

                    double dbQtyTotal = 0, dbAmtTotal = 0, dbQtyLine = 0, dbAmtLine = 0;
                    strMa_Vt_Disc = string.Empty;
                    strMa_Vt_Disc_List = string.Empty;

                    DataTable dtDiscItemSale = Discount.GetSaleGroupItem(strMa_CtKm); // Các mặt hàng mua trong chưng trình KM
                    //DataTable dtItemSale = dtEditCt.DefaultView.ToTable(true, "Ma_Vt"); // Các mặt hàng bán trong đơn hàng

                    if (dtDiscItemSale.Rows.Count == 0)
                        continue;

                    //foreach (DataRow dritem in dtItemSale.Select("Hang_Km = 0"))
                    //{
                    //    strMa_Vt_Disc = dritem["Ma_Vt"].ToString();

                    //    if (dtDiscItemSale.Select("Ma_Vt = '" + strMa_Vt_Disc + "'").Length > 0) // Nếu mặt hàng trong đơn hàng thuộc các mặt hàng mua trong CTKM
                    //    {
                    //       dbQtyLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("SO_LUONG"));
                    //       dbAmtLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("SO_LUONG"));

                    //        dtSaleItemForGroup.Rows.Add(strMa_Vt_Disc, dbQtyLine, dbAmtLine);
                    //    }
                    //}

                    foreach (DataRow dritem in dtItemSaleinInvoice.Select("Hang_Km = 0"))
                    {
                        strMa_Vt_Disc = dritem["Ma_Vt"].ToString();

                        if (dtDiscItemSale.Select("Ma_Vt = '" + strMa_Vt_Disc + "'").Length > 0) // Nếu mặt hàng trong đơn hàng thuộc các mặt hàng mua trong CTKM
                        {
                            strMa_Vt_Disc_List += strMa_Vt_Disc + ",";
                            dbQtyLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("SO_LUONG"));
                            dbAmtLine = (double)dtEditCt.AsEnumerable().Where(row => row.Field<string>("MA_VT") == strMa_Vt_Disc && row.Field<bool>("HANG_KM") == false).Sum(row => row.Field<decimal>("TIEN_NT9"));
                            dbQtyTotal += dbQtyLine;
                            dbAmtTotal += dbAmtLine;
                            dtSaleItemForGroup.Rows.Add(strMa_Vt_Disc, dbQtyLine, dbAmtLine);
                            //dbQtyTotal += Common.SumDCValue(dtEditCt, "So_Luong", "Ma_Vt = '" + strMa_Vt_Disc + "' AND Hang_Km <> true");
                            //dbAmtTotal += Common.SumDCValue(dtEditCt, "Tien_Nt9", "Ma_Vt = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");
                        }
                    }

                    if (dbQtyTotal + dbAmtTotal == 0)
                        continue;

                    DataTable dtBreakBy = Discount.GetDiscBreak(strMa_CtKm, "", strBreakBy == "Q" ? dbQtyTotal : dbAmtTotal);

                    if (dtBreakBy.Rows.Count > 0)
                    {
                        //double dbBreakAmt = strBreakBy == "Q" ? Convert.ToDouble(dtBreakBy.Rows[0]["BreakQty"]):Convert.ToDouble(dtBreakBy.Rows[0]["BreakAmt"]);
                        string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                        double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                        int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi
                        if (strHinh_Thuc_KM == "PP")
                        {
                            Discount.Calc_Chiet_Khau_ForGroup(this, dbAmtDisc, strMa_Vt_Disc_List, strMa_CtKm, strSttKM, isEditKm, strMa_Ns, ref dbAmtAlloc);

                        }
                        else if (strHinh_Thuc_KM == "II")
                        {
                            dbAmtDisc *= iDiscTime;
                            double dbPer = Math.Round((dbAmtDisc / dbAmtTotal) * 100, 7);
                            Discount.Calc_Chiet_Khau_ForGroup(this, dbPer, strMa_Vt_Disc_List, strMa_CtKm, strSttKM, isEditKm, strMa_Ns, ref dbAmtAlloc);
                        }
                        else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                        {
                            foreach (DataRow drbr in dtBreakBy.Rows)
                            {
                                dbAmtDisc = Convert.ToDouble(drbr["Amt"]);
                                strSttKM = drbr["Stt"].ToString();
                                iDiscTime = Convert.ToInt32(drbr["DiscTime"]);
                                //Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Select("Ma_Vt = '" + strMa_Vt_Disc + "'")[0]);
                                Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Select("Ma_Vt = '" + strMa_Vt_Disc + "'")[0], strMa_Ns, ref dbQtyAlloc);
                            }
                        }
                        dtEditCt.AcceptChanges();

                    }

                    Calc_Thue_Vat_All();

                }
                #endregion
                #region B - Khuyến mãi bộ sản phẩm
                else if (strLoai_CtKm == "G" && strLoai_Ap_KM == "CB") // Loại khuyến mãi bộ sản phẩm
                {
                    double dbQty = 0, dbAmt = 0;
                    strMa_Vt_Disc = string.Empty;
                    strMa_Vt_Disc_List = string.Empty;


                    //DataTable dtItemSale = dtEditCt.DefaultView.ToTable(true, "Ma_Vt"); // Các mặt hàng bán trong đơn hàng

                    DataColumn dcSo_Luong = new DataColumn("So_Luong", typeof(string));
                    dcSo_Luong.DefaultValue = 0;
                    dtItemSaleinInvoice.Columns.Add(dcSo_Luong);

                    DataColumn dcTien = new DataColumn("Tien", typeof(double));
                    dcTien.DefaultValue = 0;
                    dtItemSaleinInvoice.Columns.Add(dcTien);

                    foreach (DataRow dritem in dtItemSaleinInvoice.Select("Hang_Km = 0"))
                    {
                        strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                        strMa_Vt_Disc_List += strMa_Vt_Disc + ",";

                        dbQty = Common.SumDCValue(dtEditCt, "So_Luong", "Ma_Vt = '" + strMa_Vt_Disc + "' AND Hang_Km <> true");
                        dbAmt = Common.SumDCValue(dtEditCt, "Tien_Nt9", "Ma_Vt = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");

                        dritem["So_Luong"] = dbQty;
                        dritem["Tien"] = dbAmt;
                        dritem.AcceptChanges();
                    }

                    DataTable dtBreakBy = Discount.GetDiscBreakBundle(strMa_CtKm, dtItemSaleinInvoice);

                    if (dtBreakBy.Rows.Count > 0)
                    {

                        string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                        double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                        int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi
                        if (strHinh_Thuc_KM == "PP")
                        {
                            Discount.Calc_Chiet_Khau_ForGroup(this, dbAmtDisc, strMa_Vt_Disc_List, strMa_CtKm, strSttKM, isEditKm, strMa_Ns, ref dbAmtAlloc);

                        }
                        else if (strHinh_Thuc_KM == "II")
                        {
                            dbAmtDisc *= iDiscTime;
                            double dbPer = Math.Round((dbAmtDisc / dbAmt) * 100, 7);
                            Discount.Calc_Chiet_Khau_ForGroup(this, dbPer, strMa_Vt_Disc_List, strMa_CtKm, strSttKM, isEditKm, strMa_Ns, ref dbAmtAlloc);
                        }
                        else if (strHinh_Thuc_KM == "IN") // Khuyến mãi tặng hàng 
                        {
                            Discount.CalDiscountFreeItem(this, strMa_CtKm, strSttKM, isEditKm, iDiscTime, dtEditCt.Rows[0], strMa_Ns, ref dbQtyAlloc);
                        }
                        dtEditCt.AcceptChanges();

                    }

                    Calc_Thue_Vat_All();
                }
                #endregion

                #region I - Chiết khấu hóa đơn

                else if (strLoai_CtKm == "I") // Loại Khuyến mãi/ CK Invoice
                {

                    double dbTTien = 0;
                    //double dbTien_CkInvoice = 0;
                    double dbQty = 0, dbAmt = 0;
                    strMa_Vt_Disc = string.Empty;
                    strMa_Vt_Disc_List = string.Empty;

                    DataColumn dcSo_Luong = new DataColumn("So_Luong", typeof(string));
                    dcSo_Luong.DefaultValue = 0;
                    dtItemSaleinInvoice.Columns.Add(dcSo_Luong);

                    DataColumn dcTien = new DataColumn("Tien", typeof(double));
                    dcTien.DefaultValue = 0;
                    dtItemSaleinInvoice.Columns.Add(dcTien);

                    foreach (DataRow dritem in dtItemSaleinInvoice.Rows)
                    {
                        strMa_Vt_Disc = dritem["Ma_Vt"].ToString();
                        strMa_Vt_Disc_List += strMa_Vt_Disc + ",";

                        dbQty = Common.SumDCValue(dtEditCt, "So_Luong", "Ma_Vt = '" + strMa_Vt_Disc + "' AND Hang_Km <> true");
                        dbAmt = Common.SumDCValue(dtEditCt, "Tien_Nt9", "Ma_Vt = '" + strMa_Vt_Disc + "'  AND Hang_Km  <> true");

                        dritem["So_Luong"] = dbQty;
                        dritem["Tien"] = dbAmt;
                        dritem.AcceptChanges();
                    }

                    #region Kiểm tra theo số tiền
                    if (strBreakBy == "A") // Kiểm tra theo số tiền
                    {



                        dbTTien = Convert.ToDouble(Common.SumDCValue(dtEditCt, "Tien_Nt9", ""));//+ Convert.ToDouble(drEditCt["Tien4"]);
                        //dbAmtDisc = Math.Round((dbAmtDisc / dbTTien) * 100, 7); //% trên tổng đơn hàng
                        DataTable dtBreakBy = Discount.GetDiscBreakInvoice(strMa_CtKm, dtItemSaleinInvoice, dbTTien);

                        if (dtBreakBy.Rows.Count > 0)
                        {
                            string strSttKM = dtBreakBy.Rows[0]["Stt"].ToString();
                            double dbAmtDisc = Convert.ToDouble(dtBreakBy.Rows[0]["Amt"]);
                            int iDiscTime = Convert.ToInt32(dtBreakBy.Rows[0]["DiscTime"]);  // số xuất khuyến mãi

                            if (strHinh_Thuc_KM == "II")
                            {
                                dbAmtDisc = Math.Round((dbAmtDisc / dbTTien) * 100, 7); //% trên tổng đơn hàng
                            }
                            Discount.Calc_Chiet_Khau_ForInvoice(this, dbAmtDisc, strMa_CtKm, strSttKM, isEditKm, strMa_Ns, ref dbAmtAlloc);
                        }

                    }
                    #endregion
                }

                #endregion


            }


            foreach (DataRow drit in this.dtDiscFreeItem.Rows)
            {
                Voucher.Calc_So_Luong(drit);
                dtEditCt.ImportRow(drit);
            }
            dtDiscFreeItem.Rows.Clear();

            dtEditCt.AcceptChanges();
            bdsEditCt.DataSource = dtEditCt;
            dgvEditCt1.DataSource = bdsEditCt;
            base.ShowSuccessMessage("Đã tính xong khuyến mãi cho đơn hàng!");
        }
        /*
        private bool CheckItemDiscGroup(string strMa_CTKM, string strSttKM, string strDiscItem)
        {
            try
            {
                Hashtable htGroupItem = new Hashtable();
                htGroupItem["MA_CTKM"] = strMa_CTKM;
                htGroupItem["STT"] = strSttKM;
                htGroupItem["MA_VT"] = strDiscItem;
                //DataTable dt = SQLExec.ExecuteReturnDt("SP_OM_CheckItemInGroup", htGroupItem, CommandType.StoredProcedure);
                return Convert.ToBoolean(SQLExec.ExecuteReturnValue("SP_OM_CheckItemInGroup", htGroupItem, CommandType.StoredProcedure));
            }
            catch
            {
                return false;
            }
        }
        private bool CheckItemDisc(string strMa_CTKM, string strSttKM, string strDiscItem)
        {
            try
            {
                Hashtable htGroupItem = new Hashtable();
                htGroupItem["MA_CTKM"] = strMa_CTKM;
                htGroupItem["STT"] = strSttKM;
                htGroupItem["MA_VT"] = strDiscItem;
                return (bool)SQLExec.ExecuteReturnValue("SP_OM_CheckDiscItem", htGroupItem, CommandType.StoredProcedure);
            }
            catch
            {
                return false;
            }
        }*/


        #endregion


        #endregion

        #region Su kien

        #region FormEvent

        void btnImportExcel_Click(object sender, EventArgs e)
        {
            //Voucher.ImportCtExcel(this);
            this.CalcDiscount();
        }

        void btHanTt_Click(object sender, EventArgs e)
        {
            this.HanTt();
        }

        void btChon_HD_Click(object sender, EventArgs e)
        {
            frmChonHD_Filter frm = new frmChonHD_Filter();
            frm.Load(this);

            Voucher.Calc_So_Luong_All(this);
            Voucher.Update_TTien(this);
        }
        void btInherit_Click(object sender, EventArgs e)
        {
            this.InheritVoucher();
        }

        void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string currentDir = Environment.CurrentDirectory;
            System.Diagnostics.Process.Start(currentDir + @"\Help\" + drDmCt["Help_File"]);
        }
        private void BindingCombobox()
        {


            string strSQL = @" sp_OM_GetCombovalue @Key = 'RETURNTYPE'";
            cbxLoai_Tra_Hang.DataSource = SQLExec.ExecuteReturnDt(strSQL, CommandType.Text);
            cbxLoai_Tra_Hang.ValueMember = "ID";
            cbxLoai_Tra_Hang.DisplayMember = "Value";


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
                //this.TinhSoCt1();
            }
        }
        void txtMa_Ct_TextChanged(object sender, EventArgs e)
        {
            this.drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct);
        }

        void txtSo_Ct_Validating(object sender, CancelEventArgs e)
        {
            CheckSo_Ct1();
            //Cap nhat So hoa don            
            //txtSo_Ct0.Text = txtSo_Ct.Text;
        }
        void txtCt_Di_Kem_Validating(object sender, CancelEventArgs e)
        {
            if (!this.Is_CtEdit)
                return;

            string strValue = txtCt_Di_Kem.Text.Trim();
            string Stt_Org = string.Empty;

            bool bRequire = false;

            DataRow drLookup = Lookup.ShowQuickLookup("SO_CT_IN", strValue, bRequire, "Ma_Dt  = '" + txtMa_Dt.Text + "'", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                Stt_Org = string.Empty;
                txtCt_Di_Kem.Text = string.Empty;

                foreach (DataRow dr in dtEditCt.Rows)
                {
                    //DataRow drvt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", dr["Ma_Vt"].ToString());
                    dr["Tk_Co2"] = drDmCt["Tk_Co"];
                }
            }
            else
            {
                txtCt_Di_Kem.Text = drLookup["SO_CT_IN"].ToString();
                Stt_Org = drLookup["Stt"].ToString();
                strStt_Hd = Stt_Org;
                //if(txtMa_CbNV_GH.Text == string.Empty)
                txtMa_CbNV.Text = drLookup["Ma_CbNV"].ToString();
                txtMa_CbNV_GH.Text = drLookup["Ma_CbNV_GH"].ToString();


                string strMsg = "Bạn có muốn lấy thông tin đơn bán hàng không?";
                if (EpointMessage.MsgYes_No(strMsg))
                {
                    DataRow drEditCt = dtEditCt.NewRow();
                    Common.CopyDataRow(dtEditCt.Rows[0], drEditCt);

                    dtEditCt.Rows.Clear();

                    DataTable dtInvoice = DataTool.SQLGetDataTable("ARBAN", "", "Stt = '" + Stt_Org + "'", "Stt0");

                    foreach (DataRow drViewCt in dtInvoice.Rows)
                    {
                        DataRow drEditCtNew = dtEditCt.NewRow();
                        Common.CopyDataRow(drEditCt, drEditCtNew);
                        Common.CopyDataRow(drViewCt, drEditCtNew);


                        drEditCtNew["Stt"] = strStt;
                        drEditCtNew["Tk_No"] = drViewCt["Tk_Co"];
                        drEditCtNew["Tk_Co"] = drViewCt["Tk_No"];
                        drEditCtNew["Tk_No2"] = drViewCt["Tk_Co2"];
                        drEditCtNew["Tk_Co2"] = drViewCt["Tk_No2"];
                        //Stt_Org
                        if (drEditCtNew.Table.Columns.Contains("Stt_Org"))
                            drEditCtNew["Stt_Org"] = drViewCt["Stt"];


                        //if (drEditCtNew["Ma_Ctkm"].ToString() != string.Empty && Convert.ToDouble(drEditCtNew["Tien4"]) > 0)
                        //{
                        //    drEditCtNew["Ma_Ctkm_M"] = drEditCtNew["Ma_Ctkm"];
                        //    drEditCtNew["Ma_Ctkm"] = "";

                        //    drEditCtNew["Tien_M4"] = drEditCtNew["Tien4"];
                        //    drEditCtNew["Tien_Ck"] = 0;
                        //}

                        dtEditCt.Rows.Add(drEditCtNew);
                    }

                    dtEditCt.AcceptChanges();


                }
                else
                {
                    //if (dtEditCt.Columns.Contains("Stt_Org"))
                    //foreach (DataRow dr in dtEditCt.Rows)
                    //{
                    //    dr["Stt_Org"] = Stt_Org;
                    //}
                }




            }

            if (drEditPh.Table.Columns.Contains("Stt_Org"))
            {
                drEditPh["Stt_Org"] = Stt_Org;
                drEditPh.AcceptChanges();
                Voucher.Update_Detail(this, "Stt_Org,So_Ct");
                Voucher.Update_TTien(this);
            }


            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }

        }
        void dteNgay_Ct_Validating(object sender, CancelEventArgs e)
        {
            //Quyen so
            //string strSQL = "SELECT Quyen_So FROM LIQUYENSO WHERE Month(Ngay_Begin) <= Month('" + dteNgay_Ct.Text + "') AND Month(Ngay_End) >= Month('"
            //                        + dteNgay_Ct.Text + "') AND Year(Ngay_End) = Year ('" + dteNgay_Ct.Text + "')";
            //DataTable dtQuyen_So = SQLExec.ExecuteReturnDt(strSQL);


            this.Ma_Tte_Valid();
            Common.GatherMemvar(this, ref drEditPh);


            //TinhSoCt1();
            //Cap nhat Ngay hoa don            
            //dteNgay_Ct0.Text = dteNgay_Ct.Text;
        }
        void txtMa_Tte_Validating(object sender, CancelEventArgs e)
        {
            this.Ma_Tte_Valid();
        }
        void numTy_Gia_Leave(object sender, EventArgs e)
        {
            this.Ma_Tte_Valid();
        }

        private void txtMa_Dt_Validating(object sender, CancelEventArgs e)
        {
            //    if (txtMa_Dt.AutoFilter != null)
            //    {
            //        txtMa_Dt.AutoFilter.Visible = false;
            //        txtMa_Dt.AutoFilter = null;
            //    }
            if (!txtMa_Dt.bTextChange)
                return;


            string strValue = txtMa_Dt.Text.Trim();
            bool bRequire = true;

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
                //txtTen_Dt.Text = drLookup["Ten_Dt"].ToString();
                this.drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", txtMa_Dt.Text);

                if (txtMa_Dt.Text != (string)drEditPh["Ma_Dt"])
                {
                    numSoDu131.Value = Voucher.GetDuCuoiDt(drEditPh, "1311", txtMa_Dt.Text);

                    txtTen_Dt.Text = drLookup["Ten_Dt"].ToString();
                    txtOng_Ba.Text = drLookup["Ong_Ba"].ToString() == string.Empty ? drLookup["Ten_Dt"].ToString() : drLookup["Ong_Ba"].ToString();

                    if (drLookup["Dia_Chi"].ToString() != string.Empty)
                        txtDia_Chi.Text = drLookup["Dia_Chi"].ToString();
                    else
                        txtDia_Chi.Text = "";

                }

                if (drLookup.Table.Columns.Contains("Ma_Cbnv_Bh") && drLookup["Ma_Cbnv_Bh"].ToString() != string.Empty && txtMa_Dt.bTextChange && drEditPh["So_Ct_Lap"].ToString() == string.Empty)
                {
                    txtMa_CbNV.Text = drLookup["Ma_Cbnv_Bh"].ToString();
                    txtTen_NVBH.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNV", "Ten_CbNv", txtMa_CbNV.Text.Trim());
                }
                if (drLookup.Table.Columns.Contains("Ma_Cbnv_Gh") && drLookup["Ma_Cbnv_Gh"].ToString() != string.Empty && txtMa_Dt.bTextChange && drEditPh["So_Ct_Lap"].ToString() == string.Empty)
                {
                    txtMa_CbNV_GH.Text = drLookup["Ma_Cbnv_Gh"].ToString();
                    txtTen_NVGH.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNV", "Ten_CbNv", txtMa_CbNV_GH.Text.Trim());
                }

                if (drLookup.Table.Columns.Contains("Han_Tt"))
                {
                    numHan_Tt.Value = Convert.ToDouble(drLookup["Han_Tt"]);
                }
            }

            //dicName[txtTen_Dt.Name] = txtTen_Dt.Text;

            Voucher.Update_Detail(this, "Ma_Dt");

            foreach (DataRow drCurrent in this.dtEditCt.Rows)
            {
                //if (strMa_Ct == "TL")//TL
                if ((string)drDmCt["Nh_Ct"] == "1")
                {
                    // Theo Doi Tuong
                    if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Hbtl"))
                        drCurrent["Tk_No2"] = (string)drDmDt["Tk_Hbtl"] != String.Empty ? (string)drDmDt["Tk_Hbtl"] : drCurrent["Tk_No2"];

                    if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                        drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_Co2"];
                }
                else//HD
                {

                    // Theo Doi Tuong
                    if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                        drCurrent["Tk_No2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_No2"];

                    if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dthu"))
                        drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dthu"] != String.Empty ? (string)drDmDt["Tk_Dthu"] : drCurrent["Tk_Co2"];


                }
            }
            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_Thue_Enter(object sender, EventArgs e)
        {
            this.ucNotice.Text = dicName.GetValue("Ten_Thue");
        }
        void txtMa_Thue_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Thue.Text;
            bool bRequire = false;

            string strMa_Thue_Old = drEditPh["Ma_Thue"] == DBNull.Value ? string.Empty : (string)drEditPh["Ma_Thue"];

            //frmThue frmLookup = new frmThue();
            DataRow drLookup = Lookup.ShowLookup("Ma_Thue", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup != null)
            {
                txtMa_Thue.Text = (string)drLookup["Ma_Thue"];
                dicName.SetValue("Ten_Thue", drLookup["Ten_Thue"].ToString());

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
                frmDoiTuong frmLookup = new frmDoiTuong();
                DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONG", "Ma_So_Thue", strValue, bRequire, "");

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
                            frmDoiTuong frmLookup = new frmDoiTuong();
                            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONG", "Ma_So_Thue", strValue, bRequire, "");

                            if (bRequire && drLookup == null)
                                e.Cancel = true;

                            if (drLookup == null)
                            {
                                txtMa_So_Thue.Text = string.Empty;
                                txtTen_DtGtgt.Text = string.Empty;
                            }
                            else
                            {
                                txtMa_So_Thue.Text = drLookup["Ma_So_Thue"].ToString();
                                txtTen_DtGtgt.Text = drLookup["Ten_Dt"].ToString();
                            }
                        }
                        else
                        {
                            if (Common.MsgYes_No("Bạn có chắc chắn thêm mới Đối tượng - Mã số thuế?"))
                            {
                                DataRow drNew = dtLookup.NewRow();
                                drNew["Ma_Dt"] = drNew["Ma_So_Thue"] = strValue;
                                drNew["Ma_Nh_Dt"] = "MA_SO_THUE";

                                frmDoiTuong_Edit frmEdit = new frmDoiTuong_Edit();
                                frmEdit.Load(enuEdit.New, drNew);

                                if (frmEdit.isAccept)
                                {
                                    txtMa_So_Thue.Text = (string)drNew["Ma_So_Thue"];
                                    txtTen_DtGtgt.Text = (string)drNew["Ten_Dt"];
                                }
                            }
                        }
                    }
                }
            }
            //this.SelectNextControl(this.ActiveControl, true, true, true, true);
        }

        void txtTk_No3_Enter(object sender, EventArgs e)
        {
            if (bdsEditCt.Position < 0)
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            //this.ucNotice.Text = dicName.GetValue("Ten_Tk_No3");
            if ((string)drCurrent["Ma_Thue"] != string.Empty)
                this.ucNotice.Text = Voucher.GetDuCuoi(drCurrent, txtTk_No3.Text);
        }
        void txtTk_No3_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_No3.Text.Trim();
            bool bRequire = txtMa_Thue.Text.Trim() == string.Empty ? false : true;

            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_No3.Text = string.Empty;
                ucNotice.Text = string.Empty;
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

        void txtTk_No2_Enter(object sender, EventArgs e)
        {
            if (bdsEditCt.Position < 0)
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
        }

        void txtTk_Co3_Enter(object sender, EventArgs e)
        {
            if (bdsEditCt.Position < 0)
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;

            //ucNotice.Text = dicName.GetValue("Ten_Tk_Co3");
            if ((string)drCurrent["Ma_Thue"] != string.Empty)
                this.ucNotice.Text = Voucher.GetDuCuoi(drCurrent, txtTk_Co3.Text);
        }
        void txtTk_Co3_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtTk_Co3.Text.Trim();
            bool bRequire = txtMa_Thue.Text.Trim() == string.Empty ? false : true;

            DataRow drLookup = Lookup.ShowLookup("Tk", strValue, bRequire, "", "Tk_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtTk_Co3.Text = string.Empty;
                ucNotice.Text = string.Empty;
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
        void txtMa_CbNV_GH_Validating(object sender, CancelEventArgs e)
        {
            string strValue = string.Empty;

            strValue = txtMa_CbNV_GH.Text;

            bool bRequire = false;

            if (strMa_Ct == "INT")
                bRequire = true;

            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return;

            if (drLookup == null)
            {
                txtMa_CbNV_GH.Text = string.Empty;
                txtTen_NVGH.Text = string.Empty;
            }
            else
            {
                txtMa_CbNV_GH.Text = drLookup["Ma_CbNv"].ToString();
                txtTen_NVGH.Text = drLookup["Ten_CbNv"].ToString();
            }

            return;
        }

        void txtMa_CbNV_BH_Validating(object sender, CancelEventArgs e)
        {
            string strValue = string.Empty;

            strValue = txtMa_CbNV.Text;

            bool bRequire = true;


            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                return;

            if (drLookup == null)
            {
                txtMa_CbNV.Text = string.Empty;
                txtTen_NVBH.Text = string.Empty;
            }
            else
            {
                txtMa_CbNV.Text = drLookup["Ma_CbNv"].ToString();
                txtTen_NVBH.Text = drLookup["Ten_CbNv"].ToString();
            }

            return;
        }
        void numChiet_Khau_Validating(object sender, CancelEventArgs e)
        {
            double dbTTien = Convert.ToDouble(Common.SumDCValue(dtEditCt, "Tien_Nt9", ""));
            double dbTTienCk = Convert.ToDouble(Common.SumDCValue(dtEditCt, "Tien_M4", "")); ;

            double dbTTien_Ck_M = dbTTien * (numCK_M.Value / 100);
            if (this.bCalcInvoiceDiscount)
            {
                dbTTien_Ck_M = dbTTien_Ck_M - dbTTienCk;
            }
            if (this.iDiscountRound > 0)
            {
                dbTTien_Ck_M = Math.Floor(dbTTien_Ck_M / iDiscountRound) * iDiscountRound;
            }
            numTTien4_HD.Value = dbTTien_Ck_M;
            Discount.Calc_Chiet_Khau_ForManual(this, numTTien4_HD.Value);
            //if (!chkIs_Sua_ChietKhau.Checked)
            //{
            //    Voucher.Update_Detail(this, "Chiet_Khau");
            //    Voucher.Calc_Chiet_Khau_All(this);
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
            }

            dicName.SetValue(txtTen_Hd.Name, txtTen_Hd.Text);

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

                case Keys.F4:// Show/ Hiden discount manual Columns
                    if (Parameters.GetParaValue("DMS_DISCMANUAL") != null && Parameters.GetParaValue("DMS_DISCMANUAL").ToString() != string.Empty)
                    {
                        foreach (DataRow dr in dtEditCt.Rows)
                        {
                            if (Convert.ToDouble(dr["Tien_Ck"]) != 0)
                            {

                            }
                        }
                        //string strDisc_Col = Parameters.GetParaValue("DMS_DISCMANUAL_COL").ToString();
                        foreach (DataGridViewColumn dgvc in dgvEditCt1.Columns)
                        {
                            if (Common.Inlist(dgvc.Name, this.strDMS_DISCMANUAL_COL))//dgvc.Name == "TIEN_M4" || dgvc.Name == "MA_CTKM_M" || dgvc.Name == "CHIET_KHAU"
                            {
                                dgvc.Visible = !dgvc.Visible;
                            }
                        }
                    }
                    dgvEditCt1.ResizeGridView();
                    break;
                case Keys.F6:

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

                case Keys.F7:
                    string strMsg = "Bạn có muốn xóa tính khuyến mã tự động?";
                    if (EpointMessage.MsgYes_No(strMsg, "Y"))
                    {
                        //this.InheritVoucher();
                        Discount.ClearPromotionAuto(ref dtEditCt, ref dtEditCtDisc);
                        Voucher.Update_TTien(this);
                    }
                    break;
                case Keys.F10:
                    //this.InheritVoucher();
                    this.CalcDiscount();
                    break;

                case Keys.F11:
                    this.HanTt();
                    break;
                    //case Keys.F9:
                    //    this.CalcDiscount();
                    //    break;
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
        void numTTien4_Validated(object sender, EventArgs e)
        {
            TTien_Valid();
        }
        void numTTien_CK_M4_Validated(object sender, EventArgs e)
        {
            //numTTien4.Value = numTTien4_HD.Value;
            Discount.Calc_Chiet_Khau_ForManual(this, numTTien_CK_M4.Value);
            /*
          
           double dbTTien = Convert.ToDouble(Common.SumDCValue(dtEditCt, "Tien_Nt9", ""));//+ Convert.ToDouble(drEditCt["Tien4"]);
           double dbAmtDisc = Math.Round((numTTien4_HD.Value / dbTTien) * 100, 7); //% trên tổng đơn hàng
          
            if (dtEditCt.Columns.Contains("Tien_Ck_M4"))
                Discount.Calc_Chiet_Khau_ForInvoice(this, dbAmtDisc,true);
            else
                Discount.Calc_Chiet_Khau_ForInvoice(this, dbAmtDisc);

           Voucher.Update_TTien(this);
            //TTien_Valid();*/
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

            if (Common.Inlist(strColumnName, "TIEN_NT4, TIEN4 , CHIET_KHAU, HANG_KM"))
            {
                if ((!(bool)drCurrent["Is_EditDisc"]) || (bool)drCurrent["Hang_Km"])
                {
                    dgvCell.ReadOnly = true;
                }

            }
            else if (Common.Inlist(strColumnName, "TIEN_M4"))
            {
                if ((bool)drCurrent["Hang_Km"])
                {
                    dgvCell.ReadOnly = true;
                }

            }

            else if (strColumnName == "MA_LO")
            {
                DataRow drvt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", (string)drCurrent["Ma_Vt"]);
                if (!(bool)drvt["LotSerial"])
                {
                    drCurrent["MA_LO"] = string.Empty;
                    dgvCell.ReadOnly = true;
                }
            }
            else if (strColumnName == "MA_CTKM_M")
            {
                if ((bool)drCurrent["HANG_KM"] && (string)drCurrent["MA_SO"] != string.Empty)
                {
                    dgvCell.ReadOnly = true;
                }

                if (Convert.ToDouble(drCurrent["TIEN_M4"]) == 0 && !(bool)drCurrent["HANG_KM"])
                    dgvCell.ReadOnly = true;
                else
                    dgvCell.ReadOnly = false;

            }
            //else
            //    dgvCell.ReadOnly = false;

            //if (Common.Inlist(strColumnName, "MA_VT,GIA_NT, GIA, TIEN_NT, TIEN , TIEN_NT4, TIEN4 , HANG_KM ,SO_LUONG9,GIA_NT9, TIEN_NT9, MA_VT ,TEN_VT,MA_KHO, HANG_KM, MA_CBNV"))
            //{
            //    if ((bool)drCurrent["HANG_KM"])
            //    {
            //        if (!(bool)drCurrent["Is_EditDisc"])
            //        {
            //            dgvCell.ReadOnly = true;
            //        }
            //        else
            //            dgvCell.ReadOnly = false;
            //    }
            //}

            if (Common.Inlist(strColumnName, "GIA_NT9, TIEN_NT9"))
            {
                if ((bool)drCurrent["HANG_KM"])
                {
                    drCurrent["GIA_NT9"] = 0;
                    drCurrent["TIEN_NT9"] = 0;
                    drCurrent["GIA2"] = 0;
                    drCurrent["TIEN2"] = 0;
                    drCurrent["GIA_NT2"] = 0;
                    drCurrent["TIEN_NT2"] = 0;
                    drCurrent["TIEN4"] = 0;
                    drCurrent["TIEN_M4"] = 0;
                    drCurrent["CHIET_KHAU"] = 0;
                    dgvCell.ReadOnly = true;
                }
            }
            if (Common.Inlist(strColumnName, "GIA_NT, GIA, TIEN_NT, TIEN"))
            {
                //if ((bool)dgvEditCt1.CurrentRow.Cells["AUTO_COST"].Value == true)
                //{
                //    dgvCell.ReadOnly = true;
                //    //dgvCell.Value = 0;
                //}
                //else
                //    dgvCell.ReadOnly = false;
            }

            if (Common.Inlist(strColumnName, "MA_VT,MA_KHO"))
            {
                if ((string)drCurrent["Ma_Vt"] != string.Empty)
                    ucNotice.Text = Voucher.GetTonCuoi(drCurrent) + Voucher.GetGiaBanLast(drCurrent);

                dicName.SetValue("TON_CUOI", ucNotice.Text);
            }
            else if (Common.Inlist(strColumnName, "TEN_VT,DVT"))
            {
                ucNotice.Text = dicName.GetValue("TON_CUOI") + Voucher.GetGiaBanLast(drCurrent);
            }
            else if (Common.Inlist(strColumnName, "TK_NO, TK_CO, TK_NO3, TK_CO3,TK_NO2, TK_CO2, TK_NO4, TK_CO4"))
            {
                if ((string)drCurrent[strColumnName] != string.Empty)
                    this.ucNotice.SetText(string.Empty, Voucher.GetDuCuoi(drCurrent, (string)drCurrent[strColumnName]));
            }
            else if (dgvCell.Tag != null)
                this.ucNotice.SetText(dgvCell.Value.ToString(), (string)dgvCell.Tag);
        }

        void dgvEditCt_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {//Cai dat Lookup

            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            //Xu ly phim Enter
            if (dgvEditCt.kLastKey == Keys.Enter)
            {
                dgvEditCt.kLastKey = Keys.None;

                if (this.CellKeyEnter())
                    e.Cancel = true;
            }

            //Xu ly Lookup
            if (this.ActiveControl == null)
                return;

            //e.Cancel = true;

            if (this.ActiveControl == dgvEditCt || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                DataGridViewCell dgvCell = dgvEditCt.CurrentCell;
                string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

                bool bLookup = true;

                if (Common.Inlist(strColumnName, "TK_NO,TK_CO,TK_NO2,TK_CO2"))
                    bLookup = dgvLookupTk(ref dgvCell, strColumnName);

                else if (strColumnName == "MA_DT")
                    bLookup = dgvLookupMa_Dt(ref dgvCell);

                else if (strColumnName == "MA_THUE")
                    bLookup = dgvLookupMa_Thue(ref dgvCell);

                else if (strColumnName == "MA_VT")
                    bLookup = dgvLookupMa_Vt(ref dgvCell);

                else if (strColumnName == "MA_KHO")
                    bLookup = dgvLookupMa_Kho(ref dgvCell);

                else if (strColumnName == "MA_CBNV")
                    bLookup = dgvLookupMa_CbNv(ref dgvCell);

                else if (strColumnName == "MA_LO")
                    bLookup = dgvLookupMa_Lo(ref dgvCell);
                else if (strColumnName == "MA_CTKM_M")
                    bLookup = dgvLookupMa_Ctkm(ref dgvCell);
                ////drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                //DataRow drvt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", (string)drCurrent["Ma_Vt"]);
                //if ((bool)drvt["LotSerial"])
                //{
                //    bLookup = dgvLookupMa_Lo(ref dgvCell);
                //    drCurrent["Han_Sd"] = dgvCell.Tag.ToString();
                //}
                //else
                //{
                //    bLookup = false;
                //    //dgvEditCt.CancelEdit();
                //}


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

            //Nếu Tk_No2 trong Hoa don ban hang là tien mat thì ẩn HanTT
            if (strMa_Ct == "HD")
            {
                DataRow drEditCt = dtEditCt.Rows[0];
                string str = (string)drEditCt["Tk_No2"].ToString().Substring(0, 3);
                if (str == "111")
                {
                    numHan_Tt.Enabled = false;

                }
                else
                {
                    numHan_Tt.Enabled = true;

                }
            }

            //Cập nhật giá từ chính sách giá
            if (Common.Inlist(strColumnName, "SO_LUONG9"))
            {
                Update_Gia_Vt(drCurrent);
                Voucher.Update_TTien(this);

                if (!(bool)drCurrent["Auto_Cost"])
                    Voucher.Calc_Tien_Von(drCurrent);
            }
            //if (Common.Inlist(strColumnName, "DVT"))
            //{
            //    drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            //    string strMa_Vt = (string)drCurrent["Ma_Vt"];
            //    string strDvt_Old = (string)drCurrent["Dvt"];
            //    string strDvt_Chuan = string.Empty;

            //    DataRow drDmVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", strMa_Vt);
            //    strDvt_Chuan = (string)drDmVt["Dvt"];

            //    string inputMask = (string)drDmVt["Dvt"];

            //    for (int i = 1; i <= 3; i++)
            //        inputMask += (string)drDmVt["Dvt" + i] == string.Empty ? string.Empty : "," + (string)drDmVt["Dvt" + i];

            //    if (inputMask != string.Empty)
            //        inputMask += "," + inputMask;
            //    if (inputMask == null || inputMask == string.Empty)
            //        return;

            //    string[] strArrInputMask = inputMask.Split(',');
            //    for (int i = 0; i <= strArrInputMask.Length - 1; i++)
            //        if (strArrInputMask[i] == strDvt_Old)
            //        {
            //            drCurrent["Dvt"] = strArrInputMask[i + 1];
            //            break;
            //        }

            //    if ((string)drCurrent["Dvt"] == strDvt_Chuan)
            //        drCurrent["He_So9"] = 1;
            //    else
            //        for (int i = 1; i <= 3; i++)
            //            if ((string)drDmVt["Dvt" + i] == (string)drCurrent["Dvt"])
            //                drCurrent["He_So9"] = drDmVt["He_So" + i];

            //    Voucher.Calc_So_Luong(drCurrent);
            //}
            if (Common.Inlist(strColumnName, "DVT"))
            {
                drCurrent["GIA_NT9"] = 0;
                drCurrent["TIEN_NT9"] = 0;
                Update_Gia_Vt(drCurrent);
                Voucher.Calc_So_Luong(drCurrent);
                Voucher.Calc_Tien(drCurrent);
                Voucher.Update_TTien(this);
                Voucher.Adjust_TThue_Vat(this, true);


            }
            if (Common.Inlist(strColumnName, "SO_LUONG9,GIA_NT9,TIEN_NT9,TIEN2"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    Voucher.Calc_So_Luong(drCurrent);
                    Voucher.Calc_Tien(drCurrent);
                    Voucher.Update_TTien(this);
                    Voucher.Adjust_TThue_Vat(this, true);

                }
                else if (Common.Inlist(strColumnName, "GIA_NT9"))
                {
                    double dbGia_Nt9 = 0;
                    if (double.TryParse(drCurrent["GIA_NT9"].ToString(), out dbGia_Nt9))
                    {

                        Voucher.Calc_So_Luong(drCurrent);
                        Voucher.Calc_Tien(drCurrent);
                        Voucher.Update_TTien(this);
                        Voucher.Adjust_TThue_Vat(this, true);

                    }
                }


                //Kiểm tra tồn kho
                if ((string)drCurrent["Ma_Vt"] != string.Empty && (string)drCurrent["Ma_Kho"] != string.Empty &&
                    (string)drDmCt["Nh_Ct"] == "2" && strColumnName == "SO_LUONG9")
                {
                    double dbSo_Luong = Convert.ToDouble(drCurrent["So_Luong"]);
                    double dbTon_Cuoi = 0;
                    Voucher.GetTonCuoi0(drCurrent, ref dbTon_Cuoi);

                    if (dbSo_Luong > dbTon_Cuoi)
                    {
                        string strOptionMsg = string.Empty;

                        strOptionMsg = Parameters.GetParaValue("INCHECKTON") == null ? "1" : Convert.ToString(Parameters.GetParaValue("INCHECKTON"));

                        if (strOptionMsg == "1")
                        {
                            string strMsg = string.Empty;

                            if (Element.sysLanguage == enuLanguageType.Vietnamese)
                                strMsg = "Số lượng xuất: " + dbSo_Luong.ToString("N2") + " > số lượng tồn: " + dbTon_Cuoi.ToString("N2");
                            else
                                strMsg = "Out quantity: " + dbSo_Luong.ToString("N2") + " > closing inventory quantity: " + dbTon_Cuoi.ToString("N2");

                            Common.MsgCancel(strMsg);
                        }
                    }
                }

                //Tính giá vốn tức thời cho người dùng tham khảo
                if (enuNew_Edit != enuEdit.Edit && Collection.Parameters.ContainsKey("AUTO_GIA_BQTT") && Collection.Parameters["AUTO_GIA_BQTT"].ToString() == "1")
                {
                    if (drCurrent["Ma_Vt"].ToString() != "" && drCurrent["Ma_Kho"].ToString() != "" && Convert.ToDouble(drCurrent["So_Luong"]) != 0)
                    {

                        double dbGia_TT = 0;
                        string StrDescr = Voucher.GetGiaVonBQTT(drCurrent, ref dbGia_TT);

                        if (txtMa_Tte.Text == "VND")
                        {
                            drCurrent["Gia"] = dbGia_TT;
                            drCurrent["Tien"] = Convert.ToDouble(drCurrent["So_Luong"]) * dbGia_TT;
                        }
                        else
                            drCurrent["Gia_Nt"] = Math.Round(dbGia_TT / numTy_Gia.Value, 4);
                    }

                }


                if (Collection.Parameters.ContainsKey("AUTO_GIAVON_BQTT") && Collection.Parameters["AUTO_GIAVON_BQTT"].ToString() == "1")
                {
                    if (drCurrent["Ma_Vt"].ToString() != "" && drCurrent["Ma_Kho"].ToString() != "" && Convert.ToDouble(drCurrent["So_Luong"]) != 0)
                    {
                        drCurrent["Tien"] = drCurrent["Tien_Nt"] = drCurrent["Tien_Nt9"];
                        drCurrent["Gia"] = drCurrent["Gia_Nt"] = Convert.ToDouble(drCurrent["Tien"]) / Convert.ToDouble(drCurrent["So_Luong"]);
                        //drCurrent["Auto_Cost"] = 0;
                    }

                }

            }

            else if (Common.Inlist(strColumnName, "TIEN2"))
            {
                Voucher.Calc_Tien(drCurrent);
                Voucher.Update_TTien(this);
            }
            else if (Common.Inlist(strColumnName, "TIEN4"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    double dbTien_Nt9 = drCurrent["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_Nt9"]);
                    double dbTien4 = Convert.ToDouble(drCurrent["Tien4"]);
                    drCurrent["Tien_Nt4"] = drCurrent["Tien4"];

                    if (dbTien4 > dbTien_Nt9)
                    {
                        drCurrent["Tien4"] = drCurrent["Tien4", DataRowVersion.Original];
                        drCurrent["Tien_Nt4"] = drCurrent["Tien4"];
                    }
                    else
                    {
                        double dbTien2 = dbTien_Nt9 - dbTien4;
                        drCurrent["Tien3"] = 0;
                        drCurrent["Tien_Nt3"] = 0;

                        drCurrent["Tien2"] = drCurrent["Tien_Nt2"] = dbTien2;
                    }
                    Calc_Thue_Vat(drCurrent);
                    Voucher.Update_TTien(this);
                }
            }
            else if (Common.Inlist(strColumnName, "CHIET_KHAU"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    double dbTien_Nt9 = drCurrent["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_Nt9"]);
                    double dbChiet_Khau = Convert.ToDouble(drCurrent["Chiet_Khau"]);

                    if (dbChiet_Khau > 100 || dbChiet_Khau < 0)
                    {
                        drCurrent["Chiet_Khau"] = drCurrent["Chiet_Khau", DataRowVersion.Original];
                        //drCurrent["Tien4"] = 0;
                    }
                    else
                    {
                        drCurrent["Tien4"] = 0;
                        double dbTien4 = Convert.ToDouble(drCurrent["Tien4"]);

                        double dbTien2 = dbTien_Nt9 - dbTien4;
                        drCurrent["Tien3"] = 0;
                        drCurrent["Tien_Nt3"] = 0;
                        drCurrent["Tien2"] = drCurrent["Tien_Nt2"] = dbTien2;
                        Voucher.Calc_Chiet_Khau(drCurrent);
                        Calc_Thue_Vat(drCurrent);
                        Voucher.Update_TTien(this);
                    }

                    //dbTien4 = Math.Round((dbTien_Nt9 * dbChiet_Khau) / 100, 0);
                    //drCurrent["Tien4"] = dbTien4;


                }
            }
            else if (Common.Inlist(strColumnName, "TIEN_M4"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    double dbTien_M4_Old = Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]);
                    double dbTien4_Old = Convert.ToDouble(drCurrent["Tien4", DataRowVersion.Original]);
                    double dbTien_Nt9 = drCurrent["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_Nt9"]);
                    double dbTien4;//= Convert.ToDouble(drCurrent["Tien4"]) + Convert.ToDouble(drCurrent["Tien_M4"]) - dbTien_M4_Old;

                    if (dbTien4_Old > 0)
                    {
                        dbTien4 = dbTien4_Old + Convert.ToDouble(drCurrent["Tien_M4"]) - dbTien_M4_Old;
                    }
                    else
                    {
                        dbTien4 = Convert.ToDouble(drCurrent["Tien_M4"]);
                    }

                    drCurrent["Tien_Nt4"] = drCurrent["Tien4"] = dbTien4;

                    if (dbTien4 > dbTien_Nt9)
                    {
                        drCurrent["Tien_M4"] = dbTien_M4_Old;
                        drCurrent["Tien4"] = drCurrent["Tien_Nt4"] = dbTien4_Old;
                        //drCurrent["Tien_Nt4"] = drCurrent["Tien4"];
                    }
                    else
                    {
                        double dbTien2 = dbTien_Nt9 - dbTien4;
                        drCurrent["Tien3"] = 0;
                        drCurrent["Tien_Nt3"] = 0;

                        drCurrent["Tien2"] = drCurrent["Tien_Nt2"] = dbTien2;

                        if (dbTien_M4_Old == 0)
                            drCurrent["Ma_CTKM_M"] = "";
                    }


                    Calc_Thue_Vat(drCurrent);
                    Voucher.Update_TTien(this);
                }
            }
            else if (Common.Inlist(strColumnName, "CHIET_KHAU_M"))
            {
                if (drCurrent.RowState == DataRowState.Added || Convert.ToDouble(drCurrent[strColumnName]) != Convert.ToDouble(drCurrent[strColumnName, DataRowVersion.Original]))
                {
                    double dbTien_Nt9 = drCurrent["Tien_Nt9"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_Nt9"]);
                    double dbTien4 = drCurrent["Tien4"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien4"]);
                    double dbChiet_Khau_M = Convert.ToDouble(drCurrent["CHIET_KHAU_M"]);

                    if (dbChiet_Khau_M > 100 || dbChiet_Khau_M < 0)
                    {
                        drCurrent["CHIET_KHAU_M"] = drCurrent["CHIET_KHAU_M", DataRowVersion.Original];
                    }
                    else
                    {

                        drCurrent["Tien4"] = 0;
                        //dbTien4 = Convert.ToDouble(drCurrent["Tien4"]);
                        double dbTien4_Calc = Math.Round(dbTien_Nt9 * dbChiet_Khau_M / 100, MidpointRounding.AwayFromZero);
                        dbTien4 += dbTien4_Calc;

                        double dbTien2 = dbTien_Nt9 - dbTien4;
                        drCurrent["Tien3"] = 0;
                        drCurrent["Tien_Nt3"] = 0;
                        drCurrent["Tien2"] = drCurrent["Tien_Nt2"] = dbTien2;


                        Voucher.Calc_Chiet_Khau(drCurrent);
                        Calc_Thue_Vat(drCurrent);
                        Voucher.Update_TTien(this);
                    }

                    //dbTien4 = Math.Round((dbTien_Nt9 * dbChiet_Khau) / 100, 0);
                    //drCurrent["Tien4"] = dbTien4;


                }
            }

            if (Common.Inlist(strColumnName, "SO_LUONG9,GIA_NT9,TIEN_NT9,TIEN2"))
                drCurrent.AcceptChanges();

            bdsEditCt.EndEdit();//Cap nhat lai DataSource                   
        }

        void dgvEditCt1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvVoucher dgvEditCt = (dgvVoucher)sender;

            if (dgvEditCt.CurrentCell == null)
                return;

            if (this.ActiveControl != dgvEditCt)
                return;

            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (strColumnName == "MA_VT")
                this.bMa_Vt_Changed = true;

        }

        void dgvEditCt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
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
            //else
            //{

            //}

        }
        void dgvEdit_Ct1_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                DataTable dtStruck = GetDataFromClipboard(dataInClipboard);

                if (dtStruck != null)
                {
                    int r = dgvEditCt1.SelectedCells[0].RowIndex;
                    int c = dgvEditCt1.SelectedCells[0].ColumnIndex;

                    //foreach (DataRow dr in dtStruck.Rows)
                    //{
                    //    for (int iCol = 0; iCol < dtStruck.Columns.Count; iCol++)
                    //    { 

                    //    }
                    //}

                    //for (int iRow = 0; iRow < dtStruck.Rows.Count; iRow++)
                    //{
                    //    //cycle through cell values
                    //    for (int iCol = 0; iCol < dtStruck.Columns.Count; iCol++)
                    //    {
                    //        //assign cell value, only if it within columns of the grid
                    //        if (dgvEditCt1.ColumnCount - 1 >= c + iCol)
                    //        {
                    //            this.dgvEditCt1.Rows[r + iRow].Cells[c + iCol].Value = dtStruck.Rows[iRow][iCol];
                    //        }
                    //    }
                    //    this.dgvEditCt1.Rows.Add();
                    //}

                    //string strMsg = "Bạn có muốn lấy thông tin đơn bán hàng không?";
                    if (true)
                    {
                        DataRow drEditCt = dtEditCt.NewRow();
                        Common.CopyDataRow(dtEditCt.Rows[0], drEditCt);

                        dtEditCt.Rows.Clear();


                        foreach (DataRow drViewCt in dtStruck.Rows)
                        {
                            DataRow drEditCtNew = dtEditCt.NewRow();
                            Common.CopyDataRow(drEditCt, drEditCtNew);
                            Common.CopyDataRow(drViewCt, drEditCtNew);
                            Voucher.Calc_So_Luong(drEditCtNew);
                            Voucher.Calc_Tien(drEditCtNew);
                            dtEditCt.Rows.Add(drEditCtNew);
                        }

                        dtEditCt.AcceptChanges();
                        Voucher.Update_TTien(this);
                        Voucher.Adjust_TThue_Vat(this, true);

                    }

                }
            }
        }
        void dgvEditCt2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {// Hien notice khi Gotfocus

        }

        void dgvEditCt2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { //Xu ly phim Enter, Lookup danh muc

            if (this.ActiveControl != dgvEditCt2)
                return;

            //Xu ly phim Enter
            if (dgvEditCt2.kLastKey == Keys.Enter)
            {
                dgvEditCt2.kLastKey = Keys.None;

                if (dgvEditCt2.bIsCurrentLastRow && dgvEditCt2.bIsCurrentLastColumn)
                {
                    this.SelectNextControl(dgvEditCt2, true, true, true, true);
                }
            }

            if (this.ActiveControl == dgvEditCt2 || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;
                DataGridViewCell dgvCell = dgvEditCt2.CurrentCell;
                string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

                bool bLookup = true;

                if (Common.Inlist(strColumnName, "TK_NO,TK_CO"))
                    bLookup = dgvLookupTk(ref dgvCell, strColumnName);
            }

        }

        void dgvEditCt2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Tinh toan cac Gia tri, cong thuc
            dgvVoucher dgvEditCt = (dgvVoucher)sender;
            if (this.ActiveControl != dgvEditCt && this.ActiveControl != null && this.ActiveControl.GetType().Name != "DataGridViewTextBoxEditingControl")
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            DataGridViewCell dgvCell = ((dgvVoucher)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (Common.Inlist(strColumnName, "GIA_NT, GIA ,TIEN_NT, TIEN"))
            {
                if (!(bool)drCurrent["Auto_Cost"])
                    Voucher.Calc_Tien_Von(drCurrent);
            }

            bdsEditCt.EndEdit();
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

        private bool dgvLookupMa_Ctkm(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;
            double dbTien_M4 = drCurrent["Tien_M4"] == DBNull.Value ? 0 : Convert.ToDouble(drCurrent["Tien_M4"]);


            if (dbTien_M4 > 0)
                bRequire = true;
            else if ((bool)drCurrent["Hang_Km"])
            {
                bRequire = true;
            }

            DataRow drLookup = Lookup.ShowQuickLookup("MA_CTKM_M", strValue, bRequire, "'" + Library.DateToStr(Convert.ToDateTime(drCurrent["Ngay_Ct"].ToString())) + "' BETWEEN Ngay_Bd AND Ngay_Kt ", "");
            //DataRow drLookup = Lookup.ShowLookup("MA_CTKM", strValue, bRequire, "", "");
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
                dgvCell.Value = drLookup["Ma_CtKM"].ToString();
                dgvCell.Tag = drLookup["Ten_CtKM"].ToString();
            }
            return true;
        }
        private bool dgvLookupMa_CbNv(ref DataGridViewCell dgvCell)
        {
            string strValue = string.Empty;

            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();

            bool bRequire = false;


            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

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
                dgvCell.Value = drLookup["Ma_CbNv"].ToString();
                dgvCell.Tag = drLookup["Ten_CbNv"].ToString();
            }

            return true;
        }

        private bool dgvLookupMa_Lo(ref DataGridViewCell dgvCell)
        {
            //return false;
            string strValue = string.Empty;
            string strFillValue = string.Empty;
            if (this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
                strValue = this.ActiveControl.Text;
            else
                strValue = dgvCell.FormattedValue.ToString().Trim();


            bool bRequire = true;
            strFillValue = " Ma_Vt = '" + drCurrent["Ma_Vt"] + "' AND Nam = " + Element.sysWorkingYear.ToString();
            DataRow drvt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", (string)drCurrent["Ma_Vt"]);
            if (!(bool)drvt["LotSerial"])
            {
                dgvCell.Value = string.Empty;
                dgvCell.Tag = string.Empty;
                strFillValue = "0=1";
                bRequire = false;
            }


            DataRow drLookup = Lookup.ShowQuickLookup("Ma_Lo", strValue, bRequire, strFillValue, "");

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
                dgvCell.Value = drLookup["Ma_Lo"].ToString();
                drCurrent["Han_Sd"] = drLookup["Han_Sd"];
                drCurrent["Ma_Kho"] = drLookup["Ma_Kho"];
            }

            return true;
        }
        private bool dgvLookupMa_Thue(ref DataGridViewCell dgvCell)
        {
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
            }
            else
            {
                dgvEditCt1.CancelEdit();
                dgvCell.Value = drLookup["Ma_Thue"].ToString();
                dgvCell.Tag = drLookup["Ten_Thue"].ToString();
                drCurrent["Thue_GtGt"] = Convert.ToInt32(drLookup["Thue_Suat"]);
            }
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
                    drCurrent["Is_EditDisc"] = 1;
                    //drCurrent["Ma_Kho"] = drLookup["Ma_Kho_Ban"];
                    drCurrent["Dvt"] = drLookup["Dvt_MD"] == string.Empty ? drLookup["Dvt"] : drLookup["Dvt_MD"];


                    // tính hệ số
                    drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                    string strDvt_Old = (string)drCurrent["Dvt"];
                    string strDvt_Chuan = (string)drLookup["Dvt"];


                    if ((string)drCurrent["Dvt"] == strDvt_Chuan)
                        drCurrent["He_So9"] = 1;
                    else
                        for (int i = 1; i <= 3; i++)
                            if ((string)drLookup["Dvt" + i] == (string)drCurrent["Dvt"])
                                drCurrent["He_So9"] = drLookup["He_So" + i];


                    //drCurrent["He_So9"] = 1;

                    Voucher.Calc_So_Luong(drCurrent);

                    //if (strMa_Ct == "TL")//TL
                    if ((string)drDmCt["Nh_Ct"] == "1")
                    {
                        drCurrent["Tk_No"] = drLookup["Tk_Vt"];
                        drCurrent["Tk_Co"] = drLookup["Tk_Gv"];
                        drCurrent["Tk_Co2"] = drLookup["Tk_Hbtl"];
                        drCurrent["Tk_No2"] = drLookup["Tk_Dt"];
                        if (strMa_Ct == "INT")
                        {
                            drCurrent["Tk_Co2"] = drDmCt["Tk_Co"];
                            drCurrent["Tk_No2"] = drDmCt["Tk_No"];
                        }

                        // Theo Doi Tuong
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Hbtl"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Hbtl"] != String.Empty ? (string)drDmDt["Tk_Hbtl"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_Co2"];
                    }
                    else//HD
                    {
                        drCurrent["Tk_No"] = drLookup["Tk_Gv"];
                        drCurrent["Tk_Co"] = drLookup["Tk_Vt"];
                        drCurrent["Tk_Co2"] = drLookup["Tk_Dt"];



                        if (drLookup.Table.Columns.Contains("Ma_Thue_OUT"))
                            drCurrent["Ma_Thue"] = drLookup["Ma_Thue_OUT"];
                        // Theo Doi Tuong
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dthu"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dthu"] != String.Empty ? (string)drDmDt["Tk_Dthu"] : drCurrent["Tk_Co2"];

                        Calc_Thue_Vat_Tk(drCurrent);
                        Calc_Thue_Vat(drCurrent);
                    }




                }
                else
                {
                    if (drCurrent["Ten_Vt"] == DBNull.Value || (string)drCurrent["Ten_Vt"] == string.Empty)
                        drCurrent["Ten_Vt"] = drLookup["Ten_Vt"];

                    if (drCurrent["Dvt"] == DBNull.Value || (string)drCurrent["Dvt"] == string.Empty)
                        drCurrent["Dvt"] = drLookup["Dvt"];

                    if ((string)drDmCt["Nh_Ct"] == "1")// Don tra
                    {


                    }
                    else// don ban
                    {


                    }
                    //if (strMa_Ct == "TL")//TL
                    if ((string)drDmCt["Nh_Ct"] == "1")
                    {
                        if (drCurrent["Tk_No"] == DBNull.Value || (string)drCurrent["Tk_No"] == string.Empty)
                            drCurrent["Tk_No"] = drLookup["Tk_Vt"];

                        if (drCurrent["Tk_Co"] == DBNull.Value || (string)drCurrent["Tk_Co"] == string.Empty)
                            drCurrent["Tk_Co"] = drLookup["Tk_Gv"];

                        if (drCurrent["Tk_No2"] == DBNull.Value || (string)drCurrent["Tk_No2"] == string.Empty)
                            drCurrent["Tk_No2"] = drLookup["Tk_HBTL"];

                        // Lay theo đối tượng
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Hbtl"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Hbtl"] != String.Empty ? (string)drDmDt["Tk_Hbtl"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_Co2"];
                    }
                    else//HD
                    {
                        if (drCurrent["Tk_No"] == DBNull.Value || (string)drCurrent["Tk_No"] == string.Empty)
                            drCurrent["Tk_No"] = drLookup["Tk_Gv"];

                        if (drCurrent["Tk_Co"] == DBNull.Value || (string)drCurrent["Tk_Co"] == string.Empty)
                            drCurrent["Tk_Co"] = drLookup["Tk_Vt"];

                        if (drCurrent["Tk_Co2"] == DBNull.Value || (string)drCurrent["Tk_Co2"] == string.Empty)
                            drCurrent["Tk_Co2"] = drLookup["Tk_Dt"];


                        if (drLookup.Table.Columns.Contains("Ma_Thue_OUT"))
                            if (drCurrent["Ma_Thue"] == DBNull.Value || (string)drCurrent["Ma_Thue"] == string.Empty)
                                drCurrent["Ma_Thue"] = drLookup["Ma_Thue_OUT"];

                        // Lay theo đối tượng
                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dt"))
                            drCurrent["Tk_No2"] = (string)drDmDt["Tk_Dt"] != String.Empty ? (string)drDmDt["Tk_Dt"] : drCurrent["Tk_No2"];

                        if (this.drDmDt != null && this.drDmDt.Table.Columns.Contains("Tk_Dthu"))
                            drCurrent["Tk_Co2"] = (string)drDmDt["Tk_Dthu"] != String.Empty ? (string)drDmDt["Tk_Dthu"] : drCurrent["Tk_Co2"];
                        Calc_Thue_Vat_Tk(drCurrent);
                        Calc_Thue_Vat(drCurrent);
                    }
                }
                // Get Tk_Gv from LiKho
                string strMa_Kho = drCurrent["Ma_Kho"] == DBNull.Value ? string.Empty : (string)drCurrent["Ma_Kho"];
                DataRow drKho = DataTool.SQLGetDataRowByID("LIKHO", "Ma_Kho", strMa_Kho);

                if (drKho == null)
                {

                }
                else
                {

                    //if (drKho.Table.Columns.Contains("Tk_Gv") && drKho["Tk_Gv"].ToString() != string.Empty)
                    if ((string)drDmCt["Nh_Ct"] == "1")//HD In
                    {
                        drCurrent["Tk_Co"] = drKho.Table.Columns.Contains("Tk_Gv") && drKho["Tk_Gv"].ToString() != string.Empty ? drKho["Tk_Gv"].ToString() : drCurrent["Tk_Co"];
                        drCurrent["Tk_No"] = drKho.Table.Columns.Contains("Tk_Kho") && drKho["Tk_Kho"].ToString() != string.Empty ? drKho["Tk_Kho"].ToString() : drCurrent["Tk_No"];

                        drCurrent["Tk_Co2"] = drKho.Table.Columns.Contains("Tk_No") && drKho["Tk_No"].ToString() != string.Empty ? drKho["Tk_No"].ToString() : drCurrent["Tk_Co2"];
                        drCurrent["Tk_No2"] = drKho.Table.Columns.Contains("Tk_Dthu") && drKho["Tk_Dthu"].ToString() != string.Empty ? drKho["Tk_Dthu"].ToString() : drCurrent["Tk_No2"];
                    }
                    else // HD Out
                    {
                        drCurrent["Tk_Co"] = drKho.Table.Columns.Contains("Tk_Kho") && drKho["Tk_Kho"].ToString() != string.Empty ? drKho["Tk_Kho"].ToString() : drCurrent["Tk_Co"];
                        drCurrent["Tk_No"] = drKho.Table.Columns.Contains("Tk_Gv") && drKho["Tk_Gv"].ToString() != string.Empty ? drKho["Tk_Gv"].ToString() : drCurrent["Tk_No"];

                        drCurrent["Tk_Co2"] = drKho.Table.Columns.Contains("Tk_Dthu") && drKho["Tk_Dthu"].ToString() != string.Empty ? drKho["Tk_Dthu"].ToString() : drCurrent["Tk_Co2"];
                        drCurrent["Tk_No2"] = drKho.Table.Columns.Contains("Tk_No") && drKho["Tk_No"].ToString() != string.Empty ? drKho["Tk_No"].ToString() : drCurrent["Tk_No2"];
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

            bool bRequire = true;

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

                //if (drLookup.Table.Columns.Contains("Tk_Gv") && drLookup["Tk_Gv"].ToString() != string.Empty)
                //    if ((string)drDmCt["Nh_Ct"] == "1")
                //    {

                //        drCurrent["Tk_Co"] = drLookup["Tk_Gv"].ToString();
                //    }
                //    else
                //    {
                //        drCurrent["Tk_No"] = drLookup["Tk_Gv"].ToString();
                //    }
                //if ((string)drDmCt["Nh_Ct"] == "1")
                //{
                //    drCurrent["Tk_Co"] = drLookup.Table.Columns.Contains("Tk_Gv") && drLookup["Tk_Gv"].ToString() != string.Empty ? drLookup["Tk_Gv"].ToString() : drCurrent["Tk_Co"];
                //    drCurrent["Tk_No"] = drLookup.Table.Columns.Contains("Tk_Kho") && drLookup["Tk_Kho"].ToString() != string.Empty ? drLookup["Tk_Kho"].ToString() : drCurrent["Tk_No"];
                //}
                //else
                //{
                //    drCurrent["Tk_Co"] = drLookup.Table.Columns.Contains("Tk_Kho") && drLookup["Tk_Kho"].ToString() != string.Empty ? drLookup["Tk_Kho"].ToString() : drCurrent["Tk_Co"];
                //    drCurrent["Tk_No"] = drLookup.Table.Columns.Contains("Tk_Gv") && drLookup["Tk_Gv"].ToString() != string.Empty ? drLookup["Tk_Gv"].ToString() : drCurrent["Tk_No"];
                //}

                if ((string)drDmCt["Nh_Ct"] == "1")//HD In
                {
                    drCurrent["Tk_Co"] = drLookup.Table.Columns.Contains("Tk_Gv") && drLookup["Tk_Gv"].ToString() != string.Empty ? drLookup["Tk_Gv"].ToString() : drCurrent["Tk_Co"];
                    drCurrent["Tk_No"] = drLookup.Table.Columns.Contains("Tk_Kho") && drLookup["Tk_Kho"].ToString() != string.Empty ? drLookup["Tk_Kho"].ToString() : drCurrent["Tk_No"];

                    drCurrent["Tk_Co2"] = drLookup.Table.Columns.Contains("Tk_No") && drLookup["Tk_No"].ToString() != string.Empty ? drLookup["Tk_No"].ToString() : drCurrent["Tk_Co2"];
                    drCurrent["Tk_No2"] = drLookup.Table.Columns.Contains("Tk_Dthu") && drLookup["Tk_Dthu"].ToString() != string.Empty ? drLookup["Tk_Dthu"].ToString() : drCurrent["Tk_No2"];
                }
                else // HD Out
                {
                    drCurrent["Tk_Co"] = drLookup.Table.Columns.Contains("Tk_Kho") && drLookup["Tk_Kho"].ToString() != string.Empty ? drLookup["Tk_Kho"].ToString() : drCurrent["Tk_Co"];
                    drCurrent["Tk_No"] = drLookup.Table.Columns.Contains("Tk_Gv") && drLookup["Tk_Gv"].ToString() != string.Empty ? drLookup["Tk_Gv"].ToString() : drCurrent["Tk_No"];

                    drCurrent["Tk_Co2"] = drLookup.Table.Columns.Contains("Tk_Dthu") && drLookup["Tk_Dthu"].ToString() != string.Empty ? drLookup["Tk_Dthu"].ToString() : drCurrent["Tk_Co2"];
                    drCurrent["Tk_No2"] = drLookup.Table.Columns.Contains("Tk_No") && drLookup["Tk_No"].ToString() != string.Empty ? drLookup["Tk_No"].ToString() : drCurrent["Tk_No2"];
                }
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
                    this.ucNotice.SetText("Data Lock Date");
                }
                // Không cho chinh sửa khi tạo PXK
                //if (drEditPh["So_Ct_Lap"].ToString() != string.Empty && drEditPh["Ma_Ct"].ToString() == "IN")
                //{
                //    this.dgvEditCt1.Enabled = false;
                //    this.btgAccept.btAccept.Enabled = false;
                //}
                //kiểm tra xem chứng từ đã thanh toán chưa
                if (!Check_ThanhToan())
                {
                    this.dteNgay_Ct.Enabled = false;
                    this.dgvEditCt1.ReadOnly = true;
                    //this.dgvEditCt2.ReadOnly = false;
                    //this.dgvEditCt1.Enabled = false;
                    this.btgAccept.btAccept.Enabled = false;
                    this.ucNotice.SetText("Data Lock Payment");
                    this.Is_CtEdit = false;
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
                                this.ucNotice.SetText("Data Lock Permission");
                                this.dgvEditCt1.ReadOnly = true;
                                this.Is_CtEdit = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void BindingTaxDefault(string Ma_Thue_Dft)
        {
            if (Ma_Thue_Dft == string.Empty)
                return;
            DataRow drLookup = DataTool.SQLGetDataRowByID("LITHUE", "Ma_Thue", Ma_Thue_Dft);
            if (drLookup != null)
            {
                txtMa_Thue.Text = (string)drLookup["Ma_Thue"];
                dicName.SetValue("Ten_Thue", drLookup["Ten_Thue"].ToString());
                this.drEditPh["Thue_Gtgt"] = drLookup["Thue_Suat"];
                this.drEditPh["Ma_Thue"] = (string)drLookup["Ma_Thue"];
            }
            this.Ma_Thue_Valid();
            Voucher.Update_Detail(this, "Ma_Thue, Thue_Gtgt");
            Voucher.Adjust_TThue_Vat(this, true);

        }
        public string strDiscItem { get; set; }

        private bool Check_ThanhToan()
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("STT", strStt);
            string strSQL = @" DECLARE @Stt_HD VARCHAR(20) SET @Stt_HD = @Stt  " +
                            @" SELECT Stt_TT FROM GLTHANHTOANCT  (NOLOCK) WHERE Stt_HD = @Stt_HD" +
                            @" UNION ALL SELECT Stt FROM GLVOUCHER  (NOLOCK) WHERE Stt = @Stt_HD AND Ma_Ct IN ('IN','') AND Duyet = 1 " +
                            @" UNION ALL SELECT Stt FROM GLVOUCHER   (NOLOCK) WHERE Stt = @Stt_HD AND Ma_Ct IN ('INT') AND Duyet = 1 AND So_Ct_Lap <> '' ";
            DataTable dt = SQLExec.ExecuteReturnDt("sp_Check_ThanhToan", htPara, CommandType.StoredProcedure);
            if (dt.Rows.Count > 0)
                return false;
            return true;
        }

        private DataTable GetDataFromClipboard(IDataObject dataInClipboard)
        {
            try
            {
                DataTable dtStruck = SQLExec.ExecuteReturnDt("select Ma_Vt,Ten_Vt,Dvt,Ma_Kho,So_Luong9,Gia_Nt9,Tien_Nt9  FROM ARBAN WHERE 0=1");
                DataTable dtData = new DataTable();
                //if user clicked Shift+Ins or Ctrl+V (paste from clipboard)

                char[] rowSplitter = { '\r', '\n' };
                char[] columnSplitter = { '\t' };
                //get the text from clipboard
                //IDataObject dataInClipboard = Clipboard.GetDataObject();
                string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);
                //split it into lines
                string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
                //get the row and column of selected cell in grid
                //int r = this.SelectedCells[0].RowIndex;
                //int c = this.SelectedCells[0].ColumnIndex;
                ////add rows into grid to fit clipboard lines
                //if (this.Rows.Count < (r + rowsInClipboard.Length))
                //{
                //    this.Rows.Add(r + rowsInClipboard.Length - this.Rows.Count);
                //}


                // loop through the lines, split them into cells and place the values in the corresponding cell.
                for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
                {
                    //split row into cell values
                    string[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);
                    DataRow drEx = dtStruck.NewRow();
                    //cycle through cell values
                    for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                    {
                        //DataColumn dtcolumn = new DataColumn("datacolumn" + iCol);
                        //dtData.Columns.Add(dtcolumn);                           
                        drEx[iCol] = valuesInRow[iCol];

                    }
                    dtStruck.Rows.Add(drEx);
                }

                return dtStruck;
            }
            catch
            {
                MessageBox.Show("Data is not Valid !!");
                return null;
            }
        }

    }
}
