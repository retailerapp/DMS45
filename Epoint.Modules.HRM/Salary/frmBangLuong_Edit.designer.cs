namespace Epoint.Modules.Salary
{
	partial class frmBangLuong_Edit
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
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.dgvSalary_Edit = new Epoint.Systems.Controls.dgvControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary_Edit)).BeginInit();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(238, 328);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 33);
            this.btgAccept.TabIndex = 1;
            // 
            // dgvSalary_Edit
            // 
            this.dgvSalary_Edit.AllowUserToAddRows = false;
            this.dgvSalary_Edit.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvSalary_Edit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSalary_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalary_Edit.BackgroundColor = System.Drawing.Color.White;
            this.dgvSalary_Edit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSalary_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalary_Edit.EnableHeadersVisualStyles = false;
            this.dgvSalary_Edit.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvSalary_Edit.Location = new System.Drawing.Point(2, 1);
            this.dgvSalary_Edit.MultiSelect = false;
            this.dgvSalary_Edit.Name = "dgvSalary_Edit";
            this.dgvSalary_Edit.ReadOnly = true;
            this.dgvSalary_Edit.Size = new System.Drawing.Size(421, 320);
            this.dgvSalary_Edit.strZone = "";
            this.dgvSalary_Edit.TabIndex = 0;
            // 
            // frmBangLuong_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 369);
            this.Controls.Add(this.dgvSalary_Edit);
            this.Controls.Add(this.btgAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBangLuong_Edit";
            this.Tag = "frmSalary_Edit";
            this.Text = "frmSalary_Edit";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary_Edit)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Customizes.btgAccept btgAccept;
		private Epoint.Systems.Controls.dgvControl dgvSalary_Edit;

	}
}