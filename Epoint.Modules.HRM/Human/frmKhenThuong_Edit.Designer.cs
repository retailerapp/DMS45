namespace Epoint.Modules.HRM
{
	partial class frmKhenThuong_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhenThuong_Edit));
            this.dteNgay_HL = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_QD = new Epoint.Systems.Controls.txtDateTime();
            this.lbtLoai_KTKL = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblNgay_HL = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_QD = new Epoint.Systems.Controls.lblControl();
            this.lblNguoi_Ky = new Epoint.Systems.Controls.lblControl();
            this.lblHinh_Thuc = new Epoint.Systems.Controls.lblControl();
            this.lblNoi_Dung = new Epoint.Systems.Controls.lblControl();
            this.txtNguoi_Ky = new Epoint.Systems.Controls.txtTextBox();
            this.txtHinh_Thuc = new Epoint.Systems.Controls.txtTextBox();
            this.lblSo_QD = new Epoint.Systems.Controls.lblControl();
            this.txtNoi_Dung = new Epoint.Systems.Controls.txtTextBox();
            this.txtSo_QD = new Epoint.Systems.Controls.txtTextBox();
            this.lblLoai_KTKL = new Epoint.Systems.Controls.lblControl();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.enuLoai_KTKL = new Epoint.Systems.Controls.txtEnum();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.SuspendLayout();
            // 
            // dteNgay_HL
            // 
            this.dteNgay_HL.bAllowEmpty = true;
            this.dteNgay_HL.bRequire = false;
            this.dteNgay_HL.bSelectOnFocus = false;
            this.dteNgay_HL.bShowDateTimePicker = true;
            this.dteNgay_HL.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_HL.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_HL.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_HL.Location = new System.Drawing.Point(133, 156);
            this.dteNgay_HL.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_HL.Mask = "00/00/0000";
            this.dteNgay_HL.Name = "dteNgay_HL";
            this.dteNgay_HL.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_HL.TabIndex = 6;
            // 
            // dteNgay_QD
            // 
            this.dteNgay_QD.bAllowEmpty = true;
            this.dteNgay_QD.bRequire = false;
            this.dteNgay_QD.bSelectOnFocus = false;
            this.dteNgay_QD.bShowDateTimePicker = true;
            this.dteNgay_QD.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_QD.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_QD.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_QD.Location = new System.Drawing.Point(133, 133);
            this.dteNgay_QD.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_QD.Mask = "00/00/0000";
            this.dteNgay_QD.Name = "dteNgay_QD";
            this.dteNgay_QD.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_QD.TabIndex = 4;
            // 
            // lbtLoai_KTKL
            // 
            this.lbtLoai_KTKL.AutoEllipsis = true;
            this.lbtLoai_KTKL.AutoSize = true;
            this.lbtLoai_KTKL.BackColor = System.Drawing.Color.Transparent;
            this.lbtLoai_KTKL.ForeColor = System.Drawing.Color.Blue;
            this.lbtLoai_KTKL.Location = new System.Drawing.Point(169, 46);
            this.lbtLoai_KTKL.Name = "lbtLoai_KTKL";
            this.lbtLoai_KTKL.Size = new System.Drawing.Size(130, 13);
            this.lbtLoai_KTKL.TabIndex = 129;
            this.lbtLoai_KTKL.Text = "1- Khen thưởng, 2- Kỷ luật";
            this.lbtLoai_KTKL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_CbNv
            // 
            this.lbtTen_CbNv.AutoEllipsis = true;
            this.lbtTen_CbNv.AutoSize = true;
            this.lbtTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_CbNv.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_CbNv.Location = new System.Drawing.Point(258, 22);
            this.lbtTen_CbNv.Name = "lbtTen_CbNv";
            this.lbtTen_CbNv.Size = new System.Drawing.Size(59, 13);
            this.lbtTen_CbNv.TabIndex = 128;
            this.lbtTen_CbNv.Text = "Ten_CbNv";
            this.lbtTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(427, 215);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 8;
            // 
            // lblNgay_HL
            // 
            this.lblNgay_HL.AutoEllipsis = true;
            this.lblNgay_HL.AutoSize = true;
            this.lblNgay_HL.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_HL.Location = new System.Drawing.Point(24, 159);
            this.lblNgay_HL.Name = "lblNgay_HL";
            this.lblNgay_HL.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_HL.TabIndex = 127;
            this.lblNgay_HL.Tag = "Ngay_HL";
            this.lblNgay_HL.Text = "Ngày hiệu lực";
            this.lblNgay_HL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_QD
            // 
            this.lblNgay_QD.AutoEllipsis = true;
            this.lblNgay_QD.AutoSize = true;
            this.lblNgay_QD.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_QD.Location = new System.Drawing.Point(25, 136);
            this.lblNgay_QD.Name = "lblNgay_QD";
            this.lblNgay_QD.Size = new System.Drawing.Size(85, 13);
            this.lblNgay_QD.TabIndex = 125;
            this.lblNgay_QD.Tag = "Ngay_QD";
            this.lblNgay_QD.Text = "Ngày quyết định";
            this.lblNgay_QD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNguoi_Ky
            // 
            this.lblNguoi_Ky.AutoEllipsis = true;
            this.lblNguoi_Ky.AutoSize = true;
            this.lblNguoi_Ky.BackColor = System.Drawing.Color.Transparent;
            this.lblNguoi_Ky.Location = new System.Drawing.Point(236, 136);
            this.lblNguoi_Ky.Name = "lblNguoi_Ky";
            this.lblNguoi_Ky.Size = new System.Drawing.Size(49, 13);
            this.lblNguoi_Ky.TabIndex = 124;
            this.lblNguoi_Ky.Tag = "Nguoi_Ky";
            this.lblNguoi_Ky.Text = "Người ký";
            this.lblNguoi_Ky.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHinh_Thuc
            // 
            this.lblHinh_Thuc.AutoEllipsis = true;
            this.lblHinh_Thuc.AutoSize = true;
            this.lblHinh_Thuc.BackColor = System.Drawing.Color.Transparent;
            this.lblHinh_Thuc.Location = new System.Drawing.Point(25, 182);
            this.lblHinh_Thuc.Name = "lblHinh_Thuc";
            this.lblHinh_Thuc.Size = new System.Drawing.Size(83, 13);
            this.lblHinh_Thuc.TabIndex = 126;
            this.lblHinh_Thuc.Tag = "Hinh_Thuc";
            this.lblHinh_Thuc.Text = "Hình thức KTKL";
            this.lblHinh_Thuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNoi_Dung
            // 
            this.lblNoi_Dung.AutoEllipsis = true;
            this.lblNoi_Dung.AutoSize = true;
            this.lblNoi_Dung.BackColor = System.Drawing.Color.Transparent;
            this.lblNoi_Dung.Location = new System.Drawing.Point(24, 91);
            this.lblNoi_Dung.Name = "lblNoi_Dung";
            this.lblNoi_Dung.Size = new System.Drawing.Size(50, 13);
            this.lblNoi_Dung.TabIndex = 122;
            this.lblNoi_Dung.Tag = "Noi_Dung";
            this.lblNoi_Dung.Text = "Nội dung";
            this.lblNoi_Dung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNguoi_Ky
            // 
            this.txtNguoi_Ky.bEnabled = true;
            this.txtNguoi_Ky.bIsLookup = false;
            this.txtNguoi_Ky.bReadOnly = false;
            this.txtNguoi_Ky.bRequire = false;
            this.txtNguoi_Ky.KeyFilter = "";
            this.txtNguoi_Ky.Location = new System.Drawing.Point(290, 133);
            this.txtNguoi_Ky.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNguoi_Ky.Name = "txtNguoi_Ky";
            this.txtNguoi_Ky.Size = new System.Drawing.Size(307, 20);
            this.txtNguoi_Ky.TabIndex = 5;
            this.txtNguoi_Ky.UseAutoFilter = false;
            // 
            // txtHinh_Thuc
            // 
            this.txtHinh_Thuc.bEnabled = true;
            this.txtHinh_Thuc.bIsLookup = false;
            this.txtHinh_Thuc.bReadOnly = false;
            this.txtHinh_Thuc.bRequire = false;
            this.txtHinh_Thuc.KeyFilter = "";
            this.txtHinh_Thuc.Location = new System.Drawing.Point(133, 179);
            this.txtHinh_Thuc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtHinh_Thuc.Name = "txtHinh_Thuc";
            this.txtHinh_Thuc.Size = new System.Drawing.Size(464, 20);
            this.txtHinh_Thuc.TabIndex = 7;
            this.txtHinh_Thuc.UseAutoFilter = false;
            // 
            // lblSo_QD
            // 
            this.lblSo_QD.AutoEllipsis = true;
            this.lblSo_QD.AutoSize = true;
            this.lblSo_QD.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_QD.Location = new System.Drawing.Point(24, 68);
            this.lblSo_QD.Name = "lblSo_QD";
            this.lblSo_QD.Size = new System.Drawing.Size(73, 13);
            this.lblSo_QD.TabIndex = 123;
            this.lblSo_QD.Tag = "So_QD";
            this.lblSo_QD.Text = "Số quyết định";
            this.lblSo_QD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoi_Dung
            // 
            this.txtNoi_Dung.bEnabled = true;
            this.txtNoi_Dung.bIsLookup = false;
            this.txtNoi_Dung.bReadOnly = false;
            this.txtNoi_Dung.bRequire = false;
            this.txtNoi_Dung.KeyFilter = "";
            this.txtNoi_Dung.Location = new System.Drawing.Point(133, 88);
            this.txtNoi_Dung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNoi_Dung.Multiline = true;
            this.txtNoi_Dung.Name = "txtNoi_Dung";
            this.txtNoi_Dung.Size = new System.Drawing.Size(464, 42);
            this.txtNoi_Dung.TabIndex = 3;
            this.txtNoi_Dung.UseAutoFilter = false;
            // 
            // txtSo_QD
            // 
            this.txtSo_QD.bEnabled = true;
            this.txtSo_QD.bIsLookup = false;
            this.txtSo_QD.bReadOnly = false;
            this.txtSo_QD.bRequire = false;
            this.txtSo_QD.KeyFilter = "";
            this.txtSo_QD.Location = new System.Drawing.Point(133, 65);
            this.txtSo_QD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtSo_QD.Name = "txtSo_QD";
            this.txtSo_QD.Size = new System.Drawing.Size(120, 20);
            this.txtSo_QD.TabIndex = 2;
            this.txtSo_QD.UseAutoFilter = false;
            // 
            // lblLoai_KTKL
            // 
            this.lblLoai_KTKL.AutoEllipsis = true;
            this.lblLoai_KTKL.AutoSize = true;
            this.lblLoai_KTKL.BackColor = System.Drawing.Color.Transparent;
            this.lblLoai_KTKL.Location = new System.Drawing.Point(25, 45);
            this.lblLoai_KTKL.Name = "lblLoai_KTKL";
            this.lblLoai_KTKL.Size = new System.Drawing.Size(72, 13);
            this.lblLoai_KTKL.TabIndex = 121;
            this.lblLoai_KTKL.Tag = "Loai_KTKL";
            this.lblLoai_KTKL.Text = "Mã nhân viên";
            this.lblLoai_KTKL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(24, 22);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 120;
            this.lblMa_CbNv.Tag = "Ma_CbNv";
            this.lblMa_CbNv.Text = "Mã nhân viên";
            this.lblMa_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // enuLoai_KTKL
            // 
            this.enuLoai_KTKL.bEnabled = true;
            this.enuLoai_KTKL.bIsLookup = false;
            this.enuLoai_KTKL.bReadOnly = false;
            this.enuLoai_KTKL.bRequire = false;
            this.enuLoai_KTKL.InputMask = "1,2";
            this.enuLoai_KTKL.KeyFilter = "";
            this.enuLoai_KTKL.Location = new System.Drawing.Point(133, 42);
            this.enuLoai_KTKL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.enuLoai_KTKL.Name = "enuLoai_KTKL";
            this.enuLoai_KTKL.Size = new System.Drawing.Size(25, 20);
            this.enuLoai_KTKL.TabIndex = 1;
            this.enuLoai_KTKL.Text = "1";
            this.enuLoai_KTKL.UseAutoFilter = false;
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(133, 19);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(120, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // frmKhenThuong_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 264);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_HL);
            this.Controls.Add(this.dteNgay_QD);
            this.Controls.Add(this.lbtLoai_KTKL);
            this.Controls.Add(this.lbtTen_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblNgay_HL);
            this.Controls.Add(this.lblNgay_QD);
            this.Controls.Add(this.lblNguoi_Ky);
            this.Controls.Add(this.lblHinh_Thuc);
            this.Controls.Add(this.lblNoi_Dung);
            this.Controls.Add(this.txtNguoi_Ky);
            this.Controls.Add(this.txtHinh_Thuc);
            this.Controls.Add(this.lblSo_QD);
            this.Controls.Add(this.txtNoi_Dung);
            this.Controls.Add(this.txtSo_QD);
            this.Controls.Add(this.lblLoai_KTKL);
            this.Controls.Add(this.lblMa_CbNv);
            this.Controls.Add(this.enuLoai_KTKL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKhenThuong_Edit";
            this.Text = "frmKhenThuong_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Epoint.Systems.Controls.txtDateTime dteNgay_HL;
		private Epoint.Systems.Controls.txtDateTime dteNgay_QD;
		private Epoint.Systems.Controls.lblControl lbtLoai_KTKL;
		private Epoint.Systems.Controls.lblControl lbtTen_CbNv;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblNgay_HL;
		private Epoint.Systems.Controls.lblControl lblNgay_QD;
		private Epoint.Systems.Controls.lblControl lblNguoi_Ky;
		private Epoint.Systems.Controls.lblControl lblHinh_Thuc;
		private Epoint.Systems.Controls.lblControl lblNoi_Dung;
		private Epoint.Systems.Controls.txtTextBox txtNguoi_Ky;
		private Epoint.Systems.Controls.txtTextBox txtHinh_Thuc;
		private Epoint.Systems.Controls.lblControl lblSo_QD;
		private Epoint.Systems.Controls.txtTextBox txtNoi_Dung;
		private Epoint.Systems.Controls.txtTextBox txtSo_QD;
		private Epoint.Systems.Controls.lblControl lblLoai_KTKL;
		private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Epoint.Systems.Controls.txtEnum enuLoai_KTKL;
        private Systems.Controls.txtTextLookup txtMa_CbNv;


	}
}