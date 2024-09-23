using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using Epoint.Systems;
using Epoint.Systems.Elements;

namespace Epoint.Modules.HRM
{
	public partial class frmChamCong_VanTay: Epoint.Systems.Customizes.frmView
	{

		#region Khai bao bien
		public DataTable dtChamCong_VanTay;
		BindingSource bdsChamCong_VanTay = new BindingSource();
		
		#endregion 						

		#region Contructor

		public frmChamCong_VanTay()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
		}

		public void Load()
		{			
			Build();
			FillData();
			BindingLanguage();
            			
			ShowDialog();		  
		}	
		
		#endregion

		#region Build, FillData
		private void Build()
		{			
			dgvChamCong_VanTay.strZone = "CHAMCONG_VANTAY";
			dgvChamCong_VanTay.BuildGridView(this.isLookup);

			this.Controls.Add(dgvChamCong_VanTay);
            dgvChamCong_VanTay.ReadOnly = false;
		}

		private void FillData()
		{
			bdsChamCong_VanTay = new BindingSource();

            dtChamCong_VanTay = SQLExec.ExecuteReturnDt("SELECT Ma_CbNv, Ten_CbNv, Ma_Bp, Ma_ViTri, Is_Van_Tay FROM LINHANVIEN WHERE Ma_Data = '" + Element.sysMa_Data + "'");

            bdsChamCong_VanTay.DataSource = dtChamCong_VanTay;
			dgvChamCong_VanTay.DataSource = bdsChamCong_VanTay;

			bdsChamCong_VanTay.Position = 0;
			bdsSearch = bdsChamCong_VanTay;
		}

        private void UpdateVanTay()
        {
            for (int i = 0; i < dgvChamCong_VanTay.RowCount; i++)
            {
                string strSQL = "UPDATE LINHANVIEN SET Is_Van_Tay = " + (Convert.ToBoolean(dgvChamCong_VanTay.Rows[i].Cells["Is_Van_Tay"].Value)==true?"1":"0") + " WHERE Ma_CbNv = '" + dgvChamCong_VanTay.Rows[i].Cells["Ma_CbNv"].Value.ToString() + "'";
                SQLExec.Execute(strSQL);
            }
        }

		#endregion		

		#region Su kien	

		protected override void OnKeyDown(KeyEventArgs e)
		{
            switch (e.KeyCode)
            {
                case Keys.Space:
                    DataRow drCurrent = ((DataRowView)bdsChamCong_VanTay.Current).Row;
                    drCurrent["Is_Van_tay"] = !(bool)drCurrent["Is_Van_Tay"];
                    break;
            }
			if (e.Control)
			{
				switch (e.KeyCode)
				{
					case Keys.A:
						foreach (DataRow dr in dtChamCong_VanTay.Rows)
							dr["Is_Van_Tay"] = true;
						break;					
				}
			}

			base.OnKeyDown(e);
		}

		void btAccept_Click(object sender, EventArgs e)
		{
            this.UpdateVanTay();
            this.Close();
		}
		void btCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion 
	}
}