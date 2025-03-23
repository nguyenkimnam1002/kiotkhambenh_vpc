using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucTabNGT02K002_KBHB_PHONGKHAM : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabNGT02K002_KBHB_PHONGKHAM()
        {
            InitializeComponent();
        }
        public void setData(DataRow dr)
        {
            // {"PHONGID": "4951","CHANDOANRAVIEN": "","MACHANDOANRAVIEN": "","CHANDOANRAVIEN_KEMTHEO": "","MACHANDOANRAVIEN_KEMTHEO": ""
            // ,"CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KEMTHEO2": ""
            // ,"ORG_NAME": "Phòng 5: Tai Mũi Họng (K105)","KHAMBENH_TOANTHAN": "","KHAMBENH_BOPHAN": "","KHAMBENH_MACH": "","KHAMBENH_NHIETDO": ""
            // ,"KHAMBENH_HUYETAP_LOW": "","KHAMBENH_HUYETAP_HIGH": "","KHAMBENH_NHIPTHO": "","KHAMBENH_CANNANG": "","KHAMBENH_CHIEUCAO": "","DAXULY": ""
            // ,"KHAMBENH_VONGNGUC": "","KHAMBENH_VONGDAU": "","TOMTATKQCANLAMSANG": "","CHANDOANBANDAU": "","MACHANDOANBANDAU": ""},

            txtKHAMBENH_TOANTHAN.Text = dr["KHAMBENH_TOANTHAN"].ToString();
            txtKHAMBENH_BOPHAN.Text = dr["KHAMBENH_BOPHAN"].ToString();
            txtCHANDOANBANDAU.Text = dr["CHANDOANBANDAU"].ToString();
            txtTOMTATKQCANLAMSANG.Text = dr["TOMTATKQCANLAMSANG"].ToString();  
            txtDAXULY.Text = dr["DAXULY"].ToString();

            txtKHAMBENH_MACH.Text = dr["KHAMBENH_MACH"].ToString();
            txtKHAMBENH_NHIETDO.Text = dr["KHAMBENH_NHIETDO"].ToString();
            txtKHAMBENH_HUYETAP_LOW.Text = dr["KHAMBENH_HUYETAP_LOW"].ToString();
            txtKHAMBENH_HUYETAP_HIGH.Text = dr["KHAMBENH_HUYETAP_HIGH"].ToString();
            txtKHAMBENH_NHIPTHO.Text = dr["KHAMBENH_NHIPTHO"].ToString();
            txtKHAMBENH_CANNANG.Text = dr["KHAMBENH_CANNANG"].ToString();
            txtKHAMBENH_CHIEUCAO.Text = dr["KHAMBENH_CHIEUCAO"].ToString();
            ChiSo_BMI();

            txtBenhChinh.Text = dr["MACHANDOANRAVIEN"].ToString();
            txtBenhPhu.Text = dr["CHANDOANRAVIEN_KEMTHEO"].ToString();
        }
        private void ChiSo_BMI()
        {
            if (txtKHAMBENH_CHIEUCAO.Text.Trim() != "" && txtKHAMBENH_CANNANG.Text.Trim() != "")
            {
                try
                {
                    float cannang = float.Parse(txtKHAMBENH_CANNANG.Text.Trim());
                    float chieucao = float.Parse(txtKHAMBENH_CHIEUCAO.Text.Trim());

                    float bmi = Func.BMI(cannang, chieucao);
                    lbBMI.Text = "MBI (kg/m2): " + bmi.ToString() + Func.BMI_Mess(bmi);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
 
}
