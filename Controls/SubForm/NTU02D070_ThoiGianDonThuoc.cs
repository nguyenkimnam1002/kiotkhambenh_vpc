using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D070_ThoiGianDonThuoc : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NTU02D070_ThoiGianDonThuoc()
        {
            InitializeComponent();
        }

        private void NTU02D070_ThoiGianDonThuoc_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = Func.getSysDatetime();
        }
        string maubenhphamid = "";
        string type = "";
        public void loadData(string maubenhphamid, string type)
        {
            this.maubenhphamid = maubenhphamid;
            this.type = type; 
        } 
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // {"func":"ajaxCALL_SP_I","params":["NT.026.COPY","310546$12/03/2018 13:57:03"],"uuid":"6b9f8ac4-1223-4f85-b72d-2311ed4f8639"}
            // {"result": "0","out_var": "[]","error_code": 0,"error_msg": ""}
           
            string sqlname = "";
            if (type == "7" || type == "8")
            {
                sqlname = "NT.COPY.TH.01"; 
            }
            else if (type == "1" || type == "2" || type == "5")
            {
                sqlname = "NT.026.COPY"; 
            }
            else if (type == "4")
            {
                sqlname = "NTU02D070.EV001"; 
            }

            string ret = RequestHTTP.call_ajaxCALL_SP_I(sqlname, maubenhphamid + "$" + dateEdit1.DateTime.ToString(Const.FORMAT_datetime1));
                        
            if (ret != "0")
            {
                MessageBox.Show("Tạo bản sao thành công");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Tạo bản sao phiếu không thành công");
            }
        }

    }
}