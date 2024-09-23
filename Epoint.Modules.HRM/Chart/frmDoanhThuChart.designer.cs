using System.Windows.Forms;
using System.Drawing;

namespace Epoint.Modules.HRM
{
    partial class frmDoanhThuChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHumanChart));
            this.btOk = new Epoint.Systems.Controls.btControl();
            this.btCancel = new Epoint.Systems.Controls.btControl();
            this.viewDoanhThu = new DataDynamics.ActiveReports.Viewer.Viewer();
            this.cboChiTieu = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Image = ((System.Drawing.Image)(resources.GetObject("btOk.Image")));
            this.btOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOk.Location = new System.Drawing.Point(446, 389);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(76, 29);
            this.btOk.TabIndex = 4;
            this.btOk.Tag = "ACCEPT";
            this.btOk.Text = "&Đồng ý";
            this.btOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Image = ((System.Drawing.Image)(resources.GetObject("btCancel.Image")));
            this.btCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btCancel.Location = new System.Drawing.Point(528, 389);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(76, 29);
            this.btCancel.TabIndex = 6;
            this.btCancel.Tag = "Cancel";
            this.btCancel.Text = "&Hủy bỏ";
            this.btCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // viewDoanhThu
            // 
            this.viewDoanhThu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewDoanhThu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.viewDoanhThu.BackColor = System.Drawing.SystemColors.Control;
            this.viewDoanhThu.Document = new DataDynamics.ActiveReports.Document.Document("ARNet Document");
            this.viewDoanhThu.Location = new System.Drawing.Point(0, 52);
            this.viewDoanhThu.Name = "viewDoanhThu";
            this.viewDoanhThu.ReportViewer.CurrentPage = 0;
            this.viewDoanhThu.ReportViewer.MultiplePageCols = 3;
            this.viewDoanhThu.ReportViewer.MultiplePageMode = true;
            this.viewDoanhThu.ReportViewer.MultiplePageRows = 2;
            this.viewDoanhThu.ReportViewer.RepositionPage = true;
            this.viewDoanhThu.ReportViewer.RulerVisible = false;
            this.viewDoanhThu.ReportViewer.ViewType = DataDynamics.ActiveReports.Viewer.ViewType.ContinuousScroll;
            this.viewDoanhThu.Size = new System.Drawing.Size(635, 378);
            this.viewDoanhThu.TabIndex = 7;
            this.viewDoanhThu.TableOfContents.Text = "Table Of Contents";
            this.viewDoanhThu.TableOfContents.Width = 200;
            this.viewDoanhThu.TabTitleLength = 35;
            this.viewDoanhThu.Toolbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewDoanhThu.Toolbar.Visible = false;
            // 
            // cboChiTieu
            // 
            this.cboChiTieu.FormattingEnabled = true;
            this.cboChiTieu.Location = new System.Drawing.Point(145, 25);
            this.cboChiTieu.Name = "cboChiTieu";
            this.cboChiTieu.Size = new System.Drawing.Size(354, 21);
            this.cboChiTieu.TabIndex = 8;
            // 
            // frmHumanChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(635, 430);
            this.Controls.Add(this.cboChiTieu);
            this.Controls.Add(this.viewDoanhThu);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Name = "frmHumanChart";
            this.Text = "frmHumanChart";
            this.ResumeLayout(false);

		}

		#endregion

		private Epoint.Systems.Controls.btControl btOk;
        private Epoint.Systems.Controls.btControl btCancel;
        private DataDynamics.ActiveReports.Viewer.Viewer viewDoanhThu;
        private ComboBox cboChiTieu;
	}
}