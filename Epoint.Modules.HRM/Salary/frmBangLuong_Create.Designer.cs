namespace Epoint.Modules.Salary
{
	partial class frmBangLuong_Create
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBangLuong_Create));
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.enuBangLuong_Option = new Epoint.Systems.Controls.txtEnum();
            this.lbtLoai_Tn = new Epoint.Systems.Controls.lbtControl();
            this.lblControl1 = new Epoint.Systems.Controls.lblControl();
            this.numThang1 = new Epoint.Systems.Controls.numControl();
            this.numNam1 = new Epoint.Systems.Controls.numControl();
            this.lblThang0 = new System.Windows.Forms.Label();
            this.lblNam = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(308, 123);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(169, 33);
            this.btgAccept.TabIndex = 5;
            // 
            // enuBangLuong_Option
            // 
            this.enuBangLuong_Option.bEnabled = true;
            this.enuBangLuong_Option.bIsLookup = false;
            this.enuBangLuong_Option.bReadOnly = false;
            this.enuBangLuong_Option.bRequire = false;
            this.enuBangLuong_Option.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.enuBangLuong_Option.InputMask = "1,2";
            this.enuBangLuong_Option.KeyFilter = "";
            this.enuBangLuong_Option.Location = new System.Drawing.Point(95, 80);
            this.enuBangLuong_Option.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.enuBangLuong_Option.MaxLength = 20;
            this.enuBangLuong_Option.Name = "enuBangLuong_Option";
            this.enuBangLuong_Option.Size = new System.Drawing.Size(58, 20);
            this.enuBangLuong_Option.TabIndex = 2;
            this.enuBangLuong_Option.Text = "1";
            this.enuBangLuong_Option.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.enuBangLuong_Option.UseAutoFilter = false;
            // 
            // lbtLoai_Tn
            // 
            this.lbtLoai_Tn.AutoEllipsis = true;
            this.lbtLoai_Tn.AutoSize = true;
            this.lbtLoai_Tn.ForeColor = System.Drawing.Color.Blue;
            this.lbtLoai_Tn.Location = new System.Drawing.Point(158, 83);
            this.lbtLoai_Tn.Name = "lbtLoai_Tn";
            this.lbtLoai_Tn.Size = new System.Drawing.Size(316, 13);
            this.lbtLoai_Tn.TabIndex = 3;
            this.lbtLoai_Tn.Tag = "KeThua_SoLieu";
            this.lbtLoai_Tn.Text = "1 - Kế thừa số liệu tháng trước, 2 - Lấy số liệu từ tham số khai báo";
            this.lbtLoai_Tn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblControl1
            // 
            this.lblControl1.AutoEllipsis = true;
            this.lblControl1.AutoSize = true;
            this.lblControl1.BackColor = System.Drawing.Color.Transparent;
            this.lblControl1.Location = new System.Drawing.Point(46, 83);
            this.lblControl1.Name = "lblControl1";
            this.lblControl1.Size = new System.Drawing.Size(39, 13);
            this.lblControl1.TabIndex = 66;
            this.lblControl1.Tag = "SoLieu";
            this.lblControl1.Text = "Số liệu";
            this.lblControl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numThang1
            // 
            this.numThang1.bEnabled = true;
            this.numThang1.bFormat = true;
            this.numThang1.bIsLookup = false;
            this.numThang1.bReadOnly = false;
            this.numThang1.bRequire = false;
            this.numThang1.KeyFilter = "";
            this.numThang1.Location = new System.Drawing.Point(95, 32);
            this.numThang1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numThang1.Name = "numThang1";
            this.numThang1.Scale = 0;
            this.numThang1.Size = new System.Drawing.Size(58, 20);
            this.numThang1.TabIndex = 0;
            this.numThang1.Text = "0";
            this.numThang1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numThang1.UseAutoFilter = false;
            this.numThang1.Value = 0D;
            // 
            // numNam1
            // 
            this.numNam1.bEnabled = true;
            this.numNam1.bFormat = true;
            this.numNam1.bIsLookup = false;
            this.numNam1.bReadOnly = false;
            this.numNam1.bRequire = false;
            this.numNam1.KeyFilter = "";
            this.numNam1.Location = new System.Drawing.Point(95, 56);
            this.numNam1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.numNam1.Name = "numNam1";
            this.numNam1.Scale = 0;
            this.numNam1.Size = new System.Drawing.Size(58, 20);
            this.numNam1.TabIndex = 1;
            this.numNam1.Text = "0";
            this.numNam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNam1.UseAutoFilter = false;
            this.numNam1.Value = 0D;
            // 
            // lblThang0
            // 
            this.lblThang0.AutoSize = true;
            this.lblThang0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang0.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblThang0.Location = new System.Drawing.Point(46, 35);
            this.lblThang0.Name = "lblThang0";
            this.lblThang0.Size = new System.Drawing.Size(38, 13);
            this.lblThang0.TabIndex = 68;
            this.lblThang0.Text = "Tháng";
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNam.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNam.Location = new System.Drawing.Point(46, 59);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(29, 13);
            this.lblNam.TabIndex = 68;
            this.lblNam.Tag = "Nam";
            this.lblNam.Text = "Năm";
            // 
            // frmBangLuong_Create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 176);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.lblThang0);
            this.Controls.Add(this.numNam1);
            this.Controls.Add(this.numThang1);
            this.Controls.Add(this.enuBangLuong_Option);
            this.Controls.Add(this.lbtLoai_Tn);
            this.Controls.Add(this.lblControl1);
            this.Controls.Add(this.btgAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBangLuong_Create";
            this.Tag = "frmBangLuong_Create";
            this.Text = "frmBangLuong_Create";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.txtEnum enuBangLuong_Option;
		private Epoint.Systems.Controls.lbtControl lbtLoai_Tn;
		private Epoint.Systems.Controls.lblControl lblControl1;
		private Epoint.Systems.Controls.numControl numThang1;
		private Epoint.Systems.Controls.numControl numNam1;
		private System.Windows.Forms.Label lblThang0;
		private System.Windows.Forms.Label lblNam;
	}
}