using System.Resources;
using DataDynamics.ActiveReports.Chart;
using DataDynamics.ActiveReports;
using System.Data;
using System.Drawing;
using DataDynamics.ActiveReports.Chart.Graphics;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using DDCssLib;
namespace Epoint.Modules.HRM
{
    partial class rptReportChart
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
            if (disposing)
            {
               
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ResourceManager manager = new ResourceManager(typeof(rptReportChart));
            ChartArea area = new ChartArea();
            Axis axis = new Axis();
            Axis axis2 = new Axis();
            Axis axis3 = new Axis();
            Axis axis4 = new Axis();
            Axis axis5 = new Axis();
            Legend legend = new Legend();
            Title title = new Title();
            Title title2 = new Title();
            Series series = new Series();
            Title title3 = new Title();
            Title title4 = new Title();
            this.pageHeader = new PageHeader();
            this.detail = new Detail();
            this.pageFooter = new PageFooter();
            this.chartControl1 = new ChartControl();
            this.chartControl1.BeginInit();
            this.BeginInit();
            this.pageHeader.Controls.AddRange(new ARControl[] { this.chartControl1 });
            this.pageHeader.Height = 2.8125f;
            this.pageHeader.Name = "pageHeader";
            this.detail.ColumnSpacing = 0f;
            this.detail.Height = 0.0625f;
            this.detail.Name = "detail";
            this.pageFooter.Height = 0f;
            this.pageFooter.Name = "pageFooter";
            this.chartControl1.AutoRefresh = true;
            this.chartControl1.Backdrop = new BackdropItem(GradientType.Vertical, Color.White, Color.SteelBlue);
            this.chartControl1.Border.BottomColor = Color.Black;
            this.chartControl1.Border.BottomStyle = BorderLineStyle.None;
            this.chartControl1.Border.LeftColor = Color.Black;
            this.chartControl1.Border.LeftStyle = BorderLineStyle.None;
            this.chartControl1.Border.RightColor = Color.Black;
            this.chartControl1.Border.RightStyle = BorderLineStyle.None;
            this.chartControl1.Border.TopColor = Color.Black;
            this.chartControl1.Border.TopStyle = BorderLineStyle.None;
            area.AntiAliasMode = AntiAliasMode.Graphics;
            axis.AxisType = AxisType.Categorical;
            axis.LabelFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis.MajorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 1.0, 0f, false);
            axis.MinorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis.Title = "Axis X";
            axis.TitleFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis2.LabelFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis2.LabelsGap = 0;
            axis2.LabelsVisible = false;
            axis2.Line = new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0,DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None);
            axis2.MajorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis2.MinorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis2.Position = 0.0;
            axis2.TickOffset = 0.0;
            axis2.TitleFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis2.Visible = false;
            axis3.LabelFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis3.LabelsVisible = false;
            axis3.MajorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis3.MinorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis3.Position = 0.0;
            axis3.Title = "Axis Y";
            axis3.TitleFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f), -90f);
            axis4.LabelFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis4.LabelsVisible = false;
            axis4.Line = new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None);
            axis4.MajorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis4.MinorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis4.TitleFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis4.Visible = false;
            axis5.LabelFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis5.LabelsGap = 0;
            axis5.LabelsVisible = false;
            axis5.Line = new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None);
            axis5.MajorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis5.MinorTick = new Tick(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0.0, 0f, false);
            axis5.Position = 0.0;
            axis5.TickOffset = 0.0;
            axis5.TitleFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            axis5.Visible = false;
            area.Axes.AddRange(new AxisBase[] { axis, axis2, axis3, axis4, axis5 });
            area.Backdrop = new BackdropItem(BackdropStyle.Transparent, Color.White, Color.White, GradientType.Vertical, HatchStyle.DottedGrid, null, PicturePutStyle.Stretched);
            area.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0, Color.Black);
            area.Light = new Light(new Point3d(10f, 40f, 20f), LightType.InfiniteDirectional, 0.3f);
            area.Name = "defaultArea";
            area.Projection = new Projection(ProjectionType.Orthogonal, 0.1f, 0.1f, 0f, 37f);
            this.chartControl1.ChartAreas.AddRange(new ChartArea[] { area });
            this.chartControl1.ChartBorder = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0, Color.Black);
            this.chartControl1.Height = 2.8125f;
            this.chartControl1.Left = 0f;
            legend.Alignment = Alignment.Bottom;
            legend.Backdrop = new BackdropItem(Color.White, 0x80);
            legend.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(), 0, Color.Black);
            legend.DockArea = area;
            title.Backdrop = new Backdrop(BackdropStyle.Transparent, Color.White, Color.White, GradientType.Vertical, HatchStyle.DottedGrid, null, PicturePutStyle.Stretched);
            title.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0, Color.Black);
            title.DockArea = null;
            title.Font = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            title.Name = "";
            title.Text = "";
            title.Visible = false;
            legend.Footer = title;
            legend.GridLayout = new GridLayout(2, 4);
            title2.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.White, 2), 0, Color.Black);
            title2.DockArea = null;
            title2.Font = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            title2.Name = "";
            title2.Text = "Legend";
            title2.Visible = false;
            legend.Header = title2;
            legend.LabelsFont = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            legend.Name = "defaultLegend";
            this.chartControl1.Legends.AddRange(new Legend[] { legend });
            this.chartControl1.Name = "chartControl1";
            series.AxisX = axis;
            series.AxisY = axis3;
            series.ChartArea = area;
            series.ColorPalette = ColorPalette.Default;
            series.Legend = legend;
            series.LegendText = "";
            series.Name = "Series1";
            series.Properties = new CustomProperties(new KeyValuePair[] { new KeyValuePair("HoleSize", 0f), new KeyValuePair("Marker", new Marker(7, MarkerStyle.Triangle, new Backdrop(), new DataDynamics.ActiveReports.Chart.Graphics.Line(), new LabelInfo(new DataDynamics.ActiveReports.Chart.Graphics.Line(), new Backdrop(), new FontInfo(Color.Black, new Font("Arial", 8f)), "{Value}", Alignment.Bottom))), new KeyValuePair("BarType", BarType.Bar) });
            series.Type = ChartType.Doughnut3D;
            //series.Type = ChartType.StackedBar3D;
            this.chartControl1.Series.AddRange(new Series[] { series });
            title3.Alignment = Alignment.Center;
            title3.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0, Color.Black);
            title3.DockArea = null;
            title3.Font = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            title3.Name = "header";
            title3.Text = "Chart title";
            title3.Visible = false;
            title4.Border = new DataDynamics.ActiveReports.Chart.Border(new DataDynamics.ActiveReports.Chart.Graphics.Line(Color.Transparent, 0, DataDynamics.ActiveReports.Chart.Graphics.LineStyle.None), 0, Color.Black);
            title4.DockArea = null;
            title4.Docking = DockType.Bottom;
            title4.Font = new FontInfo(Color.Black, new Font("Microsoft Sans Serif", 8f));
            title4.Name = "footer";
            title4.Text = "Chart Footer";
            title4.Visible = false;
            this.chartControl1.Titles.AddRange(new Title[] { title3, title4 });
            this.chartControl1.Top = 0f;
            this.chartControl1.UIOptions = UIOptions.ForceHitTesting;
            //this.chartControl1.UIOptions = UIOptions.ContextCustomize;
            this.chartControl1.Width = 3.4375f;
            base.MasterReport = false;
            base.PageSettings.DefaultPaperSize = false;
            base.PageSettings.Margins.Bottom = 0f;
            base.PageSettings.Margins.Left = 0f;
            base.PageSettings.Margins.Right = 0f;
            base.PageSettings.Margins.Top = 0f;
            base.PageSettings.PaperHeight = 4f;
            base.PageSettings.PaperKind = PaperKind.Custom;
            base.PageSettings.PaperName = "Custom paper";
            base.PageSettings.PaperWidth = 4f;
            base.PrintWidth = 3.4375f;
            base.Sections.Add(this.pageHeader);
            base.Sections.Add(this.detail);
            base.Sections.Add(this.pageFooter);
            base.StyleSheet.Add(new StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; color: Black; ", "Normal"));
            base.StyleSheet.Add(new StyleSheetRule("font-size: 16pt; font-weight: bold; ", "Heading1", "Normal"));
            base.StyleSheet.Add(new StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: italic; ", "Heading2", "Normal"));
            base.StyleSheet.Add(new StyleSheetRule("font-size: 13pt; font-weight: bold; ", "Heading3", "Normal"));
            this.chartControl1.EndInit();
            this.EndInit();
        }

        private ChartControl chartControl1;
        private Detail detail;
        
        private PageFooter pageFooter;
        private PageHeader pageHeader;



 


        #endregion
    }
}
