using System;
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
	public partial class frmPmVPP : Epoint.Lists.frmView
	{
		#region Khai bao bien

		private DataTable dtPmVPP;
        private DataTable dtPmVPP_Detail;
		private DataRow drCurrent;
		private BindingSource bdsPmVPP = new BindingSource();
        private BindingSource bdsPmVPP_Detail = new BindingSource();
		private dgvControl dgvPmVPP = new dgvControl();
        private dgvControl dgvPmVPP_Detail = new dgvControl();
		public bool bLookupByGroup = false;

		#endregion

		#region Contructor
		public frmPmVPP()
		{
			InitializeComponent();

			this.KeyDown += new KeyEventHandler(frmPmVPP_KeyDown);
            dgvPmVPP.CellMouseClick += new DataGridViewCellMouseEventHandler(dgvPmVPP_CellMouseClick);
		}

		public override void Load()
		{
			strTableName = "L14PMVPP";

			Build();
			FillData();
			BindingLanguage();

			if (this.isLookup)
				this.ShowDialog();
			else
				this.Show();
			//this.statusStrip1.Visible = false;
		}

		#endregion

		#region Build, FillData
		private void Build()
		{
			dgvPmVPP.Dock = DockStyle.Fill;
            dgvPmVPP_Detail.Dock = DockStyle.Fill;
			dgvPmVPP.strZone = "PMVPP";
            dgvPmVPP_Detail.strZone = "PMVPP1";

			dgvPmVPP.BuildGridView(this.isLookup);
            dgvPmVPP_Detail.BuildGridView(this.isLookup);

            this.pnPmVPP.Controls.Add(dgvPmVPP);
            this.pnPmVPP_Detail.Controls.Add(dgvPmVPP_Detail);			
			this.ExportControl = dgvPmVPP;
            this.ExportControl = dgvPmVPP_Detail;
		}

		public void FillData()
		{
            //Lay Ma_CbNv
            string sqlMa_CbNv = "SELECT Ma_CbNv FROM L00MEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'";
            string strMa_CbNv = string.Empty;
            DataTable dtMa_CbNv = SQLExec.ExecuteReturnDt(sqlMa_CbNv);
            if (dtMa_CbNv != null)
            {
                foreach (DataRow drMa_CbNv in dtMa_CbNv.Rows)
                {
                    strMa_CbNv = drMa_CbNv["Ma_CbNv"].ToString();
                }
            }

            string strSelect = "SELECT DISTINCT T1.Stt, T1.Thang, T1.Ma_CbNv, T1.Ma_Bp, T1.Duyet_GD, " +
            "T1.Duyet_GD_Log, T1.Duyet_HCNS, T1.Duyet_HCNS_Log, T2.Ten_CbNv " +
            "FROM L14PMVPP T1 LEFT JOIN L81DMCBNV T2 ON T1.Ma_CbNv = T2.Ma_CbNv " +
            " ORDER BY T1.Stt";

            dtPmVPP = SQLExec.ExecuteReturnDt(strSelect);
            dtPmVPP_Detail = DataTool.SQLGetDataTable(strTableName, "*", "", "Stt");
            
			bdsPmVPP.DataSource = dtPmVPP;
			dgvPmVPP.DataSource = bdsPmVPP;

            bdsPmVPP_Detail.DataSource = dtPmVPP_Detail;
            dgvPmVPP_Detail.DataSource = bdsPmVPP_Detail;
            
            bdsPmVPP.PositionChanged +=new EventHandler(bdsPmVPP_PositionChanged);

            if (bdsPmVPP.Count > 0)
            {
                string strPmVPP = (string)((DataRowView)bdsPmVPP.Current).Row["Stt"];
                bdsPmVPP_Detail.Filter = "Stt = '" + strPmVPP + "'";
            }

			if (bdsPmVPP.Count >= 0)
				bdsPmVPP.Position = 0;

			//Uy quyen cho lop co so tim kiem
			bdsSearch = bdsPmVPP;
		}
		#endregion

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsPmVPP.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsPmVPP.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsPmVPP.Current).Row, ref drCurrent);
			else
				drCurrent = dtPmVPP.NewRow();

			frmPmVPP_Edit frmEdit = new frmPmVPP_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			// Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsPmVPP.Position >= 0)
						dtPmVPP.ImportRow(drCurrent);
					else
						dtPmVPP.Rows.Add(drCurrent);

					bdsPmVPP.Position = bdsPmVPP.Find("Stt", drCurrent["Stt"]);
                    dtPmVPP_Detail.Merge(frmEdit.dtEditCt);                    
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsPmVPP.Current).Row);

                    foreach(DataRow dr in dtPmVPP_Detail.Rows)
                        if(dr["Stt"].Equals(drCurrent["Stt"]))
                            dr.Delete();

                    dtPmVPP_Detail.AcceptChanges();
                    dtPmVPP_Detail.Merge(frmEdit.dtEditCt);
				}
				dtPmVPP.AcceptChanges();
                dtPmVPP_Detail.AcceptChanges();  
			}
		}

		public override void Delete()
		{
			if (bdsPmVPP.Position < 0)
				return;
			drCurrent = ((DataRowView)bdsPmVPP.Current).Row;

            //Mau tin da Duyet thi khong cho xoa
            if ((Boolean)drCurrent["Duyet_GD"] == true)
            {
                MessageBox.Show("Mẫu tin đã duyệt. Không được xóa !", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

			string strDelete = "DELETE FROM " + strTableName + " WHERE Stt = '" + (string)drCurrent["Stt"] + "'";

			if (SQLExec.Execute(strDelete))
			{
				bdsPmVPP.RemoveAt(bdsPmVPP.Position);
				dtPmVPP.AcceptChanges();
			}
		}

        void dgvPmVPP_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string strColumnName = dgvPmVPP.Columns[e.ColumnIndex].Name;

            if (e.RowIndex < 0)
                return;

            drCurrent = ((DataRowView)bdsPmVPP.Current).Row;

            if (strColumnName == "DUYET_GD" && (Boolean)drCurrent["DUYET_GD"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_GD", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMVPP";

                frm.Load(drCurrent);
            }
            if (strColumnName == "DUYET_HCNS" && (Boolean)drCurrent["DUYET_HCNS"] == false)
            {
                if (!Element.sysIs_Admin && !Common.CheckPermission("DUYET_HCNS", enuPermission_Type.Allow_Access))
                {
                    Common.MsgCancel("Bạn không có quyền duyệt chức năng này");
                    return;
                }

                frmDuyet frm = new frmDuyet();
                frm.strColumnName = strColumnName;
                frm.strTableName = "L14PMVPP";

                frm.Load(drCurrent);
            }
            else
            {
                frmDuyet frm = new frmDuyet();
                frm.Enabled = false;
            }
        }


		#endregion

		#region Event

		void frmPmVPP_KeyDown(object sender, KeyEventArgs e)
		{
	
		}
        void bdsPmVPP_PositionChanged(object sender, EventArgs e)
        {
            string strPmVPP = (string)((DataRowView)bdsPmVPP.Current).Row["Stt"];
            bdsPmVPP_Detail.Filter = "Stt = '" + strPmVPP + "'";
        }

		#endregion
	}
}