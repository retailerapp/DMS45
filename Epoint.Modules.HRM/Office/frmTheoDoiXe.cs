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
	public partial class frmTheoDoiXe : Epoint.Lists.frmView
	{
		#region Khai bao bien

		private DataTable dtTheoDoiXe;
		private DataRow drCurrent;
		private BindingSource bdsTheoDoiXe = new BindingSource();
		private dgvControl dgvTheoDoiXe = new dgvControl();
		public bool bLookupByGroup = false;

        string strMa_CbNv = string.Empty;
		#endregion 

		#region Contructor
		public frmTheoDoiXe()
		{
			InitializeComponent();

			this.Resize += new EventHandler(ResizeEvent);
            dgvTheoDoiXe.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvTheoDoiXe_CellMouseClick);
            this.btFilter.Click += new EventHandler(btFilter_Click);
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
			htHistory["DIEN_GIAI"] = "Phiếu mượn thiết bị";
            strTableName = "L14LTDX";
			strCode = "MA_CBNV";
			strName = "MA_CBNV";

            dteTu_Ngay.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            dteDen_Ngay.Text = DateTime.Now.ToShortDateString();
		}

		#endregion

		#region Build, FillData
		private void Build()
		{
			dgvTheoDoiXe.Dock = DockStyle.Fill;
			dgvTheoDoiXe.strZone = "THEODOIXE";

			dgvTheoDoiXe.BuildGridView(this.isLookup);

            this.splitContainer1.Panel2.Controls.Add(dgvTheoDoiXe);
			//this.Controls.Add(dgvTheoDoiXe);
		}

		public void FillData()	
		{
            Hashtable ht = new Hashtable();
            ht.Add("MEMBER_ID", Element.sysUser_Id.ToString());
            ht.Add("NGAY_CT1", dteTu_Ngay.Text);
            ht.Add("NGAY_CT2", dteDen_Ngay.Text);
            ht.Add("MA_DVCS", Element.sysMa_DvCs);

            dtTheoDoiXe = SQLExec.ExecuteReturnDt("sp_GetLTDX", ht, CommandType.StoredProcedure);

			bdsTheoDoiXe.DataSource = dtTheoDoiXe;
			dgvTheoDoiXe.DataSource = bdsTheoDoiXe;

			if (bdsTheoDoiXe.Count >= 0)
				bdsTheoDoiXe.Position = 0;

			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsTheoDoiXe;
			ExportControl = dgvTheoDoiXe;

			if (this.isLookup)
				this.MoveToLookupValue();

		}

		private void MoveToLookupValue()
		{//Tim den mau tin tim kiem

			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtTheoDoiXe.Rows.Count - 1; i++)
				if (((string)dtTheoDoiXe.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsTheoDoiXe.Position = i;
					break;
				}
		}

		#endregion 

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsTheoDoiXe.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsTheoDoiXe.Position >= 0)
				//drCurrent = DataTool.SQLGetDataRowByID(strTableName, strCode, (string)((DataRowView)bdsTheoDoiXe.Current)[strCode]);
                Common.CopyDataRow(((DataRowView)bdsTheoDoiXe.Current).Row, ref drCurrent);

			if (drCurrent == null)
			{
				drCurrent = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null).NewRow();
				Common.SetDefaultDataRow(ref drCurrent);
			}

			if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
				drCurrent[strCode] = GetAutoCode((string)drCurrent[strCode]);


			frmTheoDoiXe_Edit frmEdit = new frmTheoDoiXe_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			// Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					DataRow drNewRow = dtTheoDoiXe.NewRow();
					Common.CopyDataRow(drCurrent, drNewRow);

                    DataTable dt_CbNv = SQLExec.ExecuteReturnDt("SELECT Ten_CbNv FROM L81DMCBNV WHERE Ma_CbNv = '" + strMa_CbNv + "'");
                    foreach (DataRow dr_CbNv in dt_CbNv.Rows)
                    {
                        drNewRow["Ten_CbNv"] = dr_CbNv["Ten_CbNv"].ToString();
                    }
					dtTheoDoiXe.Rows.Add(drNewRow);
                    bdsTheoDoiXe.Position = bdsTheoDoiXe.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsTheoDoiXe.Current).Row);
				}

                drCurrent = ((DataRowView)bdsTheoDoiXe.Current).Row;

                if (drCurrent.Table.Columns.Contains("Ten_CbNv"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("L81DMCBNV", "Ma_CbNv", drCurrent["Ma_CbNv"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv"] = drDmCbNv["Ten_CbNv"].ToString();
                }

                if (drCurrent.Table.Columns.Contains("Ten_Dt"))
                {
                    DataRow drDmDt = DataTool.SQLGetDataRowByID("L81DMDT", "Ma_Dt", drCurrent["Ma_Dt"].ToString());
                    if (drDmDt != null)
                        drCurrent["Ten_Dt"] = drDmDt["Ten_Dt"].ToString();
                }

				dtTheoDoiXe.AcceptChanges();
			}
			//else
			//    dtTheoDoiXe.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsTheoDoiXe.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsTheoDoiXe.Current).Row;

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

				bdsTheoDoiXe.RemoveAt(bdsTheoDoiXe.Position);
				dtTheoDoiXe.AcceptChanges();
			}
		}

		public override void MergeID()
		{
            //if(bdsTheoDoiXe.Count <= 0)
            //    return;
				
            //drCurrent = ((DataRowView)bdsTheoDoiXe.Current).Row;
            //string strOldValue = (string)drCurrent[strCode];

            //frmMergeID frm = new frmMergeID();

            //frm.Load(strTableName, strCode, strName, strOldValue, "TheoDoiXe");			

            //if (frm.isAccept)
            //{
            //    string strNewValue = frm.strNewValue;
            //    string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
            //    if (!Common.MsgYes_No(strMsg))
            //        return;

            //    if (DataTool.SQLMergeID(strCode, strTableName, strOldValue, strNewValue))
            //    {					
            //        bdsTheoDoiXe.RemoveCurrent();
            //        bdsTheoDoiXe.Position = bdsTheoDoiXe.Find(strCode, strNewValue);
            //    }
            //}
		}

		#endregion 

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsTheoDoiXe == null || bdsTheoDoiXe.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsTheoDoiXe.Current).Row;
			DataTable dtTemp = dtTheoDoiXe.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void  EnterProcess()
		{
			if (bdsTheoDoiXe.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsTheoDoiXe.Current).Row;
				this.Close();
			}
		}

        void dgvTheoDoiXe_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string strColumnName = dgvTheoDoiXe.Columns[e.ColumnIndex].Name;

            if (e.RowIndex < 0)
                return;

            drCurrent = ((DataRowView)bdsTheoDoiXe.Current).Row;

            if (strColumnName == "DUYET" && (Boolean)drCurrent["DUYET"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_CV", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14LTDX";

                frm.Load(drCurrent);
            }
            else
            {
                frmDuyet frm = new frmDuyet();
                frm.Enabled = false;
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
			dgvTheoDoiXe.ResizeGridView();
		}

		#endregion 
	}
	
}