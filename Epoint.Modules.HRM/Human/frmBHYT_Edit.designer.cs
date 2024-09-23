namespace Epoint.Modules.HRM
{
    partial class frmBHYT_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBHYT_Edit));
            this.dteNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblGhi_Chu = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_End = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.lblSo_BHYT = new Epoint.Systems.Controls.lblControl();
            this.txtGhi_Chu = new Epoint.Systems.Controls.txtTextBox();
            this.txtSo_The_BHYT = new Epoint.Systems.Controls.txtTextBox();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lblNoi_KCB = new Epoint.Systems.Controls.lblControl();
            this.txtNoi_KCB = new Epoint.Systems.Controls.txtTextBox();
            this.lblTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.txtNgay_Tra_The = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_Tra_The = new Epoint.Systems.Controls.lblControl();
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
            this.dteNgay_End.Location = new System.Drawing.Point(136, 120);
            this.dteNgay_End.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_End.Mask = "00/00/0000";
            this.dteNgay_End.Name = "dteNgay_End";
            this.dteNgay_End.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_End.TabIndex = 4;
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
            this.dteNgay_Begin.Location = new System.Drawing.Point(136, 96);
            this.dteNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Begin.Mask = "00/00/0000";
            this.dteNgay_Begin.Name = "dteNgay_Begin";
            this.dteNgay_Begin.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_Begin.TabIndex = 3;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(382, 257);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 34);
            this.btgAccept.TabIndex = 8;
            // 
            // lblGhi_Chu
            // 
            this.lblGhi_Chu.AutoEllipsis = true;
            this.lblGhi_Chu.AutoSize = true;
            this.lblGhi_Chu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhi_Chu.Location = new System.Drawing.Point(27, 195);
            this.lblGhi_Chu.Name = "lblGhi_Chu";
            this.lblGhi_Chu.Size = new System.Drawing.Size(44, 13);
            this.lblGhi_Chu.TabIndex = 123;
            this.lblGhi_Chu.Tag = "Ghi_Chu";
            this.lblGhi_Chu.Text = "Ghi chú";
            this.lblGhi_Chu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_End
            // 
            this.lblNgay_End.AutoEllipsis = true;
            this.lblNgay_End.AutoSize = true;
            this.lblNgay_End.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_End.Location = new System.Drawing.Point(27, 123);
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
            this.lblNgay_Begin.Location = new System.Drawing.Point(27, 99);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 125;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSo_BHYT
            // 
            this.lblSo_BHYT.AutoEllipsis = true;
            this.lblSo_BHYT.AutoSize = true;
            this.lblSo_BHYT.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_BHYT.Location = new System.Drawing.Point(27, 75);
            this.lblSo_BHYT.Name = "lblSo_BHYT";
            this.lblSo_BHYT.Size = new System.Drawing.Size(70, 13);
            this.lblSo_BHYT.TabIndex = 127;
            this.lblSo_BHYT.Tag = "So_The_BHYT";
            this.lblSo_BHYT.Text = "Số thẻ BHYT";
            this.lblSo_BHYT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhi_Chu
            // 
            this.txtGhi_Chu.bEnabled = true;
            this.txtGhi_Chu.bIsLookup = false;
            this.txtGhi_Chu.bReadOnly = false;
            this.txtGhi_Chu.bRequire = false;
            this.txtGhi_Chu.KeyFilter = "";
            this.txtGhi_Chu.Location = new System.Drawing.Point(136, 192);
            this.txtGhi_Chu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGhi_Chu.Multiline = true;
            this.txtGhi_Chu.Name = "txtGhi_Chu";
            this.txtGhi_Chu.Size = new System.Drawing.Size(415, 40);
            this.txtGhi_Chu.TabIndex = 7;
            this.txtGhi_Chu.UseAutoFilter = false;
            // 
            // txtSo_The_BHYT
            // 
            this.txtSo_The_BHYT.bEnabled = true;
            this.txtSo_The_BHYT.bIsLookup = false;
            this.txtSo_The_BHYT.bReadOnly = false;
            this.txtSo_The_BHYT.bRequire = false;
            this.txtSo_The_BHYT.KeyFilter = "";
            this.txtSo_The_BHYT.Location = new System.Drawing.Point(136, 72);
            this.txtSo_The_BHYT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtSo_The_BHYT.Name = "txtSo_The_BHYT";
            this.txtSo_The_BHYT.Size = new System.Drawing.Size(415, 20);
            this.txtSo_The_BHYT.TabIndex = 2;
            this.txtSo_The_BHYT.UseAutoFilter = false;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(27, 27);
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(136, 24);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(164, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // lblNoi_KCB
            // 
            this.lblNoi_KCB.AutoEllipsis = true;
            this.lblNoi_KCB.AutoSize = true;
            this.lblNoi_KCB.BackColor = System.Drawing.Color.Transparent;
            this.lblNoi_KCB.Location = new System.Drawing.Point(27, 171);
            this.lblNoi_KCB.Name = "lblNoi_KCB";
            this.lblNoi_KCB.Size = new System.Drawing.Size(89, 13);
            this.lblNoi_KCB.TabIndex = 130;
            this.lblNoi_KCB.Tag = "Noi_KCB";
            this.lblNoi_KCB.Text = "Nơi đăng ký KCB";
            this.lblNoi_KCB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNoi_KCB
            // 
            this.txtNoi_KCB.bEnabled = true;
            this.txtNoi_KCB.bIsLookup = false;
            this.txtNoi_KCB.bReadOnly = false;
            this.txtNoi_KCB.bRequire = false;
            this.txtNoi_KCB.KeyFilter = "";
            this.txtNoi_KCB.Location = new System.Drawing.Point(136, 168);
            this.txtNoi_KCB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNoi_KCB.Name = "txtNoi_KCB";
            this.txtNoi_KCB.Size = new System.Drawing.Size(415, 20);
            this.txtNoi_KCB.TabIndex = 6;
            this.txtNoi_KCB.UseAutoFilter = false;
            // 
            // lblTen_CbNv
            // 
            this.lblTen_CbNv.AutoEllipsis = true;
            this.lblTen_CbNv.AutoSize = true;
            this.lblTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_CbNv.Location = new System.Drawing.Point(27, 51);
            this.lblTen_CbNv.Name = "lblTen_CbNv";
            this.lblTen_CbNv.Size = new System.Drawing.Size(76, 13);
            this.lblTen_CbNv.TabIndex = 142;
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
            this.txtTen_CbNv.Location = new System.Drawing.Point(136, 48);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(415, 20);
            this.txtTen_CbNv.TabIndex = 1;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // txtNgay_Tra_The
            // 
            this.txtNgay_Tra_The.bAllowEmpty = true;
            this.txtNgay_Tra_The.bRequire = false;
            this.txtNgay_Tra_The.bSelectOnFocus = false;
            this.txtNgay_Tra_The.bShowDateTimePicker = true;
            this.txtNgay_Tra_The.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.txtNgay_Tra_The.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_Tra_The.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_Tra_The.Location = new System.Drawing.Point(136, 144);
            this.txtNgay_Tra_The.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtNgay_Tra_The.Mask = "00/00/0000";
            this.txtNgay_Tra_The.Name = "txtNgay_Tra_The";
            this.txtNgay_Tra_The.Size = new System.Drawing.Size(74, 20);
            this.txtNgay_Tra_The.TabIndex = 5;
            // 
            // lblNgay_Tra_The
            // 
            this.lblNgay_Tra_The.AutoEllipsis = true;
            this.lblNgay_Tra_The.AutoSize = true;
            this.lblNgay_Tra_The.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Tra_The.Location = new System.Drawing.Point(27, 147);
            this.lblNgay_Tra_The.Name = "lblNgay_Tra_The";
            this.lblNgay_Tra_The.Size = new System.Drawing.Size(65, 13);
            this.lblNgay_Tra_The.TabIndex = 144;
            this.lblNgay_Tra_The.Tag = "Ngay_Tra_The";
            this.lblNgay_Tra_The.Text = "Ngày trả thẻ";
            this.lblNgay_Tra_The.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmBHYT_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 309);
            this.Controls.Add(this.txtNgay_Tra_The);
            this.Controls.Add(this.lblNgay_Tra_The);
            this.Controls.Add(this.lblTen_CbNv);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.lblNoi_KCB);
            this.Controls.Add(this.txtNoi_KCB);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_End);
            this.Controls.Add(this.dteNgay_Begin);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblGhi_Chu);
            this.Controls.Add(this.lblNgay_End);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.lblSo_BHYT);
            this.Controls.Add(this.txtGhi_Chu);
            this.Controls.Add(this.txtSo_The_BHYT);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBHYT_Edit";
            this.Text = "frmBHYT_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private Epoint.Systems.Controls.txtDateTime dteNgay_End;
        private Epoint.Systems.Controls.txtDateTime dteNgay_Begin;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblGhi_Chu;
		private Epoint.Systems.Controls.lblControl lblNgay_End;
        private Epoint.Systems.Controls.lblControl lblNgay_Begin;
		private Epoint.Systems.Controls.lblControl lblSo_BHYT;
        private Epoint.Systems.Controls.txtTextBox txtGhi_Chu;
        private Epoint.Systems.Controls.txtTextBox txtSo_The_BHYT;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.lblControl lblNoi_KCB;
        private Systems.Controls.txtTextBox txtNoi_KCB;
        private Systems.Controls.lblControl lblTen_CbNv;
        private Systems.Controls.txtTextBox txtTen_CbNv;
        private Systems.Controls.txtDateTime txtNgay_Tra_The;
        private Systems.Controls.lblControl lblNgay_Tra_The;

	}
}