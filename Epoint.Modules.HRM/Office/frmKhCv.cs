using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;

namespace Epoint.Modules.HRM
{
	public partial class frmKhCv : Epoint.Lists.frmView
	{
		#region Khai bao bien

		private DataTable dtKhCv;
		private DataRow drCurrent;
		private BindingSource bdsKhCv = new BindingSource();
		private dgvControl dgvKhCv = new dgvControl();
		public bool bLookupByGroup = false;
        
		#endregion 

		#region Contructor
		public frmKhCv()
		{
			InitializeComponent();

			this.Resize += new EventHandler(ResizeEvent);            
            this.btFilter.Click += new EventHandler(btFilter_Click);
            dgvKhCv.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvKhCv_CellMouseClick);
		}	
		public override void Load()
		{
			Init();
			Build();
			FillData();
			BindingLanguage();
			base.Load();

			if (this.isLookup)
				this.ShowDialog();
			else
				this.Show();
		}

		public override void LoadLookup()
		{
			this.Load();
		}

		private void Init()
		{
			htHistory["DIEN_GIAI"] = "Kế hoạch công việc";
            strTableName = "HRKHCV";
			strCode = "MA_CBNV";
			strName = "MA_CBNV";

            dteTu_Ngay.Text = DateTime.Now.ToShortDateString();
            dteDen_Ngay.Text = DateTime.Now.ToShortDateString();
		}

		#endregion

		#region Build, FillData
		private void Build()
		{
			dgvKhCv.Dock = DockStyle.Fill;
			dgvKhCv.strZone = "KHCV";

			dgvKhCv.BuildGridView(this.isLookup);

            this.splitContainer1.Panel2.Controls.Add(dgvKhCv);
			//this.Controls.Add(dgvKhCv);
		}

		public void FillData()
		{
            
            Hashtable ht = new Hashtable();
            ht.Add("MEMBER_ID", Element.sysUser_Id.ToString());
            ht.Add("NGAY_CT1", dteTu_Ngay.Text);
            ht.Add("NGAY_CT2", dteDen_Ngay.Text);                
            ht.Add("MA_DVCS", Element.sysMa_DvCs);

            dtKhCv = SQLExec.ExecuteReturnDt("sp_GetKHCV", ht, CommandType.StoredProcedure);
            
        	bdsKhCv.DataSource = dtKhCv;
			dgvKhCv.DataSource = bdsKhCv;

			if (bdsKhCv.Count >= 0)
				bdsKhCv.Position = 0;

			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsKhCv;
			ExportControl = dgvKhCv;

			if (this.isLookup)
				this.MoveToLookupValue();
		}

		private void MoveToLookupValue()
		{//Tim den mau tin tim kiem

			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtKhCv.Rows.Count - 1; i++)
				if (((string)dtKhCv.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsKhCv.Position = i;
					break;
				}
		}

		#endregion        

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsKhCv.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsKhCv.Position >= 0)
				//drCurrent = DataTool.SQLGetDataRowByID(strTableName, strCode, (string)((DataRowView)bdsKhCv.Current)[strCode]);            
                Common.CopyDataRow(((DataRowView)bdsKhCv.Current).Row, ref drCurrent);

			if (drCurrent == null)
			{
				drCurrent = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null).NewRow();
				Common.SetDefaultDataRow(ref drCurrent);
			}

			if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
				drCurrent[strCode] = GetAutoCode((string)drCurrent[strCode]);	

			frmKhCv_Edit frmEdit = new frmKhCv_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			// Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					DataRow drNewRow = dtKhCv.NewRow();
					Common.CopyDataRow(drCurrent, drNewRow);                    

					dtKhCv.Rows.Add(drNewRow);
					bdsKhCv.Position = bdsKhCv.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsKhCv.Current).Row);
				}

                drCurrent = ((DataRowView)bdsKhCv.Current).Row;

                if (drCurrent.Table.Columns.Contains("Ten_CbNv"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("LINHANVIEN", "Ma_CbNv", drCurrent["Ma_CbNv"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv"] = drDmCbNv["Ten_CbNv"].ToString();
                }

                
                if (drCurrent.Table.Columns.Contains("Ten_CbNv_Ht"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("LINHANVIEN", "Ma_CbNv", drCurrent["Ma_CbNv_Ht"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv_Ht"] = drDmCbNv["Ten_CbNv"].ToString();
                }

                if (drCurrent.Table.Columns.Contains("Ten_Dt"))
                {
                    DataRow drDmDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", drCurrent["Ma_Dt"].ToString());
                    if (drDmDt != null)
                        drCurrent["Ten_Dt"] = drDmDt["Ten_Dt"].ToString();
                }

				dtKhCv.AcceptChanges();
			}
			//else
			//    dtKhCv.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsKhCv.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsKhCv.Current).Row;

            //Mau tin da Duyet thi khong cho xoa
            if ((Boolean)drCurrent["Duyet"] == true)
            {
                MessageBox.Show("Mẫu tin đã duyệt. Không được xóa !", "Thông báo", MessageBoxButtons.OK);
                return;
            }

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete(strTableName, drCurrent))
			{
				//Cập nhật History
				htHistory["CODE"] = drCurrent[strCode];
				htHistory["NAME"] = drCurrent[strName];
				htHistory["UPDATE_TYPE"] = "D";
				UpdateHistory();

				bdsKhCv.RemoveAt(bdsKhCv.Position);
				dtKhCv.AcceptChanges();
			}
		}

		public override void MergeID()
		{
            //if(bdsKhCv.Count <= 0)
            //    return;
				
            //drCurrent = ((DataRowView)bdsKhCv.Current).Row;
            //string strOldValue = (string)drCurrent[strCode];

            //frmMergeID frm = new frmMergeID();

            //frm.Load(strTableName, strCode, strName, strOldValue, "KhCv");			

            //if (frm.isAccept)
            //{
            //    string strNewValue = frm.strNewValue;
            //    string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
            //    if (!Common.MsgYes_No(strMsg))
            //        return;

            //    if (DataTool.SQLMergeID(strCode, strTableName, strOldValue, strNewValue))
            //    {					
            //        bdsKhCv.RemoveCurrent();
            //        bdsKhCv.Position = bdsKhCv.Find(strCode, strNewValue);
            //    }
            //}
		}

		#endregion 

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsKhCv == null || bdsKhCv.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsKhCv.Current).Row;
			DataTable dtTemp = dtKhCv.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}
		public override void  EnterProcess()
		{
			if (bdsKhCv.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsKhCv.Current).Row;
				this.Close();
			}
		}

		#endregion 

		#region Su kien

        void btFilter_Click(object sender, EventArgs e)
        {
            FillData();
        }

		private void ResizeEvent(object sender, EventArgs e)
		{
			dgvKhCv.ResizeGridView();
		}

        void dgvKhCv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string strColumnName = dgvKhCv.Columns[e.ColumnIndex].Name;

            if (e.RowIndex < 0)
                return;

            drCurrent = ((DataRowView)bdsKhCv.Current).Row;

            if (strColumnName == "DUYET" && (Boolean)drCurrent["DUYET"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_CV", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "HRKHCV";

                frm.Load(drCurrent);
            }
            else
            {
                frmDuyet frm = new frmDuyet();
                frm.Enabled = false;
            }
        }

		#endregion 
	}
	
}