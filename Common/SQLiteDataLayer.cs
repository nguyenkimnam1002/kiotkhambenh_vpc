using System;
using System.Collections.Generic;
using System.IO;
using System.Text; 

using System.Data;
using System.Data.SQLite;

namespace VNPT.HIS.Common
{
    public class SQLiteDataLayer : System.IDisposable
    {
        #region Connection
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public SQLiteDataLayer()
        {
            this.cnn = null;
            this.connected = false;
            this.lastError = string.Empty;

            this.strConn = Const.CONN_SQLite;
            Connect();
        } 
        public void Dispose()
        {
            this.Disconnect();
        }
        private bool Connect()
        {
            log.Info("Conn sqltie" + strConn);
            // Nếu đã kết nối rồi thì sẽ gây lỗi
            if (this.connected)
            {
                this.lastError = "Connection already established.";
                return false;
            }

            // Khởi tạo kết nối
            this.cnn = new SQLiteConnection(strConn);
            //Data Source=sqlitename;Password=password;

            // Nếu có cho mật mã thì ấn định luôn
            //this.cnn.SetPassword("test");

            try
            {
                // Mở CSDL
                this.cnn.Open();
                // Khởi tạo cấu trúc các bảng
                if (!existTable("authorsqlitehunglv")) createAllTable();
            }
            catch (SQLiteException ex)
            {
                log.Fatal(ex.ToString());
                // thường có 2 trường hợp lỗi ở đây:
                // 1. Tập tin CSDL không truy cập được.
                // 2. Mật mã không đúng.
                this.lastError = ex.Message;
                return false;
            }

            // Đã kết nối
            log.Info("Conn sqltie OKE");
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
        public bool ChangePassword(string newPassword)
        {
            try{
                if (this.connected == false)
                {
                    this.lastError = "Connection not established.";
                    return false;
                }

                this.cnn.ChangePassword(newPassword);

                this.lastError = string.Empty;
                return true;                
            }
            catch(Exception ex){
                System.Console.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion

        #region Common function
        public bool execute(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                sql,
                this.cnn);
            cmd.ExecuteNonQuery();

            this.lastError = string.Empty;
            return true;
        }
        public bool existTable(string table_name)
        {
            bool ret = false;
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(
                    "SELECT name FROM sqlite_master WHERE type='table' AND name='" + table_name + "';",
                    this.cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0) ret = true;

                da.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
        }
        public bool truncateAllTable()
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(
                "SELECT name FROM sqlite_master WHERE type='table';",
                this.cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i]["name"].ToString() != "authorsqlitehunglv")
                    truncateTable(dt.Rows[i]["name"].ToString());

            // Trả bộ nhớ
            da.Dispose();
            dt.Dispose();
            return true;
        }
        public bool truncateTable(string table_name)
        {
            execute("DELETE FROM " + table_name + ";");
            execute("VACUUM;");
            return true;
        }
        public bool dropAllTable()
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(
                "SELECT name FROM sqlite_master WHERE type='table';",
                this.cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
                if (dt.Rows[i]["name"].ToString() != "authorsqlitehunglv")
                    dropTable(dt.Rows[i]["name"].ToString());

            // Trả bộ nhớ
            da.Dispose();
            dt.Dispose();
            return true;
        }
        public bool dropTable(string table_name)
        {
            execute("DROP TABLE IF EXISTS " + table_name + ";");
            execute("VACUUM;");
            return true;
        }

        #endregion

        #region DÙNG CHO THIẾT LẬP CACHE KIỂU MỚI
        public string TBL_CACHE = "tbl_cache";
        public string TBL_TEMP_VALUE = "tbltempvalue";
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
        public bool test_cache_add(string name, string value)
        {
            try
            {
                string lastupdate = Func.getSysDatetime(Const.FORMAT_date1);
                string sql = "UPDATE " + TBL_CACHE + " set value= @value where name=@name;";
                SQLiteCommand cmd = new SQLiteCommand(sql, this.cnn);
                // Ấn định tham số
                cmd.Parameters.Add(new SQLiteParameter("@name", name));
                cmd.Parameters.Add(new SQLiteParameter("@value", value));
                cmd.Parameters.Add(new SQLiteParameter("@lastupdate", lastupdate));
                // Thi hành
                int ret = cmd.ExecuteNonQuery();

                if (ret == 0)
                {
                    sql = "INSERT INTO " + TBL_CACHE + " VALUES (@name, @value, @lastupdate);";
                    cmd = new SQLiteCommand(sql, this.cnn);

                    // Ấn định tham số
                    cmd.Parameters.Add(new SQLiteParameter("@name", name));
                    cmd.Parameters.Add(new SQLiteParameter("@value", value));
                    cmd.Parameters.Add(new SQLiteParameter("@lastupdate", lastupdate));
                    // Thi hành
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return false;
            }
        }
        public string test_cache_get(string name)
        {
            try
            {
                string sql = "SELECT value FROM " + TBL_CACHE + " WHERE name=@name;";
                SQLiteCommand cmd = new SQLiteCommand(sql, this.cnn);
                cmd.Parameters.Add(new SQLiteParameter("@name", name));

                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();

                da.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
        }

        #endregion

        #region Hàm khác
        //frm Thiết lập phòng
        public void SelectDept_updatename(int local_khoaId, string local_khoa, int local_phongId, string local_phong)
        {
            cache_add("local_khoaId", local_khoaId);
            cache_add("local_khoa", local_khoa);

            cache_add("local_phongId", local_phongId);
            cache_add("local_phong", local_phong);
        }
        public void SelectDept_getname(out int local_khoaId, out string local_khoa, out int local_phongId, out string local_phong)
        {
            local_khoaId = cache_getint("local_khoaId");
            local_khoa = cache_get("local_khoa");

            local_phongId = cache_getint("local_phongId");
            local_phong = cache_get("local_phong");
        }
        //Khởi tạo create all table
        private void createAllTable()
        {
            SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE authorsqlitehunglv (name TEXT);", this.cnn);
            cmd.ExecuteNonQuery();
            // các biến lẻ
            cmd = new SQLiteCommand("CREATE TABLE "+ TBL_TEMP_VALUE + " (name TEXT, value TEXT);", this.cnn);
            cmd.ExecuteNonQuery();

            // các tập đối tượng
            cmd = new SQLiteCommand("CREATE TABLE "+ TBL_CACHE + " (name TEXT, value TEXT, lastupdate TEXT);", this.cnn);
            cmd.ExecuteNonQuery();
        }
        public void clearAllCache()
        {
            truncateAllTable();
        }
        #endregion


        public void CacheObject_Create(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            CacheObject_Create(dt.TableName, dt);
        }
        
        public void CacheObject_Create(string tableName, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            try
            {
                // Drop table
                execute("DROP TABLE IF EXISTS " + tableName);

                // Create table
                string sql_create = "CREATE TABLE " + tableName + " (";
                string sql_insert = "INSERT INTO " + tableName + " VALUES (";

                for (int i = 0; i < dt.Columns.Count; i++){
                    sql_create += dt.Columns[i].ColumnName + " TEXT,";
                    sql_insert += "@" + dt.Columns[i].ColumnName + ",";
                }
                if (dt.Columns.Count > 0)
                {
                    sql_create = sql_create.Substring(0, sql_create.Length - 1);
                    sql_insert = sql_insert.Substring(0, sql_insert.Length - 1);
                }
                sql_create += ");";
                sql_insert += ");";

                execute(sql_create);

                // Insert dt to table

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SQLiteCommand cmd = new SQLiteCommand(sql_insert, this.cnn);
                    for (int j = 0; j < dt.Columns.Count; j++)
                        cmd.Parameters.Add(new SQLiteParameter("@" + dt.Columns[j].ColumnName, dt.Rows[i][j].ToString()));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            { log.Fatal(ex.ToString()); }
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
            { log.Fatal(ex.ToString()); }
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
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return ret;
        }
        //update từng bản ghi
        public void CacheObject_Update(string tableName, DataTable dt)
        {
            //  table

            // Create table

            // Insert dt to table

        }
        
        
        

        // biến lẻ
        public bool cache_add(string name, string value)
        {
            try
            {
                log.Info(name + "-" + value);
                string sql = "UPDATE "+ TBL_TEMP_VALUE + " set value= @value where name=@name;";
                SQLiteCommand cmd = new SQLiteCommand(sql, this.cnn);
                // Ấn định tham số
                cmd.Parameters.Add(new SQLiteParameter("@name", name));
                cmd.Parameters.Add(new SQLiteParameter("@value", value));
                // Thi hành
                int ret = cmd.ExecuteNonQuery();

                log.Info((ret == 0 ? "-->Them moi" : "-->Update"));
                if (ret == 0)
                {
                    sql = "INSERT INTO " + TBL_TEMP_VALUE + " VALUES (@name, @value);";
                    cmd = new SQLiteCommand(sql, this.cnn);

                    // Ấn định tham số
                    cmd.Parameters.Add(new SQLiteParameter("@name", name));
                    cmd.Parameters.Add(new SQLiteParameter("@value", value));
                    // Thi hành
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return false;
            }
        }
        public bool cache_add(string name, int value)
        {
            return cache_add(name, value + "");
        }
        public string cache_get(string name)
        {
            try
            { 
                string sql = "SELECT value FROM " + TBL_TEMP_VALUE + " WHERE name=@name;";
                SQLiteCommand cmd = new SQLiteCommand(sql, this.cnn);
                cmd.Parameters.Add(new SQLiteParameter("@name", name));

                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0) log.Info(name + ": " + dt.Rows.Count + " " + dt.Rows[0][0].ToString());
                else log.Info(name + ": " + dt.Rows.Count);

                if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();

                da.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            { 
                log.Fatal(ex.ToString()); 
            }
            return "";
        }
        public int cache_getint(string name)
        {
            try
            {
                string s = cache_get(name);
                return Convert.ToInt32(s);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return -1;
            }
            return -1;
        }
        
        

        
    }
}
