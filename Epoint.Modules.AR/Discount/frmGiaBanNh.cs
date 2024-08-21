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
using Epoint.Systems.Elements;
using System.Data.OleDb;
using System.Reflection;
using System.Collections;
using System.Globalization;

namespace Epoint.Modules.AR
{
    public partial class frmGiaBanNh : Epoint.Systems.Customizes.frmView
    {   
        
        
        const string TABLENAME = "ARGIABAN";
        private DataTable dtGiaBan;

        BindingSource bdsGiaBan = new BindingSource();
        DataRow drCurrent;
        dgvControl dgvGiaBan = new dgvControl();     
       
        
        private DataTable dtDuDauIN;
        private BindingSource bdsDuDauIN = new BindingSource();
        private dgvControl dgvDuDauIN = new dgvControl();


        public frmGiaBanNh()
        {
            InitializeComponent();
        }

        public override void Load()
        {
            this.Build();
            this.FillData();

            this.Show();
        }

        private void Build()
        {
            dgvGiaBan.Dock = DockStyle.Fill;
            dgvGiaBan.strZone = "GiaBan";
            


            this.dgvDuDauIN.strZone = "GIABAN_NH";
            this.dgvDuDauIN.Dock = DockStyle.Fill;


            this.Controls.Add(dgvGiaBan);
            this.Controls.Add(dgvDuDauIN);
            
            this.dgvGiaBan.BuildGridView();
            this.dgvDuDauIN.BuildGridView();


            this.dgvGiaBan.Visible = false;


           
        }

        private void FillData()
        {
            string strSQLExecNh = "SELECT  * FROM LIDOITUONGNH WHERE Nh_Cuoi = 1";

            dtDuDauIN = SQLExec.ExecuteReturnDt(strSQLExecNh);


            bdsDuDauIN.DataSource = dtDuDauIN;
            dgvDuDauIN.DataSource = bdsDuDauIN;

            string strSQLExec = "SELECT GiaBan.*, DmVt.Ten_Vt, DmDt.Ten_Dt FROM ARGIABAN GiaBan " +
                " LEFT JOIN LIVATTU DmVt ON GiaBan.Ma_Vt = DmVt.Ma_Vt " +
                " LEFT JOIN LIDOITUONG DmDt ON GiaBan.Ma_Dt = DmDt.Ma_Dt";

            dtGiaBan = SQLExec.ExecuteReturnDt(strSQLExec);

            bdsGiaBan.DataSource = dtGiaBan;
            dgvGiaBan.DataSource = bdsGiaBan;
        }
        void EnterValid()
        {
            //Hashtable ht = new Hashtable();
           string strMa_Nh_Dt = ((DataRowView)bdsDuDauIN.Current)["MA_NH_DT"].ToString();
           string strMa_Dv_Cs= Element.sysMa_DvCs;

            string strSQLExec = "SELECT GiaBan.*, DmVt.Ten_Vt, DmDt.Ten_Dt FROM ARGIABAN GiaBan " +
                           " LEFT JOIN LIVATTU DmVt ON GiaBan.Ma_Vt = DmVt.Ma_Vt " +
                           " WHERE Giaban.MA_NH_DT = '"+strMa_Nh_Dt+"'  AND Ma_Data = @MA_DVCS";

            dtGiaBan = SQLExec.ExecuteReturnDt(strSQLExec);

            bdsGiaBan.DataSource = dtGiaBan;
            dgvGiaBan.DataSource = bdsGiaBan;

            dgvGiaBan.Visible = true;

            bdsSearch = bdsGiaBan;
            ExportControl = dgvGiaBan;
        }
        public override void Edit(enuEdit enuNew_Edit)
        {
            if (bdsGiaBan.Position < 0 && enuNew_Edit == enuEdit.Edit)
                return;

            //Copy hang hien tai            
            if (bdsGiaBan.Position >= 0)
                Common.CopyDataRow(((DataRowView)bdsGiaBan.Current).Row, ref drCurrent);
            else
                drCurrent = dtGiaBan.NewRow();

            frmGiaBan_Edit frmEdit = new frmGiaBan_Edit();
            frmEdit.Object_ID = this.Object_ID;
            frmEdit.Load(enuNew_Edit, drCurrent);

            //Accept
            if (frmEdit.isAccept)
            {
                if (enuNew_Edit == enuEdit.New)
                {
                    if (bdsGiaBan.Position >= 0)
                        dtGiaBan.ImportRow(drCurrent);
                    else
                        dtGiaBan.Rows.Add(drCurrent);

                    bdsGiaBan.Position = bdsGiaBan.Find("Ident00", drCurrent["Ident00"]);
                }
                else
                {
                    Common.CopyDataRow(drCurrent, ((DataRowView)bdsGiaBan.Current).Row);
                }

                dtGiaBan.AcceptChanges();
            }
            else
                dtGiaBan.RejectChanges();
        }

        public override void Delete()
        {
            if (bdsGiaBan.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsGiaBan.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;

            if (DataTool.SQLDelete("ARGIABAN", drCurrent))
            {
                bdsGiaBan.RemoveAt(bdsGiaBan.Position);
                dtGiaBan.AcceptChanges();
            }
        }
        public virtual void Import_Excel()
        {
            string strTableName = TABLENAME;
            OpenFileDialog ofdlg = new OpenFileDialog();

            ofdlg.DefaultExt = "xls";
            ofdlg.Filter = "*.xls|*.xls";

            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

            string probeConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;
						Data Source= " + ofdlg.FileName + ";" +
                        "Extended Properties=\"Excel 8.0;HDR=YES\"";

            using (OleDbConnection probeConn = new OleDbConnection(probeConnStr))
            {
                probeConn.Open();
                string probe = "SELECT * FROM [Sheet1$] " + //Sheet1$A1:A65536
                                "Where Ma_Vt IS NOT NULL AND Gia IS NOT NULL";

                using (OleDbDataAdapter oleDbDapter = new OleDbDataAdapter(probe, probeConn))
                {
                    DataTable tbExcel = new DataTable();
                    oleDbDapter.Fill(tbExcel);

                    DataTable dtStruct = DataTool.SQLGetDataTable(strTableName, "TOP 0 * ", " 0 = 1", null);
                    DataTable dtStruct2 = dtStruct.Clone();
                    DataRow drNewRow = dtStruct.NewRow();
                    dtStruct.Rows.Add(drNewRow);

                    foreach (DataColumn dc in dtStruct2.Columns)
                        if (dc.DataType.ToString() == "System.Byte[]")
                        {
                            dtStruct.Columns.Remove(dc.ColumnName);
                            dtStruct.AcceptChanges();
                        }



                    Common.SetDefaultDataRow(ref drNewRow);

                    string strMsg = (Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn ghi đè lên mẫu tin đã tồn tại không ?" : "Do you want to override exists data ?");
                    bool bIs_Overide = Common.MsgYes_No(strMsg);

                    foreach (DataRow drExcel in tbExcel.Rows)
                    {
                        Common.CopyDataRow(drExcel, drNewRow);
                        drNewRow.AcceptChanges();

                        //if (DataTool.SQLCheckExist(strTableName, new string [] { "","","",""},new object [] {} ))

                        DataTool.SQLUpdate(enuEdit.New, strTableName, ref drNewRow);
                    }
                }
            }

            Common.MsgOk(Languages.GetLanguage("End_Process"));
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F10 && Common.CheckPermission(this.Object_ID, enuPermission_Type.Allow_New) && Common.CheckPermission(this.Object_ID, enuPermission_Type.Allow_Edit))
                Import_Excel();
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        EnterValid();
                        return;

                    case Keys.Escape:
                        if (dgvGiaBan.Visible)
                        {
                            dgvDuDauIN.Visible = true;
                            dgvGiaBan.Visible = false;

                            bdsSearch = bdsDuDauIN;
                            ExportControl = dgvDuDauIN;
                        }
                        else

                            Common.CloseCurrentFormOnMain();
                        return;

                }

                if (this.ActiveControl == dgvGiaBan)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.F2:
                            this.Edit(enuEdit.New);
                            return;

                        case Keys.F3:
                            this.Edit(enuEdit.Edit);
                            return;

                        case Keys.F8:
                            this.Delete();
                            return;
                    }
                }
                base.OnKeyDown(e);
            }
        }
    }
}
