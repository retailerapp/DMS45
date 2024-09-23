namespace Epoint.Modules.Salary
{
	partial class frmBangLuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBangLuong));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblThang = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cboMa_Bp = new Epoint.Systems.Controls.cboMultiControl();
            this.lblMa_Bp = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Bp = new Epoint.Systems.Controls.lbtControl();
            this.cboThang = new Epoint.Systems.Controls.cboControl();
            this.btCreateSalary = new Epoint.Systems.Controls.btControl();
            this.btCalcSalary = new Epoint.Systems.Controls.btControl();
            this.btPostedSalary = new Epoint.Systems.Controls.btControl();
            this.btImport = new Epoint.Systems.Controls.btControl();
            this.btDeleteSalary = new Epoint.Systems.Controls.btControl();
            this.btnSendMail = new Epoint.Systems.Controls.btControl();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "posted");
            this.imageList1.Images.SetKeyName(1, "delete");
            this.imageList1.Images.SetKeyName(2, "calc");
            this.imageList1.Images.SetKeyName(3, "add");
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.Blue;
            this.lblThang.Location = new System.Drawing.Point(32, 11);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(43, 13);
            this.lblThang.TabIndex = 4;
            this.lblThang.Tag = "Thang";
            this.lblThang.Text = "Tháng";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(3, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 441);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(18, 486);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 71);
            this.panel2.TabIndex = 3;
            this.panel2.TabStop = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.ForeColor = System.Drawing.Color.Blue;
            this.radioButton3.Location = new System.Drawing.Point(3, 50);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(212, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Tag = "TAB3";
            this.radioButton3.Text = "&3. Tổng hợp và thanh toán lương";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.Color.Blue;
            this.radioButton2.Location = new System.Drawing.Point(3, 27);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(223, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Tag = "TAB2";
            this.radioButton2.Text = "&2. Bảo hiểm và các khoản giảm trừ";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.ForeColor = System.Drawing.Color.Blue;
            this.radioButton1.Location = new System.Drawing.Point(3, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(221, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "Tab1";
            this.radioButton1.Text = "&1. Lương và các khoản tăng lương";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cboMa_Bp
            // 
            this.cboMa_Bp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboMa_Bp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboMa_Bp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboMa_Bp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMa_Bp.InitValue = null;
            this.cboMa_Bp.Location = new System.Drawing.Point(243, 8);
            this.cboMa_Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.cboMa_Bp.MaxLength = 20;
            this.cboMa_Bp.Name = "cboMa_Bp";
            this.cboMa_Bp.Size = new System.Drawing.Size(116, 21);
            this.cboMa_Bp.strValueList = null;
            this.cboMa_Bp.TabIndex = 1;
            this.cboMa_Bp.UseAutoComplete = false;
            this.cboMa_Bp.UseBindingValue = false;
            // 
            // lblMa_Bp
            // 
            this.lblMa_Bp.AutoEllipsis = true;
            this.lblMa_Bp.AutoSize = true;
            this.lblMa_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Bp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMa_Bp.ForeColor = System.Drawing.Color.Blue;
            this.lblMa_Bp.Location = new System.Drawing.Point(164, 11);
            this.lblMa_Bp.Name = "lblMa_Bp";
            this.lblMa_Bp.Size = new System.Drawing.Size(74, 13);
            this.lblMa_Bp.TabIndex = 65;
            this.lblMa_Bp.Tag = "Ma_Bp";
            this.lblMa_Bp.Text = "Mã bộ phận";
            this.lblMa_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Bp
            // 
            this.lbtTen_Bp.AutoEllipsis = true;
            this.lbtTen_Bp.AutoSize = true;
            this.lbtTen_Bp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtTen_Bp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbtTen_Bp.Location = new System.Drawing.Point(364, 11);
            this.lbtTen_Bp.Name = "lbtTen_Bp";
            this.lbtTen_Bp.Size = new System.Drawing.Size(80, 13);
            this.lbtTen_Bp.TabIndex = 65;
            this.lbtTen_Bp.Tag = "";
            this.lbtTen_Bp.Text = "Tên Bộ phận";
            this.lbtTen_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboThang
            // 
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.InitValue = null;
            this.cboThang.Location = new System.Drawing.Point(80, 8);
            this.cboThang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(51, 21);
            this.cboThang.strValueList = null;
            this.cboThang.TabIndex = 0;
            this.cboThang.UseAutoComplete = false;
            this.cboThang.UseBindingValue = false;
            // 
            // btCreateSalary
            // 
            this.btCreateSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateSalary.ForeColor = System.Drawing.Color.Blue;
            this.btCreateSalary.Image = ((System.Drawing.Image)(resources.GetObject("btCreateSalary.Image")));
            this.btCreateSalary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCreateSalary.Location = new System.Drawing.Point(367, 491);
            this.btCreateSalary.Name = "btCreateSalary";
            this.btCreateSalary.Size = new System.Drawing.Size(135, 30);
            this.btCreateSalary.TabIndex = 4;
            this.btCreateSalary.Tag = "BangLuong_Create";
            this.btCreateSalary.Text = "&Tạo bảng lương";
            this.btCreateSalary.UseVisualStyleBackColor = true;
            // 
            // btCalcSalary
            // 
            this.btCalcSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCalcSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCalcSalary.ForeColor = System.Drawing.Color.Blue;
            this.btCalcSalary.Image = ((System.Drawing.Image)(resources.GetObject("btCalcSalary.Image")));
            this.btCalcSalary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCalcSalary.Location = new System.Drawing.Point(508, 491);
            this.btCalcSalary.Name = "btCalcSalary";
            this.btCalcSalary.Size = new System.Drawing.Size(135, 30);
            this.btCalcSalary.TabIndex = 5;
            this.btCalcSalary.Tag = "TinhLuong";
            this.btCalcSalary.Text = "Tính &lương";
            this.btCalcSalary.UseVisualStyleBackColor = true;
            // 
            // btPostedSalary
            // 
            this.btPostedSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPostedSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPostedSalary.ForeColor = System.Drawing.Color.Blue;
            this.btPostedSalary.Image = ((System.Drawing.Image)(resources.GetObject("btPostedSalary.Image")));
            this.btPostedSalary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btPostedSalary.Location = new System.Drawing.Point(367, 526);
            this.btPostedSalary.Name = "btPostedSalary";
            this.btPostedSalary.Size = new System.Drawing.Size(135, 30);
            this.btPostedSalary.TabIndex = 6;
            this.btPostedSalary.Tag = "HachToan_Luong";
            this.btPostedSalary.Text = "Hạch toán lương";
            this.btPostedSalary.UseVisualStyleBackColor = true;
            // 
            // btImport
            // 
            this.btImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImport.ForeColor = System.Drawing.Color.Blue;
            this.btImport.Image = ((System.Drawing.Image)(resources.GetObject("btImport.Image")));
            this.btImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btImport.Location = new System.Drawing.Point(507, 526);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(136, 30);
            this.btImport.TabIndex = 7;
            this.btImport.Tag = "Import";
            this.btImport.Text = "&Lấy dữ liệu";
            this.btImport.UseVisualStyleBackColor = true;
            // 
            // btDeleteSalary
            // 
            this.btDeleteSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDeleteSalary.ForeColor = System.Drawing.Color.Blue;
            this.btDeleteSalary.Image = ((System.Drawing.Image)(resources.GetObject("btDeleteSalary.Image")));
            this.btDeleteSalary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDeleteSalary.Location = new System.Drawing.Point(649, 490);
            this.btDeleteSalary.Name = "btDeleteSalary";
            this.btDeleteSalary.Size = new System.Drawing.Size(138, 30);
            this.btDeleteSalary.TabIndex = 8;
            this.btDeleteSalary.Tag = "BangLuong_Delete";
            this.btDeleteSalary.Text = "&Xóa bảng lương";
            this.btDeleteSalary.UseVisualStyleBackColor = true;
            // 
            // btnSendMail
            // 
            this.btnSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMail.ForeColor = System.Drawing.Color.Blue;
            this.btnSendMail.Image = ((System.Drawing.Image)(resources.GetObject("btnSendMail.Image")));
            this.btnSendMail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendMail.Location = new System.Drawing.Point(649, 526);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(135, 30);
            this.btnSendMail.TabIndex = 6;
            this.btnSendMail.Tag = "Send_Mail";
            this.btnSendMail.Text = "Gửi Email";
            this.btnSendMail.UseVisualStyleBackColor = true;
            // 
            // frmBangLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.btDeleteSalary);
            this.Controls.Add(this.btImport);
            this.Controls.Add(this.btnSendMail);
            this.Controls.Add(this.btPostedSalary);
            this.Controls.Add(this.btCalcSalary);
            this.Controls.Add(this.btCreateSalary);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.cboMa_Bp);
            this.Controls.Add(this.lbtTen_Bp);
            this.Controls.Add(this.lblMa_Bp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblThang);
            this.Name = "frmBangLuong";
            this.Tag = "frmBangLuong";
            this.Text = "frmBangLuong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label lblThang;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
		private Epoint.Systems.Controls.cboMultiControl cboMa_Bp;
		private Epoint.Systems.Controls.lblControl lblMa_Bp;
        private Epoint.Systems.Controls.lbtControl lbtTen_Bp;
		public Epoint.Systems.Controls.cboControl cboThang;
        private System.Windows.Forms.ImageList imageList1;
        private Systems.Controls.btControl btCreateSalary;
        private Systems.Controls.btControl btCalcSalary;
        private Systems.Controls.btControl btPostedSalary;
        private Systems.Controls.btControl btImport;
        private Systems.Controls.btControl btDeleteSalary;
        private Systems.Controls.btControl btnSendMail;
	}
}