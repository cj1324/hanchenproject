using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Guess.DataBase;
using Guess.Model;
using Guess.Common;

namespace MvcGuessWeb.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/


        /// <summary>
        /// 首页 测试用途
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            TempData["Info"] = "Success";
            return View();
        }


        /// <summary>
        /// 登陆页面展示
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// 登陆数据提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckAdminInfo()
        {

            string errorInfo = string.Empty;
            object oUser = Request.Form["txt_username"];
            object oPwd = Request.Form["txt_password"];
            if (oUser == null || oPwd == null)
            {
              
                errorInfo = "没有提交需要的数据";
            }
            else
            {
                errorInfo= CheckUserPwd(oUser.ToString(), oPwd.ToString());
            }


           
            if (!String.IsNullOrEmpty(errorInfo))
            {
                TempData["ErrorInfo"] = errorInfo;
                return RedirectToAction("Login");
            }

            Session["AdminLogin"] = true;
            return RedirectToAction("GameInfo");
        }




        /// <summary>
        /// 判断登陆数据合法
        /// </summary>
        /// <returns></returns>
        public bool CheckLogin()
        {

            if (Session["AdminLogin"] == null)
            {
                return false;
            }

            if (!Convert.ToBoolean(Session["AdminLogin"]))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 判断帐号密码是否错误
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string CheckUserPwd(string user,string pwd)
        {

            if (string.IsNullOrEmpty(user))
            {
                return "用户名不能为空!";
            }

            if (string.IsNullOrEmpty(pwd))
            {

                return "密码不能为空!";
            }
            if (user.Trim().ToLower() != GetConfig("AdminUser").ToLower())
            {
                return "管理用帐号不存在!";
            }
            if (pwd.Trim().ToLower() != GetConfig("AdminPwd").Trim().ToLower())
            {

                return "管理员密码错误";
            }

            return string.Empty;

        }


        /// <summary>
        ///  获取服务器配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private String GetConfig(String key)
        {

            //AdminUser
            //AdminPwd
            return ConfigurationManager.AppSettings[key];
        }


        /// <summary>
        /// 游戏编辑加载页
        /// </summary>
        /// <returns></returns>
        public ActionResult GameEdit(int? id)
        {
            if (!CheckLogin())
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "请先登陆<a href='../Admin/Login'>Login</a>";
                return cr;
            }

            
            TempData["PageTitle"] = "游戏编辑";
        
            if (id!=null)
            {
                Manage_T_Game mtg = new Manage_T_Game();
                T_Game tg = mtg.GetModelById(id.Value);
                if (tg == null)
                {
                    throw new Exception("没有获取到模型!!!");
                }
                ViewData["GameInfo"] = tg;
            }
            else
            {
                throw new Exception("没有获取到ID..........");
            }
            return View("GameEdit2");
        }


        public ActionResult GameDelete()
        {
            TempData["Info"] = "Delete Fun";
            return View("Index");
        }

        /// <summary>
        /// 游戏 本轮资料修改方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GameEditFun()
        {
            if (!CheckLogin())
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "请先登陆<a href='../Admin/Login'>Login</a>";
                return cr;
            }


            //TempData["Info"] 
            Manage_T_Game mtg;
            T_Game t_g;
            string strid = Request.Form.Get("id").ToString();
            int id = 0;
            if (Int32.TryParse(strid, out id) && id > 0)  //判断ID是否合法
            {
                mtg = new Manage_T_Game();
                t_g = mtg.GetModelById(id);
                ViewData["GameInfo"] = t_g;
                
                t_g.F_NumOne = GetInt("txt_num1");
                t_g.F_NumTwo = GetInt("txt_num2");
                t_g.F_NumThree = GetInt("txt_num3");
                t_g.F_Bonus = GetInt("txt_boun");
                t_g.F_InvolvedNum = GetInt("txt_inv");
                t_g.F_WinningNum = GetInt("txt_win");
                int row=mtg.Update(t_g);
                if (row > 0)
                {
                    TempData["Info"] = "Update Success";
                }
                else
                {
                    TempData["Info"] = "Update Row Zero";
                }

            }
            else
            {
                TempData["Info"] = "ID VALUE ERROR";
                return View("GameInfo");
            }
            return View("Index");
        }

        public int GetInt(string strname)
        {
            int ret = 0;
            Object obj=Request.Form.Get(strname);
            if(obj!=null)
            {
                Int32.TryParse(obj.ToString(),out ret);
            }

            return ret;
        }


        /// <summary>
        /// 游戏期数列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult GameInfo()
        {
            if (!CheckLogin())
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content="请先登陆<a href='../Admin/Login'>Login</a>";
                return cr;
            }

            ViewData["PageTitle"] = "我是游戏 幸运28的管理";
           RecordFromPage rfp=new RecordFromPage();

            int pageindex=1;
            int type=1;

            rfp.ConnStr = DBHelper.ConnStr;
            rfp.TableName="T_Game";
            rfp.PageIndex=pageindex;
            rfp.Where=" F_Type="+type.ToString();
            rfp.Fields = "*";
            rfp.OrderFields = "F_Phases  desc ";
            rfp.PageSize = 20;
            ViewData["ConentTable"] = rfp.GetDt();

            return View();
        }

        public ActionResult GameInfoTwo()
        {
            if (!CheckLogin())
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "请先登陆<a href='../Admin/Login'>Login</a>";
                return cr;
            }


            ViewData["PageTitle"] = "我是游戏 投骰子的管理";
            RecordFromPage rfp = new RecordFromPage();

            int pageindex = 1;
            int type = 2;

            rfp.ConnStr = DBHelper.ConnStr;
            rfp.TableName = "T_Game";
            rfp.PageIndex = pageindex;
            rfp.Where = " F_Type=" + type.ToString();
            rfp.Fields = "*";
            rfp.OrderFields = "F_Phases  desc ";
            rfp.PageSize = 20;
            ViewData["ConentTable"] = rfp.GetDt();
            return View("GameInfo");

        }




    }
}
