using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.Common
{
    class JSONObject
    {

        private Dictionary<string, object> myHashMap;
        private ArrayList myKeys;

        private sealed class Null
        {
            public Null(Null x0)
                : this()
            {
            }

            protected Object clone()
            {
                return this;
            }

            public bool equals(Object _object)
            {
                return (_object == null) || (_object == this);
            }

            public String toString()
            {
                return "null";
            }

            private Null() { }
        }

        public static readonly Object NULL = new Null(null);

        public JSONObject()
        {
            //System.out.println("new JSONObject");
            this.myHashMap = new Dictionary<string, object>();
            this.myKeys = new ArrayList();
        }
        public JSONObject(JSONTokener x)
            : this() //throws ParseException
        {
            char c;
            String key;

            if (x.nextClean() != '{')
            {
                throw new Exception("A JSONObject text must begin with '{'");
            }
            for (; ; )
            {
                c = x.nextClean();
                switch (c)
                {
                    case '\0':
                        throw new Exception("A JSONObject text must end with '}'");
                    case '}':
                        return;
                    default:
                        x.back();
                        key = x.nextValue().ToString();
                        break;
                }

                //The key is followed by ':'.

                c = x.nextClean();
                if (c != ':')
                {
                    throw new Exception("Expected a ':' after a key");
                }
                this.myKeys.Add(key);
                this.myHashMap.Add(key, x.nextValue());

                //Pairs are separated by ','.

                switch (x.nextClean())
                {
                    case ';':
                    case ',':
                        if (x.nextClean() == '}')
                        {
                            return;
                        }
                        x.back();
                        break;
                    case '}':
                        return;
                    default:
                        throw new Exception("Expected a ',' or '}'");
                }
            }
        }
        /*
        public JSONObject(JSONTokener x)
          throws ParseException
        {
          this();
          if (x.next() == '%') {
            x.unescape();
          }
          x.back();
          if (x.nextClean() != '{') {
            throw new Exception("A JSONObject must begin with '{'");
          }
          for (;;)
          {
            char c = x.nextClean();
            switch (c)
            {
            case '\000': 
              throw new Exception("A JSONObject must end with '}'");
            case '}': 
              return;
            }
            x.back();
            String key = x.nextValue().toString();
            if (x.nextClean() != ':') {
              throw new Exception("Expected a ':' after a key");
            }
            this.myKeys.add(key);
            this.myHashMap.put(key, x.nextValue());
            switch (x.nextClean())
            {
            case ',': 
              if (x.nextClean() == '}') {
                return;
              }
              x.back();
            }
          }
        }
        */
        public JSONObject(String _string)
            : this(new JSONTokener(_string))
        //throws ParseException
        {

        }

        public JSONObject(Dictionary<string, object> map)
        {
            this.myHashMap = new Dictionary<string, object>(map);
        }

        public JSONObject accumulate(String key, Object value)
        //throws NullPointerException
        {
            Object o = opt(key);
            if (o == null)
            {
                put(key, value);
            }
            else if ((o is JSONArray))
            {
                JSONArray a = (JSONArray)o;
                a.put(value);
            }
            else
            {
                JSONArray a = new JSONArray();
                a.put(o);
                a.put(value);
                put(key, a);
            }
            return this;
        }

        public Object get(String key)
        //throws NoSuchElementException
        {
            Object o = opt(key);
            if (o == null)
            {
                return "";
                //throw new Exception("JSONObject[" + quote(key) + "] not found.");
            }
            return o;
        }

        public bool getBoolean(String key)
        //throws ClassCastException, NoSuchElementException
        {
            Object o = get(key);
            return Number.getBoolean(o);
        }

        public double getDouble(String key)
        //throws NoSuchElementException, NumberFormatException
        {
            Object o = get(key);
            return Number.getDouble(o);
        }

        Dictionary<string, object> getHashMap()
        {
            return this.myHashMap;
        }

        public int getInt(String key)
        //throws NoSuchElementException, NumberFormatException
        {
            Object o = get(key);
            return Number.getInt(o);
        }

        public long getLong(String key)
        //throws NoSuchElementException, NumberFormatException
        {
            Object o = get(key);
            return Number.getLong(o);
        }

        public JSONArray getJSONArray(String key)
        //throws NoSuchElementException
        {
            Object o = get(key);
            if ((o is JSONArray))
            {
                return (JSONArray)o;
            }
            throw new Exception("JSONObject[" + quote(key) + "] is not a JSONArray.");
        }

        public JSONObject getJSONObject(String key)
        //throws NoSuchElementException
        {
            Object o = get(key);
            if ((o is JSONObject))
            {
                return (JSONObject)o;
            }
            throw new Exception("JSONObject[" + quote(key) + "] is not a JSONObject.");
        }

        public String getString(String key)
        {
            //throws NoSuchElementException

            object ret = get(key);
            return ret != JSONObject.NULL ? ret.ToString() : "";
        }

        public bool has(String key)
        {
            return this.myHashMap.ContainsKey(key);
        }

        public bool isNull(String key)
        {
            return NULL.Equals(opt(key));
        }
        public Dictionary<string, object>.KeyCollection.Enumerator keys()
        {
            return this.myHashMap.Keys.GetEnumerator();
        }
        public int length()
        {
            return this.myHashMap.Count;
        }

        public JSONArray names()
        {
            JSONArray ja = new JSONArray();
            Dictionary<string, object>.KeyCollection.Enumerator _keys = keys();
            while (_keys.MoveNext())
            {
                ja.put(_keys.Current);
            }
            if (ja.length() == 0)
            {
                return null;
            }
            return ja;
        }
        /*
    public static String numberToString(Number n)
    //throws ArithmeticException
    {
        if ((((n is float)) && ((((float)n).isInfinite()) || (((Float)n).isNaN()))) || (((n is Double)) && ((((Double)n).isInfinite()) || (((Double)n).isNaN())))) {
            throw new ArithmeticException("JSON can only serialize finite numbers.");
        }
        String s = n.toString().toLowerCase();
        if ((s.indexOf('e') < 0) && (s.indexOf('.') > 0))
        {
            while (s.endsWith("0"))
            {
                s = s.substring(0, s.length() - 1);
            }
            if (s.endsWith("."))
            {
                s = s.substring(0, s.length() - 1);
            }
        }
        return s;
    }
    */
        public Object opt(String key)
        //throws NullPointerException
        {
            if (key == null)
            {
                throw new Exception("Null key");
            }
            return this.myHashMap[key];
        }

        public bool optBoolean(String key)
        {
            return optBoolean(key, false);
        }

        public bool optBoolean(String key, bool defaultValue)
        {
            Object o = opt(key);
            if (o != null)
            {
                if ((o.Equals(Boolean.Parse("false"))) || (o.Equals("false")))
                {
                    return false;
                }
                if ((o.Equals(Boolean.Parse("true"))) || (o.Equals("true")))
                {
                    return true;
                }
            }

            return defaultValue;
        }

        public double optDouble(String key)
        {
            return optDouble(key, (0.0D / 0.0D));
        }

        public double optDouble(String key, double defaultValue)
        {
            Object o = opt(key);
            if (o != null)
            {
                return Number.getDouble(o);
            }
            return defaultValue;
        }

        public int optInt(String key)
        {
            return optInt(key, 0);
        }

        public int optInt(String key, int defaultValue)
        {
            Object o = opt(key);
            if (o != null)
            {
                return Number.getInt(o);
            }
            return defaultValue;
        }

        public JSONArray optJSONArray(String key)
        {
            Object o = opt(key);
            if ((o is JSONArray))
            {
                return (JSONArray)o;
            }
            return null;
        }

        public JSONObject optJSONObject(String key)
        {
            Object o = opt(key);
            if ((o is JSONObject))
            {
                return (JSONObject)o;
            }
            return null;
        }

        public String optString(String key)
        {
            return optString(key, "");
        }

        public String optString(String key, String defaultValue)
        {
            Object o = opt(key);
            if (o != null)
            {
                return o.ToString();
            }
            return defaultValue;
        }
        /*
            public JSONObject put(String key, bool value)
          {
                put(key, new Boolean(value));
                return this;
            }

            public JSONObject put(String key, double value)
          {
                put(key, new Double(value));
                return this;
            }

            public JSONObject put(String key, int value)
          {
                put(key, new Integer(value));
                return this;
            }
        */
        public JSONObject put(String key, Object value)
        //throws NullPointerException
        {
            if (key == null)
            {
                throw new Exception("Null key.");
            }
            if (value != null)
            {
                this.myKeys.Add(key);
                this.myHashMap.Add(key, value);
            }
            else
            {
                remove(key);
            }
            return this;
        }

        public JSONObject putOpt(String key, Object value)
        //throws NullPointerException
        {
            if (value != null)
            {
                put(key, value);
            }
            return this;
        }

        public static String quote(String _string)
        {
            if ((_string == null) || (_string.Length == 0))
            {
                return "\"\"";
            }
            int len = _string.Length;
            StringBuilder sb = new StringBuilder(len + 4);


            sb.Append('"');
            for (int i = 0; i < len; i++)
            {
                char c = _string[i];
                switch (c)
                {
                    case '"':
                    case '/':
                    case '\\':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        //if ((c < ' ') || (c >= ''))
                        //{
                        //        //String t = "000" + Integer.toHexString(c);
                        //        String t = "000" + Convert.ToString((int)c);
                        //        sb.Append("\\u" + t.Substring(t.Length - 4));
                        //}
                        //else
                        //{
                        sb.Append(c);
                        //}
                        break;
                }
            }
            sb.Append('"');
            return sb.ToString();
        }

        public Object remove(String key)
        {
            this.myKeys.Remove(key);
            return this.myHashMap.Remove(key);
        }

        public JSONArray toJSONArray(JSONArray names)
        {
            if ((names == null) || (names.length() == 0))
            {
                return null;
            }
            JSONArray ja = new JSONArray();
            for (int i = 0; i < names.length(); i++)
            {
                ja.put(opt(names.getString(i)));
            }
            return ja;
        }

        public String toString()
        {
            return toString(0, 0);
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
            sb.Append("{\n");

            int ii = 0;
            while (ii < this.myKeys.Count)
            {
                String s = (String)this.myKeys[ii++];
                //Console.WriteLine("myKeys[" + ii + "]=" + s);
                Object o = this.myHashMap[s];
                if (o != null)
                {
                    if (sb.Length > 2)
                    {
                        sb.Append(",\n");
                    }
                    sb.Append(pad);
                    sb.Append(quote(s));
                    sb.Append(": ");
                    if ((o is String))
                    {
                        sb.Append(quote((String)o));
                    }
                    else if ((Number.isNumber(o)))
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
            }
            sb.Append('}');
            //Console.WriteLine("sb.toString()=\n" + sb.ToString());
            return sb.ToString();
        }

    }
    public class Number
    {
        private Number() { }
        public static bool isNumber(object value)
        {
            bool rt = false;
            rt = (value is sbyte
        || value is byte
        || value is short
        || value is ushort
        || value is int
        || value is uint
        || value is long
        || value is ulong
        || value is float
        || value is double
        || value is decimal);
            return rt;
        }

        public static bool getBoolean(Object o)
        {
            if ((o is bool))
            {
                return ((bool)o);
            }
            if ((o is String))
            {
                return bool.Parse((String)o);
            }
            throw new Exception("Object is not a Boolean.");
        }
        public static double getDouble(Object o)
        {
            if (isNumber(o))
            {
                return ((double)o);
            }
            if ((o is String))
            {
                return Double.Parse((String)o);
            }
            throw new Exception("Object is not a Number.");
        }
        public static int getInt(Object o)
        {
            if (isNumber(o))
            {
                return ((int)o);
            }
            if ((o is String))
            {
                return int.Parse((String)o);
            }
            throw new Exception("Object is not a Number.");
        }
        public static long getLong(Object o)
        {
            if (isNumber(o))
            {
                return ((long)o);
            }
            if ((o is String))
            {
                return long.Parse((String)o);
            }
            throw new Exception("Object is not a Number.");
        }
    }
}
