using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guess.DataBase;
using System.Data;
namespace MvcGuessWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {

            ViewData["PageTitle"] = "人人翁";
            return View();
        }

        [HttpGet]
        public ActionResult Play()
        {
            ViewData["PageTitle"] = "人人翁 主游戏";
            RecordFromPage rfp = new RecordFromPage();

            int pageindex = 1;
            int type = 1;

            rfp.ConnStr = DBHelper.ConnStr;
            rfp.TableName = "T_Game";
            rfp.PageIndex = pageindex;
            rfp.Where = " F_Type=" + type.ToString();
            rfp.Fields = "*";
            rfp.OrderFields = "F_Phases desc";
            rfp.PageSize = 20;
            DataTable dt=rfp.GetDt();
            if (dt == null || dt.Rows.Count == 0)
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "Table NULL";
                return cr;

            }
            if(pageindex==1)
            {
                for(int i=0;i<3;i++)
                {
                   DataRow dr= dt.NewRow();
                    dr["F_Phases"]=Convert.ToInt32(dt.Rows[0]["F_Phases"])+1;
                    dr["F_LotteryDate"]=Convert.ToDateTime(dt.Rows[0]["F_LotteryDate"]).AddMinutes(3);
                    dr["F_NumOne"]=0;
                    dr["F_NumTwo"]=0;
                    dr["F_NumThree"]=0;
                    dr["F_Bonus"]=0;
                    dr["F_Id"]=0;
                    dr["F_InvolvedNum"]=0;
                    dr["F_WinningNum"]=0;
                   dr["F_Lottery"]=false;
                    dt.Rows.InsertAt(dr,0);
                }

            }
            string defimgattr = " border='0' align='absmiddle'  width='18'  height='21' style='display:inline'";
            switch (type)
            {
                case 1:
                    ViewData["LotteryShow"] = "<img " + defimgattr + " src='../Content/numimg/0{0}.jpg'> + <img  " + defimgattr + " src='../Content/numimg/0{1}.jpg'>+<img " + defimgattr + " src='../Content/numimg/0{2}.jpg'>=<img  " + defimgattr + " src='../Content/numimg/{3}.jpg'>";
                    break;
                case 2:
                    ViewData["LotteryShow"] = " {0}X{1}X{2}X{3}";
                    break;
                default:
                    ViewData["LotteryShow"] = "{0}-{1}-{2}-{3}";
                    break;
            }
            ViewData["GameType"] = type.ToString();
            ViewData["ConentTable"] = dt;
            return View();
        }


        [HttpGet]
        public ActionResult PlayGameOne()
        {
            ViewData["PageTitle"] = "人人翁  幸运28";
            RecordFromPage rfp = new RecordFromPage();

            int pageindex = 1;
            int type = 1;

            rfp.ConnStr = DBHelper.ConnStr;
            rfp.TableName = "T_Game";
            rfp.PageIndex = pageindex;
            rfp.Where = " F_Type=" + type.ToString();
            rfp.Fields = "*";
            rfp.OrderFields = "F_Phases desc";
            rfp.PageSize = 20;
            DataTable dt = rfp.GetDt();
            if (dt == null || dt.Rows.Count == 0)
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "Table NULL";
                return cr;

            }
            if (pageindex == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["F_Phases"] = Convert.ToInt32(dt.Rows[0]["F_Phases"]) + 1;
                    dr["F_LotteryDate"] = Convert.ToDateTime(dt.Rows[0]["F_LotteryDate"]).AddMinutes(3);
                    dr["F_NumOne"] = 0;
                    dr["F_NumTwo"] = 0;
                    dr["F_NumThree"] = 0;
                    dr["F_Bonus"] = 0;
                    dr["F_Id"] = 0;
                    dr["F_InvolvedNum"] = 0;
                    dr["F_WinningNum"] = 0;
                    dr["F_Lottery"] = false;
                    dt.Rows.InsertAt(dr, 0);
                }

            }
            string defimgattr = " border='0' align='absmiddle'  width='18'  height='21' style='display:inline'";
            switch (type)
            {
                case 1:
                    ViewData["LotteryShow"] = "<img " + defimgattr + " src='../Content/numimg/0{0}.jpg'> + <img  " + defimgattr + " src='../Content/numimg/0{1}.jpg'> + <img " + defimgattr + " src='../Content/numimg/0{2}.jpg'> = <img  " + defimgattr + " src='../Content/numimg/{3}.jpg'>";
                    break;
                case 2:
                    ViewData["LotteryShow"] = " {0}X{1}X{2}X{3}";
                    break;
                default:
                    ViewData["LotteryShow"] = "{0}-{1}-{2}-{3}";
                    break;
            }
            ViewData["GameType"] = type.ToString();
            ViewData["ConentTable"] = dt;

            ViewData["PagerData"] = new { urlrou = "javascript:var o={0};", count=20,size=20,index=1 };
            return View("Play");
        }


        [HttpGet]
        public ActionResult PlayGameTwo()
        {
            ViewData["PageTitle"] = "人人翁  掷骰子";
            RecordFromPage rfp = new RecordFromPage();

            int pageindex = 1;
            int type = 2;

            rfp.ConnStr = DBHelper.ConnStr;
            rfp.TableName = "T_Game";
            rfp.PageIndex = pageindex;
            rfp.Where = " F_Type=" + type.ToString();
            rfp.Fields = "*";
            rfp.OrderFields = "F_Phases desc";
            rfp.PageSize = 20;
            DataTable dt = rfp.GetDt();

            if (dt == null || dt.Rows.Count == 0)
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "text/html";
                cr.Content = "Table NULL";
                return cr;

            }
            if (pageindex == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["F_Phases"] = Convert.ToInt32(dt.Rows[0]["F_Phases"]) + 1;
                    dr["F_LotteryDate"] = Convert.ToDateTime(dt.Rows[0]["F_LotteryDate"]).AddMinutes(3);
                    dr["F_NumOne"] = 0;
                    dr["F_NumTwo"] = 0;
                    dr["F_NumThree"] = 0;
                    dr["F_Bonus"] = 0;
                    dr["F_Id"] = 0;
                    dr["F_InvolvedNum"] = 0;
                    dr["F_WinningNum"] = 0;
                    dr["F_Lottery"] = false;
                    dt.Rows.InsertAt(dr, 0);
                }

            }
            string defimgattr = " border='0' align='absmiddle'  width='18'  height='21' style='display:inline'";
            switch (type)
            {
                case 1:
                    ViewData["LotteryShow"] = "<img " + defimgattr + " src='../Content/numimg/0{0}.jpg'> + <img  " + defimgattr + " src='../Content/numimg/0{1}.jpg'>+<img " + defimgattr + " src='../Content/numimg/0{2}.jpg'>=<img  " + defimgattr + " src='../Content/numimg/{3}.jpg'>";
                    break;
                case 2:
                    ViewData["LotteryShow"] = " {0}X{1}X{2}X{3}";
                    break;
                default:
                    ViewData["LotteryShow"] = "{0}-{1}-{2}-{3}";
                    break;
            }
            ViewData["GameType"] = type.ToString();
            ViewData["ConentTable"] = dt;
            return View("Play");
        }


    }
}
