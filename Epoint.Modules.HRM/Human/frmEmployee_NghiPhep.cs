using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems;
using Epoint.Lists;
using Epoint.Controllers;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Controls;
using Epoint.Systems.Customizes;
using Epoint.Systems.Librarys;
using Epoint.Systems.Elements;

namespace Epoint.Modules.HRM
{
	public partial class frmEmployee_NghiPhep : Epoint.Lists.frmView
	{
		#region Khai bao bien
		private tlControl tlDmCbNv = new tlControl();

		DataTable dtEmployee;		
		DataTable dtNghiPhep;        
        DataTable dtPhepNam;        
        
		BindingSource bdsEmployee = new BindingSource();		
		BindingSource bdsNghiPhep = new BindingSource();        
        BindingSource bdsPhepNam = new BindingSource();        

		private DataRow drCurrent;

		#endregion

		#region Contructor

		public frmEmployee_NghiPhep()
		{
			InitializeComponent();

			bdsEmployee.PositionChanged += new EventHandler(bdsEmployee_PositionChanged);

            //btNew.Click += new EventHandler(btNew_Click);
            //btEdit.Click += new EventHandler(btEdit_Click);
            //btDelete.Click += new EventHandler(btDelete_Click);            
		}
		public override void Load()
		{
			this.Build();
			this.FillData(string.Empty);
			this.BindingData();
			this.BindingLanguage();

			this.Show();
		}

		#endregion

		#region Build, FillData

		private void Build()
		{
			tlDmCbNv.KeyFieldName = "MA_CBNV";
			tlDmCbNv.ParentFieldName = "MA_BP";
			tlDmCbNv.Dock = DockStyle.Fill;
            
			tlDmCbNv.strZone = "EMPLOYEE";
			tlDmCbNv.BuildTreeList(this.isLookup);

            this.splControl1.Visible = false;
			this.pageEmployee.Controls.Add(tlDmCbNv);			

			dgvNghiPhep.strZone = "NGHIPHEP";
			dgvNghiPhep.BuildGridView();

            dgvPhepNam.strZone = "PHEPNAM";
            dgvPhepNam.BuildGridView();            
		}

		private void FillData(string strKey)
		{
			dtEmployee = SQLExec.ExecuteReturnDt("EXEC Sp_GetDmCbNv");
			bdsEmployee.DataSource = dtEmployee;
			tlDmCbNv.DataSource = bdsEmployee;
			//bdsEmployee.Position = 0;

			//NGHIPHEP
			dtNghiPhep = DataTool.SQLGetDataTable("HRNGHIPHEP", null, strKey, "Ngay_Begin, Ngay_End");
			bdsNghiPhep.DataSource = dtNghiPhep;
			dgvNghiPhep.DataSource = bdsNghiPhep;
                        
            //PHEPNAM
            dtPhepNam = DataTool.SQLGetDataTable("HRPHEPNAM", null, strKey, "Ma_CbNv");
            bdsPhepNam.DataSource = dtPhepNam;
            dgvPhepNam.DataSource = bdsPhepNam;
            
			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsEmployee;
			ExportControl = tlDmCbNv;

			tlDmCbNv.Expand = (bool)SQLExec.ExecuteReturnValue("SELECT Expand FROM SYSZONE WHERE ZONE = '" + tlDmCbNv.strZone + "'");
		}

		private void BindingData()
		{
			foreach (Control ctrl in tpTTNV.Controls)
			{
                if (ctrl.GetType() == typeof(txtTextBox) || ctrl.GetType() == typeof(TextBox) || ctrl.GetType() == typeof(txtDateTime) || ctrl.GetType() == typeof(cboControl) || ctrl.GetType() == typeof(ComboBox))
				{
					string strFieldName = ctrl.Name.Substring(3);

					if (((DataTable)bdsEmployee.DataSource).Columns.Contains(strFieldName))
						ctrl.DataBindings.Add("Text", bdsEmployee, strFieldName);
				}                
			}

			//picHinh.DataBindings.Add("Image", bdsEmployee, "Hinh");
		}

		#endregion

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{            
			if (tabDetail.SelectedTab == pageNghiPhep || dgvNghiPhep.Focused)
				this.EditNghiPhep(enuNew_Edit);            
            else if (tabDetail.SelectedTab == pagePhepNam || dgvPhepNam.Focused)
                this.EditPhepNam(enuNew_Edit);            
		}

		public override void Delete()
		{         
		    if (tabDetail.SelectedTab == pageNghiPhep || dgvNghiPhep.Focused)
				this.DeleteNghiPhep();            
            else if (tabDetail.SelectedTab == pagePhepNam || dgvPhepNam.Focused)
                this.DeletePhepNam();            
		}
        //void btNew_Click(object sender, EventArgs e)
        //{   
        //    if (tabEmployee.Focused || pageEmployee.Focused || tlDmCbNv.Focused)
        //        this.EditEmployee(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageGiaDinh || dgvGiaDinh.Focused)
        //        this.EditGiaDinh(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageDaoTao || dgvDaoTao.Focused)
        //        this.EditDaoTao(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageCongTac || dgvCongTac.Focused)
        //        this.EditCongTac(enuEdit.New);        
        //    else if (tabDetail.SelectedTab == pageKhenThuong || dgvKhenThuong.Focused)
        //        this.EditKhenThuong(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.EditHopDong(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageNghiPhep || dgvNghiPhep.Focused)
        //        this.EditNghiPhep(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageBaoHiem || dgvBaoHiem.Focused)
        //        this.EditBaoHiem(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageNghiThaiSan || dgvNghiThaiSan.Focused)
        //        this.EditNghiThaiSan(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.EditKinhNghiem(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.EditKyNang(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageTuyenDung || dgvTuyenDung.Focused)
        //        this.EditTuyenDung(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageThue || dgvThue.Focused)
        //        this.EditThue(enuEdit.New);            
        //}
        //void btEdit_Click(object sender, EventArgs e)
        //{   
        //    if (tabEmployee.Focused || pageEmployee.Focused || tlDmCbNv.Focused)
        //        this.EditEmployee(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageGiaDinh || dgvGiaDinh.Focused)
        //        this.EditGiaDinh(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageDaoTao || dgvDaoTao.Focused)
        //        this.EditDaoTao(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageCongTac || dgvCongTac.Focused)
        //        this.EditCongTac(enuEdit.Edit);        
        //    else if (tabDetail.SelectedTab == pageKhenThuong || dgvKhenThuong.Focused)
        //        this.EditKhenThuong(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.EditHopDong(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageNghiPhep || dgvNghiPhep.Focused)
        //        this.EditNghiPhep(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageBaoHiem || dgvBaoHiem.Focused)
        //        this.EditBaoHiem(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageNghiThaiSan || dgvNghiThaiSan.Focused)
        //        this.EditNghiThaiSan(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.EditKinhNghiem(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.EditKyNang(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageTuyenDung || dgvTuyenDung.Focused)
        //        this.EditTuyenDung(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageThue || dgvThue.Focused)
        //        this.EditThue(enuEdit.Edit);            
        //}

        //void btDelete_Click(object sender, EventArgs e)
        //{
        //    if (tabEmployee.Focused || pageEmployee.Focused || tlDmCbNv.Focused)
        //        this.DeleteEmployee();
        //    else if (tabDetail.SelectedTab == pageGiaDinh || dgvGiaDinh.Focused)
        //        this.DeleteGiaDinh();
        //    else if (tabDetail.SelectedTab == pageDaoTao || dgvDaoTao.Focused)
        //        this.DeleteDaoTao();
        //    else if (tabDetail.SelectedTab == pageCongTac || dgvCongTac.Focused)
        //        this.DeleteCongTac();        
        //    else if (tabDetail.SelectedTab == pageKhenThuong || dgvKhenThuong.Focused)
        //        this.DeleteKhenThuong();
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.DeleteHopDong();
        //    else if (tabDetail.SelectedTab == pageNghiPhep || dgvNghiPhep.Focused)
        //        this.DeleteNghiPhep();
        //    else if (tabDetail.SelectedTab == pageBaoHiem || dgvBaoHiem.Focused)
        //        this.DeleteBaoHiem();
        //    else if (tabDetail.SelectedTab == pageNghiThaiSan || dgvNghiThaiSan.Focused)
        //        this.DeleteNghiThaiSan();
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.DeleteKinhNghiem();
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.DeleteKyNang();
        //    else if (tabDetail.SelectedTab == pageTuyenDung || dgvTuyenDung.Focused)
        //        this.DeleteTuyenDung();
        //    else if (tabDetail.SelectedTab == pageThue || dgvThue.Focused)
        //        this.DeleteThue();               
        //}

		void EditEmployee(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai
			if (bdsEmployee.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsEmployee.Current).Row, ref drCurrent);
			else
				drCurrent = dtEmployee.NewRow();

			frmEmployee_Edit frmEdit = new frmEmployee_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsEmployee.Position >= 0)
						dtEmployee.ImportRow(drCurrent);
					else
						dtEmployee.Rows.Add(drCurrent);

					bdsEmployee.Position = bdsEmployee.Find("MA_CBNV", drCurrent["MA_CBNV"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsEmployee.Current).Row);

				dtEmployee.AcceptChanges();
			}
		}		
		void EditNghiPhep(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0)
				return;

			if (bdsNghiPhep.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

			//Copy hang hien tai            
			if (bdsNghiPhep.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsNghiPhep.Current).Row, ref drCurrent);
			else
				drCurrent = dtNghiPhep.NewRow();

			drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];

			frmNghiPhep_Edit frmEdit = new frmNghiPhep_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsNghiPhep.Position >= 0)
						dtNghiPhep.ImportRow(drCurrent);
					else
						dtNghiPhep.Rows.Add(drCurrent);

					bdsNghiPhep.Position = bdsNghiPhep.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsNghiPhep.Current).Row);

				dtNghiPhep.AcceptChanges();
			}
			else
				dtNghiPhep.RejectChanges();
		}
        
        void EditPhepNam(enuEdit enuNew_Edit)
        {
            if (bdsEmployee.Position < 0)
                return;

            if (bdsPhepNam.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

            //Copy hang hien tai            
            if (bdsPhepNam.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsPhepNam.Current).Row, ref drCurrent);
            else
                drCurrent = dtPhepNam.NewRow();

            drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];

            frmPhepNam_Edit frmEdit = new frmPhepNam_Edit();
            frmEdit.Load(enuNew_Edit, drCurrent);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsPhepNam.Position >= 0)
                        dtPhepNam.ImportRow(drCurrent);
                    else
                        dtPhepNam.Rows.Add(drCurrent);

                    bdsPhepNam.Position = bdsPhepNam.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsPhepNam.Current).Row);

                dtPhepNam.AcceptChanges();
            }
            else
                dtPhepNam.RejectChanges();
        }        
		void DeleteEmployee()
		{
			if (bdsEmployee.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsEmployee.Current).Row;

			if ((bool)drCurrent["Is_BoPhan"])
				return;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("LINHANVIEN", drCurrent))
			{
				bdsEmployee.RemoveAt(bdsEmployee.Position);
				dtEmployee.AcceptChanges();
			}
		}		
		void DeleteNghiPhep()
		{
			if (bdsNghiPhep.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsNghiPhep.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRNGHIPHEP", drCurrent))
			{
				bdsNghiPhep.RemoveAt(bdsNghiPhep.Position);
				dtNghiPhep.AcceptChanges();
			}
		}

        void DeletePhepNam()
        {
            if (bdsPhepNam.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsPhepNam.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("HRPHEPNAM", drCurrent))
            {
                bdsPhepNam.RemoveAt(bdsPhepNam.Position);
                dtPhepNam.AcceptChanges();
            }
        }        
		public override void MergeID()
		{
			if (bdsEmployee.Count <= 0)
				return;

			drCurrent = ((DataRowView)bdsEmployee.Current).Row;
			string strOldValue = (string)drCurrent["Ma_CbNv"];

			frmMergeID frm = new frmMergeID();

			frm.Load("LINHANVIEN", "Ma_CbNv", "Ten_CbNv", strOldValue, "DMCBNV");

			if (frm.isAccept)
			{
				string strNewValue = frm.strNewValue;
				string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
				if (!Common.MsgYes_No(strMsg))
					return;

				if (DataTool.SQLMergeID("Ma_CbNv", "LINHANVIEN", strOldValue, strNewValue))
				{
					bdsEmployee.RemoveCurrent();
					bdsEmployee.Position = bdsEmployee.Find("Ma_CbNv", strNewValue);
				}
			}
		}

		#endregion

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsEmployee == null || bdsEmployee.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsEmployee.Current).Row;
			DataTable dtTemp = dtEmployee.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void EnterProcess()
		{
			if (bdsEmployee.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsEmployee.Current).Row;
				this.Close();
			}
		}

		#endregion

		#region Su kien

		void bdsEmployee_PositionChanged(object sender, EventArgs e)
		{
			drCurrent = ((DataRowView)bdsEmployee.Current).Row;                     
             
            bdsNghiPhep.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsPhepNam.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";                        

			object objPic = SQLExec.ExecuteReturnValue("SELECT Hinh FROM LINHANVIEN WHERE Ma_CbNv = '" + (string)drCurrent["Ma_CbNv"] + "'");
			Byte[] bytePic = (Byte[])objPic;
			if (objPic != null && objPic != DBNull.Value && bytePic.Length != 0)
			{
				byte[] barrImg = bytePic;
				string strFileName = Convert.ToString(DateTime.Now.ToFileTime());
				System.IO.FileStream fs = new System.IO.FileStream(strFileName, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write);
				fs.Write(barrImg, 0, barrImg.Length);
				fs.Flush();
				fs.Close();
				picHinh.Image = Image.FromFile(strFileName);
				picHinh.SizeMode = PictureBoxSizeMode.Zoom;
			}
			else
				picHinh.Image = null;
		}

        //void btDetailNew_Click(object sender, EventArgs e)
        //{
        //    if (tabDetail.SelectedTab == pageGiaDinh)
        //        this.EditGiaDinh(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageDaoTao)
        //        this.EditDaoTao(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageCongTac)
        //        this.EditCongTac(enuEdit.New);        
        //    else if (tabDetail.SelectedTab == pageKhenThuong)
        //        this.EditKhenThuong(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageHopDong)
        //        this.EditHopDong(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageNghiPhep)
        //        this.EditNghiPhep(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageTsTn0)
        //        this.EditTsTn0(enuEdit.New);
        //}

        //void btDetailEdit_Click(object sender, EventArgs e)
        //{
        //    if (tabDetail.SelectedTab == pageGiaDinh)
        //        this.EditGiaDinh(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageDaoTao)
        //        this.EditDaoTao(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageCongTac)
        //        this.EditCongTac(enuEdit.Edit);        
        //    else if (tabDetail.SelectedTab == pageKhenThuong)
        //        this.EditKhenThuong(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageHopDong)
        //        this.EditHopDong(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageNghiPhep)
        //        this.EditNghiPhep(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageTsTn0)
        //        this.EditTsTn0(enuEdit.Edit);
        //}

        //void btDetailDelete_Click(object sender, EventArgs e)
        //{
        //    if (tabDetail.SelectedTab == pageGiaDinh)
        //        this.DeleteGiaDinh();
        //    else if (tabDetail.SelectedTab == pageDaoTao)
        //        this.DeleteDaoTao();
        //    else if (tabDetail.SelectedTab == pageCongTac)
        //        this.DeleteCongTac();        
        //    else if (tabDetail.SelectedTab == pageKhenThuong)
        //        this.DeleteKhenThuong();
        //    else if (tabDetail.SelectedTab == pageHopDong)
        //        this.DeleteHopDong();
        //    else if (tabDetail.SelectedTab == pageNghiPhep)
        //        this.DeleteNghiPhep();
        //    else if (tabDetail.SelectedTab == pageTsTn0)
        //        this.DeleteTsTn0();
        //}

        //void btNew_Click(object sender, EventArgs e)
        //{
        //    this.EditEmployee(enuEdit.New);
        //}

        //void btEdit_Click(object sender, EventArgs e)
        //{
        //    this.EditEmployee(enuEdit.Edit);
        //}

        //void btDelete_Click(object sender, EventArgs e)
        //{
        //    this.DeleteEmployee();
        //}

		#endregion
	}
}
