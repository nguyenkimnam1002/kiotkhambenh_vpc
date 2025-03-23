using System;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using System.Text.RegularExpressions;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H027_DoiGioChiDinh : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string MAUBENHPHAMID;
        private static string NGAYMAUBENHPHAM;
        private static string NGAYMAUBENHPHAMHOANTHANH;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string mauBenhPhamId, string ngayMauBenhPham, string ngayMauBenhPhamHT)
        {
            MAUBENHPHAMID = mauBenhPhamId;
            NGAYMAUBENHPHAM = ngayMauBenhPham;
            NGAYMAUBENHPHAMHOANTHANH = ngayMauBenhPhamHT;
        }

        public NTU01H027_DoiGioChiDinh()
        {
            InitializeComponent();
        }

        private void NTU01H027_DoiGioChiDinh_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(NGAYMAUBENHPHAM))
                    dEditThoiGianChiDinh.DateTime = DateTime.ParseExact(NGAYMAUBENHPHAM, Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(NGAYMAUBENHPHAMHOANTHANH))
                    dEditThoiGianThucHien.DateTime = DateTime.ParseExact(NGAYMAUBENHPHAMHOANTHANH, Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dEditThoiGianChiDinh.Text))
                {
                    MessageBox.Show("Chưa chọn thời gian chỉ định", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                var regex = new Regex(@"/^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/]\d{4} (\d{2}):(\d{2}):(\d{2})$/");
                var matches = regex.Matches(dEditThoiGianChiDinh.Text);

                if (matches.Count > 0)
                {
                    MessageBox.Show("Thời gian chỉ định không hợp lệ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                matches = regex.Matches(dEditThoiGianThucHien.Text);
                if (!string.IsNullOrWhiteSpace(dEditThoiGianThucHien.Text) && matches.Count > 0)
                {
                    MessageBox.Show("Thời gian thực hiện không hợp lệ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string par = MAUBENHPHAMID + "$" + dEditThoiGianChiDinh.Text + "$" + dEditThoiGianThucHien.Text;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV018", par);
                if ("1".Equals(result))
                {
                    DialogResult dialogResult = MessageBox.Show("Chuyển thời gian thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (dialogResult == DialogResult.OK)
                    {
                        ReturnData(true, null);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Chuyển thời gian không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}