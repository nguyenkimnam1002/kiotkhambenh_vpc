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
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K029_YeuCauMoBenhAn : DevExpress.XtraEditors.XtraForm
    {
        #region Variable
        private static readonly log4net.ILog log =
                        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly int maxLengthLyDoMoLai = 200;

        private string benhNhanId = string.Empty;
        private string khamBenhId = string.Empty;
        private string kieu = string.Empty;
        private string tiepNhanId = string.Empty;
        private string phongKhamDangKyId = string.Empty;
        private string mode = "1";

        private string yeuCauMoBenhAnID = "";

        #endregion

        #region Delegate

        delegate void CallBackShowImg(Image img);
        protected EventHandler Event_Return;

        #endregion

        #region Private

        /// <summary>
        /// khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm()
        {
            try
            {
                if (!"2".Equals(kieu))// == 2 xử lý cho trường hợp nội trú
                {
                    var rs = RequestHTTP.get_ajaxExecuteQueryO(
                        "NGT.TTMOBA",
                        new string[] { "[0]", "[1]", "[2]" },
                        new string[] { tiepNhanId, "0", Const.local_phongId.ToString() }
                        );

                    if (rs.Rows.Count > 0)
                    {
                        txtLyDoMo.Text = rs.Rows[0]["NOIDUNG"].ToString();
                        yeuCauMoBenhAnID = rs.Rows[0]["YEUCAUMOBENHANID"].ToString();
                    }
                }

                btnLuu.Leave += BtnLuu_Leave;
                btnDong.Leave += BtnDong_Leave;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        
        /// <summary>
        /// Lưu yêu cầu mở bệnh án
        /// </summary>
        private void Luu()
        {
            try
            {
                string noiDungLyDo = txtLyDoMo.Text;
                if (string.IsNullOrWhiteSpace(noiDungLyDo))
                {
                    MessageBox.Show(Const.mess_yeucaumobenhan_phainhapnoidungyeucaumo);
                    txtLyDoMo.Text = string.Empty;
                    txtLyDoMo.Focus();
                    return;
                }

                if (noiDungLyDo.Length > maxLengthLyDoMoLai)
                {
                    MessageBox.Show("Lý do mở lại không được vượt quá " + maxLengthLyDoMoLai +" kí tự !");
                    txtLyDoMo.Focus();
                    return;
                }

                string dataRequestJson = string.Empty;
                if ("0".Equals(kieu))
                {
                    var dataRequest = new
                    {
                        benhnhanid = this.benhNhanId,
                        khambenhid = this.khamBenhId,
                        khoaid = Const.local_khoaId,
                        kieu = 0,
                        mode = this.mode,
                        noidung = noiDungLyDo,
                        phongid = Const.local_phongId,
                        phongkhamdangkyid = this.phongKhamDangKyId,
                        tiepnhanid = this.tiepNhanId
                    };

                    dataRequestJson = JsonConvert.SerializeObject(dataRequest).Replace("\"", "\\\"");
                }
                else
                {
                    var dataRequest = new
                    {
                        benhnhanid = this.benhNhanId,
                        khambenhid = this.khamBenhId,
                        khoaid = Const.local_khoaId,
                        kieu = 1,
                        mode = this.mode,
                        noidung = noiDungLyDo,
                        phongid = Const.local_phongId,
                        phongkhamdangkyid = this.phongKhamDangKyId,
                        tiepnhanid = this.tiepNhanId,
                        yeucaumobenhanid = yeuCauMoBenhAnID
                    };

                    dataRequestJson = JsonConvert.SerializeObject(dataRequest).Replace("\"", "\\\"");
                }

                var ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT02K001.MOBA", dataRequestJson);

                string msgError = string.Empty;
                switch (ret)
                {
                    case "-100":
                        {
                            msgError = Const.mess_yeucaumobenhan_chuayeucaumo;
                            break;
                        }
                    case "-200":
                        {
                            msgError = Const.mess_yeucaumobenhan_daguiyeucaumochungchuamobenhan;
                            break;
                        }
                    case "-400":
                        {
                            msgError = Const.mess_yeucaumobenhan_daduyetketoan;
                            break;
                        }
                    case "-500":
                        {
                            msgError = Const.mess_yeucaumobenhan_danhapkhoanoitru;
                            break;
                        }
                    case "-600":
                        {
                            msgError = Const.mess_yeucaumobenhan_yeucaudaduocmohayguiyeucaukhac;
                            break;
                        }
                    case "-700":
                        {
                            msgError = Const.mess_yeucaumobenhan_daketthucdieutri;
                            break;
                        }
                    default:
                        {
                            if ("1".Equals(ret) || "2".Equals(ret))
                            {
                                this.Close();
                            }

                            if (this.Event_Return != null)
                            {
                                this.Event_Return(ret, null);
                            }
                            break;
                        }
                }

                if (!string.IsNullOrEmpty(msgError))
                {
                    MessageBox.Show(msgError);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Thoát khỏi màn hình yêu cầu mở bệnh án
        /// </summary>
        private void Thoat ()
        {
            this.Close();
        }

        #endregion

        #region Public

        public NGT02K029_YeuCauMoBenhAn()
        {
            InitializeComponent();
        }
        
        public void setParam(string kieu, string khambenhid, string benhnhanId, string tiepnhanid, string hosobenhanid, string phongkhamdangkyid)
        {
            this.kieu = kieu;
            this.khamBenhId = khambenhid;
            this.benhNhanId = benhnhanId;
            this.tiepNhanId = tiepnhanid;
            this.phongKhamDangKyId = phongkhamdangkyid;
        }

        public void SetEvent_Return(EventHandler event_Return)
        {
            this.Event_Return = event_Return;
        }

        #endregion

        #region Events
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Luu();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Thoat();
        }

        private void NGT02K029_YeuCauMoBenhAn_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void BtnDong_Leave(object sender, EventArgs e)
        {
            txtLyDoMo.Focus();
        }

        private void BtnLuu_Leave(object sender, EventArgs e)
        {
            btnDong.Focus();
        }

        #endregion
    }
}