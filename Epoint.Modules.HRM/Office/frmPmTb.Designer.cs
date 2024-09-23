namespace Epoint.Modules.HRM
{
	partial class frmPmTb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPmTb));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btFilter = new Epoint.Systems.Controls.btControl();
            this.lblDen_Ngay = new Epoint.Systems.Controls.lblControl();
            this.dteDen_Ngay = new Epoint.Systems.Controls.txtDateTime();
            this.lblNgay_Ct = new Epoint.Systems.Controls.lblControl();
            this.dteTu_Ngay = new Epoint.Systems.Controls.txtDateTime();
            this.pnlControl2.SuspendLayout();
            this.splControl1.Panel2.SuspendLayout();
            this.splControl1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splControl1
            // 
            this.splControl1.Size = new System.Drawing.Size(807, 569);
            this.splControl1.SplitterDistance = 508;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btFilter);
            this.splitContainer1.Panel1.Controls.Add(this.lblDen_Ngay);
            this.splitContainer1.Panel1.Controls.Add(this.dteDen_Ngay);
            this.splitContainer1.Panel1.Controls.Add(this.lblNgay_Ct);
            this.splitContainer1.Panel1.Controls.Add(this.dteTu_Ngay);
            this.splitContainer1.Size = new System.Drawing.Size(807, 569);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 1;
            // 
            // btFilter
            // 
            this.btFilter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btFilter.BackgroundImage")));
            this.btFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btFilter.Location = new System.Drawing.Point(369, 3);
            this.btFilter.Name = "btFilter";
            this.btFilter.Size = new System.Drawing.Size(49, 24);
            this.btFilter.TabIndex = 9;
            this.btFilter.Tag = "";
            this.btFilter.Text = "Lọc";
            this.btFilter.UseVisualStyleBackColor = true;
            // 
            // lblDen_Ngay
            // 
            this.lblDen_Ngay.AutoEllipsis = true;
            this.lblDen_Ngay.AutoSize = true;
            this.lblDen_Ngay.BackColor = System.Drawing.Color.Transparent;
            this.lblDen_Ngay.Location = new System.Drawing.Point(193, 9);
            this.lblDen_Ngay.Name = "lblDen_Ngay";
            this.lblDen_Ngay.Size = new System.Drawing.Size(53, 13);
            this.lblDen_Ngay.TabIndex = 7;
            this.lblDen_Ngay.Tag = "";
            this.lblDen_Ngay.Text = "Đến ngày";
            this.lblDen_Ngay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dteDen_Ngay
            // 
            this.dteDen_Ngay.bAllowEmpty = false;
            this.dteDen_Ngay.bRequire = false;
            this.dteDen_Ngay.bSelectOnFocus = false;
            this.dteDen_Ngay.bShowDateTimePicker = true;
            this.dteDen_Ngay.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteDen_Ngay.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteDen_Ngay.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteDen_Ngay.Location = new System.Drawing.Point(254, 5);
            this.dteDen_Ngay.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteDen_Ngay.Mask = "00/00/0000";
            this.dteDen_Ngay.Name = "dteDen_Ngay";
            this.dteDen_Ngay.Size = new System.Drawing.Size(101, 20);
            this.dteDen_Ngay.TabIndex = 8;
            // 
            // lblNgay_Ct
            // 
            this.lblNgay_Ct.AutoEllipsis = true;
            this.lblNgay_Ct.AutoSize = true;
            this.lblNgay_Ct.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay_Ct.Location = new System.Drawing.Point(11, 9);
            this.lblNgay_Ct.Name = "lblNgay_Ct";
            this.lblNgay_Ct.Size = new System.Drawing.Size(46, 13);
            this.lblNgay_Ct.TabIndex = 5;
            this.lblNgay_Ct.Tag = "";
            this.lblNgay_Ct.Text = "Từ ngày";
            this.lblNgay_Ct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dteTu_Ngay
            // 
            this.dteTu_Ngay.bAllowEmpty = false;
            this.dteTu_Ngay.bRequire = false;
            this.dteTu_Ngay.bSelectOnFocus = false;
            this.dteTu_Ngay.bShowDateTimePicker = true;
            this.dteTu_Ngay.Culture = new System.Globalization.CultureInfo("fr-FR");
            this.dteTu_Ngay.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.dteTu_Ngay.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.dteTu_Ngay.Location = new System.Drawing.Point(72, 5);
            this.dteTu_Ngay.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dteTu_Ngay.Mask = "00/00/0000";
            this.dteTu_Ngay.Name = "dteTu_Ngay";
            this.dteTu_Ngay.Size = new System.Drawing.Size(101, 20);
            this.dteTu_Ngay.TabIndex = 6;
            // 
            // frmPmTb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 569);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmPmTb";
            this.Object_ID = "PMTB";
            this.Tag = "frmPmTb, F2, F3, F6, F8, ESC";
            this.Text = "frmPmTb";
            this.Controls.SetChildIndex(this.splControl1, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.pnlControl2.ResumeLayout(false);
            this.splControl1.Panel2.ResumeLayout(false);
            this.splControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Epoint.Systems.Controls.btControl btFilter;
        private Epoint.Systems.Controls.lblControl lblDen_Ngay;
        private Epoint.Systems.Controls.txtDateTime dteDen_Ngay;
        private Epoint.Systems.Controls.lblControl lblNgay_Ct;
        private Epoint.Systems.Controls.txtDateTime dteTu_Ngay;



	}
}