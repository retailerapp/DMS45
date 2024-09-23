namespace Epoint.Modules.HRM
{
    partial class frmEmailConfig
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmailConfig));
            this.btOk = new Epoint.Systems.Controls.btControl();
            this.lblHost_Mail = new Epoint.Systems.Controls.lblControl();
            this.lblEmail = new Epoint.Systems.Controls.lblControl();
            this.btCancel = new Epoint.Systems.Controls.btControl();
            this.txtHostMail = new Epoint.Systems.Controls.txtTextLookup();
            this.txtEmail = new Epoint.Systems.Controls.txtTextLookup();
            this.lblPassword = new Epoint.Systems.Controls.lblControl();
            this.txtPassWord = new Epoint.Systems.Controls.txtTextLookup();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOk.ForeColor = System.Drawing.Color.MediumBlue;
            this.btOk.Image = ((System.Drawing.Image)(resources.GetObject("btOk.Image")));
            this.btOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOk.Location = new System.Drawing.Point(227, 142);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(80, 29);
            this.btOk.TabIndex = 5;
            this.btOk.Tag = "";
            this.btOk.Text = "Đồ&ng ý";
            this.btOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // lblHost_Mail
            // 
            this.lblHost_Mail.AutoEllipsis = true;
            this.lblHost_Mail.AutoSize = true;
            this.lblHost_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lblHost_Mail.Location = new System.Drawing.Point(41, 38);
            this.lblHost_Mail.Name = "lblHost_Mail";
            this.lblHost_Mail.Size = new System.Drawing.Size(50, 13);
            this.lblHost_Mail.TabIndex = 2;
            this.lblHost_Mail.Tag = "Host_Mail";
            this.lblHost_Mail.Text = "Host mail";
            this.lblHost_Mail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoEllipsis = true;
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Location = new System.Drawing.Point(41, 61);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Tag = "Email";
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.ForeColor = System.Drawing.Color.MediumBlue;
            this.btCancel.Image = ((System.Drawing.Image)(resources.GetObject("btCancel.Image")));
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(315, 142);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(80, 29);
            this.btCancel.TabIndex = 6;
            this.btCancel.Tag = "Cancel";
            this.btCancel.Text = "&Hủy bỏ";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // txtHostMail
            // 
            this.txtHostMail.bEnabled = true;
            this.txtHostMail.bIsLookup = false;
            this.txtHostMail.bReadOnly = false;
            this.txtHostMail.bRequire = false;
            this.txtHostMail.KeyFilter = "";
            this.txtHostMail.Location = new System.Drawing.Point(126, 35);
            this.txtHostMail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtHostMail.Name = "txtHostMail";
            this.txtHostMail.Size = new System.Drawing.Size(225, 20);
            this.txtHostMail.TabIndex = 2;
            this.txtHostMail.UseAutoFilter = false;
            // 
            // txtEmail
            // 
            this.txtEmail.bEnabled = true;
            this.txtEmail.bIsLookup = false;
            this.txtEmail.bReadOnly = false;
            this.txtEmail.bRequire = false;
            this.txtEmail.KeyFilter = "";
            this.txtEmail.Location = new System.Drawing.Point(126, 58);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(225, 20);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.UseAutoFilter = false;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoEllipsis = true;
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Location = new System.Drawing.Point(41, 86);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(52, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Tag = "Password";
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassWord
            // 
            this.txtPassWord.bEnabled = true;
            this.txtPassWord.bIsLookup = false;
            this.txtPassWord.bReadOnly = false;
            this.txtPassWord.bRequire = false;
            this.txtPassWord.KeyFilter = "";
            this.txtPassWord.Location = new System.Drawing.Point(126, 83);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(225, 20);
            this.txtPassWord.TabIndex = 3;
            this.txtPassWord.UseAutoFilter = false;
            // 
            // frmEmailConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(425, 182);
            this.Controls.Add(this.txtHostMail);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblHost_Mail);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Name = "frmEmailConfig";
            this.Tag = "frmEmailConfig";
            this.Text = "frmEmailConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private Epoint.Systems.Controls.btControl btOk;
        private Epoint.Systems.Controls.lblControl lblHost_Mail;
        private Epoint.Systems.Controls.lblControl lblEmail;
        private Epoint.Systems.Controls.btControl btCancel;
        private Systems.Controls.txtTextLookup txtHostMail;
        private Systems.Controls.txtTextLookup txtEmail;
        private Systems.Controls.lblControl lblPassword;
        private Systems.Controls.txtTextLookup txtPassWord;
	}
}