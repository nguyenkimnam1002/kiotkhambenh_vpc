using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D003_DichVuDinhKem : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NTU02D003_DichVuDinhKem()
        {
            InitializeComponent();
        }
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string tiepnhanid = "";
        string maubenhphamid = "";
        string dichvuid = "";
        string trangthaidichvu = "";

        public void setData(string tiepnhanid, string maubenhphamid)
        {
            this.tiepnhanid = tiepnhanid;
            this.maubenhphamid = maubenhphamid;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NTU02D003_DichVuDinhKem_Load(object sender, EventArgs e)
        {
            ucGrid_DSBenhAn.gridView.OptionsView.ShowGroupPanel = false;
            ucGrid_DSBenhAn.gridView.OptionsView.ShowViewCaption = true;
            ucGrid_DSBenhAn.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DSBenhAn.gridView.OptionsView.RowAutoHeight = true;
            ucGrid_DSBenhAn.Set_HidePage(true);
            ucGrid_DSBenhAn.setEvent(LoaducGrid_DSBenhAn);
            ucGrid_DSBenhAn.setEvent_FocusedRowChanged(ucGrid_DSBenhAn_ChangeSelected);
            LoaducGrid_DSBenhAn(1, null);

            ucGrid_DSPhieu.gridView.OptionsView.ShowGroupPanel = false;
            ucGrid_DSPhieu.gridView.OptionsView.ShowViewCaption = true;
            ucGrid_DSPhieu.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DSPhieu.gridView.OptionsView.RowAutoHeight = true;
            ucGrid_DSPhieu.setEvent(LoaducGrid_DSPhieu);
            ucGrid_DSPhieu.setEvent_FocusedRowChanged(ucGrid_DSPhieu_ChangeSelected);
            ucGrid_DSPhieu.Set_HidePage(true);

            ucGrid_DSDichVu.gridView.OptionsView.ShowGroupPanel = false;
            ucGrid_DSDichVu.gridView.OptionsView.ShowViewCaption = true;
            ucGrid_DSDichVu.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DSDichVu.gridView.OptionsView.RowAutoHeight = true;
            ucGrid_DSDichVu.setEvent(LoaducGrid_DSDichVu);
            ucGrid_DSDichVu.setEvent_FocusedRowChanged(ucGrid_DSDichVu_ChangeSelected);
            ucGrid_DSDichVu.Set_HidePage(true);
        }

        private void LoaducGrid_DSBenhAn(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    string jsonFilter = string.Empty;
                    ResponsList responses = new ResponsList();

                    var lookup_sql = "NTU02D003.01";
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(lookup_sql, 1, 10000,
                    new String[]
                    {
                        "[0]"
                    }, new string[] {
                        tiepnhanid
                    }, jsonFilter);

                    ucGrid_DSBenhAn.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(responses.rows);
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                     {
                        "RN", "MA_BENHAN", "MA_BHYT", "TG_VAOVIEN", "KHAMBENHID"
                     });
                    ucGrid_DSBenhAn.setData(dt, responses.total, responses.page);
                    ucGrid_DSBenhAn.setColumnAll(false);
                    ucGrid_DSBenhAn.setColumnMemoEdit("MA_BENHAN", 0, "Mã BA", 0);
                    ucGrid_DSBenhAn.setColumnMemoEdit("MA_BHYT", 1, "Mã BHYT", 0);
                    ucGrid_DSBenhAn.setColumnMemoEdit("TG_VAOVIEN", 2, "Vào viện", 0);

                    dichvuid = "";
                    SetButton(dichvuid);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoaducGrid_DSPhieu(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    string jsonFilter = string.Empty;
                    ResponsList responses = new ResponsList();
                    DataRow dr = (DataRow)ucGrid_DSBenhAn.gridView.GetDataRow(ucGrid_DSBenhAn.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        var lookup_sql = "NTU02D003.02";
                        responses = RequestHTTP.get_ajaxExecuteQueryPaging(lookup_sql, 1, 10000,
                        new String[]
                        {
                            "[0]", "[1]"
                        }, new string[] {
                            Const.local_user.HOSPITAL_ID, dr["KHAMBENHID"].ToString()
                        }, jsonFilter);

                        ucGrid_DSPhieu.clearData();

                        DataTable dt = new DataTable();
                        dt = MyJsonConvert.toDataTable(responses.rows);
                        if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                         {
                            "RN", "BACSI_CHIDINH", "PHONG", "TG_CHIDINH", "ORD_KHAN", "MAUBENHPHAM_ID"
                         });
                        ucGrid_DSPhieu.setData(dt, responses.total, responses.page);
                        ucGrid_DSPhieu.setColumnAll(false);
                        ucGrid_DSPhieu.setColumnMemoEdit("SO_PHIEU", 0, "Số phiếu", 0);
                        ucGrid_DSPhieu.setColumnMemoEdit("BACSI_CHIDINH", 1, "Bác sĩ chỉ định", 0);
                        ucGrid_DSPhieu.setColumnMemoEdit("PHONG", 2, "Phòng", 0);
                        ucGrid_DSPhieu.setColumnMemoEdit("TG_CHIDINH", 3, "Thời gian chỉ định", 0);
                        ucGrid_DSPhieu.setColumnMemoEdit("ORD_KHAN", 4, "Khẩn", 0);

                        dichvuid = "";
                        SetButton(dichvuid);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoaducGrid_DSDichVu(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    string jsonFilter = string.Empty;
                    ResponsList responses = new ResponsList();
                    DataRow dr = (DataRow)ucGrid_DSPhieu.gridView.GetDataRow(ucGrid_DSPhieu.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        var lookup_sql = "NTU02D003.03";
                        responses = RequestHTTP.get_ajaxExecuteQueryPaging(lookup_sql, 1, 10000,
                        new String[]
                        {
                            "[1]", "[0]"
                        }, new string[] {
                            Const.local_user.HOSPITAL_ID, dr["MAUBENHPHAM_ID"].ToString()
                        }, jsonFilter);

                        ucGrid_DSDichVu.clearData();

                        DataTable dt = new DataTable();
                        dt = MyJsonConvert.toDataTable(responses.rows);
                        if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                         {
                            "RN", "TENDICHVU", "DICHVUID", "TRANGTHAIDICHVU"
                         });
                        ucGrid_DSDichVu.setData(dt, responses.total, responses.page);
                        ucGrid_DSDichVu.setColumnAll(false);
                        ucGrid_DSDichVu.setColumnMemoEdit("TENDICHVU", 0, "Tên dịch vụ", 0);

                        maubenhphamid = dr["MAUBENHPHAM_ID"].ToString();
                        dichvuid = "";
                        SetButton(dichvuid);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSBenhAn_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DSBenhAn.gridView.FocusedRowHandle >= 0)
                {
                    LoaducGrid_DSPhieu(1, null);
                }
                else
                {
                    ClearData(ucGrid_DSPhieu);
                    ClearData(ucGrid_DSDichVu);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSPhieu_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DSPhieu.gridView.FocusedRowHandle >= 0)
                {
                    LoaducGrid_DSDichVu(1, null);
                }
                else
                {
                    ClearData(ucGrid_DSDichVu);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSDichVu_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DSDichVu.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DSDichVu.gridView.GetDataRow(ucGrid_DSDichVu.gridView.FocusedRowHandle);
                    dichvuid = dr["DICHVUID"].ToString();
                    trangthaidichvu = dr["TRANGTHAIDICHVU"].ToString();
                    SetButton(dichvuid);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ClearData(UserControl.ucGridview grid)
        {
            grid.gridControl.DataSource = null;
            grid.gridView.Columns.Clear();
        }

        private void SetButton(string dichvuid)
        {
            btnLuu.Enabled = false;
            if (!string.IsNullOrEmpty(dichvuid))
            {
                btnLuu.Enabled = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (trangthaidichvu == "7")
            {
                MessageBox.Show("Phẫu thuật thủ thuật đã hoàn thành không thể cập nhật!");
                return;
            }

            var ret = RequestHTTP.call_ajaxCALL_SP_I("NTU02D003.04", maubenhphamid + "$" + dichvuid);
            if (ret == "1")
            {
                MessageBox.Show("Chỉ định dịch vụ đính kèm thành công!");

                //var evFunc = EventUtil.getEvent("assignSevice_DV_DinhKem");
                //if (typeof evFunc=== 'function')
                //{
                //    evFunc({ msg: "Cập nhật thành công phiếu đi kèm"});
                //}
                //else
                //{
                //    console.log('evFunc not a function');
                //}
            }
            else
            {
                MessageBox.Show("Có lỗi khi thực hiện!");
            }
        }
    }
}
