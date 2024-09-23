namespace Epoint.Modules.HRM
{
	partial class frmChamCong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChamCong));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btChamCong_Tay = new Epoint.Systems.Controls.btControl();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btChamCong_Tay);
            this.splitContainer1.Size = new System.Drawing.Size(792, 566);
            this.splitContainer1.SplitterDistance = 530;
            this.splitContainer1.TabIndex = 0;
            // 
            // btChamCong_Tay
            // 
            this.btChamCong_Tay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btChamCong_Tay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChamCong_Tay.ForeColor = System.Drawing.Color.Blue;
            this.btChamCong_Tay.Image = ((System.Drawing.Image)(resources.GetObject("btChamCong_Tay.Image")));
            this.btChamCong_Tay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btChamCong_Tay.Location = new System.Drawing.Point(17, -1);
            this.btChamCong_Tay.Name = "btChamCong_Tay";
            this.btChamCong_Tay.Size = new System.Drawing.Size(224, 30);
            this.btChamCong_Tay.TabIndex = 5;
            this.btChamCong_Tay.Tag = "ChamCong_Tay";
            this.btChamCong_Tay.Text = "&Đăng ký chấm công bằng máy";
            this.btChamCong_Tay.UseVisualStyleBackColor = true;
            // 
            // frmChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmChamCong";
            this.Tag = "frmChamCong";
            this.Text = "frmChamCong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Systems.Controls.btControl btChamCong_Tay;
	}
}