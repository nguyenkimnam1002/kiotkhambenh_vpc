using System;
using System.Windows.Forms;
using System.Data;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K068_QUETMABN_KBH : DevExpress.XtraEditors.XtraForm
    {
        public NGT02K068_QUETMABN_KBH()
        {
            InitializeComponent();
        }
        string lbPara = "#";
        private string getPara(string name)
        {//  &kbtraingay=1&x=2&
            string ret = "";
            try
            {
                if (lbPara == "#")
                {
                    Control[] control = this.Controls.Find("lbPara", false);
                    if (control.Length > 0)
                    {
                        if (control[0].GetType().ToString() == "DevExpress.XtraEditors.LabelControl")
                        {
                            DevExpress.XtraEditors.LabelControl lbControl = (DevExpress.XtraEditors.LabelControl)control[0];
                            lbPara = "&" + lbControl.Text + "&";
                        }
                    }
                }

                if (lbPara.IndexOf("&" + name) > -1)
                {
                    string temp = lbPara.Substring(lbPara.IndexOf("&" + name) + ("&" + name).Length + 1);
                    if (temp.IndexOf("&") > -1) ret = temp.Substring(0, temp.IndexOf("&"));
                }
            }
            catch (Exception ex) { }

            return ret;
        }

        string LOAI = "";
        private void NGT02K068_QUETMABN_KBH_Load(object sender, EventArgs e)
        {
            LOAI = getPara("loai");
            if (LOAI == "") LOAI = "1";
            
            string _phongtl = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KBH_HANGDOI_PHONGTL");

            string title = "";
            if (LOAI == "1") title += "STT Khám Bệnh - "; 
            else if (LOAI == "2") title += "STT Viện Phí - "; 

            if (_phongtl == "-1") title += Const.local_phong; 
            else title += "TẤT CẢ";

            lbTitle.Text = title; 
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            lbThongBao.Text = "THÔNG BÁO";
            txtMaBN.Text = "";

            txtTENBENHNHAN.Text = "";
            txtNGAYSINH.Text = "";
            txtGIOITINH.Text = "";
            txtTEN_DTBN.Text = "";
            txtMA_BHYT.Text = "";
            txtSOTHUTU.Text = "";
            txtNGAYTIEPNHAN.Text = "";
            txtMABENHNHAN1.Text = "";

            txtMaBN.Focus();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtSOTHUTU.Text.Trim() == "")
            {
                lbThongBao.Text = "Yêu cầu nhập thông tin bệnh nhân cần in phiếu";  
                return;
            } 

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("TENPHIEU", "String", "STT Khám Bệnh - ");
            table.Rows.Add("SOTHUTU", "String", txtSOTHUTU.Text.Trim());

            // "pdf" --> "pdf"
            // "doc" --> "rtf"  
             Func.Print_Luon(table, "NGT_CAP_SOTHUTU", "80mm");
            //Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NGT_CAP_SOTHUTU");
            //openForm(frm);
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _loadBenhNhan();
            }
        }
        private void textEdit1_Leave(object sender, EventArgs e)
        {
            //_loadBenhNhan();
        }

        private void _loadBenhNhan()
        {
            lbThongBao.Text = "THÔNG BÁO";
            if (txtMaBN.Text.Trim() == "")
            {
                lbThongBao.Text = "Chưa nhập Mã bệnh nhân!";
                return;
            }
            
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("NGT02K068.LAYDL"
                , txtMaBN.Text.Trim() + "$" + Const.local_khoaId + "$" + Const.local_phongId + "$" + LOAI
                );

            txtTENBENHNHAN.Text = "";
            txtNGAYSINH.Text = "";
            txtGIOITINH.Text = "";
            txtTEN_DTBN.Text = "";
            txtMA_BHYT.Text = "";
            txtSOTHUTU.Text = "";
            txtNGAYTIEPNHAN.Text = "";
            txtMABENHNHAN1.Text = "";


            if (data_ar.Rows.Count > 0)
            {
                string result = data_ar.Rows[0]["RESULT"].ToString();
                if (result == "0")
                {
                    lbThongBao.Text = "Thêm vào hàng đợi thành công!";
                    txtTENBENHNHAN.Text = data_ar.Rows[0]["TENBENHNHAN"].ToString();
                    txtNGAYSINH.Text = data_ar.Rows[0]["NGAYSINH"].ToString();
                    txtGIOITINH.Text = data_ar.Rows[0]["GIOITINH"].ToString();
                    txtTEN_DTBN.Text = data_ar.Rows[0]["TEN_DTBN"].ToString();
                    txtMA_BHYT.Text = data_ar.Rows[0]["MA_BHYT"].ToString();
                    txtSOTHUTU.Text = data_ar.Rows[0]["SOTHUTU"].ToString();
                    txtNGAYTIEPNHAN.Text = data_ar.Rows[0]["NGAYTIEPNHAN"].ToString();
                    txtMABENHNHAN1.Text = data_ar.Rows[0]["MABENHNHAN1"].ToString();
                    // hidKHAMBENHID").val(dt.KHAMBENHID);
                    //hidPHONGID1").val(dt.PHONGID);

                    txtTENBENHNHAN.Focus();

                    return;
                }
                else if (result == "2")
                {
                    lbThongBao.Text = "Mã bệnh nhân này chưa có thông tin tiếp nhận ngày hôm nay.";
                }
                else if (result == "3")
                {
                    lbThongBao.Text = "Bệnh nhân đã kết thúc khám/đang điều trị ngoại trú.";
                }
                else if (result == "4")
                {
                    lbThongBao.Text = "Bệnh nhân đã có trong danh sách đợi.";
                }
                else if (result == "5")
                {
                    lbThongBao.Text = "Bệnh nhân hiện không đăng ký phòng khám này.";
                }
                else if (result == "6")
                {
                    lbThongBao.Text = "Phân hệ truyền vào không phù hợp.";
                }
                else
                {
                    lbThongBao.Text = "Có lỗi trong quá trình nhận dữ liệu";
                }

                txtMaBN.SelectAll();
            }
            else
            {
                lbThongBao.Text = "Lỗi lấy thông tin bệnh nhân";
            }
        }





        private void openForm(Form frm, string optionsPopup = "1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

    }





}