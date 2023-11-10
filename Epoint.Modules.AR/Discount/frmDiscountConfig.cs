using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems;
using Epoint.Systems.Controls;
using Epoint.Systems.Customizes;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using System.Collections;
using System.Data.SqlClient;
using Epoint.Systems.Elements;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Epoint.Modules.AR
{
    public partial class frmDiscountConfig : Epoint.Systems.Customizes.frmView
    {

        string strMa_CTKM = String.Empty;
        string strStt = String.Empty;
        DataTable dtImport;
        string strError = string.Empty;


        DataSet dsDiscountProg;

        DataTable dtDiscountProg;
        BindingSource bdsDiscountProg = new BindingSource();
        DataRow drCurrentDicsProg;


        DataTable dtDiscBreak;
        BindingSource bdsDiscBreak = new BindingSource();
        DataRow drCurrentBreak;

        DataTable dtDiscItem;
        BindingSource bdsDiscItem = new BindingSource();
        DataRow drCurentItem;

        DataTable dtDiscFreeItem;
        BindingSource bdsDiscFreeItem = new BindingSource();
        DataRow drCurrentFreeItem;

        DataTable dtDiscCust;
        BindingSource bdsDiscCust = new BindingSource();
        DataRow drCurrentDiscCust;

        DataTable dtDiscGroupCust;
        BindingSource bdsDiscGroupCust = new BindingSource();
        DataRow drCurrentDiscGroupCust;

        DataTable dtDiscGroupItem;
        BindingSource bdsDiscGroupItem = new BindingSource();
        DataRow drCurrentDiscGroupItem;

        public frmDiscountConfig()
        {
            InitializeComponent();

            dgvDiscCustomer.KeyDown += new KeyEventHandler(DgvDiscCustomer_KeyDown);

            bdsDiscountProg.PositionChanged += new EventHandler(bdsDiscountProg_PositionChanged);
            bdsDiscBreak.PositionChanged += new EventHandler(bdsDiscBreak_PositionChanged);
            this.btFillterData.Click += new EventHandler(btFillData_Click);
            this.btImport.Click += new EventHandler(btImport_Click);
        }

        

        public override void Load()
        {
            this.Build();
            this.FillData();

            this.Show();
        }

        private void Build()
        {
            dgvDiscountProg.strZone = "OM_DISCOUNT";
            dgvDiscountProg.BuildGridView(this.isLookup);
            ExportControl = dtDiscountProg;

            dgvDiscFreeItem.strZone = "OM_DiscFreeItem";
            dgvDiscFreeItem.BuildGridView(this.isLookup);
            ExportControl = dtDiscFreeItem;


            dgvDiscBreak.strZone = "OM_DISCBREAK";
            dgvDiscBreak.BuildGridView(this.isLookup);
            ExportControl = dtDiscBreak;


            dgvDiscItem.strZone = "OM_DISCITEM";
            dgvDiscItem.BuildGridView(this.isLookup);
            ExportControl = dtDiscItem;


            dgvDiscCustomer.strZone = "OM_DISCCUST";
            dgvDiscCustomer.BuildGridView(this.isLookup);
            ExportControl = dtDiscCust;


            dgvDiscGroupFreeItem.strZone = "OM_DISCGROUP";
            dgvDiscGroupFreeItem.BuildGridView(this.isLookup);
            ExportControl = dtDiscGroupItem;

            dgvDiscCustGroup.strZone = "OM_DISCCUSTGROUP";
            dgvDiscCustGroup.BuildGridView(this.isLookup);
            ExportControl = dtDiscGroupItem;


            dteNgay_BD.Text =Library.DateToStr( Epoint.Systems.Elements.Element.sysNgay_Ct1);
            dteNgay_Kt.Text = Library.DateToStr(Epoint.Systems.Elements.Element.sysNgay_Ct2);

            tabDiscCountDetail.HideAllTabPage();
            tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Dt);
            tabDiscCountDetail.ShowPageInTabControl(tpDiscCust);
            tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Vt);
           
        }

        private void FillData()
        {

            //Hashtable htDisc = new Hashtable();
            //htDisc["NGAY_CT1"] = ;
            //htDisc["NGAY_CT2"] = dteNgay_Kt.Text;

            dsDiscountProg = Discount.GetALLDiscoutProg(dteNgay_BD.Text, dteNgay_Kt.Text);

            dtDiscountProg = dsDiscountProg.Tables[0];
            bdsDiscountProg.DataSource = dtDiscountProg;
            dgvDiscountProg.DataSource = bdsDiscountProg;

            //Uy quyen cho lop co so tim kiem
            bdsSearch = bdsDiscountProg;
            ExportControl = dgvDiscountProg;

            if (bdsDiscountProg.Count >= 0)
                bdsDiscountProg.Position = 0;

            // Điêu kiện khuyến mãi
            //dtDiscBreak = DataTool.SQLGetDataTable("OM_DiscBreak", null, "", null);
            dtDiscBreak = dsDiscountProg.Tables[1];
            bdsDiscBreak.DataSource = dtDiscBreak;
            dgvDiscBreak.DataSource = bdsDiscBreak;



            //dtDiscFreeItem = DataTool.SQLGetDataTable("OM_DiscFreeItem", null, "", null);
            dtDiscFreeItem = dsDiscountProg.Tables[2];
            bdsDiscFreeItem.DataSource = dtDiscFreeItem;
            dgvDiscFreeItem.DataSource = bdsDiscFreeItem;


            //dtDiscItem = DataTool.SQLGetDataTable("OM_DiscItem", null, "", null);
            dtDiscItem = dsDiscountProg.Tables[3];
            bdsDiscItem.DataSource = dtDiscItem;
            dgvDiscItem.DataSource = bdsDiscItem;

            //dtDiscCust = DataTool.SQLGetDataTable("OM_DiscCust", null, "", null);
            dtDiscCust = dsDiscountProg.Tables[4];
            bdsDiscCust.DataSource = dtDiscCust;
            dgvDiscCustomer.DataSource = bdsDiscCust;

            //dtDiscGroupCust = DataTool.SQLGetDataTable("OM_DiscCustGroup", null, "", null); 
            dtDiscGroupCust = dsDiscountProg.Tables[5];
            bdsDiscGroupCust.DataSource = dtDiscGroupCust;
            dgvDiscCustGroup.DataSource = bdsDiscGroupCust;

            //dtDiscGroupItem = DataTool.SQLGetDataTable("OM_DiscGroupFreeItem", null, "", null);
             dtDiscGroupItem = dsDiscountProg.Tables[6];
            bdsDiscGroupItem.DataSource = dtDiscGroupItem;
            dgvDiscGroupFreeItem.DataSource = bdsDiscGroupItem;
        }

        public override void Edit(enuEdit enuNew_Edit)
        {
            if (dgvDiscountProg.Focused)
                EditDiscountProg(enuNew_Edit);
            else if (dgvDiscBreak.Focused)
                EditDiscBreak(enuNew_Edit);
            else if (dgvDiscFreeItem.Focused)
                EditDiscFreeItem(enuNew_Edit);
            else if (dgvDiscItem.Focused)
                EditDiscItem(enuNew_Edit);
            else if (dgvDiscCustomer.Focused)
                EditDiscCustomer(enuNew_Edit);
            else if (dgvDiscGroupFreeItem.Focused)
                EditDiscGroupItem(enuNew_Edit);
            else if (dgvDiscCustGroup.Focused)
                EditDiscGroupCust(enuNew_Edit);
        }
        private void EditDiscountProg(enuEdit enuNew_Edit)
        {
            if (bdsDiscountProg.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai
            if (bdsDiscountProg.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);
            else
                drCurrentDicsProg = dtDiscountProg.NewRow();

            frmDiscountConfig_Edit frmEdit = new frmDiscountConfig_Edit();
            frmEdit.Object_ID = this.Object_ID;
            frmEdit.Load(enuNew_Edit, drCurrentDicsProg);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New || enuNew_Edit == enuEdit.Copy)
                {
                    if (bdsDiscountProg.Position >= 0)
                        dtDiscountProg.ImportRow(drCurrentDicsProg);
                    else
                        dtDiscountProg.Rows.Add(drCurrentDicsProg);

                    bdsDiscountProg.Position = bdsDiscountProg.Find("Ma_CTKM", drCurrentDicsProg["Ma_CTKM"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrentDicsProg, ((DataRowView)bdsDiscountProg.Current).Row);
                }

                dtDiscountProg.AcceptChanges();
            }
            else
                dtDiscountProg.RejectChanges();

        }
        private void EditDiscBreak(enuEdit enuNew_Edit)
        {
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai
            if (bdsDiscBreak.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);
            else
                drCurrentBreak = dtDiscBreak.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);
            this.strMa_CTKM = drCurrentDicsProg["MA_CTKM"].ToString();

            if (enuNew_Edit == enuEdit.New)
            {
                drCurrentBreak["Ma_CTKM"] = this.strMa_CTKM;

                Hashtable htPara = new Hashtable();
                htPara.Add("MA_CTKM", this.strMa_CTKM);
                string strStt = SQLExec.ExecuteReturnValue("sp_OM_GetDiscoutStt", htPara, CommandType.StoredProcedure).ToString();

                drCurrentBreak["STT"] = strStt;

            }

            frmDiscBreak_Edit frmEdit = new frmDiscBreak_Edit();
            frmEdit.Load(enuNew_Edit, drCurrentBreak);



            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsDiscBreak.Position >= 0)
                        dtDiscBreak.ImportRow(drCurrentBreak);
                    else
                        dtDiscBreak.Rows.Add(drCurrentBreak);

                    bdsDiscBreak.Position = bdsDiscBreak.Find("STT", drCurrentBreak["STT"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrentBreak, ((DataRowView)bdsDiscBreak.Current).Row);
                }

                dtDiscBreak.AcceptChanges();
            }
            else
                dtDiscBreak.RejectChanges();

        }
        private void EditDiscFreeItem(enuEdit enuNew_Edit)
        {
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

            if (bdsDiscFreeItem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;
            //Copy hang hien tai CTKM
            if (bdsDiscountProg.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);

            //if (drCurrentDicsProg["Loai_KM"].ToString() == "G")
            //    return;

            //Copy hang hien tai
            if (bdsDiscFreeItem.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscFreeItem.Current).Row, ref drCurrentFreeItem);
            else
                drCurrentFreeItem = dtDiscFreeItem.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);


            if (enuNew_Edit == enuEdit.New)
            {
                drCurrentFreeItem["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];

                drCurrentFreeItem["STT"] = drCurrentBreak["STT"];

            }


            frmDiscFreeItem_Edit frmEdit = new frmDiscFreeItem_Edit();
            frmEdit.Load(enuNew_Edit, drCurrentFreeItem);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsDiscFreeItem.Position >= 0)
                        dtDiscFreeItem.ImportRow(drCurrentFreeItem);
                    else
                        dtDiscFreeItem.Rows.Add(drCurrentFreeItem);

                    bdsDiscFreeItem.Position = bdsDiscFreeItem.Find("STT", drCurrentFreeItem["STT"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrentFreeItem, ((DataRowView)bdsDiscFreeItem.Current).Row);
                }

                dtDiscFreeItem.AcceptChanges();
            }
            else
                dtDiscFreeItem.RejectChanges();

        }
        private void EditDiscItem(enuEdit enuNew_Edit) // Thêm mặt hàng bán
        {
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

            if (bdsDiscItem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai
            if (bdsDiscItem.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscItem.Current).Row, ref drCurentItem);
            else
                drCurentItem = dtDiscItem.NewRow();

            if (enuNew_Edit == enuEdit.New)
            {
                drCurentItem["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];

                drCurentItem["STT"] = drCurrentBreak["STT"];

            }

            if (enuNew_Edit == enuEdit.New && false) 
            {
                string strValue = string.Empty;
                string strValueList = string.Empty;
                string[] KeyAr = new string[3] { "Ma_CtKm", "Stt", "Ma_Vt" };
                string[] ValueAr;

                bool bRequire = true;
                DataRow drLookup = Lookup.ShowMultiLookupNew("Ma_Vt", strValue, bRequire, "", "","");
                //DataRow drLookup = Lookup.ShowLookup(frmLookup, "SYSREPORT", "REPORT_ID", strValue, bRequire, "","","Stt");

                if (bRequire && drLookup == null)
                {
                    dtDiscItem.RejectChanges();
                    return;
                }
                strValueList = drLookup["MuiltiSelectValue"].ToString();
                if (drLookup != null && strValueList != string.Empty)
                {

                    foreach (string strMa_Vt in strValueList.Split(','))
                    {
                        ValueAr = new string[3] { drCurrentBreak["Ma_CTKM"].ToString(), drCurrentBreak["STT"].ToString(), strMa_Vt };

                        DataRow dtVt = DataTool.SQLGetDataRowByID("LIVATTU", "Ma_Vt", strMa_Vt);
                        drCurentItem["Ma_Vt"] = strMa_Vt;
                        drCurentItem["Dvt"] = dtVt["DVT"];
                        drCurentItem["Ten_Vt"] = dtVt["Ten_Vt"];

                        if (!DataTool.SQLCheckExist("OM_DiscItem", KeyAr, ValueAr))
                        {
                            if (DataTool.SQLUpdate(enuEdit.New, "OM_DiscItem", ref drCurentItem))
                            {
                                if (bdsDiscItem.Position >= 0)
                                    dtDiscItem.ImportRow(drCurentItem);
                                else
                                    dtDiscItem.Rows.Add(drCurentItem);


                                dtDiscItem.AcceptChanges();
                            }
                            else
                                dtDiscItem.RejectChanges();
                        }
                    }
                }
            }
            else
            {
                frmDiscItem_Edit frmEdit = new frmDiscItem_Edit();
                frmEdit.Load(enuNew_Edit, drCurentItem);

                //Accept
                if (frmEdit.isAccept)
                {
                    if (true) //DataTool.SQLUpdate(enuNew_Edit, "OM_DiscItem", ref drCurentItem
                    {
                        if (enuNew_Edit == enuEdit.New)
                        {
                            if (bdsDiscItem.Position >= 0)
                                dtDiscItem.ImportRow(drCurentItem);
                            else
                                dtDiscItem.Rows.Add(drCurentItem);

                            bdsDiscItem.Position = bdsDiscItem.Find("MA_VT", drCurentItem["MA_VT"]);
                        }
                        else
                        {
                            Common.CopyDataRow(drCurentItem, ((DataRowView)bdsDiscItem.Current).Row);
                        }
                    }
                    dtDiscItem.AcceptChanges();
                }
                else
                    dtDiscItem.RejectChanges();
            }
        }
        private void EditDiscCustomer(enuEdit enuNew_Edit)
        {
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

            if (bdsDiscFreeItem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            ////Copy hang hien tai CTKM
            //if (bdsDiscountProg.Position >= 0)
            //    Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);

            //if (drCurrentDicsProg["Loai_KM"].ToString() == "G")
            //    return;

            //Copy hang hien tai
            if (bdsDiscCust.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscCust.Current).Row, ref drCurrentDiscCust);
            else
                drCurrentDiscCust = dtDiscCust.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);


            if (enuNew_Edit == enuEdit.New)
            {
                drCurrentDiscCust["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];

                drCurrentDiscCust["STT"] = drCurrentBreak["STT"];

            }

            string strValue = string.Empty;
            string strValueList = string.Empty;
            string [] KeyAr =  new string[3] {"Ma_CtKm","Stt","Ma_Dt"};
            string[] ValueAr;

            bool bRequire = true;
            DataRow drLookup = Lookup.ShowMultiLookup("Ma_Dt",strValue,bRequire, "","");
            //DataRow drLookup = Lookup.ShowLookup(frmLookup, "SYSREPORT", "REPORT_ID", strValue, bRequire, "","","Stt");
            strValueList = drLookup["MuiltiSelectValue"].ToString();


            if (bRequire && drLookup == null)
            {
                dtDiscCust.RejectChanges();
                return;
            }

            if (drLookup != null && strValueList != string.Empty)
            {

                foreach (string strMa_Dt in strValueList.Split(','))
                {
                    ValueAr = new string[3] {drCurrentDiscCust["Ma_CTKM"].ToString(),drCurrentDiscCust["STT"].ToString(),strMa_Dt};

                    drCurrentDiscCust["Ma_Dt"] = strMa_Dt;
                    drCurrentDiscCust["Ten_Dt"] = DataTool.SQLGetNameByCode("LIDOITUONG","Ma_Dt","Ten_Dt",strMa_Dt);

                    if (!DataTool.SQLCheckExist("OM_DISCCUST", KeyAr, ValueAr))
                    {
                        if (DataTool.SQLUpdate(enuEdit.New, "OM_DISCCUST", ref drCurrentDiscCust))
                        {
                            if (bdsDiscCust.Position >= 0)
                                dtDiscCust.ImportRow(drCurrentDiscCust);
                            else
                                dtDiscCust.Rows.Add(drCurrentDiscCust);


                            dtDiscCust.AcceptChanges();
                        }
                        else
                            dtDiscCust.RejectChanges();
                    }
                }
            }
        }
        private void EditDiscGroupItem(enuEdit enuNew_Edit)
        {


            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

            if (bdsDiscFreeItem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai CTKM
            if (bdsDiscountProg.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);

            if (drCurrentDicsProg["Loai_KM"].ToString() == "L")
                return;


            //Copy hang hien tai
            if (bdsDiscGroupItem.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscGroupItem.Current).Row, ref drCurrentDiscGroupItem);
            else
                drCurrentDiscGroupItem = dtDiscGroupItem.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);


            if (enuNew_Edit == enuEdit.New)
            {
                drCurrentDiscGroupItem["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];

                drCurrentDiscGroupItem["STT"] = drCurrentBreak["STT"];

            }

            string strValue = string.Empty;
            bool bRequire = true;
            DataRow drLookup = Lookup.ShowLookup("Ma_Nh_Vt", strValue, bRequire, "");
            //DataRow drLookup = Lookup.ShowLookup(frmLookup, "SYSREPORT", "REPORT_ID", strValue, bRequire, "","","Stt");

            if (bRequire && drLookup == null)
            {
                dtDiscGroupItem.RejectChanges();
                return;
            }

            if (drLookup != null)
            {

                drCurrentDiscGroupItem["Ma_Nh_Vt"] = drLookup["Ma_Nh_Vt"];
                drCurrentDiscGroupItem["Ten_Nh_Vt"] = drLookup["Ten_Nh_Vt"];

                if (DataTool.SQLUpdate(enuEdit.New, "OM_DiscGroupFreeItem", ref drCurrentDiscGroupItem))
                {
                    if (bdsDiscGroupItem.Position >= 0)
                        dtDiscGroupItem.ImportRow(drCurrentDiscGroupItem);
                    else
                        dtDiscGroupItem.Rows.Add(drCurrentDiscGroupItem);


                    dtDiscGroupItem.AcceptChanges();
                }
                else
                    dtDiscGroupItem.RejectChanges();
            }

        }
        private void EditDiscGroupCust(enuEdit enuNew_Edit)
        {
            
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

            if (bdsDiscFreeItem.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai CTKM
            if (bdsDiscountProg.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);

            //if (drCurrentDicsProg["Loai_KM"].ToString() == "L")
            //    return;


            //Copy hang hien tai
            if (bdsDiscGroupCust.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscGroupCust.Current).Row, ref drCurrentDiscGroupCust);
            else
                drCurrentDiscGroupCust = dtDiscGroupCust.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);


            if (enuNew_Edit == enuEdit.New)
            {
                drCurrentDiscGroupCust["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];
                drCurrentDiscGroupCust["STT"] = drCurrentBreak["STT"];

            }

            string strValue = string.Empty;
            bool bRequire = true;
            DataRow drLookup = Lookup.ShowLookup("Ma_Nh_Dt", strValue, bRequire, "");
            //DataRow drLookup = Lookup.ShowLookup(frmLookup, "SYSREPORT", "REPORT_ID", strValue, bRequire, "","","Stt");

            if (bRequire && drLookup == null)
            {
                dtDiscGroupCust.RejectChanges();
                return;
            }

            if (drLookup != null)
            {

                drCurrentDiscGroupCust["Ma_Nh_Dt"] = drLookup["Ma_Nh_Dt"];
                drCurrentDiscGroupCust["Ten_Nh_Dt"] = drLookup["Ten_Nh_Dt"];

                if (DataTool.SQLUpdate(enuEdit.New, "OM_DiscCustGroup", ref drCurrentDiscGroupCust))
                {
                    if (bdsDiscGroupCust.Position >= 0)
                        dtDiscGroupCust.ImportRow(drCurrentDiscGroupCust);
                    else
                        dtDiscGroupCust.Rows.Add(drCurrentDiscGroupCust);


                    dtDiscGroupCust.AcceptChanges();
                }
                else
                    dtDiscGroupCust.RejectChanges();
            }

        }

        public override void Delete()
        {
            if (dgvDiscountProg.Focused)
                DeleteDiscountProg();
            else if (dgvDiscBreak.Focused)
                DeleteDiscBreak();
            else if (dgvDiscFreeItem.Focused)
                DeleteDiscFreeItem();
            else if (dgvDiscItem.Focused)
                DeleteDiscItem();
            else if (dgvDiscCustomer.Focused)
                DeleteDiscCustomer();
            else if (dgvDiscGroupFreeItem.Focused)
                DeleteDiscGroup();
            else if (dgvDiscCustGroup.Focused)
                DeleteCustGroup();
        }


        private void DeleteDiscountProg()
        {
            if (bdsDiscountProg.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsDiscountProg.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;


            Hashtable htPara = new Hashtable();
            htPara["MA_CTKM"] = drCurrent["MA_CTKM"].ToString();
            bool isCheck = Convert.ToBoolean(SQLExec.ExecuteReturnValue("sp_DELETE_DISCOUNT", htPara, CommandType.StoredProcedure));
            if (isCheck)
            {
                bdsDiscountProg.RemoveAt(bdsDiscountProg.Position);
                dtDiscountProg.AcceptChanges();
            }
            else
            {
                EpointMessage.MsgOk("Chương trình khuyến mãi/ Chiết khấu đã được sử dụng");
            }
        }
        private void DeleteDiscBreak()
        {
            if (bdsDiscBreak.Position < 0)
                return;

            DataRow drCurrentDiscBreak = ((DataRowView)bdsDiscBreak.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;


            Hashtable htPara = new Hashtable();
            htPara["MA_CTKM"] = drCurrentDiscBreak["MA_CTKM"].ToString();
            htPara["STT"] = drCurrentDiscBreak["STT"].ToString();
            bool isCheck = Convert.ToBoolean(SQLExec.ExecuteReturnValue("sp_DELETE_DISCBREAK", htPara, CommandType.StoredProcedure));
            if (isCheck)
            {
                bdsDiscBreak.RemoveAt(bdsDiscBreak.Position);
                dtDiscBreak.AcceptChanges();
            }
            else
            {
                EpointMessage.MsgOk("Chương trình khuyến mãi/ Chiết khấu đã được sử dụng");
            }
        }
        private void DeleteDiscFreeItem()
        {
            if (bdsDiscFreeItem.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsDiscFreeItem.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("OM_DiscFreeItem", drCurrent))
            {
                bdsDiscFreeItem.RemoveAt(bdsDiscFreeItem.Position);
                dtDiscFreeItem.AcceptChanges();
            }
        }
        private void DeleteDiscItem()
        {
            if (bdsDiscItem.Position < 0)
                return;

            DataRow drCurrentDiscItem = ((DataRowView)bdsDiscItem.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("OM_DiscItem", drCurrentDiscItem))
            {
                bdsDiscItem.RemoveAt(bdsDiscItem.Position);
                dtDiscItem.AcceptChanges();
            }
        }
        private void DeleteDiscCustomer()
        {
            if (bdsDiscCust.Position < 0)
                return;

            DataRow drCurrentDiscCust = ((DataRowView)bdsDiscCust.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("OM_DiscCust", drCurrentDiscCust))
            {
                bdsDiscCust.RemoveAt(bdsDiscCust.Position);
                dtDiscCust.AcceptChanges();
            }
        }
        private void DeleteDiscGroup()
        {
            if (bdsDiscGroupItem.Position < 0)
                return;

            DataRow drCurrentDiscGroup = ((DataRowView)bdsDiscGroupItem.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;
            Hashtable htParaDiscGroup = new Hashtable();
            htParaDiscGroup["MA_CTKM"] = drCurrentDiscGroup["Ma_CTKM"].ToString();
            htParaDiscGroup["STT"] = drCurrentDiscGroup["Stt"].ToString();
            htParaDiscGroup["MA_NH_VT"] = drCurrentDiscGroup["Ma_Nh_Vt"].ToString();
            string[] KeyAr = new string[3] {"" ,"" , "" };

            if (SQLExec.Execute("SP_Delete_OM_DiscGroupFreeItem", htParaDiscGroup, CommandType.StoredProcedure))
            {
                bdsDiscGroupItem.RemoveAt(bdsDiscGroupItem.Position);
                dtDiscGroupItem.AcceptChanges();
            }
        }
        private void DeleteCustGroup()
        {
            if (bdsDiscGroupCust.Position < 0)
                return;

            DataRow drCurrentCustGroup = ((DataRowView)bdsDiscGroupCust.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            //string List[]= new string{[drCurrentCustGroup[""],drCurrentCustGroup[""],drCurrentCustGroup[""]};
            //string[] ParAr = new string[3] { "Ma_CTKM","Stt","Ma_Nh_Dt" };
            //  string[] KeyAr = new string[3] {drCurrentCustGroup["Ma_CTKM"].ToString(),drCurrentCustGroup["Stt"].ToString(),drCurrentCustGroup["Ma_Nh_Dt"].ToString() };
            Hashtable htParaDiscGroup = new Hashtable();
            htParaDiscGroup["MA_CTKM"] = drCurrentCustGroup["Ma_CTKM"].ToString();
            htParaDiscGroup["STT"] = drCurrentCustGroup["Stt"].ToString();
            htParaDiscGroup["MA_NH_DT"] = drCurrentCustGroup["Ma_Nh_Dt"].ToString();
            if (SQLExec.Execute("SP_Delete_OM_DiscCustGroup", htParaDiscGroup, CommandType.StoredProcedure))
            {
                bdsDiscGroupCust.RemoveAt(bdsDiscGroupCust.Position);
                dtDiscGroupCust.AcceptChanges();
            }
        }
        #region Sự kiện

        void bdsDiscountProg_PositionChanged(object sender, EventArgs e)
        {           
            if (bdsDiscountProg.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscountProg.Current).Row, ref drCurrentDicsProg);

            this.strMa_CTKM = drCurrentDicsProg["Ma_CTKM"].ToString();
            string strLoai_Km = drCurrentDicsProg["Loai_KM"].ToString();
            string strLoai_Ap_KM = drCurrentDicsProg["Loai_Ap_KM"].ToString();
            bdsDiscBreak.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";
            bdsDiscItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";
            bdsDiscCust.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";
            bdsDiscGroupItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";
            bdsDiscFreeItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";
            bdsDiscGroupCust.Filter = "Ma_CTKM = '" + strMa_CTKM + "'";


            if (dtDiscBreak != null)
            {
                //BindingSource bs = (BindingSource)bdsDiscBreak;
                DataTable dtDiscBreakFilter = Common.FilterDatatable(dtDiscBreak, "Ma_CTKM = '" + strMa_CTKM + "'");

                if (dtDiscBreakFilter != null && dtDiscBreakFilter.Rows.Count > 0)
                {
                    bdsDiscItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + dtDiscBreakFilter.Rows[0]["Stt"].ToString() + "'";
                }

                if (bdsDiscBreak.Position >= 0)
                    Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);
            }


            if (drCurrentDicsProg["Hinh_Thuc_Km"].ToString() != "IN")
            {
                //groupDiscFreeItem.Visible = false;
                spContainer2.SplitterDistance = 220;
            }
            else
            {
                //groupDiscFreeItem.Visible = true;
                spContainer2.SplitterDistance = 120;
            }


            //bdsDiscBreak_PositionChanged(sender,e);
            //tabDiscCountDetail.HideAllTabPage();
            //tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            //if (strLoai_Ap_KM == "IT") // mặt hàng
            //{
            //    tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            //    tabDiscCountDetail.ShowPageInTabControl(tpDiscItem);                
            //    tabDiscCountDetail.ShowPageInTabControl(tpDiscCust);
            //    tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Dt);
            //}
            ////else if (strLoai_Ap_KM == "CS") // Khách hàng
            ////{
            ////    tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            ////    tabDiscCountDetail.ShowPageInTabControl(tpDiscCust);

            ////}

            //else if (strLoai_Ap_KM == "NH") // Khách hàng và mặt hàng
            //{
            //    tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            //    tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Vt);
            //    tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Dt);
            //    //tabDiscCountDetail.ShowPageInTabControl(tpDiscItem);
            //    tabDiscCountDetail.ShowPageInTabControl(tpDiscCust);


            //}
            //else
            //{
            //    tabDiscCountDetail.ShowPageInTabControl(tpCtDiscount);
            //    tabDiscCountDetail.ShowPageInTabControl(tpDiscItem);
            //    tabDiscCountDetail.ShowPageInTabControl(tpDiscCust);
            //    tabDiscCountDetail.ShowPageInTabControl(tpMa_Nh_Dt);
            //}



        }
        void btFillData_Click(object sender, EventArgs e)
        {
            FillData();
        }
        void bdsDiscBreak_PositionChanged(object sender, EventArgs e)
        {
            //if (bdsDiscountProg.Position < 0 || drCurrentBreak == null)
            //    return;

            if (bdsDiscBreak.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);

            this.strMa_CTKM = drCurrentBreak["Ma_CTKM"].ToString();
            this.strStt = drCurrentBreak["STT"].ToString();

            bdsDiscFreeItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + strStt + "' ";
            bdsDiscItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + strStt + "' ";
            bdsDiscCust.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + strStt + "' ";
            bdsDiscGroupCust.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + strStt + "' ";
            bdsDiscGroupItem.Filter = "Ma_CTKM = '" + strMa_CTKM + "' AND Stt = '" + strStt + "' ";
        }

        void Setdefault(ref DataTable dtExcel)
        {

            /*
            //if (!dtImport.Columns.Contains("Tu_So_Tien"))
            //{

            //    DataColumn dcTu_So_Tien = new DataColumn("Tu_So_Tien", typeof(double));
            //    dcTu_So_Tien.DefaultValue = 0;
            //    dtImport.Columns.Add(dcTu_So_Tien);
            //}

            //if (!dtImport.Columns.Contains("Den_So_Tien"))
            //{

            //    DataColumn dcDen_So_Tien = new DataColumn("Den_So_Tien", typeof(double));
            //    dcDen_So_Tien.DefaultValue = 0;
            //    dtImport.Columns.Add(dcDen_So_Tien);
            }*/

            foreach (DataRow drEx in dtExcel.Rows)
            {
                double dbFromTien = 0, dbToTien = 0, dbTien_Ck = 0, dbCk = 0;
                if (dtExcel.Columns.Contains("Tu_So_Tien") && !double.TryParse(drEx["Tu_So_Tien"].ToString(), out dbFromTien))
                {
                    drEx["Tu_So_Tien"] = 0;
                }

                if (dtExcel.Columns.Contains("Den_So_Tien") && !double.TryParse(drEx["Den_So_Tien"].ToString(), out dbToTien))
                {
                    drEx["Den_So_Tien"] = 0;
                }
                if (dtExcel.Columns.Contains("Tien_Ck") && !double.TryParse(drEx["Tien_Ck"].ToString(), out dbTien_Ck))
                {
                    drEx["Tien_Ck"] = 0;
                }
                if (dtExcel.Columns.Contains("Ck") && !double.TryParse(drEx["Ck"].ToString(), out dbCk))
                {
                    drEx["Ck"] = 0;
                }
                //Convert.
                drEx.AcceptChanges();

                //if(dr["Tu_So_Tien"].ToString())
            }
        }
        void CopyDataToTable(ref DataTable dtImport, DataTable dtExcel)
        {
            foreach (DataRow drExcel in dtExcel.Rows)
            {
                if (drExcel["Ma_CTKM"].ToString() != string.Empty)
                {
                    DataRow drImport = dtImport.NewRow();
                    Common.SetDefaultDataRow(ref drImport);
                    Common.CopyDataRow(drExcel, drImport);
                    drImport["Tra_Sau"] = drExcel["KMTra_Sau"].ToString() == "0" ? true : false;
                    
                    dtImport.Rows.Add(drImport);
                }

            }
        }
        void btImport_Click(object sender, EventArgs e)
        {
            strError = string.Empty;

            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofdlg.RestoreDirectory = true;
            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

            DataTable dtExcel = new DataTable();
            dtImport = SQLExec.ExecuteReturnDt("DECLARE @EditDisc AS TVP_DiscoutList SELECT * FROM @EditDisc");
            dtExcel = Common.ReadExcel(ofdlg.FileName);
            Setdefault(ref dtExcel);
            CopyDataToTable(ref dtImport, dtExcel);
            
            
            EpointProcessBox.Show(this);    
        }
        public virtual void Import_Excel(bool CheckAPI)
        {
            //string strMsg = (Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn ghi đè lên mẫu tin đã tồn tại không ?" : "Do you want to override exists data ?");
            bool Is_Avail = true;
            if (true)
            {
                SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
                command.CommandText = "OM_Import_DiscountProg";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
                //command.Parameters.AddWithValue("@IsImport", false);
                SqlParameter parameter = new SqlParameter
                {
                    SqlDbType = SqlDbType.Structured,
                    ParameterName = "@EditDisc",
                    TypeName = "TVP_DiscoutList",
                    Value = dtImport
                };
                command.Parameters.Add(parameter);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Is_Avail = false;
                    command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.ExecuteNonQuery();
                   EpointProcessBox.AddMessage("Có lỗi xảy ra :" + exception.Message);
                }

            }
            //return;
            if (!Is_Avail)
                return;


            Hashtable htParaBreaby = new Hashtable();
            htParaBreaby["MA_DVCS"] = Element.sysMa_DvCs;
            htParaBreaby["LOCALMACHINE"] = System.Environment.MachineName;
            htParaBreaby["ISOVERWRITE"] = chkIsOverride.Checked;
            DataSet ds = SQLExec.ExecuteReturnDs("OM_Import_DiscountProg_CheckValid", htParaBreaby, CommandType.StoredProcedure);

            DataTable dtCtkm = ds.Tables[0];
            DataTable dtMa_vt = ds.Tables[1];
            DataTable dtMa_Vt_Km = ds.Tables[2];


            if (dtCtkm.Rows.Count > 0)
            {
                strError += "\n" + "Chương trình KM/CK đã tồn tại trong hệ thống : ";
                foreach (DataRow dr in dtCtkm.Rows)
                {
                    strError += dr["Ma_CTKM"].ToString() + ",";
                }
            }
            if (dtMa_vt.Rows.Count > 0)
            {
                strError += "\n" + "Mã hàng bán không tồn tại : ";
                foreach (DataRow dr in dtMa_vt.Rows)
                {
                    strError += dr["Ma_Vt"].ToString() + ",";
                }
            }
            if (dtMa_Vt_Km.Rows.Count > 0)
            {
                strError += "\n" + "Mã hàng khuyến mãi không tồn tại : ";
                foreach (DataRow dr in dtMa_Vt_Km.Rows)
                {
                    strError += dr["Ma_Vt"].ToString() + ",";
                }
            }

            if (strError != string.Empty)
                EpointProcessBox.AddMessage(strError);
            else
            {
                Hashtable htPara = new Hashtable();
                htPara["MA_DVCS"] = Element.sysMa_DvCs;
                htPara["LOCALMACHINE"] = System.Environment.MachineName;
                htPara["ISOVERWRITE"] = chkIsOverride.Checked;
                if(SQLExec.Execute("OM_Import_DiscountProg_Valid", htPara, CommandType.StoredProcedure))
                     EpointProcessBox.AddMessage("Import thành công!");
                else
                    EpointProcessBox.AddMessage("Import thất bại!");
            }

            EpointProcessBox.AddMessage("Kết thúc");
        }
        public virtual void Import_Excel_Customer()
        {

            strError = string.Empty;
            if (bdsDiscountProg.Position < 0)
                return;

            if (bdsDiscBreak.Position < 0)
                return;

           
          
            //Copy hang hien tai
            if (bdsDiscCust.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsDiscCust.Current).Row, ref drCurrentDiscCust);
            else
                drCurrentDiscCust = dtDiscCust.NewRow();

            Common.CopyDataRow(((DataRowView)bdsDiscBreak.Current).Row, ref drCurrentBreak);

            drCurrentDiscCust["Ma_CTKM"] = drCurrentBreak["Ma_CTKM"];
            drCurrentDiscCust["STT"] = drCurrentBreak["STT"];


            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofdlg.RestoreDirectory = true;
            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

            DataTable dtCustomer = new DataTable();
            dtCustomer = Common.ReadExcel(ofdlg.FileName);
            Setdefault(ref dtCustomer);
        
            string strValue = string.Empty;
            string strValueList = string.Empty;
            string[] KeyAr = new string[3] { "Ma_CtKm", "Stt", "Ma_Dt" };
            string[] ValueAr;


            foreach (DataRow dr in dtCustomer.Rows)
            {
                string strMa_Dt = dr["Ma_Dt"].ToString();

                if (!DataTool.SQLCheckExist("LIDOITUONG", "Ma_Dt", strMa_Dt))
                    continue;

                ValueAr = new string[3] { drCurrentDiscCust["Ma_CTKM"].ToString(), drCurrentDiscCust["STT"].ToString(), strMa_Dt };

                drCurrentDiscCust["Ma_Dt"] = strMa_Dt;
                drCurrentDiscCust["Ten_Dt"] = DataTool.SQLGetNameByCode("LIDOITUONG", "Ma_Dt", "Ten_Dt", strMa_Dt);

                if (!DataTool.SQLCheckExist("OM_DISCCUST", KeyAr, ValueAr))
                {
                    if (DataTool.SQLUpdate(enuEdit.New, "OM_DISCCUST", ref drCurrentDiscCust))
                    {
                        if (bdsDiscCust.Position >= 0)
                            dtDiscCust.ImportRow(drCurrentDiscCust);
                        else
                            dtDiscCust.Rows.Add(drCurrentDiscCust);


                        dtDiscCust.AcceptChanges();
                    }
                    else
                        dtDiscCust.RejectChanges();
                }
            }
        }

        public override void EpointRelease()
        {

            Import_Excel(true);
        
        }

        #endregion
        private void DgvDiscCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F10:
                    Import_Excel_Customer();
                    break;
            }
            
        }
       /* protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    return;
                   
            }

            if (this.ActiveControl == dgvDiscCustomer)
            {
                switch (e.KeyCode)
                {
                    case Keys.F10:
                        this.Import_Excel_Customer();
                        return;
                }
            }

            base.OnKeyDown(e);
        }*/
    }
}
