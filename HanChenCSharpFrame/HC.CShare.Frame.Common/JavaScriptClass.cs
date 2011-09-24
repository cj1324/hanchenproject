using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Resources;

namespace HC.CShare.Frame.Common
{
    /// <summary>
    /// ������javascript������ ����ű�
    /// </summary>
    public class JavaScriptClass
    {
        #region  ������ֶ�


        /// <summary>
        /// ҳ����(���븳ֵ)
        /// </summary>
        Page _pg;

        public Page Pg
        {
            get {

                if (_pg != null)
                {
                    return _pg;
                }
                else
                {
                    _pg = HttpContext.Current.Handler as Page;
                    if (_pg == null)
                    {
                        throw new ArgumentException("û�д�������page����");
                    }
                  
                   return _pg;
                    
                }
            }
            set { _pg = value; }
        }

        /// <summary>
        /// ���ڲ�Ĭ�ϵ�˳�� 
        /// </summary>
        PageLoad _pl;


        /// <summary>
        /// ����JS,DOMִ��˳��
        /// </summary>
        public PageLoad Pl
        {
            get { return _pl; }
            set { _pl = value; }
        }


        /// <summary>
        /// ע��ű�������
        /// </summary>
        private Type _ctype;

        /// <summary>
        /// �ű��������
        /// </summary>
        public Type Ctype
        {
            get {
                if (_ctype == null)
                {
                    this._ctype=Pg.GetType();
                }
                return _ctype;
            }
            set { _ctype = value; }
        }



        #endregion

        #region ����Ĺ��캯��
        /// <summary>
        /// ���ù��캯�� �޷��ⲿ����
        /// </summary>
        public JavaScriptClass()
        {
            //������ֱ�ӵ��ù���!
          
        }

        /// <summary>
        /// �ⲿ���õĹ��췽�� ��Ҫ����page����
        /// </summary>
        /// <param name="page"></param>
        public JavaScriptClass(Page page):
            this(page,PageLoad.Before,page.GetType())
        {
           
           //��this����ʵ��
        }

        /// <summary>
        /// �ⲿ���õĹ��췽�� ��Ҫpage�����type���� pageloadĬ��Ϊbefore
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ctype"></param>
        public JavaScriptClass(Page page, Type ctype)
            : this(page, PageLoad.Before, ctype)
        {
            //����this�е���ʵ��
        }


        /// <summary>
        /// ��Ҫ����js�Ĺ��캯��(����ѡ��jsִ���Ⱥ�)
        /// </summary>
        /// <param name="page">��Ҫ�����page����</param>
        /// <param name="pageload">ѡ��jsִ�е��Ⱥ�˳��</param>
        public JavaScriptClass(Page page, PageLoad pageload,Type ctype)
        {
            _pl = pageload;
            _pg = page;
            _ctype = ctype;
        }




        #endregion

        #region ������д�ű�����

        /// <summary>
        /// д�ű�����
        /// </summary>
        /// <param name="script">�ű�</param>
        protected void BaseWriteScript(String script)
        {
            BaseWriteScript(script, "jsc_by_hc");
        }


        /// <summary>
        /// д�ű��������Զ���KEY
        /// </summary>
        /// <param name="script">�ű�</param>
        /// <param name="key">�Զ���KEY</param>
        protected void BaseWriteScript(String script,String key)
        {

             Pg.ClientScript.RegisterClientScriptBlock(Ctype,key, "<script type='text/javascript' language='javascript' rel='"+key+"' >" + script + "</script>");
            
      
        }



        /// <summary>
        /// ҳ���ĵ�������Ϻ���õĽű�(���ø÷���ʱ��ȷ��ҳ��û��onload����������ͻ)
        /// </summary>
        /// <param name="script"></param>
        public void WriteScript(String script,PageLoad pl)
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

        #region ����JS��ע��


        public void hcjsreg()
        {
            StringBuilder jssb=new StringBuilder();
            jssb.Append("var HC=HC||{}");
            ResourceManager rm = new ResourceManager("HC.CShare.Frame.Common.HCResource", this.GetType().Assembly);
           // Object res= rm.GetObject("jquery");  //ToString() �����ļ�������.
         

            

            BaseWriteScript(jssb.ToString(), "hc10js");
        }

        #endregion

        #region д�ű��г��õķ���

        /// <summary>
        /// ��򵥵���ʾ(Ĭ��˳��)
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        public void Alert(String msg)
        {
            this.Alert(msg, _pl);
        }



        /// <summary>
        /// �򵥵�(alert)��ʾ(�������˳��)
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <param name="pl">ִ��˳��</param>
        public void Alert(String msg, PageLoad pl)
        {

            this.WriteScript("window.alert('" + msg + "');",pl);
        }




        /// <summary>
        /// ��ʾ����ת(Ĭ��˳��)
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��תURL</param>
        public void AlertJmp(String msg, String url)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href='" + url + "';", _pl);
        }


        /// <summary>
        /// ��ʾ����ת(�ɿ���˳��)
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <param name="url">URL</param>
        /// <param name="pl">����˳��</param>
        public void AlertJmp(String msg, String url, PageLoad pl)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href='" + url + "';", pl);
        }





        /// <summary>
        /// ֱ����ת ���ĵ�����ǰ
        /// </summary>
        /// <param name="url">��Ҫ��ת��·��</param>
        public void URLJmp(String url)
        {

            this.URLJmp(url, PageLoad.Before);
        }


        /// <summary>
        /// ֱ����ת �������Ⱥ�
        /// </summary>
        /// <param name="url">��ת��URL</param>
        /// <param name="pl">ִ��˳��</param>
        public void URLJmp(String url, PageLoad pl)
        {
            this.WriteScript("window.location.href='" + url + "';", pl);
        }



        /// <summary>
        /// ��ʾ��ˢ��
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        public void AlertRefresh(String msg)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href=window.location.href;");
        }



        /// <summary>
        /// ��ʾ��ˢ��(���Կ���˳��)
        /// </summary>
        /// <param name="msg">��Ϣ</param>
        /// <param name="pl">˳��</param>
        public void AlertRefresh(String msg,PageLoad pl)
        {
            this.WriteScript("window.alert('" + msg + "');window.location.href=window.location.href;");
        }




        #endregion


    }


    #region ִ��˳���ö��

    /// <summary>
    /// ����JS�¼���ִ��˳��(Ĭ����DOM����ǰ)
    /// </summary>
    public enum PageLoad
    {
        /// <summary>
        /// ҳ�����֮ǰ
        /// </summary>
        Before=0,

        /// <summary>
        /// ҳ�����֮��(ע����onload�¼���ͻ)
        /// </summary>
        After=1
    }

    #endregion

    #region ׼������(js����)
    /***  ҳ����ص�׼������
     
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
