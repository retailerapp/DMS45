namespace Epoint.Modules
{
	partial class frmChangeDvcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeDvcs));
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblMa_Data = new Epoint.Systems.Controls.lblControl();
            this.ucMa_Data = new Epoint.Systems.Customizes.ucMa_Data();
            this.ucMa_Data_New = new Epoint.Systems.Customizes.ucMa_Data();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(516, 151);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 2;
            // 
            // lblMa_Data
            // 
            this.lblMa_Data.AutoEllipsis = true;
            this.lblMa_Data.AutoSize = true;
            this.lblMa_Data.BackColor = System.Drawing.Color.Transparent;
            this.lblMa_Data.Location = new System.Drawing.Point(49, 34);
            this.lblMa_Data.Name = "lblMa_Data";
            this.lblMa_Data.Size = new System.Drawing.Size(56, 13);
            this.lblMa_Data.TabIndex = 5;
            this.lblMa_Data.Tag = "Ma_Data";
            this.lblMa_Data.Text = "Mã dữ liệu";
            this.lblMa_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucMa_Data
            // 
            this.ucMa_Data.Location = new System.Drawing.Point(143, 30);
            this.ucMa_Data.Name = "ucMa_Data";
            this.ucMa_Data.Size = new System.Drawing.Size(296, 24);
            this.ucMa_Data.TabIndex = 4;
            // 
            // ucMa_Data_New
            // 
            this.ucMa_Data_New.Location = new System.Drawing.Point(143, 60);
            this.ucMa_Data_New.Name = "ucMa_Data_New";
            this.ucMa_Data_New.Size = new System.Drawing.Size(296, 24);
            this.ucMa_Data_New.TabIndex = 4;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(49, 71);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(56, 13);
            this.lblControl1.TabIndex = 5;
            this.lblControl1.Tag = "Ma_Data";
            this.lblControl1.Text = "Mã dữ liệu";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmChangeDvcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 195);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.lblMa_Data);
            this.Controls.Add(this.ucMa_Data_New);
            this.Controls.Add(this.ucMa_Data);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChangeDvcs";
            this.Tag = "";
            this.Text = "Chuyển đơn vị";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		public Epoint.Systems.Customizes.btgAccept btgAccept;
        public Systems.Controls.lblControl lblMa_Data;
        public Systems.Customizes.ucMa_Data ucMa_Data;
        public Systems.Customizes.ucMa_Data ucMa_Data_New;
        public Systems.Controls.lblControl lblControl1;
    }
}