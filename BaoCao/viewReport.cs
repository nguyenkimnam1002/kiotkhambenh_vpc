using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Net;
using DevExpress.XtraEditors;
using Newtonsoft.Json;

using VNPT.HIS.Common;
using VNPT.HIS.BaoCao.Class;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;

namespace VNPT.HIS.BaoCao
{
    public partial class viewReport : DevExpress.XtraEditors.XtraForm
    {
        private string[][] resultArrRF = null;
        string paramStrReportField = "";
        int reportFieldLength = -1;
        string reportID = "-1";
        string reportTypePrint = "";
        string selectedReportCode = "";
        
        public viewReport()
        {
            InitializeComponent();


            layoutControlItem_Button.Size = new System.Drawing.Size(layoutControlItem_Button.Size.Width, ROW_HEIGHT);
            
            ribbonControl1.Minimized = true;

            DataTable dt_ChiTiet;
            string userType = Const.local_user.USER_GROUP_ID;
            string companyID = Const.local_user.COMPANY_ID;
            string pid = "1";
            string reportType = "1,2,4,5";
            String requestListBC = "{\"func\":\"dbCALL_SP_O\",\"code\":\"thu@nnc\",\"params\":[\"jdbc/DYNAMICREPORTDS\",\"DRPT.S01\",\"" + pid + "$" + companyID + "$" + userType + "$" + reportType + "$1\",0],\"uuid\":\"thu@nnc\"}";


            string respListBC = RequestHTTP.sendRequest(requestListBC);
            ResponsObj retListBC = new ResponsObj();
            retListBC = JsonConvert.DeserializeObject<ResponsObj>(respListBC, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            dt_ChiTiet = MyJsonConvert.toDataTable(retListBC.result);
 
            navBarControl1.GroupExpanded += new DevExpress.XtraNavBar.NavBarGroupEventHandler(navBarControl1_GroupExpanded);
            navBarControl1.LinkClicked += new NavBarLinkEventHandler(navBarControl1_LinkClicked);
            navBarControl1.MouseUp += new MouseEventHandler(navBarControl1_MouseUp);
              
            if (dt_ChiTiet != null && dt_ChiTiet.Rows.Count > 0)
            {
                for (int i = 0, length = dt_ChiTiet.Rows.Count; i < length; i++ ){
                    if ("#".Equals(dt_ChiTiet.Rows[i]["HLINK"].ToString()))
                    {
                        string nodeId = dt_ChiTiet.Rows[i]["ID"].ToString();
                        DevExpress.XtraNavBar.NavBarGroup navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
                        navBarGroup.Caption = dt_ChiTiet.Rows[i]["NAME"].ToString(); 
                        navBarGroup.Expanded = false ; 

                        navBarGroup.Name = "navBarGroup" + i;

                        navBarControl1.Groups.Add(navBarGroup);
                        for (int j = i + 1; j < length; j++)
                        {
                            if (nodeId.Equals(dt_ChiTiet.Rows[j]["PARENTID"].ToString()))
                            {
                                DevExpress.XtraNavBar.NavBarItem navBarItem = new DevExpress.XtraNavBar.NavBarItem(); 
                                navBarItem.Caption = dt_ChiTiet.Rows[j]["NAME"].ToString();
                                navBarItem.Name = dt_ChiTiet.Rows[j]["ID"].ToString();
                                navBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(link_Click);
                                navBarGroup.ItemLinks.Add(navBarItem);
                            }
                        }
                    }
                }

            }
        }

        void navBarControl1_MouseUp(object sender, MouseEventArgs e)
        {
            var info = navBarControl1.CalcHitInfo(e.Location);
            if (info.InGroupButton)
            {
                return;
            }
            if (info.InGroupCaption)
            {
                info.Group.Expanded = !info.Group.Expanded;
            }
        }

        void navBarControl1_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        void navBarControl1_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            foreach (NavBarGroup item in (sender as NavBarControl).Groups)
            {
                if (e.Group != item)
                {
                    item.Expanded = false;
                }
            }
        }

        protected void link_Click(object sender, EventArgs e)
        {
            layoutControlItem_Button.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always; 

            pdfViewer1.DocumentFilePath = null;  
            //some code
            DataTable dtReportField;
            int leftComponentNumber = 0;
            DevExpress.XtraNavBar.NavBarItem selectedLink = (DevExpress.XtraNavBar.NavBarItem)sender;
            selectedReportCode = selectedLink.Name;
            string request = "{\"func\":\"ajaxExecuteQuery\",\"code\":\"thu@nnc\",\"params\":[\"jdbc/DYNAMICREPORTDS\",\"DRPT.S02\"],\"options\":[{\"name\":\"[0]\",\"value\":" + selectedLink.Name + "}],\"uuid\":" + Const.local_user.UUID + "}";
            //MessageBox.Show(selectedLink.Name);
            string respReport = RequestHTTP.sendRequest(request);
            //buildParam(report_id);

            string reportName = "";
            string reportId = "";
            int ParamType, ControlType;
            
            if (respReport != null && !"".Equals(respReport))
            {
                string[][] resultArr = Newtonsoft.Json.JsonConvert.DeserializeObject<string[][]>(respReport);

                reportId = resultArr[0][0];
                reportID = resultArr[0][0];
                reportName = resultArr[0][1];
                //reportTypePrint = resultArr[0][2];
                lbTitle.Text = reportName;
            }

            string requestReportField = "{\"func\":\"ajaxExecuteQuery\",\"code\":\"thu@nnc\",\"params\":[\"jdbc/DYNAMICREPORTDS\",\"DRPT.S03\"],\"options\":[{\"name\":\"[0]\",\"value\":" + reportId + "}],\"uuid\":" + Const.local_user.UUID + "}";
            
            string respListReportFieldAr = RequestHTTP.sendRequest(requestReportField);
          
            if (respListReportFieldAr != null && !"".Equals(respListReportFieldAr))
            {
                
                try{
                    resultArrRF = Newtonsoft.Json.JsonConvert.DeserializeObject<string[][]>(respListReportFieldAr);
                }catch (Exception ex)
                {

                }
                if (resultArrRF != null && resultArrRF.Length > 0)
                {
                    layoutControl_TimKiem_Right.Clear();
                    layoutControl_TimKiem_Left.Clear();
                 
                    int size = 0; 
                    reportFieldLength = resultArrRF.Length;


                    if (reportFieldLength % 2 != 0) leftComponentNumber = reportFieldLength / 2 + 1; 
                    else leftComponentNumber = reportFieldLength / 2; 

                    for (int j = 0; j < reportFieldLength; j++)
                    {
                        string param_desc = resultArrRF[j][1];
                        string paramId = resultArrRF[j][0];
                        string fieldfsql = resultArrRF[j][2];
                        string parentChild = resultArrRF[j][3];
                        string fieldType = resultArrRF[j][5];
                        string paramName = resultArrRF[j][9];
                        string operation = resultArrRF[j][7];
                        int lookup = Func.Parse(resultArrRF[j][6]);
                        ParamType = ReportUtil.getParamType(fieldType);
                        ControlType = ReportUtil.getControlType(operation, fieldType, lookup);

                        if (ControlType == ReportUtil.CONTROL_TYPE_SINGLE_SELECT
                                || ControlType == ReportUtil.CONTROL_TYPE_MULTI_SELECT)
                        {
                            DevExpress.XtraLayout.LayoutControlItem item1;

                            if (j < leftComponentNumber)
                            {
                                item1 = layoutControl_TimKiem_Left.Root.AddItem();
                            }
                            else
                            {
                                item1 = layoutControl_TimKiem_Right.Root.AddItem();
                            }
                            // Set the item's Control and caption.
                            item1.AppearanceItemCaption.Font = Const.fontDefault;
                            item1.Name = "LayoutItem" + j;
                            DevExpress.XtraEditors.LookUpEdit LookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
                            LookUpEdit1.Properties.Appearance.Font = Const.fontDefault;
                            LookUpEdit1.Properties.AppearanceDropDown.Font = Const.fontDefault;
                            LookUpEdit1.Name = "lkpEdit" + j;
                            LookUpEdit1.Properties.NullText = "";
                            item1.Control = LookUpEdit1;
                            LookUpEdit1.ItemIndex = 0;
                            size = size + LookUpEdit1.Height + 2;

                            //Set datatable cho combobox
                            DataTable dt = new DataTable();
                            dt = ServiceViewReport.getDataToCbx(fieldfsql);
                            if (dt.Rows.Count > 0)
                            {
                                setData(LookUpEdit1, dt, 0, 1);
                            }
                            item1.Text = param_desc;
                        }
                        else if (ControlType == ReportUtil.CONTROLTYPE_DOUBLE_DATEPICKER)
                        {

                        }
                        else if (ControlType == ReportUtil.CONTROLTYPE_SINGLE_DATEPICKER || ControlType == ReportUtil.CONTROLTYPE_SINGLE_DATEPICKER_NO_TIME)
                        {
                            string format = @"dd\/MM\/yyyy";
                            if (ControlType == ReportUtil.CONTROLTYPE_SINGLE_DATEPICKER) format = @"dd\/MM\/yyyy HH:mm:ss";
                            //else if (ControlType == ReportUtil.CONTROLTYPE_SINGLE_DATEPICKER_MONTH)
                            
                            DevExpress.XtraLayout.LayoutControlItem item1;
                            if (j < leftComponentNumber) item1 = layoutControl_TimKiem_Left.Root.AddItem(); 
                            else item1 = layoutControl_TimKiem_Right.Root.AddItem();

                            // Set the item's Control and caption.
                            item1.AppearanceItemCaption.Font = Const.fontDefault;
                            item1.Name = "LayoutItem" + j;
                            DateEdit dateEdit = new DateEdit();
                            dateEdit.Properties.Appearance.Font = Const.fontDefault; 
                            dateEdit.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
                            dateEdit.Name = "dateEdit" + j;
                            dateEdit.Text = param_desc;
                            dateEdit.EditValue = DateTime.Now;                           
                            SetFormat(dateEdit, format);
                            item1.Control = dateEdit;
                            item1.Text = param_desc;
                            //item1.SizeConstraintType
                            size = size + dateEdit.Height + 2;

                            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                            dateEdit.Properties.NullValuePrompt = format.Replace("\\","");
                            dateEdit.Properties.NullValuePromptShowForEmptyValue = true;  
                        } 
                        
                    }

                    paramStrReportField = paramStrReportField + "]";
                    int a = layoutControl_TimKiem_Right.MaximumSize.Height;
                }
            }

            //reset lại size các control
            layoutControlItem_Title.ControlMaxSize = new System.Drawing.Size(lbTitle.Size.Width, layoutControlItem_Title.ControlMaxSize.Height);
            
            layoutControlItem_TimKiem.Size = new System.Drawing.Size(layoutControlItem_TimKiem.Size.Width, 5 + (resultArrRF.Length + 1) / 2 * ROW_HEIGHT);
             
            layoutControlItem_View.Location = new Point(layoutControlItem_View.Location.X, layoutControlItem_Button.Location.Y + ROW_HEIGHT);
        }

        int ROW_HEIGHT = 26;

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }


        private void navBarGroup1_ItemChanged(object sender, EventArgs e)
        {
            MessageBox.Show("expanded");
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {            
            paramStrReportField = getParam(reportFieldLength);
            if (resultArrRF != null && resultArrRF.Length > 0)
            {
                try{
                    reportTypePrint = "pdf";
                    string paramStrReportFiel1 = ServiceViewReport.EncodeTo64(paramStrReportField); 
                    string url = ServiceViewReport.getUrlReport(reportID, paramStrReportField, reportTypePrint);
                    Stream stream = ServiceViewReport.GetStreamFromUrl(url);
                    if (stream != null)
                    {
                        pdfViewer1.LoadDocument(stream);
                    }

                }catch(Exception ex){
                }
            }
        }

        public static string buildParamField(string name, string type, string value)
        {
            string param = "{";
            param = param + "\"name\":\"" + name + "\"," + "\"type\":\"" + type + "\"," + "\"value\":\"" + value + "\"}";
            return param;
        }

        public string getParam(int reportLength)
        {
            string value = "";
            string paramStr = "[";
            for (int j = 0; j < reportLength; j++)
            {
                string fieldType = resultArrRF[j][5];
                string paramName = resultArrRF[j][9];
                if ((fieldType.Contains("adDate")))
                {
                    DateEdit dateEdit = this.Controls.Find("dateEdit" + j, true).FirstOrDefault() as DateEdit;
                    //value = dateEdit.DateTime.ToString();
                    if (dateEdit.EditValue != null)
                    {
                        value = dateEdit.DateTime.ToString(VNPT.HIS.Common.Const.FORMAT_date1);
                    }
                    else
                    {
                        value = "";
                    }

                    paramStr = paramStr + buildParamField(paramName, "Date:D", value) + ",";
                }
                if ((fieldType.Contains("adDBTimeStamp")))
                {
                    DateEdit dateEdit = this.Controls.Find("dateEdit" + j, true).FirstOrDefault() as DateEdit;
                    //value = dateEdit.DateTime.ToString();
                    if (dateEdit.EditValue != null)
                    {
                        value = dateEdit.DateTime.ToString(VNPT.HIS.Common.Const.FORMAT_datetime1);
                    }
                    else
                    {
                        value = "";
                    }

                    paramStr = paramStr + buildParamField(paramName, "Date:D", value) + ",";
                }
                if ((fieldType.Contains("adNumeric")))
                {
                    LookUpEdit cbxEdit = this.Controls.Find("lkpEdit" + j, true).FirstOrDefault() as LookUpEdit;

                    if (cbxEdit.EditValue != null)
                    {
                        value = cbxEdit.EditValue + "@" + cbxEdit.Properties.GetDisplayText(cbxEdit.EditValue);
                    }
                    else
                    {
                        value = "-1" + "@" + "Chọn";
                    }
                    //value = "-1" + "@" + "chọn" ;
                    paramStr = paramStr + buildParamField(paramName, "String[][]", value) + ",";
                }
            }
            paramStr = paramStr + "]";
            paramStr = paramStr.Replace(",]", "]");

            return paramStr;
        }

        public void setData(LookUpEdit lookUpEdit1, DataTable dt, int indexValue, int indexDisplay)
        {
            setData(lookUpEdit1,dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }
        public void setData(LookUpEdit lookUpEdit1, DataTable dt, string value, string display)
        {
            clearData(lookUpEdit1);
            if (dt == null || dt.Rows.Count == 0) return;

            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.PopulateColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.Columns[0].Caption = "ID";
            lookUpEdit1.Properties.ValueMember = value;
            lookUpEdit1.Properties.DisplayMember = display;
            lookUpEdit1.Properties.DropDownRows = dt.Rows.Count < 15 ? dt.Rows.Count : 15;
            lookUpEdit1.Properties.ShowHeader = false;

            setColumnAll(lookUpEdit1,false);
            //lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.ValueMember].Visible = true;
            lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.DisplayMember].Visible = true;
            lookUpEdit1.Properties.ShowFooter = false;
        }

        public void clearData(LookUpEdit lookUpEdit1)
        {
            lookUpEdit1.Properties.DataSource = null;
        }

        public void setColumnAll(LookUpEdit lookUpEdit1,bool show)
        {
            for (int i = 0; i < lookUpEdit1.Properties.Columns.Count; i++)
            {
                lookUpEdit1.Properties.Columns[i].Visible = show;
            }
        }

        private void SetFormat(DateEdit dateEdit ,string format)
        {
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateEdit.Properties.Mask.EditMask = format;
            dateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (resultArrRF == null || resultArrRF.Length <= 0) return;

            simpleButton3.Enabled = true;
            String par_ar = "";
            par_ar = getParam(reportFieldLength);
            toExel(selectedReportCode, par_ar,"xls");
            this.Cursor = Cursors.Default;
            simpleButton3.Enabled = true;
        }

        private void toExel(String report_code, String par_ar,string type)
        {
            //Stream stream = AjaxJson.getReport(report_code, par_ar);
            string url = ServiceViewReport.getUrlReport(reportID, paramStrReportField, type);
            byte[] byteArray = ServiceViewReport.getByteArrFromUrl(url);
            if (byteArray.Length == 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Không có dữ liệu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveFileDialog1.Filter = "Exel file | *.xls";
            saveFileDialog1.Title = "Chọn thư mục lưu file exel ...";
            saveFileDialog1.FileName = Func.getSysDatetime("yyyyMMdd_HHmmss") + "_" + reportID;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                string tempFile = saveFileDialog1.FileName;
                File.WriteAllBytes(tempFile, byteArray);
                System.Diagnostics.Process.Start(tempFile);
            }

        }

        private void toExelX(String report_code, String par_ar, string type)
        {
            //Stream stream = AjaxJson.getReport(report_code, par_ar);
            string url = ServiceViewReport.getUrlReport(reportID, paramStrReportField, type);
            byte[] byteArray = ServiceViewReport.getByteArrFromUrl(url);
            if (byteArray.Length == 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Không có dữ liệu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveFileDialog1.Filter = "Exel file | *.xlsx";
            saveFileDialog1.Title = "Chọn thư mục lưu file exel ...";
            saveFileDialog1.FileName = Func.getSysDatetime("yyyyMMdd_HHmmss") + "_" + reportID;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                string tempFile = saveFileDialog1.FileName;
                File.WriteAllBytes(tempFile, byteArray);
                System.Diagnostics.Process.Start(tempFile);
            }

        }

        private void toRTF(String report_code, String par_ar, string type)
        {
            //Stream stream = AjaxJson.getReport(report_code, par_ar);
            string url = ServiceViewReport.getUrlReport(reportID, paramStrReportField, type);
            byte[] byteArray = ServiceViewReport.getByteArrFromUrl(url);
            if (byteArray.Length == 0)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Không có dữ liệu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveFileDialog1.Filter = "RTF file | *.rtf";
            saveFileDialog1.Title = "Chọn thư mục lưu file RTF ...";
            saveFileDialog1.FileName = Func.getSysDatetime("yyyyMMdd_HHmmss") + "_" + reportID;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                string tempFile = saveFileDialog1.FileName;
                File.WriteAllBytes(tempFile, byteArray);
                System.Diagnostics.Process.Start(tempFile);
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (resultArrRF == null || resultArrRF.Length <= 0) return;
            //rtf
            simpleButton4.Enabled = true;
            String par_ar = "";
            par_ar = getParam(reportFieldLength);
            toRTF(selectedReportCode, par_ar, "rtf");
            this.Cursor = Cursors.Default;
            simpleButton4.Enabled = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (resultArrRF == null || resultArrRF.Length <= 0) return;
            //Xlsx
            simpleButton5.Enabled = true;
            String par_ar = "";
            par_ar = getParam(reportFieldLength);
            toExelX(selectedReportCode, par_ar, "xlsx");
            this.Cursor = Cursors.Default;
            simpleButton5.Enabled = true;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < reportFieldLength; i++)
            {
                string fieldType = resultArrRF[i][5];
                string paramName = resultArrRF[i][9];
                if ((fieldType.Contains("adDate")))
                {
                    DateEdit dateEdit = this.Controls.Find("dateEdit" + i, true).FirstOrDefault() as DateEdit;
                    //value = dateEdit.DateTime.ToString();
                    dateEdit.EditValue = ""; 
                }
                if ((fieldType.Contains("adDBTimeStamp")))
                {
                    DateEdit dateEdit = this.Controls.Find("dateEdit" + i, true).FirstOrDefault() as DateEdit;
                    //value = dateEdit.DateTime.ToString();
                    dateEdit.EditValue = "";
                }
                if ((fieldType.Contains("adNumeric")))
                {
                    ComboBoxEdit cbxEdit = this.Controls.Find("lkpEdit" + i, true).FirstOrDefault() as ComboBoxEdit;
                    //value = dateEdit.DateTime.ToString();
                    //cbxEdit.SelectedIndex = 0; 
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThuGon_Click(object sender, EventArgs e)
        {
            if (layoutControlItem_Button.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                btnThuGon.Text = "Mở rộng";
                layoutControlItem_Button.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_TimKiem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                btnThuGon.Text = "Thu gọn";
                layoutControlItem_Button.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem_TimKiem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }


    }
}