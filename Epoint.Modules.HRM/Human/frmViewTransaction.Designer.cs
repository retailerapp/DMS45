namespace Epoint.Modules.HRM
{
    partial class frmViewTransaction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btLoadData = new System.Windows.Forms.Button();
            this.pageNP = new System.Windows.Forms.TabPage();
            this.dgvNP = new Epoint.Systems.Controls.dgvControl();
            this.pageViewdata = new System.Windows.Forms.TabPage();
            this.dgvChamCong = new Epoint.Systems.Controls.dgvControl();
            this.tabDetail = new System.Windows.Forms.TabControl();
            this.grLoadText = new System.Windows.Forms.GroupBox();
            this.numControl2 = new Epoint.Systems.Controls.numControl();
            this.numControl1 = new Epoint.Systems.Controls.numControl();
            this.numMuc_Luong = new Epoint.Systems.Controls.numControl();
            this.dteNgay_Ct2 = new Epoint.Systems.Controls.txtDateTime();
            this.dteNgay_Ct1 = new Epoint.Systems.Controls.txtDateTime();
            this.lblControl4 = new Epoint.Systems.Controls.lblControl();
            this.lblControl7 = new Epoint.Systems.Controls.lblControl();
            this.lblControl6 = new Epoint.Systems.Controls.lblControl();
            this.lblControl5 = new Epoint.Systems.Controls.lblControl();
            this.lblControl3 = new Epoint.Systems.Controls.lblControl();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.txtTen_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.txtMa_CbNv = new Epoint.Systems.Controls.txtTextBox();
            this.pageNP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNP)).BeginInit();
            this.pageViewdata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).BeginInit();
            this.tabDetail.SuspendLayout();
            this.grLoadText.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLoadData
            // 
            this.btLoadData.Location = new System.Drawing.Point(799, 19);
            this.btLoadData.Name = "btLoadData";
            this.btLoadData.Size = new System.Drawing.Size(88, 26);
            this.btLoadData.TabIndex = 3;
            this.btLoadData.Text = "Load";
            this.btLoadData.UseVisualStyleBackColor = true;
            // 
            // pageNP
            // 
            this.pageNP.Controls.Add(this.dgvNP);
            this.pageNP.Location = new System.Drawing.Point(4, 22);
            this.pageNP.Name = "pageNP";
            this.pageNP.Padding = new System.Windows.Forms.Padding(3);
            this.pageNP.Size = new System.Drawing.Size(891, 398);
            this.pageNP.TabIndex = 1;
            this.pageNP.Tag = "NP";
            this.pageNP.Text = "Nghỉ phép";
            this.pageNP.UseVisualStyleBackColor = true;
            // 
            // dgvNP
            // 
            this.dgvNP.AllowUserToAddRows = false;
            this.dgvNP.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvNP.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNP.BackgroundColor = System.Drawing.Color.White;
            this.dgvNP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvNP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNP.EnableHeadersVisualStyles = false;
            this.dgvNP.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvNP.Location = new System.Drawing.Point(3, 3);
            this.dgvNP.MultiSelect = false;
            this.dgvNP.Name = "dgvNP";
            this.dgvNP.ReadOnly = true;
            this.dgvNP.Size = new System.Drawing.Size(885, 392);
            this.dgvNP.strZone = "";
            this.dgvNP.TabIndex = 1;
            // 
            // pageViewdata
            // 
            this.pageViewdata.Controls.Add(this.dgvChamCong);
            this.pageViewdata.Location = new System.Drawing.Point(4, 22);
            this.pageViewdata.Name = "pageViewdata";
            this.pageViewdata.Padding = new System.Windows.Forms.Padding(3);
            this.pageViewdata.Size = new System.Drawing.Size(891, 398);
            this.pageViewdata.TabIndex = 0;
            this.pageViewdata.Tag = "Viewdata";
            this.pageViewdata.Text = "Chấm công";
            this.pageViewdata.UseVisualStyleBackColor = true;
            // 
            // dgvChamCong
            // 
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvChamCong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChamCong.BackgroundColor = System.Drawing.Color.White;
            this.dgvChamCong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChamCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChamCong.EnableHeadersVisualStyles = false;
            this.dgvChamCong.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvChamCong.Location = new System.Drawing.Point(3, 3);
            this.dgvChamCong.MultiSelect = false;
            this.dgvChamCong.Name = "dgvChamCong";
            this.dgvChamCong.ReadOnly = true;
            this.dgvChamCong.Size = new System.Drawing.Size(885, 392);
            this.dgvChamCong.strZone = "";
            this.dgvChamCong.TabIndex = 0;
            // 
            // tabDetail
            // 
            this.tabDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDetail.Controls.Add(this.pageViewdata);
            this.tabDetail.Controls.Add(this.pageNP);
            this.tabDetail.Location = new System.Drawing.Point(0, 109);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.SelectedIndex = 0;
            this.tabDetail.Size = new System.Drawing.Size(899, 424);
            this.tabDetail.TabIndex = 122;
            // 
            // grLoadText
            // 
            this.grLoadText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grLoadText.Controls.Add(this.numControl2);
            this.grLoadText.Controls.Add(this.numControl1);
            this.grLoadText.Controls.Add(this.numMuc_Luong);
            this.grLoadText.Controls.Add(this.dteNgay_Ct2);
            this.grLoadText.Controls.Add(this.dteNgay_Ct1);
            this.grLoadText.Controls.Add(this.lblControl4);
            this.grLoadText.Controls.Add(this.lblControl7);
            this.grLoadText.Controls.Add(this.lblControl6);
            this.grLoadText.Controls.Add(this.lblControl5);
            this.grLoadText.Controls.Add(this.lblControl3);
            this.grLoadText.Controls.Add(this.lblControl2);
            this.grLoadText.Controls.Add(this.lblControl1);
            this.grLoadText.Controls.Add(this.txtTen_CbNv);
            this.grLoadText.Controls.Add(this.txtMa_CbNv);
            this.grLoadText.Controls.Add(this.btLoadData);
            this.grLoadText.Location = new System.Drawing.Point(0, 8);
            this.grLoadText.Name = "grLoadText";
            this.grLoadText.Size = new System.Drawing.Size(899, 95);
            this.grLoadText.TabIndex = 144;
            this.grLoadText.TabStop = false;
            // 
            // numControl2
            // 
            this.numControl2.bEnabled = true;
            this.numControl2.bFormat = true;
            this.numControl2.bIsLookup = false;
            this.numControl2.bReadOnly = false;
            this.numControl2.bRequire = false;
            this.numControl2.KeyFilter = "";
            this.numControl2.Location = new System.Drawing.Point(618, 71);
            this.numControl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numControl2.Name = "numControl2";
            this.numControl2.Scale = 0;
            this.numControl2.Size = new System.Drawing.Size(140, 20);
            this.numControl2.TabIndex = 164;
            this.numControl2.Text = "0";
            this.numControl2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numControl2.UseAutoFilter = false;
            this.numControl2.Value = 0D;
            // 
            // numControl1
            // 
            this.numControl1.bEnabled = true;
            this.numControl1.bFormat = true;
            this.numControl1.bIsLookup = false;
            this.numControl1.bReadOnly = false;
            this.numControl1.bRequire = false;
            this.numControl1.KeyFilter = "";
            this.numControl1.Location = new System.Drawing.Point(374, 72);
            this.numControl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numControl1.Name = "numControl1";
            this.numControl1.Scale = 0;
            this.numControl1.Size = new System.Drawing.Size(140, 20);
            this.numControl1.TabIndex = 164;
            this.numControl1.Text = "0";
            this.numControl1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numControl1.UseAutoFilter = false;
            this.numControl1.Value = 0D;
            // 
            // numMuc_Luong
            // 
            this.numMuc_Luong.bEnabled = true;
            this.numMuc_Luong.bFormat = true;
            this.numMuc_Luong.bIsLookup = false;
            this.numMuc_Luong.bReadOnly = false;
            this.numMuc_Luong.bRequire = false;
            this.numMuc_Luong.KeyFilter = "";
            this.numMuc_Luong.Location = new System.Drawing.Point(121, 70);
            this.numMuc_Luong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numMuc_Luong.Name = "numMuc_Luong";
            this.numMuc_Luong.Scale = 0;
            this.numMuc_Luong.Size = new System.Drawing.Size(140, 20);
            this.numMuc_Luong.TabIndex = 164;
            this.numMuc_Luong.Text = "0";
            this.numMuc_Luong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMuc_Luong.UseAutoFilter = false;
            this.numMuc_Luong.Value = 0D;
            // 
            // dteNgay_Ct2
            // 
            this.dteNgay_Ct2.bAllowEmpty = true;
            this.dteNgay_Ct2.bRequire = false;
            this.dteNgay_Ct2.bSelectOnFocus = false;
            this.dteNgay_Ct2.bShowDateTimePicker = true;
            this.dteNgay_Ct2.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ct2.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ct2.Location = new System.Drawing.Point(382, 44);
            this.dteNgay_Ct2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ct2.Mask = "00/00/0000";
            this.dteNgay_Ct2.Name = "dteNgay_Ct2";
            this.dteNgay_Ct2.Size = new System.Drawing.Size(64, 20);
            this.dteNgay_Ct2.TabIndex = 2;
            // 
            // dteNgay_Ct1
            // 
            this.dteNgay_Ct1.bAllowEmpty = true;
            this.dteNgay_Ct1.bRequire = false;
            this.dteNgay_Ct1.bSelectOnFocus = false;
            this.dteNgay_Ct1.bShowDateTimePicker = true;
            this.dteNgay_Ct1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteNgay_Ct1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteNgay_Ct1.Location = new System.Drawing.Point(121, 40);
            this.dteNgay_Ct1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.dteNgay_Ct1.Mask = "00/00/0000";
            this.dteNgay_Ct1.Name = "dteNgay_Ct1";
            this.dteNgay_Ct1.Size = new System.Drawing.Size(64, 20);
            this.dteNgay_Ct1.TabIndex = 1;
            // 
            // lblControl4
            // 
            this.lblControl4.AutoEllipsis = true;
            this.lblControl4.AutoSize = true;
            this.lblControl4.BackColor = System.Drawing.Color.Transparent;
            this.lblControl4.Location = new System.Drawing.Point(273, 47);
            this.lblControl4.Name = "lblControl4";
            this.lblControl4.Size = new System.Drawing.Size(53, 13);
            this.lblControl4.TabIndex = 163;
            this.lblControl4.Text = "Đến ngày";
            this.lblControl4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl7
            // 
            this.lblControl7.AutoEllipsis = true;
            this.lblControl7.AutoSize = true;
            this.lblControl7.BackColor = System.Drawing.Color.Transparent;
            this.lblControl7.Location = new System.Drawing.Point(519, 78);
            this.lblControl7.Name = "lblControl7";
            this.lblControl7.Size = new System.Drawing.Size(73, 13);
            this.lblControl7.TabIndex = 163;
            this.lblControl7.Text = "Tổng số đi trễ";
            this.lblControl7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl6
            // 
            this.lblControl6.AutoEllipsis = true;
            this.lblControl6.AutoSize = true;
            this.lblControl6.BackColor = System.Drawing.Color.Transparent;
            this.lblControl6.Location = new System.Drawing.Point(273, 76);
            this.lblControl6.Name = "lblControl6";
            this.lblControl6.Size = new System.Drawing.Size(96, 13);
            this.lblControl6.TabIndex = 163;
            this.lblControl6.Text = "Số ngày nghỉ phép";
            this.lblControl6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl5
            // 
            this.lblControl5.AutoEllipsis = true;
            this.lblControl5.AutoSize = true;
            this.lblControl5.BackColor = System.Drawing.Color.Transparent;
            this.lblControl5.Location = new System.Drawing.Point(12, 73);
            this.lblControl5.Name = "lblControl5";
            this.lblControl5.Size = new System.Drawing.Size(86, 13);
            this.lblControl5.TabIndex = 163;
            this.lblControl5.Text = "Số ngày vi phạm";
            this.lblControl5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl3
            // 
            this.lblControl3.AutoEllipsis = true;
            this.lblControl3.AutoSize = true;
            this.lblControl3.BackColor = System.Drawing.Color.Transparent;
            this.lblControl3.Location = new System.Drawing.Point(12, 43);
            this.lblControl3.Name = "lblControl3";
            this.lblControl3.Size = new System.Drawing.Size(46, 13);
            this.lblControl3.TabIndex = 163;
            this.lblControl3.Text = "Từ ngày";
            this.lblControl3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.Location = new System.Drawing.Point(273, 16);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(76, 13);
            this.lblControl2.TabIndex = 143;
            this.lblControl2.Text = "Tên nhân viên";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(12, 16);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(72, 13);
            this.lblControl1.TabIndex = 142;
            this.lblControl1.Text = "Mã nhân viên";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTen_CbNv
            // 
            this.txtTen_CbNv.bEnabled = true;
            this.txtTen_CbNv.bIsLookup = false;
            this.txtTen_CbNv.bReadOnly = false;
            this.txtTen_CbNv.bRequire = false;
            this.txtTen_CbNv.KeyFilter = "";
            this.txtTen_CbNv.Location = new System.Drawing.Point(382, 12);
            this.txtTen_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_CbNv.Name = "txtTen_CbNv";
            this.txtTen_CbNv.Size = new System.Drawing.Size(333, 20);
            this.txtTen_CbNv.TabIndex = 141;
            this.txtTen_CbNv.UseAutoFilter = false;
            // 
            // txtMa_CbNv
            // 
            this.txtMa_CbNv.bEnabled = true;
            this.txtMa_CbNv.bIsLookup = false;
            this.txtMa_CbNv.bReadOnly = false;
            this.txtMa_CbNv.bRequire = false;
            this.txtMa_CbNv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_CbNv.KeyFilter = "";
            this.txtMa_CbNv.Location = new System.Drawing.Point(121, 13);
            this.txtMa_CbNv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_CbNv.Name = "txtMa_CbNv";
            this.txtMa_CbNv.Size = new System.Drawing.Size(120, 20);
            this.txtMa_CbNv.TabIndex = 0;
            this.txtMa_CbNv.UseAutoFilter = false;
            // 
            // frmViewTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 533);
            this.Controls.Add(this.grLoadText);
            this.Controls.Add(this.tabDetail);
            this.Name = "frmViewTransaction";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ViewTransaction";
            this.pageNP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNP)).EndInit();
            this.pageViewdata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.grLoadText.ResumeLayout(false);
            this.grLoadText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLoadData;
        private System.Windows.Forms.TabPage pageNP;
        private Systems.Controls.dgvControl dgvNP;
        private System.Windows.Forms.TabPage pageViewdata;
        private Systems.Controls.dgvControl dgvChamCong;
        private System.Windows.Forms.TabControl tabDetail;
        private System.Windows.Forms.GroupBox grLoadText;
        private Systems.Controls.lblControl lblControl2;
        private Systems.Controls.lblControl lblControl1;
        private Systems.Controls.txtTextBox txtTen_CbNv;
        private Systems.Controls.txtTextBox txtMa_CbNv;
        private Systems.Controls.txtDateTime dteNgay_Ct2;
        private Systems.Controls.txtDateTime dteNgay_Ct1;
        private Systems.Controls.lblControl lblControl4;
        private Systems.Controls.lblControl lblControl3;
        private Systems.Controls.lblControl lblControl7;
        private Systems.Controls.lblControl lblControl6;
        private Systems.Controls.lblControl lblControl5;
        private Systems.Controls.numControl numControl2;
        private Systems.Controls.numControl numControl1;
        private Systems.Controls.numControl numMuc_Luong;

	}
}