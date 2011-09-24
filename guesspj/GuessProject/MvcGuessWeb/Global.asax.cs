using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Timers;
using Guess.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace MvcGuessWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        Timer guesstimer;
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

            routes.MapRoute(
                "DefaultHome", // 路由名称
                "default.html", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
                
                );
            routes.MapRoute(
                "HCPagerDefault",
                "HCPager/{urlrou}/{count}/{size}/{index}",
                new {controller="CommControl",action="VUC_HCPager",urlrou="{0}.aspx",count=1,size=1,index=1}
                );



        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            guesstimer = new Timer(1000);
            guesstimer.Elapsed += new ElapsedEventHandler(guesstimer_Elapsed);
            guesstimer.Enabled = true;
            RegisterRoutes(RouteTable.Routes);
        }

        void guesstimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GameOnePro();
            GameTwoPro();
        }

        public  void GameTwoPro()
        {
            lock (this)
            {
                SqlParameter[] parameters = { 
                                        new SqlParameter("@type",SqlDbType.Int,4)
                                        };
                parameters[0].Value = 2;
                int tnum = DBHelper.RunProc("sp_CheckGame", parameters);
                if (tnum <= 0)
                {
                    Manage_T_Game mtg = new Manage_T_Game();
                    mtg.CreateGameTwo();
                }
                Application.Lock();
                Application["GameTwoTimer"] = tnum.ToString();
                Application.UnLock();
            }

        }

        public void GameOnePro()
        {
            lock (this)
            {
                int tnum = DBHelper.RunProc("sp_CheckGame");
                if (tnum <= 0)
                {
                    Manage_T_Game mtg = new Manage_T_Game();
                    mtg.CreateGameOne();
                }
                Application.Lock();
                Application["GameOneTimer"] = tnum.ToString();
                Application.UnLock();
            }

        }



        protected void Application_End()
        {
            guesstimer.Enabled = false;
            guesstimer.Dispose();

        }
    }
}