using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Epoint.Systems.Data;
using Epoint.Systems.Librarys;
using Epoint.Lists;
using Epoint.Systems;
using Epoint.Systems.Commons;
using Epoint.Systems.Elements;
using Epoint.Systems.Customizes;
using Epoint.Systems.Controls;

namespace Epoint.Modules.HRM
{
	public partial class frmTheoDoiXe_Edit : Epoint.Lists.frmEdit
	{
		Control ctrlFind;
        txtTextBox txtMa_CbNv;
		#region Phuong thuc

		public frmTheoDoiXe_Edit()
		{
			InitializeComponent();
			
			GetControl();
		}

		public override void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{
			this.drEdit = drEdit;
			this.enuNew_Edit = enuNew_Edit;
			this.Tag = (char)enuNew_Edit + "," + this.Tag;

            //string strCreate_Log = Common.Show_Log((string)drEdit["Create_Log"]);
            //string strLastModify_Log = Common.Show_Log((string)drEdit["LastModify_Log"]);
            //string strLog = string.Empty;
            //strLog += strCreate_Log != string.Empty ? "Create: " + strCreate_Log : "";
            //strLog += strLastModify_Log != string.Empty ? "; Last Modify: " + strLastModify_Log : "";
            //this.ucNotice.SetText(strLog);

			Common.ScaterMemvar(this, ref drEdit);

            //Lay Ma_CbNv
            string sqlMa_CbNv = "SELECT Ma_CbNv FROM L00MEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'";
            DataTable dtMa_CbNv = SQLExec.ExecuteReturnDt(sqlMa_CbNv);
            if (dtMa_CbNv != null)
            {
                foreach (DataRow drMa_CbNv in dtMa_CbNv.Rows)
                {
                    if (enuNew_Edit == enuEdit.New)
                        txtMa_CbNv.Text = drMa_CbNv["Ma_CbNv"].ToString();
                }
            }

			BindingLanguage();

			this.ShowDialog();
		}

		private void GetControl()
		{
            //this.FindControl("txtMA_CBNV", typeof(txtTextBox), ref ctrlFind);
            //if (ctrlFind != null)
            //{
            //    txtMa_CbNv = (txtTextBox)ctrlFind;
            //    txtMa_CbNv.ReadOnly = true;
            //}
		}

        //public override bool FormCheckValid()
        //{
        //    bool bvalid = true;
        //    Control ctrl = this.FindInValidControl(this);

        //    if (ctrl != null)
        //    {
        //        if (ctrl.Name.Length > 3)
        //            Common.MsgOk(Languages.GetLanguage(ctrl.Name.Substring(3)) + " " +
        //                          Languages.GetLanguage("Not_Null"));
        //        else
        //            Common.MsgOk(ctrl.Name + " " +
        //                          Languages.GetLanguage("Not_Null"));

        //        return false;
        //    }

        //    return bvalid;
        //}

		public override bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);

            //Mac dinh chua check Duyet
            if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
            {
                drEdit["Duyet"] = false;
                drEdit["Duyet_Log"] = string.Empty;
            }
            
			//Kiem tra Valid tren Form
			if (!FormCheckValid())
				return false;

            //if (this.enuNew_Edit == enuEdit.New || this.enuNew_Edit == enuEdit.Copy)
            //{
            //    drEdit["Create_Log"] = Common.GetCurrent_Log();
            //    drEdit["LastModify_Log"] = string.Empty;
            //    drEdit["Hinh"] = new byte[] { };
            //}
            //else
            //{
            //    drEdit["LastModify_Log"] = Common.GetCurrent_Log();
            //    if ((string)drEdit["Create_Log"] == string.Empty)
            //        drEdit["Create_Log"] = drEdit["LastModify_Log"];
            //}

			//Kiem tra Valid CSDL
			if (!DataTool.SQLUpdate(enuNew_Edit, "L14LTDX", ref drEdit))
				return false;			

			//Doi ma
			if (this.enuNew_Edit == enuEdit.Edit)
				DataTool.SQLChangeID("MA_CBNV", drEdit);

			return true;
		}

		#endregion 

		#region Su kien

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!Element.sysIs_Admin && enuNew_Edit == enuEdit.Edit && (bool)drEdit["Duyet"])
            {
                btgAccept.btAccept.Enabled = false;
            }
        }
		#endregion
	}
}