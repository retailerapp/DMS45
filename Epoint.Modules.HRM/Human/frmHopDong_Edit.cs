using System;
using System.Collections.Generic;
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
	public partial class frmHopDong_Edit : frmEdit
	{
		#region Phuong thuc

		public frmHopDong_Edit()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
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
            //Ma_CbNv
            if (txtMa_CbNv.Text.Trim() != string.Empty)
                txtTen_CbNv.Text = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CBNV", "Ten_CbNv", txtMa_CbNv.Text.Trim());
            else
                txtTen_CbNv.Text = string.Empty;
		}
        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = false;

            frmViTri frmLookup = new frmViTri();
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CbNv.Text = string.Empty;
                txtTen_CbNv.Text = string.Empty;
            }
            else
            {
                txtMa_CbNv.Text = drLookup["Ma_CbNv"].ToString();
                txtTen_CbNv.Text = drLookup["Ten_CbNv"].ToString();
            }

            dicName.SetValue(txtTen_CbNv.Name, txtTen_CbNv.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
		public bool FormCheckValid()
		{
			bool bvalid = true;
			if (txtNoi_Dung.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Noi_Dung") + " " +
							  Languages.GetLanguage("Not_Null"));
				return false;
			}

            if (this.dteNgay_End.Text.Trim().Length == 10 && this.dteNgay_End.Text.Trim().Substring(this.dteNgay_End.Text.Length - 4, 4) != "1900")
            {
                DateTime dteNgay_Bd = Library.StrToDate(this.dteNgay_Begin.Text);
                DateTime dteNgay_Kt = Library.StrToDate(this.dteNgay_End.Text);
                if ((DateTime.Compare(dteNgay_Bd, dteNgay_Kt) > 0))
                {
                    MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
                    this.dteNgay_End.Select();
                    return false;
                }
            }
            string strCheckSohd = "";
            if (enuNew_Edit == enuEdit.New)
                strCheckSohd = "SELECT  * FROM HRHOPDONG WHERE So_Hd ='" + txtSo_Hd.Text.Trim() + "' ";
            else
                strCheckSohd = "SELECT  * FROM HRHOPDONG WHERE So_Hd ='" + txtSo_Hd.Text.Trim() + "' AND Ident00 <> " + drEdit["Ident00"].ToString() + " ";
            DataTable dtTemp = SQLExec.ExecuteReturnDt(strCheckSohd);
            if (dtTemp.Rows.Count > 0)
            {
                MessageBox.Show("Số hợp đồng đã tồn tại");
                return false;
            }
			return bvalid;
		}

		public bool Save()
		{           
			Common.GatherMemvar(this, ref drEdit);

			//Kiem tra Valid tren Form
			if (!FormCheckValid())
				return false;

            //Kiem tra xem co ton tai hop dong nao khong
            if (enuNew_Edit == enuEdit.New && DataTool.SQLCheckExist("HRHOPDONG", new string[] { "Ma_CbNv", "Da_Cham_Dut", "Loai_Hd" },
                                            new object[] { drEdit["Ma_CbNv"].ToString(), false, drEdit["Loai_Hd"].ToString() }))
            {
                string strTen_CbNv = DataTool.SQLGetNameByCode("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", "");
                MessageBox.Show("Nhân viên " + strTen_CbNv + " có hợp đồng chưa kết thúc");
                return false;
            }

			//Luu xuong CSDL
			if (!DataTool.SQLUpdate(enuNew_Edit, "HRHOPDONG", ref drEdit))
				return false;

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
