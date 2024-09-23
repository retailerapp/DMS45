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
	public partial class frmChamCong : Epoint.Systems.Customizes.frmView
	{
		private DataTable dtChamCong;
		private BindingSource bdsChamCong = new BindingSource();
		private dgvControl dgvChamCong = new dgvControl();
		private DataRow drCurrent;

		public frmChamCong()
		{
			InitializeComponent();

            btChamCong_Tay.Click += new EventHandler(btChamCong_Tay_Click);
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
			dgvChamCong.ReadOnly = true;
			dgvChamCong.strZone = "ChamCong";
			dgvChamCong.Dock = DockStyle.Fill;
			
            this.splitContainer1.Panel1.Controls.Add(dgvChamCong);			
            dgvChamCong.BuildGridView();
		}

		private void FillData()
		{
			dtChamCong = DataTool.SQLGetDataTable("HRCHAMCONG", null, "", "Ma_Bp");

			bdsChamCong.DataSource = dtChamCong;
			dgvChamCong.DataSource = bdsChamCong;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsChamCong.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			if (bdsChamCong.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsChamCong.Current).Row, ref drCurrent);
			else
				drCurrent = dtChamCong.NewRow();

			frmChamCong_Edit frmEdit = new frmChamCong_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsChamCong.Position >= 0)
						dtChamCong.ImportRow(drCurrent);
					else
						dtChamCong.Rows.Add(drCurrent);

					bdsChamCong.Position = bdsChamCong.Find("Ident00", drCurrent["Ident00"]);
				}
				else
				{
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsChamCong.Current).Row);
				}

				dtChamCong.AcceptChanges();
			}
			else
				dtChamCong.RejectChanges();
		}

        void btChamCong_Tay_Click(object sender, EventArgs e)
        {
            frmChamCong_VanTay frm = new frmChamCong_VanTay();
            frm.Load();
        }

		public override void Delete()
		{
			if (bdsChamCong.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsChamCong.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("HRCHAMCONG", drCurrent))
			{
				bdsChamCong.RemoveAt(bdsChamCong.Position);
				dtChamCong.AcceptChanges();
			}
		}
	}
}
