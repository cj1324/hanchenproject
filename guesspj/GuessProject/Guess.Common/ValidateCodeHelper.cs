using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web;

namespace Guess.Common
{
    /// <summary>
    /// 验证码生成类  
    /// </summary>
    public class ValidateCodeHelper
    {
        #region 生成五位 字母或数字  GIF图片 cook保存 1小时 CookieName= "CheckCode"
        /// <summary>
        /// 生成五位 字母或数字  cook保存 1小时 CookieName= "CheckCode"
        /// </summary>
        /// <returns>验证码的字符串</returns>
        public string GenerateCheckCode()
        {
            int number;
            char code;
            string strCheckCode = String.Empty;
            Random random = new Random();
            for (int iCount = 0; iCount < 5; iCount++)
            {
                number = random.Next();
                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                }
                strCheckCode += code.ToString();
            }
            //保存在用户本地的Cookie里
            CookieHelper.SetCookie("CheckCode", strCheckCode, DateTime.Now.AddHours(1),false);
            //保存在服务器的Session里
            //HttpContext.Current.Session["CheckCode"] = strCheckCode;
            return strCheckCode;
        }

        /// <summary>
        /// 创建验证码图片,并将其写入HTTP流中
        /// </summary>
        /// <param name="CheckCode"></param>
        public void CreateCheckCodeImage(string CheckCode)
        {
            if (CheckCode == null || CheckCode.Trim() == String.Empty)
            {
                return;
            }
            Bitmap img = new Bitmap((int)Math.Ceiling((CheckCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(img);
            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int iCount = 0; iCount < 25; iCount++)
                {
                    int x1 = random.Next(img.Width);
                    int x2 = random.Next(img.Width);
                    int y1 = random.Next(img.Height);
                    int y2 = random.Next(img.Height);
                    g.DrawLine(new Pen(Color.WhiteSmoke), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Red, Color.SteelBlue, 1.2f, true);
                g.DrawString(CheckCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(img.Width);
                    int y = random.Next(img.Height);
                    img.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Pink), 0, 0, img.Width - 1, img.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }
        }


        #endregion


    }
}