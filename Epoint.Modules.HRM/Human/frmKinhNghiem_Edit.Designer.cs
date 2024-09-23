namespace Epoint.Modules.HRM
{
    partial class frmKinhNghiem_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKinhNghiem_Edit));
            this.dteNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.lblGhi_Chu = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_End = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.txtGhi_Chu = new Epoint.Systems.Controls.txtTextBox();
            this.lbtTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblMuc_Luong = new Epoint.Systems.Controls.lblControl();
            this.lblChuc_Danh = new Epoint.Systems.Controls.lblControl();
            this.lblDia_Chi = new Epoint.Systems.Controls.lblControl();
            this.lblTen_Bp = new Epoint.Systems.Controls.lblControl();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            this.numMuc_Luong = new Epoint.Systems.Controls.numControl();
            this.txtChuc_Danh = new Epoint.Systems.Controls.txtTextBox();
            this.txtDia_Chi = new Epoint.Systems.Controls.txtTextBox();
            this.txtTen_Bp = new Epoint.Systems.Controls.txtTextBox();
            this.txtTen_CQ = new Epoint.Systems.Controls.txtTextBox();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
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
            this.dteNgay_End.Location = new System.Drawing.Point(130, 179);
            this.dteNgay_End.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_End.Mask = "00/00/0000";
            this.dteNgay_End.Name = "dteNgay_End";
            this.dteNgay_End.Size = new System.Drawing.Size(120, 20);
            this.dteNgay_End.TabIndex = 7;
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
            this.dteNgay_Begin.Location = new System.Drawing.Point(130, 156);
            this.dteNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Begin.Mask = "00/00/0000";
            this.dteNgay_Begin.Name = "dteNgay_Begin";
            this.dteNgay_Begin.Size = new System.Drawing.Size(120, 20);
            this.dteNgay_Begin.TabIndex = 6;
            // 
            // lblGhi_Chu
            // 
            this.lblGhi_Chu.AutoEllipsis = true;
            this.lblGhi_Chu.AutoSize = true;
            this.lblGhi_Chu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhi_Chu.Location = new System.Drawing.Point(21, 205);
            this.lblGhi_Chu.Name = "lblGhi_Chu";
            this.lblGhi_Chu.Size = new System.Drawing.Size(44, 13);
            this.lblGhi_Chu.TabIndex = 134;
            this.lblGhi_Chu.Tag = "Ghi_Chu";
            this.lblGhi_Chu.Text = "Ghi chú";
            this.lblGhi_Chu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_End
            // 
            this.lblNgay_End.AutoEllipsis = true;
            this.lblNgay_End.AutoSize = true;
            this.lblNgay_End.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_End.Location = new System.Drawing.Point(21, 182);
            this.lblNgay_End.Name = "lblNgay_End";
            this.lblNgay_End.Size = new System.Drawing.Size(74, 13);
            this.lblNgay_End.TabIndex = 136;
            this.lblNgay_End.Tag = "Ngay_End";
            this.lblNgay_End.Text = "Ngày kết thúc";
            this.lblNgay_End.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_Begin
            // 
            this.lblNgay_Begin.AutoEllipsis = true;
            this.lblNgay_Begin.AutoSize = true;
            this.lblNgay_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Begin.Location = new System.Drawing.Point(21, 159);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 135;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhi_Chu
            // 
            this.txtGhi_Chu.bEnabled = true;
            this.txtGhi_Chu.bIsLookup = false;
            this.txtGhi_Chu.bReadOnly = false;
            this.txtGhi_Chu.bRequire = false;
            this.txtGhi_Chu.KeyFilter = "";
            this.txtGhi_Chu.Location = new System.Drawing.Point(130, 202);
            this.txtGhi_Chu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGhi_Chu.Multiline = true;
            this.txtGhi_Chu.Name = "txtGhi_Chu";
            this.txtGhi_Chu.Size = new System.Drawing.Size(450, 40);
            this.txtGhi_Chu.TabIndex = 8;
            this.txtGhi_Chu.UseAutoFilter = false;
            // 
            // lbtTen_CbNv
            // 
            this.lbtTen_CbNv.AutoEllipsis = true;
            this.lbtTen_CbNv.AutoSize = true;
            this.lbtTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_CbNv.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_CbNv.Location = new System.Drawing.Point(255, 22);
            this.lbtTen_CbNv.Name = "lbtTen_CbNv";
            this.lbtTen_CbNv.Size = new System.Drawing.Size(59, 13);
            this.lbtTen_CbNv.TabIndex = 133;
            this.lbtTen_CbNv.Text = "Ten_CbNv";
            this.lbtTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(412, 259);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(168, 34);
            this.btgAccept.TabIndex = 9;
            // 
            // lblMuc_Luong
            // 
            this.lblMuc_Luong.AutoEllipsis = true;
            this.lblMuc_Luong.AutoSize = true;
            this.lblMuc_Luong.BackColor = System.Drawing.Color.Transparent;
            this.lblMuc_Luong.Location = new System.Drawing.Point(21, 113);
            this.lblMuc_Luong.Name = "lblMuc_Luong";
            this.lblMuc_Luong.Size = new System.Drawing.Size(57, 13);
            this.lblMuc_Luong.TabIndex = 130;
            this.lblMuc_Luong.Tag = "Muc_Luong";
            this.lblMuc_Luong.Text = "Mức lương";
            this.lblMuc_Luong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChuc_Danh
            // 
            this.lblChuc_Danh.AutoEllipsis = true;
            this.lblChuc_Danh.AutoSize = true;
            this.lblChuc_Danh.BackColor = System.Drawing.Color.Transparent;
            this.lblChuc_Danh.Location = new System.Drawing.Point(21, 90);
            this.lblChuc_Danh.Name = "lblChuc_Danh";
            this.lblChuc_Danh.Size = new System.Drawing.Size(59, 13);
            this.lblChuc_Danh.TabIndex = 128;
            this.lblChuc_Danh.Tag = "Chuc_Danh";
            this.lblChuc_Danh.Text = "Chức danh";
            this.lblChuc_Danh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDia_Chi
            // 
            this.lblDia_Chi.AutoEllipsis = true;
            this.lblDia_Chi.AutoSize = true;
            this.lblDia_Chi.BackColor = System.Drawing.Color.Transparent;
            this.lblDia_Chi.Location = new System.Drawing.Point(21, 136);
            this.lblDia_Chi.Name = "lblDia_Chi";
            this.lblDia_Chi.Size = new System.Drawing.Size(40, 13);
            this.lblDia_Chi.TabIndex = 131;
            this.lblDia_Chi.Tag = "Dia_Chi";
            this.lblDia_Chi.Text = "Địa chỉ";
            this.lblDia_Chi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTen_Bp
            // 
            this.lblTen_Bp.AutoEllipsis = true;
            this.lblTen_Bp.AutoSize = true;
            this.lblTen_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_Bp.Location = new System.Drawing.Point(21, 67);
            this.lblTen_Bp.Name = "lblTen_Bp";
            this.lblTen_Bp.Size = new System.Drawing.Size(92, 13);
            this.lblTen_Bp.TabIndex = 132;
            this.lblTen_Bp.Tag = "Ten_Bp";
            this.lblTen_Bp.Text = "Bộ phận công tác";
            this.lblTen_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.Location = new System.Drawing.Point(21, 44);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(68, 13);
            this.lblControl2.TabIndex = 129;
            this.lblControl2.Tag = "Ten_Cq";
            this.lblControl2.Text = "Tên cơ quan";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numMuc_Luong
            // 
            this.numMuc_Luong.bEnabled = true;
            this.numMuc_Luong.bFormat = true;
            this.numMuc_Luong.bIsLookup = false;
            this.numMuc_Luong.bReadOnly = false;
            this.numMuc_Luong.bRequire = false;
            this.numMuc_Luong.KeyFilter = "";
            this.numMuc_Luong.Location = new System.Drawing.Point(130, 110);
            this.numMuc_Luong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numMuc_Luong.Name = "numMuc_Luong";
            this.numMuc_Luong.Scale = 0;
            this.numMuc_Luong.Size = new System.Drawing.Size(120, 20);
            this.numMuc_Luong.TabIndex = 4;
            this.numMuc_Luong.Text = "0";
            this.numMuc_Luong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMuc_Luong.UseAutoFilter = false;
            this.numMuc_Luong.Value = 0D;
            // 
            // txtChuc_Danh
            // 
            this.txtChuc_Danh.bEnabled = true;
            this.txtChuc_Danh.bIsLookup = false;
            this.txtChuc_Danh.bReadOnly = false;
            this.txtChuc_Danh.bRequire = false;
            this.txtChuc_Danh.KeyFilter = "";
            this.txtChuc_Danh.Location = new System.Drawing.Point(130, 87);
            this.txtChuc_Danh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtChuc_Danh.Name = "txtChuc_Danh";
            this.txtChuc_Danh.Size = new System.Drawing.Size(450, 20);
            this.txtChuc_Danh.TabIndex = 3;
            this.txtChuc_Danh.UseAutoFilter = false;
            // 
            // txtDia_Chi
            // 
            this.txtDia_Chi.bEnabled = true;
            this.txtDia_Chi.bIsLookup = false;
            this.txtDia_Chi.bReadOnly = false;
            this.txtDia_Chi.bRequire = false;
            this.txtDia_Chi.KeyFilter = "";
            this.txtDia_Chi.Location = new System.Drawing.Point(130, 133);
            this.txtDia_Chi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtDia_Chi.Name = "txtDia_Chi";
            this.txtDia_Chi.Size = new System.Drawing.Size(450, 20);
            this.txtDia_Chi.TabIndex = 5;
            this.txtDia_Chi.UseAutoFilter = false;
            // 
            // txtTen_Bp
            // 
            this.txtTen_Bp.bEnabled = true;
            this.txtTen_Bp.bIsLookup = false;
            this.txtTen_Bp.bReadOnly = false;
            this.txtTen_Bp.bRequire = false;
            this.txtTen_Bp.KeyFilter = "";
            this.txtTen_Bp.Location = new System.Drawing.Point(130, 64);
            this.txtTen_Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_Bp.Name = "txtTen_Bp";
            this.txtTen_Bp.Size = new System.Drawing.Size(450, 20);
            this.txtTen_Bp.TabIndex = 2;
            this.txtTen_Bp.UseAutoFilter = false;
            // 
            // txtTen_CQ
            // 
            this.txtTen_CQ.bEnabled = true;
            this.txtTen_CQ.bIsLookup = false;
            this.txtTen_CQ.bReadOnly = false;
            this.txtTen_CQ.bRequire = false;
            this.txtTen_CQ.KeyFilter = "";
            this.txtTen_CQ.Location = new System.Drawing.Point(130, 41);
            this.txtTen_CQ.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CQ.Name = "txtTen_CQ";
            this.txtTen_CQ.Size = new System.Drawing.Size(450, 20);
            this.txtTen_CQ.TabIndex = 1;
            this.txtTen_CQ.UseAutoFilter = false;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(21, 21);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 127;
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(130, 18);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(120, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // frmKinhNghiem_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 306);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_End);
            this.Controls.Add(this.dteNgay_Begin);
            this.Controls.Add(this.lblGhi_Chu);
            this.Controls.Add(this.lblNgay_End);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.txtGhi_Chu);
            this.Controls.Add(this.lbtTen_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblMuc_Luong);
            this.Controls.Add(this.lblChuc_Danh);
            this.Controls.Add(this.lblDia_Chi);
            this.Controls.Add(this.lblTen_Bp);
            this.Controls.Add(this.lblControl2);
            this.Controls.Add(this.numMuc_Luong);
            this.Controls.Add(this.txtChuc_Danh);
            this.Controls.Add(this.txtDia_Chi);
            this.Controls.Add(this.txtTen_Bp);
            this.Controls.Add(this.txtTen_CQ);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKinhNghiem_Edit";
            this.Text = "frmKinhNghiem_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private Epoint.Systems.Controls.txtDateTime dteNgay_End;
		private Epoint.Systems.Controls.txtDateTime dteNgay_Begin;
		private Epoint.Systems.Controls.lblControl lblGhi_Chu;
		private Epoint.Systems.Controls.lblControl lblNgay_End;
		private Epoint.Systems.Controls.lblControl lblNgay_Begin;
		private Epoint.Systems.Controls.txtTextBox txtGhi_Chu;
		private Epoint.Systems.Controls.lblControl lbtTen_CbNv;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblMuc_Luong;
		private Epoint.Systems.Controls.lblControl lblChuc_Danh;
		private Epoint.Systems.Controls.lblControl lblDia_Chi;
		private Epoint.Systems.Controls.lblControl lblTen_Bp;
		private Epoint.Systems.Controls.lblControl lblControl2;
		private Epoint.Systems.Controls.numControl numMuc_Luong;
		private Epoint.Systems.Controls.txtTextBox txtChuc_Danh;
		private Epoint.Systems.Controls.txtTextBox txtDia_Chi;
		private Epoint.Systems.Controls.txtTextBox txtTen_Bp;
		private Epoint.Systems.Controls.txtTextBox txtTen_CQ;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;

	}
}