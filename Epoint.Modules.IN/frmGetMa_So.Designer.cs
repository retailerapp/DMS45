namespace Epoint.Modules.IN
{
	partial class frmGetMa_So
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetMa_So));
            this.dgvViewCt = new Epoint.Systems.Controls.dgvControl();
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewCt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvViewCt
            // 
            this.dgvViewCt.AllowUserToAddRows = false;
            this.dgvViewCt.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvViewCt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvViewCt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViewCt.BackgroundColor = System.Drawing.Color.White;
            this.dgvViewCt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvViewCt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewCt.EnableHeadersVisualStyles = false;
            this.dgvViewCt.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvViewCt.Location = new System.Drawing.Point(2, 2);
            this.dgvViewCt.Margin = new System.Windows.Forms.Padding(1);
            this.dgvViewCt.MultiSelect = false;
            this.dgvViewCt.Name = "dgvViewCt";
            this.dgvViewCt.ReadOnly = true;
            this.dgvViewCt.Size = new System.Drawing.Size(788, 533);
            this.dgvViewCt.strZone = "";
            this.dgvViewCt.TabIndex = 0;
            // 
            // btgAccept
            // 
            this.btgAccept.Location = new System.Drawing.Point(594, 539);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(170, 30);
            this.btgAccept.TabIndex = 1;
            this.btgAccept.TabStop = false;
            // 
            // frmGetMa_So
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 571);
            this.Controls.Add(this.btgAccept);
            this.Controls.Add(this.dgvViewCt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGetMa_So";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Tag = "frmGetMa_So";
            this.Text = "frmGetMa_So";
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewCt)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Controls.dgvControl dgvViewCt;
		private Epoint.Systems.Customizes.btgAccept btgAccept;

	}
}