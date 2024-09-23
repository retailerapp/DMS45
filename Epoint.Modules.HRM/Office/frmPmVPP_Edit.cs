using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Lists;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.HRM
{
	public partial class frmPmVPP_Edit : Epoint.Systems.Customizes.frmEdit
	{
		public DataRow drCurrent;
		public DataRow drEditPh;
		public DataRow drEditPhOrg;
        private string strModule = "04";
		public string strStt;		
		public DataTable dtEditCt;
		public BindingSource bdsEditCt = new BindingSource();

		public frmPmVPP_Edit()
		{
			InitializeComponent();

			this.KeyDown += new KeyEventHandler(frmPmVPP_Edit_KeyDown);
			dgvEdit.CellValidating += new DataGridViewCellValidatingEventHandler(dgvEdit_CellValidating);
			btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
			dgvEdit.LostFocus += new EventHandler(dgvEdit_LostFocus);

            txtMa_CbNv.Validating += new CancelEventHandler(txtMa_CbNv_Validating);
            txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
            
		}

		public void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{
			this.drEdit = drEdit;

			this.enuNew_Edit = enuNew_Edit;
			this.Tag = (char)enuNew_Edit + "," + this.Tag;

            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
                this.strStt = Common.GetNewStt(strModule, true);

			this.Build();
			this.FillData();			

			Common.ScaterMemvar(this, ref drEdit);

            //Lay Ma_CbNv
            string sqlMa_CbNv = "SELECT Ma_CbNv FROM L00MEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'";
            DataTable dtMa_CbNv = SQLExec.ExecuteReturnDt(sqlMa_CbNv);
            if (dtMa_CbNv != null)
            {
                foreach (DataRow drMa_CbNv in dtMa_CbNv.Rows)
                {
                    if (enuNew_Edit == enuEdit.New)
                        txtMa_CbNv.Text = drMa_CbNv["Ma_CbNv"].ToString();
                }
            }
            txtMa_CbNv.ReadOnly = true;

			foreach (Control ctrl in this.Controls)
				if (ctrl.GetType() == typeof(txtTextBox))
					((txtTextBox)ctrl).bTextChange = false;
				else if (ctrl.GetType() == typeof(txtEnum))
					((txtEnum)ctrl).bTextChange = false;
				else if (ctrl.GetType() == typeof(numControl))
					((numControl)ctrl).bTextChange = false;

			this.BindingLanguage();
            LoadDicName();
			this.ShowDialog();
		}

		private void Build()
		{
			dgvEdit.bSortMode = false;
			dgvEdit.strZone = "PMVPP1";
			dgvEdit.BuildGridView();            
		}

		private void FillData()
		{
            string strKeyFillterCt = "Stt = '" + this.drEdit["Stt"].ToString() + "'";

			string strSelectCt = enuNew_Edit == enuEdit.New ? " TOP 1 * " : "*";

			dtEditCt = DataTool.SQLGetDataTable("L14PMVPP", strSelectCt, strKeyFillterCt, null);

			if (dtEditCt.Rows.Count == 0)
				dtEditCt.Rows.Add(dtEditCt.NewRow());

			DataRow dr = dtEditCt.Rows[0];

			if (enuNew_Edit == enuEdit.New)
			{
				foreach (DataColumn dcEditCt in dtEditCt.Columns)
					dr[dcEditCt] = DBNull.Value;

				Common.SetDefaultDataRow(ref dr);
			}

			DataColumn dc = new DataColumn("Deleted", typeof(bool));
			dc.DefaultValue = false;
			dtEditCt.Columns.Add(dc);

			bdsEditCt.DataSource = dtEditCt;
			dgvEdit.DataSource = bdsEditCt;
		}

        void txtMa_CbNv_Validating(object sender, CancelEventArgs e)
        {
            //if (sender.GetType().Name == "txtTextBox" && ((txtTextBox)sender).AutoFilter != null && ((txtTextBox)sender).AutoFilter.Visible)
            //    return;

            if (!txtMa_CbNv.bTextChange)
                return;

            string strValue = txtMa_CbNv.Text.Trim();
            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_CbNv", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_CbNv.Text = string.Empty;
                lbtTen_CbNv.Text = string.Empty;                
            }
            else
            {
                txtMa_CbNv.Text = drLookup["Ma_CbNv"].ToString();
                lbtTen_CbNv.Text = drLookup["Ten_CbNv"].ToString();                
            }

            dicName[lbtTen_CbNv.Name] = lbtTen_CbNv.Text;
        }
        void txtMa_Bp_Validating(object sender, CancelEventArgs e)
        {
            
            if (!txtMa_Bp.bTextChange)
                return;

            string strValue = txtMa_Bp.Text.Trim();
            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Bp", strValue, bRequire, "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Bp.Text = string.Empty;
                lbtTen_Bp.Text = string.Empty;
            }
            else
            {
                txtMa_Bp.Text = drLookup["Ma_Bp"].ToString();
                lbtTen_Bp.Text = drLookup["Ten_Bp"].ToString();
            }

            dicName[lbtTen_Bp.Name] = lbtTen_Bp.Text;
        }
        
        private void LoadDicName()
		{
			//txtMa_Dt
			if (txtMa_CbNv.Text.Trim() != string.Empty)
				lbtTen_CbNv.Text = DataTool.SQLGetNameByCode("L81DMCBNV", "Ma_CBNV", "Ten_CbNv", txtMa_CbNv.Text.Trim());
			else
				lbtTen_CbNv.Text = string.Empty;
		}
		
		void dgvEdit_LostFocus(object sender, EventArgs e)
		{
			((dgvVoucher)sender).ClearSelection();
		}

		void frmEditCtTien_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F3:
					if (this.ActiveControl == dgvEdit && AddRow())
					{
						int iColumn = dgvEdit.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index; ;
						int iRow = dgvEdit.Rows.GetLastRow(DataGridViewElementStates.Visible);

						dgvEdit.ClearSelection();
						dgvEdit.Rows[iRow].Cells[iColumn].Selected = true;
					}
					break;
				
				case Keys.F8:
					DeleteRow();
					break;

				case Keys.Up:

					if (this.dgvEdit.Focused && this.dgvEdit.bIsCurrentFirstRow)
						this.SelectNextControl(dgvEdit, false, true, true, true);

					break;

			}

			if (!this.dgvEdit.Focused)
				this.dgvEdit.ClearSelection();
		}

		public bool Save()
		{
			foreach (DataRow dr in dtEditCt.Rows)
			{
				DataRow dr2 = dr;
				Common.GatherMemvar(this, ref dr2);
                dr2["Stt"] = drEdit["Stt"];                
			}

			Common.CopyDataRow(dtEditCt.Rows[0], drEdit);

            Update_Stt();

			if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
			{
				if (dtEditCt.Rows.Count > 0) //Cập nhật lại dữ liệu từ chi tiết lên Header
					Common.CopyDataRow(dtEditCt.Rows[0], drEdit);
			}
			if (this.enuNew_Edit == enuEdit.Edit)
			{
				string strDelete = "DELETE FROM L14PMVPP WHERE Stt = '" + (string)drEdit["Stt"] + "'";
				SQLExec.Execute(strDelete);
			}
			foreach (DataRow dr in dtEditCt.Rows)
			{
				DataRow dr2 = dr;                
				if (!(bool)dr2["Deleted"])
                    DataTool.SQLUpdate(enuEdit.New, "L14PMVPP", ref dr2);
			}

			return true;
		}
		
        private bool CellKeyEnter()
		{//Ham thuc hien phim Enter: true: thuc hien thanh cong, false: khong thuc hien duoc			

			if (dgvEdit.CurrentCell == null)
				return false;

			DataGridViewCell dgvCell = dgvEdit.CurrentCell;
			string strCurrentColumn = dgvCell.OwningColumn.Name.ToUpper();

            #region Enter tai Dvt
            if (Common.Inlist(strCurrentColumn, "DVT"))
            {
                drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                if (drCurrent["Ten_Vt"] == DBNull.Value || (string)drCurrent["Ten_Vt"] == string.Empty)
                {
                    bool bIsCurrentLastRow = dgvEdit.bIsCurrentLastRow;

                    if (bdsEditCt.Count > 1)
                    {
                        bdsEditCt.RemoveCurrent();
                        dtEditCt.AcceptChanges();
                    }

                    if (bIsCurrentLastRow)
                    {
                        this.dgvEdit.ClearSelection();
                        this.SelectNextControl(dgvEdit, true, true, true, true);
                    }

                    return true;
                }

                return false;
            }
            #endregion

            #region Enter tai So_Luong
            if (Common.Inlist(strCurrentColumn, "SO_LUONG"))
			{
				drCurrent = ((DataRowView)bdsEditCt.Current).Row;

                if (dgvEdit.bIsCurrentLastRow)
				{
					if (!AddRow())
					{
						this.dgvEdit.ClearSelection();
						this.SelectNextControl(dgvEdit, true, true, true, true);
					}
					else
					{
						dgvEdit.FocusNextFirstCell();
						this.ActiveControl = dgvEdit;
					}
				}               
                
				return false;

            }
            #endregion
            return false;
		}

		public bool AddRow()
		{
			DataRow drNew = dtEditCt.NewRow();

			dtEditCt.Rows.Add(drNew);
			dtEditCt.AcceptChanges();

			return true;
		}
        
        public void Update_Stt()
        {
            //if (this.enuNew_Edit == enuEdit.New)
            //{
            //    while (DataTool.SQLCheckExist("L14PMVPP", "Stt", this.strStt))
            //    {
            //        this.strStt = Common.GetNewStt(strModule, true, Element.sysMa_DvCs);
            //    }

            //    foreach (DataRow drCt in dtEditCt.Rows)
            //        drCt["Stt"] = this.strStt;

            //    this.dtEditCt.AcceptChanges();
            //}
        }
		
        void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{//Cai dat Lookup

			dgvVoucher dgvEdit = (dgvVoucher)sender;
			//Xu ly phim Enter
			if (dgvEdit.kLastKey == Keys.Enter)
			{
				dgvEdit.kLastKey = Keys.None;

				if (this.CellKeyEnter())
					e.Cancel = true;
			}

			//Xu ly Lookup
			if (this.ActiveControl == null)
				return;

			//e.Cancel = true;

			if (this.ActiveControl == dgvEdit || this.ActiveControl.GetType().Name == "DataGridViewTextBoxEditingControl")
			{
				drCurrent = ((DataRowView)bdsEditCt.Current).Row;
				DataGridViewCell dgvCell = dgvEdit.CurrentCell;
				string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

				bool bLookup = true;

				if (bLookup == false)
					e.Cancel = true;
			}
		}

        public void DeleteRow()
        {
            if (dgvEdit.Focused == false)
                return;

            drCurrent = ((DataRowView)bdsEditCt.Current).Row;
            drCurrent["Deleted"] = !((bool)drCurrent["Deleted"]);

            if ((bool)drCurrent["Deleted"] == true)
            {
                Font font = new Font(dgvEdit.Font.FontFamily, dgvEdit.Font.Size, FontStyle.Strikeout);
                dgvEdit.CurrentRow.DefaultCellStyle.Font = font;
            }
            else
            {
                dgvEdit.CurrentRow.DefaultCellStyle.Font = dgvEdit.Font;
            }

        }

        void frmPmVPP_Edit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    if (this.ActiveControl == dgvEdit && AddRow())
                    {
                        int iColumn = dgvEdit.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index; ;
                        int iRow = dgvEdit.Rows.GetLastRow(DataGridViewElementStates.Visible);

                        dgvEdit.ClearSelection();
                        dgvEdit.Rows[iRow].Cells[iColumn].Selected = true;
                    }
                    break;

                case Keys.F8:
                    DeleteRow();
                    break;

                case Keys.Up:

                    if (this.dgvEdit.Focused && this.dgvEdit.bIsCurrentFirstRow)
                        this.SelectNextControl(dgvEdit, false, true, true, true);

                    break;

            }

            if (!this.dgvEdit.Focused)
                this.dgvEdit.ClearSelection();
        }

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

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.dgvEdit.ClearSelection();

            if (!Element.sysIs_Admin && enuNew_Edit == enuEdit.Edit && (bool)drEdit["Duyet_Gd"])
            {
                btgAccept.btAccept.Enabled = false;
            }
		}

	}
}