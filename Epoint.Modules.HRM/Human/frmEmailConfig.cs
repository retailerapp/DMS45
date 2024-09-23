using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Controls;
using Epoint.Systems.Elements;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Lists;

namespace Epoint.Modules.HRM
{
	public partial class frmEmailConfig : Epoint.Systems.Customizes.frmView
	{
        public frmEmailConfig()
		{
			InitializeComponent();

			btOk.Click += new EventHandler(btOk_Click);
			btCancel.Click += new EventHandler(btCancel_Click);
		}

		new public void Load()
		{
            
            this.BindingLanguage();
			this.Show();

            string strHost = Parameters.GetParaValue("HOSTMAIL").ToString();//"smtp.gmail.com";
            string strCredentials = Parameters.GetParaValue("MAILSERVER").ToString();//"vanhungsmile@gmail.com";
            string strPassclient = Parameters.GetParaValue("PASS_EMAIL").ToString();//"";// tự đặt


            txtHostMail.Text = strHost;
            txtEmail.Text = strCredentials;
            txtPassWord.Text = strPassclient;
		}

		

		void btOk_Click(object sender, EventArgs e)
		{
            Parameters.SetParaValue("HOSTMAIL",txtHostMail);
            Parameters.SetParaValue("MAILSERVER",txtEmail);
            Parameters.SetParaValue("PASS_EMAIL",txtPassWord);

            Common.CloseCurrentFormOnMain();
		}

		void btCancel_Click(object sender, EventArgs e)
		{
            Common.CloseCurrentFormOnMain();
		}

       
	}
}
