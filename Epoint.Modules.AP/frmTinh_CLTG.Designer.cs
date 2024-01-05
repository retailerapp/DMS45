namespace Epoint.Modules.GL
{
	partial class frmTinh_CLTG
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvKetQuaCLTG = new Epoint.Systems.Controls.dgvControl();
            this.btTinhCLTG = new Epoint.Systems.Controls.btControl();
            this.dteNgay_Ct = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_Ct1 = new Epoint.Systems.Controls.lblControl();
            this.txtSo_Ct = new Epoint.Systems.Controls.txtTextBox();
            this.lblSo_Ct = new Epoint.Systems.Controls.lblControl();
            this.txtMa_Ct = new Epoint.Systems.Controls.txtTextBox();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.numTy_Gia = new Epoint.Systems.Controls.numControl();
            this.lblControl9 = new Epoint.Systems.Controls.lblControl();
            this.chkIs_Hach_Toan = new Epoint.Systems.Controls.chkControl();
            this.dgvCLTG = new Epoint.Systems.Controls.dgvControl();
            this.btTinhCLTG_HetSoDu = new Epoint.Systems.Controls.btControl();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaCLTG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCLTG)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKetQuaCLTG
            // 
            this.dgvKetQuaCLTG.AllowUserToAddRows = false;
            this.dgvKetQuaCLTG.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvKetQuaCLTG.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKetQuaCLTG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKetQuaCLTG.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQuaCLTG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvKetQuaCLTG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQuaCLTG.EnableHeadersVisualStyles = false;
            this.dgvKetQuaCLTG.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvKetQuaCLTG.Location = new System.Drawing.Point(2, 304);
            this.dgvKetQuaCLTG.MultiSelect = false;
            this.dgvKetQuaCLTG.Name = "dgvKetQuaCLTG";
            this.dgvKetQuaCLTG.ReadOnly = true;
            this.dgvKetQuaCLTG.Size = new System.Drawing.Size(911, 261);
            this.dgvKetQuaCLTG.strZone = "";
            this.dgvKetQuaCLTG.TabIndex = 7;
            // 
            // btTinhCLTG
            // 
            this.btTinhCLTG.Location = new System.Drawing.Point(583, 7);
            this.btTinhCLTG.Name = "btTinhCLTG";
            this.btTinhCLTG.Size = new System.Drawing.Size(85, 29);
            this.btTinhCLTG.TabIndex = 4;
            this.btTinhCLTG.Tag = "";
            this.btTinhCLTG.Text = "&1. Tính CLTG";
            this.btTinhCLTG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btTinhCLTG.UseVisualStyleBackColor = true;
            // 
            // dteNgay_Ct
            // 
            this.dteNgay_Ct.bAllowEmpty = true;
            this.dteNgay_Ct.bRequire = false;
            this.dteNgay_Ct.bSelectOnFocus = false;
            this.dteNgay_Ct.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteNgay_Ct.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ct.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ct.Location = new System.Drawing.Point(110, 11);
            this.dteNgay_Ct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ct.Mask = "00/00/0000";
            this.dteNgay_Ct.Name = "dteNgay_Ct";
            this.dteNgay_Ct.Size = new System.Drawing.Size(71, 20);
            this.dteNgay_Ct.TabIndex = 0;
            // 
            // lblNgay_Ct1
            // 
            this.lblNgay_Ct1.AutoEllipsis = true;
            this.lblNgay_Ct1.AutoSize = true;
            this.lblNgay_Ct1.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Ct1.Location = new System.Drawing.Point(15, 15);
            this.lblNgay_Ct1.Name = "lblNgay_Ct1";
            this.lblNgay_Ct1.Size = new System.Drawing.Size(93, 13);
            this.lblNgay_Ct1.TabIndex = 89;
            this.lblNgay_Ct1.Tag = "";
            this.lblNgay_Ct1.Text = "Ngày đánh giá CL";
            this.lblNgay_Ct1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSo_Ct
            // 
            this.txtSo_Ct.bEnabled = true;
            this.txtSo_Ct.bIsLookup = false;
            this.txtSo_Ct.bReadOnly = false;
            this.txtSo_Ct.bRequire = false;
            this.txtSo_Ct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSo_Ct.KeyFilter = "";
            this.txtSo_Ct.Location = new System.Drawing.Point(306, 11);
            this.txtSo_Ct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtSo_Ct.MaxLength = 20;
            this.txtSo_Ct.Name = "txtSo_Ct";
            this.txtSo_Ct.Size = new System.Drawing.Size(100, 20);
            this.txtSo_Ct.TabIndex = 2;
            this.txtSo_Ct.Text = "CLTG";
            this.txtSo_Ct.UseAutoFilter = false;
            // 
            // lblSo_Ct
            // 
            this.lblSo_Ct.AutoEllipsis = true;
            this.lblSo_Ct.AutoSize = true;
            this.lblSo_Ct.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Ct.Location = new System.Drawing.Point(267, 14);
            this.lblSo_Ct.Name = "lblSo_Ct";
            this.lblSo_Ct.Size = new System.Drawing.Size(33, 13);
            this.lblSo_Ct.TabIndex = 91;
            this.lblSo_Ct.Tag = "So_Ct";
            this.lblSo_Ct.Text = "Số Ct";
            this.lblSo_Ct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMa_Ct
            // 
            this.txtMa_Ct.bEnabled = true;
            this.txtMa_Ct.bIsLookup = false;
            this.txtMa_Ct.bReadOnly = false;
            this.txtMa_Ct.bRequire = false;
            this.txtMa_Ct.KeyFilter = "";
            this.txtMa_Ct.Location = new System.Drawing.Point(227, 11);
            this.txtMa_Ct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Ct.Name = "txtMa_Ct";
            this.txtMa_Ct.Size = new System.Drawing.Size(31, 20);
            this.txtMa_Ct.TabIndex = 1;
            this.txtMa_Ct.Text = "TD";
            this.txtMa_Ct.UseAutoFilter = false;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(189, 15);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(35, 13);
            this.lblControl1.TabIndex = 92;
            this.lblControl1.Tag = "";
            this.lblControl1.Text = "Mã Ct";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numTy_Gia
            // 
            this.numTy_Gia.bEnabled = true;
            this.numTy_Gia.bFormat = true;
            this.numTy_Gia.bIsLookup = false;
            this.numTy_Gia.bReadOnly = false;
            this.numTy_Gia.bRequire = false;
            this.numTy_Gia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTy_Gia.KeyFilter = "";
            this.numTy_Gia.Location = new System.Drawing.Point(457, 12);
            this.numTy_Gia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numTy_Gia.Name = "numTy_Gia";
            this.numTy_Gia.Scale = 2;
            this.numTy_Gia.Size = new System.Drawing.Size(112, 20);
            this.numTy_Gia.TabIndex = 3;
            this.numTy_Gia.Text = "0.00";
            this.numTy_Gia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTy_Gia.UseAutoFilter = false;
            this.numTy_Gia.Value = 0D;
            // 
            // lblControl9
            // 
            this.lblControl9.AutoEllipsis = true;
            this.lblControl9.AutoSize = true;
            this.lblControl9.BackColor = System.Drawing.Color.Transparent;
            this.lblControl9.Location = new System.Drawing.Point(416, 15);
            this.lblControl9.Name = "lblControl9";
            this.lblControl9.Size = new System.Drawing.Size(36, 13);
            this.lblControl9.TabIndex = 95;
            this.lblControl9.Tag = "";
            this.lblControl9.Text = "Tỷ giá";
            this.lblControl9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIs_Hach_Toan
            // 
            this.chkIs_Hach_Toan.AutoSize = true;
            this.chkIs_Hach_Toan.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIs_Hach_Toan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkIs_Hach_Toan.Location = new System.Drawing.Point(12, 46);
            this.chkIs_Hach_Toan.Name = "chkIs_Hach_Toan";
            this.chkIs_Hach_Toan.Size = new System.Drawing.Size(125, 17);
            this.chkIs_Hach_Toan.TabIndex = 5;
            this.chkIs_Hach_Toan.Text = "Tạo phiếu hạch toán";
            this.chkIs_Hach_Toan.UseVisualStyleBackColor = true;
            // 
            // dgvCLTG
            // 
            this.dgvCLTG.AllowUserToAddRows = false;
            this.dgvCLTG.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCLTG.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCLTG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCLTG.BackgroundColor = System.Drawing.Color.White;
            this.dgvCLTG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCLTG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCLTG.EnableHeadersVisualStyles = false;
            this.dgvCLTG.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvCLTG.Location = new System.Drawing.Point(3, 68);
            this.dgvCLTG.Margin = new System.Windows.Forms.Padding(1);
            this.dgvCLTG.MultiSelect = false;
            this.dgvCLTG.Name = "dgvCLTG";
            this.dgvCLTG.ReadOnly = true;
            this.dgvCLTG.Size = new System.Drawing.Size(910, 232);
            this.dgvCLTG.strZone = "";
            this.dgvCLTG.TabIndex = 6;
            // 
            // btTinhCLTG_HetSoDu
            // 
            this.btTinhCLTG_HetSoDu.Location = new System.Drawing.Point(674, 7);
            this.btTinhCLTG_HetSoDu.Name = "btTinhCLTG_HetSoDu";
            this.btTinhCLTG_HetSoDu.Size = new System.Drawing.Size(85, 29);
            this.btTinhCLTG_HetSoDu.TabIndex = 8;
            this.btTinhCLTG_HetSoDu.Tag = "";
            this.btTinhCLTG_HetSoDu.Text = "&2. Tính CLTG";
            this.btTinhCLTG_HetSoDu.UseVisualStyleBackColor = true;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.ForeColor = System.Drawing.Color.Blue;
            this.lblControl2.Location = new System.Drawing.Point(764, 14);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(144, 13);
            this.lblControl2.TabIndex = 96;
            this.lblControl2.Tag = "";
            this.lblControl2.Text = "( Cho tài khoản hết ngoại tệ )";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmTinh_CLTG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 569);
            this.Controls.Add(this.lblControl2);
            this.Controls.Add(this.chkIs_Hach_Toan);
            this.Controls.Add(this.numTy_Gia);
            this.Controls.Add(this.lblControl9);
            this.Controls.Add(this.txtMa_Ct);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.txtSo_Ct);
            this.Controls.Add(this.lblSo_Ct);
            this.Controls.Add(this.dteNgay_Ct);
            this.Controls.Add(this.lblNgay_Ct1);
            this.Controls.Add(this.btTinhCLTG_HetSoDu);
            this.Controls.Add(this.btTinhCLTG);
            this.Controls.Add(this.dgvCLTG);
            this.Controls.Add(this.dgvKetQuaCLTG);
            this.Name = "frmTinh_CLTG";
            this.Tag = "frmCtTS, F2, F3, F8, ESC";
            this.Text = "frmTinh_CLTG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQuaCLTG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCLTG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Epoint.Systems.Controls.dgvControl dgvKetQuaCLTG;
		private Epoint.Systems.Controls.btControl btTinhCLTG;
		private Epoint.Systems.Controls.txtDateTime dteNgay_Ct;
		private Epoint.Systems.Controls.lblControl lblNgay_Ct1;
		private Epoint.Systems.Controls.txtTextBox txtSo_Ct;
		private Epoint.Systems.Controls.lblControl lblSo_Ct;
		private Epoint.Systems.Controls.txtTextBox txtMa_Ct;
		private Epoint.Systems.Controls.lblControl lblControl1;
		private Epoint.Systems.Controls.numControl numTy_Gia;
		private Epoint.Systems.Controls.lblControl lblControl9;
		private Epoint.Systems.Controls.chkControl chkIs_Hach_Toan;
		private Epoint.Systems.Controls.dgvControl dgvCLTG;
		private Epoint.Systems.Controls.btControl btTinhCLTG_HetSoDu;
        private Systems.Controls.lblControl lblControl2;


	}
}