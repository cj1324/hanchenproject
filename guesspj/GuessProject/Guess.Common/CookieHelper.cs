using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Guess.Common
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public static class CookieHelper
    {


        /// <summary>
        /// 获取一个数组形式的Cookies
        /// </summary>
        public static HttpCookie GetCookie(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        /// <summary>
        /// 移除Cookies
        /// </summary>
        public static void RemoveCookie(string name)
        {
            RemoveCookie(GetCookie(name));
        }

        /// <summary>
        /// 移除Cookies
        /// </summary>
        public static void RemoveCookie(HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.Expires = new DateTime(1983, 1, 2);
                Save(cookie);
            }
        }

        /// <summary>
        /// 保存Cookies
        /// </summary>
        public static void Save(HttpCookie cookie)
        {

            cookie.Domain = "";
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获取一个新的Cookies
        /// </summary>
        public static HttpCookie GetNewCookie(string name)
        {
            HttpCookie hc = new HttpCookie(name);

            return hc;
        }

        /// <summary>
        /// 取得指定名称的单值Cookie
        /// </summary>
        /// <returns></returns>
        public static string GetCookieValue(string name)
        {
            HttpCookie cookie = GetCookie(name);
            if (cookie == null || cookie.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return cookie.Value;
            }
        }

        /// <summary>
        /// 保存指定名称的单值Cookie
        /// </summary>
        /// <returns></returns>
        public static void SetCookie(string name, string value)
        {
            SetCookie(name, value, DateTime.Now,true);
        }

        /// <summary>
        /// 保存指定名称的单值Cookie
        /// </summary>
        /// <returns></returns>
        public static void SetCookie(string name, string value, DateTime expires,bool ishttp)
        {
            HttpCookie cookie = GetCookie(name);
            
            if (cookie == null)
            {
                cookie = GetNewCookie(name);
            }
            cookie.HttpOnly = ishttp;
            cookie.Value = value;
            cookie.Expires = expires;
            Save(cookie);
        }
    }

}