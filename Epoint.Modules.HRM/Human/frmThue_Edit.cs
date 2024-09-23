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
    public partial class frmThue_Edit : frmEdit
    {
        #region Phuong thuc

        public double Muc_Thue = 0;
        public double Muc_Tru = 0;

        public frmThue_Edit()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            numGiam_Tru.Validating += new CancelEventHandler(numGiam_Tru_Validating);
            numTn_Tinh_Thue.Validating += new CancelEventHandler(numTn_Tinh_Thue_Validating);
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
                lbtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CBNV", "Ten_CbNv", txtMa_CbNv.Text.Trim());
            else
                lbtTen_CbNv.Text = string.Empty;
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
            if (!DataTool.SQLUpdate(enuNew_Edit, "HRTHUE", ref drEdit))
                return false;


            //Đưa vào thông tin tính bảo hiểm -> insert 1 dòng vào HRPARAVALUE0            
            string str = string.Empty;
            Hashtable htSQLPara = new Hashtable();
            htSQLPara.Add("MA_CBNV", txtMa_CbNv.Text);
            htSQLPara.Add("MA_TN", "THUE_TNCN");
            htSQLPara.Add("VALUE", numThue_TNCN.Value);
            htSQLPara.Add("IS_UUTIEN", "1");

            string str1 = string.Empty;
            Hashtable htSQLPara1 = new Hashtable();
            htSQLPara1.Add("MA_CBNV", txtMa_CbNv.Text);
            htSQLPara1.Add("MA_TN", "HS_LUONG0");
            htSQLPara1.Add("VALUE", numGiam_Tru.Value);
            htSQLPara1.Add("IS_UUTIEN", "1");

            if (chkIs_Tinh.Checked)
            {
                str = " DELETE FROM HRPARAVALUE0 WHERE Ma_CbNv = '" + txtMa_CbNv.Text + "' AND Ma_Tn = 'THUE_TNCN'" +
                      " INSERT INTO HRPARAVALUE0 (Ma_CbNv, Ma_Tn, Value, Is_UuTien)" +
                            " VALUES (@Ma_CbNv, @Ma_Tn, @Value, @Is_UuTien)";

                str1 = " DELETE FROM HRPARAVALUE0 WHERE Ma_CbNv = '" + txtMa_CbNv.Text + "' AND Ma_Tn = 'GIACANH'" +
                      " INSERT INTO HRPARAVALUE0 (Ma_CbNv, Ma_Tn, Value, Is_UuTien)" +
                            " VALUES (@Ma_CbNv, @Ma_Tn, @Value, @Is_UuTien)";

                SQLExec.Execute(str, htSQLPara, CommandType.Text);
                SQLExec.Execute(str1, htSQLPara1, CommandType.Text);
            }
            ////////////////////////////////////

            return true;
        }
        #endregion

        #region Su kien
        void numGiam_Tru_Validating(object sender, CancelEventArgs e)
        {
            //Giam_Tru_CN: Lay tu tham so trong HRPARAVALUE
            //Giam_Tru_GD: Lay tu tham so trong HRPARAVALUE
            double Giam_Tru_CN = 0;
            double Giam_Tru_GD = 0;
            Giam_Tru_CN = Convert.ToDouble(SQLExec.ExecuteReturnValue("SELECT Muc_Ap FROM HRPARAVALUE WHERE Ma_Tn = 'GIAM_TRU_CN'"));
            Giam_Tru_GD = Convert.ToDouble(SQLExec.ExecuteReturnValue("SELECT Muc_Ap FROM HRPARAVALUE WHERE Ma_Tn = 'GIAM_TRU_GD'"));

            //Cong thuc: Tien_Giam_Tru = Giam_Tru_CN + (Giam_Tru_GD * So_Nguoi_Giam_Tru)
            numTien_Giam_Tru.Value = Giam_Tru_CN + (Giam_Tru_GD * numGiam_Tru.Value);
            //Thu nhap quy doi phai chiu thue
            numTn_Tinh_Thue.Value = numTn_Chiu_Thue.Value - numTien_Giam_Tru.Value;
        }
        void numTn_Tinh_Thue_Validating(object sender, CancelEventArgs e)
        {
            Muc_Thue = Convert.ToDouble(SQLExec.ExecuteReturnValue("SELECT Ty_Le FROM HRTHUETN WHERE Muc_Tn1 <= " + numTn_Tinh_Thue.Value + " AND Muc_Tn2 >= " + numTn_Tinh_Thue.Value));
            Muc_Tru = Convert.ToDouble(SQLExec.ExecuteReturnValue("SELECT Muc_Tru FROM HRTHUETN WHERE Muc_Tn1 <= " + numTn_Tinh_Thue.Value + " AND Muc_Tn2 >= " + numTn_Tinh_Thue.Value));

            //Muc_Tru: quy dinh theo PL02/TNCN
            numThue_TNCN.Value = (numTn_Tinh_Thue.Value * Muc_Thue / 100) - Muc_Tru;
        }
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
