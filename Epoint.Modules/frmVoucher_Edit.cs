using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;

namespace Epoint.Modules
{
    public partial class frmVoucher_Edit : Epoint.Systems.Customizes.frmEdit
    {
        public DataSet dsVoucher;
        public BindingSource bdsEditCt = new BindingSource();

        public DataTable dtEditPh;
        public DataRow drEditPh;
        public DataRow drDmNvu;
        public DataRow drEditPhOrg;

        public DataTable dtEditCt;
        public DataTable dtEditCtDisc;
        public DataTable dtEditDisc;
        public DataTable dtDiscFreeItem;
        public DataRow drCurrent;
        public DataTable dtEditCtOrg;

        public DataRow drDmCt;
        public DataRow drDmDt;

        public DataTable dtCtVt;

        public string strStt = string.Empty;
        public string strMa_Ct = string.Empty;
        public string strSo_Ct = string.Empty;
        public string strMa_Tte = string.Empty;
        public double dbTy_Gia = 0;

        public bool bDgvEditCtFocusing = false;

        public bool Is_Cancel = false;
        public bool Is_CtEdit = true;

        public DataTable dtHanTt0;

        public frmVoucher_Edit()
        {
            InitializeComponent();

            this.bdsEditCt.PositionChanged += new EventHandler(bdsEditCt_PositionChanged);
            this.bdsEditCt.DataSourceChanged += new EventHandler(bdsEditCt_DataSourceChanged);
            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            this.KeyDown += new KeyEventHandler(frmVoucher_Edit_KeyDown);
        }

        public virtual void Load(enuEdit enuNew_Edit, DataRow drEdit, DataSet dsVoucher)
        {

        }

        public virtual bool Save()
        {
            return true;
        }

        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                isAccept = true;

                if ((enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy) && drDmCt.Table.Columns.Contains("Auto_New_Voucher") && (bool)drDmCt["Auto_New_Voucher"])
                {
                    this.Load(enuEdit.New, drEdit, dsVoucher);
                }
                else
                    this.Close();
                base.ShowSuccessMessage("Thành công !");
            }
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            isAccept = false;
            Is_Cancel = true;
            this.Close();
        }

        void bdsEditCt_PositionChanged(object sender, EventArgs e)
        {
            this.ucNotice.lblRecord.Text = (bdsEditCt.Position + 1).ToString() + "/" + bdsEditCt.Count.ToString();
        }

        void bdsEditCt_DataSourceChanged(object sender, EventArgs e)
        {
            this.ucNotice.lblRecord.Text = (bdsEditCt.Position + 1).ToString() + "/" + bdsEditCt.Count.ToString();
        }

        void frmVoucher_Edit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    {
                        if (e.Control)
                        {
                            Epoint.Systems.Customizes.frmView frm = new Epoint.Systems.Customizes.frmView();
                            frm.Text = "Detail";

                            SplitContainer splc = new SplitContainer();
                            splc.Orientation = Orientation.Horizontal;
                            splc.SplitterDistance = splc.Height / 3;
                            splc.Dock = DockStyle.Fill;

                            DataGridView dgvPh = new DataGridView();
                            dgvPh.Dock = DockStyle.Fill;
                            dgvPh.DataSource = dtEditPh;
                            dgvPh.BackgroundColor = Color.White;
                            dgvPh.AllowUserToAddRows = false;
                            splc.Panel1.Controls.Add(dgvPh);

                            DataGridView dgvCt = new DataGridView();
                            dgvCt.Dock = DockStyle.Fill;
                            dgvCt.DataSource = dtEditCt;
                            dgvCt.BackgroundColor = Color.White;
                            dgvCt.AllowUserToAddRows = false;
                            splc.Panel2.Controls.Add(dgvCt);

                            frm.Controls.Add(splc);
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Show();
                        }
                        break;
                    }
                case Keys.F6:
                    if ((e.Alt || e.Control) || e.Shift)
                    {
                        if ((!e.Alt && e.Control) && !e.Shift)
                        {
                            Voucher.CopyNewRow(this);
                        }
                        break;
                    }
                    Voucher.AddRow(this);
                    break;

                case Keys.S:
                    if (e.Control)
                    {
                        if (!btgAccept.btAccept.Enabled)
                        {
                            this.Close();
                            base.ShowSuccessMessage("Không lưu được thay đổi !");
                        }
                        else if (this.Save())
                        {
                            isAccept = true;

                            if ((enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy) && drDmCt.Table.Columns.Contains("Auto_New_Voucher") && (bool)drDmCt["Auto_New_Voucher"])
                            {
                                this.Load(enuEdit.New, drEdit, dsVoucher);
                            }
                            else
                                this.Close();
                            base.ShowSuccessMessage("Thành công !");
                        }
                    }
                    break;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //Kiem tra Permission
            if (Element.Is_Running)
            {
                switch (this.enuNew_Edit)
                {
                    case enuEdit.New:
                    case enuEdit.Copy:
                        this.btgAccept.btAccept.Enabled = Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_New);
                        break;
                    case enuEdit.Edit:
                        this.btgAccept.btAccept.Enabled = Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_Edit);
                        this.Is_CtEdit = Common.CheckPermission((string)drDmCt["Object_ID"], enuPermission_Type.Allow_Edit);
                        break;
                    default:
                        break;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F4)
            {
                if (this.ActiveControl.GetType().Name == "dgvVoucher")
                    bDgvEditCtFocusing = true;
                else
                     if (bDgvEditCtFocusing)
                    bDgvEditCtFocusing = false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool CheckHeaderChange()
        {
            bool bChange = false;

            if (drEditPh == null || drEditPhOrg == null)
                return false;

            Common.SetDefaultDataRow(ref drEditPh);
            Common.SetDefaultDataRow(ref drEditPhOrg);
            for (int i = 0; i < drEditPh.Table.Columns.Count; i++)
            {
                string strType = drEditPh[i].GetType().ToString();
                string strColName = drEditPh.Table.Columns[i].ColumnName;
                if (!drEditPhOrg.Table.Columns.Contains(strColName))
                    continue;
                switch (strType)
                {
                    case "System.Decimal":
                        if (drEditPhOrg.Table.Columns.Contains(strColName) && Convert.ToDouble(drEditPh[strColName]) != Convert.ToDouble(drEditPhOrg[strColName]))
                        {
                            bChange = true;
                            //MessageBox.Show()
                            break;
                        }
                        break;
                    case "System.DateTime":
                        if (Library.DateToStr(Convert.ToDateTime(drEditPh[strColName])) != Library.DateToStr(Convert.ToDateTime(drEditPhOrg[strColName])))
                        {
                            bChange = true;
                            //MessageBox.Show()
                            break;
                        }
                        break;
                    default:
                        if (drEditPh[strColName].ToString() != drEditPhOrg[strColName].ToString())
                        {
                            //MessageBox.Show(drEditPh.Table.Columns[i].ColumnName.ToString()+ " " + drEditPh[i].ToString() + "\n" + drEditPhOrg[i].ToString());
                            bChange = true;
                            break;
                        }
                        break;
                }


            }
            return bChange;
        }

        bool CheckDetailChange()
        {
            bool bChange = false;
            if (dtEditCt == null || dtEditCtOrg == null)
                return false;
            if (dtEditCt.Rows.Count != dtEditCtOrg.Rows.Count)
                return true;
            for (int i = 0; i < dtEditCt.Rows.Count; i++)
            {
                DataRow dr = dtEditCt.Rows[i];
                foreach (DataColumn dc in dtEditCt.Columns)
                {
                    string strType = dtEditCt.Columns[dc.ColumnName].DataType.ToString();
                    if (!dtEditCtOrg.Columns.Contains(dc.ColumnName))
                        continue;
                    if (dc.ColumnName != "Deleted" && dc.ColumnName.ToUpper() != "DUYET")
                    {
                        switch (strType)
                        {
                            case "System.Int32":
                            case "System.Int16":
                            case "System.Decimal":
                                if (Convert.ToDouble(dr[dc.ColumnName]) != Convert.ToDouble(dtEditCtOrg.Rows[i][dc.ColumnName]))
                                {
                                    bChange = true;
                                    break;
                                }
                                break;
                            case "System.DateTime":
                                if (dr[dc.ColumnName].ToString() != dtEditCtOrg.Rows[i][dc.ColumnName].ToString() && !dr[dc.ColumnName].ToString().StartsWith("01/01/1900"))
                                {
                                    bChange = true;
                                    break;
                                }
                                break;
                            default:
                                if (dr[dc.ColumnName].ToString() != dtEditCtOrg.Rows[i][dc.ColumnName].ToString())
                                {
                                    bChange = true;
                                    break;
                                }
                                break;

                        }
                    }
                }
                if (bChange)
                    break;
            }
            return bChange;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.isAccept || this.Is_Cancel)
                return;
            if (this.btgAccept.btAccept.Enabled == false)
                return;
            if (enuNew_Edit == enuEdit.Edit)
            {
                if (CheckHeaderChange() || CheckDetailChange())
                {
                    if (Common.MsgYes_No(Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn lưu những thay đổi không" : "Do you want save change"))
                        this.Save();
                }

            }

            if (enuNew_Edit == enuEdit.New)
                if (Common.MsgYes_No(Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn thoát không" : "Do you want to close"))
                    base.OnClosing(e);
                else
                    e.Cancel = true;
        }
    }

    public interface IEditCtLR
    {
        DataTable dtEdiCtLR
        {
            get;
        }
    }
}
