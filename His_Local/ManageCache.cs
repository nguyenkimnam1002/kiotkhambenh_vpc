using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using VNPT.HIS.Common;
namespace MainForm
{
    public partial class ManageCache : DevExpress.XtraEditors.XtraForm
    {
        public ManageCache()
        {
            InitializeComponent();
        }

        private void ucGridview1_Load(object sender, EventArgs e)
        {

        }

        private void ManageCache_Load(object sender, EventArgs e)
        {
            // LocalConst.LOCAL_SQLITE 
            // 1: tạo bảng
            //if (!LocalConst.LOCAL_SQLITE.existTable("tbl_cache"))
            //    LocalConst.LOCAL_SQLITE.execute("CREATE TABLE tbl_cache (name TEXT, value TEXT, lastupdate TEXT);");

            DataTable dt = LocalConst.LOCAL_SQLITE.manaAllTable();
            ucBang.setEvent_FocusedRowChanged(ucBang_SelectChange);
            ucBang.setMultiSelectMode(true);
            ucBang.setData(dt, 0, 0);

            ucBang.Set_HidePage(true);
            ucValue.Set_HidePage(true);
        }
        private void ucBang_SelectChange(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)sender;
            if (drv != null)
            {
                string tablename = drv[0].ToString();
                DataTable dt = new DataTable();
                LocalConst.LOCAL_SQLITE.CacheObject_Select(tablename, out dt);
                //if (dt.Rows.Count > 0)
                {
                    ucValue.gridView.OptionsView.ColumnAutoWidth = false;
                    //ucValue.setEvent_FocusedRowChanged(ucBang_SelectChange);
                    ucValue.clearData_frmTiepNhan();
                    ucValue.setData(dt, 1, 1, dt.Rows.Count);
                    ucValue.setColumnAll(true);
                }
            }
        }

        private void Create_Table_Sqlite()
        {
            string TABLE_DMC_BENHNHAN = "CREATE TABLE DMC_BENHNHAN (MABENHNHAN TEXT, TENBENHNHAN TEXT, NGAYSINH TEXT, NAMSINH TEXT, TUOI TEXT, DVTUOI TEXT, GIOITINHID TEXT, NGHENGHIEPID TEXT, QUOCTICHID TEXT, DANTOCID TEXT, DIAPHUONGID TEXT, DIACHI TEXT, NGUOINHA TEXT, NGAYKHAM TEXT, PHONGKHAMID TEXT, HINHTHUCVAOVIENID TEXT, BACSIDIEUTRIID TEXT, DOITUONGBENHNHANID TEXT, MATHEBHYT TEXT, THOIGIAN_BD TEXT, THOIGIAN_KT TEXT, SINHTHETE TEXT, DU5NAM TEXT, DU6THANG TEXT, DIACHIBHYT TEXT, DKKCBBDID TEXT, TUYENID TEXT, DOITUONGMIENGIAMID TEXT, TYLEBH TEXT, TYLEMIENGIAM TEXT, MACHANDOANRAVIEN TEXT, CHANDOANRAVIEN TEXT, CHANDOANRAVIEN_KT TEXT)";

            string TABLE_KBH_MAUBENHPHAM = "CREATE TABLE KBH_MAUBENHPHAM (SOPHIEU TEXT, LOAINHOMMAUBENHPHAM TEXT, BENHNHANID TEXT, NGAYMAUBENHPHAM TEXT, NGUOITAO TEXT, PHONGTHUCHIENID TEXT, DICHVUID text )";

            string TABLE_KBH_DICHVU_KHAMBENH = "CREATE TABLE KBH_DICHVU_KHAMBENH (MAUBENHPHAMID TEXT, BENHNHANID TEXT, NGAYDICHVU TEXT, DICHVUID TEXT, TENDICHVU TEXT, SOLUONG TEXT, DONGIA TEXT, GIABHYT TEXT, GIAVP TEXT, GIADV TEXT, TYLEDV TEXT, BHYTTRA TEXT, NHANDANTRA TEXT, LOAIDOITUONG TEXT)";


            //Danh mục
            string TABLE_DMC_NHOM_MABHYT = "CREATE TABLE DMC_NHOM_MABHYT (NHOM_MABHYT_ID TEXT, MANHOM_BHYT  TEXT);";
            string TABLE_ORG_PHONG = "CREATE TABLE ORG_PHONG ( ORG_ID TEXT, ORG_NAME TEXT, ORG_TYPE TEXT)";
            string TABLE_DUC_KHO = "CREATE TABLE DUC_KHO (KHOID TEXT, TENKHO TEXT, TRANGTHAI TEXT, CSYTID TEXT)";
            string TABLE_ADM_USER = "CREATE TABLE ADM_USER (USER_ID TEXT, USER_NAME TEXT, OFFICER_NAME TEXT, OFFICER_TYPE TEXT)";

            string TABLE_DMC_BENHVIEN = "CREATE TABLE DMC_BENHVIEN ( BENHVIENKCBBD TEXT, TENBENHVIEN TEXT, DIACHI TEXT)";
            string TABLE_DMC_DOITUONG_DACBIET = "CREATE TABLE DMC_DOITUONG_DACBIET (DOITUONGDACBIETID TEXT, TENDOITUONGDACBIET TEXT, TYLEMIENGIAM TEXT, MA_BHYT TEXT, SUDUNG TEXT)";
            string TABLE_DMC_ICD = "CREATE TABLE DMC_ICD (ICD10CODE TEXT, ICD10NAME TEXT, ICD10NAME_EN TEXT, ICD10NAME_THUONGGOI TEXT, ICD10DISABLE text, ISREMOVE text)";
            string TABLE_DMC_DIAPHUONG = "CREATE TABLE DMC_DIAPHUONG (DIAPHUONGID TEXT, TENDIAPHUONG TEXT, MATIMKIEM TEXT, MABH TEXT, MADIAPHUONG TEXT "+
                ", TENVIETTAT TEXT, CAP TEXT, DIAPHUONGCHAID TEXT, tendiaphuongdaydu TEXT, tenviettatdaydu TEXT, QUOCTICHID TEXT)";
            
            string TABLE_DMC_DICHVU = "CREATE TABLE DMC_DICHVU (DICHVUID text, MADICHVU TEXT, LOAIDICHVU TEXT,LOAINHOMDICHVU TEXT, TENDICHVU TEXTTEXT,khoa TEXT,daxoa TEXT, GIABHYT TEXT, GIANHANDAN TEXT, GIADICHVU TEXT)";
            string TABLE_DMC_DOITUONG_BHYT = "CREATE TABLE DMC_DOITUONG_BHYT (TYLE_MIENGIAM TEXT, DOITUONG_BHYT_ID TEXT, MA_DOITUONG_BHYT TEXT)";
            string TABLE_DMC_HANG_BHTRAITUYEN = "CREATE TABLE DMC_HANG_BHTRAITUYEN (HANG_BHTRAITUYEN_ID TEXT, TYLE_NOITRU TEXT, TYLE_NGOAITRU TEXT)";

            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_BENHNHAN);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_KBH_MAUBENHPHAM);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_KBH_DICHVU_KHAMBENH);

            ////Danh mục
            ////LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_NHOM_MABHYT);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_ORG_PHONG);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DUC_KHO);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_ADM_USER);

            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_BENHVIEN);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DOITUONG_DACBIET);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_ICD);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DIAPHUONG);

            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DICHVU);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DOITUONG_BHYT);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_HANG_BHTRAITUYEN);


            //LocalConst.LOCAL_SQLITE.execute("CREATE TABLE "+ LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID TEXT, NGHENGHIEPTEN  TEXT);");
            //LocalConst.LOCAL_SQLITE.execute("CREATE TABLE " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID TEXT, DANTOCTEN  TEXT);");
            #region
            return;

            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('0','Việt Nam')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('292','Andorra')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('293','United arab emirates')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('294','Antigua and barbuda')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('295','Anguilla')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('296','An ba ni')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('297','Ác mê nia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('298','Netherlands antilles')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('299','Ăng gô la')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('300','Antarctica')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('301','Ác hen ti na')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('302','Samoa')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('303','Áo')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('304','Úc')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('305','Aruba việt nam')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('306','Azerbaijan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('307','Bosnia and herzegovina')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('308','Barbados')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('309','Bangladesh')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('310','Bỉ')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('311','Burkina faso')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('312','Bungari')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('313','Bahrain')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('314','Burundi')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('315','Benin')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('316','Bermuda')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('317','Brunei')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('318','Bolivia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('319','Braxin')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('320','Bahamas')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('321','Bhutan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('322','Bouvet island')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('323','Botswana')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('324','Belarus')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('325','Belize')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('326','Canada')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('327','Cocos (keeling) islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('328','Congo, the democratic republic of the')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('329','Central african republic')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('330','Công gô')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('331','Switzerland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('332','COTE DIVOIRE')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('333','Cook islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('334','Chi lê')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('335','Cameroon')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('336','Trung quốc')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('337','Colômbia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('338','Costa rica')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('339','Cu ba')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('340','Cape verde')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('341','Christmas island')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('342','Cyprus')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('343','Czech republic')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('344','Germany')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('345','Djibouti')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('346','Denmark')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('347','Dominica')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('348','Dominican republic')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('349','Algerie')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('350','East timor')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('351','Ecuador')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('352','Estonia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('353','Egypt')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('354','Western sahara')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('355','Eritrea')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('356','Spain')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('357','Ethiopia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('358','Finland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('359','Fiji')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('360','Falkland islands (malvinas)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('361','Micronesia, federated states of')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('362','Faroe islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('363','Pháp')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('364','Serbia and montenegro (formerly yugoslavia)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('365','Gabon')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('366','Anh')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('367','Grenada')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('368','Georgia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('369','French guiana')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('370','Guernsey')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('371','Ghana')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('372','Gibraltar')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('373','Greenland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('374','Gambia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('375','Guinea')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('376','Guadeloupe')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('377','Equatorial guinea')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('378','Greece')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('379','South georgia and the south sandwich islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('380','Guatemala')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('381','Guam')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('382','Guyana')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('383','Guinea-bissau')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('384','Hong kong')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('385','Heard and mc donald islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('386','Honduras')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('387','Croatia (local name: hrvatska)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('388','Haiti')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('389','Hungary')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('390','Indonesia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('391','Ireland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('392','Israel')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('393','Isle of man')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('394','India')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('395','British indian ocean territory')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('396','Iraq')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('397','Iran (islamic republic of)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('398','Iceland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('399','Italy')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('400','Jersey')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('401','Jamaica')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('402','Jordan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('403','Nhật')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('404','Kenya')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('405','Kyrgyzstan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('406','Campuchia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('407','Kiribati')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('408','Comoros')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('409','St. kitts and nevis')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('410','Bắc Triều Tiên')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('411','Hàn quốc')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('412','Kuwait')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('413','Cayman islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('414','Kazakhstan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('415','Lào')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('416','Lebanon')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('417','St. lucia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('418','Liechtenstein')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('419','Sri lanka')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('420','Liberia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('421','Lesotho')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('422','Lithuania')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('423','Luxembourg')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('424','Latvia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('425','Libyan arab jamahiriya')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('426','Morocco')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('427','Monaco')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('428','Moldova, republic of')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('429','Madagascar')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('430','Marshall islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('431','Macedonia, the former yugoslav republic of')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('432','Mali')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('433','Myanmar')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('434','Mongolia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('435','Macau')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('436','Northern mariana islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('437','Martinique')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('438','Mauritania')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('439','Montserrat')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('440','Malta')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('441','Mauritius')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('442','Maldives')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('443','Malawi')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('444','Mexico')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('445','Malaisia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('446','Mozambique')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('447','Namibia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('448','New caledonia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('449','Niger')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('450','Norfolk island')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('451','Nigeria')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('452','Nicaragua')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('453','Hà lan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('454','Norway')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('455','Nepal')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('456','Nauru')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('457','Niue')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('458','New zealand')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('459','Oman')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('460','Panama')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('461','Peru')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('462','French polynesia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('463','Papua new guinea')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('464','Philippines')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('465','Pakistan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('466','Poland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('467','St. pierre and miquelon')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('468','Pitcairn')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('469','Puerto rico')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('470','Palestinian authority')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('471','Portugal')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('472','Palau')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('473','Paraguay')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('474','Qatar')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('475','Reunion')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('476','Romania')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('477','Nga')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('478','Rwanda')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('479','Saudi arabia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('480','Solomon islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('481','Seychelles')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('482','Sudan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('483','Sweden')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('484','Singapore')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('485','St. helena')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('486','Slovenia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('487','Svalbard and jan mayen islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('488','Slovakia (slovak republic)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('489','Sierra leone')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('490','San marino')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('491','Senegal')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('492','Somalia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('493','Suriname')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('494','Sao tome and principe')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('495','El salvador')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('496','Syrian arab republic')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('497','Swaziland')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('498','Turks and caicos islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('499','Chad')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('500','French southern territories')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('501','Togo')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('502','Thái lan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('503','Tajikistan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('504','Tokelau')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('505','Timor leste')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('506','Turkmenistan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('507','Tunisia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('508','Tonga')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('509','Turkey')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('510','Trinidad and tobago')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('511','Tuvalu')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('512','Đài loan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('513','Tanzania, united republic of')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('514','Ukraine')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('515','Uganda')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('516','United states minor outlying islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('517','Hoa kỳ')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('518','Uruguay')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('519','Uzbekistan')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('520','Vatican city state (holy see)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('521','St. vincent and the grenadines')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('522','Venezuela')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('523','Virgin islands (british)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('524','Virgin islands (u.s.)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('526','Vanuatu')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('527','Wallis and futuna islands')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('528','Samoa')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('529','Yemen')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('530','Mayotte')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('531','Yugoslavia (serbia and montenegro)')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('532','Sovereign military order of malta (smom)no iso code')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('533','British southern and antarctic territories>')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('534','England no iso code')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('535','Scotlandno iso code')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('536','Northern ireland no iso code')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('537','Great britain see unite')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('538','Wales no iso code')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('539','South africa')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('540','Zambia')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_DANTOC + " (DANTOCID ,DANTOCTEN)  VALUES ('541','Zimbabwe')");
           
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('1','1 - Công nhân')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('2','2 - Lực lượng vũ trang')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('3','3 - Trẻ em')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('4','4 - Sinh viên, học sinh')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('5','5 - Hưu và trên 60 tuổi')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('6','6 - Nông dân')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('7','7 - Trí thức')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('8','8 - Hành chính, sự nghiệp')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('9','9 - Y tế')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('10','10 - Dịch vụ')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('11','11 - Ngoại kiều')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('12','12 - Nhân dân')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('13','13 - Giáo viên')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('14','14 - Thương binh')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('15','15 - Kế toán')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('16','16 - Loại khác')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('18','18 - NN: Người lao động làm việc trong các cơ quan, tổ chức nước ngoài hoặc tổ chức quốc tế tại Việt Nam')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('19','19 - Giáo Sư')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('20','20 - Lao Dong Tu Do')");
            LocalConst.LOCAL_SQLITE.execute("insert into " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID , NGHENGHIEPTEN)  VALUES ('27','27 - Nghề gia sư')");
            #endregion



            // LocalConst.LOCAL_SQLITE.execute("insert into DMC_NHOM_MABHYT (NHOM_MABHYT_ID , MANHOM_BHYT)  VALUES ('111','222')  ");
            DataTable dt;
            LocalConst.LOCAL_SQLITE.SqliteTable_Select("DMC_DICHVU", out dt);

            //string TABLE_DMC_DICHVsssU = "";


            //LocalConst.LOCAL_SQLITE.execute("CREATE TABLE " + LocalConst.tbl_DMC_NGHENGHIEP + " (NGHENGHIEPID TEXT, NGHENGHIEPTEN  TEXT);");
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            // Taojo cache mới
            Create_Table_Sqlite();



            //LocalConst.LOCAL_SQLITE.dropTable(LocalConst.LOCAL_SQLITE.TBL_CACHE);
            //// 1: tạo bảng
            //if (!LocalConst.LOCAL_SQLITE.existTable(LocalConst.LOCAL_SQLITE.TBL_CACHE))
            //    LocalConst.LOCAL_SQLITE.execute("CREATE TABLE "+ LocalConst.LOCAL_SQLITE.TBL_CACHE + " (name TEXT, value TEXT, lastupdate TEXT);");

            #region Tên viết tắt Tỉnh huyện xã
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_TinhhuyenxaViettat, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_TinhhuyenxaViettat));
            #endregion

            #region Danh sách các bệnh viện
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_NoiDKKCB, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_NoiDKKCB));
            #endregion

            #region Danh sách các Bệnh 
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsBenh, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_DsBenh));
            #endregion

            #region Dân tộc-Giới tính-Nghề nghiệp-Quốc tịch-Khu vực
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Dantoc, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Dantoc));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Gioitinh, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Gioitinh, "1"));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Nghenghiep, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Nghenghiep));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Quoctich, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Quoctich));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Noisong, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Noisong, "76"));
            #endregion

            #region Các ds thuốc trong form Chỉ định thuốc 
            ////khothuocID, company_id, _loainhommaubenhpham_id, loaithuocid	 
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] { "770", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"912", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });

            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"770", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13",new string[] { "912", Const.local_user.COMPANY_ID.ToString(), "7", "10" });

            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"770", Const.local_user.COMPANY_ID.ToString(), "7", "11" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "11" });
            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "11" });

            //RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.02", new string[] {Const.local_user.COMPANY_ID.ToString() });
            #endregion

            #region getTuyenKhamBenh  getDTDACBIET
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DTMienGiam, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DTMienGiam));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_Phongkham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Phongkham));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_TrangThaiKham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_TrangThaiKham));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_XuTriKB, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_XuTriKB));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DichVuKhac, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DichVuKhac));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_KhoaDTNgT, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_KhoaDTNgT));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_KhoaDTNT, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_KhoaDTNT));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DSHopDong, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DSHopDong));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_BacSyKham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_BacSyKham));
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DuongDung, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DuongDung));


            #endregion

            #region
            #endregion

            #region
            #endregion

            #region DS các tỉnh, huyện, xã 
            //tỉnh
            //string ds_tinh = RequestHTTP.WS_getTinhTP();
            //DataTable dt_tinh = Func.fill_ArrayStr_To_Datatable(ds_tinh, "");
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsTinh, ds_tinh);

            //for (int i = 0; i < dt_tinh.Rows.Count; i++)
            //{
            //    //huyện
            //    string id_tinh = dt_tinh.Rows[i]["col1"].ToString();
            //    string ds_huyen = RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DsHuyenXa, id_tinh);
            //    DataTable dt_huyen = Func.fill_ArrayStr_To_Datatable(ds_huyen, "");
            //    LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsHuyenXa + "_" + id_tinh, ds_huyen);

            //    for (int j = 0; j < dt_huyen.Rows.Count; j++)
            //    {
            //        //xã
            //        string id_huyen = dt_huyen.Rows[j]["col1"].ToString();
            //        string ds_xa = RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DsHuyenXa, id_huyen);
            //        LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsHuyenXa + "_" + id_huyen, ds_xa);
            //    }
            //}
            #endregion

            #region ds khoa + phòng
            //string ds_khoa = RequestHTTP.WS_getKhoa();
            //DataTable dt_khoa = Func.fill_ArrayStr_To_Datatable(ds_khoa, "");
            //LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + Const.local_user.HOSPITAL_ID, ds_khoa);

            //for (int i = 0; i < dt_khoa.Rows.Count; i++)
            //{
            //    string id_khoa = dt_khoa.Rows[i][0].ToString();
            //    string ds_phong = RequestHTTP.WS_getPhong(id_khoa);
            //    LocalConst.LOCAL_SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + id_khoa, ds_phong);
            //}
            #endregion
        }

        private void XOA_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa cấu trúc bảng đã chọn?", "Xóa bảng", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            int[] idxSelectRows = ucBang.gridView.GetSelectedRows();
            DataRowView drView;
            for (int i = 0; i < idxSelectRows.Length; i++)
            {
                drView = (DataRowView)ucBang.gridView.GetRow(idxSelectRows[i]);
                LocalConst.LOCAL_SQLITE.dropTable(drView[0].ToString());
            }
        }
        private void XoaDL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa dữ liệu bảng đã chọn?", "Xóa dl", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            int[] idxSelectRows = ucBang.gridView.GetSelectedRows();
            DataRowView drView;
            for (int i = 0; i < idxSelectRows.Length; i++)
            {
                drView = (DataRowView)ucBang.gridView.GetRow(idxSelectRows[i]);

                LocalConst.LOCAL_SQLITE.truncateTable(drView[0].ToString());
            }
        }

        private void F5_Click(object sender, EventArgs e)
        {
            DataTable dt = LocalConst.LOCAL_SQLITE.manaAllTable();
            ucBang.setData(dt, 0, 0);
        }

    }
}