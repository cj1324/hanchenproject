using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace HC.CShare.Frame.Common
{
    public class UrlParams
    {
        Page _pg;

        private UrlParams()
        {


        }



        public UrlParams(Page pg)
        {
            _pg = pg;
        }


        public UrlParams(UserControl uc)
        {
            _pg = uc.Page;
        }


        /// <summary>
        /// 检查获取ID参数(必须参数)
        /// </summary>
        /// <param name="idparam"></param>
        /// <param name="pg"></param>
        /// <returns></returns>
        public int CheckInt(string paramname)
        {
            int id = 0;
            String strId = _pg.Request.QueryString[paramname];
            if (!String.IsNullOrEmpty(strId))
            {
                if (!Int32.TryParse(strId, out id))
                {
                    _pg.Response.Write("警告:你输入了恶意字符串,你的IP已经被记录...");
                    _pg.Response.End();
                }
            }
            else
            {
                // _pg.Response.Clear();
                _pg.Response.Write("警告:您没有输入必须的参数.");
                _pg.Response.End();
            }
            return id;

        }

        /// <summary>
        /// 非强制性的获取INT类型
        /// </summary>
        /// <param name="paramname"></param>
        /// <returns></returns>
        public int GetInt(string paramname)
        {
          return  this.GetInt(paramname, 0);
        }



        /// <summary>
        /// 获取INT类型
        /// </summary>
        /// <param name="paramname">参数名</param>
        /// <param name="defaultnum">默认参数值</param>
        /// <returns>实际值</returns>
        public int GetInt(String paramname, int defaultnum)
        {
            int id = defaultnum;
            String strId = _pg.Request.QueryString[paramname];
            if (!String.IsNullOrEmpty(strId))
            {
                if (!Int32.TryParse(strId, out id))
                {
                    return id;
                }
            }

            return id;
        }


        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            String ret = String.Empty;
            ret=_pg.Request.QueryString[key];
            return ret;
        }




    }
}
