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
	public partial class frmTsTn : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtTsTn;
		private BindingSource bdsTsTn = new BindingSource();
		private dgvControl dgvTsTn = new dgvControl();
		private DataRow drCurrent;

		public frmTsTn()
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
			dgvTsTn.ReadOnly = true;
			dgvTsTn.strZone = "TSTN";
			dgvTsTn.Dock = DockStyle.Fill;

			this.Controls.Add(dgvTsTn);

			dgvTsTn.BuildGridView();
		}

		private void FillData()
		{
			string strSQLExec =
				"SELECT T1.*, T2.Ten_Tn, T2.Dvt FROM HRPARAVALUE T1 LEFT JOIN HRPARALIST T2 ON T1.Ma_Tn = T2.Ma_Tn " + 
					" ORDER BY Ma_Tn, Ngay_Ap";

			dtTsTn = SQLExec.ExecuteReturnDt(strSQLExec);

			bdsTsTn.DataSource = dtTsTn;
			dgvTsTn.DataSource = bdsTsTn;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsTsTn.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsTsTn.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsTsTn.Current).Row, ref drCurrent);
			else
				drCurrent = dtTsTn.NewRow();

			frmTsTn_Edit frmEdit = new frmTsTn_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsTsTn.Position >= 0)
						dtTsTn.ImportRow(drCurrent);
					else
						dtTsTn.Rows.Add(drCurrent);

					bdsTsTn.Position = bdsTsTn.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsTsTn.Current).Row);
				}

				dtTsTn.AcceptChanges();
			}
			else
				dtTsTn.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsTsTn.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsTsTn.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRPARAVALUE", drCurrent))
			{
				bdsTsTn.RemoveAt(bdsTsTn.Position);
				dtTsTn.AcceptChanges();
			}
		}
	}
}
