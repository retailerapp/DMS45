namespace Epoint.Modules.HRM
{
    partial class frmCongTac_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongTac_Edit));
            this.dteNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.lbtTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblNgay_End = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lblDia_Diem = new Epoint.Systems.Controls.lblControl();
            this.txtDia_Diem = new Epoint.Systems.Controls.txtTextBox();
            this.lblLy_Do = new Epoint.Systems.Controls.lblControl();
            this.txtLy_Do = new Epoint.Systems.Controls.txtTextBox();
            this.lblTam_Ung = new Epoint.Systems.Controls.lblControl();
            this.numTam_Ung = new Epoint.Systems.Controls.numControl();
            this.lblNguoi_Duyet = new Epoint.Systems.Controls.lblControl();
            this.txtNguoi_Duyet = new Epoint.Systems.Controls.txtTextBox();
            this.lblNguoi_Di_Cung = new Epoint.Systems.Controls.lblControl();
            this.txtNguoi_Di_Cung = new Epoint.Systems.Controls.txtTextBox();
            this.SuspendLayout();
            // 
            // dteNgay_End
            // 
            this.dteNgay_End.bAllowEmpty = true;
            this.dteNgay_End.bRequire = false;
            this.dteNgay_End.bSelectOnFocus = false;
            this.dteNgay_End.bShowDateTimePicker = true;
            this.dteNgay_End.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_End.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_End.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_End.Location = new System.Drawing.Point(133, 99);
            this.dteNgay_End.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_End.Mask = "00/00/0000";
            this.dteNgay_End.Name = "dteNgay_End";
            this.dteNgay_End.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_End.TabIndex = 3;
            // 
            // dteNgay_Begin
            // 
            this.dteNgay_Begin.bAllowEmpty = true;
            this.dteNgay_Begin.bRequire = false;
            this.dteNgay_Begin.bSelectOnFocus = false;
            this.dteNgay_Begin.bShowDateTimePicker = true;
            this.dteNgay_Begin.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_Begin.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Begin.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Begin.Location = new System.Drawing.Point(133, 76);
            this.dteNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Begin.Mask = "00/00/0000";
            this.dteNgay_Begin.Name = "dteNgay_Begin";
            this.dteNgay_Begin.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_Begin.TabIndex = 2;
            // 
            // lbtTen_CbNv
            // 
            this.lbtTen_CbNv.AutoEllipsis = true;
            this.lbtTen_CbNv.AutoSize = true;
            this.lbtTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_CbNv.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_CbNv.Location = new System.Drawing.Point(302, 33);
            this.lbtTen_CbNv.Name = "lbtTen_CbNv";
            this.lbtTen_CbNv.Size = new System.Drawing.Size(59, 13);
            this.lbtTen_CbNv.TabIndex = 128;
            this.lbtTen_CbNv.Text = "Ten_CbNv";
            this.lbtTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(377, 236);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 34);
            this.btgAccept.TabIndex = 9;
            // 
            // lblNgay_End
            // 
            this.lblNgay_End.AutoEllipsis = true;
            this.lblNgay_End.AutoSize = true;
            this.lblNgay_End.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_End.Location = new System.Drawing.Point(24, 102);
            this.lblNgay_End.Name = "lblNgay_End";
            this.lblNgay_End.Size = new System.Drawing.Size(74, 13);
            this.lblNgay_End.TabIndex = 126;
            this.lblNgay_End.Tag = "Ngay_End";
            this.lblNgay_End.Text = "Ngày kết thúc";
            this.lblNgay_End.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_Begin
            // 
            this.lblNgay_Begin.AutoEllipsis = true;
            this.lblNgay_Begin.AutoSize = true;
            this.lblNgay_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Begin.Location = new System.Drawing.Point(24, 79);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 125;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(24, 33);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 120;
            this.lblMa_CbNv.Tag = "Ma_CbNv";
            this.lblMa_CbNv.Text = "Mã nhân viên";
            this.lblMa_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMa_CbNv
            // 
            this.txtMa_CbNv.bEnabled = true;
            this.txtMa_CbNv.bIsLookup = false;
            this.txtMa_CbNv.bReadOnly = false;
            this.txtMa_CbNv.bRequire = false;
            this.txtMa_CbNv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_CbNv.ColumnsView = null;
            this.txtMa_CbNv.KeyFilter = "Ma_CbNv";
            this.txtMa_CbNv.Location = new System.Drawing.Point(133, 30);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(164, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // lblDia_Diem
            // 
            this.lblDia_Diem.AutoEllipsis = true;
            this.lblDia_Diem.AutoSize = true;
            this.lblDia_Diem.BackColor = System.Drawing.Color.Transparent;
            this.lblDia_Diem.Location = new System.Drawing.Point(24, 56);
            this.lblDia_Diem.Name = "lblDia_Diem";
            this.lblDia_Diem.Size = new System.Drawing.Size(49, 13);
            this.lblDia_Diem.TabIndex = 130;
            this.lblDia_Diem.Tag = "Dia_Diem_CT";
            this.lblDia_Diem.Text = "Địa điểm";
            this.lblDia_Diem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDia_Diem
            // 
            this.txtDia_Diem.bEnabled = true;
            this.txtDia_Diem.bIsLookup = false;
            this.txtDia_Diem.bReadOnly = false;
            this.txtDia_Diem.bRequire = false;
            this.txtDia_Diem.KeyFilter = "";
            this.txtDia_Diem.Location = new System.Drawing.Point(133, 53);
            this.txtDia_Diem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtDia_Diem.Name = "txtDia_Diem";
            this.txtDia_Diem.Size = new System.Drawing.Size(415, 20);
            this.txtDia_Diem.TabIndex = 1;
            this.txtDia_Diem.UseAutoFilter = false;
            // 
            // lblLy_Do
            // 
            this.lblLy_Do.AutoEllipsis = true;
            this.lblLy_Do.AutoSize = true;
            this.lblLy_Do.BackColor = System.Drawing.Color.Transparent;
            this.lblLy_Do.Location = new System.Drawing.Point(24, 125);
            this.lblLy_Do.Name = "lblLy_Do";
            this.lblLy_Do.Size = new System.Drawing.Size(33, 13);
            this.lblLy_Do.TabIndex = 132;
            this.lblLy_Do.Tag = "Ly_Do";
            this.lblLy_Do.Text = "Lý do";
            this.lblLy_Do.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLy_Do
            // 
            this.txtLy_Do.bEnabled = true;
            this.txtLy_Do.bIsLookup = false;
            this.txtLy_Do.bReadOnly = false;
            this.txtLy_Do.bRequire = false;
            this.txtLy_Do.KeyFilter = "";
            this.txtLy_Do.Location = new System.Drawing.Point(133, 122);
            this.txtLy_Do.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtLy_Do.Name = "txtLy_Do";
            this.txtLy_Do.Size = new System.Drawing.Size(415, 20);
            this.txtLy_Do.TabIndex = 5;
            this.txtLy_Do.UseAutoFilter = false;
            // 
            // lblTam_Ung
            // 
            this.lblTam_Ung.AutoEllipsis = true;
            this.lblTam_Ung.AutoSize = true;
            this.lblTam_Ung.BackColor = System.Drawing.Color.Transparent;
            this.lblTam_Ung.Location = new System.Drawing.Point(24, 148);
            this.lblTam_Ung.Name = "lblTam_Ung";
            this.lblTam_Ung.Size = new System.Drawing.Size(49, 13);
            this.lblTam_Ung.TabIndex = 136;
            this.lblTam_Ung.Tag = "Tam_Ung";
            this.lblTam_Ung.Text = "Tạm ứng";
            this.lblTam_Ung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numTam_Ung
            // 
            this.numTam_Ung.bEnabled = true;
            this.numTam_Ung.bFormat = true;
            this.numTam_Ung.bIsLookup = false;
            this.numTam_Ung.bReadOnly = false;
            this.numTam_Ung.bRequire = false;
            this.numTam_Ung.KeyFilter = "";
            this.numTam_Ung.Location = new System.Drawing.Point(133, 145);
            this.numTam_Ung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numTam_Ung.Name = "numTam_Ung";
            this.numTam_Ung.Scale = 2;
            this.numTam_Ung.Size = new System.Drawing.Size(164, 20);
            this.numTam_Ung.TabIndex = 6;
            this.numTam_Ung.Text = "0.00";
            this.numTam_Ung.UseAutoFilter = false;
            this.numTam_Ung.Value = 0D;
            // 
            // lblNguoi_Duyet
            // 
            this.lblNguoi_Duyet.AutoEllipsis = true;
            this.lblNguoi_Duyet.AutoSize = true;
            this.lblNguoi_Duyet.BackColor = System.Drawing.Color.Transparent;
            this.lblNguoi_Duyet.Location = new System.Drawing.Point(24, 171);
            this.lblNguoi_Duyet.Name = "lblNguoi_Duyet";
            this.lblNguoi_Duyet.Size = new System.Drawing.Size(64, 13);
            this.lblNguoi_Duyet.TabIndex = 138;
            this.lblNguoi_Duyet.Tag = "Nguoi_Duyet";
            this.lblNguoi_Duyet.Text = "Người duyệt";
            this.lblNguoi_Duyet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNguoi_Duyet
            // 
            this.txtNguoi_Duyet.bEnabled = true;
            this.txtNguoi_Duyet.bIsLookup = false;
            this.txtNguoi_Duyet.bReadOnly = false;
            this.txtNguoi_Duyet.bRequire = false;
            this.txtNguoi_Duyet.KeyFilter = "";
            this.txtNguoi_Duyet.Location = new System.Drawing.Point(133, 168);
            this.txtNguoi_Duyet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNguoi_Duyet.Name = "txtNguoi_Duyet";
            this.txtNguoi_Duyet.Size = new System.Drawing.Size(415, 20);
            this.txtNguoi_Duyet.TabIndex = 7;
            this.txtNguoi_Duyet.UseAutoFilter = false;
            // 
            // lblNguoi_Di_Cung
            // 
            this.lblNguoi_Di_Cung.AutoEllipsis = true;
            this.lblNguoi_Di_Cung.AutoSize = true;
            this.lblNguoi_Di_Cung.BackColor = System.Drawing.Color.Transparent;
            this.lblNguoi_Di_Cung.Location = new System.Drawing.Point(24, 194);
            this.lblNguoi_Di_Cung.Name = "lblNguoi_Di_Cung";
            this.lblNguoi_Di_Cung.Size = new System.Drawing.Size(74, 13);
            this.lblNguoi_Di_Cung.TabIndex = 140;
            this.lblNguoi_Di_Cung.Tag = "Nguoi_Di_Cung";
            this.lblNguoi_Di_Cung.Text = "Người đi cùng";
            this.lblNguoi_Di_Cung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNguoi_Di_Cung
            // 
            this.txtNguoi_Di_Cung.bEnabled = true;
            this.txtNguoi_Di_Cung.bIsLookup = false;
            this.txtNguoi_Di_Cung.bReadOnly = false;
            this.txtNguoi_Di_Cung.bRequire = false;
            this.txtNguoi_Di_Cung.KeyFilter = "";
            this.txtNguoi_Di_Cung.Location = new System.Drawing.Point(133, 191);
            this.txtNguoi_Di_Cung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNguoi_Di_Cung.Name = "txtNguoi_Di_Cung";
            this.txtNguoi_Di_Cung.Size = new System.Drawing.Size(415, 20);
            this.txtNguoi_Di_Cung.TabIndex = 8;
            this.txtNguoi_Di_Cung.UseAutoFilter = false;
            // 
            // frmCongTac_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 287);
            this.Controls.Add(this.lblNguoi_Di_Cung);
            this.Controls.Add(this.txtNguoi_Di_Cung);
            this.Controls.Add(this.lblNguoi_Duyet);
            this.Controls.Add(this.txtNguoi_Duyet);
            this.Controls.Add(this.lblTam_Ung);
            this.Controls.Add(this.numTam_Ung);
            this.Controls.Add(this.lblLy_Do);
            this.Controls.Add(this.txtLy_Do);
            this.Controls.Add(this.lblDia_Diem);
            this.Controls.Add(this.txtDia_Diem);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_End);
            this.Controls.Add(this.dteNgay_Begin);
            this.Controls.Add(this.lbtTen_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblNgay_End);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCongTac_Edit";
            this.Text = "frmCongTac_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private Epoint.Systems.Controls.txtDateTime dteNgay_End;
		private Epoint.Systems.Controls.txtDateTime dteNgay_Begin;
		private Epoint.Systems.Controls.lblControl lbtTen_CbNv;
        public Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblNgay_End;
        private Epoint.Systems.Controls.lblControl lblNgay_Begin;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.lblControl lblDia_Diem;
        private Systems.Controls.txtTextBox txtDia_Diem;
        private Systems.Controls.lblControl lblLy_Do;
        private Systems.Controls.txtTextBox txtLy_Do;
        private Systems.Controls.lblControl lblTam_Ung;
        private Systems.Controls.numControl numTam_Ung;
        private Systems.Controls.lblControl lblNguoi_Duyet;
        private Systems.Controls.txtTextBox txtNguoi_Duyet;
        private Systems.Controls.lblControl lblNguoi_Di_Cung;
        private Systems.Controls.txtTextBox txtNguoi_Di_Cung;

	}
}