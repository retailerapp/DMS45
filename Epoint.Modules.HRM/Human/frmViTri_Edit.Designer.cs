namespace Epoint.Modules.HRM
{
	partial class frmViTri_Edit
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
            this.txtTen_ViTri = new Epoint.Systems.Controls.txtTextBox();
            this.txtMa_ViTri = new Epoint.Systems.Controls.txtTextBox();
            this.lbTen_Km = new Epoint.Systems.Controls.lblControl();
            this.lbMa_ViTri = new Epoint.Systems.Controls.lblControl();
            this.grTitle1 = new System.Windows.Forms.GroupBox();
            this.tabEdit.SuspendLayout();
            this.Page1.SuspendLayout();
            this.Page2.SuspendLayout();
            this.grTitle1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Location = new System.Drawing.Point(342, 190);
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            // 
            // tabEdit
            // 
            this.tabEdit.Size = new System.Drawing.Size(512, 168);
            this.tabEdit.TabIndex = 0;
            // 
            // Page1
            // 
            this.Page1.Controls.Add(this.grTitle1);
            this.Page1.Size = new System.Drawing.Size(504, 142);
            // 
            // Page2
            // 
            this.Page2.Size = new System.Drawing.Size(504, 149);
            // 
            // txtTen_ViTri
            // 
            this.txtTen_ViTri.bEnabled = true;
            this.txtTen_ViTri.bIsLookup = false;
            this.txtTen_ViTri.bReadOnly = false;
            this.txtTen_ViTri.bRequire = false;
            this.txtTen_ViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen_ViTri.KeyFilter = "";
            this.txtTen_ViTri.Location = new System.Drawing.Point(125, 61);
            this.txtTen_ViTri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtTen_ViTri.MaxLength = 100;
            this.txtTen_ViTri.Name = "txtTen_ViTri";
            this.txtTen_ViTri.Size = new System.Drawing.Size(335, 20);
            this.txtTen_ViTri.TabIndex = 1;
            this.txtTen_ViTri.UseAutoFilter = false;
            // 
            // txtMa_ViTri
            // 
            this.txtMa_ViTri.bEnabled = true;
            this.txtMa_ViTri.bIsLookup = false;
            this.txtMa_ViTri.bReadOnly = false;
            this.txtMa_ViTri.bRequire = false;
            this.txtMa_ViTri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMa_ViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa_ViTri.KeyFilter = "";
            this.txtMa_ViTri.Location = new System.Drawing.Point(125, 37);
            this.txtMa_ViTri.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtMa_ViTri.MaxLength = 20;
            this.txtMa_ViTri.Name = "txtMa_ViTri";
            this.txtMa_ViTri.Size = new System.Drawing.Size(120, 20);
            this.txtMa_ViTri.TabIndex = 0;
            this.txtMa_ViTri.UseAutoFilter = false;
            // 
            // lbTen_Km
            // 
            this.lbTen_Km.AutoEllipsis = true;
            this.lbTen_Km.AutoSize = true;
            this.lbTen_Km.BackColor = System.Drawing.Color.Transparent;
            this.lbTen_Km.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTen_Km.Location = new System.Drawing.Point(14, 62);
            this.lbTen_Km.Name = "lbTen_Km";
            this.lbTen_Km.Size = new System.Drawing.Size(50, 13);
            this.lbTen_Km.TabIndex = 19;
            this.lbTen_Km.Tag = "Ten_ViTri";
            this.lbTen_Km.Text = "Tên vị trí";
            this.lbTen_Km.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMa_ViTri
            // 
            this.lbMa_ViTri.AutoEllipsis = true;
            this.lbMa_ViTri.AutoSize = true;
            this.lbMa_ViTri.BackColor = System.Drawing.Color.Transparent;
            this.lbMa_ViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMa_ViTri.Location = new System.Drawing.Point(14, 37);
            this.lbMa_ViTri.Name = "lbMa_ViTri";
            this.lbMa_ViTri.Size = new System.Drawing.Size(46, 13);
            this.lbMa_ViTri.TabIndex = 20;
            this.lbMa_ViTri.Tag = "Ma_ViTri";
            this.lbMa_ViTri.Text = "Mã vị trí";
            this.lbMa_ViTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grTitle1
            // 
            this.grTitle1.Controls.Add(this.lbMa_ViTri);
            this.grTitle1.Controls.Add(this.lbTen_Km);
            this.grTitle1.Controls.Add(this.txtTen_ViTri);
            this.grTitle1.Controls.Add(this.txtMa_ViTri);
            this.grTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grTitle1.Location = new System.Drawing.Point(13, 9);
            this.grTitle1.Name = "grTitle1";
            this.grTitle1.Size = new System.Drawing.Size(478, 121);
            this.grTitle1.TabIndex = 23;
            this.grTitle1.TabStop = false;
            this.grTitle1.Text = "Vị trí";
            // 
            // frmViTri_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 234);
            this.Name = "frmViTri_Edit";
            this.Object_ID = "LIKHOANMUC";
            this.Tag = "frmKhoanMuc, ESC";
            this.Text = "frmKhoanMuc";
            this.tabEdit.ResumeLayout(false);
            this.Page1.ResumeLayout(false);
            this.Page2.ResumeLayout(false);
            this.Page2.PerformLayout();
            this.grTitle1.ResumeLayout(false);
            this.grTitle1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Controls.txtTextBox txtTen_ViTri;
		private Epoint.Systems.Controls.txtTextBox txtMa_ViTri;
		private Epoint.Systems.Controls.lblControl lbTen_Km;
		private Epoint.Systems.Controls.lblControl lbMa_ViTri;
        private System.Windows.Forms.GroupBox grTitle1;

	}
}