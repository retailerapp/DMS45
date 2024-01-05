namespace Epoint.Modules.AP
{
	partial class frmPbCp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPbCp));
            this.numTien_Pb_Nt = new Epoint.Systems.Controls.numControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.lblTien_Pb_Nt = new Epoint.Systems.Controls.lblControl();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.txtLoai_Pb = new Epoint.Systems.Controls.txtEnum();
            this.lblLoai_Pb = new Epoint.Systems.Controls.lblControl();
            this.grbPhanBo = new Epoint.Systems.Controls.grbControl();
            this.grbPhanBo.SuspendLayout();
            this.SuspendLayout();
            // 
            // numTien_Pb_Nt
            // 
            this.numTien_Pb_Nt.bEnabled = true;
            this.numTien_Pb_Nt.bFormat = true;
            this.numTien_Pb_Nt.bIsLookup = false;
            this.numTien_Pb_Nt.bReadOnly = false;
            this.numTien_Pb_Nt.bRequire = false;
            this.numTien_Pb_Nt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTien_Pb_Nt.KeyFilter = "";
            this.numTien_Pb_Nt.Location = new System.Drawing.Point(142, 23);
            this.numTien_Pb_Nt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numTien_Pb_Nt.Name = "numTien_Pb_Nt";
            this.numTien_Pb_Nt.Scale = 2;
            this.numTien_Pb_Nt.Size = new System.Drawing.Size(120, 20);
            this.numTien_Pb_Nt.TabIndex = 0;
            this.numTien_Pb_Nt.Text = "0.00";
            this.numTien_Pb_Nt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTien_Pb_Nt.UseAutoFilter = false;
            this.numTien_Pb_Nt.Value = 0D;
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(304, 135);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(171, 33);
            this.btgAccept.TabIndex = 2;
            // 
            // lblTien_Pb_Nt
            // 
            this.lblTien_Pb_Nt.AutoEllipsis = true;
            this.lblTien_Pb_Nt.AutoSize = true;
            this.lblTien_Pb_Nt.BackColor = System.Drawing.Color.Transparent;
            this.lblTien_Pb_Nt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTien_Pb_Nt.Location = new System.Drawing.Point(31, 26);
            this.lblTien_Pb_Nt.Name = "lblTien_Pb_Nt";
            this.lblTien_Pb_Nt.Size = new System.Drawing.Size(34, 13);
            this.lblTien_Pb_Nt.TabIndex = 56;
            this.lblTien_Pb_Nt.Tag = "Amount";
            this.lblTien_Pb_Nt.Text = "Giá trị";
            this.lblTien_Pb_Nt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControl1.ForeColor = System.Drawing.Color.Blue;
            this.lblControl1.Location = new System.Drawing.Point(178, 49);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(158, 13);
            this.lblControl1.TabIndex = 59;
            this.lblControl1.Tag = "Allocate_Option";
            this.lblControl1.Text = "1- Theo giá trị, 2- Theo số lượng";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLoai_Pb
            // 
            this.txtLoai_Pb.bEnabled = true;
            this.txtLoai_Pb.bIsLookup = false;
            this.txtLoai_Pb.bReadOnly = false;
            this.txtLoai_Pb.bRequire = false;
            this.txtLoai_Pb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoai_Pb.InputMask = "1,2";
            this.txtLoai_Pb.KeyFilter = "";
            this.txtLoai_Pb.Location = new System.Drawing.Point(142, 46);
            this.txtLoai_Pb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtLoai_Pb.Name = "txtLoai_Pb";
            this.txtLoai_Pb.Size = new System.Drawing.Size(27, 20);
            this.txtLoai_Pb.TabIndex = 1;
            this.txtLoai_Pb.Text = "1";
            this.txtLoai_Pb.UseAutoFilter = false;
            // 
            // lblLoai_Pb
            // 
            this.lblLoai_Pb.AutoEllipsis = true;
            this.lblLoai_Pb.AutoSize = true;
            this.lblLoai_Pb.BackColor = System.Drawing.Color.Transparent;
            this.lblLoai_Pb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoai_Pb.Location = new System.Drawing.Point(31, 49);
            this.lblLoai_Pb.Name = "lblLoai_Pb";
            this.lblLoai_Pb.Size = new System.Drawing.Size(71, 13);
            this.lblLoai_Pb.TabIndex = 58;
            this.lblLoai_Pb.Tag = "Allocate_Follow";
            this.lblLoai_Pb.Text = "Phân bổ theo";
            this.lblLoai_Pb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grbPhanBo
            // 
            this.grbPhanBo.Controls.Add(this.lblControl1);
            this.grbPhanBo.Controls.Add(this.txtLoai_Pb);
            this.grbPhanBo.Controls.Add(this.lblLoai_Pb);
            this.grbPhanBo.Controls.Add(this.lblTien_Pb_Nt);
            this.grbPhanBo.Controls.Add(this.numTien_Pb_Nt);
            this.grbPhanBo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbPhanBo.Location = new System.Drawing.Point(26, 25);
            this.grbPhanBo.Name = "grbPhanBo";
            this.grbPhanBo.Size = new System.Drawing.Size(447, 90);
            this.grbPhanBo.TabIndex = 0;
            this.grbPhanBo.TabStop = false;
            this.grbPhanBo.Tag = "Amount_Allocate";
            this.grbPhanBo.Text = "Giá trị phân bổ";
            // 
            // frmPbCp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 179);
            this.Controls.Add(this.grbPhanBo);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPbCp";
            this.Tag = "frmPhanBoChiPhi";
            this.Text = "frmPhanBoChiPhi";
            this.grbPhanBo.ResumeLayout(false);
            this.grbPhanBo.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        public Epoint.Systems.Controls.numControl numTien_Pb_Nt;
		private Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.lblControl lblTien_Pb_Nt;
		private Epoint.Systems.Controls.lblControl lblControl1;
		public Epoint.Systems.Controls.txtEnum txtLoai_Pb;
		private Epoint.Systems.Controls.lblControl lblLoai_Pb;
        private Epoint.Systems.Controls.grbControl grbPhanBo;
	}
}