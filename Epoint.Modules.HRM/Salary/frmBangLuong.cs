using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Commons;
using Epoint.Systems.Data;
using Epoint.Systems.Librarys;
using Epoint.Systems.Elements;
using Epoint.Lists;

namespace Epoint.Modules.Salary
{
    public partial class frmBangLuong : Epoint.Systems.Customizes.frmView
    {
        private DataTable dtBangLuong;
        private DataTable dtDmTn;

        private BindingSource bdsBangLuong = new BindingSource();
        private DataRow drCurrent;
        private dgvControl dgvBangLuong = new dgvControl();

        public frmBangLuong()
        {
            InitializeComponent();

            cboThang.SelectedValueChanged += new EventHandler(cboThang_SelectedValueChanged);
            cboMa_Bp.TextChanged += new EventHandler(cboMa_Bp_TextChanged);

            btCreateSalary.Click += new EventHandler(btCreateSalary_Click);
            btCalcSalary.Click += new EventHandler(btCalcSalary_Click);
            btDeleteSalary.Click += new EventHandler(btDeleteSalary_Click);
            btPostedSalary.Click += new EventHandler(btPostedSalary_Click);
            btImport.Click += new EventHandler(btImport_Click);
            btnSendMail.Click += new EventHandler(btnSendMail_Click);

            radioButton1.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);

            dgvBangLuong.CellValidated += new DataGridViewCellEventHandler(dgvBangLuong_CellValidated);

            this.KeyDown += new KeyEventHandler(frmBangLuong_KeyDown);
        }

        void btnSendMail_Click(object sender, EventArgs e)
        {
            EpointProcessBox.Show(this);
        }

        void btImport_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofd.RestoreDirectory = true;

            if (Common.GetBufferValue("ImportExcelPathHRM") != string.Empty)
                ofd.InitialDirectory = Common.GetBufferValue("ImportExcelPathHRM");
            else
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Common.SetBufferValue("ImportExcelPathHRM", System.IO.Path.GetDirectoryName(ofd.FileName));
                }
                catch { }
                //string strConnectString =
                //    "Driver={Microsoft Excel Driver (*.xls, *.xlsx)};DBQ=" + ofd.FileName;

                //string strConnectString =
                //    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ofd.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX = 2\"";

                string strConnectString =
                    "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + ofd.FileName;

                OdbcConnection odbcConn = new OdbcConnection(strConnectString);
                odbcConn.Open();

                OdbcCommand odbcComm = new OdbcCommand();
                odbcComm.Connection = odbcConn;

                try
                {
                    OdbcDataAdapter odbcDA;
                    DataTable dtTestColumn = new DataTable();
                    DataTable dtImport = new DataTable();

                    //Kiểm tra tồn tại cột dữ liệu
                    odbcComm.CommandText = "SELECT * FROM [Sheet1$] WHERE 0 = 1";
                    odbcDA = new OdbcDataAdapter(odbcComm);
                    odbcDA.Fill(dtTestColumn);
                    //Kiểm tra xong

                    odbcComm.CommandText =
                        "SELECT * " +
                            " FROM [Sheet1$] ";

                    odbcDA = new OdbcDataAdapter(odbcComm);
                    odbcDA.Fill(dtImport);
                    odbcConn.Close();
                    int iColumn = dtImport.Columns.Count;

                    if (dtImport != null)
                    {

                        foreach (DataRow drImport in dtImport.Rows)
                        {
                            foreach (DataRow dr in dtBangLuong.Rows)
                            {

                                if (drImport["Ma_CbNv"].ToString() == dr["Ma_CbNv"].ToString())
                                {
                                    for (int i = 0; i < iColumn; i++)
                                    {
                                        string strColumnName = dtImport.Columns[i].ColumnName;
                                        if (dtTestColumn.Columns.Contains(strColumnName) && dtBangLuong.Columns.Contains(strColumnName))
                                        {
                                            dr[strColumnName] = drImport[strColumnName];
                                            if (dtDmTn.Select("Is_Input = true AND Ma_Tn = '" + strColumnName + "'").Length == 1)
                                            {
                                                Hashtable htPara = new Hashtable();
                                                htPara.Add("NAM", Element.sysWorkingYear);
                                                htPara.Add("THANG", this.cboThang.SelectedValue);
                                                htPara.Add("MA_CBNV", (string)dr["Ma_CbNv"]);
                                                htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                                                htPara.Add("MA_TN", strColumnName);
                                                htPara.Add("TIEN", Convert.ToDouble(dr[strColumnName]));

                                                if (SQLExec.Execute("sp_HRM_SaveBangLuong", htPara, CommandType.StoredProcedure))
                                                {
                                                    dr.AcceptChanges();
                                                }
                                            }
                                        }
                                        dr.AcceptChanges();
                                        //dtBangLuong.AcceptChanges;
                                    }
                                    //dtBangLuong.AcceptChanges;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không mở được bảng dữ liệu " + ofd.FileName + ex.Message);
                }
            }
        }

        public override void Load()
        {
            DataTable dtThang = SQLExec.ExecuteReturnDt("SELECT 0 Thang UNION SELECT DISTINCT MONTH(Ngay_Ct) AS Thang FROM HRSALARY WHERE YEAR(Ngay_Ct) = " + Element.sysWorkingYear.ToString());
            if (dtThang != null)
            {
                cboThang.ValueMember = "THANG";
                cboThang.DisplayMember = "THANG";
                cboThang.DataSource = dtThang;
                cboThang.SelectedIndex = cboThang.Items.Count - 1;
            }

            //Gắn Ma_Bp vào ComboBox
            DataTable dtDmBp = SQLExec.ExecuteReturnDt("SELECT '*' Ma_Bp, N'Tất cả' Ten_Bp UNION SELECT Ma_Bp, Ten_Bp FROM LIBOPHAN WHERE Nh_Cuoi = 1 ORDER BY Ma_Bp");
            //dtDmBp.Rows.Add(new string[] { "*", "Tất cả" });

            cboMa_Bp.lstItem.BuildListView("Ma_Bp:100,Ten_Bp:250");
            cboMa_Bp.lstItem.DataSource = dtDmBp;
            cboMa_Bp.lstItem.Size = new Size(320, cboMa_Bp.lstItem.Items.Count * 30);
            cboMa_Bp.lstItem.GridLines = true;

            this.Build();
            this.FillData();

            this.ShowBangLuong();

            this.Show();
        }

        #region Methods
        public override void EpointRelease()
        {
            string strMsg = string.Empty;
            DataTable dtPhieuLuong;


            Hashtable htPara = new Hashtable();
            htPara.Add("THANG", cboThang.SelectedValue);
            htPara.Add("NAM", Element.sysWorkingYear);
            htPara.Add("LANGUAGE_TYPE", Element.sysLanguage);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);

            dtPhieuLuong = SQLExec.ExecuteReturnDt("sp_HRM_SendMailPhieuLuong", htPara, CommandType.StoredProcedure);

            string strLuong_Cb, strLuong_Ng, strTong_Luong, strPhu_cap1, strPhu_cap2, strPhu_cap3, strTong_PC, strThuong,
                    strThue_TnCn, strLuong_Ung, strTong_Giam_tru, strLuong_TL, strTong_Giam;
            string strNgay_Cong, strNgay_Cong_Ng, strNgay_Cong_Le;
            string strMa_Cbnv,strTen_Cbnv;

            foreach (DataRow drP in dtPhieuLuong.Rows)
            {

                strLuong_Cb = Library.FormatNumber(Convert.ToDouble(drP["Luong_Chinh"]), 0, ".");
                strLuong_Ng = Library.FormatNumber(Convert.ToDouble(drP["Luong_Ng"]), 0, ".");
                strTong_Luong = Library.FormatNumber(Convert.ToDouble(drP["Tong_Luong"]), 0, ".");
                strPhu_cap1 = Library.FormatNumber(Convert.ToDouble(drP["Phucap1"]), 0, ".");
                strPhu_cap2 = Library.FormatNumber(Convert.ToDouble(drP["Phucap2"]), 0, ".");
                strPhu_cap3 = Library.FormatNumber(Convert.ToDouble(drP["Phucap3"]), 0, ".");
                strTong_PC = Library.FormatNumber(Convert.ToDouble(drP["Tong_Pc"]), 0, ".");
                strThuong = Library.FormatNumber(Convert.ToDouble(drP["Thuong"]), 0, ".");
                strThue_TnCn = Library.FormatNumber(Convert.ToDouble(drP["Thue_TNCN"]), 0, ".");
                strLuong_Ung = Library.FormatNumber(Convert.ToDouble(drP["Luong_Ung"]), 0, ".");
                strTong_Giam_tru = Library.FormatNumber(Convert.ToDouble(drP["Tong_giam_tru"]), 0, ".");
                strTong_Giam = Library.FormatNumber(Convert.ToDouble(drP["Tong_giam"]), 0, ".");
                strLuong_TL = Library.FormatNumber(Convert.ToDouble(drP["Luong_TL"]), 0, ".");

                strNgay_Cong = Library.FormatNumber(Convert.ToDouble(drP["Ngay_Cong"]), 0, ".");
                strNgay_Cong_Ng = Library.FormatNumber(Convert.ToDouble(drP["Ngay_Cong_Ng"]), 0, ".");
                strNgay_Cong_Le = Library.FormatNumber(Convert.ToDouble(drP["Ngay_Cong_Le"]), 0, ".");
                //string strBodyMail = Epoint.Modules.HRM.Properties.Resources.mail;
                strMa_Cbnv =drP["Ma_Cbnv"].ToString();
                strTen_Cbnv =drP["Ten_Cbnv"].ToString();


                //string strBody = String.Format(strBodyMail, drP["Ma_Cbnv"].ToString(), drP["Ten_Cbnv"].ToString(), "", "", "", strLuong_Cb, strLuong_Ng, strTong_Luong,
                //                        strPhu_cap1,strPhu_cap2,strPhu_cap3,strTong_PC,strThuong,strThue_TnCn,strTong_Giam,strLuong_Ung,strTong_Giam_tru,strLuong_TL);
                #region Format mail
                string strBody = @"<html xmlns:v='urn:schemas-microsoft-com:vml'
                                        xmlns:o='urn:schemas-microsoft-com:office:office'
                                        xmlns:dt='uuid:C2F41010-65B3-11d1-A29F-00AA00C14882'
                                        xmlns='http://www.w3.org/TR/REC-html40'>

                                        <head>
                                        <meta http-equiv=Content-Type content='text/html; charset=windows-1252'>
                                        <link rel=File-List href='index_files/filelist.xml'>
                                        <!--[if !mso]>
                                        <style>
                                        v\:* {behavior:url(#default#VML);}
                                        o\:* {behavior:url(#default#VML);}
                                        b\:* {behavior:url(#default#VML);}
                                        .shape {behavior:url(#default#VML);}
                                        </style>
                                        <![endif]-->
                                        <title>Blank</title>
                                        <style>
                                        <!--
                                         /* Font Definitions */
                                        @font-face
	                                        {font-family:'Times New Roman';
	                                        panose-1:2 2 6 3 5 4 5 2 3 4;}
                                         /* Style Definitions */
                                        p.MsoNormal, li.MsoNormal, div.MsoNormal
	                                        {margin-right:0pt;
	                                        text-indent:0pt;
	                                        margin-top:0pt;
	                                        margin-bottom:0pt;
	                                        text-align:left;
	                                        font-family:'Times New Roman';
	                                        font-size:10.0pt;
	                                        color:black;}
                                        ol
	                                        {margin-top:0in;
	                                        margin-bottom:0in;
	                                        margin-left:-2197in;}
                                        ul
	                                        {margin-top:0in;
	                                        margin-bottom:0in;
	                                        margin-left:-2197in;}
                                        @page
	                                        {size:8.0302in 11.0in;}
                                        -->
                                        </style>
                                        <!--[if gte mso 9]><xml>
                                         <o:shapedefaults v:ext='edit' spidmax='250887' fill='f' fillcolor='white [7]'
                                          strokecolor='black [0]'>
                                          <v:fill color='white [7]' color2='white [7]' on='f'/>
                                          <v:stroke color='black [0]' color2='white [7]'>
                                           <o:left v:ext='view' color='black [0]' color2='white [7]'/>
                                           <o:top v:ext='view' color='black [0]' color2='white [7]'/>
                                           <o:right v:ext='view' color='black [0]' color2='white [7]'/>
                                           <o:bottom v:ext='view' color='black [0]' color2='white [7]'/>
                                           <o:column v:ext='view' color='black [0]' color2='white [7]'/>
                                          </v:stroke>
                                          <v:shadow color='#ccc [4]'/>
                                          <v:textbox inset='2.88pt,2.88pt,2.88pt,2.88pt'/>
                                          <o:colormenu v:ext='edit' fillcolor='blue [1]' strokecolor='black [0]'
                                           shadowcolor='#ccc [4]'/>
                                         </o:shapedefaults><o:shapelayout v:ext='edit'>
                                          <o:idmap v:ext='edit' data='1'/>
                                         </o:shapelayout></xml><![endif]-->
                                        </head>

                                        <body link='#0066FF' vlink='#6633CC' style='margin:0'>

                                        <div style='position:absolute;width:7.-71in;height:4.1043in'>
                                        <!--[if gte vml 1]><![if mso | ie]><v:shapetype id='_x0000_t201' coordsize='21600,21600'
                                         o:spt='201' path='m,l,21600r21600,l21600,xe'>
                                         <v:stroke joinstyle='miter'/>
                                         <v:path shadowok='f' o:extrusionok='f' strokeok='f' fillok='f' o:connecttype='rect'/>
                                         <o:lock v:ext='edit' shapetype='t'/>
                                        </v:shapetype><v:shape id='_x0000_s1034' type='#_x0000_t201' style='position:absolute;
                                         left:50.25pt;top:75.74pt;width:503.25pt;height:252.47pt;z-index:3;
                                         mso-wrap-distance-left:2.88pt;mso-wrap-distance-top:2.88pt;
                                         mso-wrap-distance-right:2.88pt;mso-wrap-distance-bottom:2.88pt' stroked='f'
                                         strokecolor='black [0]' o:cliptowrap='t'>
                                         <v:stroke color2='white [7]'>
                                          <o:left v:ext='view' color='black [0]' color2='white [7]' weight='0'/>
                                          <o:top v:ext='view' color='black [0]' color2='white [7]' weight='0'/>
                                          <o:right v:ext='view' color='black [0]' color2='white [7]' weight='0'/>
                                          <o:bottom v:ext='view' color='black [0]' color2='white [7]' weight='0'/>
                                          <o:column v:ext='view' color='black [0]' color2='white [7]'/>
                                         </v:stroke>
                                         <v:shadow color='#ccc [4]'/>
                                         <o:lock v:ext='edit' rotation='t'/>
                                         <v:textbox inset='0,0,0,0'>
                                         </v:textbox>
                                        </v:shape><![endif]><![endif]-->

                                        <table v:shapes='_x0000_s1034' cellpadding=0 cellspacing=0 width=1000
                                         height=337 border=0 dir=ltr style='width:800.25pt;height:252.47pt;border-collapse:
                                         collapse;position:absolute;top:75.74pt;left:50.25pt;z-index:3'>
                                         <tr>
                                          <td width=671 height=29 valign=Top colspan=3 bgcolor='#CCCCCC'
                                          style='width:503.25pt;height:21.9457pt;padding-left:2.88pt;padding-right:
                                          2.88pt;padding-top:2.88pt;padding-bottom:2.88pt;background:#CCCCCC'>
                                          <p class=MsoNormal style='text-align:center;text-align:center'><span
                                          lang=en-US style='font-size:14.0pt;color:blue;font-weight:bold;language:en-US'>B&#7842;NG L&#431;&#416;NG THÁNG  " + cboThang.Text + @"/" + Element.sysWorkingYear + @" </span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=671 height=26 valign=Top colspan=3 style='width:503.25pt;
                                          height:19.6367pt;padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;
                                          padding-bottom:2.88pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'>Mã Nhân viên : </span><span lang=en-US style='font-size:12.0pt;
                                          language:en-US'>" + strMa_Cbnv + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=671 height=27 valign=Top colspan=3 style='width:503.25pt;
                                          height:19.6367pt;padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;
                                          padding-bottom:2.88pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'>Tên nhân viên : </span><span lang=en-US style='font-size:
                                          12.0pt;language:en-US'>" + strTen_Cbnv + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=26 valign=Top style='width:174.0pt;height:19.8087pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'><span dir=ltr></span>1./ L&#432;&#417;ng c&#417; b&#7843;n</span></p>
                                          </td>
                                          <td width=223 height=26 valign=Top style='width:167.25pt;height:19.8087pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'><span dir=ltr></span>2./ Các kho&#7843;n ph&#7909; c&#7845;p</span></p>
                                          </td>
                                          <td width=216 height=26 valign=Top style='width:162.0pt;height:19.8087pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'><span dir=ltr></span>4./ Các kho&#7843;n gi&#7843;m tr&#7915;</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=28 valign=Top style='width:174.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Ngày công :                   " + strNgay_Cong + @"</span></p>
                                          </td>
                                          <td width=232 height=28 valign=Top style='width:167.25pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Ph&#7909; c&#7845;p TN :    " + strPhu_cap1 + @"</span></p>
                                          </td>
                                          <td width=216 height=28 valign=Top style='width:162.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Thu&#7871; TNCN:    " + strThue_TnCn + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=45 valign=Top style='width:174.0pt;height:33.8575pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Ngày công ngoài:          " + strNgay_Cong_Ng + @"</span></p>
                                          </td>
                                          <td width=223 height=45 valign=Top style='width:167.25pt;height:33.8575pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>PC &#259;n tr&#432;a :      " + strPhu_cap2 + @"</span></p>
                                          </td>
                                          <td width=216 height=45 valign=Top style='width:162.0pt;height:33.8575pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>BHXH,BHYT,BHTN,KPCD: " + strTong_Giam + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=27 valign=Top style='width:174.0pt;height:20.0587pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Ngày công l&#7877;:                 " + strNgay_Cong_Le + @"</span></p>
                                          </td>
                                          <td width=232 height=27 valign=Top style='width:167.25pt;height:20.0587pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>PC X&#259;ng :         " + strPhu_cap3 + @"</span></p>
                                          </td>
                                          <td width=216 height=27 valign=Top style='width:162.0pt;height:20.0587pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>L&#432;&#417;ng &#7913;ng :     " + strLuong_Ung + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=28 valign=Top style='width:174.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>L&#432;&#417;ng c&#417; b&#7843;n:                " + strLuong_Cb + @"</span></p>
                                          </td>
                                          <td width=232 height=28 valign=Top style='width:167.25pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>T&#7893;ng ph&#7909; c&#7845;p : " + strTong_PC + @"</span></p>
                                          </td>
                                          <td width=216 height=28 valign=Top style='width:162.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>&nbsp;</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=28 valign=Top style='width:174.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>L&#432;&#417;ng ngoài gi&#7901;:          " + strLuong_Ng + @"</span></p>
                                          </td>
                                          <td width=232 height=28 valign=Top style='width:167.25pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>&nbsp;</span></p>
                                          </td>
                                          <td width=216 height=28 valign=Top style='width:162.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>&nbsp;</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=28 valign=Top style='width:174.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>T&#7893;ng l&#432;&#417;ng:                   " + strTong_Luong + @"</span></p>
                                          </td>
                                          <td width=232 height=28 valign=Top style='width:167.25pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;font-weight:bold;
                                          language:en-US'><span dir=ltr></span>3./ Th&#432;&#7903;ng :    </span><span
                                          lang=en-US style='font-size:12.0pt;language:en-US'>" + strThuong + @"</span><span
                                          lang=en-US style='font-size:12.0pt;font-weight:bold;language:en-US'> </span></p>
                                          </td>
                                          <td width=216 height=28 valign=Top style='width:162.0pt;height:20.9808pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-left:solid blue .5pt;border-top:solid blue .5pt;border-right:
                                          solid blue .5pt;border-bottom:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>T&#7893;ng gi&#7843;m tr&#7915;:  " + strTong_Giam_tru + @"</span></p>
                                          </td>
                                         </tr>
                                         <tr>
                                          <td width=232 height=45 valign=Top style='width:174.0pt;height:33.6075pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-top:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>Tông l&#432;&#417;ng th&#7921;c lãnh  :(1) + (2) + (3) - (4)</span></p>
                                          </td>
                                          <td width=232 height=45 valign=Top style='width:167.25pt;height:33.6075pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:   
                                          2.88pt;border-top:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:16.0pt;color:red;
                                          language:en-US'> " + strLuong_TL + @" (&#273;)</span></p>
                                          </td>
                                          <td width=216 height=45 valign=Top style='width:162.0pt;height:33.6075pt;
                                          padding-left:2.88pt;padding-right:2.88pt;padding-top:2.88pt;padding-bottom:
                                          2.88pt;border-top:solid blue .5pt'>
                                          <p class=MsoNormal><span lang=en-US style='font-size:12.0pt;language:en-US'>&nbsp;</span></p>
                                          </td>
                                         </tr>
                                        </table>


                                        </div>

                                        </body>

                                        </html>
                                ";
                #endregion


                string strHost = Parameters.GetParaValue("HOSTMAIL").ToString();//"smtp.gmail.com";
                string strCredentials = Parameters.GetParaValue("MAILSERVER").ToString();//"vanhungsmile@gmail.com";
                string strPassclient = Parameters.GetParaValue("PASS_EMAIL").ToString();//"";// tự đặt
                string strFrom = Parameters.GetParaValue("MAILSERVER").ToString(); //"vanhungsmile@gmail.com";
                string strTo = drP["Email"].ToString();//"hungnv@epoint.com.vn";
                string strSubject = "Bảng lương tháng " + cboThang.Text + "/" + Element.sysWorkingYear;
                EpointProcessBox.AddMessage("Đang gửi " + strMa_Cbnv + " - " + strTen_Cbnv);
                strMsg = Common.SendMail(strHost, strCredentials, strPassclient, strFrom, "Nhân sự"
                                , strTo, strSubject, strBody);
                EpointProcessBox.AddMessage(strMsg);
            }
            EpointProcessBox.AddMessage("Kết thúc ! ");

        }
        private void Build()
        {
            //Build
            dgvBangLuong.strZone = "BANGLUONG";
            dgvBangLuong.Dock = DockStyle.Fill;
            dgvBangLuong.BuildGridView();
            dgvBangLuong.Columns["Ma_CbNv"].Frozen = true;
            dgvBangLuong.Columns["Ten_CbNv"].Frozen = true;

            this.panel1.Controls.Add(dgvBangLuong);
        }

        private void FillData()
        {
            //Lấy cấu trúc các cột
            dtDmTn = SQLExec.ExecuteReturnDt("SELECT * FROM HRPARALIST ORDER BY Stt");

            //Lấy nội dung bảng lương
            Hashtable htPara = new Hashtable();
            htPara.Add("THANG", cboThang.SelectedValue);
            htPara.Add("NAM", Element.sysWorkingYear);
            htPara.Add("MA_BP", cboMa_Bp.Text);
            htPara.Add("LANGUAGE_TYPE", Element.sysLanguage);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);

            dtBangLuong = SQLExec.ExecuteReturnDt("sp_HRM_GetBangLuong", htPara, CommandType.StoredProcedure);

            bdsBangLuong.DataSource = dtBangLuong;
            dgvBangLuong.DataSource = bdsBangLuong;
            this.ExportControl = dgvBangLuong;
            bdsSearch = bdsBangLuong;
        }

        private void ShowBangLuong()
        {
            //1-THANHTOAN
            //2-THUNHAP
            //3-TRULUONG

            dgvBangLuong.ReadOnly = false;

            for (int i = 0; i < dgvBangLuong.Columns.Count; i++)
            {
                string strColumnName = dgvBangLuong.Columns[i].Name;
                int iCount = dtDmTn.Select("Ma_Tn = '" + strColumnName + "'").Length;

                if (iCount == 1)
                {
                    DataRow dr = dtDmTn.Select("Ma_Tn = '" + strColumnName + "'")[0];

                    if (radioButton1.Checked)
                        dgvBangLuong.Columns[strColumnName].Visible = (bool)dr["Is_Display1"];
                    else if (radioButton2.Checked)
                        dgvBangLuong.Columns[strColumnName].Visible = (bool)dr["Is_Display2"];
                    else
                        dgvBangLuong.Columns[strColumnName].Visible = (bool)dr["Is_Display3"];

                    dgvBangLuong.Columns[i].ReadOnly = !(bool)dr["Is_Input"];
                    dgvBangLuong.Columns[i].DefaultCellStyle.ForeColor = (bool)dr["Is_Input"] ? System.Drawing.Color.Blue : SystemColors.WindowText;

                    if ((bool)dr["Bold"])
                    {
                        dgvBangLuong.Columns[i].DefaultCellStyle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                    }
                }
                else
                {
                    dgvBangLuong.Columns[i].ReadOnly = true;
                }
            }
        }

        private void CalSalary()
        {
            Hashtable htPara = new Hashtable();

            htPara.Add("THANG", cboThang.SelectedValue);
            htPara.Add("NAM", Element.sysWorkingYear);
            htPara.Add("MA_BP", cboMa_Bp.Text);
            htPara.Add("MA_CBNV", string.Empty);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);

            SQLExec.Execute("sp_HRM_CalcBangLuong", htPara, CommandType.StoredProcedure);

            this.FillData();
        }

        public override void Edit(enuEdit enuNew_Edit)
        {
            if (bdsBangLuong.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            if (enuNew_Edit == enuEdit.New)
                return;

            if (bdsBangLuong.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsBangLuong.Current).Row, ref drCurrent);
            else
                drCurrent = dtBangLuong.NewRow();

            frmBangLuong_Edit frmEdit = new frmBangLuong_Edit();
            frmEdit.Load(drCurrent, Convert.ToInt32(this.cboThang.SelectedValue));

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsBangLuong.Position >= 0)
                        dtBangLuong.ImportRow(drCurrent);
                    else
                        dtBangLuong.Rows.Add(drCurrent);

                    bdsBangLuong.Position = bdsBangLuong.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsBangLuong.Current).Row);
                }

                dtBangLuong.AcceptChanges();
            }
        }

        public override void Delete()
        {
            if (bdsBangLuong.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsBangLuong.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            Hashtable htPara = new Hashtable();
            htPara.Add("THANG", cboThang.SelectedValue);
            htPara.Add("NAM", Element.sysWorkingYear);
            htPara.Add("MA_BP", cboMa_Bp.Text);
            htPara.Add("MA_CBNV", drCurrent["Ma_CbNv"]);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);

            if (SQLExec.Execute("sp_HRM_DeleteBangLuong", htPara, CommandType.StoredProcedure))
            {
                bdsBangLuong.RemoveAt(bdsBangLuong.Position);
                dtBangLuong.AcceptChanges();
            }
        }

        #endregion

        #region Event

        void btCalcSalary_Click(object sender, EventArgs e)
        {
            this.CalSalary();
        }

        void btCreateSalary_Click(object sender, EventArgs e)
        {
            frmBangLuong_Create frmCreate = new frmBangLuong_Create();
            frmCreate.Load(Convert.ToInt32(this.cboThang.SelectedValue), cboMa_Bp.Text.Trim(), string.Empty);

            if (frmCreate.isAccept)
            {
                //Load lai cboThang
                DataTable dtThang = SQLExec.ExecuteReturnDt("SELECT 0 Thang UNION SELECT DISTINCT MONTH(Ngay_Ct) AS Thang FROM HRSALARY WHERE YEAR(Ngay_Ct) = " + Element.sysWorkingYear.ToString());
                if (dtThang != null)
                {
                    cboThang.ValueMember = "THANG";
                    cboThang.DisplayMember = "THANG";
                    cboThang.DataSource = dtThang;
                    cboThang.SelectedIndex = cboThang.Items.Count - 1;
                }

                this.FillData();
            }
        }

        void btDeleteSalary_Click(object sender, EventArgs e)
        {
            string strMess = Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có chắc chắn xóa bảng lương tháng ?" : "Are you sure delete salary table month ?" + this.cboThang.SelectedValue;

            if (Common.MsgYes_No(strMess, "N"))
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("THANG", cboThang.SelectedValue);
                htPara.Add("NAM", Element.sysWorkingYear);
                htPara.Add("MA_BP", cboMa_Bp.Text);
                htPara.Add("MA_CBNV", string.Empty);
                htPara.Add("MA_DVCS", Element.sysMa_DvCs);

                if (SQLExec.Execute("sp_HRM_DeleteBangLuong", htPara, CommandType.StoredProcedure))
                {                    
                    //Load lai cboThang
                    DataTable dtThang = SQLExec.ExecuteReturnDt("SELECT 0 Thang UNION SELECT DISTINCT MONTH(Ngay_Ct) AS Thang FROM HRSALARY WHERE YEAR(Ngay_Ct) = " + Element.sysWorkingYear.ToString());
                    if (dtThang != null)
                    {
                        cboThang.ValueMember = "THANG";
                        cboThang.DisplayMember = "THANG";
                        cboThang.DataSource = dtThang;
                        cboThang.SelectedIndex = cboThang.Items.Count - 1;
                    }

                    this.FillData();
                }
            }
        }

        void btPostedSalary_Click(object sender, EventArgs e)
        {
            frmBangLuong_Posted frmPosted = new frmBangLuong_Posted();
            frmPosted.Load(Convert.ToInt32(this.cboThang.SelectedValue));

            if (frmPosted.isAccept)
            {
                Common.MsgOk(Languages.GetLanguage("End_Process"));
                Common.EndShowStatus();
            }
        }

        void frmBangLuong_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F12)
            //{

            //}
        }

        void cboThang_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == cboThang)
                this.FillData();
        }

        void cboMa_Bp_TextChanged(object sender, EventArgs e)
        {
            if (cboMa_Bp.lviItem != null)
                lbtTen_Bp.Text = cboMa_Bp.lviItem.SubItems["Ten_Bp"].Text;

            if (cboMa_Bp.Text == string.Empty)
                return;

            this.FillData();
        }

        void dgvBangLuong_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            drCurrent = ((DataRowView)bdsBangLuong.Current).Row;
            DataGridViewCell dgvCell = ((dgvControl)sender).CurrentCell;
            string strColumnName = dgvCell.OwningColumn.Name.ToUpper();

            if (!dgvCell.IsInEditMode)
                return;

            if ((string)drCurrent["Ma_CbNv"] == string.Empty)
                return;

            if (dtDmTn.Select("Is_Input = true AND Ma_Tn = '" + strColumnName + "'").Length == 1)
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("NAM", Element.sysWorkingYear);
                htPara.Add("THANG", this.cboThang.SelectedValue);
                htPara.Add("MA_CBNV", (string)drCurrent["Ma_CbNv"]);
                htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                htPara.Add("MA_TN", strColumnName);
                htPara.Add("TIEN", dgvCell.Value);

                if (SQLExec.Execute("sp_HRM_SaveBangLuong", htPara, CommandType.StoredProcedure))
                {
                    drCurrent.AcceptChanges();
                }
            }
        }

        void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowBangLuong();
        }

        #endregion
    }
}
