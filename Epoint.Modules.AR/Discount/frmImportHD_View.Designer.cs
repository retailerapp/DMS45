namespace Epoint.Modules.AR
{
    partial class frmImportHD_View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportHD_View));
            this.dgvViewHD = new Epoint.Systems.Controls.dgvControl();
            this.txtFile_Name = new Epoint.Systems.Controls.txtTextBox();
            this.lblSo_Ct1 = new Epoint.Systems.Controls.lblControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btLoadData = new Epoint.Systems.Customizes.btPreview();
            this.btImportData = new Epoint.Systems.Customizes.btPreview();
            this.btFile = new Epoint.Systems.Controls.btControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewHD)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvViewHD
            // 
            this.dgvViewHD.AllowUserToAddRows = false;
            this.dgvViewHD.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvViewHD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvViewHD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViewHD.BackgroundColor = System.Drawing.Color.White;
            this.dgvViewHD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViewHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewHD.EnableHeadersVisualStyles = false;
            this.dgvViewHD.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvViewHD.Location = new System.Drawing.Point(2, 84);
            this.dgvViewHD.Margin = new System.Windows.Forms.Padding(1);
            this.dgvViewHD.MultiSelect = false;
            this.dgvViewHD.Name = "dgvViewHD";
            this.dgvViewHD.ReadOnly = true;
            this.dgvViewHD.Size = new System.Drawing.Size(788, 497);
            this.dgvViewHD.strZone = "";
            this.dgvViewHD.TabIndex = 2;
            // 
            // txtFile_Name
            // 
            this.txtFile_Name.bEnabled = true;
            this.txtFile_Name.bIsLookup = false;
            this.txtFile_Name.bReadOnly = false;
            this.txtFile_Name.bRequire = false;
            this.txtFile_Name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFile_Name.KeyFilter = "";
            this.txtFile_Name.Location = new System.Drawing.Point(105, 16);
            this.txtFile_Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtFile_Name.MaxLength = 20;
            this.txtFile_Name.Name = "txtFile_Name";
            this.txtFile_Name.Size = new System.Drawing.Size(550, 20);
            this.txtFile_Name.TabIndex = 87;
            this.txtFile_Name.UseAutoFilter = false;
            // 
            // lblSo_Ct1
            // 
            this.lblSo_Ct1.AutoEllipsis = true;
            this.lblSo_Ct1.AutoSize = true;
            this.lblSo_Ct1.BackColor = System.Drawing.Color.Transparent;
            this.lblSo_Ct1.Location = new System.Drawing.Point(9, 19);
            this.lblSo_Ct1.Name = "lblSo_Ct1";
            this.lblSo_Ct1.Size = new System.Drawing.Size(23, 13);
            this.lblSo_Ct1.TabIndex = 88;
            this.lblSo_Ct1.Tag = "File";
            this.lblSo_Ct1.Text = "File";
            this.lblSo_Ct1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btLoadData);
            this.groupBox1.Controls.Add(this.btImportData);
            this.groupBox1.Controls.Add(this.btFile);
            this.groupBox1.Controls.Add(this.txtFile_Name);
            this.groupBox1.Controls.Add(this.lblSo_Ct1);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(788, 79);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lọc";
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
            // btImportData
            // 
            this.btImportData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImportData.Image = ((System.Drawing.Image)(resources.GetObject("btImportData.Image")));
            this.btImportData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btImportData.Location = new System.Drawing.Point(601, 39);
            this.btImportData.Name = "btImportData";
            this.btImportData.Size = new System.Drawing.Size(121, 36);
            this.btImportData.TabIndex = 90;
            this.btImportData.Tag = "";
            this.btImportData.Text = "Xử lý";
            this.btImportData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btImportData.UseVisualStyleBackColor = true;
            // 
            // btFile
            // 
            this.btFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFile.Image = ((System.Drawing.Image)(resources.GetObject("btFile.Image")));
            this.btFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFile.Location = new System.Drawing.Point(681, 14);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(41, 22);
            this.btFile.TabIndex = 89;
            this.btFile.Tag = "";
            this.btFile.Text = "...";
            this.btFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btFile.UseVisualStyleBackColor = true;
            // 
            // frmImportHD_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 581);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvViewHD);
            this.Name = "frmImportHD_View";
            this.Text = "frmImportHD_View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewHD)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private Epoint.Systems.Controls.dgvControl dgvViewHD;
        private Systems.Controls.txtTextBox txtFile_Name;
        private Systems.Controls.lblControl lblSo_Ct1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Systems.Controls.btControl btFile;
        private Systems.Customizes.btPreview btLoadData;
        private Systems.Customizes.btPreview btImportData;

	}
}