namespace Epoint.Modules.Salary
{
	partial class frmTsTn_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTsTn_Edit));
            this.dteNgay_Ap = new Epoint.Systems.Controls.txtDateTime();
            this.lblDien_Giai = new Epoint.Systems.Controls.lblControl();
            this.lblTk = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lbtTen_Tn = new Epoint.Systems.Controls.lbtControl();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.numMuc_Ap = new Epoint.Systems.Controls.numControl();
            this.lbtDvt = new System.Windows.Forms.Label();
            this.txtMa_Tn = new Epoint.Systems.Controls.txtTextBox();
            this.SuspendLayout();
            // 
            // dteNgay_Ap
            // 
            this.dteNgay_Ap.bAllowEmpty = true;
            this.dteNgay_Ap.bRequire = false;
            this.dteNgay_Ap.bSelectOnFocus = false;
            this.dteNgay_Ap.bShowDateTimePicker = true;
            this.dteNgay_Ap.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ap.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ap.Location = new System.Drawing.Point(146, 58);
            this.dteNgay_Ap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ap.Mask = "00/00/0000";
            this.dteNgay_Ap.Name = "dteNgay_Ap";
            this.dteNgay_Ap.Size = new System.Drawing.Size(96, 20);
            this.dteNgay_Ap.TabIndex = 2;
            // 
            // lblDien_Giai
            // 
            this.lblDien_Giai.AutoEllipsis = true;
            this.lblDien_Giai.AutoSize = true;
            this.lblDien_Giai.BackColor = System.Drawing.Color.Transparent;
            this.lblDien_Giai.Location = new System.Drawing.Point(44, 61);
            this.lblDien_Giai.Name = "lblDien_Giai";
            this.lblDien_Giai.Size = new System.Drawing.Size(47, 13);
            this.lblDien_Giai.TabIndex = 73;
            this.lblDien_Giai.Tag = "Ngay_Ap";
            this.lblDien_Giai.Text = "Ngày áp";
            this.lblDien_Giai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTk
            // 
            this.lblTk.AutoEllipsis = true;
            this.lblTk.AutoSize = true;
            this.lblTk.BackColor = System.Drawing.Color.Transparent;
            this.lblTk.Location = new System.Drawing.Point(44, 38);
            this.lblTk.Name = "lblTk";
            this.lblTk.Size = new System.Drawing.Size(67, 13);
            this.lblTk.TabIndex = 70;
            this.lblTk.Tag = "Ma_Tn";
            this.lblTk.Text = "Mã thu nhập";
            this.lblTk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(333, 131);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 4;
            // 
            // lbtTen_Tn
            // 
            this.lbtTen_Tn.AutoEllipsis = true;
            this.lbtTen_Tn.AutoSize = true;
            this.lbtTen_Tn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbtTen_Tn.Location = new System.Drawing.Point(247, 38);
            this.lbtTen_Tn.Name = "lbtTen_Tn";
            this.lbtTen_Tn.Size = new System.Drawing.Size(56, 13);
            this.lbtTen_Tn.TabIndex = 1;
            this.lbtTen_Tn.Tag = "";
            this.lbtTen_Tn.Text = "txtTen_Tn";
            this.lbtTen_Tn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(44, 84);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(43, 13);
            this.lblControl1.TabIndex = 70;
            this.lblControl1.Tag = "";
            this.lblControl1.Text = "Mức áp";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numMuc_Ap
            // 
            this.numMuc_Ap.bEnabled = true;
            this.numMuc_Ap.bFormat = true;
            this.numMuc_Ap.bIsLookup = false;
            this.numMuc_Ap.bReadOnly = false;
            this.numMuc_Ap.bRequire = false;
            this.numMuc_Ap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numMuc_Ap.KeyFilter = "";
            this.numMuc_Ap.Location = new System.Drawing.Point(146, 81);
            this.numMuc_Ap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numMuc_Ap.MaxLength = 20;
            this.numMuc_Ap.Name = "numMuc_Ap";
            this.numMuc_Ap.Scale = 2;
            this.numMuc_Ap.Size = new System.Drawing.Size(139, 20);
            this.numMuc_Ap.TabIndex = 3;
            this.numMuc_Ap.Text = "0.00";
            this.numMuc_Ap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMuc_Ap.UseAutoFilter = false;
            this.numMuc_Ap.Value = 0D;
            // 
            // lbtDvt
            // 
            this.lbtDvt.AutoSize = true;
            this.lbtDvt.Location = new System.Drawing.Point(290, 84);
            this.lbtDvt.Name = "lbtDvt";
            this.lbtDvt.Size = new System.Drawing.Size(24, 13);
            this.lbtDvt.TabIndex = 74;
            this.lbtDvt.Text = "Dvt";
            // 
            // txtMa_Tn
            // 
            this.txtMa_Tn.bEnabled = true;
            this.txtMa_Tn.bIsLookup = false;
            this.txtMa_Tn.bReadOnly = false;
            this.txtMa_Tn.bRequire = false;
            this.txtMa_Tn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Tn.KeyFilter = "";
            this.txtMa_Tn.Location = new System.Drawing.Point(146, 35);
            this.txtMa_Tn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Tn.MaxLength = 20;
            this.txtMa_Tn.Name = "txtMa_Tn";
            this.txtMa_Tn.Size = new System.Drawing.Size(96, 20);
            this.txtMa_Tn.TabIndex = 0;
            this.txtMa_Tn.UseAutoFilter = false;
            // 
            // frmTsTn_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 180);
            this.Controls.Add(this.txtMa_Tn);
            this.Controls.Add(this.lbtDvt);
            this.Controls.Add(this.dteNgay_Ap);
            this.Controls.Add(this.lblDien_Giai);
            this.Controls.Add(this.numMuc_Ap);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.lbtTen_Tn);
            this.Controls.Add(this.lblTk);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTsTn_Edit";
            this.Tag = "frmTsTn_Edit";
            this.Text = "frmTsTn_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Epoint.Systems.Controls.txtDateTime dteNgay_Ap;
        private Epoint.Systems.Controls.lblControl lblDien_Giai;
		private Epoint.Systems.Controls.lblControl lblTk;
		private Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lbtControl lbtTen_Tn;
		private Epoint.Systems.Controls.lblControl lblControl1;
		private Epoint.Systems.Controls.numControl numMuc_Ap;
        private System.Windows.Forms.Label lbtDvt;
        private Systems.Controls.txtTextBox txtMa_Tn;
	}
}