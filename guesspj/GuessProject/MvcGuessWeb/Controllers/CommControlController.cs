using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGuessWeb.Controllers
{
    /// <summary>
    /// 给用户控件调用的控制层
    /// </summary>
    public class CommControlController : Controller
    {
        //
        // GET: /CommControl/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult VUC_Test()
        {
            ViewData["Info"] = "我是VUC用户控件方法的数据!!!!!!!";
            return View();
        }

        /// <summary>
        /// 游戏列表用户控件
        /// </summary>
        /// <returns></returns>
        public ActionResult GameList()
        {
            return View();
        }


        public ActionResult VUC_TopHead()
        {
            return View();
        }
        
        public ActionResult VUC_HCPager(string urlrou,int? count,int? size, int? index)
        {
            //ViewData["NullMsg"] = "";
            //  key | value | class
            if((count??0) <=0)
            {
                ContentResult cr=new ContentResult();
                cr.ContentType="text/html";
                cr.Content="&nbsp;";
                return cr;
            }
            if (size == null || size <= 0)
            {
                size = 1;
            }
            int pagenum = count.Value / size.Value;
            pagenum += (count.Value%size.Value>0)?1:0;
            int icurr = index.Value-1;
            if (icurr <= 0)
            {
                icurr = 1;
            }

            List<String> strlist = new List<string>();
            strlist.Add(string.Format("{0}|{1}|{2}", "<<", string.Format(urlrou, "1"), "1"));
            for (int i = icurr; i < icurr + 5; i++)
            {
                strlist.Add(string.Format("{0}|{1}|{2}", i.ToString(), string.Format(urlrou, i.ToString()), i.ToString()));
            }

            strlist.Add(string.Format("{0}|{1}|{2}", ">>", string.Format(urlrou, pagenum.ToString()), pagenum.ToString()));






            ViewData["BtnList"] = strlist.ToArray();
            
            return View();
        }

        
    }
}
