namespace Epoint.Modules.HRM
{
    partial class frmCongTrinh_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongTrinh_Edit));
            this.dteNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblNhan_Xet = new Epoint.Systems.Controls.lblControl();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.txtNhan_Xet = new Epoint.Systems.Controls.txtTextBox();
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.txtMa_Sp = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_Sp = new Epoint.Systems.Controls.lblControl();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.lblTen_Sp = new Epoint.Systems.Controls.lblControl();
            this.txtTen_Sp = new Epoint.Systems.Controls.txtTextBox();
            this.SuspendLayout();
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
            this.dteNgay_Begin.Location = new System.Drawing.Point(139, 23);
            this.dteNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteNgay_Begin.Mask = "00/00/0000";
            this.dteNgay_Begin.Name = "dteNgay_Begin";
            this.dteNgay_Begin.Size = new System.Drawing.Size(74, 20);
            this.dteNgay_Begin.TabIndex = 0;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(369, 197);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(171, 34);
            this.btgAccept.TabIndex = 6;
            // 
            // lblNhan_Xet
            // 
            this.lblNhan_Xet.AutoEllipsis = true;
            this.lblNhan_Xet.AutoSize = true;
            this.lblNhan_Xet.BackColor = System.Drawing.Color.Transparent;
            this.lblNhan_Xet.Location = new System.Drawing.Point(40, 141);
            this.lblNhan_Xet.Name = "lblNhan_Xet";
            this.lblNhan_Xet.Size = new System.Drawing.Size(44, 13);
            this.lblNhan_Xet.TabIndex = 123;
            this.lblNhan_Xet.Tag = "Ghi_Chu";
            this.lblNhan_Xet.Text = "Ghi chú";
            this.lblNhan_Xet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgay_Begin
            // 
            this.lblNgay_Begin.AutoEllipsis = true;
            this.lblNgay_Begin.AutoSize = true;
            this.lblNgay_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Begin.Location = new System.Drawing.Point(40, 26);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 125;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNhan_Xet
            // 
            this.txtNhan_Xet.bEnabled = true;
            this.txtNhan_Xet.bIsLookup = false;
            this.txtNhan_Xet.bReadOnly = false;
            this.txtNhan_Xet.bRequire = false;
            this.txtNhan_Xet.KeyFilter = "";
            this.txtNhan_Xet.Location = new System.Drawing.Point(139, 138);
            this.txtNhan_Xet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNhan_Xet.Multiline = true;
            this.txtNhan_Xet.Name = "txtNhan_Xet";
            this.txtNhan_Xet.Size = new System.Drawing.Size(398, 40);
            this.txtNhan_Xet.TabIndex = 5;
            this.txtNhan_Xet.UseAutoFilter = false;
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(40, 49);
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
            this.txtMa_CbNv.Location = new System.Drawing.Point(139, 46);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(164, 20);
            this.txtMa_CbNv.TabIndex = 1;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // txtMa_Sp
            // 
            this.txtMa_Sp.bEnabled = true;
            this.txtMa_Sp.bIsLookup = false;
            this.txtMa_Sp.bReadOnly = false;
            this.txtMa_Sp.bRequire = false;
            this.txtMa_Sp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_Sp.ColumnsView = null;
            this.txtMa_Sp.KeyFilter = "Ma_Sp";
            this.txtMa_Sp.Location = new System.Drawing.Point(139, 92);
            this.txtMa_Sp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Sp.Name = "txtMa_Sp";
            this.txtMa_Sp.Size = new System.Drawing.Size(164, 20);
            this.txtMa_Sp.TabIndex = 3;
            this.txtMa_Sp.UseAutoFilter = true;
            // 
            // lblMa_Sp
            // 
            this.lblMa_Sp.AutoEllipsis = true;
            this.lblMa_Sp.AutoSize = true;
            this.lblMa_Sp.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Sp.Location = new System.Drawing.Point(40, 95);
            this.lblMa_Sp.Name = "lblMa_Sp";
            this.lblMa_Sp.Size = new System.Drawing.Size(71, 13);
            this.lblMa_Sp.TabIndex = 130;
            this.lblMa_Sp.Tag = "Ma_Sp";
            this.lblMa_Sp.Text = "Mã sản phẩm";
            this.lblMa_Sp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.Location = new System.Drawing.Point(40, 73);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(76, 13);
            this.lblControl2.TabIndex = 141;
            this.lblControl2.Tag = "Ten_CbNv";
            this.lblControl2.Text = "Tên nhân viên";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.KeyFilter = "";
            this.txtTen_CbNv.Location = new System.Drawing.Point(139, 69);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(398, 20);
            this.txtTen_CbNv.TabIndex = 2;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // lblTen_Sp
            // 
            this.lblTen_Sp.AutoEllipsis = true;
            this.lblTen_Sp.AutoSize = true;
            this.lblTen_Sp.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_Sp.Location = new System.Drawing.Point(40, 119);
            this.lblTen_Sp.Name = "lblTen_Sp";
            this.lblTen_Sp.Size = new System.Drawing.Size(75, 13);
            this.lblTen_Sp.TabIndex = 143;
            this.lblTen_Sp.Tag = "Ten_Sp";
            this.lblTen_Sp.Text = "Tên sản phẩm";
            this.lblTen_Sp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_Sp
            // 
            this.txtTen_Sp.bEnabled = true;
            this.txtTen_Sp.bIsLookup = false;
            this.txtTen_Sp.bReadOnly = false;
            this.txtTen_Sp.bRequire = false;
            this.txtTen_Sp.KeyFilter = "";
            this.txtTen_Sp.Location = new System.Drawing.Point(139, 115);
            this.txtTen_Sp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_Sp.Name = "txtTen_Sp";
            this.txtTen_Sp.Size = new System.Drawing.Size(398, 20);
            this.txtTen_Sp.TabIndex = 4;
            this.txtTen_Sp.UseAutoFilter = false;
            // 
            // frmCongTrinh_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 251);
            this.Controls.Add(this.lblTen_Sp);
            this.Controls.Add(this.txtTen_Sp);
            this.Controls.Add(this.lblControl2);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.txtMa_Sp);
            this.Controls.Add(this.lblMa_Sp);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.dteNgay_Begin);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.lblNhan_Xet);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.txtNhan_Xet);
            this.Controls.Add(this.lblMa_CbNv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCongTrinh_Edit";
            this.Text = "frmCongTrinh_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Epoint.Systems.Controls.txtDateTime dteNgay_Begin;
		public Epoint.Systems.Customizes.btgAccept btgAccept;
        private Epoint.Systems.Controls.lblControl lblNhan_Xet;
        private Epoint.Systems.Controls.lblControl lblNgay_Begin;
        private Epoint.Systems.Controls.txtTextBox txtNhan_Xet;
        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.txtTextLookup txtMa_Sp;
        private Systems.Controls.lblControl lblMa_Sp;
        private Systems.Controls.lblControl lblControl2;
        private Systems.Controls.txtTextBox txtTen_CbNv;
        private Systems.Controls.lblControl lblTen_Sp;
        private Systems.Controls.txtTextBox txtTen_Sp;

	}
}