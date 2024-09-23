using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Commons;
using Epoint.Systems.Data;
using Epoint.Systems.Librarys;

namespace Epoint.Modules.HRM
{
	public partial class frmTuyenDungKH : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtTuyenDungKH;
		private BindingSource bdsTuyenDungKH = new BindingSource();
		private dgvControl dgvTuyenDungKH = new dgvControl();
		private DataRow drCurrent;

		public frmTuyenDungKH()
		{
			InitializeComponent();
		}

		public override void Load()
		{
			this.Build();
			this.FillData();

			if (this.isLookup)
				this.ShowDialog();
			else
				this.Show();
		}

		public override void LoadLookup()
		{
			this.Load();
		}

		private void Build()
		{
			dgvTuyenDungKH.ReadOnly = true;
			dgvTuyenDungKH.strZone = "TUYENDUNGKH";
			dgvTuyenDungKH.Dock = DockStyle.Fill;

			this.Controls.Add(dgvTuyenDungKH);

			dgvTuyenDungKH.BuildGridView();
		}

		private void FillData()
		{
			dtTuyenDungKH = DataTool.SQLGetDataTable("HRTUYENDUNGKH", null, "", "Ma_Bp");

			bdsTuyenDungKH.DataSource = dtTuyenDungKH;
			dgvTuyenDungKH.DataSource = bdsTuyenDungKH;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsTuyenDungKH.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsTuyenDungKH.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsTuyenDungKH.Current).Row, ref drCurrent);
			else
				drCurrent = dtTuyenDungKH.NewRow();

			frmTuyenDungKH_Edit frmEdit = new frmTuyenDungKH_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsTuyenDungKH.Position >= 0)
						dtTuyenDungKH.ImportRow(drCurrent);
					else
						dtTuyenDungKH.Rows.Add(drCurrent);

					bdsTuyenDungKH.Position = bdsTuyenDungKH.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsTuyenDungKH.Current).Row);
				}

				dtTuyenDungKH.AcceptChanges();
			}
			else
				dtTuyenDungKH.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsTuyenDungKH.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsTuyenDungKH.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRTUYENDUNGKH", drCurrent))
			{
				bdsTuyenDungKH.RemoveAt(bdsTuyenDungKH.Position);
				dtTuyenDungKH.AcceptChanges();
			}
		}
	}
}
