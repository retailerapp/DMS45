namespace Epoint.Modules.HRM
{
    partial class frmPmVPP
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnPmVPP = new System.Windows.Forms.Panel();
            this.pnPmVPP_Detail = new System.Windows.Forms.Panel();
            this.pnlControl2.SuspendLayout();
            this.splControl1.Panel2.SuspendLayout();
            this.splControl1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splControl1
            // 
            this.splControl1.Size = new System.Drawing.Size(883, 395);
            this.splControl1.SplitterDistance = 334;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnPmVPP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnPmVPP_Detail);
            this.splitContainer1.Size = new System.Drawing.Size(883, 395);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 1;
            // 
            // pnPmVPP
            // 
            this.pnPmVPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPmVPP.Location = new System.Drawing.Point(0, 0);
            this.pnPmVPP.Name = "pnPmVPP";
            this.pnPmVPP.Size = new System.Drawing.Size(883, 179);
            this.pnPmVPP.TabIndex = 0;
            // 
            // pnPmVPP_Detail
            // 
            this.pnPmVPP_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPmVPP_Detail.Location = new System.Drawing.Point(0, 0);
            this.pnPmVPP_Detail.Name = "pnPmVPP_Detail";
            this.pnPmVPP_Detail.Size = new System.Drawing.Size(883, 212);
            this.pnPmVPP_Detail.TabIndex = 0;
            // 
            // frmPmVPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 395);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmPmVPP";
            this.Text = "frmPmVPP";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.splControl1, 0);
            this.pnlControl2.ResumeLayout(false);
            this.splControl1.Panel2.ResumeLayout(false);
            this.splControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnPmVPP;
        private System.Windows.Forms.Panel pnPmVPP_Detail;
    }
}