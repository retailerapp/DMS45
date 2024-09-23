namespace Epoint.Modules.HRM
{
    partial class frmDaoTaoKH_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDaoTaoKH_Edit));
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.txtMa_Bp = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_Bp = new Epoint.Systems.Controls.lblControl();
            this.lblSo_Nguoi = new Epoint.Systems.Controls.lblControl();
            this.numSo_Nguoi = new Epoint.Systems.Controls.numControl();
            this.txtNgay_Begin = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_Begin = new Epoint.Systems.Controls.lblControl();
            this.txtMa_ViTri = new Epoint.Systems.Controls.txtTextLookup();
            this.lblMa_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lblTen_KhoaHoc = new Epoint.Systems.Controls.lblControl();
            this.lblMa_Kh = new Epoint.Systems.Controls.lblControl();
            this.txtTen_KhoaHoc = new Epoint.Systems.Controls.txtTextBox();
            this.txtMa_KhoaHoc = new Epoint.Systems.Controls.txtTextBox();
            this.txtMuc_Tieu = new Epoint.Systems.Controls.txtTextBox();
            this.lblMuc_Tieu = new Epoint.Systems.Controls.lblControl();
            this.lblKinh_Phi = new Epoint.Systems.Controls.lblControl();
            this.numKinh_Phi = new Epoint.Systems.Controls.numControl();
            this.lbtTen_ViTri = new Epoint.Systems.Controls.lblControl();
            this.lbtTen_Bp = new Epoint.Systems.Controls.lblControl();
            this.txtTruong_Hoc = new Epoint.Systems.Controls.txtTextBox();
            this.lblTruong_Hoc = new Epoint.Systems.Controls.lblControl();
            this.txtHe_Dao_Tao = new Epoint.Systems.Controls.txtTextBox();
            this.lblHe_Dao_Tao = new Epoint.Systems.Controls.lblControl();
            this.txtVan_Bang = new Epoint.Systems.Controls.txtTextBox();
            this.lblVan_Bang = new Epoint.Systems.Controls.lblControl();
            this.txtNgay_End = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_End = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(384, 337);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 34);
            this.btgAccept.TabIndex = 12;
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
            this.txtMa_Bp.Location = new System.Drawing.Point(157, 164);
            this.txtMa_Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_Bp.Name = "txtMa_Bp";
            this.txtMa_Bp.Size = new System.Drawing.Size(110, 20);
            this.txtMa_Bp.TabIndex = 6;
            this.txtMa_Bp.UseAutoFilter = true;
            // 
            // lblMa_Bp
            // 
            this.lblMa_Bp.AutoEllipsis = true;
            this.lblMa_Bp.AutoSize = true;
            this.lblMa_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Bp.Location = new System.Drawing.Point(32, 167);
            this.lblMa_Bp.Name = "lblMa_Bp";
            this.lblMa_Bp.Size = new System.Drawing.Size(64, 13);
            this.lblMa_Bp.TabIndex = 161;
            this.lblMa_Bp.Tag = "Ma_Bp";
            this.lblMa_Bp.Text = "Mã bộ phận";
            this.lblMa_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSo_Nguoi
            // 
            this.lblSo_Nguoi.AutoEllipsis = true;
            this.lblSo_Nguoi.AutoSize = true;
            this.lblSo_Nguoi.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Nguoi.Location = new System.Drawing.Point(32, 75);
            this.lblSo_Nguoi.Name = "lblSo_Nguoi";
            this.lblSo_Nguoi.Size = new System.Drawing.Size(49, 13);
            this.lblSo_Nguoi.TabIndex = 159;
            this.lblSo_Nguoi.Tag = "So_Nguoi";
            this.lblSo_Nguoi.Text = "Số người";
            this.lblSo_Nguoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numSo_Nguoi
            // 
            this.numSo_Nguoi.bEnabled = true;
            this.numSo_Nguoi.bFormat = true;
            this.numSo_Nguoi.bIsLookup = false;
            this.numSo_Nguoi.bReadOnly = false;
            this.numSo_Nguoi.bRequire = false;
            this.numSo_Nguoi.KeyFilter = "";
            this.numSo_Nguoi.Location = new System.Drawing.Point(157, 72);
            this.numSo_Nguoi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numSo_Nguoi.Name = "numSo_Nguoi";
            this.numSo_Nguoi.Scale = 0;
            this.numSo_Nguoi.Size = new System.Drawing.Size(110, 20);
            this.numSo_Nguoi.TabIndex = 2;
            this.numSo_Nguoi.Text = "0";
            this.numSo_Nguoi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSo_Nguoi.UseAutoFilter = false;
            this.numSo_Nguoi.Value = 0D;
            // 
            // txtNgay_Begin
            // 
            this.txtNgay_Begin.bAllowEmpty = true;
            this.txtNgay_Begin.bRequire = false;
            this.txtNgay_Begin.bSelectOnFocus = false;
            this.txtNgay_Begin.bShowDateTimePicker = true;
            this.txtNgay_Begin.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.txtNgay_Begin.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_Begin.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_Begin.Location = new System.Drawing.Point(157, 210);
            this.txtNgay_Begin.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtNgay_Begin.Mask = "00/00/0000";
            this.txtNgay_Begin.Name = "txtNgay_Begin";
            this.txtNgay_Begin.Size = new System.Drawing.Size(110, 20);
            this.txtNgay_Begin.TabIndex = 8;
            this.txtNgay_Begin.Tag = "Ngay_End";
            // 
            // lblNgay_Begin
            // 
            this.lblNgay_Begin.AutoEllipsis = true;
            this.lblNgay_Begin.AutoSize = true;
            this.lblNgay_Begin.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Begin.Location = new System.Drawing.Point(32, 213);
            this.lblNgay_Begin.Name = "lblNgay_Begin";
            this.lblNgay_Begin.Size = new System.Drawing.Size(72, 13);
            this.lblNgay_Begin.TabIndex = 156;
            this.lblNgay_Begin.Tag = "Ngay_Begin";
            this.lblNgay_Begin.Text = "Ngày bắt đầu";
            this.lblNgay_Begin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txtMa_ViTri.Location = new System.Drawing.Point(157, 187);
            this.txtMa_ViTri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_ViTri.Name = "txtMa_ViTri";
            this.txtMa_ViTri.Size = new System.Drawing.Size(110, 20);
            this.txtMa_ViTri.TabIndex = 7;
            this.txtMa_ViTri.UseAutoFilter = true;
            // 
            // lblMa_ViTri
            // 
            this.lblMa_ViTri.AutoEllipsis = true;
            this.lblMa_ViTri.AutoSize = true;
            this.lblMa_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_ViTri.Location = new System.Drawing.Point(32, 190);
            this.lblMa_ViTri.Name = "lblMa_ViTri";
            this.lblMa_ViTri.Size = new System.Drawing.Size(46, 13);
            this.lblMa_ViTri.TabIndex = 166;
            this.lblMa_ViTri.Tag = "Ma_ViTri";
            this.lblMa_ViTri.Text = "Mã vị trí";
            this.lblMa_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTen_KhoaHoc
            // 
            this.lblTen_KhoaHoc.AutoEllipsis = true;
            this.lblTen_KhoaHoc.AutoSize = true;
            this.lblTen_KhoaHoc.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_KhoaHoc.Location = new System.Drawing.Point(32, 52);
            this.lblTen_KhoaHoc.Name = "lblTen_KhoaHoc";
            this.lblTen_KhoaHoc.Size = new System.Drawing.Size(74, 13);
            this.lblTen_KhoaHoc.TabIndex = 169;
            this.lblTen_KhoaHoc.Tag = "Ten_KhoaHoc";
            this.lblTen_KhoaHoc.Text = "Tên khóa học";
            this.lblTen_KhoaHoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMa_Kh
            // 
            this.lblMa_Kh.AutoEllipsis = true;
            this.lblMa_Kh.AutoSize = true;
            this.lblMa_Kh.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Kh.Location = new System.Drawing.Point(32, 29);
            this.lblMa_Kh.Name = "lblMa_Kh";
            this.lblMa_Kh.Size = new System.Drawing.Size(70, 13);
            this.lblMa_Kh.TabIndex = 173;
            this.lblMa_Kh.Tag = "Ma_KhoaHoc";
            this.lblMa_Kh.Text = "Mã khóa học";
            this.lblMa_Kh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_KhoaHoc
            // 
            this.txtTen_KhoaHoc.bEnabled = true;
            this.txtTen_KhoaHoc.bIsLookup = false;
            this.txtTen_KhoaHoc.bReadOnly = false;
            this.txtTen_KhoaHoc.bRequire = false;
            this.txtTen_KhoaHoc.KeyFilter = "";
            this.txtTen_KhoaHoc.Location = new System.Drawing.Point(157, 49);
            this.txtTen_KhoaHoc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_KhoaHoc.MaxLength = 200;
            this.txtTen_KhoaHoc.Name = "txtTen_KhoaHoc";
            this.txtTen_KhoaHoc.Size = new System.Drawing.Size(397, 20);
            this.txtTen_KhoaHoc.TabIndex = 1;
            this.txtTen_KhoaHoc.UseAutoFilter = false;
            // 
            // txtMa_KhoaHoc
            // 
            this.txtMa_KhoaHoc.bEnabled = true;
            this.txtMa_KhoaHoc.bIsLookup = false;
            this.txtMa_KhoaHoc.bReadOnly = false;
            this.txtMa_KhoaHoc.bRequire = false;
            this.txtMa_KhoaHoc.KeyFilter = "";
            this.txtMa_KhoaHoc.Location = new System.Drawing.Point(157, 26);
            this.txtMa_KhoaHoc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_KhoaHoc.MaxLength = 20;
            this.txtMa_KhoaHoc.Name = "txtMa_KhoaHoc";
            this.txtMa_KhoaHoc.Size = new System.Drawing.Size(110, 20);
            this.txtMa_KhoaHoc.TabIndex = 0;
            this.txtMa_KhoaHoc.UseAutoFilter = false;
            // 
            // txtMuc_Tieu
            // 
            this.txtMuc_Tieu.bEnabled = true;
            this.txtMuc_Tieu.bIsLookup = false;
            this.txtMuc_Tieu.bReadOnly = false;
            this.txtMuc_Tieu.bRequire = false;
            this.txtMuc_Tieu.KeyFilter = "";
            this.txtMuc_Tieu.Location = new System.Drawing.Point(157, 279);
            this.txtMuc_Tieu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMuc_Tieu.MaxLength = 500;
            this.txtMuc_Tieu.Multiline = true;
            this.txtMuc_Tieu.Name = "txtMuc_Tieu";
            this.txtMuc_Tieu.Size = new System.Drawing.Size(397, 37);
            this.txtMuc_Tieu.TabIndex = 11;
            this.txtMuc_Tieu.UseAutoFilter = false;
            // 
            // lblMuc_Tieu
            // 
            this.lblMuc_Tieu.AutoEllipsis = true;
            this.lblMuc_Tieu.AutoSize = true;
            this.lblMuc_Tieu.BackColor = System.Drawing.Color.Transparent;
            this.lblMuc_Tieu.Location = new System.Drawing.Point(32, 282);
            this.lblMuc_Tieu.Name = "lblMuc_Tieu";
            this.lblMuc_Tieu.Size = new System.Drawing.Size(48, 13);
            this.lblMuc_Tieu.TabIndex = 181;
            this.lblMuc_Tieu.Tag = "Muc_Tieu";
            this.lblMuc_Tieu.Text = "Mục tiêu";
            this.lblMuc_Tieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKinh_Phi
            // 
            this.lblKinh_Phi.AutoEllipsis = true;
            this.lblKinh_Phi.AutoSize = true;
            this.lblKinh_Phi.BackColor = System.Drawing.Color.Transparent;
            this.lblKinh_Phi.Location = new System.Drawing.Point(32, 259);
            this.lblKinh_Phi.Name = "lblKinh_Phi";
            this.lblKinh_Phi.Size = new System.Drawing.Size(47, 13);
            this.lblKinh_Phi.TabIndex = 191;
            this.lblKinh_Phi.Tag = "Kinh_Phi";
            this.lblKinh_Phi.Text = "Kinh phí";
            this.lblKinh_Phi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numKinh_Phi
            // 
            this.numKinh_Phi.bEnabled = true;
            this.numKinh_Phi.bFormat = true;
            this.numKinh_Phi.bIsLookup = false;
            this.numKinh_Phi.bReadOnly = false;
            this.numKinh_Phi.bRequire = false;
            this.numKinh_Phi.KeyFilter = "";
            this.numKinh_Phi.Location = new System.Drawing.Point(157, 256);
            this.numKinh_Phi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numKinh_Phi.Name = "numKinh_Phi";
            this.numKinh_Phi.Scale = 0;
            this.numKinh_Phi.Size = new System.Drawing.Size(110, 20);
            this.numKinh_Phi.TabIndex = 10;
            this.numKinh_Phi.Text = "0";
            this.numKinh_Phi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numKinh_Phi.UseAutoFilter = false;
            this.numKinh_Phi.Value = 0D;
            // 
            // lbtTen_ViTri
            // 
            this.lbtTen_ViTri.AutoEllipsis = true;
            this.lbtTen_ViTri.AutoSize = true;
            this.lbtTen_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_ViTri.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_ViTri.Location = new System.Drawing.Point(272, 191);
            this.lbtTen_ViTri.Name = "lbtTen_ViTri";
            this.lbtTen_ViTri.Size = new System.Drawing.Size(50, 13);
            this.lbtTen_ViTri.TabIndex = 195;
            this.lbtTen_ViTri.Text = "Tên vị trí";
            this.lbtTen_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbtTen_Bp
            // 
            this.lbtTen_Bp.AutoEllipsis = true;
            this.lbtTen_Bp.AutoSize = true;
            this.lbtTen_Bp.BackColor = System.Drawing.Color.Transparent;
            this.lbtTen_Bp.ForeColor = System.Drawing.Color.Blue;
            this.lbtTen_Bp.Location = new System.Drawing.Point(272, 168);
            this.lbtTen_Bp.Name = "lbtTen_Bp";
            this.lbtTen_Bp.Size = new System.Drawing.Size(71, 13);
            this.lbtTen_Bp.TabIndex = 194;
            this.lbtTen_Bp.Text = "Tên bộ phận ";
            this.lbtTen_Bp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTruong_Hoc
            // 
            this.txtTruong_Hoc.bEnabled = true;
            this.txtTruong_Hoc.bIsLookup = false;
            this.txtTruong_Hoc.bReadOnly = false;
            this.txtTruong_Hoc.bRequire = false;
            this.txtTruong_Hoc.KeyFilter = "";
            this.txtTruong_Hoc.Location = new System.Drawing.Point(157, 95);
            this.txtTruong_Hoc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTruong_Hoc.MaxLength = 200;
            this.txtTruong_Hoc.Name = "txtTruong_Hoc";
            this.txtTruong_Hoc.Size = new System.Drawing.Size(397, 20);
            this.txtTruong_Hoc.TabIndex = 3;
            this.txtTruong_Hoc.UseAutoFilter = false;
            // 
            // lblTruong_Hoc
            // 
            this.lblTruong_Hoc.AutoEllipsis = true;
            this.lblTruong_Hoc.AutoSize = true;
            this.lblTruong_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.lblTruong_Hoc.Location = new System.Drawing.Point(32, 98);
            this.lblTruong_Hoc.Name = "lblTruong_Hoc";
            this.lblTruong_Hoc.Size = new System.Drawing.Size(62, 13);
            this.lblTruong_Hoc.TabIndex = 197;
            this.lblTruong_Hoc.Tag = "Truong_Hoc";
            this.lblTruong_Hoc.Text = "Trường học";
            this.lblTruong_Hoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHe_Dao_Tao
            // 
            this.txtHe_Dao_Tao.bEnabled = true;
            this.txtHe_Dao_Tao.bIsLookup = false;
            this.txtHe_Dao_Tao.bReadOnly = false;
            this.txtHe_Dao_Tao.bRequire = false;
            this.txtHe_Dao_Tao.KeyFilter = "";
            this.txtHe_Dao_Tao.Location = new System.Drawing.Point(157, 118);
            this.txtHe_Dao_Tao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtHe_Dao_Tao.MaxLength = 200;
            this.txtHe_Dao_Tao.Name = "txtHe_Dao_Tao";
            this.txtHe_Dao_Tao.Size = new System.Drawing.Size(397, 20);
            this.txtHe_Dao_Tao.TabIndex = 4;
            this.txtHe_Dao_Tao.UseAutoFilter = false;
            // 
            // lblHe_Dao_Tao
            // 
            this.lblHe_Dao_Tao.AutoEllipsis = true;
            this.lblHe_Dao_Tao.AutoSize = true;
            this.lblHe_Dao_Tao.BackColor = System.Drawing.Color.Transparent;
            this.lblHe_Dao_Tao.Location = new System.Drawing.Point(32, 121);
            this.lblHe_Dao_Tao.Name = "lblHe_Dao_Tao";
            this.lblHe_Dao_Tao.Size = new System.Drawing.Size(61, 13);
            this.lblHe_Dao_Tao.TabIndex = 199;
            this.lblHe_Dao_Tao.Tag = "He_Dao_Tao";
            this.lblHe_Dao_Tao.Text = "Hệ đào tạo";
            this.lblHe_Dao_Tao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVan_Bang
            // 
            this.txtVan_Bang.bEnabled = true;
            this.txtVan_Bang.bIsLookup = false;
            this.txtVan_Bang.bReadOnly = false;
            this.txtVan_Bang.bRequire = false;
            this.txtVan_Bang.KeyFilter = "";
            this.txtVan_Bang.Location = new System.Drawing.Point(157, 141);
            this.txtVan_Bang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtVan_Bang.MaxLength = 200;
            this.txtVan_Bang.Name = "txtVan_Bang";
            this.txtVan_Bang.Size = new System.Drawing.Size(397, 20);
            this.txtVan_Bang.TabIndex = 5;
            this.txtVan_Bang.UseAutoFilter = false;
            // 
            // lblVan_Bang
            // 
            this.lblVan_Bang.AutoEllipsis = true;
            this.lblVan_Bang.AutoSize = true;
            this.lblVan_Bang.BackColor = System.Drawing.Color.Transparent;
            this.lblVan_Bang.Location = new System.Drawing.Point(32, 144);
            this.lblVan_Bang.Name = "lblVan_Bang";
            this.lblVan_Bang.Size = new System.Drawing.Size(53, 13);
            this.lblVan_Bang.TabIndex = 201;
            this.lblVan_Bang.Tag = "Van_Bang";
            this.lblVan_Bang.Text = "Văn bằng";
            this.lblVan_Bang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNgay_End
            // 
            this.txtNgay_End.bAllowEmpty = true;
            this.txtNgay_End.bRequire = false;
            this.txtNgay_End.bSelectOnFocus = false;
            this.txtNgay_End.bShowDateTimePicker = true;
            this.txtNgay_End.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.txtNgay_End.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.txtNgay_End.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtNgay_End.Location = new System.Drawing.Point(157, 233);
            this.txtNgay_End.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtNgay_End.Mask = "00/00/0000";
            this.txtNgay_End.Name = "txtNgay_End";
            this.txtNgay_End.Size = new System.Drawing.Size(110, 20);
            this.txtNgay_End.TabIndex = 9;
            this.txtNgay_End.Tag = "Ngay_End";
            // 
            // lblNgay_End
            // 
            this.lblNgay_End.AutoEllipsis = true;
            this.lblNgay_End.AutoSize = true;
            this.lblNgay_End.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_End.Location = new System.Drawing.Point(32, 236);
            this.lblNgay_End.Name = "lblNgay_End";
            this.lblNgay_End.Size = new System.Drawing.Size(74, 13);
            this.lblNgay_End.TabIndex = 203;
            this.lblNgay_End.Tag = "Ngay_End";
            this.lblNgay_End.Text = "Ngày kết thúc";
            this.lblNgay_End.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDaoTaoKH_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 388);
            this.Controls.Add(this.lblNgay_End);
            this.Controls.Add(this.txtNgay_End);
            this.Controls.Add(this.txtVan_Bang);
            this.Controls.Add(this.lblVan_Bang);
            this.Controls.Add(this.txtHe_Dao_Tao);
            this.Controls.Add(this.lblHe_Dao_Tao);
            this.Controls.Add(this.txtTruong_Hoc);
            this.Controls.Add(this.lblTruong_Hoc);
            this.Controls.Add(this.lbtTen_ViTri);
            this.Controls.Add(this.lbtTen_Bp);
            this.Controls.Add(this.lblKinh_Phi);
            this.Controls.Add(this.numKinh_Phi);
            this.Controls.Add(this.txtMuc_Tieu);
            this.Controls.Add(this.lblMuc_Tieu);
            this.Controls.Add(this.txtMa_KhoaHoc);
            this.Controls.Add(this.txtTen_KhoaHoc);
            this.Controls.Add(this.lblMa_Kh);
            this.Controls.Add(this.lblTen_KhoaHoc);
            this.Controls.Add(this.txtMa_ViTri);
            this.Controls.Add(this.lblMa_ViTri);
            this.Controls.Add(this.txtMa_Bp);
            this.Controls.Add(this.lblMa_Bp);
            this.Controls.Add(this.lblSo_Nguoi);
            this.Controls.Add(this.numSo_Nguoi);
            this.Controls.Add(this.txtNgay_Begin);
            this.Controls.Add(this.lblNgay_Begin);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDaoTaoKH_Edit";
            this.Text = "frmDaoTaoKH_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Epoint.Systems.Customizes.btgAccept btgAccept;
        private Systems.Controls.txtTextLookup txtMa_Bp;
        private Systems.Controls.lblControl lblMa_Bp;
        private Systems.Controls.lblControl lblSo_Nguoi;
        private Systems.Controls.numControl numSo_Nguoi;
        private Systems.Controls.txtDateTime txtNgay_Begin;
        private Systems.Controls.lblControl lblNgay_Begin;
        private Systems.Controls.txtTextLookup txtMa_ViTri;
        private Systems.Controls.lblControl lblMa_ViTri;
        private Systems.Controls.lblControl lblTen_KhoaHoc;
        private Systems.Controls.lblControl lblMa_Kh;
        private Systems.Controls.txtTextBox txtTen_KhoaHoc;
        private Systems.Controls.txtTextBox txtMa_KhoaHoc;
        private Systems.Controls.txtTextBox txtMuc_Tieu;
        private Systems.Controls.lblControl lblMuc_Tieu;
        private Systems.Controls.lblControl lblKinh_Phi;
        private Systems.Controls.numControl numKinh_Phi;
        private Systems.Controls.lblControl lbtTen_ViTri;
        private Systems.Controls.lblControl lbtTen_Bp;
        private Systems.Controls.txtTextBox txtTruong_Hoc;
        private Systems.Controls.lblControl lblTruong_Hoc;
        private Systems.Controls.txtTextBox txtHe_Dao_Tao;
        private Systems.Controls.lblControl lblHe_Dao_Tao;
        private Systems.Controls.txtTextBox txtVan_Bang;
        private Systems.Controls.lblControl lblVan_Bang;
        private Systems.Controls.txtDateTime txtNgay_End;
        private Systems.Controls.lblControl lblNgay_End;

	}
}