using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems;
using Epoint.Systems.Commons;

namespace Epoint.Modules.HRM
{
	public partial class frmViTri_Edit : Epoint.Lists.frmEdit
	{

        #region Phuong thuc

		public frmViTri_Edit()
		{
			InitializeComponent();
		}

		public override void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{
			this.drEdit = drEdit;
			this.enuNew_Edit = enuNew_Edit;
			this.Tag = (char)enuNew_Edit + ", " + this.Tag;

			Common.ScaterMemvar(this, ref drEdit);

			BindingLanguage();
			LoadDicName();

			this.ShowDialog();
		}

		private void LoadDicName()
		{
		}

		public override bool FormCheckValid()
        {
			bool bvalid = true ;
			if (txtMa_ViTri.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Ma_ViTri") + " " +
							  Languages.GetLanguage("Not_Null"));
                return false;
            }			

			if (txtTen_ViTri.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Ten_ViTri") + " " +
							  Languages.GetLanguage("Not_Null"));
				return false;
			}			            

            return bvalid;
        }

		public override bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);

			//Kiem tra Valid tren Form
			if (!FormCheckValid())
				return false;

			//Luu xuong CSDL
			if (!DataTool.SQLUpdate(enuNew_Edit, "HRVITRI", ref drEdit))
				return false;

			//Doi ma
			if (this.enuNew_Edit == enuEdit.Edit)
				DataTool.SQLChangeID("Ma_ViTri", drEdit);

			return true;
		}

        #endregion

        #region Su kien


        #endregion 

	}
}