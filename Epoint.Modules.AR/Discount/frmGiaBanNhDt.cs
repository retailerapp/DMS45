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
using System.Data.SqlClient;

namespace Epoint.Modules.AR
{
    public partial class frmGiaBanNhDt : Epoint.Systems.Customizes.frmView
    {
        DataTable dtGiaBan;
        BindingSource bdsGiaBan = new BindingSource();
        DataRow drCurrent;
        dgvGridControl dgvGiaBan = new dgvGridControl();



        DataTable dtDoiTuongNh;
        BindingSource bdsDoiTuongNh = new BindingSource();

        public frmGiaBanNhDt()
        {
            InitializeComponent();


            this.txtMa_Vt.TextChanged += new EventHandler(txtMa_Vt_TextChanged);
            this.cboMa_Nh_Dt.TextChanged += new EventHandler(cboMa_Nh_Dt_TextChanged);
            this.btCopyPrice.Click += new EventHandler(btCopyPrice_Click);
        }

        

        public override void Load()
        {
            this.Build();
            this.FillData();

            this.Show();
        }

        private void Build()
        {
            cboMa_Nh_Dt.lstItem.Width = 400;
            cboMa_Nh_Dt.lstItem.BuildListView("MA_NH_DT:100,TEN_NH_DT:200");


            dgvGiaBan.Dock = DockStyle.Fill;
            dgvGiaBan.strZone = "GiaBanNhDt";
            dgvGiaBan.BuildGridView();

            this.pnlGiaban.Controls.Add(dgvGiaBan);
        }

        private void FillData()
        {
            dtDoiTuongNh = DataTool.SQLGetDataTable("LIDOITUONGNH", "", "Nh_Cuoi = 1", "Ma_Nh_Dt");
            bdsDoiTuongNh.DataSource = dtDoiTuongNh;
            cboMa_Nh_Dt.lstItem.DataSource = bdsDoiTuongNh;


 //GiaBan.Ident00,GiaBan.Ma_Vt,GiaBan.Ma_Dt,GiaBan.Ngay_Ap,GiaBan.Gia,GiaBan.Ma_Data,GiaBan.Dvt,GiaBan.Ma_Nh_Dt, GiaBan.User_Crtd,GiaBan.User_Edit
            string strSQLExec = @"SELECT  GiaBan.*, DmVt.Ten_Vt, DmDt.Ten_Nh_Dt,DmVt.Ma_Nh_Vt FROM ARGIABAN GiaBan " +
                @" LEFT JOIN LIVATTU DmVt ON GiaBan.Ma_Vt = DmVt.Ma_Vt " +
                @" INNER JOIN LIDOITUONGNH DmDt ON GiaBan.Ma_Nh_Dt = DmDt.Ma_Nh_Dt 
                    WHERE Giaban.Ma_Dt = '' AND GiaBan.Ma_Data IN ('"+Element.sysMa_DvCs+"','*')";

            dtGiaBan = SQLExec.ExecuteReturnDt(strSQLExec);

            bdsGiaBan.DataSource = dtGiaBan;
            dgvGiaBan.DataSource = bdsGiaBan;

            ExportControl = dgvGiaBan;
            bdsSearch = bdsGiaBan;
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
            frmEdit.bNh_Dt = true;
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
            string strTableName = "ARGIABAN";
            OpenFileDialog ofdlg = new OpenFileDialog();

            ofdlg.DefaultExt = "xls";
            ofdlg.Filter = "*.xls|*.xls";

            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

//            string probeConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;
//						Data Source= " + ofdlg.FileName + ";" +
//                        "Extended Properties=\"Excel 8.0;HDR=YES\"";
            String probeConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
               "Data Source=" + ofdlg.FileName + ";Extended Properties=Excel 8.0;";

            using (OleDbConnection probeConn = new OleDbConnection(probeConnStr))
            {
                probeConn.Open();
                string probe = "SELECT * FROM [Sheet1$] " + //Sheet1$A1:A65536
                                "Where Ma_Nh_Dt IS NOT NULL AND Ma_Vt IS NOT NULL AND Gia IS NOT NULL";

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
                        DataTool.SQLUpdate(enuEdit.New, strTableName, ref drNewRow);
                    }
                }
            }

            Common.MsgOk(Languages.GetLanguage("End_Process"));
        }
        public virtual void Import_Excel(bool CheckAPI)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofdlg.RestoreDirectory = true;


            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

            DataTable dtImport = Common.ReadExcel(ofdlg.FileName);
            string strMsg = (Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn ghi đè lên mẫu tin đã tồn tại không ?" : "Do you want to override exists data ?");
            bool bIs_Overide = Common.MsgYes_No(strMsg);
            SaveData(dtImport, null, bIs_Overide);

        }
        public virtual void Import_Excel(DataTable dt)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();
            ofdlg.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofdlg.RestoreDirectory = true;


            if (ofdlg.ShowDialog() != DialogResult.OK)
                return;

            DataTable dtImport = Common.ReadExcel(ofdlg.FileName);
            string strMsg = (Element.sysLanguage == enuLanguageType.Vietnamese ? "Bạn có muốn ghi đè lên mẫu tin đã tồn tại không ?" : "Do you want to override exists data ?");
            bool bIs_Overide = Common.MsgYes_No(strMsg);
            SaveData(dtImport, null, bIs_Overide);

            System.Data.SqlClient.SqlCommand command = SQLExec.GetNewSQLConnection().CreateCommand();
            command.CommandText = "AR_Import_GIABAN";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", Element.sysUser_Id);
            command.Parameters.AddWithValue("@Ma_DvCs", Element.sysMa_DvCs);
            SqlParameter parameter = new SqlParameter
            {
                SqlDbType = SqlDbType.Structured,
                ParameterName = "@ARGIABAN",
                TypeName = "TVP_ARGIABAN",
                Value = dtImport
            };
            command.Parameters.Add(parameter);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                command.CommandText = "WHILE @@TRANCOUNT > 0 ROLLBACK TRANSACTION";
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.ExecuteNonQuery();
                EpointProcessBox.AddMessage("Có lỗi xảy ra :" + exception.Message);
            }

        }

        private void SaveData(DataTable tbExcel, DataTable dtStruct, bool bIs_Overide)
        {
            string strTableName = "ARGIABAN";

            if (dtStruct == null)
                dtStruct = tbExcel.Clone();
            dtStruct = DataTool.SQLGetDataTable(strTableName, "", "0=1", "");
            foreach (DataColumn dc in dtStruct.Columns)
                if (dc.DataType.ToString() == "System.Byte[]")
                {
                    dtStruct.Columns.Remove(dc.ColumnName);
                    dtStruct.AcceptChanges();
                }

            DataRow drNewRow = dtStruct.NewRow();

            foreach (DataRow drExcel in tbExcel.Rows)
            {
                try
                {
                    Common.CopyDataRow(drExcel, ref drNewRow);
                    Common.SetDefaultDataRow(ref drNewRow);
                    //drNewRow.AcceptChanges();

                    if (bIs_Overide)
                    {
                        if (DataTool.SQLCheckExist(strTableName, "Ma_Vt,Ma_Nhom_Dt,Ma_Dt", drNewRow))
                        {
                            DataTool.SQLDelete(strTableName, drNewRow);
                            DataTool.SQLUpdate(enuEdit.New, strTableName, ref drNewRow);
                            continue;
                        }
                    }
                    else
                        DataTool.SQLUpdate(enuEdit.New, strTableName, ref drNewRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi import code : " + (string)drExcel["Ma_Vt"] + "\n" + ex.ToString());
                    break;
                }
            }

        }
        public void Filter()
        {
            string strKey = "(1 = 1) ";

            if (txtMa_Vt.Text != string.Empty)
                strKey = strKey + " AND (Ma_Vt = '" + txtMa_Vt.Text + "')";
                     

            if (cboMa_Nh_Dt.Text != string.Empty)
                strKey = strKey + " AND (Ma_Nh_Dt = '" + cboMa_Nh_Dt.Text + "')";

            this.bdsGiaBan.Filter = strKey;
        }
        
        void txtMa_Vt_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        void cboMa_Nh_Dt_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        void btCopyPrice_Click(object sender, EventArgs e)
        {
            string strMa_Nh_Vt = string.Empty, strMa_Nh_Vt_To = string.Empty;
            frmGiaBanNh_Copy frm = new frmGiaBanNh_Copy();
            frm.Load();
            if(frm.isAccept)
            {
                strMa_Nh_Vt = frm.txtMa_Nh_Dt.Text;
                strMa_Nh_Vt_To = frm.txtMa_Nh_Dt_To.Text;

                Hashtable htPara = new Hashtable();
                htPara["MA_NH_DT"] = strMa_Nh_Vt;
                htPara["MA_NH_DT_TO"] = strMa_Nh_Vt_To;
                htPara["MA_DVCS"] = Element.sysMa_DvCs;
               
                //SQLExec.ExecuteReturnValue("sp_Check_PXKDetail",htPara,CommandType.StoredProcedure)

                SQLExec.Execute("sp_CopyARGiaBan", htPara, CommandType.StoredProcedure);
                EpointMessage.MsgOk("OK");
                FillData();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F10 && Common.CheckPermission(this.Object_ID, enuPermission_Type.Allow_New) && Common.CheckPermission(this.Object_ID, enuPermission_Type.Allow_Edit))
                Import_Excel(true);
            else
            {
                base.OnKeyDown(e);
            }
        }
    }
}
