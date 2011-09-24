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
        /// ����ȡID����(�������)
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
                    _pg.Response.Write("����:�������˶����ַ���,���IP�Ѿ�����¼...");
                    _pg.Response.End();
                }
            }
            else
            {
                // _pg.Response.Clear();
                _pg.Response.Write("����:��û���������Ĳ���.");
                _pg.Response.End();
            }
            return id;

        }

        /// <summary>
        /// ��ǿ���ԵĻ�ȡINT����
        /// </summary>
        /// <param name="paramname"></param>
        /// <returns></returns>
        public int GetInt(string paramname)
        {
          return  this.GetInt(paramname, 0);
        }



        /// <summary>
        /// ��ȡINT����
        /// </summary>
        /// <param name="paramname">������</param>
        /// <param name="defaultnum">Ĭ�ϲ���ֵ</param>
        /// <returns>ʵ��ֵ</returns>
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
        /// ��ȡ�ַ���
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
