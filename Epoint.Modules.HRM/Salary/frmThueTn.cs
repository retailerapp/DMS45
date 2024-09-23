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

namespace Epoint.Modules.Salary
{
	public partial class frmThueTn : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtThueTn;
		private BindingSource bdsThueTn = new BindingSource();
		private dgvControl dgvThueTn = new dgvControl();
		private DataRow drCurrent;

		public frmThueTn()
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
			dgvThueTn.ReadOnly = true;
			dgvThueTn.strZone = "THUETN";
			dgvThueTn.Dock = DockStyle.Fill;

			this.Controls.Add(dgvThueTn);

			dgvThueTn.BuildGridView();
		}

		private void FillData()
		{
			dtThueTn = DataTool.SQLGetDataTable("HRTHUETN", null, "", "Bang_Thue,Muc_Tn1,Muc_Tn2");

			bdsThueTn.DataSource = dtThueTn;
			dgvThueTn.DataSource = bdsThueTn;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsThueTn.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsThueTn.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsThueTn.Current).Row, ref drCurrent);
			else
				drCurrent = dtThueTn.NewRow();

			frmThueTn_Edit frmEdit = new frmThueTn_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsThueTn.Position >= 0)
						dtThueTn.ImportRow(drCurrent);
					else
						dtThueTn.Rows.Add(drCurrent);

					bdsThueTn.Position = bdsThueTn.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsThueTn.Current).Row);
				}

				dtThueTn.AcceptChanges();
			}
			else
				dtThueTn.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsThueTn.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsThueTn.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRTHUETN", drCurrent))
			{
				bdsThueTn.RemoveAt(bdsThueTn.Position);
				dtThueTn.AcceptChanges();
			}
		}
	}
}
