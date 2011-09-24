using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Guess.Common
{
    /// <summary>
    /// 发送邮箱用的类
    /// </summary>
    public class MailHelper
    {
        public bool Send(String toMail, String subject, String Content)
        {
            SmtpClient sm = SMTPService();
            MailMessage mm = GetMessage(toMail, subject, Content);
            try
            {
                sm.SendAsync(mm, "Success");
            }
            catch (System.Net.Mail.SmtpException e)
            {
                Console.Write(e.Message);
                return false;
            }
            return true;
        }




        /// <summary>
        /// 利用本地IIS发送邮件
        /// </summary>
        /// <returns></returns>
        protected SmtpClient SMTPLocal()
        {
            SmtpClient sc = new SmtpClient();
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.Host = " www.le173.com";
            sc.Port = 2525;
            sc.Credentials = new System.Net.NetworkCredential("LE173会员中心", "le173.com");
            return sc;

        }


        /// <summary>
        /// 利用其他STMP服务发送邮件
        /// </summary>
        /// <returns></returns>
        protected SmtpClient SMTPService()
        {
            SmtpClient stmp = new SmtpClient(GetConfig("STMP"));
            stmp.Port = 25;
            stmp.UseDefaultCredentials = true;
            stmp.EnableSsl = true;
            stmp.Credentials = new System.Net.NetworkCredential(GetConfig("User"), Encrypt.deDES(GetConfig("Pwd")));
            stmp.DeliveryMethod = SmtpDeliveryMethod.Network;
            return stmp;
        }



        protected MailMessage GetMessage(String toMail, String subject, String Content)
        {
            MailMessage sendMess = null;
            sendMess = new MailMessage();
            sendMess.IsBodyHtml = true;
            sendMess.Priority = MailPriority.Normal;
            sendMess.Subject = subject;
            sendMess.From = new MailAddress(GetConfig("From"));
            sendMess.Body = Content; // GetBody(lvInfo.NickName, lvInfo.Account, pwd);
            sendMess.To.Add(toMail);
            sendMess.BodyEncoding = System.Text.Encoding.UTF8;
            sendMess.SubjectEncoding = System.Text.Encoding.UTF8;
            return sendMess;

        }



        /// <summary>
        ///  获取邮件服务器配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private String GetConfig(String key)
        {

            //From 
            //STMP
            //User
            //Pwd
            return ConfigurationManager.AppSettings[key];
        }
    }
}