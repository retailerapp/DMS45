using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Epoint.Systems;
using Epoint.Systems.Commons;
using Epoint.Systems.Data;
using Epoint.Systems.Librarys;
using Epoint.Systems.Elements;

namespace Epoint.Modules.Salary
{
	public partial class frmBangLuong_Create : Epoint.Systems.Customizes.frmEdit
	{
		int iThang = 0;
		string strMa_Bp = string.Empty;
		string strMa_CbNv = string.Empty;

		public frmBangLuong_Create()
		{
			InitializeComponent();

			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
		}

		new public void Load(int iThang, string strMa_Bp, string strMa_CbNv)
		{
			this.iThang = iThang;
			this.strMa_Bp = strMa_Bp;
			this.strMa_CbNv = strMa_CbNv;

			//Ngầm định tháng đích tạo bảng lương
			if (iThang == 12)
			{
				this.numThang1.Value = 1;
				this.numNam1.Value = Element.sysWorkingYear + 1;
			}
			else
			{
				this.numThang1.Value = iThang + 1;
				this.numNam1.Value = Element.sysWorkingYear;
			}

			this.Text = "Tạo bảng lương mới";

			this.ShowDialog();
		}

		private bool CheckFormValid()
		{
			DateTime dNgay_Ct = Library.StrToDate("1/" + this.numThang1.Value.ToString().Trim() + "/" + this.numNam1.ToString().Trim());

			if (!Common.CheckDataLocked(dNgay_Ct))
			{
				Common.MsgCancel("Dữ liệu đã khóa!");
				return false;
			}

			return true;
		}

		private bool Save()
		{
			string strMess = Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có chắc chắn tạo bảng lương tháng ?" : "Are you sure create salary table month ?" + this.numThang1.ToString().Trim();

			if (Common.MsgYes_No(strMess, "N"))
			{
				Hashtable htPara = new Hashtable();
				htPara.Add("THANG", iThang);
				htPara.Add("NAM", Element.sysWorkingYear);
				htPara.Add("THANG1", numThang1.Value);
				htPara.Add("NAM1", numNam1.Value);
				htPara.Add("TYPE", enuBangLuong_Option.Text.Trim()); //1-Theo lương tháng trước, 2-Theo lương từ bảng lương chuẩn
				htPara.Add("MA_BP", strMa_Bp);
				htPara.Add("MA_CBNV", strMa_CbNv);
				htPara.Add("MA_DVCS", Element.sysMa_DvCs);

				SQLExec.Execute("sp_HRM_CreateBangLuong", htPara, CommandType.StoredProcedure);
			}

			return true;
		}

		void btAccept_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				this.isAccept = true;
				this.Close();                
			}
		}

		void btCancel_Click(object sender, EventArgs e)
		{
			this.isAccept = false;
			this.Close();
		}
	}
}
