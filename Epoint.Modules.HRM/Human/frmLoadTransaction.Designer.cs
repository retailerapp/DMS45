namespace Epoint.Modules.HRM
{
    partial class frmLoadTransaction
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btFile = new System.Windows.Forms.Button();
            this.btLoadData = new System.Windows.Forms.Button();
            this.grLoadText = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.btnLoadExceldata = new System.Windows.Forms.Button();
            this.btnExcelFile = new System.Windows.Forms.Button();
            this.grLoadText.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btgAccept
            // 
            this.btgAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btgAccept.Location = new System.Drawing.Point(334, 270);
            this.btgAccept.Margin = new System.Windows.Forms.Padding(2);
            this.btgAccept.Name = "btgAccept";
            this.btgAccept.Size = new System.Drawing.Size(171, 33);
            this.btgAccept.TabIndex = 119;
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(11, 19);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(414, 20);
            this.txtFile.TabIndex = 120;
            // 
            // btFile
            // 
            this.btFile.Location = new System.Drawing.Point(431, 16);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(24, 23);
            this.btFile.TabIndex = 121;
            this.btFile.Text = "...";
            this.btFile.UseVisualStyleBackColor = true;
            // 
            // btLoadData
            // 
            this.btLoadData.Location = new System.Drawing.Point(367, 45);
            this.btLoadData.Name = "btLoadData";
            this.btLoadData.Size = new System.Drawing.Size(88, 23);
            this.btLoadData.TabIndex = 121;
            this.btLoadData.Text = "Load";
            this.btLoadData.UseVisualStyleBackColor = true;
            // 
            // grLoadText
            // 
            this.grLoadText.Controls.Add(this.txtFile);
            this.grLoadText.Controls.Add(this.btLoadData);
            this.grLoadText.Controls.Add(this.btFile);
            this.grLoadText.Location = new System.Drawing.Point(9, 12);
            this.grLoadText.Name = "grLoadText";
            this.grLoadText.Size = new System.Drawing.Size(495, 117);
            this.grLoadText.TabIndex = 143;
            this.grLoadText.TabStop = false;
            this.grLoadText.Text = "Load TextFile";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtExcelFile);
            this.groupBox1.Controls.Add(this.btnLoadExceldata);
            this.groupBox1.Controls.Add(this.btnExcelFile);
            this.groupBox1.Location = new System.Drawing.Point(9, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 117);
            this.groupBox1.TabIndex = 143;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load ExcelFile";
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Location = new System.Drawing.Point(11, 19);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(414, 20);
            this.txtExcelFile.TabIndex = 120;
            // 
            // btnLoadExceldata
            // 
            this.btnLoadExceldata.Location = new System.Drawing.Point(367, 45);
            this.btnLoadExceldata.Name = "btnLoadExceldata";
            this.btnLoadExceldata.Size = new System.Drawing.Size(106, 23);
            this.btnLoadExceldata.TabIndex = 121;
            this.btnLoadExceldata.Text = "Load  excel data";
            this.btnLoadExceldata.UseVisualStyleBackColor = true;
            // 
            // btnExcelFile
            // 
            this.btnExcelFile.Location = new System.Drawing.Point(431, 16);
            this.btnExcelFile.Name = "btnExcelFile";
            this.btnExcelFile.Size = new System.Drawing.Size(24, 23);
            this.btnExcelFile.TabIndex = 121;
            this.btnExcelFile.Text = "...";
            this.btnExcelFile.UseVisualStyleBackColor = true;
            // 
            // frmLoadTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 316);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grLoadText);
            this.Controls.Add(this.btgAccept);
            this.Name = "frmLoadTransaction";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmLoadTransaction";
            this.grLoadText.ResumeLayout(false);
            this.grLoadText.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Epoint.Systems.Customizes.btgAccept btgAccept;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btFile;
        private System.Windows.Forms.Button btLoadData;
        private System.Windows.Forms.GroupBox grLoadText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.Button btnLoadExceldata;
        private System.Windows.Forms.Button btnExcelFile;

	}
}