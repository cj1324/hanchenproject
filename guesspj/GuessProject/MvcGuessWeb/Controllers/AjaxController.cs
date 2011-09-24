using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guess.Common;
using Guess.DataBase;
using Guess.Model;

namespace MvcGuessWeb.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/
        [HttpGet]
        public ActionResult Index()
        {
            ContentResult cr = new ContentResult();
            cr.ContentType = "text/html";
            cr.Content = "HTML FILE NULL";
            return cr;
        }

        [HttpPost]
        public ActionResult GameTimer()
        {
            ContentResult cres = new ContentResult();

            cres.ContentType = "text/html";
            string cont = null;
            string game = Request.Form["gtype"].ToString();
            switch (Convert.ToInt32(game))
            {
                case 1:
                    cont = "GameOneTimer";
                    break;
                case 2:
                    cont = "GameTwoTimer";
                    break;

                default:
                    cont = "ERROR";
                    break;
            }

            HttpContext.Application.Lock();
            if (HttpContext.Application[cont] != null)
            {
                cont = HttpContext.Application[cont].ToString();
            }
            HttpContext.Application.UnLock();
            Manage_T_Game mtg = new Manage_T_Game();
            int pid = mtg.GetNewPhases(Convert.ToInt32(game));
            cres.Content ="{\"id\":\""+pid.ToString()+"\",\"timer\":\""+cont+"\"}";
            return cres;
        }

    }
}
