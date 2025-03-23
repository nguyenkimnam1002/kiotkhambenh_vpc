using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.Common
{
    class JSONTokener
    {

        private int myIndex;
        private String mySource;

        public JSONTokener(String s)
        {
            this.myIndex = 0;
            this.mySource = s;
        }

        public void back()
        {
            if (this.myIndex > 0)
            {
                this.myIndex -= 1;
            }
        }

        public static int dehexchar(char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return c - '0';
            }
            if ((c >= 'A') && (c <= 'F'))
            {
                return c + '\n' - 65;
            }
            if ((c >= 'a') && (c <= 'f'))
            {
                return c + '\n' - 97;
            }
            return -1;
        }

        public bool more()
        {
            return this.myIndex < this.mySource.Length;
        }

        public char next()
        {
            char c = more() ? this.mySource[this.myIndex] : '\0';
            this.myIndex += 1;
            return c;
        }

        public char next(char c) //throws ParseException
        {
            char n = next();
            if (n != c)
            {
                throw new System.Exception("Expected '" + c + "' and instead saw '" + n + "'.");
            }
            return n;
        }

        public String next(int n)
        //throws ParseException
        {
            int i = this.myIndex;
            int j = i + n;
            if (j >= this.mySource.Length)
            {
                throw new System.Exception("Substring bounds error");
            }
            this.myIndex += n;
            return this.mySource.Substring(i, j);
        }

        public char nextClean()
        //throws ParseException
        {
            char c;
            do
            {
                for (; ; )
                {
                    c = next();
                    if (c != '/')
                    {
                        break;
                    }

                    switch (next())
                    {
                        case '/':
                            do
                            {
                                c = next();
                                if ((c == '\n') || (c == '\r'))
                                {
                                    break;
                                }
                            } while (c != 0);
                            break;
                        case '*':
                            for (; ; )
                            {
                                c = next();
                                if (c == 0)
                                {
                                    throw new System.Exception("Unclosed comment.");
                                }
                                if (c == '*')
                                {
                                    if (next() == '/')
                                    {
                                        break;
                                    }
                                    back();
                                }
                            }
                            break;
                    }
                    back();
                    return '/';
                }
            } while ((c != 0) && (c <= ' '));
            return c;
        }

        public String nextString(char quote)
        //throws ParseException
        {
            StringBuilder sb = new StringBuilder();
            for (; ; )
            {
                char c = next();
                switch (c)
                {
                    case '\0':
                    case '\n':
                    case '\r':
                        throw new System.Exception("Unterminated string");
                    case '\\':
                        c = next();
                        switch (c)
                        {
                            case 'b':
                                sb.Append('\b');
                                break;
                            case 't':
                                sb.Append('\t');
                                break;
                            case 'n':
                                sb.Append('\n');
                                break;
                            case 'f':
                                sb.Append('\f');
                                break;
                            case 'r':
                                sb.Append('\r');
                                break;
                            case 'u':
                                sb.Append((char)Convert.ToInt32(next(4), 16));// Parse(next(4), 16));
                                break;
                            case 'x':
                                sb.Append((char)Convert.ToInt32(next(2), 16));
                                break;
                            case 'c':
                            case 'd':
                            case 'e':
                            case 'g':
                            case 'h':
                            case 'i':
                            case 'j':
                            case 'k':
                            case 'l':
                            case 'm':
                            case 'o':
                            case 'p':
                            case 'q':
                            case 's':
                            case 'v':
                            case 'w':
                            default:
                                sb.Append(c);
                                break;
                        }
                        break;
                    default:
                        if (c == quote)
                        {
                            return sb.ToString();
                        }
                        sb.Append(c);
                        break;
                }
            }
        }

        public String nextTo(char d)
        {
            StringBuilder sb = new StringBuilder();
            for (; ; )
            {
                char c = next();
                if ((c == d) || (c == 0) || (c == '\n') || (c == '\r'))
                {
                    if (c != 0)
                    {
                        back();
                    }
                    return sb.ToString().Trim();
                }
                sb.Append(c);
            }
        }

        public String nextTo(String delimiters)
        {
            StringBuilder sb = new StringBuilder();
            for (; ; )
            {
                char c = next();
                if ((delimiters.IndexOf(c) >= 0) || (c == 0) || (c == '\n') || (c == '\r'))
                {
                    if (c != 0)
                    {
                        back();
                    }
                    return sb.ToString().Trim();
                }
                sb.Append(c);
            }
        }

        public Object nextValue()
        //throws ParseException
        {
            char c = nextClean();
            if ((c == '"') || (c == '\''))
            {
                return nextString(c);
            }
            if (c == '{')
            {
                back();
                return new JSONObject(this);
            }
            if (c == '[')
            {
                back();
                return new JSONArray(this);
            }
            StringBuilder sb = new StringBuilder();
            char b = c;
            while ((c >= ' ') && (c != ':') && (c != ',') && (c != ']') && (c != '}') && (c != '/'))
            {
                sb.Append(c);
                c = next();
            }
            back();
            String s = sb.ToString().Trim();
            if (s.Equals("true"))
            {
                return true;
            }
            if (s.Equals("false"))
            {
                return false;
            }
            if (s.Equals("null"))
            {
                return JSONObject.NULL;
            }
            if (((b >= '0') && (b <= '9')) || (b == '.') || (b == '-') || (b == '+'))
            {
                try
                {
                    return Func.Parse(s);
                }
                catch (Exception e)
                {
                    try
                    {
                        return Double.Parse(s);
                    }
                    catch (Exception ex) { System.Console.WriteLine(ex.ToString()); }
                    System.Console.WriteLine(e.ToString());
                }
            }
            if (s.Equals(""))
            {
                throw new System.Exception("Missing value.");
            }
            return s;
        }

        public char skipTo(char to)
        {
            int index = this.myIndex;
            char c;
            do
            {
                c = next();
                if (c == 0)
                {
                    this.myIndex = index;
                    return c;
                }
            } while (c != to);
            back();
            return c;
        }

        public void skipPast(String to)
        {
            this.myIndex = this.mySource.IndexOf(to, this.myIndex);
            if (this.myIndex < 0)
            {
                this.myIndex = this.mySource.Length;
            }
            else
            {
                this.myIndex += to.Length;
            }
        }



        public String toString()
        {
            return " at character " + this.myIndex + " of " + this.mySource;
        }

        public void unescape()
        {
            this.mySource = unescape(this.mySource);
        }

        public static String unescape(String s)
        {
            int len = s.Length;
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                char c = s[i];
                if (c == '+')
                {
                    c = ' ';
                }
                else if ((c == '%') && (i + 2 < len))
                {
                    int d = dehexchar(s[i + 1]);
                    int e = dehexchar(s[i + 2]);
                    if ((d >= 0) && (e >= 0))
                    {
                        c = (char)(d * 16 + e);
                        i += 2;
                    }
                }
                b.Append(c);
            }
            return b.ToString();
        }

    }
}
