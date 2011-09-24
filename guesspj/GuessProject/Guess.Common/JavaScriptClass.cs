using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Resources;

namespace Guess.Common
{
    /// <summary>
    /// 基本的javascript操作类 输出脚本
    /// </summary>
    public class JavaScriptClass
    {
        #region  该类的字段


        /// <summary>
        /// 页面类(必须赋值)
        /// </summary>
        Page _pg;

        public Page Pg
        {
            get
            {

                if (_pg != null)
                {
                    return _pg;
                }
                else
                {
                    _pg = HttpContext.Current.Handler as Page;
                    if (_pg == null)
                    {
                        throw new ArgumentException("没有传入必须的page对象");
                    }

                    return _pg;

                }
            }
            set { _pg = value; }
        }

        /// <summary>
        /// 类内部默认的顺序 
        /// </summary>
        PageLoad _pl;


        /// <summary>
        /// 控制JS,DOM执行顺序
        /// </summary>
        public PageLoad Pl
        {
            get { return _pl; }
            set { _pl = value; }
        }


        /// <summary>
        /// 注册脚本的类型
        /// </summary>
        private Type _ctype;

        /// <summary>
        /// 脚本类别属性
        /// </summary>
        public Type Ctype
        {
            get
            {
                if (_ctype == null)
                {
                    this._ctype = Pg.GetType();
                }
                return _ctype;
            }
            set { _ctype = value; }
        }



        #endregion

        #region 该类的构造函数
        /// <summary>
        /// 内置构造函数 无法外部调用
        /// </summary>
        public JavaScriptClass()
        {
            //不允许直接调用构造!

        }

        /// <summary>
        /// 外部调用的构造方法 需要传入page对象
        /// </summary>
        /// <param name="page"></param>
        public JavaScriptClass(Page page) :
            this(page, PageLoad.Before, page.GetType())
        {

            //在this中已实现
        }

        /// <summary>
        /// 外部调用的构造方法 需要page对象和type对象 pageload默认为before
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctype"></param>
        public JavaScriptClass(Page page, Type ctype)
            : this(page, PageLoad.Before, ctype)
        {
            //已在this中调用实现
        }


        /// <summary>
        /// 需要调用js的构造函数(可以选择js执行先后)
        /// </summary>
        /// <param name="page">需要传入的page对象</param>
        /// <param name="pageload">选择js执行的先后顺序</param>
        public JavaScriptClass(Page page, PageLoad pageload, Type ctype)
        {
            _pl = pageload;
            _pg = page;
            _ctype = ctype;
        }




        #endregion

        #region 基本的写脚本方法

        /// <summary>
        /// 写脚本方法
        /// </summary>
        /// <param name="script">脚本</param>
        protected void BaseWriteScript(String script)
        {
            BaseWriteScript(script, "jsc_by_hc");
        }


        /// <summary>
        /// 写脚本方法可自定义KEY
        /// </summary>
        /// <param name="script">脚本</param>
        /// <param name="key">自定义KEY</param>
        protected void BaseWriteScript(String script, String key)
        {

            Pg.ClientScript.RegisterClientScriptBlock(Ctype, key, "<script type='text/javascript' language='javascript' rel='" + key + "' >" + script + "</script>");


        }



        /// <summary>
        /// 页面文档加载完毕后调用的脚本(调用该方法时请确认页面没有onload方法否则会冲突)
        /// </summary>
        /// <param name="script"></param>
        public void WriteScript(String script, PageLoad pl)
        {
            if (pl == PageLoad.After)
            {
                this.BaseWriteScript("window.onload=function(){" + script + "};");
            }
            else
            {
                this.BaseWriteScript(script);
            }
        }


        public void WriteScript(String script)
        {
            this.WriteScript(script, _pl);
        }

        #endregion

        #region 寒晨JS库注册


        public void hcjsreg()
        {
            StringBuilder jssb = new StringBuilder();
            jssb.Append("var HC=HC||{}");
            ResourceManager rm = new ResourceManager("HC.CShare.Frame.Common.HCResource", this.GetType().Assembly);
            // Object res= rm.GetObject("jquery");  //ToString() 就是文件内容了.




            BaseWriteScript(jssb.ToString(), "hc10js");
        }

        #endregion

        #region 写脚本中常用的方法

        /// <summary>
        /// 最简单的提示(默认顺序)
        /// </summary>
        /// <param name="msg">消息</param>
        public void Alert(String msg)
        {
            this.Alert(msg, _pl);
        }



        /// <summary>
        /// 简单的(alert)提示(入参中有顺序)
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="pl">执行顺序</param>
        public void Alert(String msg, PageLoad pl)
        {

            this.WriteScript("window.alert('" + msg + "');", pl);
        }




        /// <summary>
        /// 提示并跳转(默认顺序)
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <param name="url">跳转URL</param>
        public void AlertJmp(String msg, String url)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href='" + url + "';", _pl);
        }


        /// <summary>
        /// 提示并跳转(可控制顺序)
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="url">URL</param>
        /// <param name="pl">控制顺序</param>
        public void AlertJmp(String msg, String url, PageLoad pl)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href='" + url + "';", pl);
        }





        /// <summary>
        /// 直接跳转 在文档加载前
        /// </summary>
        /// <param name="url">需要跳转的路径</param>
        public void URLJmp(String url)
        {

            this.URLJmp(url, PageLoad.Before);
        }


        /// <summary>
        /// 直接跳转 可设置先后
        /// </summary>
        /// <param name="url">跳转的URL</param>
        /// <param name="pl">执行顺序</param>
        public void URLJmp(String url, PageLoad pl)
        {
            this.WriteScript("window.location.href='" + url + "';", pl);
        }



        /// <summary>
        /// 提示并刷新
        /// </summary>
        /// <param name="msg">消息</param>
        public void AlertRefresh(String msg)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href=window.location.href;");
        }



        /// <summary>
        /// 提示并刷新(可以控制顺序)
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="pl">顺序</param>
        public void AlertRefresh(String msg, PageLoad pl)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href=window.location.href;");
        }




        #endregion


    }


    #region 执行顺序的枚举

    /// <summary>
    /// 控制JS事件的执行顺序(默认在DOM加载前)
    /// </summary>
    public enum PageLoad
    {
        /// <summary>
        /// 页面加载之前
        /// </summary>
        Before = 0,

        /// <summary>
        /// 页面加载之后(注意会和onload事件冲突)
        /// </summary>
        After = 1
    }

    #endregion

    #region 准备方法(js代码)
    /***  页面加载的准备方法
     
     function domReady(fn){
            var _timer_ = null;
            void function(){
                if(document.all){
                    try{
                        document.body.doScroll("left");
                        clearTimeout(_timer_,_timer_ = null, fn());
                    }catch(e){
                        _timer_ = setTimeout(arguments.callee,1000);
                    }
                }else{
                    document.addEventListener("DOMContentLoaded", fn, false);
                }
            }()
        } 
      
     
     * */

    #endregion


}