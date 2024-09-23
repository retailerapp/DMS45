using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using Epoint.Systems;
using Epoint.Systems.Elements;

namespace Epoint.Modules.Salary
{
	public partial class frmBangLuong_Edit : frmView
	{
		#region Khai bao bien
		DataTable dtSalary_Edit;
		DataRow drEdit, drCurrent;
		BindingSource bdsSalary_Edit = new BindingSource();
		int iThang;

		public bool isAccept;
		#endregion

		#region Contructor

		public frmBangLuong_Edit()
		{
			InitializeComponent();
			
			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
		}

		public void Load(DataRow drEdit, int iThang)
		{
			this.drEdit = drEdit;
			this.iThang = iThang;

			this.Build();
			this.FillData();

			this.BindingLanguage();

			this.Text = "Bảng lương: " + (string)drEdit["Ma_CbNv"] + " - " + (string)drEdit["Ten_CbNv"];

			this.ShowDialog();
		}

		#endregion

		#region Build, FillData

		private void Build()
		{
			dgvSalary_Edit.strZone = "SALARY_EDIT";
			dgvSalary_Edit.BuildGridView(this.isLookup);
			dgvSalary_Edit.ResizeGridView();

			dgvSalary_Edit.ReadOnly = false;
			foreach (DataGridViewColumn dgvc in dgvSalary_Edit.Columns)
			{
				if (dgvc.DataPropertyName == "TIEN")
					dgvc.ReadOnly = false;
				else
					dgvc.ReadOnly = true;
			}
		}

		private void FillData()
		{
			string strMa_CbNv = (string)drEdit["Ma_CbNv"];
			string strSQLExec =
				"SELECT T1.Ma_Tn, T1.Ten_Tn, T1.Dvt, T2.Tien" +
					" FROM HRPARALIST T1 LEFT JOIN " +
							"(SELECT Ma_Tn, SUM(Tien) AS Tien " +
								" FROM HRSALARY " +
								" WHERE YEAR(Ngay_Ct) = " + Element.sysWorkingYear.ToString().Trim() + " AND MONTH(Ngay_Ct) = " + iThang.ToString().Trim() + " AND Ma_CbNv = '" + strMa_CbNv + "' AND Ma_Dvcs = '" + Element.sysMa_DvCs + "' GROUP BY Ma_Tn) T2 " +
						" ON T1.Ma_Tn = T2.Ma_Tn " +
					" WHERE T1.Is_Input = 1";

			dtSalary_Edit = SQLExec.ExecuteReturnDt(strSQLExec);

			bdsSalary_Edit.DataSource = dtSalary_Edit;
			dgvSalary_Edit.DataSource = bdsSalary_Edit;
		}

		private bool FormCheckValid()
		{
			if ((string)drEdit["Ma_CbNv"] == string.Empty)
			{
				Common.MsgCancel("Chưa khai báo Mã nhân viên!");
				return false;
			}

			if (iThang < 0 || iThang > 12)
				return false;

			return true;
		}

		private bool Save()
		{
			Hashtable htPara = new Hashtable();

			htPara.Add("NAM", Element.sysWorkingYear);
			htPara.Add("THANG", iThang);
			htPara.Add("MA_CBNV", drEdit["Ma_CbNv"]);
			htPara.Add("MA_DVCS", Element.sysMa_DvCs);
			htPara.Add("MA_TN", string.Empty);
			htPara.Add("TIEN", 0);

			foreach (DataRow dr in dtSalary_Edit.Rows)
			{
				string strMa_Tn = (string)dr["Ma_Tn"];
				double dbTien = Convert.ToDouble(dr["Tien"]);
				htPara["MA_TN"] = strMa_Tn;
				htPara["TIEN"] = dbTien;

				if (SQLExec.Execute("sp_HRM_SaveBangLuong", htPara, CommandType.StoredProcedure))
				{
					if (drEdit.Table.Columns.Contains(strMa_Tn))
						drEdit[strMa_Tn] = dbTien;
				}
			}
			drEdit.AcceptChanges();

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