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
	public partial class frmPmTb : Epoint.Lists.frmView
	{
		#region Khai bao bien

        private DataTable dtPmTb;
		private DataRow drCurrent;
		private BindingSource bdsPmTb = new BindingSource();
		private dgvControl dgvPmTb = new dgvControl();
		public bool bLookupByGroup = false;

		#endregion 

		#region Contructor
		public frmPmTb()
		{
			InitializeComponent();

			this.Resize += new EventHandler(ResizeEvent);
            dgvPmTb.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvPmTb_CellMouseClick);
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
            strTableName = "L14PMTB";
			strCode = "MA_CBNV";
			strName = "MA_CBNV";

            dteTu_Ngay.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            dteDen_Ngay.Text = DateTime.Now.ToShortDateString();
		}

		#endregion

		#region Build, FillData
		private void Build()
		{
			dgvPmTb.Dock = DockStyle.Fill;
			dgvPmTb.strZone = "PMTB";

			dgvPmTb.BuildGridView(this.isLookup);
            this.splitContainer1.Panel2.Controls.Add(dgvPmTb);
			//this.Controls.Add(dgvPmTb);
		}

		public void FillData()	
		{
            Hashtable ht = new Hashtable();
            ht.Add("MEMBER_ID", Element.sysUser_Id.ToString());
            ht.Add("NGAY_CT1", dteTu_Ngay.Text);
            ht.Add("NGAY_CT2", dteDen_Ngay.Text);
            ht.Add("MA_DVCS", Element.sysMa_DvCs);

            dtPmTb = SQLExec.ExecuteReturnDt("sp_GetPmTb", ht, CommandType.StoredProcedure);

			bdsPmTb.DataSource = dtPmTb;
			dgvPmTb.DataSource = bdsPmTb;

			if (bdsPmTb.Count >= 0)
				bdsPmTb.Position = 0;

			//Uy quyen cho lop co so tim kiem           
			bdsSearch = bdsPmTb;
			ExportControl = dgvPmTb;

			if (this.isLookup)
				this.MoveToLookupValue();

		}

		private void MoveToLookupValue()
		{//Tim den mau tin tim kiem

			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtPmTb.Rows.Count - 1; i++)
				if (((string)dtPmTb.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsPmTb.Position = i;
					break;
				}
		}

		#endregion 

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsPmTb.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsPmTb.Position >= 0)
				//drCurrent = DataTool.SQLGetDataRowByID(strTableName, strCode, (string)((DataRowView)bdsPmTb.Current)[strCode]);
                Common.CopyDataRow(((DataRowView)bdsPmTb.Current).Row, ref drCurrent);

			if (drCurrent == null)
			{
				drCurrent = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null).NewRow();
				Common.SetDefaultDataRow(ref drCurrent);
			}

			if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
				drCurrent[strCode] = GetAutoCode((string)drCurrent[strCode]);

			frmPmTb_Edit frmEdit = new frmPmTb_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			// Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					DataRow drNewRow = dtPmTb.NewRow();                   
					Common.CopyDataRow(drCurrent, drNewRow);
                    
					dtPmTb.Rows.Add(drNewRow);

                    bdsPmTb.Position = bdsPmTb.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsPmTb.Current).Row);
				}

                drCurrent = ((DataRowView)bdsPmTb.Current).Row;

                if (drCurrent.Table.Columns.Contains("Ten_CbNv"))
                {
                    DataRow drDmCbNv = DataTool.SQLGetDataRowByID("L81DMCBNV", "Ma_CbNv", drCurrent["Ma_CbNv"].ToString());
                    if (drDmCbNv != null)
                        drCurrent["Ten_CbNv"] = drDmCbNv["Ten_CbNv"].ToString();
                }

				dtPmTb.AcceptChanges();
			}			
		}

		public override void Delete()
		{
			if (bdsPmTb.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsPmTb.Current).Row;

            //Mau tin da Duyet thi khong cho xoa
            if ((Boolean)drCurrent["Duyet_GD"] == true || (Boolean)drCurrent["Duyet_NHAN"] == true || (Boolean)drCurrent["Duyet_TRA"] == true)
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

				bdsPmTb.RemoveAt(bdsPmTb.Position);
				dtPmTb.AcceptChanges();
			}
		}

        void dgvPmTb_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string strColumnName = dgvPmTb.Columns[e.ColumnIndex].Name;

            if (e.RowIndex < 0)
                return;

            drCurrent = ((DataRowView)bdsPmTb.Current).Row;

            if (strColumnName == "DUYET_GD" && (Boolean)drCurrent["DUYET_GD"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_GD", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMTB";

                frm.Load(drCurrent);
            }
            else if (strColumnName == "DUYET_HCNS" && (Boolean)drCurrent["DUYET_HCNS"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_HCNS", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMTB";

                frm.Load(drCurrent);
            }
            else if(strColumnName == "DUYET_NHAN" && (Boolean)drCurrent["DUYET_NHAN"] == false)
            {
                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMTB";

                frm.Load(drCurrent);
            }
            else if (strColumnName == "DUYET_TRA" && (Boolean)drCurrent["DUYET_TRA"] == false)
            {
                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMTB";

                frm.Load(drCurrent);
            }
            else
            {
                frmDuyet frm = new frmDuyet();
                frm.Enabled = false;
            }
        }

		#endregion 

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsPmTb == null || bdsPmTb.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsPmTb.Current).Row;
			DataTable dtTemp = dtPmTb.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void  EnterProcess()
		{
			if (bdsPmTb.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsPmTb.Current).Row;
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
			dgvPmTb.ResizeGridView();
		}

		#endregion 
	}
	
}