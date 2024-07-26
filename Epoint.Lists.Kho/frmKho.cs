using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Epoint.Systems.Data;
using Epoint.Systems.Commons;
using Epoint.Systems.Librarys;
using Epoint.Systems.Controls;
using Epoint.Systems;
using Epoint.Systems.Elements;


namespace Epoint.Lists
{
	public partial class frmKho : Epoint.Lists.frmView
	{

		#region Khai bao bien
		DataTable dtKho;
		DataRow drCurrent;
		BindingSource bdsKho = new BindingSource();		
        tlControl tlKho = new tlControl();

		#endregion

		#region Contructor

		public frmKho()
		{
			InitializeComponent();

			tlKho.MouseDoubleClick += new MouseEventHandler(tlKho_MouseDoubleClick);
            btExport.Click += new EventHandler(btExport_Click);
            btRefresh.Click += new EventHandler(btRefresh_Click);
		}
                
		public override void Load()
		{
            Init();
            Build();
			FillData();
			BindingLanguage();

			if (this.isLookup)
				this.ShowDialog();
			else
				this.Show();
		}

		public override void LoadLookup()
		{
			this.Load();
		}
        private void Init()
        {
            htHistory["DIEN_GIAI"] = "Danh mục kho";
            strTableName = "LIKHO";
            strCode = "MA_KHO";
            strName = "TEN_KHO";
        }
		#endregion

		#region Build, FillData
		private void Build()
		{                 
            tlKho.KeyFieldName = "MA_KHO";
            tlKho.ParentFieldName = "MA_KHO_CHA";
            tlKho.Dock = DockStyle.Fill;

            tlKho.strZone = "Kho";
            tlKho.BuildTreeList(this.isLookup);

            if (isLookup)
            {
                this.splitContainer.Panel2.Controls.Add(splControl1);
                this.splControl1.Panel1.Controls.Add(tlKho);
            }
            else
            {
                this.splControl1.Panel1.Controls.Add(tlKho);
            }
		}

		public void FillData()
		{            
            dtKho = DataTool.SQLGetDataTable("LIKHO", null, this.strLookupKeyFilter, null);
            bdsKho.DataSource = dtKho;

            //Uy quyen cho lop co so tim kiem           
            bdsSearch = bdsKho;
            ExportControl = tlKho;

            tlKho.DataSource = bdsKho;
            bdsKho.Position = 0;

            if (this.isLookup)
                this.MoveToLookupValue();

            tlKho.Expand = (bool)SQLExec.ExecuteReturnValue("SELECT Expand FROM SYSZONE WHERE ZONE = '" + tlKho.strZone + "'");
            tlKho.Select();
		}

		private void MoveToLookupValue()
		{
			if (this.strLookupColumn == string.Empty || this.strLookupValue == string.Empty)
				return;

			for (int i = 0; i <= dtKho.Rows.Count - 1; i++)
				if (((string)dtKho.Rows[i][strLookupColumn]).StartsWith(strLookupValue))
				{
					bdsKho.Position = i;
					break;
				}
		}
		#endregion

		#region Update

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsKho.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai            
			if (bdsKho.Position >= 0)
				Common.CopyDataRow(((DataRowView)bdsKho.Current).Row, ref drCurrent);
			else
				drCurrent = dtKho.NewRow();

			frmKho_Edit frmEdit = new frmKho_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
                //Cập nhật History
                DataRow drHistory = drCurrent;
                htHistory["CODE"] = drHistory[strCode];
                htHistory["NAME"] = drHistory[strName];

                if (enuNew_Edit == enuEdit.New)
                {
                    htHistory["UPDATE_TYPE"] = "N";
                    UpdateHistory();
                }
                else if (enuNew_Edit == enuEdit.Edit && ((string)drHistory[strCode] != (string)((DataRowView)bdsKho.Current)[strCode] || (string)drHistory[strName] != (string)((DataRowView)bdsKho.Current)[strName]))
                {
                    htHistory["UPDATE_TYPE"] = "E";
                    htHistory["CODE_OLD"] = ((DataRowView)bdsKho.Current)[strCode];
                    htHistory["NAME_OLD"] = ((DataRowView)bdsKho.Current)[strName];
                    UpdateHistory();
                }
                //Cập nhật dữ liệu danh mục
				if (enuNew_Edit == enuEdit.New)
				{
					if (bdsKho.Position >= 0)
						dtKho.ImportRow(drCurrent);
					else
						dtKho.Rows.Add(drCurrent);

					bdsKho.Position = bdsKho.Find("MA_KHO", drCurrent["MA_KHO"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsKho.Current).Row);

				dtKho.AcceptChanges();
			}
            //else
            //    dtKho.RejectChanges();
		}

		public override void Delete()
		{           
            if (bdsKho.Position < 0)
                return;

            DataRow drCurrent = ((DataRowView)bdsKho.Current).Row;

            if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
                return;


            if (DataTool.SQLCheckExist("LIKHO", "Ma_Kho_Cha", drCurrent["Ma_Kho"]))
            {
                string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ?
                    "Kho: {" + drCurrent["Ten_Kho"].ToString() + "}  đang có kho con" :
                    "Warehouse: {" + drCurrent["Ten_Kho"].ToString() + "}  have child warehouse";

                Common.MsgCancel(strMsg);
                return;
            }

            if (DataTool.SQLCheckExist("vw_Thekho", strCode, drCurrent[strCode]))
            {
                string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ?
                    "Kho: {" + drCurrent[strName].ToString() + "}  đã được sử dụng trong chứng từ" :
                    "Inventory: {" + drCurrent[strName].ToString() + "}  used in voucher";

                Common.MsgCancel(strMsg);
                return;
            }

            if (DataTool.SQLDelete("LIKHO", drCurrent))
            {
                ////Sync Delete----------
                //string Is_Sync = Convert.ToString(SQLExec.ExecuteReturnValue("SELECT Parameter_Value FROM SYSPARAMETER WHERE Parameter_ID = 'SYNC_BEGIN'"));
                //if (Is_Sync == "1")
                //{
                //    SqlConnection sqlCon = SQLExecSync1.GetNewSQLConnectionSync1();
                //    if (sqlCon.State != ConnectionState.Open)
                //    {
                //        SQLExec.Execute("UPDATE SYSPARAMETER SET Parameter_Value = 0 WHERE Parameter_ID = 'SYNC_BEGIN'");
                //        string strMsg = Element.sysLanguage == enuLanguageType.Vietnamese ? "Quá trình đồng bộ đang bị gián đoạn. Vui lòng chờ trong ít phút !" : "The synchronization process is interrupted. Please wait a few minutes !";
                //        Common.MsgCancel(strMsg);
                //    }
                //    else
                //    {
                //        DataToolSync1.SQLDelete("LIKHO", drCurrent);
                //    }
                //}
                ////-----------------------

                //Cập nhật History
                htHistory["CODE"] = drCurrent[strCode];
                htHistory["NAME"] = drCurrent[strName];
                htHistory["UPDATE_TYPE"] = "D";
                UpdateHistory();
                
                bdsKho.RemoveAt(bdsKho.Position);
                dtKho.AcceptChanges();
            }
		}

		public override void MergeID()
		{
			if (bdsKho.Count <= 0)
				return;

			drCurrent = ((DataRowView)bdsKho.Current).Row;
			string strOldValue = (string)drCurrent["Ma_Kho"];

			frmMergeID frm = new frmMergeID();

			frm.Load("LIKHO", "Ma_Kho", "Ten_Kho", strOldValue, "Kho");

			if (frm.isAccept)
			{
				string strNewValue = frm.strNewValue;
				string strMsg = Element.sysLanguage == enuLanguageType.English ? "Do you want to merge " + strOldValue + " to " + strNewValue + " ?" : "Bạn có muốn gộp mã " + strOldValue + " sang " + strNewValue + " không ?";
				if (!Common.MsgYes_No(strMsg))
					return;

				if (DataTool.SQLMergeID("Ma_Kho", "LIKHO", strOldValue, strNewValue))
				{
                    ////Sync data-------------
                    //string Is_Sync = Convert.ToString(SQLExec.ExecuteReturnValue("SELECT Parameter_Value FROM SYSPARAMETER WHERE Parameter_ID = 'SYNC_BEGIN'"));
                    //if (Is_Sync == "1")
                    //{
                    //    SqlConnection sqlCon = SQLExecSync1.GetNewSQLConnectionSync1();
                    //    if (sqlCon.State != ConnectionState.Open)
                    //    {
                    //        SQLExec.Execute("UPDATE SYSPARAMETER SET Parameter_Value = 0 WHERE Parameter_ID = 'SYNC_BEGIN'");
                    //        string strMsg1 = Element.sysLanguage == enuLanguageType.Vietnamese ? "Quá trình đồng bộ đang bị gián đoạn. Vui lòng chờ trong ít phút !" : "The synchronization process is interrupted. Please wait a few minutes !";
                    //        Common.MsgCancel(strMsg1);
                    //    }
                    //    else
                    //    {
                    //        DataToolSync1.SQLMergeID("Ma_Kho", "LIKHO", strOldValue, strNewValue);
                    //    }
                    //}
                    ////----------------------

                    //Cập nhật History
                    htHistory["CODE"] = drCurrent[strCode];
                    htHistory["NAME"] = drCurrent[strName];
                    htHistory["UPDATE_TYPE"] = "D";
                    UpdateHistory();

					bdsKho.RemoveCurrent();
					bdsKho.Position = bdsKho.Find("Ma_Kho", strNewValue);
				}
			}
		}

		#endregion 

		#region EnterProcess

		bool EnterValid()
		{
			if (this.strLookupKeyValid == string.Empty || this.strLookupKeyValid == null)
				return true;

			if (bdsKho == null || bdsKho.Position < 0)
				return false;

			drCurrent = ((DataRowView)bdsKho.Current).Row;
			DataTable dtTemp = dtKho.Clone();
			dtTemp.ImportRow(drCurrent);

			if ((dtTemp.Select(this.strLookupKeyValid)).Length == 1)
				return true;
			else
				return false;

		}

		public override void  EnterProcess()
		{
			if (bdsKho.Position < 0)
				return;

            if (isLookup && EnterValid())
            {
                drLookup = ((DataRowView)bdsKho.Current).Row;
                this.Close();             
            }         
		}

		#endregion 

		#region Su kien
        void tlKho_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.isLookup)
                this.EnterProcess();
            else
                this.Edit(enuEdit.Edit);
        }

        void btExport_Click(object sender, EventArgs e)
        {
            dgvExport.bSortMode = false;
            dgvExport.strZone = "Kho";
            dgvExport.BuildGridView();
            dgvExport.DataSource = bdsKho;

            string strTitle = ((Control)ExportControl).FindForm().Text;
            if (strTitle.Contains(","))
                strTitle = strTitle.Split(',')[0];

            ExportList(dgvExport, strTitle);
        }

        void btRefresh_Click(object sender, EventArgs e)
        {
            FillData();
        }

		#endregion 
	}
}