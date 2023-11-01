using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Customizes;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Lists;
using System.Collections;
using Epoint.Systems.Elements;

namespace Epoint.Modules.AR
{
    public partial class frmGiaBan_Edit : Epoint.Systems.Customizes.frmEdit
    {
        public bool bNh_Dt = false;
        public frmGiaBan_Edit()
        {
            InitializeComponent();

            this.txtMa_Vt.Validating += new CancelEventHandler(txtMa_Vt_Validating);
            this.txtMa_Dt.Validating += new CancelEventHandler(txtMa_Dt_Validating);

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
        }

        public void Load(enuEdit enuNew_Edit, DataRow drEdit)
        {
            this.enuNew_Edit = enuNew_Edit;
            this.drEdit = drEdit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            Common.ScaterMemvar(this, ref drEdit);

            BindingLanguage();
            LoadDicName();

            if (bNh_Dt)
            {
                lblMa_Dt.Visible = false;
                txtMa_Dt.Visible = false;
                lbtTen_Dt.Visible = false;


                lblMa_Nh_Dt.Visible = true;
                txtMa_Nh_Dt.Visible = true;
                lbtTen_Nh_Dt.Visible = true;
            }


            this.ShowDialog();
        }

        private void LoadDicName()
        {
            if (txtMa_Vt.Text.Trim() != string.Empty)
            {
                lbtTen_Vt.Text = DataTool.SQLGetNameByCode("LIVATTU", "Ma_Vt", "Ten_Vt", txtMa_Vt.Text.Trim());
                dicName.Add(lbtTen_Vt.Name, lbtTen_Vt.Text);
            }
            else
                lbtTen_Vt.Text = string.Empty;
        }

        private bool FormCheckValid()
        {
            if (txtMa_Vt.Text.Trim() == string.Empty)
            {
                Common.MsgOk(Languages.GetLanguage("Ma_Vt") + " " +
                        Languages.GetLanguage("Not_Null"));
                return false;
            }

            if (bNh_Dt && txtMa_Nh_Dt.Text.Trim() == string.Empty)
            {
                Common.MsgOk(Languages.GetLanguage("Ma_Nh_Dt") + " " +
                        Languages.GetLanguage("Not_Null"));
                return false;
            }



            return true;
        }

        private bool Save()
        {
            Common.GatherMemvar(this, ref drEdit);

            if (drEdit.Table.Columns.Contains("Ten_Vt"))
                drEdit["Ten_Vt"] = lbtTen_Vt.Text;

            if (drEdit.Table.Columns.Contains("Ten_Dt"))
                drEdit["Ten_Dt"] = lbtTen_Dt.Text;

            if (drEdit.Table.Columns.Contains("Ma_Nh_Dt") && txtMa_Nh_Dt.Visible == false)
                drEdit["Ma_Nh_Dt"] = string.Empty;

            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;

            //Luu xuong CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "ARGIABAN", ref drEdit))
                return false;

            return true;
        }
        private bool Save01()
        {
            Common.GatherMemvar(this, ref drEdit);

            if (drEdit.Table.Columns.Contains("Ten_Vt"))
                drEdit["Ten_Vt"] = lbtTen_Vt.Text;

            if (drEdit.Table.Columns.Contains("Ten_Dt"))
                drEdit["Ten_Dt"] = lbtTen_Dt.Text;

            if (drEdit.Table.Columns.Contains("Ma_Nh_Dt") && txtMa_Nh_Dt.Visible == false)
                drEdit["Ma_Nh_Dt"] = string.Empty;


            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;

            Hashtable htPara = new Hashtable();
            htPara.Add("ACTION", enuNew_Edit==enuEdit.Edit? "E": "N");
            htPara.Add("IDENT00", enuNew_Edit == enuEdit.Edit ? drEdit["IDENT00"] : 0);
            htPara.Add("MA_VT", drEdit["Ma_Vt"].ToString());
            htPara.Add("DVT", drEdit["DVT"].ToString());
            htPara.Add("MA_NH_DT", drEdit["MA_NH_DT"].ToString());
            htPara.Add("MA_DT", drEdit["MA_DT"].ToString());
            htPara.Add("NGAY_AP", drEdit["NGAY_AP"]);
            //htPara.Add("UserId", Element.sysUser_Id);
            //htPara.Add("MA_DATA", Element.sysMa_DvCs);
            bool IscheckData = Convert.ToBoolean(SQLExec.ExecuteReturnValue("Sp_ARGIABAN_CheckValid", htPara, CommandType.StoredProcedure));
            if (IscheckData)
            {
                Common.MsgOk("Dữ liệu giá đã tồn tại.");
                return false;
            }


            Hashtable htParaSave = new Hashtable();
            htParaSave.Add("ACTION", enuNew_Edit == enuEdit.Edit ? "E" : "N");
            htParaSave.Add("IDENT00", enuNew_Edit == enuEdit.Edit ? drEdit["IDENT00"] : 0);
            htParaSave.Add("MA_VT", drEdit["Ma_Vt"].ToString());
            htParaSave.Add("DVT", drEdit["DVT"].ToString());
            htParaSave.Add("MA_DT", drEdit["MA_DT"].ToString());
            htParaSave.Add("MA_NH_DT", drEdit["MA_NH_DT"].ToString());
            htParaSave.Add("NGAY_AP", drEdit["NGAY_AP"]);
            htParaSave.Add("GIA", drEdit["Gia"]);
            htParaSave.Add("USERID", Element.sysUser_Id);
            htParaSave.Add("MA_DATA", Element.sysMa_DvCs);
            //Luu xuong CSDL
            if (!SQLExec.Execute("Sp_ARGIABAN_AddOrUpdate",htParaSave,CommandType.StoredProcedure))
                return false;

            return true;
        }
        void txtMa_Vt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Vt.Text.Trim();
            bool bRequire = false;

            //frmVatTu frmLookup = new frmVatTu();
            DataRow drLookup = Lookup.ShowLookup("Ma_Vt", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Vt.Text = string.Empty;
                lbtTen_Vt.Text = string.Empty;
            }
            else
            {
                txtMa_Vt.Text = ((string)drLookup["Ma_Vt"]).Trim();
                lbtTen_Vt.Text = ((string)drLookup["Ten_Vt"]).Trim();
                if (this.enuNew_Edit == enuEdit.New)
                    txtDvt.Text = (string)drLookup["Dvt"];

                string strDvt_Chuan = (string)drLookup["Dvt"];

                string inputMask = (string)drLookup["Dvt"];

                for (int i = 1; i <= 3; i++)
                    inputMask += (string)drLookup["Dvt" + i] == string.Empty ? string.Empty : "," + (string)drLookup["Dvt" + i];

                if (inputMask != string.Empty)
                    inputMask += "," + inputMask;

                lbtDvt.Text = inputMask;
                txtDvt.InputMask = inputMask;
            }

            dicName.SetValue(lbtTen_Vt.Name, lbtTen_Vt.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void txtMa_Dt_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Dt.Text.Trim();
            bool bRequire = false;

            frmDoiTuong frmLookup = new frmDoiTuong();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "LIDOITUONG", "Ma_Dt", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Dt.Text = string.Empty;
                lbtTen_Dt.Text = string.Empty;
            }
            else
            {
                txtMa_Dt.Text = ((string)drLookup["Ma_Dt"]).Trim();
                lbtTen_Dt.Text = ((string)drLookup["Ten_Dt"]).Trim();




            }

            dicName.SetValue(lbtTen_Dt.Name, lbtTen_Dt.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        void btAccept_Click(object sender, EventArgs e)
        {

            bool bINewLoad = DataTool.SQLCheckExist("sys.procedures", "Name", "Sp_ARGIABAN_CheckValid");

            if (bINewLoad == false)
            {
                if (this.Save())
                {
                    this.isAccept = true;
                    this.Close();
                }
            }
            else
            {
                if (this.Save01())
                {
                    this.isAccept = true;
                    this.Close();
                }
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            this.isAccept = false;
            this.Close();
        }
    }
}
