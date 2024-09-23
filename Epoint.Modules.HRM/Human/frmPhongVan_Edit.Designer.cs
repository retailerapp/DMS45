namespace Epoint.Modules.HRM
{
	partial class frmPhongVan_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhongVan_Edit));
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.lblNgay_Pv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.dteNgay_Pv = new Epoint.Systems.Controls.txtDateTime();
            this.txtMa_Bp = new Epoint.Systems.Controls.txtTextLookup();
            this.lbtTen_Bp = new Epoint.Systems.Controls.lblControl();
            this.lblControl14 = new Epoint.Systems.Controls.lblControl();
            this.txtGio_Pv = new Epoint.Systems.Controls.txtTextBox();
            this.lblGio_Pv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_ViTri = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Sp = new Epoint.Systems.Controls.lblControl();
            this.txtMa_Sp = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_Sp = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.KeyFilter = "";
            this.txtTen_CbNv.Location = new System.Drawing.Point(127, 81);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.MaxLength = 100;
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(271, 20);
            this.txtTen_CbNv.TabIndex = 2;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // lblNgay_Pv
            // 
            this.lblNgay_Pv.AutoEllipsis = true;
            this.lblNgay_Pv.AutoSize = true;
            this.lblNgay_Pv.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Pv.Location = new System.Drawing.Point(25, 36);
            this.lblNgay_Pv.Name = "lblNgay_Pv";
            this.lblNgay_Pv.Size = new System.Drawing.Size(86, 13);
            this.lblNgay_Pv.TabIndex = 70;
            this.lblNgay_Pv.Tag = "Ngay_Pv";
            this.lblNgay_Pv.Text = "Ngày phòng vấn";
            this.lblNgay_Pv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(309, 193);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 7;
            // 
            // lblTen_CbNv
            // 
            this.lblTen_CbNv.AutoEllipsis = true;
            this.lblTen_CbNv.AutoSize = true;
            this.lblTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_CbNv.Location = new System.Drawing.Point(25, 84);
            this.lblTen_CbNv.Name = "lblTen_CbNv";
            this.lblTen_CbNv.Size = new System.Drawing.Size(76, 13);
            this.lblTen_CbNv.TabIndex = 70;
            this.lblTen_CbNv.Tag = "Ten_CbNv";
            this.lblTen_CbNv.Text = "Tên nhân viên";
            this.lblTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dteNgay_Pv
            // 
            this.dteNgay_Pv.bAllowEmpty = true;
            this.dteNgay_Pv.bRequire = false;
            this.dteNgay_Pv.bSelectOnFocus = false;
            this.dteNgay_Pv.bShowDateTimePicker = true;
            this.dteNgay_Pv.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_Pv.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Pv.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Pv.Location = new System.Drawing.Point(127, 33);
            this.dteNgay_Pv.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Pv.Mask = "00/00/0000";
            this.dteNgay_Pv.Name = "dteNgay_Pv";
            this.dteNgay_Pv.Size = new System.Drawing.Size(120, 20);
            this.dteNgay_Pv.TabIndex = 0;
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
            this.txtMa_Bp.Location = new System.Drawing.Point(127, 105);
            this.txtMa_Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Bp.Name = "txtMa_Bp";
            this.txtMa_Bp.Size = new System.Drawing.Size(120, 20);
            this.txtMa_Bp.TabIndex = 3;
            this.txtMa_Bp.UseAutoFilter = true;
            // 
            // lbtTen_Bp
            // 
            this.lbtTen_Bp.AutoEllipsis = true;
            this.lbtTen_Bp.AutoSize = true;
            this.lbtTen_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Bp.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Bp.Location = new System.Drawing.Point(255, 110);
            this.lbtTen_Bp.Name = "lbtTen_Bp";
            this.lbtTen_Bp.Size = new System.Drawing.Size(71, 13);
            this.lbtTen_Bp.TabIndex = 128;
            this.lbtTen_Bp.Text = "Tên bộ phận ";
            this.lbtTen_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl14
            // 
            this.lblControl14.AutoEllipsis = true;
            this.lblControl14.AutoSize = true;
            this.lblControl14.BackColor = System.Drawing.Color.Transparent;
            this.lblControl14.Location = new System.Drawing.Point(25, 108);
            this.lblControl14.Name = "lblControl14";
            this.lblControl14.Size = new System.Drawing.Size(64, 13);
            this.lblControl14.TabIndex = 129;
            this.lblControl14.Tag = "Ma_Bp";
            this.lblControl14.Text = "Mã bộ phận";
            this.lblControl14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGio_Pv
            // 
            this.txtGio_Pv.bEnabled = true;
            this.txtGio_Pv.bIsLookup = false;
            this.txtGio_Pv.bReadOnly = false;
            this.txtGio_Pv.bRequire = false;
            this.txtGio_Pv.KeyFilter = "";
            this.txtGio_Pv.Location = new System.Drawing.Point(127, 57);
            this.txtGio_Pv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtGio_Pv.MaxLength = 20;
            this.txtGio_Pv.Name = "txtGio_Pv";
            this.txtGio_Pv.Size = new System.Drawing.Size(120, 20);
            this.txtGio_Pv.TabIndex = 1;
            this.txtGio_Pv.UseAutoFilter = false;
            // 
            // lblGio_Pv
            // 
            this.lblGio_Pv.AutoEllipsis = true;
            this.lblGio_Pv.AutoSize = true;
            this.lblGio_Pv.BackColor = System.Drawing.Color.Transparent;
            this.lblGio_Pv.Location = new System.Drawing.Point(25, 60);
            this.lblGio_Pv.Name = "lblGio_Pv";
            this.lblGio_Pv.Size = new System.Drawing.Size(77, 13);
            this.lblGio_Pv.TabIndex = 131;
            this.lblGio_Pv.Tag = "Gio_Pv";
            this.lblGio_Pv.Text = "Giờ phỏng vấn";
            this.lblGio_Pv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtMa_ViTri.Location = new System.Drawing.Point(127, 129);
            this.txtMa_ViTri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_ViTri.Name = "txtMa_ViTri";
            this.txtMa_ViTri.Size = new System.Drawing.Size(120, 20);
            this.txtMa_ViTri.TabIndex = 4;
            this.txtMa_ViTri.UseAutoFilter = true;
            // 
            // lblMa_ViTri
            // 
            this.lblMa_ViTri.AutoEllipsis = true;
            this.lblMa_ViTri.AutoSize = true;
            this.lblMa_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_ViTri.Location = new System.Drawing.Point(25, 132);
            this.lblMa_ViTri.Name = "lblMa_ViTri";
            this.lblMa_ViTri.Size = new System.Drawing.Size(46, 13);
            this.lblMa_ViTri.TabIndex = 133;
            this.lblMa_ViTri.Tag = "Ma_ViTri";
            this.lblMa_ViTri.Text = "Mã vị trí";
            this.lblMa_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_ViTri
            // 
            this.lbtTen_ViTri.AutoEllipsis = true;
            this.lbtTen_ViTri.AutoSize = true;
            this.lbtTen_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_ViTri.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_ViTri.Location = new System.Drawing.Point(255, 132);
            this.lbtTen_ViTri.Name = "lbtTen_ViTri";
            this.lbtTen_ViTri.Size = new System.Drawing.Size(50, 13);
            this.lbtTen_ViTri.TabIndex = 134;
            this.lbtTen_ViTri.Text = "Tên vị trí";
            this.lbtTen_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Sp
            // 
            this.lbtTen_Sp.AutoEllipsis = true;
            this.lbtTen_Sp.AutoSize = true;
            this.lbtTen_Sp.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Sp.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Sp.Location = new System.Drawing.Point(255, 156);
            this.lbtTen_Sp.Name = "lbtTen_Sp";
            this.lbtTen_Sp.Size = new System.Drawing.Size(75, 13);
            this.lbtTen_Sp.TabIndex = 137;
            this.lbtTen_Sp.Text = "Tên sản phẩm";
            this.lbtTen_Sp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMa_Sp
            // 
            this.txtMa_Sp.bEnabled = true;
            this.txtMa_Sp.bIsLookup = false;
            this.txtMa_Sp.bReadOnly = false;
            this.txtMa_Sp.bRequire = false;
            this.txtMa_Sp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Sp.ColumnsView = null;
            this.txtMa_Sp.KeyFilter = "Ma_ViTri";
            this.txtMa_Sp.Location = new System.Drawing.Point(127, 153);
            this.txtMa_Sp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Sp.Name = "txtMa_Sp";
            this.txtMa_Sp.Size = new System.Drawing.Size(120, 20);
            this.txtMa_Sp.TabIndex = 6;
            this.txtMa_Sp.UseAutoFilter = true;
            // 
            // lblMa_Sp
            // 
            this.lblMa_Sp.AutoEllipsis = true;
            this.lblMa_Sp.AutoSize = true;
            this.lblMa_Sp.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Sp.Location = new System.Drawing.Point(25, 156);
            this.lblMa_Sp.Name = "lblMa_Sp";
            this.lblMa_Sp.Size = new System.Drawing.Size(71, 13);
            this.lblMa_Sp.TabIndex = 136;
            this.lblMa_Sp.Tag = "Ma_Sp";
            this.lblMa_Sp.Text = "Mã sản phẩm";
            this.lblMa_Sp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPhongVan_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 240);
            this.Controls.Add(this.lbtTen_Sp);
            this.Controls.Add(this.txtMa_Sp);
            this.Controls.Add(this.lblMa_Sp);
            this.Controls.Add(this.lbtTen_ViTri);
            this.Controls.Add(this.txtMa_ViTri);
            this.Controls.Add(this.lblMa_ViTri);
            this.Controls.Add(this.txtGio_Pv);
            this.Controls.Add(this.lblGio_Pv);
            this.Controls.Add(this.txtMa_Bp);
            this.Controls.Add(this.lbtTen_Bp);
            this.Controls.Add(this.lblControl14);
            this.Controls.Add(this.dteNgay_Pv);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.lblTen_CbNv);
            this.Controls.Add(this.lblNgay_Pv);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPhongVan_Edit";
            this.Tag = "frmThueTNCN_Edit";
            this.Text = "frmThueTNCN_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Epoint.Systems.Controls.txtTextBox txtTen_CbNv;
		private Epoint.Systems.Controls.lblControl lblNgay_Pv;
		private Epoint.Systems.Customizes.btgAccept btgAccept;
        private Epoint.Systems.Controls.lblControl lblTen_CbNv;
        private Systems.Controls.txtDateTime dteNgay_Pv;
        private Systems.Controls.txtTextLookup txtMa_Bp;
        private Systems.Controls.lblControl lbtTen_Bp;
        private Systems.Controls.lblControl lblControl14;
        private Systems.Controls.txtTextBox txtGio_Pv;
        private Systems.Controls.lblControl lblGio_Pv;
        private Systems.Controls.txtTextLookup txtMa_ViTri;
        private Systems.Controls.lblControl lblMa_ViTri;
        private Systems.Controls.lblControl lbtTen_ViTri;
        private Systems.Controls.lblControl lbtTen_Sp;
        private Systems.Controls.txtTextLookup txtMa_Sp;
        private Systems.Controls.lblControl lblMa_Sp;
	}
}