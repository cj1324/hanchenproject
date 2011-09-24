using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Guess.Common;

namespace Guess.DataBase
{
    public static class Util
    {

        #region 检测
        /// <summary>
        /// 根据表,字段 检测是否存在该值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckPropertiesValue(string table, string field, SqlDbType sdt,int size, object value)
        {
            string sql=string.Format("select {0} from {1} where {0}=@{0}",field,table);
            SqlParameter parameter=new SqlParameter(string.Format("@{0}",field),sdt,size);
            parameter.Value = value;
            object obj = DBHelper.GetSingle(sql, DBHelper.FixParameters(parameter));
            return (obj != null);
        }


        /// <summary>
        /// 根据表,字符,主键 查找该值否则相等
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="rowid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckPropertiesRowValue(string table, string field,SqlDbType sdt,int size,string rowidfiled,int rowid, object value)
        {
            string sql = string.Format("select {0},{2} from {1} where {2}=@{2} and {0}=@{0}", field, table, rowidfiled);
            SqlParameter[] parameters = {
                                        new SqlParameter(string.Format("@{0}",field),sdt,size),
                                        new SqlParameter(string.Format("@{0}",rowidfiled),SqlDbType.Int,4)};
            parameters[0].Value = value;
            parameters[1].Value = rowid;
            object obj = DBHelper.GetSingle(sql, parameters);
            return (obj!=null);
        }
        #endregion

        #region 修改

        /// <summary>
        /// 修改某个值, 需指明 表,字段, 主键
        /// </summary>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <param name="rowid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdatePropertiesValue(string table, string field,SqlDbType sdt,int size,string rowidfield, int rowid, object value)
        {
            string sql = string.Format("updata {0} set {1}=@{1} where {2}=@{2}", table, field, rowidfield);
            SqlParameter[] parameters ={
                        new SqlParameter(string.Format("@{0}",field),sdt,size),
                        new SqlParameter(string.Format("@{1}",rowidfield),SqlDbType.Int,4)
            };
            parameters[0].Value = value;
            parameters[1].Value = rowid;
            int row= DBHelper.NonQuery(sql,parameters);
            return (row>0);
        }
        #endregion

        #region 删除
        public static bool DeleteById(string table, string field, int id)
        {
            string sql = string.Format("delete {0} where {1}=@{1}", table, field);
            SqlParameter parameter = new SqlParameter(string.Format("@{0}", field), SqlDbType.Int, 4);
            parameter.Value = id;
            int row = DBHelper.NonQuery(sql, DBHelper.FixParameters(parameter));
            return (row > 0);
        }

        #endregion

        #region 获取

        public static object GetPropertiesValue(string table, string field,string rowidfield, int rowid)
        {
            object obj = null;
            string sql = string.Format("select {0} from {1} where {2}=@{2}",field,table,rowidfield);
            SqlParameter paramter = new SqlParameter(string.Format("@{0}", rowidfield), SqlDbType.Int, 4);
            paramter.Value = rowid;
            obj = DBHelper.GetSingle(sql, DBHelper.FixParameters(paramter));
            return obj;
        }


        public static DateTime GetNowDate()
        {
            DateTime dt = DateTime.Now;
            Object date = DBHelper.GetSingle("select getdate();");
            if (!DateTime.TryParse(date.ToString(), out dt))
            {
                Logo lg = new Logo();
                lg.WriteLogo(new Exception("日期转换失败"), "在获取数据时间并进行日期转换时失败 Function:GetNowDate");
               // throw new Exception("日期转换失败!!!!!!!!!!!");
            }

            return dt;
        }

        #endregion
    }
}
