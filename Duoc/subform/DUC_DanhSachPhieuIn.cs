using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Duoc.subform
{
    public partial class DUC_DanhSachPhieuIn : DevExpress.XtraEditors.XtraForm
    {
        //DataRowView row;
        string nhapxuatid = "", hinhthucid = "", loaiphieu = "";

        private void cmdInTatCa_Click(object sender, EventArgs e)
        {
            doPrint(100);
        }

        private void cmdInTHLinh_Click(object sender, EventArgs e)
        {
            doPrint(101);
        }

        private void cmdInLinhThuoc_Click(object sender, EventArgs e)
        {
            doPrint(0);
        }

        private void cmdInLinhHoaChat_Click(object sender, EventArgs e)
        {
            doPrint(1);
        }

        private void cmdInLinhVatTu_Click(object sender, EventArgs e)
        {
            doPrint(2);
        }

        private void cmdInLinhTDongY_Click(object sender, EventArgs e)
        {
            doPrint(3);
        }

        private void cmdInLinhMau_Click(object sender, EventArgs e)
        {
            doPrint(4);
        }

        private void cmdInSuatAn_Click(object sender, EventArgs e)
        {
            doPrint(5);
        }

        private void cmdInLinhGayNghien_Click(object sender, EventArgs e)
        {
            doPrint(6);
        }

        private void cmdInLinhHuongThan_Click(object sender, EventArgs e)
        {
            doPrint(7);
        }

        private void cmdInLinhDichTruyen_Click(object sender, EventArgs e)
        {
            doPrint(8);
        }

        private void cmdDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public DUC_DanhSachPhieuIn(string nhapxuatid, string hinhthucid, string loaiphieu)
        {
            InitializeComponent();

            this.nhapxuatid = nhapxuatid;
            this.hinhthucid = hinhthucid;
            this.loaiphieu = loaiphieu;
            // nhapxuatid:row["NHAPXUATID"].ToString(), hinhthucid: that.opt.ht,loaiphieu: that.opt.type
            //          hid_NHAPXUATID").val(objVar.nhapxuatid);
            //hid_TRANGTHAIID").val(objVar.hinhthucid);
            //hid_LOAIPHIEU").val(objVar.loaiphieu);
        }
        string[] _freport;
        private void DUC_DanhSachPhieuIn_Load(object sender, EventArgs e)
        {
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_InTHL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_thuoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_hoachat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_vattu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_thuocdongy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_linhmau.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_suatan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_gaynghien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_huongthan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            div_dichtruyen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (hinhthucid == "12" && loaiphieu == "45")
                div_InTHL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D044.15", nhapxuatid);
            if (ret != null && ret != "")
            {
                _freport = ret.Split(';');
                for (var i = 0; i < _freport.Length; i++)
                {
                    if (_freport[i].Trim().IndexOf("NTU004_PHIEULINHTHUOC_01DBV01") >= 0)
                        div_thuoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    else if (_freport[i].Trim().IndexOf("LINHHOACHAT") >= 0)
                    {
                        div_hoachat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (_freport[i].Trim().IndexOf("LINHVATTU") >= 0)
                    {
                        div_vattu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (_freport[i].Trim().IndexOf("LINHTHUOCDONGY") >= 0)
                    {
                        div_thuocdongy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (_freport[i].Trim().IndexOf("LINHMAUVACHEPHAMMAU") >= 0)
                    {
                        div_linhmau.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (_freport[i].Trim().IndexOf("LINHTHUOCTPGAYNGHIENHUONGTHAN") >= 0)
                    {
                        div_gaynghien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                    else if (_freport[i].Trim().IndexOf("PHIEULINHDICHTRUYEN") >= 0)
                    {
                        div_dichtruyen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                }
            }
        }

        private void doPrint(int _print_type)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", nhapxuatid);

            VNPT.HIS.Controls.SubForm.frmPrint frm = null;

            if (_print_type != 100)
            {
                if (_print_type == 101)
                {
                    frm = new Controls.SubForm.frmPrint(table, "NTU008_SOTONGHOPTHUOCHANGNGAY_14DBV01_TT23_A2", "pdf", 720, 1200);
                }
                else
                {
                    for (var i = 0; i < _freport.Length; i++)
                    {
                        if (_freport[i].Trim().IndexOf("LINHTHUOCDONGY") >= 0 && _print_type == 3)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("LINHHOACHAT") >= 0 && _print_type == 1)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("LINHVATTU") >= 0 && _print_type == 2)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("LINHTHUOC_01DBV01") >= 0 && _print_type == 0)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("LINHMAUVACHEPHAMMAU") >= 0 && _print_type == 4)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("LINHTHUOCTPGAYNGHIENHUONGTHAN") >= 0 && (_print_type == 6 || _print_type == 7))
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                        else if (_freport[i].Trim().IndexOf("PHIEULINHDICHTRUYEN") >= 0 && _print_type == 8)
                        {
                            frm = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < _freport.Length; i++)
                {
                    VNPT.HIS.Controls.SubForm.frmPrint frm1 = new Controls.SubForm.frmPrint(table, _freport[i].Trim(), "pdf", 720, 1200);
                    openForm(frm1);
                }
            }

            if (frm != null) openForm(frm);

            this.Close();
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