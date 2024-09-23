namespace Epoint.Modules.HRM
{
    partial class frmChamCongNgay
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChamCongNgay));
            this.grbControl1 = new Epoint.Systems.Controls.grbControl();
            this.dgvHRChamCong = new Epoint.Systems.Controls.dgvControl();
            this.btCreateChamCong = new Epoint.Systems.Controls.btControl();
            this.dteNgay_Ct = new Epoint.Systems.Controls.dteDateTime();
            this.lblDien_Giai = new Epoint.Systems.Controls.lblControl();
            this.lbtLoai_Ps = new Epoint.Systems.Controls.lblControl();
            this.lblLoai_Ps = new Epoint.Systems.Controls.lblControl();
            this.txtLoai_NC = new Epoint.Systems.Controls.txtEnum();
            this.grbControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHRChamCong)).BeginInit();
            this.SuspendLayout();
            // 
            // grbControl1
            // 
            this.grbControl1.Controls.Add(this.dgvHRChamCong);
            this.grbControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grbControl1.Location = new System.Drawing.Point(0, 50);
            this.grbControl1.Name = "grbControl1";
            this.grbControl1.Size = new System.Drawing.Size(866, 516);
            this.grbControl1.TabIndex = 3;
            this.grbControl1.TabStop = false;
            // 
            // dgvHRChamCong
            // 
            this.dgvHRChamCong.AllowUserToAddRows = false;
            this.dgvHRChamCong.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvHRChamCong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHRChamCong.BackgroundColor = System.Drawing.Color.White;
            this.dgvHRChamCong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvHRChamCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHRChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHRChamCong.EnableHeadersVisualStyles = false;
            this.dgvHRChamCong.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvHRChamCong.Location = new System.Drawing.Point(3, 16);
            this.dgvHRChamCong.MultiSelect = false;
            this.dgvHRChamCong.Name = "dgvHRChamCong";
            this.dgvHRChamCong.ReadOnly = true;
            this.dgvHRChamCong.Size = new System.Drawing.Size(860, 497);
            this.dgvHRChamCong.strZone = "";
            this.dgvHRChamCong.TabIndex = 3;
            // 
            // btCreateChamCong
            // 
            this.btCreateChamCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateChamCong.ForeColor = System.Drawing.Color.Blue;
            this.btCreateChamCong.Image = ((System.Drawing.Image)(resources.GetObject("btCreateChamCong.Image")));
            this.btCreateChamCong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCreateChamCong.Location = new System.Drawing.Point(30, 12);
            this.btCreateChamCong.Name = "btCreateChamCong";
            this.btCreateChamCong.Size = new System.Drawing.Size(169, 34);
            this.btCreateChamCong.TabIndex = 2;
            this.btCreateChamCong.Tag = "";
            this.btCreateChamCong.Text = "&Tạo bảng chấm công";
            this.btCreateChamCong.UseVisualStyleBackColor = true;
            // 
            // dteNgay_Ct
            // 
            this.dteNgay_Ct.bAllowEmpty = true;
            this.dteNgay_Ct.bRequire = false;
            this.dteNgay_Ct.bSelectOnFocus = false;
            this.dteNgay_Ct.Culture = new System.Globalization.CultureInfo("en-US");
            this.dteNgay_Ct.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ct.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ct.Location = new System.Drawing.Point(322, 21);
            this.dteNgay_Ct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ct.Mask = "00/00/0000";
            this.dteNgay_Ct.Name = "dteNgay_Ct";
            this.dteNgay_Ct.SelectedText = "";
            this.dteNgay_Ct.Size = new System.Drawing.Size(111, 20);
            this.dteNgay_Ct.TabIndex = 0;
            // 
            // lblDien_Giai
            // 
            this.lblDien_Giai.AutoEllipsis = true;
            this.lblDien_Giai.AutoSize = true;
            this.lblDien_Giai.BackColor = System.Drawing.Color.Transparent;
            this.lblDien_Giai.Location = new System.Drawing.Point(229, 24);
            this.lblDien_Giai.Name = "lblDien_Giai";
            this.lblDien_Giai.Size = new System.Drawing.Size(88, 13);
            this.lblDien_Giai.TabIndex = 75;
            this.lblDien_Giai.Tag = "Ngay_Cong";
            this.lblDien_Giai.Text = "Ngày chấm công";
            this.lblDien_Giai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtLoai_Ps
            // 
            this.lbtLoai_Ps.AutoEllipsis = true;
            this.lbtLoai_Ps.AutoSize = true;
            this.lbtLoai_Ps.BackColor = System.Drawing.Color.Transparent;
            this.lbtLoai_Ps.ForeColor = System.Drawing.Color.Blue;
            this.lbtLoai_Ps.Location = new System.Drawing.Point(567, 24);
            this.lbtLoai_Ps.Name = "lbtLoai_Ps";
            this.lbtLoai_Ps.Size = new System.Drawing.Size(182, 13);
            this.lbtLoai_Ps.TabIndex = 78;
            this.lbtLoai_Ps.Text = "1-Ngày công thường, 2-Ngày công lễ";
            this.lbtLoai_Ps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLoai_Ps
            // 
            this.lblLoai_Ps.AutoEllipsis = true;
            this.lblLoai_Ps.AutoSize = true;
            this.lblLoai_Ps.BackColor = System.Drawing.Color.Transparent;
            this.lblLoai_Ps.Location = new System.Drawing.Point(440, 24);
            this.lblLoai_Ps.Name = "lblLoai_Ps";
            this.lblLoai_Ps.Size = new System.Drawing.Size(80, 13);
            this.lblLoai_Ps.TabIndex = 77;
            this.lblLoai_Ps.Tag = "Loai_NC";
            this.lblLoai_Ps.Text = "Loại ngày công";
            this.lblLoai_Ps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLoai_NC
            // 
            this.txtLoai_NC.bEnabled = true;
            this.txtLoai_NC.bIsLookup = false;
            this.txtLoai_NC.bReadOnly = false;
            this.txtLoai_NC.bRequire = false;
            this.txtLoai_NC.InputMask = "1,2";
            this.txtLoai_NC.KeyFilter = "";
            this.txtLoai_NC.Location = new System.Drawing.Point(534, 21);
            this.txtLoai_NC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtLoai_NC.Name = "txtLoai_NC";
            this.txtLoai_NC.Size = new System.Drawing.Size(29, 20);
            this.txtLoai_NC.TabIndex = 1;
            this.txtLoai_NC.Text = "1";
            this.txtLoai_NC.UseAutoFilter = false;
            // 
            // frmChamCongNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 566);
            this.Controls.Add(this.lbtLoai_Ps);
            this.Controls.Add(this.lblLoai_Ps);
            this.Controls.Add(this.txtLoai_NC);
            this.Controls.Add(this.dteNgay_Ct);
            this.Controls.Add(this.lblDien_Giai);
            this.Controls.Add(this.btCreateChamCong);
            this.Controls.Add(this.grbControl1);
            this.Name = "frmChamCongNgay";
            this.Tag = "frmViewLicense,F2,F3,F8,ESC";
            this.Text = "frmViewLicense";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grbControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHRChamCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Systems.Controls.grbControl grbControl1;
        private Systems.Controls.dgvControl dgvHRChamCong;
        private Systems.Controls.btControl btCreateChamCong;
        private Systems.Controls.dteDateTime dteNgay_Ct;
        private Systems.Controls.lblControl lblDien_Giai;
        private Systems.Controls.lblControl lbtLoai_Ps;
        private Systems.Controls.lblControl lblLoai_Ps;
        private Systems.Controls.txtEnum txtLoai_NC;
    }
}