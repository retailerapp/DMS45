namespace Epoint.Modules.HRM
{
	partial class frmPhepNam_Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhepNam_Edit));
            this.lblMa_CbNv = new Epoint.Systems.Controls.lblControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblSo_Luong = new Epoint.Systems.Controls.lblControl();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.txtDvt = new Epoint.Systems.Controls.txtTextBox();
            this.lblDvt = new Epoint.Systems.Controls.lblControl();
            this.numSo_Luong = new Epoint.Systems.Controls.numControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextLookup();
            this.lblTen_CbNv = new Epoint.Systems.Controls.lblControl();
            this.txtNam = new Epoint.Systems.Controls.txtTextBox();
            this.lblNam = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // lblMa_CbNv
            // 
            this.lblMa_CbNv.AutoEllipsis = true;
            this.lblMa_CbNv.AutoSize = true;
            this.lblMa_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_CbNv.Location = new System.Drawing.Point(41, 45);
            this.lblMa_CbNv.Name = "lblMa_CbNv";
            this.lblMa_CbNv.Size = new System.Drawing.Size(72, 13);
            this.lblMa_CbNv.TabIndex = 70;
            this.lblMa_CbNv.Tag = "Ma_CbNv";
            this.lblMa_CbNv.Text = "Mã nhân viên";
            this.lblMa_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(332, 163);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 5;
            // 
            // lblSo_Luong
            // 
            this.lblSo_Luong.AutoEllipsis = true;
            this.lblSo_Luong.AutoSize = true;
            this.lblSo_Luong.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Luong.Location = new System.Drawing.Point(41, 114);
            this.lblSo_Luong.Name = "lblSo_Luong";
            this.lblSo_Luong.Size = new System.Drawing.Size(49, 13);
            this.lblSo_Luong.TabIndex = 70;
            this.lblSo_Luong.Tag = "So_Luong";
            this.lblSo_Luong.Text = "Số lượng";
            this.lblSo_Luong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMa_CbNv
            // 
            this.txtMa_CbNv.bEnabled = true;
            this.txtMa_CbNv.bIsLookup = false;
            this.txtMa_CbNv.bReadOnly = false;
            this.txtMa_CbNv.bRequire = false;
            this.txtMa_CbNv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_CbNv.ColumnsView = null;
            this.txtMa_CbNv.KeyFilter = "Ma_Tn";
            this.txtMa_CbNv.Location = new System.Drawing.Point(134, 42);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(96, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = true;
            // 
            // txtDvt
            // 
            this.txtDvt.bEnabled = true;
            this.txtDvt.bIsLookup = false;
            this.txtDvt.bReadOnly = false;
            this.txtDvt.bRequire = false;
            this.txtDvt.KeyFilter = "";
            this.txtDvt.Location = new System.Drawing.Point(134, 134);
            this.txtDvt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtDvt.Name = "txtDvt";
            this.txtDvt.Size = new System.Drawing.Size(139, 20);
            this.txtDvt.TabIndex = 4;
            this.txtDvt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDvt.UseAutoFilter = false;
            // 
            // lblDvt
            // 
            this.lblDvt.AutoEllipsis = true;
            this.lblDvt.AutoSize = true;
            this.lblDvt.BackColor = System.Drawing.Color.Transparent;
            this.lblDvt.Location = new System.Drawing.Point(41, 136);
            this.lblDvt.Name = "lblDvt";
            this.lblDvt.Size = new System.Drawing.Size(24, 13);
            this.lblDvt.TabIndex = 139;
            this.lblDvt.Tag = "Dvt";
            this.lblDvt.Text = "Đvt";
            this.lblDvt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numSo_Luong
            // 
            this.numSo_Luong.bEnabled = true;
            this.numSo_Luong.bFormat = true;
            this.numSo_Luong.bIsLookup = false;
            this.numSo_Luong.bReadOnly = false;
            this.numSo_Luong.bRequire = false;
            this.numSo_Luong.KeyFilter = "";
            this.numSo_Luong.Location = new System.Drawing.Point(134, 111);
            this.numSo_Luong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numSo_Luong.Name = "numSo_Luong";
            this.numSo_Luong.Scale = 0;
            this.numSo_Luong.Size = new System.Drawing.Size(139, 20);
            this.numSo_Luong.TabIndex = 3;
            this.numSo_Luong.Text = "0";
            this.numSo_Luong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSo_Luong.UseAutoFilter = false;
            this.numSo_Luong.Value = 0D;
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTen_CbNv.ColumnsView = null;
            this.txtTen_CbNv.KeyFilter = "Ma_Tn";
            this.txtTen_CbNv.Location = new System.Drawing.Point(134, 65);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(366, 20);
            this.txtTen_CbNv.TabIndex = 1;
            this.txtTen_CbNv.UseAutoFilter = true;
            // 
            // lblTen_CbNv
            // 
            this.lblTen_CbNv.AutoEllipsis = true;
            this.lblTen_CbNv.AutoSize = true;
            this.lblTen_CbNv.BackColor = System.Drawing.Color.Transparent;
            this.lblTen_CbNv.Location = new System.Drawing.Point(41, 68);
            this.lblTen_CbNv.Name = "lblTen_CbNv";
            this.lblTen_CbNv.Size = new System.Drawing.Size(76, 13);
            this.lblTen_CbNv.TabIndex = 141;
            this.lblTen_CbNv.Tag = "Ten_CbNv";
            this.lblTen_CbNv.Text = "Tên nhân viên";
            this.lblTen_CbNv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNam
            // 
            this.txtNam.bEnabled = true;
            this.txtNam.bIsLookup = false;
            this.txtNam.bReadOnly = false;
            this.txtNam.bRequire = false;
            this.txtNam.KeyFilter = "";
            this.txtNam.Location = new System.Drawing.Point(134, 88);
            this.txtNam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(139, 20);
            this.txtNam.TabIndex = 2;
            this.txtNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNam.UseAutoFilter = false;
            // 
            // lblNam
            // 
            this.lblNam.AutoEllipsis = true;
            this.lblNam.AutoSize = true;
            this.lblNam.BackColor = System.Drawing.Color.Transparent;
            this.lblNam.Location = new System.Drawing.Point(41, 91);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(29, 13);
            this.lblNam.TabIndex = 145;
            this.lblNam.Tag = "";
            this.lblNam.Text = "Năm";
            this.lblNam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPhepNam_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 212);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.lblTen_CbNv);
            this.Controls.Add(this.txtTen_CbNv);
            this.Controls.Add(this.numSo_Luong);
            this.Controls.Add(this.txtDvt);
            this.Controls.Add(this.lblDvt);
            this.Controls.Add(this.txtMa_CbNv);
            this.Controls.Add(this.lblSo_Luong);
            this.Controls.Add(this.lblMa_CbNv);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPhepNam_Edit";
            this.Text = "frmDmTn_Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private Epoint.Systems.Controls.lblControl lblMa_CbNv;
        private Epoint.Systems.Customizes.btgAccept btgAccept;
        private Epoint.Systems.Controls.lblControl lblSo_Luong;
        private Systems.Controls.txtTextLookup txtMa_CbNv;
        private Systems.Controls.txtTextBox txtDvt;
        private Systems.Controls.lblControl lblDvt;
        private Systems.Controls.numControl numSo_Luong;
        private Systems.Controls.txtTextLookup txtTen_CbNv;
        private Systems.Controls.lblControl lblTen_CbNv;
        private Systems.Controls.txtTextBox txtNam;
        private Systems.Controls.lblControl lblNam;
	}
}