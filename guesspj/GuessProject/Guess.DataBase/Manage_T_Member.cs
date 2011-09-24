using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Guess.Model;

namespace Guess.DataBase
{
    public class Manage_T_Member
    {
        #region 检测方法
        /// <summary>
        /// 检测属性是否重复
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="Field">字段名</param>
        /// <returns>是否存在</returns>
        private bool CheckProperties(String field, SqlDbType sdt, int size, object value)
        {

            return Util.CheckPropertiesValue("T_Member", field, sdt, size, value);
        }


        /// <summary>
        /// 检测邮箱是否已注册
        /// </summary>
        /// <param name="value">要检测的邮箱</param>
        /// <returns>是否存在</returns>
        public bool CheckEmail(string value)
        {
            return CheckProperties("F_Email", SqlDbType.NVarChar, 50, value);
        }
        #endregion

        #region 注册方法
        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="t_m">会员信息</param>
        /// <returns>会员ID</returns>
        public int RegisterMember(T_Member model)
        {
            string sql = string.Format("insert {0}({1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}) values(@{1},@{2},@{3},@{4},@{5},@{6},@{7},@{8},@{9},@{10},@{11},@{12},@{13},@{14},@{15},@{16},@{17},@{18});select @@IDENTITY",
                "T_Member",
                "F_Email",
                "F_Password",
                "F_Sex",
                "F_NickName",
                "F_Headpic",
                "F_SecurityPassWord",
                "F_Alipay",
                "F_Issues",
                "F_Answer",
                "F_InitPassWord",
                "F_Mobile",
                "F_QQ",
                "F_Level",
                "F_Gold",
                "F_Diamond",
                "F_VIP",
                "F_KEY",
                "F_Status");

            SqlParameter[] parameters = {
					
					new SqlParameter("@F_Email", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Password", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Set", SqlDbType.Bit,1),
					new SqlParameter("@F_NickName", SqlDbType.NVarChar,20),
					new SqlParameter("@F_Headpic", SqlDbType.NVarChar,200),
					new SqlParameter("@F_SecurityPassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Alipay", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Issues", SqlDbType.NVarChar,200),
					new SqlParameter("@F_Answer", SqlDbType.NVarChar,50),
					new SqlParameter("@F_InitPassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@F_QQ", SqlDbType.NVarChar,20),
					new SqlParameter("@F_Level", SqlDbType.Int,4),
					new SqlParameter("@F_Gold", SqlDbType.Int,4),
					new SqlParameter("@F_Diamond", SqlDbType.Int,4),
					new SqlParameter("@F_VIP", SqlDbType.Bit,1),
					new SqlParameter("@F_KEY", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Status", SqlDbType.Int,4)};

            parameters[0].Value = model.F_Email;
            parameters[1].Value = model.F_Password;
            parameters[2].Value = model.F_Sex;
            parameters[3].Value = model.F_NickName;
            parameters[4].Value = model.F_Headpic;
            parameters[5].Value = model.F_SecurityPassWord;
            parameters[6].Value = model.F_Alipay;
            parameters[7].Value = model.F_Issues;
            parameters[8].Value = model.F_Answer;
            parameters[9].Value = model.F_InitPassWord;
            parameters[10].Value = model.F_Mobile;
            parameters[11].Value = model.F_QQ;
            parameters[12].Value = model.F_Level;
            parameters[13].Value = model.F_Gold;
            parameters[14].Value = model.F_Diamond;
            parameters[15].Value = model.F_VIP;
            parameters[16].Value = model.F_KEY;
            parameters[17].Value = model.F_Status;

            object obj = DBHelper.GetSingle(sql, parameters);
            int id = -1;
            if (obj == null)
            {
                return id;
            }
            Int32.TryParse(obj.ToString(), out id);
            return id;
        }

        #endregion

        #region 登陆方法
        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="user">用户名(邮箱)</param>
        /// <param name="password">密码(明文)</param>
        /// <returns></returns>
        public T_Member Login(string user, string password)
        {
            T_Member t_m = null;
            string sql = "select F_Id,F_Email,F_Password,F_Sex,F_NickName,F_Headpic,F_SecurityPassWord,F_Alipay,F_Issues,F_Answer,F_InitPassWord,F_Mobile,F_QQ,F_Level,F_Gold,F_Diamond,F_VIP,F_KEY,F_Status,F_CreateDate from T_Member where F_Email=@F_Email and F_Password=@F_Password ";
            SqlParameter[] parameters = {
                                        new SqlParameter("@F_Email",SqlDbType.NVarChar,50),
                                        new SqlParameter("@F_Password",SqlDbType.NVarChar,50)
                                        };
            parameters[0].Value = user;
            parameters[1].Value = password; //这个未加密
            SqlDataReader sdr = DBHelper.GetCursor(sql, parameters);
            if (sdr == null)
            {
                return t_m;
            }
            t_m = this.GetModelBySDR(sdr);
            return t_m;
        }

        #endregion

        #region 更新方法
        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="t_m">对象数据</param>
        /// <returns>是否成功</returns>
        public bool Update(T_Member model)
        {
            string sql = string.Format("update {0} set {1}=@{1},{2}=@{2},{3}=@{3},{4}=@{4},{5}=@{5},{6}=@{6},{7}=@{7},{8}=@{8},{9}=@{9},{10}=@{10},{11}=@{11},{12}=@{12},{13}=@{13},{14}=@{14},{15}=@{15},{16}=@{16}",
                "T_Member",
                "F_Id",
                "F_Sex",
                "F_NickName",
                "F_Headpic",
                "F_SecurityPassWord",
                "F_Alipay",
                "F_Issues",
                "F_Answer",
                "F_Mobile",
                "F_QQ",
                "F_Level",
                "F_Gold",
                "F_Diamond",
                "F_VIP",
                "F_KEY",
                "F_Status");
            SqlParameter[] parameters = {
					new SqlParameter("@F_Id", SqlDbType.Int,4),
					new SqlParameter("@F_Sex", SqlDbType.Bit,1),
					new SqlParameter("@F_NickName", SqlDbType.NVarChar,20),
					new SqlParameter("@F_Headpic", SqlDbType.NVarChar,200),
					new SqlParameter("@F_SecurityPassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Alipay", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Issues", SqlDbType.NVarChar,200),
					new SqlParameter("@F_Answer", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@F_QQ", SqlDbType.NVarChar,20),
					new SqlParameter("@F_Level", SqlDbType.Int,4),
					new SqlParameter("@F_Gold", SqlDbType.Int,4),
					new SqlParameter("@F_Diamond", SqlDbType.Int,4),
					new SqlParameter("@F_VIP", SqlDbType.Bit,1),
					new SqlParameter("@F_KEY", SqlDbType.NVarChar,50),
					new SqlParameter("@F_Status", SqlDbType.Int,4)};
            parameters[0].Value = model.F_Id;
            parameters[1].Value = model.F_Sex;
            parameters[2].Value = model.F_NickName;
            parameters[3].Value = model.F_Headpic;
            parameters[4].Value = model.F_SecurityPassWord;
            parameters[5].Value = model.F_Alipay;
            parameters[6].Value = model.F_Issues;
            parameters[7].Value = model.F_Answer;
            parameters[8].Value = model.F_Mobile;
            parameters[9].Value = model.F_QQ;
            parameters[10].Value = model.F_Level;
            parameters[11].Value = model.F_Gold;
            parameters[12].Value = model.F_Diamond;
            parameters[13].Value = model.F_VIP;
            parameters[14].Value = model.F_KEY;
            parameters[15].Value = model.F_Status;
            int row = DBHelper.NonQuery(sql, parameters);
            return (row > 0);
        }


        /// <summary>
        /// 修改某个字段的属性
        /// </summary>
        /// <param name="value"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool UpdateProperties(object value, string field)
        {

            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdatePassword(string value)
        {

            return UpdateProperties(value, "F_Password");
        }

        #endregion

        #region 获取列表

        public List<T_Member> GetList(string where, SqlParameter[] parameters)
        {
            string sql = string.Format("select F_Id,F_Email,F_Password,F_Sex,F_NickName,F_Headpic,F_SecurityPassWord,F_Alipay,F_Issues,F_Answer,F_InitPassWord,F_Mobile,F_QQ,F_Level,F_Gold,F_Diamond,F_VIP,F_KEY,F_Status,F_CreateDate from T_Member where {1}", where);

            SqlDataReader sdr = DBHelper.GetCursor(sql, parameters);
            if (sdr == null)
            {
                return null;
            }
            List<T_Member> list = new List<T_Member>();
            while (true)
            {
                T_Member t_m = GetModelBySDR(sdr);
                if (t_m != null)
                {
                    list.Add(t_m);
                }
                else
                {
                    sdr.Close();
                    return list;
                }
            }
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 根据sqldatareader进行获取模型
        /// </summary>
        /// <param name="sdr"></param>
        /// <returns></returns>
        public T_Member GetModelBySDR(SqlDataReader sdr)
        {
            T_Member t_m = null;
            if (sdr.Read())
            {
                t_m = new T_Member();
                t_m.F_Id = sdr.GetInt32(0);
                t_m.F_Email = sdr.GetString(1);
                t_m.F_Password = sdr.GetString(2);
                t_m.F_Sex = sdr.GetBoolean(3);
                t_m.F_NickName = sdr.GetString(4);
                t_m.F_Headpic = sdr.GetString(5);
                t_m.F_SecurityPassWord = sdr.GetString(6);
                t_m.F_Alipay = sdr.GetString(7);
                t_m.F_Issues = sdr.GetString(8);
                t_m.F_Answer = sdr.GetString(9);
                t_m.F_InitPassWord = sdr.GetString(10);
                t_m.F_Mobile = sdr.GetString(11);
                t_m.F_QQ = sdr.GetString(12);
                t_m.F_Level = sdr.GetInt32(13);
                t_m.F_Gold = sdr.GetInt32(14);
                t_m.F_Diamond = sdr.GetInt32(15);
                t_m.F_VIP = sdr.GetBoolean(16);
                t_m.F_KEY = sdr.GetString(17);
                t_m.F_Status = sdr.GetInt32(18);
                t_m.F_CreateDate = sdr.GetDateTime(19);
            }
            return t_m;
        }
        #endregion
    }
}
