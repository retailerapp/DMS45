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
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.HRM
{
    public partial class frmBHXH_Edit : frmEdit
    {
        #region Phuong thuc

        public frmBHXH_Edit()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
        }

        public void Load(enuEdit enuNew_Edit, DataRow drEdit)
        {
            this.drEdit = drEdit;
            this.enuNew_Edit = enuNew_Edit;
            this.Tag = (char)enuNew_Edit + "," + this.Tag;

            Common.ScaterMemvar(this, ref drEdit);

            BindingLanguage();
            LoadDicName();

            this.ShowDialog();
        }

        private void LoadDicName()
        {
            //txtMa_Dt
            if (txtMa_CbNv.Text.Trim() != string.Empty)
                txtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CBNV", "Ten_CbNv", txtMa_CbNv.Text.Trim());
            else
                txtTen_CbNv.Text = string.Empty;
        }

        public bool FormCheckValid()
        {
            bool bvalid = true;
            return bvalid;
        }

        public bool Save()
        {
            Common.GatherMemvar(this, ref drEdit);

            //Kiem tra Valid tren Form
            if (!FormCheckValid())
                return false;

            //Luu xuong CSDL
            if (!DataTool.SQLUpdate(enuNew_Edit, "HRBHXH", ref drEdit))
                return false;

            //Đưa vào thông tin tính bảo hiểm -> insert 1 dòng vào HRPARAVALUE0            
            string str = string.Empty;
            Hashtable htSQLPara = new Hashtable();
            htSQLPara.Add("MA_CBNV", txtMa_CbNv.Text);
            htSQLPara.Add("MA_TN", "LUONG3");
            htSQLPara.Add("VALUE", numLuong_BH.Value);
            htSQLPara.Add("IS_UUTIEN", "1");

            if (chkIs_Tinh.Checked)
            {
                str = " DELETE FROM HRPARAVALUE0 WHERE Ma_CbNv = '" + txtMa_CbNv.Text + "' AND Ma_Tn = 'LUONG3'" +
                      " INSERT INTO HRPARAVALUE0 (Ma_CbNv, Ma_Tn, Value, Is_UuTien)" +
                            " VALUES (@Ma_CbNv, @Ma_Tn, @Value, @Is_UuTien)";

                SQLExec.Execute(str, htSQLPara, CommandType.Text);
            }
            ////////////////////////////////////

            return true;
        }
        #endregion

        #region Su kien
        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                isAccept = true;
                this.Close();
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            isAccept = false;
            this.Close();
        }
        #endregion
    }
}
