using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Controls.Class;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucTabBenhAn : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabBenhAn()
        {
            InitializeComponent();
        }
        string ID = "";
        public void reload()
        {
            getData_table();
        }
        public bool allow_tab_reload = false;
        public void loadData(string _KHAMBENHID)
        {
            if (allow_tab_reload == false)
                if (ID == _KHAMBENHID) return;

            allow_tab_reload = false;
            ID = _KHAMBENHID;

            getData_table();
        }
        private void getData_table()
        {
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                clear();

                DataTable dt = ServiceTabDanhSachBenhNhan.getBenhAn(ID);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    memoEdit1.Text = dr["LYDOVAOVIEN"].ToString();
                    memoEdit2.Text = dr["TIENSUBENH_BANTHAN"].ToString();
                    memoEdit3.Text = dr["QUATRINHBENHLY"].ToString();
                    memoEdit4.Text = dr["TIENSUBENH_GIADINH"].ToString();

                    memoEdit5.Text = dr["KHAMBENH_TOANTHAN"].ToString();
                    memoEdit6.Text = dr["KHAMBENH_BOPHAN"].ToString();

                    textEdit1.Text = dr["KHAMBENH_MACH"].ToString();
                    textEdit4.Text = dr["KHAMBENH_NHIETDO"].ToString();
                    textEdit2.Text = dr["KHAMBENH_HUYETAP_HIGH"].ToString();
                    textEdit3.Text = dr["KHAMBENH_HUYETAP_LOW"].ToString();

                    textEdit5.Text = dr["KHAMBENH_NHIPTHO"].ToString();
                    textEdit6.Text = dr["KHAMBENH_CANNANG"].ToString();
                    textEdit7.Text = dr["KHAMBENH_CHIEUCAO"].ToString();

                    memoEdit7.Text = dr["TOMTATKQCANLAMSANG"].ToString();
                    memoEdit8.Text = dr["CHANDOANBANDAU"].ToString();
                    memoEdit9.Text = dr["HUONGXULY"].ToString();

                    textEdit8.Text = dr["BENHCHINH"].ToString();// MABENHCHINH
                    textEdit9.Text = "";
                    if (dr["BENHKEMTHEO"].ToString() != "") textEdit9.Text += dr["BENHKEMTHEO"].ToString() + "; "; // MABENHKEMTHEO , BENHKEMTHEO1, BENHKEMTHEO2
                    if (dr["BENHKEMTHEO1"].ToString() != "") textEdit9.Text += dr["BENHKEMTHEO1"].ToString() + "; ";
                    if (dr["BENHKEMTHEO2"].ToString() != "") textEdit9.Text += dr["BENHKEMTHEO2"].ToString() + "; ";

                    memoEdit10.Text = dr["KHAC"].ToString();

                    //labelControl2.Text = "BMI: " + dr["BMI"].ToString(); --> trường BMI này DB ko lưu,
                    float bmi = Func.BMI(Func.Parse_float(dr["KHAMBENH_CANNANG"].ToString())
                        , Func.Parse_float(dr["KHAMBENH_CHIEUCAO"].ToString()));
                    labelControl2.Text = "MBI: " + bmi.ToString() + " " + Func.BMI_Mess(bmi); 
                } 
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void clear()
        {
            memoEdit1.Text = "";
            memoEdit2.Text = "";
            memoEdit3.Text = "";
            memoEdit4.Text = "";

            memoEdit5.Text = "";
            memoEdit6.Text = "";

            textEdit1.Text = "";
            textEdit4.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";

            textEdit5.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";

            memoEdit7.Text = "";
            memoEdit8.Text = "";
            memoEdit9.Text = "";

            textEdit8.Text = "";
            textEdit9.Text = "";
            memoEdit10.Text = "";

            labelControl2.Text = "BMI: ";
        }
    }
}
