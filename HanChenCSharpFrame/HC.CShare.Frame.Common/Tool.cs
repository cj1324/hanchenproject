using System;
using System.Collections.Generic;
using System.Text;

namespace HC.CShare.Frame.Common
{
    /// <summary>
    /// 工具类(生成随机数)
    /// </summary>
    public static class Tool
    {
        #region 生成随机数
        /// <summary>
        /// 生成随机数 (日期时间+3为随机数,纯数字) 
        /// </summary>
        /// <returns></returns>
        public static string DateRndNum()
        {
            int time=7;
            long tick = DateTime.Now.Ticks - time;
            Random ra = new Random((int)(tick - time & 0xffffffffL) | (int)(tick + time >> 32));
            DateTime d = DateTime.Now;
            string s = String.Empty, y, m, dd, h, mm, ss;
            y = d.Year.ToString();
            m = d.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            dd = d.Day.ToString();
            if (dd.Length < 2) dd = "0" + dd;
            h = d.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = d.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = d.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;
            s += y + m + dd + h + mm + ss;
            s += ra.Next(100, 999).ToString();
            return s;
        }


        /// <summary>
        /// 生成指定位数的随机数(数字+小写字母)
        /// </summary>
        /// <param name="pwdLength"></param>
        /// <returns></returns>
        public static string MakeRandCode(int pwdLength, int time)
        {
            string tmpstr = "";
            string pwdchars = "0123456789abcdefghijklmnopqrstuvwxyz"; //密码中包含的字符数组
            int iRandNum;                   //数组索引随机数
            Random rnd = new Random(unchecked((int)DateTime.Now.Ticks + time));      //随机数生成器(每循环一次时间就改变一次)
            for (int i = 0; i < pwdLength; i++)
            {
                iRandNum = rnd.Next(pwdchars.Length);   //Random类的Next方法生成一个指定范围的随机数
                tmpstr += pwdchars[iRandNum];          //tmpstr随机添加一个字符
            }
            return tmpstr;
        }





        #endregion

        #region  生成随机数  1-9A-Z 指定位数 指定个数的字符串列表(自行判断是否重复)

        public static Random rnd;

        /// <summary>
        /// 字符串表
        /// </summary>
        /// <returns></returns>
        private static char[] GenTable()
        {
            string str = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return str.ToCharArray();
        }

        /// <summary>
        /// 生成Int类型数组
        /// </summary>
        /// <param name="len">数组长度</param>
        /// <returns></returns>
        public static int[] RanKey(int len)
        {
            int[] arrInt = new int[len];
            int maxnum = GenTable().Length;
            for (int i = 0; i < len; )
            {

                rnd = new Random();
                int rint = rnd.Next(maxnum);
                if (i > 0)
                {
                    if (arrInt[i - 1] == rint)
                    {
                        continue;
                    }
                    else
                    {
                        arrInt[i] = rint;
                        i++;
                    }
                }
                else
                {
                    arrInt[i] = rint;
                    i++;
                }



            }


            return arrInt;

        }

        /// <summary>
        /// 根据Int类型数组 和表生成对应的字符串
        /// </summary>
        /// <param name="arrint"></param>
        /// <returns></returns>
        public static string GenKey(int[] arrint)
        {
            int len = arrint.Length;
            char[] ctable = GenTable();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {

                sb.Append(ctable[arrint[i]]);
            }

            return sb.ToString();

        }

        /// <summary>
        /// 检查数组中元素中是否存在字符串
        /// </summary>
        /// <param name="strlist">数组</param>
        /// <param name="key">字符串</param>
        /// <returns></returns>
        private static bool CheckKeyRep(string[] strlist, string key)
        {
            foreach (string k in strlist)
            {
                if (!String.IsNullOrEmpty(k))
                {
                    if (k == key)
                    {
                        return true;

                    }
                }

            }
            return false;

        }

        /// <summary>
        /// 生成字符串数组 
        /// </summary>
        /// <param name="len">数组长度</param>
        /// <param name="keylen">字符串长度</param>
        /// <returns></returns>
        public static string[] GetAllKey(int len, int keylen)
        {
            String[] strlist = new String[len];
            for (int i = 0; i < len; )
            {
                String key = GenKey(RanKey(keylen));

                if (!CheckKeyRep(strlist, key))
                {

                    strlist[i] = key;
                    i++;
                }

            }

            return strlist;

        }

        #endregion

        #region  一般循环用来生成 不重复的数据  (提高算法复杂度 避免重复)

        /// <summary>
        /// 生成指定范围和位数的随机数
        /// </summary>
        /// <param name="c">可生成的政府集合</param>
        /// <param name="length">生成的长度</param>
        /// <param name="i">可变参数</param>
        /// <returns></returns>
        public static string RandomKey(char[] c, int length, int i)
        {
            StringBuilder sb = new StringBuilder();
            long tick = DateTime.Now.Ticks - i;
            Random ra = new Random((int)(tick - i & 0xffffffffL) | (int)(tick + i >> 32));
            while (sb.Length < length)
            {
                sb.Append(c[ra.Next(0, c.Length)]);
            }

            return sb.ToString();
        }

        #endregion 
    }
}
