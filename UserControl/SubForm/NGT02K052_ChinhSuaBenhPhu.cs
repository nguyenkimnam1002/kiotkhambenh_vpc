using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.UserControl.SubForm
{
    public partial class NGT02K052_ChinhSuaBenhPhu : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtBenhPhu = new DataTable();

        DataTable dtSearchLookup;

        string valueMember;
        string displayMember;
        string valueMemberName;
        string displayMemberName;
        string listBP;

        public NGT02K052_ChinhSuaBenhPhu(object dataSearchLookup
            , string valueMember, string displayMember
            , string valueMemberName, string displayMemberName
            , string listBP)
        {
            InitializeComponent();

            this.valueMember = valueMember;
            this.displayMember = displayMember;
            this.valueMemberName = valueMemberName;
            this.displayMemberName = displayMemberName;
            this.dtSearchLookup = (DataTable)dataSearchLookup;
            this.listBP = listBP;
        }

        private void NGT02K052_ChinhSuaBenhPhu_Load(object sender, EventArgs e)
        {
            ucSearchLookup1.setData(dtSearchLookup, valueMember, valueMember);
            ucSearchLookup1.setEvent(ucSearchLookupThuoc_OnChange);
            ucSearchLookup1.setAllColumn(false);
            ucSearchLookup1.setColumn(valueMember, 0, valueMemberName, 0);
            ucSearchLookup1.setColumn(displayMember, 1, displayMemberName, 0);
             
            // Danh sách BP
            dtBenhPhu.Columns.Add("STT");
            dtBenhPhu.Columns.Add(valueMember);
            dtBenhPhu.Columns.Add(displayMember);
            dtBenhPhu.Columns.Add("delete");
            
            ucDSBenh.Set_HidePage(false);
            ucDSBenh.onIndicator();
            ucDSBenh.setData(dtBenhPhu, 0, 0);
            
            ucDSBenh.setColumnAll(false);
            ucDSBenh.setColumn(valueMember, 1, valueMemberName, 20);
            ucDSBenh.setColumn(displayMember, 2, displayMemberName);
            ucDSBenh.setColumn("delete", 3, "Xóa", 20);

            ucDSBenh.setColumnButtonImage("delete", "delete.png", btnDelete_Click);

            ucDSBenh.gridView.OptionsView.ColumnAutoWidth = true;

            //Khởi tạo danh sách bệnh phụ
            string[] list = listBP.Split(';');
            for (int i = 0; i < list.Length; i++)
                ucSearchLookup1.SelectValue = list[i].Trim();
        } 
        private void ucSearchLookupThuoc_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                //Kiểm tra đã tồn tại trong DS
                bool check = true;
                for (int i=0; i< dtBenhPhu.Rows.Count; i++)
                    if (dtBenhPhu.Rows[i][valueMember].ToString() == drv[valueMember].ToString())
                    {
                        check = false;
                        MessageBox.Show("Bệnh phụ đã có trong danh sách.");
                        break;
                    }

                if (check)
                {
                    DataRow dr = dtBenhPhu.NewRow();
                    dr[0] = dtBenhPhu.Rows.Count + 1;
                    dr[valueMember] = drv[valueMember].ToString();
                    dr[displayMember] = drv[displayMember].ToString();

                    dtBenhPhu.Rows.Add(dr);
                    //ucDSBenh.gridView.OptionsView.ColumnAutoWidth = true;
                }
            }
            catch (Exception ex) { System.Console.WriteLine(ex.ToString()); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ucSearchLookup1.SelectValue = "";
            dtBenhPhu.Rows.RemoveAt(ucDSBenh.gridView.FocusedRowHandle); 
        } 

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ReturnData != null)
            {
                Dictionary<string, string> SelectList = new Dictionary<string, string>();
                for (int i = 0; i < dtBenhPhu.Rows.Count; i++)
                    SelectList.Add(dtBenhPhu.Rows[i][valueMember].ToString(), dtBenhPhu.Rows[i][displayMember].ToString());

                ReturnData(SelectList, null);
                this.Close();
            }
        }        

        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}