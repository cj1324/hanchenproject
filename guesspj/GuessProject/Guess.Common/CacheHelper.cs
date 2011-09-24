using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Guess.Common
{
    /// <summary>
    /// 辅助的B/S缓存类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 获取当前应用程序指定key的Cache值
        /// </summary>
        public static object GetCache(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static void SetCache(string key, object value)
        {
            SetCache(key, value, DateTime.Now, TimeSpan.FromSeconds(30d));
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        public static void SetCache(string key, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpContext.Current.Cache.Insert(key, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public static object RemoveCache(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return HttpContext.Current.Cache.Remove(key);
            }
            throw new ArgumentException("错误的CacheKey!");
        }
    }
}