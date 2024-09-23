using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.HRM
{
    public partial class frmLoadTransaction : frmView
    {
        #region Phuong thuc

        string strPathFile;
        DataTable dtImportExcel;

        public frmLoadTransaction()
        {
            InitializeComponent();

            this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
            this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);
            this.btFile.Click += new EventHandler(btFile_Click);
            this.btLoadData.Click += new EventHandler(btLoadData_Click);
            //btnGetData.Click += new EventHandler(btnGetData_Click);

            btnExcelFile.Click += new EventHandler(btnExcelFile_Click);
            btnLoadExceldata.Click += new EventHandler(btnLoadExceldata_Click);
        }
        public override void Load()
        {

            BindingLanguage();
            LoadDicName();
            //this.dteNgay_Ct1.Text = DateTime.Now.ToShortDateString();
            //this.dteNgay_Ct2.Text = DateTime.Now.ToShortDateString();


            //this.txtTime1.Text = string.Format(DateTime.Now.ToLongTimeString(), "hh:mm:ss");
            this.Show();
        }

        private void LoadDicName()
        {

        }

        public bool FormCheckValid()
        {
            bool bvalid = true;

            return bvalid;
        }

        public bool Save()
        {

            return true;
        }
        #endregion

        #region Su kien
        void btAccept_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                this.Close();
            }
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.RestoreDirectory = true;
            if (Common.GetBufferValue("PATHFILEDATASHEET") == string.Empty)
                ofd.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            else
                ofd.InitialDirectory = Common.GetBufferValue("PATHFILEDATASHEET");

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
                FileInfo MyFile = new FileInfo(ofd.FileName);
                string strPathFile = MyFile.DirectoryName;
                Common.SetBufferValueNotCheck("PATHFILEDATASHEET", strPathFile);
                //string []a = Directory.GetDirectories(ofd.FileName);
                //MessageBox.Show(strPathFile);

                //Element.sysNgay_Ct1 = Element.sysNgay_Begin;
                //Element.sysNgay_Ct2 = Element.sysNgay_End;

            }
        }


        private void btLoadData_Click(object sender, EventArgs e)
        {
            try
            {

                Common.ShowStatus(Languages.GetLanguage("In_Process"));
                string strSQL = string.Empty;
                string path = txtFile.Text;
                string[] file = path.Split('\\');
                string strFileName = file[file.Length - 1];
                StreamReader Reader = new StreamReader(path);
                while (Reader.Peek() > 0)
                {
                    string s = Reader.ReadLine();
                    Hashtable htPara = new Hashtable();
                    htPara.Add("TRANTEXT", s);
                    htPara.Add("FILESOURCE", strFileName);
                    htPara.Add("MA_DVCS", Element.sysMa_DvCs);
                    //                    strSQL = @"DELETE HRCHAMCONG WHERE TranText = @TRANTEXT
                    //                                INSERT HRCHAMCONG(TranText,FileSource,Ma_Dvcs) VALUES(@TRANTEXT,@FILESOURCE,@MA_DVCS)";
                    SQLExec.Execute("pr_InsertDatafile", htPara, CommandType.StoredProcedure);

                }
                Reader.Close();
                UpdateData(strFileName);

                Common.EndShowStatus();
                EpointMessage.MsgOk("Import dữ liệu thành công");
            }
            catch (FileNotFoundException ExNot)
            {
                MessageBox.Show("Không có file này" + ExNot.ToString(), "File Error");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Không mở được file này", "File Error");
            }
        }

        ////Tính số thời gian login logout
        //void btnGetData_Click(object sender, EventArgs e)
        //{
        //    //Common.ShowStatus("Đang lấy dư liệu chấm công.");

        //    //Hashtable htPara = new Hashtable();
        //    //htPara["NGAY_CT1"] = Library.StrToDate(dteNgay_Ct1.Text);
        //    //htPara["NGAY_CT2"] = Library.StrToDate(dteNgay_Ct2.Text);

        //    //SQLExec.Execute("sp_GetDataChamCong", htPara, CommandType.StoredProcedure);

        //    //Common.ShowStatus("Đã lấy dữ liệu xong.");
        //}

        #endregion

        private DataTable ReadFileExcel()
        {
            string strSelect = string.Empty;
            string strSheetName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xls files (*.xls;*.xlsx)|*.xls;*.xlsx";
            ofd.RestoreDirectory = true;

            if (Common.GetBufferValue("ImportExcelChamCong") != string.Empty)
                ofd.InitialDirectory = Common.GetBufferValue("ImportExcelChamCong");
            else
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtExcelFile.Text = ofd.FileName;
                Common.SetBufferValue("ImportExcelPath", System.IO.Path.GetDirectoryName(ofd.FileName));

                /*
                 //string strConnectString =
                 //    "Driver={Microsoft Excel Driver (*.xls, *.xlsx)};DBQ=" + ofd.FileName;

                 //string strConnectString =
                 //    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ofd.FileName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX = 2\"";
               
                 string strConnectString =
                     "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + ofd.FileName;

                 OdbcConnection odbcConn = new OdbcConnection(strConnectString);
                 odbcConn.Open();
                 */
                //DataTable dt =  odbcConn.GetSchema();
                string[] Sheetname = Common.GetExcelSheetNames(ofd.FileName);
                if (Sheetname != null)
                    if (Sheetname.Length > 0)
                        strSheetName = Sheetname[0];
                if (strSheetName == string.Empty)
                {
                    EpointMessage.MsgOk("File data không hợp lệ");
                    return null;
                }


                try
                {
                    //OdbcCommand odbcComm = new OdbcCommand();

                    //odbcComm.Connection = odbcConn;
                    //OdbcDataAdapter odbcDA;

                    DataTable dtTestColumn = new DataTable();
                    DataTable dtImport = new DataTable();

                    //Kiểm tra tồn tại cột dữ liệu
                    //odbcComm.CommandText = "SELECT * FROM [" + strSheetName + "] WHERE 0 = 1";
                    //odbcDA = new OdbcDataAdapter(odbcComm);
                    //odbcDA.Fill(dtTestColumn);

                    //if (dtTestColumn.Columns.Contains("A") && dtTestColumn.Columns.Contains("B") && dtTestColumn.Columns.Contains("C"))
                    //    strSelect = "SELECT A,B,C " +
                    //        " FROM [" + strSheetName + "] ";
                    //else// if (dtTestColumn.Columns.Contains(" Allowance Report") && dtTestColumn.Columns.Contains(":") && dtTestColumn.Columns.Contains(" "))
                    //    strSelect = "SELECT [Allowance Report] AS A,[:] AS B,[ ] AS C " +
                    //" FROM [" + strSheetName + "] ";
                    ////Kiểm tra xong
                    //odbcComm.CommandText = strSelect;

                    //odbcDA = new OdbcDataAdapter(odbcComm);
                    //odbcDA.Fill(dtImport);
                    //odbcConn.Close();

                    dtTestColumn = Common.GetExcelToTableByCmd(ofd.FileName, "SELECT * FROM [" + strSheetName + "] WHERE 0 = 1");
                    if (dtTestColumn.Columns.Contains("A") && dtTestColumn.Columns.Contains("B") && dtTestColumn.Columns.Contains("C"))
                        strSelect = "SELECT A,B,C " +
                            " FROM [" + strSheetName + "] ";
                    else// if (dtTestColumn.Columns.Contains(" Allowance Report") && dtTestColumn.Columns.Contains(":") && dtTestColumn.Columns.Contains(" "))
                        strSelect = "SELECT [Allowance Report] AS A,[:] AS B,[ ] AS C " +
                                                    " FROM [" + strSheetName + "] ";
                    dtImport = dtTestColumn = Common.GetExcelToTableByCmd(ofd.FileName, strSelect);

                    return dtImport;
                }
                catch
                {
                    EpointMessage.MsgOk("Không thể đọc dữ liệu");
                }
                //finally
                //{
                //    // Clean up.
                //    //if (odbcConn != null)
                //    //{
                //    //    odbcConn.Close();
                //    //    odbcConn.Dispose();
                //    //}
                //    if (dtImportExcel != null)
                //    {
                //        dtImportExcel.Dispose();
                //    }
                //}
            }
            return null;
        }
        private void UpdateData(string strFileName)
        {

            Hashtable htPara = new Hashtable();
            htPara.Add("FILESOURCE", strFileName);

            SQLExec.Execute("pr_InsertDataChamcong", htPara, CommandType.StoredProcedure);


            //DataTable dtTrans = SQLExec.ExecuteReturnDt("SELECT * FROM HRCHAMCONG WHERE FileSource = '" + strFileName + "'");
            ////dtTrans = DataTool.SQLGetDataTable("HRCHAMCONG", "", "FileSource = '" + strFileName + "'","");
            //if (dtTrans == null)
            //    return;

            //foreach (DataRow dr in dtTrans.Rows)
            //{
            //    string strTranText = dr["TranText"].ToString();
            //    string[] Trans = strTranText.Split(',');

            //    Hashtable htPara = new Hashtable();
            //    htPara.Add("IDENT", dr["Ident00"].ToString());
            //    htPara.Add("CODE", Trans[0]);
            //    htPara.Add("CODENV", Trans[1]);
            //    htPara.Add("LAN", Trans[2]);
            //    htPara.Add("TRANDATE", Trans[3]);
            //    htPara.Add("TRANTIME", Trans[4]);

            //    SQLExec.Execute("pr_UpdateDataChamcong", htPara, CommandType.StoredProcedure);


            //}

        }
        void btnLoadExceldata_Click(object sender, EventArgs e)
        {
            Common.ShowStatus("Đang import dữ liệu chấm công");
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (dtImportExcel == null)
            {
                EpointMessage.MsgOk("Không thể đọc dữ liệu");
                return;
            }
            System.Data.SqlClient.SqlCommand sqlcomm = SQLExec.GetSQLCommand();
            sqlcomm.CommandText = "pr_HRM_Importdata";
            sqlcomm.CommandType = CommandType.StoredProcedure;

            sqlcomm.Parameters.AddWithValue("@Ma_Dvcs", Element.sysMa_DvCs);
            sqlcomm.Parameters.AddWithValue("@FileName", "");
            sqlcomm.Parameters.AddWithValue("@SheetName", "");
            sqlcomm.Parameters.AddWithValue("@Create_Log", Common.GetCurrent_Log());

            //Add tham số bảng
            System.Data.SqlClient.SqlParameter para = new System.Data.SqlClient.SqlParameter();
            para = sqlcomm.Parameters.AddWithValue("@ImportData", dtImportExcel);
            para.SqlDbType = SqlDbType.Structured;

            para.TypeName = "dbo.ImportData";
            //sqlcomm.ExecuteNonQuery();
            System.Data.SqlClient.SqlDataAdapter sqlda = new System.Data.SqlClient.SqlDataAdapter(sqlcomm);
            sqlda.Fill(ds);
            Common.EndShowStatus();
            EpointMessage.MsgOk("Import dữ liệu thành công!");

        }

        void btnExcelFile_Click(object sender, EventArgs e)
        {
            Common.ShowStatus("Đang kiểm tra dữ liệu chấm công");
            dtImportExcel = ReadFileExcel();
            Common.EndShowStatus();
        }
        public static bool CopyByShare(string strSourcePath, string strDestPath, string fileName, string strUsername, string strPassword)
        {
            string strDriveLetter = "";

            String[] strDrivers = Directory.GetLogicalDrives();
            for (char cLetter = 'R'; cLetter <= 'Z'; cLetter++)
            {
                if (!Directory.Exists(cLetter.ToString() + @":\"))
                {
                    strDriveLetter = cLetter.ToString();
                    break;
                }
            }

            if (!Directory.Exists(strSourcePath) && strSourcePath.StartsWith(@"\\"))
            {
                string strCommand = String.Format("NET USE {0}: {1} {2} /USER:{3}", strDriveLetter, strSourcePath, strPassword, strUsername);

                if (ExecuteCmd(strCommand))
                {
                    strCommand = String.Format("NET USE {0}: /DELETE", strDriveLetter);
                    ExecuteCmd(strCommand);
                }
            }

            if (!Directory.Exists(strSourcePath))
            {
                MessageBox.Show("Không thể truy cập được thư mục " + strSourcePath);
                return false;
            }

            if (!Directory.Exists(Directory.GetParent(Path.Combine(strDestPath, fileName)).FullName))
            {
                Directory.CreateDirectory(Directory.GetParent(Path.Combine(strDestPath, fileName)).FullName);
            }

            try
            {
                //Copy tập tin
                if (File.Exists(Path.Combine(strSourcePath, fileName)))
                {
                    File.Copy(Path.Combine(strSourcePath, fileName), Path.Combine(strDestPath, fileName), true);
                    //CopySection(Path.Combine(strSourcePath, file.Name), Path.Combine(strDestPath, file.Name));
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool ExecuteCmd(string strCommand)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run, and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + strCommand);
                // The following commands are needed to redirect the standard output. 
                //This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                if (result.Length > 0)
                {
                    //MessageBox.Show("Error:" + result);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception objException)
            {
                MessageBox.Show("Error:" + objException.Message);
                return false;
            }
        }
        private void DefaultTable()
        {
            if (dtImportExcel == null)
                return;
            double a;

            foreach (DataRow dr in dtImportExcel.Rows)
            {
                foreach (DataColumn dc in dtImportExcel.Columns)
                {
                    //if (Common.Inlist(dc.ColumnName, "A,B,C"))
                    //    if (dr[dc].ToString() == "")
                    //        dr[dc] = "";


                }
            }
        }

    }
}
