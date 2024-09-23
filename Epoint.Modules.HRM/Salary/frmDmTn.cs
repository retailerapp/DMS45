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
	public partial class frmDmTn : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtDmTn;
		private BindingSource bdsDmTn = new BindingSource();
		private dgvControl dgvDmTn = new dgvControl();
		private DataRow drCurrent;

		public frmDmTn()
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
			dgvDmTn.ReadOnly = true;
			dgvDmTn.strZone = "DMTN";
			dgvDmTn.Dock = DockStyle.Fill;

			this.Controls.Add(dgvDmTn);

			dgvDmTn.BuildGridView();
		}

		private void FillData()
		{
			dtDmTn = DataTool.SQLGetDataTable("HRPARALIST", null, "", "Stt, Ma_Tn");          

			bdsDmTn.DataSource = dtDmTn;
			dgvDmTn.DataSource = bdsDmTn;
		}

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsDmTn == null || bdsDmTn.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsDmTn.Current).Row;
			DataTable dtTemp = dtDmTn.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;
		}

		public override void EnterProcess()
		{
			if (bdsDmTn.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsDmTn.Current).Row;
				this.Close();
			}
		}

		#endregion 

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsDmTn.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsDmTn.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsDmTn.Current).Row, ref drCurrent);
			else
				drCurrent = dtDmTn.NewRow();

			frmDmTn_Edit frmEdit = new frmDmTn_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsDmTn.Position >= 0)
						dtDmTn.ImportRow(drCurrent);
					else
						dtDmTn.Rows.Add(drCurrent);

					bdsDmTn.Position = bdsDmTn.Find("MA_TN", drCurrent["MA_TN"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsDmTn.Current).Row);
				}

				dtDmTn.AcceptChanges();
			}
			else
				dtDmTn.RejectChanges();
		}

		public override void Delete()
		{
			if (bdsDmTn.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsDmTn.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRPARALIST", drCurrent))
			{
				bdsDmTn.RemoveAt(bdsDmTn.Position);
				dtDmTn.AcceptChanges();
			}
		}
	}
}
