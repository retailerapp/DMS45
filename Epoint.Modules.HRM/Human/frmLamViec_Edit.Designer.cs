namespace Epoint.Modules.HRM
{
    partial class frmLamViec_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLamViec_Edit));
            this.lbtTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblGhi_Chu = new Epoint.Systems.Controls.lblControl();
            this.txtGhi_Chu = new Epoint.Systems.Controls.txtTextBox();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.txtNgay_Thu_Viec = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_Thu_Viec = new Epoint.Systems.Controls.lblControl();
            this.chkIs_Thu_Viec = new Epoint.Systems.Controls.chkControl();
            this.lblSo_Nghi_Viec = new Epoint.Systems.Controls.lblControl();
            this.txtSo_Nghi_Viec = new Epoint.Systems.Controls.txtTextBox();
            this.txtNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.chkIs_Nghi_Viec = new Epoint.Systems.Controls.chkControl();
            this.chkIs_Bat_Dau = new Epoint.Systems.Controls.chkControl();
            this.txtNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // lbtTen_CbNv
            // 
            this.lbtTen_CbNv.AutoEllipsis = true;
            this.lbtTen_CbNv.AutoSize = true;
            this.lbtTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_CbNv.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_CbNv.Location = new System.Drawing.Point(271, 28);
            this.lbtTen_CbNv.Name = "lbtTen_CbNv";
            this.lbtTen_CbNv.Size = new System.Drawing.Size(59, 13);
            this.lbtTen_CbNv.TabIndex = 128;
            this.lbtTen_CbNv.Text = "Ten_CbNv";
            this.lbtTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(388, 311);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(168, 33);
            this.btgAccept.TabIndex = 9;
            // 
            // lblGhi_Chu
            // 
            this.lblGhi_Chu.AutoEllipsis = true;
            this.lblGhi_Chu.AutoSize = true;
            this.lblGhi_Chu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhi_Chu.Location = new System.Drawing.Point(51, 258);
            this.lblGhi_Chu.Name = "lblGhi_Chu";
            this.lblGhi_Chu.Size = new System.Drawing.Size(44, 13);
            this.lblGhi_Chu.TabIndex = 123;
            this.lblGhi_Chu.Tag = "Ghi_Chu";
            this.lblGhi_Chu.Text = "Ghi chú";
            this.lblGhi_Chu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhi_Chu
            // 
            this.txtGhi_Chu.bEnabled = true;
            this.txtGhi_Chu.bIsLookup = false;
            this.txtGhi_Chu.bReadOnly = false;
            this.txtGhi_Chu.bRequire = false;
            this.txtGhi_Chu.KeyFilter = "";
            this.txtGhi_Chu.Location = new System.Drawing.Point(141, 254);
            this.txtGhi_Chu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGhi_Chu.Multiline = true;
            this.txtGhi_Chu.Name = "txtGhi_Chu";
            this.txtGhi_Chu.Size = new System.Drawing.Size(415, 40);
            this.txtGhi_Chu.TabIndex = 8;
            this.txtGhi_Chu.UseAutoFilter = false;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(51, 27);
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(141, 24);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(120, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // txtNgay_Thu_Viec
            // 
            this.txtNgay_Thu_Viec.bAllowEmpty = true;
            this.txtNgay_Thu_Viec.bRequire = false;
            this.txtNgay_Thu_Viec.bSelectOnFocus = false;
            this.txtNgay_Thu_Viec.bShowDateTimePicker = true;
            this.txtNgay_Thu_Viec.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_Thu_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgay_Thu_Viec.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_Thu_Viec.Location = new System.Drawing.Point(141, 77);
            this.txtNgay_Thu_Viec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNgay_Thu_Viec.Mask = "00/00/0000";
            this.txtNgay_Thu_Viec.Name = "txtNgay_Thu_Viec";
            this.txtNgay_Thu_Viec.Size = new System.Drawing.Size(83, 20);
            this.txtNgay_Thu_Viec.TabIndex = 2;
            // 
            // lblNgay_Thu_Viec
            // 
            this.lblNgay_Thu_Viec.AutoEllipsis = true;
            this.lblNgay_Thu_Viec.AutoSize = true;
            this.lblNgay_Thu_Viec.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Thu_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay_Thu_Viec.Location = new System.Drawing.Point(51, 84);
            this.lblNgay_Thu_Viec.Name = "lblNgay_Thu_Viec";
            this.lblNgay_Thu_Viec.Size = new System.Drawing.Size(73, 13);
            this.lblNgay_Thu_Viec.TabIndex = 195;
            this.lblNgay_Thu_Viec.Tag = "Ngay_Thu_Viec";
            this.lblNgay_Thu_Viec.Text = "Ngày thử việc";
            this.lblNgay_Thu_Viec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIs_Thu_Viec
            // 
            this.chkIs_Thu_Viec.AutoSize = true;
            this.chkIs_Thu_Viec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIs_Thu_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIs_Thu_Viec.Location = new System.Drawing.Point(51, 61);
            this.chkIs_Thu_Viec.Name = "chkIs_Thu_Viec";
            this.chkIs_Thu_Viec.Size = new System.Drawing.Size(68, 17);
            this.chkIs_Thu_Viec.TabIndex = 1;
            this.chkIs_Thu_Viec.Tag = "Is_Thu_Viec";
            this.chkIs_Thu_Viec.Text = "Thử việc";
            this.chkIs_Thu_Viec.UseVisualStyleBackColor = true;
            // 
            // lblSo_Nghi_Viec
            // 
            this.lblSo_Nghi_Viec.AutoEllipsis = true;
            this.lblSo_Nghi_Viec.AutoSize = true;
            this.lblSo_Nghi_Viec.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Nghi_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSo_Nghi_Viec.Location = new System.Drawing.Point(51, 234);
            this.lblSo_Nghi_Viec.Name = "lblSo_Nghi_Viec";
            this.lblSo_Nghi_Viec.Size = new System.Drawing.Size(85, 13);
            this.lblSo_Nghi_Viec.TabIndex = 194;
            this.lblSo_Nghi_Viec.Tag = "So_Nghi_Viec";
            this.lblSo_Nghi_Viec.Text = "Số QĐ nghỉ việc";
            this.lblSo_Nghi_Viec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSo_Nghi_Viec
            // 
            this.txtSo_Nghi_Viec.AccessibleName = "6";
            this.txtSo_Nghi_Viec.bEnabled = true;
            this.txtSo_Nghi_Viec.bIsLookup = false;
            this.txtSo_Nghi_Viec.bReadOnly = false;
            this.txtSo_Nghi_Viec.bRequire = false;
            this.txtSo_Nghi_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSo_Nghi_Viec.KeyFilter = "";
            this.txtSo_Nghi_Viec.Location = new System.Drawing.Point(141, 230);
            this.txtSo_Nghi_Viec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtSo_Nghi_Viec.Name = "txtSo_Nghi_Viec";
            this.txtSo_Nghi_Viec.Size = new System.Drawing.Size(415, 20);
            this.txtSo_Nghi_Viec.TabIndex = 7;
            this.txtSo_Nghi_Viec.UseAutoFilter = false;
            // 
            // txtNgay_Begin
            // 
            this.txtNgay_Begin.bAllowEmpty = true;
            this.txtNgay_Begin.bRequire = false;
            this.txtNgay_Begin.bSelectOnFocus = false;
            this.txtNgay_Begin.bShowDateTimePicker = true;
            this.txtNgay_Begin.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_Begin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgay_Begin.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_Begin.Location = new System.Drawing.Point(141, 143);
            this.txtNgay_Begin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNgay_Begin.Mask = "00/00/0000";
            this.txtNgay_Begin.Name = "txtNgay_Begin";
            this.txtNgay_Begin.Size = new System.Drawing.Size(83, 20);
            this.txtNgay_Begin.TabIndex = 4;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControl1.Location = new System.Drawing.Point(51, 146);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(72, 13);
            this.lblControl1.TabIndex = 193;
            this.lblControl1.Tag = "Ngay_Vao_Lam";
            this.lblControl1.Text = "Ngày vào làm";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIs_Nghi_Viec
            // 
            this.chkIs_Nghi_Viec.AutoSize = true;
            this.chkIs_Nghi_Viec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIs_Nghi_Viec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIs_Nghi_Viec.Location = new System.Drawing.Point(51, 186);
            this.chkIs_Nghi_Viec.Name = "chkIs_Nghi_Viec";
            this.chkIs_Nghi_Viec.Size = new System.Drawing.Size(71, 17);
            this.chkIs_Nghi_Viec.TabIndex = 5;
            this.chkIs_Nghi_Viec.Tag = "Is_Nghi_Viec";
            this.chkIs_Nghi_Viec.Text = "Nghỉ việc";
            this.chkIs_Nghi_Viec.UseVisualStyleBackColor = true;
            // 
            // chkIs_Bat_Dau
            // 
            this.chkIs_Bat_Dau.AutoSize = true;
            this.chkIs_Bat_Dau.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIs_Bat_Dau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIs_Bat_Dau.Location = new System.Drawing.Point(51, 123);
            this.chkIs_Bat_Dau.Name = "chkIs_Bat_Dau";
            this.chkIs_Bat_Dau.Size = new System.Drawing.Size(83, 17);
            this.chkIs_Bat_Dau.TabIndex = 3;
            this.chkIs_Bat_Dau.Tag = "Is_Bat_Dau";
            this.chkIs_Bat_Dau.Text = "Bắt đầu làm";
            this.chkIs_Bat_Dau.UseVisualStyleBackColor = true;
            // 
            // txtNgay_End
            // 
            this.txtNgay_End.bAllowEmpty = true;
            this.txtNgay_End.bRequire = false;
            this.txtNgay_End.bSelectOnFocus = false;
            this.txtNgay_End.bShowDateTimePicker = true;
            this.txtNgay_End.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_End.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNgay_End.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_End.Location = new System.Drawing.Point(141, 206);
            this.txtNgay_End.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNgay_End.Mask = "00/00/0000";
            this.txtNgay_End.Name = "txtNgay_End";
            this.txtNgay_End.Size = new System.Drawing.Size(83, 20);
            this.txtNgay_End.TabIndex = 6;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControl2.Location = new System.Drawing.Point(51, 209);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(78, 13);
            this.lblControl2.TabIndex = 192;
            this.lblControl2.Tag = "Ngay_End";
            this.lblControl2.Text = "Ngày nghỉ việc";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmLamViec_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 357);
            this.Controls.Add(this.txtNgay_Thu_Viec);
            this.Controls.Add(this.lblNgay_Thu_Viec);
            this.Controls.Add(this.chkIs_Thu_Viec);
            this.Controls.Add(this.lblSo_Nghi_Viec);
            this.Controls.Add(this.txtSo_Nghi_Viec);
            this.Controls.Add(this.txtNgay_Begin);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.chkIs_Nghi_Viec);
            this.Controls.Add(this.chkIs_Bat_Dau);
            this.Controls.Add(this.txtNgay_End);
            this.Controls.Add(this.lblControl2);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.lbtTen_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblGhi_Chu);
            this.Controls.Add(this.txtGhi_Chu);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLamViec_Edit";
            this.Text = "frmLamViec_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Epoint.Systems.Controls.lblControl lbtTen_CbNv;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
        private Epoint.Systems.Controls.lblControl lblGhi_Chu;
        private Epoint.Systems.Controls.txtTextBox txtGhi_Chu;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.txtDateTime txtNgay_Thu_Viec;
        private Systems.Controls.lblControl lblNgay_Thu_Viec;
        private Systems.Controls.chkControl chkIs_Thu_Viec;
        private Systems.Controls.lblControl lblSo_Nghi_Viec;
        private Systems.Controls.txtTextBox txtSo_Nghi_Viec;
        private Systems.Controls.txtDateTime txtNgay_Begin;
        private Systems.Controls.lblControl lblControl1;
        private Systems.Controls.chkControl chkIs_Nghi_Viec;
        private Systems.Controls.chkControl chkIs_Bat_Dau;
        private Systems.Controls.txtDateTime txtNgay_End;
        private Systems.Controls.lblControl lblControl2;

	}
}