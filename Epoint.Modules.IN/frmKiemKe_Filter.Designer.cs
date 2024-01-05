namespace Epoint.Modules.IN
{
	partial class frmKiemKe_Filter 
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
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Kho = new Epoint.Systems.Controls.lbtControl();
            this.lbtTen_Vt = new Epoint.Systems.Controls.lbtControl();
            this.lblMa_Vt = new Epoint.Systems.Controls.lblControl();
            this.lblControl6 = new Epoint.Systems.Controls.lblControl();
            this.numThang = new Epoint.Systems.Controls.numControl();
            this.chkCapNhatTonKho = new Epoint.Systems.Controls.chkControl();
            this.chkCapNhatKiemKe = new Epoint.Systems.Controls.chkControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.txtMa_Kho = new Epoint.Systems.Controls.txtTextLookup();
            this.txtMa_Vt = new Epoint.Systems.Controls.txtTextLookup();
            this.SuspendLayout();
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(24, 52);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(43, 13);
            this.lblControl1.TabIndex = 2;
            this.lblControl1.Tag = "Ma_Kho";
            this.lblControl1.Text = "Mã kho";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Kho
            // 
            this.lbtTen_Kho.AutoSize = true;
            this.lbtTen_Kho.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Kho.Location = new System.Drawing.Point(221, 52);
            this.lbtTen_Kho.Name = "lbtTen_Kho";
            this.lbtTen_Kho.Size = new System.Drawing.Size(51, 13);
            this.lbtTen_Kho.TabIndex = 3;
            this.lbtTen_Kho.Tag = "Ten_Kho";
            this.lbtTen_Kho.Text = "Ten_Kho";
            this.lbtTen_Kho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Vt
            // 
            this.lbtTen_Vt.AutoSize = true;
            this.lbtTen_Vt.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Vt.Location = new System.Drawing.Point(221, 75);
            this.lbtTen_Vt.Name = "lbtTen_Vt";
            this.lbtTen_Vt.Size = new System.Drawing.Size(42, 13);
            this.lbtTen_Vt.TabIndex = 3;
            this.lbtTen_Vt.Tag = "Ten_Vt";
            this.lbtTen_Vt.Text = "Ten_Vt";
            this.lbtTen_Vt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_Vt
            // 
            this.lblMa_Vt.AutoEllipsis = true;
            this.lblMa_Vt.AutoSize = true;
            this.lblMa_Vt.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Vt.Location = new System.Drawing.Point(24, 75);
            this.lblMa_Vt.Name = "lblMa_Vt";
            this.lblMa_Vt.Size = new System.Drawing.Size(52, 13);
            this.lblMa_Vt.TabIndex = 2;
            this.lblMa_Vt.Tag = "Ma_Vt";
            this.lblMa_Vt.Text = "Mã vật tư";
            this.lblMa_Vt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl6
            // 
            this.lblControl6.AutoEllipsis = true;
            this.lblControl6.AutoSize = true;
            this.lblControl6.BackColor = System.Drawing.Color.Transparent;
            this.lblControl6.Location = new System.Drawing.Point(24, 29);
            this.lblControl6.Name = "lblControl6";
            this.lblControl6.Size = new System.Drawing.Size(38, 13);
            this.lblControl6.TabIndex = 2;
            this.lblControl6.Tag = "Thang";
            this.lblControl6.Text = "Tháng";
            this.lblControl6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numThang
            // 
            this.numThang.bEnabled = true;
            this.numThang.bFormat = true;
            this.numThang.bIsLookup = false;
            this.numThang.bReadOnly = false;
            this.numThang.bRequire = false;
            this.numThang.KeyFilter = "";
            this.numThang.Location = new System.Drawing.Point(115, 27);
            this.numThang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numThang.Name = "numThang";
            this.numThang.Scale = 0;
            this.numThang.Size = new System.Drawing.Size(37, 20);
            this.numThang.TabIndex = 0;
            this.numThang.Tag = "";
            this.numThang.Text = "0";
            this.numThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numThang.UseAutoFilter = false;
            this.numThang.Value = 0D;
            // 
            // chkCapNhatTonKho
            // 
            this.chkCapNhatTonKho.AutoSize = true;
            this.chkCapNhatTonKho.Location = new System.Drawing.Point(115, 106);
            this.chkCapNhatTonKho.Name = "chkCapNhatTonKho";
            this.chkCapNhatTonKho.Size = new System.Drawing.Size(154, 17);
            this.chkCapNhatTonKho.TabIndex = 3;
            this.chkCapNhatTonKho.Text = "Cập nhật lại số liệu tồn kho";
            this.chkCapNhatTonKho.UseVisualStyleBackColor = true;
            // 
            // chkCapNhatKiemKe
            // 
            this.chkCapNhatKiemKe.AutoSize = true;
            this.chkCapNhatKiemKe.Location = new System.Drawing.Point(115, 129);
            this.chkCapNhatKiemKe.Name = "chkCapNhatKiemKe";
            this.chkCapNhatKiemKe.Size = new System.Drawing.Size(209, 17);
            this.chkCapNhatKiemKe.TabIndex = 4;
            this.chkCapNhatKiemKe.Text = "Tự động lấy số tồn kho làm số kiểm kê";
            this.chkCapNhatKiemKe.UseVisualStyleBackColor = true;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(241, 169);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(181, 29);
            this.btgAccept.TabIndex = 5;
            // 
            // txtMa_Kho
            // 
            this.txtMa_Kho.bEnabled = true;
            this.txtMa_Kho.bIsLookup = false;
            this.txtMa_Kho.bReadOnly = false;
            this.txtMa_Kho.bRequire = false;
            this.txtMa_Kho.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Kho.KeyFilter = "Ma_Kho";
            this.txtMa_Kho.Location = new System.Drawing.Point(115, 50);
            this.txtMa_Kho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Kho.Name = "txtMa_Kho";
            this.txtMa_Kho.Size = new System.Drawing.Size(101, 20);
            this.txtMa_Kho.TabIndex = 1;
            this.txtMa_Kho.UseAutoFilter = true;
            // 
            // txtMa_Vt
            // 
            this.txtMa_Vt.bEnabled = true;
            this.txtMa_Vt.bIsLookup = false;
            this.txtMa_Vt.bReadOnly = false;
            this.txtMa_Vt.bRequire = false;
            this.txtMa_Vt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Vt.KeyFilter = "Ma_Vt";
            this.txtMa_Vt.Location = new System.Drawing.Point(115, 73);
            this.txtMa_Vt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Vt.Name = "txtMa_Vt";
            this.txtMa_Vt.Size = new System.Drawing.Size(101, 20);
            this.txtMa_Vt.TabIndex = 2;
            this.txtMa_Vt.UseAutoFilter = true;
            // 
            // frmKiemKe_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 210);
            this.Controls.Add(this.txtMa_Vt);
            this.Controls.Add(this.txtMa_Kho);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.chkCapNhatTonKho);
            this.Controls.Add(this.chkCapNhatKiemKe);
            this.Controls.Add(this.numThang);
            this.Controls.Add(this.lblMa_Vt);
            this.Controls.Add(this.lblControl6);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.lbtTen_Vt);
            this.Controls.Add(this.lbtTen_Kho);
            this.Name = "frmKiemKe_Filter";
            this.Tag = "frmKiem_Ke_Filter";
            this.Text = "frmKiem_Ke_Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private Epoint.Systems.Controls.lblControl lblControl1;
		private Epoint.Systems.Controls.lbtControl lbtTen_Kho;
		private Epoint.Systems.Controls.lbtControl lbtTen_Vt;
        private Epoint.Systems.Controls.lblControl lblMa_Vt;
        private Epoint.Systems.Controls.lblControl lblControl6;
        public Epoint.Systems.Controls.numControl numThang;
        public Epoint.Systems.Controls.chkControl chkCapNhatTonKho;
        public Epoint.Systems.Controls.chkControl chkCapNhatKiemKe;
        private Epoint.Systems.Customizes.btgAccept btgAccept;
        public Systems.Controls.txtTextLookup txtMa_Kho;
        public Systems.Controls.txtTextLookup txtMa_Vt;
	}
}