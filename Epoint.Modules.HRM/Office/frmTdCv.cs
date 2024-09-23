using System;
using System.Collections;
using System.Collections.Generic;
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
	public partial class frmTdCv : Epoint.Lists.frmView
	{
		#region Khai bao bien

		private DataTable dtTdCv;
		private DataRow drCurrent;
		private BindingSource bdsTdCv = new BindingSource();
		private dgvControl dgvTdCv = new dgvControl();
		public bool bLookupByGroup = false;

		#endregion 

		#region Contructor
		public frmTdCv()
		{
			InitializeComponent();

			this.Resize += new EventHandler(ResizeEvent);
            this.btFilter.Click += new EventHandler(btFilter_Click);
            dgvTdCv.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvTdCv_CellMouseClick);
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
			htHistory["DIEN_GIAI"] = "Tiến độ công việc";
            strTableName = "L14TDCV";
			strCode = "MA_CBNV";
			strName = "MA_CBNV";

            dteTu_Ngay.Text = DateTime.Now.ToShortDateString();
            dteDen_Ngay.Text = DateTime.Now.ToShortDateString();
		}

		#endregion

		#region Build, FillData
		private void Build()
		{
			dgvTdCv.Dock = DockStyle.Fill;
			dgvTdCv.strZone = "TDCV";

			dgvTdCv.BuildGridView(this.isLookup);

            this.splitContainer1.Panel2.Controls.Add(dgvTdCv);
			//this.Controls.Add(dgvTdCv);
		}

		public void FillData()	
		{
            //Lay Ma_CbNv
            Hashtable ht = new Hashtable();
            ht.Add("MEMBER_ID", Element.sysUser_Id.ToString());
            ht.Add("NGAY_CT1", dteTu_Ngay.Text);
            ht.Add("NGAY_CT2", dteDen_Ngay.Text);
            ht.Add("MA_DVCS", Element.sysMa_DvCs);

            dtTdCv = SQLExec.ExecuteReturnDt("sp_GetTDCV", ht, CommandType.StoredProcedure);

			bdsTdCv.DataSource = dtTdCv;
			dgvTdCv.DataSource = bdsTdCv;

			if (bdsTdCv.Count >= 0)
				bdsTdCv.Position = 0;

			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsTdCv;
			ExportControl = dgvTdCv;

			if (this.isLookup)
				this.MoveToLookupValue();
		}

		private void MoveToLookupValue()
		{//Tim den mau tin tim kiem

			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtTdCv.Rows.Count - 1; i++)
				if (((string)dtTdCv.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsTdCv.Position = i;
					break;
				}
		}

		#endregion 

		#region Update

        public override void Edit(enuEdit enuNew_Edit)
        {
            if (bdsTdCv.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai            
            if (bdsTdCv.Position >= 0)
                //drCurrent = DataTool.SQLGetDataRowByID(strTableName, strCode, (string)((DataRowView)bdsTdCv.Current)[strCode]);
                Common.CopyDataRow(((DataRowView)bdsTdCv.Current).Row, ref drCurrent);

            if (drCurrent == null)
            {
                drCurrent = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null).NewRow();
                Common.SetDefaultDataRow(ref drCurrent);
            }

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
                drCurrent[strCode] = GetAutoCode((string)drCurrent[strCode]);

            frmTdCv_Edit frmEdit = new frmTdCv_Edit();
            frmEdit.Load(enuNew_Edit, drCurrent);

            // Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    DataRow drNewRow = dtTdCv.NewRow();
                    Common.CopyDataRow(drCurrent, drNewRow);
                    dtTdCv.Rows.Add(drNewRow);
                    bdsTdCv.Position = bdsTdCv.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsTdCv.Current).Row);
                }

                drCurrent = ((DataRowView)bdsTdCv.Current).Row;

                if (drCurrent.Table.Columns.Contains("Ten_CbNv"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("L81DMCBNV", "Ma_CbNv", drCurrent["Ma_CbNv"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv"] = drDmCbNv["Ten_CbNv"].ToString();
                }


                if (drCurrent.Table.Columns.Contains("Ten_CbNv_Ht"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("L81DMCBNV", "Ma_CbNv", drCurrent["Ma_CbNv_Ht"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv_Ht"] = drDmCbNv["Ten_CbNv"].ToString();
                }

                if (drCurrent.Table.Columns.Contains("Ten_Dt"))
                {
                    DataRow drDmDt = DataTool.SQLGetDataRowByID("L81DMDT", "Ma_Dt", drCurrent["Ma_Dt"].ToString());
                    if (drDmDt != null)
                        drCurrent["Ten_Dt"] = drDmDt["Ten_Dt"].ToString();
                }

                dtTdCv.AcceptChanges();
            }
            //else
            //    dtTdCv.RejectChanges();
        }

		public override void Delete()
		{
			if (bdsTdCv.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsTdCv.Current).Row;

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

				bdsTdCv.RemoveAt(bdsTdCv.Position);
				dtTdCv.AcceptChanges();
			}
		}

		public override void MergeID()
		{
            //if(bdsTdCv.Count <= 0)
            //    return;
				
            //drCurrent = ((DataRowView)bdsTdCv.Current).Row;
            //string strOldValue = (string)drCurrent[strCode];

            //frmMergeID frm = new frmMergeID();

            //frm.Load(strTableName, strCode, strName, strOldValue, "TdCv");			

            //if (frm.isAccept)
            //{
            //    string strNewValue = frm.strNewValue;
            //    string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
            //    if (!Common.MsgYes_No(strMsg))
            //        return;

            //    if (DataTool.SQLMergeID(strCode, strTableName, strOldValue, strNewValue))
            //    {					
            //        bdsTdCv.RemoveCurrent();
            //        bdsTdCv.Position = bdsTdCv.Find(strCode, strNewValue);
            //    }
            //}
		}

		#endregion 

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsTdCv == null || bdsTdCv.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsTdCv.Current).Row;
			DataTable dtTemp = dtTdCv.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void  EnterProcess()
		{
			if (bdsTdCv.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsTdCv.Current).Row;
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
			dgvTdCv.ResizeGridView();
		}

        void dgvTdCv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string strColumnName = dgvTdCv.Columns[e.ColumnIndex].Name;

            if (e.RowIndex < 0)
                return;

            drCurrent = ((DataRowView)bdsTdCv.Current).Row;

            if (strColumnName == "DUYET" && (Boolean)drCurrent["DUYET"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_CV", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14TDCV";

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