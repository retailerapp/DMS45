using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Elements;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using Epoint.Systems;

namespace Epoint.Modules.HRM
{
	public partial class frmViTri : Epoint.Lists.frmView
	{

		#region Khai bao bien
		DataTable dtViTri;
		DataRow drCurrent;
		BindingSource bdsViTri = new BindingSource();
        tlControl tlViTri = new tlControl();

		#endregion 						

		#region Contructor

		public frmViTri()
		{
			InitializeComponent();

            tlViTri.MouseDoubleClick += new MouseEventHandler(tlViTri_MouseDoubleClick);
		}

		public override void Load()
		{
            Init();
            Build();
			FillData();
			BindingLanguage();

			if (this.isLookup)
				this.ShowDialog();
			else
				this.Show();
		}

		public override void LoadLookup()
		{
			this.Load();
		}
        private void Init()
        {
            htHistory["DIEN_GIAI"] = "Danh mục vị trí";
            strTableName = "HRVITRI";
            strCode = "MA_VITRI";
            strName = "TEN_VITRI";
        }
		#endregion

		#region Build, FillData
		private void Build()
		{
            tlViTri.KeyFieldName = "MA_VITRI";
            tlViTri.ParentFieldName = "MA_VITRI_CHA";
            tlViTri.Dock = DockStyle.Fill;

            tlViTri.strZone = "ViTri";
            tlViTri.BuildTreeList(this.isLookup);

            this.Controls.Add(tlViTri);
		}

		public void FillData()
		{
            dtViTri = DataTool.SQLGetDataTable("HRVITRI", null, this.strLookupKeyFilter, null);
            bdsViTri.DataSource = dtViTri;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsViTri;
            ExportControl = tlViTri;

            tlViTri.DataSource = bdsViTri;
            bdsViTri.Position = 0;

            if (this.isLookup)
                this.MoveToLookupValue();

            tlViTri.Expand = (bool)SQLExec.ExecuteReturnValue("SELECT Expand FROM SYSZONE WHERE ZONE = '" + tlViTri.strZone + "'");
		}

		private void MoveToLookupValue()
		{
			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtViTri.Rows.Count - 1; i++)
				if (((string)dtViTri.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsViTri.Position = i;
					break;
				}
		}

		#endregion

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsViTri.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsViTri.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsViTri.Current).Row, ref drCurrent);
			else
				drCurrent = dtViTri.NewRow();

			frmViTri_Edit frmEdit = new frmViTri_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
                //Cập nhật dữ liệu danh mục
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsViTri.Position >= 0)
						dtViTri.ImportRow(drCurrent);
					else
						dtViTri.Rows.Add(drCurrent);

					bdsViTri.Position = bdsViTri.Find("MA_VITRI", drCurrent["MA_VITRI"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsViTri.Current).Row);

				dtViTri.AcceptChanges();
			}
            //else
            //    dtViTri.RejectChanges();
		}

		public override void Delete()
		{
            if (bdsViTri.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsViTri.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("HRVITRI", drCurrent))
            {                               
                bdsViTri.RemoveAt(bdsViTri.Position);
                dtViTri.AcceptChanges();
            }
		}

		public override void MergeID()
		{
			if (bdsViTri.Count <= 0)
				return;

			drCurrent = ((DataRowView)bdsViTri.Current).Row;
			string strOldValue = (string)drCurrent["MA_VITRI"];

            Epoint.Lists.frmMergeID frm = new Epoint.Lists.frmMergeID();

			frm.Load("HRVITRI", "MA_VITRI", "Ten_Km", strOldValue, "ViTri");

			if (frm.isAccept)
			{
				string strNewValue = frm.strNewValue;
				string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
				if (!Common.MsgYes_No(strMsg))
					return;

				if (DataTool.SQLMergeID("MA_VITRI", "HRVITRI", strOldValue, strNewValue))
				{
					bdsViTri.RemoveCurrent();
					bdsViTri.Position = bdsViTri.Find("MA_VITRI", strNewValue);
				}
			}
		}

		#endregion

		#region EnterProcess

		private bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsViTri == null || bdsViTri.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsViTri.Current).Row;
			DataTable dtTemp = dtViTri.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void  EnterProcess()
		{
			if (bdsViTri.Position < 0)
				return;

			if (isLookup && EnterValid())
			{
				drLookup = ((DataRowView)bdsViTri.Current).Row;
				this.Close();
			}
		}

		#endregion 

		#region Su kien

        void tlViTri_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.isLookup)
                this.EnterProcess();
            else
                this.Edit(enuEdit.Edit);
        }    

		#endregion 
	}
}