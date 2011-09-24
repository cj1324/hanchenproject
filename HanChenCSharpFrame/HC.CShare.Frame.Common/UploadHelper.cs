using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace HC.CShare.Frame.Common
{
    public class UploadHelper
    {
        #region 上传验证


        /// <summary>
        /// 根据ContentType判断是否是图片(同时验证扩展名)
        /// </summary>
        /// <param name="fu_img"></param>
        /// <returns></returns>
        public bool CheckUploadByContTypeIsImg(HttpPostedFile hpf)
        {
            string type = hpf.ContentType;
            string extname= System.IO.Path.GetExtension(hpf.FileName);
            bool isImg = false;

            if (type.Equals("image/jpeg") || type.Equals("image/pjpeg") || type.Equals("image/png") || type.Equals("image/gif") || type.Equals("image/bmp") || type.Equals("image/x-png"))
            {
                if (extname.Equals(".jpg") || extname.Equals(".png") || extname.Equals(".gif") || extname.Equals(".bmp"))
                {
                    isImg = true;
                }
                else
                {
                    isImg = false;
                }
            }
            else
            {
                isImg = false;
            }


            return isImg;

        }


        /// <summary>
        /// 自己定义ContentType判断(同时验证扩展名)
        /// </summary>
        /// <param name="hpf"></param>
        /// <param name="contType"></param>
        /// <returns></returns>
        public bool CheckUploadByContType(HttpPostedFile hpf, String contType)
        {
            string type = hpf.ContentType;
            bool isImg = false;

            if (type.Equals(contType))
            {
                isImg = true;
            }
            else
            {
                isImg = false;
            }


            return isImg;

        }





        /// <summary>
        /// 根据文件的前2个字节进行判断文件类型是不是图片
        /// </summary>
        /// <param name="hpf"></param>
        /// <returns></returns>
        public bool CheckUploadByTwoByteIsImg(HttpPostedFile hpf)
        {
            System.IO.FileStream fs = new System.IO.FileStream(hpf.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();

            }
            catch
            {

            }
            r.Close();
            fs.Close();
            //说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "13780")
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 根据文件的前2个字节进行判断文件类型  ---说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
        /// </summary>
        /// <param name="hpf"></param>
        /// <param name="code">文件的前2个字节转化出来的小数</param>
        /// <returns></returns>
        public bool CheckUploadByTwoByte(HttpPostedFile hpf, Int64 code)
        {
            System.IO.FileStream fs = new System.IO.FileStream(hpf.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();

            }
            catch
            {

            }
            r.Close();
            fs.Close();
            //
            //if (fileclass == code.ToString())
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return (fileclass == code.ToString());

        }





        //开始上传

        /// <summary>
        /// 上传文件方法(需要测试的方法)
        /// </summary>
        /// <param name="hpf">需要上传的文件</param>
        /// <param name="pathdir">需要上传的路径 前后的/都需要</param>
        /// <param name="servicePath">网站目录的根路径 传入 Server.MapPath("~/") </param>
        /// <returns>上传成功后的路径</returns>
        public string ToUpdateLoad(HttpPostedFile hpf, String pathdir)
        {

            string file;
            string servicePath = HttpContext.Current.Server.MapPath("~/");
            if (!System.IO.Directory.Exists(servicePath + pathdir))
            {
                System.IO.Directory.CreateDirectory(servicePath + pathdir);
            }

           

            //简单实现上传
            file = pathdir + Tool.DateRndNum() + System.IO.Path.GetExtension(hpf.FileName);
            try
            {
                hpf.SaveAs(servicePath + file);
            }
            catch { file = String.Empty; }

            return file;
        }



        /// <summary>
        /// 上传文件方法(需要测试的方法)
        /// </summary>
        /// <param name="hpf">需要上传的文件</param>
        /// <param name="pathdir">需要上传的路径</param>
        /// <returns>上传成功后的路径</returns>
        public string ToUpdateLoad(HttpPostedFile hpf)
        {
            string pathdir = GetUpDir();
            return ToUpdateLoad(hpf, pathdir);
            
        }


        /// <summary>
        /// 获取上传的路径图片的保存路径
        /// </summary>
        /// <returns></returns>
        private string GetUpDir()
        {

            return "/UserUpload/";
        }


        /// <summary>
        /// 获取上传的路径图片的保存路径 (只需要传入文件夹名 前后都必须有/ 例如 /Img/productImg/ )
        /// </summary>
        /// <returns></returns>
        private string GetUpDir(string subDir)
        {

            return "/UserUpload" + subDir;
        }



        /// <summary>
        /// 判断上传文件大小
        /// </summary>
        /// <param name="fu">上传控件</param>
        /// <param name="length">文件最大值(M单位)</param>
        /// <returns></returns>
        public bool ValUpdateLength(HttpPostedFile hpf, Int32 length)
        {
            if (hpf.ContentLength < length * 1048576)
            {
                return true;
            }

            return false;

        }

        public bool ValUpdateLength(HttpPostedFile hpf)
        {

            return this.ValUpdateLength(hpf, 5);
        }



        #endregion
    }
}
