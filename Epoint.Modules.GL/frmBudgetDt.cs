﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Lists;
using Epoint.Systems.Commons;
using Epoint.Systems.Customizes;

namespace Epoint.Modules.GL
{
	public partial class frmBudgetDt : Epoint.Systems.Customizes.frmView
	{
		DataTable dtBudget;
		BindingSource bdsBudget = new BindingSource();

		DataRow drCurrent;

		string strTk = string.Empty;

		public frmBudgetDt()
		{
			InitializeComponent();

			btNew.Click += new EventHandler(btNew_Click);
			btEdit.Click += new EventHandler(btEdit_Click);
			btDelete.Click += new EventHandler(btDelete_Click);
			//btExit.Click += new EventHandler(btExit_Click);
		}

		public void Load(string strTk)
		{
			this.strTk = strTk;

			this.Build();
			this.FillData();
			this.BindingLanguage();

			this.Show();
		}

		void Build()
		{
			dgvBudget.strZone = "GLBUDGETDT";
			dgvBudget.BuildGridView();
		}

		void FillData()
		{
			Hashtable htPara = new Hashtable();
			htPara.Add("NAM", Element.sysWorkingYear);
			htPara.Add("TK", strTk);
			htPara.Add("KIEU_TH", 1);
			htPara.Add("MA_DVCS", Element.sysMa_DvCs);

			dtBudget = SQLExec.ExecuteReturnDt("sp_GetBudgetDt", htPara, CommandType.StoredProcedure);

			//DataColumn dcNew = new DataColumn("TSO_LUONG_KH", typeof(double));
			//dcNew.Expression = "So_Luong_Kh01+So_Luong_Kh02+So_Luong_Kh03+So_Luong_Kh04+So_Luong_Kh05+So_Luong_Kh06+So_Luong_Kh07+So_Luong_Kh08+So_Luong_Kh09+So_Luong_Kh10+So_Luong_Kh11+So_Luong_Kh12";
			//dtBudget.Columns.Add(dcNew);

			//DataColumn dcNew2 = new DataColumn("TTIEN_KH", typeof(double));
			//dcNew.Expression = "Tien_Kh01+Tien_Kh02+Tien_Kh03+Tien_Kh04+Tien_Kh05+Tien_Kh06+Tien_Kh07+Tien_Kh08+Tien_Kh09+Tien_Kh10+Tien_Kh11+Tien_Kh12";
			//dtBudget.Columns.Add(dcNew);

			bdsBudget.DataSource = dtBudget;
			dgvBudget.DataSource = bdsBudget;

			bdsBudget.Sort = "Bold, TK, MA_VT";

			bdsSearch = bdsBudget;
			ExportControl = dgvBudget;
		}

		public override void Edit(enuEdit enuNew_Edit)
		{
			if (bdsBudget.Position < 0 && enuNew_Edit == enuEdit.Edit)
				return;

			//Copy hang hien tai
			//if (bdsBudget.Position >= 0)
			if (enuNew_Edit == enuEdit.Edit)
				Common.CopyDataRow(((DataRowView)bdsBudget.Current).Row, ref drCurrent);
			else
				drCurrent = dtBudget.NewRow();

			if (enuNew_Edit == enuEdit.New)
			{
				drCurrent["Nam"] = Element.sysWorkingYear;
				drCurrent["Bold"] = false;
				//drCurrent["Tk"] = strTk;
			}

			frmBudgetDt_Edit frmEdit = new frmBudgetDt_Edit();
			frmEdit.Load(enuNew_Edit, drCurrent);

			//Accept
			if (frmEdit.isAccept)
			{
				if (enuNew_Edit == enuEdit.New)
				{
					//if (bdsBudget.Position >= 0)
					if (enuNew_Edit == enuEdit.Edit)
						dtBudget.ImportRow(drCurrent);
					else
						dtBudget.Rows.Add(drCurrent);

					bdsBudget.Position = bdsBudget.Find("Ident00", drCurrent["Ident00"]);
				}
				else
					Common.CopyDataRow(drCurrent, ((DataRowView)bdsBudget.Current).Row);

				dtBudget.AcceptChanges();

				this.Update_TTien();
			}
		}

		public override void Delete()
		{
			if (bdsBudget.Position < 0)
				return;

			DataRow drCurrent = ((DataRowView)bdsBudget.Current).Row;

			if (!Common.MsgYes_No(Languages.GetLanguage("SURE_DELETE")))
				return;

			if (DataTool.SQLDelete("GLKEHOACH", drCurrent))
			{
				bdsBudget.RemoveAt(bdsBudget.Position);
				dtBudget.AcceptChanges();
			}
		}

		void Update_TTien()
		{
			DataRow drTotal = dtBudget.Select("Bold = true")[0];

			for (int iThang = 1; iThang <= 12; iThang++)
			{
				drTotal["So_Luong_Kh" + iThang.ToString().Trim().PadLeft(2, '0')] = Common.SumDCValue(dtBudget, "So_Luong_Kh" + iThang.ToString().Trim().PadLeft(2, '0'), "Bold = false");
				drTotal["Tien_Kh" + iThang.ToString().Trim().PadLeft(2, '0')] = Common.SumDCValue(dtBudget, "Tien_Kh" + iThang.ToString().Trim().PadLeft(2, '0'), "Bold = false");
				drTotal["Tien2_Kh" + iThang.ToString().Trim().PadLeft(2, '0')] = Common.SumDCValue(dtBudget, "Tien2_Kh" + iThang.ToString().Trim().PadLeft(2, '0'), "Bold = false");
			}
		}

		void btNew_Click(object sender, EventArgs e)
		{
			this.Edit(enuEdit.New);
		}

		void btEdit_Click(object sender, EventArgs e)
		{
			this.Edit(enuEdit.Edit);
		}

		void btDelete_Click(object sender, EventArgs e)
		{
			this.Delete();
		}

		void btExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
