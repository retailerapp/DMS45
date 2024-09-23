using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Epoint.Systems.Controls;
using Epoint.Systems.Librarys;
using Epoint.Systems.Data;
using Epoint.Systems;
using Epoint.Systems.Elements;
using Epoint.Systems.Commons;
using Epoint.Lists;

namespace Epoint.Modules.HRM
{
	public partial class frmUngTuyen_Edit : Epoint.Systems.Customizes.frmEdit
	{
		string strMa_CbNv = string.Empty;
		DataRow drCurrent;

		#region Phuong thuc

		public frmUngTuyen_Edit()
		{
			InitializeComponent();

			this.btgAccept.btAccept.Click += new EventHandler(btAccept_Click);
			this.btgAccept.btCancel.Click += new EventHandler(btCancel_Click);

            txtMa_Bp.Validating += new CancelEventHandler(txtMa_Bp_Validating);
            txtMa_ViTri.Validating += new CancelEventHandler(txtMa_ViTri_Validating);  

			this.picHinh.DoubleClick += new EventHandler(picHinhLoad);
		}		

		public void Load(enuEdit enuNew_Edit, DataRow drEdit)
		{            
			this.drEdit = drEdit;
			this.enuNew_Edit = enuNew_Edit;
			this.Tag = (char)enuNew_Edit + "," + this.Tag;
			this.strMa_CbNv = (string)drEdit["Ma_CbNv"];
			this.ActiveControl = txtMa_CbNv;

			Common.ScaterMemvar(this, ref drEdit);
            txtMa_Data.Text = Element.sysMa_DvCs;

			BindingLanguage();
			LoadDicName();

			BindingPicture();

			this.ShowDialog();
		}

		private void LoadDicName()
		{
            //txtMa_Bp
            if (txtMa_Bp.Text.Trim() != string.Empty)
            {
                lbtTen_Bp.Text = DataTool.SQLGetNameByCode("LIBOPHAN", "Ma_Bp", "Ten_Bp", txtMa_Bp.Text.Trim());
                dicName.SetValue(lbtTen_Bp.Name, lbtTen_Bp.Text);
            }
            else
                lbtTen_Bp.Text = string.Empty;

            //txtMa_ViTri
            if (txtMa_ViTri.Text.Trim() != string.Empty)
            {
                lbtTen_ViTri.Text = DataTool.SQLGetNameByCode("HRVITRI", "Ma_ViTri", "Ten_ViTri", txtMa_ViTri.Text.Trim());
                dicName.SetValue(lbtTen_ViTri.Name, lbtTen_ViTri.Text);
            }
            else
                lbtTen_Bp.Text = string.Empty;
		}

		public bool FormCheckValid()
		{
			bool bvalid = true;
			if (txtMa_CbNv.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Ma_CbNv") + " " +
							  Languages.GetLanguage("Not_Null"));
				return false;
			}

			if (txtTen_CbNv.Text.Trim() == string.Empty)
			{
				Common.MsgOk(Languages.GetLanguage("Ten_CbNv") + " " +
							  Languages.GetLanguage("Not_Null"));
				return false;
			}

			return bvalid;
		}

		public bool Save()
		{
			Common.GatherMemvar(this, ref drEdit);

			//Kiem tra Valid tren Form
			if (!FormCheckValid())
				return false;

			//Luu xuong CSDL
			if (!DataTool.SQLUpdate(enuNew_Edit, "HRUNGTUYEN", ref drEdit))
				return false;

			this.SavePicture();

			//Doi ma
			if (this.enuNew_Edit == enuEdit.Edit)
				DataTool.SQLChangeID("MA_CBNV", drEdit);

			return true;
		}		

		#region Picture

		private void BindingPicture()
		{
			object objPic = SQLExec.ExecuteReturnValue("SELECT Hinh FROM HRUNGTUYEN WHERE Ma_CbNv = '" + strMa_CbNv + "'");
			Byte[] bytePic = (Byte[])objPic;

			if (objPic != DBNull.Value && bytePic.Length != 0)
			{
				byte[] barrImg = bytePic;
				string strFileName = Convert.ToString(DateTime.Now.ToFileTime());
				FileStream fs = new FileStream(strFileName, FileMode.CreateNew, FileAccess.Write);
				fs.Write(barrImg, 0, barrImg.Length);
				fs.Flush();
				fs.Close();
				picHinh.Image = Image.FromFile(strFileName);
				picHinh.SizeMode = PictureBoxSizeMode.Zoom;
			}
		}

		private void LoadPicture()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			fileDialog.Filter = fileDialog.Filter = "(*.BMP;*.JPG;*.GIF;*.JPG;*.JPEG;*.PNG)|*.BMP;*.JPG;*.GIF;*.JPG;*.JPEG;*.PNG|All files (*.*)|*.*"; ;

			if (fileDialog.ShowDialog() != DialogResult.OK)
				return;

			FileInfo fiImage = new FileInfo(fileDialog.FileName);
			FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);

			picHinh.Image = new Bitmap(Image.FromStream(fs), picHinh.Size);
			picHinh.SizeMode = PictureBoxSizeMode.Zoom;
		}

		private void SavePicture()
		{
			Hashtable ht = new Hashtable();

			if (picHinh.Image != null)
			{
				byte[] barrImg = (byte[])System.ComponentModel.TypeDescriptor.GetConverter(picHinh.Image).ConvertTo(picHinh.Image, typeof(byte[]));
				ht["HINH"] = barrImg;
			}
			else
			{
				ht["HINH"] = new byte[] { };
			}

			SQLExec.Execute("UPDATE HRUNGTUYEN SET Hinh = @Hinh WHERE Ma_CbNv = '" + strMa_CbNv + "'", ht, CommandType.Text);
		}

		#endregion

		#endregion

		#region Su kien
        
        void txtMa_Bp_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_Bp.Text.Trim();
            bool bRequire = false;

            //
            DataRow drLookup = Lookup.ShowLookup("Ma_Bp", strValue, bRequire, "", "Nh_Cuoi = 1");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_Bp.Text = string.Empty;
                lbtTen_Bp.Text = string.Empty;
            }
            else
            {
                txtMa_Bp.Text = drLookup["Ma_Bp"].ToString();
                lbtTen_Bp.Text = drLookup["Ten_Bp"].ToString();
            }

            dicName.SetValue(lbtTen_Bp.Name, lbtTen_Bp.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
        void txtMa_ViTri_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtMa_ViTri.Text.Trim();
            bool bRequire = false;

            frmViTri frmLookup = new frmViTri();
            DataRow drLookup = Lookup.ShowLookup(frmLookup, "HRVITRI", "Ma_ViTri", strValue, bRequire, "", "");

            if (bRequire && drLookup == null)
                e.Cancel = true;

            if (drLookup == null)
            {
                txtMa_ViTri.Text = string.Empty;
                lbtTen_ViTri.Text = string.Empty;
            }
            else
            {
                txtMa_ViTri.Text = drLookup["Ma_ViTri"].ToString();
                lbtTen_ViTri.Text = drLookup["Ten_ViTri"].ToString();
            }

            dicName.SetValue(lbtTen_ViTri.Name, lbtTen_ViTri.Text);

            if ((((txtTextLookup)sender).AutoFilter != null) && ((txtTextLookup)sender).AutoFilter.Visible)
            {
                ((txtTextLookup)sender).AutoFilter.Visible = false;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }
		void btAccept_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				isAccept = true;
				this.Close();
			}
		}
		void btCancel_Click(object sender, EventArgs e)
		{
			isAccept = false;
			this.Close();
		}

		void picHinhLoad(object sender, EventArgs e)
		{
			LoadPicture();			
		}

		#endregion

        
	}
}
