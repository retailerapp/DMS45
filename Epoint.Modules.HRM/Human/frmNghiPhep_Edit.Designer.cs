namespace Epoint.Modules.HRM
{
    partial class frmNghiPhep_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNghiPhep_Edit));
            this.numSo_Ngay_Nghi = new Epoint.Systems.Controls.numUpDown();
            this.cboLoai_Phep = new Epoint.Systems.Controls.cboControl();
            this.chkCo_Tinh_Luong = new Epoint.Systems.Controls.chkControl();
            this.dteNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblNoi_Dung = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_End = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.lblNguoi_Duyet = new Epoint.Systems.Controls.lblControl();
            this.lblSo_Ngay_Nghi = new Epoint.Systems.Controls.lblControl();
            this.txtNoi_Dung = new Epoint.Systems.Controls.txtTextBox();
            this.txtNguoi_Duyet = new Epoint.Systems.Controls.txtTextBox();
            this.lblLoai_Phep = new Epoint.Systems.Controls.lblControl();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtGhi_Chu = new Epoint.Systems.Controls.txtTextBox();
            this.lblGhi_Chu = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lblTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.lblGio_End = new Epoint.Systems.Controls.lblControl();
            this.lblGio_Begin = new Epoint.Systems.Controls.lblControl();
            this.txtGio_Begin = new Epoint.Systems.Controls.txtTextBox();
            this.txtGio_End = new Epoint.Systems.Controls.txtTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSo_Ngay_Nghi)).BeginInit();
            this.SuspendLayout();
            // 
            // numSo_Ngay_Nghi
            // 
            this.numSo_Ngay_Nghi.Location = new System.Drawing.Point(122, 200);
            this.numSo_Ngay_Nghi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numSo_Ngay_Nghi.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSo_Ngay_Nghi.Name = "numSo_Ngay_Nghi";
            this.numSo_Ngay_Nghi.Size = new System.Drawing.Size(72, 20);
            this.numSo_Ngay_Nghi.TabIndex = 10;
            // 
            // cboLoai_Phep
            // 
            this.cboLoai_Phep.InitValue = null;
            this.cboLoai_Phep.Items.AddRange(new object[] {
            "Nghỉ ốm",
            "Nghỉ thai sản",
            "Nghỉ sảy thai",
            "Nghỉ kết hôn",
            "Nghỉ khám thai ",
            "Nghỉ tang",
            "Nghỉ khác"});
            this.cboLoai_Phep.Location = new System.Drawing.Point(121, 107);
            this.cboLoai_Phep.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.cboLoai_Phep.Name = "cboLoai_Phep";
            this.cboLoai_Phep.Size = new System.Drawing.Size(146, 21);
            this.cboLoai_Phep.strValueList = null;
            this.cboLoai_Phep.TabIndex = 3;
            this.cboLoai_Phep.UseAutoComplete = false;
            this.cboLoai_Phep.UseBindingValue = false;
            // 
            // chkCo_Tinh_Luong
            // 
            this.chkCo_Tinh_Luong.AutoSize = true;
            this.chkCo_Tinh_Luong.ForeColor = System.Drawing.Color.Blue;
            this.chkCo_Tinh_Luong.Location = new System.Drawing.Point(217, 203);
            this.chkCo_Tinh_Luong.Name = "chkCo_Tinh_Luong";
            this.chkCo_Tinh_Luong.Size = new System.Drawing.Size(90, 17);
            this.chkCo_Tinh_Luong.TabIndex = 10;
            this.chkCo_Tinh_Luong.Tag = "Co_Tinh_Luong";
            this.chkCo_Tinh_Luong.Text = "Có tính lương";
            this.chkCo_Tinh_Luong.UseVisualStyleBackColor = true;
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
            this.dteNgay_End.Location = new System.Drawing.Point(121, 154);
            this.dteNgay_End.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_End.Mask = "00/00/0000";
            this.dteNgay_End.Name = "dteNgay_End";
            this.dteNgay_End.Size = new System.Drawing.Size(146, 20);
            this.dteNgay_End.TabIndex = 5;
            this.dteNgay_End.ValidatingType = typeof(System.DateTime);
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
            this.dteNgay_Begin.Location = new System.Drawing.Point(121, 131);
            this.dteNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Begin.Mask = "00/00/0000";
            this.dteNgay_Begin.Name = "dteNgay_Begin";
            this.dteNgay_Begin.Size = new System.Drawing.Size(146, 20);
            this.dteNgay_Begin.TabIndex = 4;
            this.dteNgay_Begin.ValidatingType = typeof(System.DateTime);
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(431, 273);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(168, 34);
            this.btgAccept.TabIndex = 12;
            // 
            // lblNoi_Dung
            // 
            this.lblNoi_Dung.AutoEllipsis = true;
            this.lblNoi_Dung.AutoSize = true;
            this.lblNoi_Dung.BackColor = System.Drawing.Color.Transparent;
            this.lblNoi_Dung.Location = new System.Drawing.Point(31, 69);
            this.lblNoi_Dung.Name = "lblNoi_Dung";
            this.lblNoi_Dung.Size = new System.Drawing.Size(50, 13);
            this.lblNoi_Dung.TabIndex = 125;
            this.lblNoi_Dung.Tag = "Noi_Dung";
            this.lblNoi_Dung.Text = "Nội dung";
            this.lblNoi_Dung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_End
            // 
            this.lblNgay_End.AutoEllipsis = true;
            this.lblNgay_End.AutoSize = true;
            this.lblNgay_End.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_End.Location = new System.Drawing.Point(31, 159);
            this.lblNgay_End.Name = "lblNgay_End";
            this.lblNgay_End.Size = new System.Drawing.Size(74, 13);
            this.lblNgay_End.TabIndex = 127;
            this.lblNgay_End.Tag = "Ngay_End";
            this.lblNgay_End.Text = "Ngày kết thúc";
            this.lblNgay_End.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_Begin
            // 
            this.lblNgay_Begin.AutoEllipsis = true;
            this.lblNgay_Begin.AutoSize = true;
            this.lblNgay_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Begin.Location = new System.Drawing.Point(31, 134);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 129;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNguoi_Duyet
            // 
            this.lblNguoi_Duyet.AutoEllipsis = true;
            this.lblNguoi_Duyet.AutoSize = true;
            this.lblNguoi_Duyet.BackColor = System.Drawing.Color.Transparent;
            this.lblNguoi_Duyet.Location = new System.Drawing.Point(31, 180);
            this.lblNguoi_Duyet.Name = "lblNguoi_Duyet";
            this.lblNguoi_Duyet.Size = new System.Drawing.Size(64, 13);
            this.lblNguoi_Duyet.TabIndex = 126;
            this.lblNguoi_Duyet.Tag = "Nguoi_Duyet";
            this.lblNguoi_Duyet.Text = "Người duyệt";
            this.lblNguoi_Duyet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSo_Ngay_Nghi
            // 
            this.lblSo_Ngay_Nghi.AutoEllipsis = true;
            this.lblSo_Ngay_Nghi.AutoSize = true;
            this.lblSo_Ngay_Nghi.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Ngay_Nghi.Location = new System.Drawing.Point(31, 202);
            this.lblSo_Ngay_Nghi.Name = "lblSo_Ngay_Nghi";
            this.lblSo_Ngay_Nghi.Size = new System.Drawing.Size(69, 13);
            this.lblSo_Ngay_Nghi.TabIndex = 123;
            this.lblSo_Ngay_Nghi.Tag = "So_Ngay_Nghi";
            this.lblSo_Ngay_Nghi.Text = "Số ngày nghỉ";
            this.lblSo_Ngay_Nghi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoi_Dung
            // 
            this.txtNoi_Dung.bEnabled = true;
            this.txtNoi_Dung.bIsLookup = false;
            this.txtNoi_Dung.bReadOnly = false;
            this.txtNoi_Dung.bRequire = false;
            this.txtNoi_Dung.KeyFilter = "";
            this.txtNoi_Dung.Location = new System.Drawing.Point(121, 66);
            this.txtNoi_Dung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNoi_Dung.Multiline = true;
            this.txtNoi_Dung.Name = "txtNoi_Dung";
            this.txtNoi_Dung.Size = new System.Drawing.Size(477, 38);
            this.txtNoi_Dung.TabIndex = 2;
            this.txtNoi_Dung.UseAutoFilter = false;
            // 
            // txtNguoi_Duyet
            // 
            this.txtNguoi_Duyet.bEnabled = true;
            this.txtNguoi_Duyet.bIsLookup = false;
            this.txtNguoi_Duyet.bReadOnly = false;
            this.txtNguoi_Duyet.bRequire = false;
            this.txtNguoi_Duyet.KeyFilter = "";
            this.txtNguoi_Duyet.Location = new System.Drawing.Point(121, 177);
            this.txtNguoi_Duyet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNguoi_Duyet.Name = "txtNguoi_Duyet";
            this.txtNguoi_Duyet.Size = new System.Drawing.Size(477, 20);
            this.txtNguoi_Duyet.TabIndex = 9;
            this.txtNguoi_Duyet.UseAutoFilter = false;
            // 
            // lblLoai_Phep
            // 
            this.lblLoai_Phep.AutoEllipsis = true;
            this.lblLoai_Phep.AutoSize = true;
            this.lblLoai_Phep.BackColor = System.Drawing.Color.Transparent;
            this.lblLoai_Phep.Location = new System.Drawing.Point(31, 110);
            this.lblLoai_Phep.Name = "lblLoai_Phep";
            this.lblLoai_Phep.Size = new System.Drawing.Size(77, 13);
            this.lblLoai_Phep.TabIndex = 124;
            this.lblLoai_Phep.Tag = "Loai_Phep";
            this.lblLoai_Phep.Text = "Loại nghỉ phép";
            this.lblLoai_Phep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(31, 23);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 122;
            this.lblMa_CbNv.Tag = "Ma_CbNv";
            this.lblMa_CbNv.Text = "Mã nhân viên";
            this.lblMa_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhi_Chu
            // 
            this.txtGhi_Chu.bEnabled = true;
            this.txtGhi_Chu.bIsLookup = false;
            this.txtGhi_Chu.bReadOnly = false;
            this.txtGhi_Chu.bRequire = false;
            this.txtGhi_Chu.KeyFilter = "";
            this.txtGhi_Chu.Location = new System.Drawing.Point(121, 223);
            this.txtGhi_Chu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGhi_Chu.Multiline = true;
            this.txtGhi_Chu.Name = "txtGhi_Chu";
            this.txtGhi_Chu.Size = new System.Drawing.Size(477, 39);
            this.txtGhi_Chu.TabIndex = 11;
            this.txtGhi_Chu.UseAutoFilter = false;
            // 
            // lblGhi_Chu
            // 
            this.lblGhi_Chu.AutoEllipsis = true;
            this.lblGhi_Chu.AutoSize = true;
            this.lblGhi_Chu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhi_Chu.Location = new System.Drawing.Point(31, 226);
            this.lblGhi_Chu.Name = "lblGhi_Chu";
            this.lblGhi_Chu.Size = new System.Drawing.Size(44, 13);
            this.lblGhi_Chu.TabIndex = 125;
            this.lblGhi_Chu.Tag = "Ghi_Chu";
            this.lblGhi_Chu.Text = "Ghi chú";
            this.lblGhi_Chu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(121, 20);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(146, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // lblTen_CbNv
            // 
            this.lblTen_CbNv.AutoEllipsis = true;
            this.lblTen_CbNv.AutoSize = true;
            this.lblTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_CbNv.Location = new System.Drawing.Point(31, 46);
            this.lblTen_CbNv.Name = "lblTen_CbNv";
            this.lblTen_CbNv.Size = new System.Drawing.Size(76, 13);
            this.lblTen_CbNv.TabIndex = 132;
            this.lblTen_CbNv.Tag = "Ten_CbNv";
            this.lblTen_CbNv.Text = "Tên nhân viên";
            this.lblTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.KeyFilter = "";
            this.txtTen_CbNv.Location = new System.Drawing.Point(121, 43);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(477, 20);
            this.txtTen_CbNv.TabIndex = 1;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // lblGio_End
            // 
            this.lblGio_End.AutoEllipsis = true;
            this.lblGio_End.AutoSize = true;
            this.lblGio_End.BackColor = System.Drawing.Color.Transparent;
            this.lblGio_End.Location = new System.Drawing.Point(282, 159);
            this.lblGio_End.Name = "lblGio_End";
            this.lblGio_End.Size = new System.Drawing.Size(65, 13);
            this.lblGio_End.TabIndex = 135;
            this.lblGio_End.Tag = "Gio_End";
            this.lblGio_End.Text = "Giờ kết thúc";
            this.lblGio_End.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGio_Begin
            // 
            this.lblGio_Begin.AutoEllipsis = true;
            this.lblGio_Begin.AutoSize = true;
            this.lblGio_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblGio_Begin.Location = new System.Drawing.Point(282, 136);
            this.lblGio_Begin.Name = "lblGio_Begin";
            this.lblGio_Begin.Size = new System.Drawing.Size(63, 13);
            this.lblGio_Begin.TabIndex = 136;
            this.lblGio_Begin.Tag = "Gio_Begin";
            this.lblGio_Begin.Text = "Giờ bắt đầu";
            this.lblGio_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGio_Begin
            // 
            this.txtGio_Begin.bEnabled = true;
            this.txtGio_Begin.bIsLookup = false;
            this.txtGio_Begin.bReadOnly = false;
            this.txtGio_Begin.bRequire = false;
            this.txtGio_Begin.KeyFilter = "";
            this.txtGio_Begin.Location = new System.Drawing.Point(350, 131);
            this.txtGio_Begin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGio_Begin.Name = "txtGio_Begin";
            this.txtGio_Begin.Size = new System.Drawing.Size(61, 20);
            this.txtGio_Begin.TabIndex = 6;
            this.txtGio_Begin.UseAutoFilter = false;
            // 
            // txtGio_End
            // 
            this.txtGio_End.bEnabled = true;
            this.txtGio_End.bIsLookup = false;
            this.txtGio_End.bReadOnly = false;
            this.txtGio_End.bRequire = false;
            this.txtGio_End.KeyFilter = "";
            this.txtGio_End.Location = new System.Drawing.Point(350, 154);
            this.txtGio_End.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGio_End.Name = "txtGio_End";
            this.txtGio_End.Size = new System.Drawing.Size(61, 20);
            this.txtGio_End.TabIndex = 7;
            this.txtGio_End.UseAutoFilter = false;
            // 
            // frmNghiPhep_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 319);
            this.Controls.Add(this.txtGio_End);
            this.Controls.Add(this.txtGio_Begin);
            this.Controls.Add(this.lblGio_End);
            this.Controls.Add(this.lblGio_Begin);
            this.Controls.Add(this.lblTen_CbNv);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.numSo_Ngay_Nghi);
            this.Controls.Add(this.cboLoai_Phep);
            this.Controls.Add(this.chkCo_Tinh_Luong);
            this.Controls.Add(this.dteNgay_End);
            this.Controls.Add(this.dteNgay_Begin);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblGhi_Chu);
            this.Controls.Add(this.lblNoi_Dung);
            this.Controls.Add(this.lblNgay_End);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.lblNguoi_Duyet);
            this.Controls.Add(this.lblSo_Ngay_Nghi);
            this.Controls.Add(this.txtGhi_Chu);
            this.Controls.Add(this.txtNoi_Dung);
            this.Controls.Add(this.txtNguoi_Duyet);
            this.Controls.Add(this.lblLoai_Phep);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNghiPhep_Edit";
            this.Text = "frmNGHIPHEP_Edit";
            ((System.ComponentModel.ISupportInitialize)(this.numSo_Ngay_Nghi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private Epoint.Systems.Controls.numUpDown numSo_Ngay_Nghi;
		private Epoint.Systems.Controls.cboControl cboLoai_Phep;
        private Epoint.Systems.Controls.chkControl chkCo_Tinh_Luong;
		private Epoint.Systems.Controls.txtDateTime dteNgay_End;
		private Epoint.Systems.Controls.txtDateTime dteNgay_Begin;
        public Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblNoi_Dung;
		private Epoint.Systems.Controls.lblControl lblNgay_End;
		private Epoint.Systems.Controls.lblControl lblNgay_Begin;
		private Epoint.Systems.Controls.lblControl lblNguoi_Duyet;
		private Epoint.Systems.Controls.lblControl lblSo_Ngay_Nghi;
		private Epoint.Systems.Controls.txtTextBox txtNoi_Dung;
		private Epoint.Systems.Controls.txtTextBox txtNguoi_Duyet;
		private Epoint.Systems.Controls.lblControl lblLoai_Phep;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
		private Epoint.Systems.Controls.txtTextBox txtGhi_Chu;
		private Epoint.Systems.Controls.lblControl lblGhi_Chu;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.lblControl lblTen_CbNv;
        private Systems.Controls.txtTextBox txtTen_CbNv;
        private Systems.Controls.lblControl lblGio_End;
        private Systems.Controls.lblControl lblGio_Begin;
        private Systems.Controls.txtTextBox txtGio_Begin;
        private Systems.Controls.txtTextBox txtGio_End;

	}
}