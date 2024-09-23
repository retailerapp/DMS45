namespace Epoint.Modules.HRM
{
    partial class frmChamCong_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChamCong_Edit));
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.txtMa_Bp = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_Bp = new Epoint.Systems.Controls.lblControl();
            this.txtThoi_Gian = new Epoint.Systems.Controls.txtDateTime();
            this.lblThoi_Gian = new Epoint.Systems.Controls.lblControl();
            this.txtMa_ViTri = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lblTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.lblTrang_Thai = new Epoint.Systems.Controls.lblControl();
            this.txtGhi_Chu = new Epoint.Systems.Controls.txtTextBox();
            this.lblGhi_Chu = new Epoint.Systems.Controls.lblControl();
            this.cboTrang_Thai = new Epoint.Systems.Controls.cboControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lbtTen_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Bp = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(351, 248);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 34);
            this.btgAccept.TabIndex = 8;
            // 
            // txtMa_Bp
            // 
            this.txtMa_Bp.bEnabled = true;
            this.txtMa_Bp.bIsLookup = false;
            this.txtMa_Bp.bReadOnly = false;
            this.txtMa_Bp.bRequire = false;
            this.txtMa_Bp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Bp.ColumnsView = null;
            this.txtMa_Bp.KeyFilter = "Ma_Bp";
            this.txtMa_Bp.Location = new System.Drawing.Point(118, 80);
            this.txtMa_Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Bp.Name = "txtMa_Bp";
            this.txtMa_Bp.Size = new System.Drawing.Size(164, 20);
            this.txtMa_Bp.TabIndex = 2;
            this.txtMa_Bp.UseAutoFilter = true;
            // 
            // lblMa_Bp
            // 
            this.lblMa_Bp.AutoEllipsis = true;
            this.lblMa_Bp.AutoSize = true;
            this.lblMa_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Bp.Location = new System.Drawing.Point(32, 83);
            this.lblMa_Bp.Name = "lblMa_Bp";
            this.lblMa_Bp.Size = new System.Drawing.Size(64, 13);
            this.lblMa_Bp.TabIndex = 161;
            this.lblMa_Bp.Tag = "Ma_Bp";
            this.lblMa_Bp.Text = "Mã bộ phận";
            this.lblMa_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtThoi_Gian
            // 
            this.txtThoi_Gian.bAllowEmpty = false;
            this.txtThoi_Gian.bRequire = false;
            this.txtThoi_Gian.bSelectOnFocus = false;
            this.txtThoi_Gian.bShowDateTimePicker = true;
            this.txtThoi_Gian.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.txtThoi_Gian.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtThoi_Gian.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtThoi_Gian.Location = new System.Drawing.Point(118, 128);
            this.txtThoi_Gian.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtThoi_Gian.Mask = "00/00/0000 00:00:00";
            this.txtThoi_Gian.Name = "txtThoi_Gian";
            this.txtThoi_Gian.Size = new System.Drawing.Size(164, 20);
            this.txtThoi_Gian.TabIndex = 4;
            this.txtThoi_Gian.Tag = "Thoi_Gian";
            // 
            // lblThoi_Gian
            // 
            this.lblThoi_Gian.AutoEllipsis = true;
            this.lblThoi_Gian.AutoSize = true;
            this.lblThoi_Gian.BackColor = System.Drawing.Color.Transparent;
            this.lblThoi_Gian.Location = new System.Drawing.Point(32, 131);
            this.lblThoi_Gian.Name = "lblThoi_Gian";
            this.lblThoi_Gian.Size = new System.Drawing.Size(51, 13);
            this.lblThoi_Gian.TabIndex = 156;
            this.lblThoi_Gian.Tag = "Thoi_Gian";
            this.lblThoi_Gian.Text = "Thời gian";
            this.lblThoi_Gian.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMa_ViTri
            // 
            this.txtMa_ViTri.bEnabled = true;
            this.txtMa_ViTri.bIsLookup = false;
            this.txtMa_ViTri.bReadOnly = false;
            this.txtMa_ViTri.bRequire = false;
            this.txtMa_ViTri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_ViTri.ColumnsView = null;
            this.txtMa_ViTri.KeyFilter = "Ma_ViTri";
            this.txtMa_ViTri.Location = new System.Drawing.Point(118, 104);
            this.txtMa_ViTri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_ViTri.Name = "txtMa_ViTri";
            this.txtMa_ViTri.Size = new System.Drawing.Size(164, 20);
            this.txtMa_ViTri.TabIndex = 3;
            this.txtMa_ViTri.UseAutoFilter = true;
            // 
            // lblMa_ViTri
            // 
            this.lblMa_ViTri.AutoEllipsis = true;
            this.lblMa_ViTri.AutoSize = true;
            this.lblMa_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_ViTri.Location = new System.Drawing.Point(32, 107);
            this.lblMa_ViTri.Name = "lblMa_ViTri";
            this.lblMa_ViTri.Size = new System.Drawing.Size(46, 13);
            this.lblMa_ViTri.TabIndex = 166;
            this.lblMa_ViTri.Tag = "Ma_ViTri";
            this.lblMa_ViTri.Text = "Mã vị trí";
            this.lblMa_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTen_CbNv
            // 
            this.lblTen_CbNv.AutoEllipsis = true;
            this.lblTen_CbNv.AutoSize = true;
            this.lblTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_CbNv.Location = new System.Drawing.Point(32, 59);
            this.lblTen_CbNv.Name = "lblTen_CbNv";
            this.lblTen_CbNv.Size = new System.Drawing.Size(76, 13);
            this.lblTen_CbNv.TabIndex = 169;
            this.lblTen_CbNv.Tag = "Ten_CbNv";
            this.lblTen_CbNv.Text = "Tên nhân viên";
            this.lblTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(32, 35);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 173;
            this.lblMa_CbNv.Tag = "Ma_CbNv";
            this.lblMa_CbNv.Text = "Mã nhân viên";
            this.lblMa_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.KeyFilter = "";
            this.txtTen_CbNv.Location = new System.Drawing.Point(118, 56);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.MaxLength = 200;
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(404, 20);
            this.txtTen_CbNv.TabIndex = 1;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // lblTrang_Thai
            // 
            this.lblTrang_Thai.AutoEllipsis = true;
            this.lblTrang_Thai.AutoSize = true;
            this.lblTrang_Thai.BackColor = System.Drawing.Color.Transparent;
            this.lblTrang_Thai.Location = new System.Drawing.Point(32, 155);
            this.lblTrang_Thai.Name = "lblTrang_Thai";
            this.lblTrang_Thai.Size = new System.Drawing.Size(55, 13);
            this.lblTrang_Thai.TabIndex = 175;
            this.lblTrang_Thai.Tag = "Trang_Thai";
            this.lblTrang_Thai.Text = "Trạng thái";
            this.lblTrang_Thai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGhi_Chu
            // 
            this.txtGhi_Chu.bEnabled = true;
            this.txtGhi_Chu.bIsLookup = false;
            this.txtGhi_Chu.bReadOnly = false;
            this.txtGhi_Chu.bRequire = false;
            this.txtGhi_Chu.KeyFilter = "";
            this.txtGhi_Chu.Location = new System.Drawing.Point(118, 178);
            this.txtGhi_Chu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGhi_Chu.MaxLength = 500;
            this.txtGhi_Chu.Multiline = true;
            this.txtGhi_Chu.Name = "txtGhi_Chu";
            this.txtGhi_Chu.Size = new System.Drawing.Size(404, 52);
            this.txtGhi_Chu.TabIndex = 6;
            this.txtGhi_Chu.UseAutoFilter = false;
            // 
            // lblGhi_Chu
            // 
            this.lblGhi_Chu.AutoEllipsis = true;
            this.lblGhi_Chu.AutoSize = true;
            this.lblGhi_Chu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhi_Chu.Location = new System.Drawing.Point(32, 181);
            this.lblGhi_Chu.Name = "lblGhi_Chu";
            this.lblGhi_Chu.Size = new System.Drawing.Size(44, 13);
            this.lblGhi_Chu.TabIndex = 193;
            this.lblGhi_Chu.Tag = "Ghi_Chu";
            this.lblGhi_Chu.Text = "Ghi chú";
            this.lblGhi_Chu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTrang_Thai
            // 
            this.cboTrang_Thai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrang_Thai.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTrang_Thai.FormattingEnabled = true;
            this.cboTrang_Thai.InitValue = null;
            this.cboTrang_Thai.Items.AddRange(new object[] {
            "Vào",
            "Ra"});
            this.cboTrang_Thai.Location = new System.Drawing.Point(118, 152);
            this.cboTrang_Thai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.cboTrang_Thai.Name = "cboTrang_Thai";
            this.cboTrang_Thai.Size = new System.Drawing.Size(89, 21);
            this.cboTrang_Thai.strValueList = null;
            this.cboTrang_Thai.TabIndex = 5;
            this.cboTrang_Thai.UseAutoComplete = false;
            this.cboTrang_Thai.UseBindingValue = false;
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(118, 32);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(164, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // lbtTen_ViTri
            // 
            this.lbtTen_ViTri.AutoEllipsis = true;
            this.lbtTen_ViTri.AutoSize = true;
            this.lbtTen_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_ViTri.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_ViTri.Location = new System.Drawing.Point(295, 107);
            this.lbtTen_ViTri.Name = "lbtTen_ViTri";
            this.lbtTen_ViTri.Size = new System.Drawing.Size(50, 13);
            this.lbtTen_ViTri.TabIndex = 195;
            this.lbtTen_ViTri.Tag = "Ten_ViTri";
            this.lbtTen_ViTri.Text = "Tên vị trí";
            this.lbtTen_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Bp
            // 
            this.lbtTen_Bp.AutoEllipsis = true;
            this.lbtTen_Bp.AutoSize = true;
            this.lbtTen_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Bp.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Bp.Location = new System.Drawing.Point(295, 83);
            this.lbtTen_Bp.Name = "lbtTen_Bp";
            this.lbtTen_Bp.Size = new System.Drawing.Size(68, 13);
            this.lbtTen_Bp.TabIndex = 194;
            this.lbtTen_Bp.Tag = "Ten_Bp";
            this.lbtTen_Bp.Text = "Tên bộ phận";
            this.lbtTen_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmChamCong_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 297);
            this.Controls.Add(this.lbtTen_ViTri);
            this.Controls.Add(this.lbtTen_Bp);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.cboTrang_Thai);
            this.Controls.Add(this.txtGhi_Chu);
            this.Controls.Add(this.lblGhi_Chu);
            this.Controls.Add(this.lblTrang_Thai);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.lblMa_CbNv);
            this.Controls.Add(this.lblTen_CbNv);
            this.Controls.Add(this.txtMa_ViTri);
            this.Controls.Add(this.lblMa_ViTri);
            this.Controls.Add(this.txtMa_Bp);
            this.Controls.Add(this.lblMa_Bp);
            this.Controls.Add(this.txtThoi_Gian);
            this.Controls.Add(this.lblThoi_Gian);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChamCong_Edit";
            this.Text = "frmChamCong_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Epoint.Systems.Customizes.btgAccept btgAccept;
        private Systems.Controls.txtTextLookup txtMa_Bp;
        private Systems.Controls.lblControl lblMa_Bp;
        private Systems.Controls.txtDateTime txtThoi_Gian;
        private Systems.Controls.lblControl lblThoi_Gian;
        private Systems.Controls.txtTextLookup txtMa_ViTri;
        private Systems.Controls.lblControl lblMa_ViTri;
        private Systems.Controls.lblControl lblTen_CbNv;
        private Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextBox txtTen_CbNv;
        private Systems.Controls.lblControl lblTrang_Thai;
        private Systems.Controls.txtTextBox txtGhi_Chu;
        private Systems.Controls.lblControl lblGhi_Chu;
        private Systems.Controls.cboControl cboTrang_Thai;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.lblControl lbtTen_ViTri;
        private Systems.Controls.lblControl lbtTen_Bp;

	}
}