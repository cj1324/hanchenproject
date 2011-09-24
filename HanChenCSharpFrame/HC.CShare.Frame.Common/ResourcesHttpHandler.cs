using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace HC.CShare.Frame.Common
{
    public class ResourcesHttpHandler:IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
           // throw new Exception("The method or operation is not implemented.");
            
            string resestr=context.Request.QueryString["id"];
            if (String.IsNullOrEmpty(resestr))
            {
                //输出错误信息
                return;
            }

            Object  obj=HCResource.ResourceManager.GetObject(resestr);

            if (obj == null)
            {
                return;
            }

            WriteContent(obj);

        }

        /// <summary>
        /// 根据获取的对象 获取内容
        /// </summary>
        /// <param name="obj"></param>
        public void WriteContent(Object obj)
        {

        }

        #endregion
    }
}
