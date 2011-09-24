using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Guess.Common
{
    /// <summary>
    /// HTML页面纠正C#代码
    /// </summary>
    public static class FixHtml
    {
        /// <summary>
        /// 处理图片路径
        /// </summary>
        /// <param name="currdir"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ImgPath(string currdir, string value)
        {
            return currdir + value;
        }


        /// <summary>
        /// 处理字符长度(待完善)
        /// </summary>
        /// <returns></returns>
        public static string StrSub(string str, int len)
        {
            String ret = String.Empty;
            if (str.Length <= len)
            {

                ret = str;
            }
            else
            {
                ret = str.Substring(0, len - 1) + "...";
            }

            return ret;
        }


        /// <summary>
        /// 过滤出中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ContentRegex(string input)
        {
            //input = input.Replace("\r", "");
            //input = input.Replace("\n", "");
            //Match m = Regex.Match(input, @"(?<=<p>)([\s\S]*?)(?=</p>)");
            //string value = m.Value;
            //return value;

            ////@"[\u4e00-\u9fa5]+";

            string ResultString = "";
            for (int i = 0; i < input.Length - 1; i++)
            {
                string mystr = input.Substring(i, 1);
                if (Regex.IsMatch(mystr, "[\u4e00-\u9fa5]|[，。？：；“”！《》]"))
                    ResultString += mystr;

            }

            return ResultString;
        }
    }
}