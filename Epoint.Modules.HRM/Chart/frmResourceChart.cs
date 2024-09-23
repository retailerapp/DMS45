using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Chart;
using DataDynamics.ActiveReports.Design;
using DataDynamics.ActiveReports.Chart.Graphics;
using Epoint.Systems.Elements;
using Epoint.Systems.Customizes;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Lists;
using System.Collections;
using System.Data.SqlClient;

namespace Epoint.Modules.HRM
{
    public partial class frmResourceChart : Epoint.Systems.Customizes.frmView
    {
        string strMa_Ct;
        private static float viewDoanhThuHeight = 0f;
        private static float viewDoanhThuWidth = 0f;
        private static rptResourceChart rptChar = new rptResourceChart();

        public frmResourceChart()
        {
            InitializeComponent();

            btOk.Click += new EventHandler(btOk_Click);
            btCancel.Click += new EventHandler(btCancel_Click);
        }

        void cboChiTieu_SelectedValueChanged(object sender, EventArgs e)
        {
            
            
        }       
       

        new public void Load()
        {
            this.BindingLanguage();
            rptChar = new rptResourceChart();
            this.viewDoanhThu.HorizontalScroll.Visible = false;
            this.viewDoanhThu.VerticalScroll.Visible = false;
            this.viewDoanhThu.Document = rptChar.Document;


            LoadResourceChart();
            this.Show();
        }
        private static void LoadResourceChart()
        {
            try
            {

                DataTable dtResource = SQLExec.ExecuteReturnDt("EXEC sp_HRM_ResourceAnalysis");
                rptChar.fHeight = viewDoanhThuHeight;
                rptChar.fWidth = viewDoanhThuWidth;
                rptChar.SetLicense("RGN,RGN Warez Group,DD-APN-30-C01339,W44SSM949SWJ449HSHMF");
                rptChar.Load(dtResource, "ID", "Value");
                try
                {
                    rptChar.Run();
                }
                catch (Exception)
                {
                    rptChar.Run();
                }
            }
            catch(Exception ex)
            {
                EpointMessage.MsgOk(ex.ToString());
            }
        }


        void btOk_Click(object sender, EventArgs e)
        {
            Common.CloseCurrentForm();
            Application.Restart();
        }
        
        void btCancel_Click(object sender, EventArgs e)
        {
            Common.CloseCurrentForm();
        }
    }
}
