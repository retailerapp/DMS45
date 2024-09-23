namespace Epoint.Modules.HRM
{
	partial class frmDuyet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDuyet));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblControl2 = new Epoint.Systems.Controls.lblControl();
            this.txtDuyet_Log = new Epoint.Systems.Controls.txtTextBox();
            this.chkDuyet = new Epoint.Systems.Controls.chkControl();
            this.btSave = new Epoint.Systems.Controls.btControl();
            this.btExit = new Epoint.Systems.Controls.btControl();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 134);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblControl2);
            this.tabPage3.Controls.Add(this.txtDuyet_Log);
            this.tabPage3.Controls.Add(this.chkDuyet);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(392, 108);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Duyệt";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblControl2
            // 
            this.lblControl2.AutoEllipsis = true;
            this.lblControl2.AutoSize = true;
            this.lblControl2.BackColor = System.Drawing.Color.Transparent;
            this.lblControl2.Location = new System.Drawing.Point(12, 51);
            this.lblControl2.Name = "lblControl2";
            this.lblControl2.Size = new System.Drawing.Size(73, 13);
            this.lblControl2.TabIndex = 95;
            this.lblControl2.Tag = "";
            this.lblControl2.Text = "Nhật ký duyệt";
            this.lblControl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDuyet_Log
            // 
            this.txtDuyet_Log.bEnabled = true;
            this.txtDuyet_Log.bIsLookup = false;
            this.txtDuyet_Log.bReadOnly = false;
            this.txtDuyet_Log.bRequire = false;
            this.txtDuyet_Log.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDuyet_Log.Enabled = false;
            this.txtDuyet_Log.KeyFilter = "";
            this.txtDuyet_Log.Location = new System.Drawing.Point(108, 48);
            this.txtDuyet_Log.Margin = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.txtDuyet_Log.MaxLength = 20;
            this.txtDuyet_Log.Name = "txtDuyet_Log";
            this.txtDuyet_Log.Size = new System.Drawing.Size(136, 20);
            this.txtDuyet_Log.TabIndex = 3;
            this.txtDuyet_Log.UseAutoFilter = false;
            // 
            // chkDuyet
            // 
            this.chkDuyet.AutoSize = true;
            this.chkDuyet.Enabled = false;
            this.chkDuyet.Location = new System.Drawing.Point(108, 20);
            this.chkDuyet.Name = "chkDuyet";
            this.chkDuyet.Size = new System.Drawing.Size(54, 17);
            this.chkDuyet.TabIndex = 0;
            this.chkDuyet.Text = "Duyệt";
            this.chkDuyet.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSave.Enabled = false;
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSave.Location = new System.Drawing.Point(256, 161);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 1;
            this.btSave.Tag = "Save";
            this.btSave.Text = "&Lưu";
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // btExit
            // 
            this.btExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btExit.BackgroundImage")));
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btExit.Location = new System.Drawing.Point(337, 161);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(75, 23);
            this.btExit.TabIndex = 1;
            this.btExit.Tag = "Exit";
            this.btExit.Text = "&Quay ra";
            this.btExit.UseVisualStyleBackColor = true;
            // 
            // frmDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 197);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDuyet";
            this.Text = "Duyệt chứng từ";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage3;
		private Epoint.Systems.Controls.lblControl lblControl2;
		public Epoint.Systems.Controls.txtTextBox txtDuyet_Log;
        private Epoint.Systems.Controls.chkControl chkDuyet;
		private Epoint.Systems.Controls.btControl btSave;
		private Epoint.Systems.Controls.btControl btExit;
	}
}