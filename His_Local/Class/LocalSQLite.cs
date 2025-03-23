using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace VNPT.HIS.Common
{
    public class LocalSQLite : System.IDisposable
    {
        // biến lưu giữ kết nốio
        protected SQLiteConnection cnn;
        protected string strConn;
        // biến trạng thái kết nối
        protected bool connected;
        // biến thông điệp lỗi gần nhất
        protected string lastError;

        // Thuộc tính cho biết trạng thái kết nối
        public bool Connected
        {
            get { return this.connected; }
        }

        // Thuộc tính cho biết thông điệp lỗi gần nhất
        public string LastError
        {
            get { return this.lastError; }
        }

        public LocalSQLite()
        {
            this.cnn = null;
            this.connected = false;
            this.lastError = string.Empty;

            this.strConn = LocalConst.LocalConn_SQLite;
            Connect();
        }

        public void Dispose()
        {
            this.Disconnect();
        }

        private bool Connect()
        { 
            // Nếu đã kết nối rồi thì sẽ gây lỗi
            if (this.connected)
            {
                this.lastError = "Connection already established.";
                return false;
            }

            // Khởi tạo kết nối
            this.cnn = new SQLiteConnection(strConn);

            try
            {
                // Mở CSDL
                this.cnn.Open();
            }
            catch (SQLiteException ex)
            {
                // thường có 2 trường hợp lỗi ở đây:
                // 1. Tập tin CSDL không truy cập được.
                // 2. Mật mã không đúng.
                this.lastError = ex.Message;
                return false;
            }

            // Đã kết nối
            this.connected = true;

            // Không có lỗi
            this.lastError = string.Empty;
            return true;
        }

        public bool Disconnect()
        {
            try{
                // Nếu chưa kết nối thì cũng xem là lỗi
                if (this.connected == false)
                {
                    this.lastError = "Connection not established.";
                    return false;
                }

                // Đóng CSDL
                this.cnn.Close();
                // Trả bộ nhớ
                this.cnn.Dispose();
                this.cnn = null;
                this.connected = false;
                this.lastError = string.Empty;
                return true;
            }
            catch(Exception ex){
                return false;
            }
        }

        public void SqliteTable_Select(string tableName, out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM " + tableName + ";", this.cnn);
                DataTable dt_ret = new DataTable();
                da.Fill(dt_ret);
                da.Dispose();
                if (dt_ret != null && dt_ret.Rows.Count > 0) dt = dt_ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        public void SqliteTable_SelectSql(string sql, out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, this.cnn);
                DataTable dt_ret = new DataTable();
                da.Fill(dt_ret);
                da.Dispose();
                if (dt_ret.Rows.Count > 0) dt = dt_ret;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        #region Other function
        public DataTable manaAllTable()
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    "SELECT name FROM sqlite_master WHERE type='table';",
                    this.cnn);
                da.Fill(dt);
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            return dt;
        }

        public bool execute(string sql)
        { 
            SQLiteCommand cmd = new SQLiteCommand(sql, this.cnn);
            cmd.ExecuteNonQuery();

            this.lastError = string.Empty;
            return true;
        }
        public void CacheObject_Select(string tableName, out DataTable dt)
        {
            dt = new DataTable();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM " + tableName + ";", this.cnn);
                DataTable dt_ret = new DataTable();
                da.Fill(dt_ret);

                da.Dispose();

                if (dt_ret != null && dt_ret.Rows.Count > 0) dt = dt_ret;
            }
            catch (Exception ex)
            {  }
        }
        public string CacheObject_SelectItem(string tableName, string colName, string colValue)
        {
            string ret = "";
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT id, name FROM " + tableName + " where " + colName + "='" + colValue + "';", this.cnn);

                da.Fill(dt);

                da.Dispose();

                if (dt.Rows.Count > 0) ret = dt.Rows[0]["name"].ToString();
            }
            catch (Exception ex) { }
            return ret;
        }
        public bool dropTable(string table_name)
        {
            execute("DROP TABLE IF EXISTS " + table_name + ";");
            execute("VACUUM;");
            return true;
        }
        public bool truncateTable(string table_name)
        {
            execute("DELETE FROM " + table_name + ";");
            execute("VACUUM;");
            return true;
        }



        #endregion

        public bool sqliteTransaction_NhapBenhNhan(ThongTinBenhNhan bn, MauBenhPhamObj mbp, DichVuKhamBenhObj dv, bool isEdit)
        {
            /*
            isEdit:
            - true: Update bệnh nhân
            - false: thêm mới bệnh nhân
            */
            SQLiteCommand cmd = new SQLiteCommand(this.cnn);
            var transaction = this.cnn.BeginTransaction();
            try
            {
                //var culture = new CultureInfo("en-US");
                var culture = CultureInfo.InvariantCulture;
                if (isEdit)
                {
                    if (string.IsNullOrEmpty(bn.BENHNHANID)) return false;
                    // Update thông tin bệnh nhân
                    cmd.CommandText = "UPDATE DMC_BENHNHAN SET ";
                    cmd.CommandText += "TENBENHNHAN = " + addCol(bn.TENBENHNHAN) + ",";
                    cmd.CommandText += "NGAYSINH = " + addCol(bn.NGAYSINH) + ",";
                    cmd.CommandText += "NAMSINH = " + addCol(bn.NAMSINH) + ",";
                    cmd.CommandText += "TUOI = " + addCol(bn.TUOI) + ",";
                    cmd.CommandText += "DVTUOI = " + addCol(bn.DVTUOI) + ",";
                    cmd.CommandText += "GIOITINHID = " + addCol(bn.GIOITINHID) + ",";
                    cmd.CommandText += "NGHENGHIEPID = " + addCol(bn.NGHENGHIEPID) + ",";
                    cmd.CommandText += "QUOCTICHID = " + addCol(bn.QUOCTICHID) + ",";
                    cmd.CommandText += "DANTOCID = " + addCol(bn.DANTOCID) + ",";
                    cmd.CommandText += "DIAPHUONGID = " + addCol(bn.DIAPHUONGID) + ",";
                    cmd.CommandText += "DIACHI = " + addCol(bn.DIACHI) + ",";
                    cmd.CommandText += "NGUOINHA = " + addCol(bn.NGUOINHA) + ",";
                    cmd.CommandText += "NGAYKHAM = " + addCol(bn.NGAYKHAM) + ",";
                    cmd.CommandText += "PHONGKHAMID = " + addCol(bn.PHONGKHAMID) + ",";
                    cmd.CommandText += "HINHTHUCVAOVIENID = " + addCol(bn.HINHTHUCVAOVIENID) + ",";
                    cmd.CommandText += "BACSIDIEUTRIID = " + addCol(bn.BACSIDIEUTRIID) + ",";
                    cmd.CommandText += "DOITUONGBENHNHANID = " + addCol(bn.DOITUONGBENHNHANID) + ",";
                    cmd.CommandText += "MATHEBHYT = " + addCol(bn.MATHEBHYT) + ",";
                    cmd.CommandText += "THOIGIAN_BD = " + addCol(bn.THOIGIAN_BD) + ",";
                    cmd.CommandText += "THOIGIAN_KT = " + addCol(bn.THOIGIAN_KT) + ",";
                    cmd.CommandText += "SINHTHETE = " + addCol(bn.SINHTHETE) + ",";
                    cmd.CommandText += "DU5NAM = " + addCol(bn.DU5NAM) + ",";
                    cmd.CommandText += "DU6THANG = " + addCol(bn.DU6THANG) + ",";
                    cmd.CommandText += "DIACHIBHYT = " + addCol(bn.DIACHIBHYT) + ",";
                    cmd.CommandText += "DKKCBBDID = " + addCol(bn.DKKCBBDID) + ",";
                    cmd.CommandText += "TUYENID = " + addCol(bn.TUYENID) + ",";
                    cmd.CommandText += "DOITUONGMIENGIAMID = " + addCol(bn.DOITUONGMIENGIAMID) + ",";
                    cmd.CommandText += "TYLEBH = " + addCol(bn.TYLEBH) + ",";
                    cmd.CommandText += "TYLEMIENGIAM = " + addCol(bn.TYLEMIENGIAM) + ",";
                    cmd.CommandText += "MACHANDOANRAVIEN = " + addCol(bn.MACHANDOANRAVIEN) + ",";
                    cmd.CommandText += "CHANDOANRAVIEN = " + addCol(bn.CHANDOANRAVIEN) + ",";
                    cmd.CommandText += "CHANDOANRAVIEN_KT = " + addCol(bn.CHANDOANRAVIEN_KT);
                    cmd.CommandText += " WHERE BENHNHANID = " + addCol(bn.BENHNHANID);
                    cmd.ExecuteNonQuery();

                    // Update Mẫu bệnh phẩm
                    cmd.CommandText = "UPDATE KBH_MAUBENHPHAM SET ";
                    cmd.CommandText += "NGAYMAUBENHPHAM = " + addCol(mbp.NGAYMAUBENHPHAM) + ",";
                    cmd.CommandText += "NGUOITAO = " + addCol(mbp.NGUOITAO);
                    cmd.CommandText += " WHERE MAUBENHPHAMID = " + addCol(mbp.MAUBENHPHAMID);
                    cmd.ExecuteNonQuery();

                    // Update dịch vụ khám bệnh
                    cmd.CommandText = "UPDATE KBH_DICHVU_KHAMBENH SET ";
                    cmd.CommandText += "NGAYDICHVU = " + addCol(dv.NGAYDICHVU) + ",";
                    cmd.CommandText += "DICHVUID = " + addCol(dv.DICHVUID) + ",";
                    cmd.CommandText += "TENDICHVU = " + addCol(dv.TENDICHVU) + ",";
                    cmd.CommandText += "DONGIA = " + addCol(float.Parse(dv.DONGIA).ToString("F", culture)) + ",";
                    cmd.CommandText += "GIABHYT = " + addCol(float.Parse(dv.GIABHYT).ToString("F", culture)) + ",";
                    cmd.CommandText += "GIAVP = " + addCol(float.Parse(dv.GIAVP).ToString("F", culture)) + ",";
                    cmd.CommandText += "GIADV = " + addCol(float.Parse(dv.GIADV).ToString("F", culture)) + ",";
                    cmd.CommandText += "BHYTTRA = " + addCol(float.Parse(dv.BHYTTRA).ToString("F", culture)) + ",";
                    cmd.CommandText += "NHANDANTRA = " + addCol(float.Parse(dv.NHANDANTRA).ToString("F", culture));
                    cmd.CommandText += " WHERE DICHVUKHAMBENHID = " + addCol(dv.DICHVUKHAMBENHID);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "INSERT INTO DMC_BENHNHAN (MABENHNHAN, TENBENHNHAN, NGAYSINH, NAMSINH, TUOI, DVTUOI, GIOITINHID, NGHENGHIEPID, QUOCTICHID, DANTOCID, DIAPHUONGID, DIACHI, NGUOINHA, NGAYKHAM, PHONGKHAMID, HINHTHUCVAOVIENID, BACSIDIEUTRIID, DOITUONGBENHNHANID, MATHEBHYT, THOIGIAN_BD, THOIGIAN_KT, SINHTHETE, DU5NAM, DU6THANG, DIACHIBHYT, DKKCBBDID, TUYENID, DOITUONGMIENGIAMID, TYLEBH, TYLEMIENGIAM, MACHANDOANRAVIEN, CHANDOANRAVIEN, CHANDOANRAVIEN_KT) VALUES (";
                    cmd.CommandText += addCol(bn.MABENHNHAN) + ",";
                    cmd.CommandText += addCol(bn.TENBENHNHAN) + ",";
                    cmd.CommandText += addCol(bn.NGAYSINH) + ",";
                    cmd.CommandText += addCol(bn.NAMSINH) + ",";
                    cmd.CommandText += addCol(bn.TUOI) + ",";
                    cmd.CommandText += addCol(bn.DVTUOI) + ",";
                    cmd.CommandText += addCol(bn.GIOITINHID) + ",";
                    cmd.CommandText += addCol(bn.NGHENGHIEPID) + ",";
                    cmd.CommandText += addCol(bn.QUOCTICHID) + ",";
                    cmd.CommandText += addCol(bn.DANTOCID) + ",";
                    cmd.CommandText += addCol(bn.DIAPHUONGID) + ",";
                    cmd.CommandText += addCol(bn.DIACHI) + ",";
                    cmd.CommandText += addCol(bn.NGUOINHA) + ",";
                    cmd.CommandText += addCol(bn.NGAYKHAM) + ",";
                    cmd.CommandText += addCol(bn.PHONGKHAMID) + ",";
                    cmd.CommandText += addCol(bn.HINHTHUCVAOVIENID) + ",";
                    cmd.CommandText += addCol(bn.BACSIDIEUTRIID) + ",";
                    cmd.CommandText += addCol(bn.DOITUONGBENHNHANID) + ",";
                    cmd.CommandText += addCol(bn.MATHEBHYT) + ",";
                    cmd.CommandText += addCol(bn.THOIGIAN_BD) + ",";
                    cmd.CommandText += addCol(bn.THOIGIAN_KT) + ",";
                    cmd.CommandText += addCol(bn.SINHTHETE) + ",";
                    cmd.CommandText += addCol(bn.DU5NAM) + ",";
                    cmd.CommandText += addCol(bn.DU6THANG) + ",";
                    cmd.CommandText += addCol(bn.DIACHIBHYT) + ",";
                    cmd.CommandText += addCol(bn.DKKCBBDID) + ",";
                    cmd.CommandText += addCol(bn.TUYENID) + ",";
                    cmd.CommandText += addCol(bn.DOITUONGMIENGIAMID) + ",";
                    cmd.CommandText += addCol(bn.TYLEBH) + ",";
                    cmd.CommandText += addCol(bn.TYLEMIENGIAM) + ",";
                    cmd.CommandText += addCol(bn.MACHANDOANRAVIEN) + ",";
                    cmd.CommandText += addCol(bn.CHANDOANRAVIEN) + ",";
                    cmd.CommandText += addCol(bn.CHANDOANRAVIEN_KT);
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                    // Lấy BENHNHANID vừa insert
                    cmd.CommandText = "SELECT last_insert_rowid() FROM DMC_BENHNHAN";
                    string benhNhanId = cmd.ExecuteScalar().ToString();

                    mbp.BENHNHANID = benhNhanId;
                    cmd.CommandText = "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO) VALUES (";
                    cmd.CommandText += addCol(mbp.SOPHIEU) + ",";
                    cmd.CommandText += addCol(mbp.LOAINHOMMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.BENHNHANID) + ",";
                    cmd.CommandText += addCol(mbp.NGAYMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.NGUOITAO);
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                    // Lấy MAUBENHPHAMID vừa insert
                    cmd.CommandText = "SELECT last_insert_rowid() FROM KBH_MAUBENHPHAM";
                    string mbpId = cmd.ExecuteScalar().ToString();

                    cmd.CommandText = "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG) VALUES (";
                    cmd.CommandText += addCol(mbpId) + ",";
                    cmd.CommandText += addCol(benhNhanId) + ",";
                    cmd.CommandText += addCol(dv.NGAYDICHVU) + ",";
                    cmd.CommandText += addCol(dv.DICHVUID) + ",";
                    cmd.CommandText += addCol(dv.TENDICHVU) + ",";
                    cmd.CommandText += addCol(dv.SOLUONG) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.DONGIA).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.GIABHYT).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.GIAVP).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.GIADV).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(dv.TYLEDV) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.BHYTTRA).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(float.Parse(dv.NHANDANTRA).ToString("F", culture)) + ",";
                    cmd.CommandText += addCol(dv.LOAIDOITUONG);
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
                this.lastError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                return false;
            }
        }

        /*
         * Hàm xóa bệnh nhân tiếp nhận
         */
        public bool sqliteTransaction_XoaBenhNhan(string benhNhanId)
        {
            SQLiteCommand cmd = new SQLiteCommand(this.cnn);
            var transaction = this.cnn.BeginTransaction();
            try
            {
                cmd.CommandText = "DELETE FROM KBH_MAUBENHPHAM WHERE BENHNHANID = " + benhNhanId;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM KBH_DICHVU_KHAMBENH WHERE BENHNHANID = " + benhNhanId;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM DMC_BENHNHAN WHERE BENHNHANID = " + benhNhanId;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                this.lastError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                return false;
            }
        }

        /*
         * Hàm Cập nhật mẫu bệnh phẩm, dịch vụ khám bệnh khi chỉ định dịch vụ
            isEdit:
            - true: Update mẫu bệnh phẩm dịch vụ khám -> xóa mbp, dvkb cũ và thêm mới vào
            - false: thêm mới mbp và dvkb
            */
        public bool sqliteTransaction_ChiDinhDichVu(Dictionary<string, DataTable> map, MauBenhPhamObj mbp, bool isEdit)
        {
            SQLiteCommand cmd = new SQLiteCommand(this.cnn);
            var transaction = this.cnn.BeginTransaction();
            try
            {
                //var culture = new CultureInfo("en-US");
                var culture = CultureInfo.InvariantCulture;
                string sqlQuery = "";
                DataTable dtSql = new DataTable();
                string soPhieu = "";

                // Nếu sửa mẫu bệnh phẩm -> xóa dữ liệu cũ và tạo mới dữ liệu
                if (isEdit)
                {
                    cmd.CommandText = "DELETE FROM KBH_MAUBENHPHAM WHERE MAUBENHPHAMID = " + mbp.MAUBENHPHAMID;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM KBH_DICHVU_KHAMBENH WHERE MAUBENHPHAMID = " + mbp.MAUBENHPHAMID;
                    cmd.ExecuteNonQuery();
                }

                // Thêm mới mẫu bệnh phẩm và dịch vụ khám tương ứng cho từng mbp
                foreach (string key in map.Keys)
                {
                    DataTable dt = map[key];
                    string[] keyValue = key.Split(',');
                    mbp.PHONGTHUCHIENID = keyValue[0];
                    mbp.LOAINHOMMAUBENHPHAM = keyValue[1];
                    // Tự sinh số phiếu
                    sqlQuery = "SELECT MAX(MAUBENHPHAMID) MAXMBP FROM KBH_MAUBENHPHAM";
                    SqliteTable_SelectSql(sqlQuery, out dtSql);
                    if (dtSql.Rows.Count > 0 && !string.IsNullOrEmpty(dtSql.Rows[0]["MAXMBP"].ToString()))
                        soPhieu = (int.Parse(dtSql.Rows[0]["MAXMBP"].ToString()) + 1).ToString();
                    else
                        soPhieu = "1";
                    mbp.SOPHIEU = "P" + soPhieu.PadLeft(9, '0');

                    cmd.CommandText = "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO, PHONGTHUCHIENID) VALUES (";
                    cmd.CommandText += addCol(mbp.SOPHIEU) + ",";
                    cmd.CommandText += addCol(mbp.LOAINHOMMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.BENHNHANID) + ",";
                    cmd.CommandText += addCol(mbp.NGAYMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.NGUOITAO) + ",";
                    cmd.CommandText += addCol(mbp.PHONGTHUCHIENID);
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                    // Lấy MAUBENHPHAMID vừa insert
                    cmd.CommandText = "SELECT last_insert_rowid() FROM KBH_MAUBENHPHAM";
                    string mbpId = cmd.ExecuteScalar().ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        cmd.CommandText = "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG) VALUES (";
                        cmd.CommandText += addCol(mbpId) + ",";
                        cmd.CommandText += addCol(mbp.BENHNHANID) + ",";
                        cmd.CommandText += addCol(mbp.NGAYMAUBENHPHAM) + ",";
                        cmd.CommandText += addCol(dr["DICHVUID"].ToString()) + ",";
                        cmd.CommandText += addCol(dr["TENDICHVU"].ToString()) + ",";
                        cmd.CommandText += addCol(dr["SOLUONG"].ToString()) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["DONGIA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIABHYT"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIANHANDAN"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIADICHVU"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol("100") + ",";
                        cmd.CommandText += addCol(float.Parse(dr["BHYT_TRA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["ND_TRA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(dr["LOAIDOITUONG"].ToString());
                        cmd.CommandText += ")";
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                transaction.Commit();
                this.lastError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                return false;
            }
        }

        /*
         * Hàm Cập nhật mẫu bệnh phẩm, dịch vụ khám bệnh khi chỉ định thuốc
         * isEdit:
            - true: Update mẫu bệnh phẩm dịch vụ khám -> xóa mbp, dvkb cũ và thêm mới vào
            - false: thêm mới mbp và dvkb
         */
        public bool sqliteTransaction_ChiDinhThuoc(Dictionary<string, DataTable> map, MauBenhPhamObj mbp, bool isEdit)
        {
            SQLiteCommand cmd = new SQLiteCommand(this.cnn);
            var transaction = this.cnn.BeginTransaction();
            string sqlQuery = "";
            DataTable dtSql = new DataTable();
            try
            {
                // Nếu sửa mẫu bệnh phẩm -> xóa dữ liệu cũ và tạo mới dữ liệu
                if (isEdit)
                {
                    cmd.CommandText = "DELETE FROM KBH_MAUBENHPHAM WHERE MAUBENHPHAMID = " + mbp.MAUBENHPHAMID;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM KBH_DICHVU_KHAMBENH WHERE MAUBENHPHAMID = " + mbp.MAUBENHPHAMID;
                    cmd.ExecuteNonQuery();
                }

                //var culture = new CultureInfo("en-US");
                var culture = CultureInfo.InvariantCulture;
                foreach (string key in map.Keys)
                {
                    DataTable dt = map[key];
                    string[] keyValue = key.Split(',');
                    mbp.KHOTHUOCID = keyValue[0];
                    mbp.LOAINHOMMAUBENHPHAM = keyValue[1];
                    // Tự sinh số phiếu
                    string soPhieu = "";
                    sqlQuery = "SELECT MAX(MAUBENHPHAMID) MAXMBP FROM KBH_MAUBENHPHAM";
                    SqliteTable_SelectSql(sqlQuery, out dtSql);
                    if (dtSql.Rows.Count > 0 && !string.IsNullOrEmpty(dtSql.Rows[0]["MAXMBP"].ToString()))
                        soPhieu = (int.Parse(dtSql.Rows[0]["MAXMBP"].ToString()) + 1).ToString();
                    else
                        soPhieu = "1";
                    mbp.SOPHIEU = "P" + soPhieu.PadLeft(9, '0');

                    cmd.CommandText = "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO, KHOTHUOCID) VALUES (";
                    cmd.CommandText += addCol(mbp.SOPHIEU) + ",";
                    cmd.CommandText += addCol(mbp.LOAINHOMMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.BENHNHANID) + ",";
                    cmd.CommandText += addCol(mbp.NGAYMAUBENHPHAM) + ",";
                    cmd.CommandText += addCol(mbp.NGUOITAO) + ",";
                    cmd.CommandText += addCol(mbp.KHOTHUOCID);
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                    // Lấy MAUBENHPHAMID vừa insert
                    cmd.CommandText = "SELECT last_insert_rowid() FROM KBH_MAUBENHPHAM";
                    string mbpId = cmd.ExecuteScalar().ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        cmd.CommandText = "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG, GHICHU) VALUES (";
                        cmd.CommandText += addCol(mbpId) + ",";
                        cmd.CommandText += addCol(mbp.BENHNHANID) + ",";
                        cmd.CommandText += addCol(mbp.NGAYMAUBENHPHAM) + ",";
                        cmd.CommandText += addCol(dr["THUOCVATTUID"].ToString()) + ",";
                        cmd.CommandText += addCol(dr["TENTHUOC"].ToString()) + ",";
                        cmd.CommandText += addCol(dr["SOLUONG"].ToString()) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["DONGIA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIABHYT"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIANHANDAN"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["GIADICHVU"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol("100") + ",";
                        cmd.CommandText += addCol(float.Parse(dr["BHYT_TRA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(float.Parse(dr["ND_TRA"].ToString()).ToString("F", culture)) + ",";
                        cmd.CommandText += addCol(dr["LOAIDOITUONG"].ToString()) + ",";
                        cmd.CommandText += addCol(dr["CACHDUNG"].ToString());
                        cmd.CommandText += ")";
                        cmd.ExecuteNonQuery();
                    }

                }
                transaction.Commit();
                this.lastError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                return false;
            }
        }

        /*
         * Hàm xóa mẫu bệnh phẩm
         */
        public bool sqliteTransaction_XoaMauBenhPham(string mbpId)
        {
            SQLiteCommand cmd = new SQLiteCommand(this.cnn);
            var transaction = this.cnn.BeginTransaction();
            try
            {
                cmd.CommandText = "DELETE FROM KBH_MAUBENHPHAM WHERE MAUBENHPHAMID = " + mbpId;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM KBH_DICHVU_KHAMBENH WHERE MAUBENHPHAMID = " + mbpId;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                this.lastError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                return false;
            }
        }

        private string addCol(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return "'" + value.Replace("'", "''") + "'";
            else
                return "''";
        }
    }
}
