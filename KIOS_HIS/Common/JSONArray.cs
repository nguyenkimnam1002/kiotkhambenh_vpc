using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Collections; 
namespace VNPT.HIS.Common
{
    class JSONArray
    {

        private ArrayList myArrayList;

        public JSONArray()
        {
            this.myArrayList = new ArrayList();
        }

        public JSONArray(JSONTokener x)
            : this()  //throws ParseException
        {
            if (x.nextClean() != '[')
            {
                throw new Exception("A JSONArray text must start with '['");
            }
            if (x.nextClean() != ']')
            {
                x.back();
                for (; ; )
                {
                    if (x.nextClean() == ',')
                    {
                        x.back();
                        this.myArrayList.Add(JSONObject.NULL);
                    }
                    else
                    {
                        x.back();
                        this.myArrayList.Add(x.nextValue());
                    }
                    switch (x.nextClean())
                    {
                        case ',':
                            if (x.nextClean() == ']')
                            {
                                return;
                            }
                            x.back();
                            break;
                        case ']':
                            return;
                        default:
                            throw new Exception("Expected a ',' or ']'");
                    }
                }
            }
        }
        public JSONArray(String _string)
            : this(new JSONTokener(_string))
        //throws ParseException
        {

        }

        public JSONArray(ArrayList collection)
        {
            this.myArrayList = new ArrayList(collection);
        }

        public Object get(int index)
        //throws NoSuchElementException
        {
            Object o = opt(index);
            if (o == null)
            {
                throw new Exception("JSONArray[" + index + "] not found.");
            }
            return o;
        }

        ArrayList getArrayList()
        {
            return this.myArrayList;
        }

        public bool getBoolean(int index)
        //throws ClassCastException, NoSuchElementException
        {
            Object o = get(index);
            return Number.getBoolean(o);
        }

        public double getDouble(int index)
        //throws NoSuchElementException, NumberFormatException
        {
            Object o = get(index);
            return Number.getDouble(o);
        }

        public int getInt(int index)
        //throws NoSuchElementException, NumberFormatException
        {
            Object o = get(index);
            return Number.getInt(o);
        }

        public JSONArray getJSONArray(int index)
        //throws NoSuchElementException
        {
            Object o = get(index);
            if ((o is JSONArray))
            {
                return (JSONArray)o;
            }
            throw new Exception("JSONArray[" + index + "] is not a JSONArray.");
        }

        public JSONObject getJSONObject(int index)
        //throws NoSuchElementException
        {
            Object o = get(index);
            if ((o is JSONObject))
            {
                return (JSONObject)o;
            }
            throw new Exception("JSONArray[" + index + "] is not a JSONObject.");
        }

        public String getString(int index)
        //throws NoSuchElementException
        {
            return get(index).ToString();
        }

        public bool isNull(int index)
        {
            Object o = opt(index);
            return (o == null) || (o.Equals(null));
        }

        public String join(String separator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.myArrayList.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }
                Object o = this.myArrayList[i];
                if (o == null)
                {
                    sb.Append("null");
                }
                else if ((o is String))
                {
                    sb.Append(JSONObject.quote((String)o));
                }
                else if (Number.isNumber(o))
                {
                    sb.Append(Convert.ToString(o));
                }
                else
                {
                    sb.Append(o.ToString());
                }
            }
            return sb.ToString();
        }

        public int length()
        {
            return this.myArrayList.Count;
        }

        public Object opt(int index)
        {
            if ((index < 0) || (index >= length()))
            {
                return null;
            }
            return this.myArrayList[index];
        }

        public bool optBoolean(int index)
        {
            return optBoolean(index, false);
        }

        public bool optBoolean(int index, bool defaultValue)
        {
            Object o = opt(index);
            if (o != null)
            {
                return Number.getBoolean(o);
            }
            return defaultValue;
        }

        public double optDouble(int index)
        {
            return optDouble(index, (0.0D / 0.0D));
        }

        public double optDouble(int index, double defaultValue)
        {
            Object o = opt(index);
            if (o != null)
            {
                return Number.getDouble(o);
            }
            return defaultValue;
        }

        public int optInt(int index)
        {
            return optInt(index, 0);
        }

        public int optInt(int index, int defaultValue)
        {
            Object o = opt(index);
            if (o != null)
            {
                return Number.getInt(o);
            }
            return defaultValue;
        }

        public JSONArray optJSONArray(int index)
        {
            Object o = opt(index);
            if ((o is JSONArray))
            {
                return (JSONArray)o;
            }
            return null;
        }

        public JSONObject optJSONObject(int index)
        {
            Object o = opt(index);
            if ((o is JSONObject))
            {
                return (JSONObject)o;
            }
            return null;
        }

        public String optString(int index)
        {
            return optString(index, "");
        }

        public String optString(int index, String defaultValue)
        {
            Object o = opt(index);
            if (o != null)
            {
                return o.ToString();
            }
            return defaultValue;
        }
        /*
        public JSONArray put(bool value)
        {
            put(new Boolean(value));
            return this;
        }

        public JSONArray put(double value)
        {
            put(new Double(value));
            return this;
        }

        public JSONArray put(int value)
        {
            put(new Integer(value));
            return this;
        }
        */
        public JSONArray put(Object value)
        {
            this.myArrayList.Add(value);
            return this;
        }
        /*
        public JSONArray put(int index, bool value)
        {
            put(index, new Boolean(value));
            return this;
        }

        public JSONArray put(int index, double value)
        {
            put(index, new Double(value));
            return this;
        }

        public JSONArray put(int index, int value)
        {
            put(index, new Integer(value));
            return this;
        }
        */
        public JSONArray put(int index, Object value)
        //throws NoSuchElementException, NullPointerException
        {
            if (index < 0)
            {
                throw new Exception("JSONArray[" + index + "] not found.");
            }
            if (value == null)
            {
                throw new Exception("NullPointerException");
            }
            if (index < length())
            {
                this.myArrayList[index] = value;
            }
            else
            {
                while (index != length())
                {
                    put(null);
                }
                put(value);
            }
            return this;
        }

        public JSONObject toJSONObject(JSONArray names)
        {
            if ((names == null) || (names.length() == 0) || (length() == 0))
            {
                return null;
            }
            JSONObject jo = new JSONObject();
            for (int i = 0; i < names.length(); i++)
            {
                jo.put(names.getString(i), opt(i));
            }
            return jo;
        }

        public String toString()
        {
            return '[' + join(",") + ']';
        }

        public String toString(int indentFactor)
        {
            return toString(indentFactor, 0);
        }

        public String toString(int indentFactor, int indent)
        {
            String pad = "";
            StringBuilder sb = new StringBuilder();
            indent += indentFactor;
            for (int i = 0; i < indent; i++)
            {
                pad = pad + ' ';
            }
            sb.Append("[\n");
            for (int i = 0; i < this.myArrayList.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(",\n");
                }
                sb.Append(pad);
                Object o = this.myArrayList[i];
                if (o == null)
                {
                    sb.Append("null");
                }
                else if ((o is String))
                {
                    sb.Append(JSONObject.quote((String)o));
                }
                else if (Number.isNumber(o))
                {
                    sb.Append(Convert.ToString(o));
                }
                else if ((o is JSONObject))
                {
                    sb.Append(((JSONObject)o).toString(indentFactor, indent));
                }
                else if ((o is JSONArray))
                {
                    sb.Append(((JSONArray)o).toString(indentFactor, indent));
                }
                else
                {
                    sb.Append(o.ToString());
                }
            }
            sb.Append(']');
            return sb.ToString();
        }

    }
}
