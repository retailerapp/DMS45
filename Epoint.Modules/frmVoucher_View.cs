using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Commons;
using System.Data.OleDb;
using System.Data.SqlClient;
using Epoint.Systems.Customizes;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using System.Linq;
using System.Globalization;

namespace Epoint.Modules
{
    public partial class frmVoucher_View : Epoint.Systems.Customizes.frmView
    {
        #region Fields
        Color usd_color = Color.FromArgb(255, 49, 106, 197);
        public bool IsFormatGrid = true;
        public bool isFormated = false;
        public DataSet dsVoucher = new DataSet("dsVoucher");

        public DataTable dtViewPh;
        public DataTable dtViewCt;

        public BindingSource bdsViewPh = new BindingSource();
        public BindingSource bdsViewCt = new BindingSource();

        public dgvVoucherGrid dgvViewPh = new dgvVoucherGrid();
        public dgvVoucherGrid dgvViewCt = new dgvVoucherGrid();

        //public dgvVoucher dgvViewPh = new dgvVoucher();
        //public dgvVoucher dgvViewCt = new dgvVoucher();

        public DataRelation drlView;
        public string strModule = string.Empty;
        public string strMa_Ct_List = string.Empty;
        public string strStt_List = string.Empty;
        public string strMa_Kho_Access_List = string.Empty;
        public bool IsDmsInvoice = false;
        public DataRow drCurrent;
        public DataRow drDmCt;
        public DataTable dtSttList = null;

        public string strFilterKey_Old = string.Empty;
        private DataTable dtDataExcelImport = null;
        bool bGetDefColImport = false;
        DataTable dtDefineRow_Pos;
        DataTable dtDefineCol_Pos;
        DataTable dtDefineCol_DataType;
        #endregion

        #region Contructor

        public frmVoucher_View()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            this.SetFormatGrid();
            this.Resize += new EventHandler(frmViewPh_Resize);
            this.KeyDown += new KeyEventHandler(KeyDownEvent);

            bdsViewPh.PositionChanged += new EventHandler(bdsViewPh_PositionChanged);

            btPreview.Click += new EventHandler(btPreview_Click);
            btPrint.Click += new EventHandler(btPrint_Click);
            btFilter.Click += new EventHandler(btFilter_Click);
            //btExit.Click += new EventHandler(btExit_Click);
            btBack.Click += new EventHandler(btBack_Click);
            btImport.Click += new EventHandler(btImport_Click);
            dgvViewPh.dgvGridView.Click += new EventHandler(dgvViewPh_CellMouseClick);
            //dgvViewPh.dgvGridView.CustomDrawCell += new RowCellCustomDrawEventHandler(dgvViewPh_CellFormatting);
            //dgvViewPh.dgvGridView.RowCellStyle += dgvViewPh_RowCellStyle;
            dgvViewPh.Enter += new EventHandler(dgvViewPh_Enter);
            dgvViewCt.Enter += new EventHandler(dgvViewCt_Enter);
            if (this.IsFormatGrid)
                dgvViewPh.dgvGridView.CustomDrawCell += new RowCellCustomDrawEventHandler(dgvViewPh_CellFormatting);
            this.SetColor();

        }

        public void Load(string strMa_Ct_List)
        {
            this.strMa_Ct_List = strMa_Ct_List;
            this.Object_ID = strMa_Ct_List;
            this.strMa_Kho_Access_List = SQLExec.ExecuteReturnValue("  SELECT TOP 1 Ma_Kho_Access  FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'").ToString();

            if (Common.Inlist(this.strMa_Ct_List, "IN,INT,SC"))
            {
                this.IsDmsInvoice = true;
                btImport.Visible = true;
            }

            this.Build();

            object objNgay_CtMax = SQLExec.ExecuteReturnValue("SELECT TOP 1 (Ngay_Ct) FROM " + (string)drDmCt["Table_Ph"] + " WHERE Ma_Ct LIKE '" + this.strMa_Ct_List.Split(',')[0] + "' AND Ma_DvCs = '" + Element.sysMa_DvCs + "' ORDER BY Ngay_Ct DESC");
            int iInterval = Convert.ToInt32(Parameters.GetParaValue("DAY_FILTER"));
            string sYear = Convert.ToString(Parameters.GetParaValue("YEAR_FILTER"));
            string sMonth = Convert.ToString(Parameters.GetParaValue("MONTH_FILTER"));

            DateTime dteNgay_Ct2 = objNgay_CtMax == null || objNgay_CtMax == DBNull.Value ? DateTime.Now : (DateTime)objNgay_CtMax;
            DateTime dteNgay_Ct1 = dteNgay_Ct2.Subtract(new TimeSpan(iInterval, 0, 0, 0));

            string strFilterKey = string.Empty;
            strFilterKey += "(Ma_Ct IN ('" + strMa_Ct_List.Replace(",", "','") + "'))";

            //Thông: Hiển thị chứng từ theo năm hoặc theo ngày
            if (sYear == "1")
                strFilterKey += " AND (YEAR(Ngay_Ct) =  " + Element.sysWorkingYear + ")";
            else
                strFilterKey += " AND (Ngay_Ct BETWEEN  '" + Library.DateToStr(dteNgay_Ct1) + "' AND '" + Library.DateToStr(dteNgay_Ct2) + "')";

            if (sMonth == "1")
                strFilterKey += " AND (MONTH(Ngay_Ct) =  " + DateTime.Now.Month + ") ";

            strFilterKey += " AND (Ma_DvCs = '" + Element.sysMa_DvCs + "')";

            //strFilterKey_Old -> btBack
            strFilterKey_Old = strFilterKey;

            if (Common.Inlist(strMa_Ct_List, "BG,SO"))
            {
                this.FillDataBG(strFilterKey, strFilterKey);
            }
            else if (this.IsDmsInvoice)
            {
                this.FillData_OM(strFilterKey, strFilterKey);
                btnPXK.Visible = true;
                btDiscoutDetail.Visible = true;

                if (strMa_Ct_List == "INT")
                {
                    btnPXK.AutoSize = true;
                    btnPXK.Text = "Xác nhận trả";
                }
            }
            else if (Common.Inlist(strMa_Ct_List, "PTT"))
            {
                this.FillData(strFilterKey, strFilterKey);
                btnPXK.Visible = true;
                btnPXK.AutoSize = true;
                btnPXK.Text = "Hủy Thanh Toán";
            }
            else
                this.FillData(strFilterKey, strFilterKey);
            this.BindingLanguage();
            this.BindingTong_Tien();

            //if (strMa_Ct_List == "PC")
            //    this.dgvViewPh.FormatCell = false;
            //string strMa_Ct_Access = SQLExec.ExecuteReturnValue("SELECT Ma_Ct_Access FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "' AND MEMBER_TYPE = 'U'").ToString();
            //if (!Element.sysIs_Admin)
            //    if (Common.Inlist(strMa_Ct_List, strMa_Ct_Access))
            //    if (!Common.CheckPermission("ACCESS_PRICE", enuPermission_Type.Allow_Access))
            //    {
            //        dgvViewCt.Columns["GIA_NT9"].Visible = false;
            //        dgvViewCt.Columns["TIEN_NT9"].Visible = false;
            //        dgvViewPh.Columns["TTIEN"].Visible = false;
            //    }
            this.FormLayout();

            this.Show();
        }

        #endregion

        #region Build, FillData

        private void Build()
        {
            string strMa_Ct = strMa_Ct_List.Split(',')[0];
            lbtStt.Text = string.Empty;
            drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strMa_Ct);

            //dgvViewPh             
            dgvViewPh.ReadOnly = true;
            dgvViewPh.strZone = (string)drDmCt["Zone_ViewPh"];

            dgvViewPh.BuildGridView(false);

            //if (!dgvViewPh.Columns.Contains("Ma_Tte"))
            //{
            //    dgvViewPh.Columns.Add("Ma_Tte", "Ma_Tte"); //Thêm cột để phân biệt chứng từ Nte, VND
            //    dgvViewPh.Columns["Ma_Tte"].DataPropertyName = "MA_TTE";
            //    dgvViewPh.Columns["Ma_Tte"].ValueType = typeof(string);
            //    dgvViewPh.Columns["Ma_Tte"].Visible = false;
            //}

            //dgvViewPh.Columns.Add("Mark", "Mark"); //Đánh dấu dòng
            //dgvViewPh.Columns["Mark"].DataPropertyName = "MARK";
            //dgvViewPh.Columns["Mark"].ValueType = typeof(string);
            //dgvViewPh.Columns["Mark"].Visible = false;

            //dgvViewCt
            dgvViewCt.ReadOnly = true;
            dgvViewCt.strZone = (string)drDmCt["Zone_ViewCt"];

            dgvViewCt.BuildGridView(false);

            //Position
            this.Controls.Add(dgvViewPh);
            this.Controls.Add(dgvViewCt);
            dgvViewPh.TabIndex = 0;
            dgvViewCt.TabIndex = 1;

        }

        private void FillData(string strKey_Ph, string strKey_Ct)
        {
            bool bINewLoad = DataTool.SQLCheckExist("sys.procedures", "Name", "sp_Vourcher_GetDataBegin");
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];
            //if (!Element.sysIs_Admin)
            //{
            //    if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
            //    {
            //        strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
            //        // bỏ chỗ này xem có nhanh hơn không
            //        //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
            //    }

            //    //if ((this.strModule == "02" || this.strModule == "05") && this.strMa_Kho_Access_List != string.Empty)
            //    //{
            //    //    strKey_Ph += @" AND EXISTS
            //    //            (
            //    //                SELECT TOP 1 1 FROM " + strKey_Ct + @" AS Ct
            //    //                WHERE Ct.Stt = h.Stt AND
            //    //                (AR.Ma_Kho  LIKE ''' + REPLACE(" + this.strMa_Kho_Access_List + ", ', ', ''' OR Ma_Kho LIKE ''') + '''))'";

            //    //}
            //}          


            if (bINewLoad && Common.Inlist(this.strModule, "01,02,05"))// NewLoad way
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("MA_CT", strMa_Ct_List);
                htPara.Add("TABLE_CT", strTable_Ct);
                htPara.Add("KEY", strKey_Ph);
                htPara.Add("USER_ID", Element.sysUser_Id);
                htPara.Add("IS_ADMIN", Element.sysIs_Admin);
                htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                DataSet ds = SQLExec.ExecuteReturnDs("sp_Vourcher_GetDataBegin", htPara, CommandType.StoredProcedure);

                dtViewPh = ds.Tables[0].Copy();
                dtViewCt = ds.Tables[1].Copy();

                dtViewPh.TableName = strTable_Ph;
                dtViewCt.TableName = strTable_Ct;

                dsVoucher.Tables.Clear();
                dsVoucher.Tables.Add(dtViewPh);
                dsVoucher.Tables.Add(dtViewCt);
            }
            else //OldLoad Way
            {
                if (!Element.sysIs_Admin)
                {
                    if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                    {
                        strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                        // bỏ chỗ này xem có nhanh hơn không
                        //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                    }
                    else
                    {
                        string strUser_Access = (string)SQLExec.ExecuteReturnValue("SELECT Member_Access FROM SYSMEMBER WHERE Member_ID = '" + Element.sysUser_Id + "'") + ",";

                        if (!strUser_Access.Contains("*,"))// *: Xem chứng từ của tất cả user, ngược lại là chỉ xem được các chứng từ của user cho phép
                        {
                            string[] User = strUser_Access.Split(new char[] { ',' });
                            string strUser = string.Empty;
                            foreach (string u in User)
                            {
                                if (u.Trim() != "")
                                    strUser += "'" + u + "',";
                            }
                            //Cong them User hien tai dang log vao -> user dang log vao co the thay chung tu cua chinh minh va chung tu cua user khác duoc cho phep truy xuat
                            strUser = strUser + "'" + Element.sysUser_Id + "'";
                            strKey_Ph += " AND SUBSTRING(Create_Log,15,LEN(Create_Log)) IN (" + strUser + ")";
                            strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) IN (" + strUser + "))";
                        }
                    }
                }
                string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, CAST(0 AS BIT) AS CHON ,CAST(0 AS BIT) AS Mark,SUBSTRING(Create_Log,15,LEN(Create_Log)) AS User_Create";
                dtViewPh = DataTool.SQLGetDataTable(strTable_Ph, strSelectPh, strKey_Ph, "Ngay_Ct, So_Ct");
                dtViewPh.TableName = strTable_Ph;

                bdsViewPh.DataSource = dtViewPh;
                dgvViewPh.DataSource = bdsViewPh;

                dtViewCt = DataTool.SQLGetDataTable(strTable_Ct, "*", strKey_Ct, "Ngay_Ct");
                dtViewCt.TableName = strTable_Ct;


                dsVoucher.Tables.Clear();
                dsVoucher.Tables.Add(dtViewPh);
                dsVoucher.Tables.Add(dtViewCt);


            }


            //Thêm tổng tiền ở phía dưới
            if (dtViewCt.Columns.Contains("TTien_Nt") && dtViewCt.Columns.Contains("TTien_Nt3"))
            {
                DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
                dcNew.Expression = "Tien + Tien3";
                dtViewCt.Columns.Add(dcNew);

                dcNew = new DataColumn("TTIEN_NT", typeof(double));
                dcNew.Expression = "Tien_Nt + Tien_Nt3";
                dtViewCt.Columns.Add(dcNew);
            }

            bdsViewPh.DataSource = dtViewPh;
            dgvViewPh.DataSource = bdsViewPh;

            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;


            //Thêm tổng tiền ở phía dưới
            //if (!dtViewPh.Columns.Contains("FORE_COLOR"))
            //{
            //    DataColumn dcNew = new DataColumn("FORE_COLOR", typeof(string));
            //    dtViewPh.Columns.Add(dcNew);              
            //}



            //bdsViewCt.DataSource = dtViewCt;
            //dgvViewCt.DataSource = bdsViewCt;

            //dsVoucher.Tables.Clear();
            //dsVoucher.Tables.Add(dtViewPh);
            //dsVoucher.Tables.Add(dtViewCt);

            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);


            DataRow[] arrdrViewCt;
            DataRow drViewCt;
            foreach (DataRow drViewPh in dtViewPh.Rows)
            {
                string strStt = (string)drViewPh["Stt"];
                arrdrViewCt = dtViewCt.Select("Stt = '" + strStt + "'");

                if (arrdrViewCt.Length > 0)
                    drViewCt = arrdrViewCt[0];
                else
                    continue;

                Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);

                //drViewPh["FORE_COLOR"] = drViewPh["MA_TTE"].ToString() != Element.sysMa_Tte ? "RED" : "";
            }

            bdsViewPh.MoveLast();

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        private void FillData_OM(string strKey_Ph, string strKey_Ct)
        {
            bool bINewLoad = DataTool.SQLCheckExist("sys.procedures", "Name", "sp_OM_GetDataBegin");
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];
            if (!bINewLoad)// OldLoad way
            {
                if (!Element.sysIs_Admin)
                {
                    if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                    {
                        strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                        // bỏ chỗ này xem có nhanh hơn không
                        //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                    }
                }

                string strSelectPh = @"SELECT h.*, ISNULL(d.Ma_PX,'') AS Ma_Px,dt.Ma_Tuyen,Ten_Tuyen = Isnull(ty.Ten_Tuyen,'Không có'), TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, TTien_Nt9 =  TTien0 + TTien3 + TTien4 ,  
                                    CAST(0 AS BIT) AS Mark ,CAST(0 AS BIT) AS CHON, SUBSTRING(h.Create_Log,15,LEN(h.Create_Log)) AS User_Create										
									FROM " + strTable_Ph + @" h   (NOLOCK)
									LEFT JOIN (SELECT Stt,Ma_PX FROM OM_PXKDetail  (NOLOCK)) d on d.Stt = h.Stt                                   
                                    INNER JOIN LIDOITUONG dt (NOLOCK) on dt.Ma_Dt = h.Ma_Dt  
                                    LEFT JOIN LITUYEN ty  (NOLOCK) ON dt.Ma_Tuyen = ty.Ma_Tuyen 
                                    WHERE " + strKey_Ph + @"
                                    ORDER BY h.Ngay_Ct, h.So_Ct
									";


                dtViewPh = SQLExec.ExecuteReturnDt(strSelectPh);

                dtViewPh.TableName = strTable_Ph;

                //bdsViewPh.DataSource = dtViewPh;
                //dgvViewPh.DataSource = bdsViewPh;

                dtViewCt = DataTool.SQLGetDataTable(strTable_Ct, "*", strKey_Ct, "Ngay_Ct");
                dtViewCt.TableName = strTable_Ct;

                dsVoucher.Tables.Clear();
                dsVoucher.Tables.Add(dtViewPh);
                dsVoucher.Tables.Add(dtViewCt);
            }
            else //NewLoad Way
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("MA_CT", strMa_Ct_List);
                htPara.Add("KEY", strKey_Ph);
                htPara.Add("USER_ID", Element.sysUser_Id);
                htPara.Add("IS_ADMIN", Element.sysIs_Admin);
                htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                DataSet ds = SQLExec.ExecuteReturnDs("sp_OM_GetDataBegin", htPara, CommandType.StoredProcedure);

                dtViewPh = ds.Tables[0].Copy();
                dtViewCt = ds.Tables[1].Copy();
                dtViewPh.TableName = strTable_Ph;
                dtViewCt.TableName = strTable_Ct;

                dsVoucher.Tables.Clear();
                dsVoucher.Tables.Add(dtViewPh);
                dsVoucher.Tables.Add(dtViewCt);

            }
            //Thêm tổng tiền ở phía dưới
            if (dtViewCt.Columns.Contains("TTien_Nt") && dtViewCt.Columns.Contains("TTien_Nt3"))
            {
                DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
                dcNew.Expression = "Tien + Tien3";
                dtViewCt.Columns.Add(dcNew);

                dcNew = new DataColumn("TTIEN_NT", typeof(double));
                dcNew.Expression = "Tien_Nt + Tien_Nt3";
                dtViewCt.Columns.Add(dcNew);
            }


            bdsViewPh.DataSource = dtViewPh;
            dgvViewPh.DataSource = bdsViewPh;
            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;



            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);


            DataRow[] arrdrViewCt;
            DataRow drViewCt;
            foreach (DataRow drViewPh in dtViewPh.Rows)
            {
                string strStt = (string)drViewPh["Stt"];
                arrdrViewCt = dtViewCt.Select("Stt = '" + strStt + "'");

                if (arrdrViewCt.Length > 0)
                    drViewCt = arrdrViewCt[0];
                else
                    continue;

                Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);
            }
            //dgvViewPh.dgvGridView.Focus();
            //dgvViewPh.dgvGridView.SelectRow(dgvViewPh.dgvGridView.RowCount - 1);
            bdsViewPh.MoveLast();

            int value = dgvViewPh.dgvGridView.RowCount - 1;
            dgvViewPh.dgvGridView.TopRowIndex = value;
            dgvViewPh.dgvGridView.FocusedRowHandle = value;
            //dgvViewPh.dgvGridView.MoveLast();
            //

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        private void FillData_IN(string strKey_Ph, string strKey_Ct)
        {
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];
            if (!Element.sysIs_Admin)
            {
                if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                {
                    strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                    // bỏ chỗ này xem có nhanh hơn không
                    //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                }

                if ((this.strModule == "02" || this.strModule == "05") && this.strMa_Kho_Access_List != string.Empty)
                {
                    strKey_Ph += @" AND EXISTS
                            (
                                SELECT TOP 1 1 FROM " + strKey_Ct + @" AS Ct
                                WHERE Ct.Stt = h.Stt AND
                                (AR.Ma_Kho  LIKE ''' + REPLACE(" + this.strMa_Kho_Access_List + ", ', ', ''' OR Ma_Kho LIKE ''') + '''))'";

                }
            }

            string strSelectPh = " *, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, CAST(0 AS BIT) AS CHON ,CAST(0 AS BIT) AS Mark,SUBSTRING(Create_Log,15,LEN(Create_Log)) AS User_Create";
            dtViewPh = DataTool.SQLGetDataTable(strTable_Ph, strSelectPh, strKey_Ph, "Ngay_Ct, So_Ct");
            dtViewPh.TableName = strTable_Ph;

            bdsViewPh.DataSource = dtViewPh;
            dgvViewPh.DataSource = bdsViewPh;

            dtViewCt = DataTool.SQLGetDataTable(strTable_Ct, "*", strKey_Ct, "Ngay_Ct");
            dtViewCt.TableName = strTable_Ct;

            //Thêm tổng tiền ở phía dưới
            if (dtViewCt.Columns.Contains("TTien_Nt") && dtViewCt.Columns.Contains("TTien_Nt3"))
            {
                DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
                dcNew.Expression = "Tien + Tien3";
                dtViewCt.Columns.Add(dcNew);

                dcNew = new DataColumn("TTIEN_NT", typeof(double));
                dcNew.Expression = "Tien_Nt + Tien_Nt3";
                dtViewCt.Columns.Add(dcNew);
            }


            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;

            dsVoucher.Tables.Clear();
            dsVoucher.Tables.Add(dtViewPh);
            dsVoucher.Tables.Add(dtViewCt);

            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);


            DataRow[] arrdrViewCt;
            DataRow drViewCt;
            foreach (DataRow drViewPh in dtViewPh.Rows)
            {
                string strStt = (string)drViewPh["Stt"];
                arrdrViewCt = dtViewCt.Select("Stt = '" + strStt + "'");

                if (arrdrViewCt.Length > 0)
                    drViewCt = arrdrViewCt[0];
                else
                    continue;

                Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);

                //drViewPh["FORE_COLOR"] = drViewPh["MA_TTE"].ToString() != Element.sysMa_Tte ? "RED" : "";
            }

            bdsViewPh.MoveLast();

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        private void FillDataIN(string strKey, bool temp)
        {
            bool bINewLoad = DataTool.SQLCheckExist("sys.procedures", "Name", "sp_OM_GetDataBegin");
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];
            if (!bINewLoad)// OldLoad way
            {
                if (!Element.sysIs_Admin)
                {
                    if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                    {
                        strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                        // bỏ chỗ này xem có nhanh hơn không
                        //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                    }
                }

                string strSelectPh = @"SELECT h.*, ISNULL(d.Ma_PX,'') AS Ma_Px,dt.Ma_Tuyen,Ten_Tuyen = Isnull(ty.Ten_Tuyen,'Không có'), TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, TTien_Nt9 =  TTien0 + TTien3 + TTien4 ,  CAST(0 AS BIT) AS Mark ,CAST(0 AS BIT) AS CHON, SUBSTRING(h.Create_Log,15,LEN(h.Create_Log)) AS User_Create
										
									FROM " + strTable_Ph + @" h   (NOLOCK)
									LEFT JOIN (SELECT Stt,Ma_PX FROM OM_PXKDetail  (NOLOCK)) d on d.Stt = h.Stt                                   
                                    INNER JOIN LIDOITUONG dt (NOLOCK) on dt.Ma_Dt = h.Ma_Dt  
                                    LEFT JOIN LITUYEN ty  (NOLOCK) ON dt.Ma_Tuyen = ty.Ma_Tuyen 
                                    WHERE " + strKey + @"
                                    ORDER BY h.Ngay_Ct, h.So_Ct
									";


                dtViewPh = SQLExec.ExecuteReturnDt(strSelectPh);

                dtViewPh.TableName = strTable_Ph;

                //bdsViewPh.DataSource = dtViewPh;
                //dgvViewPh.DataSource = bdsViewPh;

                dtViewCt = DataTool.SQLGetDataTable(strTable_Ct, "*", strKey_Ct, "Ngay_Ct");
                dtViewCt.TableName = strTable_Ct;

                dsVoucher.Tables.Clear();
                dsVoucher.Tables.Add(dtViewPh);
                dsVoucher.Tables.Add(dtViewCt);
            }
            else //NewLoad Way
            {
                Hashtable htPara = new Hashtable();
                htPara.Add("MA_CT", strMa_Ct_List);
                htPara.Add("KEY", strKey);
                htPara.Add("USER_ID", Element.sysUser_Id);
                htPara.Add("IS_ADMIN", Element.sysIs_Admin);
                htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                dsVoucher = SQLExec.ExecuteReturnDs("sp_OM_GetDataBegin", htPara, CommandType.StoredProcedure);

                dtViewPh = dsVoucher.Tables[0].Copy();
                dtViewCt = dsVoucher.Tables[1].Copy();
                dtViewPh.TableName = strTable_Ph;
                dtViewCt.TableName = strTable_Ct;


                //bdsViewPh.DataSource = dtViewPh;
                //dgvViewPh.DataSource = bdsViewPh;
            }
            //Thêm tổng tiền ở phía dưới
            if (dtViewCt.Columns.Contains("TTien_Nt") && dtViewCt.Columns.Contains("TTien_Nt3"))
            {
                DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
                dcNew.Expression = "Tien + Tien3";
                dtViewCt.Columns.Add(dcNew);

                dcNew = new DataColumn("TTIEN_NT", typeof(double));
                dcNew.Expression = "Tien_Nt + Tien_Nt3";
                dtViewCt.Columns.Add(dcNew);
            }
            bdsViewPh.DataSource = dtViewPh;
            dgvViewPh.DataSource = bdsViewPh;
            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;



            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);


            DataRow[] arrdrViewCt;
            DataRow drViewCt;
            foreach (DataRow drViewPh in dtViewPh.Rows)
            {
                string strStt = (string)drViewPh["Stt"];
                arrdrViewCt = dtViewCt.Select("Stt = '" + strStt + "'");

                if (arrdrViewCt.Length > 0)
                    drViewCt = arrdrViewCt[0];
                else
                    continue;

                Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);
            }
            //dgvViewPh.dgvGridView.Focus();
            //dgvViewPh.dgvGridView.SelectRow(dgvViewPh.dgvGridView.RowCount - 1);
            bdsViewPh.MoveLast();

            int value = dgvViewPh.dgvGridView.RowCount - 1;
            dgvViewPh.dgvGridView.TopRowIndex = value;
            dgvViewPh.dgvGridView.FocusedRowHandle = value;
            //dgvViewPh.dgvGridView.MoveLast();
            //

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        private void FillDataBG(string strKey_Ph, string strKey_Ct)
        {
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];


            string strSelectPh = string.Empty;

            if (strMa_Ct_List == "BG")
                strSelectPh = @"SELECT *, CAST(0 AS BIT) AS Is_Used, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, CAST(0 AS BIT) AS Mark 
										INTO #T_Ph
									FROM " + strTable_Ph + " WHERE " + strKey_Ph + @"
									UPDATE #T_Ph SET
										Is_Used = 1
										WHERE So_Ct IN (SELECT DISTINCT So_Ct_Bg FROM ARSO)

									SELECT * FROM #T_Ph ORDER BY Ngay_Ct, So_Ct
									DROP TABLE #T_Ph";

            else if (strMa_Ct_List == "SO")
                strSelectPh = @"SELECT *, CAST(0 AS BIT) AS Is_Used, TTien0 + TTien3 AS TTien, TTien_Nt0 + TTien_Nt3 AS TTien_Nt, CAST(0 AS BIT) AS Mark 
										INTO #T_Ph
									FROM " + strTable_Ph + " WHERE " + strKey_Ph + @"
									UPDATE #T_Ph SET
										Is_Used = 1
										WHERE Stt IN (SELECT DISTINCT Stt FROM MACTLSX)

									SELECT * FROM #T_Ph ORDER BY Ngay_Ct, So_Ct
									DROP TABLE #T_Ph";


            dtViewPh = SQLExec.ExecuteReturnDt(strSelectPh);

            dtViewPh.TableName = strTable_Ph;

            bdsViewPh.DataSource = dtViewPh;
            dgvViewPh.DataSource = bdsViewPh;

            bdsViewPh.Filter = "Is_Used = 0";

            dtViewCt = DataTool.SQLGetDataTable(strTable_Ct, "*", strKey_Ct, "Ngay_Ct");
            dtViewCt.TableName = strTable_Ct;

            //Thêm tổng tiền ở phía dưới
            if (dtViewCt.Columns.Contains("TTien_Nt") && dtViewCt.Columns.Contains("TTien_Nt3"))
            {
                DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
                dcNew.Expression = "Tien + Tien3";
                dtViewCt.Columns.Add(dcNew);

                dcNew = new DataColumn("TTIEN_NT", typeof(double));
                dcNew.Expression = "Tien_Nt + Tien_Nt3";
                dtViewCt.Columns.Add(dcNew);
            }

            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;

            dsVoucher.Tables.Clear();
            dsVoucher.Tables.Add(dtViewPh);
            dsVoucher.Tables.Add(dtViewCt);

            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);
            foreach (DataRow drViewPh in dtViewPh.Rows)
            {
                string strStt = (string)drViewPh["Stt"];
                DataRow drViewCt = dtViewCt.Select("Stt = '" + strStt + "'")[0];

                Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);
            }

            bdsViewPh.MoveLast();

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        private void FillDataKiemKe(string strKey)
        {
            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];


            string strSelectPh = string.Empty;

            Hashtable htPara = new Hashtable();
            htPara.Add("MA_CT", strMa_Ct_List);
            htPara.Add("KEY", strKey);
            htPara.Add("USER_ID", Element.sysUser_Id);
            htPara.Add("IS_ADMIN", Element.sysIs_Admin);
            htPara.Add("MA_DVCS", Element.sysMa_DvCs);
            dsVoucher = SQLExec.ExecuteReturnDs("sp_IN_GetDataKiemKe", htPara, CommandType.StoredProcedure);

            dtViewPh = dsVoucher.Tables[0].Copy();
            dtViewCt = dsVoucher.Tables[1].Copy();

            dtViewPh.TableName = strTable_Ph;
            dtViewCt.TableName = strTable_Ct;

            //bdsViewPh.DataSource = dtViewPh;
            //dgvViewPh.DataSource = bdsViewPh;

            //Thêm tổng tiền ở phía dưới
            //if (dtViewCt.Columns.Contains("Tien") && dtViewCt.Columns.Contains("Tien_KK"))
            //{
            //    DataColumn dcNew = new DataColumn("TTIEN", typeof(double));
            //    dcNew.Expression = "Tien";
            //    dtViewCt.Columns.Add(dcNew);

            //    dcNew = new DataColumn("TTIEN_KK", typeof(double));
            //    dcNew.Expression = "Tien_KK";
            //    dtViewCt.Columns.Add(dcNew);
            //}

            bdsViewCt.DataSource = dtViewCt;
            dgvViewCt.DataSource = bdsViewCt;

            //dsVoucher.Tables.Clear();
            //dsVoucher.Tables.Add(dtViewPh);
            //dsVoucher.Tables.Add(dtViewCt);

            //Lay du lieu tu Ct len Ph theo danh sach Carry_Header
            //Common.CopyDataColumn(dtViewCt, dtViewPh, (string)drDmCt["Update_Header"]);
            //foreach (DataRow drViewPh in dtViewPh.Rows)
            //{
            //    string strStt = (string)drViewPh["Stt"];
            //    DataRow drViewCt = dtViewCt.Select("Stt = '" + strStt + "'")[0];

            //    Common.CopyDataRow(drViewCt, drViewPh, (string)drDmCt["Update_Header"]);
            //}

            bdsViewPh.MoveLast();

            this.bdsSearch = bdsViewPh;
            this.ExportControl = dgvViewPh;
        }
        public void Filter()
        {
            DataTable dtFilter = new DataTable();

            dtFilter.Columns.Add(new DataColumn("Ma_Ct", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ngay_Ct1", typeof(DateTime)));
            dtFilter.Columns.Add(new DataColumn("Ngay_Ct2", typeof(DateTime)));
            dtFilter.Columns.Add(new DataColumn("So_Ct1", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("So_Ct2", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tien1", typeof(double)));
            dtFilter.Columns.Add(new DataColumn("Tien2", typeof(double)));
            dtFilter.Columns.Add(new DataColumn("Dien_Giai", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Tte", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tk", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("No_Co", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tk_Du", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Thue", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Hd", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Dt", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Km", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Bp", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Sp", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Nx", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Kho", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Vt", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_CbNv", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Job", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Tc", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Kv", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Nvu", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Table", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Is_Thue_Vat", typeof(bool)));
            dtFilter.Columns.Add(new DataColumn("Is_Nb", typeof(bool)));
            dtFilter.Columns.Add(new DataColumn("Ma_DvCs", typeof(string)));

            DataRow drFilter = dtFilter.NewRow();

            //Set Default 
            drFilter["Ma_Ct"] = strMa_Ct_List;
            drFilter["Ngay_Ct1"] = Element.sysNgay_Ct1;
            drFilter["Ngay_Ct2"] = Element.sysNgay_Ct2;

            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];

            drFilter["Table"] = strTable_Ct;
            drFilter["Ma_DvCs"] = Element.sysMa_DvCs;

            if (!this.Filter_ShowForm(drFilter))
                return;

            string strKey = (string)SQLExec.ExecuteReturnValue("sp_GetVoucherFilterKey", drFilter, CommandType.StoredProcedure);

            string strKey_Ph, strKey_Ct, strUserFilter_Ph = string.Empty, strUserFilter_Ct = string.Empty;
            if (drFilter["Is_Thue_Vat"].ToString() != string.Empty)
            {
                if ((bool)drFilter["Is_Thue_Vat"])
                    strKey_Ph = "Stt IN (" + strKey + " AND (Is_Thue_Vat = 1))";
                else if ((bool)drFilter["Is_Nb"])
                    strKey_Ph = "Stt IN (" + strKey + " AND (Is_Thue_Vat = 0))";
                else
                    strKey_Ph = "Stt IN (" + strKey + ")";
            }
            else
                strKey_Ph = "Stt IN (" + strKey + ")";

            strKey_Ct = "Stt IN (" + strKey + ")";

            if (!Element.sysIs_Admin)
            {
                if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                {
                    strUserFilter_Ph = "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                    strUserFilter_Ct = " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";

                    //strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                    //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                }
            }
            if (Common.Inlist(strMa_Ct_List, "BG,SO"))
                this.FillDataBG(strFilterKey_Old, strFilterKey_Old);
            else if (this.IsDmsInvoice)
            {
                strKey_Ph = "h." + strKey_Ph;
                this.FillData_OM(strKey_Ph, strKey_Ct);
            }
            else
            {
                strKey_Ph += strUserFilter_Ph;
                strKey_Ct += strUserFilter_Ct;
                this.FillData(strKey_Ph, strKey_Ct);
            }
            Element.sysNgay_Ct1 = Convert.ToDateTime(drFilter["Ngay_Ct1"]);
            Element.sysNgay_Ct2 = Convert.ToDateTime(drFilter["Ngay_Ct2"]);
        }
        public void Filter(bool isNew)
        {
            DataTable dtFilter = new DataTable();

            dtFilter.Columns.Add(new DataColumn("Ma_Ct", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ngay_Ct1", typeof(DateTime)));
            dtFilter.Columns.Add(new DataColumn("Ngay_Ct2", typeof(DateTime)));
            dtFilter.Columns.Add(new DataColumn("So_Ct1", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("So_Ct2", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tien1", typeof(double)));
            dtFilter.Columns.Add(new DataColumn("Tien2", typeof(double)));
            dtFilter.Columns.Add(new DataColumn("Dien_Giai", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Tte", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tk", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("No_Co", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Tk_Du", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Thue", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Hd", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Dt", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Km", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Bp", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Sp", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Nx", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Kho", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Vt", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_CbNv", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Job", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Tc", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Ma_Kv", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Table", typeof(string)));
            dtFilter.Columns.Add(new DataColumn("Is_Thue_Vat", typeof(bool)));
            dtFilter.Columns.Add(new DataColumn("Is_Nb", typeof(bool)));
            dtFilter.Columns.Add(new DataColumn("Ma_DvCs", typeof(string)));

            DataRow drFilter = dtFilter.NewRow();

            //Set Default 
            drFilter["Ma_Ct"] = strMa_Ct_List;
            drFilter["Ngay_Ct1"] = Element.sysNgay_Ct1;
            drFilter["Ngay_Ct2"] = Element.sysNgay_Ct2;

            string strTable_Ph = (string)drDmCt["Table_Ph"];
            string strTable_Ct = (string)drDmCt["Table_Ct"];

            drFilter["Table"] = strTable_Ct;
            drFilter["Ma_DvCs"] = Element.sysMa_DvCs;

            if (!this.Filter_ShowForm(drFilter))
                return;

            string strKey = (string)SQLExec.ExecuteReturnValue("sp_GetVoucherFilterKeyNew", drFilter, CommandType.StoredProcedure);
            string strSelectCt = "SELECT TOP 1 Stt  FROM " + strTable_Ct + " AS Table_Ct WHERE ";
            string strKey_Ph, strKey_Ct, strUserFilter_Ph = string.Empty, strUserFilter_Ct = string.Empty;
            if (drFilter["Is_Thue_Vat"].ToString() != string.Empty)
            {
                if ((bool)drFilter["Is_Thue_Vat"])
                {
                    //strKey_Ph = "Stt IN (" + strKey + " AND (Is_Thue_Vat = 1))";
                    strKey_Ph = " EXISTS  (" + strSelectCt + " h.Stt = Table_Ct.Stt AND " + strKey + " AND (Is_Thue_Vat = 1))";
                }
                else if ((bool)drFilter["Is_Nb"])
                {
                    //strKey_Ph = "Stt IN (" + strKey + " AND (Is_Thue_Vat = 0))";
                    strKey_Ph = " EXISTS  (" + strSelectCt + " h.Stt = Table_Ct.Stt AND " + strKey + "AND (Is_Thue_Vat = 0))";
                }
                else
                {
                    strKey_Ph = " EXISTS  (" + strSelectCt + " h.Stt = Table_Ct.Stt AND " + strKey + ")";
                }
            }
            else
                strKey_Ph = " EXISTS  (" + strSelectCt + " h.Stt = Table_Ct.Stt AND " + strKey + ")";

            strKey_Ct = "Stt IN (" + strKey + ")";

            if (!Element.sysIs_Admin)
            {
                if (!Common.CheckPermission("MEMBER_ID_ALLOW", enuPermission_Type.Allow_Access))
                {
                    strUserFilter_Ph = "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                    strUserFilter_Ct = " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";

                    //strKey_Ph += "AND SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "'";
                    //strKey_Ct += " AND Stt IN (SELECT Stt FROM " + strTable_Ph + " WHERE SUBSTRING(Create_Log,15,LEN(Create_Log)) = '" + Element.sysUser_Id + "')";
                }
            }
            if (Common.Inlist(strMa_Ct_List, "BG,SO"))
                this.FillDataBG(strFilterKey_Old, strFilterKey_Old);
            else if (this.IsDmsInvoice) //Common.Inlist(strMa_Ct_List, "IN,INT")
            {
                //strKey_Ph = "h." + strKey_Ph;
                this.FillData_OM(strKey_Ph, strKey_Ct);
            }
            else
            {
                strKey_Ph += strUserFilter_Ph;
                strKey_Ct += strUserFilter_Ct;
                this.FillData(strKey_Ph, strKey_Ct);
            }
            Element.sysNgay_Ct1 = Convert.ToDateTime(drFilter["Ngay_Ct1"]);
            Element.sysNgay_Ct2 = Convert.ToDateTime(drFilter["Ngay_Ct2"]);
        }

        public void FillDataNew()
        {
            if (Common.Inlist(strMa_Ct_List, "BG,SO"))
                this.FillDataBG(strFilterKey_Old, strFilterKey_Old);
            else if (Common.Inlist(strMa_Ct_List, "IN,INT,SC"))
                this.FillData_OM(strFilterKey_Old, strFilterKey_Old);
            else
                this.FillData(strFilterKey_Old, strFilterKey_Old);
        }

        public virtual bool Filter_ShowForm(DataRow drFilter)
        {
            frmFilter frm = new frmFilter();
            frm.Load(drFilter);

            return frm.isAccept;
        }

        private void Print(bool bPreview)
        {
            if (bdsViewPh.Position < 0)
                return;

            drCurrent = ((DataRowView)bdsViewPh.Current).Row;
            string stt = string.Empty;
            DataRow[] drArrPrint;
            bool bAcceptShowDialog = true;
            bool bInVisibleNextPrint = false;
            string strReport_File_First = string.Empty;


            if (this.IsDmsInvoice)
            {
                drArrPrint = dtViewPh.Select("MARK = true");

                //if (dgvViewPh.dgvGridView.SelectedRowsCount > 1)
                //{                   
                //    foreach(int i in  dgvViewPh.dgvGridView.GetSelectedRows())
                //    {
                //        stt = dgvViewPh.dgvGridView.GetRowCellValue(i, "STT").ToString();
                //        DataRow drP = DataTool.SQLGetDataRowByID("GLVOUCHER", "STT", stt);
                //        PrintVoucher.Print(drP, bPreview, true, ref bInVisibleNextPrint, ref strReport_File_First);
                //    }
                //    return;
                //}             
            }
            else
                drArrPrint = dtViewPh.Select("CHON = true");

            if (drArrPrint.Length > 1)
            {
                for (int i = 0; i < drArrPrint.Length; i++)
                {
                    drCurrent = drArrPrint[i];

                    if (i == 0)
                    {
                        bAcceptShowDialog = PrintVoucher.Print(drCurrent, bPreview, true, ref bInVisibleNextPrint, ref strReport_File_First);
                    }
                    else
                    {
                        if (bAcceptShowDialog)
                            bAcceptShowDialog = PrintVoucher.Print(drCurrent, bPreview, false, ref bInVisibleNextPrint, ref strReport_File_First);
                        else
                            break;
                    }

                    if (bAcceptShowDialog)
                    {
                        if (this.IsDmsInvoice)
                            drCurrent["MARK"] = false;
                        else
                            drCurrent["CHON"] = false;
                    }
                }
            }
            else
                PrintVoucher.Print(drCurrent, bPreview, true, ref bInVisibleNextPrint, ref strReport_File_First);
        }
        private void PrintRpt(bool bPreview)
        {
            if (bdsViewPh.Position < 0)
                return;

            drCurrent = ((DataRowView)bdsViewPh.Current).Row;
            string stt = drCurrent["Stt"].ToString();
            string rptFileName = Application.StartupPath + @"\Reports\CT_IN_Report.rpt";
            DataRow[] drArrPrint;
            bool bAcceptShowDialog = true;
            bool bInVisibleNextPrint = false;
            string strReport_File_First = string.Empty;

            PrintVoucher.Print_Crytal(stt, rptFileName, bPreview, true, null);
        }
        private void SetColor()
        {
            this.BackColor = Color.FromArgb(34, 32, 11, 43);
        }
        private void SetFormatGrid()
        {

            if (Collection.Parameters.ContainsKey("VOURCHER_FORMAT") && Collection.Parameters["VOURCHER_FORMAT"].ToString() == "0")
            {
                this.isFormated = false;
            }

            if (!Common.Inlist(strMa_Ct_List, "NM,NK,PT,PC,BN,BC"))
                this.isFormated = false;
        }
        private void Design()
        {
            string strMa_Ct = strMa_Ct_List.Split(',')[0];

            DataRow drDmCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", strMa_Ct);

            DataTable dtDmMauIn = DataTool.SQLGetDataTable("SYSDMMAUCT", "Ma_Mau, Ten_Mau", "Ma_Ct = '" + strMa_Ct + "'", "Ma_Mau");
            string strReport_File = (string)dtDmMauIn.Rows[0]["Ma_Mau"];

            if (dtDmMauIn.Rows.Count > 1)
            {
                frmDesign_Mau_Ct frmDesign = new frmDesign_Mau_Ct();
                frmDesign.strMa_Ct = strMa_Ct;
                frmDesign.Is_Design = false;
                frmDesign.Load();
                strReport_File = frmDesign.strMa_Mau;

                if (frmDesign.Is_Design)
                {
                    Epoint.Reports.frmReportDesign frm = new Epoint.Reports.frmReportDesign();
                    frm.Load(strReport_File);
                }
            }
            else
            {
                Epoint.Reports.frmReportDesign frm = new Epoint.Reports.frmReportDesign();
                frm.Load(strReport_File);
            }
        }

        private void BindingTong_Tien()
        {
            numTTien0.DataBindings.Add("Value", bdsViewPh, "TTien0");
            numTTien_Nt0.DataBindings.Add("Value", bdsViewPh, "TTien_Nt0");

            numTTien3.DataBindings.Add("Value", bdsViewPh, "TTien3");
            numTTien_Nt3.DataBindings.Add("Value", bdsViewPh, "TTien_Nt3");

            numTSo_Luong.DataBindings.Add("Value", bdsViewPh, "TSo_Luong");

        }

        private void FormLayout()
        {
            dgvViewPh.Location = new Point(3, 3);
            dgvViewPh.Width = this.Width - 7;
            dgvViewPh.Height = (int)(0.5 * this.Height);

            dgvViewCt.Location = new Point(dgvViewPh.Left, dgvViewPh.Bottom);
            dgvViewCt.Width = this.Width - 7;
            dgvViewCt.Height = this.Height - dgvViewPh.Height - pnlTTien.Height - 20;

            dgvViewPh.ResizeGridView();
            dgvViewCt.ResizeGridView();
        }

        private void Mark()
        {
            if (bdsViewPh.Position < 0)
                return;

            //if (dgvViewPh.Columns[dgvViewPh.CurrentCell.ColumnIndex].Name != "LOCKED")
            //{
            //    if (dgvViewPh.Columns.Contains("Mark"))
            //    {
            //        drCurrent = ((DataRowView)bdsViewPh.Current).Row;

            //        drCurrent["Mark"] = !(bool)drCurrent["Mark"];
            //        dgvViewPh.Refresh();
            //    }
            //}
        }

        private void DanhSoCt()
        {
            if (bdsViewPh.Count <= 0)
                return;

            drCurrent = ((DataRowView)bdsViewPh.Current).Row;

            string strMa_Ct = (string)drCurrent["Ma_Ct"];
            int iThang = ((DateTime)drCurrent["Ngay_Ct"]).Month;
            string strFormat_Text = string.Empty;
            string strFix_Text = string.Empty;
            string strStt = string.Empty;
            string strSo_Ct = string.Empty;

            DataTable dtDmSoCt = DataTool.SQLGetDataTable("SYSDMSOCT", "", "Ma_Ct = '" + strMa_Ct + "' AND Loai_Ma_Ct = " + iThang.ToString().Trim(), "");

            if (dtDmSoCt != null && dtDmSoCt.Rows.Count > 0)
            {
                strFormat_Text = (string)dtDmSoCt.Rows[0]["Format_Text"];
                strFix_Text = (string)dtDmSoCt.Rows[0]["Fix_Text"];
            }

            frmDanhSo_Ct frm = new frmDanhSo_Ct();
            frm.txtFormat_Text.Text = strFormat_Text;
            frm.txtFix_Text.Text = strFix_Text;
            frm.Load();

            if (frm.isAccept)
            {
                int iSo_Ct = Convert.ToInt32(frm.numSo_Ct.Value);
                strFormat_Text = frm.txtFormat_Text.Text;
                strFix_Text = frm.txtFix_Text.Text;

                for (int i = 0; i < bdsViewPh.Count; i++)
                {
                    bdsViewPh.Position = i;

                    string strSo_Ct_New = iSo_Ct.ToString().PadLeft(strFormat_Text.Length, '0');
                    if (strFix_Text.Contains(","))
                        strSo_Ct_New = strFix_Text.Replace(",", strSo_Ct_New);
                    else
                        strSo_Ct_New = strFix_Text + strSo_Ct_New;

                    drCurrent = ((DataRowView)bdsViewPh.Current).Row;
                    strStt = (string)drCurrent["Stt"];
                    strSo_Ct = (string)drCurrent["So_Ct"];

                    string strSQLExec =
                        "UPDATE GLVOUCHER SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE CATIEN SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE APMUA SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE ARBAN SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE INNHAPXUAT SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE GLKETOAN SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "'" +
                        "UPDATE GLTHANHTOAN SET So_Ct = '" + strSo_Ct_New + "' WHERE Stt = '" + strStt + "' AND So_Ct = '" + strSo_Ct + "'";

                    if (SQLExec.Execute(strSQLExec))
                    {
                        drCurrent["So_Ct"] = strSo_Ct_New;
                    }

                    iSo_Ct++;
                }
            }
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////
        /// </summary>

        #region Import Excel chưng từ
        private string strKey_Ph, strKey_Ct;
        //Import Excel chưng từ
        /*
          case Keys.F10:
                    switch (e.Modifiers)
                    {
                        case Keys.None:
                            if (((e.KeyCode == Keys.F10) && Common.CheckPermission(base.Object_ID, enuPermission_Type.Allow_New)) && Common.CheckPermission(base.Object_ID, enuPermission_Type.Allow_Edit))
                            {
                                this.Cursor = Cursors.WaitCursor;
                                this.Import_Excel();
                                this.Cursor = Cursors.Default;
                            }
                            return;

                        case Keys.Control:
                            //this.Export_Excel();
                            break;
                    }
                    break;
         */

        private void ConvertFontImport(DataRow drConvert, string strColumnName)
        {
            if ((strColumnName != "strNew_Edit") && (drConvert[strColumnName].GetType().ToString() == "System.String"))
            {
                //string str2 = Parameters.GetParaValue("FONT_IMPORT").ToString().Trim();

                string strFontImport = "U";

                if (strFontImport != null)
                {
                    if (!(strFontImport == "T"))
                    {
                        if (strFontImport == "V")
                        {
                        }
                    }
                    else
                    {
                        drConvert[strColumnName] = ConvertFont.TCVN3ToUnicode(drConvert[strColumnName].ToString());
                    }
                }
            }
        }


        private void SetKeyFillter()
        {
            object objNgay_Ct = SQLExec.ExecuteReturnValue("SELECT TOP 1 (Ngay_Ct) FROM " + ((string)this.drDmCt["Table_Ph"]) + " WHERE Ma_Ct LIKE '" + this.strMa_Ct_List.Split(new char[] { ',' })[0] + "' AND Ma_DvCs = '" + Element.sysMa_DvCs + "' ORDER BY Ngay_Ct DESC");

            //object obj3 = null;
            //if (this.drDmCt.Table.Columns.Contains("Day_Filter"))
            //{
            //    obj3 = SQLExec.ExecuteReturnValue("SELECT Day_Filter FROM L00DMCT WHERE Ma_Ct LIKE '" + this.strMa_Ct_List.Split(new char[] { ',' })[0] + "' ");
            //}
            //string str = (obj3 == null) ? "" : obj3.ToString();

            //int days = (str == "") ? Convert.ToInt32(Parameters.GetParaValue("DAY_FILTER")) : Convert.ToInt32(str);

            int days = Convert.ToInt32(Parameters.GetParaValue("DAY_FILTER"));
            DateTime dteNgay = (objNgay_Ct == DBNull.Value || objNgay_Ct == null) ? DateTime.Now : ((DateTime)objNgay_Ct);
            DateTime time2 = dteNgay.Subtract(new TimeSpan(days, 0, 0, 0));
            string str4 = string.Empty + "(Ma_Ct IN ('" + this.strMa_Ct_List.Replace(",", "','") + "'))";
            string str2 = str4 + " AND (Ngay_Ct BETWEEN  '" + Library.DateToStr(time2) + "' AND '" + Library.DateToStr(dteNgay) + "')";
            if ((bool)DataTool.SQLGetDataRowByID("SYSDMDVCS", "Ma_DvCs", Element.sysMa_DvCs)["Tong_Hop"])
            {
                str2 = str2 + " AND (Ma_DvCs IN (SELECT Ma_DvCs FROM SYSDMDVCS WHERE Tong_Hop = 0))";
            }
            else
            {
                str2 = str2 + " AND (Ma_DvCs = '" + Element.sysMa_DvCs + "')";
            }

            //object obj4 = SQLExec.ExecuteReturnValue("SELECT Parameter_Value FROM SYSPARAMETER WHERE Parameter_ID='CT_FILTER'").ToString();
            //string strExprList = (obj4 == null) ? "" : obj4.ToString();
            //if ((strExprList != "") && Common.Inlist(this.strMa_Ct_List, strExprList))
            //{
            //    this.strKey_Ph = Element.sysIs_Admin ? str2 : string.Concat(new object[] { str2, " AND (RIGHT( Create_Log,", Element.sysUser_Id.Length, ") = '", Element.sysUser_Id, "')" });
            //}
            //else
            //{
            //    this.strKey_Ph = str2;
            //}

            this.strKey_Ph = str2;
            this.strKey_Ct = str2;
        }
        private void GetDefColImport()
        {

            //Get Table Define Column
            if (!bGetDefColImport)
            {
                DataSet dsDefColImport = SQLExec.ExecuteReturnDs("sp_IF_GetDefColImport", new string[] { "Table_Name" }, new object[] { "AR_PEPSIORDER" }, CommandType.StoredProcedure);

                dtDefineRow_Pos = dsDefColImport.Tables[0];
                dtDefineCol_Pos = dsDefColImport.Tables[1];
                dtDefineCol_DataType = dsDefColImport.Tables[2];

                bGetDefColImport = true;
            }

        }
        private object GetValue(string strData_Type, object objValue)
        {
            if (Common.Inlist(strData_Type, "INT,MONEY,NUMBER"))
            {
                objValue = objValue.ToString().Replace(" ", "");

                if (objValue.ToString() == "")
                    objValue = 0;
                else
                {
                    try
                    {
                        objValue = strData_Type == "INT" ? Convert.ToInt32(objValue) : Convert.ToDouble(objValue);
                    }
                    catch
                    {
                        StringBuilder strValue = new StringBuilder();
                        foreach (char c in objValue.ToString())
                        {
                            if ((c == '-') || (c == '.') || (c == ','))
                                strValue.Append(c);
                            else if (Char.IsDigit(c))
                                strValue.Append(c);
                        }

                        objValue = strData_Type == "INT" ? Convert.ToInt32(strValue.ToString()) : Convert.ToDouble(strValue.ToString());
                    }
                }
            }
            else if (strData_Type == "BIT")
            {
                objValue = objValue.ToString().Replace(" ", "");

                if (objValue.ToString() == "")
                    objValue = 0;

                if (Common.Inlist(objValue.ToString(), "Fail,Pass"))
                    objValue = objValue.ToString() == "Fail" ? 0 : 1;
            }
            else if (strData_Type == "DATE")
            {
                string date = objValue.ToString();
                //dt will be DateTime type
                objValue = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                //if (FileName.Contains(".xls"))
                //    objValue = DateTime.ParseExact(date, "MM/d/yyyy", CultureInfo.CurrentCulture);
                //else
                //    objValue = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            }
            return objValue;
        }


        /// <summary>
        /// Import Excel for ttd
        /// </summary>
        /// 
        public void Import_Excel(bool IsAspocel)
        {

            string Ma_Vt_List = string.Empty, Ma_Dt_List = string.Empty, So_Ct_List = string.Empty;
            DataRow rowCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct_List);
            string strTableName = (string)rowCt["Table_Ct"];
            string strTable_Ph = (string)rowCt["Table_Ph"];
            string strSp_UpdatePh = (string)rowCt["Sp_UpdatePh"];
            string strSp_UpdateCt = (string)rowCt["Sp_UpdateCt"];
            DataTable tableVoucher = DataTool.SQLGetDataTable(strTable_Ph, "TOP 0 * ", " 0 = 1", null);
            DataTable tableDetail = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null);
            DataTable tableStructForStt = DataTool.SQLGetDataTable(strTableName, "TOP 0 Ma_Ct, Ngay_Ct, So_Ct ", " 0 = 1", null);

            int RowDataImport = 0;
            EpointProcessBox.AddMessage("Loading data....................");

            try
            {

                //DataTable tableARBan = tableDetail.Clone();

                string strModule = string.Empty;

                switch (strTableName)
                {
                    case "CATIEN":
                        strModule = "01";
                        break;

                    case "APMUA":
                    case "APPO":
                        strModule = "02";
                        break;

                    case "ARBAN":
                    case "ARSO":
                        strModule = "04";
                        break;

                    case "INNHAPXUAT":
                        strModule = "05";
                        break;

                    case "GLKETOAN":
                        strModule = "80";
                        break;
                }



                if (strModule == "01")// CA
                {
                    if (!this.dtDataExcelImport.Columns.Contains("TIEN"))
                    {
                        if (!this.dtDataExcelImport.Columns.Contains("PS_NO") || !this.dtDataExcelImport.Columns.Contains("PS_CO"))
                        {
                            EpointProcessBox.AddMessage("Tập tin excel không đúng mẫu import phân hệ tiền!");
                            EpointProcessBox.ShowOK(true);
                            return;
                        }
                    }
                }
                //else if (strModule == "02")// AR
                //{
                //    if (!this.dtDataExcelImport.Columns.Contains("SO_CT") || !this.dtDataExcelImport.Columns.Contains("NGAY_CT") || !this.dtDataExcelImport.Columns.Contains("MA_VT"))
                //    {
                //        EpointProcessBox.AddMessage("Tập tin excel không đúng mẫu import phân hệ bán hàng!");
                //        EpointProcessBox.ShowOK(true);
                //        return;

                //    }
                //}
                this.GetDefColImport();
               
                object objValue;
                //Xu ly dữ liệu
                foreach (DataRow drRow0 in dtDefineRow_Pos.Rows)
                {
                    int iRowGetValue = Convert.ToInt32(drRow0[0]);
                    RowDataImport = 0;
                    foreach (DataRow drImport in this.dtDataExcelImport.Rows)
                    {
                        if (this.dtDataExcelImport.Rows.IndexOf(drImport) < iRowGetValue)
                            continue;
                        if (drImport[0].ToString().Trim() == String.Empty)
                            continue;
                        //drImport[0].ToString();

                        RowDataImport ++;
                        DataRow drNewRowFirst = tableDetail.NewRow();
                        foreach (DataColumn dc in dtDefineCol_Pos.Columns)
                        {
                            if (!drNewRowFirst.Table.Columns.Contains(dc.ColumnName))
                            {
                                drNewRowFirst.Table.Columns.Add(dc.ColumnName, dtDataExcelImport.Columns[Convert.ToInt32(dtDefineCol_Pos.Rows[0][dc.ColumnName])].DataType);
                            }

                            if (dtDataExcelImport.Columns[Convert.ToInt32(dtDefineCol_Pos.Rows[0][dc.ColumnName])].DataType == drNewRowFirst.Table.Columns[dc.ColumnName].DataType)
                            {
                                drNewRowFirst[dc.ColumnName] = drImport[Convert.ToInt32(dtDefineCol_Pos.Rows[0][dc.ColumnName])];
                            }
                            else
                            {
                                string strData_Type = dtDefineCol_DataType.Rows[0][dc.ColumnName].ToString();
                                try { objValue = drImport[Convert.ToInt32(dtDefineCol_Pos.Rows[0][dc.ColumnName])]; }
                                catch { objValue = ""; }

                                objValue = GetValue(strData_Type, objValue);
                                drNewRowFirst[dc.ColumnName] = objValue;
                            }


                        }
                        Common.SetDefaultDataRow(ref drNewRowFirst);
                        tableDetail.Rows.Add(drNewRowFirst);
                        tableDetail.AcceptChanges();
                    }
                }

                var structInvoice = from c in tableDetail.AsEnumerable()
                                    group c by new
                                    {
                                        So_Ct = c.Field<string>("So_Ct")
                                    } into g
                                    where g.Key.So_Ct != string.Empty
                                    select new
                                    {
                                        g.Key.So_Ct
                                    };
                //return;
                var structVattu = from c in tableDetail.AsEnumerable()
                                  group c by new
                                  {
                                      Ma_Vt = c.Field<string>("Ma_Vt")
                                  } into g
                                  where g.Key.Ma_Vt != string.Empty
                                  select new
                                  {
                                      g.Key.Ma_Vt
                                  };
                var structDoituong = from c in tableDetail.AsEnumerable()
                                     group c by new
                                     {
                                         Ma_Dt = c.Field<string>("Ma_Dt")
                                     } into g
                                     where g.Key.Ma_Dt != string.Empty
                                     select new
                                     {
                                         g.Key.Ma_Dt
                                     };
                foreach (var c in structInvoice)
                {
                    DataRow drVou = DataTool.SQLGetDataRowByID("GLVOUCHER", "So_Ct", c.So_Ct);
                    if (drVou != null)
                    {
                        So_Ct_List += c.So_Ct + ";" + System.Environment.NewLine;
                    }
                }
                if (So_Ct_List != string.Empty)
                {
                    EpointProcessBox.AddMessage("Các chứng từ đã tồn tại:" + So_Ct_List);
                    return;
                }

                foreach (var c in structVattu)
                {
                    DataRow drVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", c.Ma_Vt);
                    if (drVt == null)
                    {
                        Ma_Vt_List += c.Ma_Vt + ";" + System.Environment.NewLine;
                    }
                }

                foreach (var c in structDoituong)
                {
                    DataRow drdt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", c.Ma_Dt);
                    if (drdt == null)
                    {
                        Ma_Dt_List += c.Ma_Dt + ";" + System.Environment.NewLine;
                    }
                }

                if (Ma_Vt_List != string.Empty)
                {
                    EpointProcessBox.AddMessage("Danh sách mã hàng không tồn tại:" + Ma_Vt_List);
                }

                if (Ma_Dt_List != string.Empty)
                {
                    EpointProcessBox.AddMessage("Danh sách khách hàng không tồn tại:" + Ma_Dt_List);
                }

                if (Ma_Vt_List != string.Empty || Ma_Vt_List != string.Empty)
                {
                    EpointProcessBox.AddMessage("Import đơn hàng thất bại.");
                    return;
                }

                foreach (DataRow rowEx in tableDetail.Rows)
                {
                    if (rowEx["Ma_Vt"].ToString() != string.Empty)
                    {
                        DataRow drVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", rowEx["Ma_Vt"].ToString());
                        DataRow drDt = DataTool.SQLGetDataRowByID("LIDOITUONG", "Ma_Dt", rowEx["Ma_Dt"].ToString());
                        rowEx["Ma_Dvcs"] = Element.sysMa_DvCs;
                        rowEx["Ma_Ct"] = this.strMa_Ct_List;
                        rowEx["User_Crtd"] = Element.sysUser_Id;
                        rowEx["Han_Tt"] = 7;
                        rowEx["Ma_Tte"] = rowEx["Ma_Tte"].ToString().Trim() == string.Empty ? "VND" : rowEx["Ma_Tte"];
                        rowEx["Ty_Gia"] = rowEx["Ma_Tte"].ToString().Trim() == Element.sysMa_Tte ? 1 : rowEx["Ty_Gia"];
                        rowEx["Ong_Ba"] = rowEx["Ong_Ba"].ToString().Trim() == string.Empty ? rowEx["Ten_Dt"] : rowEx["Ong_Ba"];
                        rowEx["Auto_Cost"] = "true";
                        rowEx["Tien_Nt"] = rowEx["Tien"];
                        rowEx["Tien_Nt2"] = rowEx["Tien2"];
                        rowEx["Tien_Nt3"] = rowEx["Tien3"];
                        rowEx["Tien_Nt4"] = rowEx["Tien4"];
                        //rowEx["Is_Thue_VAT"] = "false";
                        rowEx["NoPosted"] = "false";
                        rowEx["Ma_Ct"] = rowEx["Ma_Ct"].ToString().Trim() == "" ? this.strMa_Ct_List : rowEx["Ma_Ct"];
                        rowEx["Tk_No"] = drVt["Tk_Gv"];
                        rowEx["Tk_Co"] = drVt["Tk_Vt"];
                        rowEx["Tk_No2"] = this.drDmCt["Tk_No"];
                        rowEx["Tk_Co2"] = this.drDmCt["Tk_Co"];
                        rowEx["Ngay_Ct"] = ((DateTime)rowEx["Ngay_Ct"]).ToShortDateString();

                        rowEx["So_Luong9"] = Convert.ToDouble(rowEx["So_LuongL"]) > 0? Convert.ToDouble(rowEx["So_LuongT"]) * Convert.ToDouble(drVt["He_So1"]) + Convert.ToDouble(rowEx["So_LuongL"]) : Convert.ToDouble(rowEx["So_LuongT"]);                        
                        rowEx["He_So9"] = Convert.ToDouble(rowEx["So_LuongL"]) > 0 ? 1 : Convert.ToDouble(drVt["He_So1"]);
                        rowEx["So_Luong"] = Convert.ToDouble(rowEx["So_Luong9"]) * Convert.ToDouble(rowEx["He_So9"]);
                        rowEx["Gia_Nt9"] = Convert.ToDouble(rowEx["Tien_Nt9"]) / Convert.ToDouble(rowEx["So_Luong9"]);
                        rowEx["Dvt"] = Convert.ToDouble(rowEx["So_LuongL"]) > 0 ? drVt["Dvt"]: drVt["Dvt1"];
                        rowEx["Ma_Kho"] = drVt["Ma_Kho_Ban"];
                        rowEx["Hang_Km"] = Convert.ToDouble(rowEx["Gia_Nt9"]) == 0 ? "true" : "false";
                    }
                    else
                    {
                        rowEx["Ma_Ct"] = this.strMa_Ct_List;
                        DataRow drVoucher = tableVoucher.NewRow();
                        Common.SetDefaultDataRow(ref drVoucher);
                        Common.CopyDataRow(rowEx, drVoucher);
                        tableVoucher.Rows.Add(drVoucher);
                    }
                }


                //return;

                EpointProcessBox.AddMessage("Importing Invoice......................");
                //return;
                //var structStt = from c in tableARBan.AsEnumerable()
                //                group c by new
                //                {
                //                    Ma_Ct = c.Field<string>("Ma_Ct"),
                //                    So_Ct = c.Field<string>("So_Ct"),//column names for checking duplicate values.
                //                    Ngay_Ct = c.Field<DateTime>("Ngay_Ct"),

                //                } into g
                //                where g.Key.So_Ct != string.Empty
                //                select new
                //                {
                //                    g.Key.Ma_Ct,
                //                    g.Key.So_Ct,
                //                    g.Key.Ngay_Ct
                //                };


                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                SqlTransaction ImportTrans = command.Connection.BeginTransaction("Update_Voucher_Tran");
                command.Transaction = ImportTrans;

                foreach (DataRow drVoucher in tableVoucher.Rows)
                {

                    string strColumnName;
                    Exception exception;
                    DataRow[] drAr = tableDetail.Select(string.Concat(new object[] { "Ma_Ct = '", drVoucher["Ma_Ct"], "' AND Ngay_Ct = '", drVoucher["Ngay_Ct"], "' AND So_Ct = '", drVoucher["So_Ct"], "' AND Ma_Vt <> ''" }));
                    //DataRow drVoucher = tableVoucher.NewRow();
                    //Common.SetDefaultDataRow(ref drVoucher);
                    //Common.CopyDataRow(drAr[0], drVoucher);
                    if (drVoucher.Table.Columns.Contains("Duyet"))
                    {
                        drVoucher["Duyet"] = 0;
                        drVoucher["So_Ct_Px"] = drVoucher["So_Ct"];
                    }
                    if (drVoucher.Table.Columns.Contains("Pt_Tt"))
                    {
                        drVoucher["Pt_Tt"] = "TM/CK";
                    }
                    if (tableDetail.Columns.Contains("So_Luong") && tableVoucher.Columns.Contains("TSo_Luong"))
                    {
                        drVoucher["TSo_Luong"] = Common.SumDCValue(drAr, "So_Luong");
                    }
                    if (tableDetail.Columns.Contains("Tien"))
                    {
                        drVoucher["TTien0"] = Common.SumDCValue(drAr, "Tien");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt"))
                    {
                        drVoucher["TTien_Nt0"] = Common.SumDCValue(drAr, "Tien_Nt");
                    }
                    if (tableDetail.Columns.Contains("Tien2"))
                    {
                        drVoucher["TTien0"] = Common.SumDCValue(drAr, "Tien2");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt2"))
                    {
                        drVoucher["TTien_Nt0"] = Common.SumDCValue(drAr, "Tien_Nt2");
                    }
                    if (tableDetail.Columns.Contains("Tien3"))
                    {
                        drVoucher["TTien3"] = Common.SumDCValue(drAr, "Tien3");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt3"))
                    {
                        drVoucher["TTien_Nt3"] = Common.SumDCValue(drAr, "Tien_Nt3");
                    }
                    if (tableDetail.Columns.Contains("Tien4"))
                    {
                        drVoucher["TTien4"] = Common.SumDCValue(drAr, "Tien4");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt4"))
                    {
                        drVoucher["TTien_Nt4"] = Common.SumDCValue(drAr, "Tien_Nt4");
                    }
                    if (tableDetail.Columns.Contains("Tien5"))
                    {
                        drVoucher["TTien5"] = Common.SumDCValue(drAr, "Tien5");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt5"))
                    {
                        drVoucher["TTien_Nt5"] = Common.SumDCValue(drAr, "Tien_Nt5");
                    }
                    if (tableDetail.Columns.Contains("Tien6"))
                    {
                        drVoucher["TTien6"] = Common.SumDCValue(drAr, "Tien6");
                    }
                    if (tableDetail.Columns.Contains("Tien_Nt6"))
                    {
                        drVoucher["TTien_Nt6"] = Common.SumDCValue(drAr, "Tien_Nt6");
                    }
                    drVoucher["Create_Log"] = Common.GetCurrent_Log();

                    string newStt = Common.GetNewStt(strModule, true);
                    while (DataTool.SQLCheckExist(strTable_Ph, "Stt", newStt))
                    {
                        newStt = Common.GetNewStt(strModule, true);
                    }
                    try
                    {
                        /*
                        //string strSQLDeleteTableDetail = string.Concat(new object[] {
                        //                                                "DELETE FROM ", strTableName, " WHERE Ma_Ct = '",drc["Ma_Ct"], "' AND Ngay_Ct = '",Convert.ToDateTime(drc["Ngay_Ct"]).ToString("yyyy-MM-dd"), "' AND So_Ct = '", drc["So_Ct"], "' AND Ma_DvCs = '", Element.sysMa_DvCs,
                        //                                                "';\n DELETE FROM ", strTable_Ph, " WHERE Ma_Ct = '", drc["Ma_Ct"], "' AND Ngay_Ct = '", Convert.ToDateTime(drc["Ngay_Ct"]).ToString("yyyy-MM-dd"),
                        //                                                "' AND So_Ct = '", drc["So_Ct"], "' AND Ma_DvCs = '", Element.sysMa_DvCs, "'"
                        //                                             });
                        //command.CommandText = strSQLDeleteTableDetail;
                        //command.CommandType = CommandType.Text;
                        //command.ExecuteNonQuery();*/
                        if (drVoucher != null)
                        {
                            command.CommandText = strSp_UpdatePh;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Clear();
                            string str10 = "Object_id = Object_id('" + strSp_UpdatePh + "')";
                            DataTable dtParram = DataTool.SQLGetDataTable("Sys.Parameters", "Name", str10, null);
                            command.Parameters.AddWithValue("@strNew_Edit", 'N');
                            //Common.SetDefaultDataRow(ref drVoucher);
                            drVoucher["Stt"] = newStt;
                            foreach (DataRow drPar in dtParram.Rows)
                            {
                                strColumnName = ((string)drPar["Name"]).Replace("@", "");
                                this.ConvertFontImport(drVoucher, strColumnName);
                                if (drVoucher.Table.Columns.Contains(strColumnName))
                                {
                                    command.Parameters.AddWithValue("@" + strColumnName, drVoucher[strColumnName]);
                                }
                            }

                            command.ExecuteNonQuery();

                        }

                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ImportTrans.Rollback();
                        EpointProcessBox.ShowOK(true);
                        EpointProcessBox.AddMessage("Có lỗi xảy ra....................\n" + RowDataImport.ToString() + exception.Message);

                    }
                    command.Parameters.Clear();
                    command.CommandText = strSp_UpdateCt;
                    command.CommandType = CommandType.StoredProcedure;
                    string strKey = "Object_id = Object_id('" + strSp_UpdateCt + "')";
                    DataTable dtParam = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);
                    int num = 1;
                    foreach (DataRow dataRow in drAr)
                    {
                        command.Parameters.Clear();
                        DataRow rowDetail = dataRow;
                        Common.SetDefaultDataRow(ref rowDetail);
                        rowDetail["Stt"] = newStt;
                        //rowDetail["Stt0"] = ++num;
                        if (rowDetail.Table.Columns.Contains("Duyet"))
                        {
                            rowDetail["Duyet"] = true;
                        }
                        if (rowDetail.Table.Columns.Contains("Auto_Cost"))
                        {
                            rowDetail["Auto_Cost"] = true;
                        }
                        if (rowDetail.Table.Columns.Contains("He_So9"))
                        {
                            //rowDetail["He_So9"] = 1;
                        }
                        foreach (DataRow row6 in dtParam.Rows)
                        {
                            strColumnName = ((string)row6["Name"]).Replace("@", "");
                            this.ConvertFontImport(rowDetail, strColumnName);
                            if (rowDetail.Table.Columns.Contains(strColumnName))
                            {
                                command.Parameters.AddWithValue("@" + strColumnName, rowDetail[strColumnName]);
                            }
                        }
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            ImportTrans.Rollback();
                            EpointProcessBox.ShowOK(true);
                            EpointProcessBox.AddMessage("Có lỗi xảy ra....................\n" + RowDataImport.ToString() + exception.Message);

                        }
                    }


                }

                ImportTrans.Commit();




                EpointProcessBox.AddMessage("Kết thúc!");
            }
            catch (Exception ex)
            {
                EpointProcessBox.AddMessage("Có lỗi xảy ra....................\n" + RowDataImport.ToString() + ex.Message);
            }

        }
        public void Import_Excel_OLD(bool IsAspocel)
        {
            try
            {

                EpointProcessBox.AddMessage("Loading data....................");
                if (dtDataExcelImport != null)
                {

                    DataRow rowCt = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct_List);
                    string strTableName = (string)rowCt["Table_Ct"];
                    string strTable_Ph = (string)rowCt["Table_Ph"];
                    string strSp_UpdatePh = (string)rowCt["Sp_UpdatePh"];
                    string strSp_UpdateCt = (string)rowCt["Sp_UpdateCt"];
                    DataTable tableVoucher = DataTool.SQLGetDataTable(strTable_Ph, "TOP 0 * ", " 0 = 1", null);
                    DataTable tableDetail = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null);
                    DataTable tableStructForStt = DataTool.SQLGetDataTable(strTableName, "TOP 0 Ma_Ct, Ngay_Ct, So_Ct ", " 0 = 1", null);
                    DataTable tableARBan = tableDetail.Clone();
                    //Format dtimport
                    foreach (DataRow rowIp in dtDataExcelImport.Rows)
                    {
                        rowIp["Ong_Ba"] = rowIp["Ong_Ba"].ToString().Trim() == string.Empty ? rowIp["Ten_Dt"] : rowIp["Ong_Ba"];
                        rowIp["Gia2"] = rowIp["Gia"];
                        rowIp["Gia_Nt2"] = rowIp["Gia2"];
                        rowIp["Tien_Nt"] = rowIp["Tien"];
                        rowIp["Tien2"] = rowIp["Tien"];
                        rowIp["Tien_Nt2"] = rowIp["Tien"];
                        rowIp["Gia_Nt"] = rowIp["Gia"];
                        //rowIp["Tien_Nt9"] = rowIp["Tien"];
                        //rowIp["He_So9"] = 1;

                        if (dtDataExcelImport.Columns.Contains("Auto_Cost"))
                        {
                            rowIp["Auto_Cost"] = rowIp["Auto_Cost"].ToString() == "1" ? "true" : "false";
                        }

                        if (dtDataExcelImport.Columns.Contains("Is_Thue_VAT"))
                        {
                            rowIp["Is_Thue_VAT"] = rowIp["Is_Thue_VAT"].ToString() == "1" ? "true" : "false";
                        }

                        if (dtDataExcelImport.Columns.Contains("NoPosted"))
                        {
                            rowIp["NoPosted"] = rowIp["NoPosted"].ToString() == "1" ? "true" : "false";
                        }
                    }




                    tableARBan.Merge(dtDataExcelImport, true, MissingSchemaAction.Ignore);
                    foreach (DataRow rowAR in tableARBan.Rows)
                    {
                        DataRow dr = rowAR;
                        Common.SetDefaultDataRow(ref dr);
                        if (tableARBan.Columns.Contains("Ten_DtGtGt"))
                        {
                            dr["Ten_DtGtGt"] = dr["Ten_Dt"];
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt2"))
                        {
                            dr["Tien_Nt9"] = dr["Tien_Nt2"];
                        }
                        else if (tableARBan.Columns.Contains("Tien_Nt"))
                        {
                            dr["Tien_Nt9"] = dr["Tien_Nt"];
                        }
                        if (tableARBan.Columns.Contains("So_Luong") && tableARBan.Columns.Contains("So_Luong9"))
                        {
                            dr["So_Luong9"] = dr["So_Luong"];
                        }
                        if (tableARBan.Columns.Contains("Gia_Nt2") && tableARBan.Columns.Contains("Gia_Nt9"))
                        {
                            dr["Gia_Nt9"] = dr["Gia_Nt2"];
                        }
                        if (tableARBan.Columns.Contains("Gia_Nt") && tableARBan.Columns.Contains("Gia_Nt9"))
                        {
                            dr["Gia_Nt9"] = dr["Gia_Nt"];
                        }
                        if (tableARBan.Columns.Contains("So_Ct") && (dr["So_Ct"] == DBNull.Value || dr["So_Ct"].ToString() == string.Empty))
                        {
                            dr["So_Ct"] = dr["So_Ct0"];
                        }
                    }

                    var structStt = from c in tableARBan.AsEnumerable()
                                    group c by new
                                    {
                                        Ma_Ct = c.Field<string>("Ma_Ct"),
                                        So_Ct = c.Field<string>("So_Ct"),//column names for checking duplicate values.
                                        Ngay_Ct = c.Field<DateTime>("Ngay_Ct"),

                                    } into g
                                    where g.Key.So_Ct != string.Empty
                                    select new
                                    {
                                        g.Key.Ma_Ct,
                                        g.Key.So_Ct,
                                        g.Key.Ngay_Ct
                                    };

                    EpointProcessBox.AddMessage("Importing Invoice......................");
                    foreach (var c in structStt)
                    {

                        if (c.So_Ct == string.Empty)
                            continue;

                        string strColumnName;
                        Exception exception;
                        DataRow[] drAr = tableARBan.Select(string.Concat(new object[] { "Ma_Ct = '", c.Ma_Ct, "' AND Ngay_Ct = '", c.Ngay_Ct, "' AND So_Ct = '", c.So_Ct, "'" }));
                        DataRow drVoucher = tableVoucher.NewRow();
                        Common.SetDefaultDataRow(ref drVoucher);
                        Common.CopyDataRow(drAr[0], drVoucher);
                        if (drVoucher.Table.Columns.Contains("Duyet"))
                        {
                            drVoucher["Duyet"] = 1;
                        }
                        if (tableARBan.Columns.Contains("So_Luong") && tableVoucher.Columns.Contains("TSo_Luong"))
                        {
                            drVoucher["TSo_Luong"] = Common.SumDCValue(drAr, "So_Luong");
                        }
                        if (tableARBan.Columns.Contains("Tien"))
                        {
                            drVoucher["TTien0"] = Common.SumDCValue(drAr, "Tien");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt"))
                        {
                            drVoucher["TTien_Nt0"] = Common.SumDCValue(drAr, "Tien_Nt");
                        }
                        if (tableARBan.Columns.Contains("Tien2"))
                        {
                            drVoucher["TTien0"] = Common.SumDCValue(drAr, "Tien2");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt2"))
                        {
                            drVoucher["TTien_Nt0"] = Common.SumDCValue(drAr, "Tien_Nt2");
                        }
                        if (tableARBan.Columns.Contains("Tien3"))
                        {
                            drVoucher["TTien3"] = Common.SumDCValue(drAr, "Tien3");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt3"))
                        {
                            drVoucher["TTien_Nt3"] = Common.SumDCValue(drAr, "Tien_Nt3");
                        }
                        if (tableARBan.Columns.Contains("Tien4"))
                        {
                            drVoucher["TTien4"] = Common.SumDCValue(drAr, "Tien4");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt4"))
                        {
                            drVoucher["TTien_Nt4"] = Common.SumDCValue(drAr, "Tien_Nt4");
                        }
                        if (tableARBan.Columns.Contains("Tien5"))
                        {
                            drVoucher["TTien5"] = Common.SumDCValue(drAr, "Tien5");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt5"))
                        {
                            drVoucher["TTien_Nt5"] = Common.SumDCValue(drAr, "Tien_Nt5");
                        }
                        if (tableARBan.Columns.Contains("Tien6"))
                        {
                            drVoucher["TTien6"] = Common.SumDCValue(drAr, "Tien6");
                        }
                        if (tableARBan.Columns.Contains("Tien_Nt6"))
                        {
                            drVoucher["TTien_Nt6"] = Common.SumDCValue(drAr, "Tien_Nt6");
                        }
                        SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                        SqlTransaction ImportTrans = command.Connection.BeginTransaction("Update_Voucher_Tran");
                        command.Transaction = ImportTrans;
                        string strModule = string.Empty;
                        switch (strTableName)
                        {
                            case "CATIEN":
                                strModule = "01";
                                break;

                            case "APMUA":
                            case "APPO":
                                strModule = "02";
                                break;

                            case "ARBAN":
                            case "ARSO":
                                strModule = "04";
                                break;

                            case "INNHAPXUAT":
                                strModule = "05";
                                break;

                            case "GLKETOAN":
                                strModule = "80";
                                break;
                        }
                        string newStt = Common.GetNewStt(strModule, true);
                        while (DataTool.SQLCheckExist(strTable_Ph, "Stt", newStt))
                        {
                            newStt = Common.GetNewStt(strModule, true);
                        }
                        SQLExec.Execute(string.Concat(new object[] {
                                                                        "DELETE FROM ", strTableName, " WHERE Ma_Ct = '", c.Ma_Ct, "' AND Ngay_Ct = '", Library.DateToStr((DateTime)c.Ngay_Ct), "' AND So_Ct = '", c.So_Ct, "' AND Ma_DvCs = '", Element.sysMa_DvCs, "';\r\n\t\t\t\t\t\t\t\t\t\t\t DELETE FROM ", strTable_Ph, " WHERE Ma_Ct = '", c.Ma_Ct, "' AND Ngay_Ct = '", Library.DateToStr((DateTime) c.Ngay_Ct),
                                                                        "' AND So_Ct = '", c.So_Ct, "' AND Ma_DvCs = '", Element.sysMa_DvCs, "'"
                                                                     }));
                        if (drVoucher != null)
                        {
                            command.CommandText = strSp_UpdatePh;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Clear();
                            string str10 = "Object_id = Object_id('" + strSp_UpdatePh + "')";
                            DataTable dtParram = DataTool.SQLGetDataTable("Sys.Parameters", "Name", str10, null);
                            command.Parameters.AddWithValue("@strNew_Edit", 'N');
                            Common.SetDefaultDataRow(ref drVoucher);
                            drVoucher["Stt"] = newStt;
                            foreach (DataRow drPar in dtParram.Rows)
                            {
                                strColumnName = ((string)drPar["Name"]).Replace("@", "");
                                this.ConvertFontImport(drVoucher, strColumnName);
                                if (drVoucher.Table.Columns.Contains(strColumnName))
                                {
                                    command.Parameters.AddWithValue("@" + strColumnName, drVoucher[strColumnName]);
                                }
                            }
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                                ImportTrans.Rollback();
                            }
                        }
                        command.Parameters.Clear();
                        command.CommandText = strSp_UpdateCt;
                        command.CommandType = CommandType.StoredProcedure;
                        string strKey = "Object_id = Object_id('" + strSp_UpdateCt + "')";
                        DataTable dtParam = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);
                        int num = 1;
                        foreach (DataRow dataRow in drAr)
                        {
                            command.Parameters.Clear();
                            DataRow rowDetail = dataRow;
                            Common.SetDefaultDataRow(ref rowDetail);
                            rowDetail["Stt"] = newStt;
                            rowDetail["Stt0"] = ++num;
                            if (rowDetail.Table.Columns.Contains("Duyet"))
                            {
                                rowDetail["Duyet"] = true;
                            }
                            if (rowDetail.Table.Columns.Contains("Auto_Cost"))
                            {
                                rowDetail["Auto_Cost"] = true;
                            }
                            if (rowDetail.Table.Columns.Contains("He_So9"))
                            {
                                rowDetail["He_So9"] = 1;
                            }
                            foreach (DataRow row6 in dtParam.Rows)
                            {
                                strColumnName = ((string)row6["Name"]).Replace("@", "");
                                this.ConvertFontImport(rowDetail, strColumnName);
                                if (rowDetail.Table.Columns.Contains(strColumnName))
                                {
                                    command.Parameters.AddWithValue("@" + strColumnName, rowDetail[strColumnName]);
                                }
                            }
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                                //MessageBox.Show("Có lỗi xảy ra trong quá trình import  :" + exception.Message); 
                                command.Transaction.Rollback();
                                EpointProcessBox.AddMessage("Có lỗi xảy ra....................\n" + exception.Message);

                            }
                        }
                        ImportTrans.Commit();

                    }
                }
                EpointProcessBox.AddMessage("Kết thúc!");
            }
            catch (Exception ex)
            {
                EpointProcessBox.AddMessage("Có lỗi xảy ra....................\n" + ex.Message);
                //EpointMessage.MsgOk(ex.Message);
            }
        }

        public void Import_Excel()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = "xls",
                Filter = "*.xls|*.xls"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;\r\n\t\t\t\t\t\tData Source= " + dialog.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES\""))
                {
                    connection.Open();
                    string selectCommandText = "SELECT * FROM [Sheet1$] Where Ma_Ct IS NOT NULL  AND Ma_Ct = '" + this.strMa_Ct_List + "'";

                    //if (((string)Parameters.GetParaValue("IMPORTVOUCHER_OPTION")) == "1")
                    //{
                    //    selectCommandText = selectCommandText + " AND Ma_Ct = '" + this.strMa_Ct_List + "'";
                    //}
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DataRow row = DataTool.SQLGetDataRowByID("SYSDMCT", "Ma_Ct", this.strMa_Ct_List);
                        string strTable_Ct = (string)row["Table_Ct"];
                        string strTable_Ph = (string)row["Table_Ph"];
                        string strSp_UpdatePh = (string)row["Sp_UpdatePh"];
                        string strSp_UpdateCt = (string)row["Sp_UpdateCt"];
                        DataTable table2 = DataTool.SQLGetDataTable(strTable_Ph, "TOP 0 * ", " 0 = 1", null);
                        DataTable table3 = DataTool.SQLGetDataTable(strTable_Ct, "TOP 0 * ", " 0 = 1", null);
                        DataTable table4 = DataTool.SQLGetDataTable(strTable_Ct, "TOP 0 Ma_Ct, Ngay_Ct, So_Ct ", " 0 = 1", null);
                        DataTable table5 = table3.Clone();
                        table5.Merge(dataTable, true, MissingSchemaAction.Ignore);
                        foreach (DataRow row2 in table5.Rows)
                        {
                            DataRow dr = row2;
                            Common.SetDefaultDataRow(ref dr);
                            if (table5.Columns.Contains("Tien_Nt2"))
                            {
                                dr["Tien_Nt9"] = dr["Tien_Nt2"];
                            }
                            else if (table5.Columns.Contains("Tien_Nt"))
                            {
                                dr["Tien_Nt9"] = dr["Tien_Nt"];
                            }
                            if (table5.Columns.Contains("So_Luong") && table5.Columns.Contains("So_Luong9"))
                            {
                                dr["So_Luong9"] = dr["So_Luong"];
                            }
                            if (table5.Columns.Contains("Gia_Nt2") && table5.Columns.Contains("Gia_Nt9"))
                            {
                                dr["Gia_Nt9"] = dr["Gia_Nt2"];
                            }
                            if (table5.Columns.Contains("Gia_Nt") && table5.Columns.Contains("Gia_Nt9"))
                            {
                                dr["Gia_Nt9"] = dr["Gia_Nt"];
                            }
                            if (table5.Columns.Contains("So_Ct") && (dr["So_Ct"] == DBNull.Value))
                            {
                                dr["So_Ct"] = string.Empty;
                            }
                        }
                        foreach (DataRow row3 in table5.Rows)
                        {
                            if (table4.Select(string.Concat(new object[] { "Ma_Ct = '", row3["Ma_Ct"], "' AND Ngay_Ct = '", row3["Ngay_Ct"], "' AND So_Ct = '", row3["So_Ct"], "'" })).Length == 0)
                            {
                                DataRow row4 = table4.NewRow();
                                row4["Ma_Ct"] = row3["Ma_Ct"];
                                row4["Ngay_Ct"] = row3["Ngay_Ct"];
                                row4["So_Ct"] = row3["So_Ct"];
                                table4.Rows.Add(row4);
                            }
                        }
                        foreach (DataRow row4 in table4.Rows)
                        {
                            string str11;
                            Exception exception;
                            DataRow[] drArr = table5.Select(string.Concat(new object[] { "Ma_Ct = '", row4["Ma_Ct"], "' AND Ngay_Ct = '", row4["Ngay_Ct"], "' AND So_Ct = '", row4["So_Ct"], "'" }));
                            DataRow row5 = table2.NewRow();
                            Common.SetDefaultDataRow(ref row5);
                            Common.CopyDataRow(drArr[0], row5);
                            if (row5.Table.Columns.Contains("Duyet"))
                            {
                                row5["Duyet"] = 1;
                            }
                            if (table5.Columns.Contains("So_Luong") && table2.Columns.Contains("TSo_Luong"))
                            {
                                row5["TSo_Luong"] = Common.SumDCValue(drArr, "So_Luong");
                            }
                            if (table5.Columns.Contains("Tien"))
                            {
                                row5["TTien0"] = Common.SumDCValue(drArr, "Tien");
                            }
                            if (table5.Columns.Contains("Tien_Nt"))
                            {
                                row5["TTien_Nt0"] = Common.SumDCValue(drArr, "Tien_Nt");
                            }
                            if (table5.Columns.Contains("Tien2"))
                            {
                                row5["TTien0"] = Common.SumDCValue(drArr, "Tien2");
                            }
                            if (table5.Columns.Contains("Tien_Nt2"))
                            {
                                row5["TTien_Nt0"] = Common.SumDCValue(drArr, "Tien_Nt2");
                            }
                            if (table5.Columns.Contains("Tien3"))
                            {
                                row5["TTien3"] = Common.SumDCValue(drArr, "Tien3");
                            }
                            if (table5.Columns.Contains("Tien_Nt3"))
                            {
                                row5["TTien_Nt3"] = Common.SumDCValue(drArr, "Tien_Nt3");
                            }
                            if (table5.Columns.Contains("Tien4"))
                            {
                                row5["TTien4"] = Common.SumDCValue(drArr, "Tien4");
                            }
                            if (table5.Columns.Contains("Tien_Nt4"))
                            {
                                row5["TTien_Nt4"] = Common.SumDCValue(drArr, "Tien_Nt4");
                            }
                            if (table5.Columns.Contains("Tien5"))
                            {
                                row5["TTien5"] = Common.SumDCValue(drArr, "Tien5");
                            }
                            if (table5.Columns.Contains("Tien_Nt5"))
                            {
                                row5["TTien_Nt5"] = Common.SumDCValue(drArr, "Tien_Nt5");
                            }
                            if (table5.Columns.Contains("Tien6"))
                            {
                                row5["TTien6"] = Common.SumDCValue(drArr, "Tien6");
                            }
                            if (table5.Columns.Contains("Tien_Nt6"))
                            {
                                row5["TTien_Nt6"] = Common.SumDCValue(drArr, "Tien_Nt6");
                            }
                            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                            SqlTransaction transaction = command.Connection.BeginTransaction("Update_Voucher_Tran");
                            command.Transaction = transaction;
                            string strModule = string.Empty;
                            switch (strTable_Ct)
                            {
                                case "CATIEN":
                                    strModule = "01";
                                    break;

                                case "APMUA":
                                case "APPO":
                                    strModule = "02";
                                    break;

                                case "ARBAN":
                                case "ARSO":
                                    strModule = "04";
                                    break;

                                case "INNHAPXUAT":
                                    strModule = "05";
                                    break;

                                case "GLKETOAN":
                                    strModule = "80";
                                    break;
                            }
                            string newStt = Common.GetNewStt(strModule, true);
                            while (DataTool.SQLCheckExist(strTable_Ph, "Stt", newStt))
                            {
                                newStt = Common.GetNewStt(strModule, true);
                            }
                            SQLExec.Execute(string.Concat(new object[] {
                                                                        "DELETE FROM ", strTable_Ct, " WHERE Ma_Ct = '", row4["Ma_Ct"], "' AND Ngay_Ct = '", Library.DateToStr((DateTime) row4["Ngay_Ct"]), "' AND So_Ct = '", row4["So_Ct"], "' AND Ma_DvCs = '", Element.sysMa_DvCs, "';\r\n\t\t\t\t\t\t\t\t\t\t\t DELETE FROM ", strTable_Ph, " WHERE Ma_Ct = '", row4["Ma_Ct"], "' AND Ngay_Ct = '", Library.DateToStr((DateTime) row4["Ngay_Ct"]),
                                                                        "' AND So_Ct = '", row4["So_Ct"], "' AND Ma_DvCs = '", Element.sysMa_DvCs, "'"
                                                                     }));
                            if (row5 != null)
                            {
                                command.CommandText = strSp_UpdatePh;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Clear();
                                string str10 = "Object_id = Object_id('" + strSp_UpdatePh + "')";
                                DataTable table6 = DataTool.SQLGetDataTable("Sys.Parameters", "Name", str10, null);
                                command.Parameters.AddWithValue("@strNew_Edit", 'N');
                                Common.SetDefaultDataRow(ref row5);
                                row5["Stt"] = newStt;
                                foreach (DataRow row6 in table6.Rows)
                                {
                                    str11 = ((string)row6["Name"]).Replace("@", "");
                                    this.ConvertFontImport(row5, str11);
                                    if (row5.Table.Columns.Contains(str11))
                                    {
                                        command.Parameters.AddWithValue("@" + str11, row5[str11]);
                                    }
                                }
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception exception1)
                                {
                                    exception = exception1;
                                    MessageBox.Show("Có lỗi xảy ra :" + exception.Message);
                                    transaction.Rollback();
                                }
                            }
                            command.Parameters.Clear();
                            command.CommandText = strSp_UpdateCt;
                            command.CommandType = CommandType.StoredProcedure;
                            string strKey = "Object_id = Object_id('" + strSp_UpdateCt + "')";
                            DataTable table7 = DataTool.SQLGetDataTable("Sys.Parameters", "Name", strKey, null);
                            int num = 1;
                            foreach (DataRow row7 in drArr)
                            {
                                command.Parameters.Clear();
                                DataRow row8 = row7;
                                Common.SetDefaultDataRow(ref row8);
                                row8["Stt"] = newStt;
                                row8["Stt0"] = ++num;
                                if (row8.Table.Columns.Contains("Duyet"))
                                {
                                    row8["Duyet"] = true;
                                }
                                foreach (DataRow row6 in table7.Rows)
                                {
                                    str11 = ((string)row6["Name"]).Replace("@", "");
                                    this.ConvertFontImport(row8, str11);
                                    if (row8.Table.Columns.Contains(str11))
                                    {
                                        command.Parameters.AddWithValue("@" + str11, row8[str11]);
                                    }
                                }
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception exception2)
                                {
                                    exception = exception2;
                                    MessageBox.Show("Có lỗi xảy ra trong quá trình import  :" + exception.Message);
                                    command.Transaction.Rollback();
                                }
                            }
                            transaction.Commit();
                        }
                    }
                }
                this.SetKeyFillter();
                this.FillData(this.strKey_Ph, this.strKey_Ct);
                Common.MsgOk(Languages.GetLanguage("End_Process"));
            }
        }

        public override void EpointRelease()
        {
            Import_Excel(true);
        }
        #endregion

        /// <summary>
        /// //////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion

        #region Event

        void frmViewPh_Resize(object sender, EventArgs e)
        {
            this.FormLayout();
        }

        void KeyDownEvent(object sender, KeyEventArgs e)
        {
            string strColumnName = dgvViewPh.dgvGridView.FocusedColumn.Name;
            if (bdsViewPh.Position >= 0)
                drCurrent = ((DataRowView)bdsViewPh.Current).Row;
            switch (e.KeyCode)
            {
                case Keys.F9:
                    this.Filter();
                    break;

                case Keys.F7:
                    switch (e.Modifiers)
                    {
                        case Keys.Shift:
                            Design();
                            break;

                        case Keys.Control:
                            Print(true);
                            break;

                        case Keys.None:
                            {
                                if (this.IsDmsInvoice)
                                    PrintRpt(true);
                                else
                                    Print(false);
                            }
                            break;
                    }
                    break;

                case Keys.Space:
                    Mark();

                    break;

                case Keys.A:
                    if (strColumnName == "MARK")
                        if (e.Modifiers == Keys.Control)
                        {
                            for (int i = 0; i < dgvViewPh.dgvGridView.RowCount; i++)
                            {
                                dgvViewPh.dgvGridView.SetRowCellValue(i, "MARK", true);
                            }

                            dgvViewPh.Refresh();
                        }

                    if (strColumnName == "CHON" && Common.Inlist(strMa_Ct_List, "IN,INT"))
                        if (e.Modifiers == Keys.Control)
                        {
                            string strListInvoice = string.Empty;

                            for (int i = 0; i < dgvViewPh.dgvGridView.RowCount; i++)
                            {
                                if (dgvViewPh.dgvGridView.Columns["SO_CT_LAP"] != null && dgvViewPh.dgvGridView.GetRowCellValue(i, "SO_CT_LAP").ToString() == string.Empty && !(bool)dgvViewPh.dgvGridView.GetRowCellValue(i, "DUYET"))
                                {
                                    dgvViewPh.dgvGridView.SetRowCellValue(i, "CHON", true);
                                    dtViewPh.AcceptChanges();
                                    //strListInvoice += "," + dgvViewPh.dgvGridView.GetRowCellValue(i, "STT");
                                }
                            }
                            dgvViewPh.Refresh();

                            GetInfoPXK();
                            //strStt_List = string.Empty;
                            //DataRow[] drArrStt = dtViewPh.Select("CHON = true");

                            //if (drArrStt.Length == 0)
                            //{
                            //    lbtStt.Text = "";
                            //    return;
                            //}
                            //for (int i = 0; i < drArrStt.Length; i++)
                            //{
                            //    strStt_List += drArrStt[i]["Stt"].ToString() + ",";
                            //}

                            //Hashtable htPara = new Hashtable();
                            //htPara.Add("STTLIST", strStt_List);
                            //htPara.Add("MA_DVCS", Element.sysMa_DvCs);


                            //lbtStt.Text = SQLExec.ExecuteReturnValue("sp_GetPXKInfo", htPara, CommandType.StoredProcedure).ToString();
                        }

                    break;

                case Keys.U:
                    if (strColumnName == "MARK")
                        if (e.Modifiers == Keys.Control)
                        {
                            for (int i = 0; i < dgvViewPh.dgvGridView.RowCount; i++)
                            {
                                dgvViewPh.dgvGridView.SetRowCellValue(i, "MARK", false);
                            }

                            dgvViewPh.Refresh();
                        }

                    if (strColumnName == "CHON" && this.IsDmsInvoice)
                        if (e.Modifiers == Keys.Control)
                        {
                            for (int i = 0; i < dgvViewPh.dgvGridView.RowCount; i++)
                            {
                                if (dgvViewPh.dgvGridView.Columns["SO_CT_LAP"] != null && dgvViewPh.dgvGridView.GetRowCellValue(i, "SO_CT_LAP").ToString() == string.Empty)
                                    dgvViewPh.dgvGridView.SetRowCellValue(i, "CHON", false);
                            }

                            dgvViewPh.Refresh();
                        }

                    break;


                case Keys.F10:
                    switch (e.Modifiers)
                    {
                        case Keys.None:
                            if (((e.KeyCode == Keys.F10) && Common.CheckPermission(base.Object_ID, enuPermission_Type.Allow_New)) && Common.CheckPermission(base.Object_ID, enuPermission_Type.Allow_Edit))
                            {
                                this.Cursor = Cursors.WaitCursor;
                                //this.Import_Excel(false);                               
                                OpenFileDialog dialog = new OpenFileDialog
                                {
                                    DefaultExt = "xlsx",
                                    Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx"
                                };
                                if (dialog.ShowDialog() == DialogResult.OK)
                                {

                                    this.dtDataExcelImport = Common.ReadExcel(dialog.FileName);
                                }
                                EpointProcessBox.Show(this);
                                this.Cursor = Cursors.Default;
                            }
                            return;

                        case Keys.Control:
                            //this.Export_Excel();
                            break;
                    }
                    break;

            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 && e.Shift)
            {
                this.DanhSoCt();
                return;
            }
            else
                base.OnKeyDown(e);
        }

        void bdsViewPh_PositionChanged(object sender, EventArgs e)
        {
            if (bdsViewPh.Position < 0)
                return;

            //lblRecordNo.Text = (bdsViewPh.Position + 1).ToString() + "/" + bdsViewPh.Count.ToString();

            drCurrent = ((DataRowView)bdsViewPh.Current).Row;
            string strStt = (string)drCurrent["Stt"];

            bdsViewCt.Filter = "(Stt = '" + strStt + "')";
        }

        void btBack_Click(object sender, EventArgs e)
        {
            if (Common.Inlist(strMa_Ct_List, "BG,SO"))
                this.FillDataBG(strFilterKey_Old, strFilterKey_Old);
            else if (this.IsDmsInvoice)//Common.Inlist(strMa_Ct_List, "IN,INT")
                this.FillData_OM(strFilterKey_Old, strFilterKey_Old);
            else
                this.FillData(strFilterKey_Old, strFilterKey_Old);
        }
    void btImport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;                     
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = "xlsx",
                Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.dtDataExcelImport = Common.ReadExcel(dialog.FileName);
                EpointProcessBox.Show(this);
                
            }

            this.Cursor = Cursors.Default;
        }

        void btFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        void btPreview_Click(object sender, EventArgs e)
        {
            this.Print(true);
            //this.PrintRpt(true);
        }

        void btPrint_Click(object sender, EventArgs e)
        {
            this.Print(false);
        }

        void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        void dgvViewCt_Enter(object sender, EventArgs e)
        {
            ExportControl = sender;
        }

        void dgvViewPh_Enter(object sender, EventArgs e)
        {
            ExportControl = sender;
        }
        private void dgvViewPh_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (!this.dgvViewPh.FormatCell)
            //    return;
            if (e.CellValue == null || e.CellValue == DBNull.Value)
                return;

            if (e.RowHandle < 0)
                return;

            GridView gridView = (GridView)sender;




            if ((GridColumn)gridView.Columns["MA_CT"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_CT") != null)
            {
                if (Common.Inlist(strMa_Ct_List, "NM,NK,PT,PC,BN,BC"))
                {
                    if ((GridColumn)gridView.Columns["MA_TTE"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != Element.sysMa_Tte)
                        e.Appearance.ForeColor = usd_color;
                }
            }

        }

        void dgvViewPh_CellFormatting(object sender, RowCellCustomDrawEventArgs e)
        {

            //if (dgvViewPh.dgvGridView.State == GridState.CellSelection || dgvViewPh.dgvGridView.State == GridState.ColumnFilterDown || dgvViewPh.dgvGridView.State == GridState.Scrolling || dgvViewPh.dgvGridView.State == GridState.FilterPanelTextPressed )
            //    return;
            //if (!dgvViewPh.dgvGridView.Editable)
            //    return;
            //if (!Common.Inlist(strMa_Ct_List, "NM,NK,PT,PC,BN,BC"))
            //    return;

            if (e.CellValue == null || e.CellValue == DBNull.Value)
                return;

            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName != "MA_TTE")
                return;

            GridView gridView = (GridView)sender;

            if ((GridColumn)gridView.Columns["MA_TTE"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != string.Empty && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != Element.sysMa_Tte)
                e.Appearance.ForeColor = usd_color;//Color.FromArgb(255, 49, 106, 197);


            //if ((GridColumn)gridView.Columns["MA_CT"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_CT") != null)
            //{
            //    if (Common.Inlist((string)gridView.GetRowCellValue(e.RowHandle, "MA_CT"), "BG,PO,SO,IN,INT"))
            //    {
            //        if ((GridColumn)gridView.Columns["SO_CT_LAP"] != null && gridView.GetRowCellValue(e.RowHandle, "SO_CT_LAP").ToString() == string.Empty && !(bool)gridView.GetRowCellValue(e.RowHandle, "DUYET"))
            //            e.Appearance.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        //if (Common.Inlist((string)gridView.GetRowCellValue(e.RowHandle, "MA_CT"), "NM,NK,PT,PC,BN,BC"))
            //        if ((GridColumn)gridView.Columns["MA_TTE"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != string.Empty && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != Element.sysMa_Tte)
            //            e.Appearance.ForeColor = Color.FromArgb(255, 49, 106, 197);
            //    }

            //}

        }

        /*void dgvViewPh_CellFormatting0(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue == null || e.CellValue == DBNull.Value)
                return;

            if (e.RowHandle < 0)
                return;          


            GridView gridView = (GridView)sender;



            //if ((GridColumn)gridView.Columns["MA_CT"] == null || gridView.GetRowCellValue(e.RowHandle, "MA_CT") == null)
            //    return;



            //if (dgvViewPh.Columns.Contains("Ma_Tte"))
            //{
            //    if (dgvViewPh.Rows[e.RowIndex].Cells["Ma_Tte"].Value != null)
            //    {
            //        if (dgvViewPh.Rows[e.RowIndex].Cells["Ma_Tte"].Value.ToString() != Element.sysMa_Tte)
            //            e.CellStyle.ForeColor = Color.FromArgb(255, 49, 106, 197);
            //        else
            //            e.CellStyle.ForeColor = dgvViewPh.DefaultCellStyle.ForeColor;
            //    }
            //}
            //if (dgvViewPh.Columns.Contains("Mark"))
            //{
            //    if (dgvViewPh.Rows[e.RowIndex].Cells["Mark"].Value != null)
            //    {
            //        if ((bool)dgvViewPh.Rows[e.RowIndex].Cells["Mark"].Value == true)
            //        {
            //            e.CellStyle.BackColor = Color.FromArgb(255, 0, 0, 255);
            //        }
            //    }
            //}
            //if (Common.Inlist(dgvViewPh.Rows[e.RowIndex].Cells["Ma_Ct"].Value.ToString(), "IN,BG,PO,SO,LSX"))
            //{
            //    if (dgvViewPh.Columns.Contains("So_Ct_Lap"))
            //    {
            //        if ((string)dgvViewPh.Rows[e.RowIndex].Cells["So_Ct_Lap"].Value == "")
            //            e.CellStyle.ForeColor = Color.Red;
            //    }
            //}


            if ((GridColumn)gridView.Columns["MA_CT"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_CT") != null)
            {
                if (Common.Inlist((string)gridView.GetRowCellValue(e.RowHandle, "MA_CT"), "BG,PO,SO,IN,INT"))
                    if ((GridColumn)gridView.Columns["SO_CT_LAP"] != null && gridView.GetRowCellValue(e.RowHandle, "SO_CT_LAP").ToString() == string.Empty && !(bool)gridView.GetRowCellValue(e.RowHandle, "DUYET"))
                        e.Appearance.ForeColor = Color.Red;

                if (Common.Inlist((string)gridView.GetRowCellValue(e.RowHandle, "MA_CT"), "NM,NK,PT,PC,BN,BC"))
                {
                    if ((GridColumn)gridView.Columns["MA_TTE"] != null && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != string.Empty && gridView.GetRowCellValue(e.RowHandle, "MA_TTE").ToString() != Element.sysMa_Tte)
                        e.Appearance.ForeColor = Color.FromArgb(255, 49, 106, 197);
                }

            }

        }*/

        void dgvViewPh_CellMouseClick(object sender, EventArgs e)
        {
            if (bdsViewPh.Position < 0)
                return;

            string strColumnName = dgvViewPh.dgvGridView.FocusedColumn.Name.ToUpper();

            drCurrent = ((DataRowView)bdsViewPh.Current).Row;
            string strStt = drCurrent["Stt"].ToString();
            if (strColumnName == "DUYET")
            {
                frmDuyet frm = new frmDuyet();
                frm.Load(drCurrent);
            }
            else if (strColumnName == "CHON")
            {
                if (drCurrent["So_Ct_Lap"].ToString() == "")//&& !(bool)drCurrent["Duyet"]
                    drCurrent["CHON"] = !Convert.ToBoolean(drCurrent["CHON"]);
                else
                    MessageBox.Show("Chứng từ đã xuất kho!");

                drCurrent.AcceptChanges();
            }
            else if (strColumnName == "MARK")
            {

                drCurrent["MARK"] = !Convert.ToBoolean(drCurrent["MARK"]);
                drCurrent.AcceptChanges();
            }
            if (strColumnName == "TAX")
            {
                bool IsCheckTranferTax = true;

                if (IsCheckTranferTax)
                {
                    bool IsAccept = Common.MsgYes_No("Chứng từ đã được thay đổi, bạn có muốn chuyển lại không?");
                    if (IsAccept)
                    {

                    }
                }
                else
                {

                }
            }



            GetInfoPXK();


        }


        void GetInfoPXK()
        {
            if (!this.IsDmsInvoice)
                return;


            dtSttList = new DataTable();
            dtSttList.Columns.Add(new DataColumn("Stt"));


            strStt_List = string.Empty;
            DataRow[] drArrStt = dtViewPh.Select("CHON = true");

            if (drArrStt.Length == 0)
            {
                lbtStt.Text = "";
                return;
            }

            for (int i = 0; i < drArrStt.Length; i++)
            {
                //strStt_List += drArrStt[i]["Stt"].ToString() + ",";
                DataRow drStt = dtSttList.NewRow();
                drStt["Stt"] = drArrStt[i]["Stt"].ToString();
                dtSttList.Rows.Add(drStt);

            }


            DataTable dtReturn = new DataTable();
            SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "IN_GetPXKInfo";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@SttList",
                TypeName = "TVP_STTLIST",
                Value = dtSttList
            };
            command.Parameters.Add(parameter);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dtReturn);

                lbtStt.Text = dtReturn.Rows[0][0].ToString();
            }
            catch
            {
                lbtStt.Text = string.Empty;
            }

            //Hashtable htPara = new Hashtable();
            //htPara.Add("STTLIST", strStt_List);
            //htPara.Add("MA_DVCS", Element.sysMa_DvCs);
            //lbtStt.Text = SQLExec.ExecuteReturnValue("sp_GetPXKInfo", htPara, CommandType.StoredProcedure).ToString();

        }

        #endregion
    }
}
