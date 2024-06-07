namespace Epoint.Modules.AR
{
    partial class frmAPILog
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
            this.btgAccept = new Epoint.Systems.Customizes.btgAccept();
            this.ListOrder = new System.Windows.Forms.GroupBox();
            this.txtLog = new Epoint.Systems.Controls.txtTextBox();
            this.ListOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(684, 685);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(181, 29);
            this.btgAccept.TabIndex = 3;
            this.btgAccept.TabStop = false;
            // 
            // ListOrder
            // 
            this.ListOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListOrder.Controls.Add(this.txtLog);
            this.ListOrder.Location = new System.Drawing.Point(0, 6);
            this.ListOrder.Name = "ListOrder";
            this.ListOrder.Size = new System.Drawing.Size(882, 673);
            this.ListOrder.TabIndex = 89;
            this.ListOrder.TabStop = false;
            this.ListOrder.Text = "Lọc";
            // 
            // txtLog
            // 
            this.txtLog.bEnabled = true;
            this.txtLog.bIsLookup = false;
            this.txtLog.bReadOnly = false;
            this.txtLog.bRequire = false;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.KeyFilter = "";
            this.txtLog.Location = new System.Drawing.Point(3, 16);
            this.txtLog.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.txtLog.MaxLength = 100;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(876, 654);
            this.txtLog.TabIndex = 138;
            this.txtLog.UseAutoFilter = false;
            // 
            // frmAPILog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 722);
            this.Controls.Add(this.ListOrder);
            this.Controls.Add(this.btgAccept);
            this.Name = "frmAPILog";
            this.Text = "Chi tiết khuyến mãi";
            this.ListOrder.ResumeLayout(false);
            this.ListOrder.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Customizes.btgAccept btgAccept;        
        private System.Windows.Forms.GroupBox ListOrder;
        private Systems.Controls.txtTextBox txtLog;
    }
}