using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Text.RegularExpressions;

namespace Guess.Common
{
    /// <summary>
    /// 常用的一些加密类(静态类)
    /// </summary>
    public static class Encrypt
    {
        #region 关键KEY保密字段

        private static string hcKey = "HanChenCSharpFrame";   //HanChenCSharpFrame
        #endregion

        #region DES加密解密

        /// <summary>
        /// DES算法加密
        /// </summary>
        /// <param name="pToEncrypt">需要加密的字符串</param>
        /// <param name="sKey">加密的key</param>
        /// <returns>加密后的密文</returns>
        public static string DES(string pToEncrypt, string sKey)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0, 8));　//建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0, 8));　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            //定义将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //上面已经完成了把加密后的结果放到内存中去

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DES加密方法 默认KEY
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DES(string encryptString)
        {
            return Encrypt.DES(encryptString, hcKey);
        }


        /// <summary>
        /// DES的解密方法
        /// </summary>
        /// <param name="decryptString">需要解密的字符串</param>
        /// <param name="sKey">解密的KEY</param>
        /// <returns></returns>
        public static string deDES(string decryptString, string sKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(sKey.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());

        }

        /// <summary>
        /// 默认KEY的解密方法
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string deDES(string decryptString)
        {
            return Encrypt.deDES(decryptString, hcKey);

        }
        #endregion

        #region MD5及其相关算法
        /// <summary>
        /// 常规的MD5算法
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string MD5(string pwd)
        {
            string md5code;
            try
            {
                md5code = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToUpper();
            }
            catch (Exception ex)
            {

                throw new Exception("md5加密的时候遇到错误__Error:"+ex.Message);
            }

            return md5code;

        }


        /// <summary>
        /// MD5简单变异算法 by:寒晨
        /// </summary>
        /// <param name="pwd">真实密码</param>
        /// <returns>变异MD5加密后</returns>
        public static string MD5Var(string pwd)
        {
            string md5code;
            md5code = Encrypt.MD5(pwd);
            char[] md5arr = md5code.ToCharArray();

            if (md5arr.Length == 32)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i % 2 == 0)
                    {
                        char tempc = md5arr[i];
                        md5arr[i] = md5arr[31 - i];
                        md5arr[31 - i] = tempc;


                    }
                    else
                    {
                        switch (md5arr[i])
                        {
                            case 'A':
                                md5arr[i] = 'B';
                                break;
                            case 'B':
                                md5arr[i] = 'C';
                                break;
                            case '1':
                                md5arr[i] = '2';
                                break;
                            case '2':
                                md5arr[i] = '3';
                                goto case '3';
                            case '3':
                                md5arr[i] = '4';
                                break;
                            default:
                                break;
                        }
                    }

                }

                StringBuilder retcode = new StringBuilder();
                foreach (char c in md5arr)
                {

                    retcode.Append(c);
                }
                return retcode.ToString();

            }
            return "Error";
        }
        #endregion

        #region 检查密码强度

        /// <summary>
        /// 检查密码强度返回得分
        /// </summary>
        /// <param name="strPWD"></param>
        /// <returns></returns>
        public static int checkStrong(string strPWD)
        {
            //(([0-9]+[a-zA-Z]+[0-9]*)|([a-zA-Z]+[0-9]+[a-zA-Z]*))
            int score = 0;
            if (strPWD.Length <= 6) score += 5;
            if (strPWD.Length < 8 && strPWD.Length > 6) score += 10;
            if (strPWD.Length >= 8) score += 25;
            Regex reg1 = new Regex(@"^[A-Z]+$"); //验证是否纯大写字母
            if (reg1.IsMatch(strPWD)) score += 10;
            Regex reg2 = new Regex(@"^[a-z]+$"); //验证是否纯小写字母
            if (reg2.IsMatch(strPWD)) score += 10;

            Regex reg3 = new Regex(@"^[0-9]+$"); //验证是否纯数字
            if (reg3.IsMatch(strPWD)) score += 10;

            Regex reg4 = new Regex(@"^(([a-z]+[A-Z]+)|([A-Z]+[a-z]+))+$"); //验证是否大小写混合字母
            if (reg4.IsMatch(strPWD)) score += 25;

            //Regex reg4 = new Regex(@"\d{1}");      //验证是否只有一个数字
            //if (reg4.IsMatch(strPWD)) score += 10;
            //Regex reg5 = new Regex(@"\d{3,}");      //验证是有三个数字或三个以上
            //if (reg5.IsMatch(strPWD)) score += 10;
            //Regex reg6 = new Regex(@"[!@#$%&*]*{1}");   //验证是否只有一个特殊字符
            //if (reg6.IsMatch(strPWD)) score += 10;
            //Regex reg7 = new Regex(@"[\s\S]*{3,}");   //验证三个特殊字符或三个以上
            //if (reg7.IsMatch(strPWD)) score += 25;
            //Regex reg8 = new Regex(@"^[A-Za-z0-9]+$"); //验证是否字母和数字
            //if (reg8.IsMatch(strPWD)) score += 2;
            //Regex reg9 = new Regex(@"^[A-Za-z0-9]+$"); //验证是否字母和数字
            //if (reg9.IsMatch(strPWD)) score += 2;

            Regex reg5 = new Regex(@"^(([0-9]+[a-zA-Z]+[0-9]*)|([a-zA-Z]+[0-9]+[a-zA-Z]*))+$"); //验证是否字母、数字混合
            if (reg5.IsMatch(strPWD)) score += 65;
            return score;
        }
        #endregion

    }
}