using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Librarys;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using System.Collections;

namespace Epoint.Modules.HRM
{
    public partial class frmChamCongNgay : Epoint.Systems.Customizes.frmView
    {

        #region Khai bao bien

        private DataTable dtHRChamCong;
        private DataRow drCurrent;
        private BindingSource bdsHRChamCong = new BindingSource();
        //private dgvControl dgvResource = new dgvControl();

        #endregion

        #region Contructor

        public frmChamCongNgay()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(KeyDownEvent);
            this.btCreateChamCong.Click += new EventHandler(btCreateChamCong_Click);
            dteNgay_Ct.Validating += new CancelEventHandler(dteNgay_Ct_Validating);
            this.dgvHRChamCong.Resize += new EventHandler(dgvChangeID_Resize);
            this.dgvHRChamCong.CellClick += new DataGridViewCellEventHandler(dgvListCustomerLics_CellClick);
            dgvHRChamCong.KeyDown += new KeyEventHandler(dgvHRChamCong_KeyDown);           

        }        
        public override void Load()
        {
            dteNgay_Ct.Text = Library.DateToStr(DateTime.Now);
            Build();
            FillData();
            BindingLanguage();

            this.Show();
        }

        #endregion

        #region Build, FillData

        private void Build()
        {
            dgvHRChamCong.Dock = DockStyle.Fill;

            dgvHRChamCong.strZone = "CHAMCONGNGAY";
            dgvHRChamCong.BuildGridView(false);

            //this.Controls.Add(dgvResource);
        }

        private void FillData()
        {
            Hashtable htParameter = new Hashtable();

            htParameter.Add("NGAY_CT", Library.StrToDate(dteNgay_Ct.Text));
            htParameter.Add("LOAI_NC", txtLoai_NC.Text);
            htParameter.Add("MA_DVCS", Element.sysMa_DvCs);

            dtHRChamCong = SQLExec.ExecuteReturnDt("sp_HRM_LoadChamCong", htParameter, CommandType.StoredProcedure);

            this.bdsHRChamCong.DataSource = dtHRChamCong;
            dgvHRChamCong.DataSource = bdsHRChamCong;
            this.ExportControl = dgvHRChamCong;
            bdsSearch = bdsHRChamCong;

        }

        #endregion

        #region Update

        public override void Edit(enuEdit enuNew_Edit)
        {

        }

        public override void Delete()
        {

        }

        #endregion

        #region Su kien

        void btCreateChamCong_Click(object sender, EventArgs e)
        {
            bool flag2 = true;
            if (DataTool.SQLCheckExist("HRCHAMCONGNGAY", new string[] { "TranDate", "Loai_Nc" }, new object[] { dteNgay_Ct.Text, txtLoai_NC.Text }))
            {
                string strMsg = (Element.sysLanguage == enuLanguageType.Vietnamese) ? "Ngày công đã tồn tại, Bạn có muốn tạo lại ?" : "Do you want to override exists data ?";
                flag2 = Common.MsgYes_No(strMsg);
            }

            if (flag2)
            {
                Hashtable htParameter = new Hashtable();

                htParameter.Add("NGAY_CT", Library.StrToDate(dteNgay_Ct.Text));
                htParameter.Add("LOAI_NC", txtLoai_NC.Text);
                htParameter.Add("MA_DVCS", Element.sysMa_DvCs);

                SQLExec.Execute("sp_HRM_TaoNgayCong", htParameter, CommandType.StoredProcedure);
                dtHRChamCong = SQLExec.ExecuteReturnDt("sp_HRM_LoadChamCong", htParameter, CommandType.StoredProcedure);
            }
            this.bdsHRChamCong.DataSource = dtHRChamCong;
            dgvHRChamCong.DataSource = bdsHRChamCong;
            this.ExportControl = dgvHRChamCong;
            bdsSearch = bdsHRChamCong;
        }
        void KeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    Delete();
                    break;
            }
        }

        void dgvChangeID_Resize(object sender, EventArgs e)
        {
            this.dgvHRChamCong.ResizeGridView();
        }
        void dgvListCustomerLics_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            DataGridViewCell dgvCell = ((dgvControl)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();
            if (Common.Inlist(strColumnName, "NGAY_CONG"))
            {
                drCurrent = ((DataRowView)bdsHRChamCong.Current).Row;
                drCurrent["Ngay_Cong"] = !Convert.ToBoolean(drCurrent["Ngay_Cong"]);
                if (Convert.ToBoolean(drCurrent["Ngay_Cong"]))
                {
                    drCurrent["Ngay_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Cong"]);
                    drCurrent["Ngay_Khong_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Cong"]);
                }
                drCurrent.AcceptChanges();

                string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                SQLExec.Execute(sqlUpdate);
            }

            else if (Common.Inlist(strColumnName, "NGAY_PHEP"))
            {
                drCurrent = ((DataRowView)bdsHRChamCong.Current).Row;
                drCurrent["Ngay_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Phep"]);
                if (Convert.ToBoolean(drCurrent["Ngay_Phep"]))
                {
                    drCurrent["Ngay_Cong"] = !Convert.ToBoolean(drCurrent["Ngay_Phep"]);
                    drCurrent["Ngay_Khong_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Phep"]);
                }
                drCurrent.AcceptChanges();

                string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                SQLExec.Execute(sqlUpdate);
            }
            else if (Common.Inlist(strColumnName, "NGAY_KHONG_PHEP"))
            {
                drCurrent = ((DataRowView)bdsHRChamCong.Current).Row;
                drCurrent["Ngay_Khong_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]);
                if (Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]))
                {
                    drCurrent["Ngay_Phep"] = !Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]);
                    drCurrent["Ngay_Cong"] = !Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]);
                }
                drCurrent.AcceptChanges();
                string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                SQLExec.Execute(sqlUpdate);
            }
        }

        void dteNgay_Ct_Validating(object sender, CancelEventArgs e)
        {
            FillData();
        }
        void dgvHRChamCong_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.A)
            {
                DataGridViewCell dgvCell = ((dgvControl)sender).CurrentCell;
                string strColumnName = dgvCell.OwningColumn.Name.ToUpper();
                foreach (DataRow drCurrent in dtHRChamCong.Rows)
                {
                    //dtHRChamCong.Rows[i][strColumnName] = true;
                    if (Common.Inlist(strColumnName, "NGAY_CONG"))
                    {

                        drCurrent["Ngay_Cong"] = true;
                        if (Convert.ToBoolean(drCurrent["Ngay_Cong"]))
                        {
                            drCurrent["Ngay_Phep"] = false;
                            drCurrent["Ngay_Khong_Phep"] = false;
                        }
                        drCurrent.AcceptChanges();

                        string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                        SQLExec.Execute(sqlUpdate);
                    }

                    else if (Common.Inlist(strColumnName, "NGAY_PHEP"))
                    {

                        drCurrent["Ngay_Phep"] = true;
                        if (Convert.ToBoolean(drCurrent["Ngay_Phep"]))
                        {
                            drCurrent["Ngay_Cong"] = false;
                            drCurrent["Ngay_Khong_Phep"] = false;
                        }
                        drCurrent.AcceptChanges();

                        string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                        SQLExec.Execute(sqlUpdate);
                    }
                    else if (Common.Inlist(strColumnName, "NGAY_KHONG_PHEP"))
                    {

                        drCurrent["Ngay_Khong_Phep"] = true;
                        if (Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]))
                        {
                            drCurrent["Ngay_Phep"] = false;
                            drCurrent["Ngay_Cong"] = false;
                        }
                        drCurrent.AcceptChanges();
                        string sqlUpdate = @"UPDATE HRCHAMCONGNGAY SET Ngay_Cong = '" + Convert.ToBoolean(drCurrent["Ngay_Cong"]) + @"' ,Ngay_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Phep"]) + @"',Ngay_Khong_Phep = '" + Convert.ToBoolean(drCurrent["Ngay_Khong_Phep"]) + @"'
                                WHERE Ident00 = " + drCurrent["Ident00"].ToString();
                        SQLExec.Execute(sqlUpdate);
                    }
                }
            }
        }


        #endregion

    }
}
