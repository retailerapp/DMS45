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
	public partial class frmEmployee_UngTuyen : Epoint.Lists.frmView
	{
		#region Khai bao bien
		private tlControl tlDmCbNv = new tlControl();

		DataTable dtEmployee;
		DataTable dtGiaDinh;
		DataTable dtCongTac;
		DataTable dtDaoTao;		
		DataTable dtKhenThuong;		
        DataTable dtDanhGia;
        DataTable dtKinhNghiem;
        DataTable dtKyNang;
         

		BindingSource bdsEmployee = new BindingSource();
		BindingSource bdsGiaDinh = new BindingSource();
		BindingSource bdsCongTac = new BindingSource();
		BindingSource bdsDaoTao = new BindingSource();		
		BindingSource bdsKhenThuong = new BindingSource();		
        BindingSource bdsDanhGia = new BindingSource();
        BindingSource bdsKinhNghiem = new BindingSource();
        BindingSource bdsKyNang = new BindingSource();              	

		private DataRow drCurrent;

		#endregion

		#region Contructor

		public frmEmployee_UngTuyen()
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

			dgvGiaDinh.strZone = "GIADINH";
			dgvGiaDinh.BuildGridView();

			dgvCongTac.strZone = "CONGTAC";
			dgvCongTac.BuildGridView();

			dgvDaoTao.strZone = "DAOTAO";
			dgvDaoTao.BuildGridView();
            
			dgvKhenThuong.strZone = "KHENTHUONG";
			dgvKhenThuong.BuildGridView();			

            dgvDanhGia.strZone = "DANHGIA";
            dgvDanhGia.BuildGridView();

            dgvKinhNghiem.strZone = "KINHNGHIEM";
            dgvKinhNghiem.BuildGridView();

            dgvKyNang.strZone = "KYNANG";
            dgvKyNang.BuildGridView();            
		}

		private void FillData(string strKey)
		{
			dtEmployee = SQLExec.ExecuteReturnDt("EXEC Sp_GetDmCbNv_UngTuyen");
			bdsEmployee.DataSource = dtEmployee;
			tlDmCbNv.DataSource = bdsEmployee;
			//bdsEmployee.Position = 0;

			//GIADINH
			dtGiaDinh = DataTool.SQLGetDataTable("HRGIADINH", null, strKey, "Nam_Sinh, Loai_QHGD");
			bdsGiaDinh.DataSource = dtGiaDinh;
			dgvGiaDinh.DataSource = bdsGiaDinh;

			//CONGTAC
			dtCongTac = DataTool.SQLGetDataTable("HRCONGTAC", null, strKey, "Ngay_Begin, Ngay_End");
			bdsCongTac.DataSource = dtCongTac;
			dgvCongTac.DataSource = bdsCongTac;

			//DAOTAO
			dtDaoTao = DataTool.SQLGetDataTable("HRDAOTAO", null, strKey, "Ngay_Begin, Ngay_End");
			bdsDaoTao.DataSource = dtDaoTao;
			dgvDaoTao.DataSource = bdsDaoTao;

			//KHENTHUONG
			dtKhenThuong = DataTool.SQLGetDataTable("HRKHENTHUONG", null, strKey, "Ngay_QD, Ngay_HL");
			bdsKhenThuong.DataSource = dtKhenThuong;
			dgvKhenThuong.DataSource = bdsKhenThuong;

            //DANHGIA
            dtDanhGia = DataTool.SQLGetDataTable("HRDANHGIA", null, strKey, "Ngay_DG");            
            bdsDanhGia.DataSource = dtDanhGia;
            dgvDanhGia.DataSource = bdsDanhGia;

            //KINHNGHIEM
            dtKinhNghiem = DataTool.SQLGetDataTable("HRKINHNGHIEM", null, strKey, "Ngay_Begin, Ngay_End");
            bdsKinhNghiem.DataSource = dtKinhNghiem;
            dgvKinhNghiem.DataSource = bdsKinhNghiem;

            //KYNANG
            dtKyNang = DataTool.SQLGetDataTable("HRKYNANG", null, strKey, "Ky_Nang");
            bdsKyNang.DataSource = dtKyNang;
            dgvKyNang.DataSource = bdsKyNang;
            
            //Tham số các khoản thu nhập
			string strSQLExec =
				"SELECT T1.*, T2.Ten_Tn, T2.Dvt FROM HRPARAVALUE0 T1 LEFT JOIN HRPARALIST T2 ON T1.Ma_Tn = T2.Ma_Tn " +
					" ORDER BY Ngay_Ap";

			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsEmployee;
			ExportControl = tlDmCbNv;

			tlDmCbNv.Expand = (bool)SQLExec.ExecuteReturnValue("SELECT Expand FROM SYSZONE WHERE ZONE = '" + tlDmCbNv.strZone + "'");
		}

		private void BindingData()
		{
			foreach (Control ctrl in tpTTNV.Controls)
			{
                if (ctrl.GetType() == typeof(txtTextBox) || ctrl.GetType() == typeof(TextBox) || ctrl.GetType() == typeof(txtDateTime) || ctrl.GetType() == typeof(cboControl) || ctrl.GetType() == typeof(ComboBox) || ctrl.GetType() == typeof(chkControl))
				{
					string strFieldName = ctrl.Name.Substring(3);

                    if (((DataTable)bdsEmployee.DataSource).Columns.Contains(strFieldName))
                    {
                        ctrl.DataBindings.Add("Text", bdsEmployee, strFieldName);
                    }
				}                
			}

			//picHinh.DataBindings.Add("Image", bdsEmployee, "Hinh");
		}

		#endregion

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (tabEmployee.Focused || pageEmployee.Focused || tlDmCbNv.Focused)
				this.EditEmployee(enuNew_Edit);
			else if (tabDetail.SelectedTab == pageGiaDinh || dgvGiaDinh.Focused)
				this.EditGiaDinh(enuNew_Edit);			
            else if (tabDetail.SelectedTab == pageDaoTao || dgvDaoTao.Focused)
                this.EditDaoTao(enuNew_Edit);
			else if (tabDetail.SelectedTab == pageCongTac || dgvCongTac.Focused)
				this.EditCongTac(enuNew_Edit);			
			else if (tabDetail.SelectedTab == pageKhenThuong || dgvKhenThuong.Focused)
				this.EditKhenThuong(enuNew_Edit);						
            else if (tabDetail.SelectedTab == pageDanhGia || dgvDanhGia.Focused)
                this.EditDanhGia(enuNew_Edit);
            else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
                this.EditKinhNghiem(enuNew_Edit);
            else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
                this.EditKyNang(enuNew_Edit);            
		}

		public override void Delete()
		{
			if (tabEmployee.Focused || pageEmployee.Focused || tlDmCbNv.Focused)
				this.DeleteEmployee();
			else if (tabDetail.SelectedTab == pageGiaDinh || dgvGiaDinh.Focused)
				this.DeleteGiaDinh();
			else if (tabDetail.SelectedTab == pageDaoTao || dgvDaoTao.Focused)
				this.DeleteDaoTao();
			else if (tabDetail.SelectedTab == pageCongTac || dgvCongTac.Focused)
				this.DeleteCongTac();			
			else if (tabDetail.SelectedTab == pageKhenThuong || dgvKhenThuong.Focused)
				this.DeleteKhenThuong();						
            else if (tabDetail.SelectedTab == pageDanhGia || dgvDanhGia.Focused)
                this.DeleteDanhGia();
            else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
                this.DeleteKinhNghiem();
            else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
                this.DeleteKyNang();            
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
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.EditHopDong(enuEdit.New);        
        //    else if (tabDetail.SelectedTab == pageDanhGia || dgvDanhGia.Focused)
        //        this.EditDanhGia(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.EditKinhNghiem(enuEdit.New);
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.EditKyNang(enuEdit.New);        
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
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.EditHopDong(enuEdit.Edit);        
        //    else if (tabDetail.SelectedTab == pageDanhGia || dgvDanhGia.Focused)
        //        this.EditDanhGia(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.EditKinhNghiem(enuEdit.Edit);
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.EditKyNang(enuEdit.Edit);        
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
        //    else if (tabDetail.SelectedTab == pageHopDong || dgvHopDong.Focused)
        //        this.DeleteHopDong();        
        //    else if (tabDetail.SelectedTab == pageDanhGia || dgvDanhGia.Focused)
        //        this.DeleteDanhGia();
        //    else if (tabDetail.SelectedTab == pageKinhNghiem || dgvKinhNghiem.Focused)
        //        this.DeleteKinhNghiem();
        //    else if (tabDetail.SelectedTab == pageKyNang || dgvKyNang.Focused)
        //        this.DeleteKyNang();        
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

			frmUngTuyen_Edit frmEdit = new frmUngTuyen_Edit();
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

		void EditGiaDinh(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0)
				return;

			if (bdsGiaDinh.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

			//Copy hang hien tai            
			if (bdsGiaDinh.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsGiaDinh.Current).Row, ref drCurrent);
			else
				drCurrent = dtGiaDinh.NewRow();

			drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];
			frmGiaDinh_Edit frmEdit = new frmGiaDinh_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsGiaDinh.Position >= 0)
						dtGiaDinh.ImportRow(drCurrent);
					else
						dtGiaDinh.Rows.Add(drCurrent);

					bdsGiaDinh.Position = bdsGiaDinh.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsGiaDinh.Current).Row);

				dtGiaDinh.AcceptChanges();
			}
			else
				dtGiaDinh.RejectChanges();
		}

		void EditCongTac(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0)
				return;

			if (bdsCongTac.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

			//Copy hang hien tai            
			if (bdsCongTac.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsCongTac.Current).Row, ref drCurrent);
			else
				drCurrent = dtCongTac.NewRow();

			drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];
			frmCongTac_Edit frmEdit = new frmCongTac_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsCongTac.Position >= 0)
						dtCongTac.ImportRow(drCurrent);
					else
						dtCongTac.Rows.Add(drCurrent);

					bdsCongTac.Position = bdsCongTac.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsCongTac.Current).Row);

				dtCongTac.AcceptChanges();
			}
			else
				dtCongTac.RejectChanges();
		}

		void EditDaoTao(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0)
				return;

			if (bdsDaoTao.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

			//Copy hang hien tai            
			if (bdsDaoTao.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsDaoTao.Current).Row, ref drCurrent);
			else
				drCurrent = dtDaoTao.NewRow();

			drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];
			frmDaoTao_Edit frmEdit = new frmDaoTao_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsDaoTao.Position >= 0)
						dtDaoTao.ImportRow(drCurrent);
					else
						dtDaoTao.Rows.Add(drCurrent);

					bdsDaoTao.Position = bdsDaoTao.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsDaoTao.Current).Row);

				dtDaoTao.AcceptChanges();
			}
			else
				dtDaoTao.RejectChanges();
		}

		void EditKhenThuong(enuEdit enuNew_Edit)
		{
			if (bdsEmployee.Position < 0)
				return;

			if (bdsKhenThuong.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

			//Copy hang hien tai            
			if (bdsKhenThuong.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsKhenThuong.Current).Row, ref drCurrent);
			else
				drCurrent = dtKhenThuong.NewRow();

			drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];
			frmKhenThuong_Edit frmEdit = new frmKhenThuong_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsKhenThuong.Position >= 0)
						dtKhenThuong.ImportRow(drCurrent);
					else
						dtKhenThuong.Rows.Add(drCurrent);

					bdsKhenThuong.Position = bdsKhenThuong.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsKhenThuong.Current).Row);

				dtKhenThuong.AcceptChanges();
			}
			else
				dtKhenThuong.RejectChanges();
		}		
        void EditDanhGia(enuEdit enuNew_Edit)
        {
            if (bdsEmployee.Position < 0)
                return;

            if (bdsDanhGia.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

            //Copy hang hien tai            
            if (bdsDanhGia.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDanhGia.Current).Row, ref drCurrent);
            else
                drCurrent = dtDanhGia.NewRow();

            drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];

            frmDanhGia_Edit frmEdit = new frmDanhGia_Edit();
            frmEdit.Load(enuNew_Edit, drCurrent);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsDanhGia.Position >= 0)
                        dtDanhGia.ImportRow(drCurrent);
                    else
                        dtDanhGia.Rows.Add(drCurrent);

                    bdsDanhGia.Position = bdsDanhGia.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsDanhGia.Current).Row);

                dtDanhGia.AcceptChanges();
            }
            else
                dtDanhGia.RejectChanges();
        }
        void EditKinhNghiem(enuEdit enuNew_Edit)
        {
            if (bdsEmployee.Position < 0)
                return;

            if (bdsKinhNghiem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

            //Copy hang hien tai            
            if (bdsKinhNghiem.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsKinhNghiem.Current).Row, ref drCurrent);
            else
                drCurrent = dtKinhNghiem.NewRow();

            drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];

            frmKinhNghiem_Edit frmEdit = new frmKinhNghiem_Edit();
            frmEdit.Load(enuNew_Edit, drCurrent);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsKinhNghiem.Position >= 0)
                        dtKinhNghiem.ImportRow(drCurrent);
                    else
                        dtKinhNghiem.Rows.Add(drCurrent);

                    bdsKinhNghiem.Position = bdsKinhNghiem.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsKinhNghiem.Current).Row);

                dtKinhNghiem.AcceptChanges();
            }
            else
                dtKinhNghiem.RejectChanges();
        }
        void EditKyNang(enuEdit enuNew_Edit)
        {
            if (bdsEmployee.Position < 0)
                return;

            if (bdsKyNang.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            DataRow drEmployee = ((DataRowView)bdsEmployee.Current).Row;

            //Copy hang hien tai            
            if (bdsKyNang.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsKyNang.Current).Row, ref drCurrent);
            else
                drCurrent = dtKyNang.NewRow();

            drCurrent["Ma_CbNv"] = (string)drEmployee["Ma_CbNv"];

            frmKyNang_Edit frmEdit = new frmKyNang_Edit();
            frmEdit.Load(enuNew_Edit, drCurrent);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsKyNang.Position >= 0)
                        dtKyNang.ImportRow(drCurrent);
                    else
                        dtKyNang.Rows.Add(drCurrent);

                    bdsKyNang.Position = bdsKyNang.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsKyNang.Current).Row);

                dtKyNang.AcceptChanges();
            }
            else
                dtKyNang.RejectChanges();
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

			if (DataTool.SQLDelete("HRUNGTUYEN", drCurrent))
			{
				bdsEmployee.RemoveAt(bdsEmployee.Position);
				dtEmployee.AcceptChanges();
			}
		}

		void DeleteGiaDinh()
		{
			if (bdsGiaDinh.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsGiaDinh.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRGIADINH", drCurrent))
			{
				bdsGiaDinh.RemoveAt(bdsGiaDinh.Position);
				dtGiaDinh.AcceptChanges();
			}
		}

		void DeleteCongTac()
		{
			if (bdsCongTac.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsCongTac.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRCONGTAC", drCurrent))
			{
				bdsCongTac.RemoveAt(bdsCongTac.Position);
				dtCongTac.AcceptChanges();
			}
		}

		void DeleteDaoTao()
		{
			if (bdsDaoTao.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsDaoTao.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRDAOTAO", drCurrent))
			{
				bdsDaoTao.RemoveAt(bdsDaoTao.Position);
				dtDaoTao.AcceptChanges();
			}
		}
        	
		void DeleteKhenThuong()
		{
			if (bdsKhenThuong.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsKhenThuong.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRKHENTHUONG", drCurrent))
			{
				bdsKhenThuong.RemoveAt(bdsKhenThuong.Position);
				dtKhenThuong.AcceptChanges();
			}
		}		        
        void DeleteDanhGia()
        {
            if (bdsDanhGia.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsDanhGia.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("HRDANHGIA", drCurrent))
            {
                bdsDanhGia.RemoveAt(bdsDanhGia.Position);
                dtDanhGia.AcceptChanges();
            }
        }
        void DeleteKinhNghiem()
        {
            if (bdsKinhNghiem.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsKinhNghiem.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("HRKINHNGHIEM", drCurrent))
            {
                bdsKinhNghiem.RemoveAt(bdsKinhNghiem.Position);
                dtKinhNghiem.AcceptChanges();
            }
        }
        void DeleteKyNang()
        {
            if (bdsKyNang.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsKyNang.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("HRKYNANG", drCurrent))
            {
                bdsKyNang.RemoveAt(bdsKyNang.Position);
                dtKyNang.AcceptChanges();
            }
        }        
		public override void MergeID()
		{
			if (bdsEmployee.Count <= 0)
				return;

			drCurrent = ((DataRowView)bdsEmployee.Current).Row;
			string strOldValue = (string)drCurrent["Ma_CbNv"];

			frmMergeID frm = new frmMergeID();

			frm.Load("HRUNGTUYEN", "Ma_CbNv", "Ten_CbNv", strOldValue, "DMCBNV");

			if (frm.isAccept)
			{
				string strNewValue = frm.strNewValue;
				string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
				if (!Common.MsgYes_No(strMsg))
					return;

				if (DataTool.SQLMergeID("Ma_CbNv", "HRUNGTUYEN", strOldValue, strNewValue))
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

            bdsCongTac.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsDanhGia.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsDaoTao.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsGiaDinh.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";            
            bdsKhenThuong.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsKinhNghiem.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";
            bdsKyNang.Filter = "Ma_CbNv = '" + drCurrent["Ma_CbNv"] + "'";            

			object objPic = SQLExec.ExecuteReturnValue("SELECT Hinh FROM HRUNGTUYEN WHERE Ma_CbNv = '" + (string)drCurrent["Ma_CbNv"] + "'");
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
