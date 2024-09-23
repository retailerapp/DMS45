namespace Epoint.Modules.HRM
{
	partial class frmChamCong_VanTay
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChamCong_VanTay));
            this.dgvChamCong_VanTay = new Epoint.Systems.Controls.dgvControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong_VanTay)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChamCong_VanTay
            // 
            this.dgvChamCong_VanTay.AllowUserToAddRows = false;
            this.dgvChamCong_VanTay.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvChamCong_VanTay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChamCong_VanTay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChamCong_VanTay.BackgroundColor = System.Drawing.Color.White;
            this.dgvChamCong_VanTay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChamCong_VanTay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamCong_VanTay.EnableHeadersVisualStyles = false;
            this.dgvChamCong_VanTay.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvChamCong_VanTay.Location = new System.Drawing.Point(2, 2);
            this.dgvChamCong_VanTay.Margin = new System.Windows.Forms.Padding(1);
            this.dgvChamCong_VanTay.MultiSelect = false;
            this.dgvChamCong_VanTay.Name = "dgvChamCong_VanTay";
            this.dgvChamCong_VanTay.ReadOnly = true;
            this.dgvChamCong_VanTay.Size = new System.Drawing.Size(788, 526);
            this.dgvChamCong_VanTay.strZone = "";
            this.dgvChamCong_VanTay.TabIndex = 0;
            // 
            // btgAccept
            // 
            this.btgAccept.Location = new System.Drawing.Point(594, 533);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 1;
            this.btgAccept.TabStop = false;
            // 
            // frmChamCong_VanTay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 571);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.dgvChamCong_VanTay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChamCong_VanTay";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Tag = "frmChamCong_VanTay";
            this.Text = "frmChamCong_VanTay";
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong_VanTay)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Controls.dgvControl dgvChamCong_VanTay;
        private Epoint.Systems.Customizes.btgAccept btgAccept;

	}
}