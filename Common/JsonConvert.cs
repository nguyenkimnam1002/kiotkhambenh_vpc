using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Data;

using System.Reflection;
using System.Configuration;

namespace VNPT.HIS.Common
{
    public static class MyJsonConvert
    {

        public static object DeserializeObject(String jsonObj, Type classType)
        {
            //classType.GetMember()
            //dynamic obj = JsonConvert.DeserializeObject(jsonObj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //int id = int.Parse(obj.id);
            if (classType.Name == "")
            {
                return toDataTable(jsonObj);
            }
            else
            {
                object objClass = Activator.CreateInstance(classType);
                JSONObject objJson = new JSONObject(jsonObj);
                //DataTable tbl = new DataTable();
                PropertyInfo[] pi_ar = classType.GetProperties();

                //MemberInfo[] mi_ar = classType.GetMembers();
                for (int i1 = 0; i1 < pi_ar.Length; i1++)
                {
                    pi_ar[i1].SetValue(objClass, objJson.getString(pi_ar[i1].Name), null);
                }
                return objClass;
            }
        }
        public static DataTable toDataTable(Object _jsonAr)
        {
            DataTable tbl = new DataTable();

            if (_jsonAr == null || _jsonAr == "") return tbl;

            string jsonAr = _jsonAr.ToString();

            JSONArray ja = new JSONArray(jsonAr);
            JSONObject obj;
            Dictionary<string, object>.KeyCollection.Enumerator _keys;
            if (ja.length() > 0)
            {
                obj = ja.getJSONObject(0);
                _keys = obj.keys();

                while (_keys.MoveNext())
                {
                    string name = (string)_keys.Current;
                    tbl.Columns.Add(name);
                }
            }
            for (int i1 = 0; i1 < ja.length(); i1++)
            {
                obj = ja.getJSONObject(i1);
                _keys = obj.keys();
                DataRow dr = tbl.NewRow();
                while (_keys.MoveNext())
                {
                    string name = (string)_keys.Current;

                    dr[name] = obj.getString(name);
                }

                tbl.Rows.Add(dr);

            }

            return tbl;
        }

        public static DataTable toDataTable2(String jsonAr)
        {
            DataTable tbl = new DataTable();
             
            JSONArray ja = new JSONArray(jsonAr);
            JSONObject obj;
            Dictionary<string, object>.KeyCollection.Enumerator _keys;
            if (ja.length() > 0)
            {
                obj = ja.getJSONObject(0);
                _keys = obj.keys();

                while (_keys.MoveNext())
                {
                    string name = (string)_keys.Current;
                    tbl.Columns.Add(name);
                }
            }
            for (int i1 = 0; i1 < ja.length(); i1++)
            {
                obj = ja.getJSONObject(i1);
                _keys = obj.keys();
                DataRow dr = tbl.NewRow();
                while (_keys.MoveNext())
                {
                    string name = (string)_keys.Current;

                    dr[name] = obj.getString(name);
                }

                tbl.Rows.Add(dr);

            }

            return tbl;
        }
    } // class
}
