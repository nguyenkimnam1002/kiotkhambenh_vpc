using System;
using System.Text;

using System.Globalization;
using System.Data;
using System.Windows.Forms;

namespace VNPT.HIS.BaoCao.Class
{
    public static class DynamicReport
    {
        /*        
                public static void openReport(int userGroupId)
                {
                    frmViewReport frmReport = new frmViewReport();
                    frmReport.showMode = 0;
                    frmReport.userGroupId = userGroupId;
                    frmReport.Show();
                }
                public static void openReport(String reportCode)
                {
                    frmViewReport frmReport = new frmViewReport();
                    frmReport.showMode = 1;
                    frmReport.reportCode = reportCode;
                    frmReport.Show();
                }
                public static void openReport(String reportCode, String param)
                {
                    frmViewReport frmReport = new frmViewReport();
                    frmReport.showMode = 2;
                    frmReport.reportCode = reportCode;
                    frmReport.reportParam = param;
                    frmReport.Show();
                }
            */
    }
    public static class ReportUtil
    {

        //------------------------------//
        //		HANG SO SO SANH			//
        public static string QUERY_STRING_FIELD_NAME = "queryString";
        public static int OPERATION_UNDEFINED = 0;
        public static int OPERATION_GREATER_OR_EQUAL = 1;
        public static int OPERATION_SMALLER_OR_EQUAL = 2;
        public static int OPERATION_EQUAL = 3;
        public static int OPERATION_BETWEEN = 4;
        public static int OPERATION_IN = 5;
        public static int OPERATION_LIKE = 6;
        //public static int OPERATION_UNDEFINED = 7;

        //------------------------------//
        //		KIEU DU LIEU			//
        public static int PARAM_TYPE_TEXT = 1;
        public static int PARAM_TYPE_NUMERIC = 2;
        public static int PARAM_TYPE_DATE = 3;
        public static int PARAM_TYPE_UNDEFINED = 0;
        public static int PARAM_TYPE_DATE_NO_TIME = 5;
        public static int PARAM_TYPE_DATE_MONTH = 6;

        //------------------------------//
        //		KIEU DU LIEU			//
        public static int CONTROL_TYPE_UNDEFINED = 0;

        public static int CONTROL_TYPE_SINGLE_SELECT = 1;
        public static int CONTROL_TYPE_MULTI_SELECT = 2;

        public static int CONTROLTYPE_SINGLE_TEXT = 3;
        public static int CONTROLTYPE_DOUBLE_TEXT = 4;
        public static int CONTROLTYPE_MULTI_TEXT = 5;

        public static int CONTROLTYPE_SINGLE_NUMERIC = 6;
        public static int CONTROLTYPE_DOUBLE_NUMERIC = 7;
        public static int CONTROLTYPE_MULTI_NUMERIC = 8;

        public static int CONTROLTYPE_SINGLE_DATEPICKER = 9;

        public static int CONTROLTYPE_SINGLE_DATEPICKER_NO_TIME = 11;
        public static int CONTROLTYPE_SINGLE_DATEPICKER_MONTH = 12;
        public static int CONTROLTYPE_DOUBLE_DATEPICKER = 10;
        public static int CONTROLTYPE_DOUBLE_DATEPICKER_NO_TIME = 13;
        public static int CONTROLTYPE_DOUBLE_DATEPICKER_MONTH = 14;

        /* Ham lay kieu dieu kien so sanh
         * param : operation - Kieu dk truyen vao
         * return: Hang so cua tung loai dieu kien so sanh 
        */
        public static int getOperation(string operation)
        {
            switch (operation)
            {
                case ">=":
                    return OPERATION_GREATER_OR_EQUAL;
                    break;
                case "<=":
                    return OPERATION_SMALLER_OR_EQUAL;
                    break;
                case "=":
                    return OPERATION_EQUAL;
                    break;
                case "B":
                    return OPERATION_BETWEEN;
                    break;
                case "IN":
                    return OPERATION_IN;
                    break;
                case "Like":
                    return OPERATION_LIKE;
                    break;
                default:
                    return OPERATION_UNDEFINED;
            }
        }

        /* Ham lay kien du lieu cua tham so
	 * param : paramFieldType - Kieu dk truyen vao
	 * return: Hang so tuong ung
	 */
        public static int getParamType(string paramFieldType)
        {
            int rReturn = 0;
            if ((paramFieldType == "adChar") || (paramFieldType == "adVarChar") ||
                    (paramFieldType == "adVarWChar") || (paramFieldType == "adChar"))
            {
                rReturn = PARAM_TYPE_TEXT;
            }
            if ((paramFieldType == "adDouble") || (paramFieldType == "adNumeric") ||
                        (paramFieldType == "adVarNumeric"))
            {
                rReturn = PARAM_TYPE_NUMERIC;
            }
            if (paramFieldType == "adDBTimeStamp")
            {
                rReturn = PARAM_TYPE_DATE;
            }
            if (paramFieldType == "adDate")
            {
                rReturn = PARAM_TYPE_DATE_NO_TIME;
            }
            if (paramFieldType == "adMonth")
            {
                rReturn = PARAM_TYPE_DATE_MONTH;
            }
            return rReturn;
        }

        /* Ham tra ve kieu du lieu ngay thang
         * param : vDate - du lieu kieu DATE
         * return: ret_date
         */
        public static DataTable getDatabaseServerDate(string db_name)
        {
            //"dd/MM/yyyy HH:mm:ss"
          //  string sSQL = "SELECT TO_DATE((TO_CHAR(SYSDATE,'mm/dd/yyyy')||' 00:00:01'),'mm/dd/yyyy hh24:mi:ss') FROM dual";
            DataTable ret_date = null;// AjaxJson.executeQueryByIdT(new String[] { sSQL });
            return ret_date;
        }

        /* Ham thiet lap kieu hien thi: getDisplayedStrSingleObject()
         * param :  controlType - Kieu dieu khien
         * 			operation 	- Kieu so sanh
         * 			singleObject- 
         */
        public static string getDisplayedStrSingleObject(int controlType, string operation, string singleObject)
        {
            var returnedValue = "Wrong function!";
            switch (controlType)
            {
                case 10://CONTROLTYPE_DOUBLE_DATEPICKER:
                    break;
                case 7://CONTROLTYPE_DOUBLE_NUMERIC:
                case 4://CONTROLTYPE_DOUBLE_TEXT:
                    break;
                case 8://CONTROLTYPE_MULTI_NUMERIC:
                case 5://CONTROLTYPE_MULTI_TEXT:
                    break;
                case 2://CONTROL_TYPE_MULTI_SELECT:
                case 1://CONTROL_TYPE_SINGLE_SELECT:
                    break;
                case 9://CONTROLTYPE_SINGLE_DATEPICKER:
                    switch (operation)
                    {
                        case "1"://OPERATION_GREATER_OR_EQUAL:
                            returnedValue = "sau ngày " + getViewDate(VNPT.HIS.Common.Func.ParseDate(singleObject));
                            break;
                        case "2"://OPERATION_SMALLER_OR_EQUAL:
                            returnedValue = "trước ngày " + getViewDate(VNPT.HIS.Common.Func.ParseDate(singleObject));
                            break;
                        case "3"://OPERATION_EQUAL:
                            returnedValue = getViewDate(VNPT.HIS.Common.Func.ParseDate(singleObject));
                            break;
                    }
                    break;
                case 6://CONTROLTYPE_SINGLE_NUMERIC:
                case 3://CONTROLTYPE_SINGLE_TEXT:
                    switch (operation)
                    {
                        case "1"://OPERATION_GREATER_OR_EQUAL:
                            returnedValue = "lớn hơn " + singleObject;
                            break;
                        case "2"://OPERATION_SMALLER_OR_EQUAL:
                            returnedValue = "nhỏ hơn " + singleObject;
                            break;
                        case "3"://OPERATION_EQUAL:
                            returnedValue = singleObject;
                            break;
                        case "6"://OPERATION_LIKE:
                            returnedValue = "có đúng " + singleObject;
                            break;
                    }
                    break;
            }
            return returnedValue;
        }

        /* Ham thiet lap kieu hien thi: getDisplayedStrDoubleObject()
         * param :  controlType - Kieu dieu khien
         * 			operation 	- Kieu so sanh
         * 			multiObjects- Hien thi cho nhieu object
         */
        public static string getDisplayedStrDoubleObject(int controlType, string operation, String[] multiObjects)
        {
            var returnedValue = "Wrong function!";
            switch (controlType)
            {
                case 10://CONTROLTYPE_DOUBLE_DATEPICKER:
                    DateTime beginDate = VNPT.HIS.Common.Func.ParseDate(multiObjects[0]);
                    DateTime endDate = VNPT.HIS.Common.Func.ParseDate(multiObjects[1]);
                    returnedValue = "Từ ngày " + getViewDate(beginDate) + " Tới ngày " + getViewDate(endDate);
                    break;
                case 7://CONTROLTYPE_DOUBLE_NUMERIC:
                case 4://CONTROLTYPE_DOUBLE_TEXT:
                    returnedValue = "nằm giữa " + multiObjects[0] + " và "
                            + multiObjects[1];
                    break;
                case 8://CONTROLTYPE_MULTI_NUMERIC:
                case 5://CONTROLTYPE_MULTI_TEXT:
                case 2://CONTROL_TYPE_MULTI_SELECT:
                    returnedValue = "";
                    StringBuilder sb = new StringBuilder();
                    int i;
                    for (i = 0; i < multiObjects.Length; i++)
                    {
                        sb.Append(multiObjects[i].ToString()).Append(",");
                    }
                    //sb = sb.Substring(0, sb.Length - 1);
                    returnedValue += sb.ToString().Substring(0, sb.Length - 1);
                    break;
                case 1://CONTROL_TYPE_SINGLE_SELECT:
                    returnedValue = multiObjects[0];
                    break;
                case 9://CONTROLTYPE_SINGLE_DATEPICKER:
                    break;
                case 6://CONTROLTYPE_SINGLE_NUMERIC:
                    break;
                case 3://CONTROLTYPE_SINGLE_TEXT:
                    break;
            }
            return returnedValue;
        }

        /* Ham tra ve kieu dieu khien: getControlType()
         * param :  operation	- Toan tu
         * 			fieldType 	- Kieu truong du lieu
         * 			lookup		- Tim kiem
         */
        public static int getControlType(string operation, string fieldType, int lookup)
        {
            if (lookup == 1)
            {
                switch (getOperation(operation))
                {
                    case 3://OPERATION_EQUAL:
                        return CONTROL_TYPE_SINGLE_SELECT;
                    case 5://OPERATION_IN:
                        return CONTROL_TYPE_MULTI_SELECT;
                    default:
                        return -1;
                }
            }

            switch (getParamType(fieldType))
            {
                case 6://date month:
                    switch (getOperation(operation))
                    {
                        case 3://OPERATION_EQUAL:
                        case 1://OPERATION_GREATER_OR_EQUAL:
                        case 2://OPERATION_SMALLER_OR_EQUAL:
                            return CONTROLTYPE_SINGLE_DATEPICKER_MONTH;
                        case 4://OPERATION_BETWEEN:
                            return CONTROLTYPE_DOUBLE_DATEPICKER_MONTH;
                        case 5:
                            return CONTROLTYPE_SINGLE_DATEPICKER_MONTH;
                        default:
                            return -1;
                    }
                case 5://date no time:
                    switch (getOperation(operation))
                    {
                        case 3://OPERATION_EQUAL:
                        case 1://OPERATION_GREATER_OR_EQUAL:
                        case 2://OPERATION_SMALLER_OR_EQUAL:
                            return CONTROLTYPE_SINGLE_DATEPICKER_NO_TIME;
                        case 4://OPERATION_BETWEEN:
                            return CONTROLTYPE_DOUBLE_DATEPICKER_NO_TIME;
                        case 5:
                            return CONTROLTYPE_SINGLE_DATEPICKER_NO_TIME;
                        default:
                            return -1;
                    }
                case 3://PARAM_TYPE_DATE:
                    switch (getOperation(operation))
                    {
                        case 3://OPERATION_EQUAL:
                        case 1://OPERATION_GREATER_OR_EQUAL:
                        case 2://OPERATION_SMALLER_OR_EQUAL:
                            return CONTROLTYPE_SINGLE_DATEPICKER;
                        case 4://OPERATION_BETWEEN:
                            return CONTROLTYPE_DOUBLE_DATEPICKER;
                        case 5:
                            return CONTROLTYPE_SINGLE_DATEPICKER;
                        default:
                            return -1;
                    }
                case 2://PARAM_TYPE_NUMERIC:
                    switch (getOperation(operation))
                    {
                        case 3://OPERATION_EQUAL:
                        case 1://OPERATION_GREATER_OR_EQUAL:
                        case 2://OPERATION_SMALLER_OR_EQUAL:
                            return CONTROLTYPE_SINGLE_NUMERIC;
                        case 4://OPERATION_BETWEEN:
                            return CONTROLTYPE_DOUBLE_NUMERIC;
                        case 5://OPERATION_IN:
                            return CONTROLTYPE_MULTI_NUMERIC;
                        default:
                            return -1;
                    }
                case 1://PARAM_TYPE_TEXT:
                    switch (getOperation(operation))
                    {
                        case 3://OPERATION_EQUAL:
                        case 1://OPERATION_GREATER_OR_EQUAL:
                        case 2://OPERATION_SMALLER_OR_EQUAL:
                        case 6://OPERATION_LIKE:
                            return CONTROLTYPE_SINGLE_TEXT;
                        case 4://OPERATION_BETWEEN:
                            return CONTROLTYPE_DOUBLE_TEXT;
                        case 5://OPERATION_IN:
                            return CONTROLTYPE_MULTI_TEXT;
                        default:
                            return -1;
                    }
                default:
                    return -1;
            }
        }

        public static string getViewDate(DateTime dt)
        {
            return dt.ToString("dd'/'MM'/'yyyy");
        }

        public static Control getControlByName(TableLayoutPanel panel, String ctlName, String opt)
        {
            Control _clt = null;
            foreach (Control ctl in panel.Controls)
            {
                String _prefix = "";
                if (ctl is TextBox)
                {
                    _prefix = "txt" + opt;
                }
                else if (ctl is DateTimePicker)
                {
                    _prefix = "dtp" + opt;
                }
                else if (ctl is ComboBox)
                {
                    _prefix = "cbo" + opt;
                }
                else if (ctl is CheckedListBox)
                {
                    _prefix = "lst" + opt;
                }
                if (ctl.Name == _prefix + ctlName)
                {
                    _clt = ctl;
                    break;
                }
            }
            return _clt;
        }
        public static Object getControlValue(Control ctl)
        {
            Object value = null;
            if (ctl != null)
            {
                if (ctl is TextBox)
                {
                    value = (ctl as TextBox).Text;
                }
                else if (ctl is DateTimePicker)
                {
                    value = (ctl as DateTimePicker).Text;
                }
                else if (ctl is ComboBox)
                {
                    value = (ctl as ComboBox).SelectedValue;
                }
                else if (ctl is CheckedListBox)
                {
                    value = (ctl as CheckedListBox).SelectedValue;
                }
            }
            return value;
        }
    }
}
