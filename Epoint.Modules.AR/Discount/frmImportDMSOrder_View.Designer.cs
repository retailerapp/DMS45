namespace Epoint.Modules.AR
{
    partial class frmImportDMSOrder_View
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportDMSOrder_View));
            this.dgvViewHD = new Epoint.Systems.Controls.dgvControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbNgay_Ap = new Epoint.Systems.Controls.lblControl();
            this.dteNgay_BD = new Epoint.Systems.Controls.dteDateTime();
            this.dteNgay_Kt = new Epoint.Systems.Controls.dteDateTime();
            this.lbGia = new Epoint.Systems.Controls.lblControl();
            this.btLoadData = new Epoint.Systems.Customizes.btPreview();
            this.btThanhtoan = new Epoint.Systems.Customizes.btPreview();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewHD)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvViewHD
            // 
            this.dgvViewHD.AllowUserToAddRows = false;
            this.dgvViewHD.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvViewHD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvViewHD.BackgroundColor = System.Drawing.Color.White;
            this.dgvViewHD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViewHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvViewHD.EnableHeadersVisualStyles = false;
            this.dgvViewHD.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvViewHD.Location = new System.Drawing.Point(3, 16);
            this.dgvViewHD.Margin = new System.Windows.Forms.Padding(1);
            this.dgvViewHD.MultiSelect = false;
            this.dgvViewHD.Name = "dgvViewHD";
            this.dgvViewHD.ReadOnly = true;
            this.dgvViewHD.Size = new System.Drawing.Size(1133, 487);
            this.dgvViewHD.strZone = "";
            this.dgvViewHD.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbNgay_Ap);
            this.groupBox1.Controls.Add(this.dteNgay_BD);
            this.groupBox1.Controls.Add(this.dteNgay_Kt);
            this.groupBox1.Controls.Add(this.lbGia);
            this.groupBox1.Controls.Add(this.btLoadData);
            this.groupBox1.Controls.Add(this.btThanhtoan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1139, 79);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
            // 
            // lbNgay_Ap
            // 
            this.lbNgay_Ap.AutoEllipsis = true;
            this.lbNgay_Ap.AutoSize = true;
            this.lbNgay_Ap.BackColor = System.Drawing.Color.Transparent;
            this.lbNgay_Ap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgay_Ap.Location = new System.Drawing.Point(29, 16);
            this.lbNgay_Ap.Name = "lbNgay_Ap";
            this.lbNgay_Ap.Size = new System.Drawing.Size(72, 13);
            this.lbNgay_Ap.TabIndex = 141;
            this.lbNgay_Ap.Tag = "Ngay_BD";
            this.lbNgay_Ap.Text = "Ngày bắt đầu";
            this.lbNgay_Ap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dteNgay_BD
            // 
            this.dteNgay_BD.bAllowEmpty = true;
            this.dteNgay_BD.bRequire = false;
            this.dteNgay_BD.bSelectOnFocus = false;
            this.dteNgay_BD.Culture = new System.Globalization.CultureInfo("en-US");
            this.dteNgay_BD.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_BD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteNgay_BD.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_BD.Location = new System.Drawing.Point(134, 14);
            this.dteNgay_BD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_BD.Mask = "00/00/0000";
            this.dteNgay_BD.Name = "dteNgay_BD";
            this.dteNgay_BD.SelectedText = "";
            this.dteNgay_BD.Size = new System.Drawing.Size(111, 20);
            this.dteNgay_BD.TabIndex = 139;
            // 
            // dteNgay_Kt
            // 
            this.dteNgay_Kt.bAllowEmpty = true;
            this.dteNgay_Kt.bRequire = false;
            this.dteNgay_Kt.bSelectOnFocus = false;
            this.dteNgay_Kt.Culture = new System.Globalization.CultureInfo("en-US");
            this.dteNgay_Kt.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Kt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteNgay_Kt.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Kt.Location = new System.Drawing.Point(134, 37);
            this.dteNgay_Kt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Kt.Mask = "00/00/0000";
            this.dteNgay_Kt.Name = "dteNgay_Kt";
            this.dteNgay_Kt.SelectedText = "";
            this.dteNgay_Kt.Size = new System.Drawing.Size(111, 20);
            this.dteNgay_Kt.TabIndex = 140;
            // 
            // lbGia
            // 
            this.lbGia.AutoEllipsis = true;
            this.lbGia.AutoSize = true;
            this.lbGia.BackColor = System.Drawing.Color.Transparent;
            this.lbGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGia.Location = new System.Drawing.Point(29, 40);
            this.lbGia.Name = "lbGia";
            this.lbGia.Size = new System.Drawing.Size(74, 13);
            this.lbGia.TabIndex = 142;
            this.lbGia.Tag = "Ngay_Kt";
            this.lbGia.Text = "Ngày kết thúc";
            this.lbGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btLoadData
            // 
            this.btLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadData.Image = ((System.Drawing.Image)(resources.GetObject("btLoadData.Image")));
            this.btLoadData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLoadData.Location = new System.Drawing.Point(469, 41);
            this.btLoadData.Name = "btLoadData";
            this.btLoadData.Size = new System.Drawing.Size(116, 32);
            this.btLoadData.TabIndex = 91;
            this.btLoadData.Tag = "";
            this.btLoadData.Text = "Load dữ liệu";
            this.btLoadData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btLoadData.UseVisualStyleBackColor = true;
            // 
            // btThanhtoan
            // 
            this.btThanhtoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThanhtoan.Image = ((System.Drawing.Image)(resources.GetObject("btThanhtoan.Image")));
            this.btThanhtoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThanhtoan.Location = new System.Drawing.Point(601, 39);
            this.btThanhtoan.Name = "btThanhtoan";
            this.btThanhtoan.Size = new System.Drawing.Size(121, 36);
            this.btThanhtoan.TabIndex = 90;
            this.btThanhtoan.Tag = "";
            this.btThanhtoan.Text = "Xử lý";
            this.btThanhtoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btThanhtoan.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvViewHD);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1139, 506);
            this.groupBox2.TabIndex = 89;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dữ liệu đơn hàng";
            // 
            // frmImportDMSOrder_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 585);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmImportDMSOrder_View";
            this.Text = "frmImportHD_View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewHD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private Epoint.Systems.Controls.dgvControl dgvViewHD;
        private System.Windows.Forms.GroupBox groupBox1;
        private Systems.Customizes.btPreview btLoadData;
        private Systems.Customizes.btPreview btThanhtoan;
        private Systems.Controls.lblControl lbNgay_Ap;
        private Systems.Controls.dteDateTime dteNgay_BD;
        private Systems.Controls.dteDateTime dteNgay_Kt;
        private Systems.Controls.lblControl lbGia;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}