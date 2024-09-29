using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using Microsoft.Office;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

using Epoint.Systems.Data;
using Epoint.Systems.Elements;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;

namespace Epoint.Modules.HRM
{
    public class HRMCommon
    {

        public static DataTable GetCustomerTable(string strMa_Th)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("USERID", Element.sysUser_Id);
            htPara.Add("PARENTFIELDNAME", strMa_Th);
            htPara.Add("TRANG_THAI", "Đang Làm");
            return SQLExec.ExecuteReturnDt("HRM_GetDmCbNv_ParentName", htPara, CommandType.StoredProcedure);
        }
        public static DataTable GetCustomerTable(string strMa_Th, string strStatus)
        {
            Hashtable htPara = new Hashtable();
            htPara.Add("USERID", Element.sysUser_Id);
            htPara.Add("PARENTFIELDNAME", strMa_Th);
            htPara.Add("TRANG_THAI", strStatus);
            return SQLExec.ExecuteReturnDt("HRM_GetDmCbNv_ParentName", htPara, CommandType.StoredProcedure);
        }
        public static tlControl GetTreeViewCust(bool isLookup)
        {

            tlControl tlDmCbNv = new tlControl();
            tlDmCbNv.KeyFieldName = "MA_CBNV";
            tlDmCbNv.ParentFieldName = "MA_TH";
            tlDmCbNv.strZone = "EMPLOYEE";
            tlDmCbNv.Dock = DockStyle.Fill;
            tlDmCbNv.BuildTreeList(isLookup);

            return tlDmCbNv;
        }


        public static void WriteToWordQTTC04(DataTable dtResult, string strFileTemplate, string strFileName)
        {

            string Content = string.Empty;
            string strSaveFile = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Local Settings", @"C:\") + @"\Temp\HRM\" + strFileName;
            string strDirec = Directory.GetParent(strSaveFile).ToString();
            if (!Directory.Exists(strDirec))
                Directory.CreateDirectory(strDirec);

            if (System.IO.File.Exists(strSaveFile))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.SetAttributes(strSaveFile, 0);
                File.Delete(strSaveFile);
            }
            System.IO.File.Copy(strFileTemplate, strSaveFile, true);

            object missing = System.Reflection.Missing.Value;
            object newTemplate = false;
            object docType = 0;
            object confirm = false;
            object myTrue = false; // mở file word với readonly = false


            Word.Application wrdApp = new Word.Application();
            wrdApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            wrdApp.UserName = Environment.UserName;

            object fileName = strSaveFile;
            Word.Document docOpen = wrdApp.Documents.Open(ref fileName, ref confirm, ref myTrue, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            try
            {
                wrdApp.Selection.Find.ClearFormatting();
                wrdApp.Selection.Find.Replacement.ClearFormatting();

                //CultureInfo cti = new CultureInfo("en-GB");
                //cti.NumberFormat.NumberGroupSeparator = ",";
                //cti.NumberFormat.NumberGroupSizes = new int[] { 3, 3, 3, 3, 3 };
                //cti.NumberFormat.NumberDecimalSeparator = ".";

                //cti.NumberFormat.CurrencyGroupSeparator = ",";
                //cti.NumberFormat.CurrencyGroupSizes = new int[] { 3, 3, 3, 3, 3 };
                //cti.NumberFormat.CurrencyDecimalSeparator = ".";

                CultureInfo cti = new CultureInfo("vi-VN");

                object replaceAll = Word.WdReplace.wdReplaceAll;

                //Replace Detail
                /*
                foreach (DataColumn dc in dtResult.Columns)
                {
                    string strMa_SoID = dc.ColumnName.ToUpper();

                    string strMa_So = string.Empty;
                    if (strMa_SoID.Contains("NGAY") && dc.DataType == typeof(DateTime))
                    {
                        if (dtResult.Rows[0][strMa_SoID] != DBNull.Value)
                            strMa_So = Library.DateToStr(Convert.ToDateTime(dtResult.Rows[0][strMa_SoID]));
                        else
                            strMa_So = "......./../......";
                    }
                    else
                        strMa_So = (string)dtResult.Rows[0][strMa_SoID];
                    try
                    {
                        wrdApp.Selection.Find.Text = "[" + strMa_SoID + "]";
                        wrdApp.Selection.Find.Replacement.Text = strMa_So;
                        wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                    }
                    catch { }
                    //wrdApp.Selection.Find.Text = "[" + strMa_So + "_Du_Cuoi]";
                    //wrdApp.Selection.Find.Replacement.Text = strDu_Cuoi;
                    //wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                }*/
                //Replace Detail
                foreach (DataColumn dc in dtResult.Columns)
                {
                    string strMa_SoID = dc.ColumnName.ToUpper();

                    string strMa_So = string.Empty;
                    if (strMa_SoID.Contains("NGAY") && dc.DataType == typeof(DateTime))
                    {
                        if (dtResult.Rows[0][strMa_SoID] != DBNull.Value)
                            strMa_So = Library.DateToStr(Convert.ToDateTime(dtResult.Rows[0][strMa_SoID]));
                        else
                            strMa_So = "......./../......";

                        wrdApp.Selection.Find.Text = "[" + strMa_SoID + "]";
                        wrdApp.Selection.Find.Replacement.Text = strMa_So;
                        wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);

                        continue;
                    }
                    else
                    {
                        string T_Replace = string.Empty;
                        strMa_So = (string)dtResult.Rows[0][strMa_SoID];
                        strMa_SoID = "[" + strMa_SoID + "]";

                        int chunks = strMa_So.Length / 200;

                        if (strMa_So.Length % 200 > 0)
                            chunks = chunks + 1;

                        if (chunks == 1)
                        {
                            wrdApp.Selection.Find.Text = strMa_SoID;
                            wrdApp.Selection.Find.Replacement.Text = strMa_So;
                            wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                        }
                        else
                        {
                            for (int i = 1; i <= chunks; i++)
                            {

                                if (i < chunks)
                                {
                                    T_Replace = strMa_So.Substring((i - 1) * 200, 200);
                                    T_Replace += strMa_SoID;
                                }
                                else
                                {
                                    T_Replace = strMa_So.Substring((i - 1) * 200, strMa_So.Length - (i - 1) * 200);
                                }

                                wrdApp.Selection.Find.Text = strMa_SoID;
                                wrdApp.Selection.Find.Replacement.Text = T_Replace;
                                wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                            }
                        }


                    }

                    //wrdApp.Selection.Find.Text = "[" + strMa_SoID + "]";
                    //wrdApp.Selection.Find.Replacement.Text = strMa_So;
                    //wrdApp.Selection.Find.Execute(ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing);

                }

                //////////////////////////////////////////////////////
                object saveChanges = Word.WdSaveOptions.wdSaveChanges;
                object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                object routeDocument = true;

                docOpen.Save();
                wrdApp.Visible = true;
                wrdApp.ShowMe();
            }
            catch
            {

                wrdApp.Visible = true;
                wrdApp.ShowMe();
            }
        }

    }
}
