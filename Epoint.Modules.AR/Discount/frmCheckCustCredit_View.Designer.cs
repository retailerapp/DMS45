namespace Epoint.Modules.AR
{
    partial class frmCheckCustCredit_View
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
            this.ListOrder.Location = new System.Drawing.Point(0, 6);
            this.ListOrder.Name = "ListOrder";
            this.ListOrder.Size = new System.Drawing.Size(882, 673);
            this.ListOrder.TabIndex = 89;
            this.ListOrder.TabStop = false;
            this.ListOrder.Text = "Lọc";
            // 
            // frmCheckCustCredit_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 722);
            this.Controls.Add(this.ListOrder);
            this.Controls.Add(this.btgAccept);
            this.Name = "frmCheckCustCredit_View";
            this.Text = "Chi tiết hạn mức công nợ";
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Customizes.btgAccept btgAccept;        
        private System.Windows.Forms.GroupBox ListOrder;

	}
}