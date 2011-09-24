using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Text.RegularExpressions;

namespace HC.CShare.Frame.Common
{
    /// <summary>
    /// ���õ�һЩ������(��̬��)
    /// </summary>
    public static class Encrypt
    {
        #region �ؼ�KEY�����ֶ�

        private static string hcKey = "HanChenCSharpFrame";   //HanChenCSharpFrame
        #endregion

        #region DES���ܽ���

        /// <summary>
        /// DES�㷨����
        /// </summary>
        /// <param name="pToEncrypt">��Ҫ���ܵ��ַ���</param>
        /// <param name="sKey">���ܵ�key</param>
        /// <returns>���ܺ������</returns>
        public static string DES(string pToEncrypt, string sKey)
        {
            //�������ݼ��ܱ�׼(DES)�㷨�ļ��ܷ����ṩ���� (CSP) �汾�İ�װ����
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0,8));��//�������ܶ������Կ��ƫ����
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0,8));�� //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes����

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);//���ַ����ŵ�byte������

            MemoryStream ms = new MemoryStream();//������֧�ִ洢��Ϊ�ڴ������
            //���彫���������ӵ�����ת������
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //�����Ѿ�����˰Ѽ��ܺ�Ľ���ŵ��ڴ���ȥ

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DES���ܷ��� Ĭ��KEY
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DES(string encryptString)
        {
            return  Encrypt.DES(encryptString, hcKey);
        }


        /// <summary>
        /// DES�Ľ��ܷ���
        /// </summary>
        /// <param name="decryptString">��Ҫ���ܵ��ַ���</param>
        /// <param name="sKey">���ܵ�KEY</param>
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
        /// Ĭ��KEY�Ľ��ܷ���
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string deDES(string decryptString)
        {
            return Encrypt.deDES(decryptString, hcKey);

        }
        #endregion

        #region MD5��������㷨
        /// <summary>
        /// �����MD5�㷨
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

                throw new Exception("md5���ܵ�ʱ����������__");
            }

            return md5code;

        }


        /// <summary>
        /// MD5�򵥱����㷨 by:����
        /// </summary>
        /// <param name="pwd">��ʵ����</param>
        /// <returns>����MD5���ܺ�</returns>
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

        #region �������ǿ��

        /// <summary>
        /// �������ǿ�ȷ��ص÷�
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
            Regex reg1 = new Regex(@"^[A-Z]+$"); //��֤�Ƿ񴿴�д��ĸ
            if (reg1.IsMatch(strPWD)) score += 10;
            Regex reg2 = new Regex(@"^[a-z]+$"); //��֤�Ƿ�Сд��ĸ
            if (reg2.IsMatch(strPWD)) score += 10;

            Regex reg3 = new Regex(@"^[0-9]+$"); //��֤�Ƿ�����
            if (reg3.IsMatch(strPWD)) score += 10;

            Regex reg4 = new Regex(@"^(([a-z]+[A-Z]+)|([A-Z]+[a-z]+))+$"); //��֤�Ƿ��Сд�����ĸ
            if (reg4.IsMatch(strPWD)) score += 25;

            //Regex reg4 = new Regex(@"\d{1}");      //��֤�Ƿ�ֻ��һ������
            //if (reg4.IsMatch(strPWD)) score += 10;
            //Regex reg5 = new Regex(@"\d{3,}");      //��֤�����������ֻ���������
            //if (reg5.IsMatch(strPWD)) score += 10;
            //Regex reg6 = new Regex(@"[!@#$%&*]*{1}");   //��֤�Ƿ�ֻ��һ�������ַ�
            //if (reg6.IsMatch(strPWD)) score += 10;
            //Regex reg7 = new Regex(@"[\s\S]*{3,}");   //��֤���������ַ�����������
            //if (reg7.IsMatch(strPWD)) score += 25;
            //Regex reg8 = new Regex(@"^[A-Za-z0-9]+$"); //��֤�Ƿ���ĸ������
            //if (reg8.IsMatch(strPWD)) score += 2;
            //Regex reg9 = new Regex(@"^[A-Za-z0-9]+$"); //��֤�Ƿ���ĸ������
            //if (reg9.IsMatch(strPWD)) score += 2;

            Regex reg5 = new Regex(@"^(([0-9]+[a-zA-Z]+[0-9]*)|([a-zA-Z]+[0-9]+[a-zA-Z]*))+$"); //��֤�Ƿ���ĸ�����ֻ��
            if (reg5.IsMatch(strPWD)) score += 65;
            return score;
        }
        #endregion

    }
}
