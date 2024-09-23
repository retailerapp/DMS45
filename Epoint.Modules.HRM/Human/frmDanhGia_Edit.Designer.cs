namespace Epoint.Modules.HRM
{
    partial class frmDanhGia_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDanhGia_Edit));
            this.dteNgay_DG = new Epoint.Systems.Controls.txtDateTime();
            this.lbtTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblNhan_Xet = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_DG = new Epoint.Systems.Controls.lblControl();
            this.txtNhan_Xet = new Epoint.Systems.Controls.txtTextBox();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMuc_Tieu = new Epoint.Systems.Controls.lblControl();
            this.txtMuc_Tieu = new Epoint.Systems.Controls.txtTextBox();
            this.lblDe_Xuat = new Epoint.Systems.Controls.lblControl();
            this.txtDe_Xuat = new Epoint.Systems.Controls.txtTextBox();
            this.lblDiem_Tu_DG = new Epoint.Systems.Controls.lblControl();
            this.numDiem_Tu_DG = new Epoint.Systems.Controls.numControl();
            this.lblDiem_QL_DG = new Epoint.Systems.Controls.lblControl();
            this.numDiem_QL_DG = new Epoint.Systems.Controls.numControl();
            this.chkIs_Danh_Gia = new Epoint.Systems.Controls.chkControl();
            this.SuspendLayout();
            // 
            // dteNgay_DG
            // 
            this.dteNgay_DG.bAllowEmpty = true;
            this.dteNgay_DG.bRequire = false;
            this.dteNgay_DG.bSelectOnFocus = false;
            this.dteNgay_DG.bShowDateTimePicker = true;
            this.dteNgay_DG.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_DG.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_DG.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_DG.Location = new System.Drawing.Point(149, 58);
            this.dteNgay_DG.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_DG.Mask = "00/00/0000";
            this.dteNgay_DG.Name = "dteNgay_DG";
            this.dteNgay_DG.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_DG.TabIndex = 1;
            // 
            // lbtTen_CbNv
            // 
            this.lbtTen_CbNv.AutoEllipsis = true;
            this.lbtTen_CbNv.AutoSize = true;
            this.lbtTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_CbNv.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_CbNv.Location = new System.Drawing.Point(318, 38);
            this.lbtTen_CbNv.Name = "lbtTen_CbNv";
            this.lbtTen_CbNv.Size = new System.Drawing.Size(59, 13);
            this.lbtTen_CbNv.TabIndex = 128;
            this.lbtTen_CbNv.Text = "Ten_CbNv";
            this.lbtTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(395, 258);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 9;
            // 
            // lblNhan_Xet
            // 
            this.lblNhan_Xet.AutoEllipsis = true;
            this.lblNhan_Xet.AutoSize = true;
            this.lblNhan_Xet.BackColor = System.Drawing.Color.Transparent;
            this.lblNhan_Xet.Location = new System.Drawing.Point(24, 176);
            this.lblNhan_Xet.Name = "lblNhan_Xet";
            this.lblNhan_Xet.Size = new System.Drawing.Size(108, 13);
            this.lblNhan_Xet.TabIndex = 123;
            this.lblNhan_Xet.Tag = "Nhan_Xet";
            this.lblNhan_Xet.Text = "Nhận xét của quản lý";
            this.lblNhan_Xet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_DG
            // 
            this.lblNgay_DG.AutoEllipsis = true;
            this.lblNgay_DG.AutoSize = true;
            this.lblNgay_DG.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_DG.Location = new System.Drawing.Point(24, 61);
            this.lblNgay_DG.Name = "lblNgay_DG";
            this.lblNgay_DG.Size = new System.Drawing.Size(77, 13);
            this.lblNgay_DG.TabIndex = 125;
            this.lblNgay_DG.Tag = "Ngay_DG";
            this.lblNgay_DG.Text = "Ngày đánh giá";
            this.lblNgay_DG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNhan_Xet
            // 
            this.txtNhan_Xet.bEnabled = true;
            this.txtNhan_Xet.bIsLookup = false;
            this.txtNhan_Xet.bReadOnly = false;
            this.txtNhan_Xet.bRequire = false;
            this.txtNhan_Xet.KeyFilter = "";
            this.txtNhan_Xet.Location = new System.Drawing.Point(149, 173);
            this.txtNhan_Xet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNhan_Xet.Multiline = true;
            this.txtNhan_Xet.Name = "txtNhan_Xet";
            this.txtNhan_Xet.Size = new System.Drawing.Size(415, 40);
            this.txtNhan_Xet.TabIndex = 6;
            this.txtNhan_Xet.UseAutoFilter = false;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(24, 38);
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(149, 35);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(164, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // lblMuc_Tieu
            // 
            this.lblMuc_Tieu.AutoEllipsis = true;
            this.lblMuc_Tieu.AutoSize = true;
            this.lblMuc_Tieu.BackColor = System.Drawing.Color.Transparent;
            this.lblMuc_Tieu.Location = new System.Drawing.Point(24, 130);
            this.lblMuc_Tieu.Name = "lblMuc_Tieu";
            this.lblMuc_Tieu.Size = new System.Drawing.Size(103, 13);
            this.lblMuc_Tieu.TabIndex = 130;
            this.lblMuc_Tieu.Tag = "Muc_Tieu";
            this.lblMuc_Tieu.Text = "Mục tiêu trong kỳ tới";
            this.lblMuc_Tieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMuc_Tieu
            // 
            this.txtMuc_Tieu.bEnabled = true;
            this.txtMuc_Tieu.bIsLookup = false;
            this.txtMuc_Tieu.bReadOnly = false;
            this.txtMuc_Tieu.bRequire = false;
            this.txtMuc_Tieu.KeyFilter = "";
            this.txtMuc_Tieu.Location = new System.Drawing.Point(149, 127);
            this.txtMuc_Tieu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMuc_Tieu.Name = "txtMuc_Tieu";
            this.txtMuc_Tieu.Size = new System.Drawing.Size(415, 20);
            this.txtMuc_Tieu.TabIndex = 4;
            this.txtMuc_Tieu.UseAutoFilter = false;
            // 
            // lblDe_Xuat
            // 
            this.lblDe_Xuat.AutoEllipsis = true;
            this.lblDe_Xuat.AutoSize = true;
            this.lblDe_Xuat.BackColor = System.Drawing.Color.Transparent;
            this.lblDe_Xuat.Location = new System.Drawing.Point(24, 153);
            this.lblDe_Xuat.Name = "lblDe_Xuat";
            this.lblDe_Xuat.Size = new System.Drawing.Size(115, 13);
            this.lblDe_Xuat.TabIndex = 132;
            this.lblDe_Xuat.Tag = "De_Xuat";
            this.lblDe_Xuat.Text = "Đề xuất của nhân viên";
            this.lblDe_Xuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDe_Xuat
            // 
            this.txtDe_Xuat.bEnabled = true;
            this.txtDe_Xuat.bIsLookup = false;
            this.txtDe_Xuat.bReadOnly = false;
            this.txtDe_Xuat.bRequire = false;
            this.txtDe_Xuat.KeyFilter = "";
            this.txtDe_Xuat.Location = new System.Drawing.Point(149, 150);
            this.txtDe_Xuat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtDe_Xuat.Name = "txtDe_Xuat";
            this.txtDe_Xuat.Size = new System.Drawing.Size(415, 20);
            this.txtDe_Xuat.TabIndex = 5;
            this.txtDe_Xuat.UseAutoFilter = false;
            // 
            // lblDiem_Tu_DG
            // 
            this.lblDiem_Tu_DG.AutoEllipsis = true;
            this.lblDiem_Tu_DG.AutoSize = true;
            this.lblDiem_Tu_DG.BackColor = System.Drawing.Color.Transparent;
            this.lblDiem_Tu_DG.Location = new System.Drawing.Point(24, 84);
            this.lblDiem_Tu_DG.Name = "lblDiem_Tu_DG";
            this.lblDiem_Tu_DG.Size = new System.Drawing.Size(88, 13);
            this.lblDiem_Tu_DG.TabIndex = 136;
            this.lblDiem_Tu_DG.Tag = "Diem_Tu_DG";
            this.lblDiem_Tu_DG.Text = "Điểm tự đánh giá";
            this.lblDiem_Tu_DG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numDiem_Tu_DG
            // 
            this.numDiem_Tu_DG.bEnabled = true;
            this.numDiem_Tu_DG.bFormat = true;
            this.numDiem_Tu_DG.bIsLookup = false;
            this.numDiem_Tu_DG.bReadOnly = false;
            this.numDiem_Tu_DG.bRequire = false;
            this.numDiem_Tu_DG.KeyFilter = "";
            this.numDiem_Tu_DG.Location = new System.Drawing.Point(149, 81);
            this.numDiem_Tu_DG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numDiem_Tu_DG.Name = "numDiem_Tu_DG";
            this.numDiem_Tu_DG.Scale = 2;
            this.numDiem_Tu_DG.Size = new System.Drawing.Size(164, 20);
            this.numDiem_Tu_DG.TabIndex = 2;
            this.numDiem_Tu_DG.Text = "0.00";
            this.numDiem_Tu_DG.UseAutoFilter = false;
            this.numDiem_Tu_DG.Value = 0D;
            // 
            // lblDiem_QL_DG
            // 
            this.lblDiem_QL_DG.AutoEllipsis = true;
            this.lblDiem_QL_DG.AutoSize = true;
            this.lblDiem_QL_DG.BackColor = System.Drawing.Color.Transparent;
            this.lblDiem_QL_DG.Location = new System.Drawing.Point(24, 107);
            this.lblDiem_QL_DG.Name = "lblDiem_QL_DG";
            this.lblDiem_QL_DG.Size = new System.Drawing.Size(113, 13);
            this.lblDiem_QL_DG.TabIndex = 140;
            this.lblDiem_QL_DG.Tag = "Diem_QL_DG";
            this.lblDiem_QL_DG.Text = "Điểm quản lý đánh giá";
            this.lblDiem_QL_DG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numDiem_QL_DG
            // 
            this.numDiem_QL_DG.bEnabled = true;
            this.numDiem_QL_DG.bFormat = true;
            this.numDiem_QL_DG.bIsLookup = false;
            this.numDiem_QL_DG.bReadOnly = false;
            this.numDiem_QL_DG.bRequire = false;
            this.numDiem_QL_DG.KeyFilter = "";
            this.numDiem_QL_DG.Location = new System.Drawing.Point(149, 104);
            this.numDiem_QL_DG.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numDiem_QL_DG.Name = "numDiem_QL_DG";
            this.numDiem_QL_DG.Scale = 2;
            this.numDiem_QL_DG.Size = new System.Drawing.Size(164, 20);
            this.numDiem_QL_DG.TabIndex = 3;
            this.numDiem_QL_DG.Text = "0.00";
            this.numDiem_QL_DG.UseAutoFilter = false;
            this.numDiem_QL_DG.Value = 0D;
            // 
            // chkIs_Danh_Gia
            // 
            this.chkIs_Danh_Gia.AutoSize = true;
            this.chkIs_Danh_Gia.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIs_Danh_Gia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIs_Danh_Gia.Location = new System.Drawing.Point(149, 227);
            this.chkIs_Danh_Gia.Name = "chkIs_Danh_Gia";
            this.chkIs_Danh_Gia.Size = new System.Drawing.Size(85, 17);
            this.chkIs_Danh_Gia.TabIndex = 8;
            this.chkIs_Danh_Gia.Tag = "Is_Danh_Gia";
            this.chkIs_Danh_Gia.Text = "Đã đánh giá";
            this.chkIs_Danh_Gia.UseVisualStyleBackColor = true;
            // 
            // frmDanhGia_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 315);
            this.Controls.Add(this.chkIs_Danh_Gia);
            this.Controls.Add(this.lblDiem_QL_DG);
            this.Controls.Add(this.numDiem_QL_DG);
            this.Controls.Add(this.lblDiem_Tu_DG);
            this.Controls.Add(this.numDiem_Tu_DG);
            this.Controls.Add(this.lblDe_Xuat);
            this.Controls.Add(this.txtDe_Xuat);
            this.Controls.Add(this.lblMuc_Tieu);
            this.Controls.Add(this.txtMuc_Tieu);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_DG);
            this.Controls.Add(this.lbtTen_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblNhan_Xet);
            this.Controls.Add(this.lblNgay_DG);
            this.Controls.Add(this.txtNhan_Xet);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDanhGia_Edit";
            this.Text = "frmDanhGia_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Epoint.Systems.Controls.txtDateTime dteNgay_DG;
		private Epoint.Systems.Controls.lblControl lbtTen_CbNv;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
        private Epoint.Systems.Controls.lblControl lblNhan_Xet;
        private Epoint.Systems.Controls.lblControl lblNgay_DG;
        private Epoint.Systems.Controls.txtTextBox txtNhan_Xet;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.lblControl lblMuc_Tieu;
        private Systems.Controls.txtTextBox txtMuc_Tieu;
        private Systems.Controls.lblControl lblDe_Xuat;
        private Systems.Controls.txtTextBox txtDe_Xuat;
        private Systems.Controls.lblControl lblDiem_Tu_DG;
        private Systems.Controls.numControl numDiem_Tu_DG;
        private Systems.Controls.lblControl lblDiem_QL_DG;
        private Systems.Controls.numControl numDiem_QL_DG;
        private Systems.Controls.chkControl chkIs_Danh_Gia;

	}
}