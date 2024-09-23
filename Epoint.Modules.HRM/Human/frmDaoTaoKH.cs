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
	public partial class frmDaoTaoKH : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtDaoTaoKH;
		private BindingSource bdsDaoTaoKH = new BindingSource();
		private dgvControl dgvDaoTaoKH = new dgvControl();
		private DataRow drCurrent;

		public frmDaoTaoKH()
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
			dgvDaoTaoKH.ReadOnly = true;
			dgvDaoTaoKH.strZone = "DaoTaoKH";
			dgvDaoTaoKH.Dock = DockStyle.Fill;

			this.Controls.Add(dgvDaoTaoKH);

			dgvDaoTaoKH.BuildGridView();
		}

		private void FillData()
		{
			dtDaoTaoKH = DataTool.SQLGetDataTable("HRDAOTAOKH", null, "", "Ma_Bp");

			bdsDaoTaoKH.DataSource = dtDaoTaoKH;
			dgvDaoTaoKH.DataSource = bdsDaoTaoKH;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsDaoTaoKH.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsDaoTaoKH.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsDaoTaoKH.Current).Row, ref drCurrent);
			else
				drCurrent = dtDaoTaoKH.NewRow();

			frmDaoTaoKH_Edit frmEdit = new frmDaoTaoKH_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsDaoTaoKH.Position >= 0)
						dtDaoTaoKH.ImportRow(drCurrent);
					else
						dtDaoTaoKH.Rows.Add(drCurrent);

					bdsDaoTaoKH.Position = bdsDaoTaoKH.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsDaoTaoKH.Current).Row);
				}

				dtDaoTaoKH.AcceptChanges();
			}
			else
				dtDaoTaoKH.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsDaoTaoKH.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsDaoTaoKH.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRDAOTAOKH", drCurrent))
			{
				bdsDaoTaoKH.RemoveAt(bdsDaoTaoKH.Position);
				dtDaoTaoKH.AcceptChanges();
			}
		}
	}
}
