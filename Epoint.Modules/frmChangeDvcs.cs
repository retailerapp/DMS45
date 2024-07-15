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
using Epoint.Systems.Customizes;
using Epoint.Systems.Elements;

namespace Epoint.Modules
{
    public partial class frmChangeDvcs : Epoint.Systems.Customizes.frmEdit
    {

        string strTableName = string.Empty;
        string strColumn_Type = string.Empty;
        string strColumnName = string.Empty;
        string strOldValue = string.Empty;
        public string strStt = string.Empty;
        public string strNewValue = string.Empty;
        string strZone = string.Empty;

        public frmChangeDvcs()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
        }

        public void Load(string Stt)
        {
            //Mac dinh Ma_Data --> theo tham so he thong
            this.strStt = Stt;
            //this.ucMa_Data.cboMa_Data.Text = Element.sysMa_Data;

            //Mac dinh Ma_Data --> theo SYSDMDVCS_DEFAULTLIST
            this.ucMa_Data.cboMa_Data.Text = Convert.ToString(SQLExec.ExecuteReturnValue("SELECT Ma_DvCs FROM GLVoucher WHERE Stt ='" + this.strStt + "'"));
           
            this.BindingLanguage();
            this.ShowDialog();
        }



        private void btAccept_Click(object sender, EventArgs e)
        {
            if (this.ucMa_Data_New.cboMa_Data.Text !="*" && this.ucMa_Data_New.cboMa_Data.Text != this.ucMa_Data.cboMa_Data.Text)
            {
                SQLExec.Execute("Update   GLVoucher SET Ma_DvCs = '" + this.ucMa_Data_New.cboMa_Data.Text + "' WHERE Stt ='" + this.strStt + "'");

            }   

            isAccept = true;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            isAccept = false;
            this.Close();
        }
    }
}
