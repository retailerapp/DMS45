using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Epoint.Systems;
using Epoint.Systems.Librarys;
using Epoint.Systems.Elements;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;

namespace Epoint.Modules.Salary
{
	public partial class frmBangLuong_Posted : Epoint.Systems.Customizes.frmEdit
	{
		public frmBangLuong_Posted()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
			this.numThang.TextChanged += new EventHandler(numThang_TextChanged);
			this.numThang.Validating += new CancelEventHandler(numThang_Validating);
		}

		new public void Load(int iThang)
		{
			this.numThang.Value = iThang;
			this.txtMa_Ct.Text = "TD";

			this.ShowDialog();
		}

		private bool FormCheckValid()
		{
			if (txtMa_Ct.Text == string.Empty)
				return false;

			if (numThang.Value < 1 || numThang.Value > 12)
				return false;

			return true;
		}

		void numThang_TextChanged(object sender, EventArgs e)
		{
			this.txtDien_Giai.Text = "Hạch toán lương tháng " + this.numThang.Text + "/" + Element.sysWorkingYear.ToString();
		}

		void numThang_Validating(object sender, CancelEventArgs e)
		{
			if (numThang.Value < 0 || numThang.Value > 12)
				e.Cancel = false;
		}

		void btAccept_Click(object sender, EventArgs e)
		{
			DateTime dteNgay_Kh1 = Common.GetDate(Element.sysWorkingYear, Convert.ToInt16(numThang.Value), 1);

			if (!Common.CheckDataLocked(dteNgay_Kh1))
			{
				Common.MsgCancel("Dữ liệu đã khóa, liên hệ với nhà quản trị!");
				return;
			}

			if (this.FormCheckValid())
			{
				Hashtable htParameter = new Hashtable();
				htParameter.Add("THANG", numThang.Value);
				htParameter.Add("NAM", Element.sysWorkingYear);
				htParameter.Add("MA_CT", this.txtMa_Ct.Text);
				htParameter.Add("SO_CT", this.txtSo_Ct.Text);
				htParameter.Add("DIEN_GIAI", this.txtDien_Giai.Text);
				htParameter.Add("MA_DVCS", Element.sysMa_DvCs);

				SQLExec.Execute("sp_HRM_BangLuong_Update_Ct", htParameter, CommandType.StoredProcedure);

                isAccept = true;
				this.Close();
			}            
		}

		void btCancel_Click(object sender, EventArgs e)
		{
            isAccept = false;
			this.Close();
		}
	}
}
