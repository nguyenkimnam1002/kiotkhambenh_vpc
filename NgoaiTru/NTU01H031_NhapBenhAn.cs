using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NTU01H031_NhapBenhAn : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NTU01H031_NhapBenhAn()
        {
            InitializeComponent();
        }
         
        string khamBenhID = "";
        string hoSoBenhAnID = "";
        string benhnhanid = "";
        string phongID = Const.local_phongId.ToString();
         
        public void Load_Data( string khamBenhID, string hoSoBenhAnID, string benhnhanid) 
        { 
            this.khamBenhID = khamBenhID;
            this.hoSoBenhAnID = hoSoBenhAnID; 
            this.benhnhanid = benhnhanid; 
        }

        private void NTU01H031_NhapBenhAn_Load(object sender, EventArgs e)
        {
            Init_Form();
        }

        public void Init_Form()
        {
            DataTable dt_SoVaoVien = RequestHTTP.get_ajaxExecuteQueryO("NTU01H031.EV002", hoSoBenhAnID);
            etext_SoVaoVien.Text = dt_SoVaoVien.Rows[0]["SOVAOVIEN"].ToString();
            //dt_BenhAn.Rows[0]["LOAIBENHANID"].ToString();
            DataTable dt_BenhAn = RequestHTTP.get_ajaxExecuteQuery("COM.LOAIBENHAN1", new string[] { "[0]" }, new string[] { "1" });
            if (dt_BenhAn == null || dt_BenhAn.Rows.Count <= 0)
            {
                dt_BenhAn = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            if (dt_BenhAn.Rows.Count == 4)// tạm xóa loại YH cổ truyền đi vì ít dùng
                dt_BenhAn.Rows.RemoveAt(dt_BenhAn.Rows.Count - 1);

            DataRow dr_BenhAn = dt_BenhAn.NewRow();
            dr_BenhAn["col1"] = "";
            dr_BenhAn["col2"] = "Chọn";
            dt_BenhAn.Rows.InsertAt(dr_BenhAn, 0);
            
            uccbox_BenhAn.setData(dt_BenhAn, 0, 1);
            uccbox_BenhAn.setColumnAll(false);
            uccbox_BenhAn.setColumn(1, true);

            for (int i = 0; i < dt_BenhAn.Rows.Count; i++)
                if (dt_SoVaoVien.Rows[0]["LOAIBENHANID"].ToString() == dt_BenhAn.Rows[i]["col1"].ToString())
                {
                    uccbox_BenhAn.SelectValue = dt_SoVaoVien.Rows[0]["LOAIBENHANID"].ToString();
                    break;
                }
                else
                    uccbox_BenhAn.SelectIndex = 0;
        }

        private void openForm(Form frm, string optionsPopup)
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (uccbox_BenhAn.SelectValue == "")
            {
                MessageBox.Show("Chưa chọn loại bệnh án");
                return;
            }
            if (etext_SoVaoVien.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập số vào viện");
                return;
            }

            string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H031.EV001", etext_SoVaoVien.Text.Trim() + "$" + uccbox_BenhAn.SelectValue + "$" + hoSoBenhAnID);
            if (result == "1")
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("BAN.DONGBO.LEFT1", hoSoBenhAnID + "$" + khamBenhID + "$" + uccbox_BenhAn.SelectValue + "$" + phongID);
                if (_return == "1")
                {
                    //if (ReturnData != null) ReturnData(uccbox_BenhAn.SelectValue, null);
                    ReturnData_NTU01H031_NhapBenhAn(uccbox_BenhAn.SelectValue);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật số vào viện cho bệnh nhân không thành công");
                    return;
                }
            }
        }
        private void ReturnData_NTU01H031_NhapBenhAn(string loaibenhanid)
        {  
            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NT.021.LOAI.BA", loaibenhanid);
            // [{"LOAIBENHANID": "22","MALOAIBENHAN": "TMH02","TENLOAIBENHAN": "Bệnh án ngoại trú tai mũi họng","VERSION": null
            // ,"SYNC_FLAG": null,"UPDATE_FLAG": null,"MEDICAL_TYPE": "1","URL": "BAN01TMH02_NTTaiMuiHong"}]
            if (dt.Rows.Count == 0) return;

            string _sreenName = dt.Rows[0]["URL"].ToString();
            string _tenloaibenhan = dt.Rows[0]["TENLOAIBENHAN"].ToString();
            string _maloaibenhan = dt.Rows[0]["MALOAIBENHAN"].ToString();

            if (_sreenName != "")
            {
                // với Ngoại trú _sreenName có 4 giá trị sau: BAN01NT01_NgoaiTru     BAN01RHM02_NTRangHamMat     BAN01TMH02_NTTaiMuiHong     BAN01YHCTNT02_YHCTNgoaiTru
                BAN01NT01_NgoaiTru frm = new BAN01NT01_NgoaiTru(_tenloaibenhan, _sreenName);
                frm.Set_Data(
                    khamBenhID,
                    hoSoBenhAnID,
                    benhnhanid,
                    loaibenhanid,
                    _maloaibenhan
                    );
                openForm(frm, "1");
                // "manager.jsp?func=../benhan/" + _sreenName, paramInput, "Cập nhật " + _tenloaibenhan, 1300, 610);
            }
            else
            {
                MessageBox.Show("Không tồn tại loại bệnh án này trong dữ liệu");
                return;
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //protected EventHandler ReturnData;
        //public void setReturnData(EventHandler eventReturnData)
        //{
        //    ReturnData = eventReturnData;
        //}
    }
}
