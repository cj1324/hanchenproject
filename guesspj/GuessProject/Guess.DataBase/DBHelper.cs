using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Guess.Common;

namespace Guess.DataBase
{
    /// <summary>
    /// 数据库操作方法
    /// </summary>
    public static class DBHelper
    {

        public static string ConnStr
        {
            get {return ConfigurationManager.ConnectionStrings["MSSQLCONN"].ToString();}
        }

        public static SqlConnection GetConn()
        {
            return (new SqlConnection(ConnStr));
             
        }

        public static SqlCommand GetCmd(string sql)
        {

            return (new SqlCommand(sql,GetConn()));
        }


        /// <summary>
        /// 把单个参数转化成参数数组
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static SqlParameter[] FixParameters(SqlParameter parameter)
        {
            SqlParameter[] parameters=new SqlParameter[1];
            parameters[0] = parameter;
            return parameters;
        }


        public static object GetSingle(string sql)
        {
            return GetSingle(sql,null);
        }

        /// <summary>
        /// 查询并获取第一行,第一列的数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object GetSingle(string sql, SqlParameter[] parameters)
        {
            object obj=null;
            SqlCommand cmd = GetCmd(sql);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                cmd.Connection.Open();
                obj = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Logo lg = new Logo();
                lg.WriteLogo(ex, "Function:GetSingle   SQL:" + sql);
            }
            finally
            {
                cmd.Connection.Close();
            }

            return obj;

        }

        public static int NonQuery(string sql)
        {
            return NonQuery(sql, null);
        }

        /// <summary>
        /// 查询受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int NonQuery(string sql, SqlParameter[] parameters)
        {
            int row = 0;
            SqlCommand cmd = GetCmd(sql);
           
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                cmd.Connection.Open();
                row = cmd.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                Logo lg = new Logo();
                lg.WriteLogo(ex, "Function:NonQuery   SQL:" + sql);
            }
            finally
            {
                cmd.Connection.Close();
            }

            return row;
        }

        public static SqlDataReader GetCursor(string sql)
        {

            return GetCursor(sql, null);
        }

        /// <summary>
        /// 查询获取游标
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetCursor(string sql, SqlParameter[] parameters)
        {
            SqlDataReader sdr = null;
            SqlCommand cmd = GetCmd(sql);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                cmd.Connection.Open();
                sdr=cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (SqlException ex)
            {
                Logo lg = new Logo();
                lg.WriteLogo(ex, "Function:GetCursor   SQL:" + sql);
            }
            if(sdr!=null&&!sdr.HasRows)
            {
                sdr.Close();
                cmd.Connection.Close();
                sdr=null;
            }


            return sdr;
        }

        public static int RunProc(string procname)
        {
          return  RunProc(procname, null);
        }

        public static int RunProc(string procname, SqlParameter[] parameters)
        {
            int ret=0;
            Object obj;
            using (SqlCommand cmd = new SqlCommand(procname, GetConn()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                SqlParameter retparam = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
                retparam.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(retparam);

                try
                {
                    cmd.Connection.Open();
                    obj = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Logo lg = new Logo();
                    lg.WriteLogo(ex, "Function:RunProc   PROCNAME:" + procname);
                }
                finally
                {
                    cmd.Connection.Close();
                }

                if (cmd.Parameters["@RETURN_VALUE"] == null || cmd.Parameters["@RETURN_VALUE"].Value==null || cmd.Parameters["@RETURN_VALUE"].Value == DBNull.Value)
                {
                    return -1;
                }

                if (!Int32.TryParse(cmd.Parameters["@RETURN_VALUE"].Value.ToString(), out ret))
                {
                    Logo lg = new Logo();
                    lg.WriteLogo(new Exception("CONVRT ERROR:  VALUE:" + cmd.Parameters["@RETURN_VALUE"].Value.ToString()));
                }

            }
            return ret;
        }

    }
}
