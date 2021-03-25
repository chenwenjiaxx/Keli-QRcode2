using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DriverParameterReSet
{
    class StatusTool
    {
        public static Dictionary<int, int> RunState1(string str)
        {
            Dictionary<int, int> returnMap = new Dictionary<int, int>();

            string tmp = Convert16To2(str);
            for (int i = tmp.Length; i > 0; i--)
            {
                if (i == 12)
                {
                    int j = Convert.ToInt32(tmp.Substring(i - 2, 2), 2);
                    returnMap.Add(tmp.Length - i, j);
                    i--;//此位置地址是两位
                    returnMap.Add(tmp.Length - i, j);
                }
                else
                {
                    returnMap.Add(tmp.Length - i, Convert.ToInt32(tmp.Substring(i - 1, 1), 2));
                }

            }

            return returnMap;//每个bit位的值
        }

        //寄存器解析
        public static Dictionary<int, int> StorageResolver(string str)
        {
            Dictionary<int, int> returnMap = new Dictionary<int, int>();

            string tmp = Convert16To2(str);
            for (int i = tmp.Length; i > 0; i--)
            {
                returnMap.Add(tmp.Length - i, Convert.ToInt32(tmp.Substring(i - 1, 1), 2));
            }

            return returnMap;//每个bit位的值
        }

        //16进制转2进制
        public static string Convert16To2(string str)
        {
            string finStr = "";
            string str16 = Regex.Replace(str, @"\s", "").ToUpper().Replace("0X", "");
            foreach (char c in str16)
            {
                int i = int.Parse(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2));
                finStr += string.Format("{0:d4}", i);
            }

            return finStr;
        }

        //2进制转16进制
        public static string Convert2To16(string str)
        {
            string finStr = "";

            string str16 = Regex.Replace(str, @"\s", "");
            for (; str16.Length % 4 != 0;)
            {
                str16 = "0" + str16;
            }

            for (int i = 0; i < str16.Length; i += 4)
            {
                string tmp = str16.Substring(i, 4);
                finStr += Convert.ToString(Convert.ToInt32(tmp.ToString(), 2), 16);
            }

            return finStr.ToUpper();
        }

        //接收数据装换字符串
        public static string ArrayToString(ArrayList array)
        {
            return string.Join(" ", (string[])array.ToArray(typeof(string)));
        }
    }
}
