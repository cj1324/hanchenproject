using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HC.CShare.Frame.Common
{
    public  class HCConfig
    {
        #region 文档基础处理
        /// <summary>
        /// 获取配置文件的地址
        /// </summary>
        /// <returns></returns>
        public string GetFilePath(){return null;}



        /// <summary>
        /// 获取配置XML文档对象
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetXDoc()
        {
            XmlDocument xdoc = new XmlDocument();
            try
            {
                xdoc.Load(GetFilePath());
            }
            catch(Exception e) 
            {
                return null;
                //throw e;
            }
            return xdoc;

        }



        /// <summary>
        /// 保存修改后的文档方法
        /// </summary>
        /// <param name="xdoc"></param>
        public void SaveXDoc(XmlDocument xdoc)
        {
            try
            {
                xdoc.Save(GetFilePath());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion 

        #region 段落操作
        /// <summary>
        /// 检查段落是否存在
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool  CheckSection(XmlDocument xdoc,string name)
        {
            bool ret = false;
            //待实现
            return ret;

        }

        /// <summary>
        /// 获取段落节点
        /// </summary>
        /// <param name="xdoc">文档对象</param>
        /// <param name="name">节点名称</param>
        /// <returns>节点</returns>
        public XmlElement GetSection(XmlDocument xdoc,string name)
        {
            XmlElement retEm=null;
            //待实现
            return retEm;

        }

        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public bool CreateSection(XmlDocument xdoc)
        {

            return false;
        }


        #endregion 

        #region 项配置操作

        public string GetConfigStr(string Sec, string key,string defstr)
        {

            return String.Empty;
        }


        public bool SetConfigStr(string Sec, string key, string value)
        {
            return false;
        }




        public int GetConfigInt(string Sec, string key, int defnum)
        {

            return 0;
        }

        public bool SetConfigInt(string Sec, string key, int value)
        {

            return false;
        }


        #endregion




    }
}
