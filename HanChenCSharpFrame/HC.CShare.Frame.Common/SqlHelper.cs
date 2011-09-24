using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HC.CShare.Frame.Common
{
    /// <summary>
    /// 生成SQL语句辅助类
    /// </summary>
    public static class SqlHelper
    {

        #region 生成完整的SQL
        /// <summary>
        /// 生成SQL语句 方法
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fields">字段</param>
        /// <param name="topnum">前几条</param>
        /// <param name="where">where 条件列表</param>
        /// <returns>SQL语句</returns>
        public static string SelectTable(string tablename,string fields,string topnum,string[] where,string order)
        {
            StringBuilder sb = new StringBuilder();


            return sb.ToString();

        }
        #endregion

        #region 生成SQL的WHERE条件
        /// <summary>
        ///  关键字模糊查询 获取WHERE条件方法
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="key">关键字</param>
        /// <returns>where条件</returns>
        public static string WhereLike(string field, string key)
        {
            String ret = String.Format(" %s like '%%s%'",field,key.Replace('\'',' '));

            return ret;
        }


        /// <summary>
        /// 时间段查询
        /// </summary>
        /// <param name="field">需要查询的时间字段</param>
        /// <param name="begin">开始的日期 格式yyyy-MM-dd</param>
        /// <param name="end">结束的日期 格式同上</param>
        /// <returns>SQL中的where条件</returns>
        public static string WhereTimeRange(string field, string begin, string end)
        {


            StringBuilder sb = new StringBuilder();


            String BeginTime, EndTime;
            if (String.IsNullOrEmpty(begin))
            {
                BeginTime = "2005-01-01 00:00";
            }
            else
            {
                BeginTime = begin + " 00:00";

            }

            if (String.IsNullOrEmpty(end))
            {
                EndTime = "2050-12-30 23:59";
            }
            else
            {
                EndTime = end + " 23:59";
            }



            sb.Append(" " + field + " between convert(datetime,'" + BeginTime + "') and convert(datetime,'" + EndTime + "') ");

            return sb.ToString();
        }


        public static string WhereEquals(string field, int value)
        {
            return null;
        }

        public static string WhereEquals(string field, string value)
        {

            return null;
        }
        #endregion 

        #region SQL安全处理



        /// <summary>
        /// 检查SQL安全,不允许带单引号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CheckSqlStrSafe(string value)
        {
            if (value.IndexOf('\'') < 0)
            {
                return value;
            }

            return String.Empty;
        }




        #endregion
        
    }
}
