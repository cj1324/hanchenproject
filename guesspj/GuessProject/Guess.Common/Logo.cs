using System;
using System.Collections.Generic;
//using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Text;

//用来配合枚举的Description说明
using System.ComponentModel;
using System.Reflection;

namespace Guess.Common
{
    /// <summary>
    ///Logo 的摘要说明
    /// </summary>
    public class Logo
    {
        private String LogoPath = "/App_Data/Logo";
        private String LogoFileEx = ".log";
        private FileStream fs = null;
       // private StreamReader sr = null;
        private StreamWriter sw = null;

        public Logo()
        {

        }

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        private static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }


        /// <summary>
        /// 写入文本的内容
        /// </summary>
        /// <param name="Mess"></param>
        /// <param name="lt"></param>
        /// <returns></returns>
        private static String GetLogo(String Mess, LogoType lt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("\t\t");
            sb.Append(GetEnumDescription(lt));
            sb.Append("\t");
            sb.Append(Mess);



            return sb.ToString();
        }



        /// <summary>
        /// 写日志 (发布时不实现)
        /// </summary>
        /// <param name="Mess">日志内容</param>
        /// <param name="lt">日志类型</param>
        /// <returns></returns>
        public bool WriteLogo(String Mess, LogoType lt)
        {
            bool ret = false;
            String path; //= HttpContext.Current.Server.MapPath(LogoPath);
          //  string path = Application.StartupPath + LogoPath;
            //path="e:\\logo";
            try
            {
                //CheckFile(path);
                //fs = new FileStream(path + LogoFileEx, FileMode.Append, FileAccess.Write);
                //sw = new StreamWriter(fs);
                //sw.WriteLine(GetLogo(Mess, lt));
                ret = true;
            }
            catch (IOException e)
            {
                // throw new Exception(e.Message);
                ret = false;
            }
            finally
            {
              //  sw.Close();
              //  fs.Close();
            }


            return ret;
        }

        /// <summary>
        /// 写简单的错误日志
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool WriteLogo(Exception e)
        {
            return WriteLogo(e.Message, LogoType.Error);
        }



        public bool WriteLogo(Exception e, string msg)
        {
            return WriteLogo(msg + "\r\n" + e.Message, LogoType.Error);
        }


        /// <summary>
        /// 检查文件状态
        /// </summary>
        /// <param name="path">文件名(不包含扩展名)</param>
        private void CheckFile(String path)
        {
            FileInfo fi = new FileInfo(path + LogoFileEx);
            if (fi.Exists)
            {
                if (fi.Length > (long)1048576)         //判断文件是否大于1M.
                {
                    if (BackupLogo(path))
                    {
                        if (!CreateFile(path))
                        {
                            //创建文件失败
                            throw new Exception("创建日志文件失败(已备份)");
                        }
                    }
                }

            }
            else
            {
                if (!CreateFile(path))
                {
                    //创建文件失败
                    throw new Exception("创建日志文件失败");
                }

            }
        }

        /// <summary>
        /// 备份日志文件
        /// </summary>
        /// <param name="path">文件名(不包含扩展名)</param>
        /// <returns>备份是否成功</returns>
        private bool BackupLogo(String path)
        {
            bool ret = false;
            try
            {
                String tempFileName = GetBackupFileName(path);
                File.Move(path + LogoFileEx, tempFileName);
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// 获取备份的文件名
        /// </summary>
        /// <param name="path">文件名(不包含扩展名)</param>
        /// <returns>备份的文件名</returns>
        private String GetBackupFileName(String path)
        {
            int i = 1;

            while (File.Exists(path + i.ToString() + LogoFileEx))
            {
                i++;
            }

            return path + i.ToString() + LogoFileEx;
        }

        /// <summary>
        /// 获取日志头行 声明.
        /// </summary>
        /// <returns></returns>
        private String GetFileHead()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t\t日期\t\t");
            sb.Append("\t");
            sb.Append("日志类型");
            sb.Append("\t");
            sb.Append("日志内容");


            return sb.ToString();
        }


        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CreateFile(String path)
        {
            bool ret = false;
            try
            {
                fs = new FileStream(path + LogoFileEx, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(GetFileHead());
                sw.WriteLine(" ");
                ret = true;
            }
            catch(IOException ex)
            {
                throw new Exception(ex.Message);
                
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }


            return ret;
        }
    }

    /// <summary>
    /// 写入的日志类型
    /// </summary>
    public enum LogoType
    {
        [Description("系统日志")]
        System = 0,
        [Description("错误日志")]
        Error = 1,
        [Description("操作日志")]
        Info = 2


    }
}
