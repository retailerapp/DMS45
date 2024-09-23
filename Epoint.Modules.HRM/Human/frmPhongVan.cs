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
	public partial class frmPhongVan : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtPhongVan;
		private BindingSource bdsPhongVan = new BindingSource();
		private dgvControl dgvPhongVan = new dgvControl();
		private DataRow drCurrent;

		public frmPhongVan()
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
			dgvPhongVan.ReadOnly = true;
			dgvPhongVan.strZone = "PHONGVAN";
			dgvPhongVan.Dock = DockStyle.Fill;

			this.Controls.Add(dgvPhongVan);

			dgvPhongVan.BuildGridView();
		}

		private void FillData()
		{
			dtPhongVan = DataTool.SQLGetDataTable("HRPHONGVAN", null, "", "Ngay_Pv");

			bdsPhongVan.DataSource = dtPhongVan;
			dgvPhongVan.DataSource = bdsPhongVan;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsPhongVan.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsPhongVan.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsPhongVan.Current).Row, ref drCurrent);
			else
				drCurrent = dtPhongVan.NewRow();

			frmPhongVan_Edit frmEdit = new frmPhongVan_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsPhongVan.Position >= 0)
						dtPhongVan.ImportRow(drCurrent);
					else
						dtPhongVan.Rows.Add(drCurrent);

					bdsPhongVan.Position = bdsPhongVan.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsPhongVan.Current).Row);
				}

				dtPhongVan.AcceptChanges();
			}
			else
				dtPhongVan.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsPhongVan.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsPhongVan.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRPHONGVAN", drCurrent))
			{
				bdsPhongVan.RemoveAt(bdsPhongVan.Position);
				dtPhongVan.AcceptChanges();
			}
		}
	}
}
