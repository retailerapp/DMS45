namespace Epoint.Modules.AP
{
	partial class frmGiaMua_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGiaMua_Edit));
            this.lbtTen_Dt = new Epoint.Systems.Controls.lblControl();
            this.lblMa_Dt = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Vt = new Epoint.Systems.Controls.lblControl();
            this.lbGia = new Epoint.Systems.Controls.lblControl();
            this.numGia = new Epoint.Systems.Controls.numControl();
            this.dteNgay_Ap = new Epoint.Systems.Controls.txtDateTime();
            this.lbNgay_Ap = new Epoint.Systems.Controls.lblControl();
            this.lbMa_Vt = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.txtMa_Vt = new Epoint.Systems.Controls.txtTextLookup();
            this.txtMa_Dt = new Epoint.Systems.Controls.txtTextLookup();
            this.SuspendLayout();
            // 
            // lbtTen_Dt
            // 
            this.lbtTen_Dt.AutoEllipsis = true;
            this.lbtTen_Dt.AutoSize = true;
            this.lbtTen_Dt.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Dt.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Dt.Location = new System.Drawing.Point(255, 49);
            this.lbtTen_Dt.Name = "lbtTen_Dt";
            this.lbtTen_Dt.Size = new System.Drawing.Size(74, 13);
            this.lbtTen_Dt.TabIndex = 70;
            this.lbtTen_Dt.Text = "Tên đối tượng";
            this.lbtTen_Dt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_Dt
            // 
            this.lblMa_Dt.AutoEllipsis = true;
            this.lblMa_Dt.AutoSize = true;
            this.lblMa_Dt.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Dt.Location = new System.Drawing.Point(35, 49);
            this.lblMa_Dt.Name = "lblMa_Dt";
            this.lblMa_Dt.Size = new System.Drawing.Size(70, 13);
            this.lblMa_Dt.TabIndex = 69;
            this.lblMa_Dt.Tag = "Ma_Dt";
            this.lblMa_Dt.Text = "Mã đối tượng";
            this.lblMa_Dt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Vt
            // 
            this.lbtTen_Vt.AutoEllipsis = true;
            this.lbtTen_Vt.AutoSize = true;
            this.lbtTen_Vt.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Vt.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Vt.Location = new System.Drawing.Point(255, 26);
            this.lbtTen_Vt.Name = "lbtTen_Vt";
            this.lbtTen_Vt.Size = new System.Drawing.Size(56, 13);
            this.lbtTen_Vt.TabIndex = 68;
            this.lbtTen_Vt.Text = "Tên vật tư";
            this.lbtTen_Vt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbGia
            // 
            this.lbGia.AutoEllipsis = true;
            this.lbGia.AutoSize = true;
            this.lbGia.BackColor = System.Drawing.Color.Transparent;
            this.lbGia.Location = new System.Drawing.Point(35, 95);
            this.lbGia.Name = "lbGia";
            this.lbGia.Size = new System.Drawing.Size(46, 13);
            this.lbGia.TabIndex = 67;
            this.lbGia.Tag = "Gia_Mua";
            this.lbGia.Text = "Giá mua";
            this.lbGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGia
            // 
            this.numGia.bEnabled = true;
            this.numGia.bFormat = true;
            this.numGia.bIsLookup = false;
            this.numGia.bReadOnly = false;
            this.numGia.bRequire = false;
            this.numGia.KeyFilter = "";
            this.numGia.Location = new System.Drawing.Point(128, 92);
            this.numGia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numGia.Name = "numGia";
            this.numGia.Scale = 2;
            this.numGia.Size = new System.Drawing.Size(120, 20);
            this.numGia.TabIndex = 3;
            this.numGia.Text = "0.00";
            this.numGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numGia.UseAutoFilter = false;
            this.numGia.Value = 0D;
            // 
            // dteNgay_Ap
            // 
            this.dteNgay_Ap.bAllowEmpty = true;
            this.dteNgay_Ap.bRequire = false;
            this.dteNgay_Ap.bSelectOnFocus = false;
            this.dteNgay_Ap.bShowDateTimePicker = true;
            this.dteNgay_Ap.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_Ap.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ap.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ap.Location = new System.Drawing.Point(128, 69);
            this.dteNgay_Ap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ap.Mask = "00/00/0000";
            this.dteNgay_Ap.Name = "dteNgay_Ap";
            this.dteNgay_Ap.Size = new System.Drawing.Size(66, 20);
            this.dteNgay_Ap.TabIndex = 2;
            // 
            // lbNgay_Ap
            // 
            this.lbNgay_Ap.AutoEllipsis = true;
            this.lbNgay_Ap.AutoSize = true;
            this.lbNgay_Ap.BackColor = System.Drawing.Color.Transparent;
            this.lbNgay_Ap.Location = new System.Drawing.Point(35, 71);
            this.lbNgay_Ap.Name = "lbNgay_Ap";
            this.lbNgay_Ap.Size = new System.Drawing.Size(47, 13);
            this.lbNgay_Ap.TabIndex = 66;
            this.lbNgay_Ap.Tag = "Ngay_Ap";
            this.lbNgay_Ap.Text = "Ngày áp";
            this.lbNgay_Ap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMa_Vt
            // 
            this.lbMa_Vt.AutoEllipsis = true;
            this.lbMa_Vt.AutoSize = true;
            this.lbMa_Vt.BackColor = System.Drawing.Color.Transparent;
            this.lbMa_Vt.Location = new System.Drawing.Point(35, 26);
            this.lbMa_Vt.Name = "lbMa_Vt";
            this.lbMa_Vt.Size = new System.Drawing.Size(52, 13);
            this.lbMa_Vt.TabIndex = 65;
            this.lbMa_Vt.Tag = "Ma_Vt";
            this.lbMa_Vt.Text = "Mã vật tư";
            this.lbMa_Vt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(292, 136);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 33);
            this.btgAccept.TabIndex = 73;
            // 
            // txtMa_Vt
            // 
            this.txtMa_Vt.bEnabled = true;
            this.txtMa_Vt.bIsLookup = false;
            this.txtMa_Vt.bReadOnly = false;
            this.txtMa_Vt.bRequire = false;
            this.txtMa_Vt.ColumnsView = null;
            this.txtMa_Vt.KeyFilter = "Ma_Vt";
            this.txtMa_Vt.Location = new System.Drawing.Point(128, 23);
            this.txtMa_Vt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Vt.Name = "txtMa_Vt";
            this.txtMa_Vt.Size = new System.Drawing.Size(120, 20);
            this.txtMa_Vt.TabIndex = 0;
            this.txtMa_Vt.UseAutoFilter = true;
            // 
            // txtMa_Dt
            // 
            this.txtMa_Dt.bEnabled = true;
            this.txtMa_Dt.bIsLookup = false;
            this.txtMa_Dt.bReadOnly = false;
            this.txtMa_Dt.bRequire = false;
            this.txtMa_Dt.ColumnsView = null;
            this.txtMa_Dt.KeyFilter = "Ma_Dt";
            this.txtMa_Dt.Location = new System.Drawing.Point(128, 46);
            this.txtMa_Dt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Dt.Name = "txtMa_Dt";
            this.txtMa_Dt.Size = new System.Drawing.Size(120, 20);
            this.txtMa_Dt.TabIndex = 1;
            this.txtMa_Dt.UseAutoFilter = true;
            // 
            // frmGiaMua_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 182);
            this.Controls.Add(this.txtMa_Dt);
            this.Controls.Add(this.txtMa_Vt);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lbtTen_Dt);
            this.Controls.Add(this.lblMa_Dt);
            this.Controls.Add(this.lbtTen_Vt);
            this.Controls.Add(this.lbGia);
            this.Controls.Add(this.numGia);
            this.Controls.Add(this.dteNgay_Ap);
            this.Controls.Add(this.lbNgay_Ap);
            this.Controls.Add(this.lbMa_Vt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGiaMua_Edit";
            this.Tag = "frmGiaBan";
            this.Text = "frmGiaBan";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private Epoint.Systems.Controls.lblControl lbtTen_Dt;
		private Epoint.Systems.Controls.lblControl lblMa_Dt;
		private Epoint.Systems.Controls.lblControl lbtTen_Vt;
		private Epoint.Systems.Controls.lblControl lbGia;
		private Epoint.Systems.Controls.numControl numGia;
		private Epoint.Systems.Controls.txtDateTime dteNgay_Ap;
        private Epoint.Systems.Controls.lblControl lbNgay_Ap;
		private Epoint.Systems.Controls.lblControl lbMa_Vt;
		private Epoint.Systems.Customizes.btgAccept btgAccept;
        private Systems.Controls.txtTextLookup txtMa_Vt;
        private Systems.Controls.txtTextLookup txtMa_Dt;
	}
}